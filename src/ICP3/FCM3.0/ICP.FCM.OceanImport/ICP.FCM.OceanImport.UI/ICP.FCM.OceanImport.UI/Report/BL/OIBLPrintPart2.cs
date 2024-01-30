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
using ICP.FCM.OceanImport.ServiceInterface;
using Microsoft.Practices.ObjectBuilder;
using ICP.Common.UI;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.OA.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanImport.UI.Report
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class OIBLPrintPart2 : CompositeReportViewPart
    {
        #region Service

        [ServiceDependency]
        public IOIReportDataService oiReportSrvice { get; set; }

        [ServiceDependency]
        public IOceanImportService oiService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelperService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ICustomerService customerService { get; set; }

        #endregion
        #region 属性

        bool needCloseBL = false;

        #endregion
        public OIBLPrintPart2()
        {
            InitializeComponent();
        }
        protected override void Locale()
        {
            base.Locale();
            labHead.Text = "标题";
            //labLOGO.Text = "图标";
            labStyle.Text = "样式";
            labTitle.Text = "抬头";
            groupStyle.Text = "样式";
            labHBLNo.Text = "HBL提单号";
        }
        protected override void LoadData()
        {
            InitControls();
        }
        private void InitControls()
        {
            OceanBusinessInfo businessInfo = oiService.GetBusinessInfoByEdit(_currentOceanBusinessList.ID);
            foreach (var item in businessInfo.HBLList)
            {
                cmbHBLNo.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.HBLNo, item.ID));
            }

            cmbHBLNo.SelectedIndex = 0;
            SetCmbCompany();
            SetCmbReportStyle();
            
        }
        #region cmbInit

        List<ConfigureList> _configureList = null;
        private void SetCmbCompany()
        {
            //公司 配置里的公司列表
            _configureList = ICPCommUIHelperService.SetCmbConfigureCompany(cmbCompany);
            cmbCompany.SelectedIndex = 0;
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


        #endregion
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

        #endregion
        protected override void Query()
        {
            Print();
        }
        private void Print()
        {
            
            if (cmbReportStyle.EditValue == null) return;

            BLReportStly blReportStly = (BLReportStly)Enum.Parse(typeof(BLReportStly), cmbReportStyle.EditValue.ToString());
            string reportFilePath = getFileNameByCmb(blReportStly);

            try
            {
                if (cmbHBLNo.EditValue == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "" : "没有提单，不能打印!", LocalData.IsEnglish ? "Tip" : "提示");
                    return;
                }

                OIBLReportData data = oiReportSrvice.GetOIBLReportData((Guid)cmbHBLNo.EditValue);
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
                    blReportData.ReportStyle = "DRAFT ONLY";
                else if (blReportStly == BLReportStly.Require)
                    blReportData.ReportStyle = "O B/L REQUIER";
                else if (blReportStly == BLReportStly.UnStyleCopy)
                    blReportData.ReportStyle = "COPY";
                else
                    blReportData.ReportStyle = blReportStly.ToString();

                #endregion

                #region 抬头
                if (blReportStly != BLReportStly.Original) blReportData.Header = cmbHead.Text;
                #endregion

                #region 根据选择的公司生成公司信息
                BulidCompanyInfo(blReportData);
                #endregion

                if (blReportStly == BLReportStly.DraftOnly || blReportStly == BLReportStly.Original)
                {
                    blReportData.MBLNo = string.Empty;
                }
                else if (string.IsNullOrEmpty(blReportData.MBLNo) == false)
                {
                    blReportData.MBLNo = "MBLNO:" + blReportData.MBLNo;
                }

                if (blReportData.ETD != null)
                {
                    blReportData.ETDString = blReportData.ETD.Value.ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                }

                BindingSource bs = new BindingSource();
                bs.DataSource = blReportData;

                Dictionary<string, object> dicData = new Dictionary<string, object>();
                dicData.Add("ReportSource", bs);
                reportViewer.BindData(reportFilePath, dicData, null, GetOperationInfo());
                 
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
            
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

            if (blReportStly == BLReportStly.Original || blReportStly == BLReportStly.UnStyleCopy)
                fileName += "OI_BL_Original_Report.frx";
            else if (blReportStly == BLReportStly.TelexRelease || blReportStly == BLReportStly.DraftOnly || blReportStly == BLReportStly.Require
                     || blReportStly == BLReportStly.Copy)
                fileName += "OI_BL_TR_Report.frx";
            else if (blReportStly == BLReportStly.Attachment)
                fileName += "OI_BL_Annex_Report.frx";
            else if (blReportStly == BLReportStly.ChinaShip || blReportStly == BLReportStly.ChinaShipHK)
                fileName += "OI_BL_ChinaShip_Report.frx";
            else if (blReportStly == BLReportStly.CopyLa)
                fileName += "OI_BL_CopyLa_Report.frx";
            else if (blReportStly == BLReportStly.DLfcm)
                fileName += "OI_BL_DLfcm_Report.frx";
            else
                return string.Empty;

            if (string.IsNullOrEmpty(fileName) == false)
                fileName = System.Windows.Forms.Application.StartupPath + "\\Reports\\OceanImport\\" + fileName;

            return fileName;
        }
        #region IPart 成员

        OceanBusinessList _currentOceanBusinessList = null;
        public override void Init(IDictionary<string, object> values)
        {
            _currentOceanBusinessList = ICP.FCM.Common.UI.Utility.GetValue("OceanBusinessList", values) as OceanBusinessList;
        }

        private OperationMailMessages GetOperationInfo()
        {
            if (_currentOceanBusinessList == null)
                return null;
            OperationMailMessages operationInfo = new OperationMailMessages();
            operationInfo.OperationType = ICP.OA.ServiceInterface.DataObjects.OperationType.OceanImport;
            operationInfo.OperationID = _currentOceanBusinessList.ID;
            operationInfo.FormType = ICP.OA.ServiceInterface.DataObjects.FormType.Booking;
            operationInfo.FormID = _currentOceanBusinessList.ID;
            return operationInfo;
        }

        #endregion
        protected override void AfterPrint()
        {
            if (needCloseBL == false || _currentOceanBusinessList.State == OIOrderState.Checked) return;

            try
            {
                SingleResultData result = oiService.ChangeOIOrderState(_currentOceanBusinessList.ID, OIOrderState.BookingConfirmed, string.Empty, LocalData.UserInfo.LoginID, _currentOceanBusinessList.UpdateDate);
                _currentOceanBusinessList.State = OIOrderState.BookingConfirmed;
                _currentOceanBusinessList.UpdateDate = result.UpdateDate;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
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
        [MemberDescription("副本LA")]
        CopyLa,
        [MemberDescription("中海")]
        ChinaShip,
        [MemberDescription("中海(HK)")]
        ChinaShipHK,
        [MemberDescription("附页")]
        Attachment,
        [MemberDescription("大连提货单")]
        DLfcm,
    }

    class BLReportClientData : OIBLReportData
    {
        public string ETDString { get; set; }
        public string IssueDateString { get; set; }
        public string NumberOfOriginalString { get; set; }
    }
}
