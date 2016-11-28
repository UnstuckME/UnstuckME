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
        static void Main(string[] args)
        {
            /*
             *Opens an UnstuckMEService for clients to connect to.  
             */
            using (ServiceHost host = new ServiceHost(typeof(UnstuckMEService)))
            {
                host.Open();
                Console.WriteLine("Server is Open");
                Console.WriteLine("<Press Enter to Shut Down Server");
                Console.ReadLine();
            }
        }
    }
}
