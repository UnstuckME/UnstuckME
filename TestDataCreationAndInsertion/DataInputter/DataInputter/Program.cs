using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
    WARNING : This code has no error checking
    
    Notes: you will need to update the connection strings to contain your user
    name and your password. also the csv files are located using an absolute file path
    that you will need to update to refelct the correct location of the files
    on your pc.

    Added Info on running this program:
    run the DB recreation scripts before running this code.
    This code will take 15-20 minutes to complete its opperations.
*/




namespace DataInputter
{
   public  class DataHolderNames
    {
        public String ID;
        public String Fname;
        public String Lname;
        public String Email;
        public String Password;
        public String Privelidges;
    }

    public class DataHolderClasses
    {
        public String ID;
        public String CourseName;
        public String Code;
        public String Number;
    }

    public class DataHolderStickers
    {
        public String ID;
        public String ProblemDescription;
        public String ClassID;
        public String StudentID;
        public String TutorID;
        public String StudentReviewID;
        public String TutorReviewID;
        public String MinimumStarRanking;
        public String SubmitTime;
        public String Timeout;
    }

    public class DataHolderReviews
    {
        public String ID;
        public String StarRanking;
        public String Description;
        public String StickerID;
        public String UserID;
    }

    public class DataHolderUserToChat
    {
        public String UserID;
        public String ChatID;
    }
    public class DataHolderMessages
    {
        public String MessageID;
        public String ChatID;
        public String MessageText;
        public String UserID;
    }

    class Program
    {
        static void Main(string[] args)
        {
            string Password;
            string UserName;
            
            Console.WriteLine("Enter User Name:");
            UserName = Console.ReadLine();
            Console.WriteLine("EnterPassword:");
            Console.ForegroundColor = ConsoleColor.Black;
            Password = Console.ReadLine();
            string connectionString = "Persist Security Info=False;User ID=" + UserName + ";" + "Password=" + Password +
                ";Initial Catalog=UnStuckME_DB;Server=aura.students.cset.oit.edu";
            Console.Clear();
            Console.ResetColor();

            Console.WriteLine("Begining Inserting Users");
            InsertUsers(connectionString);
            Console.WriteLine("         Inserting Users Complete");
            Console.WriteLine("Begining Inserting Classes");
            InsertClasses(connectionString);
                       
            Console.WriteLine("         Inserting Classes Complete");

            Console.WriteLine("Begining Inserting Stickers");
            InsertStickers(connectionString);
            Console.WriteLine("         Inserting Stickers Complete");
            
            Console.WriteLine("Begining Inserting Reviews");
            InsertReviews(connectionString);
            Console.WriteLine("         Inserting Reviews Complete");
            Console.WriteLine("Begining Inserting Official Mentor Orgs");
            InsertOfficialMentoringOrgs(connectionString);
            Console.WriteLine("         Inserting Official Mentor Orgs Complete");
            Console.WriteLine("Begining Inserting Official Mentors");
            InsertOfficalMentors(connectionString);
            Console.WriteLine("         Inserting Official Mentors Complete");
            Console.WriteLine("Begining Inserting Chat");
            InsertChat(connectionString);
            Console.WriteLine("         Inserting Chat Complete");

            Console.WriteLine("Begining Inserting Messages");
            InsertMessages(connectionString);
            Console.WriteLine("         Inserting Messages Complete");
            Console.WriteLine("Done");
            
        }
        static void InsertUsers(string connectionString)
        {
            //update the file path in each insert method
            StreamReader reader = new StreamReader(File.OpenRead(@"..\..\..\..\FakeDataUsers.csv"));
            List<DataHolderNames> listA = new List<DataHolderNames>();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (!String.IsNullOrWhiteSpace(line))
                {
                    string[] values = line.Split(',');
                    if (values.Length >= 6)
                    {
                        DataHolderNames cont = new DataHolderNames();
                        cont.ID = values[0];
                        cont.Fname = values[1];
                        cont.Lname = values[2];
                        cont.Email = values[3];
                        cont.Password = values[4];
                        cont.Privelidges = values[5];

                        listA.Add(cont);
                    }
                }
            }
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (DataHolderNames item in listA)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO UserProfile (DisplayFName, DisplayLName, EmailAddress, UserPassword, Privileges, Salt) VALUES (@DisplayFName, @DisplayLName, @EmailAddress, @UserPassword, @Privileges, @Salt)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@DisplayFName", item.Fname);
                    cmd.Parameters.AddWithValue("@DisplayLName", item.Lname);
                    cmd.Parameters.AddWithValue("@EmailAddress", item.Email);
                    cmd.Parameters.AddWithValue("@UserPassword", item.Password);
                    cmd.Parameters.AddWithValue("@Privileges", item.Privelidges);
                    RandomNumberGenerator rng = new RNGCryptoServiceProvider();
                    
                        byte[] tokenData = new byte[32];
                        rng.GetBytes(tokenData);

                        string token = Convert.ToBase64String(tokenData);
                    
                    cmd.Parameters.AddWithValue("@Salt", token);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        static void InsertClasses(string connectionString)
        {
            StreamReader reader = new StreamReader(File.OpenRead(@"..\..\..\..\FakeDataClasses.csv"));
            List<DataHolderClasses> listA = new List<DataHolderClasses>();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (!String.IsNullOrWhiteSpace(line))
                {
                    string[] values = line.Split(',');
                    if (values.Length >= 5)
                    {
                        DataHolderClasses cont = new DataHolderClasses();
                        cont.ID = values[0];
                        cont.CourseName = values[1];
                        cont.Code = values[2];
                        cont.Number = values[3];
                        
                        listA.Add(cont);
                    }
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (DataHolderClasses item in listA)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Classes (CourseName, CourseCode, CourseNumber, TermOffered) VALUES (@CourseName, @CourseCode, @CourseNumber, @TermOffered)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@CourseName", item.CourseName);
                    cmd.Parameters.AddWithValue("@CourseCode", item.Code);
                    cmd.Parameters.AddWithValue("@CourseNumber", Convert.ToInt16(item.Number));                    
                    
                    cmd.ExecuteNonQuery();
                    
                }
                connection.Close();
            }
        }

        static void InsertStickers(string connectionString)
        {
            StreamReader reader = new StreamReader(File.OpenRead(@"..\..\..\..\FakeDataStickers.csv"));
            List<DataHolderStickers> listA = new List<DataHolderStickers>();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (!String.IsNullOrWhiteSpace(line))
                {
                    string[] values = line.Split(',');
                    if (values.Length >= 4)
                    {
                        DataHolderStickers cont = new DataHolderStickers();
                        cont.ID = values[0];
                        cont.ClassID = values[1];
                        cont.ProblemDescription = values[2];
                        cont.StudentID = values[3];
                        cont.TutorID= values[4];
                        //cont.StudentReviewID = values[5];
                        //cont.TutorReviewID = values[6];
                        //cont.MinimumStarRanking = values[7];
                        //cont.Timeout = values[8];
                        //cont.SubmitTime = values[9];


                        listA.Add(cont);
                    }
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (DataHolderStickers item in listA)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Sticker (ProblemDescription, ClassID, StudentID, TutorID, MinimumStarRanking, SubmitTime, Timeout) VALUES (@ProblemDescription, @ClassID, @StudentID, @TutorID, @MinimumStarRanking, @SubmitTime, @Timeout)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ProblemDescription", item.ProblemDescription);
                    cmd.Parameters.AddWithValue("@ClassID", Convert.ToInt32(item.ClassID));
                    cmd.Parameters.AddWithValue("@StudentID", Convert.ToInt32(item.StudentID));
                    cmd.Parameters.AddWithValue("@TutorID", Convert.ToInt32(item.TutorID));
                    cmd.Parameters.AddWithValue("@MinimumStarRanking", .5);
                    cmd.Parameters.AddWithValue("@SubmitTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Timeout", DateTime.MaxValue);

                    
                    cmd.ExecuteNonQuery();
                    
                }
                connection.Close();
            }
        }
        static void InsertReviews(string connectionString)
        {
            StreamReader reader = new StreamReader(File.OpenRead(@"..\..\..\..\FakeDataReviews.csv"));
            List<DataHolderReviews> listA = new List<DataHolderReviews>();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (!String.IsNullOrWhiteSpace(line))
                {
                    string[] values = line.Split(',');
                    if (values.Length >= 4)
                    {
                        DataHolderReviews cont = new DataHolderReviews();
                        cont.ID = values[0];
                        cont.StickerID = values[1];
                        cont.UserID = values[2];
                        cont.StarRanking = values[3];
                        cont.Description = values[4];
                        listA.Add(cont);
                    }
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (DataHolderReviews item in listA)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Review (StickerID, ReviewerID, StarRanking, Description) VALUES (@StickerID, @ReviewerID, @StarRanking, @Description)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@StickerID", item.StickerID);
                    cmd.Parameters.AddWithValue("@ReviewerID", item.UserID);
                    cmd.Parameters.AddWithValue("@StarRanking", Convert.ToDouble(item.StarRanking));
                    cmd.Parameters.AddWithValue("@Description", item.Description);
                    


                    
                    cmd.ExecuteNonQuery();

                    
                }
                connection.Close();
            }
        }

        static void InsertOfficialMentoringOrgs(string connectionString)
        {

        }
        static void InsertOfficalMentors(string connectionString)
        {
            //InsertUserIntoMentorProgram
        }
        static void InsertChat(string connectionString)
        {
            using (StreamReader reader = new StreamReader(File.OpenRead(@"..\..\..\..\FakeDataChat.csv")))
            {
                int countChats = 0;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        countChats++;
                    }
                }
                for (int i = 0; i < countChats; i++)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        //CreateChat
                        SqlCommand cmd = new SqlCommand("Insert Into Chat Default Values");
                        cmd.Connection = connection;
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            //InsertUserIntoChat
            List<DataHolderUserToChat> listA = new List<DataHolderUserToChat>();
            using (StreamReader reader = new StreamReader(File.OpenRead(@"..\..\..\..\FakeDataUserToChat.csv")))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        string[] values = line.Split(',');
                        if (values.Length >= 2)
                        {
                            DataHolderUserToChat cont = new DataHolderUserToChat();
                            cont.UserID = values[0];
                            cont.ChatID = values[1];

                            listA.Add(cont);
                        }
                    }
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (DataHolderUserToChat item in listA)
                {
                    SqlCommand cmd = new SqlCommand("InsertUserIntoChat");
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserID", Convert.ToInt32(item.UserID)));
                    cmd.Parameters.Add(new SqlParameter("@ChatID", Convert.ToInt32(item.ChatID)));

                    
                    cmd.ExecuteNonQuery();
                    
                }
                connection.Close();
            }

       }

        static void InsertMessages(string connectionString)
        {
            StreamReader reader = new StreamReader(File.OpenRead(@"..\..\..\..\FakeDataMessages.csv"));
            List<DataHolderMessages> listA = new List<DataHolderMessages>();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (!String.IsNullOrWhiteSpace(line))
                {
                    string[] values = line.Split(',');
                    if (values.Length >= 3)
                    {
                        DataHolderMessages cont = new DataHolderMessages();
                        cont.MessageID = values[0];
                        cont.ChatID = values[1];
                        cont.MessageText = values[2];
                        cont.UserID = values[3];
                        listA.Add(cont);
                    }
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (DataHolderMessages item in listA)
                {
                    SqlCommand cmd = new SqlCommand("InsertMessage");
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ChatID", Convert.ToInt32(item.ChatID)));
                    cmd.Parameters.Add(new SqlParameter("@Message", item.MessageText));
                    cmd.Parameters.Add(new SqlParameter("@UserID", Convert.ToInt32(item.UserID)));

                    
                    cmd.ExecuteNonQuery();
                    
                }
                connection.Close();
            }
        }
    }
}
