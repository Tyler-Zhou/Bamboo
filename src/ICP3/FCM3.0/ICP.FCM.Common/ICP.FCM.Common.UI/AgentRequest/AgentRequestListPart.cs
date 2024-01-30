using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.UI.AgentRequest
{
    [ToolboxItem(false)]
    public partial class AgentRequestListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IFCMCommonService fcmcommonService { get; set; }

        #endregion

        #region Init

        public AgentRequestListPart()
        {
            InitializeComponent();
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            colAgentName.Caption = "代理";
            colOperationNo.Caption = "业务号";
            colPOD.Caption = "卸货港";
            colType.Caption = "类型";
            colSenderRemark.Caption = "申请备注";
            colSenderDate.Caption = "申请日期";
            colSenderName.Caption = "申请人";
            colSolveDate.Caption = "回复日期";
            colSolverName.Caption = "回复人";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {
            //AgentType
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<AgentType>> agentTypes
                = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<AgentType>(LocalData.IsEnglish);
            foreach (var item in agentTypes)
            {
                rcmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected AgentRequestInfo CurrentRow
        {
            get { return Current as AgentRequestInfo; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                bsList.DataSource = value;
                bsList.ResetBindings(false);
                if (CurrentChanged != null) CurrentChanged(this, Current);
            }
        }

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;

        #endregion

        #region GridView Event

        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentRow != null) Workitem.Commands[OEAgentRequesCommandConstants.Command_Assign].Execute();
        }

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            AgentRequestInfo list = gvMain.GetRow(e.RowHandle) as AgentRequestInfo;
            if (list == null) return;

        }

        #endregion

        #region Workitem Common

        [CommandHandler(OEAgentRequesCommandConstants.Command_Assign)]
        public void Command_EditData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            AgentRequestInfo editData = fcmcommonService.GetAgentRequestInfo(CurrentRow.ID);
            string title = LocalData.IsEnglish ? "Agent Assign" : "指定代理";
            Utility.ShowEditPart<AssignAgentRequestPart>(Workitem, editData, title, EditPartSaved);
        }

        void EditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            AgentRequestInfo data = prams[0] as AgentRequestInfo;
            List<AgentRequestInfo> source = this.DataSource as List<AgentRequestInfo>;
            if (source == null || source.Count == 0)
            {
                bsList.Add(data);
                bsList.ResetBindings(false);
            }
            else
            {
                AgentRequestInfo tager = source.Find(delegate(AgentRequestInfo item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    Utility.CopyToValue(data, tager, typeof(AgentRequestList));
                    bsList.ResetItem(bsList.IndexOf(tager));
                }

            }
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }

        [CommandHandler(OEAgentRequesCommandConstants.Command_Reject)]
        public void Command_Reject(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            Common.Parts.EditRemarkPart editRemarkPart = Workitem.Items.AddNew<Common.Parts.EditRemarkPart>();
            editRemarkPart.Saved += delegate(object[] prams)
            {
                try
                {
                    SingleResult result = fcmcommonService.RejectAgentRequest(CurrentRow.ID
                                                      , LocalData.UserInfo.LoginID
                                                      , DateTime.Now
                                                      , prams[0].ToString()
                                                      , CurrentRow.UpdateDate);

                    AgentRequestInfo currentRow = CurrentRow;
                    currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    bsList.ResetCurrentItem();
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
            };
            string title = LocalData.IsEnglish ? "Reject Request" : "打回申请代理";
            Utility.ShowDialog(editRemarkPart, title);
        }

        void editRemarkPart_Saved(params object[] prams)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
