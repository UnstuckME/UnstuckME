using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace InsertSchoolsIntoDatabase
{
	class DatabaseOperations
	{
		// The directory for the images
		public static string IMAGES_PATH = "C:\\Users\\colem_000\\Documents\\OIT\\CST 316-26-36 - Junior Project\\UnstuckME\\UnstuckMECore\\Media\\";
		static string DB_USER_NAME = "UnstuckME_Student_Guest";
		static string DB_USER_PWD = "oK@yP@$$W0rd";

		public static SqlConnection GetConnection()
		{
			SqlConnection connection = new SqlConnection();
			connection.ConnectionString =
				//"Data Source=localhost\\SqlExpress;" + 
				//"Data Source=ORANGE1\\ORANGE1" +
				"Data Source=aura.students.cset.oit.edu" +
				";Initial Catalog=UnstuckME_Schools" +
				";Integrated Security=False" +
				";User ID=" + DB_USER_NAME + ";Password=" + DB_USER_PWD;
			return connection;
		}

		public static void WriteImage(string imageName)
		{
			SqlConnection connection = null;

			try
			{
				// 1. Read image from file
				string filepath = IMAGES_PATH + imageName;
				string schoolname = imageName.Split('.')[0];

				if (File.Exists(filepath) == false)
					throw new Exception("File Not Found: " + filepath);

				FileStream sourceStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);

				int streamLength = (int)sourceStream.Length;
				byte[] productImage = new byte[streamLength];
				string[] schoolname_split = imageName.Split();
				string school_email_cred = string.Empty;

				foreach (string item in schoolname_split)
				{
					if (item.Length > 3)
						school_email_cred += item[0].ToString().ToLower();
				}

				sourceStream.Read(productImage, 0, streamLength);
				sourceStream.Close();

				// 2. Write image to database
				connection = GetConnection();

				SqlCommand command = new SqlCommand();
				command.Connection = connection;
				command.CommandText =
					"INSERT INTO dbo.School (SchoolName, EmailCredentials) " +
					"VALUES (@SchoolName, @EmailCredentials)" +
				
					"INSERT INTO dbo.SchoolLogo (LogoID, Logo)" +
					"VALUES (@@IDENTITY, @SchoolLogo)";
				command.Parameters.AddWithValue("@SchoolName", schoolname);
				command.Parameters.AddWithValue("@EmailCredentials", "@" + school_email_cred + ".edu");
				command.Parameters.AddWithValue("@SchoolLogo", productImage);

				connection.Open();
				command.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				if (connection != null)
				{
					connection.Close();
				}
			}
		}

		public static Byte[] ReadImage(int imageID)
		{
			SqlConnection connection = null;

			try
			{
				connection = GetConnection();

				SqlCommand command = new SqlCommand();
				command.Connection = connection;
				command.CommandText =
					"SELECT Logo " +
					"FROM dbo.SchoolLogo " +
					"WHERE LogoID = @ImageID";
				command.Parameters.AddWithValue("@ImageID", imageID);

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				byte[] imageByteArray = null;
				if (reader.Read() == false)
					throw new Exception("Unable to read image.");
				imageByteArray = (byte[])reader[0];
				reader.Close();

				return imageByteArray;
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				if (connection != null)
				{
					connection.Close();
				}
			}
		}

		// I really don't want to think about how many ugly things I did to accomplish this.
		// This should not be used as a model, unless it models bad.
		public static int ReadExtraData(int imageID, System.Windows.Forms.TextBox SchoolNameTB, System.Windows.Forms.TextBox EmailCredTB)
		{
			SqlConnection connection = null;

			try
			{
				connection = GetConnection();

				SqlCommand command = new SqlCommand();
				command.Connection = connection;
				command.CommandText =
					"SELECT SchoolName, EmailCredentials " +
					"FROM School " +
					"WHERE SchoolID = @SchoolID";
				command.Parameters.AddWithValue("@SchoolID", imageID);

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read() == false)
					throw new Exception("Unable to read image.");

				char[] SchoolName = new char[128];
				char[] EmailCred = new char[64];

				reader.GetChars(reader.GetOrdinal("SchoolName"), 0, SchoolName, 0, 128);
				reader.GetChars(reader.GetOrdinal("EmailCredentials"), 0, EmailCred, 0, 64);

				SchoolNameTB.Text = new string(SchoolName);
				EmailCredTB.Text = new string(EmailCred);

				return 0;
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				if (connection != null)
				{
					connection.Close();
				}
			}
		}

		public static List<int> GetImageIDList()
		{
			SqlConnection connection = null;
			try
			{
				connection = GetConnection();

				SqlCommand command = new SqlCommand();
				command.Connection = connection;
				command.CommandText =
					"SELECT LogoID FROM dbo.SchoolLogo " +
					"ORDER BY LogoID";

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				List<int> imageIDList = new List<int>();
				while (reader.Read())
				{
					int imageID = (int)reader[0];
					imageIDList.Add(imageID);
				}
				reader.Close();

				return imageIDList;
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				if (connection != null)
				{
					connection.Close();
				}
			}
		}
	}
}
