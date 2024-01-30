using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.Business.Common.UI.Communication
{
    /// <summary>
    /// 带有操作工具栏的沟通历史列表控件
    /// </summary>
    public partial class UCCommunicationHistory : UserControl, IDataBind
    {
        #region Services & Property
        #region Services
        /// <summary>
        /// Workitem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        } 
        #endregion

        #region Property
        #region Presenter
        /// <summary>
        /// 数据代理
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public CommunicationHistoryListPresenter Presenter
        {
            get;
            set;
        }   
        #endregion

        #region ShowTools
        #region 是否显示工具栏
        private bool _IsShowTools;
        /// <summary>
        /// 是否显示工具栏
        /// </summary>
        public bool IsShowTools
        {
            get
            {
                return _IsShowTools;
            }
            set
            {
                _IsShowTools = value;
                ucCommunicationHistoryOperationBar.Visible = false;
            }
        } 
        #endregion
        #endregion

        #region 是否显示复选框

        /// <summary>
        /// 是否显示复选框
        /// </summary>
        public bool IsShowChoose
        {
            set
            {
                ucCommunicationHistoryList.IsShowChoose = value;
            }
        }
        #endregion

        #region DataSource
        /// <summary>
        /// 列表数据源
        /// </summary>
        public List<CommunicationHistory> DataSource
        {
            get { return ucCommunicationHistoryList.DataSource; }
            set
            {
                ucCommunicationHistoryList.DataSource = value;
            }
        }
        #endregion
        #endregion

        #endregion

        #region Init
        /// <summary>
        /// 构造函数
        /// </summary>
        public UCCommunicationHistory()
        {
            InitializeComponent();
            _IsShowTools = true;
        } 
        #endregion

        #region IDataBind 成员

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="context"></param>
        public void BindData(BusinessOperationContext context)
        {
            Presenter.ucList = this.ucCommunicationHistoryList;
            if (IsShowTools)
            {
                bool isNeedRegisterEvent = ucCommunicationHistoryOperationBar.ucList == null;
                ucCommunicationHistoryOperationBar.ucList = this.ucCommunicationHistoryList;
                if (isNeedRegisterEvent)
                {
                    ucCommunicationHistoryOperationBar.RegisterEvent();
                }
                ucCommunicationHistoryOperationBar.ListPresenter = Presenter;
                ucCommunicationHistoryOperationBar.ListPresenter.CurWorkItem = Workitem;
            }
            ucCommunicationHistoryList.ListPresenter = Presenter;
            ucCommunicationHistoryList.Workitem = Workitem;
            Presenter.LoadData(context);
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="business"></param>
        public void DataBind(BusinessOperationContext business)
        {
            BindData(business);
        }
        /// <summary>
        /// 空间只读控制
        /// </summary>
        /// <param name="flg"></param>
        public void ControlsReadOnly(bool flg)
        {
            ucCommunicationHistoryOperationBar.Enabled = flg;
        }
        #endregion
    }
}
