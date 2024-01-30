using System;
using System.Collections.Generic;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CargoTrackingSaveRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid OperationID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? CarrierID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? VoyageID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ETD { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ETA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DETA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FETA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? PickUpID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Guid> ContainerIds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DateTime?> LastFreeDates { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DateTime?> PickUpDates { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DateTime?> ReturnDates { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DateTime?> AvailableTimes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DateTime?> DeliveryTimes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> PickUpNos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DateTime?> AvailableDates { get; set; }
    }
}
