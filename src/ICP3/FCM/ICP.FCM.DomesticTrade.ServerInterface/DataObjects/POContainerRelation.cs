using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.DomesticTrade.ServiceInterface.DataObjects
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CompositePoAndItems : BaseDataObject
    {
        /// <summary>
        /// 
        /// </summary>
        public List<DTBookingPOList> PoList {get;set;}

        /// <summary>
        /// 
        /// </summary>
        public List<DTPOItemList> PoItemsList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<POContainerRelation> POContainerRelationsList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<PoItemContainerRelation> PoItemContainerRelationsList { get; set; }
    }

    /// <summary>
    /// PO和Container的关联记录
    /// </summary>
    [Serializable]
    public class POContainerRelation : BaseDataObject
    {
        /// <summary>
        /// 箱子ID
        /// </summary>
        public Guid ContainerId { get; set; }

        /// <summary>
        /// PO的ID
        /// </summary>
        public Guid PoId { get; set; }
    }

    /// <summary>
    /// PO的Item和Container的关联记录
    /// </summary>
    [Serializable]
    public class PoItemContainerRelation : BaseDataObject
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid PoId { get; set; }

        /// <summary>
        /// Item的ID
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// 箱子ID
        /// </summary>
        public Guid ContainerId { get; set; }
    }
}
