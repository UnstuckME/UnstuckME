using System;
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
                var stickers = GetActiveStickers(userID, organizationID);

                foreach (var client in _connectedClients)
                {
                    if (client.Value.User.UserID == userID)
                    {
                        foreach (UnstuckMEAvailableSticker sticker in stickers)
                            client.Value.Connection.RecieveNewSticker(sticker);

                        break;
                    }
                }
                //int index = -1;
                //do
                //{
                //    ++index;

                //    if (_connectedClients[index].User.UserID == userID)
                //    {
                //        foreach (UnstuckMEAvailableSticker sticker in stickers)
                //            _connectedClients[index].Connection.RecieveNewSticker(sticker);
                //    }
                //} while (_connectedClients[index].User.UserID != userID && index < _connectedClients.Count);
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
                        Organization newOrg = new Organization()
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
                            orgs.Add(new Organization()
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
