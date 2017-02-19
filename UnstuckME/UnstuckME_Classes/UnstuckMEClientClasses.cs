using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UnstuckME_Classes
{
    public class UserInfo
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public float AverageStudentRank { get; set; }
        public float AverageTutorRank { get; set; }
        public int TotalTutorReviews { get; set; }
        public int TotalStudentReviews { get; set; }
        public int Privileges { get; set; }
        public byte[] UserProfilePictureBytes { get; set; }
		public string UserPassword { get; set; }
		public string Salt { get; set; }
	}

    public class UserClass
    {
        public string CourseCode { set; get; }
        public string CourseName { set; get; }
        public short CourseNumber { set; get; }
        public int ClassID { set; get; }
    }

    //This Will hold all of the information for a chat on the UserGUI
    public class UnstuckMEChat
    {
        public int ChatID { get; set; }
        public List<UnstuckMEChatUser> Users { get; set; }
        public List<UnstuckMEMessage> Messages { get; set; }
    }

    //This Represents a chat user in a UnstuckMEChat
    public class UnstuckMEChatUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public ImageSource ProfilePicture { get; set; }
    }

    //This Represents a Message in an UnstuckMEChat.
    public class UnstuckMEMessage
    {
        public int ChatID { get; set; }
        public string FilePath { get; set; }
        public int MessageID { get; set; }
        public DateTime Time { get; set; }
        public bool IsFile { get; set; }
        public string Message { get; set; }
        //public int UserID { get; set; }
        public string Username { get; set; }
        public int SenderID { get; set; }
        public List<int> UsersInConvo { get; set; }
    }

    //This Will be passed into the ChatMessage UserControl everytime a message is submitted.
    public class UnstuckMEGUIChatMessage
    {
        public int ChatID { get; set; }
        public string FilePath { get; set; }
        public int MessageID { get; set; }
        public DateTime Time { get; set; }
        public bool IsFile { get; set; }
        public string Message { get; set; }
        public int SenderID { get; set; }
        public string Username { get; set; }
        public List<int> UsersInConvo { get; set; }
        public ImageSource ProfilePic { get; set; }


        public UnstuckMEGUIChatMessage(UnstuckMEMessage inMessage, UnstuckMEChat inChat)
        {
            UsersInConvo = new List<int>();
            ChatID = inMessage.ChatID;
            FilePath = inMessage.FilePath;
            MessageID = inMessage.MessageID;
            Time = inMessage.Time;
            IsFile = inMessage.IsFile;
            Message = inMessage.Message;
            SenderID = inMessage.SenderID;
            Username = inMessage.Username;
            foreach (UnstuckMEChatUser User in inChat.Users)
            {
                if(User.UserID == SenderID)
                {
                    ProfilePic = User.ProfilePicture;
                }
                UsersInConvo.Add(User.UserID);
            }
        }

        public void SetProfilePic (ImageSource inProfilePic)
        {
            ProfilePic = inProfilePic;
        }
    }

    public class UnstuckMEReview
    {
        public int ReviewID { get; set; }
        public int StickerID { get; set; }
        public int ReviewerID { get; set; }
        public float StarRanking { get; set; }
        public string Description { get; set; }
    }

    public class UnstuckMESticker
    {
        public int StickerID { get; set; }
        public string ProblemDescription { get; set; }
        public int ClassID { get; set; }
        public int ChatID { get; set; }
        public int StudentID { get; set; }
        public int TutorID { get; set; }
        public float MinimumStarRanking { get; set; }
        public List<int> AttachedOrganizations { get; set; }
        public DateTime SubmitTime { get; set; }
        public int Timeout { get; set; }
    }

    public class UnstuckMEAvailableSticker
    {
        public int StickerID { get; set; }
        public string ProblemDescription { get; set; }
        public int ClassID { get; set; }
        public string CourseCode { set; get; }
        public string CourseName { set; get; }
        public short CourseNumber { set; get; }
        public int StudentID { get; set; }
        public double StudentRanking { get; set; }
        public DateTime Timeout { get; set; }
    }

    public class UnstuckMESchool
    {
        public int SchoolID { get; set; }
        public string SchoolName { get; set; }
        public string SchoolDomain { get; set; }
        public string SchoolEmailCredentials { get; set; }
        public string ServerIPAdress { get; set; }
        public string ServerName { get; set; }
    }
    public class Organization
    {
        public int MentorID { get; set; }
        public string OrganizationName { get; set; }
    }
}
