using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace UnstuckMEInterfaces
{
    public interface IServer
    {
        [OperationContract]
        void GetMessage();

        [OperationContract]
        bool isOnline();

        [OperationContract]
        void GetUpdate(int value, string emailAddress);
    }
}
