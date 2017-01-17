using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UnstuckMEInterfaces;
using System.Threading;

namespace UnstuckMEUserGUI
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    class ClientCallback : IClient
    {
        //Forces The Cient to close with a messagebox popup.
        //Message Style Legend: 0 - Info(Blue), 1 - Warning(Yellow), 2 - Error(Red)
        public void ForceClose(int messageStyle, string message)
        {
            //(StartWindow)Application.Current.MainWindow).MessageBoxToUserAndShutdown(messageStyle, message)
            //App.Current.MainWindow.Hide();
            try
            {
                ((StartWindow)Application.Current.MainWindow).StartThread(messageStyle, message);
            }
            catch(Exception ex)
            {

            }

        }

        public bool isOnline()
        {
            return true;
        }
    }
}
