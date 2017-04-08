using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
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
        public static ChannelFactory<IUnstuckMEFileStream> Stream_ChannelFactory;
        public static UnstuckMEWindow MainWindow;
        public static List<UnstuckMEChatUser> FriendsList;
        public static Brush Blue;
        public static Brush Red;
        public static UnstuckMEPages Pages;
        public static ImageSourceConverter ImageConverter;
        public static List<UnstuckMEChat> ChatSessions;
        public static UnstuckMEChat CurrentChatSession;

    }
}
