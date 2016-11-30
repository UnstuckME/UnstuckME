using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;

namespace UnstuckMEServerGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_RunServer_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            currentDir = currentDir.Parent;
            currentDir = currentDir.Parent;
            currentDir = currentDir.Parent;
            string serverPath = currentDir.FullName + "/UnstuckMEServer/bin/Release/UnstuckMEServer.exe";
            Process startServer = new Process();
            startServer.StartInfo.RedirectStandardOutput = true;
            startServer.StartInfo.UseShellExecute = false;
            startServer.StartInfo.CreateNoWindow = true;
            startServer.StartInfo.Verb = "runas";
            startServer.StartInfo.FileName = serverPath;
            startServer.Start();
        }

        private void buttonKillServer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process[] server = Process.GetProcessesByName("UnstuckMEServer");
                server[0].Kill();
            }
            catch
            {

            }

        }
    }
}
