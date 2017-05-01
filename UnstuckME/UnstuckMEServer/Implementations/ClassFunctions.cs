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
        /// Gets the classes a user can tutor for.
        /// </summary>
        /// <param name="UserID">The unique identifier of the user we are getting the classes of.</param>
        /// <returns>A list of classes the specified user has signed up to tutor. This includes the subject, course name and number, and the unique identifier.</returns>
        public List<UserClass> GetUserClasses(int UserID)
        {
            try
            {
                List<UserClass> rlist = new List<UserClass>();

                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    using (var classes = db.GetUserClasses(UserID))
                    {
                        foreach (var c in classes)
                        {
                            UserClass temp = new UserClass()
                            {
                                ClassID = c.ClassID,
                                CourseCode = c.CourseCode,
                                CourseName = c.CourseName,
                                CourseNumber = c.CourseNumber
                            };

                            rlist.Add(temp);
                        }
                    }
                }

                return rlist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Removes the specified class from a user's list of classes he/she can tutor.
        /// </summary>
        /// <param name="UserID">The unique identifier of the user.</param>
        /// <param name="ClassID">The unique identifier of the class.</param>
        public void RemoveUserFromClass(int UserID, int ClassID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.DeleteUserFromClass(UserID, ClassID);
            }
        }

        /// <summary>
        /// Adds the specified class to a user's list of classes he/she can tutor.
        /// </summary>
        /// <param name="UserID">The unique identifier of the user.</param>
        /// <param name="ClassID">The unique identifier of the class.</param>
        public void InsertStudentIntoClass(int UserID, int ClassID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.InsertStudentIntoClass(UserID, ClassID);
            }
        }

        /// <summary>
        /// Gets all the course codes in the database.
        /// </summary>
        /// <returns>A list of strings containing the course codes in the database.</returns>
        public List<string> GetCourseCodes()
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                List<string> rlist = new List<string>();
                List<string> rlist2 = new List<string>();

                using (var codes = db.GetCourseCodes())
                {
                    foreach (var code in codes)
                        rlist.Add(code);
                }

                IEnumerable<string> list = rlist.Distinct();

                foreach (string classcode in list)
                    rlist2.Add(classcode);

                return rlist2;
            }
        }

        /// <summary>
        /// Gets the unique identifier of a specific class.
        /// </summary>
        /// <param name="code">The course code of the class.</param>
        /// <param name="number">The course number of the class.</param>
        /// <returns>An integer representing the unique identifier of a specific class.</returns>
        public int GetCourseIdNumberByCodeAndNumber(string code, string number)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                int num = Convert.ToInt32(number);
                var ID = db.GetCourseIDByCodeAndNumber(code, (short)num).First();

                return ID ?? -1;
            }
        }

        /// <summary>
        /// Gets the course name of a specific class.
        /// </summary>
        /// <param name="code">The course code of the class.</param>
        /// <param name="number">The course number of the class.</param>
        /// <returns>A string representing the name of a specific class.</returns>
        public string GetCourseNameByCodeAndNumber(string code, string number)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                int num = Convert.ToInt32(number);
                return db.GetCourseNameByCodeAndNumber(code, (short)num).First();
            }
        }

        /// <summary>
        /// Gets all the course numbers by subject.
        /// </summary>
        /// <param name="CourseCode">The course code.</param>
        /// <returns>A list of course numbers associated with a subject.</returns>
        public List<string> GetCourseNumbersByCourseCode(string CourseCode)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                List<string> rlist = new List<string>();
                List<string> rlist2 = new List<string>();

                using (var codes = db.GetCourseNumberByCourseCode(CourseCode))
                {
                    foreach (var code in codes)
                    {
                        if (code != null)
                            rlist.Add(code.Value.ToString());
                    }
                }

                IEnumerable<string> list = rlist.Distinct();

                foreach (string classcode in list)
                    rlist2.Add(classcode);

                return rlist2;
            }
        }

        /// <summary>
        /// Gets the information of a single class.
        /// </summary>
        /// <param name="classID">The unique identifier of a specific class.</param>
        /// <returns>A UserClass containing the course code, name, and number.</returns>
        public UserClass GetSingleClass(int classID)
        {
            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var _class = db.ViewClasses(classID).First();

                    UserClass temp = new UserClass
                    {
                        ClassID = _class.ClassID,
                        CourseNumber = _class.CourseNumber,
                        CourseName = _class.CourseName,
                        CourseCode = _class.CourseCode
                    };

                    return temp;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets the classes and adds them to the client. There is a much more efficient way to do this.
        /// </summary>
        /// <param name="inClass">The unique identifier of the class to be added.</param>
        /// <param name="userID">The unique identifier of the user adding the class.</param>
        public void AddClassesToClient(int inClass, int userID)
        {
            try
            {
                UserClass temp = new UserClass();
                List<UnstuckMEAvailableSticker> newstickers = GetActiveStickers(userID, classID: inClass);

                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var c = db.ViewClasses(inClass).First();

                    temp.CourseNumber = c.CourseNumber;
                    temp.CourseName = c.CourseName;
                    temp.CourseCode = c.CourseCode;
                    temp.ClassID = c.ClassID;
                }

                foreach (var client in _connectedClients)
                {
                    if (client.Key == userID)
                    {
                        client.Value.Connection.AddClasses(temp);
                        foreach (UnstuckMEAvailableSticker sticker in newstickers)
                            client.Value.Connection.RecieveNewSticker(sticker);

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Adds a new class to the UnstuckME Database 
        /// </summary>
        /// <param name="newClass">Passes a DBClass object that contains the (CourseName, CourseCode, CourseNUmber)</param>
        /// <returns>A boolean indicating whether or not it was able to add the class to the UnstuckME_DB</returns>
        public bool AddClass(UserClass newClass)
        {
            bool addedClassSucessfully = true;
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                int callReturned = db.CreateNewClass(newClass.CourseName, newClass.CourseCode, newClass.CourseNumber);

                if (callReturned == 0)
                    addedClassSucessfully = false;
            }

            return addedClassSucessfully;
        }
    }
}