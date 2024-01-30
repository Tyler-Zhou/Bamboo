using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.FAM.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// CredentialsDetailSaveRequest
    /// </summary>
    [Serializable]
    public class CredentialsDetailSaveRequest : SaveRequest
    {
        /// <summary>
        /// WriteOffID
        /// </summary>
        public Guid WriteOffID
        {
            get;
            set;
        }
        /// <summary>
        /// IDs
        /// </summary>
        public Guid?[] IDs
        {
            get;
            set;
        }
        /// <summary>
        /// GLIDs
        /// </summary>
        public Guid[] GLIDs
        {
            get;
            set;
        }
        /// <summary>
        /// Remarks
        /// </summary>
        public string[] Remarks
        {
            get;
            set;
        }
        /// <summary>
        /// OrgDebigs
        /// </summary>
        public decimal[] OrgDebigs
        {
            get;
            set;
        }
        /// <summary>
        /// OrgCredits
        /// </summary>
        public decimal[] OrgCredits
        {
            get;
            set;
        }
        /// <summary>
        /// Rates
        /// </summary>
        public decimal[] Rates
        {
            get;
            set;
        }
        /// <summary>
        /// Debigs
        /// </summary>
        public decimal[] Debigs
        {
            get;
            set;
        }
        /// <summary>
        /// Credits
        /// </summary>
        public decimal[] Credits
        {
            get;
            set;
        }
        /// <summary>
        /// Customer
        /// </summary>
        public string[] Customer
        {
            get;
            set;
        }
        /// <summary>
        /// UpdateDate
        /// </summary>
        public DateTime?[] UpdateDate
        {
            get;
            set;
        }
    }
}
