using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UnstuckMEInterfaces;

namespace UnstuckMEServerGUI
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    class ServerCallback : IServer
    {
        public bool isOnline()
        {
            return true;
        }

        public void GetUpdate(int value , string emailAddress)
        {
            switch(value)
            {
                case 0:
                    {
                        ((ServerRunning)Application.Current.MainWindow).AddUser(emailAddress);
                        break;
                    }
                case 1:
                    {
                        ((ServerRunning)Application.Current.MainWindow).RemoveUser(emailAddress);
                        break;
                    }
            }
        }
    }
}
