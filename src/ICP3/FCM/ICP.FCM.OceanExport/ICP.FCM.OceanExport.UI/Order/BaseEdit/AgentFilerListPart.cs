using System;
using System.Collections.Generic;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.OceanExport.UI.Order
{
    /// <summary>
    /// 港后客服联系人面板
    /// </summary>
    public partial class AgentFilerListPart : BaseListPart
    {

        public IOceanExportService OceanExportservice
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        [ServiceDependency]
        public WorkItem RootWorkItem
        {
            get;
            set;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public AgentFilerListPart()
        {
            InitializeComponent();
            this.Disposed += (sender, e) =>
            {
                this.gcMain.DataSource = null;
                if (this.bsAgentFilerList != null)
                {
                    this.bsAgentFilerList.DataSource = null;

                    this.bsAgentFilerList.Dispose();
                }
                if (RootWorkItem != null)
                {
                    RootWorkItem.Items.Remove(this);
                    RootWorkItem = null;
                }

            };

        }
        /// <summary>
        /// 收货人名称
        /// </summary>
        string consigneeName;

        public override object DataSource
        {
            get { return consigneeName; }
            set { BindSource(value); }
        }
        void BindSource(object value)
        {
            consigneeName = value as string;
            if (!string.IsNullOrEmpty(consigneeName))
            {
                List<AgentFilerList> filerList = OceanExportservice.GetAgentFilerList(consigneeName) ?? new List<AgentFilerList>();
                InnerBind(filerList);
            }
        }
        private void InnerBind(List<AgentFilerList> filerList)
        {
            bsAgentFilerList.DataSource = filerList;
            this.bsAgentFilerList.ResetBindings(false);
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                bsAgentFilerList.DataSource = OceanExportservice.GetAgentFilerList(txtConsignee.Text.Trim());
                bsAgentFilerList.ResetBindings(false);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }
        }
    }

}
