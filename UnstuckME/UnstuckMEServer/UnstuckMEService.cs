using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UnstuckMEServer;
using UnstuckME_Classes;
using System.Security.Cryptography;

namespace UnstuckMEInterfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UnstuckMEService" in both code and config file together.
    /// <summary>
    /// Implement any Operation Contracts from IUnstuckMEService.cs in this file.
    /// </summary>
    public class UnstuckMEService : IUnstuckMEService
    {
        public void ChangeUserName(string emailaddress, string newFirstName, string newLastName)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {

                var users = (from u in db.UserProfiles
                             where u.EmailAddress == emailaddress
                             select u).First();
                users.DisplayFName = newFirstName;
                users.DisplayLName = newLastName;
                db.SaveChanges();
            }
        }

        public int CreateNewUser(string displayFName, string displayLName, string emailAddress, string userPassword, string privileges, string salt)
        {
            int retVal = -1;

            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                retVal = db.CreateNewUser(displayFName, displayLName, emailAddress, userPassword, privileges, salt);
            }
            return retVal;
        }

        public UserNameAndEmail GetUserDisplayInfo(int UserID)
        {
            UserNameAndEmail userInfo = new UserNameAndEmail();
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                //Feel free to attempt to optimize this code (currently 3 DB calls it think). I can't figure it out any other way.
                userInfo.EmailAddress = (from u in db.GetDisplayNameAndEmail(UserID)
                                         select u.EmailAddress).First();
                userInfo.FirstName = (from u in db.GetDisplayNameAndEmail(UserID)
                                      select u.DisplayFName).First();
                userInfo.LastName = (from u in db.GetDisplayNameAndEmail(UserID)
                                     select u.DisplayLName).First();
            }
            return userInfo;
        }

        public int GetUserID(string emailAddress)
        {
            int userID = 0;
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var temp = db.GetUserID(emailAddress);
                userID = temp.First().Value;
            }
            return userID;
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        public bool UserLoginAttempt(string emailAddress, string passWord)
        {
            bool loginAttempt = false;
            //string salt = null;
            //string storedPassword = null;

            Console.WriteLine("User Login Attempt by {0}\n Hashed Password: {1}", emailAddress, passWord.First());

            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                try
                {
                    string databasePassword = (from u in db.GetUserPasswordAndSalt(emailAddress)
                                               select u.UserPassword).Last();

                    string salt = (from u in db.GetUserPasswordAndSalt(emailAddress)
                                   select u.Salt).First();

                    byte[] checkPassword = GenerateSaltedHash(GetBytes(passWord), GetBytes(salt));
                    string stringOfPassword = "";

                    foreach (byte element in checkPassword)
                    {
                        stringOfPassword += element;
                    }

                    Console.WriteLine("Length = {0}", stringOfPassword.Length);
                    if (stringOfPassword == databasePassword)
                    {
                        loginAttempt = true;
                    }
                }
                catch (Exception)
                {
                    loginAttempt = false;

                }

            }

            return loginAttempt;
        }

        public List<UserClasses> GetUserClasses(int UserID)
        {
            try
            {
                List<UserClasses> Rlist = new List<UserClasses>();

                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var classes = db.GetUserClasses(UserID);
                    UserClasses temp = new UserClasses();

                    //This might work, if not let me know and i'll figure out something else.
                    foreach (var c in classes)
                    {
                        temp.CourseCode = c.CourseCode;
                        temp.CourseName = c.CourseName;
                        temp.CourseNumber = c.CourseNumber;
                        Rlist.Add(temp);
                    }
                }
                return Rlist;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void InsertStudentIntoClass(int UserID, int ClassID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.InsertStudentIntoClass(UserID, ClassID);
            }
        }

    }
}
