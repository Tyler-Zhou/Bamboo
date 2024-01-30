/**
 *  创建时间:2014-07-17
 *  创建人:Joabwang    
 *  描  述:主面板
 **/

using ICP.Business.Common.ServiceInterface;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Action = System.Action;
using Exception = System.Exception;

namespace ICP.MailCenterFramework.UI
{
    /// <summary>
    /// 邮件中心主面板
    /// </summary>
    public partial class MailCenterUI : UserControl
    {
        #region  Windows user32
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr ChangeWindowMessageFilter(uint message, uint dwFlag);
        #endregion

        #region 快捷键

        #region 注册快捷方式
        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="hWnd">热键产生WM_HOTKEY消息的窗口句柄</param>
        /// <param name="id">定义热键的标识符</param>
        /// <param name="fsModifiers">为了产生WM_HOTKEY消息而必须与由nVirtKey参数定义的键一起按下的键</param>
        /// <param name="vk">热键的虚拟键码</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd, // handle to window 
            int id, // hot key identifier 
            KeyModifiers fsModifiers, // key-modifier options 
            Keys vk // virtual-key code 
            );
        /// <summary>
        /// 移除热键
        /// </summary>
        /// <param name="hWnd">与被释放的热键相关的窗口句柄。若热键不与窗口相关，则该参数为NULL</param>
        /// <param name="id">被释放的热键的标识符</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd, // handle to window 
            int id // hot key identifier 
            );

        #endregion

        #region 全局快捷键

        /// <summary>
        /// 快捷键键值对
        /// </summary>
        private static Dictionary<Int32, ToolStripMenuItem> hotKeys = new Dictionary<Int32, ToolStripMenuItem>();
        /// <summary>
        /// 监视Windows消息
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_HOTKEY = 0x0312;//按快捷键
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    //调用主处理程序
                    if (hotKeys.ContainsKey(m.WParam.ToInt32()))
                    {
                        if (WhetherOutLook() == false) return;
                        KeysClick(hotKeys[m.WParam.ToInt32()], new EventArgs());
                    }
                    break;
            }
            base.WndProc(ref m);
        }
        /// <summary>
        /// 设置全局快捷键
        /// </summary>
        public void SetShortcut()
        {
            try
            {
                AddToolStripMenu();
                int id = 100;
                hotKeys.Clear();
                foreach (var item in ToolStripMenu)
                {
                    Keys keys = SetContextMenuItemKeys(item.Name);
                    if (keys != Keys.None)
                    {

                        if (!hotKeys.ContainsKey(id))
                        {
                            RegisterHotKey(Handle, id, KeyModifiers.Control, keys);
                            string Code = Enum.GetName(typeof(Keys), keys);
                            Code = Code.Replace("D1", "1")
                                    .Replace("D2", "2")
                                    .Replace("D3", "3")
                                    .Replace("D4", "4")
                                    .Replace("D5", "5");
                            if (item.Text.Contains(Code) == false)
                            {
                                item.Text = item.Text + "  " + "Ctrl+" + Code;
                            }
                            hotKeys.Add(id++, item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("SetShortcut", ex);
            }
        }

        /// <summary>
        /// 设置右键菜单项快捷键
        /// </summary>
        /// <param name="contextMenuItemId">菜单项ID</param>
        /// <returns>HotKey</returns>
        public Keys SetContextMenuItemKeys(string contextMenuItemId)
        {
            Keys hotkey = Keys.None;
            switch (contextMenuItemId.Trim())
            {
                case "OpenSO":
                    hotkey = Keys.Q;
                    break;
                case "CommunicationAccInfo":
                    hotkey = Keys.W;
                    break;
                case "OpenBusiness":
                    hotkey = Keys.E;
                    break;
                case "AddMBL":
                    hotkey = Keys.R;
                    break;
                case "AddHBL":
                    hotkey = Keys.T;
                    break;
                case "UploadSIAttachment":
                    hotkey = Keys.A;
                    break;
                case "UploadSOAttachment":
                    hotkey = Keys.S;
                    break;
                case "UploadMBLAttachment":
                    hotkey = Keys.D;
                    break;
                case "UploadAPAttachment":
                    hotkey = Keys.F;
                    break;
                case "UploadANAttachment":
                    hotkey = Keys.G;
                    break;
                case "UploadAttachment":
                    hotkey = Keys.H;
                    break;
                case "OenTaskCenter":
                    hotkey = Keys.D1;
                    break;
                case "ShowContactsAndAssistants":
                    hotkey = Keys.D2;
                    break;
                case "MailofCustomer":
                    hotkey = Keys.D3;
                    break;
                case "HistoryofCarrier":
                    hotkey = Keys.D4;
                    break;
                case "HistoryofAgent":
                    hotkey = Keys.D5;
                    break;
                default:
                    hotkey = Keys.None;
                    break;
            }
            return hotkey;
        }

        /// <summary>
        /// 快捷键点击事件注入
        /// </summary>
        void KeysClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                Type type = Type.GetType("ICP.MailCenterFramework.UI.ContextMenuTemplate");
                if (type != null)
                {
                    MethodInfo method = type.GetMethod(item.AccessibleName);
                    object meobj = Activator.CreateInstance(type);
                    method.Invoke(meobj, null);
                }
            }
        }
        #endregion 
        #endregion

        #region 变量对象

        /// <summary>
        /// 超时限定:预览邮件超时值
        /// </summary>
        private const int _timeOut = 5*1000;

        /// <summary>
        /// 是否加载数据：0：默认未加载，1：含业务数据，-1：未找到数据
        /// </summary>
        private int _loadData = 0;

        /// <summary>
        /// 刷新业务数据计时器
        /// 每隔1秒刷新一次
        /// </summary>
        Timer RefreshTime;
        /// <summary>
        /// 是否显示Refresh Data
        /// </summary>
        bool _ShowRefreshDataException = true;
        /// <summary>
        /// 面板使用方法集合  确保只有一个实体
        /// </summary>
        public Assist _Assist;
        /// <summary>
        /// 鼠标右键菜单  确保只有一个实体
        /// </summary>
        public ContextMenuTemplate ContextMenuTemplate;
        /// <summary>
        /// 海出控件集合
        /// </summary>
        public List<ToolStripMenuItem> OeToolStripMenuItems;
        /// <summary>
        /// 海进控件集合
        /// </summary>
        public List<ToolStripMenuItem> OiToolStripMenuItems;
        /// <summary>
        /// 询价控件集合
        /// </summary>
        public List<ToolStripMenuItem> InToolStripMenuItems;
        /// <summary>
        ///  生成全局快捷键使用的控件集合
        /// </summary>
        public List<ToolStripMenuItem> ToolStripMenu;
        /// <summary>
        /// 当前邮件实体
        /// </summary>
        public Message.ServiceInterface.Message MessageEntity { get; set; }
        /// <summary>
        /// 当前邮件附件集合:上传附件时另存附件使用
        /// </summary>
        public Attachments CurrentMailAttachments { get; set; }
        /// <summary>
        /// 当前行数据
        /// </summary>
        public DataRowView DataRowView
        {
            get
            {
                var dataGridViewRow = gvList.CurrentRow;
                if (dataGridViewRow == null)
                    return null;
                if (_loadData != 1)
                    return null;
                return (DataRowView)dataGridViewRow.DataBoundItem;
            }
        }
        /// <summary>
        /// 当前行的业务类型
        /// </summary>
        public OperationType OperationType
        {
            get
            {
                if (DataRowView != null)
                {
                    return (OperationType)Enum.Parse(typeof(OperationType), DataRowView["OperationType"].ToString());
                }
                else
                {
                    return OperationType.OceanExport;
                }

            }
        }

        /// <summary>
        /// 当前行业务ID
        /// </summary>
        public Guid OperationId
        {
            get
            {
                if (DataRowView != null)
                {
                    return new Guid(DataRowView["OceanBookingID"].ToString());
                }
                else
                {
                    return Guid.Empty;
                }
            }
        }
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo
        {

            get
            {
                if (DataRowView != null)
                {
                    return DataRowView["NO"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// MBL,HBL号
        /// </summary>
        public string OperationBLNO
        {
            get
            {
                if (DataRowView != null)
                {
                    return DataRowView["BLNO"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? OperationUpdateDate
        {
            get
            {
                if (DataRowView != null)
                {
                    if (!string.IsNullOrEmpty(DataRowView["UpdateDate"].ToString()))
                    {
                        return DateTime.Parse(DataRowView["UpdateDate"].ToString());
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// 当前业务是否有效
        /// </summary>
        public bool OperationIsValid
        {
            get
            {
                if (DataRowView != null)
                {
                    return bool.Parse(DataRowView["IsValid"].ToString());
                }
                else
                {
                    return true;
                }
            }

        }
        /// <summary>
        /// 数据库视图CODE的名称
        /// </summary>
        public string TemplateCode { get { return "MailLink4in1"; } }
        /// <summary>
        /// 缓存数据库服务接口
        /// </summary>
        public IDataCacheOperationService DataCacheOperationService
        {
            get
            {
                IDataCacheOperationService temp = null;
                try
                {
                    temp = ServiceClient.GetService<IDataCacheOperationService>();
                    //temp = null;
                }
                catch (Exception ex)
                {
                    ToolUtility.WriteLog("MailCenterUI DataCacheOperationService", ex);
                    temp = null;
                }
                return temp;
            }
        }

        /// <summary>
        /// FCM 公共服务接口
        /// </summary>
        public IICPCommonOperationService ICPCommonOperationService
        {
            get
            {
                IICPCommonOperationService temp = null;
                try
                {
                    temp = ServiceClient.GetService<IICPCommonOperationService>();
                }
                catch (Exception ex)
                {
                    ToolUtility.WriteLog("Assist ICPCommonOperationService", ex);
                    temp = null;
                }
                return temp;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public MailCenterUI()
        {
            try
            {
                InitializeComponent();
                InitControl();


                Disposed += (sender, args) =>
                {
                    OperateEvent(false);
                    _Assist = null;
                    ContextMenuTemplate = null;
                };
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("MailCenterUI Constructor", ex);
            }
        }

        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        void MailCenterUI_Load(object sender, EventArgs e)
        {
            try
            {
                InitData();

                #region 解决 页面DragDrop不触发的问题

                try
                {
                    //判断当前客户端的系统是否为Windeos 7  如果是 进行加载项解决DragDrop不触发如不是不加载
                    if (getOSVersion().Contains("7"))
                    {
                        uint WM_DROPFILES = 0x0233;
                        uint WM_COPYDATA = 0x4A;
                        uint MSGFLT_ADD = 1;
                        ChangeWindowMessageFilter(WM_DROPFILES, MSGFLT_ADD);
                        ChangeWindowMessageFilter(WM_COPYDATA, MSGFLT_ADD);
                        ChangeWindowMessageFilter(0x0049, MSGFLT_ADD);
                    }
                }
                catch (Exception ex)
                {
                    //禁止再次注册快捷键
                    HelpMailStore.CanRegisterHotKey = false;
                    ToolUtility.WriteLog("Shortcuts Registration Failed", ex);
                }

                #endregion
                if (!Parameter.IsEnableICPConnection)
                    EnableAssociate("Part", false);
            }
            catch (Exception ex)
            {
                EnableAssociate("Part", false);
                ToolUtility.WriteLog("MailCenterUI Part Load", ex);
            }
        }
        /// <summary>
        /// 拖放完成时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MailCenterUI_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = DragDropEffects.Copy;
                MemoryStream filenames = (MemoryStream)e.Data.GetData("FileGroupDescriptor");
                Attachmentupload(filenames.ToArray());
            }
            catch (Exception ex)
            {
                //执行拖动查询
                string keyWord = e.Data.GetData(DataFormats.UnicodeText).ToString();
                SearchByKeyword(keyWord);
            }
        }
        /// <summary>
        /// 拖放控件到边界时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MailCenterUI_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        #region   UI按钮
        /// <summary>
        /// 新增海出订舱
        /// </summary>
        void tsmiBtnOE_Click(object sender, EventArgs e)
        {
            try
            {
                _Assist.AddSoForOe(TemplateCode, MessageEntity);
                RefreshData();
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("New Ocean Export", ex);
            }
        }
        void tsmiBtnAssociate_Click(object sender, EventArgs e)
        {
            try
            {
                _Assist.ClikckAssociate(AssociateType.Normal, ContactType.Unknown, GetBusinessOperationContexts(), MessageEntity, OperationId, OperationType);
                RefreshData();
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("Associate Normal", ex);
            }
        }
        /// <summary>
        /// 关联并设置为客户邮件
        /// </summary>
        void tsmiBtnAssociateAsCustomerMail_Click(object sender, EventArgs e)
        {
            try
            {
                _Assist.ClikckAssociate(AssociateType.AsCustomer, ContactType.Customer, GetBusinessOperationContexts(), MessageEntity, OperationId, OperationType);
                RefreshData();
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("Associate As Customer Mail", ex);
            }
        }
        /// <summary>
        /// 关联并设置为承运人邮件
        /// </summary>
        void tsmiBtnAssociateAsCarrierMail_Click(object sender, EventArgs e)
        {
            try
            {
                _Assist.ClikckAssociate(AssociateType.AsCarrier, ContactType.Carrier, GetBusinessOperationContexts(), MessageEntity, OperationId, OperationType);
                RefreshData();
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("Associate As Carrier Mail", ex);
            }
        }
        /// <summary>
        /// 高级查询按钮
        /// </summary>
        void tsmiBtnAdvancedquery_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessQueryCriteria businessQuery = _Assist.Advancedquery(OperationType);
                if (string.IsNullOrEmpty(businessQuery.AdvanceQueryString))
                {
                    ToolUtility.WriteLog("Advanced Query->AdvanceQueryString为空", null);
                }
                //运行查询
                businessQuery.TemplateCode = TemplateCode;
                if (string.IsNullOrEmpty(businessQuery.TemplateCode))
                {
                    ToolUtility.WriteLog("Advanced Query->TemplateCode为空", null);
                }
                businessQuery.EmailAddress = "";
                businessQuery.companyIDs = string.IsNullOrEmpty(GetQueryConditions.SelectedCompanyIds) ? null : GetQueryConditions.SelectedCompanyIds.Split(',').Select(id => new Guid(id)).ToArray();
                if (businessQuery.companyIDs == null)
                {
                    ToolUtility.WriteLog("Advanced Query->companyIDs为空", null);
                }
                OperationMessageController OperationMsgCtr = new OperationMessageController();
                if (OperationMsgCtr != null)
                {
                    DataTable dt = OperationMsgCtr.AdvanceSearch(businessQuery, MessageEntity);
                    DataBind(dt);
                }
                else
                {
                    ToolUtility.WriteLog("Advanced Query->OperationMsgCtr为空", null);
                    MessageBox.Show("Advanced Query" + Environment.NewLine + "出错，请联系电脑部!");
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("Advanced Query", ex);
            }

        }
        #endregion

        #region DataGridView 事件
        /// <summary>
        /// 数据绑定后设置行样式
        /// </summary>
        void gvList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (_loadData != 1 || gvList.Rows.Count == 0) return;
                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    DataRowView drRowView = (DataRowView)gvList.Rows[i].DataBoundItem;
                    if (drRowView == null) return;
                    if (!bool.Parse(drRowView["IsValid"].ToString())) //无效订单
                    {
                        gvList.Rows[i].DefaultCellStyle.ForeColor = Color.Gray;
                        gvList.Rows[i].DefaultCellStyle.Font = new Font("宋体", 9, FontStyle.Strikeout);
                    }
                    else
                    {
                        if (IsRelationData(drRowView["NO"].ToString()))
                        {
                            gvList.Rows[i].DefaultCellStyle.Font = new Font("宋体", 9, FontStyle.Bold);
                        }
                    }
                }
                if (gvList.Rows.Count == 1 && UIHelper.MessageRelationOperationList == null)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)gvList.Rows[0].Cells["check"];
                    checkCell.Value = true;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("DataGridView DataBindingComplete", ex);
            }
        }

        /// <summary>
        /// 单元格双击 根据业务打开对应的窗口
        /// </summary>
        void gvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (OperationId == Guid.Empty) return;
                if (gvList.Rows.Count == 0) return;
                if (OperationType == OperationType.OceanExport)
                {
                    _Assist.EditOeBooking(OperationId, OperationNo, OperationUpdateDate, TemplateCode, MessageEntity);
                }
                if (OperationType == OperationType.OceanImport)
                {
                    _Assist.EditOiBusiness(OperationId, OperationNo, OperationUpdateDate, TemplateCode, MessageEntity);
                }
                if (OperationType == OperationType.AirExport)
                {
                    _Assist.AirExportEditData(OperationId, OperationNo, OperationUpdateDate, TemplateCode, MessageEntity);
                }
                if (OperationType == OperationType.AirImport)
                {
                    _Assist.AirImportEditBooking(OperationId, OperationNo, OperationUpdateDate, TemplateCode, MessageEntity);
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("DataGridView CellDoubleClick", ex);
            }
        }
        /// <summary>
        /// 鼠标右键菜单项
        /// </summary>
        void gvList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (gvList.Rows.Count == 0) return;
                Assignment();
                SetShortcut();
                //根据业务类型显示或者隐藏设置阶段(SI,SO)及其高级查找的按钮
                ButtonControl(OperationType);
                if (e.Button == MouseButtons.Right && e.Clicks == 1)
                {
                    //OperationSaveController OperationSaveCtr = new OperationSaveController();
                    ////是外部邮件但联系人没保存时，右键不可用
                    //if (OperationSaveCtr.IsExternalMail(MessageEntity) && !OperationSaveCtr.IsAllContactExsit())
                    //{
                    //    return;
                    //}
                    if (e.RowIndex >= 0)
                    {
                        //若行已是选中状态就不再进行设置
                        if (gvList.Rows[e.RowIndex].Selected == false)
                        {
                            gvList.ClearSelection();
                            gvList.Rows[e.RowIndex].Selected = true;
                        }
                        //只选中一行时设置活动单元格
                        if (gvList.SelectedRows.Count == 1)
                        {
                            gvList.CurrentCell = gvList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        }
                        AddToolStripMenu();
                        contextMenuStrip.Show(MousePosition.X, MousePosition.Y);
                    }
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("DataGridView CellMouseDown", ex);
            }
        }

        /// <summary>
        /// 拖放控件到边界时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gvList_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        /// <summary>
        /// 拖放完成时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gvList_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = DragDropEffects.Copy;


                MemoryStream filenames = (MemoryStream)e.Data.GetData("FileGroupDescriptor");
                Attachmentupload(filenames.ToArray());
            }
            catch (Exception)
            {
                //执行拖动查询
                string keyWord = e.Data.GetData(DataFormats.UnicodeText).ToString();
                SearchByKeyword(keyWord);
            }
        }

        #endregion
        #endregion

        #region 刷新业务列表数据
        /// <summary>
        /// 刷新业务列表数据
        /// </summary>
        public void RefreshData()
        {
            try
            {
                if (MessageEntity != null)
                {
                    EnableAssociate("", true);
                    DataTable dt = null;
                    BusinessQueryCriteria Criteria = new BusinessQueryCriteria();
                    OperationMessageController operationMsgCtr = new OperationMessageController();
                    OperationSaveController operationSaveCtr = new OperationSaveController();
                    List<OperationMessageRelation> messageRelations;
                    Criteria.TemplateCode = TemplateCode;
                    Criteria.companyIDs = string.IsNullOrEmpty(GetQueryConditions.SelectedCompanyIds)
                        ? null
                        : GetQueryConditions.SelectedCompanyIds.Split(',').Select(id => new Guid(id)).ToArray();
                    string messageReference = string.Empty;
                    if (operationSaveCtr.IsSaveExternalMail(MessageEntity) && MessageEntity.UserProperties != null)
                    {
                        messageReference = MessageEntity.UserProperties.Reference;
                    }
                    messageRelations =
                        operationMsgCtr.GetOperationMessageRelationByMessageIdAndReference(MessageEntity.MessageId,
                            messageReference); //查找关联信息
                    HelpMailStore.CurrentMessageRelation = messageRelations;//全局缓存当前邮件的关联信息
                    //如果找到关联信息，返回业务数据
                    if (messageRelations != null && messageRelations.Count > 0)
                    {
                        //设置该邮件存储在数据库中的IMessageID，方便后续关联业务使用
                        MessageEntity.Id = messageRelations[0].IMessageId;
                        dt = operationMsgCtr.GetOperationListByMessageRelations(Criteria, messageRelations.ToArray());
                        //缓存系统自动默认搜索的关联业务集合
                        if (dt != null)
                        {
                            UIHelper.MessageRelationOperationList = dt.Copy(); //高级搜索用到
                        }
                    }
                    DataBind(dt);
                    //重置提示信息变量
                    _ShowRefreshDataException = true;

                }
                else
                    EnableAssociate("Part", false);
            }
            catch (Exception ex)
            {
                if (IsICPConnectionException(ex))
                    return;
                if (_ShowRefreshDataException)
                {
                    _ShowRefreshDataException = false; //不再重复提示信息
                    ToolUtility.WriteLog("RefreshData", ex);
                    MessageBox.Show("RefreshData" + Environment.NewLine + ex, "System Information");
                }
            }
        }
        #endregion

        #region    生成鼠标右键菜单项

        /// <summary>
        /// 根据XML数据生成控件
        /// </summary>
        public void AddToolStripMenu()
        {
            if (OperationType == OperationType.OceanExport)
            {
                if (OeToolStripMenuItems.Count <= 0)
                {
                    OeToolStripMenuItems = ContextMenuTemplate.GetItems(TemplateCode + "_" + OperationType);
                }
                AddMenuStrip(OeToolStripMenuItems);
            }
            else if (OperationType == OperationType.OceanImport)
            {
                if (OiToolStripMenuItems.Count <= 0)
                {
                    OiToolStripMenuItems = ContextMenuTemplate.GetItems(TemplateCode + "_" + OperationType);
                }
                AddMenuStrip(OiToolStripMenuItems);
            }
            else if (OperationType == OperationType.InquireRate)
            {
                if (InToolStripMenuItems.Count <= 0)
                {
                    InToolStripMenuItems = ContextMenuTemplate.GetItems(TemplateCode + "_" + OperationType);
                }
                AddMenuStrip(InToolStripMenuItems);
            }
        }
        /// <summary>
        /// 构造右键菜单
        /// </summary>
        public void AddMenuStrip(List<ToolStripMenuItem> toolStripMenu)
        {
            ToolStripMenu = toolStripMenu;
            contextMenuStrip.Items.Clear();
            foreach (var i in toolStripMenu)
            {
                if (i.AutoToolTip)
                {
                    ToolStripSeparator toolStripSeparator = new ToolStripSeparator();
                    toolStripSeparator.Name = "ToolStrip" + i.Name;
                    contextMenuStrip.Items.Add(toolStripSeparator);
                }
                if (string.IsNullOrEmpty(i.Tag.ToString()))
                {
                    contextMenuStrip.Items.Add(i);
                }
                else
                {
                    //查找包含下级的菜单项
                    ToolStripMenuItem toolStripTemp = toolStripMenu.FirstOrDefault(n => n.Tag.ToString() == i.Tag.ToString());
                    if (toolStripTemp != null && toolStripTemp != i)
                    {
                        toolStripTemp.DropDownItems.Add(i);
                    }
                    else
                    {
                        //当前的TAG的名称相等时，将控件是作为当前子项的父项
                        contextMenuStrip.Items.Add(i);
                    }
                }
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 绑定DataGrid
        /// </summary>
        public void DataBind(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                _loadData = 1;
                gvList.DataSource = null;
                EnableAssociate("Associate", true);
                if (dt.Columns.Contains("Selected"))
                {
                    dt.Columns.Remove("Selected");
                }
                if (dt.Rows.Count > 0)
                {
                    dt = FormatData(dt);
                }
                gvList.DataSource = dt;
                if (dt.Rows.Count <= 2)
                {
                    if (Parent != null)
                    {
                        Parent.Height = 50;
                    }
                }
                else
                {
                    if (Parent != null)
                    {
                        Parent.Height = 100;
                    }
                }
            }
            else
            {
                if (_loadData == 1)
                    return;
                _loadData = -1;
                EnableAssociate("Associate", false);
                dt = new DataTable("NoDataTable");
                dt.Columns.Add("No");
                dt.Columns.Add("BLNO");
                DataRow row = dt.NewRow();
                row["No"] = OutlookUtility.IsEnglish ? "No Shipment Found" : "未找到业务数据";
                row["BLNO"] = OutlookUtility.IsEnglish ? "" : "如需关联，请通过拽入单号、SO号等业务信息至此处或通过[查询]按钮查询";
                dt.Rows.Add(row);
                gvList.DataSource = dt;
            }
        }

        /// <summary>
        /// 内容显示转化
        /// </summary>
        public DataTable FormatData(DataTable dt)
        {
            DataTable newdt = dt.Copy();
            foreach (DataRow row in newdt.Rows)
            {
                if (!string.IsNullOrEmpty(row["BLNO"].ToString()))
                {

                    row["BLNO"] = row["BLNO"].ToString().Replace(" ", "").Replace("\n", " \n");
                }

                if (!string.IsNullOrEmpty(row["RefNO"].ToString()))
                {
                    row["RefNO"] = row["RefNO"].ToString().Replace(" ", "").Replace("\n", "  \n");
                }
                if (!string.IsNullOrEmpty(row["Description"].ToString()))
                {
                    row["Description"] = row["Description"].ToString().Replace(" ", "").Replace("\n", " \n");
                }
            }
            return newdt;
        }

        /// <summary>
        /// 根据关键字快速搜索
        /// </summary>
        public void SearchByKeyword(string keyword)
        {
            keyword = keyword.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                BusinessQueryCriteria Criteria = new BusinessQueryCriteria();
                OperationMessageController OperationMsgCtr = new OperationMessageController();
                GetQueryConditions GQS = new GetQueryConditions();
                Criteria.EmailAddress = MessageEntity.SendFrom;
                Criteria.TemplateCode = TemplateCode;
                Criteria.AdvanceQueryString = GQS.EmailQuery(keyword);
                Criteria.companyIDs = string.IsNullOrEmpty(GetQueryConditions.SelectedCompanyIds) ? null : GetQueryConditions.SelectedCompanyIds.Split(',').Select(id => new Guid(id)).ToArray();
                Criteria.NeedSearchInSQLServer = true;
                Criteria.ServerQueryString = GetQueryConditions.ServerQueryString;
                DataTable dt = OperationMsgCtr.GetOperationListByKeyWord(Criteria);
                DataBind(dt);
            }
        }

        /// <summary>
        /// 返回关联操作集合信息
        /// </summary>
        /// <returns></returns>
        public List<BusinessOperationContext> GetBusinessOperationContexts()
        {
            List<BusinessOperationContext> businessOperation = new List<BusinessOperationContext>();
            gvList.EndEdit();
            for (int i = 0; i < gvList.RowCount; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)gvList.Rows[i].Cells["check"];
                bool flag = Convert.ToBoolean(checkCell.Value);
                if (flag)
                {
                    DataRowView drRowView = (DataRowView)gvList.Rows[i].DataBoundItem;
                    BusinessOperationContext businessOperationContext = new BusinessOperationContext();
                    businessOperationContext.OperationID = new Guid(drRowView["OceanBookingID"].ToString());
                    businessOperationContext.OperationNO = drRowView["NO"].ToString();
                    businessOperationContext.OperationType = (OperationType)Enum.Parse(typeof(OperationType), drRowView["OperationType"].ToString());
                    if (!string.IsNullOrEmpty(drRowView["UpdateDate"].ToString()))
                    {
                        businessOperationContext.UpdateDate = DateTime.Parse(drRowView["UpdateDate"].ToString());
                    }
                    businessOperation.Add(businessOperationContext);
                }
            }
            return businessOperation;
        }

        /// <summary>
        /// 根据业务类型控制按钮的显示和隐藏
        /// </summary>
        /// <param name="operationType">业务类型</param>
        public void ButtonControl(OperationType operationType)
        {
            //tsmiBtnShowButton：关联并设置SO、SI
            switch (operationType)
            {
                case OperationType.OceanImport:
                    break;
                case OperationType.InquireRate:
                case OperationType.QuotedPrice:
                    tsmiBtnAdvancedquery.Visible = false;
                    break;
                default:
                    if (tsmiBtnAdvancedquery.Visible == false)
                    {
                        tsmiBtnAdvancedquery.Visible = true;
                    }
                    break;
            }
        }

        /// <summary>
        /// 给右键菜单赋值
        /// </summary>
        public void Assignment()
        {
            Parameter.OperationId = OperationId;
            Parameter.OperationNo = OperationNo;
            Parameter.OperationType = OperationType;
            Parameter.UpdateDate = OperationUpdateDate;
            Parameter.Message = MessageEntity;
            Parameter.TemplateCode = TemplateCode;
            Parameter.BLNO = OperationBLNO;
            Parameter.MailAttachmentContents = OutlookUtility.GetAttachmentContentsByMailItem(HelpMailStore.CurrentMailItem as _MailItem);
        }

        /// <summary>
        /// 允许上传附件的类型
        /// </summary>
        /// <returns></returns>
        public static string[] FilterFilesExtension()
        {
            string[] extensions = { ".txt", ".pdf", ".doc", ".docx", ".rtf", ".xls", ".xlsx", ".ppt", ".pptx", ".jpg", ".jpeg", ".gif", ".png", ".tif", ".tiff", ".bmp", ".msg", ".html", ".htm", ".mht" };
            string[] upperExtensions = (from item in extensions
                                        select item.ToUpper()).ToArray();
            return extensions.Union(upperExtensions).ToArray();
        }

        /// <summary>
        /// 事件操作
        /// </summary>
        /// <param name="isAdd">是否添加</param>
        void OperateEvent(bool isAdd)
        {
            if (isAdd)
            {
                Load += MailCenterUI_Load;                      //窗体加载
                tsmiBtnOE.Click += tsmiBtnOE_Click;             //海出订舱
                tsmiBtnAssociate.Click += tsmiBtnAssociate_Click;                     //关联
                tsmiBtnAssociateAsCustomerMail.Click += tsmiBtnAssociateAsCustomerMail_Click;  //关联并设置为客户邮件
                tsmiBtnAssociateAsCarrierMail.Click += tsmiBtnAssociateAsCarrierMail_Click;    //关联并设置为承运人邮件
                tsmiBtnAdvancedquery.Click += tsmiBtnAdvancedquery_Click;   //高级查询

                gvList.CellDoubleClick += gvList_CellDoubleClick;       //列双击
                gvList.CellMouseDown += gvList_CellMouseDown;           //列右键菜单
                gvList.DataBindingComplete += gvList_DataBindingComplete;//数据绑定后：列样式设置

                gvList.DragDrop += gvList_DragDrop;
                gvList.DragOver += gvList_DragOver;
                DragDrop += MailCenterUI_DragDrop;
                DragOver += MailCenterUI_DragOver;
            }
            else
            {
                Load -= MailCenterUI_Load;                      //窗体加载
                tsmiBtnOE.Click -= tsmiBtnOE_Click;             //海出订舱
                tsmiBtnAssociate.Click -= tsmiBtnAssociate_Click;                     //关联
                tsmiBtnAssociateAsCustomerMail.Click += tsmiBtnAssociateAsCustomerMail_Click;  //关联并设置为客户邮件
                tsmiBtnAssociateAsCarrierMail.Click += tsmiBtnAssociateAsCarrierMail_Click;    //关联并设置为承运人邮件
                tsmiBtnAdvancedquery.Click -= tsmiBtnAdvancedquery_Click;   //高级查询

                gvList.CellDoubleClick -= gvList_CellDoubleClick;       //列双击
                gvList.CellMouseDown -= gvList_CellMouseDown;           //列右键菜单
                gvList.DataBindingComplete -= gvList_DataBindingComplete;//数据绑定后：列样式设置


                DragDrop -= MailCenterUI_DragDrop;
                DragOver -= MailCenterUI_DragOver;
                gvList.DragDrop -= gvList_DragDrop;
                gvList.DragOver -= gvList_DragOver;

            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        void InitControl()
        {
            #region 文本及其样式设置
            if (!LocalData.IsDesignMode)
            {
                if (LocalData.IsEnglish)
                {
                    tsBtnNewBusiness.Text = "New";
                    tsmiBtnOE.Text = "Ocean Export";
                    tsddBtnAssociate.Text = "Associate";
                    tsddBtnAssociate.ToolTipText = "Associate";

                    tsmiBtnAssociate.Text = "Associate As Co-worker";
                    tsmiBtnAssociateAsCustomerMail.Text = "Associate as Customer Mail";
                    tsmiBtnAssociateAsCarrierMail.Text = "Associate as Carrier Mail";
                    tsmiBtnAdvancedquery.Text = "Search";
                    tsmiBtnAdvancedquery.ToolTipText = "Search";
                }
            }
            gvList.AutoGenerateColumns = false;
            #endregion
            //事件添加
            OperateEvent(true);
        }

        void InitData()
        {
            try
            {
                Parameter.FlagFinish = 0;
                _ShowRefreshDataException = true;
                //RefreshTime = new Timer();
                //RefreshTime.Interval = 1000;
                //RefreshTime.Tick += RefreshData;
                _Assist = new Assist();
                ContextMenuTemplate = new ContextMenuTemplate();
                OeToolStripMenuItems = new List<ToolStripMenuItem>();
                OiToolStripMenuItems = new List<ToolStripMenuItem>();
                InToolStripMenuItems = new List<ToolStripMenuItem>();
                ToolStripMenu = new List<ToolStripMenuItem>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 判断当前的单号是否已经关联
        /// </summary>
        /// <param name="no">业务号</param>
        /// <returns></returns>
        bool IsRelationData(string no)
        {
            bool flag = false;
            DataTable dt = UIHelper.MessageRelationOperationList;
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows.Cast<DataRow>().Any(row => row["NO"].ToString() == no))
                {
                    flag = true;
                }
            }
            return flag;
        }

        /// <summary>
        /// 附件上传
        /// </summary>
        public void Attachmentupload(byte[] filenames)
        {
            try
            {
                bool flg = false;
                //得到拖动文件上传的名称
                string fileName = Encoding.Default.GetString(filenames);
                fileName = fileName.Replace("\0", "");
                string extension = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf("."));
                //string extension = fileName.GetExtension();
                //得到附件可以上传的扩展名
                string[] fileExtensions = FilterFilesExtension();
                string[] temp = fileExtensions.Where<string>(o => o.Equals(extension.ToLower())).ToArray();
                if (temp != null && temp.Length == 0)
                {
                    //定义改变上传附件是鼠标光标变为初始化状态
                    Action act = new Action(() => MessageBox.Show(LocalData.IsEnglish ? "could not upload the unknown file type." : "不能上传未知文件类型."));
                    act.BeginInvoke(null, null);
                    return;
                }
                //获取附件真实地址(另存附件至临时目录，上传时从临时目录读取文件)
                MessageEntity.Attachments = OutlookUtility.GetAttachmentContentsByMailItem(HelpMailStore.CurrentMailItem as _MailItem); ;
                //和邮件实体比较，查询是否包含当前附件
                if (MessageEntity.Attachments.Any())
                {
                    foreach (var item in MessageEntity.Attachments.Where(item => fileName.Contains(item.Name)))
                    {
                        flg = true;
                    }
                }
                //当前邮件实体包含附件信息，打开上传附件UI 
                if (flg)
                {
                    using (new CursorHelper(Cursors.WaitCursor))
                    {
                        DateTime? dateTime = null;
                        if (!string.IsNullOrEmpty(DataRowView["UpdateDate"].ToString()))
                        {
                            dateTime = DateTime.Parse(DataRowView["UpdateDate"].ToString());
                        }
                        if (DataCacheOperationService != null)
                            ICPCommonOperationService.Upload(UploadWay.DirectOpen, OperationId, MessageEntity.Subject, MessageEntity,
                            SelectionType.Normal, OperationType, new List<string>(), OperationNo, dateTime, MessageEntity);

                    }
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("Attachmentupload", ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取当前客户端操作系统
        /// </summary>
        /// <returns></returns>
        private string getOSVersion()
        {
            try
            {
                string osVersion = string.Empty;
                RegistryKey rk = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion");
                if (rk != null)
                {
                    osVersion = rk.GetValue("ProductName").ToString();
                }
                return osVersion;
                }
            catch
            {
                return "unknown";
            }
        }
        /// <summary>
        /// 启用，禁用关联
        /// </summary>
        /// <param name="controlType">类型：面板、关联相关控件</param>
        /// <param name="enableValue">Boolean值：可用、不可用</param>
        private void EnableAssociate(string controlType, bool enableValue)
        {
            switch (controlType)
            {
                case "Part": //面板操作
                    Enabled = enableValue;
                    break;
                case "Associate":
                    tsddBtnAssociate.Enabled = enableValue;
                    break;
                default:
                    tsBtnNewBusiness.Enabled = enableValue;
                    tsddBtnAssociate.Enabled = enableValue;
                    tsmiBtnAdvancedquery.Enabled = enableValue;
                    gvList.Enabled = enableValue;
                    break;
            }
        }
        #endregion

        #region 当选择邮件时进行触发
        /// <summary>
        /// 当选择邮件时进行触发
        /// </summary>
        public void OnCurrentMail_Changed(object currentItem, out int num)
        {
            HelpMailStore.msg = null;
            HelpMailStore.CurrentMailItem = null;
            DataTable dt = null;
            CurrentMailAttachments = null;
            UIHelper.MessageRelationOperationList = null; //此处清空，根据关联信息获取业务集合时赋值，高级搜索中用到
            BusinessQueryCriteria criteria;
            OperationMessageController operationMsgCtr;
            OperationSaveController operationSaveCtr;
            List<OperationMessageRelation> messageRelations = null;
            num = 0;
            Stopwatch totalWatch = null;
            Stopwatch searchData = null;
            StringBuilder strLog = null;
            if (DataCacheOperationService == null)
            {
                EnableAssociate("Part", false);
                return;
            }
            try
            {
                EnableAssociate("", true);
                totalWatch = StopwatchHelper.StartStopwatch();
                
                strLog = new StringBuilder();
                //1--Convert Message Begin
                strLog.AppendFormat("{0}\t", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                #region 1.当前邮件转换成Message对象
                searchData = StopwatchHelper.StartStopwatch();
                if (currentItem is MailItem) //当前为邮件
                {
                    MailItem currentMail = currentItem as MailItem;
                    if (currentMail.Sent)
                    {
                        HelpMailStore.CurrentMailItem = currentItem;
                        MessageEntity = OutlookUtility.ConvertMailItemToMessageInfo(currentMail);
                        if (MessageEntity != null)
                        {
                            MessageEntity.IsMailItem = true;
                        }
                    }
                }
                else if (currentItem is ReportItem) //当前为报文
                {
                    _ReportItem reportItem = currentItem as ReportItem;
                    MessageEntity = OutlookUtility.ConvertReportItemToMessageInfo(reportItem);
                    if (MessageEntity != null)
                        MessageEntity.IsMailItem = false;
                }
                #endregion
                searchData.Stop();
                //2--Convert Message
                strLog.AppendFormat("{0}\t", searchData.ElapsedMilliseconds);

                //邮件或报文转换Message成功后查询业务数据并进行自动关联,失败则禁用面板
                if (MessageEntity != null && !string.IsNullOrEmpty(MessageEntity.EntryID))
                {
                    if (!MessageEntity.EntryID.Equals(HelpMailStore.EntryID))
                    {
                        if (string.IsNullOrEmpty(MessageEntity.MessageId))
                            EnableAssociate("Associate", false);

                        HelpMailStore.msg = MessageEntity; //全局缓存邮件实体
                        HelpMailStore.EntryID = MessageEntity.EntryID;
                        criteria = new BusinessQueryCriteria();
                        criteria.TemplateCode = TemplateCode;
                        operationMsgCtr = new OperationMessageController();
                        operationSaveCtr = new OperationSaveController();
                        //3--EntryID
                        strLog.AppendFormat("{0}\t", MessageEntity.EntryID);
                        #region 2.查找关联信息
                        searchData.Reset();
                        searchData.Start();
                        //4--Search Message Relations Begin
                        strLog.AppendFormat("{0}\t", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        criteria.companyIDs = string.IsNullOrEmpty(GetQueryConditions.SelectedCompanyIds)
                            ? null
                            : GetQueryConditions.SelectedCompanyIds.Split(',').Select(id => new Guid(id)).ToArray();
                        string messageReference = string.Empty;
                        //不包含未保存外部联系人且父邮件存在关联信息则获取父邮件的关联IMessageID
                        if (MessageEntity.UserProperties != null && operationSaveCtr.IsSaveExternalMail(MessageEntity))
                        {
                            messageReference = MessageEntity.UserProperties.Reference;
                        }
                        messageRelations =
                            operationMsgCtr.GetOperationMessageRelationByMessageIdAndReference(MessageEntity.MessageId,
                                messageReference); //查找关联信息
                        HelpMailStore.CurrentMessageRelation = messageRelations;//全局缓存当前邮件的关联信息
                        searchData.Stop();
                        //5--Search Message Relations
                        strLog.AppendFormat("{0}\t", searchData.ElapsedMilliseconds);

                        #endregion

                        #region 3.查找业务数据

                        searchData.Reset();
                        searchData.Start();
                        //6--Search Business Begin
                        strLog.AppendFormat("{0}\t", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        //如果找到关联信息，返回业务数据
                        if (messageRelations != null && messageRelations.Count > 0)
                        {
                            //设置该邮件存储在数据库中的IMessageID，方便后续关联业务使用
                            MessageEntity.Id = messageRelations[0].IMessageId;
                            MessageEntity.RelationType = messageRelations[0].RelationType;
                            dt = operationMsgCtr.GetOperationListByMessageRelations(criteria, messageRelations.ToArray());
                            //缓存系统自动默认搜索的关联业务集合
                            if (dt != null)
                            {
                                UIHelper.MessageRelationOperationList = dt.Copy(); //高级搜索用到
                            }
                        }
                        else
                        {
                            //关联信息没有找到，再根据主题单号查找
                            criteria.AdvanceQueryString = GetQueryConditions.AppendAdvanceStringToSQL(MessageEntity.Subject);
                            if (!string.IsNullOrEmpty(criteria.AdvanceQueryString))
                            {
                                criteria.SearchType = SearchActionType.SubjectInNO;
                                criteria.TemplateCode = TemplateCode;
                                dt = operationSaveCtr.GetOperationListBySubjectInNO(criteria, MessageEntity);
                                if (dt == null || dt.Rows.Count <= 0)
                                {
                                    //没有找到，根据主题识别列表获取业务列表
                                }
                            }
                        }
                        searchData.Stop();
                        //7--Search Business Relations
                        strLog.AppendFormat("{0}\t", searchData.ElapsedMilliseconds);

                        #endregion

                        num = dt != null ? dt.Rows.Count : 0;
                        DataBind(dt);
                        searchData.Reset();
                        searchData.Start();
                        //8--PostHandle Begin
                        strLog.AppendFormat("{0}\t", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        //绑定后的处理
                        BusinessQueryResult result = new BusinessQueryResult();
                        result.Relations = messageRelations;
                        result.Dt = dt;
                        operationSaveCtr.PostHandle(result, null, currentItem);
                        searchData.Stop();
                        totalWatch.Stop();
                        //9--PostHandle
                        strLog.AppendFormat("{0}\t", searchData.ElapsedMilliseconds);
                        //10--Total
                        strLog.AppendFormat("{0}\t", totalWatch.ElapsedMilliseconds);
                        if (num > 0 && MessageEntity.IsAssociated)
                        {
                            strLog.AppendFormat("{0}\t", "自动关联");
                            operationSaveCtr.SaveAsMail(MessageEntity.EntryID, MessageEntity.BackupMailState, currentItem);
                        }
                        else
                            strLog.AppendFormat("{0}\t", "选择邮件");

                        
                    }else
                        strLog = null;
                }
                else
                {
                    strLog = null;
                    MessageEntity = null;
                    EnableAssociate("Part", false);
                }
                Parameter.FlagFinish = (Parameter.FlagFinish == 0) ? 2 : Parameter.FlagFinish;
            }
            catch (Exception ex)
            {
                HelpMailStore.msg = null;
                HelpMailStore.CurrentMailItem = null;
                UIHelper.MessageRelationOperationList = null; //此处清空，根据关联信息获取业务集合时赋值，高级搜索中用到
                dt = null;
                CurrentMailAttachments = null;
                messageRelations = null;
                MessageEntity = null;

                if (!IsICPConnectionException(ex))
                {
                    ToolUtility.WriteLog("OnCurrentMail", ex);
                    MessageBox.Show("OnCurrentMail_Changed" + Environment.NewLine + ex);
                }
            }
            finally
            {
                if (strLog != null && _loadData != -1)
                    ToolUtility.TempWriteLog("SelectedMail", strLog.ToString());
                totalWatch = null;
                searchData = null;
                strLog = null;
                criteria = null;
                operationMsgCtr = null;
                operationSaveCtr = null;
                if (messageRelations != null)
                    messageRelations.Clear();
                messageRelations = null;
            }
        }
        #endregion

        #region ICP连接异常处理
        /// <summary>
        /// 是否ICP连接异常
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public bool IsICPConnectionException(Exception ex)
        {
            if (ex == null || ex.Message.Contains("没有终结点在侦听可以接受消息的") ||
                ex.Message.Contains("There was no endpoint listening at net.pipe")
                || ex.Message.Contains("net.pipe:"))
            {
                Parameter.IsEnableICPConnection = false;
                MessageBox.Show(
                    OutlookUtility.IsEnglish ? "ICP is Closed!" : "无法连接到ICP，关联面板将禁用，请ICP正常运行后重启OutLook！"
                    , OutlookUtility.IsEnglish ? "Tips" : "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EnableAssociate("Part", false);
                return true;
            }
            return false;
        }
        #endregion

        #region 判断当前激活窗体是否为Outlook
        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "GetWindowText")]
        static extern int GetWindowText(IntPtr handle, StringBuilder text, int MaxLen);
        /// <summary>
        /// 当前窗体是否为OutLook
        /// </summary>
        /// <returns></returns>
        public bool WhetherOutLook()
        {
            StringBuilder stringBuilder = new StringBuilder(1024);
            int i = GetWindowText(GetForegroundWindow(), stringBuilder, stringBuilder.Capacity);
            return stringBuilder.ToString().Contains("Microsoft Outlook");
        }
        #endregion

        ///// <summary>
        ///// 刷新业务数据
        ///// </summary>
        //void StartRefreshData()
        //{
        //    RefreshTime.Enabled = true;
        //    RefreshTime.Start();
        //}

        ///// <summary>
        ///// 停止业务数据
        ///// </summary>
        //void StopRefreshData()
        //{
        //    if (RefreshTime != null && RefreshTime.Enabled)
        //    {
        //        RefreshTime.Stop();
        //        RefreshTime.Enabled = false;
        //    }
        //}
    }
}