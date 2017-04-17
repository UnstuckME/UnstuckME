using System;
using System.ServiceModel;
using System.Threading;

namespace UnstuckMEInterfaces
{
    class Program
    {
        public static UnstuckMEService _server;
        static void Main(string[] args)
        {
            try
            {
                _server = new UnstuckMEService();
                using (ServiceHost host = new ServiceHost(_server))
                {
                    host.Open();

                    Thread userStatusCheck = new Thread(_server.CheckUserStatus);
                    userStatusCheck.Start();
                    Thread newMessageCheck = new Thread(_server.CheckForNewMessages);
                    newMessageCheck.Start();
                    Thread newStickerCheck = new Thread(_server.CheckForNewStickers);
                    newStickerCheck.Start();

                    Console.WriteLine("Server is Running...");
                    Console.WriteLine("<Press Enter to Shut Down Server>");
                    Console.ReadLine();
					Console.WriteLine("Server shutting down");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("\nSERVER START ERROR: \n" + ex.Message);
                Console.WriteLine("\nPress Enter To Exit...");
                Console.ReadLine();
            }
            
        }
    }
}
