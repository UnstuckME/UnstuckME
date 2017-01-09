using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnstuckMEUserGUI
{
	class School
	{
		string school_name;
		byte[] school_logo;

		public School()
		{
			school_name = string.Empty;
			school_logo = null;
		}

		public School(string name, byte[] logo)
		{
			school_name = name;
			school_logo = logo;
		}

		public byte[] Logo
		{
			get { return school_logo; }
			set { school_logo = value; }
		}

		public string Name
		{
			get { return school_name; }
			set { school_name = value; }
		}
	}

	class SchoolDB_Connection
	{
		private static SqlConnection GetConnection()
		{
			SqlConnection connection = new SqlConnection();
			connection.ConnectionString =
				"Data Source=aura.students.cset.oit.edu" +
				";Initial Catalog=UnstuckME_Schools" +
				";Integrated Security=False" +
				";User ID=;Password=";
			return connection;
		}

		public static List<School> GetSchoolNamesAndImages()
		{
			SqlConnection connection = null;

			try
			{
				connection = GetConnection();
				SqlCommand command = new SqlCommand();
				command.Connection = connection;
				command.CommandText =
					"SELECT SchoolName, Logo " +
					"FROM dbo.SchoolLogo JOIN dbo.School " +
					"ON dbo.SchoolLogo.LogoID = dbo.School.SchoolID";

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
				List<School> images = new List<School>();

				for (int i = 0; reader.Read(); i++)
					images.Add(new School(reader[0].ToString(), (byte[])reader[1]));

				reader.Close();
				return images;
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				if (connection != null)
					connection.Close();
			}
		}
	}
}
