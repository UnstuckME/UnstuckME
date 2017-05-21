using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using UnstuckMeLoggers;

namespace UnstuckME_Classes
{
    public enum Privileges
    {
        InvalidUser, Admin, Moderator, User
    }

	public enum UnstuckMEBox
	{
		Shutdown, OKCancel, OK, YesNo, YesNoCancel
	}

	public enum UnstuckMEBoxImage
	{
		Error, Warning, Information
	}

	public enum EmailType
	{
		CreateAccount, ResetPassword
	}

    public enum StickerStatus
    {
        Available, Accepted, Resolved
    }

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
		public Privileges Privileges { get; set; }
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

		public UserClass()
		{
			CourseName = "Unnamed";
			CourseCode = "NA";
			CourseNumber = 0;
		}

		public UserClass(string name, string code, string number)
		{
			CourseName = name.Substring(0, Math.Min(name.Length, 100));
			CourseCode = code.Substring(0, Math.Min(code.Length, 5));
			CourseNumber = Convert.ToInt16(number);
		}
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
		public string EmailAddress { get; set; }
        
        public byte[] UnProccessPhot { get; set; }
        
	}

	//This Represents a Message in an UnstuckMEChat.
	public class UnstuckMEMessage
	{
        private string _filepath = string.Empty;

		public int MessageID { get; set; }
		public int ChatID { get; set; }
		public string Message { get; set; }

	    public string FilePath
	    {
	        get { return _filepath; }
	        set
	        {
	            _filepath = value;
	            FileSize = -1;

	            if (!string.IsNullOrEmpty(_filepath))
	            {
	                if (File.Exists(_filepath))
	                    FileSize = new FileInfo(_filepath).Length;
	            }
	        }
        }

	    public long FileSize { get; set; }
		public int SenderID { get; set; }
		public DateTime Time { get; set; }
		public string Username { get; set; }
		public List<int> UsersInConvo { get; set; }
	}

	//This Will be passed into the ChatMessage UserControl everytime a message is submitted.
	public class UnstuckMEGUIChatMessage
	{
        public UnstuckMEMessage ChatMessage { get; set; }
        public ImageSource ProfilePic { get; set; }

		public UnstuckMEGUIChatMessage(UnstuckMEMessage inMessage, UnstuckMEChat inChat)
		{
		    ChatMessage = new UnstuckMEMessage
		    {
		        UsersInConvo = new List<int>(),
		        ChatID = inMessage.ChatID,
                FilePath = inMessage.FilePath,
                FileSize = inMessage.FileSize,
                MessageID = inMessage.MessageID,
		        Time = inMessage.Time,
		        Message = inMessage.Message,
		        SenderID = inMessage.SenderID,
		        Username = inMessage.Username
		    };

		    foreach (UnstuckMEChatUser user in inChat.Users)
			{
				if (user.UserID == ChatMessage.SenderID)
				    ProfilePic = user.ProfilePicture;

			    ChatMessage.UsersInConvo.Add(user.UserID);
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
		public string CourseCode { get; set; }
		public string CourseName { get; set; }
		public short CourseNumber { get; set; }
		public int ChatID { get; set; }
		public int StudentID { get; set; }
		public int? TutorID { get; set; }
		public double StudentRanking { get; set; }
		public float MinimumStarRanking { get; set; }
		public List<int> AttachedOrganizations { get; set; }
		public DateTime SubmitTime { get; set; }
		public DateTime Timeout { get; set; }

		public UnstuckMESticker()
		{ }

		public UnstuckMESticker(UnstuckMEBigSticker s)
		{
			StickerID = s.StickerID;
			ProblemDescription = s.ProblemDescription;
			ClassID = s.Class.ClassID;
			ChatID = s.ChatID;
			StudentID = s.StudentID;
			TutorID = s.TutorID;
			MinimumStarRanking = Convert.ToSingle(s.MinimumStarRanking);
			AttachedOrganizations = s.AttachedOrganizations;
			SubmitTime = s.SubmitTime;
			Timeout = s.SubmitTime.AddSeconds(s.TimeoutInt);
		}
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

	public class UnstuckMEBigSticker
	{
		public int StickerID { get; set; }
		public string ProblemDescription { get; set; }
		public int StudentID { get; set; }
		public double StudentRanking { get; set; }
		public int ChatID { get; set; }
		public int TutorID { get; set; }
		public double TutorRanking { get; set; }
		public double MinimumStarRanking { get; set; }
		public UserClass Class { get; set; }
		public List<int> AttachedOrganizations { get; set; }
		public DateTime SubmitTime { get; set; }
		public int TimeoutInt { get; set; }
		public DateTime Timeout { get; set; }
	}

	public class UnstuckMESchool
	{
		public int    SchoolID { get; set; }
		public string SchoolName { get; set; }
		public string SchoolDomain { get; set; }
		public string SchoolEmailCredentials { get; set; }
		public string ServerIPAdress { get; set; }
		public string ServerName { get; set; }
		public string LogoLastModified { get; set; }
	}

	public class Organization : IEquatable<Organization>
	{
		public int MentorID { get; set; }
		public string OrganizationName { get; set; }

        public bool Equals(Organization other)
        {
            return other != null && MentorID == other.MentorID && OrganizationName == other.OrganizationName;
        }
    }

	public static class Thumbnail
	{
		public static void Convert(ref Image image)
		{
			int newWidth = (int)(0.1 * image.Width), newHeight = (int)(0.1 * image.Height);

			Bitmap bitmap = new Bitmap(image, newWidth, newHeight);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				graphics.DrawImage(image, 0, 0, newWidth, newHeight);
			}
		}
	}

	public class UnstuckMEDirectory
	{
		private static readonly string programDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData, Environment.SpecialFolderOption.Create) + @"\UnstuckME";
		private static readonly string schoolDir = Path.Combine(programDir, "Schools");
		private static readonly string chatDir = Path.Combine(programDir, "Chats");
		private static readonly string friendsDir = Path.Combine(programDir, "Friends");

		//Intentionally one way getters because a user should not modify a program directory
		public string ProgramDir
		{
			get { return programDir; }
		}
		
		public string SchoolDir
		{ 
			get { return schoolDir; }
		}

		public string ChatDir
		{
			get { return chatDir; }
		}

		public string FriendsDir
		{
			get { return friendsDir; }
		}

		public string MakeDir(string newDirName)
		{
			string newDirectory = Path.Combine(programDir, newDirName);
			Directory.CreateDirectory(newDirectory);
			return newDirectory;
		}

		public string MakeChatDir(string newDirName)
		{
			string newDirectory = Path.Combine(chatDir, newDirName);
			Directory.CreateDirectory(newDirectory);
			
			return newDirectory;
		}

		public void AddChatDatFile(string chatNum)
		{
			string chatDatPath = Path.Combine(chatDir, chatNum, ".dat");
			if (!File.Exists(chatDatPath))
			{
				try
				{
					File.WriteAllText(chatDatPath, "<MemeberGroup>" + Environment.NewLine + "</MemeberGroup>");
				}
				catch (Exception ex)
				{
				    var trace = new StackTrace(ex, true).GetFrame(0).GetMethod();
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_UNABLE_TO_READWRITE, ex.Message, trace.Name);
				}
			}
		}

        public async void AddNewFriend(UnstuckMEChatUser newFriend)
        {
            string byteString = Encoding.UTF8.GetString(newFriend.UnProccessPhot, 0, newFriend.UnProccessPhot.Length);
            string friendInfo = string.Format("<Friend>" + Environment.NewLine +
                                              "\t<UserID>{0}</UserID>" + Environment.NewLine +
                                              "\t<Username>{1}</Username>" + Environment.NewLine +
                                              "\t<UserEmail>{2}</UserEmail>" + Environment.NewLine +
                                              "\t<UserProfilePic>" + Environment.NewLine + 
                                              "\t\t{3}" + Environment.NewLine + 
                                              "</UserProfilePic>" + Environment.NewLine +
                                              "</Friend>",
                                              newFriend.UserID, newFriend.UserName, newFriend.EmailAddress, byteString);

            await WriteToFileAsync(Path.Combine(friendsDir, newFriend.UserID + ".dat"), friendInfo);
        }

        public void AddNewMsgFile(string chatNum, bool ifNeeded = false)
		{
			string chatPath = Path.Combine(chatDir, chatNum);
			int maxChatFile = Directory.GetFiles(chatPath).Where(name => !name.EndsWith(".dat")).Select(f => Convert.ToInt32(f.Replace(chatPath + "\\msg_", ""))).DefaultIfEmpty(999).Max();

			if (ifNeeded == false || maxChatFile == 999)
				File.WriteAllText(chatPath + @"\msg_" + (maxChatFile + 1), "<MessageGroup>" + Environment.NewLine + "</MessageGroup>");
		}

		public bool DeleteFriend(int? friendId)
		{
			try
			{
				string deleteFriend = Path.Combine(FriendsDir, friendId + ".dat");
				if (File.Exists(deleteFriend))
				{
					File.Delete(deleteFriend);
					return true;
				}
			}
			catch (Exception ex)
			{
			    var trace = new StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_UNABLE_TO_READWRITE, ex.Message, trace.Name);
			}

			return false;
		}

		public List<int?> GetAllFriends()
		{
			try
			{
				return Directory.GetFiles(friendsDir).Select(f => Convert.ToInt32(f.Replace(friendsDir + "\\", "").Replace(".dat", ""))).DefaultIfEmpty(-1).Select(i => (int?)i).ToList();
			}
			catch (Exception ex)
			{
			    var trace = new StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_UNABLE_TO_READWRITE, ex.Message, trace.Name);
				return null;
			}
		}

		public UnstuckMEDirectory()
		{
			//This will not recreate the directory if it already exists
			Directory.CreateDirectory(programDir);
			Directory.CreateDirectory(schoolDir);
			Directory.CreateDirectory(chatDir);
			Directory.CreateDirectory(friendsDir);
		}

        static async Task WriteToFileAsync(string filePath, string text)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            byte[] buffer = Encoding.Unicode.GetBytes(text);

            const int offset = 0;
            const int sizeOfBuffer = 4096;

            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, sizeOfBuffer, true))
                {
                    await fileStream.WriteAsync(buffer, offset, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                var trace = new StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_UNABLE_TO_READWRITE, ex.Message, trace.Name);
            }
        }
    }

    public sealed class ComboboxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}