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
		string school_name;
		byte[] school_logo;
		string server_domain;
		string server_name;
		string server_IP;

		public School()
		{
			school_name = string.Empty;
			school_logo = null;
			server_domain = string.Empty;
			server_name = string.Empty;
			server_IP = string.Empty;
		}

		public School(string name, byte[] logo)
		{
			school_name = name;
			school_logo = logo;
		}

		public School(string name, byte[] logo, string domain, string server, string IP)
		{
			school_name = name;
			school_logo = logo;
			server_domain = domain;
			server_name = server;
			server_IP = IP;
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

		public string Domain
		{
			get { return server_domain; }
			set { server_domain = value; }
		}

		public string Server
		{
			get { return server_name; }
			set { server_name = value; }
		}

		public string IP
		{
			get { return server_IP; }
			set { server_IP = value; }
		}
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

				//this works just need data in Server table
				//foreach (var school in schools)
				//	GetServerInfo(school, connection);

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

		public static void GetServerInfo(School school, SqlConnection connection)
		{
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

				school.Domain = reader[0].ToString();
				school.Server = reader[1].ToString();
				school.IP = reader[2].ToString();

				reader.Close();
			}
			catch (Exception e)
			{
				throw e;
			}
		}
	}
}
