using System;

namespace ICP.FCM.OceanExport.ServiceInterface.DataObjects.ReportObjects
{
    /// <summary>
    /// 派车国内报表数据对象
    /// </summary>
    [Serializable]
    public class CustomsCNReportData
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
