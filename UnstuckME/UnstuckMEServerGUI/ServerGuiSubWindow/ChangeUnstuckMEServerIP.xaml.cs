using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using UnstuckMEInterfaces;

namespace UnstuckMEServerGUI.ServerGuiSubWindow
{
    /// <summary>
    /// Interaction logic for ChangeUnstuckMEServerIP.xaml
    /// </summary>
    public partial class ChangeUnstuckMEServerIP : Window
    {

        private string m_SchoolName = System.Configuration.ConfigurationManager.AppSettings["SchoolName"];
        private int? m_serverID = null;
        private bool m_pastTest = false;
        private string m_appConfigDir = "";


        public ChangeUnstuckMEServerIP()
        {
            InitializeComponent();

            using (UnstuckME_SchoolsEntities schoolDB = new UnstuckME_SchoolsEntities())
            {
                var serverInfo = (from Servers in schoolDB.Servers
                    where Servers.School.SchoolName == m_SchoolName
                    select new {Servers.ServerID, Servers.ServerIPAddress, Servers.ServerName}).First();
                if (serverInfo != null)
                {
                    textBoxNewIP.Text = serverInfo.ServerIPAddress;
                    textBoxServerName.Text = serverInfo.ServerName;
                }
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            buttonTest_Click(sender, e);


        }

        

        private bool CheckIPAddress()
        {
            string input = textBoxNewIP.Text;

            bool isAnIPAddress = false;

            IPAddress address;
            if (IPAddress.TryParse(input, out address))
            {
                switch (address.AddressFamily)
                {
                    case System.Net.Sockets.AddressFamily.InterNetwork:
                        isAnIPAddress = true;
                        break;
                    case System.Net.Sockets.AddressFamily.InterNetworkV6:
                        isAnIPAddress = true;
                        break;
                    default:
                        isAnIPAddress = false;
                        break;
                }
            }

            return isAnIPAddress;
        }

        private bool CheckLocalHost()
        {
            bool localHostConnection = false;
            if (textBoxNewIP.Text == "net.tcp://localhost")
            {
                localHostConnection = true;
            }
            else if (textBoxNewIP.Text == "//localhost")
            {
                textBoxNewIP.Text = "net.tcp://localhost";
                localHostConnection = true;
            }
            else
            {
                localHostConnection = false;
            }

            return localHostConnection;
        }

        private bool UpdateAppConfigExe()
        {
            bool exeUpdated = false;

            if (CheckIPAddress() == false && CheckLocalHost() == false)
            {
                MessageBox.Show("It appears you have not eneter a valid IPV4/ IPV6/ or //LocalHost connection", "Invalid COnnection Settings", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(m_appConfigDir);

                if (CheckIPAddress() == true)
                {
                    XmlNodeList endpoints = doc.GetElementsByTagName("endpoint");
                    foreach (XmlNode item in endpoints)
                    {
                        var adressAttribute = item.Attributes["address"];
                        if (!ReferenceEquals(null, adressAttribute))
                        {
                            adressAttribute.Value = string.Format("net.tcp://{0}:9000/UnstuckMEService", textBoxNewIP.Text);
                        }

                        var bindingAttribute = item.Attributes["bindingConfiguration"];
                        if (!ReferenceEquals(null, adressAttribute))
                        {
                            bindingAttribute.Value = string.Format("TcpBindingConfiguration");
                        }
                    }

                    XmlNodeList bindingNames = doc.GetElementsByTagName("binding");
                    foreach (XmlNode item in bindingNames)
                    {
                        var bindingName = item.Attributes["name"];
                        if (!ReferenceEquals(null, bindingName))
                        {
                            bindingName.Value = string.Format("TcpBindingConfiguration");
                        }

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

                else if (CheckLocalHost() == true)
                {
                    XmlNodeList endpoints = doc.GetElementsByTagName("endpoint");
                    foreach (XmlNode item in endpoints)
                    {
                        var adressAttribute = item.Attributes["address"];
                        if (!ReferenceEquals(null, adressAttribute))
                        {
                            adressAttribute.Value = string.Format("net.tcp://localhost:9000/UnstuckMEService");
                        }

                        var bindingAttribute = item.Attributes["bindingConfiguration"];
                        if (!ReferenceEquals(null, adressAttribute))
                        {
                            bindingAttribute.Value = string.Format("IncreaseMaxBuffer_and_MessageSize");
                        }
                    }

                    XmlNodeList bindingNames = doc.GetElementsByTagName("binding");

                    foreach (XmlNode item in bindingNames)
                    {
                        var bindingName = item.Attributes["name"];
                        if (!ReferenceEquals(null, bindingName))
                        {
                            bindingName.Value = string.Format("IncreaseMaxBuffer_and_MessageSize");
                        }

                        if (!ReferenceEquals(null, item.Attributes["sendTimeout"]))
                        {
                            item.Attributes.RemoveNamedItem("sendTimeout");
                        }
                        if (!ReferenceEquals(null, item.Attributes["portSharingEnabled"]))
                        {
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

            if (UpdateAppConfigExe() == true)
            {
                m_appConfigDir = tempDir.FullName + "/UnstuckMEServerGUI/bin/Release/UnstuckMEServerGUI.exe.Config";

                if (UpdateAppConfigExe() == true)
                {

                    try
                    {
                        DirectoryInfo currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());
                        currentDir = currentDir.Parent.Parent.Parent;
                        string serverPath = currentDir.FullName + "/UnstuckMEServer/bin/Release/UnstuckMEServer.exe";

                        Process startServer = new Process();

                        startServer.StartInfo.Verb = "runas";
                        startServer.StartInfo.FileName = serverPath;
                        startServer.Start();


                        DuplexChannelFactory<IUnstuckMEServer> channelFactory =
                            new DuplexChannelFactory<IUnstuckMEServer>(new ServerCallback(),
                                "UnstuckMEServerLocalHostEndPoint");
                        IUnstuckMEServer testingChannel = channelFactory.CreateChannel();

                        if (testingChannel.TestNewConfig() == true)
                        {
                            MessageBox.Show("Internal connection between administrator successful!", "Connection Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            try
                            {
                                Process[] pname = Process.GetProcessesByName("UnstuckMEServer");
                                pname[0].Kill();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Server Kill Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Unable To Connect At That Address", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

