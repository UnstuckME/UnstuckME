using System;
using System.Collections.Generic;
using System.Linq;
using UnstuckMEServer;
using UnstuckME_Classes;

namespace UnstuckMEInterfaces
{
    public partial class UnstuckMEService : IUnstuckMEService, IUnstuckMEServer, IUnstuckMEFileStream
    {
        /// <summary>
        /// Returns the reviews submitted by a specific user as a student.
        /// </summary>
        /// <param name="userID">The unique identifier of the account holder.</param>
        /// <param name="minstarrank">The minimum star ranking to see reviews. This parameter is optional, with a default value of 0.</param>
        /// <returns>A list containing all the reviews submitted by the user specified as a student.</returns>
        public List<UnstuckMEReview> GetUserStudentReviews(int userID, float minstarrank = 0)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var studentReviews = db.GetUserStudentReviews(userID, minstarrank);

                List<UnstuckMEReview> studentReviewList = new List<UnstuckMEReview>();
                foreach (var review in studentReviews)
                {
                    UnstuckMEReview usReview = new UnstuckMEReview()
                    {
                        ReviewID = review.ReviewID,
                        StickerID = review.StickerID,
                        ReviewerID = review.ReviewerID,
                        StarRanking = (float)review.StarRanking,
                        Description = review.Description
                    };

                    studentReviewList.Add(usReview);
                }

                return studentReviewList;
            }
        }

        /// <summary>
        /// Returns the reviews submitted by a specific user as a tutor.
        /// </summary>
        /// <param name="userID">The unique identifier of the account holder.</param>
        /// <param name="minstarrank">The minimum star ranking to see reviews. This parameter is optional, with a default value of 0.</param>
        /// <returns>A list containing all the reviews submitted by the user specified as a tutor.</returns>
        public List<UnstuckMEReview> GetUserTutorReviews(int userID, float minstarrank = 0)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var tutorReviews = db.GetUserTutorReviews(userID, minstarrank);

                List<UnstuckMEReview> tutorReviewList = new List<UnstuckMEReview>();
                foreach (var review in tutorReviews)
                {
                    UnstuckMEReview usReview = new UnstuckMEReview()
                    {
                        ReviewID = review.ReviewID,
                        StickerID = review.StickerID,
                        ReviewerID = review.ReviewerID,
                        StarRanking = (review.StarRanking.HasValue) ? (float)review.StarRanking.Value : 0,
                        Description = review.Description
                    };

                    tutorReviewList.Add(usReview);
                }

                return tutorReviewList;
            }
        }

        /// <summary>
        /// Submits a review to the database. Finds the other user associated with the sticker and makes them submit
        /// a review if they are online, otherwise adds it to the _ReviewList queue so they can submit it when they
        /// next log on.
        /// </summary>
        /// <param name="stickerID">The unique identifier of the sticker associated with the review.</param>
        /// <param name="reviewerID">The unique identifier of the user submitting the review.</param>
        /// <param name="starRanking">The rating given to the user being reviewed.</param>
        /// <param name="description">The description of the review.</param>
        /// <param name="isAStudent">True if the user being reviewed is a student, false otherwise.</param>
        /// <returns>Returns 0 if the review was created successfully, 1 if unsuccessful.</returns>
        public int CreateReview(int stickerID, int reviewerID, double starRanking, string description, bool isAStudent)
        {
            try
            {
                Nullable<int> retVal = 0;
                Nullable<int> reviewedID = 0;

                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    retVal = db.CreateReview(stickerID, reviewerID, starRanking, description).First();

                    if (retVal.HasValue && retVal.Value != -1)
                    {
                        if (isAStudent)// the person being reviewed is a student
                        {
                            reviewedID = (from I in db.Stickers
                                          where I.TutorID == reviewerID
                                          select I.StudentID).First();

                            db.AddStudentStarRankToUser(reviewedID.Value, starRanking);
                        }
                        else // the person being reviewed is a tutor
                        {
                            reviewedID = (from I in db.Stickers
                                          where I.StudentID == reviewerID
                                          select I.TutorID).First();

                            db.AddTutorStarRankToUser(reviewedID.Value, starRanking);
                        }

                        var Reviews = from r in db.Reviews
                                      where r.StickerID == stickerID
                                      select r.StickerID;

                        if (Reviews.Count() <= 1)
                        {
                            bool found = false;
                            foreach (var client in _connectedClients)
                            {
                                if (client.Key == reviewedID.Value)
                                {
                                    if (isAStudent)
                                        client.Value.connection.CreateReviewAsTutor(stickerID);
                                    else
                                        client.Value.connection.CreateReviewAsStudent(stickerID);

                                    found = true;   //other sticker member is online
                                    break;
                                }
                            }

                            if (!found)     //other sticker member is not online
                                _ReviewList.Enqueue(stickerID);     //add sticker to queue so they can submit a review when they log on next
                        }
                        else
                            db.MarkStickerAsResolved(stickerID);    //all reviews have been submitted, mark as resolved
                    }
                }

                return retVal.Value;
            }
            catch (Exception)
            {
                return -1; //If Failure to create review
            }
        }

        /// <summary>
        /// When logging on, checks for any stickers that need reviews. This will only occur if the other member of the
        /// sticker has submitted a review and this user was not online when this occurred.
        /// </summary>
        /// <param name="userID">The unique identifier of the user who is checking for reviews.</param>
        /// <returns>Contains the StickerID and true if they are the student, false if they are the tutor. If there is no sticker, returns 0 for the sticker ID.</returns>
        public KeyValuePair<int, bool> CheckForReviews(int userID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                if (_ReviewList.Count != 0)
                {
                    foreach (int Review_StickerID in _ReviewList)
                    {
                        var reviews = (from s in db.Stickers
                                       join r in db.Reviews on s.StickerID equals r.StickerID
                                       where s.StickerID == Review_StickerID
                                       select new { s.StickerID, s.TutorID, s.StudentID, r.ReviewerID });

                        foreach (var rev in reviews)
                        {
                            if (userID == rev.TutorID && userID != rev.ReviewerID)
                            {
                                int temp = rev.StickerID;
                                _ReviewList.TryDequeue(out temp);
                                return new KeyValuePair<int, bool>(rev.StickerID, false);
                            }
                            else if (userID == rev.StudentID && userID != rev.ReviewerID)
                            {
                                int temp = rev.StickerID;
                                _ReviewList.TryDequeue(out temp);
                                return new KeyValuePair<int, bool>(rev.StickerID, true);
                            }
                        }
                    }
                }
            }

            return new KeyValuePair<int, bool>(0, false);
        }
    }
}