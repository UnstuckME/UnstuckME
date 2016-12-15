using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnstuckMEInterfaces;

namespace UnstuckMEServer
{
    public class ConnectedClient
    {
        public IClient connection;
        public string EmailAddress { get; set; }
    }
}
