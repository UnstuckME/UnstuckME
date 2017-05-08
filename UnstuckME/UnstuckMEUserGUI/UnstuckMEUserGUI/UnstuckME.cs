using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Media;
using UnstuckME_Classes;
using UnstuckMEInterfaces;

namespace UnstuckMEUserGUI
{
    public static class UnstuckME
    {
        public static IUnstuckMEService Server;
        public static IUnstuckMEFileStream FileStream;
        public static UserInfo User;
        public static ImageSource UserProfilePicture;
        public static DuplexChannelFactory<IUnstuckMEService> ChannelFactory;
        public static ChannelFactory<IUnstuckMEFileStream> StreamChannelFactory;
        public static UnstuckMEWindow MainWindow;
        public static List<UnstuckMEChatUser> FriendsList;
        public static Brush Blue;
        public static Brush Red;
        public static UnstuckMEPages Pages;
        public static ImageSourceConverter ImageConverter;
        public static List<UnstuckMEChat> ChatSessions;
        public static UnstuckMEChat CurrentChatSession;
        public static UnstuckMEDirectory ProgramDir;
        private static int _retryCount = 0;
        private static string _UPW = string.Empty;

        internal static void ConnectToServer()
        {
            try
            {
                ChannelFactory = new DuplexChannelFactory<IUnstuckMEService>(new ClientCallback(), "UnstuckMEServiceEndPoint");
                Server = ChannelFactory.CreateChannel();
                ChannelFactory.Faulted += OnConnectionToServerFaulted;
                ChannelFactory.Closed += OnConnectionToServerClosed;
                _retryCount = 0;
            }
            catch (Exception)
            {
                if (_retryCount == 5)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK, "Failed to connect to server", "Server Connection Error", UnstuckMEBoxImage.Error);
                        messagebox.ShowDialog();
                        //App.Current.Shutdown();
                    });
                }
                else
                {
                    _retryCount++;
                    ConnectToServer();
                }
            }
        }

        internal static void SetUPW(string P)
        {
            _UPW = P;
        }

        internal static void OnConnectionToServerFaulted(object sender, EventArgs ea)
        {
            ChannelFactory.Abort();
        }

        internal static void OnConnectionToServerClosed(object sender, EventArgs ea)
        {
            bool reLoginAttempt = false;
            Application.Current.Dispatcher.Invoke(() =>
            {
                UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK, "Connection Faulted", "Server Connection Error", UnstuckMEBoxImage.Error);
                messagebox.ShowDialog();
            });

            ChannelFactory.Faulted -= OnConnectionToServerFaulted;
            ChannelFactory.Closed -= OnConnectionToServerClosed;
            ConnectToServer();

            int test = 0;
            while (!reLoginAttempt)
            {
                reLoginAttempt = ReLogin();
                if (test == 10)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK, "Failed to relogin", "Server Connection Error", UnstuckMEBoxImage.Error);
                        messagebox.ShowDialog();
                        //App.Current.Shutdown();
                    });
                }
            }
        }

        internal static void ConnectToStreamService()
        {
            StreamChannelFactory = new ChannelFactory<IUnstuckMEFileStream>("UnstuckMEStreamingEndPoint");
            FileStream = StreamChannelFactory.CreateChannel();
        }

        internal static bool ReLogin()
        {
            bool success = false;

            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    if (Server.UserLoginAttempt(User.EmailAddress, _UPW) == null)
                        throw new Exception();
                    
                    success = true;
                }
                catch (Exception)
                {
                    success = false;
                }
            });

            return success;
        }
    }
}
