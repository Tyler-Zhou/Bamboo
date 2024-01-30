using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ICP.EDIManager.ServiceInterface.Entity
{
    public class ShippingOrder
    {
        /// <summary>
        ///业务Id
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public Guid Id { get; set; }

        /// <summary>
        ///业务No
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string No { get; set; }

        /// <summary>
        ///更新时间
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public DateTime UpdateDate { get; set; }
    }
    public class EdiSender
    {
        /// <summary>
        ///发送人Id
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public Guid Id { get; set; }

        /// <summary>
        ///发送人名称
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string Ename { get; set; }

        /// <summary>
        ///发送人邮箱
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string Email { get; set; }
    }
}
