using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.ViewInfo;
using ICP.Business.Common.ServiceInterface;
using ICP.Common.CommandHandler.ServiceInterface;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using ICP.Operation.Common.ServiceInterface;
using ICP.Operation.Common.UI;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.TaskCenter.ServiceInterface;
using ICP.TaskCenter.ServiceInterface.Common;
using ICP.TaskCenter.UI.MainWork;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace ICP.TaskCenter.UI
{
    /// <summary>
    /// 任务中心功能树视图列表用户控件
    /// </summary>
    [SmartPart]
    public partial class ViewListSmartPart : XtraUserControl
    {
        #region Fields & Services & Property & Delegate
        #region Fields
        /// <summary>
        /// 当前节点ID
        /// </summary>
        string CurrentID = "";
        /// <summary>
        /// 记录新增节点的信息
        /// </summary>
        private TreeListNode _node = null;
        private NodeInfo template = null;
        /// <summary>
        /// 根节点
        /// </summary>
        private NodeInfo _parentNode = null;
        /// <summary>
        /// 订单工作区控制器
        /// </summary>
        MainWorkController mainWorkController = null;
        /// <summary>
        /// 异步读取中正在加载的节点集合
        /// </summary>
        private List<NodeInfo> loadingNodes = new List<NodeInfo>();
        /// <summary>
        /// 提示信息
        /// </summary>
        private string tempNodeCaption = LocalData.IsEnglish ? "(Loading Child Nodes...)" : "(正在载入子节点...)";
        /// <summary>
        /// 节点的ID
        /// </summary>
        Guid nodeinfoid;
        /// <summary>
        /// 节点所属人的
        /// </summary>
        Guid userId;
        /// <summary>
        /// 节点实体对象
        /// </summary>
        private NodeInfo temp;
        /// <summary>
        ///  默认组织结构信息
        /// </summary>
        List<LocalOrganizationInfo> ListOranizationInfo = new List<LocalOrganizationInfo>();
        /// <summary>
        /// 组织根
        /// </summary>
        private Guid OrganizationRoot = new Guid("7ca04a4e-e40b-e011-916b-001321cc6d9f");
        /// <summary>
        /// 读取模版的路径
        /// </summary>
        private readonly string _fileRootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
        /// <summary>
        /// Xml的文件名称
        /// </summary>
        private const string TempalteFileName = "QueryConditions.xml";
        /// <summary>
        /// 查询节点ID
        /// </summary>
        private static Guid searchNodeID = Guid.NewGuid();
        /// <summary>
        /// 协助同事UI
        /// </summary>
        public UserAssists UserAssists = new UserAssists();
        #endregion

        #region Services
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }
        /// <summary>
        /// Client WorkItem
        /// </summary>
        public WorkItem ClientWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        /// <summary>
        /// RCM 提供给外界调用的公共服务接口
        /// </summary>
        public IICPCommonOperationService ICPCommonOperationService
        {
            get { return ServiceClient.GetService<IICPCommonOperationService>(); }
        }
        /// <summary>
        /// 用户服务
        /// </summary>
        public IUserService UserService
        {
            get { return ServiceClient.GetService<IUserService>(); }
        }

        #endregion

        #region Property
        /// <summary>
        /// 操作口岸ID集合
        /// </summary>
        public List<Guid> CompanyIDs
        {
            get
            {
                return (from CheckedListBoxItem item in cmbOpBranch.Properties.Items
                        where item.CheckState == CheckState.Checked
                        select new Guid(item.Value.ToString())).ToList();
            }
        }
        /// <summary>
        /// Treeview 数据源
        /// </summary>
        private List<NodeInfo> CurrentNodeList
        {
            get
            {
                return bindingSourceNodes.DataSource as List<NodeInfo>;
            }
        }
        /// <summary>
        /// 订单工作区控制器
        /// </summary>
        public MainWorkController MainWorkController
        {
            get
            {
                if (mainWorkController != null)
                    return mainWorkController;
                else
                {
                    mainWorkController = RootWorkItem.Items.AddNew<MainWorkController>();
                    return mainWorkController;
                }
            }
        }
        /// <summary>
        /// 程序集名称
        /// </summary>
        private string AssamblyName
        {
            get
            {
                MethodBase methodother = MethodBase.GetCurrentMethod();
                if (methodother.DeclaringType != null)
                    return methodother.DeclaringType.FullName;
                return "ICP.TaskCenter.UI";
            }
        }
        #endregion

        #region Delegate
        /// <summary>
        /// 当前视图节点改变事件
        /// </summary>
        public EventHandler<CommonEventArgs<string>> ViewNodeChangedEventHandler;
        /// <summary>
        /// 枚举
        /// </summary>
        private EventHandler<CommonEventArgs<ChildrenNodeDataFetchParameter>> ChildrenNodeDataFetchFinishedHandler;
        /// <summary>
        /// 枚举
        /// </summary>
        /// <param name="parameter"></param>
        private delegate void RefreshDelegate(ChildrenNodeDataFetchParameter parameter);
        #endregion

        #endregion

        #region Init
        /// <summary>
        /// 构造函数
        /// </summary>
        public ViewListSmartPart()
        {
            InitializeComponent();
            RegisterEvent();
            if (!LocalData.IsDesignMode)
            {
                string TipCN = "请输入No, BL No, CTN No, SO No, PO No 或客户名称后按钮回车"
                    + Environment.NewLine + "110022 查找一个单号的业务"
                    + Environment.NewLine + "110022/110033查找2个单号的业务"
                    + Environment.NewLine + "Kosmos查找客户名称叫Kosmos的业务"
                    + Environment.NewLine + "Kosmos/Jade 查找客户名称叫Kosmos和Jade的业务";
                string TipEn = "Input No, BL No, CTN No, SO No, PO No or Customer then press Enter to search shipments"
                    + Environment.NewLine + "Search a shipment If NO contains  110022"
                    + Environment.NewLine + "Search shipments If NOs contains 110022 or 110033"
                    + Environment.NewLine + "Search shipments If it's customer is Kosmos"
                    + Environment.NewLine + "Search shipments If it's customer is Kosmos or Jade";
                txtquery.ToolTip = LocalData.IsEnglish ? TipEn : TipCN;
                barButDelete.Caption = LocalData.IsEnglish ? "Remove the result" : "移除结果";
                barButKeep.Caption = LocalData.IsEnglish ? "Keep the result" : "保留结果";
                btnAssists.Text = LocalData.IsEnglish ? "Assist Staff" : "协助同事";
            }
            bindingSourceNodes.DataSource = new List<NodeInfo>();
            treeListNodes.DataSource = bindingSourceNodes;
            ChildrenNodeDataFetchFinishedHandler += OnNodeDataFetchFinsihed;
            Disposed += (sender, arg) =>
            {
                UnRegisterEvent();
                ViewNodeChangedEventHandler = null;
                loadingNodes = null;
                ChildrenNodeDataFetchFinishedHandler = null;
                if (RootWorkItem != null)
                {
                    RootWorkItem.Items.Remove(mainWorkController);

                    RootWorkItem.SmartParts.Remove(this);
                    RootWorkItem = null;
                }
                mainWorkController = null;
            };
        }

        /// <summary>
        /// 重写加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //打开界面以后锁定光标于文本框中
            txtquery.Focus();
            InitData();
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
            txtquery.KeyDown += txtquery_KeyDown;
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        private void UnRegisterEvent()
        {
            txtquery.KeyDown -= txtquery_KeyDown;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitData()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                #region Nodes
                List<NodeInfo> nodes = MainWorkController.OperationViewService.GetUserRootOperationViewList();

                CopyLocalOrganizationInfo();

                if (nodes.Exists((NodeInfo tmp) => { return tmp.Id == LocalData.UserInfo.LoginID; }))
                {
                    removeSingleRoot(OrganizationRoot, LocalData.UserInfo.LoginID);
                    if (ListOranizationInfo.Count > 0)
                    {
                        foreach (LocalOrganizationInfo tmp in ListOranizationInfo)
                        {
                            nodes.Add(LocalOrganizationInfoToNodeInfo(tmp));
                        }
                    }

                }
                #endregion

                #region Handler
                EventHandler<CommonEventArgs<ChildrenNodeDataFetchParameter>> handler = Interlocked.CompareExchange(ref ChildrenNodeDataFetchFinishedHandler, null, null);

                if (handler != null)
                {
                    handler(null, new CommonEventArgs<ChildrenNodeDataFetchParameter>(new ChildrenNodeDataFetchParameter { Parent = null, Children = nodes }));
                }
                #endregion

                #region Controls Enable
                txtquery.Enabled = btnAdvancedquery.Enabled = btnEndtAdvancedquery.Enabled = Convert.ToBoolean(nodes.Count);
                #endregion

                #region Company
                //获取用户所挂公司列表
                List<OrganizationList> userCompanyList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
                cmbOpBranch.Properties.Items.BeginUpdate();
                cmbOpBranch.Properties.Items.Clear();
                foreach (var item in userCompanyList)
                {
                    cmbOpBranch.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                        item.ID == LocalData.UserInfo.DefaultCompanyID ? CheckState.Checked : CheckState.Unchecked, true);
                }
                #endregion
            }

            if (treeListNodes.Nodes.Count > 0)
            {
                txtquery.Enabled = true;
                btnAdvancedquery.Enabled = true;
                treeListNodes.Nodes[0].ExpandAll();
                template = treeListNodes.GetDataRecordByNode(treeListNodes.Nodes[0]) as NodeInfo;
                if (template != null)
                {
                    SaveNodeInfo(template.ViewCode, LocalData.UserInfo.LoginID);
                }
            }
        }
        #endregion

        #region Controls Event
        #region  文本框查询
        /// <summary>
        /// 回车执行查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtquery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QuickQuery();
            }
        }
        /// <summary>
        /// 按钮查询
        /// </summary>
        private void btnEndtAdvancedquery_Click(object sender, EventArgs e)
        {
            QuickQuery();
        }
        #endregion

        #region 高级查询按钮
        /// <summary>
        /// 高级查询
        /// </summary>
        private void BtnAdvancedquery_Click(object sender, EventArgs e)
        {
            txtquery.EditValue = null;
            txtquery.Text = string.Empty;
            var initValues = new Dictionary<string, object>();
            /****数据库里面加字段来标识当前节点的类型****/
            if (template.OperationType == OperationType.Unknown)
            {
                var nodeInfoList = bindingSourceNodes.DataSource as List<NodeInfo>;
                NodeInfo Node = nodeInfoList.FirstOrDefault(n => n.ParentId == template.Id && n.OperationType != OperationType.Unknown);
                if (Node != null)
                {
                    template.OperationType = Node.OperationType;
                }
            }
            if (template.OperationType == OperationType.Unknown) return;
            //节点为子节点时操作
            switch (template.OperationType)
            {
                case OperationType.OceanExport:
                    initValues.Add(Constants.BusinessTypeKey, BusinessType.OE);
                    break;
                case OperationType.OceanImport:
                    initValues.Add(Constants.BusinessTypeKey, BusinessType.OI);
                    break;
                case OperationType.WorkFlow:
                    initValues.Add(Constants.BusinessTypeKey, BusinessType.WF);
                    break;
                case OperationType.InquireRate:
                    initValues.Add(Constants.BusinessTypeKey, BusinessType.IR);
                    break;
            }
            //}
            //高级查找时是否显示可以选择业务（1为可以选择。0为不可选择）
            initValues.Add("ShowOperationType", 0);
            var frmQuery = RootWorkItem.Items.AddNew<frmAdvanceQuery>();

            frmQuery.Init(initValues);
            frmQuery.MaximizeBox = frmQuery.MinimizeBox = false;
            frmQuery.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            var result = frmQuery.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                //这里加入查询的条件
                var advanceQueryString = frmQuery.AdvanceQueryString;
                if (!string.IsNullOrEmpty(advanceQueryString))
                {
                    Stopwatch stopwatch = StopwatchHelper.StartStopwatch();
                    AddTreeListNode(advanceQueryString, 0);
                    //高级查询条件的保存
                    if (GetQueryConditions() == false)
                    {
                        AddQueryConditions(advanceQueryString);
                    }
                    else
                    {
                        UpdateQueryConditions(advanceQueryString);
                    }
                    StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, AssamblyName, "SEARCH", "任务中心高级查找");
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(),
                                                                LocalData.IsEnglish
                                                                    ? "The query result failure, please select nodes to query again." : "查询结果失败，请重新选择节点进行查询.");
                }

            }
            // }

        }
        #endregion

        #region Treeview
        /// <summary>
        /// 节点选择
        /// </summary>
        void treeListNodes_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button != MouseButtons.Left)
                return;
            TreeListHitInfo hitInfo = treeListNodes.CalcHitInfo(new Point(e.X, e.Y));

            TreeListNode node = hitInfo.Node;
            if (node == null)
                return;
            template = treeListNodes.GetDataRecordByNode(node) as NodeInfo;
            if (template == null)
                return;
            template.LockCompanyIDs = string.Empty;
            foreach (var itemID in CompanyIDs)
            {
                if (string.IsNullOrEmpty(template.LockCompanyIDs))
                {
                    template.LockCompanyIDs = itemID.ToString();
                }
                else
                {
                    template.LockCompanyIDs = template.LockCompanyIDs + "&#;" + itemID;
                }
            }
            if (node.HasChildren)
            {
                template.HasFetchChildrenData = true;
            }
            //SetQueryButtonEnable(template);

            if (node.ParentNode == null)
            {
                _parentNode = template;
                btnAdvancedquery.Enabled = true;
            }
            else
            {
                //获取子节点下面的节点信息
                List<NodeInfo> nodes = MainWorkController.OperationViewService.GetWorkSpaceSonViewList(template.SqlId, template.Id);
                if (nodes.Any())
                {
                    bool are = false;
                    if (CurrentNodeList.Any())
                    {
                        foreach (var item in nodes)
                        {
                            if (CurrentNodeList.FirstOrDefault(n => n.Caption == item.Caption) == null && are == false)
                            {
                                are = true;
                            }
                        }
                    }
                    if (are)
                    {
                        EventHandler<CommonEventArgs<ChildrenNodeDataFetchParameter>> handler = Interlocked.CompareExchange(ref ChildrenNodeDataFetchFinishedHandler, null, null);
                        if (handler != null)
                        {
                            handler(null, new CommonEventArgs<ChildrenNodeDataFetchParameter>(new ChildrenNodeDataFetchParameter { Parent = template, Children = nodes }));
                        }
                        return;
                    }
                }
            }
            ProcessNode(template, node, hitInfo);
            if (!string.IsNullOrEmpty(template.ViewCode))
            {
                if (template.ViewCode.Contains("TaskCenter_EDI_ALL") || template.ViewCode.Contains("TaskCenter_EDI_AMS"))
                {
                    ICPCommonOperationService.TemplateCode = template.ViewCode;
                    mainWorkController.ShowTaskItems(template);
                }
                else if (template.Caption.Contains(LocalData.IsEnglish ? "Search Result" : "查询结果") == false &&
                    string.IsNullOrEmpty(template.BaseCriteria))
                {
                    return;
                }
                else
                {
                    ICPCommonOperationService.TemplateCode = template.ViewCode;
                    mainWorkController.ShowTaskItems(template);
                }
            }
        }
        /// <summary>
        /// 节点选择
        /// </summary>
        void treeListNodes_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (e.Node == e.OldNode)
                return;
            NodeInfo template = treeListNodes.GetDataRecordByNode(e.Node) as NodeInfo;
            if (template == null || string.IsNullOrEmpty(template.ViewCode))
                return;
            if (ViewNodeChangedEventHandler != null)
            {
                ViewNodeChangedEventHandler(this, new CommonEventArgs<string>(template.ViewCode));
            }
        }
        /// <summary>
        /// 节点之前的图片
        /// </summary>
        void treeListNodes_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            NodeInfo template = treeListNodes.GetDataRecordByNode(e.Node) as NodeInfo;
            if (template != null)
                e.NodeImageIndex = (template.HasChildren && !e.Node.Expanded) ? 0 : 1;
        }
        /// <summary>
        /// 树的ToolTip显示
        /// </summary>
        private void toolTipTreeList_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl is TreeList)
            {
                TreeList tree = (TreeList)e.SelectedControl;

                TreeListHitInfo hit = tree.CalcHitInfo(e.ControlMousePosition);

                if (hit.HitInfoType == HitInfoType.Cell)
                {

                    object cellInfo = new TreeListCellToolTipInfo(hit.Node, hit.Column, null);

                    NodeInfo template = treeListNodes.GetDataRecordByNode(hit.Node) as NodeInfo;
                    if (template != null)
                    {
                        if (string.IsNullOrEmpty(template.TooltiopCn) ||
                           string.IsNullOrEmpty(template.TooltiopEn))
                        {
                            return;
                        }
                        else
                        {
                            string toolTipcontent = LocalData.IsEnglish
                                                    ? template.TooltiopEn.Trim()
                                                    : template.TooltiopCn.Trim();
                            e.Info = new ToolTipControlInfo(cellInfo, toolTipcontent);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 树右键菜单显示
        /// </summary>
        private void treeListNodes_MouseUp(object sender, MouseEventArgs e)
        {
            TreeList tree = sender as TreeList;
            if (e.Button == MouseButtons.Right
                    && ModifierKeys == Keys.None
                    && treeListNodes.State == TreeListState.Regular)
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                #region 获取当前选择的节点的对象
                TreeListHitInfo hitInfo = tree.CalcHitInfo(e.Location);
                TreeListNode node = hitInfo.Node;
                if (node == null)
                    return;
                template = treeListNodes.GetDataRecordByNode(node) as NodeInfo;
                #endregion
                if (template != null && template.Caption.Contains(LocalData.IsEnglish ? "Search Result" : "查询结果") == false)
                    return;
                if (hitInfo.HitInfoType == HitInfoType.Cell)
                {
                    tree.SetFocusedNode(hitInfo.Node);
                }
                if (tree.FocusedNode != null)
                {
                    popupMenu1.ShowPopup(p);
                }
            }
        }
        /// <summary>
        /// 删除节点信息
        /// </summary>
        private void barButDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            CurrentNodeList.Remove(template);
            bindingSourceNodes.ResetBindings(false);
            ClientWorkItem.Commands[TaskCenterCommandConstants.SetReadOnly].Execute();
            //删除节点后清空查询条件，默认选择第一个节点并且光标锁定在文本框中方便查询
            txtquery.EditValue = string.Empty;
            treeListNodes.SetFocusedNode(treeListNodes.Nodes.FirstNode);
            txtquery.Focus();
        }
        /// <summary>
        /// 保留节点信息
        /// </summary>
        private void barButKeep_ItemClick(object sender, ItemClickEventArgs e)
        {
            template.Keep = true;
            if (!string.IsNullOrEmpty(txtquery.EditValue.ToString().Trim()))
            {
                template.Caption = template.Caption + "(" +
                                   txtquery.EditValue.ToString().Trim() + ")";
            }
            else
            {
                foreach (var node in bindingSourceNodes)
                {
                    NodeInfo info = node as NodeInfo;
                    if (info == null)
                        continue;
                    if (info.Caption.Contains(LocalData.IsEnglish ? "Search Result(" : "查询结果("))
                    {
                        string[] Caption = info.Caption.Split('(', ')');
                        if (Caption.Count() > 2)
                        {
                            int index = int.Parse(Caption[1]) + 1;
                            info.Caption = LocalData.IsEnglish ? "Search Result(" + index + ")" : "查询结果(" + index + ")";
                        }
                    }
                    else if (info.Caption.Contains(LocalData.IsEnglish ? "Search Result" : "查询结果"))
                    {
                        info.Caption = LocalData.IsEnglish ? "Search Result(" + 1 + ")" : "查询结果(" + 1 + ")";
                    }

                }
            }
            bindingSourceNodes.ResetBindings(false);
        }
        #endregion

        #region 按钮的提示信息
        /// <summary>
        /// 按钮的提示信息
        /// </summary>
        private void barManager1_HighlightedLinkChanged(object sender, HighlightedLinkChangedEventArgs e)
        {
            if (e.Link == null)
            {
                barManager1.GetToolTipController().HideHint();
                return;
            }
            if (!e.Link.IsLinkInMenu) return;

            ToolTipControllerShowEventArgs te = new ToolTipControllerShowEventArgs();
            te.ToolTipLocation = ToolTipLocation.Fixed;
            te.SuperTip = new SuperToolTip();
            string Tip = string.Empty;
            string KeepTip = LocalData.IsEnglish ? "Clicks if the search result to be kept." : "单击保留此查询结果。";
            string DeleteTipe = LocalData.IsEnglish ? "Clicks if the earch result to be removed." : "单击移除此查询结果。";
            Tip = e.Link.Item.Name == "barButKeep" ? KeepTip : DeleteTipe;
            te.SuperTip.Items.Add(Tip);
            Point linkPoint = new Point(e.Link.Bounds.Right, e.Link.Bounds.Top);
            barManager1.GetToolTipController().ShowHint(te, e.Link.LinkPointToScreen(linkPoint));
        }
        #endregion

        #region 协助同事
        /// <summary>
        /// 协助同事按钮
        /// </summary>
        private void simpleButtonAssists_Click(object sender, EventArgs e)
        {
            UserAssists.ViewListSmartPart = this;
            UserAssists.Text = LocalData.IsEnglish ? "Assist Staff" : "协助同事";
            UserAssists.ShowDialog();
        }
        #endregion
        #endregion

        #region  CommandHandler 列表按F5查询/更多查找
        /// <summary>
        /// 列表按F5查询
        /// </summary>
        [CommandHandler(CommonCommandName.Command_F5_Search)]
        public void Command_F5_Search(object sender, EventArgs eventArgs)
        {

            try
            {
                txtquery.EditValue = null;
                /****数据库里面加字段来标识当前节点的类型****/

                //节点是人员节点直接返回
                if (template.NodeType == NodeType.ParentStaff || template.NodeType == NodeType.Staff)
                {
                    return;
                }

                //这里加入查询的条件

                var advanceQueryString = RootWorkItem.RootWorkItem.State["AdventQueryString"].ToString();
                if (!advanceQueryString.Contains("$@IsValid"))
                {
                    if (template.OperationType == OperationType.InquireRate) //询价
                    {
                        advanceQueryString = GetIRAdvanceQueryString(advanceQueryString);
                    }
                    else if (template.OperationType == OperationType.WorkFlow
                      || template.Caption.Contains("流程工作")
                      || template.Caption.Contains("WorkFlow"))
                    {
                        advanceQueryString = GetWFAdvanceQueryString(advanceQueryString);
                    }
                    else
                    {
                        advanceQueryString = AdvanceQueryString(advanceQueryString, false, false);
                    }
                    if (template.OperationType != OperationType.WorkFlow
                        && template.OperationType != OperationType.InquireRate)
                    {
                        advanceQueryString += " and $@OPD@=0 ";
                    }
                }
                if (!string.IsNullOrEmpty(advanceQueryString))
                {
                    AddTreeListNode(advanceQueryString, 100);
                    //高级查询条件的保存
                    if (GetQueryConditions() == false)
                    {
                        AddQueryConditions(advanceQueryString);
                    }
                    else
                    {
                        UpdateQueryConditions(advanceQueryString);
                    }
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(),
                                                                LocalData.IsEnglish
                                                                    ? "The query result failure, please select nodes to query again." : "查询结果失败，请重新选择节点进行查询.");
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }


        }
        /// <summary>
        /// 更多查询
        /// </summary>
        [CommandHandler("Command_More_Search")]
        public void Command_More_Search(object sender, EventArgs eventArgs)
        {
            Stopwatch stopwatch = StopwatchHelper.StartStopwatch();
            string query = string.Empty;
            bool txtSearch = txtquery.EditValue != null;
            query = txtquery.EditValue == null ? RootWorkItem.RootWorkItem.State["AdventQueryString"].ToString() : txtquery.EditValue.ToString().Trim();
            if (string.IsNullOrEmpty(query))
                return;
            string advanceQueryString = string.Empty;
            if (template.OperationType == OperationType.InquireRate) //询价
            {
                advanceQueryString = GetIRAdvanceQueryString(query);
            }
            else if (template.OperationType == OperationType.WorkFlow
              || template.Caption.Contains("流程工作")
              || template.Caption.Contains("WorkFlow"))
            {
                advanceQueryString = GetWFAdvanceQueryString(query);
            }
            else
            {
                advanceQueryString = AdvanceQueryString(query, true, txtSearch);
            }
            //ICP.Operation.Common.UI-ListBaseBusinessPart 面板
            ListBaseBusinessPart listBaseBusinessPart = RootWorkItem.State["CurrentBaseBusinessPart"] as ListBaseBusinessPart;
            if (template != null)
            {
                //存在拼装条件直接查询
                if (!string.IsNullOrEmpty(advanceQueryString)
                    && template.OperationType != OperationType.WorkFlow
                    && template.OperationType != OperationType.InquireRate)
                {
                    AddTreeListNode(advanceQueryString, 2000);
                }
                else if (template.OperationType == OperationType.WorkFlow || template.OperationType == OperationType.InquireRate)
                {
                    //流程 & 询价
                    AddTreeListNode(advanceQueryString, 200);
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(),
                                               LocalData.IsEnglish
                                                   ? "The query result failure, please select nodes to query again."
                                                   : "查询结果失败，请重新选择节点进行查询.");
                }
            }
            StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, AssamblyName, "SEARCH", "任务中心更多查询|全局|搜索条件为" + query);
            //全局记录搜索条件文本框的值
            ClientWorkItem.State["Logsquery"] = query;
        }
        #endregion

        #region Custom Method

        #region  文本框输入查询
        /// <summary>
        /// 查询文本是否存在数字：包含数字则为单号查询，否则为客户查询
        /// </summary>
        /// <param name="query">字符</param>
        /// <returns></returns>
        public bool IsNumber(string query)
        {
            bool flg = false;
            int index = 0;
            for (int i = 0; i < query.Length; i++)
            {
                flg = char.IsNumber(query[i]);
                if (flg)
                {
                    index++;
                }
            }
            //数值超过1位 按照单号查询
            if (index > 1)
            {
                flg = true;
            }
            return flg;
        }
        /// <summary>
        /// 判断字符串中是否包含中文
        /// </summary>
        /// <param name="str">需要判断的字符串</param>
        /// <returns>判断结果</returns>
        public bool HasChinese(string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }
        /// <summary>
        /// 返回搜索条件
        /// </summary>
        /// <param name="query">搜索条件</param>
        /// <param name="global">是否需要加载用户默认的全部口岸(文本框查询是加载的是用户权限的默认口岸，点击更多按钮以后加载用户权限的所有口岸)</param>
        /// <param name="txtSearch">文本框查询</param>
        /// <returns></returns>
        public string AdvanceQueryString(string query, bool global, bool txtSearch = true)
        {
            string advanceQueryString = "1=1";
            string[] queryspit = txtSearch ? query.Split('/') : new[] { query };
            string operationIDs = string.Empty;
            //执行多条
            if (queryspit.Count() > 1)
            {
                for (int i = 0; i < queryspit.Count(); i++)
                {
                    //单号的查询
                    if (IsNumber(queryspit[i]))
                    {
                        operationIDs = GetOperationsIDByNo(queryspit[i], global);
                        if (!string.IsNullOrEmpty(operationIDs))
                        {
                            advanceQueryString += " and " + operationIDs;
                        }
                        else
                        {
                            if (advanceQueryString == "1=1")
                            {
                                advanceQueryString += " and ($@NO@ like  '%" + queryspit[i] + "%'";
                                advanceQueryString += " or $@RefNO@ like '%" + queryspit[i] + "%'";
                            }
                            else
                            {
                                advanceQueryString += " or $@NO@ like  '%" + queryspit[i] + "%'";
                                advanceQueryString += " or $@RefNO@ like '%" + queryspit[i] + "%'";
                            }
                        }
                    }
                    //客户名称的查询
                    else
                    {
                        string putStr = MainWorkController.OperationViewService.GetDeleteMarkerForInputStr(queryspit[i]);
                        if (advanceQueryString == "1=1")
                        {
                            advanceQueryString += "  and   (EXISTS (SELECT 1   FROM   fcm.FasterSearchCustomers fsc    INNER JOIN pub.VCustomers c   ON  c.ID = fsc.CustomerID  AND CHARINDEX("
                                + "'" + putStr + "'" + ", c.CodeName) > 0" + " where  fsc.OperationID = BOKI.ID)";
                        }
                        else
                        {

                            advanceQueryString += "   or  EXISTS (SELECT 1   FROM   fcm.FasterSearchCustomers fsc       INNER JOIN pub.VCustomers c   ON  c.ID = fsc.CustomerID  AND CHARINDEX("
                             + "'" + putStr + "'" + ", c.CodeName) > 0" + " where  fsc.OperationID = BOKI.ID)";
                        }
                    }
                }
            }
            //执行单个的查询
            else
            {
                if (IsNumber(query))
                {
                    operationIDs = GetOperationsIDByNo(query, global);
                    if (!string.IsNullOrEmpty(operationIDs))
                    {
                        advanceQueryString += " and " + operationIDs;
                    }
                    else
                    {
                        advanceQueryString += " and ($@NO@ like  '%" + query + "%'";
                        advanceQueryString += " or $@RefNO@ like '%" + query + "%'";
                        advanceQueryString += " or $@BLNo@ like '%" + query + "%'";
                    }
                }
                else
                {
                    string putStr = MainWorkController.OperationViewService.GetDeleteMarkerForInputStr(query);
                    advanceQueryString += "  and   EXISTS (SELECT 1   FROM   fcm.FasterSearchCustomers fsc     INNER JOIN pub.VCustomers c   ON  c.ID = fsc.CustomerID  AND CHARINDEX("
                           + "'" + putStr + "'" + ", c.CodeName) > 0" + " where  fsc.OperationID = BOKI.ID";
                }
            }
            if (string.IsNullOrEmpty(operationIDs))
                advanceQueryString += ")";
            #region  加载默认口岸还是全部口岸
            string companyId = string.Empty;
            if (global == false)
            {
                if (CompanyIDs != null && CompanyIDs.Count > 0)
                {
                    foreach (var itemID in CompanyIDs)
                    {
                        if (string.IsNullOrEmpty(companyId))
                        {
                            companyId = "'" + itemID + "'";
                        }
                        else
                        {
                            companyId = companyId + "," + "'" + itemID + "'";
                        }
                    }
                }
                else
                {
                    var copanyFirst = LocalData.UserInfo.UserOrganizationList.FirstOrDefault(o => o.Type == LocalOrganizationType.Company && o.IsDefault == true);
                    if (copanyFirst != null)
                    {
                        companyId = "'" + copanyFirst.ID + "'";
                    }
                }
            }
            else
            {
                var companyList = LocalData.UserInfo.UserOrganizationList.Where(o => o.Type == LocalOrganizationType.Company).ToList();
                //判断当前用户口岸是否多个（避免组成SQL条件失败）
                if (companyList.Any())
                {
                    foreach (var item in companyList)
                    {
                        if (string.IsNullOrEmpty(companyId))
                        {
                            companyId = "'" + item.ID + "'";
                        }
                        else
                        {
                            companyId = companyId + "," + "'" + item.ID + "'";
                        }
                    }
                }
            }
            if (companyId.Contains(",") == false)
            {
                advanceQueryString += " and $@CompanyID@ = " + companyId + " ";
            }
            else
            {
                advanceQueryString += " and $@CompanyID@ in (" + companyId + ")";
            }
            #endregion
            return advanceQueryString;
        }
        /// <summary>
        /// 流程业务构建查询语句
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public string GetWFAdvanceQueryString(string query)
        {
            string advanceQueryString = "1=1";
            if (IsNumber(query))
            {
                advanceQueryString += " $@No@='" + query + "'";
            }
            else
            {
                advanceQueryString += " $@WorkName@='" + query + "'";
            }

            return advanceQueryString;
        }
        /// <summary>
        /// 询价业务构建查询语句
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public string GetIRAdvanceQueryString(string query)
        {
            string advanceQueryString = "1=1";
            string[] queryspit = query.Split('/');
            //执行多条
            if (queryspit.Count() > 1)
            {
                for (int i = 0; i < queryspit.Count(); i++)
                {
                    if ("1=1".Equals(advanceQueryString)) //首次组装字符串：AND+(
                        advanceQueryString += " AND (";
                    else //后续组装：OR
                        advanceQueryString += " OR ";
                    if (IsNumber(queryspit[i])) //单号查询
                        advanceQueryString += " AND ($@No@ LIKE '%" + queryspit[i] + "%'";
                    else if (HasChinese(queryspit[i])) //含中文
                    {
                        //POL 
                        advanceQueryString += " $@POLCName@ LIKE '%" + queryspit[i] + "%'";
                        //POD
                        advanceQueryString += " OR $@PODCName@ LIKE '%" + queryspit[i] + "%' ";
                        //Delivery
                        advanceQueryString += " OR $@DeliveryCName@ LIKE '%" + queryspit[i] + "%' ";
                    }
                    else
                    {
                        //POL
                        advanceQueryString += " $@POLEName@ LIKE '%" + queryspit[i] + "%'";
                        //POD
                        advanceQueryString += " OR $@PODEName@ LIKE '%" + queryspit[i] + "%' ";
                        //Delivery
                        advanceQueryString += " OR $@DeliveryEName@ LIKE '%" + queryspit[i] + "%' ";
                    }
                }
            }
            //执行单个的查询
            else
            {
                if (IsNumber(query))
                {
                    advanceQueryString += " AND ($@No@ LIKE  '%" + query + "%'";
                }
                else if (HasChinese(query)) //含中文
                {
                    //POL 
                    advanceQueryString += " AND ($@POLCName@ LIKE '%" + query + "%'";
                    //POD
                    advanceQueryString += " OR $@PODCName@ LIKE '%" + query + "%' ";
                    //Delivery
                    advanceQueryString += " OR $@DeliveryCName@ LIKE '%" + query + "%' ";
                }
                else
                {
                    //POL
                    advanceQueryString += " AND ($@POLEName@ LIKE '%" + query + "%'";
                    //POD
                    advanceQueryString += " OR $@PODEName@ LIKE '%" + query + "%' ";
                    //Delivery
                    advanceQueryString += " OR $@DeliveryEName@ LIKE '%" + query + "%' ";
                }
            }
            advanceQueryString += ")";

            return advanceQueryString;
        }
        /// <summary>
        /// 文本框快速查询
        /// </summary>
        public void QuickQuery()
        {
            Stopwatch stopwatch = StopwatchHelper.StartStopwatch();
            if (txtquery.EditValue == null) return;
            var query = txtquery.EditValue.ToString().Trim();
            if (string.IsNullOrEmpty(query))
                return;
            string advanceQueryString = string.Empty;
            if (template.OperationType == OperationType.InquireRate) //询价
            {
                advanceQueryString = GetIRAdvanceQueryString(query);
            }
            else if (template.OperationType == OperationType.WorkFlow
              || template.Caption.Contains("流程工作")
              || template.Caption.Contains("WorkFlow"))
            {
                advanceQueryString = GetWFAdvanceQueryString(query);
            }
            else
            {
                advanceQueryString = AdvanceQueryString(query, false, true);
            }
            //ICP.Operation.Common.UI-ListBaseBusinessPart 面板
            ListBaseBusinessPart listBaseBusinessPart = RootWorkItem.State["CurrentBaseBusinessPart"] as ListBaseBusinessPart;
            if (template != null)
            {
                //存在拼装条件直接查询
                if (!string.IsNullOrEmpty(advanceQueryString)
                    && template.OperationType != OperationType.WorkFlow
                    && template.OperationType != OperationType.InquireRate)
                {
                    advanceQueryString += " and $@OPD@=0 ";
                    AddTreeListNode(advanceQueryString, 50);
                }
                else if (template.OperationType == OperationType.WorkFlow || template.OperationType == OperationType.InquireRate)
                {
                    //流程 & 询价
                    AddTreeListNode(advanceQueryString, 50);
                }
                //先屏蔽列表搜索，目前搜索会增加节点 查询结果
                //else if (listBaseBusinessPart != null)
                //{
                //    //判断当前的搜索条件在当前的节点中是否有满足的数据，
                //    //如无到数据进行查询操作，如有直接在数据源中进行查找以及选择
                //    if (listBaseBusinessPart.TaskCenterPositioning(advanceQueryString) == false)
                //    {
                //        advanceQueryString += " and $@OPD@=0 ";
                //        AddTreeListNode(advanceQueryString, 50);
                //    }
                //}
                else
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(),
                                               LocalData.IsEnglish
                                                   ? "The query result failure, please select nodes to query again."
                                                   : "查询结果失败，请重新选择节点进行查询.");
                }
            }
            MethodBase method = MethodBase.GetCurrentMethod();
            StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, AssamblyName, "SEARCH", "任务中心快速查找|OPD=0|搜索条件为" + query);
            //全局记录搜索条件文本框的值
            ClientWorkItem.State["Logsquery"] = query;
        }
        /// <summary>
        /// 通过单号获取业务ID集合
        /// </summary>
        /// <param name="strNo">单号</param>
        /// <param name="global">全局查找</param>
        /// <returns></returns>
        string GetOperationsIDByNo(string strNo, bool global)
        {
            if (global)
                return string.Empty;
            if (LocalData.OperationViewInfo == null || LocalData.OperationViewInfo.Rows.Count <= 0)
                return string.Empty;
            StringBuilder returnValue = new StringBuilder();
            returnValue.Append(" [oViewCache].[OperationID] ");
            IEnumerable<DataRow> CurrentTypeRows = LocalData.OperationViewInfo.AsEnumerable().Where(
                fItem => (OperationType) fItem.Field<byte>("OperationType") == template.OperationType
                );
            if (CurrentTypeRows.Any())
            {
                IEnumerable<DataRow> resultRows = CurrentTypeRows.Where(fItem =>
                    fItem.Field<string>("No").Contains(strNo)
                    || fItem.Field<string>("RefNo").Contains(strNo)
                    || fItem.Field<string>("BLNo").Contains(strNo)
                );
                var dataRows = resultRows as DataRow[];
                if (dataRows != null && dataRows.Any())
                {
                    if (dataRows.Count() == 1)
                    {
                        if (ArgumentHelper.GuidIsNullOrEmpty(dataRows.First().Field<Guid?>("OceanBookingID")))
                            returnValue = new StringBuilder();
                        else
                            returnValue.AppendFormat("='{0}'", dataRows.First().Field<Guid>("OceanBookingID"));
                    }
                    else
                    {
                        returnValue.Append(" IN (");
                        foreach (DataRow rowItem in dataRows.Where(rowItem => !ArgumentHelper.GuidIsNullOrEmpty(rowItem.Field<Guid?>("OceanBookingID"))))
                        {
                            returnValue.AppendFormat("'{0}'", rowItem.Field<Guid>("OceanBookingID"));
                        }
                        returnValue.Replace("''", "','");
                        returnValue.Append(" )");
                    }
                }
            }
            return !returnValue.ToString().Contains("'") ? string.Empty : returnValue.ToString();
        }
        #endregion

        /// <summary>
        /// 根据传递的业务参数，在根节点下添加业务节点
        /// </summary>
        /// <param name="operationContext">业务信息</param>
        public void AddBusinessNode(BusinessOperationContext operationContext)
        {

            if (operationContext == null || operationContext.OperationID == Guid.Empty)
            {
                return;
            }
            if (CurrentNodeList == null || CurrentNodeList.Count <= 0)
            {
                return;
            }
            treeListNodes.ForceInitialize();
            txtquery.Text = operationContext.OperationNO;
            txtquery.Enabled = true;
            btnAdvancedquery.Enabled = true;
            #region 根据当前用户的默认岗位查询节点加载到对应的父节点中
            UserDetailInfo userDetailInfo = UserService.GetUserDetailInfo(LocalData.UserInfo.LoginID);
            if (userDetailInfo == null) return;
            var nodeInfoList = bindingSourceNodes.DataSource as List<NodeInfo>;
            if (nodeInfoList != null)
            {
                var caption = nodeInfoList.FirstOrDefault(n => n.Caption.Contains(userDetailInfo.JobName));
                if (caption != null)
                {
                    //选中项
                    treeListNodes.FocusedNode = treeListNodes.FindNodeByKeyID(caption.Id);
                    //取到当前的新增节点信息
                    _node = treeListNodes.FindNodeByKeyID(caption.Id);
                    //根据节点的信息获取节点的坐标值
                    var cellInfo = treeListNodes.ViewInfo.RowsInfo[_node].Cells[0] as CellInfo;
                    if (cellInfo != null)
                    {
                        Rectangle r = cellInfo.Bounds;
                        //构造MouseEventArgs参数
                        MouseEventArgs mouseEvent = new MouseEventArgs(MouseButtons.Left, 1, r.X, r.Y, 0);
                        //调用方法
                        treeListNodes_MouseClick(null, mouseEvent);
                    }
                }
            }
            #endregion
            QuickQuery();
        }

        /// <summary>
        /// 复制一份当前用户的组织架构
        /// </summary>
        private void CopyLocalOrganizationInfo()
        {

            ListOranizationInfo = (from d in LocalData.UserInfo.UserOrganizationList
                                   select new LocalOrganizationInfo
                                   {
                                       ID = d.ID,
                                       Code = d.Code,
                                       CShortName = d.CShortName,
                                       EShortName = d.EShortName,
                                       FullName = d.FullName,
                                       ParentID = d.ParentID,
                                       Type = d.Type
                                   }).ToList();

        }
        /// <summary>
        /// 移除节点
        /// </summary>
        /// <param name="root">根节点</param>
        /// <param name="changeParentID">更改的上级节点</param>
        void removeSingleRoot(Guid root, Guid changeParentID)
        {
            if (ListOranizationInfo.Count < 1) return;
            List<LocalOrganizationInfo> result = ListOranizationInfo.FindAll((LocalOrganizationInfo tmp) =>
            {
                return

                    tmp.ParentID == root;
            });
            if (result != null)
            {
                ListOranizationInfo.RemoveAll((LocalOrganizationInfo tmp) => { return tmp.ID == root; });
                if (result.Count > 1)
                {
                    foreach (LocalOrganizationInfo tmp in result)
                    {
                        tmp.ParentID = changeParentID;
                    }
                }
                else if (result.Count == 1)
                {
                    removeSingleRoot(result[0].ID, changeParentID);
                }
            }

        }

        /// <summary>
        /// 构造节点
        /// </summary>
        /// <param name="tmp"></param>
        /// <returns></returns>
        NodeInfo LocalOrganizationInfoToNodeInfo(LocalOrganizationInfo tmp)
        {
            return new NodeInfo()
            {
                Id = tmp.ID,
                ParentId = tmp.ParentID,
                UserID = LocalData.UserInfo.LoginID,
                Caption = LocalData.IsEnglish ? tmp.EShortName : tmp.CShortName,
                NodeType = (NodeType)((tmp.Type) + 4),
                HasFetchChildrenData = ListOranizationInfo.Exists((LocalOrganizationInfo s) =>
                {
                    return s.ParentID

                        == tmp.ID;
                }),
                HasChildren = true,



            };

        }

        /// <summary>
        /// 点击节点执行查询
        /// </summary>
        /// <param name="nTemplate">节点的实体</param>
        /// <param name="node">节点集合</param>
        /// <param name="hitInfo">选择的节点</param>
        private void ProcessNode(NodeInfo nTemplate, TreeListNode node, TreeListHitInfo hitInfo)
        {
            SaveNodeInfo(nTemplate.ViewCode, nTemplate.UserID);
            if (node.Expanded && nTemplate.HasFetchChildrenData)
            {
                if (hitInfo.Column == null)
                {
                    node.Expanded = false;
                }
                else
                {
                    ClientWorkItem.Commands[TaskCenterCommandConstants.SetReadOnly].Execute();
                }
                return;

            }

            if (node.HasChildren == false && !nTemplate.HasFetchChildrenData && nTemplate.HasChildren)
            {
                lock (loadingNodes)
                {
                    if (loadingNodes.Contains(nTemplate))
                        return;
                    loadingNodes.Add(nTemplate);
                    nTemplate.Caption += tempNodeCaption;
                    InnerInit(nTemplate);
                }
            }
            else if (!nTemplate.HasChildren)
            {
                ClientWorkItem.Commands[TaskCenterCommandConstants.CancelReadOnly].Execute();
                return;
            }
            else
            {
                if (hitInfo.Column == null)
                {
                    node.Expanded = true;
                }
            }
        }

        /// <summary>
        /// 得到父节点的子节点数据
        /// </summary>
        /// <param name="ntemplate" type="NodeInfo">父节点</param>
        private void InnerInit(NodeInfo ntemplate)
        {
            //WaitCallback callback = (data) =>
            //{
            //ClientHelper.SetApplicationContext();
            lock (MainWorkWorkItem.synObj)
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    temp = ntemplate as NodeInfo;
                    Guid? parentId = null;
                    Guid? WorkSpaceId = null;

                    if (temp != null)
                    {
                        parentId = temp.SqlId;
                        WorkSpaceId = temp.SearchCode;
                        userId = temp.UserID;
                        nodeinfoid = temp.Id;
                    }
                    var nodeInfoList = bindingSourceNodes.DataSource as List<NodeInfo>;
                    List<NodeInfo> nodes = null;
                    switch (temp.NodeType)
                    {
                        case NodeType.Staff:
                            nodes = mainWorkController.OperationViewService.GetUserWorkSpaceList(parentId);
                            foreach (var node in nodes)
                            {
                                if (nodeInfoList.FirstOrDefault(n => n.Id == node.Id) != null)
                                {
                                    node.Id = Guid.NewGuid();
                                    node.ParentId = temp.Id;
                                }
                                else
                                {
                                    node.ParentId = temp.Id;
                                }
                            }
                            break;
                        case NodeType.OperateType:
                            nodes = mainWorkController.OperationViewService.GetWorkSpaceOperationViewList(parentId, WorkSpaceId, userId);
                            if (nodes != null)
                            {
                                var unodes = nodes.Where(n => n.ParentId != null).ToList();
                                unodes.ForEach(n =>
                                {
                                    n.ParentId = temp.Id;
                                });
                            }
                            break;
                        case NodeType.ParentStaff:
                            nodes = mainWorkController.OperationViewService.GetSubordinateUserList();
                            break;
                        case NodeType.Company:
                            nodes = mainWorkController.OperationViewService.GetDepartmentUserList(ntemplate.Id);
                            break;
                        case NodeType.Department:
                            nodes = mainWorkController.OperationViewService.GetDepartmentUserList(ntemplate.Id);
                            break;
                        case NodeType.Group:
                            nodes = mainWorkController.OperationViewService.GetDepartmentUserList(ntemplate.Id);
                            break;
                        case NodeType.AssistStaff:
                            nodes = mainWorkController.OperationViewService.GetSubordinateUserAssistsList();
                            break;
                        default:
                            break;
                    }
                    if (nodes == null || !nodes.Any())
                    {
                        ntemplate.HasChildren = false;
                        ntemplate.HasFetchChildrenData = true;
                    }
                    else
                    {
                        ntemplate.HasChildren = true;
                        ntemplate.HasFetchChildrenData = true;
                    }
                    EventHandler<CommonEventArgs<ChildrenNodeDataFetchParameter>> handler = Interlocked.CompareExchange(ref ChildrenNodeDataFetchFinishedHandler, null, null);

                    if (handler != null)
                    {
                        handler(null, new CommonEventArgs<ChildrenNodeDataFetchParameter>(new ChildrenNodeDataFetchParameter { Parent = temp, Children = nodes }));
                    }
                }
            }
        }

        /// <summary>
        /// 记录上次高级查询的条件
        /// </summary>
        /// <param name="stroriginal"></param>
        public void AddQueryConditions(string stroriginal)
        {
            var templateFilePath = Path.Combine(_fileRootDirectory, TempalteFileName);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(templateFilePath);
            XmlNode root = xmlDoc.SelectSingleNode("Template");//查找<Template>   
            XmlElement xe1 = xmlDoc.CreateElement("Item");//创建一个<Item>节点   
            xe1.SetAttribute("UserId", LocalData.UserInfo.LoginID.ToString());//设置该节点Usrid属性   
            xe1.SetAttribute("Stroriginal", stroriginal);//设置该节点stroriginal属性
            DirectoryInfo myDirectoryInfo = new DirectoryInfo(templateFilePath);
            myDirectoryInfo.Attributes &= ~FileAttributes.ReadOnly;
            if (root != null)
            {
                root.AppendChild(xe1);
                xmlDoc.Save(templateFilePath);
            }
        }

        /// <summary>
        /// 点击节点保存到配置文件中
        /// </summary>
        /// <param name="code">树的Code</param>
        /// <param name="userid">节点的UserID</param>
        public void SaveNodeInfo(string code, Guid userid)
        {
            var templateFilePath = Path.Combine(_fileRootDirectory, TempalteFileName);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(templateFilePath);
            XmlNode root = xmlDoc.SelectSingleNode("Template");//查找<Template>
            if (root == null) return;
            XmlNodeList nodeList = root.ChildNodes;
            //修改节点信息
            foreach (XmlElement xe in nodeList.Cast<XmlElement>())
            {
                xe.SetAttribute("ViewCode", code);
                xe.SetAttribute("UserID", userid.ToString());
            }
            //新建节点信息
            if (nodeList.Count == 0)
            {
                XmlElement xe1 = xmlDoc.CreateElement("Code");//创建一个<Item>节点  
                xe1.SetAttribute("ViewCode", code);//设置该节点Usrid属性 
                XmlElement xe2 = xmlDoc.CreateElement("UserID");//创建一个<Item>节点  
                xe1.SetAttribute("UserID", userid.ToString());//设置该节点Usrid属性 
                DirectoryInfo myDirectoryInfo = new DirectoryInfo(templateFilePath);
                myDirectoryInfo.Attributes &= ~FileAttributes.ReadOnly;
                root.AppendChild(xe1);
                root.AppendChild(xe2);
            }
            xmlDoc.Save(templateFilePath);
        }

        /// <summary>
        /// 修改用户已经存在的高级查询记录
        /// </summary>
        /// <param name="stroriginal">查询条件</param>
        public void UpdateQueryConditions(string stroriginal)
        {
            var templateFilePath = Path.Combine(_fileRootDirectory, TempalteFileName);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(templateFilePath);
            XmlNode rootNode = xmlDoc.SelectSingleNode("Template");
            if (rootNode == null) return;
            XmlNodeList nodeList = rootNode.ChildNodes;
            foreach (var xml in nodeList)
            {
                XmlElement xe = (XmlElement)xml;
                if (xe.GetAttribute("UserId") == LocalData.UserInfo.LoginID.ToString())
                {
                    xe.SetAttribute("Stroriginal", stroriginal);
                }
            }
            xmlDoc.Save(templateFilePath);
        }

        /// <summary>
        /// 查找当前用户是否存在高级查询的记录
        /// </summary>
        public bool GetQueryConditions()
        {
            bool flg = false;
            var templateFilePath = Path.Combine(_fileRootDirectory, TempalteFileName);
            var document = XDocument.Load(templateFilePath);
            //根据当前的代码读取对应的XML段落
            var xmldocment = from ent in document.Descendants("Item") select ent;
            if (xmldocment.Any())
            {
                foreach (var xElement in xmldocment)
                {
                    Guid Userid = new Guid(xElement.Attribute("UserId").Value);
                    if (Userid == LocalData.UserInfo.LoginID)
                    {
                        flg = true;
                    }
                }
            }
            return flg;
        }

        /// <summary>
        /// 增加节点信息
        /// </summary>
        public void AddTreeListNode(string advanceQueryString, int topCount)
        {
            var nodeInfoList = bindingSourceNodes.DataSource as List<NodeInfo>;
            if (nodeInfoList == null)
                return;
            string caption = LocalData.IsEnglish ? "Search Result" : "查询结果";
            ClientWorkItem.Commands[TaskCenterCommandConstants.CommandDisableTaskCenter].Execute();
            NodeInfo nodeAll = null;
            string Caption = LocalData.IsEnglish ? "All" : "所有";

            #region  节点添加
            //如果当前的业务类型为未知 ，知道当前的节点为父节点
            if (template.OperationType == OperationType.Unknown || template.ParentId == null)
            {
                nodeAll =
                    nodeInfoList.FirstOrDefault(
                        n => n.ParentId == template.Id && n.Caption.Contains(Caption));
            }
            else
            {
                if (template.Hierarchy == 3)
                {
                    //获取二级节点的ID
                    var firstOrDefault = nodeInfoList.FirstOrDefault(n => n.Id == template.ParentId);
                    if (firstOrDefault != null)
                    {
                        nodeAll =
                            nodeInfoList.FirstOrDefault(
                                n => n.ParentId == firstOrDefault.ParentId && n.Caption.Contains(Caption));
                    }

                }
                else
                {
                    nodeAll = nodeInfoList.FirstOrDefault(n => n.ParentId == template.ParentId && n.Caption.Contains(Caption));
                }
            }
            //如果当前的节点不属于当前用户的话，那么去获取当前用户的第一个根节点，然后进行节点的添加
            if (nodeAll == null)
            {
                Guid id = nodeInfoList.Where(n => n.NodeType == NodeType.OperateType).ToList()[0].Id;
                nodeAll =
                    nodeInfoList.FirstOrDefault(
                        n => n.ParentId == id && n.Caption.Contains(Caption));
            }
            #endregion

            if (nodeAll == null)
                return;
            //根据当前节点的ALL节点的信息生成查询结果节点
            NodeInfo searchnodeInfo = null;
            searchnodeInfo = new NodeInfo
            {
                Id = Guid.NewGuid(),
                UserID = nodeAll.UserID,
                ParentId = nodeAll.ParentId,
                SearchCode = nodeAll.SearchCode,
                ViewCode = nodeAll.ViewCode,
                NodeType = nodeAll.NodeType,
                HasChildren = false,
                HasFetchChildrenData = true,
                Caption = caption,
                AdvanceQueryString = advanceQueryString,
                OperationType = nodeAll.OperationType,
                TopCount = topCount
            };
            //更换查询条件时候先在结果中移除上次的结果
            //if (nodeInfoList != null)
            //{
            //节点不被保留时才会被替换
            var romovenodeinfo = nodeInfoList.FirstOrDefault(n => n.Caption == caption && n.Keep == false);
            if (romovenodeinfo != null)
            {
                romovenodeinfo.Id = searchnodeInfo.Id;
                romovenodeinfo.UserID = searchnodeInfo.UserID;
                romovenodeinfo.ParentId = searchnodeInfo.ParentId;
                romovenodeinfo.SearchCode = searchnodeInfo.SearchCode;
                romovenodeinfo.ViewCode = searchnodeInfo.ViewCode;
                romovenodeinfo.NodeType = searchnodeInfo.NodeType;
                romovenodeinfo.HasChildren = searchnodeInfo.HasChildren;
                romovenodeinfo.HasFetchChildrenData = searchnodeInfo.HasFetchChildrenData;
                romovenodeinfo.Caption = searchnodeInfo.Caption;
                romovenodeinfo.AdvanceQueryString = searchnodeInfo.AdvanceQueryString;
                romovenodeinfo.OperationType = searchnodeInfo.OperationType;
                romovenodeinfo.TopCount = topCount;
            }
            else
            {
                nodeInfoList.Add(searchnodeInfo);
            }
            //}

            //刷新树的数据源
            bindingSourceNodes.DataSource = nodeInfoList;
            //  bindingSourceNodes.ResetBindings(false);
            treeListNodes.RefreshDataSource();
            //选中项
            treeListNodes.FocusedNode = treeListNodes.FindNodeByKeyID(searchnodeInfo.Id);
            //取到当前的新增节点信息
            _node = treeListNodes.FindNodeByKeyID(searchnodeInfo.Id);
            //根据节点的信息获取节点的坐标值
            var cellInfo = treeListNodes.ViewInfo.RowsInfo[_node].Cells[0] as CellInfo;
            if (cellInfo != null)
            {
                Rectangle r = cellInfo.Bounds;
                //构造MouseEventArgs参数
                MouseEventArgs mouseEvent = new MouseEventArgs(MouseButtons.Left, 1, r.X, r.Y, 0);
                //调用方法
                treeListNodes_MouseClick(null, mouseEvent);
            }
        }


        /// <summary>
        /// 加载完成后，在正在加载LoadingNodes集合中清除该节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNodeDataFetchFinsihed(object sender, CommonEventArgs<ChildrenNodeDataFetchParameter> e)
        {
            NodeInfo parent = e.Data.Parent;
            if (parent != null)
            {
                lock (loadingNodes)
                {
                    parent.HasFetchChildrenData = true;
                    loadingNodes.Remove(parent);

                }
                parent.Caption = parent.Caption.Replace(tempNodeCaption, "");
            }

            RefreshDelegate refreshDelegate = InnerRefresh;
            Invoke(refreshDelegate, e.Data);
        }

        /// <summary>
        /// 刷新数据源
        /// </summary>
        /// <param name="parameter"></param>
        private void InnerRefresh(ChildrenNodeDataFetchParameter parameter)
        {
            (bindingSourceNodes.DataSource as List<NodeInfo>).AddRange(parameter.Children);
            bindingSourceNodes.ResetBindings(false);
            treeListNodes.RefreshDataSource();
            if (nodeinfoid != Guid.Empty)
            {
                var nodeInfos = bindingSourceNodes.DataSource as List<NodeInfo>;
                if (nodeInfos != null && nodeInfos.Any())
                {
                    if (nodeInfos.FirstOrDefault(n => n.Id == nodeinfoid) != null)
                    {
                        treeListNodes.FindNodeByKeyID(nodeinfoid).ExpandAll();
                    }
                }
            }

        }

        #region 协助同事
        /// <summary>
        /// 移除树的结构集合
        /// </summary>
        public void RefreshNodeInfo()
        {
            bindingSourceNodes.Clear();
            bindingSourceNodes.ResetBindings(false);
            InitData();
            List<NodeInfo> newNodeInfos = bindingSourceNodes.DataSource as List<NodeInfo>;
            string viewCode = SetViewCode();
            string viewcod = viewCode.Split('|')[0];
            Guid userid = new Guid(viewCode.Split('|')[1]);
            if (newNodeInfos != null && newNodeInfos.Count > 0)
            {
                #region  协助同事面板回调处理
                //如果在协助同事窗口前，点击的是属于自己的节点信息，
                //那么在删除或者新增协助同事以后，系统会自动回到之前点击的属于自己的节点信息

                //如果在协助同事窗口前，点击的是属于同事的节点信息，
                //那么在删除或者新增协助同事以后，系统默认将节点锁定在第一个根节点上

                NodeInfo node = newNodeInfos.FirstOrDefault(n => n.ViewCode == viewcod);
                if (node == null)
                {
                    treeListNodes.FindNodeByKeyID(newNodeInfos[0].Id).ExpandAll();
                    ClientWorkItem.Commands[TaskCenterCommandConstants.SetReadOnly].Execute();
                    SaveNodeInfo(newNodeInfos[0].ViewCode, LocalData.UserInfo.LoginID);
                    return;
                }
                else if (string.IsNullOrEmpty(viewcod))
                {
                    treeListNodes.FindNodeByKeyID(newNodeInfos[0].Id).ExpandAll();
                    ClientWorkItem.Commands[TaskCenterCommandConstants.SetReadOnly].Execute();
                    SaveNodeInfo(newNodeInfos[0].ViewCode, LocalData.UserInfo.LoginID);
                    return;
                }
                else if (userid.Equals(LocalData.UserInfo.LoginID) == false)
                {
                    treeListNodes.FindNodeByKeyID(newNodeInfos[0].Id).ExpandAll();
                    ClientWorkItem.Commands[TaskCenterCommandConstants.SetReadOnly].Execute();
                    SaveNodeInfo(newNodeInfos[0].ViewCode, LocalData.UserInfo.LoginID);
                    return;
                }
                #endregion

                treeListNodes.FocusedNode = treeListNodes.FindNodeByKeyID(node.Id);
                //取到节点信息
                _node = treeListNodes.FindNodeByKeyID(node.Id);
                //根据节点的信息获取节点的坐标值
                var cellInfo = treeListNodes.ViewInfo.RowsInfo[_node].Cells[0] as CellInfo;
                if (cellInfo != null)
                {
                    Rectangle r = cellInfo.Bounds;
                    //构造MouseEventArgs参数
                    MouseEventArgs mouseEvent = new MouseEventArgs(MouseButtons.Left, 1, r.X, r.Y, 0);
                    //调用方法
                    treeListNodes_MouseClick(null, mouseEvent);
                }
            }
        }
        /// <summary>
        /// 获取当前用户的选择节点Code
        /// </summary>
        /// <returns></returns>
        public string SetViewCode()
        {
            string returnstr = string.Empty;
            var templateFilePath = Path.Combine(_fileRootDirectory, TempalteFileName);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(templateFilePath);
            var selectSingleNode = xmlDoc.SelectSingleNode("Template");
            if (selectSingleNode != null)
            {
                XmlNodeList nodes = selectSingleNode.ChildNodes;
                XmlElement xe = (XmlElement)nodes[0];
                if (xe != null)
                {
                    returnstr = xe.Attributes["ViewCode"].Value + "|" + xe.Attributes["UserID"].Value;
                }
            }
            return returnstr;
        }
        #endregion
        #endregion

        #region Comment Code
        /// <summary>
        /// 构建搜索节点
        /// </summary>
        /// <param name="operationType"></param>
        /// <returns></returns>
        private NodeInfo BuildSearchNode(OperationType operationType)
        {

            NodeInfo nodeAll = CurrentNodeList.Find((NodeInfo tmp) =>
            {
                return tmp.ViewCode.ToUpper().Contains("ALL") && tmp.OperationType == operationType;
            });

            //根据当前节点的ALL节点的信息生成查询结果节点

            NodeInfo searchNodeInfo = new NodeInfo
            {
                Id = searchNodeID,
                UserID = nodeAll.UserID,
                ParentId = nodeAll.ParentId,
                SearchCode = nodeAll.SearchCode,
                ViewCode = nodeAll.ViewCode,//"TaskCenter_OceanExport_NewBooking",
                NodeType = nodeAll.NodeType,
                HasChildren = false,
                HasFetchChildrenData = true,
                Caption = LocalData.IsEnglish ? "Search Result" : "查询结果",
                AdvanceQueryString = null,
                OperationType = nodeAll.OperationType
            };
            return searchNodeInfo;

        }
        #endregion
    }

}
/// <summary>
/// 加载节点EventArgs
/// </summary>
public class ChildrenNodeDataFetchParameter
{
    /// <summary>
    /// 父节点
    /// </summary>
    public NodeInfo Parent { get; set; }
    /// <summary>
    /// 子节点集合
    /// </summary>
    public List<NodeInfo> Children { get; set; }

}

