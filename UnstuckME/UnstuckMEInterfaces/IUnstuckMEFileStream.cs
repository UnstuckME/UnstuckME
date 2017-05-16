using System.IO;
using System.ServiceModel;
using UnstuckME_Classes;

namespace UnstuckMEInterfaces
{
    [ServiceContract]
    public interface IUnstuckMEFileStream
    {
        /// <summary>
        /// Currently retrieves the data of the profile picture fro the database. Set up to retrieve the filepath of the picture from the database,
        /// open the file, and convert it to a byte array.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <returns>A Stream object containing the data of the image file.</returns>
        [OperationContract]
        Stream GetProfilePicture(int userID);

        /// <summary>
        /// Overwrites the profile picture data of a specific user on the database.
        /// </summary>
        /// <param name="image">A custom stream that contains the data of the image file and the information of the requesting user.</param>
        [OperationContract(IsOneWay = true)]
        void SetProfilePicture(UnstuckMEStream image);

        /// <summary>
        /// Gets a file from the server using the filepath obtained using a messageID."/>
        /// </summary>
        /// <param name="messageID">The unique identifier of the message to get the filepath to download the file.</param>
        /// <returns>Returns a custom stream that contains the data of the file.</returns>
        [OperationContract]
        UnstuckMEStream GetFile(int messageID);

        /// <summary>
        /// Sends the file to the server and saves it locally.
        /// </summary>
        /// <param name="file">A custom stream containing the file bytes, the name of the file, and the user who sent it.</param>
        /// <returns>The full path where the file was stored on the server.</returns>
        [OperationContract]
        string SendFile(UnstuckMEStream file);
    }
}
