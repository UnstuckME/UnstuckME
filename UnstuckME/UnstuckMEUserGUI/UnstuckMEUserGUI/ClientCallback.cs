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
        public void ForceClose()
        {
            UnstuckMEMessageBox messageBox = new UnstuckMEMessageBox(0, "UnstuckME Server has shutdown. Please Contact Your Server Administrator");
            messageBox.Show();
        }

        public bool isOnline()
        {
            return true;
        }
    }
}
