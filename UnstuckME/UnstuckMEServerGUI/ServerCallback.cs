using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
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
    }
}
