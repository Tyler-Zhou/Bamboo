namespace ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.ReportObjects
{
    using System;

    /// <summary>
    ///派车国外报表数据对象(短格式)
    /// </summary>
    [Serializable]
    public class PickupENShortReportData
    {
        /// <summary>
        /// CompanyName(客户端构建)
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// CompanyAddress(客户端构建)
        /// </summary>
        public string CompanyAddress { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// ReferenceNO
        /// </summary>
        public string ReferenceNO { get; set; }
        /// <summary>
        /// Carrier
        /// </summary>
        public string Carrier { get; set; }
        /// <summary>
        /// CYLocation
        /// </summary>
        public string CYLocation { get; set; }
        /// <summary>
        /// Destination
        /// </summary>
        public string Destination { get; set; }
        /// <summary>
        /// BookingNO
        /// </summary>
        public string BookingNO { get; set; }
        /// <summary>
        /// ETA
        /// </summary>
        public DateTime? ETA { get; set; }
        /// <summary>
        /// LastFreeDate
        /// </summary>
        public string LastFreeDate { get; set; }
        /// <summary>
        /// ContainerNOs
        /// </summary>
        public string ContainerNOs { get; set; }
        /// <summary>
        /// HBLNO
        /// </summary>
        public string HBLNO { get; set; }
        /// <summary>
        /// CustomerRefNo
        /// </summary>
        public string CustomerRefNo { get; set; }
        /// <summary>
        /// DeliveryToInfo
        /// </summary>
        public string DeliveryToInfo { get; set; }
        /// <summary>
        /// PKGS
        /// </summary>
        public string PKGS { get; set; }
        /// <summary>
        /// GoodsDescription
        /// </summary>
        public string GoodsDescription { get; set; }
        /// <summary>
        /// Weigh
        /// </summary>
        public decimal Weigh { get; set; }
        /// <summary>
        /// Measurement
        /// </summary>
        public decimal Measurement { get; set; }
        /// <summary>
        /// PaymentTypeName
        /// </summary>
        public string PaymentTypeName { get; set; }
        /// <summary>
        /// CurrentUserName(客户端构建)
        /// </summary>
        public string CurrentUserName { get; set; }
    }

    /// <summary>
    /// 派车国外报表数据对象
    /// </summary>
    [Serializable]
    public class PickupENReportData
    {
        /// <summary>
        /// CompanyName(客户端构建)
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// CompanyAddress(客户端构建)
        /// </summary>
        public string CompanyAddress { get; set; }
        /// <summary>
        /// CompanyTelFax(客户端构建)
        /// </summary>
        public string CompanyTelFax { get; set; }
        /// <summary>
        /// TruckerInfo(拖车公司描述)
        /// </summary>
        public string TruckerInfo { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// CurrentUserName(客户端构建)
        /// </summary>
        public string CurrentUserName { get; set; }
        /// <summary>
        /// Commodity(2.0拖车中有个Commodity的维护框
        /// </summary>
        public string Commodity { get; set; }
        /// <summary>
        /// TotalPKGS(2.0拖车中有个TotalPKGS的维护框
        /// </summary>
        public string TotalPKGS { get; set; }
        /// <summary>
        /// PickupAtInfo(提柜地
        /// </summary>
        public string PickupAtInfo { get; set; }
        /// <summary>
        /// CustomerRefNo(2.0拖车中有个Trucker RefNo的维护框
        /// </summary>
        public string CustomerRefNo { get; set; }
        /// <summary>
        /// PickupContact(2.0拖车中有个维护框
        /// </summary>
        public string PickupContact { get; set; }
        /// <summary>
        /// PickupDate
        /// </summary>
        public DateTime? PickupDate { get; set; }
        /// <summary>
        /// DeliveryToInfo
        /// </summary>
        public string DeliveryToInfo { get; set; }
        /// <summary>
        /// DeliveryRefrenceNo
        /// </summary>
        public string DeliveryRefrenceNo { get; set; }
        /// <summary>
        /// DeliveryContact
        /// </summary>
        public string DeliveryContact { get; set; }
        /// <summary>
        /// DeliveryDate
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// LastFreeDate
        /// </summary>
        public DateTime? LastFreeDate { get; set; }
        /// <summary>
        /// BillToInfo
        /// </summary>
        public string BillToInfo { get; set; }
        /// <summary>
        /// BillToRefNO
        /// </summary>
        public string BillToRefNO { get; set; }
        /// <summary>
        /// Carrier
        /// </summary>
        public string Carrier { get; set; }
        /// <summary>
        /// BookingNO
        /// </summary>
        public string BookingNO { get; set; }
        /// <summary>
        /// VesselName
        /// </summary>
        public string VesselName { get; set; }
        /// <summary>
        /// ContainerNOs(CCLU6970035 / 40HQ 
        /// </summary>
        public string ContainerNOs { get; set; }
        /// <summary>
        /// HBLNo
        /// </summary>
        public string HBLNo { get; set; }
        /// <summary>
        /// PortofReceipt
        /// </summary>
        public string PortofReceipt { get; set; }
        /// <summary>
        /// POL
        /// </summary>
        public string POL { get; set; }
        /// <summary>
        /// ETD
        /// </summary>
        public DateTime? ETD { get; set; }
        /// <summary>
        /// Marks
        /// </summary>
        public string Marks { get; set; }
        /// <summary>
        /// GoodsDescription
        /// </summary>
        public string GoodsDescription { get; set; }
        /// <summary>
        /// PKGS
        /// </summary>
        public int PKGS { get; set; }
        /// <summary>
        /// Weight
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// Measurement
        /// </summary>
        public decimal Measurement { get; set; }
    }

    /// <summary>
    /// 派车国内报表数据对象
    /// </summary>
    [Serializable]
    public class PickupCNReportData
    {
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// To
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// ToATTN
        /// </summary>
        public string ToATTN { get; set; }
        /// <summary>
        /// ToTel
        /// </summary>
        public string ToTel { get; set; }
        /// <summary>
        /// ToFax
        /// </summary>
        public string ToFax { get; set; }
        /// <summary>
        /// From(客户端构建)
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// FromMail(客户端构建)
        /// </summary>
        public string FromMail { get; set; }
        /// <summary>
        /// FromTel(客户端构建)
        /// </summary>
        public string FromTel { get; set; }
        /// <summary>
        /// FromFax(客户端构建)
        /// </summary>
        public string FromFax { get; set; }
        /// <summary>
        /// SONO
        /// </summary>
        public string SONO { get; set; }
        /// <summary>
        /// VesselVoyage
        /// </summary>
        public string VesselVoyage { get; set; }
        /// <summary>
        /// ContainerInfo
        /// </summary>
        public string ContainerInfo { get; set; }
        /// <summary>
        /// Carrier
        /// </summary>
        public string Carrier { get; set; }
        /// <summary>
        /// LoadDate
        /// </summary>
        public DateTime? LoadDate { get; set; }
        /// <summary>
        /// LoadAddress
        /// </summary>
        public string LoadAddress { get; set; }
        /// <summary>
        /// Charges
        /// </summary>
        public string Charges { get; set; }
        /// <summary>
        /// NeedBook
        /// </summary>
        public string NeedBook { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }
    }


}
