using System.Collections.Generic;
using UnstuckMEServer;
using UnstuckME_Classes;

namespace UnstuckMEInterfaces
{
    public partial class UnstuckMEService : IUnstuckMEService, IUnstuckMEServer, IUnstuckMEFileStream
    {
        /// <summary>
        /// Associates a user with an official tutoring organization.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <param name="organizationID">The unique identifier of the tutoring organization.</param>
        public void AddUserToTutoringOrganization(int userID, int organizationID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.InsertUserIntoMentorProgram(userID, organizationID);
            }
        }

        /// <summary>
        /// Gets all tutoring organizations in the database.
        /// </summary>
        /// <returns>A list of organizations containing the unique identifier and the name of each organization.</returns>
        public List<Organization> GetAllOrganizations()
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var organizations = db.GetAllOrganizations();

                List<Organization> orgs = new List<Organization>();

                foreach (var org in organizations)
                {
                    Organization new_org = new Organization()
                    {
                        MentorID = org.MentorID,
                        OrganizationName = org.OrganizationName
                    };
                    orgs.Add(new_org);
                }
                //organizations.Dispose();		//need this to release memory   
                return orgs;
            }
        }

        /// <summary>
        /// Creates a new mentor organzation on the database.4
        /// </summary>
        /// <param name="name">The name of the new organization.</param>
        public void CreateMentorOrg(string name)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.CreateMentorOrganization(name);
            }
        }
    }
}
