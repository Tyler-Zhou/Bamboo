using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ICP.EDIManager.ServiceInterface.Entity
{
    public class CargoTracking
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
        public string CarrierCode { get; set; }

        /// <summary>
        ///订舱号
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string BookingNo { get; set; }

        /// <summary>
        ///提单号
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string MBLNo { get; set; }

        /// <summary>
        ///箱号
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string ContainerNo { get; set; }

        /// <summary>
        ///箱子状态
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string ContainerState { get; set; }

        /// <summary>
        ///箱子状态生成时间
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public DateTime Date { get; set; }

        /// <summary>
        ///箱子所在地
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string Location { get; set; }

        /// <summary>
        ///卸货港
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string POD { get; set; }

        /// <summary>
        ///装货港
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string POL { get; set; }

        /// <summary>
        ///交货地
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string DeliveryPort { get; set; }

        /// <summary>
        ///收货地
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string ReceiptPort { get; set; }

        /// <summary>
        ///生成日
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public DateTime CreateDate { get; set; }
    }
}
