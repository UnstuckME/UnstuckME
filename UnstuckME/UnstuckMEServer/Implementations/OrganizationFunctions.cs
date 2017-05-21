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
        /// Associates a user with an official tutoring organization.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <param name="organizationID">The unique identifier of the tutoring organization.</param>
        /// <returns>Returns -1 if failed, 0 if successful.</returns>
        public async Task<int> AddUserToTutoringOrganization(int userID, int organizationID)
        {
            int? retVal = -1;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    if (db.InsertUserIntoMentorProgram(userID, organizationID).First() != -1)
                    {
                        await Task.Factory.StartNew(() => AddOrganizationStickerToUser(userID, organizationID));
                        retVal = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return retVal.Value;
        }

        /// <summary>
        /// Asynchronously adds active stickers from the organization to the client's interface.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <param name="organizationID">The unique identifier of the tutoring organization.</param>
        private void AddOrganizationStickerToUser(int userID, int organizationID)
        {
            List<UnstuckMEAvailableSticker> stickers = GetActiveStickersFromOrganization(userID, organizationID);

            if (stickers.Count > 0)
            {
                foreach (var client in _connectedClients)
                {
                    if (client.Value.User.UserID == userID)
                    {
                        foreach (UnstuckMEAvailableSticker sticker in stickers)
                            client.Value.Connection.RecieveNewSticker(sticker);

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Associates a user with an official tutoring organization.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <param name="organizationID">The unique identifier of the tutoring organization.</param>
        /// <returns>Returns -1 if failed, 0 if successful.</returns>
        public async Task<int> RemoveUserFromTutoringOrganization(int userID, int organizationID)
        {
            int? retVal = -1;

            try
            {
                List<UnstuckMEAvailableSticker> stickers = GetActiveStickersFromOrganization(userID, organizationID);

                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    if (stickers.Count > 0 && db.RemoveUserFromMentorProgram(userID, organizationID).First() != -1)
                    {
                        await Task.Factory.StartNew(() => RemoveOrganizationStickerToUser(stickers, userID));
                        retVal = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return retVal.Value;
        }

        /// <summary>
        /// Asynchronously adds active stickers from the organization to the client's interface.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <param name="stickers"></param>
        private void RemoveOrganizationStickerToUser(IEnumerable<UnstuckMEAvailableSticker> stickers, int userID)
        {
            foreach (var client in _connectedClients)
            {
                if (client.Value.User.UserID == userID)
                {
                    foreach (UnstuckMEAvailableSticker sticker in stickers)
                        client.Value.Connection.RemoveGUISticker(sticker.StickerID);

                    break;
                }
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
                List<Organization> orgs = new List<Organization>();
                using (var organizations = db.GetAllOrganizations())
                {
                    foreach (var org in organizations)
                    {
                        Organization newOrg = new Organization
                        {
                            MentorID = org.MentorID,
                            OrganizationName = org.OrganizationName
                        };

                        orgs.Add(newOrg);
                    }
                }

                return orgs;
            }
        }

        /// <summary>
        /// Gets all the organizations that a user is a member of.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <returns>A list of organizations that contains the unique identifiers and the name of each.</returns>
        public List<Organization> GetUserOrganizations(int userID)
        {
            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    List<Organization> orgs = new List<Organization>();

                    using (var userOrgs = db.GetUserOrganizations(userID))
                    {
                        foreach (var org in userOrgs)
                        {
                            orgs.Add(new Organization
                            {
                                MentorID = org.MentorID,
                                OrganizationName = org.OrganizationName
                            });
                        }
                    }

                    return orgs;
                }
            }
            catch (Exception)
            {
                return null;
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
