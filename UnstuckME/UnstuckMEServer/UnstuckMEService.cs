using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

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

        public List<string> ListUsersFullName()
        {
            Console.WriteLine("Attempting User Name Select");
            List<string> userList = new List<string>();
            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var users = from u in db.UserProfiles
                                select u.DisplayFName + " " + u.DisplayLName;

                    userList = users.ToList();
                }
            }
            catch
            {

            }
            return userList;
        }
       
    }
}
