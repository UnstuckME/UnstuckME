using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                        StarRanking = review.StarRanking.HasValue ? (float)review.StarRanking : 0F,
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
                        StarRanking = review.StarRanking.HasValue ? (float)review.StarRanking.Value : 0,
                        Description = review.Description
                    };

                    tutorReviewList.Add(usReview);
                }

                return tutorReviewList;
            }
        }

        /// <summary>
        /// Gets the review that have been submitted of a particular user to display on their Profile Page.
        /// </summary>
        /// <param name="userID">The unique identifier of the user who has reviews.</param>
        /// <returns>A list containing all the reviews submitted on the user.</returns>
        public List<UnstuckMEReview> GetReviewsOfUser(int userID)
        {
            List<UnstuckMEReview> reviewsOfUser = new List<UnstuckMEReview>();

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var reviews = db.GetReviewsOfUser(userID);

                    foreach (var review in reviews)
                    {
                        UnstuckMEReview newReview = new UnstuckMEReview()
                        {
                            ReviewID = review.ReviewID,
                            StickerID = review.StickerID,
                            StarRanking = review.StarRanking.HasValue ? (float) review.StarRanking : 0F,
                            Description = review.Description
                        };

                        reviewsOfUser.Add(newReview);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to get reviews of user " + userID);
            }

            return reviewsOfUser;
        }

        /// <summary>
        /// Gets a list of Review IDs that have been reported by a user.
        /// </summary>
        /// <param name="userID">The unique identifer of the user who has submitted reports.</param>
        /// <returns>A list of integers containing the unique identifiers of the reviews that
        /// have been reported by the user.</returns>
        public List<int> GetReportedReviewIDs(int userID)
        {
            List<int> reportedReviewIDs = new List<int>();

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    //var reviewIDs = db.GetReportedReviewIDS(userID);
                    var reviewIDs = from r in db.Reviews join rp in db.Reports
                                    on r.ReviewID equals rp.ReviewID
                                    where rp.FlaggerID == userID
                                    select rp.ReviewID;

                    foreach (int reviewID in reviewIDs)
                        reportedReviewIDs.Add(reviewID);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to get review IDs that have been reported by user " + userID);
            }

            return reportedReviewIDs;
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
        public async Task<int> CreateReview(int stickerID, int reviewerID, double starRanking, string description, bool isAStudent)
        {
            int? newReviewID = -1;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    newReviewID = db.CreateReview(stickerID, reviewerID, starRanking, description).First();

                    if (newReviewID.HasValue && newReviewID.Value != -1)
                    {
                        int? reviewedID = 0;

                        if (isAStudent) // the person being reviewed is a student
                        {
                            reviewedID = (from I in db.Stickers
                                          where I.TutorID == reviewerID && I.StickerID == stickerID
                                          select I.StudentID).First();

                            if (reviewedID.HasValue)
                                db.AddStudentStarRankToUser(reviewedID.Value, starRanking);
                        }
                        else // the person being reviewed is a tutor
                        {
                            reviewedID = (from I in db.Stickers
                                          where I.StudentID == reviewerID && I.StickerID == stickerID
                                          select I.TutorID).First();

                            if (reviewedID.HasValue)
                                db.AddTutorStarRankToUser(reviewedID.Value, starRanking);
                        }

                        var reviewCount = (from r in db.Reviews
                                      where r.StickerID == stickerID
                                      select r.StickerID).Count();

                        if (reviewCount == 2)
                            db.MarkStickerAsResolved(stickerID); //all reviews have been submitted, mark as resolved

                        await Task.Factory.StartNew(() => AsyncSendReviewToUser(new UnstuckMEReview
                        {
                            StickerID = stickerID,
                            ReviewID = newReviewID.Value,
                            Description = description,
                            ReviewerID = reviewerID,
                            StarRanking = (float)starRanking
                        }, reviewedID ?? 0, reviewCount == 2 ? (bool?)null : isAStudent));
                    }
                    else
                        newReviewID = -1;
                }

                return newReviewID.Value;
            }
            catch (Exception)
            {
                return newReviewID.Value; //If Failure to create review
            }
        }

        /// <summary>
        /// Asynchronously sends the newly submitted review to any online users or enqueues it for them to submit
        /// when they next log in.
        /// </summary>
        /// <param name="newReview">The unique identifier of the newly created review to send to associated users.</param>
        /// <param name="reviewedID">The unique identifier of the user who needs the review.</param>
        /// <param name="needsAnotherReview">Determines if all reviews have been submitted. If not and the user
        /// associated with the sticker is online, server will request a review from them. If the other user is
        /// not online, then the review will be enqueued so the next time they are online, they can submit one.</param>
        private void AsyncSendReviewToUser(UnstuckMEReview newReview, int reviewedID, bool? needsAnotherReview)
        {
            bool found = false;

            foreach (var client in _connectedClients)
            {
                if (client.Key == reviewedID)
                {
                    UserInfo user = GetUserInfo(client.Key, null);
                    //send the review to put in their Profile page and update their student/tutor ratings
                    client.Value.Connection.RecieveReviewAndUpdateRatings(newReview, user.AverageTutorRank, user.AverageStudentRank);

                    if (needsAnotherReview.HasValue)
                    {
                        if (needsAnotherReview.Value)
                            client.Value.Connection.CreateReviewAsTutor(newReview.StickerID);
                        else
                            client.Value.Connection.CreateReviewAsStudent(newReview.StickerID);

                        found = true;
                        break;
                    }
                }
            }

            if (!found) //other sticker member is not online
                _reviewList.Enqueue(newReview.StickerID); //add sticker to queue so they can submit a review when they log on next
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
                if (_reviewList.Count != 0)
                {
                    foreach (int reviewSticker in _reviewList)
                    {
                        var reviews = from s in db.Stickers
                                      join r in db.Reviews on s.StickerID equals r.StickerID
                                      where s.StickerID == reviewSticker
                                      select new { s.StickerID, s.TutorID, s.StudentID, r.ReviewerID };

                        foreach (var rev in reviews)
                        {
                            int temp = 0;
                            if (userID == rev.TutorID && userID != rev.ReviewerID)
                            {
                                _reviewList.TryDequeue(out temp);
                                return new KeyValuePair<int, bool>(rev.StickerID, false);
                            }
                            if (userID == rev.StudentID && userID != rev.ReviewerID)
                            {
                                _reviewList.TryDequeue(out temp);
                                return new KeyValuePair<int, bool>(rev.StickerID, true);
                            }
                        }
                    }
                }
            }

            return new KeyValuePair<int, bool>(0, false);
        }

        /// <summary>
        /// Determines if a sticker has been reviewed.
        /// </summary>
        /// <param name="stickerID">The unique identifier of the sticker to find reviews of.</param>
        /// <param name="userID">The unique identifier of the user who may have submitted a review.</param>
        /// <returns>Returns true if user <paramref name="userID"/> has submitted a review on sticker <paramref name="stickerID"/>, false if not.</returns>
        public bool BeenReviewed(int stickerID, int userID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                return (from r in db.Reviews where r.StickerID == stickerID && r.ReviewerID == userID select r).Any();
            }
        }

        public List<int> GetAllReportedReviewIDs()
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var thing = (from r in db.Reports select r.ReportID);
                return thing.AsEnumerable().ToList();
            }
        }


        public void MarkReportedReviewAsResolved(bool acceptable, int reviewID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                if (acceptable)
                {
                    var target = from r in db.Reports where r.ReportID == reviewID select r;
    
                    foreach (var item in target)
                    {
                        db.Reports.Remove(item);
                    }

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        
                    }
                }
                else
                {
                    Report target = (from r in db.Reports where r.ReportID == reviewID select r).SingleOrDefault();
                    int reviewToChange = target.ReviewID;

                    Review ToChange = (from r in db.Reviews where r.ReviewID == reviewToChange select r).SingleOrDefault();
                    ToChange.Description = "This review fell into an open man hole and was eaten by a sewer gator...";
                    db.Reports.Remove(target);
                    db.SaveChanges();
                    
                }
            }
        }
        public UnstuckMEReview GetReportedReview(int ReportID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                int target = (from r in db.Reports where r.ReportID == ReportID  select r.ReviewID).Single();
                var review = (from r in db.Reviews where r.ReviewID == target select r).Single();
                UnstuckMEReview rval = new UnstuckMEReview();
                rval.Description = review.Description;
                rval.ReviewerID = review.ReviewerID;
                rval.ReviewID = review.ReviewID;
                //rval.StarRanking = review.StarRanking;
                rval.StickerID = review.StickerID;
                return rval;
            }
        }
        public string GetReportDescription(int ReportID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                return (from r in db.Reports where r.ReportID == ReportID select r.ReportDescription).Single();
            }
        }
    }
}