using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 联系人参数
    /// </summary>
    [Serializable]
    public class OperationContactParameters
    {
        public List<string> Mails { get; set; }

        public Guid OceanBookingID { get; set; }

        public OperationType OperationType { get; set; }
    }
}
