using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ICP.EDIManager.ServiceInterface.Entity
{
    public class InttraNum
    {
        /// <summary>
        ///业务Id
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public Guid Id { get; set; }

        /// <summary>
        ///船东编号
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string No { get; set; }

        /// <summary>
        ///订舱号
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string InttraNumber { get; set; }
    }
}
