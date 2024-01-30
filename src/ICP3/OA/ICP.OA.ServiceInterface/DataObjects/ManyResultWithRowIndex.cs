using System;
using System.Collections.Generic;

namespace ICP.OA.ServiceInterface.DataObjects
{
    [Serializable]
    public class ManyResultWithRowIndex
    {
        public ManyResultWithRowIndex()
        {
            this.Results = new List<SingleResultWithRowIndex>();
        }

        public List<SingleResultWithRowIndex> Results
        {
            get;
            set;
        }
    }

    [Serializable]
    public class SingleResultWithRowIndex
    {
        public Guid ID { get; set; }

        public int RowIndex { get; set; }

        public DateTime? UpdateDate { get; set; }
    }


    [Serializable]
    public class SingleResultWithHierarchyCode
    {
        public Guid ID { get; set; }

        public string HierarchyCode { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
