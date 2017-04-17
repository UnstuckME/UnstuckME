using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
		/// <summary>
		/// Invoked when a tutor accepts a sticker. Updates the TutorID associated with that sticker.
		/// </summary>
		/// <param name="userID">The unique identifier of the user who has accepted the sticker.</param>
		/// <param name="stickerID">The unique identifier fo the sticker that has been accepted.</param>
		[OperationContract(IsOneWay = true)]
		void AcceptSticker(int userID, int stickerID);

		/// <summary>
		/// Registers another user as a contact.
		/// </summary>
		/// <param name="userId">The unique identifier of the callee.</param>
		/// <param name="friendUserID">The unique identifier of the user to add as a contact.</param>
		/// <returns>The unique identifier of the user to add as a contact if successful, -1 if unsuccessful.</returns>
		[OperationContract]
		int AddFriend(int userId, int friendUserID);

		/// <summary>
		/// Creates a chat associated with a user.
		/// </summary>
		/// <param name="userID">The unique identifer of the callee.</param>
		/// <returns>The unique identifier of the newly created chat if successful, -1 if unsuccessful.</returns>
		[OperationContract]
		int CreateChat(int userID);

		/// <summary>
		/// Gets all the information of a specific user given the unique identifier or the email address of the user.
		/// </summary>
		/// <param name="userID">The unique identifier of the user.</param>
		/// <param name="emailAddress">The email address of the user.</param>
		/// <returns>A UserInfo structure that contains the UserID, first and last name, email address, privileges, average student and
		/// tutor ranks, the total number of reviews submitted as a student and tutor, password, salt value used for hashing, and the bytes
		/// representing the data of their profile picture.</returns>
		[OperationContract]
		UserInfo GetUserInfo(Nullable<int> userID, string emailAddress);

		/// <summary>
		/// Gets the unique identifier of a particular user.
		/// </summary>
		/// <param name="emailAddress">The email address of the user we need the unique identifier for.</param>
		/// <returns>An integer representing the unique identifier associated with the given email address</returns>
		[OperationContract]
		int GetUserID(string emailAddress);

		/// <summary>
		/// Changes the first and last name of the user with the specified email address.
		/// </summary>
		/// <param name="emailaddress">The email address of the user.</param>
		/// <param name="newFirstName">The new first name of the user.</param>
		/// <param name="newLastName">The new last name of the user.</param>
		[OperationContract]
		void ChangeUserName(string emailaddress, string newFirstName, string newLastName);

		/// <summary>
		/// Attempts to log the user in. Starts by recreating the hashed password, then checks to see if the user is already logged on so
		/// that they can't be logged in more than once. If successful, logs the callback channel, the incoming message properties, and
		/// the all the user's info from the database in the server's list of connected clients.
		/// </summary>
		/// <param name="emailAddress">The email address of the user attempting to log in.</param>
		/// <param name="passWord">The password of the user attempting to log in.</param>
		/// <returns>A UserInfo structure that contains the UserID, first and last name, email address, privileges, average student and
		/// tutor ranks, the total number of reviews submitted as a student and tutor, password, salt value used for hashing, and the bytes
		/// representing the data of their profile picture.</returns>
		[OperationContract]
		UserInfo UserLoginAttempt(string emailAddress, string passWord);

		/// <summary>
		/// Attempts to log the user in. Starts by recreating the hashed password, then checks to see if the user is already logged on so
		/// that they can't be logged in more than once. If successful, logs the callback channel, the incoming message properties, and
		/// the all the user's info from the database in the server's list of connected clients.
		/// </summary>
		/// <param name="emailAddress">The email address of the user attempting to log in.</param>
		/// <param name="passWord">The password of the user attempting to log in.</param>
		/// <returns>A UserInfo structure that contains the UserID, first and last name, email address, privileges, average student and
		/// tutor ranks, the total number of reviews submitted as a student and tutor, password, salt value used for hashing, and the bytes
		/// representing the data of their profile picture.</returns>
		[OperationContract]
		bool CreateNewUser(string displayFName, string displayLName, string emailAddress, string userPassword);

		/// <summary>
		/// Gets the classes a user can tutor for.
		/// </summary>
		/// <param name="UserID">The unique identifier of the user we are getting the classes of.</param>
		/// <returns>A list of classes the specified user has signed up to tutor. This includes the subject, course name and number, and the unique identifier.</returns>
		[OperationContract]
		List<UserClass> GetUserClasses(int UserID);

		/// <summary>
		/// Removes the specified class from a user's list of classes he/she can tutor.
		/// </summary>
		/// <param name="UserID">The unique identifier of the user.</param>
		/// <param name="ClassID">The unique identifier of the class.</param>
		[OperationContract]
		void RemoveUserFromClass(int UserID, int ClassID);

		/// <summary>
		/// Adds the specified class to a user's list of classes he/she can tutor.
		/// </summary>
		/// <param name="UserID">The unique identifier of the user.</param>
		/// <param name="ClassID">The unique identifier of the class.</param>
		[OperationContract]
		void InsertStudentIntoClass(int UserID, int ClassID);

		/// <summary>
		/// Checks to see if the email address exists on the database.
		/// </summary>
		/// <param name="emailAddress">The email address of the user.</param>
		/// <returns>True if the email address is specified with an account, false if not.</returns>
		[OperationContract]
		bool IsValidUser(string emailAddress);

		/// <summary>
		/// Disconnects a user from the server.
		/// </summary>
		[OperationContract]
		void Logout();

		/// <summary>
		/// Changes the password of the specified user. Rehashes the password before storing on the database.
		/// </summary>
		/// <param name="User">All the necessary user information.</param>
		/// <param name="newPassword">The new password.</param>
		[OperationContract]
		void ChangePassword(UserInfo User, string newPassword);

		/// <summary>
		/// Deletes an account and everything associated with that account.
		/// </summary>
		/// <param name="userID">The unique identifier of the account.</param>
		[OperationContract]
		void DeleteUserAccount(int userID);

		/// <summary>
		/// Gets the stickers that have been accepted by a tutor and marked as resolved.
		/// </summary>
		/// <param name="minstarrank">The minimum star ranking required in order to see the sticker. This parameter is optional, with a default value of 0.</param>
		/// <param name="organizationID">The unique identifer of the organization to filter. This parameter is optional, with a default value of null.</param>
		/// <param name="userID">The unique identifer of the account that submitted the stickers. This parameter is optional, with a default value of null.</param>
		/// <param name="classID">The unique identifier of the class to filter the results through. This parameter is optional, with a default value of null.</param>
		/// <returns>A list of stickers that have tutors and marked as resolved.</returns>
		[OperationContract]
		List<UnstuckMESticker> GetResolvedStickers(double minstarrank = 0, Nullable<int> organizationID = null, Nullable<int> userID = null, Nullable<int> classID = null);

		/// <summary>
		/// Gets the stickers that have not been accepted by a tutor and surpassed the timeout date.
		/// </summary>
		/// <param name="minstarrank">The minimum star ranking required in order to see the sticker. This parameter is optional, with a default value of 0.</param>
		/// <param name="organizationID">The unique identifer of the organization to filter. This parameter is optional, with a default value of null.</param>
		/// <param name="userID">The unique identifer of the account that submitted the stickers. This parameter is optional, with a default value of null.</param>
		/// <param name="classID">The unique identifier of the class to filter the results through. This parameter is optional, with a default value of null.</param>
		/// <returns>A list of stickers that have not been accepted by a tutor and surpassed the timeout date.</returns>
		[OperationContract]
		List<UnstuckMESticker> GetTimedOutStickers(double minstarrank = 0, Nullable<int> organizationID = null, Nullable<int> userID = null, Nullable<int> classID = null);

		/// <summary>
		/// Returns the reviews submitted by a specific user as a student.
		/// </summary>
		/// <param name="userID">The unique identifier of the account holder.</param>
		/// <param name="minstarrank">The minimum star ranking to see reviews. This parameter is optional, with a default value of 0.</param>
		/// <returns>A list containing all the reviews submitted by the user specified as a student.</returns>
		[OperationContract]
		List<UnstuckMEReview> GetUserStudentReviews(int userID, float minstarrank = 0);

		/// <summary>
		/// Returns the reviews submitted by a specific user as a tutor.
		/// </summary>
		/// <param name="userID">The unique identifier of the account holder.</param>
		/// <param name="minstarrank">The minimum star ranking to see reviews. This parameter is optional, with a default value of 0.</param>
		/// <returns>A list containing all the reviews submitted by the user specified as a tutor.</returns>
		[OperationContract]
		List<UnstuckMEReview> GetUserTutorReviews(int userID, float minstarrank = 0);

		/// <summary>
		/// Gets the stickers submitted by a user, regardless if they are resolved or active.
		/// </summary>
		/// <param name="userID">The unique identifer of the account.</param>
		/// <param name="organizationID">The unique identifer of the organization to filter. This parameter is optional, with a default value of null.</param>
		/// <param name="minstarrank">The minimum star ranking required in order to see the sticker. This parameter is optional, with a default value of 0.</param>
		/// <param name="classID">The unique identifier of the class to filter the results through. This parameter is optional, with a default value of null.</param>
		/// <returns>A list of stickers that have been submitted by a specific user that matches the filtering criteria.</returns>
		[OperationContract]
		List<UnstuckMESticker> GetUserSubmittedStickers(int userID, Nullable<int> organizationID = null, float minstarrank = 0, Nullable<int> classID = null);

		/// <summary>
		/// Gets the stickers a user has tutored, regardless if they are resolved or active.
		/// </summary>
		/// <param name="userID">The unique identifer of the account.</param>
		/// <param name="organizationID">The unique identifer of the organization to filter. This parameter is optional, with a default value of null.</param>
		/// <param name="minstarrank">The minimum star ranking required in order to see the sticker. This parameter is optional, with a default value of 0.</param>
		/// <param name="classID">The unique identifier of the class to filter the results through. This parameter is optional, with a default value of null.</param>
		/// <returns>A list of stickers that a specific user has tutored that matches the filtering criteria.</returns>
		[OperationContract]
		List<UnstuckMESticker> GetUserTutoredStickers(int userID, Nullable<int> organizationID = null, float minstarrank = 0, Nullable<int> classID = null);

		/// <summary>
		/// Gets the stickers available to tutor. This is currently untested, though it should work.
		/// </summary>
		/// <param name="caller">The unqiue identifier of the caller of the function.</param>
		/// <param name="organizationID">The unique identifier of the of the organization to filter. This parameter is optional, with a default value of null.</param>
		/// <param name="minstarrank">The minimum star ranking required in order to see the sticker. This should be the callee's student star ranking. This paramter is optional, with a default value of 0.</param>
		/// <param name="userID">The unique identifier of the account to filter. This paramter is optional, with a default value of null.</param>
		/// <param name="classID">the unique identifier of the class to filter the results through. This parameter is optional, with a default value of null.</param>
		/// <returns>A list of stickers available to tutor that meets the filtering criteria.</returns>
		[OperationContract]
		List<UnstuckMESticker> GetActiveStickers(int caller, Nullable<int> organizationID = null, float minstarrank = 0, Nullable<int> userID = null, Nullable<int> classID = null);

		/// <summary>
		/// Associates a user with an official tutoring organization.
		/// </summary>
		/// <param name="userID">The unique identifier of the user.</param>
		/// <param name="organizationID">The unique identifier of the tutoring organization.</param>
		[OperationContract]
		void AddUserToTutoringOrganization(int userID, int organizationID);

		/// <summary>
		/// Submits a new sticker to the database and associates it with any specified tutoring organizations. Queues the sticker to be sent to qualified online users.
		/// </summary>
		/// <param name="newSticker">The new sticker.</param>
		/// <returns>The new sticker's unique identifier if it was submitted successfully, -1 if not.</returns>
		[OperationContract]
		int SubmitSticker(UnstuckMEBigSticker newSticker);

		/// <summary>
		/// Gets all the course codes in the database.
		/// </summary>
		/// <returns>A list of strings containing the course codes in the database.</returns>
		[OperationContract]
		List<string> GetCourseCodes();

		/// <summary>
		/// Gets the unique identifier of a specific class.
		/// </summary>
		/// <param name="code">The course code of the class.</param>
		/// <param name="number">The course number of the class.</param>
		/// <returns>An integer representing the unique identifier of a specific class.</returns>
		[OperationContract]
		int GetCourseIdNumberByCodeAndNumber(string code, string number);

		/// <summary>
		/// Gets the course name of a specific class.
		/// </summary>
		/// <param name="code">The course code of the class.</param>
		/// <param name="number">The course number of the class.</param>
		/// <returns>A string representing the name of a specific class.</returns>
		[OperationContract]
		string GetCourseNameByCodeAndNumber(string code, string number);

		/// <summary>
		/// Gets all the course numbers by subject.
		/// </summary>
		/// <param name="CourseCode">The course code.</param>
		/// <returns>A list of course numbers associated with a subject.</returns>
		[OperationContract]
		List<string> GetCourseNumbersByCourseCode(string CourseCode);

		/// <summary>
		/// Gets all tutoring organizations in the database.
		/// </summary>
		/// <returns>A list of organizations containing the unique identifier and the name of each organization.</returns>
		[OperationContract]
		List<Organization> GetAllOrganizations();

		/// <summary>
		/// Submits a report to the database.
		/// </summary>
		/// <param name="reportDescription">The description of the report.</param>
		/// <param name="flaggerID">The unique identifier of the client who submitted the report.</param>
		/// <param name="reviewID">The unique idenitifer of the review that is being reported.</param>
		/// <returns>The unique identifier of the newly submitted report if successul, -1 if unsuccessful.</returns>
		[OperationContract]
		int CreateReport(string reportDescription, int flaggerID, int reviewID);

        /// <summary>
        /// Submits a review to the database. Finds the other user associated with the sticker and makes them submit
        /// a review if they are online, otherwise adds it to the _ReviewList queue so they can submit it when they
        /// next log on.
        /// </summary>
        /// <param name="stickerID">The unique identifier of the sticker associated with the review.</param>
        /// <param name="reviewerID">The unique identifier of the user submitting the review.</param>
        /// <param name="starRanking">The rating given to the user being reviewed.</param>
        /// <param name="description">The description of the review.</param>
        /// <param name="isAStudent">True if the user being reviewed is a student, false otherwise.</param>
        /// <returns>Returns 0 if the review was created successfully, 1 if unsuccessful.</returns>
		[OperationContract]
		int CreateReview(int stickerID, int reviewerID, double starRanking, string description, bool isAStudent);

		/// <summary>
		/// Removes a user from their contacts.
		/// </summary>
		/// <param name="userID">The unique identifier of the callee.</param>
		/// <param name="fileID">The unique identifier of the user to removed from contacts.</param>
		/// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
		[OperationContract]
		int DeleteFriend(int userID, int friendID);

		/// <summary>
		/// Delete a report submitted by a specific user. More than likely does not work, do not use.
		/// </summary>
		/// <param name="userID">The unique identifier of the user who submitted the report.</param>
		/// <param name="reportID">The unique identifier of the report to be removed.</param>
		/// <returns>Returns 0 if successful, -1 if unsuccessful</returns>
		[OperationContract]
		int DeleteReportByUser(int userID, int reportID);

		/// <summary>
		/// Adds a user to a chat.
		/// </summary>
		/// <param name="userID">The unique identifier of the user to add to the chat.</param>
		/// <param name="chatID">The unique identifier of the chat to add the user to.</param>
		/// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
		[OperationContract]
		int InsertUserIntoChat(int userID, int chatID);

		/// <summary>
		/// Gets the chats and messages of each chat a user is associated with.
		/// </summary>
		/// <param name="userID">The unique identifier of a specific user.</param>
		/// <returns>A list of chats and the messages in each chat.</returns>
		[OperationContract]
		List<UnstuckMEChat> GetUserChats(int userID);

		/// <summary>
		/// Gets the messages and users in a specific chat.
		/// </summary>
		/// <param name="chatID">The unique identifier of a specific chat.</param>
		/// <returns>An UnstuckMEChat that contains the messages and members of the specified chat.</returns>
		[OperationContract]
		UnstuckMEChat GetSingleChat(int chatID);

		/// <summary>
		/// Associates a sticker with a chat once the sticker has been acccepted.
		/// </summary>
		/// <param name="chatID">The unique identifier of the chat.</param>
		/// <param name="stickerID">The unique identifier of the sticker.</param>
		[OperationContract]
		void AddChatToSticker(int chatID, int stickerID);

        /// <summary>
        /// Queues a message to be sent to the users in the chat.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <returns>The message ID of the message that was just inserted into the database.</returns>
        [OperationContract]
		int SendMessage(UnstuckMEMessage message);

		/// <summary>
		/// Gets the stickers when first logging in.
		/// </summary>
		/// <param name="userID">The unique identifier of a specific user.</param>
		/// <returns>A list of available stickers, containing the unqiue identifier of the class the sticker is associated with
		/// and all the information for that class, the description, the unique identifier of the sticker, the unique identifier
		/// of the user who submitted the sticker, the student ranking of that user, and the timeout date.</returns>
		[OperationContract]	
		List<UnstuckMEAvailableSticker> InitialAvailableStickerPull(int userID);

		/// <summary>
		/// Gets the information of a single class.
		/// </summary>
		/// <param name="classID">The unique identifier of a specific class.</param>
		/// <returns>A UserClass containing the course code, name, and number.</returns>
		[OperationContract]
		UserClass GetSingleClass(int classID);

		/// <summary>
		/// Gets the classes and adds them to the client. There is a much more efficient way to do this.
		/// </summary>
		/// <param name="inClass">The unique identifier of the class to be added.</param>
		/// <param name="userID">The unique identifier of the user adding the class.</param>
		[OperationContract(IsOneWay = true)]
		void AddClassesToClient(int inClass, int userID);

		/// <summary>
		/// Gets all the contacts associated with a specific user.
		/// </summary>
		/// <param name="userID">The unique identifier of a specific user.</param>
		/// <returns>A list of users containing the first name and unique identifier of each contact.</returns>
		[OperationContract]
		List<UnstuckMEChatUser> GetFriends(int userID);

		/// <summary>
		/// Gets the first name of the specified user.
		/// </summary>
		/// <param name="userID">The unique identifier of a specific user.</param>
		/// <returns>The first name of the specified user.</returns>
		[OperationContract]
		string GetUserDisplayName(int userID);

		/// <summary>
		/// Changes the privileges of a specific user.
		/// </summary>
		/// <param name="userPrivs">The new user's privlieges.</param>
		/// <param name="userID">The unique identifier of a specific user.</param>
		[OperationContract]
		void SetUserPrivileges(Privileges userPrivs, int userID);

		/// <summary>
		/// Sends an account verification email containing a code in order to activiate the account. Uses email settings configured
		/// by the server administrator to send the email.
		/// </summary>
		/// <param name="emailtype">An enum specifying the purpose of the email being sent.</param>
		/// <param name="userEmailAddress">The email address of the new user.</param>
		/// <param name="username">The first name of the new user.</param>
		/// <returns>An 8-character code that must be entered in on the client in order to activate the account.</returns>
		[OperationContract]
		string SendEmail(EmailType emailtype, string userEmailAddress, string username);

		/// <summary>
		/// Creates a new mentor organzation on the database.
		/// </summary>
		/// <param name="name">The name of the new organization.</param>
		[OperationContract]
		void CreateMentorOrg(string name);

        /// <summary>
		/// Adds a new class to the UnstuckME Database 
		/// </summary>
		/// <param name="DBClass">Passes a DBClass object that contains the (CourseName, CourseCode, CourseNUmber)</param>
        /// <returns>A boolean indicating whether or not it was able to add the class to the UnstuckME_DB</returns>
		[OperationContract]
		bool AddClass(UserClass newClass);

        /// <summary>
        /// Updates a chat message if a user in the conversation has edited it.
        /// </summary>
        /// <param name="message">The message that has been edited.</param>
        /// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        [OperationContract]
        int EditMessage(UnstuckMEMessage message);

        /// <summary>
        /// Deletes a message. Is broadcasted to the other online users in the chat once it is deleted.
        /// </summary>
        /// <param name="message">The message to be deleted.</param>
        /// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        [OperationContract]
        int DeleteMessage(UnstuckMEMessage message);

        /// <summary>
        /// When logging on, checks for any stickers that need reviews. This will only occur if the other member of the
        /// sticker has submitted a review and this user was not online when this occured.
        /// </summary>
        /// <param name="userID">The unique identifier of the user who is checking for reviews.</param>
        /// <returns>Contains the StickerID and true if they are the student, false if they are the tutor. If there is no sticker, returns 0 for the sticker ID.</returns>
        [OperationContract]
        KeyValuePair<int, bool> CheckForReviews(int userID);
    }

    [ServiceContract(CallbackContract = typeof(IServer))]
	public interface IUnstuckMEServer
	{
        /// <summary>
        /// Returns a boolean value identifying that the server is running.
        /// </summary>
        /// <returns>True.</returns>
        [OperationContract]
		bool TestNewConfig();

		/// <summary>
		/// Attempts to log in a server administrator.
		/// </summary>
		/// <param name="LoggingInAdmin">The information of the server administrator.</param>
		[OperationContract]
		void RegisterServerAdmin(AdminInfo admin);

		/// <summary>
		/// Disconnects a server administrator.
		/// </summary>
		[OperationContract]
		void AdminLogout();

		/// <summary>
		/// Logs information for actions invoked by a server administrator. Currently only writes a message to the console.
		/// </summary>
		/// <param name="message">The message to be logged.</param>
		[OperationContract]
		void AdminLogMessage(string message);

		/// <summary>
		/// Gets all the clients that are currently logged in.
		/// </summary>
		/// <returns>A list of UserInfo structures containing all the information of each online user.</returns>
		[OperationContract]
		List<UserInfo> AdminGetAllOnlineUsers();

		/// <summary>
		/// Sends a message to all users who are online.
		/// </summary>
		/// <param name="recipients">The recipients of the </param>
		/// <param name="message"></param>
		[OperationContract]
		void AdminSendMessageToUsers(List<string> recipients, string message);

		/// <summary>
		/// Sends a message to all connected clients that the server is shutting down.
		/// </summary>
		[OperationContract]
		void AdminServerShuttingDown();

		/// <summary>
		/// Registers a new tutoring organization. Can only be invoked by an administrator.
		/// </summary>
		/// <param name="organizationName">The name of the new tutoring organization.</param>
		/// <returns>The unique identifer of the newly created organzation if successful, -1 if unsuccessful.</returns>
		[OperationContract]
		int AdminCreateMentoringOrganization(string organizationName);

		/// <summary>
		/// Registers a new class. Can only be invoked by an administrator.
		/// </summary>
		/// <param name="courseName">The name of the new class.</param>
		/// <param name="courseCode">The subject of the new class.</param>
		/// <param name="courseNumber">The course number of the new class.</param>
		/// <returns>The unique identifier of the newly created class if successful, -1 if unsuccessful.</returns>
		[OperationContract]
		int AdminCreateClass(string courseName, string courseCode, int courseNumber);

		/// <summary>
		/// Remove a class from the database. Can only be invoked by an administrator.
		/// </summary>
		/// <param name="classID">The unique identifier of the class to be removed.</param>
		/// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
		[OperationContract]
		int AdminDeleteClass(int classID);

		/// <summary>
		/// Removes a tutoring organization from the database. Can only be invoked by an administrator.
		/// </summary>
		/// <param name="organizationID">The unique identifier of the organization to be removed.</param>
		/// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
		[OperationContract]
		int AdminDeleteMentoringOrganization(int organizationID);

		/// <summary>
		/// Removes a report from the database. Can only be invoked by an administrator.
		/// </summary>
		/// <param name="reportID">The unique identifier of the report to be removed.</param>
		/// <returns>Returns 0 if sucessful, -1 if unsuccessful.</returns>
		[OperationContract]
		int AdminDeleteReport(int reportID);

		/// <summary>
		/// Not implemented. Do not use.
		/// </summary>
		/// <returns>Returns a file.</returns>
		[OperationContract]
		UnstuckMEFile UploadDocument();
	}

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