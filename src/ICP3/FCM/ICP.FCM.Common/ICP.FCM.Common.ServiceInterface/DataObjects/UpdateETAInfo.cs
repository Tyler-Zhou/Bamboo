using System;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateETAInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid VoyageID
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid CompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsETA
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsWareHouse
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ETA
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid WareHouseID
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public String WareHouseName
        {
            get;
            set;
        }
    }
}
