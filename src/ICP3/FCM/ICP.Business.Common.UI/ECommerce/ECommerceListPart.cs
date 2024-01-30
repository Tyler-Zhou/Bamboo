using DevExpress.XtraBars;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ICP.Business.Common.UI.ECommerce
{
    /// <summary>
    /// 电商列表
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class ECommerceListPart : BaseListEditPart, IDataBind
    {
        #region Member
        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        /// <summary>
        /// 模板编号
        /// </summary>
        public string TemplateCode { get; set; }

        /// <summary>
        /// 业务操作上下文
        /// </summary>
        public BusinessOperationContext CurrentContext { get; set; }

        /// <summary>
        /// 返回当前选择行数据
        /// </summary>
        public override object Current
        {
            get
            {
                return bindingSource.Current;
            }
        }

        /// <summary>
        /// 当前行
        /// </summary>
        public ECommerceList CurrentRow { get { return bindingSource.Current == null ? null : bindingSource.Current as ECommerceList; } }

        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bindingSource.DataSource;
            }
            set
            {
                bindingSource.DataSource = value;
                bindingSource.ResetBindings(false);
            }
        }

        private ConfigureInfo _configureInfo;
        /// <summary>
        /// 公司配置
        /// </summary>
        ConfigureInfo _ConfigureInfo
        {
            get
            {
                Guid companyID = CurrentContext == null ? LocalData.UserInfo.DefaultCompanyID : CurrentContext.CompanyID;
                if (_configureInfo == null || !_configureInfo.CompanyID.Equals(companyID))
                {
                    _configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                }
                return _configureInfo;
            }
        }

        /// <summary>
        /// 数据源(List)
        /// </summary>
        public List<ECommerceList> ListDataSource
        {
            get { return DataSource as List<ECommerceList>; }
            set
            {
                DataSource = value;
            }
        }

        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ECommercePresenter Presenter { get; set; }

        IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override event CurrentChangedHandler CurrentChanged;
        /// <summary>
        /// 
        /// </summary>
        public override event CancelEventHandler CurrentChanging;
        /// <summary>
        /// 
        /// </summary>
        public override event SavedHandler Saved;
        /// <summary>
        /// 
        /// </summary>
        public override event SelectedHandler Selected;
        /// <summary>
        /// 
        /// </summary>
        public override event SelectingHandler Selecting;

        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public ECommerceListPart()
        {
            InitializeComponent();
            RegisterEvents();
            Disposed += delegate
            {
                _barItemDic.Clear();
                _barItemDic = null;

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            

        }
        #endregion

        #region Init

        #region ToolPart
        /// <summary>
        /// 注册事件
        /// </summary>
        void RegisterEvents()
        {
            barRefersh.ItemClick += barRefersh_ItemClick;
            barAssociation.ItemClick += barAssociation_ItemClick;
            gvList.DoubleClick += gvList_DoubleClick;
        }
        
        /// <summary>
        /// 重写加载方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && !LocalData.IsEnglish)
            {
                InitControls();
            }
        }

        /// <summary>
        /// 初始化控件(设置控件显示文本)
        /// </summary>
        void InitControls()
        {
            if (pnlGridList.Controls.Count == 0)
                pnlGridList.Controls.Add(gcList);

        }
        #endregion

        #endregion

        #region Delegate & Window Event
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barRefersh_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Workitem.Commands[ECommerceCommandConstants.COMMAND_REFERSH].Execute();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Workitem.Commands[ECommerceCommandConstants.COMMAND_ADD].Execute();

                Workitem.Commands[ECommerceCommandConstants.COMMAND_REFERSH].Execute();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        /// <summary>
        /// 编辑
        /// </summary>
        private void barEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Workitem.Commands[ECommerceCommandConstants.COMMAND_EDIT].Execute();
                Workitem.Commands[ECommerceCommandConstants.COMMAND_REFERSH].Execute();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        /// <summary>
        /// 关联
        /// </summary>
        void barAssociation_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Workitem.Commands[ECommerceCommandConstants.COMMAND_ASSOCIATION].Execute();
                Workitem.Commands[ECommerceCommandConstants.COMMAND_REFERSH].Execute();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        /// <summary>
        /// 双击网格
        /// </summary>
        void gvList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Workitem.Commands[ECommerceCommandConstants.COMMAND_EDIT].Execute();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        /// <summary>
        /// 列表刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(ECommerceCommandConstants.COMMAND_REFERSH)]
        public void BLCommand_Refersh(object sender, EventArgs e)
        {
            Presenter.BindData(CurrentContext);
        }

        /// <summary>
        /// 关联业务
        /// </summary>
        [CommandHandler(ECommerceCommandConstants.COMMAND_ASSOCIATION)]
        public void BLCommand_Association(object sender, EventArgs e)
        {
            Presenter.AssociationBusiness(Workitem,CurrentContext);
        }
        /// <summary>
        /// 编辑业务
        /// </summary>
        [CommandHandler(ECommerceCommandConstants.COMMAND_ADD)]
        public void BLCommand_Add(object sender, EventArgs e)
        {
            Presenter.AddBusiness();
        }
        /// <summary>
        /// 编辑业务
        /// </summary>
        [CommandHandler(ECommerceCommandConstants.COMMAND_EDIT)]
        public void BLCommand_Edit(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            Presenter.EdiBusiness(CurrentRow.ID, CurrentContext.CompanyID, CurrentRow.No);
        }
        #endregion

        #region Methods
        /// <summary>
        /// gcList绑定数据
        /// </summary>
        /// <param name="context"></param>
        public void DataBind(BusinessOperationContext context)
        {
            CurrentContext = context;
            Presenter.ViewPart = this;
            colPODName.SummaryItem.DisplayFormat = "" + _ConfigureInfo.StandardCurrency + ":{0}";
            Presenter.BindData(CurrentContext);
        }
        /// <summary>
        /// 控件只读
        /// </summary>
        /// <param name="flg">是否只读</param>
        public void ControlsReadOnly(bool flg)
        {
            barRefersh.Enabled = flg;
        }

        /// <summary>
        /// 控件设置启用禁用
        /// </summary>
        /// <param name="name"></param>
        /// <param name="enable"></param>
        public void SetEnable(string name, bool enable)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Enabled = enable;
        }

        #endregion

       
    }
}
