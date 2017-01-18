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

namespace UnstuckMEServerGUI.ServerGuiSubWindow
{
    /// <summary>
    /// Interaction logic for ChangeDabaseConnectionSettings.xaml
    /// </summary>
    public partial class ChangeDabaseConnectionSettings : Window
    {
        private bool useWindowsAuthenfication = false;

        public ChangeDabaseConnectionSettings()
        {
            InitializeComponent();
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

            try
            {

                UnstuckMEServer_DBEntities SelectedDB = new UnstuckMEServer_DBEntities();
                SelectedDB.ChangeDatabase(configConnectionStringName: "UnstuckMEServer_DBEntities", userId: textBoxUsername.Text, password: passwordBoxPassword.Password, dataSource: textBoxDataSource.Text, integratedSecuity: useWindowsAuthenfication);

                string message = "Connecting to" + SelectedDB.Database.Connection.ConnectionString.ToString();

                MessageBox.Show(message);
                using (var connection = new SqlConnection(SelectedDB.Database.Connection.ConnectionString))
                {
                    var query = "select 1";
                    if (displayOutput == true)
                        MessageBox.Show("Executing: test query");

                    var command = new SqlCommand(query, connection);

                    connection.Open();
                    if (displayOutput == true)
                        MessageBox.Show("SQL Connection successful.");

                    command.ExecuteScalar();
                    if (displayOutput == true)
                        MessageBox.Show("SQL Query execution successful.");

                    if (displayOutput == false)
                    {
                        Configuration exeConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                        exeConfig.ConnectionStrings.ConnectionStrings["UnstuckMEServer_DBEntities"].ConnectionString = SelectedDB.Database.Connection.ConnectionString.ToString();
                        exeConfig.Save(ConfigurationSaveMode.Modified);
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
