using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UnstuckMEInterfaces;

namespace UnstuckMEUserGUI
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    class ClientCallback : IClient
    {
        public void GetMessage(string message, string emailAddress)
        {
            throw new NotImplementedException();
        }
    }
}
