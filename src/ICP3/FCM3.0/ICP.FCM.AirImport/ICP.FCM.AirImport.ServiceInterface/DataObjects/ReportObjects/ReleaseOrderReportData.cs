using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using System.Runtime.Serialization;

namespace ICP.FCM.AirImport.ServiceInterface
{
    /// <summary>
    /// ReleaseOrderReportData详细对象
    /// </summary>
    [Serializable]
    public class ReleaseOrderReportData
    {
        /// <summary>
        /// 
        /// </summary>
        public ReleaseOrderData ReleaseOrderData { get; set; }
    }

     /// <summary>
    /// ReleaseOrderData详细对象
    /// </summary>
    [Serializable]
    [KnownType(typeof(DryCargo))]
    [KnownType(typeof(ReeferCargo))]
    [KnownType(typeof(DangerousCargo))]
    [KnownType(typeof(AwkwardCargo))]
    public class ReleaseOrderData
    {
        public string CurrentUser { get; set; }
        public string CurrentDate { get; set; }
        public string CompanyTelFax { get; set; }
        public string CompanyAddress { get; set; }      
        public string CompanyName { get; set; }
        public string SpecialInstruction { get; set; }

        /// <summary>
        /// 提货地描述（包括名称，地址，电话，传真）
        /// </summary>
        public string FinalWareHouseDescription { get; set; }

        /// <summary>
        /// 收货人描述（包括名称，地址，电话，传真）
        /// </summary>
        public string ConsigneeDescription { get; set; }

        /// <summary>
        /// CustomerRefNo
        /// </summary>
        public string CustomerRefNo { get; set; }

        /// <summary>
        /// MasterBLNo
        /// </summary>
        public string MasterBLNo { get; set; }

        /// <summary>
        /// SubBLNo
        /// </summary>
        public string SubBLNo { get; set; }

        /// <summary>
        /// AMSHouseBLNo
        /// </summary>
        public string AMSHouseBLNo { get; set; }

        /// <summary>
        /// ETA
        /// </summary>
        public string ETA { get; set; }

        /// <summary>
        /// ITNo
        /// </summary>
        public string ITNo { get; set; }

        ///// <summary>
        ///// 唛头
        ///// </summary>
        //public string Marks { get; set; }

        /// <summary>
        /// 包装件数和单位
        /// </summary>
        public string NoOfPackages { get; set; }

        public CargoDescription CargoDescription { get; set; }

        /// <summary>
        /// GoodsDescription
        /// </summary>
        public string GoodsDescription
        {
            get
            {
                if (this.CargoDescription != null && this.CargoDescription.Cargo != null)
                {
                    return this.CargoDescription.Cargo.ToString(LocalData.IsEnglish);
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// LoadPortName
        /// </summary>
        public string LoadPortName { get; set; }

        /// <summary>
        /// ETD
        /// </summary>
        public string ETD { get; set; }

        /// <summary>
        /// HouseBLNo
        /// </summary>
        public string HouseBLNo { get; set; }

        /// <summary>
        /// HouseBLId
        /// </summary>
        public Guid HouseBLId { get; set; }

        /// <summary>
        /// FlightNo
        /// </summary>
        public string FlightNo { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }
    }
}
