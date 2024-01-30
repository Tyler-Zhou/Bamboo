using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.EDI.ServiceInterface;
using ICP.EDI.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ICP.EDI.UI
{
    /// <summary>
    /// EDI邮件发送窗体
    /// </summary>
    public partial class EDISendForm : XtraForm
    {
        #region 成员
        /// <summary>
        /// 配置选项
        /// </summary>
        public EDIConfigItem ConfigOption { get;set;}
        /// <summary>
        /// 发送选项
        /// </summary>
        public EDISendOption SendOption { get;set;}

        string rootUrl = string.Empty;
        string loginName = string.Empty;
        string password = string.Empty;
        string _serverConfigKey;
        string _subject;
        string Agent;
        /// <summary>
        /// 当前文档号
        /// </summary>
        string _documentNo = string.Empty;
        SendHead oSendHead = new SendHead();
        Http oHttp = new Http();
        /// <summary>
        /// EDI 模式
        /// </summary>
        EDIMode _ediMode { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        Guid _companyID { get; set; }
        /// <summary>
        /// 主ID(业务/MBL)
        /// </summary>
        Guid[] _ids { get; set; }
        /// <summary>
        /// 主号码(业务/MBL)
        /// </summary>
        string[] _nos { get; set; }
        /// <summary>
        /// 子ID(业务>>MBL/MBL>>HBL)
        /// </summary>
        Guid[] _fids { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        OperationType _jobType { get; set; }
        /// <summary>
        /// AMS
        /// </summary>
        AMSEntryType _amsEntryType { get; set; }
        /// <summary>
        /// ACI
        /// </summary>
        ACIEntryType _aciEntryType { get; set; }
        /// <summary>
        /// 船东ID
        /// </summary>
        public Guid CarrierID { get; set; }
        
        public string[] shipperFormat { get; set; }
        public string[] consigneeFormat { get; set; }
        public string[] shipperName { get; set; }
        public string[] consigneeName { get; set; }
        public string[] NotifyFormat { get; set; }
        public string[] NotifyName { get; set; }
        public string[] otherFormat { get; set; }
        public string[] goodinfoFormat { get; set; }
        public string[] markFormat { get; set; }
        /// <summary>
        /// 保存中海补料ID
        /// </summary>
        public Guid[] CSCLSIID { get; set; }
        #endregion

        #region 服务

        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        /// <summary>
        /// EDI服务
        /// </summary>
        public IEDIService EDIService
        {

            get
            {
                return ServiceClient.GetService<IEDIService>();
            }
        }
        #endregion

        #region 资源初始化与释放

        public EDISendForm()
        {
            InitializeComponent();

            if (!LocalData.IsDesignMode)
            {
                Load += new EventHandler(EDISendForm_Load);
                Disposed += new EventHandler(EDISendForm_Disposed);
            }
        }

        private void EDISendForm_Load(object sender, EventArgs e)
        {
            btnSend.Text = "&Send";
            btnClose.Text = "&Close";
            dtDate.DateTime = DateTime.Now;

            try
            {
                InitControls();
                gvMain.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(gvMain_CustomDrawRowIndicator);
                gvMain.IndicatorWidth = 30;
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowError(ex.Message);
                Close();
            }
        }

        void EDISendForm_Disposed(object sender, EventArgs e)
        {
            gcMain.DataSource = null;
            bsLogs.PositionChanged -= bsLogs_PositionChanged;
            bsLogs.DataSource = null;
            bsLogs.Dispose();
            gvMain.CustomDrawRowIndicator -= gvMain_CustomDrawRowIndicator;
            if (WorkItem != null)
            {
                WorkItem.Items.Remove(this);
                WorkItem = null;
            }
        }



        #endregion

        #region 事件处理
        /// <summary>
        /// 
        /// </summary>
        void gvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        void btnSend_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;

            int theradID = 0;
            theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Sending...");

            try
            {
                //修改文档标志
                EDIFlagType flagType = EDIFlagType.Original;
                if (rbCancel.Checked)
                {
                    flagType = EDIFlagType.Cancel;
                }
                else if (rbOriginal.Checked)
                {
                    flagType = EDIFlagType.Original;
                }
                else if (rbReplace.Checked)
                {
                    flagType = EDIFlagType.Replace;
                }

                if (SendOption != null)
                {
                    SendOption.Flag = flagType;
                }
                else
                {
                    SendOption = new EDISendOption()
                    {
                        ServiceKey = _serverConfigKey,
                        EdiMode = _ediMode,
                        CompanyID = _companyID,
                        SendByID = LocalData.UserInfo.LoginID,
                        IDs = _ids,
                        NOs = _nos,
                        FIDs = _fids,
                        FNOs = _nos,
                        Flag = flagType,
                        Subject = txtSubject.Text,
                        Content = txtContent.Text,
                        AMSEntryType = _amsEntryType,
                        ACIEntryType = _aciEntryType,
                        ShipperFormat = shipperFormat,
                        ShipperName = shipperName,
                        ConsigneeFormat = consigneeFormat,
                        ConsigneeName = consigneeName,
                        NotifyFormat = NotifyFormat,
                        NotifyName = NotifyName,
                        GoodinfoFormat = goodinfoFormat,
                        MarkFormat = markFormat,
                        OtherFormat = otherFormat,
                        CarrierID = CarrierID,
                        Agent = Agent,
                        AgentName = cmbAgent.Text,
                        Department = txtDepartment.Text,
                        Contact = txtContack.Text,
                        ContactTel = txtTel.Text,
                        ContactEmail = txtEmail.Text,
                        VGMRrmark = txtRemark.Text,
                        VGMDate = dtDate.DateTime.ToString("yyyy-MM-dd"),
                        VersionNo = 0,
                    };
                }

                if (_serverConfigKey.ToUpper() == EDITypeByCarrier.PIL_Booking.ToString().ToUpper())
                {
                    SendOption.PILCommodity = mcmbCommodity.EditValue.ToString();
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "正在发送...");
                //中海订舱返回DataSet
                if (_serverConfigKey == EDITypeByCarrier.CSCL_Booking_NorthChina.ToString())
                {
                    //返回订舱文件
                    DataSet ds = EDIService.SendCSCLBookingEDI(SendOption);
                    CSCLClientOperation(ds, SendOption.Flag, _serverConfigKey, _nos, true);
                }
                else if (_serverConfigKey == EDITypeByCarrier.CSCL_SI_NorthChina.ToString())
                {
                    //修改订舱文件
                    CSCLSIID = SendOption.FIDs;
                    SendOption.FIDs = SendOption.IDs;
                    SendOption.FNOs = SendOption.FNOs;
                    SendOption.EdiMode = EDIMode.Booking;
                    string subject = SendOption.Subject;
                    SendOption.Subject = "中海电子订舱";
                    SendOption.Flag = EDIFlagType.Replace;
                    SendOption.ServiceKey = EDITypeByCarrier.CSCL_Booking_NorthChina.ToString();
                    DataSet ds1 = EDIService.SendCSCLBookingEDI(SendOption);
                    CSCLClientOperation(ds1, EDIFlagType.Replace, EDITypeByCarrier.CSCL_Booking_NorthChina.ToString(), _nos, true);
                    //返回补料文件
                    SendOption.FIDs = CSCLSIID;
                    SendOption.Flag = flagType;
                    SendOption.EdiMode = EDIMode.SI;
                    SendOption.ServiceKey = EDITypeByCarrier.CSCL_SI_NorthChina.ToString();
                    SendOption.Subject = subject;
                    DataSet ds = EDIService.SendCSCLSIEDI(SendOption);
                    CSCLClientOperation(ds, SendOption.Flag, _serverConfigKey, _nos, true);
                }
                else
                {
                    if(SendOption.VersionNo>0)
                    {
                        EDIService.Send(SendOption,ConfigOption);
                    }
                    else
                    {
                        EDIService.SendEDI(SendOption);
                    }
                    DialogResult = DialogResult.OK;
                }
                Close();
            }
            catch (Exception appEx)
            {
                MessageBoxService.ShowError("Send failly!!!" + System.Environment.NewLine + appEx.Message, "Error");
            }
            finally
            {
                ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void cmbAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAgent.SelectedIndex < 0)
                return;

            Agent = cmbAgent.Properties.Items[cmbAgent.SelectedIndex].Value.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        void bsLogs_PositionChanged(object sender, EventArgs e)
        {
            if (bsLogs.Count == 0 || bsLogs.Current == null) return;

            LogData log = bsLogs.Current as LogData;
            if (log != null)
            {
                txtDescripton.Text = log.EDIContent;
            }
            else
            {
                txtDescripton.Text = "";
            }
        }

        /// <summary>
        /// 日志查看
        /// </summary>
        void linkLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (splitContainer1.PanelVisibility == DevExpress.XtraEditors.SplitPanelVisibility.Panel2)
            {
                linkLog.Text = "返回";

                splitContainer1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            }
            else
            {
                linkLog.Text = "日志";

                splitContainer1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region 本地方法

        /// <summary>
        /// 控件数据初始化
        /// </summary>
        void InitControls()
        {
            #region 初始化界面数据
            if (_ediMode == EDIMode.Unknown && SendOption != null)
            {
                _ediMode = SendOption.EdiMode;
                _ids = SendOption.IDs;
                _nos = SendOption.NOs;
                _fids = SendOption.FIDs;
                _amsEntryType = SendOption.AMSEntryType;
                _aciEntryType = SendOption.ACIEntryType;
                _serverConfigKey = SendOption.ServiceKey.IsNullOrEmpty() ? "" : SendOption.ServiceKey;
            }
            string nosStr = "";
            if (_nos != null && _nos.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string no in _nos)
                {
                    if (sb.Length > 0) sb.Append(",");
                    sb.Append(no);
                }
                nosStr = sb.ToString();
            }
            if (string.IsNullOrEmpty(_subject))
            {
                _subject = "";
                if (ConfigOption != null)
                {
                    _subject += ConfigOption.SubjectPrefix + " ";
                }
                _subject += EnumHelper.GetDescription(_ediMode, LocalData.IsEnglish);

                if (!nosStr.IsNullOrEmpty())
                {
                    _subject += "(" + nosStr + ")";
                }
            }
            else
            {
                if(SendOption!=null)
                    SendOption.Subject = _subject;
            }



            txtSubject.Text = _subject;

            List<LogData> logs = new List<LogData>();
            if (_ediMode == EDIMode.Booking)
            {
                logs = EDIService.GetLogList(_ids);
            }
            else
            {
                logs = EDIService.GetLogList(_fids);
            }



            if (logs == null) logs = new List<LogData>();
            bsLogs.DataSource = logs;

            if (logs.Count > 0)
            {
                int i = (from d in logs where d.EDIMode == _ediMode && (d.EDIFlag == EDIFlagType.Original || d.EDIFlag == EDIFlagType.Replace) select d).Count();

                if (i > 0)
                {
                    rbReplace.Checked = true;
                }
            }
            #endregion

            #region 初始化PIL品名
            if (_serverConfigKey.ToUpper() == EDITypeByCarrier.PIL_Booking.ToString().ToUpper())
            {
                List<EDIDictCodeList> ediDciList = ConfigureService.GetEDIDictCodeList(EDIDicType.PILCommodCode);
                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add("AMSName", "Commodity Description");
                mcmbCommodity.InitSource(ediDciList, col, "AMSName", "AMSCode");

                mcmbCommodity.ShowSelectedValue("470", "Toys");
            }
            else
            {
                txtContent.Height = txtContent.Height + 32;
                pnlPILCommod.Visible = false;
            }
            //是cosco中远补料，则隐藏取消edi选择项
            if (_serverConfigKey.ToUpper() == EDITypeByCarrier.COSCO_SI.ToString().ToUpper())
            {
                rbCancel.Visible = false;
            }

            if (_serverConfigKey != "NBEDIVGMANL")
            {
                panelVGM.Visible = false;
                txtContent.Height = txtContent.Height + 134;
            }
            if (!(_serverConfigKey == "NBEDIBookingANL" && _ediMode == EDIMode.SI))
            {
                if (_serverConfigKey != "NBEDIVGMANL")
                {
                    panelAange.Visible = false;
                    txtContent.Height = txtContent.Height + 32;
                }
            }
            else
            {
                cmbAgent.SelectedIndex = 0;
            }

            #endregion
        }

        /// <summary>
        /// 窗体数据验证
        /// </summary>
        /// <returns></returns>
        bool ValidateData()
        {
            bool isSucc = true;
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(txtSubject.Text.Trim()))
            {
                errorProvider1.SetError(txtSubject, Utility.IsEnglish ? "Must input" : "必须填写");
                isSucc = false;
            }
            if (_serverConfigKey.ToUpper() == EDITypeByCarrier.PIL_Booking.ToString().ToUpper())
            {
                if (mcmbCommodity.EditValue == null)
                {
                    errorProvider1.SetError(mcmbCommodity, Utility.IsEnglish ? "Commodity Must input" : "必须填写");
                    isSucc = false;
                }
            }

            return isSucc;
        }

        /// <summary>
        /// 根据发送选项给临时变量复制
        /// </summary>
        /// <param name="pSendOption"></param>
        void SetData(EDISendOption pSendOption)
        {
            _ediMode = pSendOption.EdiMode;
            _companyID = pSendOption.CompanyID;
            _ids = pSendOption.IDs;
            _fids = pSendOption.FIDs;
            _nos = pSendOption.NOs;
            _jobType = pSendOption.OperationType;
            _subject = pSendOption.Subject;
            _serverConfigKey = pSendOption.ServiceKey;
            _amsEntryType = pSendOption.AMSEntryType;
            _aciEntryType = pSendOption.ACIEntryType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds">数据源，保存成本地txt文件</param>
        /// <param name="type">原单/修改/取消</param>
        /// <param name="ediOperationType"></param>
        /// <param name="opOrMBLNo">订舱时：业务号 补料时：MBLNO</param>
        /// <param name="isGo">是否显示跳转到结果</param>
        private void CSCLClientOperation(DataSet ds, EDIFlagType type, string ediOperationType, string[] opOrMBLNo, bool isGo)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in ds.Tables["DataSource"].Rows)
            {
                sb.AppendLine(dr[0].ToString());
            }
            //自定义EDI文件名
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(1000, 9999) + ".txt";

            //创建文件夹,存放订舱txt文件
            string folderPath = System.Windows.Forms.Application.StartupPath + "\\CSCLBookingFile";
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }
            //保存为txt
            string filePath = folderPath + "\\" + fileName;
            if (File.Exists(filePath))//有重复的，更新文件名
            {
                fileName = DateTime.Now.ToString("YYYYMMDDHHmmss") + new Random().Next(1000, 9999) + ".txt";
                filePath = folderPath + "\\" + fileName;
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                byte[] data = Encoding.Default.GetBytes(sb.ToString());
                fs.Write(data, 0, data.Length);
                fs.Close();
            }

            rootUrl = ds.Tables["CSCL"].Rows[0]["CSCLWebURL"].ToString();
            loginName = ds.Tables["CSCL"].Rows[0]["CSCLLoginName"].ToString();
            password = ds.Tables["CSCL"].Rows[0]["CSCLPassword"].ToString();

            //订舱Url
            string goBookingAddUrl = rootUrl + "ebooking/ediorderdo.jsp";
            //补料Url
            string goSIAddUrl = rootUrl + "eloading/ediloadingdo.jsp";
            //更改订舱URL
            string goBookingUpdateUrl = rootUrl + "ebooking/edichangedo.jsp";
            //登录

            if (LocalData.UserInfo.DefaultCompanyID == new Guid("62D46581-B6CC-477E-8A60-7375FACD9813"))
            {
                rootUrl = rootUrl + "login.jsp";
            }

            oHttp.Login(ref oSendHead, rootUrl, loginName, password);

            //订舱
            if (ediOperationType == EDITypeByCarrier.CSCL_Booking_NorthChina.ToString())
            {

                if (type == EDIFlagType.Original)
                {
                    DoCSCLNewBooking(goBookingAddUrl, filePath, opOrMBLNo);
                }
                else if (type == EDIFlagType.Replace)
                {
                    DoReplaceCSCLBooking(goBookingUpdateUrl, filePath, opOrMBLNo, isGo);
                }
            }
            //补料
            else if (ediOperationType == EDITypeByCarrier.CSCL_SI_NorthChina.ToString())
            {
                DoCSCLSI(goSIAddUrl, filePath, opOrMBLNo);
            }

        }
        /// <summary>
        /// 中海补料
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filePath"></param>
        /// <param name="mblNos"></param>
        private void DoCSCLSI(string url, string filePath, string[] mblNos)
        {
            string result = InnerSend(string.Empty, false, true, url, filePath);
            if (string.IsNullOrEmpty(result))
            {
                DialogResult dialogResult = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "SI Sucessfully,Do you want to check?" : "补料成功,是否检查?", "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    ShowECheckForm(mblNos, rootUrl, loginName, password, EDITypeByCarrier.CSCL_SI_NorthChina, 1);
                }
            }
            else
            {
                throw new ICPException(result);
            }
        }
        /// <summary>
        /// 显示结果页面
        /// </summary>
        /// <param name="nos"></param>
        /// <param name="rootUrl"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="ediType"></param>
        private void ShowECheckForm(string[] nos, string rootUrl, string userId, string password, EDITypeByCarrier pEDITypeByCarrier, int sendType)
        {
            CSCLWeb frmWeb = new CSCLWeb();
            frmWeb.OpOrMBLNos = nos;
            frmWeb.UID = userId;
            frmWeb.Pwd = password;
            frmWeb.Path = rootUrl;
            frmWeb.FEDITypeByCarrier = pEDITypeByCarrier;
            frmWeb.oHttp = oHttp;
            frmWeb.oSendHead = oSendHead;
            frmWeb.SendType = sendType;
            frmWeb.EDIService = EDIService;
            frmWeb.ShowDialog();
        }
        /// <summary>
        /// 实际发送中海订舱或补料,返回错误信息
        /// </summary>
        /// <param name="ediImp"></param>
        /// <param name="needCarrier"></param>
        /// <param name="needHoldOffice"></param>
        /// <param name="url"></param>
        /// <param name="filePath"></param>
        /// <returns>不为空，则表示发送错误，否则为成功</returns>
        private string InnerSend(string ediImp, bool needCarrier, bool needHoldOffice, string url, string filePath)
        {
            NameValueCollection collection = GetPostNameValueCollection(ediImp, needCarrier, needHoldOffice);
            string resultHtml = HttpUploadFile(url, filePath, "file1", "text/plain", collection);
            string result = InspectResult(url, resultHtml);
            return result;
        }
        /// <summary>
        /// 中海更改订舱
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filePath"></param>
        /// <param name="operationNos"></param>
        /// <param name="isGo">是否显示跳转</param>
        private void DoReplaceCSCLBooking(string url, string filePath, string[] operationNos, bool isGo)
        {
            string result = InnerSend(string.Empty, false, false, url, filePath);
            if (string.IsNullOrEmpty(result) && isGo)
            {
                DialogResult dialogResult = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Replace booking Sucessfully,Do you want to check?" : "订舱更改成功,是否检查?", "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    ShowECheckForm(operationNos, rootUrl, loginName, password, EDITypeByCarrier.CSCL_Booking_NorthChina, 0);
                }
            }
            else
            {
                throw new ICPException(result);
            }
        }
        /// <summary>
        /// 中海新增订舱
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filePath"></param>
        /// <param name="operationNos"></param>
        private void DoCSCLNewBooking(string url, string filePath, string[] operationNos)
        {
            string result = InnerSend("Y", true, true, url, filePath);
            if (string.IsNullOrEmpty(result))
            {

                DialogResult dialogResult = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Booking Sucessfully,the booking is in draft,Do you want to check?" : "订舱成功,当前订舱为草稿状态,是否检查?", "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    ShowECheckForm(operationNos, rootUrl, loginName, password, EDITypeByCarrier.CSCL_Booking_NorthChina, 0);
                }

            }
            else
            {
                throw new ICPException(result);
            }
        }
        private NameValueCollection GetPostNameValueCollection(string ediImp, bool needCarrier, bool needHoldOffice)
        {

            NameValueCollection collection = new NameValueCollection();
            collection.Add("file6", string.Empty);
            collection.Add("file5", string.Empty);
            collection.Add("file4", string.Empty);
            collection.Add("file3", string.Empty);
            collection.Add("file2", string.Empty);
            if (needHoldOffice)
            {
                string hold_office = GetHoldOffice();
                collection.Add("hold_office", hold_office);
            }
            if (needCarrier)
            {
                string carrier = "CSC";
                collection.Add("CARRIER", carrier);
            }
            collection.Add("ediImp", ediImp);
            return collection;


        }
        /// <summary>
        /// 获取中海订舱办代码
        /// </summary>
        /// <returns></returns>
        private string GetHoldOffice()
        {
            string companyID = LocalData.UserInfo.DefaultCompanyID.ToString().ToLower();
            //大连
            if (companyID == "B1AFAD8F-55DD-4E29-A250-EB82AB3971FE".ToLower())
                return "CSCLDLC";
            //青岛
            else if (companyID == "F289109A-C29E-4B0B-A41A-C22D9E70A72F".ToLower())
                return "CSCLTAO";
            //连云港
            else if (companyID == "62D46581-B6CC-477E-8A60-7375FACD9813".ToLower())
                return "CSCLLYG";
            //宁波
            else if (companyID == "a62a9f8e-e69c-4e6e-ad85-e75aed3c6cf9".ToLower())
                return "CSCLNGB";
            //武汉
            else if (companyID == "FEC967BF-275B-453B-AA3B-E7730C907272".ToLower())
                return "CSCLSHA";
            //上海
            else if (companyID == "B13FAC2D-8250-4990-A622-5ECA00D3A030".ToLower())
                return "CSCLSHA";
            //重庆
            else if (companyID == "57C0032C-572C-48B2-AD2D-75458F2F8B63".ToLower())
                return "CSCLCKG";
            //天津
            else if (companyID == "D8D57403-D663-4A93-A927-144907B7963B".ToLower())
                return "CSCLTJ";
            return string.Empty;
        }


        public void SetData(string serverConfigKey, EDIMode ediMode, Guid companyID, string subject, Guid[] oIds, Guid[] fIds, string[] mblNos, OperationType jobType, AMSEntryType amsEntryType, ACIEntryType aciEntryType)
        {
            _ediMode = ediMode;
            _companyID = companyID;
            _ids = oIds;
            _fids = fIds;
            _nos = mblNos;
            _jobType = jobType;
            _subject = subject;
            _serverConfigKey = serverConfigKey;
            _amsEntryType = amsEntryType;
            _aciEntryType = aciEntryType;
        }
        #endregion


        
        public string HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection nvc)
        {
            string temp = string.Empty;
            string boundary = DateTime.Now.Ticks.ToString("x");
            string boundaryString = "\r\n--" + boundary + "\r\n";
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes(boundaryString);
            Uri url2 = new Uri(url);
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Headers.Add("Accept-Language", "zh-CN");
            wr.Accept = "application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
            wr.Method = "POST";
            wr.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; CIBA; .NET CLR 2.0.50727; .NET CLR 3.0.04506.590; .NET CLR 3.5.20706)";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;
            wr.CookieContainer = new CookieContainer();

            if (oSendHead.Cookies != null)
            {
                string cookieHeader = "";
                foreach (Cookie cookie in oSendHead.Cookies)
                {
                    string str5 = cookieHeader;
                    cookieHeader = str5 + cookie.Name + "=" + cookie.Value + ",";
                }
                wr.CookieContainer.SetCookies(new Uri(oSendHead.Host), cookieHeader);
            }
            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            int i = 0;
            foreach (string key in nvc.Keys)
            {
                if (i == 0)
                {
                    string beginBoundaryString = "--" + boundary + "\r\n";
                    byte[] beginBoundarybytes = System.Text.Encoding.ASCII.GetBytes(beginBoundaryString);
                    rs.Write(beginBoundarybytes, 0, beginBoundarybytes.Length);
                }
                else
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                }
                string formitem = string.Format(formdataTemplate, key, nvc[key]);

                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
                i++;
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, Path.GetFileName(file), contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);

            rs.Close();

            HttpWebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse() as HttpWebResponse;
                oSendHead.Cookies = wresp.Cookies;
                Stream stream2 = wresp.GetResponseStream();
                string characterSet = wresp.CharacterSet;
                StreamReader reader2 = new StreamReader(stream2, Encoding.GetEncoding(characterSet));
                return reader2.ReadToEnd();


            }
            catch (Exception ex)
            {
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
                throw ex;
            }
            finally
            {

                wr = null;
            }
        }

        private string InspectResult(string url, string resultHtml)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(resultHtml);

            HtmlAgilityPack.HtmlNode tableNode = doc.DocumentNode.SelectSingleNode("//table[@id=\"data_tableinfo\"]");

            List<HtmlAgilityPack.HtmlNode> trCollection = tableNode.Descendants("tr").ToList();
            if (trCollection[1].Descendants("td").Count() != 3)
            {
                throw new ICPException(string.Format("订舱网站:{0}已发生变化，订舱失败，请立即联系系统管理员!", url));
            }
            string result = string.Empty;
            for (int i = 1; i < trCollection.Count; i++)
            {
                IEnumerable<HtmlAgilityPack.HtmlNode> childNodes = trCollection[i].Descendants("td");

                if (childNodes.ElementAt(1).InnerText.Contains("错误") || childNodes.ElementAt(1).InnerText.Contains("失败"))
                {
                    result += childNodes.ElementAt(2).InnerText + Environment.NewLine;
                }

            }
            return result;
        }

        #region Comment code
        ///// <summary>
        ///// GetXMLList
        ///// </summary>
        ///// <param name="xmlStr">XML字符串</param>
        ///// <returns>List</returns>
        //private List<CSCLBookingConfig> GetCSCLBookingConfigListByXMLStr(string xmlStr)
        //{
        //    List<CSCLBookingConfig> CSCLBookingConfigList = null;
        //    if (!string.IsNullOrEmpty(xmlStr))
        //    {
        //        XmlDocument xmlDoc = new XmlDocument();
        //        byte[] bs = Encoding.UTF8.GetBytes(xmlStr);
        //        using (MemoryStream ms = new MemoryStream(bs))
        //        {
        //            xmlDoc.Load(ms); //加载XML文档   
        //            XmlNodeList list = xmlDoc.SelectSingleNode("Customers").ChildNodes;
        //            //Console.WriteLine(list.Count);
        //            if (list != null && list.Count > 0)
        //            {
        //                CSCLBookingConfigList = new List<CSCLBookingConfig>();
        //                foreach (XmlNode node in list)
        //                {
        //                    CSCLBookingConfig cfg = new CSCLBookingConfig();
        //                    cfg.ID = new Guid((node["ID"]).InnerText);
        //                    cfg.Path = (node["Path"]).InnerText;
        //                    cfg.Name = (node["Name"]).InnerText;
        //                    cfg.Pwd = (node["Pwd"]).InnerText;

        //                    CSCLBookingConfigList.Add(cfg);
        //                }
        //            }
        //        }
        //    }
        //    return CSCLBookingConfigList;
        //}
        #endregion
    }
    /// <summary>
    /// 自定义的Web请求头部信息类
    /// </summary>
    public struct SendHead
    {
        public string Host;
        public string Referer;
        public CookieCollection Cookies;
        public string Action;
        public string PostData;
        public string Method;
        public string ContentType;
        public string Html;
        public string AcceptLanguage;
        public string EncodeName;
        public string Port;
    }
    /// <summary>
    /// 发送Web请求辅助类
    /// </summary>
    public class Http
    {
        public CookieCollection Cookies = null;
        public void Login(ref SendHead oSendHead, string rootUrl, string loginName, string password)
        {
            oSendHead.Cookies = null;

            Uri actionUrl = new Uri(rootUrl);
            oSendHead.Method = "POST";
            oSendHead.Referer = rootUrl;
            oSendHead.Host = "http://" + actionUrl.Host;//主机
            if (rootUrl.IndexOf("https://") >= 0) //网址
                oSendHead.Host = "https://" + actionUrl.Host;
            if (actionUrl.Port != 80) //主机+端口
                oSendHead.Host = oSendHead.Host + ":" + actionUrl.Port;
            oSendHead.Action = actionUrl.PathAndQuery;//
            oSendHead.AcceptLanguage = "zh-cn";
            oSendHead.PostData = string.Format("userid={0}&password={1}&B1=%B5%C7%C2%BC&flag=eAdminLogin", loginName, password);
            Send(ref oSendHead);
            oSendHead.Cookies = Cookies;
            if (oSendHead.Html.Contains("登录失败"))
            {
                throw new ApplicationException(string.Format("登录 {0} 失败，请检查账号和密码是否正确!", oSendHead.Host + oSendHead.Action));
            }

        }
        /// <summary>
        /// 发送web请求，并返回请求结果文本
        /// </summary>
        /// <param name="oHead"></param>
        /// <returns></returns>
        public string Send(ref SendHead oHead)
        {
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            for (int i = 0; i < 3; i++)
            {
                request = (HttpWebRequest)WebRequest.Create(oHead.Host + oHead.Action);
                request.ProtocolVersion = new Version("1.1");
                request.Referer = oHead.Referer;

                if (!string.IsNullOrEmpty(oHead.AcceptLanguage))
                {
                    request.Headers.Add("Accept-Language", oHead.AcceptLanguage);
                }
                request.Headers.Add("UA-CPU", "x86");
                request.Accept = " image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/xaml+xml, application/vnd.ms-xpsdocument, application/x-ms-xbap, application/x-ms-application, */*";
                request.CookieContainer = new CookieContainer();
                request.Timeout = 0xafc8;
                if (oHead.Cookies != null)
                {
                    string cookieHeader = "";
                    foreach (Cookie cookie in oHead.Cookies)
                    {
                        string str5 = cookieHeader;
                        cookieHeader = str5 + cookie.Name + "=" + cookie.Value + ",";
                    }
                    request.CookieContainer.SetCookies(new Uri(oHead.Host), cookieHeader);
                }
                //response.ContentEncoding



                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; CIBA; .NET CLR 2.0.50727; .NET CLR 3.0.04506.590; .NET CLR 3.5.20706)";
                request.Credentials = CredentialCache.DefaultCredentials;
                request.IfModifiedSince = DateTime.MinValue;
                if (oHead.Method.ToLower() == "post")
                {
                    byte[] bytes;
                    request.Method = "POST";
                    if ((oHead.EncodeName == null) || (oHead.EncodeName.Length == 0))
                    {
                        bytes = Encoding.ASCII.GetBytes(oHead.PostData);
                    }
                    else
                    {
                        bytes = Encoding.GetEncoding(oHead.EncodeName).GetBytes(oHead.PostData);
                    }
                    if (string.IsNullOrEmpty(oHead.ContentType))
                    {
                        request.ContentType = "application/x-www-form-urlencoded";
                    }
                    else
                    {
                        request.ContentType = oHead.ContentType;
                    }
                    request.ContentLength = bytes.Length;
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                try
                {
                    response = (HttpWebResponse)request.GetResponse();

                    break;
                }
                catch (WebException exception)
                {
                    if (exception.Response != null)
                    {
                        response = (HttpWebResponse)exception.Response;
                        break;
                    }
                    if (i == 2)
                    {
                        throw exception;
                    }
                }
            }
            CookieCollection cookies = request.CookieContainer.GetCookies(request.RequestUri);
            foreach (Cookie cookie in cookies)
            {
                if (response.Cookies[cookie.Name] == null)
                {
                    response.Cookies.Add(cookie);
                }
            }
            Stream responseStream = response.GetResponseStream();
            string characterSet = response.CharacterSet;
            if ((characterSet == null) || (characterSet.Length == 0))
            {
                characterSet = "gb2312";
            }
            if (characterSet.ToLower() == "iso-8859-1" || characterSet.ToUpper() == "MS949")
            {
                characterSet = "gb2312";
            }
            if (!string.IsNullOrEmpty(oHead.EncodeName))
            {
                characterSet = oHead.EncodeName;
            }
            StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding(characterSet));
            string str3 = reader.ReadToEnd();
            Cookies = response.Cookies;
            oHead.Cookies = response.Cookies;
            reader.Close();
            responseStream.Close();
            response.Close();
            return (oHead.Html = str3);
        }
    }

}
