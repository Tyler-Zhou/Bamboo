using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.Common.UI.ReportView;
using ICP.Framework.ClientComponents;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects.ReportObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FileSystem.ServiceInterface;

namespace ICP.FCM.OceanExport.UI.BL
{
    /// <summary>
    /// 打印提单
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class NewBLPrintPart : CompositeReportViewPart
    {
        #region Fields
        /// <summary>
        /// 提单对象
        /// </summary>
        OceanBLList _oceanBLList = null;
        /// <summary>
        /// 需要关闭BL
        /// </summary>
        bool needCloseBL = false;
        /// <summary>
        /// 邮件Message
        /// </summary>
        Message.ServiceInterface.Message _message = null;
        /// <summary>
        /// BL抬头集合
        /// </summary>
        List<ConfigureKeyList> _bltitleList = new List<ConfigureKeyList>();
        /// <summary>
        /// 配置列表
        /// </summary>
        List<ConfigureList> _configureList = null;
        #endregion

        #region Service
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        /// <summary>
        /// 海出报表数据服务
        /// </summary>
        public IOEReportDataService OEReportDataSrvice
        {
            get
            {
                return ServiceClient.GetService<IOEReportDataService>();
            }
        }
        /// <summary>
        /// 海出服务
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }

        /// <summary>
        /// 公共客户管理服务
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        /// <summary>
        /// 基础信息服务(国家、省份、地区)
        /// </summary>
        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get { return _message; }
            set
            {
                if (value != null)
                {
                    _message = value as Message.ServiceInterface.Message;
                }
            }
        }

        /// <summary>
        /// 美线
        /// </summary>
        public static List<Guid> USAShippingLines
        {
            get
            {
                List<Guid> idlist = new List<Guid>();

                idlist.Add(new Guid("6F51BA0E-397C-4AF8-A453-617B1051E76B"));//美西
                idlist.Add(new Guid("8F09FD42-3BBA-4EA9-BB5B-80E53770CA84"));//北美区
                idlist.Add(new Guid("FC4361F1-FF7A-4B57-B411-99E106D1B7C0"));//美国航线
                idlist.Add(new Guid("E2D05D39-B9A2-4C7D-838E-C6FA466609EE"));//美东
                return idlist;
            }
        }

        #endregion

        #region Init
        /// <summary>
        /// 打印提单
        /// </summary>
        public NewBLPrintPart()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            _oceanBLList = FCM.Common.UI.FCMUIUtility.GetValue("OceanBLList", values) as OceanBLList;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Locale()
        {
            base.Locale();
            if (!LocalData.IsEnglish)
            {
                labHead.Text = "标题";
                labStyle.Text = "样式";
                labTitle.Text = "抬头";
                groupStyle.Text = "样式";

                chkOrderNo.Text = "显示订舱号";
                chkShowAMS.Text = "显示 AMS";
            }
            Disposed += delegate
            {
                _bltitleList = null;
                _oceanBLList = null;
                cmbCompany.OnFirstEnter -= OncmbCompanyFirstEnter;
                cmbReportStyle.SelectedIndexChanged -= cmbReportStyle_SelectedIndexChanged;
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }

            };
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        protected override void LoadData()
        {
            InitControls();
        }
        #endregion

        #region Controls Event
        /// <summary>
        /// 口岸首次获得焦点
        /// </summary>
        private void OncmbCompanyFirstEnter(object sender, EventArgs e)
        {
            _bltitleList = ConfigureService.GetConfigureKeyListForBLTitle();
            cmbCompany.Properties.BeginUpdate();
            cmbCompany.Properties.Items.Clear();
            foreach (ConfigureKeyList item in _bltitleList)
            {
                cmbCompany.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.ID));
            }
            cmbCompany.Properties.EndUpdate();
        }
        /// <summary>
        /// 报表类型选择更爱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbReportStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReportStyle.EditValue == null) return;
            BLReportStly blReportStly = (BLReportStly)(Enum.Parse(typeof(BLReportStly), cmbReportStyle.EditValue.ToString()));
            if (blReportStly == BLReportStly.Original || blReportStly == BLReportStly.TelexRelease)
            {
                //1):打印电放，正本提单时候关单. 
                needCloseBL = true;
                cmbHead.Enabled = false;
            }
            else
            {
                //2):打印副本的时候，可以更改打印标题.
                needCloseBL = false;
                cmbHead.Enabled = true;
            }
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 查询
        /// </summary>
        protected override void Query()
        {
            Print();
        }
        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            btnQuery.Enabled = false;
            try
            {
                if (cmbReportStyle.EditValue == null) return;
                if (cmbHead.Text.Contains("CITY OCEAN"))
                {
                    if (_oceanBLList.IsHasContract && _oceanBLList.CarrierID.ToString().ToUpper() == "BF072F15-BEE9-4C33-8448-70931FC06FA9" && USAShippingLines.Contains((Guid)_oceanBLList.ShippingLineID))
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "MSC's contract cannot used CITY OCEAN TITLE" : "MSC的合约，不能使用CITY OCEAN抬头");
                        return;
                    }
                }

                BLReportStly blReportStly = (BLReportStly)Enum.Parse(typeof(BLReportStly), cmbReportStyle.EditValue.ToString());
                string BLName = string.Empty;
                //保存类型
                DocumentType DocType = DocumentType.MBL;
                string reportFilePath = getFileNameByCmb(blReportStly);
                if (string.IsNullOrEmpty(reportFilePath))
                    return;
                Message.ServiceInterface.Message result = GetOperationInfo();

                #region 附页，打印箱
                if (blReportStly == BLReportStly.Attachment)
                {
                    List<BLReportData> reportCtnList = new List<BLReportData>();
                    List<OceanBLContainerList> ctnList = null;
                    string BLQtyUnit = string.Empty;
                    string BLWeightUnit = string.Empty;
                    string BLMeasurementUnit = string.Empty;

                    if (_oceanBLList.BLType == FCMBLType.MBL)
                    {
                        ctnList = OceanExportService.GetOceanMBLContainerList(_oceanBLList.ID);
                        OceanMBLInfo mbl = OceanExportService.GetOceanMBLInfo(_oceanBLList.ID);
                        BLQtyUnit = mbl.QuantityUnitName;
                        BLWeightUnit = mbl.WeightUnitName;
                        BLMeasurementUnit = mbl.MeasurementUnitName;
                        BLName += "BL " + mbl.No;
                        DocType = DocumentType.MBL;
                    }
                    else
                    {
                        ctnList = OceanExportService.GetOceanHBLContainerList(_oceanBLList.ID);
                        OceanHBLInfo hbl = OceanExportService.GetOceanHBLInfo(_oceanBLList.ID);

                        BLQtyUnit = hbl.QuantityUnitName;
                        BLWeightUnit = hbl.WeightUnitName;
                        BLMeasurementUnit = hbl.MeasurementUnitName;
                        BLName += "BL " + hbl.No;
                        DocType = DocumentType.HBL;
                    }

                    if (ctnList != null && ctnList.Count > 0)
                    {
                        foreach (var ctn in ctnList)
                        {
                            string ctnInfo = string.Empty;
                            if (ctn.IsPartOf)
                            {
                                ctnInfo += "PART OF ";
                            }

                            ctnInfo += string.Format("{0} / {1} / {2}", string.IsNullOrEmpty(ctn.No) ? string.Empty : ctn.No.ToUpper(), ctn.TypeName, ctn.SealNo);
                            ctnInfo += "/ " + ctn.Quantity + " " +
                                                (
                                                    ctn.Quantity > 1 ?
                                                        (
                                                            BLQtyUnit.ToUpper().EndsWith("S") ?
                                                            BLQtyUnit.ToString() :
                                                            BLQtyUnit + "S"
                                                        )
                                                        :
                                                        BLQtyUnit
                                                );

                            string strWeight = ctn.Weight.ToString("F3");
                            string strMeasurement = ctn.Measurement.ToString("F3");
                            ctnInfo += "/ " + strWeight + " " + BLWeightUnit;
                            ctnInfo += "/ " + strMeasurement + " " + BLMeasurementUnit;

                            BLReportData ctndata = new BLReportData();
                            ctndata.CtnQtyInfo = ctnInfo;
                            reportCtnList.Add(ctndata);
                        }
                    }



                    Dictionary<string, object> ctnSource = new Dictionary<string, object>();
                    ctnSource.Add("ReportData", reportCtnList);
                    ctnSource.Add(ICP.Common.ServiceInterface.CommonConstants.DocumentNameKey, BLName + "_" + blReportStly.ToString());
                    ctnSource.Add(ICP.Common.ServiceInterface.CommonConstants.DocumentTypeKey, DocType);
                    if (_message != null)
                    {
                        reportViewer.BindData(reportFilePath, ctnSource, null, _message);
                    }
                    else
                    {
                        reportViewer.BindData(reportFilePath, ctnSource, null, result);
                    }

                    return;
                }
                #endregion

                BLReportData data = OEReportDataSrvice.GetBLReportData(_oceanBLList.ID, _oceanBLList.BLType);

                BLReportClientData blReportData = new BLReportClientData();
                OEUtility.CopyToValue(data, blReportData, typeof(BLReportClientData));

                #region 1.提单份数,电放为ZERO(0),其它为 THREE(3); 2.电放\DRAFT ONLY\O B/L REQUIER的字符设置

                blReportData.NumberOfOriginalString = "THREE(3)";
                if (blReportStly == BLReportStly.TelexRelease)
                {
                    blReportData.ReportStyle = "TELEX RELEASE";
                    blReportData.NumberOfOriginalString = "ZERO(0)";
                }
                else if (blReportStly == BLReportStly.DraftOnly)
                {
                    blReportData.ReportStyle = "DRAFT ONLY";
                }
                else if (blReportStly == BLReportStly.Require)
                {
                    blReportData.ReportStyle = "O B/L REQUIRE";
                }
                else if (blReportStly == BLReportStly.UnStyleCopy)
                {
                    blReportData.ReportStyle = "COPY";
                }
                else
                {
                    blReportData.ReportStyle = blReportStly.ToString();
                }

                #endregion

                #region 抬头
                if (blReportStly != BLReportStly.Original) blReportData.Header = cmbHead.Text;
                #endregion

                #region 船名航次
                //if (data.VoyageShowType == VoyageShowType.Confirm)
                //{
                //    data.PlaceOfReceipt = string.Empty;
                //    data.PreCarriage = string.Empty;
                //}

                #endregion

                #region 根据选择的公司生成公司信息
                //BulidCompanyInfo(blReportData);
                #endregion

                blReportData.CompanyName = cmbCompany.Text;
                //if (cmbCompany.SelectedText == "CITY OCEAN LOGISTICS CO.,LTD.")
                //{
                //    blReportData.ISFNO = "CTYO";
                //}
                //else if (cmbCompany.SelectedText == "TOP SHIPPING LOGISTICS CO.,LTD")
                //{
                //    blReportData.ISFNO = "TPHJ";
                //}

                if (blReportData.BLType == FCMBLType.MBL)
                {
                    if (chkOrderNo.Checked)
                    {
                        if (!string.IsNullOrEmpty(blReportData.ShipperOrderNo))
                        {
                            blReportData.MBLNo = "ORDER NO:" + blReportData.ShipperOrderNo;
                        }
                        else
                        {
                            blReportData.MBLNo = string.Empty;
                        }
                    }
                    else { blReportData.MBLNo = string.Empty; }

                    DocType = DocumentType.MBL;

                    switch (blReportData.FCMReleaseType)
                    {
                        case FCMReleaseType.Unknown:
                            break;
                        case FCMReleaseType.Original:
                            blReportData.FCMReleaseTypeText = "* Original B/L Required *";
                            break;
                        case FCMReleaseType.Telex:
                            blReportData.FCMReleaseTypeText = "* Telex Release *";
                            break;
                        case FCMReleaseType.SeaWay:
                            blReportData.FCMReleaseTypeText = "* NEED SEA WAY BILL *";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    //HBL提单打印，shipper's Ref ,如果是到美加的单，就显示MBLNO。
                    if (!ArgumentHelper.GuidIsNullOrEmpty(blReportData.ShippingLineID) && OEUtility.NAShippingLines.Contains(blReportData.ShippingLineID.Value))
                    {
                        if (!string.IsNullOrEmpty(blReportData.MBLNo))
                        {
                            blReportData.MBLNo = "MBL NO:" + blReportData.MBLNo;
                        }
                    }
                    else
                    {
                        blReportData.MBLNo = string.Empty;
                    }
                    DocType = DocumentType.HBL;
                }

                if (blReportData.ETD != null)
                {
                    blReportData.ETDString = blReportData.ETD.Value.ToString("MMM.dd,yyyy", DateTimeFormatInfo.InvariantInfo);
                }

                if (blReportData.BLType == FCMBLType.MBL)
                {
                    if (chkShowAMS.Checked == false)
                    {
                        blReportData.Agent = string.Empty;
                    }
                    else
                    {
                        OceanMBLInfo mbl = OceanExportService.GetOceanMBLInfo(_oceanBLList.ID);
                        blReportData.Agent = mbl.AgentText;
                    }
                }

                #region Harvest，Connie，客人要求不要在BL的 MEASUREMENT 栏目显示 0 或者 0.000
                if (blReportData.Measurement == "0.000 CBM")
                {
                    blReportData.Measurement = string.Empty;
                }

                if (!string.IsNullOrEmpty(blReportData.DescriptionOfContainer))
                {
                    blReportData.DescriptionOfContainer = blReportData.DescriptionOfContainer.Replace("/0.000CBM", "");
                }
                #endregion


                BLName += "BL" + System.Text.RegularExpressions.Regex.Replace(_oceanBLList.No, @"/|\*|\(", "_") + "_" + blReportStly.ToString();

                #region  合单都必须提供详细的分柜资料包括每柜品名和唛头

                blReportData.DescriptionOfContainer = strFormatEidt(blReportData.DescriptionOfContainer);

                #endregion
                BindingSource bs = new BindingSource();
                bs.DataSource = blReportData;
                Dictionary<string, object> reportSource = new Dictionary<string, object>();
                reportSource.Add("ReportSource", bs);
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.DocumentNameKey, BLName);
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.DocumentTypeKey, DocType);
                reportSource.Add("FormId", _oceanBLList.ID);
                //测试数据
                //1.动态添加多个导出指定文档类型的菜单
                //2.动态添加一个导出指定文档类型的菜单

                DocumentType documenttype = DocType == DocumentType.MBL ? DocumentType.SIMBL : DocumentType.SIHBL;
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.FileArchiveMenuInfoKey, new FileArchiveMenuInfo { CCaption = "以SIBL格式保存到文档列表", ECaption = "Save SIBL into Document List", DocumentType = documenttype.ToString(), FileName = documenttype + "_" + _oceanBLList.No });
                if (_message != null)
                {
                    reportViewer.BindData(reportFilePath, reportSource, null, _message);
                }
                else
                {
                    OceanBookingInfo info = OceanExportService.GetOceanBookingInfo(_oceanBLList.OceanBookingID);
                    CustomerInfo _customerInfo = CustomerService.GetCustomerInfo(info.CustomerID);
                    if (_customerInfo != null && !string.IsNullOrEmpty(_customerInfo.Fax))
                    {
                        reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, _customerInfo.Fax);
                    }
                    if (_customerInfo != null && !string.IsNullOrEmpty(_customerInfo.EMail))
                    {
                        reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, _customerInfo.EMail);
                    }
                    reportViewer.BindData(reportFilePath, reportSource, null, result);
                }
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
            finally { btnQuery.Enabled = true; }
        }
        /// <summary>
        /// 设置口岸
        /// </summary>
        private void SetCmbCompany()
        {
            cmbCompany.OnFirstEnter += OncmbCompanyFirstEnter;
            cmbCompany.ShowSelectedValue(_oceanBLList.BLTitleID, _oceanBLList.BLTitleName);
        }

        private void InitControls()
        {
            if (_oceanBLList.BLType == FCMBLType.HBL)
            {
                chkShowAMS.Visible = false;
                chkOrderNo.Visible = false;
                //cmbCompany.Enabled = false;
            }
            SetCmbCompany();
            SetCmbReportStyle();
            //cmbCompany.Enabled = false;
        }

        void SetCmbReportStyle()
        {
            List<EnumHelper.ListItem<BLReportStly>> bookingTypes = EnumHelper.GetEnumValues<BLReportStly>(LocalData.IsEnglish);
            cmbReportStyle.Properties.BeginUpdate();
            foreach (var item in bookingTypes)
            {
                cmbReportStyle.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbReportStyle.Properties.EndUpdate();
            cmbReportStyle.SelectedIndexChanged += new EventHandler(cmbReportStyle_SelectedIndexChanged);
            cmbReportStyle.SelectedIndex = 0;

        }

        string getFileNameByCmb(BLReportStly blReportStly)
        {
            string fileName = string.Empty;
            bool isFarEast = false;  //提单的口岸公司是否属于远东区(bug3828:提单修改，先改远东区的)
            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(_oceanBLList.CompanyID);
            OceanBookingInfo oceanbookinginfo = OceanExportService.GetOceanBookingInfo(_oceanBLList.OceanBookingID);
            LocationInfo locationinfo = GeographyService.GetLocationInfo(oceanbookinginfo.PlaceOfDeliveryID);
            //目的港代理为CITYOCEAN && 放单类型为电放 && 电放号为空禁止打印
            bool isPrintable = true;
            if (_oceanBLList.BLType == FCMBLType.HBL
                && blReportStly == BLReportStly.TelexRelease
                && string.IsNullOrEmpty(_oceanBLList.TelexNo))
            {
                List<ConfigureList> oList = ConfigureService.GetConfigureListByList(null, null, true, 0);
                foreach (ConfigureList oItem in oList.Where(oItem => !ArgumentHelper.GuidIsNullOrEmpty(oItem.CustomerID)
                                                                     && !ArgumentHelper.GuidIsNullOrEmpty(oceanbookinginfo.AgentID)
                                                                     && oItem.CustomerID.Equals(oceanbookinginfo.AgentID)))
                {
                    MessageBoxService.ShowWarning(
                        LocalData.IsEnglish ? "The HBL could not be printed because Telex NO is empty." : "电放号为空，不能打印!"
                        , LocalData.IsEnglish ? " Tip" : "提示", MessageBoxButtons.OK);
                    isPrintable = false;
                }
                if (!isPrintable)
                    return string.Empty;
            }

            if (configureInfo.SolutionID == new Guid("B6E4DDED-4359-456A-B835-E8401C910FD0")) //远东解决方案
            {
                isFarEast = true;
            }


            if (blReportStly == BLReportStly.Original || blReportStly == BLReportStly.UnStyleCopy)
            {
                if (_oceanBLList.BLType == FCMBLType.HBL && _oceanBLList.CompanyID == new Guid("5A827ADF-38C7-4A2F-99A7-AD717CE91718")) //越南公司
                {
                    fileName += "OE_BL_Original_ReportForVietnam";
                }
                else if (_oceanBLList.BLType == FCMBLType.HBL && locationinfo.CountryID == new Guid("1F276DB3-4B23-4EEB-A139-25F8912610BE"))//目的地巴西
                {
                    if (cmbCompany.Text.Contains("DA WU"))
                    {
                        fileName += "OE_BL_Original_ReportForBraZil_DAWU";
                    }
                    else
                        fileName += "OE_BL_Original_ReportForBraZil";
                }
                else if (_oceanBLList.BLType == FCMBLType.HBL && locationinfo.CountryID == new Guid("C39E4B07-6D6B-42A9-BA0B-512AB48C7274"))//目的地阿根廷
                {
                    if (cmbCompany.Text.Contains("DA WU"))
                    {
                        fileName += "OE_BL_Original_ReportForArgentina_DAWU";
                    }
                    else
                        fileName += "OE_BL_Original_ReportForArgentina";
                }
                else
                {
                    if (isFarEast)
                    {
                        fileName += "OE_BL_Original_Report_FarEast";
                    }
                    else
                    {
                        fileName += "OE_BL_Original_Report";
                    }
                }
            }
            else if (blReportStly == BLReportStly.Copy)
            {
                CompanyReportConfigureList reportConfigure
                   = ConfigureService.GetReportConfigureList(_oceanBLList.CompanyID, CommonConstants.ReportRegionType);
                if (reportConfigure != null && _oceanBLList.BLType == FCMBLType.MBL)
                {
                    fileName += "OE_BL_CopyLa_Report";  //美国MBL的格式按照附件格式更改。 
                }
                else
                {
                    if (_oceanBLList.BLType == FCMBLType.HBL && _oceanBLList.CompanyID == new Guid("5A827ADF-38C7-4A2F-99A7-AD717CE91718")) //越南公司
                    {
                        fileName += "OE_BL_TR_ReportForVietnam";
                    }
                    else if (_oceanBLList.CompanyID == new Guid("A62A9F8E-E69C-4E6E-AD85-E75AED3C6CF9")) //宁波公司
                    {
                        if (cmbCompany.Text.Contains("DA WU"))
                        {
                            fileName += "OE_BL_TR_Report_NingBo_DAWU";
                        }
                        else
                            fileName += "OE_BL_TR_Report_NingBo";
                    }
                    else if (_oceanBLList.BLType == FCMBLType.HBL && locationinfo.CountryID == new Guid("1F276DB3-4B23-4EEB-A139-25F8912610BE"))//目的地巴西
                    {
                        if (cmbCompany.Text.Contains("DA WU"))
                        {
                            fileName += "OE_BL_TR_ReportForBraZil_DAWU";
                        }
                        else
                            fileName += "OE_BL_TR_ReportForBraZil";
                    }
                    else if (_oceanBLList.CompanyID == new Guid("A62A9F8E-E69C-4E6E-AD85-E75AED3C6CF9") 
                        && locationinfo.CountryID == new Guid("C39E4B07-6D6B-42A9-BA0B-512AB48C7274")) //宁波公司且是阿根廷线
                    {
                        fileName += "OE_BL_TR_Report_NingBo";
                    }
                    else if (_oceanBLList.BLType == FCMBLType.HBL && locationinfo.CountryID == new Guid("C39E4B07-6D6B-42A9-BA0B-512AB48C7274"))//目的地阿根廷
                    {
                        if (cmbCompany.Text.Contains("DA WU"))
                        {
                            fileName += "OE_BL_TR_ReportForArgentina_DAWU";
                        }
                        else
                            fileName += "OE_BL_TR_ReportForArgentina";
                    }
                    else
                    {
                        if (isFarEast)
                        {
                            fileName += "OE_BL_TR_Report_FarEast";
                        }
                        else
                        {
                            if (cmbCompany.Text.Contains("DA WU"))
                            {
                                fileName += "OE_BL_TR_Report_DAWU";
                            }else
                            {
                                fileName += "OE_BL_TR_Report";
                            }
                        }
                    }
                }
            }
            else if (blReportStly == BLReportStly.TelexRelease || blReportStly == BLReportStly.DraftOnly || blReportStly == BLReportStly.Require)
            {
                //越南口岸
                if (_oceanBLList.BLType == FCMBLType.HBL && _oceanBLList.CompanyID == new Guid("5A827ADF-38C7-4A2F-99A7-AD717CE91718"))
                {
                    fileName += "OE_BL_TR_ReportForVietnam";
                }
                else if (_oceanBLList.BLType == FCMBLType.HBL && locationinfo.CountryID == new Guid("C39E4B07-6D6B-42A9-BA0B-512AB48C7274"))//目的地阿根廷
                {

                    fileName += "OE_BL_TR_ReportForArgentina";
                }
                else
                {
                    if (isFarEast)
                    {
                        fileName += "OE_BL_TR_Report_FarEast";
                    }
                    else
                    {
                        fileName += "OE_BL_TR_Report";
                    }
                }
            }
            else if (blReportStly == BLReportStly.Attachment)
            {
                if (isFarEast)
                {
                    //国内A4
                    fileName += "OE_BL_Annex_Report";
                }
                else
                {
                    //国外Letter
                    fileName += "OE_BL_Annex_Report_Letter";
                }
            }
            else if (blReportStly == BLReportStly.ChinaShip || blReportStly == BLReportStly.ChinaShipHK)
            {
                fileName += "OE_BL_ChinaShip_Report";
            }
            //else if (blReportStly ==  BLReportStly.CopyLa )
            //    fileName += "OE_BL_CopyLa_Report";
            else if (blReportStly == BLReportStly.DLfcm)
            {
                fileName += "OE_BL_DLfcm_Report";
            }
            else if (blReportStly == BLReportStly.JDYfcm)
            {
                fileName += "OE_BL_TR_ReportForJDY";
            }
            else
                return string.Empty;

            if (string.IsNullOrEmpty(fileName) == false)
                fileName =string.Format("{0}\\Reports\\OceanExport\\{1}.frx", Application.StartupPath,fileName);

            return fileName;
        }

        /// <summary>
        /// 打印后
        /// 1.更改提单状态
        /// </summary>
        protected override void AfterPrint()
        {
            if (needCloseBL == false || _oceanBLList.State == OEBLState.Checked) return;

            try
            {
                SingleResult result;
                if (_oceanBLList.BLType == FCMBLType.MBL)
                    result = OceanExportService.ChangeOceanMBLState(_oceanBLList.ID, OEBLState.Checked, LocalData.UserInfo.LoginID, _oceanBLList.UpdateDate);
                else
                    result = OceanExportService.ChangeOceanMBLState(_oceanBLList.ID, OEBLState.Checked, LocalData.UserInfo.LoginID, _oceanBLList.UpdateDate);

                _oceanBLList.State = OEBLState.Checked;
                _oceanBLList.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        private Message.ServiceInterface.Message GetOperationInfo()
        {
            if (_oceanBLList == null)
                return null;
            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.UserProperties = new MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanExport;
            message.UserProperties.OperationId = _oceanBLList.OceanBookingID;
            message.UserProperties.FormType = (_oceanBLList.BLType == FCMBLType.MBL) ? FormType.MBL : FormType.HBL;
            message.UserProperties.FormId = _oceanBLList.ID;
            return message;
        }

        private string strFormatEidt(string orgstr)
        {
            string format = string.Empty;
            string[] orgArr = orgstr.Split('|');

            if (orgArr.Length > 0)
            {
                format += orgArr[0] + Environment.NewLine;
                for (int i = 1; i < orgArr.Length; i++)
                {
                    if (string.IsNullOrEmpty(orgArr[i]))
                    {
                        continue;
                    }

                    string[] Arr = orgArr[i].Split('@');
                    format += Arr[0] + Environment.NewLine;
                    if (Arr.Length >= 1)
                    {
                        string[] markArr = Arr.Length >= 2 ? Arr[1].Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries) : null;
                        string[] commodityArr = Arr.Length >= 3 ? Arr[2].Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries) : null;
                        int marklen = 0;
                        if (markArr != null && commodityArr != null)
                        {
                            foreach (string mark in markArr)
                            {
                                if (mark.Length > marklen)
                                {
                                    marklen = mark.Length;
                                }
                            }
                            if (markArr.Length <= commodityArr.Length)
                            {
                                for (int j = 0; j < commodityArr.Length; j++)
                                {
                                    if (j < markArr.Length)
                                    {
                                        if (string.IsNullOrEmpty(markArr[j]) && string.IsNullOrEmpty(commodityArr[j]))
                                        {
                                            continue;
                                        }
                                        format += markArr[j];
                                        for (int z = 0; z <= (marklen - markArr[j].Length); z++)
                                        {
                                            format += "  ";
                                        }
                                        format += "                   " + commodityArr[j] + Environment.NewLine;

                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(commodityArr[j]))
                                        {
                                            continue;
                                        }
                                        for (int z = 0; z <= marklen + 1; z++)
                                        {
                                            format += "  ";
                                        }
                                        format += "                   " + commodityArr[j] + Environment.NewLine;
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < markArr.Length; j++)
                                {
                                    if (j < commodityArr.Length)
                                    {
                                        if (string.IsNullOrEmpty(markArr[j]) && string.IsNullOrEmpty(commodityArr[j]))
                                        {
                                            continue;
                                        }
                                        format += markArr[j];
                                        for (int z = 0; z <= (marklen - markArr[j].Length); z++)
                                        {
                                            format += "  ";
                                        }
                                        format += "                   " + commodityArr[j] + Environment.NewLine;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(markArr[j]))
                                        {
                                            continue;
                                        }
                                        format += markArr[j] + Environment.NewLine;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (markArr != null && markArr.Length > 0)
                            {
                                for (int j = 0; j < markArr.Length; j++)
                                {
                                    format += markArr[j] + Environment.NewLine;
                                }
                            }
                            else if (commodityArr != null && commodityArr.Length > 0)
                            {
                                for (int j = 0; j < commodityArr.Length; j++)
                                {
                                    format += commodityArr[j] + Environment.NewLine;
                                }
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(format))
            {
                return format;
            }
            return orgstr;
        }
        #endregion

    }
    enum BLReportStly
    {
        /// <summary>
        /// 正本
        /// </summary>
        [MemberDescription("正本", "Original")]
        Original = 0,
        /// <summary>
        /// 电放
        /// </summary>
        [MemberDescription("电放", "TelexRelease")]
        TelexRelease,
        /// <summary>
        /// DRAFT ONLY
        /// </summary>
        [MemberDescription("DRAFT ONLY", "DraftOnly")]
        DraftOnly,
        /// <summary>
        /// O B/L Require
        /// </summary>
        [MemberDescription("O B/L Require", "Require")]
        Require,
        /// <summary>
        /// 无格式副本
        /// </summary>
        [MemberDescription("无格式副本", "UnStyleCopy")]
        UnStyleCopy,
        /// <summary>
        /// 副本
        /// </summary>
        [MemberDescription("副本", "Copy")]
        Copy,
        /// <summary>
        /// 中海
        /// </summary>
        [MemberDescription("中海", "ChinaShip")]
        ChinaShip,
        /// <summary>
        /// 中海(HK)
        /// </summary>
        [MemberDescription("中海(HK)", "ChinaShipHK")]
        ChinaShipHK,
        /// <summary>
        /// 附页
        /// </summary>
        [MemberDescription("附页", "Attachment")]
        Attachment,
        /// <summary>
        /// 大连提货单
        /// </summary>
        [MemberDescription("大连提货单", "DLfcm")]
        DLfcm,
        /// <summary>
        /// JDY提单格式
        /// </summary>
        [MemberDescription("JDY提单格式", "JDY BL FORMAT")]
        JDYfcm,
    }

    class BLReportClientData : BLReportData
    {
        public string ETDString { get; set; }
        public string IssueDateString { get; set; }
        public string NumberOfOriginalString { get; set; }
    }
}
