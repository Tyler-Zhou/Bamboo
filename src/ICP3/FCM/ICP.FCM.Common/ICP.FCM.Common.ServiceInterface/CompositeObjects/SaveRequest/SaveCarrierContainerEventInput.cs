using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.Common.ServiceInterface.CompositeObjects
{
    public class SaveCarrierContainerEventInput
    {
        public Guid Id { get; set; }
        public Guid ContainerID { get; set; }
        public int EventIndex { get; set; }
        public DateTime EventDate { get; set; }
        public string EvenLocation { get; set; }
        public string EventDescription { get; set; }
        public string Mode { get; set; }
        public int EventStatus { get; set; }
        public int EventType { get; set; }
        public bool IsManual { get; set; }
        public Guid SaveByID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string EventCode { get; set; }
        public bool IsEnglish { get; set; }
    }
}
