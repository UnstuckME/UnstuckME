using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UnstuckMEInterfaces;
using System.Threading;
using System.Diagnostics;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    class ClientCallback : IClient
    {
        public void AddClasses(UserClass inClass)
        {
            try
            {
                Application.Current.Windows.OfType<UnstuckMEWindow>().SingleOrDefault().RecieveAddedClass(inClass);
            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Forces The Cient to close with a messagebox popup.
        public void ForceClose()
        {
            UnstuckMEMessageBox messageBox = new UnstuckMEMessageBox(0, "UnstuckME Server has shutdown. Please Contact Your Server Administrator", "Server Shutdown");
            messageBox.Show();
        }

        //This Will Update a users conversation if they are online and another user sends them a message.
        public void GetMessage(UnstuckMEMessage message)
        {
            Application.Current.Windows.OfType<UnstuckMEWindow>().SingleOrDefault().RecieveChatMessage(message);

        }

		public void GetFile(UnstuckMEMessage message, UnstuckMEFile file)
		{
			Application.Current.Windows.OfType<UnstuckMEWindow>().SingleOrDefault().RecieveChatFile(message, file);
		}

        public void RemoveGUISticker(int stickerID)
        {
            try
            {
                Application.Current.Windows.OfType<UnstuckMEWindow>().SingleOrDefault().RemoveStickerFromAvailableStickers(stickerID);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
