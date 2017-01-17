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
        public void ForceClose(int messageStyle, string message)
        {
            App.Current.MainWindow.Hide();
            ((StartWindow)Application.Current.MainWindow).MessageBoxToUser(messageStyle, message);
            App.Current.Shutdown();
        }

        public void GetMessage(string message, string emailAddress)
        {
            throw new NotImplementedException();
        }

        public bool isOnline()
        {
            return true;
        }
    }
}
