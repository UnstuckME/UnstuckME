using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UnstuckMEInterfaces;

namespace UnstuckMEUserGUI
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    class ClientCallback : IClient
    {
        //Forces The Cient to close with a messagebox popup.
        //Message Style Legend: 0 - Info(Blue), 1 - Warning(Yellow), 2 - Error(Red)
        public void ForceClose(int messageStyle, string message)
        {
            App.Current.MainWindow.Hide();
            ((StartWindow)Application.Current.MainWindow).MessageBoxToUser(messageStyle, message);
            App.Current.Shutdown();
        }

        public bool isOnline()
        {
            return true;
        }
    }
}
