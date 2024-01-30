using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.Business.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System.Windows.Forms;

namespace ICP.Operation.Common.UI
{
    /// <summary>
    /// 内部邮件沟通面板
    /// </summary>
    public partial class InternalMailBaseBusinessPart : BaseBusinessPart
    {
        #region Fields & Property & Services
        /// <summary>
        /// 防止线程冲突
        /// </summary>
        public static object synObj = new object();
        /// <summary>
        ///动态的工具栏Element
        /// </summary>
        private const string btnDynamic = "btnDynamic";
        /// <summary>
        /// 标识是否已经构造了动态的工具栏Element
        /// </summary>
        private bool IsCreateDynamicBarItem = false;

        /// <summary>
        /// WorkItem
        /// </summary>
        private WorkItem RootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        /// <summary>
        /// 是否英文
        /// </summary>
        public bool IsEnglish
        {
            get { return LocalData.IsEnglish; }
        }

        /// <summary>
        /// 具体内部邮件的用户控件
        /// </summary>
        public IInternalMailElement ElementPart { get; set; }

        /// <summary>
        /// 内部沟通面板模版配置数据
        /// </summary>
        public MailBusinessPartTemplateData templateData { get; set; }
        /// <summary>
        /// 海运业务信息
        /// </summary>
        public OceanBookingInfo BookingInfo { get; set; } 
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public InternalMailBaseBusinessPart()
        {
            InitializeComponent();
            Disposed += delegate { DisposedCompent(); };
        }
        /// <summary>
        /// 获取模版数据
        /// </summary>
        /// <param name="templateCode">模板代码</param>
        /// <param name="operationType">业务类型</param>
        /// <returns></returns>
        private MailBusinessPartTemplateData GetTemplateData(string templateCode, OperationType operationType)
        {
            templateData = InternalMailFileLoader.Current[new SelectionKey() { TemplateCode = templateCode, Type = operationType }];
            return templateData;
        }

        
        /// <summary>
        /// 释放组件
        /// </summary>
        private void DisposedCompent()
        {
            BookingInfo = null;
            templateData = null;
            ElementPart = null;
            if (RootWorkItem != null)
            {
                RootWorkItem.Items.Remove(this);
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parameter"></param>
        public override void Init(object parameter)
        {
            base.Init(parameter);
            ClearControls();
            BuildSpecialDynamicToolBar();
            BuildInPanel();
        }
        /// <summary>
        /// 添加询价菜单栏和OperationNO菜单栏
        /// </summary>
        private void BuildSpecialDynamicToolBar()
        {
            if (TemplateCode.Equals(ListFormType.MailLink4NewInquireRate.ToString()))
            {
                AddToolBarElement(ToolbarManager.CreateOperationToolbarCommandInfo("btnInquireHistory", InquireHistoryName(),
                                                                                        "Command_InquireHistory",
                                                                                        TemplateCode, "",
                                                                                        MenuItemType.Button, "", true));
                AddToolBarElement(ToolbarManager.CreateOperationToolbarCommandInfo("btnOperationNO", (IsEnglish ? "OperationNO:" : "业务号:"), "btnOperationNO", TemplateCode, "", MenuItemType.Button, "", true));
                OperationToolbarCommand toolbarEntity = ToolbarManager.CreateOperationToolbarCommand(FormID, OperationType, TemplateCode);
                if (toolbarEntity == null)
                    return;
                AddToolBarElement(toolbarEntity);
                IsCreateDynamicBarItem = true;
                toolbarEntity = null;
            }
        }
        /// <summary>
        /// 询价历史(标题)
        /// </summary>
        /// <returns></returns>
        private string InquireHistoryName()
        {
            return string.Format("{0}{1}", LocalData.UserInfo.LoginName, (LocalData.IsEnglish ? "'s Inquire History" : "的询价历史记录"));
        }

        /// <summary>
        /// 构建面板
        /// </summary>
        public void BuildInPanel()
        {
            GetTemplateData(TemplateCode, OperationType);
            if (templateData == null || templateData.ControlName == null)
            {
                pnlInternalMailRegion.Controls.Clear();
                return;
            }
            ElementPart = InternalMailBusinessPartFactory.CreateInstance(templateData.Assmbly, templateData.ControlName, TemplateCode);
            Control ucInternalMailElementPart = ElementPart as Control;
            if (ucInternalMailElementPart == null) return;
            ucInternalMailElementPart.Dock = DockStyle.Fill;
            pnlInternalMailRegion.Controls.Add(ucInternalMailElementPart);
            ucInternalMailElementPart = null;
        }
        /// <summary>
        /// 清空控件
        /// </summary>
        private void ClearControls()
        {
            ElementPart = null;
            pnlInternalMailRegion.Controls.Clear();
        }

        /// <summary>
        /// 数据传输
        /// </summary>
        public void TransferData()
        {
            if (ElementPart != null)
            {
                ToolbarManager.RenameToolBarItem(TemplateCode, OperationID, OperationType, IsCreateDynamicBarItem);
                SetDataSource();
                //标识需要限制最小宽度和高度
                RootWorkItem.State["LimitInternalMailSize"] = 1;
                RootWorkItem.Commands["Command_InternalMailBusinessPartFixedSize"].Execute();
                //mark:第一次生成 BarItem时，不需要重命名Dynamic BarItem，当用户切换邮件至“MailLink4NewInquireRate”面板时，需要重新去赋值OperationNo
                IsCreateDynamicBarItem = false;
            }
        }

        /// <summary>
        /// 设置数据源
        /// </summary>
        private void SetDataSource()
        {
            lock (synObj)
            {
                if (BookingInfo != null)
                    ElementPart.DataBind(new ElementParams(OperationContext, baseBindingSource, templateData.Editable)
                    {
                        Data = BookingInfo.ContainerDescription.ToString()
                    });
                else
                    ElementPart.DataBind(CreateElementParams());
            }
        }
        /// <summary>
        /// 创建内部沟通面板参数
        /// </summary>
        /// <returns></returns>
        private ElementParams CreateElementParams()
        {
            return new ElementParams(OperationContext, baseBindingSource, templateData.Editable);
        }
    }
}
