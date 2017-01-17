using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace UnstuckMEInterfaces
{
    public interface IClient
    {
        [OperationContract]
        bool isOnline();

        [OperationContract]
        void ForceClose(int messageStyle, string message);
    }
}
