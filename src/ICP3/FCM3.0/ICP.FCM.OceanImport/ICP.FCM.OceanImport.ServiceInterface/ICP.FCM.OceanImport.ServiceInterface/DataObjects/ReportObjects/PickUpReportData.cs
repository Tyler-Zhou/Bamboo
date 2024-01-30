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
    /// 提货通知书报表打印数据对象
    /// </summary>
    public class PickUpReportData
    {
        public string CompanyAddress { get; set; }

        public string CompanyName { get; set; }

        public string CompanyTelFax { get; set; }

        public string CurrentDate { get; set; }

        public string TruckerDescription { get; set; }

        public string PickUpAtDescription { get; set; }

        public string DeliveryAtDescription { get; set; }

        public string BillToDescription { get; set; }

        public string CurrentUserEmail { get; set; }

        public string Commodity { get; set; }

        public string CntrRetrun { get; set; }

        public string PickupRefrenceNo { get; set; }

        public string DeliveryRefrenceNo { get; set; }

        public string PickupDate { get; set; }

        public string DeliveryDate { get; set; }

        public string BillingReferenceNo { get; set; }

        public string Carrier { get; set; }

        public string MasterBLNo { get; set; }

        public string VesselVoyageNo { get; set; }

        public string HouseBLNo { get; set; }

        public string AMSHouseBLNo { get; set; }

        public string POD { get; set; }

        public string PlaceOfDelivery { get; set; }

        public string ETA { get; set; }

        public string DETA { get; set; }

        public string ITNo { get; set; }

        public string ContainerNOs { get; set; }

        public string Marks { get; set; }

        public string GoodsDescription { get; set; }

        public string PKGS { get; set; }

        public string Weight { get; set; }

        public string Measurement { get; set; }

        public string SPECIALINSTRUCTION { get; set; }
    }
}


