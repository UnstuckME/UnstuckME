﻿using System;
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
        [OperationContract(IsOneWay = true)]
        void ForceClose();

        [OperationContract]
        void GetMessageFromServer(string message);

        [OperationContract]
        void GetMessage(UnstuckMEMessage message);

		[OperationContract]
		void GetFile(UnstuckMEMessage message, UnstuckMEFile file);

        [OperationContract]
        void AddClasses(UserClass inClass);

        [OperationContract]
        void RemoveGUISticker(int stickerID);

        [OperationContract(IsOneWay = true)]
        void RecieveNewSticker(UnstuckMEAvailableSticker inSticker);
    }
}
