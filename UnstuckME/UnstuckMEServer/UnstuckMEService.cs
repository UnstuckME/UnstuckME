using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UnstuckMEServer;

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
            using (UnstuckME_DBEntities2 db = new UnstuckME_DBEntities2())
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

            using (UnstuckME_DBEntities2 db = new UnstuckME_DBEntities2())
            {
               retVal = db.CreateNewUser(displayFName, displayLName, emailAddress, userPassword, privileges, salt);
            }

            return retVal;
        }

        public string GetUserEmail(int UserID)
        {
            string email;
            using (UnstuckME_DBEntities2 db = new UnstuckME_DBEntities2())
            {
                var temp = db.GetDisplayNameAndEmail(UserID);
                email = temp.ElementAtOrDefault(2).ToString();
            }
            return email;
        }

        public int GetUserID(string emailAddress)
        {
            int userID = 0;
            using (UnstuckME_DBEntities2 db = new UnstuckME_DBEntities2())
            {
                var temp = db.GetUserID(emailAddress);
                userID = temp.First().Value;
            }
            return userID;
        }

        public bool UserLoginAttempt(string emailAddress, string passWord)
        {
            bool loginAttempt = false;
            //string salt = null;
            //string storedPassword = null;

            Console.WriteLine("User Login Attempt by {0}\n Hashed Password: {1}", emailAddress, passWord.First());

            using (UnstuckME_DBEntities2 db = new UnstuckME_DBEntities2())
            {
                loginAttempt = true;//for kyronns gui testing
                try
                {
                    var temp = db.GetUserID(emailAddress);
                    int UserID = temp.First().Value;
                }
                catch (Exception)
                {
                    loginAttempt = false;
                    
                }
                    
            }

            return loginAttempt;
        }
    }
}
