using System;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.UI.AgentRequest
{
    public partial class AssignAgentRequestPart : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }


        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        #endregion

        #region Init

        public AssignAgentRequestPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.Saved = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (LocalData.IsEnglish) SetCNText();
        }

        private void SetCNText()
        {
            labAgent.Text = "代理";
            labRemark.Text = "备注";
            btnCancel.Text= "取消(&C)";
            btnOK.Text = "确定(&O)";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {
            AgentRequestInfo currentData = bindingSource1.DataSource as AgentRequestInfo;

            DataFindClientService.Register(stxtAgent, CommonFinderConstants.CustomerAgentFinder, ICP.Common.UI.SearchFieldConstants.CodeName, ICP.Common.UI.SearchFieldConstants.ResultValue,
                     delegate(object inputSource, object[] resultData)
                     {
                         stxtAgent.Text = currentData.AgentName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                         stxtAgent.Tag = currentData.AgentID = new Guid(resultData[0].ToString());

                     }, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
        }

        #endregion

        #region IEditPart 成员

        void BindingData(object data)
        {
            AgentRequestInfo info = data as AgentRequestInfo;
            info.SolveDate = DateTime.Now;
            info.SolverID = LocalData.UserInfo.LoginID;
            info.SolverName = LocalData.UserInfo.LoginName;
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

        #region btn

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        bool Save()
        {
            if (ValidateData() == false) return false;

            try
            {
                AgentRequestInfo currentData = bindingSource1.DataSource as AgentRequestInfo;
               
                SingleResult result = FCMCommonService.AnswerAgentRequest(currentData.ID
                                                  , currentData.SolverID.Value
                                                  , currentData.SolveDate.Value
                                                  , currentData.SolverRemark
                                                  , currentData.AgentID
                                                  , currentData.UpdateDate);

                currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                if (Saved != null) Saved(new object[] { currentData }); 
                return true;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return false; }
        }

        private bool ValidateData()
        {
            AgentRequestInfo currentData = bindingSource1.DataSource as AgentRequestInfo;
            if(currentData.Validate(delegate(ValidateEventArgs e)
                {
                    if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(currentData.AgentID))
                    { 
                        e.SetErrorInfo("AgentID", LocalData.IsEnglish ? "Agent Must Input" : "代理必须输入.");
                    }
                }
            )==false) return false;

            return true;
        }

        #endregion
    }
}

