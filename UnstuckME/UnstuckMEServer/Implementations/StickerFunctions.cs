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
        /// Invoked when a tutor accepts a sticker. Updates the TutorID associated with that sticker.
        /// </summary>
        /// <param name="tutorID">The unique identifier of the user who has accepted the sticker.</param>
        /// <param name="stickerID">The unique identifier of the sticker that has been accepted.</param>
        /// <param name="studentID">The unique identifier of the user who submitted the sticker.</param>
        public async void AcceptSticker(int tutorID, int studentID, int stickerID)
        {
            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    db.UpdateTutorIDByTutorIDAndStickerID(tutorID, stickerID);
                }

                await Task.Factory.StartNew(() => AsyncAcceptSticker(tutorID, studentID, stickerID));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Asyncronously gets all the online users who can see the sticker that has been accepted and
        /// removes it from their interface.
        /// </summary>
        /// <param name="tutorID">The unique identifier of the user who has accepted the sticker.</param>
        /// <param name="stickerID">The unique identifier of the sticker that has been accepted.</param>
        /// <param name="studentID">The unique identifier of the user who submitted the sticker.</param>
        private void AsyncAcceptSticker(int tutorID, int studentID, int stickerID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var tutors = db.GetUsersThatCanTutorASticker(stickerID);

                foreach (var client in _connectedClients)
                {
                    if (client.Key != tutorID && tutors.Contains(client.Key))
                        client.Value.Connection.RemoveGUISticker(stickerID);
                    if (client.Key == studentID)
                        client.Value.Connection.UpdateStickerStatus(StickerStatus.Accepted, stickerID);
                }
            }
        }

        /// <summary>
        /// Gets the stickers that have been accepted by a tutor and marked as resolved.
        /// </summary>
        /// <param name="userID">The unique identifer of the account that submitted the stickers.</param>
        /// <returns>A list of stickers that have tutors and marked as resolved.</returns>
        public List<UnstuckMESticker> GetStickerHistory(int userID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                List<UnstuckMESticker> stickerList = new List<UnstuckMESticker>();

                /***    replace the stored procedure call with whatever you implement here     ***/
                using (var stickers = db.GetAllResolvedOrTimedOutStickers(userID, null, null, null))
                {
                    foreach (var sticker in stickers)
                    {
                        UnstuckMESticker usSticker = new UnstuckMESticker
                        {
                            StickerID = sticker.StickerID,
                            ClassID = sticker.ClassID,
                            StudentID = sticker.StudentID,
                            ProblemDescription = sticker.ProblemDescription,
                            MinimumStarRanking = sticker.MinimumStarRanking.HasValue ? (float)sticker.MinimumStarRanking : 0,
                            SubmitTime = sticker.SubmitTime,
                            Timeout = sticker.Timeout
                        };

                        stickerList.Add(usSticker);
                    }
                }

                return stickerList;
            }
        }

        /// <summary>
        /// Gets the stickers submitted by a user, regardless if they are resolved or active.
        /// </summary>
        /// <param name="userID">The unique identifer of the account.</param>
        /// <param name="minstarrank">The minimum star ranking required in order to see the sticker. This parameter is optional, with a default value of 0.</param>
        /// <param name="classID">The unique identifier of the class to filter the results through. This parameter is optional, with a default value of null.</param>
        /// <returns>A list of stickers that have been submitted by a specific user that matches the filtering criteria.</returns>
        public List<UnstuckMESticker> GetUserSubmittedStickers(int userID, float minstarrank = 0, int classID = 0)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                List<UnstuckMESticker> stickerList = new List<UnstuckMESticker>();

                using (var userStickers = db.GetUserSubmittedStickers(userID, minstarrank, classID))
                {
                    foreach (var sticker in userStickers)
                    {
                        UnstuckMESticker usSticker = new UnstuckMESticker
                        {
                            StickerID = sticker.StickerID ?? 0,
                            ProblemDescription = sticker.ProblemDescription,
                            ClassID = sticker.ClassID ?? 0,
                            StudentID = sticker.StudentID ?? 0,
                            TutorID = sticker.TutorID ?? 1,
                            MinimumStarRanking = sticker.MinimumStarRanking.HasValue ? (float)sticker.MinimumStarRanking : 0,
                            SubmitTime = sticker.SubmitTime ?? DateTime.MinValue,
                            Timeout = sticker.Timeout ?? DateTime.MinValue
                        };

                        stickerList.Add(usSticker);
                    }
                }

                return stickerList;
            }
        }

        /// <summary>
        /// Gets the stickers a user has tutored, regardless if they are resolved or active.
        /// </summary>
        /// <param name="userID">The unique identifer of the account.</param>
        /// <param name="minstarrank">The minimum star ranking required in order to see the sticker. This parameter is optional, with a default value of 0.</param>
        /// <param name="classID">The unique identifier of the class to filter the results through. This parameter is optional, with a default value of null.</param>
        /// <returns>A list of stickers that a specific user has tutored that matches the filtering criteria.</returns>
        public List<UnstuckMESticker> GetUserTutoredStickers(int userID, float minstarrank = 0, int classID = 0)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                List<UnstuckMESticker> stickerList = new List<UnstuckMESticker>();

                using (var userStickers = db.GetUserTutoredStickers(userID, minstarrank, classID))
                {
                    foreach (var sticker in userStickers)
                    {
                        UnstuckMESticker usSticker = new UnstuckMESticker
                        {
                            StickerID = sticker.StickerID,
                            ProblemDescription = sticker.ProblemDescription,
                            ClassID = sticker.ClassID,
                            StudentID = sticker.StudentID,
                            TutorID = sticker.TutorID ?? 1,
                            MinimumStarRanking = sticker.MinimumStarRanking.HasValue ? (float)sticker.MinimumStarRanking : 0,
                            SubmitTime = sticker.SubmitTime,
                            Timeout = sticker.Timeout
                        };

                        stickerList.Add(usSticker);
                    }
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
        public List<UnstuckMEAvailableSticker> GetActiveStickers(int caller, int? organizationID = null, float minstarrank = 0, int? userID = null, int? classID = null)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                List<UnstuckMEAvailableSticker> stickerList = new List<UnstuckMEAvailableSticker>();

                using (var userStickers = db.GetActiveStickers(caller, organizationID, minstarrank, userID, classID))
                {
                    foreach (var sticker in userStickers)
                    {
                        UnstuckMEAvailableSticker usSticker = new UnstuckMEAvailableSticker
                        {
                            StickerID = sticker.StickerID ?? 0,
                            ProblemDescription = sticker.ProblemDescription,
                            ClassID = sticker.ClassID ?? 0,
                            CourseCode = sticker.CourseCode,
                            CourseName = sticker.CourseName,
                            CourseNumber = sticker.CourseNumber ?? 0,
                            StudentID = sticker.StudentID ?? 0,
                            StudentRanking = sticker.MinimumStarRanking ?? 0,
                            Timeout = sticker.Timeout ?? DateTime.MinValue
                        };

                        stickerList.Add(usSticker);
                    }
                }

                return stickerList;
            }
        }

        /// <summary>
        /// Gets the stickers available to tutor from a specific tutoring organization.
        /// </summary>
        /// <param name="caller">The unqiue identifier of the caller of the function.</param>
        /// <param name="organizationID">The unique identifier of the of the organization to filter. This parameter is optional, with a default value of null.</param>
        /// <returns>A list of stickers available to tutor that meets the filtering criteria.</returns>
        public List<UnstuckMEAvailableSticker> GetActiveStickersFromOrganization(int caller, int organizationID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                List<UnstuckMEAvailableSticker> stickerList = new List<UnstuckMEAvailableSticker>();

                using (var userStickers = db.GetActiveStickersFromOrganization(caller, organizationID))
                {
                    foreach (var sticker in userStickers)
                    {
                        UnstuckMEAvailableSticker usSticker = new UnstuckMEAvailableSticker
                        {
                            StickerID = sticker.StickerID,
                            ProblemDescription = sticker.ProblemDescription,
                            ClassID = sticker.ClassID,
                            CourseCode = sticker.CourseCode,
                            CourseName = sticker.CourseName,
                            CourseNumber = sticker.CourseNumber,
                            StudentID = sticker.StudentID,
                            StudentRanking = sticker.MinimumStarRanking ?? 0,
                            Timeout = sticker.Timeout
                        };

                        stickerList.Add(usSticker);
                    }
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
            int retstickerID = -1;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var stickerID = db.CreateSticker(newSticker.ProblemDescription, newSticker.Class.ClassID, newSticker.StudentID, newSticker.MinimumStarRanking, newSticker.TimeoutInt).First();

                    if (stickerID.HasValue)
                    {
                        if (stickerID.Value == 0)
                            throw new Exception("Create Sticker Failed, Returned sticker ID = 0");

                        retstickerID = stickerID.Value;
                    }

                    if (newSticker.AttachedOrganizations.Count != 0)
                    {
                        foreach (int orgID in newSticker.AttachedOrganizations)
                            db.AddOrgToSticker(retstickerID, orgID);
                    }

                    newSticker.StickerID = retstickerID;
                    _stickerList.Enqueue(newSticker);
                }

                return retstickerID;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sticker Submit Error: " + ex.Message);
                return retstickerID;
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
                    using (var dbStickers = db.InitialStickerPull(userID))
                    {
                        foreach (var sticker in dbStickers)
                        {
                            UnstuckMEAvailableSticker temp = new UnstuckMEAvailableSticker
                            {
                                ClassID = sticker.ClassID ?? 0,
                                CourseCode = sticker.CourseCode,
                                CourseName = sticker.CourseName,
                                CourseNumber = sticker.CourseNumber ?? 0,
                                ProblemDescription = sticker.ProblemDescription,
                                StickerID = sticker.StickerID ?? 0,
                                StudentID = sticker.StudentID ?? 0,
                                StudentRanking = sticker.StudentRanking ?? 0,
                                Timeout = sticker.Timeout ?? DateTime.MinValue
                            };

                            stickerList.Add(temp);
                        }
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
        public async Task<int> DeleteSticker(int stickerID)
        {
            int retVal = -1;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    db.DeleteStickerByStickerID(stickerID);
                }

                await Task.Factory.StartNew(() => AsyncDeleteSticker(stickerID));
                retVal = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return retVal;
        }

        /// <summary>
        /// Asyncronously gets all the online users who can see the sticker that has been deleted
        /// and removes it from their interface.
        /// </summary>
        /// <param name="stickerID">The unique identifier of the sticker to delete.</param>
        private void AsyncDeleteSticker(int stickerID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var tutors = db.GetUsersThatCanTutorASticker(stickerID);

                foreach (var tutor in tutors)
                {
                    if (tutor.HasValue && _connectedClients.ContainsKey(tutor.Value))
                        _connectedClients[tutor.Value].Connection.RemoveGUISticker(stickerID);
                }
            }
        }

        /// <summary>
        /// Removes the tutor associated with the sticker given by <paramref name="stickerID"/> and sends it out
        /// to online tutors who are eligible to see it.
        /// </summary>
        /// <param name="stickerID">The unique identifier of the sticker to be relabeled as active.</param>
        /// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        public async Task<int> RemoveTutorFromSticker(int stickerID)
        {
            int retVal = -1;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    db.UpdateTutorIDByTutorIDAndStickerID(null, stickerID); //removes the tutor from the sticker
                    var sticker = db.GetStickerInfo(stickerID).First();

                    //not sure if this will work
                    UnstuckMEBigSticker bigsticker = new UnstuckMEBigSticker
                    {
                        StickerID = stickerID,
                        ProblemDescription = sticker.ProblemDescription,
                        StudentID = sticker.StudentID ?? 0,
                        TutorID = sticker.TutorID ?? 0,
                        MinimumStarRanking = sticker.MinimumStarRanking ?? 0,
                        Class = new UserClass
                        {
                            ClassID = sticker.ClassID ?? 0,
                            CourseCode = sticker.CourseCode,
                            CourseName = sticker.CourseName,
                            CourseNumber = sticker.CourseNumber ?? 0
                        },
                        SubmitTime = sticker.SubmitTime ?? DateTime.MinValue,
                        Timeout = sticker.Timeout ?? DateTime.MinValue
                    };

                    await Task.Factory.StartNew(() => AsyncMakeStickerActive(bigsticker));
                    _stickerList.Enqueue(bigsticker);

                    retVal = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return retVal;
        }

        /// <summary>
        /// Update the sticker on the client who submitted it that it has been made active again.
        /// </summary>
        /// <param name="sticker">The sticker that has been made active again.</param>
        private void AsyncMakeStickerActive(UnstuckMEBigSticker sticker)
        {
            bool found = false;

            for (int index = 0; index < _connectedClients.Count && !found; index++)
            {
                if (_connectedClients[index].User.UserID == sticker.StudentID)
                {
                    _connectedClients[index].Connection.UpdateStickerStatus(StickerStatus.Available, sticker.StickerID);
                    found = true;
                }
            }
        }

        /// <summary>
        /// returns a single sticker by ID, sticker currently only contains coursename and problem description
        /// </summary>
        /// <param name="stickerID"></param>
        /// <returns></returns>
        public UnstuckMESticker GetSticker(int stickerID)
        {
            UnstuckMESticker rsticker = new UnstuckMESticker();

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var sticker = db.GetStickerInfo(stickerID).First();

                    rsticker.CourseName = sticker.CourseName;
                    rsticker.ProblemDescription = sticker.ProblemDescription;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return rsticker;
        }
    }
}