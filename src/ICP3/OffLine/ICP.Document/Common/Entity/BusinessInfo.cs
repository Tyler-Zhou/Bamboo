#region Comment

/*
 * 
 * FileName:    BusinessInfo.cs
 * CreatedOn:   2014/5/14 星期三 17:15:27
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->业务订单实体类
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;

namespace ICP.Document
{
    /// <summary>
    /// 业务订单实体类
    /// </summary>
    [Serializable]
    public class BusinessInfo
    {
        /// <summary>
        /// 业务操作ID
        /// </summary>
        public Guid OperationID{get;set;}
        /// <summary>
        /// NO：业务号
        /// </summary>
        public String NO { get; set; }
        /// <summary>
        /// SO NO
        /// </summary>
        public String SO_NO { get; set; }
        /// <summary>
        /// Carrier
        /// </summary>
        public String Carrier { get; set; }
        /// <summary>
        /// POL
        /// </summary>
        public String POL { get; set; }
        /// <summary>
        /// POD
        /// </summary>
        public String POD { get; set; }
        /// <summary>
        /// Vessel
        /// </summary>
        public String Vessel { get; set; }
        /// <summary>
        /// Voyage
        /// </summary>
        public String Voyage { get; set; }
        /// <summary>
        /// RefNO
        /// </summary>
        public String RefNO { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// ContainerDesc
        /// </summary>
        public String ContainerDesc { get; set; }
        /// <summary>
        /// ContainerNO
        /// </summary>
        public String ContainerNO { get; set; }
        /// <summary>
        /// ETD
        /// </summary>
        public DateTime? ETD { get; set; }
        /// <summary>
        /// ETA
        /// </summary>
        public DateTime? ETA { get; set; }
    }
}
