//-----------------------------------------------------------------------
// <copyright file="ShellOutputPart.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.UI
{
    using System;
    using DevExpress.XtraEditors;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.WF.ServiceInterface;
    using ICP.WF.ServiceInterface.DataObject;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
	/// ����ͼ.
	/// </summary>
    public class WorkListFlowChatPart : XtraUserControl, IWorkListFlowChatService
    {

        #region ��ʼ�����ͷ�

        public WorkListFlowChatPart()
		{
			InitializeComponent();

            viewSequeueWorkflowView1.WorkItemDoubleClick+=new EventHandler<WorkItemEventArgs>(viewSequeueWorkflowView1_WorkItemDoubleClick);
            
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{   
                this.WorkItemDoubleClick = null;
                if (viewSequeueWorkflowView1 != null)
                {
                    viewSequeueWorkflowView1.WorkItemDoubleClick -= this.viewSequeueWorkflowView1_WorkItemDoubleClick;
                }
                if (this.workItem != null)
                {
                    this.workItem.Items.Remove(this);
                    this.workItem = null;
                }
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

        private ICP.WF.Activities.ViewSequeueWorkflowView viewSequeueWorkflowView1;

		#region Component Designer generated code
        private System.ComponentModel.IContainer components = null;


		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkListFlowChatPart));
            this.viewSequeueWorkflowView1 = new ICP.WF.Activities.ViewSequeueWorkflowView();
            this.SuspendLayout();
            // 
            // viewSequeueWorkflowView1
            // 
            resources.ApplyResources(this.viewSequeueWorkflowView1, "viewSequeueWorkflowView1");
            this.viewSequeueWorkflowView1.isCanceled = false;
            this.viewSequeueWorkflowView1.Name = "viewSequeueWorkflowView1";
            // 
            // WorkListFlowChatPart
            // 
            this.Controls.Add(this.viewSequeueWorkflowView1);
            this.Name = "WorkListFlowChatPart";
            resources.ApplyResources(this, "$this");
            this.ResumeLayout(false);

		}
		#endregion
        
        #endregion


        #region ����
        public IWorkflowService WorkflowService
        {
            get 
            {
                return ServiceClient.GetService<IWorkflowService>();
            }
        }

        [ServiceDependency]
        public WorkItem workItem
        {
            get;
            set;
        }
        #endregion

        #region �ӿڵ�ʵ��
        /// <summary>
        /// �������ͼ
        /// </summary>
        public void Clear()
        {
            viewSequeueWorkflowView1.ClearWorkFlowChart();
        }

        void viewSequeueWorkflowView1_WorkItemDoubleClick(object sender, WorkItemEventArgs e)
        {
            RaiseWorkItemDoubleClick(sender, e);

           
        }

        /// <summary>
        /// �鿴ָ��������
        /// </summary>
        public void ViewWorkFolwChart(Guid workID)
        {
            viewSequeueWorkflowView1.ShowWorkFlowChart(WorkflowService,workID);

        }


        /// <summary>
        /// �Ƿ���Խ���ȡ������
        /// </summary>
        /// <returns></returns>
        public bool isCanceled()
        {
            return viewSequeueWorkflowView1.isCanceled;
        }

        /// <summary>
        /// ������˫��ʱ�������¼�
        /// </summary>
        public event EventHandler<WorkItemEventArgs> WorkItemDoubleClick;


        /// <summary>
        /// ������˫��ʱ�������¼�
        /// </summary>
        /// <param name="sender">�������ݶ���</param>
        /// <param name="e"></param>
        private void RaiseWorkItemDoubleClick(object sender, WorkItemEventArgs e)
        {
            if (WorkItemDoubleClick != null)
            {
                WorkItemDoubleClick(sender, e);
            }
        }
        #endregion


    }
}
