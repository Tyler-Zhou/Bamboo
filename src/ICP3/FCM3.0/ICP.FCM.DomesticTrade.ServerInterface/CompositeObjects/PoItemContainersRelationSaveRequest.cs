﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.DomesticTrade.ServiceInterface.CompositeObjects
{
    [Serializable]
    public class PoItemContainersRelationSaveRequest
    {
        public Guid[] poIds;

        public Guid[] poContainerIds;

        public Guid[] itemIds;

        public Guid[] itemContainerIds;

        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID;
    }
}
