using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
using Microsoft.WindowsAPICodePack.Dialogs;
using UnstuckME_Classes;

namespace UnstuckMEServerGUI.ServerGuiSubWindow
{
	/// <summary>
	/// Interaction logic for EmailSettings.xaml
	/// </summary>
	public partial class EmailSettings : Window
	{
		public static AdminInfo Admin;

		public EmailSettings(ref AdminInfo currentAdmin)
		{
			InitializeComponent();
			Admin = currentAdmin;
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
			var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(dir.Parent.Parent.Parent.FullName + "/UnstuckMEServer/bin/Release/UnstuckMEServer.exe");
			var mailSettings = (System.Net.Configuration.SmtpSection)config.GetSection("system.net/mailSettings/smtp");

			if (mailSettings.DeliveryFormat == SmtpDeliveryFormat.SevenBit)
				comboboxDeliveryFormat.SelectedIndex = 0;
			else
				comboboxDeliveryFormat.SelectedIndex = 1;

			if (mailSettings.DeliveryMethod == SmtpDeliveryMethod.Network)
				comboboxDeliveryMethod.SelectedIndex = 0;
			else
			{
				if (mailSettings.DeliveryMethod == SmtpDeliveryMethod.PickupDirectoryFromIis)
					comboboxDeliveryMethod.SelectedIndex = 1;
				else
					comboboxDeliveryMethod.SelectedIndex = 2;

				checkboxSetSpecifiedPickupDirectory.IsChecked = true;
				checkboxSetSpecifiedPickupDirectory_Checked(sender, (RoutedEventArgs)e);
				textboxSpecifiedPickupDirectory.Text = mailSettings.SpecifiedPickupDirectory.PickupDirectoryLocation;
			}

			checkboxSSL.IsChecked = mailSettings.Network.EnableSsl;
			textboxUsername.Text = mailSettings.Network.UserName;
			textboxPassword.Password = mailSettings.Network.Password;
			textboxHost.Text = mailSettings.Network.Host;
			textboxPort.Text = mailSettings.Network.Port.ToString();
		}

		private void buttonPickupDirectoryBrowse_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
				{
					dialog.EnsurePathExists = true;
					dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
					dialog.IsFolderPicker = true;
					dialog.Title = "Select Pickup Directory";
					dialog.ShowHiddenItems = false;
					dialog.Multiselect = false;

					if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
						textboxSpecifiedPickupDirectory.Text = dialog.FileName;
				}
			}
			catch (Exception)
			{ }
		}

		private void buttonCancel_Click(object sender, RoutedEventArgs e)
		{
			App.Current.MainWindow = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault();
			this.Close();
		}

		private void checkboxSetSpecifiedPickupDirectory_Checked(object sender, RoutedEventArgs e)
		{
			labelSpecifiedPickupDirectory.Visibility = Visibility.Visible;
			textboxSpecifiedPickupDirectory.Visibility = Visibility.Visible;
			buttonPickupDirectoryBrowse.Visibility = Visibility.Visible;
		}

		private void checkboxSetSpecifiedPickupDirectory_Unchecked(object sender, RoutedEventArgs e)
		{
			labelSpecifiedPickupDirectory.Visibility = Visibility.Hidden;
			textboxSpecifiedPickupDirectory.Visibility = Visibility.Hidden;
			buttonPickupDirectoryBrowse.Visibility = Visibility.Hidden;
		}

		private void buttonSaveEmailSettings_Click(object sender, RoutedEventArgs e)
		{
			if (textboxSpecifiedPickupDirectory.Text != string.Empty)
			{
				DirectoryInfo dir = new DirectoryInfo(textboxSpecifiedPickupDirectory.Text);

				if (!dir.Exists)
				{
					if (MessageBox.Show("The Pickup Directory " + textboxSpecifiedPickupDirectory.Text + " does not exist. Do you wish to create this directory?", "Pickup Directory Doesn't Exist", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
						dir.Create();
				}
			}

			SetEmailSettings();
			buttonCancel_Click(sender, e);
		}

		private void SetEmailSettings()
		{
			DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
			var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(dir.Parent.Parent.Parent.FullName + "/UnstuckMEServer/bin/Release/UnstuckMEServer.exe");
			var mailSettings = (System.Net.Configuration.SmtpSection)config.GetSection("system.net/mailSettings/smtp");

			if ((SmtpDeliveryFormat)comboboxDeliveryFormat.SelectedIndex == SmtpDeliveryFormat.SevenBit)
				mailSettings.DeliveryFormat = SmtpDeliveryFormat.SevenBit;
			else
				mailSettings.DeliveryFormat = SmtpDeliveryFormat.International;

			if ((SmtpDeliveryMethod)comboboxDeliveryMethod.SelectedIndex == SmtpDeliveryMethod.Network)
				mailSettings.DeliveryMethod = SmtpDeliveryMethod.Network;
			else if ((SmtpDeliveryMethod)comboboxDeliveryMethod.SelectedIndex == SmtpDeliveryMethod.PickupDirectoryFromIis)
				mailSettings.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
			else
				mailSettings.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;

			mailSettings.Network.EnableSsl = checkboxSSL.IsChecked.Value;
			mailSettings.Network.UserName = textboxUsername.Text;
			mailSettings.Network.Password = textboxPassword.Password;
			mailSettings.Network.Host = textboxHost.Text;
			mailSettings.Network.Port = Convert.ToInt32(textboxPort.Text);
			mailSettings.SpecifiedPickupDirectory.PickupDirectoryLocation = textboxSpecifiedPickupDirectory.Text;
			config.Save();

			config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
			config.AppSettings.Settings["EmailSettingsSet"].Value = "true";
			config.Save();
		}
	}
}								