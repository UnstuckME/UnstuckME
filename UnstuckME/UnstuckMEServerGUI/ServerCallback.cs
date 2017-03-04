using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UnstuckME_Classes;
using UnstuckMEInterfaces;

namespace UnstuckMEServerGUI
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    class ServerCallback : IServer
    {
		public void GetMessage()
		{
			throw new NotImplementedException();
		}

		public bool isOnline()
        {
            return true;
        }

        public void GetUpdate(int value, UnstuckME_Classes.UserInfo user)
        {
            switch (value)
            {
                case 0:
                    {
                        ((ServerRunning)Application.Current.MainWindow).AddUser(user.EmailAddress, (Privileges)user.Privileges);
                        break;
                    }
                case 1:
                    {
                        ((ServerRunning)Application.Current.MainWindow).RemoveUser(user.EmailAddress);
                        break;
                    }
            }
        }
    }

	//[CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
	//class StreamCallback : IFileStream
	//{
	//	public Stream EchoStream(Stream stream)
	//	{
	//		string filepath = "C:\\Users\\colem_000\\Documents\\OIT\\CST 316-26-36 - Junior Project\\UnstuckME\\UnstuckMECore\\UnstuckME\\UnstuckMEServer\\Files";

	//		try
	//		{
	//			FileStream file = File.Create(Environment.CurrentDirectory);
	//			stream.CopyToAsync(file);
	//			return file;
	//		}
	//		catch (Exception ex)
	//		{
	//			Console.WriteLine("An exception was thrown while trying to open file {0}", filepath);
	//			Console.WriteLine("Exception is: ");
	//			Console.WriteLine(ex.ToString());
	//			throw ex;
	//		}
	//	}

	//	public Stream GetReversedStream()
	//	{
	//		throw new NotImplementedException();
	//	}

	//	public Stream GetStream(string data)
	//	{
	//		throw new NotImplementedException();
	//	}

	//	public bool UploadStream(Stream stream)
	//	{
	//		throw new NotImplementedException();
	//	}
	//}
}
