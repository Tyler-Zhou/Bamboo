using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Common.UI.ReportView;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface;
using Microsoft.Practices.ObjectBuilder;
using ICP.Common.UI;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects.ReportObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.OA.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanExport.UI.BL
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class NewBLPrintPart : CompositeReportViewPart
    {
        #region Service

        [ServiceDependency]
        public IOEReportDataService oeReportSrvice { get; set; }

        [ServiceDependency]
        public IOceanExportService oeService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelper { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ICustomerService customerService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IConfigureService configureService { get; set; }


        #endregion
        #region 属性

        bool needCloseBL = false;

        #endregion

        public NewBLPrintPart()
        {
            InitializeComponent();
        }
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
        }
        protected override void LoadData()
        {
            InitControls();

        }
        private void InitControls()
        {
            if (_OceanBLList.BLType == FCMBLType.HBL)
            {
                chkShowAMS.Visible = false;
                chkOrderNo.Visible = false;
            }
            SetCmbCompany();
            SetCmbReportStyle();
        }
        #region cmbInit


        List<ConfigureList> _configureList = null;
        private void SetCmbCompany()
        {
            //公司 配置里的公司列表
            _configureList = ICPCommUIHelper.SetCmbConfigureCompany(cmbCompany);
            //cmbCompany.SelectedIndex = 0;
            this.cmbCompany.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.DefaultCompanyName);
        }

        void SetCmbReportStyle()
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<BLReportStly>> bookingTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<BLReportStly>(LocalData.IsEnglish);
            foreach (var item in bookingTypes)
            {
                cmbReportStyle.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbReportStyle.SelectedIndexChanged += new EventHandler(cmbReportStyle_SelectedIndexChanged);
            cmbReportStyle.SelectedIndex = 0;
        }
        #region cmbSelectIndexChanged

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

        protected override void Query()
        {
            Print();
        }

        #endregion

        #endregion
        private void Print()
        {
            btnQuery.Enabled = false;
            if (cmbReportStyle.EditValue == null) return;

            BLReportStly blReportStly = (BLReportStly)Enum.Parse(typeof(BLReportStly), cmbReportStyle.EditValue.ToString());
            string reportFilePath = getFileNameByCmb(blReportStly);

            try
            {
                ICP.Message.ServiceInterface.Message result = GetOperationInfo();

                #region 附页，打印箱
                if (blReportStly == BLReportStly.Attachment)
                {
                    List<BLReportData> reportCtnList = new List<BLReportData>();
                    List<OceanBLContainerList> ctnList = null;
                    string BLQtyUnit = string.Empty;
                    string BLWeightUnit = string.Empty;
                    string BLMeasurementUnit = string.Empty;
                    if (_OceanBLList.BLType == FCMBLType.MBL)
                    {
                        ctnList = oeService.GetOceanMBLContainerList(_OceanBLList.ID);
                        OceanMBLInfo mbl = oeService.GetOceanMBLInfo(_OceanBLList.ID);
                        BLQtyUnit = mbl.QuantityUnitName;
                        BLWeightUnit = mbl.WeightUnitName;
                        BLMeasurementUnit = mbl.MeasurementUnitName;
                    }
                    else
                    {
                        ctnList = oeService.GetOceanHBLContainerList(_OceanBLList.ID);
                        OceanHBLInfo hbl = oeService.GetOceanHBLInfo(_OceanBLList.ID);
                        BLQtyUnit = hbl.QuantityUnitName;
                        BLWeightUnit = hbl.WeightUnitName;
                        BLMeasurementUnit = hbl.MeasurementUnitName;
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
                    reportViewer.BindData(reportFilePath, ctnSource, null, result);
                    return;
                }
                #endregion

                BLReportData data = oeReportSrvice.GetBLReportData(_OceanBLList.ID, _OceanBLList.BLType);
                BLReportClientData blReportData = new BLReportClientData();
                Utility.CopyToValue(data, blReportData, typeof(BLReportClientData));

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
                BulidCompanyInfo(blReportData);
                #endregion

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
                }
                else
                {
                    //HBL提单打印，shipper's Ref ,如果是到美加的单，就显示MBLNO。
                    if (!Utility.GuidIsNullOrEmpty(blReportData.ShippingLineID) && Utility.NAShippingLines.Contains(blReportData.ShippingLineID.Value))
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
                }

                if (blReportData.ETD != null)
                {
                    blReportData.ETDString = blReportData.ETD.Value.ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                }

                if (blReportData.BLType == FCMBLType.MBL)
                {
                    if (chkShowAMS.Checked == false)
                    {
                        blReportData.Agent = string.Empty;
                    }
                    else
                    {
                        //应该要改存储过程的，有空通知李亮改
                        OceanMBLInfo mbl = oeService.GetOceanMBLInfo(_OceanBLList.ID);
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

                BindingSource bs = new BindingSource();
                bs.DataSource = blReportData;
                Dictionary<string, object> reportSource = new Dictionary<string, object>();
                reportSource.Add("ReportSource", bs);
                reportViewer.BindData(reportFilePath, reportSource, null, result);

            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
            finally { btnQuery.Enabled = true; }
        }
        /// <summary>
        /// 根据选择的公司生成公司信息
        /// </summary>
        private void BulidCompanyInfo(BLReportClientData blReportData)
        {
            if (cmbCompany.EditValue == null) return;

            Guid companyID = new Guid(cmbCompany.EditValue.ToString());
            ConfigureList tager = _configureList.Find(delegate(ConfigureList item) { return item.CompanyID == companyID; });
            if (tager == null) return;
            CustomerInfo customer = customerService.GetCustomerInfo(tager.CustomerID);
            blReportData.CompanyAddress = customer.EAddress;
            blReportData.CompanyFax = customer.Fax;
            blReportData.CompanyName = customer.EName;
            blReportData.CompanyTel = customer.Tel1 + (string.IsNullOrEmpty(customer.Tel2) ? string.Empty : "/" + customer.Tel2);
        }
        string getFileNameByCmb(BLReportStly blReportStly)
        {
            string fileName = string.Empty;
            bool isFarEast = false;  //提单的口岸公司是否属于远东区(bug3828:提单修改，先改远东区的)
            ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(_OceanBLList.CompanyID);
            if (configureInfo.SolutionID == new Guid("b6e4dded-4359-456a-b835-e8401c910fd0"))
            {
                isFarEast = true; 
            }

            if (blReportStly == BLReportStly.Original || blReportStly == BLReportStly.UnStyleCopy)
            {
                if (_OceanBLList.BLType == FCMBLType.HBL && _OceanBLList.CompanyID == new Guid("5a827adf-38c7-4a2f-99a7-ad717ce91718")) //越南公司
                {
                    fileName += "OE_BL_Original_ReportForVietnam.frx";
                }
                else
                {
                    if (isFarEast)
                    {
                        fileName += "OE_BL_Original_Report_FarEast.frx";
                    }
                    else
                    {
                        fileName += "OE_BL_Original_Report.frx";
                    }
                }
            }
            else if (blReportStly == BLReportStly.Copy)
            {
                CompanyReportConfigureList reportConfigure
                   = configureService.GetReportConfigureList(_OceanBLList.CompanyID, CommonConstants.ReportRegionType);
                if (reportConfigure != null && _OceanBLList.BLType == FCMBLType.MBL)
                {
                    fileName += "OE_BL_CopyLa_Report.frx";  //美国MBL的格式按照附件格式更改。 
                }
                else
                {
                    if (_OceanBLList.BLType == FCMBLType.HBL && _OceanBLList.CompanyID == new Guid("5a827adf-38c7-4a2f-99a7-ad717ce91718")) //越南公司
                    {
                        fileName += "OE_BL_TR_ReportForVietnam.frx";
                    }
                    else
                    {
                        if (isFarEast)
                        {
                            fileName += "OE_BL_TR_Report_FarEast.frx";
                        }
                        else
                        {
                            fileName += "OE_BL_TR_Report.frx";
                        }
                    }
                }
            }
            else if (blReportStly == BLReportStly.TelexRelease || blReportStly == BLReportStly.DraftOnly || blReportStly == BLReportStly.Require)
            {
                if (_OceanBLList.BLType == FCMBLType.HBL && _OceanBLList.CompanyID == new Guid("5a827adf-38c7-4a2f-99a7-ad717ce91718"))
                {
                    fileName += "OE_BL_TR_ReportForVietnam.frx";
                }
                else
                {
                    if (isFarEast)
                    {
                        fileName += "OE_BL_TR_Report_FarEast.frx";
                    }
                    else
                    {
                        fileName += "OE_BL_TR_Report.frx";
                    }
                }
            }
            else if (blReportStly == BLReportStly.Attachment)
            {
                if(isFarEast)
                {
                    //国内A4
                    fileName += "OE_BL_Annex_Report.frx";
                }
                else
                {
                    //国外Letter
                    fileName += "OE_BL_Annex_Report_Letter.frx";
                }
            }
            else if (blReportStly == BLReportStly.ChinaShip || blReportStly == BLReportStly.ChinaShipHK)
            {
                fileName += "OE_BL_ChinaShip_Report.frx";
            }
            //else if (blReportStly ==  BLReportStly.CopyLa )
            //    fileName += "OE_BL_CopyLa_Report.frx";
            else if (blReportStly == BLReportStly.DLfcm)
            {
                fileName += "OE_BL_DLfcm_Report.frx";
            }
            else
                return string.Empty;

            if (string.IsNullOrEmpty(fileName) == false)
                fileName = System.Windows.Forms.Application.StartupPath + "\\Reports\\OceanExport\\" + fileName;

            return fileName;
        }


        protected override void AfterPrint()
        {
            if (needCloseBL == false || _OceanBLList.State == OEBLState.Checked) return;

            try
            {
                SingleResult result;
                if (_OceanBLList.BLType == FCMBLType.MBL)
                    result = oeService.ChangeOceanMBLState(_OceanBLList.ID, OEBLState.Checked, LocalData.UserInfo.LoginID, _OceanBLList.UpdateDate);
                else
                    result = oeService.ChangeOceanMBLState(_OceanBLList.ID, OEBLState.Checked, LocalData.UserInfo.LoginID, _OceanBLList.UpdateDate);

                this._OceanBLList.State = OEBLState.Checked;
                _OceanBLList.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            } 
        }

        #region IPart 成员
        OceanBLList _OceanBLList = null;

        public override void Init(IDictionary<string, object> values)
        {
            _OceanBLList = ICP.FCM.Common.UI.Utility.GetValue("OceanBLList", values) as OceanBLList;
        }
        #endregion

        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (_OceanBLList == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties.OperationType = OperationType.OceanExport;
            message.UserProperties.OperationId = _OceanBLList.OceanBookingID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.MBL;
            message.UserProperties.FormId = _OceanBLList.ID;
            return message;
        }

    }
    enum BLReportStly
    {
        [MemberDescription("正本")]
        Original = 0,
        [MemberDescription("电放")]
        TelexRelease,
        [MemberDescription("DRAFT ONLY")]
        DraftOnly,
        [MemberDescription("O B/L Require")]
        Require,
        [MemberDescription("无格式副本")]
        UnStyleCopy,
        [MemberDescription("副本")]
        Copy,
        //[MemberDescription("副本LA")]
        //CopyLa,
        [MemberDescription("中海")]
        ChinaShip,
        [MemberDescription("中海(HK)")]
        ChinaShipHK,
        [MemberDescription("附页")]
        Attachment,
        [MemberDescription("大连提货单")]
        DLfcm,
    }

    class BLReportClientData : BLReportData
    {
        public string ETDString { get; set; }
        public string IssueDateString { get; set; }
        public string NumberOfOriginalString { get; set; }
    }
}
