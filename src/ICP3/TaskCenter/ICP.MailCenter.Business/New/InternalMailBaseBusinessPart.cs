using System;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.MailCenter.Business.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Common.Business.ServiceInterface
{
    /// <summary>
    /// 内部邮件沟通面板
    /// </summary>
    public partial class InternalMailBaseBusinessPart : BaseBusinessPart_New
    {
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
  

        #region 服务

        private WorkItem RootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        private IOceanExportService _OeService;
        public IOceanExportService OeService
        {
            get
            {
                if (_OeService == null)
                    _OeService = ServiceClient.GetService<IOceanExportService>();
                return _OeService;
            }
        }

        private IInquireRatesService _inquireRatesService;
        public IInquireRatesService inquireRatesService
        {
            get
            {
                if (_inquireRatesService == null)
                    _inquireRatesService = ServiceClient.GetService<IInquireRatesService>();
                return _inquireRatesService;
            }
        }


        #endregion

        #region 属性
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
        public InternalMailTemplateData templateData { get; set; }
        /// <summary>
        /// 海运业务信息
        /// </summary>
        public OceanBookingInfo BookingInfo { get; set; }


        #endregion

        private InternalMailTemplateData GetTemplateData(string templateCode, OperationType operationType)
        {
            templateData = InternalMailFileLoader.Current[new SelectionKey() { TemplateCode = templateCode, Type = operationType }];
            return templateData;
        }

        public InternalMailBaseBusinessPart()
        {
            InitializeComponent();
            this.Disposed += delegate { DisposedCompent(); };
        }

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

        public override void Init(object parameter)
        {
            base.Init(parameter);
            ClearControls();
            BuildSpecialDynamicToolBar();
            BuildInPanel();
        }

        private void BuildSpecialDynamicToolBar()
        {
            if (TemplateCode.Equals(ListFormType.MailLink4NewInquireRate.ToString()))
            {
                this.AddToolBarElement(ToolbarManager.CreateOperationToolbarCommandInfo(InquireHistoryName(),
                                                                                        "Command_InquireHistory",
                                                                                        TemplateCode, "",
                                                                                        MenuItemType.Button, "", true));
                this.AddToolBarElement(ToolbarManager.CreateOperationToolbarCommandInfo((IsEnglish ? "OperationNO:" : "业务号:"), "btnOperationNO", TemplateCode, "", MenuItemType.Button, "", true));
                BookingInfo = GetOceanBookingInfo(this.FormID);
                if (BookingInfo == null)
                    return;
                this.AddToolBarElement(ToolbarManager.CreateOperationToolbarCommandInfo(BookingInfo.No, btnDynamic, TemplateCode, "", MenuItemType.TextBox, "", false));
                IsCreateDynamicBarItem = true;
            }
        }

        private string InquireHistoryName()
        {
            return string.Format("{0}{1}", LocalData.UserInfo.LoginName, (LocalData.IsEnglish ? "'s Inquire History" : "的询价历史记录"));
        }

        [CommandHandler("Command_GetInquireHistoryName")]
        public void Command_GetInquireHistoryName(object sender, EventArgs e)
        {
            GetReallyInquireHistoryName();
        }

        private string GetReallyInquireHistoryName()
        {
            int count = 0;
            InquierOceanRatesResult data = null;
            try
            {
                data = inquireRatesService.GetInquireOceanRateList(string.Empty,
                                                                   string.Empty,
                                                                   string.Empty,
                                                                   string.Empty,
                                                                   LocalData.UserInfo.LoginID,
                                                                   true,
                                                                   null,
                                                                   null,
                                                                   LocalData.UserInfo.LoginID);
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }

            if (data == null || data.UnReadCountList != null)
            {
                foreach (var item in data.UnReadCountList)
                {
                    if (item.Type == InquierType.TruckingRates)
                    {
                        count = item.CountOfUnreply;
                    }
                }
            }

            return string.Format("{0}{1}{2}", LocalData.UserInfo.LoginName, (IsEnglish ? "'s Inquire History" : "的询价历史"), (count == 0 ? "" : "(" + count.ToString() + ")"));
        }

        public OceanBookingInfo GetOceanBookingInfo(Guid operationId)
        {
            return OeService.GetOceanBookingInfo(operationId);
        }

        public void BuildInPanel()
        {
            GetTemplateData(TemplateCode, this.OperationType);
            if (templateData == null || templateData.ControlName == null)
            {
                pnlInternalMailRegion.Controls.Clear();
                return;
            }
            ElementPart = InternalMailBusinessPartFactory.CreateInstance(templateData.Assmbly, templateData.ControlName, TemplateCode);
            Control ucInternalMailElementPart = ElementPart as Control;
            ucInternalMailElementPart.Dock = DockStyle.Fill;
            this.pnlInternalMailRegion.Controls.Add(ucInternalMailElementPart);
            ucInternalMailElementPart = null;
        }

        private void ClearControls()
        {
            ElementPart = null;
            this.pnlInternalMailRegion.Controls.Clear();
        }

        /// <summary>
        /// 数据传输
        /// </summary>
        public void TransferData()
        {
            if (ElementPart != null)
            {
                if (this.TemplateCode.Equals(ListFormType.MailLink4NewInquireRate.ToString()) && !IsCreateDynamicBarItem)
                {
                    ToolbarManager.RenameToolBarDynamicElement(OperationID);
                }

                SetDataSource();
                //标识需要限制最小宽度和高度
                RootWorkItem.State["LimitInternalMailSize"] = 1;
                this.RootWorkItem.Commands["Command_InternalMailBusinessPartFixedSize"].Execute();
                //mark:第一次生成 BarItem时，不需要重命名Dynamic BarItem，当用户切换邮件至“MailLink4NewInquireRate”面板时，需要重新去赋值OperationNo
                IsCreateDynamicBarItem = false;
            }
        }



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

        private ElementParams CreateElementParams()
        {
            return new ElementParams(OperationContext, baseBindingSource, templateData.Editable);
        }
    }
}
