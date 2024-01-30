using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.Sys.ServiceInterface.DataObjects
{
    [Serializable]
    public class WorkSpaceList : BaseDataObject
    {
        public Guid ID { get; set; }
        public string Code { get; set; }
        public string CName { get; set; }
        public string EName { get; set; }
        public Guid CreateBy { get; set; }
        public string CreateByName { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
      [Serializable]
    public class OperationViewList : BaseDataObject
    {
        public Boolean IsCheck { get; set; }
        public Int32 ShowIndex { get; set; }
        public Guid ID { get; set; }
        public string Code { get; set; }
        public string CName { get; set; }
        public string EName { get; set; }
        public string TooltiopCN { get; set; }
        public string TooltiopEN { get; set; }
        public OperationType OperationType { get; set; }
        public string SelectedColumn { get; set; }
        public string BaseCriteria { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

    }

}
