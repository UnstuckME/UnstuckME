﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Configuration;
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
using UnstuckME_Classes;
using UnstuckMEInterfaces;
using UnstuckMeLoggers;
using UnstuckMEUserGUI.SubWindows;

namespace UnstuckMEUserGUI
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		private static List<UnstuckMESchool> schools;
		private string m_SchoolName = null;
		private string m_orginalSchoolName = null;
		private string m_SchoolInfoFilePath = null;
		private string verification_code = null;
		private bool content_rendered = true;
		private short failed_attempts = 0;

		public LoginWindow()
		{
			InitializeComponent();
            UnstuckME.ChannelFactory = new DuplexChannelFactory<IUnstuckMEService>(new ClientCallback(), "UnstuckMEServiceEndPoint");
            UnstuckME.Server = UnstuckME.ChannelFactory.CreateChannel();
            UnstuckME.Stream_ChannelFactory = new ChannelFactory<IUnstuckMEFileStream>("UnstuckMEStreamingEndPoint");
            UnstuckME.FileStream = UnstuckME.Stream_ChannelFactory.CreateChannel();
            UnstuckME.Blue = buttonCreateAccount.Background;
            UnstuckME.Red = buttonCancel.Background;

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            m_orginalSchoolName = m_SchoolName = config.AppSettings.Settings["SchoolName"].Value;

			try
			{
                UnstuckME.ProgramDir = new UnstuckMEDirectory();
				m_SchoolInfoFilePath = System.IO.Path.Combine(UnstuckME.ProgramDir.SchoolDir, "schoolLogo.dat");

				if (File.Exists(m_SchoolInfoFilePath) != true)
				{
					FileStream fs = new FileStream(m_SchoolInfoFilePath, FileMode.CreateNew);
					fs.Close();

                    using (StreamWriter file = new StreamWriter(m_SchoolInfoFilePath, false))
                    {
                        file.WriteLine("Last Modified = NULL");
                        file.WriteLine("Photo ID = NULL");
                        file.WriteLine("Photo Info = NULL");
                    }
                }

                string remember_me = config.AppSettings.Settings["RememberMe"].Value;
                string m_username = config.AppSettings.Settings["Username"].Value;
                string m_password = config.AppSettings.Settings["Password"].Value;

                if (m_username != string.Empty)
                {
                    //System.Windows.Media.Brush brush = (System.Windows.Media.Brush)(new BrushConverter().ConvertFromString("#FFCFCF56"));

                    textBoxUserName_GotFocus(null, null);
                    textBoxPasswordPreview_GotFocus(null, null);
                    textBoxUserName.Text = m_username;
                    //textBoxUserName.Background = brush;
                    passwordBox.Password = m_password;
                    //passwordBox.Background = brush;
                    checkboxRememberMe.IsChecked = true;

                    if (remember_me == "true")
                        buttonLogin_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected ERROR: Unable to load cached file - Unexpected behavior may occur");
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_UNABLE_TO_READWRITE, ex.Message);
            }
		}

		private async void Window_ContentRendered(object sender, EventArgs e)
		{
			schools = await LoadSchoolsAsync();
			foreach (UnstuckMESchool school in schools)
			{
				comboBoxSchools.Items.Add(new ComboBoxItem().Content = school.SchoolName);
			}

			if (!string.IsNullOrEmpty(m_SchoolName))
				comboBoxSchools.SelectedIndex = comboBoxSchools.Items.IndexOf(m_SchoolName);
			else
				comboBoxSchools.SelectedIndex = comboBoxSchools.Items.IndexOf("Local Ip");

            content_rendered = false;
        }

        private Task<List<UnstuckMESchool>> LoadSchoolsAsync()
		{
			return Task.Factory.StartNew(() => LoadSchools());
		}

		List<UnstuckMESchool> LoadSchools()
		{
			//PRETTY sure you could pass a list of schools to the LINQ to fill
			List<UnstuckMESchool> tempSchools = new List<UnstuckMESchool>();
			using (UnstuckME_SchoolsEntities db = new UnstuckME_SchoolsEntities())
			{
				var dbSchools = from s in db.Schools
								join l in db.SchoolLogoes on s.SchoolID equals l.LogoID
								//join j in db.Servers on s.SchoolID equals j.SchoolID      /*No Schools have a server currently*/
								select new
								{
									SchoolName = s.SchoolName,                                      //join l in db.SchoolLogoes on s.SchoolID equals l.LogoID /*No Logos need to be pulled*/   
									EmailCredentials = s.EmailCredentials,
									SchoolID = s.SchoolID,
									LastModified = l.LastModified//,
									//Domain = j.ServerDomain,
									//ServerName = j.ServerName,
									//IPAddress = j.ServerIPAddress
								};

				foreach (var dbschool in dbSchools)
				{
					UnstuckMESchool newSchool = new UnstuckMESchool()
					{
						SchoolID = dbschool.SchoolID,
						SchoolName = dbschool.SchoolName,
						SchoolEmailCredentials = dbschool.EmailCredentials,
						LogoLastModified = dbschool.LastModified.ToString()//,
						//SchoolDomain = dbschool.Domain,
						//ServerName = dbschool.ServerName,
						//ServerIPAdress = dbschool.IPAddress
					};

					tempSchools.Add(newSchool);
				}
			}
			return tempSchools;
		}

		private async void buttonLogin_Click(object sender, RoutedEventArgs e)
		{
			bool isValid = false;

			string emailAttempt = textBoxUserName.Text;
			string passwordAttempt = passwordBox.Password;
			_labelInvalidLogin.Visibility = Visibility.Hidden;
			try
			{
				if (textBoxUserName.Text.Length <= 0)
					throw new Exception("Enter an Email Address");
				if (passwordBox.Password.Length <= 6 || passwordBox.Password.Length >= 32)
					throw new Exception("Enter a Valid Password");
				//if (comboBoxSchools.SelectedValue.ToString() != "Oregon Institute of Technology") // This is a serious hack around for the presentation
					//throw new Exception("Unable to connect to server");
				
				isValid = true;
			}
			catch (Exception ex)
			{
				failed_attempts++;
				if (failed_attempts > 5)
				{
					buttonResetPassword.IsEnabled = true;
					buttonResetPassword.Visibility = Visibility.Visible;
				}

				if (ex.Message != "Unable to connect to server")
					UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message);
				else
					UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, "NET TCP client issued a request, but received no or failed response along specified channel");

				_labelInvalidLogin.Content = ex.Message;
				passwordBox.Password = string.Empty;
				textBoxUserName_TextChanged(sender, e as TextChangedEventArgs);
				_labelInvalidLogin.Visibility = Visibility.Visible;
				isValid = false;
			}
			if (isValid)
			{
				try
				{
					UnstuckME.User = await Task.Factory.StartNew(() => ServerLoginAttemptAsynch(emailAttempt, passwordAttempt));

					if (UnstuckME.User.EmailAddress.ToLower() != emailAttempt.ToLower())
					{
						_labelInvalidLogin.Content = "Invalid Username/Password";
						throw new Exception();
					}
					else
					{
						var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                        if (checkboxRememberMe.IsChecked.Value)
                        {
                            config.AppSettings.Settings["Username"].Value = textBoxUserName.Text;
                            config.AppSettings.Settings["Password"].Value = passwordBox.Password;
                            config.AppSettings.Settings["RememberMe"].Value = "true";
                        }
                        else
                        {
                            config.AppSettings.Settings["Username"].Value = string.Empty;
                            config.AppSettings.Settings["Password"].Value = string.Empty;
                            config.AppSettings.Settings["RememberMe"].Value = "false";
                        }

						config.AppSettings.Settings["SchoolName"].Value = comboBoxSchools.SelectedValue.ToString();
						ChannelEndpointElement endpoint = ((ClientSection)config.GetSection("system.serviceModel/client")).Endpoints[0];
						//endpoint.Address = new Uri("net.tcp://" + (comboBoxSchools.SelectedItem as UnstuckMESchool).ServerIPAdress + @"/UnstuckMEService");
						config.Save();

						UnstuckMEWindow mainWindow = new UnstuckMEWindow();
						mainWindow.Show();
						this.Close();
					}
				}
				catch (Exception exp)
				{
					failed_attempts++;
					if (failed_attempts > 5)
					{
						buttonResetPassword.IsEnabled = true;
						buttonResetPassword.Visibility = Visibility.Visible;
					}

					isValid = false;
					_labelInvalidLogin.Visibility = Visibility.Visible;
					UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, exp.Message);

					try
					{
						UnstuckME.ChannelFactory.Abort();
						UnstuckME.ChannelFactory = new DuplexChannelFactory<IUnstuckMEService>(new ClientCallback(), "UnstuckMEServiceEndPoint");
						UnstuckME.Server = UnstuckME.ChannelFactory.CreateChannel();
					}
					catch (Exception exp2)
					{
						MessageBox.Show("There is a problem connecting to the server. Please Contact Your Server Administrator. UnstuckME will now close.", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
						UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp2.Message);
						this.Close();
					}
				}
			}
		}
		//Warning was bothering me. Sorry If I forget to uncomment this.
		//private async void LoadSchoolLogo()
		//{ 
		//    using (UnstuckME_SchoolsEntities db = new UnstuckME_SchoolsEntities())
		//    {
		//        var dbSchools = (from schoolLogos in db.Schools select new { logo = schoolLogos.SchoolLogo }).First();
		//    }

		//}

		private UserInfo ServerLoginAttemptAsynch(string emailAttempt, string passwordAttempt)
		{
			UserInfo temp = new UserInfo();
			this.Dispatcher.Invoke(() =>
			{
				try
				{
					temp = UnstuckME.Server.UserLoginAttempt(emailAttempt, passwordAttempt);
				}
				catch (Exception exp)
				{
					UnstuckMEUserEndMasterErrLogger logger = UnstuckMEUserEndMasterErrLogger.GetInstance();
					logger.WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message);
					_labelInvalidLogin.Content = "Server Unavailable!";
				}
			});

			return temp;
		}

		private void sendVerificationCode_Click(object sender, RoutedEventArgs e)
		{
			bool validCredentials = false;

			try
			{
				Exception invalidCreds = new Exception("Invalid Credentials!");
				if (textBoxCreateEmailAddress.Text.Length <= 0)
					throw invalidCreds;
				if (textBoxCreateFirstName.Text.Length <= 0)
					throw invalidCreds;
				if (textBoxCreateLastName.Text.Length <= 0)
					throw invalidCreds;
				if (passwordBoxCreate.Password.Length <= 6)
					throw invalidCreds;
				if (passwordBoxCreate.Password.Length >= 32)
					throw invalidCreds;
				if (passwordBoxCreate.Password != passwordBoxCreateConfirm.Password)
					throw invalidCreds;

				validCredentials = true;
			}
			catch (Exception exp)
			{
				UnstuckMEUserEndMasterErrLogger logger = UnstuckMEUserEndMasterErrLogger.GetInstance();
				logger.WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, exp.Message);
				validCredentials = false;
				labelCreateIncorrectCreds.Visibility = Visibility.Visible;
			}

			if (validCredentials)
			{
				AccountVerificationGrid.IsEnabled = true;
				AccountVerificationGrid.Visibility = Visibility.Visible;
				buttonResendVerificationCode_Click(sender, e);
			}
		}

		private async void buttonCreate_Click(object sender, RoutedEventArgs e)
		{
			if (textboxVerificationCode.Text == verification_code)
			{
				try
				{
					if (await Task.Factory.StartNew(() => CreateUserAsynch()))
					{
						int userID = await Task.Factory.StartNew(() => GetUserIDAsynch());
						await Task.Factory.StartNew(() => InsertProfilePictureAsynch(userID));
					}
					else
					{
						throw new Exception("Error occurred creating a new user, Please Contact Your Server Administrator");
					}
					_LoginGrid.IsEnabled = true;
					textBoxUserName.Text = textBoxCreateEmailAddress.Text;
					passwordBox.Password = passwordBoxCreate.Password;
					_AccountCreationGrid.IsEnabled = false;
					_AccountCreationGrid.Visibility = Visibility.Hidden;
					AccountVerificationGrid.IsEnabled = false;
					AccountVerificationGrid.Visibility = Visibility.Collapsed;
					_LoginGrid.Visibility = Visibility.Visible;
					AfterUserCreationTextBoxandPasswordBoxUpdate();
				}
				catch (Exception ex)
				{
					UnstuckMEUserEndMasterErrLogger logger = UnstuckMEUserEndMasterErrLogger.GetInstance();
					logger.WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message);

					MessageBox.Show(ex.Message, "Account Creation Error", MessageBoxButton.OK, MessageBoxImage.Error);
					try
					{
						UnstuckME.ChannelFactory.Abort();
						UnstuckME.ChannelFactory = new DuplexChannelFactory<IUnstuckMEService>(new ClientCallback(), "UnstuckMEServiceEndPoint");
						UnstuckME.Server = UnstuckME.ChannelFactory.CreateChannel();
					}
					catch (Exception exp)
					{
						MessageBox.Show("There is a problem re-connecting to the server. Please Contact Your Server Administrator. UnstuckME will now close.", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
						logger.WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message);
						this.Close();
					}
				}
			}
			else
				labelVerificationError.Visibility = Visibility.Visible;
		}

		private bool CreateUserAsynch()
		{
			bool temp = false;
			this.Dispatcher.Invoke(() =>
			{
				try
				{
					temp = UnstuckME.Server.CreateNewUser(textBoxCreateFirstName.Text, textBoxCreateLastName.Text, textBoxCreateEmailAddress.Text, passwordBoxCreate.Password);

				}
				catch (Exception exp)
				{
					UnstuckMEUserEndMasterErrLogger logger = UnstuckMEUserEndMasterErrLogger.GetInstance();
					logger.WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message);
				}
			});
			return temp;
		}

		private int GetUserIDAsynch()
		{
			int temp = -1;
			this.Dispatcher.Invoke(() =>
			{
				try
				{
					temp = UnstuckME.Server.GetUserID(textBoxCreateEmailAddress.Text);
				}
				catch (Exception exp)
				{
					UnstuckMEUserEndMasterErrLogger logger = UnstuckMEUserEndMasterErrLogger.GetInstance();
					logger.WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message);
				}
			});
			return temp;
		}

		private void InsertProfilePictureAsynch(int userID)
		{
			this.Dispatcher.Invoke(() =>
			{
				ImageConverter converter = new ImageConverter();
				byte[] avatar = (byte[])converter.ConvertTo(Properties.Resources.UserBlue, typeof(byte[]));
                try
                {
                    using (UnstuckMEStream stream = new UnstuckMEStream(avatar, true))
                    {
                        stream.User = new UserInfo()
                        {
                            UserID = userID
                        };
                        UnstuckME.FileStream.SetProfilePicture(stream);
                    }
				}
				catch (Exception exp)
				{
					UnstuckMEUserEndMasterErrLogger logger = UnstuckMEUserEndMasterErrLogger.GetInstance();
					logger.WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message);
				}
			});
		}
		private void buttonCreateAccount_MouseEnter(object sender, MouseEventArgs e)
		{
			buttonCreateAccount.Foreground = System.Windows.Media.Brushes.Black;
		}

		private void buttonCreateAccount_MouseLeave(object sender, MouseEventArgs e)
		{
			buttonCreateAccount.Foreground = System.Windows.Media.Brushes.White;
		}

		private void buttonCreateAccount_Click(object sender, RoutedEventArgs e)
		{
			_LoginGrid.IsEnabled = false;
			_LoginGrid.Visibility = Visibility.Hidden;
			_AccountCreationGrid.IsEnabled = true;
			_AccountCreationGrid.Visibility = Visibility.Visible;
			buttonResetPassword.IsEnabled = false;
			buttonResetPassword.Visibility = Visibility.Collapsed;
		}

		private void buttonCancel_MouseEnter(object sender, MouseEventArgs e)
		{
			buttonCreateAccount.Foreground = System.Windows.Media.Brushes.Black;
		}

		private void buttonCancel_MouseLeave(object sender, MouseEventArgs e)
		{
			buttonCreateAccount.Foreground = System.Windows.Media.Brushes.White;
		}
		private void buttonCreate_MouseEnter(object sender, MouseEventArgs e)
		{
			buttonCreateAccount.Foreground = System.Windows.Media.Brushes.Black;
		}

		private void buttonCreate_MouseLeave(object sender, MouseEventArgs e)
		{
			buttonCreateAccount.Foreground = System.Windows.Media.Brushes.White;
		}

		private void buttonCancel_Click(object sender, RoutedEventArgs e)
		{
			_AccountCreationGrid.IsEnabled = false;
			_AccountCreationGrid.Visibility = Visibility.Hidden;
			_LoginGrid.IsEnabled = true;
			_LoginGrid.Visibility = Visibility.Visible;
			textBoxCreateFirstName.Text = string.Empty;
			textBoxCreateLastName.Text = string.Empty;
			textBoxCreateEmailAddress.Text = string.Empty;
			passwordBoxCreate.Password = string.Empty;
			passwordBoxCreateConfirm.Password = string.Empty;
			passwordBoxCreate_PasswordChanged(sender, e);
		}

		private void AfterUserCreationTextBoxandPasswordBoxUpdate()
		{
			textBoxUserName.Foreground = System.Windows.Media.Brushes.Black;
			textBoxUserName.FontStyle = FontStyles.Normal;
			textBoxPasswordPreview.Visibility = Visibility.Hidden;
			textBoxPasswordPreview.IsEnabled = false;
			passwordBox.Visibility = Visibility.Visible;
		}

		private void textBoxUserName_GotFocus(object sender, RoutedEventArgs e)
		{
			if (textBoxUserName.Text == "Example@oit.edu")
			{
				textBoxUserName.Text = string.Empty;
				textBoxUserName.Foreground = System.Windows.Media.Brushes.Black;
				textBoxUserName.FontStyle = FontStyles.Normal;
			}
		}

		private void textBoxPasswordPreview_GotFocus(object sender, RoutedEventArgs e)
		{
			textBoxPasswordPreview.Visibility = Visibility.Hidden;
			textBoxPasswordPreview.IsEnabled = false;
			passwordBox.Visibility = Visibility.Visible;
			passwordBox.Focus();
		}

		private void textBoxUserName_LostFocus(object sender, RoutedEventArgs e)
		{
			if (textBoxUserName.Text == string.Empty)
			{
				textBoxUserName.Text = "Example@oit.edu";
				textBoxUserName.Foreground = System.Windows.Media.Brushes.Gray;
				textBoxUserName.FontStyle = FontStyles.Italic;
			}
		}

		private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (passwordBox.Password == string.Empty)
			{
				textBoxPasswordPreview.Visibility = Visibility.Visible;
				textBoxPasswordPreview.IsEnabled = true;
				passwordBox.Visibility = Visibility.Hidden;
			}
		}

		private string FindDrive()
		{
			bool foundCDrive = false;
			string driveToUse = null;
			DriveInfo[] myDrives = DriveInfo.GetDrives();

			foreach (DriveInfo drive in myDrives)
			{
				if (drive.Name.ToLower() == @"c:\")
				{
					foundCDrive = true;
					driveToUse = drive.Name;
					break;
				}
			}

			if (foundCDrive != true)
			{
				driveToUse = myDrives[0].Name;
			}

			return driveToUse;
		}

		private void comboBoxSchools_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox changedCombo = (ComboBox)sender;
			string selectedItem = changedCombo.SelectedItem.ToString();

			if (comboBoxSchools.SelectedIndex != 0)
			{
				//Choose a new school
				if (m_SchoolName != comboBoxSchools.SelectedValue.ToString())
				{
					System.Configuration.ConfigurationManager.AppSettings["SchoolName"] = selectedItem;
					m_SchoolName = selectedItem;
				}

				foreach (UnstuckMESchool school in schools)
				{
					if (school.SchoolName == m_SchoolName)
					{
						CheckSchoolLogo(school.SchoolID);
						break;
					}
				}
			}
		}

		private void CheckSchoolLogo(int logoID)
		{
			string lastmodifiedDate = null;

			bool lastModifiedMatches = false;
			bool logoIdMatches = false;
			bool stopCheck = false;
			bool newLogoNeeded = true;

			using (UnstuckME_SchoolsEntities db = new UnstuckME_SchoolsEntities())
			{
				lastmodifiedDate = (from l in db.SchoolLogoes where l.LogoID == logoID select new { lastModified = l.LastModified }).First().ToString();

				try
				{
					string line = "";
					StreamReader file = new StreamReader(m_SchoolInfoFilePath);

					while (stopCheck != true && ((line = file.ReadLine()) != null))
					{
						if (line.Contains("Last Modified ="))
						{
							if (line.Contains(lastmodifiedDate))
							{
								lastModifiedMatches = true;
							}
							else
							{
								stopCheck = true;
							}
						}
						else if (line.Contains("Photo ID ="))
						{
							if (line.Contains(logoID.ToString()))
							{
								logoIdMatches = true;
							}
							else
							{
								stopCheck = true;
							}
						}

						if (lastModifiedMatches == true && logoIdMatches == true)
						{
							stopCheck = true;
							newLogoNeeded = false;
						}
					}

					if (newLogoNeeded == true)
					{
						var schoolLogoObj = (from l in db.SchoolLogoes where l.LogoID == logoID select new { logo = l.Logo }).First();
						byte[] imgByteArray = schoolLogoObj.logo;
						imageForSchoolLogo.Source = ConvertByteArrayToBitmapImage(imgByteArray);
                    }

					file.Close();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Unexpected ERROR: Unable to load cached file - Unexpected behavior may occur");
					UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_UNABLE_TO_READWRITE, ex.Message);
				}
			}


		}

		//private void ChangeConnectionString()
		//{
		//    int schoolId = -1;
		//    foreach (UnstuckMESchool school in schools)
		//    {
		//        if (school.SchoolName == comboBoxSchools.SelectedValue.ToString())
		//        {
		//            schoolId = school.SchoolID;
		//            break;
		//        }
		//    }

		//    if (comboBoxSchools.SelectedValue.ToString() != m_orginalSchoolName)
		//    {
		//        var connectionString = ConfigurationManager.ConnectionStrings["UnstuckMEServiceEndPoint"].ConnectionString;
		//        string schoolIp = null;
		//        using (UnstuckME_SchoolsEntities db = new UnstuckME_SchoolsEntities())
		//        {
		//            schoolIp = (from server in db.Servers where server.SchoolID == schoolId select new {server.ServerIPAddress}).First().ToString();
		//        }


		//    }
		//}

		private static BitmapImage ConvertByteArrayToBitmapImage(byte[] bytes)
		{
			MemoryStream stream = new MemoryStream(bytes);
			stream.Seek(0, SeekOrigin.Begin);
			BitmapImage image = new BitmapImage();

			image.BeginInit();
			image.StreamSource = stream;
			image.EndInit();

			return image;
		}

		private void buttonResendVerificationCode_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				verification_code = UnstuckME.Server.SendEmail(EmailType.CreateAccount, textBoxCreateEmailAddress.Text, textBoxCreateFirstName.Text);
				UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK, "Please check your email for the verification code to verify your account", "Verification Code Sent", UnstuckMEBoxImage.Information);
				messagebox.ShowDialog();
			}
			catch (Exception ex)
			{
				AccountVerificationCanvas_MouseDown(sender, e as MouseEventArgs);
				UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message);
				UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK, "An error occured trying to connect to the server. If this problem persists, please contact an UnstuckME server administrator to resolve this issue. Thank you.", "Email Verification Code Failed to Send", UnstuckMEBoxImage.Warning);
				messagebox.ShowDialog();
			}
		}

		private void AccountVerificationCanvas_MouseDown(object sender, MouseEventArgs e)
		{
			verification_code = string.Empty;
			AccountVerificationGrid.IsEnabled = false;
			AccountVerificationGrid.Visibility = Visibility.Collapsed;
		}

		private void textboxVerificationCode_GotFocus(object sender, RoutedEventArgs e)
		{
			if (textboxVerificationCode.Text == "xxxxxxxx")
			{
				textboxVerificationCode.Text = string.Empty;
				textboxVerificationCode.Foreground = System.Windows.Media.Brushes.Black;
				textboxVerificationCode.FontStyle = FontStyles.Normal;
			}
		}

		private void textboxVerificationCode_LostFocus(object sender, RoutedEventArgs e)
		{
			if (textboxVerificationCode.Text == string.Empty)
			{
				textboxVerificationCode.Text = "xxxxxxxx";
				textboxVerificationCode.Foreground = System.Windows.Media.Brushes.Gray;
				textboxVerificationCode.FontStyle = FontStyles.Italic;
			}
		}

		private void passwordBoxCreate_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (passwordBoxCreate.Password == string.Empty)
				passwordBoxCreate.Background = System.Windows.Media.Brushes.White;
			else if (passwordBoxCreate.Password.Length < 3)
				passwordBoxCreate.Background = System.Windows.Media.Brushes.Red;
			else
				passwordBoxCreate.Background = System.Windows.Media.Brushes.Green;

			passwordBoxCreateConfirm_PasswordChanged(sender, e);
		}

		private void passwordBoxCreateConfirm_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (passwordBoxCreate.Password == string.Empty && passwordBoxCreateConfirm.Password == string.Empty)
				passwordBoxCreateConfirm.Background = System.Windows.Media.Brushes.White;
			else if (passwordBoxCreateConfirm.Password == passwordBoxCreate.Password && passwordBoxCreate.Background == System.Windows.Media.Brushes.Green)
				passwordBoxCreateConfirm.Background = System.Windows.Media.Brushes.Green;
			else
				passwordBoxCreateConfirm.Background = System.Windows.Media.Brushes.Red;
		}

		private void textBoxUserName_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				System.Windows.Media.Color brush = (System.Windows.Media.Color)(System.Windows.Media.ColorConverter.ConvertFromString("#FFCFCF56"));

				if ((textBoxUserName.Background as SolidColorBrush).Color == brush)
					textBoxUserName.Background = System.Windows.Media.Brushes.White;
				if (!content_rendered && (passwordBox.Background as SolidColorBrush).Color == brush)
					passwordBox.Background = System.Windows.Media.Brushes.White;
			}
			catch (Exception)
			{ }
		}

		private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			try
			{
				System.Windows.Media.Color brush = (System.Windows.Media.Color)(System.Windows.Media.ColorConverter.ConvertFromString("#FFCFCF56"));

				if ((passwordBox.Background as SolidColorBrush).Color == brush)
					passwordBox.Background = System.Windows.Media.Brushes.White;
				if (!content_rendered && (textBoxUserName.Background as SolidColorBrush).Color == brush)
					textBoxUserName.Background = System.Windows.Media.Brushes.White;
			}
			catch (Exception)
			{ }
		}

		private void buttonResetPassword_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (textBoxUserName.Text != "Example@oit.edu")
				{
					UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.YesNo, string.Format("Is {0} the email address of the account you wish to reset your password for?", textBoxUserName.Text), "Please Verify the Account to Reset the Password", UnstuckMEBoxImage.Warning);
					messagebox.ShowDialog();

					if (messagebox.DialogResult.Value)  //user pressed yes
					{
						try
						{
							UserInfo user = UnstuckME.Server.GetUserInfo(null, textBoxUserName.Text);

							string new_password = UnstuckME.Server.SendEmail(EmailType.ResetPassword, textBoxUserName.Text, user.FirstName);
							messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK, "Please check your email for your new password", "New Password Sent", UnstuckMEBoxImage.Information);
							messagebox.ShowDialog();

							UnstuckME.Server.ChangePassword(user, new_password);
						}
						catch (Exception ex)
						{
							messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK, string.Format("The email address {0} is not associated with an account. Please enter a valid email address.", textBoxUserName.Text), "Email Address Is Not Associated With An Account", UnstuckMEBoxImage.Error);
							messagebox.ShowDialog();
							UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message);

							try
							{
								UnstuckME.ChannelFactory.Abort();
								UnstuckME.ChannelFactory = new DuplexChannelFactory<IUnstuckMEService>(new ClientCallback(), "UnstuckMEServiceEndPoint");
								UnstuckME.Server = UnstuckME.ChannelFactory.CreateChannel();
							}
							catch (Exception exp2)
							{
								MessageBox.Show("There is a problem connecting to the server. Please Contact Your Server Administrator. UnstuckME will now close.", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
								UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp2.Message);
								this.Close();
							}
						}
					}
				}
				else
				{
					UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK, string.Format("The email address {0} is not associated with an account. Please enter a valid email address.", textBoxUserName.Text), "Email Address Is Not Associated With An Account", UnstuckMEBoxImage.Error);
					messagebox.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message);
				UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK, "An error occured trying to connect to the server. If this problem persists, please contact an UnstuckME server administrator to resolve this issue. Thank you.", "New Password Email Failed to Send", UnstuckMEBoxImage.Warning);
				messagebox.ShowDialog();
			}
		}

		private void buttonResetPassword_MouseEnter(object sender, MouseEventArgs e)
		{
			buttonResetPassword.Foreground = System.Windows.Media.Brushes.Black;
		}

		private void buttonResetPassword_MouseLeave(object sender, MouseEventArgs e)
		{
			buttonResetPassword.Foreground = System.Windows.Media.Brushes.White;
		}
	}
}