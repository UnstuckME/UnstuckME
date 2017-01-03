using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace InsertSchoolsIntoDatabase
{
	class DatabaseOperations
	{
		// The directory for the images
		public static string IMAGES_PATH = "C:\\Users\\colem_000\\Documents\\OIT\\CST 316-26-36 - Junior Project\\UnstuckME\\UnstuckMECore\\Media";
		static string DB_USER_NAME = "jesse_chaney";
		static string DB_USER_PWD = "NotMyP@$$W0rd";

		public static SqlConnection GetConnection()
		{
			SqlConnection connection = new SqlConnection();
			connection.ConnectionString =
				//"Data Source=localhost\\SqlExpress;" + 
				//"Data Source=ORANGE1\\ORANGE1" +
				"Data Source=aura.students.cset.oit.edu" +
				";Initial Catalog=" + DB_USER_NAME +
				";Integrated Security=False" +
				";User ID=" + DB_USER_NAME + ";Password=" + DB_USER_PWD;
			return connection;
		}

		public static void WriteImage(int productID, string imageName)
		{
			SqlConnection connection = null;

			try
			{
				// 1. Read image from file
				string filepath = IMAGES_PATH + imageName;

				if (File.Exists(filepath) == false)
					throw new Exception("File Not Found: " + filepath);

				FileStream sourceStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);

				int streamLength = (int)sourceStream.Length;
				byte[] productImage = new byte[streamLength];
				string[] schoolname_split = imageName.Split();
				string school_email_cred = string.Empty;

				foreach (string item in schoolname_split)
					school_email_cred += schoolname_split[0][0].ToString();

				sourceStream.Read(productImage, 0, streamLength);
				sourceStream.Close();

				// 2. Write image to database
				connection = GetConnection();

				SqlCommand command = new SqlCommand();
				command.Connection = connection;
				command.CommandText =
					"INSERT INTO UnstuckME_Schools.School (SchoolName, EmailCredentials) " +
					"VALUES (@SchoolName, @EmailCredentials)";
				command.Parameters.AddWithValue("@SchoolName", imageName);
				command.Parameters.AddWithValue("@EmailCredentials", "@" + school_email_cred + ".edu");

				connection.Open();
				int schoolID = (int)command.ExecuteScalar();

				command.CommandText =
					"INSERT INTO UnstuckME_Schools.SchoolLogo (LogoID, Logo)" +
					"VALUES (@@IDENTITY, @SchoolLogo)";
				command.Parameters.AddWithValue("@SchoolLogo", productImage);
				//command.Parameters.AddWithValue("@@IDENTITY", schoolID);

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
					"SELECT ProductImage " +
					"FROM blobs.ProductImages " +
					"WHERE ImageID = @ImageID";
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
		public static int ReadExtraData(int imageID
			, System.Windows.Forms.TextBox prodIdTB, System.Windows.Forms.TextBox userIdTB, System.Windows.Forms.TextBox loadedFileTB)
		{
			SqlConnection connection = null;
			try
			{
				connection = GetConnection();

				SqlCommand command = new SqlCommand();
				command.Connection = connection;
				command.CommandText =
					"SELECT UserId, ProductId, LoadedFromFile " +
					"FROM blobs.ProductImages " +
					"WHERE ImageID = @ImageID";
				command.Parameters.AddWithValue("@ImageID", imageID);

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read() == false)
					throw new Exception("Unable to read image.");

				// Gasp! Well, I don't have to worry about deallocating it.
				char[] loadedFile = new char[256];
				char[] userId = new char[100];
				int productId;

				long bytesRead = reader.GetChars(reader.GetOrdinal("UserID"), 0, userId, 0, 100);
				productId = reader.GetInt32(reader.GetOrdinal("ProductID"));
				bytesRead = reader.GetChars(reader.GetOrdinal("LoadedFromFile"), 0, loadedFile, 0, 256);

				prodIdTB.Text = productId.ToString();
				userIdTB.Text = new string(userId);
				loadedFileTB.Text = new string(loadedFile);

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
					"SELECT LogoID FROM UnstuckME_Schools.SchoolLogo " +
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
