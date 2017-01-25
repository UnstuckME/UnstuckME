using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnstuckMEInterfaces
{
    class Program
    {
        public static UnstuckMEService _server;
        static void Main(string[] args)
        {
            /*
             * potato
             *Opens an UnstuckMEService for clients to connect to.  
             */
             //testiung things
            _server = new UnstuckMEService();
            using (ServiceHost host = new ServiceHost(_server))
            {
                host.Open();
                //Task.Factory.StartNew(_server.CheckStatus, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                Thread userStatusCheck = new Thread(_server.CheckUserStatus);
                userStatusCheck.Start();
                Console.WriteLine("Server is Running...");
                Console.WriteLine("<Press Enter to Shut Down Server");
                Console.ReadLine();
            }
        }
    }
}
