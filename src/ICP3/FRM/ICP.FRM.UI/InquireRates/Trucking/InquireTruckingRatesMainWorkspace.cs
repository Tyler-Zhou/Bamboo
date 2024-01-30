using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using ICP.Framework.CommonLibrary.Client;
using ICP.FRM.ServiceInterface;
using Microsoft.Practices.CompositeUI;

namespace ICP.FRM.UI.InquireRates
{
    [ToolboxItem(false)]
    public partial class InquireTruckingRatesMainWorkspace : XtraUserControl
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 询价服务
        /// </summary>
        [ServiceDependency]
        public IInquireRatesService inquireRatesService { get; set; }
        #endregion      

        #region 成员变量
        /// <summary>
        /// 当前询价单对象
        /// </summary>
        private ClientInquierTruckingRate _CurrentInquierRate;
        /// <summary>
        /// 当前询价单对象
        /// </summary>
        public ClientInquierTruckingRate CurrentInquierRate
        {
            get { return _CurrentInquierRate; }
            set
            {
                //初始化
                IsMainInfoCutover = false;
                ClientInquierTruckingRate temp = value as ClientInquierTruckingRate;
                if (temp == null)
                {
                    _CurrentInquierRate = null;
                    IsMainInfoCutover = true;
                    return;
                }
                //是否主询价切换判断
                if (_CurrentInquierRate == null ||
                    (_CurrentInquierRate.ID != temp.ID &&
                     _CurrentInquierRate.ID != temp.MainRecordID &&
                     _CurrentInquierRate.MainRecordID != temp.ID &&
                     (_CurrentInquierRate.MainRecordID == null ||
                      _CurrentInquierRate.MainRecordID != temp.MainRecordID)))
                {
                    IsMainInfoCutover = true;

                }
                //设置当前询价
                _CurrentInquierRate = temp;
                if (_CurrentInquierRate != null && IsMainInfoCutover)
                    _CurrentInquierRate.InquirePriceInquireBysList =
                       inquireRatesService.GetInquirePriceInquireBys(_CurrentInquierRate.ID, _CurrentInquierRate.MainRecordID);

            }
        }
        /// <summary>
        /// 是否主询价信息切换
        /// </summary>
        public bool IsMainInfoCutover { get; set; }
        #endregion

        #region 构造函数
        public InquireTruckingRatesMainWorkspace()
        {
            InitializeComponent();
            xtraTabControl1.SelectedPageChanged += xtraTabControl1_SelectedPageChanged;
            Disposed += delegate
            {
                xtraTabControl1.SelectedPageChanged -= xtraTabControl1_SelectedPageChanged;
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Workspaces.Remove(CommunicationHistoryWorkspace);
                    Workitem.Workspaces.Remove(GeneralInfoWorkspace);
                    Workitem.Items.Remove(this);

                    ListWorkspace.PerformLayout();
                    CommunicationHistoryWorkspace.PerformLayout();
                    GeneralInfoWorkspace.PerformLayout();
                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;

                }
            };
        }

        void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            PanelDataBind();
        }

        
        #endregion

        /// <summary>
        /// 面板数据绑定
        /// </summary>
        public void PanelDataBind()
        {
            var dataBindControl = EnsureDockPanelControlExists();
            if (dataBindControl != null)
            {
                dataBindControl.DataSourceBind(CurrentInquierRate);
            }
        }

        /// <summary>
        /// 在当前DockPanel查找用户控件
        /// </summary>
        /// <returns></returns>
        private IInquierRateDataBind EnsureDockPanelControlExists()
        {
            UserControl userControl = null;
            //当前DockPanel未激活
            if (xtraTabControl1.SelectedTabPage == null)
                return null;
            switch (xtraTabControl1.SelectedTabPage.Name)
            {
                    
                case "tabGeneralInfo":
                    if (IsMainInfoCutover)
                        userControl = GeneralInfoWorkspace.Controls[0] as UserControl;
                    else
                        userControl = null;
                    break;
                case "tabHistory":
                    #region History
                    if (IsMainInfoCutover && _CurrentInquierRate!=null)
                    {
                        InquireRatesHistoryToolBar historyToolBar = HistoryToolBarWorkspace.Controls[0] as InquireRatesHistoryToolBar;
                        if (historyToolBar != null)
                        {
                            historyToolBar.Enabled = true;
                            //historyToolBar.Enabled = true;

                            if (_CurrentInquierRate.RespondByID == LocalData.UserInfo.LoginID)
                                historyToolBar.Enabled = true;
                            else
                                historyToolBar.Enabled = false;
                            //4.询价人字符串集合
                            string inquireBys = string.Empty;
                            if (_CurrentInquierRate.InquirePriceInquireBysList != null)
                            {
                                //组合串联询价人字符串
                                foreach (var item in _CurrentInquierRate.InquirePriceInquireBysList)
                                {
                                    if (string.IsNullOrEmpty(inquireBys))
                                        inquireBys += item.InquireByEName;
                                    else
                                        inquireBys += ',' + item.InquireByEName;
                                }
                                //询价人字符串不为空，历史记录替换按钮增加提示
                                if (!string.IsNullOrEmpty(inquireBys))
                                {
                                    historyToolBar.barReplace.SuperTip.Items.Clear();
                                    historyToolBar.barReplace.SuperTip.Items.AddTitle(
                                        "Merge the new Inquire Price into the selected Inquire Price of History.When you click it, " +
                                        inquireBys + " will be noticed with the Rates.");
                                }
                            }
                        }
                        userControl = HistoryWorkspace.Controls[0] as UserControl;
                    }
                    else
                        userControl = null;
                    #endregion
                    break;
                case "tabCommunicationHistoryWorkspace":
                    userControl = CommunicationHistoryWorkspace.Controls[0] as UserControl;
                    break;
            }
            if (userControl != null)
                return userControl as IInquierRateDataBind;
            else
                return null;
        }
    }
}
