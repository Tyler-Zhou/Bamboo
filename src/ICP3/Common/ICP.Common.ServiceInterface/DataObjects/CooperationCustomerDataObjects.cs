using System;
using System.Collections.Generic;
using System.Text;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Common.ServiceInterface.DataObjects
{
    #region CRMCustomerList
    /// <summary>
    /// CRMCustomerList
    /// </summary>
    public class CooperationCustomerList : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        ///客户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        ///国家
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        ///揽货人
        /// </summary>
        public string SalesName { get; set; }

        /// <summary>
        ///最近一个月业务量
        /// </summary>
        public string BusinessInfo { get; set; }
        /// <summary>
        /// BusinessList
        /// </summary>
        public List<CompanyDescriptionList> BusinessList { get; set; }
        /// <summary>
        ///鼠标提示按口岸分组提示
        /// </summary>
        public string BusinessDescription
        {
            get
            {
                if (BusinessList == null || BusinessList.Count == 0) return string.Empty;

                StringBuilder bulider = new StringBuilder();
                foreach (var item in BusinessList)
                {
                    if (bulider.Length > 0) bulider.Append("\r\n");

                    bulider.Append(item.CompanyName + " : " + item.Description);
                }

                return bulider.ToString();
            }
        }

        /// <summary>
        ///未付款金额
        /// </summary>
        public string DebtInfo { get; set; }
        /// <summary>
        /// DebtList
        /// </summary>
        public List<CompanyDescriptionList> DebtList { get; set; }
        /// <summary>
        /// 按公司分组的描述
        /// </summary>
        public string DebtDescription
        {
            get
            {
                if (DebtList == null || DebtList.Count == 0) return string.Empty;

                StringBuilder bulider = new StringBuilder();
                foreach (var item in DebtList)
                {
                    if (bulider.Length > 0) bulider.Append("\r\n");

                    bulider.Append(item.CompanyName + " : " + item.Description);
                }

                return bulider.ToString();
            }
        }
    } 
    #endregion

    #region 按公司分组的描述
    /// <summary>
    /// 按公司分组的描述
    /// </summary>
    public class CompanyDescriptionList
    {
        /// <summary>
        /// 公司名
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    } 
    #endregion

    #region 客户档案
    /// <summary>
    /// 客户档案
    /// </summary>
    public class CooperationCustomerArchives : BaseDataObject
    {
        #region ID

        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    base.OnPropertyChanged("ID", value);
                }
            }
        }


        #endregion

        #region 销售信息
        #region Code
        string _Code;
        /// <summary>
        ///Code
        /// </summary>
        public string Code
        {
            get { return _Code; }
            set
            {
                if (_Code != value)
                {
                    _Code = value;
                    this.NotifyPropertyChanged(o => o.Code);
                }
            }
        }
        #endregion

        #region CName
        string _CName;
        /// <summary>
        ///CName
        /// </summary>
        public string CName
        {
            get { return _CName; }
            set
            {
                if (_CName != value)
                {
                    _CName = value;
                    this.NotifyPropertyChanged(o => o.CName);
                }
            }
        }
        #endregion

        #region EName
        string _EName;
        /// <summary>
        ///EName
        /// </summary>
        public string EName
        {
            get { return _EName; }
            set
            {
                if (_EName != value)
                {
                    _EName = value;
                    this.NotifyPropertyChanged(o => o.EName);
                }
            }
        }
        #endregion

        #region CAddress
        string _CAddress;
        /// <summary>
        ///CAddress
        /// </summary>
        public string CAddress
        {
            get { return _CAddress; }
            set
            {
                if (_CAddress != value)
                {
                    _CAddress = value;
                    this.NotifyPropertyChanged(o => o.CAddress);
                }
            }
        }
        #endregion

        #region EAddress
        string _EAddress;
        /// <summary>
        ///EAddress
        /// </summary>
        public string EAddress
        {
            get { return _EAddress; }
            set
            {
                if (_EAddress != value)
                {
                    _EAddress = value;
                    this.NotifyPropertyChanged(o => o.EAddress);
                }
            }
        }
        #endregion

        #region SalesContactName
        string _SalesContactName;
        /// <summary>
        ///SalesContactName
        /// </summary>
        public string SalesContactName
        {
            get { return _SalesContactName; }
            set
            {
                if (_SalesContactName != value)
                {
                    _SalesContactName = value;
                    this.NotifyPropertyChanged(o => o.SalesContactName);
                }
            }
        }
        #endregion

        #region SalesContactAddress
        string _SalesContactAddress;
        /// <summary>
        ///SalesContactAddress
        /// </summary>
        public string SalesContactAddress
        {
            get { return _SalesContactAddress; }
            set
            {
                if (_SalesContactAddress != value)
                {
                    _SalesContactAddress = value;
                    this.NotifyPropertyChanged(o => o.SalesContactAddress);
                }
            }
        }
        #endregion

        #region SalesContactTel
        string _SalesContactTel;
        /// <summary>
        ///SalesContactTel
        /// </summary>
        public string SalesContactTel
        {
            get { return _SalesContactTel; }
            set
            {
                if (_SalesContactTel != value)
                {
                    _SalesContactTel = value;
                    this.NotifyPropertyChanged(o => o.SalesContactTel);
                }
            }
        }
        #endregion

        #region SalesContactFax
        string _SalesContactFax;
        /// <summary>
        ///SalesContactFax
        /// </summary>
        public string SalesContactFax
        {
            get { return _SalesContactFax; }
            set
            {
                if (_SalesContactFax != value)
                {
                    _SalesContactFax = value;
                    this.NotifyPropertyChanged(o => o.SalesContactFax);
                }
            }
        }
        #endregion

        #region SalesContactEmail
        string _SalesContactEmail;
        /// <summary>
        ///SalesContactEmail
        /// </summary>
        public string SalesContactEmail
        {
            get { return _SalesContactEmail; }
            set
            {
                if (_SalesContactEmail != value)
                {
                    _SalesContactEmail = value;
                    this.NotifyPropertyChanged(o => o.SalesContactEmail);
                }
            }
        }
        #endregion

        #region SalesContactRemark
        string _SalesContactRemark;
        /// <summary>
        ///SalesContactRemark
        /// </summary>
        public string SalesContactRemark
        {
            get { return _SalesContactRemark; }
            set
            {
                if (_SalesContactRemark != value)
                {
                    _SalesContactRemark = value;
                    this.NotifyPropertyChanged(o => o.SalesContactRemark);
                }
            }
        }
        #endregion

        #endregion

        #region 操作信息

        #region IsTruck
        bool _IsTruck;
        /// <summary>
        ///IsTruck
        /// </summary>
        public bool IsTruck
        {
            get { return _IsTruck; }
            set
            {
                if (_IsTruck != value)
                {
                    _IsTruck = value;
                    this.NotifyPropertyChanged(o => o.IsTruck);
                }
            }
        }
        #endregion

        #region TruckDemandID
        Guid _TruckDemandID;
        /// <summary>
        ///TruckDemandID
        /// </summary>
        public Guid TruckDemandID
        {
            get { return _TruckDemandID; }
            set
            {
                if (_TruckDemandID != value)
                {
                    _TruckDemandID = value;
                    this.NotifyPropertyChanged(o => o.TruckDemandID);
                }
            }
        }
        #endregion

        #region TruckDemandName
        string _TruckDemandName;
        /// <summary>
        ///TruckDemandName
        /// </summary>
        public string TruckDemandName
        {
            get { return _TruckDemandName; }
            set
            {
                if (_TruckDemandName != value)
                {
                    _TruckDemandName = value;
                    this.NotifyPropertyChanged(o => o.TruckDemandName);
                }
            }
        }
        #endregion

        #region IsWareHouse
        bool _IsWareHouse;
        /// <summary>
        ///IsWareHouse
        /// </summary>
        public bool IsWareHouse
        {
            get { return _IsWareHouse; }
            set
            {
                if (_IsWareHouse != value)
                {
                    _IsWareHouse = value;
                    this.NotifyPropertyChanged(o => o.IsWareHouse);
                }
            }
        }
        #endregion

        #region WareHouseDemandID
        Guid _WareHouseDemandID;
        /// <summary>
        ///WareHouseDemandID
        /// </summary>
        public Guid WareHouseDemandID
        {
            get { return _WareHouseDemandID; }
            set
            {
                if (_WareHouseDemandID != value)
                {
                    _WareHouseDemandID = value;
                    this.NotifyPropertyChanged(o => o.WareHouseDemandID);
                }
            }
        }
        #endregion

        #region WareHouseDemandName
        string _WareHouseDemandName;
        /// <summary>
        ///WareHouseDemandName
        /// </summary>
        public string WareHouseDemandName
        {
            get { return _WareHouseDemandName; }
            set
            {
                if (_WareHouseDemandName != value)
                {
                    _WareHouseDemandName = value;
                    this.NotifyPropertyChanged(o => o.WareHouseDemandName);
                }
            }
        }
        #endregion

        #region IsCustoms
        bool _IsCustoms;
        /// <summary>
        ///IsCustoms
        /// </summary>
        public bool IsCustoms
        {
            get { return _IsCustoms; }
            set
            {
                if (_IsCustoms != value)
                {
                    _IsCustoms = value;
                    this.NotifyPropertyChanged(o => o.IsCustoms);
                }
            }
        }
        #endregion

        #region CustomsDemandID
        Guid _CustomsDemandID;
        /// <summary>
        ///CustomsDemandID
        /// </summary>
        public Guid CustomsDemandID
        {
            get { return _CustomsDemandID; }
            set
            {
                if (_CustomsDemandID != value)
                {
                    _CustomsDemandID = value;
                    this.NotifyPropertyChanged(o => o.CustomsDemandID);
                }
            }
        }
        #endregion

        #region CustomsDemandName
        string _CustomsDemandName;
        /// <summary>
        ///CustomsDemandName
        /// </summary>
        public string CustomsDemandName
        {
            get { return _CustomsDemandName; }
            set
            {
                if (_CustomsDemandName != value)
                {
                    _CustomsDemandName = value;
                    this.NotifyPropertyChanged(o => o.CustomsDemandName);
                }
            }
        }
        #endregion

        #region IsCommodityInspection
        bool _IsCommodityInspection;
        /// <summary>
        ///IsCommodityInspection
        /// </summary>
        public bool IsCommodityInspection
        {
            get { return _IsCommodityInspection; }
            set
            {
                if (_IsCommodityInspection != value)
                {
                    _IsCommodityInspection = value;
                    this.NotifyPropertyChanged(o => o.IsCommodityInspection);
                }
            }
        }
        #endregion

        #region CommodityInspectionDemandID
        Guid _CommodityInspectionDemandID;
        /// <summary>
        ///CommodityInspectionDemandID
        /// </summary>
        public Guid CommodityInspectionDemandID
        {
            get { return _CommodityInspectionDemandID; }
            set
            {
                if (_CommodityInspectionDemandID != value)
                {
                    _CommodityInspectionDemandID = value;
                    this.NotifyPropertyChanged(o => o.CommodityInspectionDemandID);
                }
            }
        }
        #endregion

        #region CommodityInspectionDemandName
        string _CommodityInspectionDemandName;
        /// <summary>
        ///CommodityInspectionDemandName
        /// </summary>
        public string CommodityInspectionDemandName
        {
            get { return _CommodityInspectionDemandName; }
            set
            {
                if (_CommodityInspectionDemandName != value)
                {
                    _CommodityInspectionDemandName = value;
                    this.NotifyPropertyChanged(o => o.CommodityInspectionDemandName);
                }
            }
        }
        #endregion

        #region IsFumigated
        bool _IsFumigated;
        /// <summary>
        ///IsFumigated
        /// </summary>
        public bool IsFumigated
        {
            get { return _IsFumigated; }
            set
            {
                if (_IsFumigated != value)
                {
                    _IsFumigated = value;
                    this.NotifyPropertyChanged(o => o.IsFumigated);
                }
            }
        }
        #endregion

        #region FumigatedDemandID
        Guid _FumigatedDemandID;
        /// <summary>
        ///FumigatedDemandID
        /// </summary>
        public Guid FumigatedDemandID
        {
            get { return _FumigatedDemandID; }
            set
            {
                if (_FumigatedDemandID != value)
                {
                    _FumigatedDemandID = value;
                    this.NotifyPropertyChanged(o => o.FumigatedDemandID);
                }
            }
        }
        #endregion

        #region FumigatedDemandName
        string _FumigatedDemandName;
        /// <summary>
        ///FumigatedDemandName
        /// </summary>
        public string FumigatedDemandName
        {
            get { return _FumigatedDemandName; }
            set
            {
                if (_FumigatedDemandName != value)
                {
                    _FumigatedDemandName = value;
                    this.NotifyPropertyChanged(o => o.FumigatedDemandName);
                }
            }
        }
        #endregion

        #region IsCertificate
        bool _IsCertificate;
        /// <summary>
        ///IsCertificate
        /// </summary>
        public bool IsCertificate
        {
            get { return _IsCertificate; }
            set
            {
                if (_IsCertificate != value)
                {
                    _IsCertificate = value;
                    this.NotifyPropertyChanged(o => o.IsCertificate);
                }
            }
        }
        #endregion

        #region CertificateDemandID
        Guid _CertificateDemandID;
        /// <summary>
        ///CertificateDemandID
        /// </summary>
        public Guid CertificateDemandID
        {
            get { return _CertificateDemandID; }
            set
            {
                if (_CertificateDemandID != value)
                {
                    _CertificateDemandID = value;
                    this.NotifyPropertyChanged(o => o.CertificateDemandID);
                }
            }
        }
        #endregion

        #region CertificateDemandName
        string _CertificateDemandName;
        /// <summary>
        ///CertificateDemandName
        /// </summary>
        public string CertificateDemandName
        {
            get { return _CertificateDemandName; }
            set
            {
                if (_CertificateDemandName != value)
                {
                    _CertificateDemandName = value;
                    this.NotifyPropertyChanged(o => o.CertificateDemandName);
                }
            }
        }
        #endregion

        #region UrgeSI
        bool _UrgeSI;
        /// <summary>
        ///UrgeSI
        /// </summary>
        public bool UrgeSI
        {
            get { return _UrgeSI; }
            set
            {
                if (_UrgeSI != value)
                {
                    _UrgeSI = value;
                    this.NotifyPropertyChanged(o => o.UrgeSI);
                }
            }
        }
        #endregion

        #region SIDemandID
        Guid _SIDemandID;
        /// <summary>
        ///SIDemandID
        /// </summary>
        public Guid SIDemandID
        {
            get { return _SIDemandID; }
            set
            {
                if (_SIDemandID != value)
                {
                    _SIDemandID = value;
                    this.NotifyPropertyChanged(o => o.SIDemandID);
                }
            }
        }
        #endregion

        #region SIDemandName
        string _SIDemandName;
        /// <summary>
        ///SIDemandName
        /// </summary>
        public string SIDemandName
        {
            get { return _SIDemandName; }
            set
            {
                if (_SIDemandName != value)
                {
                    _SIDemandName = value;
                    this.NotifyPropertyChanged(o => o.SIDemandName);
                }
            }
        }
        #endregion

        #region OperateDemand
        string _OperateDemand;
        /// <summary>
        ///OperateDemand
        /// </summary>
        public string OperateDemand
        {
            get { return _OperateDemand; }
            set
            {
                if (_OperateDemand != value)
                {
                    _OperateDemand = value;
                    this.NotifyPropertyChanged(o => o.OperateDemand);
                }
            }
        }
        #endregion

        #endregion

        #region 财务信息

        #region IsSameAsSalesContact
        bool _IsSameAsSalesContact;
        /// <summary>
        ///IsSameAsSalesContact
        /// </summary>
        public bool IsSameAsSalesContact
        {
            get { return _IsSameAsSalesContact; }
            set
            {
                if (_IsSameAsSalesContact != value)
                {
                    _IsSameAsSalesContact = value;
                    this.NotifyPropertyChanged(o => o.IsSameAsSalesContact);
                }
            }
        }
        #endregion

        #region FinanceContactName
        string _FinanceContactName;
        /// <summary>
        ///FinanceContactName
        /// </summary>
        public string FinanceContactName
        {
            get { return _FinanceContactName; }
            set
            {
                if (_FinanceContactName != value)
                {
                    _FinanceContactName = value;
                    this.NotifyPropertyChanged(o => o.FinanceContactName);
                }
            }
        }
        #endregion

        #region FinanceContactAddress
        string _FinanceContactAddress;
        /// <summary>
        ///FinanceContactAddress
        /// </summary>
        public string FinanceContactAddress
        {
            get { return _FinanceContactAddress; }
            set
            {
                if (_FinanceContactAddress != value)
                {
                    _FinanceContactAddress = value;
                    this.NotifyPropertyChanged(o => o.FinanceContactAddress);
                }
            }
        }
        #endregion

        #region FinanceContactTel
        string _FinanceContactTel;
        /// <summary>
        ///FinanceContactTel
        /// </summary>
        public string FinanceContactTel
        {
            get { return _FinanceContactTel; }
            set
            {
                if (_FinanceContactTel != value)
                {
                    _FinanceContactTel = value;
                    this.NotifyPropertyChanged(o => o.FinanceContactTel);
                }
            }
        }
        #endregion

        #region FinanceContactFax
        string _FinanceContactFax;
        /// <summary>
        ///FinanceContactFax
        /// </summary>
        public string FinanceContactFax
        {
            get { return _FinanceContactFax; }
            set
            {
                if (_FinanceContactFax != value)
                {
                    _FinanceContactFax = value;
                    this.NotifyPropertyChanged(o => o.FinanceContactFax);
                }
            }
        }
        #endregion

        #region FinanceContactEmail
        string _FinanceContactEmail;
        /// <summary>
        ///FinanceContactEmail
        /// </summary>
        public string FinanceContactEmail
        {
            get { return _FinanceContactEmail; }
            set
            {
                if (_FinanceContactEmail != value)
                {
                    _FinanceContactEmail = value;
                    this.NotifyPropertyChanged(o => o.FinanceContactEmail);
                }
            }
        }
        #endregion

        #region FinanceContactRemark
        string _FinanceContactRemark;
        /// <summary>
        ///FinanceContactRemark
        /// </summary>
        public string FinanceContactRemark
        {
            get { return _FinanceContactRemark; }
            set
            {
                if (_FinanceContactRemark != value)
                {
                    _FinanceContactRemark = value;
                    this.NotifyPropertyChanged(o => o.FinanceContactRemark);
                }
            }
        }
        #endregion

        #region InvoiceTitel
        string _InvoiceTitel;
        /// <summary>
        ///InvoiceTitel
        /// </summary>
        public string InvoiceTitel
        {
            get { return _InvoiceTitel; }
            set
            {
                if (_InvoiceTitel != value)
                {
                    _InvoiceTitel = value;
                    this.NotifyPropertyChanged(o => o.InvoiceTitel);
                }
            }
        }
        #endregion

        #region InvoiceDemandID
        Guid _InvoiceDemandID;
        /// <summary>
        ///InvoiceDemandID
        /// </summary>
        public Guid InvoiceDemandID
        {
            get { return _InvoiceDemandID; }
            set
            {
                if (_InvoiceDemandID != value)
                {
                    _InvoiceDemandID = value;
                    this.NotifyPropertyChanged(o => o.InvoiceDemandID);
                }
            }
        }
        #endregion

        #region InvoiceDemandName
        string _InvoiceDemandName;
        /// <summary>
        ///InvoiceDemandName
        /// </summary>
        public string InvoiceDemandName
        {
            get { return _InvoiceDemandName; }
            set
            {
                if (_InvoiceDemandName != value)
                {
                    _InvoiceDemandName = value;
                    this.NotifyPropertyChanged(o => o.InvoiceDemandName);
                }
            }
        }
        #endregion

        #region UrgeDebt
        bool _UrgeDebt;
        /// <summary>
        ///UrgeDebt
        /// </summary>
        public bool UrgeDebt
        {
            get { return _UrgeDebt; }
            set
            {
                if (_UrgeDebt != value)
                {
                    _UrgeDebt = value;
                    this.NotifyPropertyChanged(o => o.UrgeDebt);
                }
            }
        }
        #endregion

        #region DebtDemandID
        Guid _DebtDemandID;
        /// <summary>
        ///DebtDemandID
        /// </summary>
        public Guid DebtDemandID
        {
            get { return _DebtDemandID; }
            set
            {
                if (_DebtDemandID != value)
                {
                    _DebtDemandID = value;
                    this.NotifyPropertyChanged(o => o.DebtDemandID);
                }
            }
        }
        #endregion

        #region DebtDemandName
        string _DebtDemandName;
        /// <summary>
        ///DebtDemandName
        /// </summary>
        public string DebtDemandName
        {
            get { return _DebtDemandName; }
            set
            {
                if (_DebtDemandName != value)
                {
                    _DebtDemandName = value;
                    this.NotifyPropertyChanged(o => o.DebtDemandName);
                }
            }
        }
        #endregion

        #region FinanceDemand
        string _FinanceDemand;
        /// <summary>
        ///FinanceDemand
        /// </summary>
        public string FinanceDemand
        {
            get { return _FinanceDemand; }
            set
            {
                if (_FinanceDemand != value)
                {
                    _FinanceDemand = value;
                    this.NotifyPropertyChanged(o => o.FinanceDemand);
                }
            }
        }
        #endregion

        #endregion
    }

     /// <summary>
    /// 客户档案
    /// </summary>
    public class CCPrecautionsList : BaseDataObject
    {
        public Guid ID { get; set; }

        public PrecautionsType Type { get; set; }

        public string Description { get; set; }
    }

    

    #endregion

    #region 合作伙伴列表
    //3、合作伙伴列表：该客户作为销售客户时收发通类型客户列表，列表内容：客户名称、国家、客户类型、揽货人
    /// <summary>
    /// 合作伙伴列表
    /// </summary>
    public class CooperationCustomerPartnerList : BaseDataObject
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PartnerType PartnerType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SalesName { get; set; }
    }
    #endregion

    #region 业务列表

    /// <summary>
    /// 业务列表对象
    /// </summary>
    [Serializable]
    public partial class CCBusinessList : BaseDataObject
    {
        /// <summary>
        /// 判断是否新
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        /// <summary>
        /// 选择
        /// </summary>
        public bool Selected { get; set; }

        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }

        string _salesname;
        /// <summary>
        /// 揽货人
        /// </summary>
        public string SalesName
        {
            get
            {
                return _salesname;
            }
            set
            {
                if (_salesname != value)
                {
                    _salesname = value;
                    this.NotifyPropertyChanged(o => o.SalesName);
                }
            }
        }

        string _StateDescription;
        /// <summary>
        /// 状态描述
        /// </summary>
        public string StateDescription
        {
            get { return _StateDescription; }
            set
            {
                if (_StateDescription != value)
                {
                    _StateDescription = value;
                    this.NotifyPropertyChanged(o => o.StateDescription);
                }
            }
        }

        string _CompanyName;
        /// <summary>
        /// 公司
        /// </summary>
        public string CompanyName
        {
            get { return _CompanyName; }
            set
            {
                if (_CompanyName != value)
                {
                    _CompanyName = value;
                    this.NotifyPropertyChanged(o => o.CompanyName);
                }
            }
        }


        Guid _CompanyId;
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID
        {
            get { return _CompanyId; }
            set
            {
                if (_CompanyId != value)
                {
                    _CompanyId = value;
                    this.NotifyPropertyChanged(o => o.CompanyID);
                }
            }
        }


        Guid? _AgentID;
        /// <summary>
        /// 代理ID
        /// </summary>
        public Guid? AgentID
        {
            get { return _AgentID; }
            set
            {
                if (_AgentID != value)
                {
                    _AgentID = value;
                    this.NotifyPropertyChanged(o => o.AgentID);
                }
            }
        }

        Guid _SolutionID;
        /// <summary>
        /// 公司对应的解决方案ID
        /// </summary>
        public Guid SolutionID
        {
            get { return _SolutionID; }
            set
            {
                if (_SolutionID != value)
                {
                    _SolutionID = value;
                    this.NotifyPropertyChanged(o => o.SolutionID);
                }
            }
        }

        string _operationNo;
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNO
        {
            get { return _operationNo; }
            set
            {
                if (_operationNo != value)
                {
                    _operationNo = value;
                    this.NotifyPropertyChanged(o => o.OperationNO);
                }
            }
        }

        OperationType _OperationType;
        /// <summary>
        /// OperationType
        /// </summary>
        public OperationType OperationType
        {
            get { return _OperationType; }
            set
            {
                if (_OperationType != value)
                {
                    _OperationType = value;
                    this.NotifyPropertyChanged(o => o.OperationType);
                }
            }
        }

        string _CustomerName;
        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerName
        {
            get { return _CustomerName; }
            set
            {
                if (_CustomerName != value)
                {
                    _CustomerName = value;
                    this.NotifyPropertyChanged(o => o.CustomerName);
                }
            }
        }


        Guid _CustomerID;
        /// <summary>
        /// 客户
        /// </summary>
        public Guid CustomerID
        {
            get { return _CustomerID; }
            set
            {
                if (_CustomerID != value)
                {
                    _CustomerID = value;
                    this.NotifyPropertyChanged(o => o.CustomerID);
                }
            }
        }

        string _GoodsNmae;
        /// <summary>
        /// 品名
        /// </summary>
        public string GoodsNmae
        {
            get { return _GoodsNmae; }
            set
            {
                if (_GoodsNmae != value)
                {
                    _GoodsNmae = value;
                    this.NotifyPropertyChanged(o => o.GoodsNmae);
                }
            }
        }



        string _RefNO;
        /// <summary>
        /// 参考号
        /// </summary>
        public string RefNO
        {
            get { return _RefNO; }
            set
            {
                if (_RefNO != value)
                {
                    _RefNO = value;
                    this.NotifyPropertyChanged(o => o.RefNO);
                }
            }
        }

        string _OperationDescription;
        /// <summary>
        /// 业务描述
        /// </summary>
        public string OperationDescription
        {
            get { return _OperationDescription; }
            set
            {
                if (_OperationDescription != value)
                {
                    _OperationDescription = value;
                    this.NotifyPropertyChanged(o => o.OperationDescription);
                }
            }
        }

        decimal _DR;
        /// <summary>
        /// 应收
        /// </summary>
        public decimal DR
        {
            get
            {
                return _DR;
            }
            set
            {
                if (_DR != value)
                {
                    _DR = value;
                    this.NotifyPropertyChanged(o => o.DR);
                }
            }
        }

        decimal _CR;
        /// <summary>
        /// 应付
        /// </summary>
        public decimal CR
        {
            get
            {
                return _CR;
            }
            set
            {
                if (_CR != value)
                {
                    _CR = value;
                    this.NotifyPropertyChanged(o => o.CR);
                }
            }
        }

        decimal _Profit;
        /// <summary>
        /// 利润
        /// </summary>
        public decimal Profit
        {
            get
            {
                return _Profit;
            }
            set
            {
                if (_Profit != value)
                {
                    _Profit = value;
                    this.NotifyPropertyChanged(o => o.Profit);
                }
            }
        }


        string _DRDescription;
        /// <summary>
        /// 应收文本
        /// </summary>
        public string DRDescription
        {
            get
            {
                return _DRDescription;
            }
            set
            {
                if (_DRDescription != value)
                {
                    _DRDescription = value;
                    this.NotifyPropertyChanged(o => o.DRDescription);
                }
            }
        }

        string _CRDescription;
        /// <summary>
        /// 应付文本
        /// </summary>
        public string CRDescription
        {
            get
            {
                return _CRDescription;
            }
            set
            {
                if (_CRDescription != value)
                {
                    _CRDescription = value;
                    this.NotifyPropertyChanged(o => o.CRDescription);
                }
            }
        }

        string _ProfitDescription;
        /// <summary>
        /// 利润
        /// </summary>
        public string ProfitDescription
        {
            get
            {
                return _ProfitDescription;
            }
            set
            {
                if (_ProfitDescription != value)
                {
                    _ProfitDescription = value;
                    this.NotifyPropertyChanged(o => o.ProfitDescription);
                }
            }
        }

        Guid _DefaultCurrencyID;
        /// <summary>
        /// 默认币种ID
        /// </summary>
        public Guid DefaultCurrencyID
        {
            get
            {
                return _DefaultCurrencyID;
            }
            set
            {
                if (_DefaultCurrencyID != value)
                {
                    _DefaultCurrencyID = value;
                    this.NotifyPropertyChanged(o => o.DefaultCurrencyID);
                }
            }
        }

        string _DefaultCurrencyName;
        /// <summary>
        /// 默认币种
        /// </summary>
        public string DefaultCurrencyName
        {
            get
            {
                return _DefaultCurrencyName;
            }
            set
            {
                if (_DefaultCurrencyName != value)
                {
                    _DefaultCurrencyName = value;
                    this.NotifyPropertyChanged(o => o.DefaultCurrencyName);
                }
            }
        }
    }

    #endregion
}
