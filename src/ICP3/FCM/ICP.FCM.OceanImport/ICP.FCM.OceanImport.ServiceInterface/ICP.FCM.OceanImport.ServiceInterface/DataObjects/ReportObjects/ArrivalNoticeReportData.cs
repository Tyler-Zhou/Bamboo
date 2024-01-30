using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using System.Runtime.Serialization;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    /// <summary>
    /// ContainerInfoReportData详细对象
    /// </summary>
    [Serializable]
    public class ArrivalNoticeReportData
    {
        public ArrivalNoticeData ArrivalNoticeData { get; set; }

        public List<ArrivalNoticeFee> ArrivalNoticeFees { get; set; }

        public List<ArrivalNoticeFeeAmount> ArrivalNoticeFeeAmounts { get; set; }

        public List<ContainerInfoReportData> ContainerList { get; set; }
    }

    /// <summary>
    /// ContainerInfoReportData详细对象
    /// </summary>
    [Serializable]
    [KnownType(typeof(DryCargo))]
    [KnownType(typeof(ReeferCargo))]
    [KnownType(typeof(DangerousCargo))]
    [KnownType(typeof(AwkwardCargo))]
    public class ArrivalNoticeData
    {
        public string CurrentDate { get; set; }

        /// <summary>
        /// 参数,不需要数据库提供
        /// </summary>
        public string Title { get; set; }     

        /// <summary>
        /// 参数,不需要数据库提供
        /// </summary>
        public string Show { get; set; }

        /// <summary>
        /// 参数,不需要数据库提供
        /// </summary>
        public string ShowNoRows { get; set; }

        /// <summary>
        /// 参数,不需要数据库提供
        /// </summary>
        public string ShowFeeAttachement { get; set; }

        /// <summary>
        /// 参数,不需要数据库提供
        /// </summary>
        public string SeeAttachementFee { get; set; }

        /// <summary>
        /// 参数,不需要数据库提供
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// 参数,不需要数据库提供
        /// </summary>
        public string SumAmount { get; set; }

        /// <summary>
        /// 参数,不需要数据库提供
        /// </summary>
        public string IsEnghish { get; set; }

        public string companyAddress { get; set; }

        public string companyName { get; set; }

        public string CompanyTelFax { get; set; }

        public string PRemark { get; set; }

        /// <summary>
        /// ShipperDescription
        /// </summary>
        public string ShipperDescription { get; set; }

        /// <summary>
        /// ConsigneeDescription
        /// </summary>
        public string ConsigneeDescription { get; set; }

        /// <summary>
        /// NotifyPartyDescription
        /// </summary>
        public string NotifyPartyDescription { get; set; }

        /// <summary>
        /// CustomerBrokerDescription
        /// </summary>
        public string CustomerBrokerDescription { get; set; }

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
        /// HouseBLNo
        /// </summary>
        public string HouseBLNo { get; set; }

        /// <summary>
        /// ReferenceNO
        /// </summary>
        public string ReferenceNO { get; set; }

        /// <summary>
        /// 收货地名称
        /// </summary>
        public string ReceiptPlaceName { get; set; }

        /// <summary>
        /// LoadPortName
        /// </summary>
        public string LoadPortName { get; set; }

        /// <summary>
        /// ETD
        /// </summary>
        public string ETD { get; set; }

        /// <summary>
        /// DiscPortName
        /// </summary>
        public string DiscPortName { get; set; }

        /// <summary>
        /// ETA
        /// </summary>
        public string ETA { get; set; }

        /// <summary>
        /// ETA
        /// </summary>
        public string GateInDate { get; set; }

        /// <summary>
        /// PlaceOfDeliveryName
        /// </summary>
        public string PlaceOfDeliveryName { get; set; }

        /// <summary>
        /// PETA
        /// </summary>
        public string PETA { get; set; }

        /// <summary>
        /// DestinationPortName
        /// </summary>
        public string DestinationPortName { get; set; }

        /// <summary>
        /// FETA
        /// </summary>
        public string FETA { get; set; }

        /// <summary>
        /// VesselVoyageNo
        /// </summary>
        public string VesselVoyageNo { get; set; }

        /// <summary>
        /// LastFreeDate
        /// </summary>
        public string LastFreeDate { get; set; }

        /// <summary>
        /// GODate
        /// </summary>
        public string GODate { get; set; }

        /// <summary>
        /// CNTRReturnName
        /// </summary>
        public string CNTRReturnName { get; set; }

        /// <summary>
        /// ITNo
        /// </summary>
        public string ITNo { get; set; }

        /// <summary>
        /// ITDate
        /// </summary>
        public string ITDate { get; set; }

        /// <summary>
        /// ITPlace
        /// </summary>
        public string ITPlace { get; set; }

        /// <summary>
        /// Marks
        /// </summary>
        public string Marks { get; set; }

        /// <summary>
        /// NoOfPackages
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
        /// GrossWeight
        /// </summary>
        public string GrossWeight { get; set; }

        /// <summary>
        /// Measurement
        /// </summary>
        public string Measurement { get; set; }

        /// <summary>
        /// DeliveryTermName
        /// </summary>
        public string DeliveryTermName { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// CYLocationDescription
        /// </summary>
        public string CYLocationDescription { get; set; }

        /// <summary>
        /// PreparedName
        /// </summary>
        public string PreparedName
        {
            get
            {
                string result = string.Empty;
                result = string.IsNullOrEmpty(Prepared) ? string.Empty : (Prepared + (string.IsNullOrEmpty(PreparedEmail) ? string.Empty : "\r\n" + PreparedEmail));
                return result;
            }
        }

        /// <summary>
        /// Prepared
        /// </summary>
        public string Prepared { get; set; }

        /// <summary>
        /// PreparedEmail
        /// </summary>
        public string PreparedEmail { get; set; }

        /// <summary>
        /// LeadSealing
        /// </summary>
        public string LeadSealing { get; set; }

        /// <summary>
        /// Other Info(可能不需要)
        /// </summary>
        public string POInfo { get; set; }

        /// <summary>
        /// TelexNo(电放号)
        /// </summary>
        public string TelexNo { get; set; }

        /// <summary>
        /// ExpressRelease
        /// </summary>
        public string ExpressRelease
        {
            get
            {
                string reslut = string.Empty;
                switch (ReleaseType)
                {
                    case FCMReleaseType.Telex:
                        reslut = string.IsNullOrEmpty(TelexNo) ? "Original B/L Required" : "EXPRESS RELEASE";
                        break;
                    case FCMReleaseType.Original:
                        reslut = OBLReceived ? "Original B/L Received" : "Original B/L Required";
                        break;
                }
                return reslut;
            }
        }

        /// <summary>
        /// 是否电放
        /// </summary>
        public FCMReleaseType ReleaseType { get; set; }

        /// <summary>
        /// 是否提单已收
        /// </summary>
        public bool OBLReceived { get; set; }

        /// <summary>
        /// ContainerInfo
        /// </summary>
        public string ContainerInfo { get; set; }

        /// <summary>
        /// ScheduledFlight
        /// </summary>
        public string ScheduledFlight { get; set; }

        /// <summary>
        /// ReleaseOrderRequired
        /// </summary>
        public string ReleaseOrderRequired { get; set; }
    }

    /// <summary>
    /// ArrivalNoticeFee详细对象
    /// </summary>
    [Serializable]
    public class ArrivalNoticeFee
    {
        /// <summary>
        /// 费用描述
        /// </summary>
        public string ChargeItemDescription { get; set; }

        /// <summary>
        /// 应付
        /// </summary>
        public decimal? PAmount { get; set; }

        /// <summary>
        /// BillNo
        /// </summary>
        public string BillNo { get; set; }

        /// <summary>
        /// 应收
        /// </summary>
        public decimal? CAmount { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string EName { get; set; }

    }

    /// <summary>
    /// ArrivalNoticeFee详细对象
    /// </summary>
    [Serializable]
    public class ArrivalNoticeFeeAmount
    {
        /// <summary>
        /// LabelText
        /// </summary>
        public string LabelText { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public decimal? Amount
        {
            get
            {
                if (PAmount == null && CAmount == null)
                {
                    return null;
                }
                else if (PAmount == null)
                {
                    return CAmount;
                }
                else if (CAmount == null)
                {
                    return -PAmount;
                }
                else if (CAmount > PAmount)
                {
                    return CAmount - PAmount;
                }
                else if (CAmount < PAmount)
                {
                    return -(PAmount - CAmount);
                }

                return null;
            }
            set
            {
            }
        }

        /// <summary>
        /// 应付
        /// </summary>
        public decimal? PAmount { get; set; }

        /// <summary>
        /// 应收
        /// </summary>
        public decimal? CAmount { get; set; }

        /// <summary>
        /// EName
        /// </summary>
        public string EName { get; set; }
    }
}


