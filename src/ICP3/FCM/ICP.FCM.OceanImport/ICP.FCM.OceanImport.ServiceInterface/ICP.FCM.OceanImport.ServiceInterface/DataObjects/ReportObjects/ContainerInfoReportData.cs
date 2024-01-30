using System;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    /// <summary>
    /// ContainerInfoReportData详细对象
    /// </summary>
    [Serializable]
    public class ContainerInfoReportData
    {
        /// <summary>
        /// ContainerNo
        /// </summary>
        public string ContainerNo { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }      

        /// <summary>
        /// SealNo
        /// </summary>
        public string SealNo { get; set; }

        /// <summary>
        /// PickupNo
        /// </summary>
        public string PickupNo { get; set; }

        /// <summary>
        /// LastFreeDate
        /// </summary>
        public string LastFreeDate { get; set; }

        /// <summary>
        /// GODate
        /// </summary>
        public string GODate { get; set; }

        /// <summary>
        /// 箱的数量
        /// </summary>
        public string Quantity { get; set; }

        /// <summary>
        /// 箱的重量
        /// </summary>
        public string Weight { get; set; }
    }
}
