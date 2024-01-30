using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using System.Runtime.Serialization;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    /// <summary>
    /// 巴西到港通知书报表数据
    /// </summary>
    [Serializable]
    public class ArrivalNoticeReportDataForBrazil
    {
        public string LogoPath { get; set; }
        public string CustomerName { get; set; }

        public string ShipperName { get; set; }

        public string ConsigneeName { get; set; }

        public string HBLNO { get; set; }

        /// <summary>
        /// 箱号
        /// </summary>
        public string CNTRNO { get; set; }

        /// <summary>
        /// 贸易条款
        /// </summary>
        public string INCOTERMS { get; set; }

        public string Volume { get; set; }

        public string POLName { get; set; }

        public string PODName { get; set; }

        public string ETD { get; set; }

        public string ETA { get; set; }

        /// <summary>
        /// 船名
        /// </summary>
        public string VesselName { get; set; }

        /// <summary>
        /// 航次
        /// </summary>
        public string VoyageName { get; set; }
    }    
}


