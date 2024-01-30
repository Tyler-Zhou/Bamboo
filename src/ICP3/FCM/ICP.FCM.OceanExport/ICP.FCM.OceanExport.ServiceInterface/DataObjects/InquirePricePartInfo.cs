using System;

namespace ICP.FCM.OceanExport.ServiceInterface.DataObjects
{
    
    /// <summary>
    /// 询价面板实体类对象
    /// </summary>
    [Serializable]
    public class InquirePricePartInfo
    {
        /// <summary>
        /// 询价ID
        /// </summary>
        public Guid InquirePriceID { get; set; }

        /// <summary>
        /// 询价No
        /// </summary>
        public string InquirePriceNO { get; set; }

        /// <summary>
        /// 商务确认人ID
        /// </summary>
        public Guid ConfirmedByID { get; set; }

        /// <summary>
        /// 商务确认人Eame
        /// </summary>
        public string ConfirmedByEame { get; set; }

        /// <summary>
        /// 是否需要商务(重新)确认
        /// </summary>
        public bool NeedConfirmation { get; set; }
    }
}
