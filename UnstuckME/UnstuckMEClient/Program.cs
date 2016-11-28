using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UnstuckMEInterfaces;

namespace UnstuckMEClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Opens a connection to UnstuckME Database.
            ChannelFactory<IUnstuckMEService> channelFactory = new ChannelFactory<IUnstuckMEService>("UnstuckMEClient");

            IUnstuckMEService proxy = channelFactory.CreateChannel();

            //Calls Change username OperationalContract (Located in IUnstuckMEService.cs -> UnstuckMEService.cs)
            proxy.ChangeUserName("JAMES.SMITH@oit.edu", "Jacob", "Jericho");

            //Calls List all user first name (Located in IUnstuckMEService.cs -> UnstuckMEService.cs)
            List<string> users = proxy.ListUsersFullName();

            foreach (var item in users)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}
