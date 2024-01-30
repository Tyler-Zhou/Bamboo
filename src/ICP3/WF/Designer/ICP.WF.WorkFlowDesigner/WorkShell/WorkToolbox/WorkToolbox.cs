

//-----------------------------------------------------------------------
// <copyright file="ShellToolboxPart.cs" company="ICP">
//     Copyright (c) ICP. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.WorkFlowDesigner
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
    using ICP.WF.Activities;
    using ICP.WF.ServiceInterface.Client;
    using Microsoft.Practices.CompositeUI;
    using System.ComponentModel.Design.Serialization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Workflow.ComponentModel;
    using System.Workflow.ComponentModel.Design;
    using System.ComponentModel;

    /// <summary>
    /// 工具栏
    /// </summary>
    public class WorkToolbox : XtraUserControl, IWorkToolbox
    {
        private const string CF_DESIGNER = "CF_WINOEDESIGNERCOMPONENTS";

        public event ExceptionEventHandler OnException;

        public IDesignerHost CurrentDesignerHost { get; set; }

        #region 服务
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

        #region 本地变量

        private Hashtable customCreators;

        #endregion

        #region 资源初始化与释放

        public WorkToolbox()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                this.InitializeToolbox();

                this.AddEventHandlers();
            }
            this.Disposed += delegate {
                if (customCreators != null)
                {
                    customCreators.Clear();
                    customCreators = null;
                }
                if (toolsListBox != null)
                {
                    toolsListBox.MouseDown -= toolsListBox_MouseDown;
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkToolbox));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpDesc = new DevExpress.XtraEditors.GroupControl();
            this.txtDesc = new DevExpress.XtraEditors.MemoEdit();
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
            // ShellToolboxPart
            // 
            this.AllowDrop = true;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ShellToolboxPart";
            resources.ApplyResources(this, "$this");
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpDesc)).EndInit();
            this.grpDesc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #endregion


        #region 本地方法

        /// <summary>
        /// 初始化工具栏
        /// </summary>
        private void InitializeToolbox()
        {
            if (toolsListBox != null)
            {
                return;
            }

            //初始化NavBarControl
            toolsListBox = new NavBarControl();
            toolsListBox.BackColor = System.Drawing.Color.Red;
            toolsListBox.BackColor = System.Drawing.SystemColors.ControlLight;
            toolsListBox.Name = "ToolsListBox";
            toolsListBox.Dock = DockStyle.Fill;
            toolsListBox.DragDropFlags = NavBarDragDrop.AllowOuterDrop;
            this.splitContainer1.Panel1.Controls.Add(toolsListBox);

            InitImgList(toolsListBox);

            //将NavBarGroup添加到NavBarControl中
            ToolboxConfig config = DefaultConfigMananger.Default.GetToolboxConfig();

            if (config == null)
            {
                XtraMessageBox.Show(Utility.GetString("NoExistsConfigFile", "No Exists Config File"));
                return;    
            }

            List<ToolboxGroup> groups = config.FindGroups(ToolboxGroupType.Workflow);
            int i = 0;
            foreach (ToolboxGroup g in groups)
            {
                //填加组
                NavBarGroup group = new NavBarGroup();
                group.Tag = g.ETitle;
                group.Caption = g.CTitle;
                group.Name = g.ETitle;
                group.Expanded = true;
                group.DragDropFlags = NavBarDragDrop.AllowOuterDrop;
                toolsListBox.Groups.Add(group);

                //填加指针项
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
                        ///设置图片列表
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
        /// 初始化图片列表
        /// </summary>
        private void InitImgList(NavBarControl listToolbox)
        {
            imageCollection1 = new DevExpress.Utils.ImageCollection();
            listToolbox.LargeImages = imageCollection1;
            listToolbox.SmallImages = imageCollection1;
        }

        /// <summary>
        /// 挂载事件处理
        /// </summary>
        private void AddEventHandlers()
        {
            toolsListBox.MouseDown += new MouseEventHandler(toolsListBox_MouseDown);
        }


        #endregion

        #region 事件处理

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
            
                if (toolsListBox.HotTrackedLink == null)
                {
                    return;
                }
                if (this.CurrentDesignerHost == null)
                {
                    return;
                }

                ICP.WF.ServiceInterface.Client.ToolboxItem toolboxItem = (ICP.WF.ServiceInterface.Client.ToolboxItem)toolsListBox.HotTrackedLink.Item.Tag;
                if (toolboxItem == null)
                {
                    return;
                }


                if (e.Clicks == 2)
                {
                    System.Drawing.Design.IToolboxUser tbu = this.CurrentDesignerHost.GetDesigner(this.CurrentDesignerHost.RootComponent) as System.Drawing.Design.IToolboxUser;
                    if (tbu != null)
                    {
                        tbu.ToolPicked(WorkToolbox.GetToolboxItem(toolboxItem.GetComponentType()));
                    }
                }
                else if (e.Clicks < 2)
                {
                    DataObject d = this.SerializeToolboxItem(WorkToolbox.GetToolboxItem(toolboxItem.GetComponentType())) as DataObject;
                    toolsListBox.DoDragDrop(d, DragDropEffects.Copy| DragDropEffects.Move);
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

        #region 公共方法

        /// <summary>
        ///  为指定的数据格式添加新的工具箱项创建者

        /// </summary>
        /// <param name="creator">System.Drawing.Design.ToolboxItemCreatorCallback，它可在调用工具箱项时创建组件</param>
        /// <param name="format">创建者处理的数据格式</param>
        public void AddCreator(ToolboxItemCreatorCallback creator, string format)
        {
            AddCreator(creator, format, null);
        }


        /// <summary>
        /// 为指定的数据格式和设计器宿主添加新的工具箱项创建者

        /// </summary>
        /// <param name="creator">System.Drawing.Design.ToolboxItemCreatorCallback，它可在调用工具箱项时创建组件。</param>
        /// <param name="format">创建者处理的数据格式</param>
        /// <param name="host">System.ComponentModel.Design.IDesignerHost，表示与创建者相关联的设计器宿主。</param>
        public void AddCreator(ToolboxItemCreatorCallback creator, string format, IDesignerHost host)
        {
            if (creator == null || format == null)
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
                    throw new Exception("There is already a creator registered for the format '" + format + "'.");
                }
            }

            customCreators[format] = creator;
        }

        /// <summary>
        /// 将指定的与项目链接的工具箱项添加到工具箱
        /// </summary>
        /// <param name="toolboxItem">要添加到工具箱中的已链接 System.Drawing.Design.ToolboxItem。</param>
        /// <param name="host">当前设计文档的 System.ComponentModel.Design.IDesignerHost</param>
        public void AddLinkedToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem, IDesignerHost host)
        {
        }

        /// <summary>
        /// 将指定的与项目链接的工具箱项添加到指定类别的工具箱

        /// </summary>
        /// <param name="toolboxItem">要添加到工具箱中的已链接 System.Drawing.Design.ToolboxItem</param>
        /// <param name="category">要添加给工具箱项的工具箱项类别</param>
        /// <param name="host">当前设计文档的 System.ComponentModel.Design.IDesignerHost</param>
        public void AddLinkedToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem, string category, IDesignerHost host)
        {
        }

        /// <summary>
        /// 将指定的工具箱项添加到工具箱中

        /// </summary>
        /// <param name="toolboxItem"> 要添加到工具箱中的 System.Drawing.Design.ToolboxItem</param>
        public virtual void AddToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem)
        {
        }

        /// <summary>
        /// 将指定的工具箱项添加到指定类别的工具箱

        /// </summary>
        /// <param name="toolboxItem"></param>
        /// <param name="host"></param>
        public virtual void AddToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem, IDesignerHost host)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toolboxItem"></param>
        /// <param name="category"></param>
        public virtual void AddToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem, string category)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toolboxItem"></param>
        /// <param name="category"></param>
        /// <param name="host"></param>
        public virtual void AddToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem, string category, IDesignerHost host)
        {
        }

        /// <summary>
        /// 属性指示工具箱的当前可选类别。

        /// </summary>
        public CategoryNameCollection CategoryNames
        {
            get
            {
                return new CategoryNameCollection(new string[] { "Workflow" });
            }
        }

        /// <summary>
        /// 属性指示当前选定的工具箱类别。

        /// </summary>
        public string SelectedCategory
        {
            get
            {
                return "Workflow";
            }
            set
            {
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual System.Drawing.Design.ToolboxItem GetSelectedToolboxItem()
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

            return WorkToolbox.GetToolboxItem(toolboxItem.GetComponentType());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public virtual System.Drawing.Design.ToolboxItem GetSelectedToolboxItem(IDesignerHost host)
        {
            return GetSelectedToolboxItem();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toolboxItem"></param>
        /// <returns></returns>
        public object SerializeToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem)
        {
            DataObject dataObject = new DataObject();
            dataObject.SetData(typeof(System.Drawing.Design.ToolboxItem), toolboxItem);
            return dataObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataObject"></param>
        /// <returns></returns>
        public System.Drawing.Design.ToolboxItem DeserializeToolboxItem(object dataObject)
        {
            return DeserializeToolboxItem(dataObject, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        public System.Drawing.Design.ToolboxItem DeserializeToolboxItem(object data, IDesignerHost host)
        {
            IDataObject dataObject = data as IDataObject;

            if (dataObject == null)
            {
                return null;
            }

            System.Drawing.Design.ToolboxItem t = (System.Drawing.Design.ToolboxItem)dataObject.GetData(typeof(System.Drawing.Design.ToolboxItem));

            if (t == null)
            {
                string format;
                ToolboxItemCreatorCallback creator = FindToolboxItemCreator(dataObject, host, out format);

                if (creator != null)
                {
                    return creator(dataObject, format);
                }
            }

            return t;
        }

        private ToolboxItemCreatorCallback FindToolboxItemCreator(IDataObject dataObj, IDesignerHost host, out string foundFormat)
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
                        if (keyParts.Length == 1 || (host != null && host.GetHashCode().ToString().Equals(keyParts[1])))
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


        /// <summary>
        /// 方法检索工具箱的项；可以通过按照一个工具箱类别对项进行筛选。
        /// </summary>
        /// <returns></returns>
        public ToolboxItemCollection GetToolboxItems()
        {
            return new ToolboxItemCollection(new System.Drawing.Design.ToolboxItem[0]);
        }

        /// <summary>
        /// 方法检索工具箱的项；可以通过按照一个工具箱类别对项进行筛选。

        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public ToolboxItemCollection GetToolboxItems(IDesignerHost host)
        {
            return new ToolboxItemCollection(new System.Drawing.Design.ToolboxItem[0]);
        }

        /// <summary>
        /// 方法检索工具箱的项；可以通过按照一个工具箱类别对项进行筛选。

        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public ToolboxItemCollection GetToolboxItems(string category)
        {
            return new ToolboxItemCollection(new System.Drawing.Design.ToolboxItem[0]);
        }

        /// <summary>
        /// 方法检索工具箱的项；可以通过按照一个工具箱类别对项进行筛选。

        /// </summary>
        /// <param name="category"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        public ToolboxItemCollection GetToolboxItems(string category, IDesignerHost host)
        {
            return new ToolboxItemCollection(new System.Drawing.Design.ToolboxItem[0]);
        }

        /// <summary>
        /// 如果指定的序列化对象为 ToolboxItem，指定的设计器宿主是否支持该序列化对象，或该序列化对象是否与指定属性相匹配。

        /// </summary>
        /// <param name="data"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        public bool IsSupported(object data, IDesignerHost host)
        {
            return true;
        }

        /// <summary>
        /// 如果指定的序列化对象为 ToolboxItem，指定的设计器宿主是否支持该序列化对象，或该序列化对象是否与指定属性相匹配。

        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="filterAttributes"></param>
        /// <returns></returns>
        public bool IsSupported(object serializedObject, ICollection filterAttributes)
        {
            return true;
        }

        /// <summary>
        /// 方法指示指定的序列化对象是否为 ToolboxItem。

        /// </summary>
        /// <param name="dataObject"></param>
        /// <returns></returns>
        public bool IsToolboxItem(object dataObject)
        {
            return IsToolboxItem(dataObject, null);
        }

        /// <summary>
        /// 方法指示指定的序列化对象是否为 ToolboxItem。

        /// </summary>
        /// <param name="data"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        public bool IsToolboxItem(object data, IDesignerHost host)
        {
            IDataObject dataObject = data as IDataObject;
            if (dataObject == null)
            {
                return false;
            }

            if (dataObject.GetDataPresent(typeof(System.Drawing.Design.ToolboxItem)))
            {
                return true;
            }
            else
            {
                string format;
                ToolboxItemCreatorCallback creator = FindToolboxItemCreator(dataObject, host, out format);
                if (creator != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 刷新工具箱显示，从而反映工具箱项的当前状态。

        /// </summary>
        public new void Refresh()
        {
        }

        /// <summary>
        /// 移除指定数据类型的任何 ToolboxItemCreatorCallback 委托。

        /// </summary>
        /// <param name="format"></param>
        public void RemoveCreator(string format)
        {
            RemoveCreator(format, null);
        }

        /// <summary>
        /// 移除指定数据类型的任何 ToolboxItemCreatorCallback 委托。

        /// </summary>
        /// <param name="format"></param>
        /// <param name="host"></param>
        public void RemoveCreator(string format, IDesignerHost host)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }

            if (customCreators != null)
            {
                string key = format;
                if (host != null)
                {
                    key += ", " + host.GetHashCode().ToString();
                }
                customCreators.Remove(key);
            }
        }

        /// <summary>
        /// 移除指定的 ToolboxItem。

        /// </summary>
        /// <param name="toolComponentClass"></param>
        public virtual void RemoveToolboxItem(System.Drawing.Design.ToolboxItem toolComponentClass)
        {
        }

        /// <summary>
        /// 移除指定的 ToolboxItem。

        /// </summary>
        /// <param name="componentClass"></param>
        /// <param name="category"></param>
        public virtual void RemoveToolboxItem(System.Drawing.Design.ToolboxItem componentClass, string category)
        {
        }

        /// <summary>
        ///  将当前应用程序的光标设置为表示当前选定工具的光标。

        /// </summary>
        /// <returns></returns>
        public virtual bool SetCursor()
        {
            return false;
        }

        /// <summary>
        /// 选择指定的工具箱项

        /// </summary>
        /// <param name="selectedToolClass"></param>
        public virtual void SetSelectedToolboxItem(System.Drawing.Design.ToolboxItem selectedToolClass)
        {
           
        }

        /// <summary>
        /// 根据类型添加一个工具项
        /// </summary>
        /// <param name="t"></param>
        public void AddType(Type t)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Attribute[] GetEnabledAttributes()
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attrs"></param>
        public void SetEnabledAttributes(Attribute[] attrs)
        {
        }

        /// <summary>
        /// 通知工具箱服务，选定工具已被使用。

        /// </summary>
        public void SelectedToolboxItemUsed()
        {
            SetSelectedToolboxItem(null);
        }

        /// <summary>
        /// 获取表示指定的工具箱项的可序列化对象。

        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="activities"></param>
        /// <returns></returns>
        public static IDataObject SerializeActivitiesToDataObject(IServiceProvider serviceProvider, ICollection activities)
        {
            // get component serialization service
            ComponentSerializationService css = (ComponentSerializationService)serviceProvider.GetService(typeof(ComponentSerializationService));
            if (css == null)
            {
                throw new InvalidOperationException("Component Serialization Service is missing.");
            }

            // serialize all activities to the store
            SerializationStore store = css.CreateStore();
            using (store)
            {
                foreach (Activity activity in activities)
                {
                    css.Serialize(store, activity);
                }
            }

            // wrap it with clipboard style object
            Stream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, store);
            stream.Seek(0, SeekOrigin.Begin);
            return new DataObject(CF_DESIGNER, stream);
        }

        /// <summary>
        /// 根据序列画对象反序列化为活动列表
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="dataObj"></param>
        /// <param name="addReference"></param>
        /// <returns></returns>
        public static Activity[] DeserializeActivitiesFromDataObject(IServiceProvider serviceProvider, IDataObject dataObj, bool addReference)
        {
            IDesignerHost designerHost = (IDesignerHost)serviceProvider.GetService(typeof(IDesignerHost));
            if (designerHost == null)
            {
                throw new InvalidOperationException("IDesignerHost is missing.");
            }

            if (dataObj == null)
            {
                return new Activity[] { };
            }

            object data = dataObj.GetData(CF_DESIGNER);
            ICollection activities = null;
            if (data is Stream)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                ((Stream)data).Seek(0, SeekOrigin.Begin);
                object serializationData = formatter.Deserialize((Stream)data);
                if (serializationData is SerializationStore)
                {
                    // get component serialization service
                    ComponentSerializationService css = serviceProvider.GetService(typeof(ComponentSerializationService)) as ComponentSerializationService;
                    if (css == null)
                    {
                        throw new Exception("ComponentSerializationService is missing.");
                    }

                    // deserialize data
                    activities = css.Deserialize((SerializationStore)serializationData);
                }
            }
            else
            {
                // Now check for a toolbox item.
                IToolboxService ts = (IToolboxService)serviceProvider.GetService(typeof(IToolboxService));
                if (ts != null && ts.IsSupported(dataObj, designerHost))
                {
                    System.Drawing.Design.ToolboxItem toolBoxItem = ts.DeserializeToolboxItem(dataObj, designerHost);
                    if (toolBoxItem != null)
                    {
                        // this will make sure that we add the assembly reference to project
                        if (addReference && toolBoxItem.AssemblyName != null)
                        {
                            ITypeResolutionService trs = serviceProvider.GetService(typeof(ITypeResolutionService)) as ITypeResolutionService;
                            if (trs != null)
                            {
                                trs.ReferenceAssembly(toolBoxItem.AssemblyName);
                            }
                        }

                        ActivityToolboxItem ActivityToolboxItem = toolBoxItem as ActivityToolboxItem;
                        if (addReference && ActivityToolboxItem != null)
                        {
                            activities = ActivityToolboxItem.CreateComponentsWithUI(designerHost);
                        }
                        else
                        {
                            activities = toolBoxItem.CreateComponents(designerHost);
                        }
                    }
                }
            }

            return (activities != null) ? (Activity[])(new ArrayList(activities).ToArray(typeof(Activity))) : new Activity[] { };
        }

        /// <summary>
        /// 获取指定类型的工具项
        /// </summary>
        /// <param name="toolType">工具项类型</param>
        /// <returns>工具项</returns>
        internal static System.Drawing.Design.ToolboxItem GetToolboxItem(Type toolType)
        {
            if (toolType == null)
            {
                throw new ArgumentNullException("toolType");
            }

            System.Drawing.Design.ToolboxItem item = null;
            if ((toolType.IsPublic || toolType.IsNestedPublic) && typeof(IComponent).IsAssignableFrom(toolType) && !toolType.IsAbstract)
            {
                ToolboxItemAttribute toolboxItemAttribute = (ToolboxItemAttribute)TypeDescriptor.GetAttributes(toolType)[typeof(ToolboxItemAttribute)];
                if (toolboxItemAttribute != null && !toolboxItemAttribute.IsDefaultAttribute())
                {
                    Type itemType = toolboxItemAttribute.ToolboxItemType;
                    if (itemType != null)
                    {
                        ConstructorInfo ctor = itemType.GetConstructor(new Type[] { typeof(Type) });
                        if (ctor != null)
                        {
                            item = (System.Drawing.Design.ToolboxItem)ctor.Invoke(new object[] { toolType });
                        }
                        else
                        {
                            ctor = itemType.GetConstructor(new Type[0]);
                            if (ctor != null)
                            {
                                item = (System.Drawing.Design.ToolboxItem)ctor.Invoke(new object[0]);
                                item.Initialize(toolType);
                            }
                        }
                    }
                }
                else if (!toolboxItemAttribute.Equals(System.ComponentModel.ToolboxItemAttribute.None))
                {
                    item = new System.Drawing.Design.ToolboxItem(toolType);
                }
            }
            else if (typeof(System.Drawing.Design.ToolboxItem).IsAssignableFrom(toolType))
            {
                try
                {
                    item = (System.Drawing.Design.ToolboxItem)Activator.CreateInstance(toolType, true);
                }
                catch
                {
                }
            }

            return item;
        }

        #endregion
    }


}
