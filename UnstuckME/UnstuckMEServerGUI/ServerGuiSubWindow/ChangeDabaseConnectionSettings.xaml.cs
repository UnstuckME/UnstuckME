using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.EntityClient;

namespace UnstuckMEServerGUI.ServerGuiSubWindow
{
    /// <summary>
    /// Interaction logic for ChangeDabaseConnectionSettings.xaml
    /// </summary>
    public partial class ChangeDabaseConnectionSettings : Window
    {
        private string schoolName = (System.Configuration.ConfigurationManager.AppSettings["SchoolName"]);

        private bool useWindowsAuthenfication = false;

        public ChangeDabaseConnectionSettings()
        {
            InitializeComponent();


            
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
                }
                else
                {
                    MessageBox.Show("It appears you have never configured your School's MSSQL Database with us. Please enter your database information so we can connect your new UnstuckME server to it.", "UnstuckME MSSQL Server Configuration", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                
            }
            
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonTestConnection_Click(object sender, RoutedEventArgs e)
        {
            bool displayOutput = true;
            if (testConnection(displayOutput) == true)
            {
                textBlockConnectionTestedCheck.Visibility = Visibility.Visible;
            }
            else
            {
                textBlockConnectionTestedCheck.Visibility = Visibility.Hidden;
            }
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
            bool displayOutput = false;
            if(testConnection(displayOutput) == true)
            {  
                textBlockConnectionTestedCheck.Visibility = Visibility.Visible;
                this.Close();
            }
            else
            {
                textBlockConnectionTestedCheck.Visibility = Visibility.Hidden;
            }
        }

        private bool testConnection(bool displayOutput)
        {
            bool successfullConnection = true;

            if(textBoxDatabaseName.Text == "")
                throw new Exception("Please enter the name of your Database");
            if(textBoxDataSource.Text == "")
                throw new Exception("Please enter the ip or domain address of your MSSQL server");
            if(textBoxUsername.Text == "")
                throw new Exception("Please enter a username that has READ/WRITE permissions for your MSSQL server");
            if(passwordBoxPassword.Password == "")
                throw new Exception(string.Concat("Please enter/re-enter the password associated with: ", textBoxUsername.Text));
            try
            {

                UnstuckMEServer_DBEntities SelectedDB = new UnstuckMEServer_DBEntities();
                //MessageBox.Show(SelectedDB.Database.Connection.ConnectionString.ToString());

                SelectedDB.ChangeDatabase(configConnectionStringName: "UnstuckMEServer_DBEntities",initialCatalog: textBoxDatabaseName.Text, userId: textBoxUsername.Text, password: passwordBoxPassword.Password, dataSource: textBoxDataSource.Text, integratedSecuity: useWindowsAuthenfication);

                //string message = "Connecting to " + SelectedDB.Database.Connection.ConnectionString.ToString();
                //if (displayOutput == true)
                 //MessageBox.Show(message);

                //var entityCnxStringBuilder = new EntityConnectionStringBuilder(SelectedDB.Configuration.ToString());
                //MessageBox.Show(entityCnxStringBuilder.ToString());                
                           
                using (var connection = new SqlConnection(SelectedDB.Database.Connection.ConnectionString))
                {
                    var query = "select 1";
                    if (displayOutput == true)
                        MessageBox.Show("Executing: test query");

                    var command = new SqlCommand(query, connection);

                    connection.Open();
                    command.ExecuteScalar();
                    if (displayOutput == true)
                        MessageBox.Show("SQL Query execution successful\nSQL Connection successful.");

                    if (displayOutput == false) // Actually modify the app.config here
                    {
                        System.Configuration.ConfigurationManager.AppSettings["DatabaseName"] = textBoxDatabaseName.Text;

                        Configuration exeConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                        exeConfig.ConnectionStrings.ConnectionStrings["UnstuckMEServer_DBEntities"].ConnectionString = ConnectionTools.entityCnxStringBuilder.ToString();
                        exeConfig.Save(ConfigurationSaveMode.Modified);

                        // Try and update the UnstuckME_Schools DB with the new information
                        try
                        {
                            using (UnstuckME_SchoolsEntities schoolDB = new UnstuckME_SchoolsEntities())
                            {

                                var firstDatabse =
                                (from Databases in schoolDB.Databases
                                    where Databases.School.SchoolName == schoolName
                                    select Databases).First();


                                firstDatabse.DatabaseName = textBoxDatabaseName.Text;
                                firstDatabse.DatabaseAdminUsername = textBoxUsername.Text;
                                firstDatabse.DatabaseUsingWindowsAuthen = checkBox.IsChecked;
                                firstDatabse.DatabaseIP = textBoxDataSource.Text;

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
            catch (Exception ex)
            {
                successfullConnection = false;
                MessageBox.Show(ex.Message, "Error Connecting To Database", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            return successfullConnection;
        }
    }
}
