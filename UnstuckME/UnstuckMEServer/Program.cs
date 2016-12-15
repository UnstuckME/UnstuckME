using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace UnstuckMEInterfaces
{
    class Program
    {
        public static UnstuckMEService _server;
        static void Main(string[] args)
        {
            /*
             *Opens an UnstuckMEService for clients to connect to.  
             */
            _server = new UnstuckMEService();
            using (ServiceHost host = new ServiceHost(_server))
            {
                host.Open();
                Console.WriteLine("Server is Running...");
                Console.WriteLine("<Press Enter to Shut Down Server");
                Console.ReadLine();
            }
        }
    }
}
