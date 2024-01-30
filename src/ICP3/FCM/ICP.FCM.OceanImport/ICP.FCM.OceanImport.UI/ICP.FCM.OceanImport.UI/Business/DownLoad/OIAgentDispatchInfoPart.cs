using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.OceanImport.UI.Business.DownLoad
{
    /// <summary>
    /// 海进分发文档列表信息面板
    /// </summary>
    public partial class OIAgentDispatchInfoPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {   
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public OIAgentDispatchInfoPart()
        {
            InitializeComponent();
            InitControls();
            this.Disposed += delegate {
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        void InitControls()
        {
            if (!LocalData.IsEnglish)
            {
                this.lblAcceptBy.Text = "签收人";
                this.lblAcceptOn.Text = "签收时间";
                this.lblDispatchBy.Text = "分发人";
                this.lblDispatchOn.Text = "分发时间";
                this.lblAssignTo.Text = "指派给";
            }
        }

        public void SetAgentDisPatchInfo(AgentDispatchInfo info)
        {
            this.txtAcceptBy.Text = info == null ? string.Empty : info.AcceptByName;
            this.txtAcceptOn.Text = info == null ? string.Empty : info.AcceptOn.ToString();
            this.txtDispatchBy.Text = info == null ? string.Empty : info.DispatchByName;
            this.txtDispatchOn.Text = info == null ? string.Empty : info.DispatchOn.ToString();
            this.txtAssignTo.Text = info == null ? string.Empty : info.AssignToName;
        }

        public void SetCtlData(string acceptByName, string acceptOn, string assignToName)
        {
            SetData(acceptByName, acceptOn, assignToName);
        }

        private void SetData(string acceptByName, string acceptOn, string assignToName)
        {
            this.txtAcceptBy.Text = acceptByName;
            this.txtAcceptOn.Text = acceptOn;
            if (!string.IsNullOrEmpty(assignToName))
                this.txtAssignTo.Text = assignToName;
        }

        private void RefershCtn()
        {
            this.Refresh();
            this.Invalidate(true);
            this.Update();
        }
    }
}
