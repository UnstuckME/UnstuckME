using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.ServiceModel;
using System.Text;
using System.Windows.Media;
using UnstuckME_Classes;
namespace UnstuckMEInterfaces
{

    [ServiceContract(CallbackContract = typeof(IClient))]
    public interface IUnstuckMEService
    {
        [OperationContract(IsOneWay = true)]
        void AcceptSticker(int userID, int stickerID);

        [OperationContract]
        int AddFriend(int userId, int friendUserID);

        [OperationContract]
        int CreateChat(int userID);

        [OperationContract]
        UserInfo GetUserInfo(Nullable<int> userID, string emailAddress);

        [OperationContract]
        int GetUserID(string emailAddress);

        [OperationContract]
        void ChangeUserName(string emailaddress, string newFirstName, string newLastName);

        [OperationContract]
        UserInfo UserLoginAttempt(string emailAddress, string passWord);

        [OperationContract]
        bool CreateNewUser(string displayFName, string displayLName, string emailAddress, string userPassword);

        [OperationContract]
        List<UserClass> GetUserClasses(int UserID);

        [OperationContract]
        void RemoveUserFromClass(int UserID, int ClassID);

        [OperationContract]
        void InsertStudentIntoClass(int UserID, int ClassID);

        //Checks to see if specified email address exists in the database.
        [OperationContract]
        bool IsValidUser(string emailAddress);

        [OperationContract]
        void Logout();

        [OperationContract]
        void ChangePassword(UserInfo User, string newPassword);

        [OperationContract]
        void DeleteUserAccount(int userID);

		[OperationContract]
		List<UnstuckMEReview> GetUserStudentReviewsASC(int userID, short firstrow = 0, short lastrow = 50, float minstarrank = 0);

		[OperationContract]
		List<UnstuckMEReview> GetUserStudentReviewsDESC(int userID, short firstrow = 0, short lastrow = 50, float minstarrank = 0);

		[OperationContract]
		List<UnstuckMEReview> GetUserTutorReviewsASC(int userID, short firstrow = 0, short lastrow = 50, float minstarrank = 0);

		[OperationContract]
		List<UnstuckMEReview> GetUserTutorReviewsDESC(int userID, short firstrow = 0, short lastrow = 50, float minstarrank = 0);

		[OperationContract]
		List<UnstuckMESticker> GetUserSubmittedStickersASC(int userID, short firstrow = 0, short lastrow = 0, float minstarrank = 0, Nullable<int> classID = null);

		[OperationContract]
		List<UnstuckMESticker> GetUserSubmittedStickersDESC(int userID, short firstrow = 0, short lastrow = 0, float minstarrank = 0, Nullable<int> classID = null);

		[OperationContract]
		List<UnstuckMESticker> GetUserTutoredStickersASC(int userID, short firstrow = 0, short lastrow = 0, float minstarrank = 0, Nullable<int> classID = null);

		[OperationContract]
		List<UnstuckMESticker> GetUserTutoredStickersDESC(int userID, short firstrow = 0, short lastrow = 0, float minstarrank = 0, Nullable<int> classID = null);

		[OperationContract]
		List<UnstuckMEAvailableSticker> GetActiveStickersASC(float minstarrank = 0, short firstrow = 0, short lastrow = 50, Nullable<int> userID = null, Nullable<int> classID = null);

		[OperationContract]
		List<UnstuckMEAvailableSticker> GetActiveStickersDESC(float minstarrank = 0, short firstrow = 0, short lastrow = 50, Nullable<int> userID = null, Nullable<int> classID = null);

		[OperationContract]
		List<UnstuckMEAvailableSticker> GetActiveStickersWithOrg_OrgClassASC(int orgID, float minstarrank = 0, short firstrow = 0, short lastrow = 50, Nullable<int> userID = null, Nullable<int> classID = null);

		[OperationContract]
		List<UnstuckMEAvailableSticker> GetActiveStickersWithOrg_OrgDESC(int orgID, float minstarrank = 0, short firstrow = 0, short lastrow = 50, Nullable<int> userID = null, Nullable<int> classID = null);

		[OperationContract]
		List<UnstuckMEAvailableSticker> GetActiveStickersWithOrg_ClassDESC(int orgID, float minstarrank = 0, short firstrow = 0, short lastrow = 50, Nullable<int> userID = null, Nullable<int> classID = null);

		[OperationContract]
		List<UnstuckMEAvailableSticker> GetActiveStickersWithOrg_OrgClassDESC(int orgID, float minstarrank = 0, short firstrow = 0, short lastrow = 50, Nullable<int> userID = null, Nullable<int> classID = null);

		[OperationContract]
        void AddUserToTutoringOrganization(int userID, int organizationID);

        [OperationContract]
        void SubmitSticker(UnstuckMESticker newSticker);

        [OperationContract]
        byte[] GetProfilePicture(int userID);

        [OperationContract]
		void SetProfilePicture(int userID, byte[] image);

		//[OperationContract]
		//void SetProfilePicture(int userID, System.IO.FileStream image);

		[OperationContract]
        List<string> GetCourseCodes();

        [OperationContract]
        int GetCourseIdNumberByCodeAndNumber(string code, string number);

        [OperationContract]
        string GetCourseNameByCodeAndNumber(string code, string number);

        [OperationContract]
        List<string> GetCourseNumbersByCourseCode(string CourseCode);

        [OperationContract]
        UserClass GetCourseCode_Name_NumberByID(int ClassID);

        [OperationContract]
        List<Organization> GetAllOrganizations();

        [OperationContract]
        int CreateReport(string reportDescription, int flaggerID, int reviewID);

        [OperationContract]
        int CreateReview(int stickerID, int reviewerID, double starRanking, string description);

        [OperationContract]
        int DeleteFriend(int userID, int friendID);

        [OperationContract]
        int DeleteMessage(int messageID);

        [OperationContract]
        int DeleteReportByUser(int userID, int reportID);

        [OperationContract]
        int InsertUserIntoChat(int userID, int chatID);

        [OperationContract]
        List<UnstuckMEChat> GetUserChats(int userID);

        [OperationContract]
        UnstuckMEChat GetSingleChat(int chatID);

        [OperationContract]
        void SendMessage(UnstuckMEMessage message);

		[OperationContract]
		void UploadFile(UnstuckMEMessage message, UnstuckMEFile file);

		[OperationContract]	
        List<UnstuckMEAvailableSticker> InitialAvailableStickerPull(int userID);

        [OperationContract]
        UserClass GetSingleClass(int classID);

        [OperationContract(IsOneWay = true)]
        void AddClassesToClient(int inClass, int userID);

        [OperationContract]
        List<UserInfo> GetFriends(int userID);
    }

    [ServiceContract(CallbackContract = typeof(IServer))]
    public interface IUnstuckMEServer
    {
        [OperationContract]
        bool TestNewConfig();

        [OperationContract]
        void RegisterServerAdmin(AdminInfo admin);

        [OperationContract]
        void AdminLogout();

        [OperationContract]
        void AdminLogMessage(string message);

        [OperationContract]
        List<UserInfo> AdminGetAllOnlineUsers();

        [OperationContract]
        void AdminServerShuttingDown();

        [OperationContract]
        int AdminCreateMentoringOrganization(string organizationName);

        [OperationContract]
        int AdminCreateClass(string courseName, string courseCode, int courseNumber);

        [OperationContract]
        int AdminDeleteClass(int classID);

        [OperationContract]
        int AdminDeleteMentoringOrganization(int organizationID);

        [OperationContract]
        int AdminDeleteReport(int reportID);

        [OperationContract]
		UnstuckMEFile UploadDocument();
    }

    [ServiceContract(CallbackContract = typeof(IFileStream))]
    public interface IUnstuckMEFileStream
    {
		[OperationContract]
		System.IO.Stream Test(System.IO.Stream stream);
    }
}
