using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    /// <summary>
    /// 测试
    /// </summary>
    public class TestUIInfo : BaseDataObject
    {
        public bool IsValid { get; set; }

        public LedgerMasterStatus Status { get; set; }
    }
}
