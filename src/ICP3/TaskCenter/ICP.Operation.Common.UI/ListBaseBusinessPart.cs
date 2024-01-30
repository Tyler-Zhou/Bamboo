using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ICP.Business.Common.ServiceInterface;
using ICP.Business.Common.UI;
using ICP.Business.Common.UI.Contact;
using ICP.Common.CommandHandler.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using ICP.MailCenter.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CommonConstants = ICP.Operation.Common.ServiceInterface.CommonConstants;
using Constants = ICP.Operation.Common.ServiceInterface.Constants;
using EnumDocumentType = ICP.FileSystem.ServiceInterface.DocumentType;
using SortOrder = ICP.DataCache.ServiceInterface.SortOrder;

namespace ICP.Operation.Common.UI
{
    /// <summary>
    /// 带数据列表业务面板基类
    /// </summary>
    public partial class ListBaseBusinessPart : BaseBusinessPart, IListBaseBusinessPart
    {
        #region 注册快捷方式
        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="id"></param>
        /// <param name="fsModifiers"></param>
        /// <param name="vk"></param>
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
        /// <param name="hWnd"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
         IntPtr hWnd, // handle to window 
         int id // hot key identifier 
        );
        /// <summary>
        /// 热键
        /// </summary>
        [Flags()]
        public enum KeyModifiers
        {
            /// <summary>
            /// 无
            /// </summary>
            None = 0,
            /// <summary>
            /// Alt
            /// </summary>
            Alt = 1,
            /// <summary>
            /// Ctrl
            /// </summary>
            Control = 2,
            /// <summary>
            /// Shift
            /// </summary>
            Shift = 4,
            /// <summary>
            /// Windows
            /// </summary>
            Windows = 8
        }
        #endregion

        #region  Fields & Property & Services

        #region Fields
        /// <summary>
        /// 
        /// </summary>
        private object syncObj = new object();
        /// <summary>
        /// 记录当前面板数据列表中上次选择的行号
        /// </summary>
        public int previousFocusedRowHandle = -1;
        /// <summary>
        /// 排序的字段
        /// </summary>
        public string Order = string.Empty;
        /// <summary>
        /// 分组的条件
        /// </summary>
        public string Group = string.Empty;
        /// <summary>
        /// 上下文菜单拓展点名称后缀
        /// </summary>
        private string contextMenuStripUISiteName = "_MenuUISite";
        /// <summary>
        /// 当前列表的用户自定义信息
        /// </summary>
        private UserCustomGridInfo currentUserCustomGridInfo;
        /// <summary>
        /// 用户是否改变列设置，包含列宽度，列位置
        /// </summary>
        private bool isColumnSettingChanged = false;
        private const string associatedColumn = "IsAssociated";
        /// <summary>
        /// 是否有效
        /// </summary>
        private string isValid = "IsValid";
        /// <summary>
        /// 标识是否清空Grid的列和数据源
        /// </summary>
        private bool IsClearColumnsAndDataSource = false;
        /// <summary>
        /// 业务面板中，只有SONO更改了才需要去保存业务数据。
        /// </summary>
        private const string SaveEditDataFieldName = "SONO";
        /// <summary>
        /// 当前是否显示了正在提交的动画
        /// </summary>
        public bool Submiting = false;
        /// <summary>
        /// GridView是否已经存在列
        /// </summary>
        private bool HasGridViewColumns = false;
        /// <summary>
        /// 当前绑定的消息实体类
        /// </summary>
        //private ICP.Message.ServiceInterface.Message currentMessage = null;
        /// <summary>
        /// 勾选列名
        /// </summary>
        private const string selectedFieldName = "Selected";
        /// <summary>
        /// 是否为多选择
        /// </summary>
        public bool CheckSelect = false;
        /// <summary>
        /// OceanBookingID
        /// </summary>
        protected string OperationIDFieldName = Constants.OperationIDFieldName;

        /// <summary>
        /// CompanyID
        /// </summary>
        protected string CompanyIDFieldName = Constants.CompanyIDFieldName;

        #endregion Fields

        #region Property
        /// <summary>
        /// WorkItem
        /// </summary>
        public WorkItem WorkItem
        {
            get;
            set;
        }

        /// <summary>
        /// 是否显示参与者面板
        /// </summary>
        public bool? IsShowAssistantPart
        { get; set; }

        /// <summary>
        /// 当前点击上下文菜单项所包含的自定义信息
        /// </summary>
        public object CurrentContextMenuItemTag
        {
            get;
            set;
        }

        /// <summary>
        /// 操作上下文
        /// </summary>
        public BusinessOperationContext BusinessOperationContext
        {
            get;
            set;
        }


        /// <summary>
        /// 事件类表操作类
        /// </summary>
        public EventObjects EventObjects
        {
            get;
            set;
        }

        /// <summary>
        /// 邮件中心搜索数据的类型，默认是系统自动去搜索
        /// </summary>
        public SearchActionType SearchType
        {
            get { return base.SearchType; }
            set { base.SearchType = value; }
        }

        /// <summary>
        /// 是否从数据库查询
        /// </summary>
        public new bool NeedSearchInSQLServer
        {
            get
            {
                return base.NeedSearchInSQLServer;
            }
            set
            {
                base.NeedSearchInSQLServer = value;
            }
        }
        /// <summary>
        /// 查询字符串
        /// </summary>
        public new string ServerQueryString
        {
            get { return base.ServerQueryString; }
            set { base.ServerQueryString = value; }
        }

        /// <summary>
        /// 当前选择列是否有效
        /// </summary>
        public bool? IsValid
        {
            get
            {
                if (FocusedDataRow == null)
                {
                    return false;
                }
                return !FocusedDataRow.Table.Columns.Contains(isValid) ?
                    null : (bool?)FocusedDataRow["IsValid"];
            }
            set { FocusedDataRow["IsValid"] = value; }
        }

        /// <summary>
        /// 订舱号
        /// </summary>
        public string SONO
        {
            get
            {
                if (FocusedDataRow == null)
                {
                    return string.Empty;
                }
                return !FocusedDataRow.Table.Columns.Contains("SONO") ?
                    null : FocusedDataRow["SONO"].ToString();
            }
        }


        /// <summary>
        /// 选中的数据行
        /// </summary>
        public DataRow FocusedDataRow
        {
            get
            {
                return gridViewList.GetFocusedDataRow();
            }
        }

        #region 勾选的行
        /// <summary>
        /// 勾选的行
        /// </summary>
        public DataRow[] SelectedDataRows
        {
            get
            {
                DataRow[] dataRows = null;
                if (CurrentDataSource == null)
                {
                    dataRows = null;
                }
                else if (FocusedDataRow.Table.Columns.Contains(selectedFieldName))
                {
                    dataRows = CurrentDataSource.AsEnumerable().Where(o => o.Field<bool>(selectedFieldName)).ToArray();
                }
                return dataRows;
            }
        }

        /// <summary>
        /// 勾选的业务数量
        /// </summary>
        public int SelectedDataRowCount
        {
            get
            {
                if (SelectedDataRows == null)
                    return 0;
                else
                    return SelectedDataRows.Length;
            }
        }

        public new string[] SelectedOperationNos
        {
            get
            {
                string[] operationNos = null;
                if (SelectedDataRowCount > 0)
                    operationNos = SelectedDataRows.Select(item => item.Field<string>(OperationNoFieldName))
                                        .ToArray();
                base.SelectedOperationNos = operationNos;
                return operationNos;
            }
        }

        /// <summary>
        /// 勾选的业务更新时间
        /// </summary>
        public new DateTime?[] SelectedUpdateDates
        {
            get
            {
                DateTime?[] updateDates = null;
                if (SelectedDataRowCount > 0)
                    updateDates = SelectedDataRows.Select(item => item.Field<DateTime?>("UpdateDate"))
                                        .ToArray();
                base.SelectedUpdateDates = updateDates;
                return updateDates;
            }
        }

        /// <summary>
        /// 勾选的业务类型
        /// </summary>
        public new OperationType[] SelectedOperationTypes
        {
            get
            {
                OperationType[] operationTypes = null;
                if (SelectedDataRowCount > 0)
                    operationTypes =
                        SelectedDataRows.Select(item => (OperationType)item.Field<byte>(OperationTypeFieldName))
                                        .ToArray();
                base.SelectedOperationTypes = operationTypes;
                return operationTypes;
            }
        }

        /// <summary>
        /// 勾选的业务号
        /// </summary>
        public Guid[] SelectedOperationIDs
        {
            get
            {
                Guid[] operatonIDs = null;
                if (SelectedDataRowCount > 0)
                    operatonIDs = SelectedDataRows.Select(item => item.Field<Guid>(OperationIDFieldName)).ToArray();
                base.SelctedOperationIDs = operatonIDs;
                return operatonIDs;
            }
        }

        /// <summary>
        /// 勾选的口岸ID
        /// </summary>
        public Guid[] SelectedCompanyIDs
        {
            get
            {
                Guid[] companyIDs = null;
                if (SelectedDataRowCount > 0)
                    companyIDs = SelectedDataRows.Select(item => item.Field<Guid>(CompanyIDFieldName)).ToArray();
                base.SelctedCompanyIDs = companyIDs;
                return companyIDs;
            }
        }

        /// <summary>
        /// 任务中心勾选的操作
        /// </summary>
        public List<Guid> TaskCenterSelectedIDs
        {
            get
            {
                List<Guid> guids = new List<Guid>();
                if (SelectedDataRowCount > 0 && CheckSelect)
                {
                    guids = SelectedDataRows.Select(item => item.Field<Guid>(OperationIDFieldName)).ToList();
                }
                else
                {
                    Guid guid = OperationID;
                    guids.Add(guid);
                    if (SelectedDataRows != null)
                    {
                        if (SelectedDataRows.Select(item => item.Field<Guid>(OperationIDFieldName)).ToList().Any())
                        {
                            CheckSelect = true;
                        }
                    }

                }
                return guids;
            }
        }

        /// <summary>
        /// 任务中心勾选且与焦点行可用状态一致的的操作业务ID集合
        /// </summary>
        public List<Guid> TaskCenterSelectedIsValidIDs
        {
            get
            {
                List<Guid> guids = new List<Guid>();
                if (SelectedDataRowCount > 0 && CheckSelect)
                {
                    guids.AddRange(from item in SelectedDataRows where IsValid.Equals(item["IsValid"]) select item.Field<Guid>(OperationIDFieldName));
                    //guids = SelectedDataRows.Select(item => item.Field<Guid>(OperationIDFieldName)).ToList();
                }
                else
                {
                    Guid guid = OperationID;
                    guids.Add(guid);
                    if (SelectedDataRows != null)
                    {
                        if (SelectedDataRows.Select(item => item.Field<Guid>(OperationIDFieldName)).ToList().Any())
                        {
                            CheckSelect = true;
                        }
                    }

                }
                return guids;
            }
        }

        /// <summary>
        /// 业务联系信息服务客户端
        /// </summary>
        public IClientBusinessContactService ClientBusinessContactService
        {
            get { return ServiceClient.GetClientService<IClientBusinessContactService>(); }
        }

        /// <summary>
        /// 业务上下文
        /// </summary>
        public BusinessContextMenuItemGeneratorFactory BusinessContextMenuItemGeneratorFactory
        {
            get
            {
                return ClientHelper.Get<BusinessContextMenuItemGeneratorFactory, BusinessContextMenuItemGeneratorFactory>();
            }
        }
        #endregion

        #region  变量



        #endregion

        private int visibleIndex = 0;

        /// <summary>
        /// 支持拖入操作的列列表
        /// </summary>
        private List<BusinessColumnInfo> _listDropableColumns;
        private List<BusinessColumnInfo> listDropableColumns
        {
            get { return _listDropableColumns ?? (_listDropableColumns = new List<BusinessColumnInfo>()); }
            set { _listDropableColumns = value; }
        }

        /// <summary>
        /// 可编辑列列表
        /// </summary>
        private List<BusinessColumnInfo> _listEditableColumns;
        private List<BusinessColumnInfo> listEditableColumns
        {
            get { return _listEditableColumns ?? (_listEditableColumns = new List<BusinessColumnInfo>()); }
            set { _listEditableColumns = value; }
        }

        /// <summary>
        /// 维护业务和上传文档的列表字典
        /// </summary>
        private ListDictionary<Guid, string> _listDicAddedDocuments;
        private ListDictionary<Guid, string> listDicAddedDocuments
        {
            get { return _listDicAddedDocuments ?? (_listDicAddedDocuments = new ListDictionary<Guid, string>()); }
            set { _listDicAddedDocuments = value; }
        }

        /// <summary>
        /// 新上传的文档实体列表
        /// </summary>
        private List<DocumentInfo> listAddedDocuments = new List<DocumentInfo>();
        /// <summary>
        /// 新上传的文档实体列表
        /// </summary>
        public List<DocumentInfo> ListAddedDocuments
        {
            get
            {
                return listAddedDocuments;
            }
            set
            {
                listAddedDocuments = value;
            }
        }

        /// <summary>
        /// 维护业务和删除文档的列表字典
        /// </summary>
        private ListDictionary<Guid, string> _listDicDeletedDocuments;
        private ListDictionary<Guid, string> listDicDeletedDocuments
        {
            get { return _listDicDeletedDocuments ?? (_listDicDeletedDocuments = new ListDictionary<Guid, string>()); }
            set { _listDicDeletedDocuments = value; }
        }

        /// <summary>
        /// 维护行号和值发生更改列的列表字典
        /// </summary>
        private ListDictionary<int, string> _listDicEditedCells;
        private ListDictionary<int, string> listDicEditedCells
        {
            get { return _listDicEditedCells ?? (_listDicEditedCells = new ListDictionary<int, string>()); }
            set { _listDicEditedCells = value; }
        }

        /// <summary>
        /// 根据更新业务Id列表，去更新本地缓存
        /// </summary>
        private List<Guid> _operationIds;
        /// <summary>
        /// 
        /// </summary>
        public List<Guid> operationIds
        {
            get { return _operationIds ?? (_operationIds = new List<Guid>()); }
            set { _operationIds = value; }
        }

        /// <summary>
        /// 当前列表控件中的列信息
        /// </summary>
        private List<BusinessColumnInfo> listColumnInfos;
        /// <summary>
        /// 功能自定义添加列集合信息
        /// </summary>
        private List<BusinessColumnInfo> listCustomColumnInfos;

        private EnumDocumentType DocumentType
        {
            get
            {
                if (TemplateCode.Equals(ListFormType.MailLink4CarrierSO.ToString()))
                    return EnumDocumentType.OSO;
                if (TemplateCode.Equals(ListFormType.MailLink4CarrierMBL.ToString()))
                    return EnumDocumentType.MBL;
                if (TemplateCode.Equals(ListFormType.MailLink4CarrierAP.ToString()))
                    return EnumDocumentType.AP;
                if (TemplateCode.Equals(ListFormType.MailLink4CarrierAN.ToString()))
                    return EnumDocumentType.AN;
                return EnumDocumentType.Other;
            }
        }

        /// <summary>
        /// 默认是邮件中心加载数据时不显示进度窗体，任务中心则需要显示。
        /// </summary>
        public bool IsShowLoadingForm
        {
            get;
            set;
        }

        /// <summary>
        /// 邮件中心关联后，排序的列
        /// </summary>
        public GridColumnSortInfo _AssociatedSortInfo;
        /// <summary>
        /// 
        /// </summary>
        public GridColumnSortInfo AssociatedSortInfo
        {
            get
            {
                if (_AssociatedSortInfo == null)
                {
                    _AssociatedSortInfo = AddGridColumnSortInfoByFieldName(associatedColumn);
                }
                return _AssociatedSortInfo;
            }
        }

        /// <summary>
        /// 返回当前的业务状态
        /// </summary>
        public OEOrderState OeOrder;
        /// <summary>
        /// 
        /// </summary>
        public OEOrderState OeOrderState
        {
            get
            {
                if (CurrentRow.Table.Columns.Contains("State"))
                {
                    OeOrder = (OEOrderState)Enum.Parse(typeof(OEOrderState), CurrentRow["State"].ToString());
                }
                return OeOrder;
            }
        }

        /// <summary>
        /// 当前行是否包含IsTruck
        /// </summary>
        public bool RowIsTruck
        {
            get { return CurrentRow.Table.Columns.Contains("IsTruck"); }
        }
        /// <summary>
        /// 当前行是否包含IsCustoms
        /// </summary>
        public bool RowIsCustoms
        {
            get { return CurrentRow.Table.Columns.Contains("IsCustoms"); }
        }

        /// <summary>
        /// 读取当前的Tab页面
        /// </summary>
        public TabItemConfigInfo CurrentTabItem { get; set; }
        /// <summary>
        /// 创建一个新的Dev项目
        /// </summary>
        public RepositoryItem emptyEditor;


        /// <summary>
        /// 本地命令处理
        /// </summary>
        private List<string> localCommands;
        /// <summary>
        /// 本地命令处理
        /// </summary>
        public List<string> LocalCommands
        {
            get
            {
                return localCommands;
            }
            set
            {
                localCommands = value;
            }
        }

        /// <summary>
        /// 当前选中的列表数据行索引
        /// </summary>
        public int CurrentRowHandle
        {
            get
            {
                return gridViewList.FocusedRowHandle;
            }
        }
        /// <summary>
        /// 列表当前数据行
        /// </summary>
        public DataRow CurrentRow
        {
            get
            {
                RootWorkItem.State["BaseCurrentRow"] = FocusedDataRow;
                return FocusedDataRow;
            }
        }
        /// <summary>
        /// 列表当前绑定的数据源
        /// </summary>
        public DataTable CurrentDataSource
        {
            get
            {
                gridViewList.CloseEditor();
                return bindingSource.DataSource as DataTable;
            }
            set
            {
                bindingSource.DataSource = value;
                ResetBinding();
                RootWorkItem.State["BaseDataSource"] = value;
            }
        }

        /// <summary>
        /// 列表行字体
        /// </summary>
        public Font RowFont
        {
            get
            {
                return gridViewList.Appearance.Row.Font;
            }
        }
        /// <summary>
        /// BarManager 控件
        /// </summary>
        public new BarManager barManager
        {
            get { return base.barManager; }
            set { base.barManager = value; }
        }

        /// <summary>
        /// 业务类型
        /// </summary>
        public BusinessType BusinessType
        {
            get { return (BusinessType)Enum.Parse(typeof(OperationType), OperationType.ToString()); }
        }

        #endregion Property

        #region Services
        /// <summary>
        /// 读取事件列表
        /// </summary>
        public IFCMCommonService fcmCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// 消息发送成功回调接口
        /// </summary>
        public IMessageSentCallbackService MessageSentCallbackService
        {
            get
            {
                return ServiceClient.GetClientService<IMessageSentCallbackService>();
            }
        }

        /// <summary>
        /// 业务查询接口
        /// </summary>
        public IBusinessQueryService BusinessQueryService
        {
            get
            {
                return ServiceClient.GetService<IBusinessQueryService>();
            }
        }
        /// <summary>
        /// 文档使用的接口
        /// </summary>
        public IClientFileService ClientFileService
        {
            get
            {
                return ServiceClient.GetService<IClientFileService>();
            }
        }
        /// <summary>
        /// 公共服务接口
        /// </summary>
        public IICPCommonOperationService CommonOperationService
        {
            get { return ServiceClient.GetService<IICPCommonOperationService>(); }
        }
        /// <summary>
        /// 文档通知对象
        /// </summary>
        public DocumentNotifyClientService DocumentNotifyClientService
        {
            get
            {
                return ClientHelper.Get<DocumentNotifyClientService, DocumentNotifyClientService>();
            }
        }
        #endregion
        #endregion

        #region  构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ListBaseBusinessPart()
        {
            InitializeComponent();

            #region 改变控件实例化对象
            if (LocalData.ApplicationType == ApplicationType.ICP)
            {
                splitBusinessPart = new SplitContainerControl();
            }
            else
            {
                splitBusinessPart = new CustomSplitterControl();
            }
            #endregion
            Load += (sender, e) =>
            {
                if (!LocalData.IsDesignMode)
                {
                    toolTipController1.CreateShowArgs();
                    //设置toolTip显示时间
                    toolTipController1.AutoPopDelay = 2000;
                    localCommands = new List<string> { CommonConstants.Command_Add_Attachment_Name, CommonConstants.Command_Delete_Attachment_Name, "Command_BusinessPart_DataSubmit" };
                    //ServiceClient.GetClientService<IMainForm>().ApplicationExit += OnApplicationExit;
                    DocumentNotifyClientService.DocumentUploadFailed += OnDocumentUploadFailed;
                    MessageSentCallbackService.MessageSent += new EventHandler<CommonEventArgs<MessageSentParameter>>(OnMessageSent);

                    linkLabelMore.Text = LocalData.IsEnglish ? "Did you find what you were searching for?Try searching again  All  business." : "是否找到了要搜索的内容?尝试在所有业务里面搜索.";
                }
                emptyEditor = new RepositoryItem();
                gridControlList.RepositoryItems.Add(emptyEditor);
                //当前如果是在邮件中心处理的话  就将列表的表头给隐藏
                if (LocalData.ApplicationType == ApplicationType.EmailCenter)
                {
                    gridViewList.OptionsView.ShowColumnHeaders = false;
                }
            };
            Disposed += delegate
            {
                if (DocumentNotifyClientService != null)
                {
                    DocumentNotifyClientService.DocumentUploadFailed -= OnDocumentUploadFailed;
                    DocumentNotifyClientService.DocumentUploadSucessed -= OnDocumentUploadSucessed;
                }
                if (MessageSentCallbackService != null)
                {
                    MessageSentCallbackService.MessageSent -= OnMessageSent;
                }
                EndShowAnimation();
                //ServiceClient.GetClientService<IMainForm>().ApplicationExit -= OnApplicationExit;
                FocusedRowChangedHandler = null;
                gridControlList.DataSource = null;
                if (bindingSource != null)
                {
                    bindingSource.DataSource = null;
                    bindingSource.Dispose();
                    bindingSource = null;
                }

                localCommands = null;
                if (RootWorkItem != null)
                {
                    RootWorkItem.Items.Remove(this);

                }
            };
            RootWorkItem.State["CurrentBaseBusinessPart"] = this;
        }
        #endregion

        #region 公共属性及服务

        /// <summary>
        /// 列表当前选择行改变事件
        /// </summary>
        public event FocusedRowChangedEventHandler FocusedRowChangedHandler;

        /// <summary>
        /// 通过模板代码更改业务面板显示
        /// </summary>
        /// <param name="templateCode"></param>
        public void Switch(string templateCode)
        {
            UnRegisterExtensionSite();
            UpdateSubGridToolBarItem(templateCode);


            TemplateCode = templateCode;
            CreateColumns(TemplateCode);
            BindData(Parameter);
        }
        private void UpdateSubGridToolBarItem(string templateCode)
        {
            RemoveBusinessToobarItems(TemplateCode);
            RegisterExtensionSite();
            BuildToobar(templateCode);
        }
        /// <summary>
        /// 移除barManager 集合信息
        /// </summary>
        /// <param name="templateCode"></param>
        public void RemoveBusinessToobarItems(string templateCode)
        {

            List<BarItem> items = new List<BarItem>();
            BarItems barItems = barManager.Items;
            foreach (BarItem item in barItems)
            {
                ProcessBarItem(items, item, templateCode);
            }
            if (items.Count <= 0)
                return;
            foreach (BarItem item in items)
            {

                barManager.Items.Remove(item);
            }


        }
        /// <summary>
        /// 根据名称清除样式
        /// </summary>
        /// <param name="styleName"></param>
        public void ClearStyle(string styleName)
        {
            if (gridViewList.FormatConditions[styleName] == null)
            {
                return;
            }
            else
            {
                StyleFormatCondition rowCondition = gridViewList.FormatConditions[styleName] as StyleFormatCondition;
                rowCondition.Expression = "1 !=1 ";
            }
        }

        /// <summary>
        /// 添加业务样式 如果表达式和先前添加的样式相同，则直接返回
        /// </summary>
        /// <param name="styleCondition"></param>
        public void AddBusinessStyleFormationCondition(StyleFormatCondition styleCondition)
        {

            if (gridViewList.FormatConditions[styleCondition.Tag] == null)
            {
                gridViewList.FormatConditions.Add(styleCondition);
            }
            else
            {
                StyleFormatCondition rowCondition = gridViewList.FormatConditions[styleCondition.Tag] as StyleFormatCondition;
                if (rowCondition == null) return;
                if (rowCondition.Expression == styleCondition.Expression)
                    return;
                rowCondition.Expression = styleCondition.Expression;
            }
        }

        /// <summary>
        /// 设置行的样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewList_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            DataRow row = gridViewList.GetDataRow(e.RowHandle) as DataRow;
            if (row == null) return;
            if (row.Table.Columns.Contains(isValid) && !string.IsNullOrEmpty(row[isValid].ToString()))
            {
                SpecialConditioner.SetGridRowStyle(e.Appearance, row.Field<Guid>(OperationIDFieldName), row.Field<bool>(isValid));
            }
            #region  得到配置文件中需要改变背景色
            List<RowStyleConfigInfo> rowStyleConfig = RowStyleConfigController.GetRowStyleName(TemplateCode);
            if (rowStyleConfig.Any())
            {
                foreach (var rowStyle in rowStyleConfig)
                {
                    //判断需要改变的字段是否存在于表格中
                    if (row.Table.Columns.Contains(rowStyle.JudgeConditions) &&
                        !string.IsNullOrEmpty(row[rowStyle.JudgeConditions].ToString()))
                    {
                        if (bool.Parse(row[rowStyle.JudgeConditions].ToString()))
                        {
                            e.Appearance.BackColor = rowStyle.TrueColor;
                        }
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// 单元格改变颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewList_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            DataRow row = gridViewList.GetDataRow(e.RowHandle) as DataRow;
            if (row == null) return;

            #region 得到单元格的配置文件信息
            List<RowStyleConfigInfo> rowStyleConfig = RowStyleConfigController.GetRowCellStyleName(TemplateCode).Where(n => n.Font == false).ToList();
            if (rowStyleConfig.Any() == false) return;
            foreach (var rowStyle in rowStyleConfig)
            {
                if (row.Table.Columns.Contains(rowStyle.JudgeConditions))
                {
                    if (string.IsNullOrEmpty(rowStyle.Field)) return;
                    if (bool.Parse(row[rowStyle.JudgeConditions].ToString()))
                    {
                        gridViewList.Columns[rowStyle.Field].AppearanceCell.BackColor = rowStyle.TrueColor;

                    }
                    else
                    {
                        gridViewList.Columns[rowStyle.Field].AppearanceCell.BackColor = rowStyle.FalseColor;

                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// 设置列表获得焦点的行
        /// </summary>
        /// <param name="rowHandle"></param>
        public void SetFocusedRowHandle(int rowHandle)
        {
            gridViewList.FocusedRowHandle = rowHandle;
        }
        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <param name="data"></param>
        public void SetDataSource(object data)
        {
            if (bindingSource.DataSource != null && bindingSource.DataSource is IDisposable)
            {
                (bindingSource.DataSource as IDisposable).Dispose();
            }

            gridViewList.FocusedRowChanged -= OnGridViewFocusedRowChanged;
            //处理BindingSource 不能是自己的数据源。 错误 将bindingSource 为NULL 清除删除绑定的数据集合
            bindingSource.DataSource = null;
            bindingSource.DataSource = data;
            gridViewList.FocusedRowChanged += OnGridViewFocusedRowChanged;


            ResetBinding();
            RootWorkItem.State["BaseDataSource"] = data;
            if (LocalData.ApplicationType == ApplicationType.ICP)
            {
                LocatePreviousFocusedRow();
            }
            EmailButtonDisplayed();
        }

        /// <summary>
        /// 数据加载绑定
        /// </summary>
        /// <param name="parameter"></param>
        public override void BindData(object parameter)
        {
            BeforeBindData();
            base.BindData(parameter);
            //标识内部沟通面板是否要限制最小Size
            LimitInternalMailMinimize();
            UIHelper.TemplateCode = TemplateCode;

            if (LocalData.ApplicationType == ApplicationType.EmailCenter)
            {
                gridViewList.OptionsSelection.EnableAppearanceFocusedRow = false;

                foreach (GridColumn col in gridViewList.Columns)
                {
                    gridViewList.CalcColumnBestWidth(col);
                }
            }
            else
            {
                gridViewList.OptionsSelection.EnableAppearanceFocusedRow = true;
            }

        }
        /// <summary>
        /// 数据绑定之前进行的操作
        /// </summary>
        private void BeforeBindData()
        {
            AdvanceQueryString = AppendAdvanceStringToSQL();
            HasGridViewColumns = false;
            IsClearColumnsAndDataSource = false;
            //区分是否要连接本地缓存数据库还是数据库,1表示连接服务端数据库，0表示连接本地缓存数据库
            SearchType = SearchActionType.Auto;
            //如果从本地缓存数据库里没有找到数据，就需要到SQL Server 去查询
            NeedSearchInSQLServer = false;


            //清空关键字查询框
            if (ToolbarManager.barItems["txtQuery"] is BarEditItem)
            {
                BarEditItem txt = ToolbarManager.barItems["txtQuery"] as BarEditItem;
                txt.EditValue = "";
                txt.Edit.NullText = "Search NO or Customer";
            }
        }

        /// <summary>
        /// 限制邮件中心最小的大小
        /// </summary>
        private void LimitInternalMailMinimize()
        {
            RootWorkItem.State["LimitInternalMailSize"] = 2;
            RootWorkItem.Commands["Command_InternalMailBusinessPartFixedSize"].Execute();
        }

        /// <summary>
        /// 根据业务号和参考号来查询业务数据
        /// </summary>
        /// <returns></returns>
        private string AppendAdvanceStringToSQL()
        {
            StringBuilder strSQL = new StringBuilder();
            ServerQueryString = "";
            List<string> nos = RootWorkItem.State["MatchingSubjectKeyWord"] as List<string>;
            if (nos != null && nos.Count > 0)
            {
                int i = 0;
                foreach (var item in nos)
                {
                    strSQL.Append(GetQueryString(item, i == 0));
                    i++;
                }
            }
            return strSQL.ToString();
        }

        /// <summary>
        /// 将A/B/C分割，拼接SQL
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private string GetQueryString(string query)
        {
            if (query.Contains("/"))
            {
                StringBuilder strBuf = new StringBuilder();
                string[] arrQuery = query.Split(new char[1] { '/' });
                int i = 0;
                foreach (var item in arrQuery)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        strBuf.Append(GetQueryString(item, i == 0));
                        i++;
                    }
                }

                return strBuf.ToString();
            }
            else
            {
                return GetQueryString(query, true);
            }
        }

        private string GetQueryString(string query, bool firstCase)
        {
            //服务端查询条件语句
            GetServerQueryString(query);
            //客户端查询条件语句
            string localSQL = string.Format(" AND (No LIKE '%{0}%' OR RefNO LIKE '%{0}%') ", query);
            if (firstCase)
                return localSQL;
            else
                return string.Format("| {0}", localSQL);
        }

        private void GetServerQueryString(string query)
        {
            //如果邮件中心快速切换邮件，这里会出现 “集合已修改；可能无法执行枚举操作”错误。
            if (string.IsNullOrEmpty(ServerQueryString))
                ServerQueryString =
                    string.Format(" 1=1 AND $@CompanyID@ in ({0}) AND ($@NO@ LIKE '%{1}%' OR $@RefNO@ LIKE '%{1}%') ", GetCompanyQueryString(), query);
            else
            {
                ServerQueryString = string.Format("{0}{1}", ServerQueryString,
                                                                        string.Format(
                                                                            " OR ($@NO@ LIKE '%{0}%' OR  $@RefNO@ LIKE '%{0}%') ",
                                                                            query));
            }
        }

        private string GetCompanyQueryString()
        {
            string strList = string.Empty;
            string[] arrCompanyIDs = SelectedCompanyIds.Split(new char[1] { ',' });
            for (int i = 0; i < arrCompanyIDs.Length; i++)
            {
                strList += (strList.Length == 0 ? "" : ",") + "'" + arrCompanyIDs[i] + "'";
            }
            return strList;
        }

        private void ProcessBarItem(List<BarItem> items, BarItem item, string templateCode)
        {
            OperationToolbarCommand command = item.Tag == null ? null : (OperationToolbarCommand)item.Tag;
            if (command != null && command.Tag == templateCode)
            {
                if (!string.IsNullOrEmpty(command.Name))
                {
                    RootWorkItem.Commands[command.Name].RemoveInvoker(item, "ItemClick");
                    RootWorkItem.Commands[command.Name].Dispose();
                }
                if (!string.IsNullOrEmpty(command.RegisterSite))
                {
                    RootWorkItem.UIExtensionSites.UnregisterSite(command.RegisterSite);
                }
                items.Add(item);

            }


        }

        #endregion

        #region Base Member
        private string demoOperationNo;


        public override DateTime? Updatetime
        {
            get
            {
                if (FocusedDataRow == null)
                    return null;
                return FocusedDataRow["UpdateDate"] as DateTime?;
            }
            set
            {
                base.Updatetime = value;
            }
        }


        public override string OperationNo
        {
            get
            {
                if (FocusedDataRow == null)
                    return string.Empty;
                return FocusedDataRow.Field<string>(OperationNoFieldName);
            }
            set
            {
                base.OperationNo = value;
            }
        }



        public override Guid OperationID
        {
            get
            {
                if (FocusedDataRow == null)
                    return Guid.Empty;
                return FocusedDataRow.Field<Guid>(OperationIDFieldName);
            }
            set
            {
                base.OperationID = value;
            }
        }
        /// <summary>
        /// 业务的口岸ID
        /// </summary>
        public override Guid CompanyID
        {
            get
            {
                if (FocusedDataRow == null)
                    return Guid.Empty;
                return FocusedDataRow.Field<Guid>(CompanyIDFieldName);
            }
            set
            {
                base.CompanyID = value;
            }
        }
        protected string OperationTypeFieldName = Constants.OperationTypeFieldName;
        public override OperationType OperationType
        {
            get
            {
                if (FocusedDataRow == null)
                {
                    return base.OperationType;
                }
                if (FocusedDataRow.Table.Columns.Contains(OperationTypeFieldName))
                {
                    return (OperationType)FocusedDataRow.Field<Byte>(OperationTypeFieldName);
                }
                return base.OperationType;
            }
            set
            {
                base.OperationType = value;
            }
        }

        /// <summary>
        /// 扩展业务类型
        /// </summary>
        public ExpandOperationType ExpandOperationType
        {
            get
            {
                if (FocusedDataRow == null)
                {
                    return ExpandOperationType.Unknown;
                }
                if (FocusedDataRow.Table.Columns.Contains("ExpandOperationType"))
                {
                    return (ExpandOperationType)FocusedDataRow.Field<Byte>("ExpandOperationType");
                }
                return ExpandOperationType.Unknown;
            }
        }

        /// <summary>
        /// 联系人邮件地址
        /// </summary>
        public string ContactMail
        {
            get
            {
                if (FocusedDataRow == null)
                {
                    return string.Empty;
                }
                return FocusedDataRow.Field<string>("ContactMail") == null ? string.Empty : FocusedDataRow.Field<string>("ContactMail");
            }
        }
        public override void UnRegisterExtensionSite()
        {
            base.UnRegisterExtensionSite();
            string businessContextMenuStripUISiteName = GetBusinessContextMenuStripUISiteName();
            if (RootWorkItem.UIExtensionSites.Contains(businessContextMenuStripUISiteName))
            {
                RootWorkItem.UIExtensionSites.UnregisterSite(businessContextMenuStripUISiteName);
            }
        }
        public override void RegisterExtensionSite()
        {
            base.RegisterExtensionSite();
            string businessContextMenuStripUISiteName = GetBusinessContextMenuStripUISiteName();
            if (!RootWorkItem.UIExtensionSites.Contains(businessContextMenuStripUISiteName))
            {
                RootWorkItem.UIExtensionSites.RegisterSite(businessContextMenuStripUISiteName, contextMenuStrip);
            }
        }
        public string GetBusinessContextMenuStripUISiteName()
        {
            return GetPresentTemplateCode() + contextMenuStripUISiteName;
        }

        public override void AfterDataSourceChanged()
        {
            ResetDocumentRecord();
            IsShowLoadingForm = false;
        }
        #endregion

        #region IMessageBaseBusinessPart 成员
        /// <summary>
        /// 保存用户自定义列的信息
        /// </summary>
        public void SaveCustomColumnInfo()
        {
            List<CustomColumnInfo> columnInfos = GetGridColumnInfos();

            if (columnInfos.Count <= 0)
                return;

            UserCustomGridInfo gridInfo = currentUserCustomGridInfo;
            if (gridInfo == null)
            {
                gridInfo = new UserCustomGridInfo();
                gridInfo.TemplateCode = TemplateCode;
                gridInfo.Id = Guid.NewGuid();

            }
            if (gridInfo.UserId == null)
            {
                gridInfo.UserId = LocalData.UserInfo.LoginID;
            }

            gridInfo.Columns = columnInfos;
            try
            {
                isColumnSettingChanged = false;
                ServiceClient.GetService<IClientCustomDataGridService>().Save(gridInfo);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<CustomColumnInfo> GetGridColumnInfos()
        {
            int columnCount = gridViewList.Columns.Count;
            CustomColumnInfo[] list = new CustomColumnInfo[columnCount];
            ///如果有只有代码新增的排序列，则直接返回
            if (columnCount == 1)
            {
                return list.ToList();
            }
            for (int i = 0; i < columnCount; i++)
            {
                GridColumn column = gridViewList.Columns[i];
                list[i] = new CustomColumnInfo();
                list[i].Name = column.Name;
                list[i].Visible = column.Visible;
                list[i].VisibleIndex = column.VisibleIndex + 1;
                list[i].Width = column.Width;
                list[i].Fixed = (ColumnFixedStyle)Enum.Parse(typeof(ColumnFixedStyle), column.Fixed.ToString());
                list[i].AbsoluteIndex = column.AbsoluteIndex;
                list[i].SortOrder = (SortOrder)((int)column.SortOrder);
            }
            Array.Sort<CustomColumnInfo>(list, new CustomColumnInfoComparer());
            return list.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UserCustomGridInfo GetUserCustomGridInfo()
        {
            try
            {
                UserCustomGridInfo customInfo = ServiceClient.GetService<IClientCustomDataGridService>().Get(TemplateCode);
                return customInfo;
            }
            catch (Exception)
            {

            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateCode"></param>
        /// <returns></returns>
        public List<BusinessColumnInfo> GetColumnInfos(string templateCode)
        {
            return ColumnTemplateLoader.Current[templateCode];
        }

        #endregion

        #region 辅助方法
        /// <summary>
        /// 行宽度改变时设置行显示信息改变标识
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnColumnWidthChanged(object sender, ColumnEventArgs e)
        {
            isColumnSettingChanged = true;
        }
        /// <summary>
        /// 行位置改变时设置行显示信息改变标识
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnColumnPositionChanged(object sender, EventArgs e)
        {
            isColumnSettingChanged = true;
        }
        /// <summary>
        /// 移除列表列显示事件处理
        /// </summary>
        private void RemoveEventHandler()
        {
            gridViewList.ColumnPositionChanged -= OnColumnPositionChanged;
            gridViewList.ColumnWidthChanged -= OnColumnWidthChanged;
        }
        /// <summary>
        /// 添加列表列显示事件处理
        /// </summary>
        private void AddEventHandler()
        {
            gridViewList.ColumnPositionChanged += OnColumnPositionChanged;
            gridViewList.ColumnWidthChanged += OnColumnWidthChanged;
        }
        /// <summary>
        /// 
        /// </summary>
        private CustomColumnInfo _DemoColumnInfo;
        /// <summary>
        /// 
        /// </summary>
        public CustomColumnInfo DemoColumnInfo
        {
            get
            {
                if (_DemoColumnInfo == null)
                    _DemoColumnInfo = new CustomColumnInfo { VisibleIndex = visibleIndex++, Fixed = ColumnFixedStyle.None, Visible = true, Width = 100 };
                return _DemoColumnInfo;
            }
        }

        /// <summary>
        /// 生成GridControl列
        /// </summary>
        public void GenerateGridColumns()
        {
            if (gridViewList.Columns.Count == 0 && columnInfos == null)
            {
                CreateColumns(TemplateCode);
            }
        }

        /// <summary>
        /// 根据XML配置和用户自定义列信息 构造列表
        /// </summary>
        /// <param name="columnInfos"></param>
        private List<BusinessColumnInfo> columnInfos;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateCode"></param>
        private void CreateColumns(string templateCode)
        {

            #region 取消列表注册的事件
            gridViewList.Click -= new EventHandler(ViewListClick);
            gridViewList.CustomDrawColumnHeader -= new ColumnHeaderCustomDrawEventHandler(CustomDrawColumnHeader);
            gridViewList.DataSourceChanged -= new EventHandler(DataSourceChanged);
            #endregion
            CurrentDataSource = null;
            try
            {
                columnInfos = GetColumnInfos(templateCode);
                listColumnInfos = null;
                listColumnInfos = columnInfos;
                if (columnInfos == null || columnInfos.Count <= 0)
                    return;
                GetColumnsByProperties();
                SuspendLayout();  //临时挂起控件的布局逻辑。
                RemoveEventHandler();// 移除列表列显示事件处理
                ClearGridControl();
                gridViewList.Tag = templateCode;
                currentUserCustomGridInfo = null;// GetUserCustomGridInfo();

                bool isUserCustomGridInfoNull = IsUserCustomGridInfoNull();//当前列表的用户自定义信息是否为空
                CustomColumnInfo columnCustomInfo = isUserCustomGridInfoNull ? GetDemoCustomColumnInfo() : null;
                int count = columnInfos.Count;
                for (int i = 0; i < count; i++)
                {
                    InnerCreateColumn(columnInfos[i], columnCustomInfo ?? currentUserCustomGridInfo.Columns.Find(c => c.Name.Equals(columnInfos[i].Name)));
                }
                listCustomColumnInfos = null;
                //如果集合中包含Isassociated列，就不需要再添加了
                listCustomColumnInfos = listColumnInfos.FindAll(o => o.FieldName.Equals(associatedColumn));
                if (listCustomColumnInfos == null || listCustomColumnInfos.Count == 0)
                {
                    listCustomColumnInfos = GetCustomColumns();
                    listCustomColumnInfos = listCustomColumnInfos ?? new List<BusinessColumnInfo>();
                    listColumnInfos.AddRange(listCustomColumnInfos);
                    AddCustomColumns(listCustomColumnInfos);
                }

                AddSortColumnInfosGroup(Group);
                AddSortColumnInfos(string.IsNullOrEmpty(Order) ? "NO" : Order);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
                throw ex;
            }
            finally
            {
                AddEventHandler();
                ResumeLayout();
            }
        }

        private void AddSortColumnInfoByAssociated()
        {
            gridViewList.SortInfo.Add(AssociatedSortInfo);
        }

        private CustomColumnInfo GetDemoCustomColumnInfo()
        {
            int absoluteIndex = 0;
            CustomColumnInfo demoColumnInfo = DemoColumnInfo;
            demoColumnInfo.AbsoluteIndex = absoluteIndex++;

            return demoColumnInfo;
        }

        private void GetColumnsByProperties()
        {
            int count = columnInfos.Count;
            for (int i = 0; i < count; i++)
            {
                BusinessColumnInfo item = columnInfos[i];
                if (item.Dropable)
                    listDropableColumns.Add(item);
                if (item.Editable && !item.ReadOnly)
                    listEditableColumns.Add(item);

                item = null;
            }
        }

        private bool IsUserCustomGridInfoNull()
        {
            return currentUserCustomGridInfo == null;
        }

        private void ClearGridControl()
        {
            gridViewList.Columns.Clear();
            gridControlList.RepositoryItems.Clear();
            IsClearColumnsAndDataSource = true;
        }

        public override void Init(object parameter)
        {
            base.Init(parameter);
            if (SetSplitPanelVisibility(false))
            {
                return;
            }
            CreateColumns(TemplateCode);//根据XML配置和用户自定义列信息 构造列表
            if (OperationType == null)
            {
                OperationType = base.OperationContext.OperationType;
            }
            gridViewList.IndicatorWidth = 10;
            //设置行高为27
            gridViewList.RowHeight = 27;
            if (gridViewList.Columns[selectedFieldName] != null)
            {
                gridViewList.Columns[selectedFieldName].Fixed = FixedStyle.Left;
            }
            if (gridViewList.Columns[OperationNoFieldName] != null)
            {
                gridViewList.Columns[OperationNoFieldName].Fixed = FixedStyle.Left;
            }
            List<RowStyleConfigInfo> rowStyle = RowStyleConfigController.GetRowCellStyleName(TemplateCode);

            foreach (var row in rowStyle)
            {
                if (!string.IsNullOrEmpty(row.Field) && !string.IsNullOrEmpty(row.ToolTipCn) &&
                    !string.IsNullOrEmpty(row.ToolTipEn))
                {
                    UpdateToolTip(row.Field, row.ToolTipCn, row.ToolTipEn);
                }
            }
            BulidRowCellStyle();
        }

        /// <summary>
        /// 字体样式
        /// </summary>
        void BulidRowCellStyle()
        {
            List<RowStyleConfigInfo> rowStyleConfig = RowStyleConfigController.GetRowCellStyleName(TemplateCode);
            if (rowStyleConfig.Any() == false) return;
            //获取当前是不是需要改变字体颜色，改变字体颜色那么就不改变单元格背景色
            List<RowStyleConfigInfo> fontStyleConfigInfos = rowStyleConfig.Where(n => n.Font == true).ToList();

            if (fontStyleConfigInfos.Any())
            {

                GridColumn guidColumn = gridViewList.Columns.ColumnByFieldName("NO");


                StyleFormatCondition commStyleFormatCondition = new StyleFormatCondition();
                gridViewList.FormatConditions.Add(commStyleFormatCondition);
                commStyleFormatCondition.Appearance.ForeColor = Color.Black;
                commStyleFormatCondition.Condition = FormatConditionEnum.None;
                commStyleFormatCondition.Expression = "[RC] == False";
                commStyleFormatCondition.Condition = FormatConditionEnum.Expression;
                commStyleFormatCondition.ApplyToRow = false;
                commStyleFormatCondition.Column = guidColumn;


                StyleFormatCondition commStyleFormatCondition2 = new StyleFormatCondition();
                gridViewList.FormatConditions.Add(commStyleFormatCondition2);
                commStyleFormatCondition2.Appearance.ForeColor = Color.Green;
                commStyleFormatCondition2.Condition = FormatConditionEnum.None;
                commStyleFormatCondition2.Expression = "[FRC] == True";
                commStyleFormatCondition2.Condition = FormatConditionEnum.Expression;
                commStyleFormatCondition2.ApplyToRow = false;
                commStyleFormatCondition2.Column = guidColumn;


                StyleFormatCondition commStyleFormatCondition3 = new StyleFormatCondition();
                gridViewList.FormatConditions.Add(commStyleFormatCondition3);
                commStyleFormatCondition3.Appearance.ForeColor = Color.DarkKhaki;
                commStyleFormatCondition3.Condition = FormatConditionEnum.None;
                commStyleFormatCondition3.Expression = "[SqlRCFRC] == True";
                commStyleFormatCondition3.Condition = FormatConditionEnum.Expression;
                commStyleFormatCondition3.ApplyToRow = false;
                commStyleFormatCondition3.Column = guidColumn;

                StyleFormatCondition commStyleFormatCondition4 = new StyleFormatCondition();
                gridViewList.FormatConditions.Add(commStyleFormatCondition4);

                commStyleFormatCondition4.Appearance.ForeColor = Color.Blue;
                commStyleFormatCondition4.Condition = FormatConditionEnum.None;
                commStyleFormatCondition4.Expression = "[SqlIsMonthKnot] == False";
                commStyleFormatCondition4.Condition = FormatConditionEnum.Expression;
                commStyleFormatCondition4.ApplyToRow = false;
                GridColumn guidColumn2 = gridViewList.Columns.ColumnByFieldName("Customer");
                commStyleFormatCondition4.Column = guidColumn2;

                StyleFormatCondition commStyleFormatCondition5 = new StyleFormatCondition();
                gridViewList.FormatConditions.Add(commStyleFormatCondition5);
                commStyleFormatCondition5.Appearance.ForeColor = Color.Red;
                commStyleFormatCondition5.Condition = FormatConditionEnum.None;
                commStyleFormatCondition5.Expression = "[ETATimeOut2] == True";
                commStyleFormatCondition5.Condition = FormatConditionEnum.Expression;
                commStyleFormatCondition5.ApplyToRow = false;
                commStyleFormatCondition5.Column = guidColumn;

            }










        }
        private bool SetSplitPanelVisibility(bool visible)
        {
            bool isUnknownTemplateCode = false;
            //isUnknownTemplateCode = IsUnknownTemplateCode();
            //if (isUnknownTemplateCode && visible)
            //{
            //    splitBusinessPart.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
            //}
            //else
            splitBusinessPart.PanelVisibility = SplitPanelVisibility.Panel1;
            return isUnknownTemplateCode;
        }

        /// <summary>
        /// 检测当前面板是否已被释放
        /// </summary>
        /// <returns></returns>
        private bool IsBusinessPartDisposed()
        {
            return IsDisposed || FindForm().IsDisposed;
        }

        public override void AddCustomColumn(DataTable dt)
        {
            if (listCustomColumnInfos != null && listCustomColumnInfos.Count > 0)
            {
                int count = listCustomColumnInfos.Count;
                for (int i = 0; i < count; i++)
                {
                    string fieldName = listCustomColumnInfos[i].FieldName;
                    DataColumn column = dt.Columns[fieldName];
                    if (column == null)
                    {
                        column = new DataColumn(fieldName);
                        bool isStringColumn = IsStringColumn(listCustomColumnInfos[i]);
                        column.DataType = isStringColumn ? typeof(string) : typeof(int);
                        if (isStringColumn)
                            column.DefaultValue = string.Empty;
                        else
                            column.DefaultValue = 0;

                        //控制并发
                        if (dt.Columns[fieldName] == null)
                            dt.Columns.Add(column);
                    }
                }
            }
        }
        /// <summary>
        /// 判断列对应的Field是否为字符串类型
        /// </summary>
        /// <param name="columnInfo"></param>
        /// <returns></returns>
        private bool IsStringColumn(BusinessColumnInfo columnInfo)
        {
            return (columnInfo.EditType == ColumnEditType.Text || columnInfo.EditType == ColumnEditType.Memo);
        }

        /// <summary>
        /// 按照条件降序排序（默认订单号）
        /// </summary>
        private void AddSortColumnInfos(string orderWhere)
        {
            //List<BusinessColumnInfo> sortColumns = listCustomColumnInfos.FindAll(column => column.Name.ToLower().EndsWith("_sort"));
            //if (sortColumns != null && sortColumns.Count > 0)
            //{
            //    foreach (BusinessColumnInfo columnInfo in sortColumns)
            //    {
            //        AddSortColumnByFieldName(columnInfo.FieldName);
            //    }
            //}
            GridColumnSortInfo sortInfo = AddGridColumnSortInfoByFieldName(orderWhere);
            gridViewList.SortInfo.Add(sortInfo);

        }

        private GridColumnSortInfo AddGridColumnSortInfoByFieldName(string filedName)
        {
            GridColumn columnNo = gridViewList.Columns.ColumnByFieldName(filedName);
            if (filedName == "NO")
            {
                return new GridColumnSortInfo(columnNo, ColumnSortOrder.Descending);
            }
            else
            {
                return new GridColumnSortInfo(columnNo, ColumnSortOrder.Ascending);
            }

        }

        /// <summary>
        /// 按照条件分组排序
        /// </summary>
        /// <param name="group">分组条件</param>
        public void AddSortColumnInfosGroup(string group)
        {
            if (string.IsNullOrEmpty(Group)) return;
            //分组的条件
            gridViewList.GroupSummary.Clear();
            gridViewList.SortInfo.Clear();

            //gridViewList.OptionsView.ShowGroupPanel = true;          // 显示分组panel
            gridViewList.OptionsCustomization.AllowGroup = true;     //是否允许分组
            gridViewList.OptionsView.ShowGroupedColumns = true;     //显示分组的列


            var groupColumn = new List<GridColumnSortInfo>();
            if (Group.Split(',').Any())
            {
                string[] varsplit = Group.Split(',');
                for (int i = 0; i < varsplit.Count(); i++)
                {
                    AddSortColumnInfos(varsplit[i]);
                    GridColumn column = gridViewList.Columns[varsplit[i]];//拿到要分组的列
                    if (column == null)
                    {
                        return;
                    }
                    column.GroupIndex = i;//为分组的情况下，列的groupindex为-1，所有的都是一组
                    GridGroupSummaryItem item = new GridGroupSummaryItem();
                    //根据第一个分组的条件计算总计数
                    if (i == 0)
                    {
                        item.DisplayFormat = LocalData.IsEnglish ? "(Total：{0})" : "(总计：{0})";
                        item.SummaryType = SummaryItemType.Count;
                    }

                    //展开分组的节点
                    gridViewList.OptionsBehavior.AutoExpandAllGroups = true;
                    gridViewList.GroupSummary.Add(item);
                }
            }

        }
        private void AddCustomColumns(List<BusinessColumnInfo> customColumns)
        {
            if (customColumns == null || customColumns.Count <= 0)
                return;
            try
            {
                foreach (BusinessColumnInfo columnInfo in customColumns)
                {
                    GridColumn gridColumn = gridViewList.Columns.Add();

                    gridColumn.Caption = columnInfo.Caption;
                    gridColumn.FieldName = columnInfo.FieldName;
                    gridColumn.Name = columnInfo.Name;
                    RepositoryItem columnEdit = ColumnEditFactory.GetColumnEdit(columnInfo);
                    columnEdit.ReadOnly = columnInfo.ReadOnly;
                    gridControlList.RepositoryItems.AddRange(new RepositoryItem[] { columnEdit });
                    gridColumn.Width = 0;
                    gridColumn.Visible = false;
                    gridColumn.VisibleIndex = -1;
                    gridColumn.ColumnEdit = columnEdit;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
            }

        }


        /// <summary>
        /// 构造列表
        /// </summary>
        /// <param name="columnInfo"></param>
        /// <param name="columnCustomInfo"></param>
        private void InnerCreateColumn(BusinessColumnInfo columnInfo, CustomColumnInfo columnCustomInfo)
        {
            var gridColumn = gridViewList.Columns.Add();
            gridColumn.OptionsColumn.AllowEdit = columnInfo.Editable;
            if (columnCustomInfo != null)
            {
                //mark:当设置列不可见时，就不需要设置VisibleIndex，否则就会显示出来
                if (columnInfo.FieldName.Equals(associatedColumn))
                    gridColumn.Visible = false;
                else
                {
                    gridColumn.Visible = columnCustomInfo.Visible;
                    gridColumn.VisibleIndex = columnCustomInfo.VisibleIndex;
                }

                gridColumn.SortOrder = (ColumnSortOrder)Enum.Parse(typeof(ColumnSortOrder), columnCustomInfo.SortOrder.ToString());
                gridColumn.Width = columnInfo.Width == 0 ? columnCustomInfo.Width : columnInfo.Width;
            }
            else
            {
                gridColumn.Visible = true;
                gridColumn.Width = 100;

            }
            gridColumn.ToolTip = columnInfo.ToolTip;


            gridColumn.Caption = columnInfo.Caption;


            gridColumn.FieldName = columnInfo.FieldName;
            gridColumn.Name = columnInfo.Name;
            RepositoryItem columnEdit = ColumnEditFactory.GetColumnEdit(columnInfo);
            columnEdit.ReadOnly = columnInfo.ReadOnly;
            gridColumn.OptionsColumn.AllowEdit = columnInfo.Editable;
            //当前列是否显示

            #region 去除表头可以查询
            //2014/12/23 周任平 允许表头可查询
            //gridColumn.OptionsFilter.AllowAutoFilter = true;
            //gridColumn.OptionsFilter.AllowFilter = true;
            //gridColumn.OptionsFilter.ImmediateUpdateAutoFilter = false;
            #endregion

            #region 允许表头排序
            gridColumn.OptionsColumn.AllowSort = DefaultBoolean.True;
            #endregion


            //处理当前数据是否需要分组进行排序
            if (!string.IsNullOrEmpty(columnInfo.Group) || !string.IsNullOrEmpty(columnInfo.Order))
            {
                Group = string.IsNullOrEmpty(columnInfo.Group) ? string.Empty : columnInfo.Group;
                Order = string.IsNullOrEmpty(columnInfo.Order) ? string.Empty : columnInfo.Order;
            }
            //对于字段属性为DateTime特殊的处理 
            if (columnInfo.Convert)
            {
                gridColumn.DisplayFormat.FormatType = FormatType.DateTime;
                gridColumn.DisplayFormat.FormatString = LocalData.IsEnglish == false ? "yyyy-MM-dd" : "yyyy/MM/dd";
            }
            #region 对文件日和装柜日做特殊处理
            if (gridColumn.FieldName.Contains("SICut") || gridColumn.FieldName.Contains("TrkLoadOn"))
            {
                gridColumn.DisplayFormat.FormatType = FormatType.DateTime;
                gridColumn.DisplayFormat.FormatString = "MM/dd HH:mm";
            }
            #endregion

            gridControlList.RepositoryItems.AddRange(new RepositoryItem[] { columnEdit });
            gridColumn.ColumnEdit = columnEdit;

            if (columnCustomInfo != null)
            {
                if (!columnCustomInfo.Visible)
                {
                    gridViewList.Columns.ColumnByFieldName(columnInfo.FieldName).Visible = false;
                }
                gridColumn.AbsoluteIndex = columnCustomInfo.AbsoluteIndex;
            }
            if (string.IsNullOrEmpty(columnInfo.Caption))
            {
                gridViewList.Click += new EventHandler(ViewListClick);
                gridViewList.CustomDrawColumnHeader += new ColumnHeaderCustomDrawEventHandler(CustomDrawColumnHeader);
                gridViewList.DataSourceChanged += new EventHandler(DataSourceChanged);
            }
        }


        /// <summary>
        /// 添加用户用于特殊目的的自定义列,比如排序等
        /// </summary>
        /// <returns></returns>
        public List<BusinessColumnInfo> GetCustomColumns()
        {
            ICustomColumnGetter getter = BusinessInfoExtractorFactory.GetCustomColumnGetter(ParameterFullName);
            return getter.Get();
        }

        /// <summary>
        /// 重新绑定数据源
        /// </summary>
        public void ResetBinding()
        {
            bindingSource.ResetBindings(false);
        }
        /// <summary>
        /// 取消编辑
        /// </summary>
        public void CancelEdit()
        {
            (bindingSource.DataSource as DataTable).RejectChanges();
            bindingSource.CancelEdit();
            bindingSource.ResetBindings(false);
        }
        /// <summary>
        /// 接受更改
        /// </summary>
        public void AcceptChanges()
        {
            //当数据源为空时，赋值数据源
            if (bindingSource == null)
            {
                bindingSource.DataSource = RootWorkItem.State["BaseDataSource"];
            }
            DataTable dt = bindingSource.DataSource as DataTable;
            if (dt == null)
                return;
            dt.AcceptChanges();
            bindingSource.EndEdit();
            bindingSource.ResetBindings(false);
        }
        #endregion

        #region 全选按钮的操作
        private bool m_checkStatus = false;
        /// <summary>
        /// 列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewListClick(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            Point p = view.GridControl.PointToClient(MousePosition);
            GridHitInfo info = view.CalcHitInfo(p);
            if (info.HitTest == GridHitTest.Column)
            {
                if (DevHelper.ClickGridCheckBox(gridViewList, "Selected", m_checkStatus))
                {
                    m_checkStatus = !m_checkStatus;
                }
            }
        }

        private ColumnHeaderCustomDrawEventArgs checkColumnHeaderCustomDrawEventArgs;

        /// <summary>
        /// 自定义列表表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {

            if (e.Column != null && e.Column.FieldName == "Selected")
            {
                checkColumnHeaderCustomDrawEventArgs = e;
                e.Info.InnerElements.Clear();
                e.Painter.DrawObject(e.Info);
                DevHelper.DrawCheckBox(e, m_checkStatus);
                e.Handled = true;
            }
        }

        /// <summary>
        /// 处理数据源变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DataSourceChanged(object sender, EventArgs e)
        {

            GridColumn column = gridViewList.Columns.ColumnByFieldName("Selected");
            if (column != null)
            {
                column.Width = 30;
                column.OptionsColumn.ShowCaption = false;
                column.ColumnEdit = new RepositoryItemCheckEdit();
            }
        }

        #endregion

        #region 处理拖放操作


        /// <summary>
        /// 设置拖放效果为复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridControlList_DragEnter(object sender, DragEventArgs e)
        {


            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        public string fieldName = string.Empty;
        void gridControlList_DragDrop(object sender, DragEventArgs e)
        {

            GridControl grid = sender as GridControl;
            GridView view = grid.MainView as GridView;
            GridHitInfo hi = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
            //Point gvLocation = PointToScreen(gridControlList.Location);
            //int x = e.X - gvLocation.X;
            //int y = e.Y - gvLocation.Y - 20;
            //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = this.gridViewList.CalcHitInfo(new Point(x,y));
            if (hi.RowHandle < 0)
                return;
            int iMouseRowHandle = hi.RowHandle;
            //拖拽行等于当前选中行
            gridViewList.FocusedRowHandle = iMouseRowHandle;

            if (LocalData.ApplicationType == ApplicationType.ICP)
            {
                fieldName = hi.Column.FieldName;
                //如果拖动到的列不是可拖入列，则直接返回
                if (!ValidateTargetFieldDropable(fieldName))
                    return;
            }
            string[] files = MailUtility.GetDropFiles(e.Data);
            if (files == null || files.Length <= 0)
                return;
            ///只有符合类型的文档才允许拖入业务列表
            string fileExtensions = CommonUIUtility.GetFileExtensions();
            List<string> listTareget = files.Where(file => fileExtensions.Contains(Path.GetExtension(file).ToLower())).ToList();
            if (listTareget.Any() == false)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "Upload attachments do not conform to the rules in the attachment, please upload the revised again." : "上传的附件不符合附件规则，请修改后再上传.");
                return;
            }
            if (LocalData.ApplicationType == ApplicationType.EmailCenter)
            {
                //打开上传附件页面
                RootWorkItem.State["DragDocList"] = listTareget;  //拖拽的附件列表
                RootWorkItem.Commands["Command_EmailCenterUploadDragAttachment"].Execute();
            }
            else
                HandleDragDrop(iMouseRowHandle, listTareget, fieldName);
        }

        /// <summary>
        /// 验证拖动到的目标列是否可拖入
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private bool ValidateTargetFieldDropable(string fieldName)
        {
            return listDropableColumns.Exists(columnInfo => columnInfo.FieldName == fieldName);

        }
        /// <summary>
        /// 通过行索引查找数据行
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        public DataRow GetDataRowByRowHandle(int rowHandle)
        {
            if (rowHandle < 0)
                return null;
            DataRow row = gridViewList.GetDataRow(rowHandle);
            return row;
        }
        private DataTable GetDataSource()
        {
            return bindingSource.DataSource as DataTable;
        }
        /// <summary>
        ///一票业务下不允许存在多个重名文档
        /// </summary>
        /// <param name="temps"></param>
        /// <param name="existsCopyName"></param>
        /// <returns></returns>
        private bool ValidateDuplicateCopyExists(List<string> temps, DataRow row)
        {
            if (temps == null || temps.Count <= 0 || row == null)
                return false;
            List<string> listExists = new List<string>();
            foreach (BusinessColumnInfo column in listDropableColumns)
            {
                string fileNames = string.Empty;
                if (row.Table.Columns.Contains(column.FieldName))
                {
                    fileNames = row.Field<string>(column.FieldName);
                }
                if (string.IsNullOrEmpty(fileNames))
                    continue;
                List<string> temp2 = fileNames.Split(CommonConstants.NormalSeparator.ToCharArray()).Where(name => !string.IsNullOrEmpty(name)).ToList();
                listExists.AddRange(temp2);

            }

            List<string> listCommon = temps.Intersect(listExists).ToList();
            if (listCommon != null && listCommon.Count > 0)
            {
                string tip = string.Format(LocalData.IsEnglish ? "File(s):{0} already exists,Do you want to overrite?" : "文档:{0}已经存在,是否覆盖?", listCommon.Aggregate((a, b) => a + "," + b));
                DialogResult result = MessageBoxService.ShowQuestion(tip);
                if (result == DialogResult.OK)
                {
                    DeleteDocument(listCommon);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// 提供给具体业务面板处理拖放操作(处理SONO号)
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="filePaths"></param>
        public virtual void HandleDragDrop(int rowHandle, List<string> filePaths, string fieldName)
        {
            if (rowHandle < 0)
                return;
            string normalSeparator = CommonConstants.NormalSeparator;
            List<string> listFileName = (from file in filePaths
                                         select Path.GetFileName(file)).ToList();
            string fileNames = listFileName.Aggregate((a, b) => a + normalSeparator + b);
            string copyPaths = filePaths.Aggregate((a, b) => a + normalSeparator + b);
            DataRow row = GetDataRowByRowHandle(rowHandle);

            if (ValidateDuplicateCopyExists(listFileName, row))
                return;
            DataTable dt = GetDataSource();
            Guid operationId = row.Field<Guid>(OperationIDFieldName);

            List<String> listExistsCopyPath = listDicAddedDocuments.ContainsKey(operationId) ? listDicAddedDocuments[operationId] : new List<string>();

            string existsCopyName = row.Field<string>(fieldName);
            row[fieldName] = string.IsNullOrEmpty(existsCopyName) ? fileNames : existsCopyName + CommonConstants.NormalSeparator + fileNames;
            listDicAddedDocuments[operationId].AddRange(filePaths);

            EnumDocumentType documentType = listDropableColumns.Find(columnInfo => columnInfo.FieldName == fieldName).DocumentType;
            int count = filePaths.Count;
            List<DocumentInfo> documents = new List<DocumentInfo>();
            DocumentInfo document = null;
            for (int i = 0; i < count; i++)
            {
                string fileName = Path.GetFileName(filePaths[i]);
                document = new DocumentInfo();
                document.Id = Guid.NewGuid();
                document.Name = fileName;
                document.OriginalPath = filePaths[i];
                document.DocumentType = documentType;
                document.CreateBy = LocalData.UserInfo.LoginID;
                document.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                document.FormType = FormType.Unknown;
                document.OperationID = operationId;
                document.Type = OperationType;

                documents.Add(document);
                AddItemToOperationIdsList(operationId);
            }
            listAddedDocuments.AddRange(documents);

            if (documentType == EnumDocumentType.OSO && gridViewList.Columns.ColumnByFieldName(SaveEditDataFieldName) != null && fieldName == "SOCopy")
            {

                string existsSONOs = row.Field<string>(SaveEditDataFieldName);
                if (SpecialConditioner.IsAutoFileSONO())
                    AutoFillSONOs(operationId, existsSONOs, filePaths);
            }

            ResetBinding();
            gridViewList.SelectRow(rowHandle);
            // SetBarItemEnable(SaveName, true);


        }

        private void AddItemToEditdList(int rowHandle, string filedName)
        {
            if (!listDicEditedCells[rowHandle].Contains(filedName))
            {
                listDicEditedCells[rowHandle].Add(filedName);
            }
        }

        /// <summary>
        /// 通过业务ID查找所属数据行
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public DataRow GetDataRowByOperationId(Guid operationId)
        {
            DataRow[] rows = CurrentDataSource.Select(string.Format(OperationIDFieldName + "='{0}'", operationId));
            if (rows != null && rows.Length > 0)
                return rows.First();
            return null;
        }
        private string soNOseparator = "/";
        /// <summary>
        /// 自动根据拖拽文件生成SONO
        /// 规则为:
        /// 1.如果只有一个附件，则SONO为附件名称
        /// 2.多于一个附件，则首先取第一个附件的完整名称，再加上其他附件名称的后两位，中间用/分隔
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private void AutoFillSONOs(Guid operationId, string existsSONOs, List<string> filePaths)
        {
            DataRow row = GetDataRowByOperationId(operationId);

            string soNos = (from file in filePaths
                            select Path.GetFileNameWithoutExtension(file)).ToList().Aggregate((a, b) => a + CommonConstants.NormalSeparator + b);
            string newSONos = soNos;
            if (string.IsNullOrEmpty(existsSONOs))
            {
                if (filePaths.Count < 2)
                {
                    newSONos = soNos;
                }
                else
                {
                    string temp = filePaths[0];
                    filePaths.RemoveAt(0);
                    soNos = (from file in filePaths
                             let item = Path.GetFileNameWithoutExtension(file)
                             select item.Substring(item.Length - 2, 2)).ToList().Aggregate((a, b) => a + CommonConstants.NormalSeparator + b);
                    newSONos = string.Format("{0}{1}{2}", temp, soNOseparator, soNos);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(soNos))
                {
                    if (soNos.Length > 2)
                    {
                        soNos = soNos.Substring(soNos.Length - 2, 2);
                    }
                    newSONos = string.Format("{0}{1}{2}", existsSONOs, soNOseparator, soNos);
                }
            }
            row[SaveEditDataFieldName] = newSONos;
            int datasourceIndex = CurrentDataSource.Rows.IndexOf(row);
            int rowHandle = gridViewList.GetRowHandle(datasourceIndex);
            AddItemToEditdList(rowHandle, SaveEditDataFieldName);


        }


        #region 快捷键响应

        /// <summary>
        /// 监视Windows消息
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            //禁止快捷键
            //Message.ServiceInterface.Message messageInfo = MailHelper.GetMailInfo();
            //if (messageInfo != null)
            //{
            //    if (messageContextMenuStateList != null)
            //    {
            //        List<MailContactInfo> contacts = MailContactInfo.Convert(messageInfo);
            //        if (messageContextMenuStateList.ContainsKey(contacts) && messageContextMenuStateList[contacts] == false)
            //            return;
            //    }
            //}
            const int WM_HOTKEY = 0x0312;//按快捷键
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    //调用主处理程序
                    if (gridViewList.IsFocusedView)
                    {
                        if (hotKeys.ContainsKey(m.WParam.ToInt32()))
                        {
                            menuItem_Click(hotKeys[m.WParam.ToInt32()], new EventArgs());
                        }
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        #endregion

        private static Dictionary<List<MailContactInfo>, Boolean> messageContextMenuStateList = new Dictionary<List<MailContactInfo>, Boolean>();  //message的右键菜单状态（是否允许显示）
        private static Dictionary<Int32, DXMenuItem> hotKeys = new Dictionary<Int32, DXMenuItem>();
        private static ListDictionary<string, ContextMenuItemInfo> contextMenuCommonItemList = new ListDictionary<string, ContextMenuItemInfo>();
        private static object synObj = new object();
        private static bool IsCommonContextMenuItemExists(string templateCode)
        {
            lock (synObj.GetType())
            {
                return contextMenuCommonItemList.ContainsKey(templateCode);

            }
        }

        void gridViewList_MouseDown(object sender, MouseEventArgs e)
        {


            #region 注册快捷键

            if (LocalData.ApplicationType == ApplicationType.EmailCenter)
            {
                if (gridViewList.CalcHitInfo(new Point(e.X, e.Y)).Column == null)
                    return;

                //禁止右键菜单
                Message.ServiceInterface.Message messageInfo = MailHelper.GetMailInfo();
                if (messageInfo != null)
                {
                    List<MailContactInfo> contacts = MailContactInfo.Convert(messageInfo);
                    SetContextMenuState(contacts);
                    if (messageContextMenuStateList != null)
                    {
                        if (messageContextMenuStateList.ContainsKey(contacts) && messageContextMenuStateList[contacts] == false)
                            return;
                    }
                }

                #region 业务菜单项快捷键（禁用——已注释）

                //////业务菜单项
                ////IBusinessContextMenuItemGenerator generator = BusinessContextMenuItemGeneratorFactory.Get(this.OperationType);
                ////string contextMenuStripUISiteName = GetBusinessContextMenuStripUISiteName();
                ////List<ContextMenuItemInfo> businessItems = generator.Get(this.gridViewList.GetFocusedDataRow(), contextMenuStripUISiteName);
                ////int n = businessItems.Count();
                ////Int32 i = 49 + n;  //1键值为49（倒序添加）
                ////foreach (ContextMenuItemInfo item in businessItems)
                ////{
                ////    i = i - 1;
                ////    if (!hotKeys.ContainsKey(i))
                ////    {
                ////        UnregisterHotKey(Handle, i);
                ////        RegisterHotKey(Handle, i, KeyModifiers.Control, (Keys)i);

                ////        MenuItemTag tag = new MenuItemTag(item.Name, string.IsNullOrEmpty(item.BusinessNo) ? item.Tag : item.BusinessNo);
                ////        DXMenuItem menuItem = MenuItemFactory.GetMenuItem(item, tag);
                ////        menuItem.Enabled = item.Enabled;
                ////        if (!string.IsNullOrEmpty(item.Name))
                ////        {
                ////            menuItem.Click += new EventHandler(menuItem_Click);
                ////        }

                ////        hotKeys.Add(i, menuItem);
                ////    }
                ////}

                #endregion

                string templateCode = string.Format("{0}_{1}", TemplateCode, OperationType.ToString());
                if (IsCommonContextMenuItemExists(templateCode) == false)
                {
                    Clipboard.Clear();//先清空剪贴板防止剪贴板里面先复制了其他内容

                    SectionKey key = new SectionKey { SectionCode = templateCode };
                    List<ContextMenuItemInfo> items = ContextMenuFileLoader.Current[key];
                    lock (synObj)
                    {
                        contextMenuCommonItemList[templateCode].AddRange(items);
                    }

                    Int32 j = 100;
                    foreach (ContextMenuItemInfo menu in items)
                    {
                        j = j + 1;
                        if (!hotKeys.ContainsKey(j))
                        {
                            Keys hotkey = SetContextMenuItemKeys(menu.Id);
                            if (hotkey != Keys.None)
                            {
                                UnregisterHotKey(Handle, j);
                                RegisterHotKey(Handle, j, KeyModifiers.Control, hotkey);
                            }
                            MenuItemTag tag = new MenuItemTag(menu.Name, string.IsNullOrEmpty(menu.BusinessNo) ? menu.Tag : menu.BusinessNo);
                            DXMenuItem menuItem = MenuItemFactory.GetMenuItem(menu, tag);
                            menuItem.Enabled = menu.Enabled;
                            if (!string.IsNullOrEmpty(menu.Name))
                            {
                                menuItem.Click += new EventHandler(menuItem_Click);
                            }

                            hotKeys.Add(j, menuItem);
                        }
                    }
                }
            }

            #endregion

            //右键单击时，显示右键菜单
            if (e.Button == MouseButtons.Right && e.Clicks == 1)
                ShowContextMenu(gridViewList.CalcHitInfo(new Point(e.X, e.Y)));
        }

        /// <summary>
        /// 设置右键菜单项快捷键
        /// </summary>
        /// <param name="contextMenuItemID">菜单项ID</param>
        /// <returns>HotKey</returns>
        public Keys SetContextMenuItemKeys(string contextMenuItemID)
        {
            Keys hotkey = Keys.None;
            switch (contextMenuItemID.Trim())
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

        void menuItem_Click(object sender, EventArgs e)
        {
            //if (RaiseClickEvent(sender, null)) return;
            DXMenuItem item = sender as DXMenuItem;
            RootWorkItem.State["CurrentBaseBusinessPart"] = this;
            MenuItemTag tag = (MenuItemTag)item.Tag;
            CurrentContextMenuItemTag = tag.Tag;
            string itemName = tag.Name;
            if (!LocalCommands.Contains(itemName))
            {
                RootWorkItem.Commands[itemName].Execute();
            }
            else
            {
                MethodInfo method = GetType().GetMethod(itemName);
                method.Invoke(this, new object[] { sender, e });
            }
        }
        /// <summary>
        /// 显示鼠标右键菜单
        /// </summary>
        /// <param name="hi"></param>
        void ShowContextMenu(GridHitInfo hi)
        {
            if (hi.Column == null) return;

            if (FocusedDataRow != null && FocusedDataRow.Table.Columns.Contains("OperationType"))
            {
                OperationType = (OperationType)int.Parse(FocusedDataRow["OperationType"].ToString());
            }
            int clickRowHandle = hi.RowHandle;
            if (clickRowHandle < 0)
            {
                return;
            }
            int currentFocusedRowHandle = gridViewList.FocusedRowHandle;
            if (clickRowHandle != currentFocusedRowHandle)
            {
                gridViewList.FocusedRowHandle = clickRowHandle;
            }
            DataRow row = gridViewList.GetFocusedDataRow();

            if (!ShowRightContextMenu(row))
                return;


            UnregisterContextMenuUISite();

            BusinessColumnInfo columnInfo = listDropableColumns.Find(column => column.FieldName == hi.Column.FieldName);
            // 如果点击的是列表单元格，则显示菜单
            if (hi.HitTest == GridHitTest.RowCell)
            {
                CustomGridMenu gridMenu = new CustomGridMenu(gridViewList);
                RegisterContextMenuUISite(gridMenu);
                gridMenu.InitData(this, row, columnInfo, GetPresentTemplateCode());
                gridMenu.Init(hi);
                //显示在光标点击位置
                gridMenu.Show(hi.HitPoint);
            }
        }

        /// <summary>
        /// 设置联系人的右键菜单显示状态，true允许，false禁止
        /// </summary>
        private void SetContextMenuState(List<MailContactInfo> contacts)
        {
            //如果有邮件不存在于联系人列表，并且不是内部邮件，则禁止显示右键菜单
            if (!messageContextMenuStateList.ContainsKey(contacts) || messageContextMenuStateList[contacts] == false)
            {
                foreach (var item in contacts)
                {
                    EmailSourceType? mailType = ClientBusinessContactService.GetEmailType(item.EmailAddress);
                    if (mailType.Value == EmailSourceType.Unknown && FCMInterfaceUtility.ExsitsInternalContact(item.EmailAddress) == false)
                    {
                        if (messageContextMenuStateList.ContainsKey(contacts))
                            messageContextMenuStateList[contacts] = false;
                        else
                            messageContextMenuStateList.Add(contacts, false);
                        return;
                    }
                }
                if (messageContextMenuStateList.ContainsKey(contacts))
                    messageContextMenuStateList[contacts] = true;
                else
                    messageContextMenuStateList.Add(contacts, true);
            }
        }

        private bool ShowRightContextMenu(DataRow row)
        {
            if (row == null)
                return false;
            if (TemplateCode.Equals(ListFormType.MailLink4Unknown.ToString()))
            {
                if (IsShowAssistantPart.HasValue)
                {
                    if (IsShowAssistantPart.Value)
                        return true;
                    else
                        return false;
                }
                else
                    return true;
            }

            return true;
        }

        private void RegisterContextMenuUISite(CustomGridMenu gridMenu)
        {
            string businessContextMenuStripUISiteName = GetBusinessContextMenuStripUISiteName();
            if (!RootWorkItem.UIExtensionSites.Contains(businessContextMenuStripUISiteName))
            {
                RootWorkItem.UIExtensionSites.RegisterSite(businessContextMenuStripUISiteName, gridMenu);
            }
        }

        private void UnregisterContextMenuUISite()
        {
            string businessContextMenuStripUISiteName = GetBusinessContextMenuStripUISiteName();
            if (RootWorkItem.UIExtensionSites.Contains(businessContextMenuStripUISiteName))
            {
                RootWorkItem.UIExtensionSites.UnregisterSite(businessContextMenuStripUISiteName);
            }

        }

        void toolStripItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;

            RootWorkItem.State["CurrentBaseBusinessPart"] = this;
            CurrentContextMenuItemTag = item.Tag;
            string itemName = item.Name;
            if (!localCommands.Contains(itemName))
            {
                RootWorkItem.Commands[itemName].Execute();
            }
            else
            {
                MethodInfo method = GetType().GetMethod(itemName);
                method.Invoke(this, new object[] { sender, e });
            }
        }

        private List<ToolStripItem> GetBusinessItems()
        {
            List<ToolStripItem> items = new List<ToolStripItem>();
            foreach (ToolStripItem item in contextMenuStrip.Items)
            {
                if (item.Tag != null)
                {
                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonConstants.Command_Add_Attachment_Name)]
        public void Command_Add_Attachment(object sender, EventArgs e)
        {
            EnumDocumentType documentType = (EnumDocumentType)Enum.Parse(typeof(EnumDocumentType), CurrentContextMenuItemTag.ToString());
            string fieldName = listDropableColumns.Find(columnInfo => columnInfo.DocumentType == documentType).FieldName;

            string[] filePaths = CommonUIUtility.SelectFilesToUpload();
            if (filePaths == null) return;
            //文档大小超出限制是以异常形式抛出，故在此捕获异常
            try
            {
                if (!CommonUIUtility.ValidateFileInfo(filePaths)) return;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, ex);
                return;
            }

            HandleDragDrop(CurrentRowHandle, filePaths.ToList(), fieldName);

        }

        [CommandHandler("Command_VisibilitySaveBarItem")]
        public void Command_VisibilitySaveBarItem(object sender, EventArgs e)
        {
            object objBarItemVisible = RootWorkItem.State["BarItemVisibility"];
            if (objBarItemVisible == null || objBarItemVisible == "")
            {
                ToolbarManager.VisibleBarItem(barManager, "btnSaveAssistant", BarItemVisibility.Always);
            }
            else
            {
                bool visible;
                bool.TryParse(objBarItemVisible.ToString(), out visible);
                if (visible)
                    ToolbarManager.VisibleBarItem(barManager, "btnSaveAssistant", BarItemVisibility.Always);
                else
                    ToolbarManager.VisibleBarItem(barManager, "btnSaveAssistant", BarItemVisibility.Never);
            }
        }

        /// <summary>
        /// 从字符串中移除指定的子字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        private string RemoveSubString(string source, string target, string separator)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(target))
                return source;
            List<string> temp = source.Split(separator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            temp.Remove(target);
            temp.RemoveAll(item => item == separator);
            if (temp.Count == 0)
                return null;
            return temp.Aggregate((a, b) => a + separator + b).Trim(separator.ToCharArray());
        }

        /// <summary>
        /// 删除附件
        /// 如果是在服务端已保存的文档，则获取出来的信息为 文件名
        /// 否则获取出来为 本地文件的绝对路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonConstants.Command_Delete_Attachment_Name)]
        public void Command_Delete_Attachment(object sender, EventArgs e)
        {
            string attachmentName = CurrentContextMenuItemTag.ToString();
            DeleteDocument(new List<string> { attachmentName });
        }

        private void DeleteDocument(List<string> listFileName)
        {
            if (listFileName == null || listFileName.Count == 0)
                return;
            int rowHandle = CurrentRowHandle;
            foreach (string attachmentName in listFileName)
            {
                //测试是否为本地文件绝对路径，如果是，则代表当前要删除的文档为本地刚上传且还没提交到服务端保存的文档
                bool isLocalPath = Path.IsPathRooted(attachmentName);
                EnumDocumentType documentType;
                string fieldName;
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(attachmentName);
                string fileName = Path.GetFileName(attachmentName);
                //如果是本地刚上传还未提交到服务端的文档，则从相关的维护集合中删除上传信息
                if (isLocalPath)
                {
                    listDicAddedDocuments[OperationID].Remove(attachmentName);
                    DocumentInfo documentInfo = listAddedDocuments.Find(document => document.OperationID == OperationID && document.OriginalPath == attachmentName);
                    listAddedDocuments.Remove(documentInfo);
                    documentType = documentInfo.DocumentType.HasValue ? documentInfo.DocumentType.Value : EnumDocumentType.Other;
                    fieldName = listDropableColumns.Find(column => column.DocumentType == documentType).FieldName;
                    //更新对应的附件列文件名称
                    CurrentRow[fieldName] = RemoveSubString(CurrentRow[fieldName].ToString(), fileName, CommonConstants.NormalSeparator);
                }
                //如果是删除服务端文档，则添加删除记录
                else
                {
                    listDicDeletedDocuments[OperationID].Add(attachmentName);
                    List<string> dropableFieldNames = listDropableColumns.Select<BusinessColumnInfo, string>(column => column.FieldName).ToList();
                    fieldName = GetFieldName(CurrentRow, dropableFieldNames, attachmentName);
                    documentType = listDropableColumns.Find(column => column.FieldName == fieldName).DocumentType;
                    //更新对应的附件列文件名称
                    CurrentRow[fieldName] = RemoveSubString(CurrentRow[fieldName].ToString(), attachmentName, CommonConstants.NormalSeparator);
                }

                //如果是自定填充SO号，则从SO号列移除对应的SO号
                if (documentType == EnumDocumentType.OSO && gridViewList.Columns.ColumnByFieldName(SaveEditDataFieldName) != null && SOSetting.Current.AutoFillSO)
                {

                    string removeNO = fileNameWithoutExtension;
                    if (CurrentRow.Field<string>(SaveEditDataFieldName) != null && !CurrentRow.Field<string>(SaveEditDataFieldName).StartsWith(fileNameWithoutExtension))
                    {
                        removeNO = fileNameWithoutExtension.Substring(fileNameWithoutExtension.Length - 2, 2);
                    }
                    string newSONO = RemoveSubString(CurrentRow.Field<string>(SaveEditDataFieldName), removeNO, soNOseparator);
                    CurrentRow[SaveEditDataFieldName] = newSONO;
                    AddItemToEditdList(rowHandle, SaveEditDataFieldName);
                }
            }
            ResetBinding();
            gridViewList.FocusedRowHandle = rowHandle;
        }
        private void RecordChange(int rowHandle, string fieldName)
        {

            if (listEditableColumns.Exists(column => column.FieldName == fieldName))
            {
                if (!IsDataEdited)
                {
                    IsDataEdited = true;
                }
                AddItemToEditdList(rowHandle, fieldName);
            }
        }

        private string GetFieldName(DataRow dataRow, List<string> dropableFieldNames, string attachmentName)
        {
            foreach (string fieldName in dropableFieldNames)
            {
                string realField = dataRow.Field<string>(fieldName);
                if (!string.IsNullOrEmpty(realField) && realField.Contains(attachmentName))
                    return fieldName;
            }
            throw new NullReferenceException("无法找到附件所在的列名!");
        }

        /// <summary>
        /// 编辑改变单元格触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGeidViewCellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            string fieldName = e.Column.FieldName;
            int rowHandle = e.RowHandle;
            RecordChange(rowHandle, fieldName);
        }
        /// <summary>
        /// 选择行触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGridViewFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (FocusedRowChangedHandler != null)
            {
                ButtonDisplayed();
                EmailButtonDisplayed();
                FocusedRowChangedHandler(this, e);
            }
            OceanImportButtonisenabled();
            OceanExportButtonisenabled();
            //业务参与人面板需要动态构造数据
            //AssistantPartBindData();
        }

        void gridViewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridViewList.GetFocusedDisplayText().Length > 10)
            {
                toolTipController1.ShowHint(gridViewList.GetFocusedDisplayText());
            }
        }

        private void gridViewList_RowCountChanged(object sender, EventArgs e)
        {
            //if (gridViewList.RowCount > 0)
            //{
            //    //gridViewList.SelectRow(0);
            //    //this.gridViewList.SetRowCellValue(0, "Selected", "True");
            //}

        }

        /// <summary>
        /// 保存列表数据更改
        /// </summary>
        public bool Save()
        {
            //取消单元格编辑模式
            bindingSource.EndEdit();
            gridViewList.CloseEditor();
            return InnerSaveData();
        }

        private bool InnerSaveData()
        {
            if (listDicAddedDocuments.Count == 0 && listDicDeletedDocuments.Count == 0 && listDicEditedCells.Count == 0)
                return false;
            string message = LocalData.IsEnglish ? "Saving is in progress..." : "正在保存。。。";
            tokenID = LoadingServce.ShowLoadingForm(gridControlList, message);
            try
            {
                string StateFieldName = string.Empty;
                if (RootWorkItem.State["FieldName"] != null)
                {
                    StateFieldName = RootWorkItem.State["FieldName"].ToString();
                }
                //判断鼠标邮件以及拖拽时候的fileName
                string fileNames = string.IsNullOrEmpty(fieldName) ? StateFieldName : fieldName;
                if (!fileNames.Equals("AdjSOCopy"))
                {
                    //先保存业务更改信息
                    if (!string.IsNullOrEmpty(SaveUpdateOperationData()))
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), SaveUpdateOperationData());
                        return false;
                    }

                }
                //保存文档更改信息，并且更新业务附件列表
                if (SaveAttachment())
                {
                    //更新本地数据
                    if (operationIds.Count > 0)
                    {
                        foreach (Guid id in operationIds)
                        {
                            FCMCommonOperationService.UpdateLocalBusinessData(id, OperationType);
                        }
                        SpecialConditioner.SaveOperationMemo(TemplateCode, operationIds, OperationType, FormID);
                        //邮件中心上传附件列表后需要保存关联信息并且更新本地缓存
                        //if (LocalData.ApplicationType == ApplicationType.EmailCenter)
                        //{
                        //}
                    }
                    AfterSaveDocument();
                    //这里当ANCopy有上传文件的时候 执行方法
                    if (fileNames.Equals("ANCopy"))
                    {
                        SaveEventAnrc();
                    }
                    //上传SOCOPY以后的操作
                    if (fileNames.Equals("SOCopy"))
                    {
                        SaveEventSod();
                    }
                    //这里只针对于未订舱节点做刷新
                    if (TemplateCode.Contains("OceanExport_SpaceNewBookingALL"))
                    {
                        DeleteRow();
                    }
                    RootWorkItem.State["FieldName"] = null;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return false;
            }
            finally
            {
                LoadingServce.CloseLoadingForm(tokenID);
            }
            return true;
        }


        /// <summary>
        /// 操作的当前数据行
        /// </summary>
        private DataRow _row = null;
        private string SaveUpdateOperationData()
        {
            if (listDicEditedCells.Count > 0 || listEditableColumns.Any())
            {
                foreach (int rowhandle in listDicEditedCells.Keys)
                {
                    if (listDicEditedCells[rowhandle].Contains(SaveEditDataFieldName))
                    {
                        _row = GetDataRowByRowHandle(rowhandle);
                        string soNo = string.Empty;
                        string soCopy = string.Empty;
                        if (_row != null)
                        {
                            soNo = _row[SaveEditDataFieldName].ToString();
                            soCopy = _row["SOCopy"].ToString();
                        }
                        if (!string.IsNullOrEmpty(soCopy) && string.IsNullOrEmpty(soNo))
                        {

                            return LocalData.IsEnglish ? "Please input So No., and then save." : "请输入So No.,再保存.";
                        }
                        if (soNo.Length > 50)
                        {
                            return LocalData.IsEnglish ? "So No. Over length limit, please amend So No.." : "So No.已超过长度限制,请修改So No..";
                        }
                        if (string.IsNullOrEmpty(soNo) && string.IsNullOrEmpty(soCopy))
                        {
                            return LocalData.IsEnglish ? "Save failed upload attachment" : "上传附件保存失败";
                        }
                    }
                }

                List<BusinessSaveParameter> filedParameters = GetUpdateFiledNameParameter();
                if (filedParameters != null && filedParameters.Count > 0)
                {
                    if (filedParameters[0].items.ContainsKey(SaveEditDataFieldName))
                    {

                        listDicEditedCells.Clear();
                        DataTable dt = BusinessQueryService.Save(filedParameters);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int count = dt.Rows.Count;
                            for (int i = 0; i < count; i++)
                            {
                                Guid id = dt.Rows[i].Field<Guid>(OperationIDFieldName);
                                DateTime? updateDate = dt.Rows[i].Field<DateTime?>("UpdateDate");
                                DataRow row = GetDataRowByOperationId(id);
                                if (!string.IsNullOrEmpty(updateDate.ToString()))
                                {
                                    row["UpdateDate"] = updateDate;
                                }
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 保存事件以后发送邮件
        /// </summary>
        public void SaveEventSod()
        {
            if (FocusedDataRow == null) return;
            if (FocusedDataRow.Table.Columns.Contains("SOD") == false) return;
            if (bool.Parse(FocusedDataRow["SOD"].ToString())) return;
            ServiceClient.GetClientService<WorkItem>().Commands["Command_MainSOCustomerServiceSOD"].Execute();
        }
        /// <summary>
        /// 保存ANRC事件并修改当前记录在Trackings表里面的状态
        /// </summary>
        public void SaveEventAnrc()
        {
            var eventObjects = new EventObjects
            {
                OperationID = OperationID,
                OperationType = OperationType.OceanImport,
                Code = "ANRC",
                Id = Guid.Empty,
                FormID = OperationID,
                FormType = FormType.Unknown,
                IsShowAgent = false,
                IsShowCustomer = true,
                Subject = "已收到承运人到港通知书",
                Description = "Have received carrier 's arrival notice",
                Priority = MemoPriority.Normal,
                Type = MemoType.EmailLog,
                UpdateDate = DateTime.Now,
                UpdateBy = LocalData.UserInfo.LoginID
            };
            ServiceClient.GetService<IFCMCommonService>().SaveMemoInfo(eventObjects);
            //添加完事件后修改Trackings状态
            var business = new List<BusinessSaveParameter>();
            var dictionary = new Dictionary<string, object>
                            {
                                {OperationIDFieldName, OperationID},
                                {"ANRC","1"},
                                {OperationTypeFieldName,OperationType}
                            };
            var businessSave = new BusinessSaveParameter { items = dictionary };
            business.Add(businessSave);
            BusinessQueryService.Save(business);
            //执行刷新列表
            ServiceClient.GetClientService<WorkItem>().Commands["Command_ListRefresh"].Execute();
        }

        /// <summary>
        /// 构造修改参数
        /// </summary>
        /// <returns></returns>
        private List<BusinessSaveParameter> GetUpdateFiledNameParameter()
        {
            List<BusinessSaveParameter> parameters = new List<BusinessSaveParameter>();
            foreach (int rowHandle in listDicEditedCells.Keys)
            {
                BusinessSaveParameter parameter = new BusinessSaveParameter();
                parameter[OperationIDFieldName] = _row.Field<Guid>(OperationIDFieldName);
                // parameter["OperationType"] = operationType;
                if (!string.IsNullOrEmpty(_row.Field<DateTime?>("UpdateDate").ToString()))
                {
                    parameter["UpdateDate"] = _row.Field<DateTime?>("UpdateDate");
                }
                parameter[OperationTypeFieldName] = OperationType;
                foreach (string fieldName in listDicEditedCells[rowHandle])
                {
                    parameter[fieldName] = _row[fieldName];
                }
                parameters.Add(parameter);
            }
            return parameters;
        }

        private void AfterSaveDocument()
        {
            //EndShowAnimation();
            AcceptChanges();
            ResetDocumentRecord();
        }
        int tokenID = -1;
        public override void BeginShowAnimation(string tip)
        {
            if (IsShowLoadingForm)
                tokenID = LoadingServce.ShowLoadingForm(gridControlList, tip);
        }
        public override void EndShowAnimation()
        {
            if (IsShowLoadingForm)
                LoadingServce.CloseLoadingForm(tokenID);
            //取消界面的置灰--界面置灰在ICP.TaskCenter.UI.MainWorkSpace实现(任务中心单独执行)
            if (TemplateCode.Contains("TaskCenter"))
            {
                ServiceClient.GetClientService<WorkItem>().Commands["Command_CancelPuttheash"].Execute();
            }
        }
        /// <summary>
        /// 保存附件
        /// </summary>
        /// <returns></returns>
        private bool SaveAttachment()
        {
            bool IsSaveAttachment = false;
            if (listDicAddedDocuments.Count > 0 || listDicDeletedDocuments.Count > 0)
            {
                List<string> listDeletedAttachment = new List<string>();
                List<Guid> listOperationIds = new List<Guid>();
                foreach (Guid operationId in listDicDeletedDocuments.Keys)
                {
                    foreach (string attachmentName in listDicDeletedDocuments[operationId])
                    {
                        listDeletedAttachment.Add(attachmentName);
                        listOperationIds.Add(operationId);
                        AddItemToOperationIdsList(operationId);
                    }
                }
                try
                {
                    ClientFileService.Save(listAddedDocuments, listDeletedAttachment, listOperationIds);
                    IsSaveAttachment = true;
                }
                catch (Exception ex)
                {
                    IsSaveAttachment = false;
                    Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
            }

            return IsSaveAttachment;
        }

        /// <summary>
        /// 记录更改的业务ID
        /// </summary>
        /// <param name="operationID"></param>
        private void AddItemToOperationIdsList(Guid operationID)
        {
            if (!operationIds.Contains(operationID))
            {
                operationIds.Add(operationID);
            }
        }

        private FileCopyParameters CreateFileCopyParametersInfo(Guid[] operationIds, List<DocumentInfo> documents, List<string> deleteFiles, string[] soNos, DateTime?[] updateDates, bool isContainsSONO)
        {
            return new FileCopyParameters() { OperationIDs = operationIds, Documents = documents, DeleteFiles = deleteFiles, SONOs = soNos, UpdateDates = updateDates, HasSONOColumn = isContainsSONO };
        }

        private DocumentInfo CreateDocumentInfo(Guid operationId, string copyFilePath, EnumDocumentType documentType)
        {
            DocumentInfo document = new DocumentInfo();
            document.OriginalPath = copyFilePath;
            document.Id = Guid.NewGuid();
            document.Name = Path.GetFileName(copyFilePath);
            document.FileSources = FileSource.FDocument;
            document.DocumentType = documentType;
            document.OperationID = operationId;
            document.CreateBy = LocalData.UserInfo.LoginID;
            document.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            document.FormType = FormType.Booking;

            return document;
        }

        /// <summary>
        /// 清空集合信息
        /// </summary>
        private void ResetDocumentRecord()
        {
            listDicAddedDocuments.Clear();
            listAddedDocuments.Clear();
            listDicDeletedDocuments.Clear();
            operationIds.Clear();
        }

        private void OnDocumentUploadFailed(UploadFailedMessage message)
        {
            EndShowAnimation();
            TrapException(new ApplicationException(message.ErrorMessage));


        }
        private void OnDocumentUploadSucessed(DocumentInfo[] documents)
        {
            AfterSaveDocument();

        }
        /// <summary>
        /// 设置指定单元格的值
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public void SetCellValue(Guid operationId, string fieldName, object value)
        {

            DataRow row = GetDataRowByOperationId(operationId);
            if (row != null && row.Table.Columns.Contains(fieldName))
            {
                row[fieldName] = value;
            }
        }
        /// <summary>
        /// 设置过滤结果数据行指定列的值，并将其余行的IsAssociated列的值设为0
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        public void SetFilterRowCellValue(string filterExpression, string columnName, object value)
        {
            DataTable dt = CurrentDataSource;
            if (dt == null || dt.Rows.Count <= 0 || !dt.Columns.Contains(columnName))
                return;
            DataRow[] isAssociatedTrueRows = dt.Select(string.Format("{0}='{1}'", associatedColumn, "1"));
            if (isAssociatedTrueRows != null && isAssociatedTrueRows.Length > 0)
            {
                int length = isAssociatedTrueRows.Length;
                for (int index = 0; index < length; index++)
                {
                    isAssociatedTrueRows[index][associatedColumn] = "0";
                }
            }
            DataRow[] rows = dt.Select(filterExpression);
            if (rows == null || rows.Length <= 0)
                return;
            int lenth = rows.Length;
            for (int i = 0; i < lenth; i++)
            {
                rows[i][columnName] = value;
            }
            AddSortColumnInfoByAssociated();
        }
        /// <summary>
        /// 处理异常捕获
        /// </summary>
        /// <param name="ex"></param>
        public void TrapException(Exception ex)
        {
            LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
        }

        #region 邮件中心-根据关键字显示数据，并且高亮度显示关键字
        private BarEditItem barEditItem = null;
        private BarEditItem GetBarEditSearchItem()
        {
            if (barEditItem != null)
                return barEditItem;
            string businessPartToolbarUISiteName = TemplateCode;
            BarEditItem item = RootWorkItem.UIExtensionSites[businessPartToolbarUISiteName].First(barItem => ((BarItem)barItem).Name == "txtQuery") as BarEditItem;
            barEditItem = item;
            return item;
        }
        public string QueryNo
        {
            get
            {
                if (RootWorkItem.State["IsEnterKeySearch"] == null)
                {
                    return GetQueryString();
                }
                else
                {
                    bool isEnterKeySearch = false;
                    bool.TryParse(RootWorkItem.State["IsEnterKeySearch"].ToString(), out isEnterKeySearch);
                    if (isEnterKeySearch) //表示使用Enter键搜索
                    {
                        return RootWorkItem.State["QueryString"] == null ? "" : RootWorkItem.State["QueryString"].ToString().Trim();
                    }
                    else  //表示点击搜索工具栏按钮
                    {
                        return GetQueryString();
                    }
                }
            }
        }

        private string GetQueryString()
        {
            BarEditItem item = GetBarEditSearchItem();
            return item.EditValue == null ? "" : item.EditValue.ToString().Trim();
        }

        public void EmailQuery()
        {

            using (new CursorHelper(Cursors.WaitCursor))
            {
                string queryNo = QueryNo;
                //如果搜索单号为空，则直接返回
                if (string.IsNullOrEmpty(queryNo))
                    return;

                //如果此次搜索和上次搜索单号不同
                else
                {
                    InnerQuery(queryNo);
                }
            }
        }

        /// <summary>
        /// 邮件中心-从数据源中执行查找匹配的KeyWord
        /// </summary>
        /// <param name="queryNo"></param>
        private void InnerQuery(string queryNo)
        {
            ServerQueryString = "";
            AdvanceQueryString = GetQueryString(queryNo);
            ////数据绑定完成
            //RootWorkItem.State["DataBindingComplete"] = false;
            //如果从本地缓存数据库里没有找到数据，就需要到SQL Server 去查询
            NeedSearchInSQLServer = true;
            //标识是不是高级搜索
            SearchType = SearchActionType.KeyWord;
            IsShowLoadingForm = false;
            QueryData(true);
        }

        //private bool IsNullDataSource()
        //{
        //    return this.CurrentDataSource == null;
        //}  

        public string GetPresentTemplateCode()
        {
            return TemplateCode;
        }
        private string GetFilterExpression()
        {
            List<string> filterColumnNames = AllColumns();
            if (filterColumnNames == null || filterColumnNames.Count <= 0)
                return "1=1";
            return GetExpression(filterColumnNames, QueryNo, false);
        }

        /// <summary>
        /// 业务面板查找列集合
        /// </summary>
        /// <returns></returns>
        public List<string> AllColumns()
        {
            List<string> _AllColumns = new List<string>();
            List<BusinessColumnInfo> list = GetColumnInfos(GetPresentTemplateCode());
            if (list == null || list.Count == 0)
                return null;
            foreach (BusinessColumnInfo column in list)
            {
                if (column.EditType == ColumnEditType.Memo || column.EditType == ColumnEditType.Text)
                {
                    _AllColumns.Add(column.FieldName);
                }
            }
            return _AllColumns;
        }

        private string GetExpression(List<string> list, string name, bool isValueList)
        {
            if (list == null || list.Count <= 0)
                return string.Empty;
            List<string> result = new List<string>();
            foreach (string item in list)
            {
                if (isValueList)
                {
                    result.Add(string.Format(" [{0}] like '%{1}%' ", name, item));
                }
                else
                {
                    result.Add(string.Format(" [{0}] like '%{1}%' ", item, name));
                }

            }
            return result.Aggregate((condition1, condition2) => condition1 + " Or " + condition2);
        }
        #endregion



        #endregion

        #region  事件处理
        /// <summary>
        /// 单元格点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (LocalData.ApplicationType == ApplicationType.ICP)
            {

                #region  判断当前是选择操作多行还是操作当前行
                if (e.Column.FieldName == "Selected")
                {
                    CheckSelect = true;
                    bool checkbox = bool.Parse(FocusedDataRow["Selected"].ToString());
                    CheckBoxState(!checkbox);
                }
                else if (CheckSelect)
                {
                    CheckSelect = false;
                }
                #endregion


                #region 判断当前选中行的数据是否存在拖车和报关

                if (RowIsTruck && RowIsCustoms)
                {
                    if (e.Column.Caption.ToUpper().Contains("TRKA") || e.Column.Caption.ToUpper().Contains("TRKD"))
                    {
                        if (bool.Parse(CurrentRow["IsTruck"].ToString()) == false) return;
                    }
                    if (e.Column.Caption.ToUpper().Contains("CFA") || e.Column.Caption.ToUpper().Contains("CFD"))
                    {
                        if (bool.Parse(CurrentRow["IsCustoms"].ToString()) == false) return;
                    }
                }
                #endregion

                ServiceClient.GetClientService<WorkItem>().State["CurrentBaseBusinessPart"] = this;
                if (e.Column != null && CurrentDataSource.Rows.Count > 0)
                {
                    BusinessState records = ReturnBusinessState(e.Column.FieldName);
                    if (records == null) return;
                    #region 当前点击的单元格，用户是否有权限操作
                    string[] permissions = records.Permissions.Split(',');
                    bool permissionsflg = false;
                    for (int i = 0; i < permissions.Count(); i++)
                    {
                        if (permissionsflg == false)
                        {
                            var role = LocalCommonServices.PermissionService.RoleList.FirstOrDefault(n => n.CName == permissions[i].ToString());
                            if (role != null)
                            {
                                permissionsflg = true;
                            }
                        }
                    }
                    //当前用户不具备操作当前单元格的权限 直接返回
                    if (permissionsflg == false) return;
                    #endregion

                    #region  SOPV状态的勾选判断
                    if (records.Name.ToUpper() == "SOPV")
                    {
                        if (OeOrderState == OEOrderState.CancelBooking)
                        {
                            DialogResult dlg =
                            XtraMessageBox.Show(
                            LocalData.IsEnglish ? "Are you sure you restore the booking?" : "您确认恢复订舱吗?",
                            LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question);
                            if (dlg != DialogResult.OK) return;
                            //执行恢复订舱
                            RootWorkItem.Commands["Command_RestoreBooking"].Execute();
                        }
                    }
                    #endregion

                    var memoList =
                        fcmCommonService.GetMemoList(OperationID, null).FirstOrDefault(n => n.Code == records.Name);
                    if (!string.IsNullOrEmpty(records.ModifyValue))
                    {
                        if (ExceptionHandling(memoList, records))
                        {
                            return;
                        }
                    }
                    if (memoList != null && (memoList.Type == MemoType.Memo || memoList.Type == MemoType.Manually) && memoList.Logged)
                    {
                        DialogResult dlg =
                        XtraMessageBox.Show(
                        LocalData.IsEnglish ? "Do you want to removed the selected state of[" + records.Name + "]?" : "您确定清除勾选[" + records.Name + "]的状态吗？",
                        LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question);
                        if (dlg != DialogResult.OK) return;
                        if (records.Name == "HD")
                        {
                            EventObjects = new EventObjects
                            {
                                Id = Guid.Empty,
                                OccurrenceTime = DateTime.Now,
                                OperationType = OperationType,
                                FormID = OperationID,
                                OperationID = OperationID,
                                Description = "Cancel Hold delivery, O/S cancelled TLX HBL in ICP on" + DateTime.Now.ToShortDateString(),
                                Subject = "Cancel Hold Delivery/Pick-Up No",
                                ModifyValue = records.ModifyValue,
                                UpdateBy = LocalData.UserInfo.LoginID,
                                UpdateDate = DateTime.Now,
                               
                            };
                            fcmCommonService.SaveMemoInfo(EventObjects);
                            RootWorkItem.Commands["Command_ListRefresh"].Execute();
                        }
      
                        DeleteEvent(records);
                    }
                    else
                    {
                        //点击时是否需要出现提示信息
                        if (!string.IsNullOrEmpty(records.CaptionCn) || !string.IsNullOrEmpty(records.CaptionEn))
                        {
                            DialogResult dlg =
                            XtraMessageBox.Show(
                                LocalData.IsEnglish ? records.CaptionEn : records.CaptionCn,
                                LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question);
                            if (dlg != DialogResult.OK) return;
                            AddEvent(records);
                        }
                        else
                        {
                            AddEvent(records);
                        }
                    }
                    BusinessOperationContext = null;
                }
            }
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="records">实体对象</param>
        public void DeleteEvent(BusinessState records)
        {

            if (string.IsNullOrEmpty(records.ModifyValue)) return;
            List<string> eventName = new List<string>();
            var businesss = new List<BusinessSaveParameter>();
            var dictionary = new Dictionary<string, object>
                                {
                                    {records.Name, records.OriginalValue},
                                    {OperationIDFieldName, OperationID},
                                    {"OperationType",OperationType}
                                };
            eventName.Add(records.Name);
            if (!string.IsNullOrEmpty(records.AssociatedEvent))
            {
                BusinessState business = ReturnBusinessState(records.AssociatedEvent);
                dictionary.Add(business.Name, business.OriginalValue);
                var memoList =
                      fcmCommonService.GetMemoList(OperationID, null).FirstOrDefault(n => n.Code == business.Name && n.Logged == true);
                if (memoList != null)
                {
                    eventName.Add(business.Name);
                }
            }
            var businessSaves = new BusinessSaveParameter { items = dictionary };
            businesss.Add(businessSaves);
            if (BusinessQueryService.Save(businesss).Rows.Count > 0)
            {
                if (RomoveMemoInfo(eventName))
                {
                    RootWorkItem.Commands["Command_ListRefresh"].Execute();
                }
            }
            Operationlog("删除事件:" + "     单号:" + OperationNo + "      代码:" + records.Name + "         修改值:" + records.OriginalValue + "          修改人:" + LocalData.UserInfo.LoginName, "任务中心");
        }
        /// <summary>
        /// 移除生成的事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public bool RomoveMemoInfo(List<string> eventName)
        {
            bool memoInfoFlg = false;
            foreach (var e in eventName)
            {
                DataTable dt = ServiceClient.GetService<IFCMCommonService>()
                                             .GetOperationMemosID(OperationID, e);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow[] temp = CurrentDataSource.Select(OperationIDFieldName + "='" + OperationID + "'");
                    DateTime? dateTime = string.IsNullOrEmpty(temp[0]["UpdateDate"].ToString()) ? (DateTime?)null : Convert.ToDateTime(temp[0]["UpdateDate"].ToString());
                    Guid id = new Guid(dt.Rows[0]["ID"].ToString());
                    ServiceClient.GetService<IFCMCommonService>()
                                 .RemoveMemoInfo(id, LocalData.UserInfo.LoginID, dateTime);
                    memoInfoFlg = true;
                    Operationlog("执行删除:" + "     单号:" + OperationNo + "     代码:" + e + "         修改人:" + LocalData.UserInfo.LoginName, "任务中心");
                }
                else
                {
                    memoInfoFlg = false;
                }
            }
            return memoInfoFlg;
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="records">实体对象</param>
        public void AddEvent(BusinessState records)
        {
            bool perform = true;
            if (string.IsNullOrEmpty(records.Methods))
            {
                //判断当前事件是否已经生成
                EventObjects = new EventObjects
                {
                    Code = records.Name,
                    OccurrenceTime = DateTime.Now,
                    OperationType = OperationType,
                    FormID = OperationID,
                    OperationID = OperationID,
                    ModifyValue = records.ModifyValue
                };
                DataRow[] temp = CurrentDataSource.Select(OperationIDFieldName + "='" + OperationID + "'");
                string code = EventObjects == null ? records.Name : EventObjects.Code;
                //根据字段的类型判断当前事件是新增还是取消
                switch (records.SqlType)
                {
                    case "bit":
                        perform = Convert.ToBoolean(temp[0][code]);
                        break;
                    case "tinyint":
                        if (int.Parse(temp[0][code].ToString()) == 1 ||
                            int.Parse(temp[0][code].ToString()) == 2)
                        {
                            perform = false;
                        }
                        break;
                    default:
                        break;
                }
            }
            //单元格是否存在方法
            if (!string.IsNullOrEmpty(records.Methods))
            {
                IsShowLoadingForm = true;
                Submiting = true;
                ServiceClient.GetClientService<WorkItem>().Commands["Command_DisableTaskCenter"].Execute();
                RootWorkItem.Commands[records.Methods].Execute();
                ServiceClient.GetClientService<WorkItem>().Commands["Command_EnableTaskCenter"].Execute();
                return;
            }
            if (perform == false)
            {
                RootWorkItem.Commands["Command_OpenEvent"].Execute();
            }
            Operationlog("添加事件:" + "     单号:" + OperationNo + "     代码:" + records.Name + "     修改值:" + records.ModifyValue + "         修改人:" + LocalData.UserInfo.LoginName, "任务中心");
        }

        /// <summary>
        /// 返回可以修改的事件实体
        /// </summary>
        /// <param name="caption">事件名称</param>
        /// <returns></returns>
        public BusinessState ReturnBusinessState(string caption)
        {
            List<BusinessState> businessStates = null;
            BusinessStateReadXml businessStateReadXml = new BusinessStateReadXml();
            //接收可手动修改的字段信息 根据业务类型过滤
            if (OperationType != null)
            {
                businessStates = businessStateReadXml.GetBusinessState().Where(n => n.OperationType == OperationType).ToList();
            }
            //当前单元格是否可以编辑
            BusinessState records =
                (from a in businessStates where a.Name.ToUpper() == caption.ToUpper() select a)
                    .FirstOrDefault();
            return records;
        }

        /// <summary>
        /// 处理当前事件存在，勾选不正确
        /// </summary>
        /// <param name="eventObjectses"></param>
        /// <param name="records"></param>
        /// <returns>如果事件存在，勾选不正确，直接勾上状态，不产生事件信息</returns>
        public bool ExceptionHandling(EventObjects eventObjectses, BusinessState records)
        {
            bool flg = false;
            if (eventObjectses != null && records != null)
            {
                if (eventObjectses.Logged == false) return flg;
                DataRow[] temp = CurrentDataSource.Select(OperationIDFieldName + "='" + OperationID + "'");
                if (temp.Any())
                {
                    bool perform = true;
                    //根据字段的类型判断当前事件是新增还是取消
                    switch (records.SqlType)
                    {
                        case "bit":
                            perform = Convert.ToBoolean(temp[0][records.Name]);
                            break;
                        case "tinyint":
                            if (int.Parse(temp[0][records.Name].ToString()) == 1 ||
                                int.Parse(temp[0][records.Name].ToString()) == 2)
                            {
                                perform = false;
                            }
                            break;
                        default:
                            break;
                    }
                    if (perform == false)
                    {
                        var business = new List<BusinessSaveParameter>();
                        var dictionary = new Dictionary<string, object>
                            {
                                {"OceanBookingID", OperationID},
                                {records.Name,records.ModifyValue},
                                {"OperationType",OperationType}
                            };
                        var businessSave = new BusinessSaveParameter { items = dictionary };
                        business.Add(businessSave);
                        BusinessQueryService.Save(business);
                        //执行完过程后刷新记录
                        RootWorkItem.Commands["Command_ListRefresh"].Execute();
                        flg = true;
                        Operationlog("事件存在，状态未改:" + "     单号:" + OperationNo + "     代码:" + records.Name + "     修改值:" + records.ModifyValue + "         修改人:" + LocalData.UserInfo.LoginName, "任务中心");
                    }
                }
            }
            return flg;
        }


        #endregion

        #region 列表操作

        private object preObj = null;
        /// <summary>
        /// 列表行双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewList_RowClick(object sender, RowClickEventArgs e)
        {
            gridViewList.OptionsSelection.EnableAppearanceFocusedRow = true;

            previousFocusedRowHandle = e.RowHandle;
            if (e.Clicks == 2 && e.Button == MouseButtons.Left)
            {
                RootWorkItem.State["CurrentBaseBusinessPart"] = this;

                switch (OperationType)
                {
                    case OperationType.OceanExport:
                        RootWorkItem.Commands["Command_ForSoEdit"].Execute();
                        break;
                    case OperationType.OceanImport:
                        RootWorkItem.Commands["OceanImport_Edit"].Execute();
                        break;
                    case OperationType.AirExport:
                        RootWorkItem.Commands["AirExport_EditData"].Execute();
                        break;
                    case OperationType.AirImport:
                        RootWorkItem.Commands["AirImport_EditBooking"].Execute();
                        break;
                    case OperationType.Other:
                        if (ExpandOperationType == ExpandOperationType.ECommerce)
                        {
                            RootWorkItem.Commands["Command_Other_ECommerce_EditData"].Execute();
                        }
                        else
                        {
                            RootWorkItem.Commands["OtherBusiness_EditData"].Execute();
                        }
                        break;
                }
            }

            if (gridViewList.GetRowCellValue(e.RowHandle, "SOCCD") != preObj)
            {
                preObj = gridViewList.GetRowCellValue(e.RowHandle, "SOCCD");
                //Un-Confirm按钮
                if (ToolbarManager.barItems["Un-Confirm"] is BarButtonItem)
                {
                    BarButtonItem btn = ToolbarManager.barItems["Un-Confirm"] as BarButtonItem;
                    if (Convert.ToBoolean(preObj))
                        btn.Visibility = BarItemVisibility.Always;
                    else
                        btn.Visibility = BarItemVisibility.Never;
                }
            }
        }

        /// <summary>
        /// 重绘单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewList_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (LocalData.ApplicationType == ApplicationType.ICP)
            {
                DataRow row = gridViewList.GetDataRow(e.RowHandle) as DataRow;
                if (row == null) return;
                if (row.Table.Columns.Contains("IsTruck"))
                {
                    if (row.Field<bool>("IsTruck") == false)
                    {
                        if (e.Column.FieldName == "TRKA")
                        {
                            e.RepositoryItem = emptyEditor;
                        }
                        if (e.Column.FieldName.ToUpper() == "TRKD")
                        {
                            e.RepositoryItem = emptyEditor;
                        }
                        if (e.Column.FieldName.ToUpper() == "TRKP")
                        {
                            e.RepositoryItem = emptyEditor;
                        }
                    }
                }
                if (row.Table.Columns.Contains("IsCustoms"))
                {
                    if (row.Field<bool>("IsCustoms") == false)
                    {
                        if (e.Column.FieldName == "CFA")
                        {
                            e.RepositoryItem = emptyEditor;
                        }
                        if (e.Column.FieldName == "CFD")
                        {
                            e.RepositoryItem = emptyEditor;
                        }
                        if (e.Column.FieldName == "CFC")
                        {
                            e.RepositoryItem = emptyEditor;
                        }
                    }

                }

                #region  针对放货节点操作

                if (row.Table.Columns.Contains("SQLReceiveed"))
                {
                    if (int.Parse(row["SQLReceiveed"].ToString()) == 4)
                    {
                        if (e.Column.FieldName == "Receiveed")
                        {
                            e.RepositoryItem = emptyEditor;
                        }
                    }
                }
                if (row.Table.Columns.Contains("SQLPaid"))
                {
                    if (int.Parse(row["SQLPaid"].ToString()) == 4)
                    {
                        if (e.Column.FieldName == "Paid")
                        {
                            e.RepositoryItem = emptyEditor;
                        }
                    }
                }
                if (row.Table.Columns.Contains("SQLRCReceiveedState"))
                {
                    if (int.Parse(row["SQLRCReceiveedState"].ToString()) == 4)
                    {
                        if (e.Column.FieldName == "RCReceiveed")
                        {
                            e.RepositoryItem = emptyEditor;
                        }
                    }
                }
                #endregion
            }

        }
        #endregion

        #region  处理列表按F5查询

        private void gridViewList_KeyDown(object sender, KeyEventArgs e)
        {
            if (
                e.KeyCode == Keys.F5
                && gridViewList.FocusedColumn != null
                && gridViewList.FocusedValue != null)
            {
                F5Query();
            }
        }

        /// <summary>
        /// F5  查询
        /// </summary>
        public override void F5Query()
        {
            string text = gridViewList.GetFocusedDisplayText().Replace("'", "''");

            string companyId = string.Empty;
            var copany =
                LocalData.UserInfo.UserOrganizationList.Where(o => o.Type == LocalOrganizationType.Company).ToList();
            if (copany.Any() && copany.Count > 1)
            {
                foreach (var c in copany)
                {
                    if (string.IsNullOrEmpty(companyId))
                    {
                        companyId = "'" + c.ID + "'";
                    }
                    else
                    {
                        companyId = companyId + "," + "'" + c.ID + "'";
                    }

                }
            }
            else
            {
                companyId = copany[0].ID.ToString();
            }

            string strCompany = "";
            if (companyId.Contains(",") == false)
            {
                strCompany = " and $@CompanyID@ = '" + companyId + "'";
            }
            else
            {
                strCompany = " and $@CompanyID@ in (" + companyId + ")";
            }
            if (text.ToLower().Contains("checked")) return;
            //查询逻辑更改：为客户时与文本框录入回车查询一致
            if (gridViewList.FocusedColumn.FieldName.Equals("Customer"))
                WorkItem.RootWorkItem.State["AdventQueryString"] = text;
            else
                WorkItem.RootWorkItem.State["AdventQueryString"] = string.Format("$@IsValid@ = 1 and $@{0}@ = '{1}' {2} ", gridViewList.FocusedColumn.FieldName, text, strCompany);
            WorkItem.Commands["Command_F5_Search"].Execute();
            previousFocusedRowHandle = -1;
            //设置查询以后 选择是第一行数据
            gridViewList.FocusedRowHandle = 0;
        }

        #endregion

        #region   选择行CheckBox选择
        /// <summary>
        /// 选择当前的CheckBox为选中状态
        /// </summary>
        public void CheckBoxState(bool flg)
        {

            DataTable dt = RootWorkItem.State["BaseDataSource"] as DataTable;
            if (dt.Columns.Contains("Selected") == false) return;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["OceanBookingID"] != null)
                {
                    Guid bookingId = new Guid(dt.Rows[i]["OceanBookingID"].ToString());
                    if (bookingId == OperationID)
                    {
                        dt.Rows[i]["Selected"] = flg;
                    }
                }
            }
            //取消表头的CheckBox的选择状态
            m_checkStatus = false;
            if (checkColumnHeaderCustomDrawEventArgs != null)
            {
                using (Graphics graphics = CreateGraphics())
                {
                    checkColumnHeaderCustomDrawEventArgs =
                        new ColumnHeaderCustomDrawEventArgs(new GraphicsCache(graphics),
                                                            checkColumnHeaderCustomDrawEventArgs.Painter as
                                                            HeaderObjectPainter,
                                                            checkColumnHeaderCustomDrawEventArgs.Info);
                    DevHelper.DrawCheckBox(checkColumnHeaderCustomDrawEventArgs, m_checkStatus);
                }
            }
        }
        #endregion

        #region 任务中心文本框定位查询
        /// <summary>
        /// 任务中心文本框定位查询
        /// </summary>
        public bool TaskCenterPositioning(string queryString)
        {
            bool queryFlg = false;
            if (CurrentDataSource != null && CurrentDataSource.Rows.Count > 0)
            {
                queryString = queryString.Replace("$", string.Empty).Replace("@", string.Empty);
                DataRow[] dataRows = CurrentDataSource.Select(queryString);
                if (dataRows.Count() == 1)
                {
                    queryFlg = true;
                    //得到当前查询数据在DataTable行号
                    int bindingIndex = Array.IndexOf(CurrentDataSource.Select(), dataRows[0]);
                    //得到数据在GridView行号
                    int rowHandle = gridViewList.GetRowHandle(bindingIndex);
                    //选择当前数据行
                    gridViewList.FocusedRowHandle = rowHandle;
                }
            }
            return queryFlg;
        }
        #endregion

        #region  邮件中心使用的方法
        /// <summary>
        /// 邮件中心高级查询的方法
        /// </summary>
        public void EmailAdvanceQuery()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Dictionary<string, object> initValues = new Dictionary<string, object>();
                initValues.Add(Constants.BusinessTypeKey, BusinessType);
                initValues.Add("TemplateCode", TemplateCode);
                if (TemplateCode == ListFormType.MailLink4CarrierAN.ToString())
                {
                    initValues.Add(Constants.ShowOperationType, 0);
                }
                frmAdvanceQuery frmQuery = RootWorkItem.Items.AddNew<frmAdvanceQuery>();
                frmQuery.Init(initValues);
                frmQuery.MaximizeBox = frmQuery.MinimizeBox = false;
                frmQuery.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                frmQuery.AdvanceQuery += OnAdvanceQuery;
                frmQuery.Show(this);
            }
        }
        /// <summary>
        /// 高级查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="queryInfo"></param>
        private void OnAdvanceQuery(object sender, CommonEventArgs<QueryInfo> queryInfo)
        {
            AdvanceQueryString = queryInfo.Data.AdvanceQueryString;
            ServerQueryString = AdvanceQueryString;
            //区分是否要连接本地缓存数据库还是数据库,1表示连接数据库，0表示连接本地缓存数据库
            SearchType = SearchActionType.Advance;
            RootWorkItem.State["OperationType"] = (int)queryInfo.Data.OperationType;
            NeedSearchInSQLServer = false;
            IsShowLoadingForm = true;
            QueryData(true);
        }

        /// <summary>
        /// 下拉多选业务所属公司
        /// <remarks>加载时默认为所有公司</remarks>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        public void EmailSelectCompany()
        {
            if (TemplateCode.Equals(ListFormType.MailLink4Carrier.ToString()))
                return;
            object selectedCompanyIds = RootWorkItem.State["BarEditItemChangedValue"];
            SelectedCompanyIds = selectedCompanyIds == null ? null :
            selectedCompanyIds.ToString();
            IsShowLoadingForm = false;
            QueryData(false);
        }
        #endregion

        #region 任务中心使用的方法
        /// <summary>
        /// 通过包含名称查找工具栏并设置是否可用
        /// </summary>
        /// <param name="name">包含的名称</param>
        /// <param name="enable">是否可用</param>
        /// <returns></returns>
        public void SetBarItemEnable(string[] name, bool enable)
        {
            foreach (BarItem bi in barManager.Items)
            {
                //隐藏
                if (name.Contains(bi.Name) && enable)
                {
                    bi.Visibility = BarItemVisibility.Never;
                }
                //显示
                else if (name.Contains(bi.Name) && enable == false)
                {
                    bi.Visibility = BarItemVisibility.Always;
                }
            }
        }

        /// <summary>
        /// 控制按钮是否有效
        /// </summary>
        /// <param name="columns">需要判断的条件</param>
        /// <param name="buttonname">按钮的名称</param>
        /// <param name="value">是否直接置灰</param>
        public void ControlButton(string columns, string buttonname, bool? value)
        {
            DataRow dr = FocusedDataRow;
            if (dr != null)
            {
                if (dr.Table.Columns.Contains(columns))
                {
                    if (dr.Field<bool>(columns) || value == true)
                    {
                        SetBarItemEnable(new string[] { buttonname }, true);
                    }
                    else
                    {
                        SetBarItemEnable(new string[] { buttonname }, false);
                    }
                }
            }
        }

        /// <summary>
        /// 针对海进业务的取消按钮的方法
        /// </summary>
        public void OceanImportButtonisenabled()
        {
            if (FocusedDataRow != null)
            {
                SetBarItemEnable(new string[] 
                 { "OceanImportAgreeRC",
                    "OceanImportCancelAgreeRC", 
                    "OceanImportApplyReleaseCargo", 
                    "OceanImportCancelApplyRC", 
                    "OceanImportDelivery", 
                    "OceanImportCancelDelivery" }, true);
                //同意放货
                if (FocusedDataRow.Table.Columns.Contains("ARC"))
                {
                    if (FocusedDataRow.Field<bool>("ARC") == false)
                    {
                        SetBarItemEnable(new string[] { "OceanImportAgreeRC" }, false);
                    }
                    else
                    {
                        SetBarItemEnable(new string[] { "OceanImportCancelAgreeRC" }, false);
                    }
                }
                //已申请放货
                if (FocusedDataRow.Table.Columns.Contains("RCA"))
                {
                    if (FocusedDataRow.Field<bool>("RCA") == false)
                    {
                        SetBarItemEnable(new string[] { "OceanImportApplyReleaseCargo" }, false);
                    }
                    else
                    {
                        SetBarItemEnable(new string[] { "OceanImportCancelApplyRC" }, false);
                    }
                }
                //放货
                if (FocusedDataRow.Table.Columns.Contains("RC"))
                {
                    if (FocusedDataRow.Field<bool>("RC") == false)
                    {
                        SetBarItemEnable(new string[] { "OceanImportDelivery" }, false);
                    }
                    else
                    {
                        SetBarItemEnable(new string[] { "OceanImportCancelDelivery" }, false);
                    }
                }
            }
        }


        /// <summary>
        /// 针对海出业务的取消按钮的方法
        /// </summary>
        public void OceanExportButtonisenabled()
        {
            if (FocusedDataRow != null)
            {
                if (FocusedDataRow.Table.Columns.Contains("IsValid"))
                {
                    bool IsValid = FocusedDataRow.Field<bool>("IsValid");
                    string name = "ForSo_Cancel";
                    foreach (BarItem bi in barManager.Items)
                    {
                        if (name == bi.Name && IsValid == false)
                        {
                            bi.Caption = LocalData.IsEnglish ? "Restore" : "恢复";
                        }
                        else if (name == bi.Name && IsValid == true)
                        {
                            bi.Caption = LocalData.IsEnglish ? "Cancel" : "取消";
                        }
                    }
                }
            }
        }

        protected string OperationNoFieldName = Constants.OperationNOFieldName;
        /// <summary>
        /// 数据加载完后，选择行加载Tab信息
        /// </summary>
        public void LocatePreviousFocusedRow()
        {
            //判断上次记录是否存在于数据集合中
            bool rowHandle = false;
            //判断当前记录是否存在
            if (RootWorkItem.State["BaseDataSource"] != null && OperationID != Guid.Empty)
            {
                DataTable dt = RootWorkItem.State["BaseDataSource"] as DataTable;
                if (dt.Columns.Count == 0)
                    return;

                DataRow[] temp = dt.Select(OperationIDFieldName + "='" + OperationID + "'");
                if (temp == null)
                {
                    gridViewList.FocusedRowHandle = 0;
                    gridViewList.SelectCells(previousFocusedRowHandle, gridViewList.Columns[OperationNoFieldName],
                                                  previousFocusedRowHandle, gridViewList.Columns[OperationNoFieldName]);
                    rowHandle = true;
                }
            }
            if (rowHandle == false)
            {
                //选择行
                previousFocusedRowHandle = previousFocusedRowHandle == -1
                                           ? gridViewList.FocusedRowHandle
                                           : previousFocusedRowHandle;

                gridViewList.FocusedRowHandle = previousFocusedRowHandle;
                //刷新后将上次记录行数的SONO背景色改为默认的蓝色
                gridViewList.SelectCells(previousFocusedRowHandle, gridViewList.Columns[OperationNoFieldName],
                                              previousFocusedRowHandle, gridViewList.Columns[OperationNoFieldName]);
            }
            if (FocusedRowChangedHandler != null)
            {
                FocusedRowChangedHandler(this, null);
            }
            ButtonDisplayed();
            OceanImportButtonisenabled();
            OceanExportButtonisenabled();
        }

        /// <summary>
        /// 控制按钮是否显示
        /// </summary>
        public void ButtonDisplayed()
        {
            ControlButton("SOA", "ForSo_ApplySO", null);
        }

        /// <summary>
        /// 移除当前记录
        /// </summary>
        public void DeleteRow()
        {
            CurrentDataSource.Rows.Remove(gridViewList.GetFocusedDataRow());
            ResetBinding();
        }

        /// <summary>
        /// 表头追加ToolTip
        /// </summary>
        /// <param name="str">列名</param>
        /// <param name="toolTipCn">ToolTipCn</param>
        /// <param name="toolTipEn">ToolTipEn</param>
        public void UpdateToolTip(string str, string toolTipCn, string toolTipEn)
        {
            GridColumn col = gridViewList.Columns[str];
            if (col == null)
                return;
            if (string.IsNullOrEmpty(col.ToolTip))
            {
                col.ToolTip = LocalData.IsEnglish ? toolTipEn : toolTipCn;
            }
            else
            {
                string oldToolTip = col.ToolTip;
                string newToolTip = LocalData.IsEnglish ? toolTipEn : toolTipCn;
                col.ToolTip = oldToolTip + Environment.NewLine + newToolTip;
            }
        }

        #endregion

        #region 记录日志
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="msg">操作轨迹</param>
        public void Operationlog(string msg, string name)
        {
            StreamWriter sw = File.AppendText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, name) + ".Log");
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss: ") + msg);
            sw.Close();
        }

        #endregion

        #region  邮件中心-按钮的隐藏和显示
        /// <summary>
        /// 邮件中心按钮是显示还是隐藏
        /// </summary>
        bool according = false;
        /// <summary>
        /// 邮件中心按钮显示和隐藏
        /// </summary>
        public void EmailButtonDisplayed()
        {
            //针对邮件中心的按钮在选择行时 做特殊处理
            if (LocalData.ApplicationType == ApplicationType.EmailCenter)
            {

                //海进时隐藏 关联SO，关联SI 阶段的按钮
                if (OperationType == OperationType.OceanImport)
                {
                    SetBarItemEnable(new string[] { "AssociateNormal",
                                                    "AssociateSO",
                                                    "AssociateSI" 
                                                       }, true);
                    according = true;
                }
                //询价时隐藏高级查找，关联SO，关联SI阶段的按钮
                else if (OperationType == OperationType.InquireRate)
                {
                    SetBarItemEnable(new string[] { "AssociateNormal",
                                                    "AssociateSO",
                                                    "AssociateSI",
                                                    "btnAdvanceQuery"
                                                       }, true);
                    according = true;
                }
                if (OperationType == OperationType.OceanExport && according)
                {
                    according = false;
                    SetBarItemEnable(new string[] { "AssociateNormal",
                                                    "AssociateSO",
                                                    "AssociateSI" 
                                                       }, false);
                }
            }
        }
        #endregion

        #region 面板回调数据刷新
        /// <summary>
        /// 业务实体类
        /// </summary>
        private BusinessQueryCriteria businessQueryCriteria = null;
        /// <summary>
        ///邮件中心回调实现事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMessageSent(object sender, CommonEventArgs<MessageSentParameter> e)
        {
            MessageSentParameter messageSentParameter = e.Data;
            if (CommonOperationService.TemplateCode != TemplateCode || messageSentParameter == null)
            {
                return;
            }
            if (IsBusinessPartDisposed())
            {
                return;
            }
            if (RootWorkItem.State["BaseDataSource"] != null)
            {
                DataTable dataTable = RootWorkItem.State["BaseDataSource"] as DataTable;
                DataRow[] temp = dataTable.Select(OperationIDFieldName + "='" + messageSentParameter.OperationId + "'");
                if (temp.Any() == false)
                {
                    return;
                }
            }
            CallbackRefresh(messageSentParameter.OperationType, messageSentParameter.OperationId, messageSentParameter);
        }



        /// <summary>
        /// 回调刷新业务面板数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnBusinessOperationCompleted(object sender, CommonEventArgs<BusinessOperationParameter> e)
        {
            //base.OnBusinessOperationCompleted(sender, e);
            BusinessOperationParameter businessOperationParameter = e.Data;
            ////针对海进做业务下载时，页面刷新
            //if (businessOperationParameter.Context.OperationID == Guid.Empty)
            //{
            //    base.QueryData(false);
            //}
            //// 邮件中心上传附件以后刷新任务中心
            //if (businessOperationParameter.TemplateCode != ListFormType.MailLink4Unknown.ToString())
            //{
            //    businessQueryCriteria = GetQueryCriteria(true, businessOperationParameter);
            //    if (string.IsNullOrEmpty(businessQueryCriteria.TemplateCode))
            //    {
            //        businessQueryCriteria.TemplateCode = CommonOperationService.GetTemplateCode();
            //    }
            //}
            if (UIHelper.IsCancelOperation())
            {
                RootWorkItem.State["CancelOperation"] = false;
                return;
            }
            string templateCode = TemplateCode;
            if (IsBusinessPartDisposed())
            {
                return;
            }
            CallbackRefresh(businessOperationParameter.Context.OperationType, businessOperationParameter.Context.OperationID, businessOperationParameter);
            //判断当前是否存在正在提交的动画
            if (Submiting)
            {
                EndShowAnimation();
                IsShowLoadingForm = false;
                Submiting = false;
            }

        }



        /// <summary>
        /// 回调刷新列表和TAB页的方法
        /// </summary>
        /// <param name="operationType">业务类型</param>
        /// <param name="operationId">业务ID</param>
        /// <param name="e">对象</param>
        public void CallbackRefresh(OperationType operationType, Guid operationId, object e)
        {
            //WaitCallback callback = (obj) => this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
            //{
            try
            {
                if (businessQueryCriteria == null)
                {
                    businessQueryCriteria = GetQueryCriteria(true, e as BusinessOperationParameter);
                }
                #region  任务中心回调中
                //任务中心回调中 读取对应的配置信息，如果当前构造的CODE和配置文件中的CODE相同，进行更新，不相同返回
                //由于每次在任务中心点击节点都会产生新的NodeInfo的对象，并且保存在内存中
                //所以每次回调的时候会产生不必要错误信息 这样就解决任务中心在回调过程中报错的信息
                if (LocalData.ApplicationType == ApplicationType.ICP)
                {
                    string viewCode = SetViewCode();
                    //节点信息为空直接重新查询数据，保证数据刷新
                    if (string.IsNullOrEmpty(viewCode))
                    {
                        Operationlog("XML无值", "回调记录");
                        base.QueryData(false);
                    }
                    // 确保回调刷新只会被执行一次
                    if (businessQueryCriteria.TemplateCode != viewCode) return;
                }
                #endregion
                Operationlog("开始回调", "回调记录");
                IBusinessQueryServiceGetter getter =
                    BusinessInfoExtractorFactory.GetQueryService(ParameterFullName);

                if (operationId != Guid.Empty)
                {
                    businessQueryCriteria.AdvanceQueryString = string.Format("$@" + OperationIDFieldName + "@='{0}'", operationId);
                }
                object result = getter.SingleQuery(businessQueryCriteria, Parameter);
                DataTable dt = GetInnerTable(result);
                if (dt.Rows.Count > 0)
                {
                    AddCustomColumn(dt);
                }
                RefreshData(dt);
                if (FocusedRowChangedHandler != null)
                {
                    FocusedRowChangedHandler(this, null);
                }
                PostHande(result);
                businessQueryCriteria = null;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
            //});
            //ThreadPool.QueueUserWorkItem(callback, e);
        }

        /// <summary>
        /// 回调时刷新数据
        /// </summary>
        /// <param name="data"></param>
        public void RefreshData(object data)
        {
            Operationlog("刷新数据", "回调记录");
            DataTable result = data as DataTable;
            //提交data是空的话，就直接重新绑定结果
            if (result == null || result.Rows.Count <= 0)
            {
                base.QueryData(false);
                return;
            }
            try
            {
                DataRow row = result.Rows[0];
                Guid operationId = row.Field<Guid>(OperationIDFieldName);
                RemoveCellValueChangeHandler();
                DataTable baseDataSource = RootWorkItem.State["BaseDataSource"] as DataTable;
                DataRow[] temp = baseDataSource.Select(OperationIDFieldName + "='" + operationId + "'");
                //如果当前列表数据源中不含有指定业务Id的业务，则代表新增
                if (temp.Length == 0)
                {
                    CurrentDataSource.ImportRow(row);
                }
                //更新
                else if (temp.Length > 0)
                {
                    DataRow targetRow = temp[0];
                    int columnCount = targetRow.Table.Columns.Count;
                    for (int i = 0; i < columnCount; i++)
                    {
                        string columnName = targetRow.Table.Columns[i].ColumnName;
                        if (result.Columns.Contains(columnName))
                            targetRow[columnName] = row[columnName];
                    }
                }
                //重新绑定数据源
                else
                {
                    base.QueryData(false);
                }
                ResetBinding();
                AddCellValueChangeHandler();
                Operationlog("回调成功", "回调记录");
            }
            catch (Exception ex)
            {
                base.QueryData(false);
                Operationlog("错误记录:" + ex.Message.ToString(), "回调记录");
            }
        }
        public void RemoveCellValueChangeHandler()
        {
            gridViewList.CellValueChanged -= OnGeidViewCellValueChanged;
        }
        public void AddCellValueChangeHandler()
        {
            gridViewList.CellValueChanged += OnGeidViewCellValueChanged;
        }


        #region   获取当前用户的ViewCode
        //读取模版的路径
        private readonly string _fileRootDirectory = Path.Combine(LocalData.MainPath, "BusinessTemplates");
        //Xml的文件名称
        private const string TempalteFileName = "QueryConditions.xml";
        /// <summary>
        /// 获取当前用户的选择节点Code
        /// </summary>
        /// <returns></returns>
        public string SetViewCode()
        {
            string viewCode = string.Empty;
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
                    viewCode = xe.Attributes["ViewCode"].Value;
                }
            }
            return viewCode;
        }
        #endregion


        #endregion


        #region   更多按钮显示
        /// <summary>
        /// 更多按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelMore_Click(object sender, EventArgs e)
        {
            //方法实现在ICP.TaskCenter.UI->ViewListSmartPart里
            WorkItem.Commands["Command_More_Search"].Execute();
            panelMore.Visible = false;
        }

        /// <summary>
        /// 重写方法PanelMoreShow
        /// </summary>
        /// <param name="show">是否显示</param>
        public override void PanelMoreShow(bool show)
        {
            panelMore.Visible = show;
        }

        #endregion

        #region  Comment Code
        ///// <summary>
        ///// 列表行号显示
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void gridViewList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        //{
        //    if (e.Info.IsRowIndicator && e.RowHandle >= 0)
        //    {
        //        e.Info.DisplayText = (e.RowHandle + 1).ToString().Trim();
        //    }
        //}
        #endregion
    }
}
