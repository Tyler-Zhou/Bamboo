using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using System.Text.RegularExpressions;
using ICP.DataCache.ServiceInterface;
using DevExpress.XtraEditors;
using ICP.FCM.Common.UI.Common.Parts;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.OceanImport.ServiceInterface;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Message.ServiceInterface;
using ICP.Sys.ServiceInterface;
using System.Globalization;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FCM.Common.UI.Document
{
    /// <summary>
    /// 
    /// </summary>
    [Obsolete("类已过时，请使用 FrmSendAgentDocumentNew 代替")]
    public partial class FrmSendAgentDocument : BaseEditPart, IDisposable
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 代理文档分发服务
        /// </summary>
        public IAgentDocumentDispatchService AgentDocumentDispatchService
        {
            get
            {
                return ServiceClient.GetService<IAgentDocumentDispatchService>();
            }
        }

        /// <summary>
        /// 用户管理服务
        /// </summary>
        public ICP.Sys.ServiceInterface.IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<ICP.Sys.ServiceInterface.IUserService>();
            }
        }

        /// <summary>
        /// FCM公共服务
        /// </summary>
        public IOperationAgentService OperationAgentService
        {
            get
            {
                return ServiceClient.GetService<IOperationAgentService>();
            }
        }

        public IClientFileService BusinessFileService
        {
            get
            {
                return ServiceClient.GetService<IClientFileService>();
            }
        }

        public IOceanImportService oiService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }

        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        /// <summary>
        /// 海出公共服务
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }

        public IMessageService _messageService
        {
            get
            {
                return ServiceClient.GetService<IMessageService>();
            }
        }

        public IUserService _userService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        #endregion
        #region 字段及属性
        private bool isOverseasSalesListFilled = false;

        public int workflag = 1; // 工作标示 1 分文件 2 查看分文件内容 
        public int operationType = 1; //业务类型 1 进口 2 出口
        public Guid dispatchLogid;
        /// <summary>
        /// 操作ID
        /// </summary>
        public Guid OperationId { get; set; }

        /// <summary>
        /// 业务代理信息
        /// </summary>
        public OperationAgentInfo AgentInfo { get; set; }

        /// <summary>
        ///文档列表数据源
        /// </summary>
        public List<DocumentInfo> DocumentInfoDataSource { get; set; }

        /// <summary>
        /// 上次选择文档列表id
        /// </summary>
        List<Guid?> DocumentlistHistoryIds = new List<Guid?>();

        /// <summary>
        ///选择的文档主键Ids
        /// </summary>
        List<Guid?> DispatchFileIDs = new List<Guid?>();

        /// <summary>
        /// 本次选择文档列表id
        /// </summary>
        List<Guid> DocumentlistSelectIds = new List<Guid>();

        /// <summary>
        /// 文档更新时间集合
        /// </summary>
        List<DateTime?> OperationFileUpdateDates = new List<DateTime?>();

        DocumentDispatchSelectListInfo DispatchSelectListInfo = new DocumentDispatchSelectListInfo();

        /// <summary>
        /// 日志信息
        /// </summary>
        public string strDescription { get; set; }


        /// <summary>
        /// 分文档信息
        /// </summary>
        public DocumentDispatchContainerObjects DocumentDispatchData { get; set; }

        /// <summary>
        /// 业务操作上下文类
        /// </summary>
        private BusinessOperationContext OperationContext;


        private BusinessOperationContext ContextHistory;

        public delegate void Dipacth();
        public Dipacth begenDis;
        public bool isSuccess = false;
        #endregion

        /// <summary>
        /// 美加线
        /// </summary>
        public static List<Guid> NAShippingLines
        {
            get
            {
                List<Guid> idlist = new List<Guid>();

                idlist.Add(new Guid("91B68E82-32A9-4A41-8309-20DE34893C25"));//加拿大
                idlist.Add(new Guid("6F51BA0E-397C-4AF8-A453-617B1051E76B"));//美西
                idlist.Add(new Guid("8F09FD42-3BBA-4EA9-BB5B-80E53770CA84"));//北美区
                idlist.Add(new Guid("FC4361F1-FF7A-4B57-B411-99E106D1B7C0"));//美国航线
                idlist.Add(new Guid("E2D05D39-B9A2-4C7D-838E-C6FA466609EE"));//美东
                return idlist;
            }
        }

        /// <summary>
        ///绑定数据委托
        /// </summary>
        private delegate void BindDataDelegate();

        List<UserList> _list;
        public List<UserList> DataList
        {
            get
            {
                if (_list == null)
                    _list = UserService.GetOverseasSalesList("海外客服");
                return _list;
            }
        }

        #region 构造函数
        public FrmSendAgentDocument()
        {
            InitializeComponent();

            this.Disposed += delegate
            {
                this.Saved = null;

                if (Workitem != null)
                    Workitem.Items.Remove(this);
            };
            if (!DesignMode)
            {
                if (!LocalData.IsEnglish) SetCnText();
            }
        }
        #endregion

        #region 事件处理

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {

                //this.toolTipController1.SetToolTip(this.btnSend, "请选择一条记录!");
                OperationContext = this.DataSource as BusinessOperationContext;
                //业务ID
                OperationId = OperationContext.OperationID;
                DocumentDispatchData = OperationAgentService.GetOperationDocumentDispatchData(OperationId);
                operationType = (int)OperationContext.OperationType;
                LoadControl();
                //设置控件状态及数据源
                SetControlState();
                ShowToolTip();
                //begenDis = new Dipacth(strat);
                //this.ucDocumentListDispatch.ShowControlCheckState = true;
            }
        }

        void LoadControl()
        {
            ucDocumentListDispatch = Workitem.Items.AddNew<UCDocumentDispatchPartNew>();
            ucDocumentListDispatch.Dock = DockStyle.Fill;
            ucDocumentListDispatch.ShowCheckControl = true;
            ucDocumentListDispatch.IsCurrentUpdateHide = false;
            DocumentDispatchContainer.Controls.Clear();
            DocumentDispatchContainer.Controls.Add(ucDocumentListDispatch);

            int state = OperationAgentService.GetDispatchState(OperationId);
            if (state != null && state == 2)
            {
                btnSend.Enabled = false;
                string message = LocalData.IsEnglish ? "Dispatching agent fees is in progress…" : "正在分发文件中…";
                SetLabelMessage(Color.Red, message);
            }
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        /// <summary>
        /// 代理单选按钮属性改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoAgent_CheckedChanged(object sender, EventArgs e)
        {
            this.txtSendAgent.Enabled = this.rdoAgent.Checked;
            ClearValidate();
        }

        /// <summary>
        /// 海外客服单选按钮属性改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoOverseas_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = this.rdoOverseas.Checked;
            lstboxOverseas.Enabled = isChecked;
            ClearValidate();
            if (isChecked && !isOverseasSalesListFilled)
            {

                UserList insertItem = new UserList();
                insertItem.ID = Guid.Empty;
                insertItem.EName = insertItem.CName = string.Empty;
                DataList.Insert(0, insertItem);

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("EMail", LocalData.IsEnglish ? "Email" : "邮箱");

                lstboxOverseas.InitSource<UserList>(DataList, col, LocalData.IsEnglish ? "EName" : "CName", "EMail");
                isOverseasSalesListFilled = true;
            }
        }

        /// <summary>
        /// 向代理分发文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            int state = OperationAgentService.UspGetDispatchLogState(OperationId);
            if (state == 2 || state == 3)
            {
                DispathLogData newDispathLog = OperationAgentService.GetDispatchFileLogByOperation(OperationId);
                DateTime dt = DateTime.ParseExact(newDispathLog.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
                dt = dt.ToLocalTime();
                string message = LocalData.IsEnglish ? "ICP is dispatching the D/C fees at " + (dt.ToString("yyyy-MM-dd HH:mm:ss")) + "\n\t Do you want to re-dispatch it" : "ICP正在处理在（" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "）做的分发\n\t确定需要重新分发？";
                DialogResult result = MessageBox.Show(message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            if (OperationContext.OperationType == OperationType.OceanImport || OperationContext.OperationType == OperationType.AirImport)
            {
                try
                {
                    this.Visible = false;
                    this.Parent.Visible = false;
                    WaitCallback callback = data =>
                    {
                        strat();
                    };
                    ThreadPool.QueueUserWorkItem(callback);
                    MethodBase method = MethodBase.GetCurrentMethod();
                    StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "DISPATCH", string.Format("分发文件;OperationID[{0}]", OperationId));
                }
                catch (Exception ex)
                {
                    string strmessage = "";
                    strmessage = ClientHelper.GetErrorMessage(ex);
                    XtraMessageBox.Show(strmessage, "Tips", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                if (!ValidateData())
                    return;

                object objagent = null;

                if (DocumentDispatchData.DispatchInfo == null)
                {
                    DocumentDispatchData.DispatchInfo = new DocumentDispatchInfo();
                    DocumentDispatchData.DispatchInfo.IsAgainDispatch = false;
                }
                else
                {
                    DocumentDispatchData.DispatchInfo.IsAgainDispatch = true;
                }

                //是否内部代理
                if (DocumentDispatchData.AgentInfo.IsBranch)
                {
                    objagent = DocumentDispatchData.AgentInfo.AgentID as object;
                    DocumentDispatchData.DispatchInfo.RecipientType = RecipientType.Branch;
                }
                else
                {
                    if (rdoAgent.Checked == true)
                    {
                        objagent = txtSendAgent.Text as object;
                        DocumentDispatchData.DispatchInfo.RecipientType = RecipientType.External;
                        DocumentDispatchData.DispatchInfo.AgentMail = txtSendAgent.Text.Trim();
                    }
                    else
                    {
                        objagent = lstboxOverseas.EditValue;
                        DocumentDispatchData.DispatchInfo.RecipientType = RecipientType.OverseasCS;
                        DocumentDispatchData.DispatchInfo.AgentMail = lstboxOverseas.EditValue.ToString();

                        UserList info = DataList.Find(o => o.EMail == lstboxOverseas.EditValue.ToString());
                        if (info != null)
                        {
                            DocumentDispatchData.DispatchInfo.OverseasCSID = info.ID;
                        }

                        DocumentDispatchData.DispatchInfo.OverseasCSName = lstboxOverseas.EditText;
                    }
                }
                try
                {
                    DocumentlistSelectIds.Clear();
                    List<Guid?> DocumentHbls = new List<Guid?>();
                    List<Guid?> needHbls = new List<Guid?>();
                    BusinessOperationContext context = new BusinessOperationContext();
                    context.OperationID = OperationId;
                    List<DocumentInfo> DocumentList = BusinessFileService.GetBusinessDocumentList(context);


                    //获取选择的文档列表
                    foreach (var item in ucDocumentListDispatch.DocumentSourceCurrent)
                    {
                        if (item.Selected)
                        {
                            if (DocumentlistHistoryIds.Contains(item.Id))
                            {
                                DispatchSelectListInfo = DocumentDispatchData.DocumentListInfo.First(Info => Info.OperationFileID == item.Id);
                                DispatchFileIDs.Add(DispatchSelectListInfo.ID);
                            }
                            else
                            {
                                DispatchFileIDs.Add(null);
                                OperationFileUpdateDates.Add(null);
                            }
                            DocumentlistSelectIds.Add(item.Id);
                            OperationFileUpdateDates.Add(DateTime.Now);
                        }
                    }

                    foreach (var item in DocumentList)
                    {
                        if (DocumentlistSelectIds.Contains(item.Id))
                        {
                            DocumentHbls.Add(item.FormId);
                        }
                    }

                    string message = string.Empty;
                    if (OperationContext.OperationType == OperationType.OceanExport)
                    {
                        if (DocumentlistSelectIds.Count == 0)
                        {
                            message = LocalData.IsEnglish ? "Please Selected the Agent Dispatch Documents." : "请选择要分发的文档！";
                            SetLabelMessage(Color.Red, message);
                            return;
                        }
                    }

                    List<OceanHBLInfo> hbls = OceanExportService.GetOceanHBLToAgentHbls(OperationId);
                    foreach (OceanHBLInfo hbl in hbls)
                    {
                        if (!DocumentHbls.Contains(hbl.ID))
                        {
                            needHbls.Add(hbl.ID);
                            message += hbl.No + ",";
                        }
                    }

                    if (!string.IsNullOrEmpty(message))
                    {
                        message = message.Substring(0, message.Length - 1);
                        bool bind = false;
                        foreach (var item in needHbls)
                        {
                            if (DocumentList.Count(r => r.FormId == item.Value) == 0)
                            {
                                bind = true;
                                break;
                            }
                        }

                        if (bind)
                        {
                            if (LocalData.IsEnglish)
                            {
                                message = message.Insert(0, "BL:");
                                message += "has no PDF,\nPlease TAG each PDF with exact BL NO！";
                            }
                            else
                            {
                                message = message.Insert(0, "提单:");
                                message += "没有PDF对应,\n请标识每个BL.PDF属于哪个提单！";
                            }

                            if (MessageBox.Show(message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                BindCorrelation BC = new BindCorrelation();
                                BC.OperationId = this.OperationId;
                                string title = LocalData.IsEnglish ? "BindCorrelation" : "关联绑定";
                                PartLoader.ShowDialog(BC, title);
                            }
                            return;
                        }
                        else
                        {
                            if (LocalData.IsEnglish)
                            {
                                message = message.Insert(0, "You must selected the required HBLCopy:");
                            }
                            else
                            {
                                message = message.Insert(0, "您没有选择必须分发的HBLCopy:");
                            }

                            if (LocalData.IsEnglish)
                            {
                                message += "\nPlease select all the document！";
                            }
                            else
                            {
                                message += "\n请重新选择分发文档！";
                            }
                            MessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            return;
                        }
                    }

                    List<BillInfo> bills = FinanceService.GetBillInfos(OperationId);
                    bool exisisAMS = false;
                    bool exisisDHF = false;

                    if (bills != null)
                    {
                        List<BillInfo> agentbills = bills.FindAll(r => r.Type == BillType.DC);
                        if (agentbills != null)
                        {
                            agentbills.ForEach(r =>
                            {
                                if (r.Fees.Count(j => j.ChargingCodeID == new Guid("824D2382-EB05-4EF2-93CF-D81F0F7F0CEA")) > 0)
                                {
                                    exisisAMS = true;
                                }
                                if (r.Fees.Count(j => j.ChargingCodeID == new Guid("33F3EF34-A3AE-E911-B0C1-F71612D60FDF")) > 0)
                                {
                                    exisisDHF = true;
                                }
                            });
                        }
                    }

                    message = string.Empty;
                    if (LocalData.IsEnglish)
                    {
                        message = "The agency bill should have a AMS fee, please confirm whether there is no AMS fee?";
                    }
                    else
                    {
                        message = "代理账单应该有AMS费用，请确认是否没有AMS费用?";
                    }

                    if (!exisisAMS)
                    {
                        if (XtraMessageBox.Show(message, "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    message = string.Empty;
                    if (LocalData.IsEnglish)
                    {
                        message = "The agency bill should have a DHF fee, please confirm whether there is no DHF fee?";
                    }
                    else
                    {
                        message = "代理账单应该有DHF费用，请确认是否没有DHF费用?";
                    }
                    if (!exisisDHF)
                    {
                        if (XtraMessageBox.Show(message, "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    message = string.Empty;
                    this.Parent.Visible = false;
                    WaitCallback callback = data =>
                    {
                        strat();
                    };
                    ThreadPool.QueueUserWorkItem(callback);
                    MethodBase method = MethodBase.GetCurrentMethod();
                    StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "DISPATCH", string.Format("分发文件;OperationID[{0}]", OperationId));


                }
                catch (Exception ex)
                {
                    string strmessage = "";
                    strmessage = ClientHelper.GetErrorMessage(ex);
                    MethodBase method = MethodBase.GetCurrentMethod();
                    StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "DISPATCH", string.Format("分发文件失败;OperationID[{0}]", OperationId));
                    XtraMessageBox.Show(strmessage, "Tips", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            this.Parent.FindForm().Close();
        }


        public void delegatestart()
        {
            this.BeginInvoke(begenDis, null);
        }

        void RefershCall(object state)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    Closed();
                });
            }
            else
            {
                Closed();
            }
        }

        void Closed()
        {
            this.FindForm().Close();
        }

        /// <summary>
        /// 保存分文档信息
        /// </summary>
        private bool SaveDocumentDispatchInfo(int type)
        {
            switch (type)
            {
                case 1:
                    return OperationAgentService.DicpatchFilesForOE(OperationId, 1, DocumentlistSelectIds.ToArray(), LocalData.UserInfo.LoginID);
                case 2:
                    MailOIBusinessDataObjects mailInfo = oiService.GetTranserEmailInfoByID(OperationId);
                    SendEmail(mailInfo);
                    return OperationAgentService.DicpatchFilesForOI(OperationId, LocalData.UserInfo.LoginID);
                case 3:
                    return OperationAgentService.DicpatchFilesForAir(OperationId, 1, DocumentlistSelectIds.ToArray(), LocalData.UserInfo.LoginID);
                case 4:
                    return OperationAgentService.DicpatchFilesForAI(OperationId, LocalData.UserInfo.LoginID);
                default:
                    return false;
            }
        }

        #endregion

        #region 辅助方法
        private void SetCnText()
        {
            lblAgent.Text = "代理";
            ckeBelong.Text = "分公司";
            grbAgentType.Text = "代理类型";
            rdoAgent.Text = "分发给代理";
            rdoOverseas.Text = "分发给海外客服";
            txtSendAgent.ToolTip = LocalData.IsEnglish ? "separated by semicolon." : "以 ';'分割.";
            btnSend.Text = "分发(&F)";
            btnColse.Text = "取消(&C)";
            grbDispatch.Text = "分发文档给代理/海外客服";
            DocumentDispatchContainer.Text = "分发文档列表";
        }
        /// <summary>
        /// 将List转换为String
        /// </summary>
        /// <param name="listString"></param>
        /// <returns></returns>
        private string ListToString(List<DescriptionInfo> listString)
        {
            StringBuilder strBuf = new StringBuilder();
            if (listString != null && listString.Count > 0)
            {
                foreach (DescriptionInfo item in listString)
                {
                    if (!string.IsNullOrEmpty(item.Description))
                    {
                        strBuf.Append(string.Format("{0}{1}", item.Description, System.Environment.NewLine));
                    }
                }
                return strBuf.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 设置界面控件状态
        /// </summary>
        /// <param name="para"></param>
        private void SetControlState()
        {
            if (OperationContext.OperationType == OperationType.OceanImport || OperationContext.OperationType == OperationType.AirImport)
            {
                grbDispatch.Visible = false;
                txtAgent.Text = "分发代理修改账单";
                lblErr.Visible = false;
                DocumentDispatchContainer.Enabled = false;
                btnSend.Text = LocalData.IsEnglish ? "Revised" : "修订";
                return;
            }

            //如果代理信息为空
            if (DocumentDispatchData.AgentInfo == null)
            {
                //选择代理面板禁用   
                string errorMessage = LocalData.IsEnglish ? "No Agent Information for the current!" : "当前业务没有代理信息！";
                SetLabelMessage(Color.Red, errorMessage);
                btnSend.Enabled = false;//发送按钮屏蔽
                return;
            }
            else
            {
                lblErr.Text = string.Empty;
            }
            SetControls();
            SetDocumentDataSource();
        }

        void SetControls()
        {
            txtAgent.Text = DocumentDispatchData.AgentInfo.Name;

            //如果是内部代理
            if (DocumentDispatchData.AgentInfo.IsBranch == true)
            {
                grbDispatch.Visible = false;
                //是否是内部代理选择
                ckeBelong.Checked = true;
            }
            else
            {
                if (DocumentDispatchData.DispatchInfo != null)
                {
                    switch (DocumentDispatchData.DispatchInfo.RecipientType)
                    {
                        case RecipientType.External:
                            rdoAgent.Checked = true;
                            txtSendAgent.Enabled = true;
                            txtSendAgent.Text = DocumentDispatchData.DispatchInfo.AgentMail;
                            break;
                        case RecipientType.OverseasCS:
                            rdoOverseas.Checked = true;
                            this.lstboxOverseas.ShowSelectedValue(DocumentDispatchData.DispatchInfo.AgentMail, DocumentDispatchData.DispatchInfo.OverseasCSName);
                            break;
                    }
                }
                else
                {
                    rdoAgent.Checked = true;
                    txtSendAgent.Enabled = true;
                }
            }

        }

        /// <summary>
        /// 显示当前业务上一次分发信息
        /// </summary>
        void ShowToolTip()
        {
            string strDescription = ListToString(DocumentDispatchData.Description);
            //发送按钮显示日志信息
            if (!string.IsNullOrEmpty(strDescription))
            {
                this.btnSend.ToolTip = strDescription;
            }
        }

        /// <summary>
        /// 设置成功和失败信息
        /// </summary>
        /// <param name="foreColor"></param>
        /// <param name="message"></param>
        void SetLabelMessage(Color foreColor, string message)
        {
            lblErr.ForeColor = foreColor;
            lblErr.Text = message;
        }

        private void ClearValidate()
        {
            depValidate.Clear();
        }

        /// <summary>
        /// 验证
        /// </summary>
        private bool ValidateData()
        {
            if (rdoAgent.Checked && !ValidateEmail(txtSendAgent.Text))
            {
                SetLabelMessage(Color.Red, LocalData.IsEnglish ? "Please Input correct email adrress." : "请输入正确的邮件地址.");
                return false;
            }
            else if (rdoOverseas.Checked)
            {
                if (lstboxOverseas.EditValue == null || lstboxOverseas.EditValue == "")
                {
                    SetLabelMessage(Color.Red, LocalData.IsEnglish ? "Please selected OverSeas CS." : "请选择海外客服.");
                    return false;
                }
            }
            OceanBookingInfo obi = OceanExportService.GetOceanBookingInfo(OperationId);
            if (obi != null)
            {
                if (obi.ShippingLineID != Guid.Empty && NAShippingLines.Contains((Guid)obi.ShippingLineID))
                {
                    if (obi.GateInDate == null)
                    {
                        SetLabelMessage(Color.Red, LocalData.IsEnglish ? "No GateInDate cannot Dispatch Files." : "该业务未填写进港日不能分发文件.");
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 验证邮件地址
        /// </summary>
        /// <param name="strEmailAddress"></param>
        /// <returns></returns>
        private bool ValidateEmail(string text)
        {
            bool EmailCheckResult = true;
            if (string.IsNullOrEmpty(text))
            {
                EmailCheckResult = false;
            }
            else
            {

                string EmailPattern = @"^([A-Za-z0-9]{1}[A-Za-z0-9_]*)@([A-Za-z0-9_]+)[.]([A-Za-z0-9_]*)$";//E-Mail地址格式的正则表达式
                string[] arrEmailAddress = text.ToString().Split(';');
                if (arrEmailAddress != null && arrEmailAddress.Length > 0)
                {
                    List<bool> lstEmailAddress = new List<bool>(arrEmailAddress.Length);
                    foreach (string arr in arrEmailAddress)
                    {
                        if (!string.IsNullOrEmpty(arr))
                            lstEmailAddress.Add(Regex.IsMatch(arr, EmailPattern));
                    }
                    //输入的字符串中是否有不是正确的邮件地址
                    if (lstEmailAddress.Contains(false))
                        EmailCheckResult = false;
                    else
                        EmailCheckResult = true;
                }
                else
                {
                    EmailCheckResult = false;
                }
            }

            return EmailCheckResult;
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColse_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        /// <summary>
        /// 设置文档列表数据源
        /// </summary>
        /// <param name="para"></param>
        private void SetDocumentDataSource()
        {
            ucDocumentListDispatch.ContextHistory = GetHistoryContext(OperationId);
            ucDocumentListDispatch.ContextCurrent = ucDocumentListDispatch.ContextHistory;
            ucDocumentListDispatch.workflag = this.workflag;
            ucDocumentListDispatch.SetDataSource();

            DocumentInfoDataSource = ucDocumentListDispatch.DocumentSourceCurrent;
            string result = FCMCommonService.GetDispatchNewLogID(OperationId);
            List<DispatchFile> currentFiles = new List<DispatchFile>();
            if (!string.IsNullOrEmpty(result))
            {
                dispatchLogid = JSONSerializerHelper.DeserializeFromJson<Guid>(result);
                currentFiles = OperationAgentService.GetDispatchFiles(dispatchLogid);
            }

            if (DocumentInfoDataSource == null)
            {
                return;
            }
            //找出之前分发的文档ID

            foreach (var item in DocumentInfoDataSource)
            {
                if (currentFiles.Count(r => r.OperationFileID == item.Id) > 0)
                {
                    item.Selected = true;
                }
            }
            ucDocumentListDispatch.CurrentDispatchListResetBindings();
        }

        private BusinessOperationContext GetHistoryContext(Guid businessID)
        {
            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationID = businessID;
            return context;
        }
        #endregion

        #region IDisposable 成员

        void IDisposable.Dispose()
        {

        }

        #endregion

        private void txtSendAgent_EditValueChanged(object sender, EventArgs e)
        {
            ClearValidate();
        }

        private void lstboxOverseas_EditValueChanged(object sender, EventArgs e)
        {
            ClearValidate();
        }

        private void strat()
        {
            try
            {
                operationType = (int)OperationContext.OperationType;
                isSuccess = SaveDocumentDispatchInfo(operationType);

                string tip = LocalData.IsEnglish ? "" : "";

                if (isSuccess)
                {
                    if (Saved != null)
                    {
                        string documentType = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription(DocumentState.Dispatched, LocalData.IsEnglish);
                        Saved(new object[] { documentType });
                    }
                }
                else
                {
                    tip = LocalData.IsEnglish ? "Dispatching Docs is Failure!" : "文档分发失败!";
                    XtraMessageBox.Show(tip, "Tips", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                string strmessage = "";
                strmessage = ClientHelper.GetErrorMessage(ex);
                XtraMessageBox.Show(strmessage, "Tips", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        BusinessOperationContext BuildBussinessInfo(Guid OperationID, int type)
        {
            BusinessOperationContext context = BusinessOperationContext.Current;
            context.OperationID = OperationID;
            context.OperationType = (OperationType)type;
            return context;
        }

        public void SendEmail(MailOIBusinessDataObjects mailInfo)
        {
            try
            {
                if (mailInfo == null) return;
                UserInfo userInfo = new UserInfo();
                userInfo = _userService.GetUserInfo(LocalData.UserInfo.LoginID);
                DateTime dt = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                dt = dt.ToLocalTime();
                ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
                message.CreateBy = LocalData.UserInfo.LoginID;
                message.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                message.HasAttachment = false;
                message.SendFrom = userInfo.EMail;
                message.SendTo = mailInfo.POLFiler;
                message.CC = userInfo.EMail;
                message.Subject = (LocalData.IsEnglish ? "D/C Fees have been revised by the agent,Please sign in time." : "代理修改了账单请及时签收");
                message.Body = (LocalData.IsEnglish ? ("Operation No [" + mailInfo.OperationNo + "] D/C Fees have been revised at[" + dt.ToString("yyyy-MM-dd") + "] by the agent [" + LocalData.UserInfo.UserEname + "], you must accept the revised fees." + Environment.NewLine + "After this E-mail distribution by the agent to revise bill ICP automatically send, please handle in time!") : ("出口业务 [" + mailInfo.OperationNo + "] 账单已被代理 [" + LocalData.UserInfo.UserEname + "] 于 [" + dt.ToString("yyyy-MM-dd") + "] 修订并分发，请及时签收！" + Environment.NewLine + "本邮件由代理修订账单分发后ICP自动发出，请及时处理！")); 
                message.Type = MessageType.Email;
                message.BodyFormat = BodyFormat.olFormatHTML;
                message.UserProperties = new Message.ServiceInterface.MessageUserPropertiesObject
                {
                    OperationType = OperationType.OceanImport,
                    OperationId = mailInfo.OIBookingID,
                };

                _messageService.Send(message);
            }
            catch (Exception ex)
            {
                ICP.Framework.CommonLibrary.LogHelper.SaveLog(ex.Message + ex.StackTrace);
            }
        }
    }


}
