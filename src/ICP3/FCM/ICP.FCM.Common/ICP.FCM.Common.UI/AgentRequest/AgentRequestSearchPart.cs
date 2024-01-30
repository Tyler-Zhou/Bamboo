using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.Common.UI.AgentRequest
{   
    /// <summary>
    /// 代理列表查询面板界面
    /// </summary>
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class AgentRequestSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        #endregion

        #region init

        public AgentRequestSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate {

                this.OnSearched = null;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            };
            //if (LocalData.IsEnglish == false) SetCnText();
        }
        private void SetCnText()
        {
            this.btnClear.Text = "清空(&L)";
            this.btnSearch.Text = "查询(&S)";


            labMax.Text = "最大行数";
            labOperationNo.Text = "业务号";
            labState.Text = "状态";
            labType.Text = "申请类型";
            labOperationType.Text = "业务类型";

            chkSolverByMe.Text = "由我指定";
            nbarBase.Caption = "基本信息";
            nbarDate.Caption = "日期";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }

        private void InitControls()
        {
            this.fromToDateMonthControl1.IsEngish = LocalData.IsEnglish;

            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<AgentRequestStateEnum>> agentRequestStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<AgentRequestStateEnum>(LocalData.IsEnglish);
            cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", System.DBNull.Value));
            foreach (var item in agentRequestStates)
            {
                if (item.Value == 0) continue;
                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbState.SelectedIndex = 0;


            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<AgentType>> agentTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<AgentType>(LocalData.IsEnglish);
            cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", System.DBNull.Value));
            foreach (var item in agentTypes)
            {
                if (item.Value == 0) continue;
                cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbType.SelectedIndex = 0;

            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<ICP.Framework.CommonLibrary.Common.OperationType>> bussinessTypes
                = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<ICP.Framework.CommonLibrary.Common.OperationType>(LocalData.IsEnglish);
            cmbOperationType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", System.DBNull.Value));
            foreach (var item in bussinessTypes)
            {
                if (item.Value == 0) continue;
                cmbOperationType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbOperationType.SelectedIndex = 0;

            SetControlsEnterToSearch();
        }

        private void SetControlsEnterToSearch()
        {
            foreach (Control item in this.panel2.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                && (item is DevExpress.XtraEditors.SpinEdit) == false)
                {
                    item.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Enter) this.btnSearch.PerformClick();
                    };
                }
            }
        }

        #endregion

        #region ISearchPart 成员

        public override object GetData()
        {
            DateTime? dtFrom = null, dtTo = null;
            if (nbarDate.Expanded)
            {
                dtFrom = fromToDateMonthControl1.From;
                dtTo = fromToDateMonthControl1.To;
            }

            ICP.Framework.CommonLibrary.Common.OperationType? bussinessType = null;
            if (cmbOperationType.EditValue != null && cmbOperationType.EditValue != System.DBNull.Value)
            {
                bussinessType = (ICP.Framework.CommonLibrary.Common.OperationType)cmbOperationType.EditValue;
            }

            AgentRequestStateEnum? agentRequestState = null;
            if (cmbState.EditValue != null && cmbState.EditValue != System.DBNull.Value)
            {
                agentRequestState = (AgentRequestStateEnum)cmbState.EditValue;
            }

            AgentType? agentType = null;
            if (cmbType.EditValue != null && cmbType.EditValue != System.DBNull.Value)
            {
                agentType = (AgentType)cmbType.EditValue;
            }

            Guid? solverID=null;
            if(chkSolverByMe.Checked)solverID = LocalData.UserInfo.LoginID ;

            try
            {
                List<AgentRequestInfo> list = FCMCommonService.GetAgentRequestList(txtOperationNo.Text.Trim()
                                                        , bussinessType
                                                        , agentType
                                                        , agentRequestState
                                                        , solverID
                                                        , dtFrom
                                                        , dtTo
                                                        , int.Parse(numMax.Value.ToString()));

                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return null; }
        }
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

        #region btn

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (OnSearched != null)
                    OnSearched(this, GetData());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in panel2.Controls)
            {
                if (item is DevExpress.XtraEditors.ImageComboBoxEdit)
                {
                    (item as DevExpress.XtraEditors.ImageComboBoxEdit).SelectedIndex = 0;
                }
                else if (item is DevExpress.XtraEditors.TextEdit
                    && (item is DevExpress.XtraEditors.SpinEdit) == false
                    && item.Enabled == true
                    && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;

            }

            cmbType.SelectedIndex = 0;
            cmbState.SelectedIndex = 0;
        }

        #endregion
    }

}
