using System;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigureCustomerInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid CustomerID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerCname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerEname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BLTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CAScacCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string USScacCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
