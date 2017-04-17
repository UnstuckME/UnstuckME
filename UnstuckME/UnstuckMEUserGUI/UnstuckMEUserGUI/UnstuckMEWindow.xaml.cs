using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using UnstuckME_Classes;
using UnstuckMeLoggers;

namespace UnstuckMEUserGUI
{
	/// <summary>
	/// Interaction logic for UnstuckMEWindow.xaml
	/// </summary>
	public partial class UnstuckMEWindow : Window
	{
		private static Privileges userPrivileges;

        private struct DirecortyList
        {
            public string dirName;
            public bool found;
        }


		public UnstuckMEWindow()
		{
			InitializeComponent();
			UnstuckME.MainWindow = this;

			userPrivileges = (Privileges)UnstuckME.User.Privileges;
			CheckAdminPrivledges(userPrivileges); //Disables/Enables Admin/Moderator Tab Depending on Privilege

			UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_LOGIN, UnstuckME.User.EmailAddress);
		}

		async private void Window_ContentRendered(object sender, EventArgs e)
		{
			UnstuckME.Red = StickerButton.Background;
			await Task.Factory.StartNew(() => InitializeStaticMembers());
			UnstuckME.Pages.StickerPage = new StickerPage();
			UnstuckME.Pages.SettingsPage = new SettingsPage();
			UnstuckME.Pages.ChatPage = new ChatPage();
			UnstuckME.Pages.UserProfilePage = new UserProfilePage();
			UnstuckME.Pages.ModeratorPage = new ModeratorPage();
			UnstuckME.Pages.AdminPage = new AdminPage();

			SwitchToStickerTab();
			try
			{
				await Task.Factory.StartNew(() => LoadStickerPageAsync());
				await Task.Factory.StartNew(() => LoadSideBarsAsync());
				await Task.Factory.StartNew(() => LoadChatPageAsync());
				await Task.Factory.StartNew(() => LoadUserProfilePageAsync());
				await Task.Factory.StartNew(() => LoadSettingsPageAsync());
				await Task.Factory.StartNew(() => LoadAdminPageAsync());
			}
			catch (InvalidOperationException ex)
			{
				MessageBox.Show("Source: " + ex.Source + "\nMessage: " + ex.Message);
			}

			LoadingScreen.Visibility = Visibility.Collapsed;
			await Task.Factory.StartNew(() => CheckForReviews());
		}

		#region Asynchronous Loading Section

		private void InitializeStaticMembers()
		{
			UnstuckME.ImageConverter = new ImageSourceConverter();
			UnstuckME.Pages = new UnstuckMEPages();

			using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
			{
				UnstuckME.FileStream.GetProfilePicture(UnstuckME.User.UserID).CopyTo(ms);
				UnstuckME.User.UserProfilePictureBytes = ms.ToArray();
				UnstuckME.UserProfilePicture = UnstuckME.ImageConverter.ConvertFrom(UnstuckME.User.UserProfilePictureBytes) as ImageSource;  //convert image so it can be displayed
			}
		}

		private void LoadStickerPageAsync()
		{
			try
			{
				this.Dispatcher.Invoke(() =>
				{
					UnstuckME.Pages.StickerPage.AvailableStickers = UnstuckME.Server.InitialAvailableStickerPull(UnstuckME.User.UserID);

					foreach (UnstuckMEAvailableSticker sticker in UnstuckME.Pages.StickerPage.AvailableStickers)
					{
						UnstuckME.Pages.StickerPage.StackPanelAvailableStickers.Children.Add(new AvailableSticker(sticker));
					}

					UnstuckME.Pages.StickerPage.MyStickersList = UnstuckME.Server.GetUserSubmittedStickers(UnstuckME.User.UserID, null, 0, null);
					foreach (UnstuckMESticker sticker in UnstuckME.Pages.StickerPage.MyStickersList)
					{
						UnstuckME.Pages.StickerPage.StackPanelMyStickers.Children.Add(new MySticker(sticker));
					}

					UnstuckME.Pages.StickerPage.OpenStickers = UnstuckME.Server.GetUserTutoredStickers(UnstuckME.User.UserID, null, 0, null);
					foreach (UnstuckMESticker sticker in UnstuckME.Pages.StickerPage.OpenStickers)
					{
						UnstuckME.Pages.StickerPage.StackPanelOpenStickers.Children.Add(new OpenSticker(sticker));
					}
				});
			}
			catch(Exception ex)
			{
				UnstuckMEMessageBox error = new UnstuckMEMessageBox(UnstuckMEBox.OK, "Error Loading Stickers, Please Contact Your Server Administrator if Problem Persists.\n" + "Error Message: " + ex.Message, "Sticker Loading Error", UnstuckMEBoxImage.Error);
				error.ShowDialog();
			}
		}

		private void LoadChatPageAsync()
		{
			try
			{
				this.Dispatcher.Invoke(() =>
				{
					// ===========================Ryan's optimized code=========================================
					UnstuckME.ChatSessions = UnstuckME.Server.GetChatIDs(UnstuckME.User.UserID);
                    List<string> folders =  Directory.GetDirectories(UnstuckME.ProgramDir.ChatDir).ToList();

                    foreach (UnstuckMEChat chat in UnstuckME.ChatSessions)
                    {
                        string path = UnstuckME.ProgramDir.ChatDir + @"\" +chat.ChatID.ToString();
                        if (folders.Contains<string>(path) == true)
                            folders.Remove(path);
                        else
                            UnstuckME.ProgramDir.MakeChatDir(chat.ChatID.ToString());
                    }

                    foreach (string fold in folders)
                    {
                        Directory.Delete(fold, true); // Recursively delete contents of a directory
                    }

					// ~~~~~~~~~~~~~~~~~~~~AJ's Code that is currently getting re-factored~~~~~~~~~~~~~~~~~~~~~~~~~
					UnstuckME.ChatSessions = UnstuckME.Server.GetUserChats(UnstuckME.User.UserID);
					foreach (UnstuckMEChat chat in UnstuckME.ChatSessions)
					{
						UnstuckME.Pages.ChatPage.AddConversation(chat);
					}
					foreach (UnstuckMEChatUser user in UnstuckME.FriendsList)
					{
						UnstuckME.Pages.ChatPage.StackPanelAddContacts.Children.Add(new ContactCreateConversation(user));
					}
				});
			}
			catch(Exception ex)
			{
				UnstuckMEMessageBox error = new UnstuckMEMessageBox(UnstuckMEBox.OK, "Error Loading Chat Page, Please Contact Your Server Administrator if Problem Persists.\n" + "Error Message: " + ex.Message, "Chat Loading Error", UnstuckMEBoxImage.Error);
				error.ShowDialog();
			}
		}

		private void LoadUserProfilePageAsync()
		{
			try
			{
				this.Dispatcher.Invoke(() =>
				{
					UnstuckME.Pages.UserProfilePage.ProfilePicture.Source = UnstuckME.UserProfilePicture;  //convert image so it can be displayed
					UnstuckME.Pages.UserProfilePage.ImageEditProfilePicture.Source = UnstuckME.UserProfilePicture;
					UnstuckME.Pages.UserProfilePage.FirstName.Text = UnstuckME.User.FirstName;
					UnstuckME.Pages.UserProfilePage.LastName.Text = UnstuckME.User.LastName;
					UnstuckME.Pages.UserProfilePage.EmailAddress.Text = UnstuckME.User.EmailAddress;
					UnstuckME.Pages.UserProfilePage.SetStudentRating(UnstuckME.User.AverageStudentRank);
					UnstuckME.Pages.UserProfilePage.SetTutorRating(UnstuckME.User.AverageTutorRank);
					UnstuckME.Pages.UserProfilePage.RepopulateClasses();
				});
			}
			catch (Exception ex)
			{
				UnstuckMEMessageBox error = new UnstuckMEMessageBox(UnstuckMEBox.OK, "Error Loading User Profile Page, Please Contact Your Server Administrator if Problem Persists.\n" + "Error Message: " + ex.Message, "Profile Loading Error", UnstuckMEBoxImage.Error);
				error.ShowDialog();
			}

		}

		private void LoadSettingsPageAsync()
		{
			this.Dispatcher.Invoke(() =>
			{
				// your asynchronous code here.
			});
		}

		private void LoadAdminPageAsync()
		{
			this.Dispatcher.Invoke(() =>
			{
				// your asynchronous code here.
			});
		}

		private void LoadSideBarsAsync()
		{
			try
			{
				this.Dispatcher.Invoke(() =>
				{
					UnstuckME.FriendsList = UnstuckME.Server.GetFriends(UnstuckME.User.UserID);
					foreach (UnstuckMEChatUser friend in UnstuckME.FriendsList)
					{
						OnlineUsersStack.Children.Add(new OnlineUser(friend));
					}
				});
			}
			catch (Exception ex)
			{
				UnstuckMEMessageBox error = new UnstuckMEMessageBox(UnstuckMEBox.OK, "Error Right Side Panel, Please Contact Your Server Administrator if Problem Persists.\n" + "Error Message: " + ex.Message, "Contacts/Notifications Loading Error", UnstuckMEBoxImage.Error);
				error.ShowDialog();
			}
		}

		private void CheckForReviews()
		{
			System.Threading.Thread.Sleep(2000);    //wait 2 seconds so everything can be placed

			try
			{
				this.Dispatcher.Invoke(() =>
				{
					KeyValuePair<int, bool> needsreview = UnstuckME.Server.CheckForReviews(UnstuckME.User.UserID);

					if (needsreview.Key != 0)
					{
						Window win;

						if (needsreview.Value)
							win = new SubWindows.AddTutorReviewWindow(needsreview.Key);
						else
							win = new SubWindows.AddStudentReviewWindow(needsreview.Key);

						win.Show();
						win.Focus();
					}
				});
			}
			catch (Exception ex)
			{
				UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message);
				UnstuckMEMessageBox error = new UnstuckMEMessageBox(UnstuckMEBox.OK, "You need to submit a review for the stickers you have open.", "Review Needed", UnstuckMEBoxImage.Information);
				error.ShowDialog();
			}
		}

		#endregion

		public void AddUserToContactsStack(UnstuckMEChatUser inChatUser)
		{
			OnlineUsersStack.Children.Add(new OnlineUser(inChatUser));
		}

		public void CheckAdminPrivledges(Privileges inPrivleges)
		{
			switch (inPrivleges)
			{
				case Privileges.Admin: //Admin
					{
						AdminButton.Visibility = Visibility.Visible;
						AdminButton.IsEnabled = true;
						break;
					}
				case Privileges.Moderator: //Moderator
					{
						AdminButton.Visibility = Visibility.Visible;
						AdminButton.IsEnabled = true;
						break;
					}
				case Privileges.User: //User
					{
						AdminButton.Visibility = Visibility.Hidden;
						AdminButton.IsEnabled = false;
						break;
					}
				default:
					break;
			}
		}

		public void SwitchToChatTab()
		{
			MainFrame.NavigationService.RemoveBackEntry();
			MainFrame.Navigate(UnstuckME.Pages.ChatPage);
			ChatButton.Background = UnstuckME.Red;
			StickerButton.Background = UnstuckME.Blue;
			SettingButton.Background = UnstuckME.Blue;
			UserProfileButton.Background = UnstuckME.Blue;
			AdminButton.Background = UnstuckME.Blue;
			DisableStickerSubmit();
		}
		public void SwitchToStickerTab()
		{
			MainFrame.NavigationService.RemoveBackEntry();
			MainFrame.Navigate(UnstuckME.Pages.StickerPage);
			ChatButton.Background = UnstuckME.Blue;
			StickerButton.Background = UnstuckME.Red;
			SettingButton.Background = UnstuckME.Blue;
			UserProfileButton.Background = UnstuckME.Blue;
			AdminButton.Background = UnstuckME.Blue;
			EnableStickerSubmit();
		}
		public void SwitchToUserProfileTab()
		{
			MainFrame.NavigationService.RemoveBackEntry();
			MainFrame.Navigate(UnstuckME.Pages.UserProfilePage);
			ChatButton.Background = UnstuckME.Blue;
			StickerButton.Background = UnstuckME.Blue;
			SettingButton.Background = UnstuckME.Blue;
			UserProfileButton.Background = UnstuckME.Red;
			AdminButton.Background = UnstuckME.Blue;
			DisableStickerSubmit();
		}
		public void SwitchToSettingsTab()
		{
			MainFrame.NavigationService.RemoveBackEntry();
			MainFrame.Navigate(UnstuckME.Pages.SettingsPage);
			ChatButton.Background = UnstuckME.Blue;
			StickerButton.Background = UnstuckME.Blue;
			SettingButton.Background = UnstuckME.Red;
			UserProfileButton.Background = UnstuckME.Blue;
			AdminButton.Background = UnstuckME.Blue;
			DisableStickerSubmit();
		}
		public void SwitchToAdminTab(Privileges inPriviledges)
		{
			switch (inPriviledges)
			{
				case Privileges.Admin: //Admin
					{
						MainFrame.Navigate(UnstuckME.Pages.AdminPage);
						break;
					}
				case Privileges.Moderator: //Moderator
					{
						MainFrame.Navigate(UnstuckME.Pages.ModeratorPage);
						break;
					}
				default: //In case someone figures out a way to make admin button show
					{
						MessageBox.Show("You do not have access to this tab.", "Unauthorized Access", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						return;
					}
			}

			ChatButton.Background = UnstuckME.Blue;
			StickerButton.Background = UnstuckME.Blue;
			SettingButton.Background = UnstuckME.Blue;
			UserProfileButton.Background = UnstuckME.Blue;
			AdminButton.Background = UnstuckME.Red;
			DisableStickerSubmit();
		}

		private void DisableStickerSubmit()
		{
			CreateStickerButton.Visibility = Visibility.Hidden;
			CreateStickerButton.IsEnabled = false;
		}
		private void EnableStickerSubmit()
		{
			CreateStickerButton.Visibility = Visibility.Visible;
			CreateStickerButton.IsEnabled = true;
		}

		public void AddStickerToMyStickers(UnstuckMESticker inSticker)
		{
			UnstuckME.Pages.StickerPage.StackPanelMyStickers.Children.Add(new MySticker(inSticker));
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_LOGOUT, UnstuckME.User.EmailAddress);
			try
			{
				UnstuckME.Server.Logout();
			}
			catch (Exception)
			{ /*This is empty because we don't want the program to break but we also don't want to catch this exception*/ }
		}

		public void RecieveChatMessage(UnstuckMEMessage message)
		{
			UnstuckME.Pages.ChatPage.AddMessage(message);
			if ((UnstuckME.CurrentChatSession.ChatID != message.ChatID) || (MainFrame.Content != UnstuckME.Pages.ChatPage))
			{
				NewMessageNotification temp = null; 
				foreach (var notification in NotificationStack.Children.OfType<NewMessageNotification>())
				{
					if(notification.Message.ChatID == message.ChatID)
					{
						temp = notification;
					}
				}
				if (temp == null)
				{
					NotificationStack.Children.Insert(0, new NewMessageNotification(message));
				}
				else
				{
					NotificationStack.Children.Insert(0, new NewMessageNotification(message, temp.NotificationCount + 1));
					NotificationStack.Children.Remove(temp);
				}
			}
		}

		/// <summary>
		/// Currently does nothing with the file
		/// </summary>
		/// <param name="message"></param>
		/// <param name="file"></param>
		//public void RecieveChatFile(UnstuckMEMessage message, UnstuckMEFile file)
		//{
		//    if ((UnstuckME.CurrentChatSession.ChatID != message.ChatID) || (MainFrame.Content != UnstuckME.Pages.ChatPage))
		//    {
		//        UnstuckME.Pages.ChatPage.AddMessage(message, file);
		//        NotificationStack.Children.Insert(0, new NewMessageNotification(message));
		//    }
		//}

		public void RecieveAddedClass(UserClass inClass)
		{
			this.Dispatcher.Invoke(() =>
			{
				try
				{
					ClassDisplay temp = new ClassDisplay(inClass);
					UnstuckME.Pages.UserProfilePage.BottomLeftStack.Children.Add(temp);

				}
				catch (Exception)
				{

				}
			});
		}

		public void RecieveNewAvailableSticker(UnstuckMEAvailableSticker sticker)
		{
			this.Dispatcher.Invoke(() =>
			{
				try
				{
					UnstuckME.Pages.StickerPage.AvailableStickers.Add(sticker);
					UnstuckME.Pages.StickerPage.StackPanelAvailableStickers.Children.Add(new AvailableSticker(sticker));
					//NotificationStack.Children.Add(new AvailableStickerNotification(sticker));  This looks gross currently so I'm not going to use it.
				}
				catch (Exception)
				{
					MessageBox.Show("Sticker Update Failed");
				}
			});
		}

		public void RemoveStickerFromAvailableStickers(int stickerID)
		{
			this.Dispatcher.Invoke(() =>
			{
				try
				{
					AvailableSticker temp = null;
					foreach (var control in UnstuckME.Pages.StickerPage.StackPanelAvailableStickers.Children.OfType<AvailableSticker>())
					{
						if (control.Sticker.StickerID == stickerID)
						{
							temp = control;
						}
					}
					if (temp != null)
					{
						UnstuckME.Pages.StickerPage.StackPanelAvailableStickers.Children.Remove(temp);
					}
				}
				catch (Exception)
				{
					MessageBox.Show("Remove Failed");
				}
			});
		}

		public void StickerAcceptedStartConversation(UnstuckMEAvailableSticker sticker, int tutorID)
		{
			this.Dispatcher.Invoke(() =>
			{
				int PreExistingChatID = -1;
				//Search For Pre-Existing Conversation
				foreach (UnstuckMEChat chat in UnstuckME.ChatSessions)
				{
					bool studentFound = false;
					bool tutorFound = false;
					if (chat.Users.Count == 2)
					{
						foreach (UnstuckMEChatUser user in chat.Users)
						{
							if (user.UserID == sticker.StudentID)
							{ studentFound = true; }
							if (user.UserID == tutorID)
							{ tutorFound = true; }
						}
						if (studentFound && tutorFound)
						{
							PreExistingChatID = chat.ChatID;
						}
					}
				}

				if (PreExistingChatID == -1)
				{
					//Chat Not Found. Creating a new one.
					PreExistingChatID = UnstuckME.Server.CreateChat(UnstuckME.User.UserID);
					UnstuckME.Server.InsertUserIntoChat(sticker.StudentID, PreExistingChatID);
				}

				UnstuckMEMessage temp = new UnstuckMEMessage()
				{
					ChatID = PreExistingChatID,
					FilePath = string.Empty,
					Message = UnstuckME.User.FirstName + " " + UnstuckME.User.LastName + " has accepted a sticker you submitted!",
					MessageID = 0,
					SenderID = UnstuckME.User.UserID,
					Username = UnstuckME.User.FirstName,
					UsersInConvo = new List<int>()
				};

				temp.UsersInConvo.Add(UnstuckME.User.UserID);
				temp.UsersInConvo.Add(sticker.StudentID);
				temp.MessageID = UnstuckME.Server.SendMessage(temp);
				temp.Message = "Your have accepted a Sticker!";

				UnstuckMESticker tempSticker = new UnstuckMESticker()
				{
					ChatID = PreExistingChatID,
					ClassID = sticker.ClassID,
					ProblemDescription = sticker.ProblemDescription,
					StickerID = sticker.StickerID,
					StudentID = sticker.StudentID,
					Timeout = sticker.Timeout,
					TutorID = UnstuckME.User.UserID
				};

				UnstuckME.Pages.StickerPage.StackPanelOpenStickers.Children.Add(new OpenSticker(tempSticker));
				UnstuckME.Pages.ChatPage.AddMessage(temp);
				SwitchToChatTab();
				UnstuckME.Pages.ChatPage.ButtonAddUserDone_Click(null, null);

				foreach (Conversation convo in UnstuckME.Pages.ChatPage.StackPanelConversations.Children.OfType<Conversation>())
				{
					if (convo.Chat.ChatID == PreExistingChatID)
					{
						convo.ConversationUserControl_MouseLeftButtonDown(null, null);
					}
				}
			});
		}

		public void UpdateChatMessage(UnstuckMEMessage message)
		{
			this.Dispatcher.Invoke(() => 
			{
				UnstuckME.Pages.ChatPage.EditMessage(message);
			});
		}

		public void DeleteChatMessage(UnstuckMEMessage message)
		{
			this.Dispatcher.Invoke(() =>
			{
				UnstuckME.Pages.ChatPage.RemoveMessage(message);
			});
		}

		#region ButtonLogic
		private void ButtonLogout_MouseEnter(object sender, MouseEventArgs e)
		{
			ButtonLogout.Background = Brushes.IndianRed;
		}

		private void ButtonLogout_MouseLeave(object sender, MouseEventArgs e)
		{
			ButtonLogout.Background = UnstuckME.Red;
		}

		private void ButtonLogout_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			UnstuckME.Server.Logout();

			var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
			config.AppSettings.Settings["RememberMe"].Value = "false";
			config.Save();

			Application.Current.MainWindow = new LoginWindow();
			Application.Current.MainWindow.Show();
			this.Close();
		}

		private void ButtonAddContact_MouseEnter(object sender, MouseEventArgs e)
		{
			ButtonAddContact.Background = Brushes.LightSteelBlue;
		}

		private void ButtonAddContact_MouseLeave(object sender, MouseEventArgs e)
		{
			ButtonAddContact.Background = null;
		}

		private void ButtonAddContact_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			AddFriendWindow friendWindow = new AddFriendWindow();
			friendWindow.Show();
		}

		private void CreateStickerButton_MouseEnter(object sender, MouseEventArgs e)
		{
			CreateStickerButton.Background = Brushes.IndianRed;
		}

		private void CreateStickerButton_MouseLeave(object sender, MouseEventArgs e)
		{
			CreateStickerButton.Background = UnstuckME.Red;
		}

		private void CreateStickerButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			StickerCreationWindow window = new StickerCreationWindow();
			window.ShowDialog();
		}

		private void AdminButton_MouseEnter(object sender, MouseEventArgs e)
		{
			if(AdminButton.Background == UnstuckME.Blue)
			{
				AdminButton.Background = Brushes.SteelBlue;
			}
			else
			{
				AdminButton.Background = Brushes.IndianRed;
			}
		}

		private void AdminButton_MouseLeave(object sender, MouseEventArgs e)
		{
			if (AdminButton.Background == Brushes.SteelBlue)
			{
				AdminButton.Background = UnstuckME.Blue;
			}
			else
			{
				AdminButton.Background = UnstuckME.Red;
			}
		}

		private void AdminButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			SwitchToAdminTab(userPrivileges);
		}

		private void SettingButton_MouseEnter(object sender, MouseEventArgs e)
		{
			if (SettingButton.Background == UnstuckME.Blue)
			{
				SettingButton.Background = Brushes.SteelBlue;
			}
			else
			{
				SettingButton.Background = Brushes.IndianRed;
			}
		}

		private void SettingButton_MouseLeave(object sender, MouseEventArgs e)
		{
			if (SettingButton.Background == Brushes.SteelBlue)
			{
				SettingButton.Background = UnstuckME.Blue;
			}
			else
			{
				SettingButton.Background = UnstuckME.Red;
			}
		}

		private void SettingButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			SwitchToSettingsTab();
		}

		private void UserProfileButton_MouseEnter(object sender, MouseEventArgs e)
		{
			if (UserProfileButton.Background == UnstuckME.Blue)
			{
				UserProfileButton.Background = Brushes.SteelBlue;
			}
			else
			{
				UserProfileButton.Background = Brushes.IndianRed;
			}
		}

		private void UserProfileButton_MouseLeave(object sender, MouseEventArgs e)
		{
			if (UserProfileButton.Background == Brushes.SteelBlue)
			{
				UserProfileButton.Background = UnstuckME.Blue;
			}
			else
			{
				UserProfileButton.Background = UnstuckME.Red;
			}
		}

		private void UserProfileButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			SwitchToUserProfileTab();
		}

		private void ChatButtonBorder_MouseEnter(object sender, MouseEventArgs e)
		{
			if (ChatButton.Background == UnstuckME.Blue)
			{
				ChatButton.Background = Brushes.SteelBlue;
			}
			else
			{
				ChatButton.Background = Brushes.IndianRed;
			}
		}

		private void ChatButtonBorder_MouseLeave(object sender, MouseEventArgs e)
		{
			if (ChatButton.Background == Brushes.SteelBlue)
			{
				ChatButton.Background = UnstuckME.Blue;
			}
			else
			{
				ChatButton.Background = UnstuckME.Red;
			}
		}

		private void ChatButtonBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			SwitchToChatTab();
		}

		private void StickerButton_MouseEnter(object sender, MouseEventArgs e)
		{
			if (StickerButton.Background == UnstuckME.Blue)
			{
				StickerButton.Background = Brushes.SteelBlue;
			}
			else
			{
				StickerButton.Background = Brushes.IndianRed;
			}
		}

		private void StickerButton_MouseLeave(object sender, MouseEventArgs e)
		{
			if (StickerButton.Background == Brushes.SteelBlue)
			{
				StickerButton.Background = UnstuckME.Blue;
			}
			else
			{
				StickerButton.Background = UnstuckME.Red;
			}
		}

		private void StickerButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			SwitchToStickerTab();
		}
		#endregion
	}

	public class UnstuckMEPages
	{
		public StickerPage StickerPage { get; set; }
		public SettingsPage SettingsPage { get; set; }
		public UserProfilePage UserProfilePage { get; set; }
		public ChatPage ChatPage { get; set; }
		public AdminPage AdminPage { get; set; }
		public ModeratorPage ModeratorPage { get; set; }
	}
}
