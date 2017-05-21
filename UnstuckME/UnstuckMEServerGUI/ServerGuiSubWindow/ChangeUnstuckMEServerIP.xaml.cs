using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.Windows;
using System.Xml;
using UnstuckMEInterfaces;

namespace UnstuckMEServerGUI.ServerGuiSubWindow
{
    /// <summary>
    /// Interaction logic for ChangeUnstuckMEServerIP.xaml
    /// </summary>
    public partial class ChangeUnstuckMEServerIP : Window
    {
        private readonly string m_schoolName = ConfigurationManager.AppSettings["SchoolName"];
        private int? m_serverID;
        private bool m_pastTest;
        private string m_appConfigDir = string.Empty;

        public ChangeUnstuckMEServerIP()
        {
            InitializeComponent();
            try
            {
                using (UnstuckME_SchoolsEntities schoolDB = new UnstuckME_SchoolsEntities())
                {
                    var serverInfo = (from Servers in schoolDB.Servers
                                      where Servers.School.SchoolName == m_schoolName
                                      select new { Servers.ServerID, Servers.ServerIPAddress, Servers.ServerName }).First();
                    if (serverInfo != null)
                    {
                        textBoxNewIP.Text = serverInfo.ServerIPAddress;
                        textBoxServerName.Text = serverInfo.ServerName;
                        m_serverID = serverInfo.ServerID;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("We seem to experiencing some difficulties connecting to the UnstuckME Server", "Unable To Connect to UnstuckME Server", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            buttonTest_Click(sender, e); //Run one final check on current entered setttings
            if (m_pastTest)  //If the user entered settings that were able to pass all tests
            {
                try
                {
                    using (UnstuckME_SchoolsEntities schoolDB = new UnstuckME_SchoolsEntities())
                    {
                        if (m_serverID != null) // If the Admin already has server settings for their school in the database
                        {
                            var schoolServer = (from Servers in schoolDB.Servers
                                                where Servers.ServerID == m_serverID.Value
                                                select Servers).First();
                            schoolServer.ServerName = textBoxServerName.Text;
                            schoolServer.ServerIPAddress = textBoxNewIP.Text;
                        }
                        else // If this the first time the Admin is configuring the server a new row needs to be inserted into the DB
                        {
                            int schoolID = (from Schools in schoolDB.Schools
                                            where Schools.SchoolName == m_schoolName
                                            select Schools.SchoolID).First();

                            Server tempServer = new Server
                            {
                                SchoolID = schoolID,
                                ServerName = textBoxServerName.Text,
                                ServerIPAddress = textBoxNewIP.Text
                            };
                            schoolDB.Servers.Add(tempServer);
                        }

                        schoolDB.SaveChanges();

                        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                        config.AppSettings.Settings["UnstuckMEServerIP"].Value = textBoxNewIP.Text;
                        config.Save(ConfigurationSaveMode.Modified);
                    }

                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to the UnstuckME Servers", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private bool CheckIPAddress()
        {
            string input = textBoxNewIP.Text;

            bool isIPAddress = false;

            IPAddress address;
            if (IPAddress.TryParse(input, out address))
            {
                switch (address.AddressFamily)
                {
                    case AddressFamily.InterNetwork:
                        isIPAddress = true;
                        break;
                    case AddressFamily.InterNetworkV6:
                        isIPAddress = true;
                        break;
                    default:
                        break;
                }
            }

            return isIPAddress;
        }

        private bool CheckLocalHost()
        {
            bool localHostConnection = false;

            string connectionString = textBoxNewIP.Text.ToLower();

            if (connectionString == "net.tcp://localhost" ||
                connectionString == @"\\" ||
                connectionString == @"\"  ||
                connectionString == @"//" ||
                connectionString == @"/"  )
            {
                textBoxNewIP.Text = "//localhost";
                localHostConnection = true;
            }
            else
            {
                connectionString = textBoxNewIP.Text.Replace(@"\", "").Replace(@"/", "");
                if (connectionString == "localhost"  ||
                    connectionString == "hostlocal"  ||
                    connectionString == "local_host" ||
                    connectionString == "local")
                {
                    textBoxNewIP.Text = "//localhost";
                    localHostConnection = true;
                }
            }
            
            return localHostConnection;
        }

        private bool UpdateAppConfigExe()
        {
            bool exeUpdated = false;

            if (CheckIPAddress() == false && CheckLocalHost() == false)
                MessageBox.Show("It appears you have not entered a valid IPV4/ IPV6/ or //LocalHost connection", "Invalid Connection Settings", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(m_appConfigDir);

                if (CheckIPAddress())
                {
                    XmlNodeList endpoints = doc.GetElementsByTagName("endpoint");
                    foreach (XmlNode item in endpoints)
                    {
                        if (item.Attributes != null)
                        {
                            var adressAttribute = item.Attributes["address"];
                            if (!ReferenceEquals(null, adressAttribute))
                                adressAttribute.Value = string.Format("net.tcp://{0}:9000/UnstuckMEService", textBoxNewIP.Text);

                            var bindingAttribute = item.Attributes["bindingConfiguration"];
                            if (!ReferenceEquals(null, adressAttribute))
                                bindingAttribute.Value = "TcpBindingConfiguration";
                        }
                    }

                    XmlNodeList bindingNames = doc.GetElementsByTagName("binding");
                    foreach (XmlNode item in bindingNames)
                    {
                        if (item.Attributes != null)
                        {
                            var bindingName = item.Attributes["name"];
                            if (!ReferenceEquals(null, bindingName))
                                bindingName.Value = "TcpBindingConfiguration";

                            if (ReferenceEquals(null, item.Attributes["sendTimeout"]))
                            {
                                XmlAttribute sendTimeoutAttr = doc.CreateAttribute("sendTimeout");
                                sendTimeoutAttr.Value = "00:00:05";
                                item.Attributes.SetNamedItem(sendTimeoutAttr);
                            }
                            if (ReferenceEquals(null, item.Attributes["portSharingEnabled"]))
                            {
                                XmlAttribute portSharingAttr = doc.CreateAttribute("portSharingEnabled");
                                portSharingAttr.Value = "true";

                                item.Attributes.SetNamedItem(portSharingAttr);
                            }
                        }
                    }
                }
                else if (CheckLocalHost())
                {
                    XmlNodeList endpoints = doc.GetElementsByTagName("endpoint");
                    foreach (XmlNode item in endpoints)
                    {
                        if (item.Attributes != null)
                        {
                            var adressAttribute = item.Attributes["address"];
                            if (!ReferenceEquals(null, adressAttribute))
                                adressAttribute.Value = "net.tcp://localhost:9000/UnstuckMEService";

                            var bindingAttribute = item.Attributes["bindingConfiguration"];
                            if (!ReferenceEquals(null, adressAttribute))
                                bindingAttribute.Value = "NormalTCPBinding";
                        }
                    }

                    XmlNodeList bindingNames = doc.GetElementsByTagName("binding");

                    foreach (XmlNode item in bindingNames)
                    {
                        if (item.Attributes != null)
                        {
                            var bindingName = item.Attributes["name"];
                            if (!ReferenceEquals(null, bindingName))
                                bindingName.Value = "NormalTCPBinding";
                            if (!ReferenceEquals(null, item.Attributes["sendTimeout"]))
                                item.Attributes.RemoveNamedItem("sendTimeout");
                            if (!ReferenceEquals(null, item.Attributes["portSharingEnabled"]))
                                item.Attributes.RemoveNamedItem("portSharingEnabled");
                        }
                    }
                }

                doc.Save(m_appConfigDir);
                exeUpdated = true;
            }

            return exeUpdated;
        }

        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo tempDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            tempDir = tempDir.Parent.Parent.Parent;
            m_appConfigDir = tempDir.FullName + "/UnstuckMEServer/app.config";

            if (UpdateAppConfigExe())
            {
                m_appConfigDir = tempDir.FullName + "/UnstuckMEServerGUI/bin/Release/UnstuckMEServerGUI.exe.Config";

                if (UpdateAppConfigExe())
                {
                    try
                    {
                        DirectoryInfo currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());
                        currentDir = currentDir.Parent.Parent.Parent;
                        string serverPath = currentDir.FullName + "/UnstuckMEServer/bin/Release/UnstuckMEServer.exe";

                        Process startServer = new Process
                        {
                            StartInfo =
                            {
                                Verb = "runas",
                                FileName = serverPath
                            }
                        };

                        startServer.Start();
                        DuplexChannelFactory<IUnstuckMEServer> channelFactory = new DuplexChannelFactory<IUnstuckMEServer>(new ServerCallback(),"UnstuckMEServerEndPoint");
                        IUnstuckMEServer testingChannel = channelFactory.CreateChannel();

                        if (testingChannel.TestNewConfig())
                        {
                            MessageBox.Show("Internal connection between administrator successful!",
                                "Connection Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            try
                            {
                                Process[] pname = Process.GetProcessesByName("UnstuckMEServer");
                                pname[0].Kill();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Server Kill Error", MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                            }

                            m_pastTest = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Unable To Connect At That Address", MessageBoxButton.OK,
                            MessageBoxImage.Exclamation);
                        m_pastTest = false;
                    }
                }
                else
                    m_pastTest = false;
            }
            else
                m_pastTest = false;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

