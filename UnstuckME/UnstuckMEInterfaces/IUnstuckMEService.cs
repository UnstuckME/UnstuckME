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
		[OperationContract]
		int AddFriend(int userId, int friendUserID);

		[OperationContract]
		int CreateChat(int userID);

		[OperationContract]
		UserInfo GetUserInfo(int userID);

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
		List<UnstuckMEReview> GetUserStudentReviews (int userID);

		[OperationContract]
		List<UnstuckMEReview> GetUserTutorReviews(int userID);

		[OperationContract]
		List<UnstuckMESticker> GetUserSubmittedStickers(int userID);

		[OperationContract]
		List<UnstuckMESticker> GetUserTutoredStickers(int userID);

		[OperationContract]
		List<UnstuckMESticker> GetAllStickers();

		[OperationContract]
		void AddUserToTutoringOrganization(int userID, int organizationID);

		[OperationContract]
		void SubmitSticker(UnstuckMESticker newSticker);

		[OperationContract]
		byte[] GetProfilePicture(int userID);

		[OperationContract]
		void SetProfilePicture(int userID, byte[] image);

		[OperationContract]
		List<string> GetCourseCodes();

		[OperationContract]
		int GetCourseIdNumberByCodeAndNumber(string code, string number);

		[OperationContract]
		string GetCourseNameByCodeAndNumber(string code, string number);

		[OperationContract]
		List<string> GetCourseNumbersByCourseCode(String CourseCode);

		[OperationContract]
		void InsertProfilePicture(int userID, byte[] image);

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
		int InsertUserInToChat(int userID, int chatID);
	}

	[ServiceContract(CallbackContract = typeof(IServer))]
	public interface IUnstuckMEServer
	{
		[OperationContract]
		void RegisterServerAdmin(AdminInfo admin);

		[OperationContract]
		void AdminLogout();

		[OperationContract]
		void AdminLogMessage(string message);

		[OperationContract]
		List<string> AdminGetAllOnlineUsers();

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
	}
}
