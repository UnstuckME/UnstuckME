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

        public static void ConnectToServer()
        {
            try
            {
                ChannelFactory = new DuplexChannelFactory<IUnstuckMEService>(new ClientCallback(), "UnstuckMEServiceEndPoint");
                Server = ChannelFactory.CreateChannel();
                ChannelFactory.Faulted += OnConnectionToServerFaulted;
                ChannelFactory.Closed += OnConnectionToServerClosed;
                _retryCount = 0;
            }
            catch(Exception)
            {
                if (_retryCount == 5)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        System.Windows.MessageBox.Show("Failed To Reconnect");
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

        public static void SetUPW(string P)
        {
            _UPW = P;
        }

        static void OnConnectionToServerFaulted(object sender, EventArgs ea)
        {
            ChannelFactory.Abort();
        }

        static void OnConnectionToServerClosed(object sender, EventArgs ea)
        {
            bool ReLoginAttempt = false;
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show("ERROR CONNECTION FAULTED");
            });
            ChannelFactory.Faulted -= OnConnectionToServerFaulted;
            ChannelFactory.Closed -= OnConnectionToServerClosed;
            ConnectToServer();
            int test = 0;
            while (!ReLoginAttempt)
            {
                ReLoginAttempt = ReLogin();
                if(test == 10)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        System.Windows.MessageBox.Show("Failed To Relogin");
                        //App.Current.Shutdown();
                    });
                }
            }
        }

        public static void ConnectToStreamService()
        {
            StreamChannelFactory = new ChannelFactory<IUnstuckMEFileStream>("UnstuckMEStreamingEndPoint");
            FileStream = StreamChannelFactory.CreateChannel();
        }

        static bool ReLogin()
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
