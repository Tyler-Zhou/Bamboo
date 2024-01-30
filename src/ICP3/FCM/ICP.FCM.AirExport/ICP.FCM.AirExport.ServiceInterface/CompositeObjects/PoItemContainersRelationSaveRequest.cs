﻿using System;

namespace ICP.FCM.AirExport.ServiceInterface.CompositeObjects
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
