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
        /// Invoked when a tutor accepts a sticker. Updates the TutorID associated with that sticker.
        /// </summary>
        /// <param name="userID">The unique identifier of the user who has accepted the sticker.</param>
        /// <param name="stickerID">The unique identifier fo the sticker that has been accepted.</param>
        public void AcceptSticker(int tutorID, int stickerID)
        {
            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    db.UpdateTutorIDByTutorIDAndStickerID(tutorID, stickerID);
                    DateTime time;
                    _ActiveStickers.TryRemove(stickerID, out time);
                    var tutors = db.GetUsersThatCanTutorASticker(stickerID);

                    foreach (var client in _connectedClients)
                    {
                        if (client.Key != tutorID && tutors.Contains(client.Key))
                            client.Value.connection.RemoveGUISticker(stickerID);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Gets the stickers that have been accepted by a tutor and marked as resolved.
        /// </summary>
        /// <param name="minstarrank">The minimum star ranking required in order to see the sticker. This parameter is optional, with a default value of 0.</param>
        /// <param name="organizationID">The unique identifer of the organization to filter. This parameter is optional, with a default value of null.</param>
        /// <param name="userID">The unique identifer of the account that submitted the stickers. This parameter is optional, with a default value of null.</param>
        /// <param name="classID">The unique identifier of the class to filter the results through. This parameter is optional, with a default value of null.</param>
        /// <returns>A list of stickers that have tutors and marked as resolved.</returns>
        public List<UnstuckMESticker> GetResolvedStickers(double minstarrank = 0, Nullable<int> organizationID = null, Nullable<int> userID = null, Nullable<int> classID = null)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var stickers = db.GetResolvedStickers(minstarrank, organizationID, userID, classID);

                List<UnstuckMESticker> stickerList = new List<UnstuckMESticker>();
                foreach (var sticker in stickers)
                {
                    UnstuckMESticker usSticker = new UnstuckMESticker()
                    {
                        StickerID = sticker.StickerID,
                        ClassID = sticker.ClassID,
                        StudentID = sticker.StudentID,
                        ProblemDescription = sticker.ProblemDescription,
                        MinimumStarRanking = (float)sticker.MinimumStarRanking,
                        SubmitTime = sticker.SubmitTime,
                        Timeout = sticker.Timeout// - DateTime.Now).TotalSeconds;
                    };
                }

                return stickerList;
            }
        }

        /// <summary>
        /// Gets the stickers that have not been accepted by a tutor and surpassed the timeout date.
        /// </summary>
        /// <param name="minstarrank">The minimum star ranking required in order to see the sticker. This parameter is optional, with a default value of 0.</param>
        /// <param name="organizationID">The unique identifer of the organization to filter. This parameter is optional, with a default value of null.</param>
        /// <param name="userID">The unique identifer of the account that submitted the stickers. This parameter is optional, with a default value of null.</param>
        /// <param name="classID">The unique identifier of the class to filter the results through. This parameter is optional, with a default value of null.</param>
        /// <returns>A list of stickers that have not been accepted by a tutor and surpassed the timeout date.</returns>
        public List<UnstuckMESticker> GetTimedOutStickers(double minstarrank = 0, Nullable<int> organizationID = null, Nullable<int> userID = null, Nullable<int> classID = null)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var stickers = db.GetTimedOutStickers(minstarrank, organizationID, userID, classID);

                List<UnstuckMESticker> stickerList = new List<UnstuckMESticker>();
                foreach (var sticker in stickers)
                {
                    UnstuckMESticker usSticker = new UnstuckMESticker()
                    {
                        StickerID = sticker.StickerID,
                        ClassID = sticker.ClassID,
                        StudentID = sticker.StudentID,
                        ProblemDescription = sticker.ProblemDescription,
                        MinimumStarRanking = (float)sticker.MinimumStarRanking,
                        SubmitTime = sticker.SubmitTime,
                        Timeout = sticker.Timeout// - DateTime.Now).TotalSeconds;
                    };
                }

                return stickerList;
            }
        }

        /// <summary>
        /// Gets the stickers submitted by a user, regardless if they are resolved or active.
        /// </summary>
        /// <param name="userID">The unique identifer of the account.</param>
        /// <param name="organizationID">The unique identifer of the organization to filter. This parameter is optional, with a default value of null.</param>
        /// <param name="minstarrank">The minimum star ranking required in order to see the sticker. This parameter is optional, with a default value of 0.</param>
        /// <param name="classID">The unique identifier of the class to filter the results through. This parameter is optional, with a default value of null.</param>
        /// <returns>A list of stickers that have been submitted by a specific user that matches the filtering criteria.</returns>
        public List<UnstuckMESticker> GetUserSubmittedStickers(int userID, Nullable<int> organizationID = null, float minstarrank = 0, Nullable<int> classID = null)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var userStickers = db.GetUserSubmittedStickers(userID, organizationID, minstarrank, classID);

                List<UnstuckMESticker> stickerList = new List<UnstuckMESticker>();
                foreach (var sticker in userStickers)
                {
                    UnstuckMESticker usSticker = new UnstuckMESticker()
                    {
                        StickerID = sticker.StickerID,
                        ProblemDescription = sticker.ProblemDescription,
                        ClassID = sticker.ClassID,
                        StudentID = sticker.StudentID,
                        TutorID = sticker.TutorID ?? 1,
                        MinimumStarRanking = (sticker.MinimumStarRanking.HasValue) ? (float)sticker.MinimumStarRanking : 0,
                        SubmitTime = sticker.SubmitTime,
                        Timeout = sticker.Timeout// - DateTime.Now).TotalSeconds;
                    };
                    stickerList.Add(usSticker);
                }

                return stickerList;
            }
        }

        /// <summary>
        /// Gets the stickers a user has tutored, regardless if they are resolved or active.
        /// </summary>
        /// <param name="userID">The unique identifer of the account.</param>
        /// <param name="organizationID">The unique identifer of the organization to filter. This parameter is optional, with a default value of null.</param>
        /// <param name="minstarrank">The minimum star ranking required in order to see the sticker. This parameter is optional, with a default value of 0.</param>
        /// <param name="classID">The unique identifier of the class to filter the results through. This parameter is optional, with a default value of null.</param>
        /// <returns>A list of stickers that a specific user has tutored that matches the filtering criteria.</returns>
        public List<UnstuckMESticker> GetUserTutoredStickers(int userID, Nullable<int> organizationID = null, float minstarrank = 0, Nullable<int> classID = null)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var userStickers = db.GetUserTutoredStickers(userID, organizationID, minstarrank, classID);

                List<UnstuckMESticker> stickerList = new List<UnstuckMESticker>();
                foreach (var sticker in userStickers)
                {
                    UnstuckMESticker usSticker = new UnstuckMESticker()
                    {
                        StickerID = sticker.StickerID,
                        ProblemDescription = sticker.ProblemDescription,
                        ClassID = sticker.ClassID,
                        StudentID = sticker.StudentID,
                        TutorID = sticker.TutorID ?? 1,
                        MinimumStarRanking = (sticker.MinimumStarRanking.HasValue) ? (float)sticker.MinimumStarRanking : 0,
                        SubmitTime = sticker.SubmitTime,
                        Timeout = sticker.Timeout// - DateTime.Now).TotalSeconds;
                    };
                    stickerList.Add(usSticker);
                }

                return stickerList;
            }
        }

        /// <summary>
        /// Gets the stickers available to tutor. This is currently untested, though it should work.
        /// </summary>
        /// <param name="caller">The unqiue identifier of the caller of the function.</param>
        /// <param name="organizationID">The unique identifier of the of the organization to filter. This parameter is optional, with a default value of null.</param>
        /// <param name="minstarrank">The minimum star ranking required in order to see the sticker. This should be the callee's student star ranking. This paramter is optional, with a default value of 0.</param>
        /// <param name="userID">The unique identifier of the account to filter. This paramter is optional, with a default value of null.</param>
        /// <param name="classID">the unique identifier of the class to filter the results through. This parameter is optional, with a default value of null.</param>
        /// <returns>A list of stickers available to tutor that meets the filtering criteria.</returns>
        public List<UnstuckMEAvailableSticker> GetActiveStickers(int caller, Nullable<int> organizationID = null, float minstarrank = 0, Nullable<int> userID = null, Nullable<int> classID = null)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var userStickers = db.GetActiveStickers(caller, organizationID, minstarrank, userID, classID);

                List<UnstuckMEAvailableSticker> stickerList = new List<UnstuckMEAvailableSticker>();

                foreach (var sticker in userStickers)
                {
                    UnstuckMEAvailableSticker usSticker = new UnstuckMEAvailableSticker()
                    {
                        StickerID = (int)sticker.StickerID,
                        ProblemDescription = sticker.ProblemDescription,
                        ClassID = (int)sticker.ClassID,
                        CourseCode = sticker.CourseCode,
                        CourseName = sticker.CourseName,
                        CourseNumber = (short)sticker.CourseNumber,
                        StudentID = (int)sticker.StudentID,
                        //TutorID = sticker.TutorID ?? new int?(),
                        StudentRanking = (sticker.MinimumStarRanking.HasValue) ? (double)sticker.MinimumStarRanking : 0,
                        //SubmitTime = (DateTime)sticker.SubmitTime,
                        Timeout = (DateTime)sticker.Timeout
                    };
                    stickerList.Add(usSticker);
                }

                return stickerList;
            }
        }

        /// <summary>
        /// Submits a new sticker to the database and associates it with any specified tutoring organizations. Queues the sticker to be sent to qualified online users.
        /// </summary>
        /// <param name="newSticker">The new sticker.</param>
        /// <returns>The new sticker's unique identifier if it was submitted successfully, -1 if not.</returns>
        public int SubmitSticker(UnstuckMEBigSticker newSticker)
        {
            try
            {
                int retstickerID = 0;
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var stickerID = db.CreateSticker(newSticker.ProblemDescription, newSticker.Class.ClassID, newSticker.StudentID, newSticker.MinimumStarRanking, newSticker.TimeoutInt).First();

                    if (stickerID.Value == 0)
                        throw new Exception("Create Sticker Failed, Returned sticker ID = 0");

                    retstickerID = stickerID.Value;

                    if (newSticker.AttachedOrganizations.Count != 0)
                    {
                        foreach (int orgID in newSticker.AttachedOrganizations)
                            db.AddOrgToSticker(retstickerID, orgID);
                    }
                    newSticker.StickerID = retstickerID;
                    _StickerList.Enqueue(newSticker);
                    _ActiveStickers.TryAdd(newSticker.StickerID, newSticker.Timeout);
                }
                return retstickerID;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sticker Submit Error: " + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// Associates a sticker with a chat once the sticker has been acccepted.
        /// </summary>
        /// <param name="chatID">The unique identifier of the chat.</param>
        /// <param name="stickerID">The unique identifier of the sticker.</param>
        public void AddChatToSticker(int chatID, int stickerID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.UpdateStickerByChatID(chatID, stickerID);
            }
        }

        /// <summary>
        /// Gets the stickers when first logging in.
        /// </summary>
        /// <param name="userID">The unique identifier of a specific user.</param>
        /// <returns>A list of available stickers, containing the unique identifier of the class the sticker is associated with
        /// and all the information for that class, the description, the unique identifier of the sticker, the unique identifier
        /// of the user who submitted the sticker, the student ranking of that user, and the timeout date.</returns>
        public List<UnstuckMEAvailableSticker> InitialAvailableStickerPull(int userID)
        {
            try
            {
                List<UnstuckMEAvailableSticker> stickerList = new List<UnstuckMEAvailableSticker>();
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var dbStickers = db.InitialStickerPull(userID);

                    foreach (var sticker in dbStickers)
                    {
                        UnstuckMEAvailableSticker temp = new UnstuckMEAvailableSticker()
                        {
                            ClassID = sticker.ClassID.Value,
                            CourseCode = sticker.CourseCode,
                            CourseName = sticker.CourseName,
                            CourseNumber = sticker.CourseNumber.Value,
                            ProblemDescription = sticker.ProblemDescription,
                            StickerID = sticker.StickerID.Value,
                            StudentID = sticker.StudentID.Value,
                            StudentRanking = sticker.StudentRanking.Value,
                            Timeout = sticker.Timeout.Value
                        };
                        stickerList.Add(temp);
                    }

                }
                return stickerList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Deletes a sticker from the database and updates the client interfaces of tutors who are eligible to
        /// see that sticker. This should only be done if the sticker does not already have a tutor.
        /// </summary>
        /// <param name="stickerID">The unique identifier of the sticker to delete.</param>
        /// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        public int DeleteSticker(int stickerID)
        {
            int retVal = -1;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    db.DeleteStickerByStickerID(stickerID);
                    var tutors = db.GetUsersThatCanTutorASticker(stickerID);

                    foreach (var tutor in tutors)
                    {
                        if (_connectedClients.ContainsKey(tutor.Value))
                            _connectedClients[tutor.Value].connection.RemoveGUISticker(stickerID);
                    }

                    retVal = 0;
                }
            }
            catch (Exception)
            { }

            return retVal;
        }

        /// <summary>
        /// Removes the tutor associated with the sticker given by <paramref name="stickerID"/> and sends it out
        /// to online tutors who are eligible to see it.
        /// </summary>
        /// <param name="stickerID">The unique identifier of the sticker to be relabeled as active.</param>
        /// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        public int RemoveTutorFromSticker(int stickerID)
        {
            int retVal = -1;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    db.UpdateTutorIDByTutorIDAndStickerID(null, stickerID); //removes the tutor from the sticker
                    var sticker = db.GetStickerInfo(stickerID).First();

                    //not sure if this will work
                    UnstuckMEBigSticker bigsticker = new UnstuckMEBigSticker()
                    {
                        StickerID = stickerID,
                        ProblemDescription = sticker.ProblemDescription,
                        StudentID = sticker.StudentID,
                        TutorID = sticker.TutorID.Value,
                        MinimumStarRanking = sticker.MinimumStarRanking.Value,
                        Class = new UserClass()
                        {
                            ClassID = sticker.ClassID,
                            CourseCode = sticker.CourseCode,
                            CourseName = sticker.CourseName,
                            CourseNumber = sticker.CourseNumber
                        },
                        SubmitTime = sticker.SubmitTime,
                        Timeout = sticker.Timeout
                    };

                    _StickerList.Enqueue(bigsticker);
                    _ActiveStickers.TryAdd(bigsticker.StickerID, bigsticker.Timeout);

                    retVal = 0;
                }
            }
            catch (Exception)
            { }

            return retVal;
        }
        /// <summary>
        /// returns a single sticker by ID, sticker currently only contains coursename and problem description
        /// </summary>
        /// <param name="stickerID"></param>
        /// <returns></returns>
        public UnstuckMESticker GetSticker(int stickerID)
        {
            UnstuckMESticker Rsticker = new UnstuckMESticker();
            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var sticker = db.GetStickerInfo(stickerID).First();


                    Rsticker.CourseName = sticker.CourseName;
                    Rsticker.ProblemDescription = sticker.ProblemDescription;

                }
            }
            catch (Exception)
            {}
            return Rsticker;
        }
    }
}