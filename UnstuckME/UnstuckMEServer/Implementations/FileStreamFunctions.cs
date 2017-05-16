using System;
using System.Linq;
using UnstuckMEServer;
using UnstuckME_Classes;
using System.IO;

namespace UnstuckMEInterfaces
{
	public partial class UnstuckMEService : IUnstuckMEService, IUnstuckMEServer, IUnstuckMEFileStream
	{
		/// <summary>
		/// Currently retrieves the data of the profile picture fro the database. Set up to retrieve the filepath of the picture from the database,
		/// open the file, and convert it to a byte array.
		/// </summary>
		/// <param name="userID">The unique identifier of the user.</param>
		/// <returns>A Stream object containing the data of the image file.</returns>
		public Stream GetProfilePicture(int userID)
		{
			using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
			{
				//string directory = db.GetProfilePicture(userID).First();

				//using (FileStream file = new FileStream(directory, FileMode.Open, FileAccess.Read))
			    {
			        //using (MemoryStream ms = new MemoryStream())
			        //{
			        //    file.CopyTo(ms);
			        //    ms.Position = 0L;

			        //    return ms;
			        //}
			        /*****************************************************************************************************
					 * Remove the bottom lines and uncomment the above lines once filepath is implemented on the database.
					*****************************************************************************************************/
                    byte[] imgByte = db.GetProfilePicture(userID).First();

			        return new MemoryStream(imgByte);
			    }
			}
		}

		/// <summary>
		/// Overwrites the profile picture data of a specific user on the database.
		/// </summary>
		/// <param name="image">A custom stream that contains the data of the image file and the information of the requesting user.</param>
		public void SetProfilePicture(UnstuckMEStream image)
		{
			string directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData, Environment.SpecialFolderOption.Create) + @"\UnstuckME\";
			Directory.CreateDirectory(directory += image.UserID.ToString());
			Directory.CreateDirectory(directory);
			directory += @"\" + image.Filename;

			using (FileStream newfile = new FileStream(directory, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite, 26214400, FileOptions.Encrypted))
			{
				using (MemoryStream ms = new MemoryStream())
				{
					image.CopyTo(ms);   //write to a MemoryStream to easily convert to a byte array
					ms.Position = 0L;   //reset the position of the memorystream to write it to a different stream
					ms.CopyTo(newfile); //write to local filesystem
					ms.Position = 0L;

					/****************************************************************************************************************
					* When using remote server comment MemoryStream section out and swap out the UpdateProfilePicture function calls
					****************************************************************************************************************/
					using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
					{
						//db.UpdateProfilePicture(image.User.UserID, directory);
						db.UpdateProfilePicture(image.UserID, ms.ToArray()); //replace with line above once filepath is implemented on database
					}
				}
			}
		}

	    /// <summary>
	    /// Gets a file from the server using the filepath obtained using a messageID."/>
	    /// </summary>
	    /// <param name="messageID">The unique identifier of the message to get the filepath to download the file.</param>
	    /// <returns>Returns a custom stream that contains the data of the file.</returns>
        public UnstuckMEStream GetFile(int messageID)
	    {
	        try
	        {
	            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
	            {
	                var filepath = from m in db.Messages
	                               where m.MessageID == messageID
	                               select m.FilePath;

	                using (FileStream file = File.Open(filepath.First(), FileMode.Open))
	                {
	                    using (MemoryStream ms = new MemoryStream())
	                    {
	                        file.CopyTo(ms);
	                        ms.Position = 0L;
                            return new UnstuckMEStream(ms.ToArray(), false)
                            {
                                Filename = file.Name
                            };
	                    }
	                }
	            }
	        }
	        catch (Exception ex)
	        {
	            Console.WriteLine("Failed to get the file" + ex.Message);
	            return null;
	        }
	    }

	    /// <summary>
	    /// Sends the file to the server and saves it locally.
	    /// </summary>
	    /// <param name="file">A custom stream containing the file bytes, the name of the file, and the user who sent it.</param>
	    /// <returns>The full path where the file was stored on the server.</returns>
        public string SendFile(UnstuckMEStream file)
	    {
	        string directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData, Environment.SpecialFolderOption.Create) + @"\UnstuckME\";
	        Directory.CreateDirectory(directory += file.UserID.ToString());
	        Directory.CreateDirectory(directory);
	        directory += @"\" + file.Filename;

	        using (FileStream newfile = new FileStream(directory, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, 26214400, FileOptions.Encrypted))
	        {
	            using (MemoryStream ms = new MemoryStream())
	            {
                    file.CopyTo(ms);
	                ms.Position = 0L;
	                ms.CopyTo(newfile);
	            }
	        }

            return directory;
	    }
	}
}