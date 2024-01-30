//-----------------------------------------------------------------------
// <copyright file="ShellOutputPart.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
using Microsoft.Practices.CompositeUI;

	/// <summary>
	/// �������.
	/// </summary>
    public class ShellOutputPart :XtraUserControl, IOutputPart
    {

        #region ���ر���

        private ImageList imageList;
        private ContextMenuStrip contextMenuOutPut;
        private ToolStripMenuItem barRemoveSelectItem;
        private ToolStripMenuItem barClearAll;
        private DevExpress.XtraEditors.ImageListBoxControl messageList;
		

		private System.ComponentModel.IContainer components = null;

        #endregion

        #region ��ʼ�����ͷ�
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
      
        public ShellOutputPart()
		{
			InitializeComponent();

            if (DesignMode == false)
            {
                InitControls();
            }
            this.Disposed += delegate {
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            };
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShellOutputPart));
            this.contextMenuOutPut = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.barRemoveSelectItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barClearAll = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.messageList = new DevExpress.XtraEditors.ImageListBoxControl();
            this.contextMenuOutPut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageList)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuOutPut
            // 
            this.contextMenuOutPut.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.barRemoveSelectItem,
            this.barClearAll});
            this.contextMenuOutPut.Name = "contextMenuOutPut";
            resources.ApplyResources(this.contextMenuOutPut, "contextMenuOutPut");
            // 
            // barRemoveSelectItem
            // 
            this.barRemoveSelectItem.Image = global::ICP.WF.FormDesigner.Properties.Resources.ɾ��;
            this.barRemoveSelectItem.Name = "barRemoveSelectItem";
            resources.ApplyResources(this.barRemoveSelectItem, "barRemoveSelectItem");
            this.barRemoveSelectItem.Click += new System.EventHandler(this.barRemoveSelectItem_Click);
            // 
            // barClearAll
            // 
            this.barClearAll.Image = global::ICP.WF.FormDesigner.Properties.Resources.ɾ��;
            this.barClearAll.Name = "barClearAll";
            resources.ApplyResources(this.barClearAll, "barClearAll");
            this.barClearAll.Click += new System.EventHandler(this.barClearAll_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "���ݣ��죩.ico");
            this.imageList.Images.SetKeyName(1, "���ݣ��ƣ�.ico");
            // 
            // messageList
            // 
            resources.ApplyResources(this.messageList, "messageList");
            this.messageList.ContextMenuStrip = this.contextMenuOutPut;
            this.messageList.ImageList = this.imageList;
            this.messageList.Name = "messageList";
            this.messageList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            // 
            // ShellOutputPart
            // 
            this.Controls.Add(this.messageList);
            this.Name = "ShellOutputPart";
            resources.ApplyResources(this, "$this");
            this.contextMenuOutPut.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messageList)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
        
        #endregion

        #region �ⲿ�ӿ�
        /// <summary>
        /// ��Ϣ��ʾ
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            messageList.Items.Add(message, 1);
            RereshToolBars();
        }

        /// <summary>
        /// ������ʾ
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            messageList.Items.Add(message, 0);
            RereshToolBars();
        }

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        public virtual void ClearAll()
        {
            messageList.Items.Clear();

            RereshToolBars();
        }

        #endregion

        #region �¼�����
        /// <summary>
        /// �Ƴ�ѡ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barRemoveSelectItem_Click(object sender, EventArgs e)
        {
            //��ѡ��ֵ�ŵ�һ��������
            IList<ImageListBoxItem> itemList = new List<ImageListBoxItem>();
            foreach (ImageListBoxItem item in this.messageList.SelectedItems)
            {
                itemList.Add(item);
            }
            //�Ƴ�����ѡ���ֵ
            foreach (ImageListBoxItem item in itemList)
            {
                this.messageList.Items.Remove(item);
            }

            RereshToolBars();
        }
        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barClearAll_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        #endregion

        #region ���ط���
        /// <summary>
        /// ��֤��ť�Ƿ����
        /// </summary>
        private void RereshToolBars()
        {
            barRemoveSelectItem.Enabled = (messageList.SelectedItems.Count > 0);
           
            barClearAll.Enabled=(messageList.Items.Count>0);
        }

        private void InitControls()
        {
            //messageList.View = View.Details;
            //messageList.AllowColumnReorder = true;
            //messageList.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.None);
            //messageList.AutoArrange = true;
            //messageList.CheckBoxes = true;
            //messageList.FullRowSelect = true;
           
        }

        #endregion
    }
}
