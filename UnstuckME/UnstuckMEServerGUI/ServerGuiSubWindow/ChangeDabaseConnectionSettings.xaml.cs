using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace UnstuckMEServerGUI.ServerGuiSubWindow
{
    /// <summary>
    /// Interaction logic for ChangeDabaseConnectionSettings.xaml
    /// </summary>
    public partial class ChangeDabaseConnectionSettings : Window
    {
        private readonly string schoolName = ConfigurationManager.AppSettings["SchoolName"];
        private int? m_databaseID;
        private bool useWindowsAuthenfication;

        public ChangeDabaseConnectionSettings()
        {
            InitializeComponent();

            try
            {
                //Load in content from school_DB (Tranlated SQL Query)
                using (UnstuckME_SchoolsEntities db = new UnstuckME_SchoolsEntities())
                {
                    var firstDatabse = (from Databases in db.Databases where Databases.School.SchoolName == schoolName select Databases).First();

                    if (firstDatabse != null)
                    {
                        textBoxDatabaseName.Text = firstDatabse.DatabaseName;
                        textBoxUsername.Text = firstDatabse.DatabaseAdminUsername;
                        if (firstDatabse.DatabaseUsingWindowsAuthen == true)
                        {
                            useWindowsAuthenfication = true;
                            checkBox.IsChecked = true;
                        }
                        textBoxDataSource.Text = firstDatabse.DatabaseIP;
                        m_databaseID = firstDatabse.DatabaseID;
                    }
                    else
                        MessageBox.Show( "It appears you have never configured your School's MSSQL Database with us. Please enter your database information so we can connect your new UnstuckME server to it.", "UnstuckME MSSQL Server Configuration", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("We seem to experiencing some difficulties connecting to the UnstuckME Server", "Unable To Connect to UnstuckME Server", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonTestConnection_Click(object sender, RoutedEventArgs e)
        {
            textBlockConnectionTestedCheck.Visibility = TestConnection(true) ? Visibility.Visible : Visibility.Hidden;
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            useWindowsAuthenfication = true;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            useWindowsAuthenfication = false;
        }

        private void textBoxSaveConnection_Click(object sender, RoutedEventArgs e)
        {
            if (TestConnection(false))
            {  
                textBlockConnectionTestedCheck.Visibility = Visibility.Visible;
                Close();
            }
            else
                textBlockConnectionTestedCheck.Visibility = Visibility.Hidden;
        }

        private bool TestConnection(bool displayOutput)
        {
            bool successfullConnection = true;

            if(textBoxDatabaseName.Text == string.Empty)
                throw new Exception("Please enter the name of your Database");
            if(textBoxDataSource.Text == string.Empty)
                throw new Exception("Please enter the ip or domain address of your MSSQL server");
            if(textBoxUsername.Text == string.Empty)
                throw new Exception("Please enter a username that has READ/WRITE permissions for your MSSQL server");
            if(passwordBoxPassword.Password == string.Empty)
                throw new Exception(string.Concat("Please enter/re-enter the password associated with: ", textBoxUsername.Text));
            try
            {
                using (UnstuckME_DBEntities SelectedDB = new UnstuckME_DBEntities())
                {
                    //MessageBox.Show(SelectedDB.Database.Connection.ConnectionString.ToString());
                    SelectedDB.ChangeDatabase(configConnectionStringName: "UnstuckME_DBEntities", initialCatalog: textBoxDatabaseName.Text, userId: textBoxUsername.Text, password: passwordBoxPassword.Password, dataSource: textBoxDataSource.Text, integratedSecuity: useWindowsAuthenfication);

                    using (var connection = new SqlConnection(SelectedDB.Database.Connection.ConnectionString))
                    {
                        string query = "select 1";
                        if (displayOutput)
                            MessageBox.Show("Executing: test query");

                        var command = new SqlCommand(query, connection);

                        connection.Open();
                        command.ExecuteScalar();
                        if (displayOutput)
                            MessageBox.Show("SQL Query execution successful\nSQL Connection successful.");

                        if (displayOutput == false) // Actually modify the app.config here
                        {
                            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                            config.AppSettings.Settings["DatabaseName"].Value = textBoxDatabaseName.Text;
                            config.Save(ConfigurationSaveMode.Modified);

                            Configuration exeConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                            exeConfig.ConnectionStrings.ConnectionStrings["UnstuckME_DBEntities"].ConnectionString = ConnectionTools.entityCnxStringBuilder.ToString();
                            exeConfig.Save(ConfigurationSaveMode.Modified);

                            // Try and update the UnstuckME_Schools DB with the new information
                            try
                            {
                                using (UnstuckME_SchoolsEntities schoolDB = new UnstuckME_SchoolsEntities())
                                {

                                    if (m_databaseID != null)
                                    {

                                        var firstDatabse = (from Databases in schoolDB.Databases
                                                            where Databases.DatabaseID == m_databaseID.Value
                                                            select Databases).First();

                                        firstDatabse.DatabaseName = textBoxDatabaseName.Text;
                                        firstDatabse.DatabaseAdminUsername = textBoxUsername.Text;
                                        firstDatabse.DatabaseUsingWindowsAuthen = checkBox.IsChecked;
                                        firstDatabse.DatabaseIP = textBoxDataSource.Text;
                                    }

                                    else
                                    {
                                        int schoolID = (from Schools in schoolDB.Schools where Schools.SchoolName == schoolName select Schools.SchoolID).First();

                                        Database tempServer = new Database
                                        {
                                            SchoolID = schoolID,
                                            DatabaseName = textBoxDatabaseName.Text,
                                            DatabaseIP = textBoxDataSource.Text,
                                            DatabaseAdminUsername = textBoxUsername.Text,
                                            DatabaseUsingWindowsAuthen = useWindowsAuthenfication
                                        };

                                        schoolDB.Databases.Add(tempServer);
                                    }

                                    schoolDB.SaveChanges();
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("ERROR: It looks like we were unable to update our UnstuckME servers with your new information. Next time you connect you may need to re-enter some of your information", "UnstuckME Servers Unable To Update", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                successfullConnection = false;
                MessageBox.Show(ex.Message, "Error Connecting To Database", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            return successfullConnection;
        }
    }
}
