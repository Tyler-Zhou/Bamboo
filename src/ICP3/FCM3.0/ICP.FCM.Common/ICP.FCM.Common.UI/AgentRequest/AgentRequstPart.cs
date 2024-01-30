using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.UI.AgentRequest
{
    public partial class AgentRequstPart : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IFCMCommonService fcmcommonService { get; set; }

        #endregion

        #region Init

        public AgentRequstPart()
        {
            InitializeComponent();
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            if (LocalData.IsEnglish==false) SetCNText();
        }

        private void SetCNText()
        {
            labType.Text = "类型";
            labRemark.Text = "备注";
            btnCancel.Text = "取消(&C)";
            btnOK.Text = "确定(&O)";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<AgentType>> agentTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<AgentType>(LocalData.IsEnglish);
            int i = 0;
            foreach (var item in agentTypes)
            {
                if (item.Value == AgentType.Unknown) continue;
                rabRequstType.Properties.Items[i].Description = item.Name;
                rabRequstType.Properties.Items[i].Value = item.Value;
                i++;
            }
            rabRequstType.SelectedIndex = 0;
        }

        #endregion

        #region IEditPart 成员

        void BindingData(object data)
        {
            AgentRequestInfo info = data as AgentRequestInfo;
            info.SendDate = DateTime.Now;
            info.SenderID = LocalData.UserInfo.LoginID;
            info.SenderName = LocalData.UserInfo.LoginName;
            bindingSource1.DataSource = data;
        }

        public override object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return this.Save();
        }

        public override void EndEdit()
        {
            this.Validate();
            bindingSource1.EndEdit();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region IPart 成员
        ICP.Framework.CommonLibrary.Client.OperationType bussinessType = ICP.Framework.CommonLibrary.Client.OperationType.OceanExport;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "ICP.Framework.CommonLibrary.Client.OperationType")
                {
                    bussinessType = (ICP.Framework.CommonLibrary.Client.OperationType)Enum.Parse(typeof(ICP.Framework.CommonLibrary.Client.OperationType), item.Value.ToString());
                }
            }
        }
        #endregion

        #region btn

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
            this.FindForm().Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (this.Save())
                {
                    this.FindForm().DialogResult = DialogResult.OK;
                    this.FindForm().Close();
                }
            }
        }

        bool Save()
        {
            try
            {
                AgentRequestInfo currentData = bindingSource1.DataSource as AgentRequestInfo;
                currentData.Type = (AgentType)Enum.Parse(typeof(AgentType), rabRequstType.Properties.Items[rabRequstType.SelectedIndex].Value.ToString());

                SingleResult result = fcmcommonService.RequestOceanAgent(currentData.OperationID
                                                                , bussinessType
                                                                , currentData.SenderID
                                                                , currentData.SendDate
                                                                , currentData.Type
                                                                , currentData.SenderRemark
                                                                , currentData.UpdateDate);
                //currentData.ID = result.GetValue<Guid>("ID");
                currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                if (Saved != null) Saved(new object[] { currentData });
                return true;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return false; }
        }

        #endregion
    }
}
