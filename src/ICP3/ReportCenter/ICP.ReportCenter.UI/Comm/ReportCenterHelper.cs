using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.ReportCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using System.Windows.Forms;
using ICP.ReportCenter.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Server;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI
{
    public class ReportCenterHelper : Controller,IDisposable
    {
        #region Services
        public IReportCenterService ReportCenterService
        {
            get
            {
                return ServiceClient.GetService<IReportCenterService>();
            }
        }

        public ICP.Sys.ServiceInterface.IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<ICP.Sys.ServiceInterface.IOrganizationService>();
            }
        }

        public ICP.Common.ServiceInterface.IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<ICP.Common.ServiceInterface.IConfigureService>();
            }
        }

        public ICP.Common.ServiceInterface.ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICP.Common.ServiceInterface.ICustomerService>();
            }
        }

        public ICP.Common.ServiceInterface.ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ICP.Common.ServiceInterface.ITransportFoundationService>();
            }
        }


        #endregion

        #region 本地属性

        #region 服务器报表的登录信息
        private ReportServerInfo _ReportServerInfo = null;
        public ReportServerInfo ReportServerInfo
        {
            get
            {
                if (_ReportServerInfo == null) _ReportServerInfo = ReportCenterService.GetReportServerUrl();

                return _ReportServerInfo;
            }
        }
        #endregion

        #region 报表的业务类型
        private List<ReportOperationType> _ReportOperationTypes = null;
        public List<ReportOperationType> ReportOperationTypes
        {
            get
            {
                if (_ReportOperationTypes == null) _ReportOperationTypes = ReportCenterService.GetReportOperationType(false);

                return _ReportOperationTypes;
            }
        }
        #endregion

        #region 报表有箱的业务类型
        private List<ReportOperationType> _ReportCtnOperationTypes = null;
        public List<ReportOperationType> ReportCtnOperationTypes
        {
            get
            {
                if (_ReportCtnOperationTypes == null) _ReportCtnOperationTypes = ReportCenterService.GetReportOperationType(true);

                return _ReportCtnOperationTypes;
            }
        }
        #endregion

        #region 报表分组类型
        private List<ReportGroupType> _ReportGroupTypes = null;
        public List<ReportGroupType> ReportGroupTypes
        {
            get
            {
                if (_ReportGroupTypes == null) _ReportGroupTypes = ReportCenterService.GetReportGroupType();

                return _ReportGroupTypes;
            }
        }
        #endregion

        #region 部门信息
        private List<OrganizationList> _GetUserOrganizationList = null;
        /// <summary>
        /// 当前用户的组织结构列表
        /// </summary>
        public List<OrganizationList> GetUserOrganizationList
        {
            get
            {
                if (_GetUserOrganizationList == null) 
                {
                    _GetUserOrganizationList = ReportCenterService.GetOrganizationListForReport(LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                }
                OrganizationList tager = _GetUserOrganizationList.Find(o => o.ParentID == null);
                if (tager != null)
                {
                    _GetUserOrganizationList.Remove(tager);
                }
                return _GetUserOrganizationList;
            }
        }

        private List<OrganizationList> _CrmOrganizationList = null;
        public List<OrganizationList> CrmOrganizationList
        {
            get
            {
                if (_CrmOrganizationList == null)
                {
                    _CrmOrganizationList = ReportCenterService.GetOrganizationListForCRMReport(LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                }
                OrganizationList tager = _CrmOrganizationList.Find(o => o.ParentID == null);
                if (tager != null)
                {
                    _CrmOrganizationList.Remove(tager);
                }
                return _CrmOrganizationList;
            }
        }

        private List<OrganizationList> _UserCompanyList = null;
        /// <summary>
        /// 当前用户的公司列表
        /// </summary>
        public List<OrganizationList> UserCompanyList
        {
            get
            {
                if (_UserCompanyList == null)
                {
                    _UserCompanyList =
                    (from d in LocalData.UserInfo.UserOrganizationList
                     where d.Type != LocalOrganizationType.Department
                     select new OrganizationList
                     {
                         ID = d.ID,
                         Code = d.Code,
                         CShortName = d.CShortName,
                         EShortName = d.EShortName,
                         FullName = d.FullName,
                         ParentID = d.ParentID,
                         Type = (OrganizationType)d.Type
                     }).ToList();
                }

                return _UserCompanyList;
            }
        }


        private List<OrganizationList> _GetOrganizationList = null;
        /// <summary>
        /// 获得所有的组织结构
        /// </summary>
        public List<OrganizationList> GetOrganizationList
        {
            get
            {
                if (_GetOrganizationList == null)
                {
                    _GetOrganizationList = OrganizationService.GetOrganizationList(string.Empty, string.Empty, true, int.MaxValue);
                }
                OrganizationList tager = _GetOrganizationList.Find(o => o.ParentID == null);
                if (tager != null)
                {
                    _GetOrganizationList.Remove(tager);
                }
                return _GetOrganizationList;
            }
        }


        private List<OrganizationList> _OfficeList = null;
        /// <summary>
        /// 获得所有的公司列表
        /// </summary>
        public List<OrganizationList> AllOfficeList
        {
            get
            {
                if (_GetOrganizationList == null)
                {
                    _GetOrganizationList = _GetOrganizationList = OrganizationService.GetOrganizationList(string.Empty, string.Empty, true, int.MaxValue);
                    _GetOrganizationList = (from d in _GetOrganizationList where d.Type != OrganizationType.Department select d).ToList();
                }
                OrganizationList tager = _GetOrganizationList.Find(o => o.ParentID == null);
                if (tager != null)
                {
                    _GetOrganizationList.Remove(tager);
                }
                return _GetOrganizationList;
            }
        }


        private List<ShippingLineList> _ShipLines = null;
        /// <summary>
        /// 获得航线列表
        /// </summary>
        public List<ShippingLineList> ShipLines
        {
            get
            {
                if (_ShipLines == null) _ShipLines = TransportFoundationService.GetShippingLineList(string.Empty, string.Empty, true,null, 0);

                return _ShipLines;
            }
        }

        private CustomerInfo _DefaultCompany = null;
        /// <summary>
        /// 默认公司的信息
        /// </summary>
        public CustomerInfo DefaultCompany
        {
            get
            {
                if (_DefaultCompany == null)
                {
                    ConfigureInfo configureInfo= ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                    _DefaultCompany =CustomerService.GetCustomerInfo(configureInfo.CustomerID);
                }

                return _DefaultCompany;
            }
        }
        /// <summary>
        /// 默认公司的信息
        /// </summary>
        public string DefaultCompanyTelAndFax
        {
            get
            {
                StringBuilder strBuilder = new StringBuilder();
                if(DefaultCompany.Tel1.IsNullOrEmpty()==false)
                    strBuilder.Append("Tel:"+DefaultCompany.Tel1);

                if(DefaultCompany.Fax.IsNullOrEmpty()==false)
                {
                    if(strBuilder.Length >0)strBuilder.Append(" ");

                    strBuilder.Append("Fax:"+DefaultCompany.Fax);
                }

                return strBuilder.ToString();
            }
        }

        #endregion

        #region 部门信息



        #endregion

        #region CostItem
        private CostItemData _TopCostItem=null;
        public CostItemData TopCostItem
        {
            get
            {
                if (_TopCostItem == null)
                {
                    _TopCostItem = new CostItemData();
                    _TopCostItem.ID =Guid.NewGuid();
                    _TopCostItem.ParentID = null;
                    _TopCostItem.CName = _TopCostItem.FullName = "全部";
                    _TopCostItem.EName = _TopCostItem.EFullName = "ALL";
                }
                return _TopCostItem;
            }
        }

        private List<CostItemData> _CostItemDatas = null;
        public List<CostItemData> CostItemDatas
        {
            get
            {
                if (_CostItemDatas == null)
                {
                    _CostItemDatas = TransportFoundationService.GetAllCostItems();
                    List<CostItemData> tagers = _CostItemDatas.FindAll(c => c.ParentID.IsNullOrEmpty());
                    if (tagers != null && tagers.Count > 0)
                    {
                        foreach (var item in tagers)
                        {
                            item.ParentID = TopCostItem.ID;
                        }
                    }
                    _CostItemDatas.Add(TopCostItem);
                }

                return _CostItemDatas;
            }
        }

        #endregion

        #region 对帐单中的附件类型

        bool? localStatementIsRelax = null;
        /// <summary>
        /// 对帐单中的附件类型
        /// </summary>
        public bool LocalStatementIsRelax
        {
            get
            {
                if (localStatementIsRelax == null)
                {
                    CompanyReportConfigureList reportConfigure = ConfigureService.GetReportConfigureList(LocalData.UserInfo.DefaultCompanyID, ReportConfigConstants.ReportCenterConfig);
                    if (reportConfigure == null) { localStatementIsRelax = false; return localStatementIsRelax.Value; }

                    ReportParameterList reportParameterList = reportConfigure.Parameters.Find(p => p.Code == ReportConfigConstants.LocalStatementAttachType);
                    if (reportParameterList == null) { localStatementIsRelax = false; return localStatementIsRelax.Value; }

                    if (reportParameterList.ParameterValue == "0") localStatementIsRelax = false;
                    else localStatementIsRelax = true;
                }
                return localStatementIsRelax.Value;
            }
        }

        #endregion

        #region 用户的默认公司对应的客户
        CustomerInfo userCompanyInfo = null;
        /// <summary>
        /// 用户的默认公司对应的客户
        /// </summary>
        public CustomerInfo UserCompanyInfo
        {
            get
            {
                if (userCompanyInfo == null)
                {
                    ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                    userCompanyInfo = CustomerService.GetCustomerInfo(configure.CustomerID);
                }
                return userCompanyInfo;
            }
        }

        public CustomerInfo GetCompanyInfo(Guid CompanyID )
        {
            ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(CompanyID);
            return CustomerService.GetCustomerInfo(configure.CustomerID);
        }

        #endregion

        #endregion

        #region UIHelper

        #region  ReportTypeAndGroups

        /// <summary>
        /// 生成报表类型和分组方式
        /// </summary>
        public void BulidReportTypeAndGroups(Comm.Controls.ReportOperationTypePart reportOperationTypePart1
                        , Comm.Controls.CheckBoxComboBox chkcmbGroupBy
                        ,bool isContainerOnly
                        ,bool checkTopOperation)
        {
            List<ReportOperationType> reportOperationTypes = null;
            if (isContainerOnly)
                reportOperationTypes = this.ReportCtnOperationTypes;
            else
                reportOperationTypes = this.ReportOperationTypes;

                reportOperationTypePart1.SetSource(reportOperationTypes);
                if (checkTopOperation)
                    reportOperationTypePart1.CheckAll();

            if (chkcmbGroupBy == null) return;
            List<ReportGroupType> groupTypes = this.ReportGroupTypes;
            foreach (var item in groupTypes)
            {
                chkcmbGroupBy.AddItem(item.ID, LocalData.IsEnglish ? item.EName : item.CName);
            }

            reportOperationTypePart1.EditValueChanged += delegate
            {
                string[] strs = reportOperationTypePart1.EditValue.Split(new Char[] { ',' });
                List<Guid> checkId = new List<Guid>();
                List<object> chkValues = chkcmbGroupBy.EditValue;
                if (chkValues != null)
                {
                    foreach (var item in chkValues) checkId.Add(new Guid(item.ToString()));
                }

                chkcmbGroupBy.ClearItems();

                if (strs == null || strs.Length == 0)
                {
                    foreach (var item in groupTypes)
                    {
                        bool isChekc = false;
                        if (checkId != null && checkId.Contains(item.ID)) isChekc = true;

                        chkcmbGroupBy.AddItem(item.ID, LocalData.IsEnglish ? item.EName : item.CName, isChekc);
                    }
                }
                else
                {
                    List<Guid> containsTypeIds = new List<Guid>();
                    List<ReportGroupType> tagers = new List<ReportGroupType>();
                    foreach (var item in groupTypes)
                    {
                        bool isContain = false;
                        foreach (var unTypes in item.UnContainsTypes)
                        {
                            if (strs.Contains(unTypes)) { isContain = true; break; }
                        }
                        if (isContain == false) tagers.Add(item);

                    }

                    chkcmbGroupBy.ClearItems();
                    foreach (var item in tagers)
                    {
                        bool isChekc = false;
                        if (checkId != null && checkId.Contains(item.ID)) isChekc = true;

                        chkcmbGroupBy.AddItem(item.ID, LocalData.IsEnglish ? item.EName : item.CName, isChekc);
                    }
                }
                chkcmbGroupBy.RefreshText();
            };

        }

        /// <summary>
        /// 生成报表类型和分组方式
        /// </summary>
        public void BulidReportTypeAndGroups2(Comm.Controls.ReportOperationTypePart reportOperationTypePart1
                        , Comm.Controls.CheckBoxComboBox chkcmbGroupBy)
        {
            List<ReportOperationType> reportOperationTypes = ReportCtnOperationTypes.Where(fitem => ("72E37F59-A31B-4AB7-9F7C-76247F77D1F8&&0616DEEB-CCBF-4C83-BAAB-043DEACE76F6&&2B16F9F0-F10B-4BA3-97D4-E7BC44D15DD8&&EFF13648-3548-43FF-B33B-4961E2078116").Contains(("" + fitem.ID).ToUpper())).ToList();
            if (reportOperationTypes.Count>0)
            {
                reportOperationTypePart1.SetSource(reportOperationTypes);
                reportOperationTypePart1.CheckAll();
            }

            if (chkcmbGroupBy == null) return;
            List<ReportGroupType> groupTypes = this.ReportGroupTypes.Where(fitem => ("2F86EA70-C50A-4B63-A730-7EF9FB06309D&&F3BFA67D-96AA-43A5-A6B9-35CEDAB4D1CC").Contains(("" + fitem.ID).ToUpper())).ToList();
            foreach (var item in groupTypes)
            {
                chkcmbGroupBy.AddItem(item.ID, LocalData.IsEnglish ? item.EName : item.CName);
            }
        }


        public void BuidGroups_ExportContainerList(Comm.Controls.CheckBoxComboBox chkcmbGroupBy)
        {
            List<EnumHelper.ListItem<ECLGroupBy>> orderByOPType = EnumHelper.GetEnumValues<ECLGroupBy>(LocalData.IsEnglish);
            chkcmbGroupBy.ClearItems();
            foreach (var item in orderByOPType)
            {
                chkcmbGroupBy.AddItem(item.Value, item.Name, false);
            }
            chkcmbGroupBy.Items[0].CheckState = CheckState.Checked;
            chkcmbGroupBy.RefreshText();
        }

        #endregion

        #region ShipLines
        /// <summary>
        /// BuildShipLines
        /// </summary>
        /// <param name="chkcmbShipLine"></param>
        public void BuildShipLines(Comm.Controls.CheckBoxComboBox chkcmbShipLine)
        {
            chkcmbShipLine.ClearItems();
            foreach (var item in ShipLines)
            {
                chkcmbShipLine.AddItem(item.ID, LocalData.IsEnglish ? item.EName : item.CName);
            }
        }


        /// <summary>
        /// 绑定航线下拉勾选框
        /// </summary>
        /// <param name="chkcmbShipLine"></param>
        public void BuildShipLines(ICP.Framework.ClientComponents.Controls.TreeCheckControl chkcmbShipLine)
        {
            List<TreeCheckControlSource> tss = new List<TreeCheckControlSource>();
            foreach (var item in ShipLines)
            {
                tss.Add(new TreeCheckControlSource { ID = item.ID, ParentID = item.ParentID, Name = LocalData.IsEnglish ? item.EName : item.CName });
            }
            chkcmbShipLine.SetSource(tss);
        }

        #endregion

        #endregion

        #region UIDateHelper

        /// <summary>
        /// 通过业务类型获取JobType
        /// </summary>
        /// <returns></returns>
        public string GetJobTypeBitByOperationTypeControl(ICP.ReportCenter.UI.Comm.Controls.ReportOperationTypePart reportOperationTypePart1)
        {
            bool isFCL = false, isLCL = false, isBulk = false, isAIR = false, isOther = false, isCTM = false,isFBA= false;

            string[] tempStringArray = reportOperationTypePart1.EditValue.Split(new string[] { reportOperationTypePart1.SplitString }, StringSplitOptions.None);
            if (tempStringArray != null && tempStringArray.Length > 0)
            {
                foreach (var item in tempStringArray)
                {
                    switch (item)
                    {
                        case "0":
                        case "5":
                            isFCL = true;
                            break;
                        case "1":
                        case "6":
                            isLCL = true;
                            break;
                        case "2":
                        case "7":
                            isBulk = true;
                            break;
                        case "3":
                        case "8":
                            isAIR = true;
                            break;
                        case "4":
                            isOther = true;
                            break;
                        case "9":
                            isCTM = true;
                            break;
                        case "13":
                        case "14":
                        case "15":
                            isFBA = true;
                            break;
                    }
                }
            }
            string tempString = string.Empty;
            if (isFCL) tempString += "1";
            else tempString += "0";

            if (isLCL||isFBA) tempString += "1";
            else tempString += "0";

            if (isBulk) tempString += "1";
            else tempString += "0";

            if (isAIR) tempString += "1";
            else tempString += "0";

            if (isOther) tempString += "1";
            else tempString += "0";

            if (isCTM) tempString += "1";
            else tempString += "0";

            return tempString;
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            this._CostItemDatas = null;
            this._CrmOrganizationList = null;
            this._DefaultCompany = null;
            this._GetOrganizationList = null;
            this._GetUserOrganizationList = null;
            this._OfficeList = null;
            this._ReportCtnOperationTypes = null;
            this._ReportGroupTypes = null;
            this._ReportOperationTypes = null;
            this._ReportServerInfo = null;
            this._ShipLines = null;
            this._TopCostItem = null;
            this._UserCompanyList = null;
        }

        #endregion
    }

    public class RateHelper:IDisposable
    {
        #region Services
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        #region Currencys
        List<CurrencyList> _currencys;
        public List<CurrencyList> Currencys
        {
            get
            {
                if (_currencys == null) _currencys = ConfigureService.GetCurrencyList(string.Empty, string.Empty, null, true, 0);
                return _currencys;
            }
        }

        #endregion

        #region Rate
        private List<SolutionExchangeRateList> _RateList = null;
        public List<SolutionExchangeRateList> RateList
        {
            get
            {
                if (_RateList == null)
                {
                    _RateList = ConfigureService.GetCompanyExchangeRateList(LocalData.UserInfo.DefaultCompanyID, true);
                }
                return _RateList;
            }
        }
        private Guid _USDCurrencyID = Guid.Empty;
        public Guid USDCurrencyID
        {
            get
            {
                if (_USDCurrencyID == Guid.Empty)
                {
                   List<CurrencyList>  list=ConfigureService.GetCurrencyList("USD",string.Empty,null,true,0);
                   if (list != null && list.Count > 0)
                   {
                       _USDCurrencyID = list[0].ID;
                   }
                }
                return _USDCurrencyID;
            }
        }

        #endregion

        #region RateHelper

        public decimal GetRate(string sourceCurrency, string targetCurrency, DateTime billdate, List<ICP.Common.ServiceInterface.DataObjects.SolutionExchangeRateList> rateList)
        {
            decimal rate = 1m;

            if (sourceCurrency == targetCurrency) return rate;

            var rateobj = rateList.Find(delegate(ICP.Common.ServiceInterface.DataObjects.SolutionExchangeRateList r)
            {
                return r.SourceCurrency == sourceCurrency &&
                        r.TargetCurrency == targetCurrency &&
                        billdate.Date >= r.FromDate &&
                        billdate.Date <= r.ToDate;
            });

            if (rateobj == null)
            {
                rateobj = rateList.Find(delegate(ICP.Common.ServiceInterface.DataObjects.SolutionExchangeRateList r)
                {
                    return r.TargetCurrency == sourceCurrency &&
                            r.SourceCurrency == targetCurrency &&
                            billdate.Date >= r.FromDate &&
                            billdate.Date <= r.ToDate;
                });

                if (rateobj != null) rate = 1 / rateobj.Rate;
            }
            else
            {
                rate = rateobj.Rate;
            }

            if (rateobj == null)
            {
                if (LocalData.IsEnglish)
                    throw new ApplicationException("have no rate between " + sourceCurrency + " to " + targetCurrency + ".(Date:" + billdate.ToShortDateString() + ")");
                else
                    throw new ApplicationException("没有" + sourceCurrency + "和" + targetCurrency + "的汇率.(时间:" + billdate.ToShortDateString() + ")");
            }

            return rate;
        }

        /// <summary>
        /// 查找汇率
        /// </summary>
        /// <param name="sourceCurrencyID">源币种</param>
        /// <param name="targetCurrencyID">目标币种</param>
        /// <param name="billdate">日期</param>
        /// <param name="rateList">汇率列表</param>
        /// <returns></returns>
        public decimal GetRate(Guid sourceCurrencyID, Guid targetCurrencyID, DateTime billdate, List<ICP.Common.ServiceInterface.DataObjects.SolutionExchangeRateList> rateList)
        {

            if (sourceCurrencyID == Guid.Empty || targetCurrencyID == Guid.Empty) return 0m;

            if (sourceCurrencyID == targetCurrencyID) return 1;

            SolutionExchangeRateList inRate = rateList.Find(delegate(SolutionExchangeRateList item)
            {
                return
                  item.SourceCurrencyID == sourceCurrencyID && item.TargetCurrencyID == targetCurrencyID
                  && billdate.Date >= item.FromDate && billdate.Date <= item.ToDate;
            });

            if (inRate != null)
            {
                return inRate.Rate;
            }

            SolutionExchangeRateList outRate = rateList.Find(delegate(SolutionExchangeRateList item)
            {
                return
                  item.SourceCurrencyID == targetCurrencyID && item.TargetCurrencyID == sourceCurrencyID
                        && billdate.Date >= item.FromDate && billdate.Date <= item.ToDate;
            });

            if (outRate != null)
            {
                return (1 / outRate.Rate);
            }

            string sourceCurrencyName = GetCurrencyNameByCurrencyID(sourceCurrencyID);
            string tagerCurrencyName = GetCurrencyNameByCurrencyID(targetCurrencyID);

            MessageBoxService.ShowInfo(sourceCurrencyName + "=>" + tagerCurrencyName + (LocalData.IsEnglish ? " Rate Not Find." : " 找不到汇率."));
            //throw new ApplicationException(sourceCurrencyName + "=>" + tagerCurrencyName + (LocalData.IsEnglish ? " Rate Not Find." : " 找不到汇率."));
            return 0m;
        }

        public decimal GetAmountByRate(decimal amount, Guid sourceCurrencyID, Guid targetCurrencyID, List<SolutionExchangeRateList> rateList)
        {
            if (sourceCurrencyID == Guid.Empty || targetCurrencyID == Guid.Empty) return 0m;

            if (sourceCurrencyID == targetCurrencyID) return amount;

            SolutionExchangeRateList inRate = rateList.Find(delegate(SolutionExchangeRateList item)
            { return item.SourceCurrencyID == sourceCurrencyID && item.TargetCurrencyID == targetCurrencyID; });
            if (inRate != null)
            {
                return (amount * inRate.Rate);
            }

            SolutionExchangeRateList outRate = rateList.Find(delegate(SolutionExchangeRateList item)
            { return item.SourceCurrencyID == targetCurrencyID && item.TargetCurrencyID == sourceCurrencyID; });
            if (outRate != null)
            {
                return (amount / outRate.Rate);
            }

            string sourceCurrencyName = GetCurrencyNameByCurrencyID(sourceCurrencyID);
            string tagerCurrencyName = GetCurrencyNameByCurrencyID(targetCurrencyID);

            throw new ApplicationException(sourceCurrencyName + "=>" + tagerCurrencyName + (LocalData.IsEnglish ? " Rate Not Find." : " 找不到汇率."));
        }

        public string GetCurrencyNameByCurrencyID(Guid guid)
        {
            CurrencyList tager = Currencys.Find(delegate(CurrencyList item) { return item.ID == guid; });
            if (tager == null) return string.Empty;
            else return tager.Code;
        }


        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            this._currencys = null;
            this._RateList = null;
            
        }

        #endregion
    }

    public class SearchBoxAdapter
    {
        public static IDisposable RegisterSingleSearchBox(IDataFindClientService dfService, Control textBox, string finderName)
        {
          return  dfService.Register(textBox,
                finderName,
                SearchFieldConstants.CodeName,
                SearchFieldConstants.ResultValue,
                null,
                delegate(object inputSource, object[] resultData)
                {
                    textBox.Text = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    textBox.Tag = resultData[0];

                },
                delegate
                {
                    textBox.Text = string.Empty;
                    textBox.Tag = null;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
        }

        public static IDisposable RegisterMultipleSearchBox(IDataFindClientService dfService, Control textBox, string finderName)
        {
           return dfService.RegisterMultiple(textBox, finderName, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
   null,
 delegate(object inputSource, object[] resultData)
 {
     List<Guid> ids = new List<Guid>();
     StringBuilder names = new StringBuilder();
     foreach (var item in resultData)
     {
         object[] data = item as object[];
         if (data == null) continue;

         ids.Add(new Guid(data[0].ToString()));
         if (names.Length > 0) names.Append(ICP.Framework.CommonLibrary.Common.GlobalConstants.ShowDividedSymbol);
         names.Append(LocalData.IsEnglish ? data[2].ToString() : data[3].ToString());
     }
     textBox.Text = names.ToString();
     textBox.Tag = ids;

 },
 delegate
 {
     textBox.Text = string.Empty;
     textBox.Tag = null;
 },
 delegate()
 {
     List<Guid> ids = textBox.Tag as List<Guid>;
     if (ids == null) return null;
     return ids;
 },
 ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

        }
       
        public static IDisposable RegisterChargeCodeMultipleSearchBox(IDataFindClientService dfService, Control textBox, string finderName,ConditionsGetHandler getHandler)
        {
          return  dfService.RegisterMultiple(textBox, finderName, SearchFieldConstants.CodeName, SearchFieldConstants.ChargeCodeResultValue,
              getHandler,
            delegate(object inputSource, object[] resultData)
            {   
                List<Guid> ids = new List<Guid>();
                StringBuilder names = new StringBuilder();
                foreach (var item in resultData)
                {
                    object[] data = item as object[];
                    if (data == null) continue;
                        
                    ids.Add(new Guid(data[0].ToString()));
                    if (names.Length > 0) names.Append(ICP.Framework.CommonLibrary.Common.GlobalConstants.ShowDividedSymbol);
                    names.Append(data[1].ToString() );
                }
                textBox.Text = names.ToString();
                textBox.Tag = ids;

            },
            delegate
            {
                textBox.Text = string.Empty;
                textBox.Tag = null;
            },
            delegate()
            {
                List<Guid> ids = textBox.Tag as List<Guid>;
                if (ids == null) return null;
                return ids;
            },
            ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

        }
        /// <summary>
        /// 会计科目单选搜索器
        /// </summary>
        /// <param name="dfService"></param>
        /// <param name="textBox"></param>
        /// <returns></returns>
        public static IDisposable RegisterGLCodeSingleSearchBox(IDataFindClientService dfService, Control textBox, bool bindCode,List<Guid> companyIds)
        {
            CompanyIDs = companyIds;

            return dfService.Register(textBox,
               ICP.FAM.ServiceInterface.FAMFinderConstants.GLCodeFinder,
               SearchFieldConstants.CodeName,
               SearchFieldConstants.ResultValue,
               GetSolutionIDSearchCondition,
               delegate(object inputSource, object[] resultData)
               {
                   string text = resultData[1].ToString() + "-" + (LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString());
                   textBox.Text = text;
                   if (bindCode)
                   {
                       textBox.Tag = resultData[1];
                   }
                   else
                   {
                       textBox.Tag = resultData[0];
                   }
               },
               delegate
               {
                   textBox.Text = string.Empty;
                   textBox.Tag = null;
               },
               ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
        }

        /// <summary>
        /// 会计科目多选搜索器
        /// </summary>
        /// <param name="dfService"></param>
        /// <param name="textBox"></param>
        /// <returns></returns>
        public static IDisposable RegisterGLCodeMultipleSearchBox(IDataFindClientService dfService, Control textBox, List<Guid> companyIds)
        {
            CompanyIDs = companyIds;

           return  dfService.RegisterMultiple(textBox,
               ICP.FAM.ServiceInterface.FAMFinderConstants.GLCodeFinder,
               SearchFieldConstants.CodeName,
               SearchFieldConstants.ResultValue,
               GetSolutionIDSearchCondition,
               delegate(object inputSource, object[] resultData)
               {
                   List<Guid> ids = new List<Guid>();
                   StringBuilder names = new StringBuilder();
                   foreach (var item in resultData)
                   {
                       object[] data = item as object[];
                       if (data == null) continue;

                        ids.Add(new Guid(data[0].ToString()));
                       if (names.Length > 0) names.Append(ICP.Framework.CommonLibrary.Common.GlobalConstants.ShowDividedSymbol);

                       string text = data[1].ToString() + "-" + (LocalData.IsEnglish ? data[2].ToString() : data[3].ToString());

                       names.Append(text);
                   }
                   textBox.Text = names.ToString();
                   textBox.Tag = ids;

               },
               delegate
               {
                   textBox.Text = string.Empty;
                   textBox.Tag = null;
               },
                delegate()
                {
                    List<Guid> ids = textBox.Tag as List<Guid>;
                    if (ids == null) return null;
                    return ids;
                },
               ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);    
        }


        /// <summary>
        /// 费用代码多选
        /// </summary>
        /// <param name="dfService"></param>
        /// <param name="textBox"></param>
        /// <returns></returns>
        public static IDisposable RegisterChargingCodeMultipleSearchBox(IDataFindClientService dfService, Control textBox)
        {
            return dfService.RegisterMultiple(textBox,
              CommonFinderConstants.ChargingCodeFinder,
              SearchFieldConstants.CodeName,
              SearchFieldConstants.ResultValueChargeCode,
              GetSolutionIDSearchCondition,
              delegate(object inputSource, object[] resultData)
              {
                  List<Guid> ids = new List<Guid>();
                  StringBuilder names = new StringBuilder();
                  foreach (var item in resultData)
                  {
                      object[] data = item as object[];
                      if (data == null) continue;

                      ids.Add(new Guid(data[0].ToString()));
                      if (names.Length > 0) names.Append(ICP.Framework.CommonLibrary.Common.GlobalConstants.ShowDividedSymbol);

                      string text = data[1].ToString() + "-" + (LocalData.IsEnglish ? data[2].ToString() : data[3].ToString());

                      names.Append(text);
                  }
                  textBox.Text = names.ToString();
                  textBox.Tag = ids;

              },
              delegate
              {
                  textBox.Text = string.Empty;
                  textBox.Tag = null;
              },
               delegate()
               {
                   List<Guid> ids = textBox.Tag as List<Guid>;
                   if (ids == null) return null;
                   return ids;
               },
              ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);   


        }


        /// <summary>
        /// 公司列表
        /// </summary>
        public static List<Guid> CompanyIDs
        {
            get;
            set;
        }
        /// <summary>
        /// 会计科目
        /// </summary>
        /// <returns></returns>
        static SearchConditionCollection GetSolutionIDSearchCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", Utility.SolutionID, false);
            conditions.AddWithValue("CompanyIds", CompanyIDs, false);
            return conditions;
        }

        /// <summary>
        /// 费用代码SolutionID
        /// </summary>
        /// <returns></returns>
        static SearchConditionCollection GetSolutionIDForChargingCodeSearchCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", Utility.SolutionID, false);
            return conditions;
        }

    }
}
