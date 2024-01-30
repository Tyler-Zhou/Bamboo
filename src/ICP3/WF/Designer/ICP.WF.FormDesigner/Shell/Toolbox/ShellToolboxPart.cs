

//-----------------------------------------------------------------------
// <copyright file="ShellToolboxPart.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using DevExpress.XtraEditors;
    using DevExpress.XtraNavBar;
    using ICP.WF.Controls.Common;
    using ICP.WF.ServiceInterface.Client;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// ������
    /// </summary>
    public class ShellToolboxPart : XtraUserControl, IToolboxPart
    {
       
        
        #region ����

        private XtraScrollableControl xtraScrollableControl1;
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }


        #endregion

        #region ���ر���

        private Hashtable customCreators;
       
        #endregion

        #region ��Դ��ʼ�����ͷ�

        public ShellToolboxPart()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                this.InitializeToolbox();

                this.AddEventHandlers();
            }
            this.Disposed += delegate {
                if (toolsListBox != null)
                {
                    toolsListBox.MouseDown -= toolsListBox_MouseDown;
                }
                if (this.xtraScrollableControl1 != null)
                {
                    this.xtraScrollableControl1.Controls.Clear();
                    this.xtraScrollableControl1.Dispose();
                    this.xtraScrollableControl1 = null;
                }
                if (this.customCreators != null)
                {
                    this.customCreators.Clear();
                    this.customCreators = null;
                }
                this.OnException = null;
                this.CurrentDesignerHost = null;
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
      
        private System.ComponentModel.Container components = null;
        private NavBarControl toolsListBox = null;
        private SplitContainer splitContainer1;
        private DevExpress.XtraEditors.GroupControl grpDesc;
        private DevExpress.XtraEditors.MemoEdit txtDesc;
        private DevExpress.Utils.ImageCollection imageCollection1;
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShellToolboxPart));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpDesc = new DevExpress.XtraEditors.GroupControl();
            this.txtDesc = new DevExpress.XtraEditors.MemoEdit();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDesc)).BeginInit();
            this.grpDesc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.AllowDrop = true;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AllowDrop = true;
            this.splitContainer1.Panel1.Controls.Add(this.xtraScrollableControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpDesc);
            // 
            // grpDesc
            // 
            this.grpDesc.AllowDrop = true;
            this.grpDesc.Controls.Add(this.txtDesc);
            resources.ApplyResources(this.grpDesc, "grpDesc");
            this.grpDesc.Name = "grpDesc";
            // 
            // txtDesc
            // 
            this.txtDesc.AllowDrop = true;
            resources.ApplyResources(this.txtDesc, "txtDesc");
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Properties.ReadOnly = true;
            // 
            // xtraScrollableControl1
            // 
            resources.ApplyResources(this.xtraScrollableControl1, "xtraScrollableControl1");
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            // 
            // ShellToolboxPart
            // 
            this.AllowDrop = true;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ShellToolboxPart";
            resources.ApplyResources(this, "$this");
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpDesc)).EndInit();
            this.grpDesc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #endregion


        #region ���ط���

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        private void InitializeToolbox()
        {
            if (toolsListBox != null)
            {
                return;
            }

            //��ʼ��NavBarControl
            toolsListBox = new NavBarControl();
            toolsListBox.BackColor = System.Drawing.Color.Red;
            toolsListBox.BackColor = System.Drawing.SystemColors.ControlLight;
            toolsListBox.Name = "ToolsListBox";
            toolsListBox.Dock = DockStyle.Top;
            toolsListBox.Height = 900;
            toolsListBox.DragDropFlags = NavBarDragDrop.AllowOuterDrop;

            xtraScrollableControl1.Controls.Add(toolsListBox);

            InitImgList(toolsListBox);

            //��NavBarGroup��ӵ�NavBarControl��
            ToolboxConfig config = DefaultConfigMananger.Default.GetToolboxConfig();

            if (config == null)
            {
                XtraMessageBox.Show(Utility.GetString("NoExistsConfigFile", "No Exists Config File"));
                return;
            }
            
            List<ToolboxGroup> groups = config.FindGroups(ToolboxGroupType.Form);
            int i = 0;
            foreach (ToolboxGroup g in groups)
            {
                //�����
                NavBarGroup group = new NavBarGroup();
                group.Tag = g.ETitle;
                group.Caption = g.CTitle;
                group.Name = g.ETitle;
                group.Expanded = true;
                group.DragDropFlags = NavBarDragDrop.AllowOuterDrop;
                toolsListBox.Groups.Add(group);
             
                //���ָ����
                NavBarItem item = new NavBarItem();
                item.Name = "Pointer";
                item.Caption = "<Pointer>";
                item.Tag = null;
                item.CanDrag = true;
                group.ItemLinks.Add(item);

                foreach (ICP.WF.ServiceInterface.Client.ToolboxItem toolboxItem in g.Items)
                {
                    Image img = toolboxItem.GetImage();
                    if (img != null)
                    {
                        ///����ͼƬ�б�
                        i = imageCollection1.Images.Count;
                        imageCollection1.Images.Add(img, toolboxItem.GetTitle());
                        imageCollection1.Images.SetKeyName(i, toolboxItem.GetTitle());
                    }

                    NavBarItem itembutton = new NavBarItem();
                    itembutton.Name = toolboxItem.GetTitle();
                    itembutton.SmallImageIndex = i;
                    itembutton.LargeImageIndex = i;
                    itembutton.Tag = toolboxItem;
                    itembutton.CanDrag = true;
                  
                    itembutton.Caption = toolboxItem.GetTitle();
                    itembutton.Hint = toolboxItem.GetDescription();
                    itembutton.LinkClicked += new NavBarLinkEventHandler(itembutton_LinkClicked);
                    itembutton.LinkPressed += new NavBarLinkEventHandler(itembutton_LinkPressed);
                    group.ItemLinks.Add(itembutton);

                    i++;
                }
            }

        }

        /// <summary>
        /// ��ʼ��ͼƬ�б�
        /// </summary>
        private void InitImgList(NavBarControl listToolbox)
        {
            imageCollection1 = new DevExpress.Utils.ImageCollection();
            listToolbox.LargeImages = imageCollection1;
            listToolbox.SmallImages = imageCollection1;
        }

        /// <summary>
        /// �����¼�����
        /// </summary>
        private void AddEventHandlers()
        {
            toolsListBox.MouseDown += new MouseEventHandler(toolsListBox_MouseDown);
        }

       
        #endregion

        #region �¼�����

        void itembutton_LinkPressed(object sender, NavBarLinkEventArgs e)
        {
            ICP.WF.ServiceInterface.Client.ToolboxItem toolboxItem = (ICP.WF.ServiceInterface.Client.ToolboxItem)e.Link.Item.Tag;
            if (toolboxItem != null)
            {
                this.txtDesc.Text = toolboxItem.GetDescription();
            }
            else
            {
                this.txtDesc.Text = string.Empty;
            }
        }

        void itembutton_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            ICP.WF.ServiceInterface.Client.ToolboxItem toolboxItem = (ICP.WF.ServiceInterface.Client.ToolboxItem)e.Link.Item.Tag;
            if (toolboxItem != null)
            {
                this.txtDesc.Text = toolboxItem.GetDescription();
            }
            else
            {
                this.txtDesc.Text = string.Empty;
            }
        }

        void toolsListBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (toolsListBox.HotTrackedLink == null)
                {
                    return;
                }

                ICP.WF.ServiceInterface.Client.ToolboxItem toolboxItem = (ICP.WF.ServiceInterface.Client.ToolboxItem)toolsListBox.HotTrackedLink.Item.Tag;
                if (toolboxItem == null)
                {
                    return;
                }
                if (this.CurrentDesignerHost == null
                    || this.CurrentDesignerHost.RootComponent==null)
                {
                    return;
                }
                if (e.Clicks == 2)
                {
                    System.Drawing.Design.IToolboxUser tbu = this.CurrentDesignerHost.GetDesigner(this.CurrentDesignerHost.RootComponent) as System.Drawing.Design.IToolboxUser;
                    if (tbu != null)
                    {
                        tbu.ToolPicked(new System.Drawing.Design.ToolboxItem(toolboxItem.GetComponentType()));
                    }
                }
                else if (e.Clicks < 2)
                {
                    DataObject d = this.SerializeToolboxItem(new System.Drawing.Design.ToolboxItem(toolboxItem.GetComponentType())) as DataObject;
                    toolsListBox.DoDragDrop(d, DragDropEffects.Copy);
                }
            }
            catch (Exception ex)
            {
                OnRaiseException(ex);
            }
        }

        void OnRaiseException(Exception ex)
        {
            ExceptionEventArgs args = new ExceptionEventArgs(ex);
            if (OnException != null)
            {
                OnException(this, args);
            }
        }

        #endregion

        #region IToolboxService�ӿڳ�Աʵ��

        public event ExceptionEventHandler OnException;

        public IDesignerHost CurrentDesignerHost { get; set; }

        /// <summary>
        /// ����ָʾ������ĵ�ǰ��ѡ���
        /// </summary>
        public System.Drawing.Design.CategoryNameCollection CategoryNames
        {
            get
            {
                return new System.Drawing.Design.CategoryNameCollection(new string[] { "Form" });
            }
        }

        public System.Drawing.Design.ToolboxItem GetSelectedToolboxItem(IDesignerHost host)
        {
            if (toolsListBox.HotTrackedLink == null)
            {
                return null;
            }

            ICP.WF.ServiceInterface.Client.ToolboxItem toolboxItem = (ICP.WF.ServiceInterface.Client.ToolboxItem)toolsListBox.HotTrackedLink.Item.Tag;
            if (toolboxItem == null)
            {
                return null;
            }

            return new System.Drawing.Design.ToolboxItem(toolboxItem.GetComponentType());
        }

        public System.Drawing.Design.ToolboxItem GetSelectedToolboxItem()
        {
            return this.GetSelectedToolboxItem(null);
        }

        public void AddToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem, string category)
        {
        }

        public void AddToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem)
        {
        }

        public bool IsToolboxItem(
            object serializedObject,
            IDesignerHost host)
        {
            return false;
        }

        public bool IsToolboxItem(object serializedObject)
        {
            return false;
        }

        public void SetSelectedToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem)
        {
        }

        public void SelectedToolboxItemUsed()
        {
        }

        void System.Drawing.Design.IToolboxService.Refresh()
        {
        }

        public void AddLinkedToolboxItem(
            System.Drawing.Design.ToolboxItem toolboxItem, 
            string category,
            IDesignerHost host)
        {
        }

        public void AddLinkedToolboxItem(
            System.Drawing.Design.ToolboxItem toolboxItem, 
            IDesignerHost host)
        {
        }

        public bool IsSupported(
            object serializedObject, 
            ICollection filterAttributes)
        {
            return true;
        }

        public bool IsSupported(
            object serializedObject, 
            IDesignerHost host)
        {
            return true;
        }

        public string SelectedCategory
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public System.Drawing.Design.ToolboxItem DeserializeToolboxItem(
            object serializedObject, 
            IDesignerHost host)
        {
            IDataObject dataObject = serializedObject as IDataObject;
            if (dataObject == null)
            {
                return null;
            }

            System.Drawing.Design.ToolboxItem t = (System.Drawing.Design.ToolboxItem)dataObject.GetData(typeof(System.Drawing.Design.ToolboxItem));
            if (t == null)
            {
                string format;
                ToolboxItemCreatorCallback creator = this.FindToolboxItemCreator(dataObject, host, out format);
                if (creator != null)
                {
                    return creator(
                        dataObject, 
                        format);
                }
            }

            return t;
        }

        public System.Drawing.Design.ToolboxItem DeserializeToolboxItem(object serializedObject)
        {
            return this.DeserializeToolboxItem(
                serializedObject,
                this.CurrentDesignerHost);
        }

        public System.Drawing.Design.ToolboxItemCollection GetToolboxItems(
            string category, 
            IDesignerHost host)
        {
            return null;
        }

        public System.Drawing.Design.ToolboxItemCollection GetToolboxItems(string category)
        {
            return null;
        }

        public System.Drawing.Design.ToolboxItemCollection GetToolboxItems(IDesignerHost host)
        {
            return null;
        }

        public System.Drawing.Design.ToolboxItemCollection GetToolboxItems()
        {
            return null;
        }

        /// <summary>
        /// Ϊָ�������ݸ�ʽ���������������µĹ����������
        /// </summary>
        /// <param name="creator">System.Drawing.Design.ToolboxItemCreatorCallback�������ڵ��ù�������ʱ���������</param>
        /// <param name="format">�����ߴ�������ݸ�ʽ</param>
        /// <param name="host">System.ComponentModel.Design.IDesignerHost����ʾ�봴����������������������</param>
        public void AddCreator(
            System.Drawing.Design.ToolboxItemCreatorCallback creator, 
            string format, 
            IDesignerHost host)
        {
            if (creator == null 
                || format == null)
            {
                throw new ArgumentNullException(creator == null ? "creator" : "format");
            }

            if (customCreators == null)
            {
                customCreators = new Hashtable();
            }
            else
            {
                string key = format;
                if (host != null)
                {
                    key += ", " + host.GetHashCode().ToString();
                }

                if (customCreators.ContainsKey(key))
                {
                    throw new Exception(Utility.GetString("AlreadyContainCreatorRegisteredFormat", "�Ѿ���һ������ע��ĸ�ʽ ") + format);
                }
            }

            customCreators[format] = creator;
        }

        /// <summary>
        ///  Ϊָ�������ݸ�ʽ����µĹ����������
        /// </summary>
        /// <param name="creator">System.Drawing.Design.ToolboxItemCreatorCallback�������ڵ��ù�������ʱ�������</param>
        /// <param name="format">�����ߴ�������ݸ�ʽ</param>
        public void AddCreator(
            System.Drawing.Design.ToolboxItemCreatorCallback creator,
            string format)
        {
            this.AddCreator(
                creator, 
                format, 
                null);
        }

        public bool SetCursor()
        {
            return false;
        }

        public void RemoveToolboxItem(
            System.Drawing.Design.ToolboxItem toolboxItem, 
            string category)
        {
        }

        public void RemoveToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem)
        {
        }

        public object SerializeToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem)
        {
            DataObject dataObject = new DataObject();
            dataObject.SetData(
                typeof(System.Drawing.Design.ToolboxItem), 
                toolboxItem);

            return dataObject;
        }

        public void RemoveCreator(
            string format, 
            IDesignerHost host)
        {
        }

        public void RemoveCreator(string format)
        {
        }

        
        private ToolboxItemCreatorCallback FindToolboxItemCreator(
            IDataObject dataObj, 
            IDesignerHost host, 
            out string foundFormat)
        {
            foundFormat = string.Empty;
            ToolboxItemCreatorCallback creator = null;
            if (customCreators != null)
            {
                IEnumerator keys = customCreators.Keys.GetEnumerator();
                while (keys.MoveNext())
                {
                    string key = (string)keys.Current;
                    string[] keyParts = key.Split(new char[] { ',' });
                    string format = keyParts[0];
                    if (dataObj.GetDataPresent(format))
                    {
                        if (keyParts.Length == 1 
                            || (host != null && host.GetHashCode().ToString().Equals(keyParts[1])))
                        {
                            creator = (ToolboxItemCreatorCallback)customCreators[format];
                            foundFormat = format;
                            break;
                        }
                    }
                }
            }

            return creator;
        }

        #endregion
    }

  
}
