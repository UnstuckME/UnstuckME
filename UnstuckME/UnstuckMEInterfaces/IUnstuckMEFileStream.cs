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
    }
}
