using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnstuckMEUserGUI
{
	//Holds the information gathered from the school DB
	class School
	{
		public School()
		{
			Name = string.Empty;
			Logo = null;
			Domain = string.Empty;
			Server = string.Empty;
			IP = string.Empty;
		}

		public School(string name, byte[] logo)
		{
			Name = name;
			Logo = logo;
		}

		public School(string name, byte[] logo, string domain, string server, string ip)
		{
			Name = name;
			Logo = logo;
			Domain = domain;
			Server = server;
			IP = ip;
		}

		public byte[] Logo { get; set; }
		public string Name { get; set; }
		public string Domain { get; set; }
		public string Server { get; set; }
		public string IP { get; set; }
	}

	//Handles all School DB connections and queries
	static class SchoolDB_Connection
	{
		static string DB_USER_NAME = "UnstuckME_Student_Guest";
		static string DB_USER_PWD = "oK@yP@$$W0rd";

		private static SqlConnection GetConnection()
		{
			SqlConnection connection = new SqlConnection();
			connection.ConnectionString =
				"Data Source=aura.students.cset.oit.edu" +
				";Initial Catalog=UnstuckME_Schools" +
				";Integrated Security=False" +
				";User ID=" + DB_USER_NAME + ";Password=" + DB_USER_PWD;
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
				List<School> schools = new List<School>();

				for (int i = 0; reader.Read(); i++)
					schools.Add(new School(reader[0].ToString(), (byte[])reader[1]));

				reader.Close();

				for (int i = 0; GetServerInfo(schools[i], connection); i++);

				return schools;
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

		public static bool GetServerInfo(School school, SqlConnection connection)
		{
			bool result = false;

			try
			{
				SqlCommand command = new SqlCommand();
				command.Connection = connection;
				command.CommandText =
					"SELECT ServerDomain, ServerName, ServerIPAddress " +
					"FROM dbo.Server JOIN dbo.School " +
					"ON dbo.Server.SchoolID = dbo.School.SchoolID " +
					"WHERE dbo.School.SchoolName = @schoolname";
				command.Parameters.AddWithValue("@schoolname", school.Name);

				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
				{
					result = true;
					school.Domain = reader[0].ToString();
					school.Server = reader[1].ToString();
					school.IP = reader[2].ToString();
				}

				reader.Close();
				return result;
			}
			catch (Exception e)
			{
				throw e;
			}
		}
	}
}
