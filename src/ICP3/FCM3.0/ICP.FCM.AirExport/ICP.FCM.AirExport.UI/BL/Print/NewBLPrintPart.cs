using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ICP.Common.UI.ReportView;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.Common.UI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.FCM.AirExport.ServiceInterface.DataObjects.ReportObjects;
using ICP.Common.ServiceInterface.DataObjects;
using System.ComponentModel;
using ICP.OA.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;

namespace ICP.FCM.AirExport.UI.BL
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class NewBLPrintPart : CompositeReportViewPart
    {
        public NewBLPrintPart()
        {
            InitializeComponent();
        }
        #region Service

        [ServiceDependency]
        public IAirExportService aeService { get; set; }

        [ServiceDependency]
        public IConfigureService configureService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelper { get; set; }

        #endregion
        bool needCloseBL = false;
        protected override void Locale()
        {
            base.Locale();
            if (!LocalData.IsEnglish)
            {
                labStyle.Text = "样式";
                labTitle.Text = "抬头";
                groupStyle.Text = "样式";
            }
        }
        #region cmbSelectIndexChanged

        void cmbReportStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReportStyle.EditValue == null) return;
            BLReportStly blReportStly = (BLReportStly)(Enum.Parse(typeof(BLReportStly), cmbReportStyle.EditValue.ToString()));
            if (blReportStly == BLReportStly.Original)
            {
                //1):正本提单时候关单. 
                needCloseBL = true;
            }
            else
            {
                //2):打印副本的时候，可以更改打印标题.
                needCloseBL = false;
            }
        }

        #endregion

        protected override void LoadData()
        {
            InitControls();
            this.imageComboBoxEdit1.SelectedIndex = 0;
            this.imageComboBoxEdit1.SelectedIndexChanged += new EventHandler(imageComboBoxEdit1_SelectedIndexChanged);
        }
        private void InitControls()
        {
            this.imageComboBoxEdit1.Properties.Items.Clear();
            this.imageComboBoxEdit1.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("LOGO_AEC.gif", 0));
            this.imageComboBoxEdit1.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("LOGO_CITYOCEAN.gif", 1));
            this.imageComboBoxEdit1.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("LOGO_MANDRIN.gif", 2));
            this.imageComboBoxEdit1.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("LOGO_TOPSHIPPING.gif", 3));
            SetCmbReportStyle();
            SetCmbCompany();

            this.imageComboBoxEdit1.SelectedIndexChanged += new EventHandler(imageComboBoxEdit1_SelectedIndexChanged);

        }
        List<ConfigureList> _configure = null;
        private void SetCmbCompany()
        {
            //公司 配置里的公司列表
            _configure = ICPCommUIHelper.SetCmbConfigureCompany(cmbCompany);
            cmbCompany.SelectedIndex = 0;
        }
        string getFileNameByCmb(BLReportStly blReportStly)
        {
            string fileName = string.Empty;

            if (blReportStly == BLReportStly.Original)
                fileName += "AirExportBLOriginalReport.frx";//正本提单
            else if (blReportStly == BLReportStly.Copy)//副本提单
                fileName += "AirExportBLCopyReport.frx";
            else
                return string.Empty;

            if (string.IsNullOrEmpty(fileName) == false)
                fileName = System.Windows.Forms.Application.StartupPath + "\\Reports\\AirExport\\" + fileName;

            return fileName;
        }
        private static string imageName = "";
        string pivPath = Application.StartupPath + "\\Reports\\LOGO\\" + imageName;
        void imageComboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.imageComboBoxEdit1.SelectedIndex == 0) { imageName = "1.gif"; }
            else if (imageComboBoxEdit1.SelectedIndex == 1) { imageName = "2.gif"; }
            else if (imageComboBoxEdit1.SelectedIndex == 2) { imageName = "3.gif"; }
            else { imageName = "4.gif"; }
            pivPath = Application.StartupPath + "\\Reports\\LOGO\\" + imageName;
            this.pictureBox1.Image = Image.FromFile(pivPath);
        }
        protected override void AfterPrint()
        {
            if (needCloseBL == false || _AirBLList.State == AEBLState.Checked) return;

            try
            {
                SingleResult result;
                if (_AirBLList.BLType == BLType.MAWB)
                    result = aeService.ChangeAirMBLState(_AirBLList.ID, AEBLState.Checked, LocalData.UserInfo.LoginID, _AirBLList.UpdateDate);
                else
                    result = aeService.ChangeAirMBLState(_AirBLList.ID, AEBLState.Checked, LocalData.UserInfo.LoginID, _AirBLList.UpdateDate);

                this._AirBLList.State = AEBLState.Checked;
                _AirBLList.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }
        #region IPart 成员
        AirBLList _AirBLList = null;
        public override void Init(IDictionary<string, object> values)
        {
            _AirBLList = ICP.FCM.Common.UI.Utility.GetValue("AirBLList", values) as AirBLList;
        }
        #endregion
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
        protected override void Query()
        {
            Print();
        }
        private void Print()
        {
            btnQuery.Enabled = false;
            if (cmbReportStyle.EditValue == null) return;

            BLReportStly blReportStly = (BLReportStly)Enum.Parse(typeof(BLReportStly), cmbReportStyle.EditValue.ToString());
            string reportFilePath = getFileNameByCmb(blReportStly);

            try
            {
                BLReportData_HBL HBL = aeService.GetMBLReportData(_AirBLList.ID, LocalData.IsEnglish, _AirBLList.BLType);
                BLReportClientData data = new BLReportClientData();

                //#endregion
                Utility.CopyToValue(HBL, data, typeof(BLReportClientData));

                #region 1.提单份数,电放为ZERO(0),其它为 THREE(3); 2.电放\DRAFT ONLY\O B/L REQUIER的字符设置

                //HBL.NumberOfOriginalString = "THREE(3)";

                //获取公司客户名称
                string customerName = string.Empty;
                if (cmbCompany.EditValue != null)
                {
                    Guid companyId = new Guid(cmbCompany.EditValue.ToString());
                    ConfigureInfo configuraInfo = configureService.GetCompanyConfigureInfo(companyId, true);
                    customerName = configuraInfo.CustomerName;
                }

                if (blReportStly == BLReportStly.Original)
                {
                    HBL.Title = string.Empty;
                }
                else if (blReportStly == BLReportStly.Copy)
                {
                    HBL.Title = customerName;
                }

                HBL.Notify = customerName;

                #endregion

                #region 抬头
                //if (blReportStly != BLReportStly.Original) HBL.Header = cmbHead.Text;
                #endregion

                #region 根据选择的公司生成公司信息
                //BulidCompanyInfo(HBL);
                #endregion

                if (HBL.BLType == BLType.MAWB)
                {
                    HBL.HAWBNO = string.Empty;
                }

                //if (HBL.ETD != null)
                //{
                //    HBL.ETDString = HBL.ETD.Value.ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                //}
                HBL.LogoName = pivPath;//LOGO图标地址
                BindingSource bs = new BindingSource();

                //特殊情况特殊处理
                if (HBL.RateCharge == "0.00")
                {
                    HBL.RateCharge = "AS ARRANGED";
                }
                if (HBL.Total == "0.00")
                {
                    HBL.Total = "AS ARRANGED";
                }
                HBL.TranshipmentPort1By = string.Empty;


                bs.DataSource = HBL;

                Dictionary<string, object> reportSource = new Dictionary<string, object>();
                reportSource.Add("HBL", bs);
                reportViewer.BindData(reportFilePath, reportSource, null, GetOperationInfo());
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
            finally { btnQuery.Enabled = true; }
        }
        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (_AirBLList == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.AirExport;
            message.UserProperties.OperationId = _AirBLList.AirBookingID;
            if (_AirBLList.BLType == BLType.HAWB)
            {
                message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.HBL;
                message.UserProperties.FormId = _AirBLList.ID;
            }
            else
            {
                message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.MBL;
                message.UserProperties.FormId = _AirBLList.ID;
            }

            return message;
        }
    }
    enum BLReportStly
    {
        [MemberDescription("正本")]
        Original = 0,
        [MemberDescription("副本")]
        Copy,
    }

    class BLReportClientData : BLReportData_HBL
    {
    }
}
