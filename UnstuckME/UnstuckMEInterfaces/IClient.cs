using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UnstuckME_Classes;

namespace UnstuckMEInterfaces
{
    public interface IClient
    {
        [OperationContract]
        bool isOnline();

        [OperationContract(IsOneWay = true)]
        void ForceClose();

        [OperationContract]
        void GetMessage(UnstuckMEMessage message);

		[OperationContract]
		void GetFile(UnstuckMEMessage message, UnstuckMEFile file);

        [OperationContract]
        void AddClasses(UserClass inClass);
    }
}
