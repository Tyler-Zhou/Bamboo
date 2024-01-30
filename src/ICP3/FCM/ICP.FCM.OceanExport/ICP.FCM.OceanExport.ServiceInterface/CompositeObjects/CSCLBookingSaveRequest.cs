using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.OceanExport.ServiceInterface.CompositeObjects
{
    public class CSCLBookingSaveRequest : SaveRequest
    {
        private Guid oceanBookingID;
        /// <summary>
        /// 所属订舱单的GUID
        /// </summary>
        public Guid OceanBookingID
        {
            get
            {
                return oceanBookingID;
            }
            set
            {
                oceanBookingID = value;
            }
        }
        private Guid? id;
        /// <summary>
        /// ID
        /// </summary>
        public Guid? ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        private string scno;
        /// <summary>
        /// 美线服务合同号
        /// </summary>
        public string SCNO
        {
            get
            {
                return scno;
            }
            set
            {
                scno = value;
            }
        }
        private string scacCode;
        /// <summary>
        /// SCACCode
        /// </summary>
        public string SCACCode
        {
            get
            {
                return scacCode;
            }
            set
            {
                scacCode = value;
            }
        }
        private string hblNO;
        /// <summary>
        /// HBLNO
        /// </summary>
        public string HBLNO
        {
            get
            {
                return hblNO;
            }
            set
            {
                hblNO = value;
            }
        }
        private string shipper;
        /// <summary>
        /// 发货人
        /// </summary>
        public string Shipper
        {
            get
            {
                return shipper;
            }
            set
            {
                shipper = value;
            }
        }
        private string realShipper;
        /// <summary>
        /// 真实发货人
        /// </summary>
        public string RealShipper
        {
            get
            {
                return realShipper;
            }
            set
            {
                realShipper = value;
            }
        }
        private string consignee;
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee
        {
            get
            {
                return consignee;
            }
            set
            {
                consignee = value;
            }
        }
        private string realConsignee;
        /// <summary>
        /// 真实收货人
        /// </summary>
        public string RealConsignee
        {
            get
            {
                return realConsignee;
            }
            set
            {
                realConsignee = value;
            }
        }
        private string notify;
        /// <summary>
        /// 通知人
        /// </summary>
        public string Notify
        {
            get
            {
                return notify;
            }
            set
            {
                notify = value;
            }
        }
        private string realNotify;
        /// <summary>
        /// 真实通知人
        /// </summary>
        public string RealNotify
        {
            get
            {
                return realNotify;
            }
            set
            {
                realNotify = value;
            }
        }
        private string cargoDescUS;
        /// <summary>
        /// 英文品名
        /// </summary>
        public string CargoDescUS
        {
            get
            {
                return cargoDescUS;
            }
            set
            {
                cargoDescUS = value;
            }
        }
        private string remarksCN;
        /// <summary>
        /// 附加说明（中文）
        /// </summary>
        public string RemarksCN
        {
            get
            {
                return remarksCN;
            }
            set
            {
                remarksCN = value;
            }
        }
        private string bookingRemarksCN;
        /// <summary>
        /// 订舱要求（中文）
        /// </summary>
        public string BookingRemarksCN
        {
            get
            {
                return bookingRemarksCN;
            }
            set
            {
                bookingRemarksCN = value;
            }
        }

        private string bookingNO;
        /// <summary>
        /// 业务号/订舱号
        /// </summary>
        public string BookingNO
        {
            get
            {
                return bookingNO;
            }
            set
            {
                bookingNO = value;
            }
        }
        private string marks;
        /// <summary>
        /// 唛头
        /// </summary>
        public string Marks
        {
            get
            {
                return marks;
            }
            set
            {
                marks = value;
            }
        }
        private string deliveryTerm;
        /// <summary>
        /// 运输条款
        /// </summary>
        public string DeliveryTerm
        {
            get
            {
                return deliveryTerm;
            }
            set
            {
                deliveryTerm = value;
            }
        }
        private string releaseCargoType;
        /// <summary>
        /// 放货方式
        /// </summary>
        public string ReleaseCargoType
        {
            get
            {
                return releaseCargoType;
            }
            set
            {
                releaseCargoType = value;
            }
        }
        private string hsCode;
        /// <summary>
        /// 放货方式
        /// </summary>
        public string HSCode
        {
            get
            {
                return hsCode;
            }
            set
            {
                hsCode = value;
            }
        }
        public Guid SaveByID { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
