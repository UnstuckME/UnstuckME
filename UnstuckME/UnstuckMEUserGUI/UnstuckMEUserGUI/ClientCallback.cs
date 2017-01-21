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

namespace UnstuckMEUserGUI
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    class ClientCallback : IClient
    {
        //Forces The Cient to close with a messagebox popup.
        //Message Style Legend: 0 - Info(Blue), 1 - Warning(Yellow), 2 - Error(Red)
        public void ForceClose()
        {
            //((StartWindow)Application.Current.MainWindow).ForceClose();
            UnstuckMEMessageBox messageBox = new UnstuckMEMessageBox(0, "Server has shutdown. Please Contact Your Server Administrator");
            messageBox.Show();
                //Task.Factory.StartNew(() => ((StartWindow)Application.Current.MainWindow).ForceClose(), new CancellationToken(false), TaskCreationOptions.None ,TaskScheduler.FromCurrentSynchronizationContext());
            //MessageBox.Show("Server Has Shutdown");
            //string unstuckME = System.AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName;
            //Process.Start(unstuckME);
            //Application.Current.Shutdown();
            //((StartWindow)Application.Current.MainWindow).ForceCloseStart();
        }

        public bool isOnline()
        {
            return true;
        }
    }
}
