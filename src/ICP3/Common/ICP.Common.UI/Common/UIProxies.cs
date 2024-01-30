using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIManagement;
using ICP.Common.ServiceInterface;
namespace ICP.Common.UI
{
    #region Commodity

    public partial class CommodityLayoutUIProxy : ILayoutUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CommodityLayoutUI";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Commodity Manage" : "品名管理";
            }
        }
        List<LayoutElement> elements;
        public virtual List<LayoutElement> Elements
        {
            get
            {
                if (elements == null)
                {
                    elements = new List<LayoutElement>();
                    GroupElement G1 = new GroupElement();
                    G1.ID = "G1";
                    G1.Height = 0.6f;
                    G1.Dock = DockStyle.Fill;
                    G1.PartLayout = PartLayout.Tab;
                    G1.TabAlignment = TabAlignment.Top;

                    elements.Add(G1);
                    UIProxyElement customer = new UIProxyElement();
                    //customer.ID = "Commodity";
                    customer.ID = "CommodityUIProxy";
                    customer.Group = "G1";
                    customer.IsMainPart = true;
                    customer.ProxyType = typeof(CommodityUIProxy);
                    customer.Dock = DockStyle.Fill;

                    elements.Add(customer);
                    UIProxyElement customerContact = new UIProxyElement();
                    //customerContact.ID = "Commoditydetails";
                    customerContact.ID = "CommodityEditUIProxy";
                    customerContact.Group = "G1";
                    customerContact.ProxyType = typeof(CommodityEditUIProxy);
                    customerContact.Dock = DockStyle.Fill;

                    elements.Add(customerContact);
                }
                return elements;
            }
        }
        List<UIConnection> connections;
        public List<UIConnection> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new List<UIConnection>();
                    UIConnection r;
                    r = new UIConnection();
                    //r.ParentProxy = "Commodity";
                    //r.ChildProxy = "Commoditydetails";
                    r.ParentProxy = "CommodityUIProxy";
                    r.ChildProxy = "CommodityEditUIProxy";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";

                    connections.Add(r);
                }
                return connections;
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    public partial class CommodityUIProxy : ITreeManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.Commodity";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Commodity Manage" : "品名管理";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return typeof(TransportFoundation.Commodity.CommoditySearchPart);
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;

                    pb = new PropertyBinding();
                    pb.PropertyName = "CName";
                    pb.HeaderText = LocalData.IsEnglish ? "CName" : "中文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);


                    pb = new PropertyBinding();
                    pb.PropertyName = "EName";
                    pb.HeaderText = LocalData.IsEnglish ? "EName" : "英文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;

                    _pbs.Add(pb);

                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateByName" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "创建时间";
                    pb.PropertyType = typeof(DateTime);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        public virtual string ParentFieldName
        {
            get
            {
                return "ParentID";
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Disuse";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 2;
                    a.Execute = DisuseData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.ToolTip = LocalData.IsEnglish ? "Add" : "新增";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                    //a = new UIAction();
                    //a.Name = "Edit";
                    //a.Icon = SysImages.None;
                    //a.Text = LocalData.IsEnglish ? "&Edit" : "编辑(&E)";
                    //a.Time = ActionTime.Click | ActionTime.DblClickItem;
                    //a.AppearStyle = ActionAppearStyle.ToolBar;
                    //a.Data = ActionData.Current;
                    //a.Index = 1;
                    //a.Execute = EditData;

                    //actions.Add(a);
                    a = new UIAction();
                    a.Name = "Drag";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "Drag" : "Drag";
                    a.Time = ActionTime.DataChanged;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = DragData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

        protected virtual bool AddData(object obj)
        {
            return true;
        }

        protected virtual bool DragData(object obj)
        {
            return true;
        }

    }

    public partial class CommodityEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CommodityEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "CommodityEdit" : "编辑品名";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(TransportFoundation.Commodity.CommodityEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;

                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }
        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj)
        {
            return true;
        }

    }

    #endregion

    #region  Container

    public partial class ContainerLayoutUIProxy : ILayoutUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.ContainerLayoutUI";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Container" : "箱型";
            }
        }
        List<LayoutElement> elements;
        public virtual List<LayoutElement> Elements
        {
            get
            {
                if (elements == null)
                {
                    elements = new List<LayoutElement>();
                    GroupElement G1 = new GroupElement();
                    G1.ID = "G1";
                    G1.Height = 0.6f;
                    G1.Dock = DockStyle.Fill;
                    G1.PartLayout = PartLayout.Tab;
                    G1.TabAlignment = TabAlignment.Top;

                    elements.Add(G1);
                    UIProxyElement customer = new UIProxyElement();
                    customer.ID = "Container";
                    customer.Group = "G1";
                    customer.IsMainPart = true;
                    customer.ProxyType = typeof(ContainerUIProxy);
                    customer.Dock = DockStyle.Fill;

                    elements.Add(customer);
                    UIProxyElement customerContact = new UIProxyElement();
                    customerContact.ID = "Containerdetails";
                    customerContact.Group = "G1";
                    customerContact.ProxyType = typeof(ContainerEditUIProxy);
                    customerContact.Dock = DockStyle.Fill;

                    elements.Add(customerContact);
                }
                return elements;
            }
        }
        List<UIConnection> connections;
        public List<UIConnection> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new List<UIConnection>();
                    UIConnection r;
                    r = new UIConnection();
                    r.ParentProxy = "Container";
                    r.ChildProxy = "Containerdetails";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";

                    connections.Add(r);
                }
                return connections;
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    public partial class ContainerUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.Container";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Container Manage" : "箱型管理";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return typeof(TransportFoundation.Container.ContainerSearchPart);
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "Code";
                    pb.HeaderText = LocalData.IsEnglish ? "Code" : "代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "ISOCode";
                    pb.HeaderText = LocalData.IsEnglish ? "ISOCode" : "国际代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "TEU";
                    pb.HeaderText = LocalData.IsEnglish ? "TEU" : "箱量";
                    pb.PropertyType = typeof(decimal);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateByName" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "创建时间";
                    pb.PropertyType = typeof(DateTime);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Disuse";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 2;
                    a.Execute = DisuseData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

        protected virtual bool AddData(object obj)
        {
            return true;
        }
    }

    public partial class ContainerEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.ContainerEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "ContainerEdit" : "编辑箱型";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(TransportFoundation.Container.ContainerEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;

                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }
        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj)
        {
            return true;
        }

    }

    #endregion

    #region DataDictionary

    public partial class DataDictionaryLayoutUIProxy : ILayoutUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.DataDictionaryLayoutUI";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Data Dictionary" : "数据字典";
            }
        }
        List<LayoutElement> elements;
        public virtual List<LayoutElement> Elements
        {
            get
            {
                if (elements == null)
                {
                    elements = new List<LayoutElement>();
                    GroupElement G1 = new GroupElement();
                    G1.ID = "G1";
                    G1.Height = 0.6f;
                    G1.Dock = DockStyle.Fill;
                    G1.PartLayout = PartLayout.Tab;
                    G1.TabAlignment = TabAlignment.Top;

                    elements.Add(G1);
                    UIProxyElement customer = new UIProxyElement();
                    customer.ID = "datadictionary";
                    customer.Group = "G1";
                    customer.IsMainPart = true;
                    customer.ProxyType = typeof(DataDictionaryUIProxy);
                    customer.Dock = DockStyle.Fill;

                    elements.Add(customer);
                    UIProxyElement customerContact = new UIProxyElement();
                    customerContact.ID = "datadictionarydetails";
                    customerContact.Group = "G1";
                    customerContact.ProxyType = typeof(DataDictionaryEditUIProxy);
                    customerContact.Dock = DockStyle.Fill;

                    elements.Add(customerContact);
                }
                return elements;
            }
        }
        List<UIConnection> connections;
        public List<UIConnection> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new List<UIConnection>();
                    UIConnection r;
                    r = new UIConnection();
                    r.ParentProxy = "datadictionary";
                    r.ChildProxy = "datadictionarydetails";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";

                    connections.Add(r);
                }
                return connections;
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    public partial class DataDictionaryUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.DataDictionary";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "DataDictionary Manage" : "字典信息";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return typeof(TransportFoundation.DataDictionary.DataDictionarySearchPart);
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "Code";
                    pb.HeaderText = LocalData.IsEnglish ? "Code" : "代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CName";
                    pb.HeaderText = LocalData.IsEnglish ? "CName" : "中文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;

                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "EName";
                    pb.HeaderText = LocalData.IsEnglish ? "EName" : "英文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;




                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "TypeName";
                    pb.HeaderText = LocalData.IsEnglish ? "Type" : "类型";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateByName" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "创建时间";
                    pb.PropertyType = typeof(DateTime);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Disuse";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 2;
                    a.Execute = DisuseData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                    //a = new UIAction();
                    //a.Name = "Edit";
                    //a.Icon = SysImages.None;
                    //a.Text = LocalData.IsEnglish ? "&Edit" : "编辑(&E)";
                    //a.Time = ActionTime.Click | ActionTime.DblClickItem;
                    //a.AppearStyle = ActionAppearStyle.ToolBar;
                    //a.Data = ActionData.Current;
                    //a.Index = 1;
                    //a.Execute = EditData;

                    //actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

        protected virtual bool AddData(object obj)
        {
            return true;
        }

    }

    public partial class DataDictionaryEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.DataDictionaryEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "DataDictionaryEdit" : "编辑数据字典";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(TransportFoundation.DataDictionary.DataDictionaryEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;

                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }
        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj)
        {
            return true;
        }
    }

    #endregion

    #region Flight

    public partial class FlightLayoutUIProxy : ILayoutUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.FlightLayoutUI";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Flight" : "航班管理";
            }
        }
        List<LayoutElement> elements;
        public virtual List<LayoutElement> Elements
        {
            get
            {
                if (elements == null)
                {
                    elements = new List<LayoutElement>();
                    GroupElement G1 = new GroupElement();
                    G1.ID = "G1";
                    G1.Height = 0.6f;
                    G1.Dock = DockStyle.Fill;
                    G1.PartLayout = PartLayout.Tab;
                    G1.TabAlignment = TabAlignment.Top;

                    elements.Add(G1);
                    UIProxyElement customer = new UIProxyElement();
                    customer.ID = "Flight";
                    customer.Group = "G1";
                    customer.IsMainPart = true;
                    customer.ProxyType = typeof(FlightUIProxy);
                    customer.Dock = DockStyle.Fill;

                    elements.Add(customer);
                    UIProxyElement customerContact = new UIProxyElement();
                    customerContact.ID = "Flightdetails";
                    customerContact.Group = "G1";
                    customerContact.ProxyType = typeof(FlightEditUIProxy);
                    customerContact.Dock = DockStyle.Fill;

                    elements.Add(customerContact);
                }
                return elements;
            }
        }
        List<UIConnection> connections;
        public List<UIConnection> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new List<UIConnection>();
                    UIConnection r;
                    r = new UIConnection();
                    r.ParentProxy = "Flight";
                    r.ChildProxy = "Flightdetails";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";

                    connections.Add(r);
                }
                return connections;
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    public partial class FlightUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.Flight";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Flight Manage" : "航班管理";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return typeof(TransportFoundation.Flight.FlightSearchPart);
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "AirlineName";
                    pb.HeaderText = LocalData.IsEnglish ? "AirlineName" : "航空公司";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "No";
                    pb.HeaderText = LocalData.IsEnglish ? "No" : "航班号";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;

                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateByName" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;

                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "创建时间";
                    pb.PropertyType = typeof(DateTime);
                    pb.ColumnWidth = 60;

                    _pbs.Add(pb);
                }

                return _pbs;
            }
        }

        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Disuse";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 2;
                    a.Execute = DisuseData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

        protected virtual bool AddData(object obj)
        {
            return true;
        }

    }

    public partial class FlightEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.FlightEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "FlightEdit" : "编辑航班";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(TransportFoundation.Flight.FlightEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;

                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }
        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj)
        {
            return true;
        }

    }

    #endregion

    #region ShippingLine

    public partial class ShippingLineLayoutUIProxy : ILayoutUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.ShippingLineLayoutUI";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "ShippingLine Manage" : "航线管理";
            }
        }
        List<LayoutElement> elements;
        public virtual List<LayoutElement> Elements
        {
            get
            {
                if (elements == null)
                {
                    elements = new List<LayoutElement>();
                    GroupElement G1 = new GroupElement();
                    G1.ID = "G1";
                    G1.Height = 0.6f;
                    G1.Dock = DockStyle.Fill;
                    G1.PartLayout = PartLayout.Tab;
                    G1.TabAlignment = TabAlignment.Top;

                    elements.Add(G1);
                    UIProxyElement customer = new UIProxyElement();
                    customer.ID = "ShippingLine";
                    customer.Group = "G1";
                    customer.IsMainPart = true;
                    customer.ProxyType = typeof(ShippingLineUIProxy);
                    customer.Dock = DockStyle.Fill;

                    elements.Add(customer);
                    UIProxyElement customerContact = new UIProxyElement();
                    customerContact.ID = "ShippingLinedetails";
                    customerContact.Group = "G1";
                    customerContact.ProxyType = typeof(ShippingLineEditUIProxy);
                    customerContact.Dock = DockStyle.Fill;

                    elements.Add(customerContact);
                }
                return elements;
            }
        }
        List<UIConnection> connections;
        public List<UIConnection> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new List<UIConnection>();
                    UIConnection r;
                    r = new UIConnection();
                    r.ParentProxy = "ShippingLine";
                    r.ChildProxy = "ShippingLinedetails";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";

                    connections.Add(r);
                }
                return connections;
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    public partial class ShippingLineUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.ShippingLine";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "ShippingLine Manage" : "航线管理";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return typeof(TransportFoundation.ShippingLine.ShippingLineSearchPart);
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "Code";
                    pb.HeaderText = LocalData.IsEnglish ? "Code" : "代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "EName";
                    pb.HeaderText = LocalData.IsEnglish ? "EName" : "英文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CName";
                    pb.HeaderText = LocalData.IsEnglish ? "CName" : "中文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateByName" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "创建时间";
                    pb.PropertyType = typeof(DateTime);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Disuse";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 2;
                    a.Execute = DisuseData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

        protected virtual bool AddData(object obj)
        {
            return true;
        }

    }

    public partial class ShippingLineEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.ShippingLineEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "ShippingLineEdit" : "编辑航线";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(TransportFoundation.ShippingLine.ShippingLineEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;

                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }
        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj)
        {
            return true;
        }

    }

    #endregion

    #region TransportClause

    public partial class TransportClauseLayoutUIProxy : ILayoutUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.TransportClauseLayoutUI";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "TransportClause Manage" : "运输条款";
            }
        }
        List<LayoutElement> elements;
        public virtual List<LayoutElement> Elements
        {
            get
            {
                if (elements == null)
                {
                    elements = new List<LayoutElement>();
                    GroupElement G1 = new GroupElement();
                    G1.ID = "G1";
                    G1.Height = 0.6f;
                    G1.Dock = DockStyle.Fill;
                    G1.PartLayout = PartLayout.Tab;
                    G1.TabAlignment = TabAlignment.Top;

                    elements.Add(G1);
                    UIProxyElement transportclause = new UIProxyElement();
                    transportclause.ID = "transportclause";
                    transportclause.Group = "G1";
                    transportclause.IsMainPart = true;
                    transportclause.ProxyType = typeof(TransportClauseUIProxy);
                    transportclause.Dock = DockStyle.Fill;
                    elements.Add(transportclause);

                    UIProxyElement transportclauseEdit = new UIProxyElement();
                    transportclauseEdit.ID = "transportclauseEdit";
                    transportclauseEdit.Group = "G1";
                    transportclauseEdit.ProxyType = typeof(TransportClauseEditUIProxy);
                    transportclauseEdit.Dock = DockStyle.Fill;
                    elements.Add(transportclauseEdit);
                }
                return elements;
            }
        }
        List<UIConnection> connections;
        public List<UIConnection> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new List<UIConnection>();
                    UIConnection r;
                    r = new UIConnection();
                    r.ParentProxy = "transportclause";
                    r.ChildProxy = "transportclauseEdit";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";

                    connections.Add(r);
                }
                return connections;
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    public partial class TransportClauseUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.TransportClause";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "TransportClause Manage" : "运输条款";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return typeof(TransportFoundation.TransportClause.TransportClauseSearchPart);
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "OriginalCode";
                    pb.HeaderText = LocalData.IsEnglish ? "OriginalCode" : "起始地";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "DestinationCode";
                    pb.HeaderText = LocalData.IsEnglish ? "DestinationCode" : "目的地";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateByName" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "创建时间";
                    pb.PropertyType = typeof(DateTime);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Disuse";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 2;
                    a.Execute = DisuseData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

        protected virtual bool AddData(object obj)
        {
            return true;
        }

    }

    public partial class TransportClauseEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.TransportClauseEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "TransportClauseEdit" : "编辑运输条款";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(TransportFoundation.TransportClause.TransportClauseEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;

                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }
        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj)
        {
            return true;
        }

    }

    #endregion

    #region CountryProvince

    public partial class CountryProvinceLayoutUIProxy : ILayoutUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CountryProvinceLayoutUI";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Country Province" : "国家管理";
            }
        }
        List<LayoutElement> elements;
        public virtual List<LayoutElement> Elements
        {
            get
            {
                if (elements == null)
                {
                    elements = new List<LayoutElement>();
                    GroupElement G1 = new GroupElement();
                    G1.ID = "G1";
                    G1.Height = 0.6f;
                    G1.Dock = DockStyle.Fill;
                    G1.PartLayout = PartLayout.Tab;
                    G1.TabAlignment = TabAlignment.Top;

                    elements.Add(G1);
                    UIProxyElement customer = new UIProxyElement();
                    customer.ID = "CountryProvince";
                    customer.Group = "G1";
                    customer.IsMainPart = true;
                    customer.ProxyType = typeof(CountryProvinceUIProxy);
                    customer.Dock = DockStyle.Fill;

                    elements.Add(customer);
                    UIProxyElement customerContact = new UIProxyElement();
                    customerContact.ID = "CountryProvincedetails";
                    customerContact.Group = "G1";
                    customerContact.ProxyType = typeof(CountryProvinceEditUIProxy);
                    customerContact.Dock = DockStyle.Fill;

                    elements.Add(customerContact);
                }
                return elements;
            }
        }
        List<UIConnection> connections;
        public List<UIConnection> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new List<UIConnection>();
                    UIConnection r;
                    r = new UIConnection();
                    r.ParentProxy = "CountryProvince";
                    r.ChildProxy = "CountryProvincedetails";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";

                    connections.Add(r);
                }
                return connections;
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    public partial class CountryProvinceUIProxy : ITreeManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CountryProvince";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Country Province" : "国家管理";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return typeof(Geography.CountryProvince.CountryProvinceSearchPart);
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "Code";
                    pb.HeaderText = LocalData.IsEnglish ? "Code" : "代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CName";
                    pb.HeaderText = LocalData.IsEnglish ? "CName" : "中文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;

                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "EName";
                    pb.HeaderText = LocalData.IsEnglish ? "EName" : "英文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;

                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "Create By" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 40;

                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "Create Date" : "创建时间";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 40;

                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        public virtual string ParentFieldName
        {
            get
            {
                return "ParentID";
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Disuse";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 3;
                    a.Execute = DisuseData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "AddCountry";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add Country" : "新增国家(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddCountryData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "AddProvince";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "Add &ProvinceData" : "新增省/州(&P)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 1;
                    a.Execute = AddProvinceData;

                    actions.Add(a);

                    a = new UIAction();
                    a.Name = "Drag";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "Drag" : "Drag";
                    a.Time = ActionTime.DataChanged;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = DragData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

        protected virtual bool AddCountryData(object obj)
        {
            return true;
        }

        protected virtual bool AddProvinceData(object obj)
        {
            return true;
        }

        protected virtual bool DragData(object obj)
        {
            return true;
        }

    }

    public partial class CountryProvinceEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CountryProvinceEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "CountryProvinceEdit" : "编辑地理";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(Geography.CountryProvince.CountryProvinceEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;

                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }
        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj)
        {
            return true;
        }

    }

    #endregion

    #region Location

    public partial class LocationLayoutUIProxy : ILayoutUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.LocationLayoutUI";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Location Manage" : "地点信息";
            }
        }
        List<LayoutElement> elements;
        public virtual List<LayoutElement> Elements
        {
            get
            {
                if (elements == null)
                {
                    elements = new List<LayoutElement>();
                    GroupElement G1 = new GroupElement();
                    G1.ID = "G1";
                    G1.Height = 0.6f;
                    G1.Dock = DockStyle.Fill;
                    G1.PartLayout = PartLayout.Tab;
                    G1.TabAlignment = TabAlignment.Top;

                    elements.Add(G1);
                    UIProxyElement customer = new UIProxyElement();
                    customer.ID = "Location";
                    customer.Group = "G1";
                    customer.IsMainPart = true;
                    customer.ProxyType = typeof(LocationUIProxy);
                    customer.Dock = DockStyle.Fill;

                    elements.Add(customer);
                    UIProxyElement customerContact = new UIProxyElement();
                    customerContact.ID = "Locationdetails";
                    customerContact.Group = "G1";
                    customerContact.ProxyType = typeof(LocationEditUIProxy);
                    customerContact.Dock = DockStyle.Fill;

                    elements.Add(customerContact);
                }
                return elements;
            }
        }
        List<UIConnection> connections;
        public List<UIConnection> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new List<UIConnection>();
                    UIConnection r;
                    r = new UIConnection();
                    r.ParentProxy = "Location";
                    r.ChildProxy = "Locationdetails";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";

                    connections.Add(r);
                }
                return connections;
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    public partial class LocationUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.Location";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Location Manage" : "地点信息";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return typeof(Geography.Location.LocationSearchPart);
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "Code";
                    pb.HeaderText = LocalData.IsEnglish ? "Code" : "代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 50;





                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CName";
                    pb.HeaderText = LocalData.IsEnglish ? "CName" : "中文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "EName";
                    pb.HeaderText = LocalData.IsEnglish ? "EName" : "英文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "IsOcean";
                    pb.HeaderText = LocalData.IsEnglish ? "IsOcean" : "海运";
                    pb.PropertyType = typeof(bool);
                    pb.ColumnWidth = 45;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "IsAir";
                    pb.HeaderText = LocalData.IsEnglish ? "IsAir" : "空运";
                    pb.PropertyType = typeof(bool);
                    pb.ColumnWidth = 45;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "IsOther";
                    pb.HeaderText = LocalData.IsEnglish ? "IsOther" : "其他";
                    pb.PropertyType = typeof(bool);
                    pb.ColumnWidth = 45;

                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CountryProvinceName";
                    pb.HeaderText = LocalData.IsEnglish ? "CountryProvinceName" : "地区";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "Create By" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;

                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "Create Date" : "创建时间";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;

                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Disuse";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 2;
                    a.Execute = DisuseData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

        protected virtual bool AddData(object obj)
        {
            return true;
        }

    }

    public partial class LocationEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.LocationEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "LocationEdit" : "编辑地点";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(Geography.Location.LocationEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;

                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }
        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj)
        {
            return true;
        }

    }

    public partial class LocationDataFinderUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.LocationDataFinder";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Location Finder" : "地点搜索器";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "Code";
                    pb.HeaderText = LocalData.IsEnglish ? "Code" : "代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = LocalData.IsEnglish ? "EName" : "CName";
                    pb.HeaderText = LocalData.IsEnglish ? "Name" : "名称";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CountryProvinceName";
                    pb.HeaderText = LocalData.IsEnglish ? "Country/Province" : "地区";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "IsOcean";
                    pb.HeaderText = "海运";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 30;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "IsAir";
                    pb.HeaderText = "空运";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 30;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "IsOther";
                    pb.HeaderText = "其它";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 30;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Edit";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Edit" : "编辑(&E)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 1;
                    a.Execute = EditData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool AddData(object obj)
        {
            return true;
        }

        protected virtual bool EditData(object obj)
        {
            return true;
        }

    }

    #endregion

    #region CustomerLayout

    public partial class CustomerLayoutUIProxy : ILayoutUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CustomerLayout";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Customer Manage" : "客户管理";
            }
        }
        List<LayoutElement> elements;
        public virtual List<LayoutElement> Elements
        {
            get
            {
                if (elements == null)
                {
                    elements = new List<LayoutElement>();

                    GroupElement G1 = new GroupElement();
                    G1.ID = "G1";
                    G1.Height = 0.7f;
                    G1.Dock = DockStyle.Fill;
                    G1.PartLayout = PartLayout.Tab;
                    G1.TabAlignment = TabAlignment.Top;
                    elements.Add(G1);

                    UIProxyElement customer = new UIProxyElement();
                    customer.ID = "customer";
                    customer.Group = "G1";
                    customer.IsMainPart = true;
                    customer.ProxyType = typeof(CustomerUIProxy);
                    customer.Dock = DockStyle.Fill;
                    elements.Add(customer);

                    UIProxyElement customerEdit = new UIProxyElement();
                    customerEdit.ID = "customerEdit";
                    customerEdit.Group = "G1";
                    customerEdit.ProxyType = typeof(CustomerEditUIProxy);
                    customerEdit.Dock = DockStyle.Fill;
                    elements.Add(customerEdit);

                    UIProxyElement customerContact = new UIProxyElement();
                    customerContact.ID = "customerContact";
                    customerContact.Group = "G1";
                    customerContact.ProxyType = typeof(CustomerContactUIProxy);
                    customerContact.Dock = DockStyle.Fill;
                    elements.Add(customerContact);

                    UIProxyElement customerpartner = new UIProxyElement();
                    customerpartner.ID = "customerpartner";
                    customerpartner.Group = "G1";
                    customerpartner.ProxyType = typeof(CustomerPartnerUIProxy);
                    customerpartner.Dock = DockStyle.Fill;
                    elements.Add(customerpartner);

                    UIProxyElement customermemo = new UIProxyElement();
                    customermemo.ID = "customermemo";
                    customermemo.Group = "G1";
                    customermemo.ProxyType = typeof(CustomerMemoUIProxy);
                    customermemo.Dock = DockStyle.Fill;
                    elements.Add(customermemo);
                }
                return elements;
            }
        }
        List<UIConnection> connections;
        public List<UIConnection> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new List<UIConnection>();
                    UIConnection r;

                    r = new UIConnection();
                    r.ParentProxy = "customer";
                    r.ChildProxy = "customerEdit";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";
                    connections.Add(r);

                    r = new UIConnection();
                    r.ParentProxy = "customer";
                    r.ChildProxy = "customerContact";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";
                    connections.Add(r);

                    r = new UIConnection();
                    r.ParentProxy = "customer";
                    r.ChildProxy = "customerpartner";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";
                    connections.Add(r);

                    r = new UIConnection();
                    r.ParentProxy = "customer";
                    r.ChildProxy = "customermemo";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";
                    connections.Add(r);
                }
                return connections;
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    public partial class CustomerUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.Customer";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Customer Manage" : "客户管理";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return typeof(Customer.CustomerSearchPart);
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "Code";
                    pb.HeaderText = LocalData.IsEnglish ? "Code" : "代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "KeyWord";
                    pb.HeaderText = LocalData.IsEnglish ? "KeyWord" : "关键字";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "EShortName";
                    pb.HeaderText = LocalData.IsEnglish ? "EShortName" : "英文简称";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "EName";
                    pb.HeaderText = LocalData.IsEnglish ? "EName" : "英文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CName";
                    pb.HeaderText = LocalData.IsEnglish ? "CName" : "中文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "Tel1";
                    pb.HeaderText = LocalData.IsEnglish ? "Tel" : "电话";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 80;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "Fax";
                    pb.HeaderText = LocalData.IsEnglish ? "Fax" : "传真";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 80;


                    _pbs.Add(pb);

                    pb = new PropertyBinding();
                    pb.PropertyName = "CountryProvinceName";
                    pb.HeaderText = LocalData.IsEnglish ? "Location" : "地点";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 50;


                    _pbs.Add(pb);


                    pb = new PropertyBinding();
                    pb.PropertyName = "TypeDescription";
                    pb.HeaderText = LocalData.IsEnglish ? "Type" : "类型";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 50;


                    _pbs.Add(pb);

                    pb = new PropertyBinding();
                    pb.PropertyName = "CheckedStateDescription";
                    pb.HeaderText = LocalData.IsEnglish ? "State" : "状态";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);

                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateByName" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "创建时间";
                    pb.PropertyType = typeof(DateTime);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);

                    a = new UIAction();
                    a.Name = "Disuse";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 2;
                    a.Execute = DisuseData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool AddData(object obj)
        {
            return true;
        }
        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

    }

    public partial class CustomerEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CustomerEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "CustomerEdit" : "编辑客户";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.Customer.CustomerEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool SaveData(object obj) { return true; }

        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj) { return true; }

    }

    public partial class CustomerContactUIProxy : IListEditUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CustomerContact";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Contact" : "联系人";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "EName";
                    pb.HeaderText = LocalData.IsEnglish ? "EName" : "英文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;
                    pb.Editable = true;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CName";
                    pb.HeaderText = LocalData.IsEnglish ? "CName" : "中文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;
                    pb.Editable = true;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "Department";
                    pb.HeaderText = LocalData.IsEnglish ? "Department" : "部门";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 80;
                    pb.Editable = true;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "Position";
                    pb.HeaderText = LocalData.IsEnglish ? "Position" : "职位";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 80;
                    pb.Editable = true;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "Tel";
                    pb.HeaderText = LocalData.IsEnglish ? "Tel" : "电话";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 80;
                    pb.Editable = true;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "Fax";
                    pb.HeaderText = LocalData.IsEnglish ? "Fax" : "传真";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 80;
                    pb.Editable = true;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "Mobile";
                    pb.HeaderText = LocalData.IsEnglish ? "Mobile" : "手机号";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 80;
                    pb.Editable = true;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "EMail";
                    pb.HeaderText = LocalData.IsEnglish ? "EMail" : "邮件地址";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;
                    pb.Editable = true;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "Remark";
                    pb.HeaderText = LocalData.IsEnglish ? "Remark" : "备注";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;
                    pb.Editable = true;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateByName" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "创建时间";
                    pb.PropertyType = typeof(DateTime);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Disuse";
                    a.Icon = SysImages.Delete;
                    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Selected;
                    a.Index = 2;
                    a.Execute = DisuseData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Modifies;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }

        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj) { return true; }
    }

    public partial class CustomerPartnerUIProxy : IListEditUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CustomerPartner";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Partner" : "合作伙伴";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "PartnerName";
                    pb.HeaderText = LocalData.IsEnglish ? "PartnerName" : "名称";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;
                    pb.Editable = true;
                    pb.Editor = new DataFinderEditor { DataFinderName = "CustomerFinder", TextField = "PartnerName", ValueField = "PartnerID", ResultValueField = "ID", ResultTextField = LocalData.IsEnglish ? "EName" : "CName" };


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "PartnerKeyword";
                    pb.HeaderText = LocalData.IsEnglish ? "PartnerKeyword" : "关键字";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 80;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "PartnerCode";
                    pb.HeaderText = LocalData.IsEnglish ? "PartnerCode" : "代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 80;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "PartnerAddress";
                    pb.HeaderText = LocalData.IsEnglish ? "PartnerAddress" : "地址";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "PartnerTel";
                    pb.HeaderText = LocalData.IsEnglish ? "PartnerTel" : "电话";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 80;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateByName" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "创建时间";
                    pb.PropertyType = typeof(DateTime);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Delete";
                    a.Icon = SysImages.Delete;
                    a.Text = LocalData.IsEnglish ? "&Delete" : "删除(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Selected;
                    a.Index = 1;
                    a.Execute = DeleteData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Modifies;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool DeleteData(object obj)
        {
            return true;
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }

        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj) { return true; }

    }

    public partial class CustomerMemoUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CustomerMemo";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Memo" : "备注";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "Subject";
                    pb.HeaderText = LocalData.IsEnglish ? "Subject" : "主题";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "Content";
                    pb.HeaderText = LocalData.IsEnglish ? "Content" : "内容";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 80;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "Type";
                    pb.HeaderText = LocalData.IsEnglish ? "Type" : "类型";
                    pb.PropertyType = typeof(MemoType);
                    pb.ColumnWidth = 80;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateByName" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "创建时间";
                    pb.PropertyType = typeof(DateTime);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Edit";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Edit" : "编辑(&E)";
                    a.Time = ActionTime.Click | ActionTime.DblClickItem;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 1;
                    a.Execute = EditData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Delete";
                    a.Icon = SysImages.Delete;
                    a.Text = LocalData.IsEnglish ? "&Delete" : "删除(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Selected;
                    a.Index = 2;
                    a.Execute = DeleteData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool AddData(object obj)
        {
            return true;
        }

        protected virtual bool EditData(object obj)
        {
            return true;
        }

        protected virtual bool DeleteData(object obj)
        {
            return true;
        }

        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj) { return true; }

    }

    public partial class CustomerMemoEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CustomerMemoEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "MemoEdit" : "编辑备注";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.Customer.CustomerMemoEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool SaveData(object obj)
        {
            return true;
        }
    }

    public partial class CustomerDataFinderUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CustomerDataFinder";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Customer Finder" : "客户搜索器";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "Code";
                    pb.HeaderText = LocalData.IsEnglish ? "Code" : "代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "EShortName";
                    pb.HeaderText = LocalData.IsEnglish ? "EShortName" : "英文简称";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "EName";
                    pb.HeaderText = LocalData.IsEnglish ? "EName" : "英文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CName";
                    pb.HeaderText = LocalData.IsEnglish ? "CName" : "中文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "Tel";
                    pb.HeaderText = LocalData.IsEnglish ? "Tel" : "电话";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 80;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "Fax";
                    pb.HeaderText = LocalData.IsEnglish ? "Fax" : "传真";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 80;


                    _pbs.Add(pb);

                    pb = new PropertyBinding();
                    pb.PropertyName = "CountryProvinceName";
                    pb.HeaderText = LocalData.IsEnglish ? "Location" : "地点";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 50;


                    _pbs.Add(pb);


                    pb = new PropertyBinding();
                    pb.PropertyName = "TypeDescription";
                    pb.HeaderText = LocalData.IsEnglish ? "Type" : "类型";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 50;


                    _pbs.Add(pb);

                    pb = new PropertyBinding();
                    pb.PropertyName = "CheckedStateDescription";
                    pb.HeaderText = LocalData.IsEnglish ? "State" : "状态";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Add";
                    a.Action = "ADD_CUSTOMER";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Edit";
                    a.Action = "EDIT_CUSTOMER";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Edit" : "编辑(&E)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 1;
                    a.Execute = EditData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool AddData(object obj)
        {
            return true;
        }

        protected virtual bool EditData(object obj)
        {
            return true;
        }

    }

    #endregion


    #region VesselVoyageLayout

    #region Vessel

    public partial class VesselLayoutUIProxy : ILayoutUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.VesselLayoutUIProxy";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Vessel" : "船名";
            }
        }
        List<LayoutElement> elements;
        public virtual List<LayoutElement> Elements
        {
            get
            {
                if (elements == null)
                {
                    elements = new List<LayoutElement>();

                    GroupElement G1 = new GroupElement();
                    G1.ID = "G1";
                    G1.Height = 0.4f;
                    G1.Dock = DockStyle.Fill;
                    G1.PartLayout = PartLayout.Tab;
                    G1.TabAlignment = TabAlignment.Top;
                    elements.Add(G1);

                    UIProxyElement vessel = new UIProxyElement();
                    vessel.ID = "vessel";
                    vessel.Group = "G1";
                    vessel.IsMainPart = true;
                    vessel.ProxyType = typeof(VesselUIProxy);
                    vessel.Dock = DockStyle.Fill;
                    elements.Add(vessel);

                    UIProxyElement vesselEdit = new UIProxyElement();
                    vesselEdit.ID = "vesselEdit";
                    vesselEdit.Group = "G1";
                    vesselEdit.Height = 0.4f;
                    vesselEdit.ProxyType = typeof(VesselEditUIProxy);
                    vesselEdit.Dock = DockStyle.Fill;
                    elements.Add(vesselEdit);
                }
                return elements;
            }
        }
        List<UIConnection> connections;
        public List<UIConnection> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new List<UIConnection>();
                    UIConnection r;
                    r = new UIConnection();
                    r.ParentProxy = "vessel";
                    r.ChildProxy = "vesselEdit";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";
                    connections.Add(r);
                }
                return connections;
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    public partial class VesselUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.Vessel";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Vessel Manage" : "船名管理";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return typeof(TransportFoundation.VesselVoyage.VesselSearchPart);
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "Code";
                    pb.HeaderText = LocalData.IsEnglish ? "Code" : "代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "Name";
                    pb.HeaderText = LocalData.IsEnglish ? "Name" : "名称";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CarrierName";
                    pb.HeaderText = LocalData.IsEnglish ? "CarrierName" : "船东";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateByName" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "创建时间";
                    pb.PropertyType = typeof(DateTime);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Disuse";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 2;
                    a.Execute = DisuseData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

        protected virtual bool AddData(object obj)
        {
            return true;
        }
    }

    public partial class VesselEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.VesselEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "VesselEdit" : "编辑船名";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.TransportFoundation.VesselVoyage.VesselEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;

                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }
        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj) { return true; }

    }

    #endregion

    #region Voyage航次

    public partial class VoyageLayoutUIProxy : ILayoutUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.VoyageLayoutUIProxy";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Voyage" : "航次";
            }
        }
        List<LayoutElement> elements;
        public virtual List<LayoutElement> Elements
        {
            get
            {
                if (elements == null)
                {
                    elements = new List<LayoutElement>();

                    GroupElement G1 = new GroupElement();
                    G1.ID = "G1";
                    G1.Height = 0.6f;
                    G1.Dock = DockStyle.Fill;
                    G1.PartLayout = PartLayout.Tab;
                    G1.TabAlignment = TabAlignment.Top;
                    elements.Add(G1);

                    UIProxyElement vessel = new UIProxyElement();
                    vessel.ID = "VoyageUIProxy";
                    vessel.Group = "G1";
                    vessel.IsMainPart = true;
                    vessel.ProxyType = typeof(VoyageUIProxy);
                    vessel.Dock = DockStyle.Fill;
                    elements.Add(vessel);

                    UIProxyElement vesselEdit = new UIProxyElement();
                    vesselEdit.ID = "VoyageEditUIProxy";
                    vesselEdit.Group = "G1";
                    vesselEdit.Height = 0.7f;
                    vesselEdit.ProxyType = typeof(VoyageEditUIProxy);
                    vesselEdit.Dock = DockStyle.Fill;
                    elements.Add(vesselEdit);
                }
                return elements;
            }
        }
        List<UIConnection> connections;
        public List<UIConnection> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new List<UIConnection>();
                    UIConnection r;
                    r = new UIConnection();
                    r.ParentProxy = "VoyageUIProxy";
                    r.ChildProxy = "VoyageEditUIProxy";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";
                    connections.Add(r);
                }
                return connections;
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    public partial class VoyageUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.Voyage";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return false;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Voyage" : "航次";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return typeof(TransportFoundation.VesselVoyage.VoyageSearchPart);
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;

                    pb = new PropertyBinding();
                    pb.PropertyName = "No";
                    pb.HeaderText = LocalData.IsEnglish ? "No" : "航次";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 200;
                    _pbs.Add(pb);

                    pb = new PropertyBinding();
                    pb.PropertyName = "VesselName";
                    pb.HeaderText = LocalData.IsEnglish ? "VesselName" : "船名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 200;
                    _pbs.Add(pb);


                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateByName" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 150;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "创建时间";
                    pb.PropertyType = typeof(DateTime);
                    pb.ColumnWidth = 150;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;

                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;
                    actions.Add(a);

                    a = new UIAction();
                    a.Name = "Disuse";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 2;
                    a.Execute = DisuseData;

                    actions.Add(a);


                    a = new UIAction();
                    a.Name = "Copy";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Copy" : "拷贝(&O)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 3;
                    a.Execute = CopyData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

        protected virtual bool CopyData(object obj)
        {
            return true;
        }

        protected virtual bool AddData(object obj)
        {
            return true;
        }

    }
    public partial class VoyageEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.VoyageEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "VoyageEdit" : "编辑航次";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.TransportFoundation.VesselVoyage.VoyageEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;

                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }
        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj) { return true; }

    }

    #endregion

    public partial class VesselVoyageDataFinderUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.VesselVoyageDataFinder";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "VesselVoyage Finder" : "船名航次搜索器";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "No";
                    pb.HeaderText = LocalData.IsEnglish ? "NO" : "航次";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "VesselName";
                    pb.HeaderText = LocalData.IsEnglish ? "Vessel" : "船名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;

                    _pbs.Add(pb);

                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Add";
                    a.Action = "ADD_VESSELVOYAGE";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Edit";
                    a.Action = "EDIT_VESSELVOYAGE";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Edit" : "编辑(&E)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 1;
                    a.Execute = EditData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool AddData(object obj)
        {
            return true;
        }

        protected virtual bool EditData(object obj)
        {
            return true;
        }

    }

    #endregion

    #region ChargingLayout

    public partial class ChargingLayoutUIProxy : ILayoutUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.ChargingLayout";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "ChargingCode" : "费用代码";
            }
        }
        List<LayoutElement> elements;
        public virtual List<LayoutElement> Elements
        {
            get
            {
                if (elements == null)
                {
                    elements = new List<LayoutElement>();

                    GroupElement G1 = new GroupElement();
                    G1.ID = "G1";
                    G1.Height = 1f;
                    G1.Dock = DockStyle.Fill;
                    G1.PartLayout = PartLayout.Horizontal;
                    //G1.TabAlignment = TabAlignment.Top;
                    elements.Add(G1);


                    GroupElement G2 = new GroupElement();
                    G2.ID = "G2";
                    G2.Parent = "G1";
                    G2.Height = 1f;
                    G2.Dock = DockStyle.Fill;
                    G2.Width = 0.7f;
                    G2.PartLayout = PartLayout.Vertical;
                    elements.Add(G2);


                    UIProxyElement charginggroup = new UIProxyElement();
                    charginggroup.ID = "charginggroup";
                    charginggroup.ProxyType = typeof(ChargingGroupUIProxy);
                    charginggroup.Group = "G1";
                    charginggroup.Dock = DockStyle.Left;
                    charginggroup.Width = 0.4f;
                    elements.Add(charginggroup);

                    UIProxyElement chargingcode = new UIProxyElement();
                    chargingcode.ID = "chargingcode";
                    chargingcode.Group = "G2";
                    chargingcode.IsMainPart = true;
                    //chargingcode.Title = "Temp";
                    chargingcode.ProxyType = typeof(ChargingCodeUIProxy);
                    chargingcode.Dock = DockStyle.Fill;
                    elements.Add(chargingcode);

                    UIProxyElement chargingcodeEdit = new UIProxyElement();
                    chargingcodeEdit.ID = "chargingcodeEdit";
                    chargingcodeEdit.Group = "G2";
                    chargingcodeEdit.Height = 0.4f;
                    chargingcodeEdit.ProxyType = typeof(ChargingCodeEditUIProxy);
                    chargingcodeEdit.Dock = DockStyle.Bottom;
                    elements.Add(chargingcodeEdit);
                }
                return elements;
            }
        }
        List<UIConnection> connections;
        public List<UIConnection> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new List<UIConnection>();
                    UIConnection r;
                    r = new UIConnection();
                    r.ParentProxy = "charginggroup";
                    r.ChildProxy = "chargingcode";
                    r.InvokeMethod = "GroupChanged";
                    connections.Add(r);

                    r = new UIConnection();
                    r.ParentProxy = "chargingcode";
                    r.ChildProxy = "chargingcodeEdit";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";
                    connections.Add(r);

                }
                return connections;
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    public partial class ChargingGroupUIProxy : ITreeManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.ChargingGroup";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "ChargingCode" : "费用代码";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "CName";
                    pb.HeaderText = LocalData.IsEnglish ? "Name" : "名称";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 300;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        public virtual string ParentFieldName
        {
            get
            {
                return "ParentID";
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Refresh";
                    a.Icon = SysImages.Load;
                    a.Text = LocalData.IsEnglish ? "&Refresh" : "刷新(&R)";
                    a.Time = ActionTime.Click | ActionTime.Showed;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = RefreshData;

                    actions.Add(a);

                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Edit";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Edit" : "编辑(&E)";
                    a.Time = ActionTime.Click | ActionTime.DblClickItem;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 1;
                    a.Execute = EditData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Delete";
                    a.Icon = SysImages.Delete;
                    a.Text = LocalData.IsEnglish ? "&Delete" : "删除(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Selected;
                    a.Index = 2;
                    a.Execute = DeleteData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Drag";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "Drag" : "Drag";
                    a.Time = ActionTime.DataChanged;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = DragData;

                    actions.Add(a);

                }

                return actions;
            }
        }
        protected virtual bool AddData(object obj)
        {
            return true;
        }

        protected virtual bool EditData(object obj)
        {
            return true;
        }

        protected virtual bool DeleteData(object obj)
        {
            return true;
        }

        protected virtual bool DragData(object obj)
        {
            return true;
        }

        protected virtual bool RefreshData(object obj)
        {
            return true;
        }

    }

    public partial class ChargingGroupEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.ChargingGroupEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "ChargingGroupEdit" : "编辑费用代码组";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.Configure.ChargingCode.ChargingGroupEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool SaveData(object obj)
        {
            return true;
        }

    }

    public partial class ChargingCodeUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.ChargingCode";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Code" : "代码";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "Code";
                    pb.HeaderText = LocalData.IsEnglish ? "Code" : "代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CName";
                    pb.HeaderText = LocalData.IsEnglish ? "CName" : "中文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "EName";
                    pb.HeaderText = LocalData.IsEnglish ? "EName" : "英文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "IsCommission";
                    pb.HeaderText = LocalData.IsEnglish ? "IsCommission" : "佣金";
                    pb.PropertyType = typeof(bool);
                    pb.ColumnWidth = 40;

                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "IsValid";
                    pb.HeaderText = LocalData.IsEnglish ? "IsValid" : "有效";
                    pb.PropertyType = typeof(bool);
                    pb.ColumnWidth = 40;

                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateByName" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "创建时间";
                    pb.PropertyType = typeof(DateTime);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;

                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);


                    a = new UIAction();
                    a.Name = "Disuse";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 1;
                    a.Execute = DisuseData;

                    actions.Add(a);


                    a = new UIAction();
                    a.Name = "Close";
                    a.Icon = SysImages.Exit;
                    a.Text = LocalData.IsEnglish ? "&Close" : "关闭(&C)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 2;
                    a.Execute = Close;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool Close(object obj)
        {
            return true;
        }

        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

        protected virtual bool AddData(object obj)
        {
            return true;
        }

        protected virtual bool GroupChanged(object obj)
        {
            return true;
        }

    }

    public partial class ChargingCodeEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }

        public virtual string Name
        {
            get
            {
                return "ICP.Common.ChargingCodeEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "ChargingCodeEdit" : "编辑费用代码";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.Configure.ChargingCode.ChargingCodeEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;

                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }
        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj)
        {
            return true;
        }

    }


    #region 费用搜索器
    public partial class ChargingCodeDataFinderUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.ChargingCodeDataFinder";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Code Finder" : "代码搜索器";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "Code";
                    pb.HeaderText = LocalData.IsEnglish ? "Code" : "代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 0;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CName";
                    pb.HeaderText = LocalData.IsEnglish ? "CName" : "中文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 0;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "EName";
                    pb.HeaderText = LocalData.IsEnglish ? "EName" : "英文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 0;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Edit";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Edit" : "编辑(&E)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 1;
                    a.Execute = EditData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool AddData(object obj)
        {
            return true;
        }

        protected virtual bool EditData(object obj)
        {
            return true;
        }

    }

    #endregion

    #endregion

    #region Currency

    public partial class CurrencyLayoutUIProxy : ILayoutUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CurrencyLayoutUI";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Currency Manage" : "币种管理";
            }
        }
        List<LayoutElement> elements;
        public virtual List<LayoutElement> Elements
        {
            get
            {
                if (elements == null)
                {
                    elements = new List<LayoutElement>();
                    GroupElement G1 = new GroupElement();
                    G1.ID = "G1";
                    G1.Height = 0.6f;
                    G1.Dock = DockStyle.Fill;
                    G1.PartLayout = PartLayout.Tab;
                    G1.TabAlignment = TabAlignment.Top;

                    elements.Add(G1);
                    UIProxyElement customer = new UIProxyElement();
                    customer.ID = "Currency";
                    customer.Group = "G1";
                    customer.IsMainPart = true;
                    customer.ProxyType = typeof(CurrencyUIProxy);
                    customer.Dock = DockStyle.Fill;

                    elements.Add(customer);
                    UIProxyElement customerContact = new UIProxyElement();
                    customerContact.ID = "Currencydetails";
                    customerContact.Group = "G1";
                    customerContact.ProxyType = typeof(CurrencyEditUIProxy);
                    customerContact.Dock = DockStyle.Fill;

                    elements.Add(customerContact);
                }
                return elements;
            }
        }
        List<UIConnection> connections;
        public List<UIConnection> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new List<UIConnection>();
                    UIConnection r;
                    r = new UIConnection();
                    r.ParentProxy = "Currency";
                    r.ChildProxy = "Currencydetails";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";

                    connections.Add(r);
                }
                return connections;
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    public partial class CurrencyUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.Currency";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Currency Manage" : "币种管理";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return typeof(Configure.Currency.CurrencySearchPart);
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "Code";
                    pb.HeaderText = LocalData.IsEnglish ? "Code" : "代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 50;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CName";
                    pb.HeaderText = LocalData.IsEnglish ? "CName" : "中文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "EName";
                    pb.HeaderText = LocalData.IsEnglish ? "EName" : "英文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CountryName";
                    pb.HeaderText = LocalData.IsEnglish ? "CountryName" : "国家名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateByName" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "创建时间";
                    pb.PropertyType = typeof(DateTime);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Disuse";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 2;
                    a.Execute = DisuseData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

        protected virtual bool AddData(object obj)
        {
            return true;
        }

    }

    public partial class CurrencyEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CurrencyEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "CurrencyEdit" : "编辑币种";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.Configure.Currency.CurrencyEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;

                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }
        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj)
        {
            return true;
        }

    }

    public partial class CurrencyDataFinderUIProxy : IListUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CurrencyDataFinder";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Currency Finder" : "币种搜索器";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "Code";
                    pb.HeaderText = "Code";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = LocalData.IsEnglish ? "EName" : "CName";
                    pb.HeaderText = LocalData.IsEnglish ? "Name" : "名称";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    #endregion

    #region TerminalLogins

    public partial class TerminalLoginsLayoutUIProxy : ILayoutUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.TerminalLoginsLayoutUI";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "TerminalLogins Manage" : "码头账户管理";
            }
        }
        List<LayoutElement> elements;
        public virtual List<LayoutElement> Elements
        {
            get
            {
                if (elements == null)
                {
                    elements = new List<LayoutElement>();
                    GroupElement G1 = new GroupElement();
                    G1.ID = "G1";
                    G1.Height = 0.6f;
                    G1.Dock = DockStyle.Fill;
                    G1.PartLayout = PartLayout.Tab;
                    G1.TabAlignment = TabAlignment.Top;

                    elements.Add(G1);
                    UIProxyElement logins = new UIProxyElement();
                    logins.ID = "TerminalLogins";
                    logins.Group = "G1";
                    logins.IsMainPart = true;
                    logins.ProxyType = typeof(TerminalLoginsUIProxy);
                    logins.Dock = DockStyle.Fill;

                    elements.Add(logins);
                    UIProxyElement loginsDetail = new UIProxyElement();
                    loginsDetail.ID = "TerminalLoginsDetails";
                    loginsDetail.Group = "G1";
                    loginsDetail.ProxyType = typeof(TerminalLoginsEditUIProxy);
                    loginsDetail.Dock = DockStyle.Fill;

                    elements.Add(loginsDetail);
                }
                return elements;
            }
        }
        List<UIConnection> connections;
        public List<UIConnection> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new List<UIConnection>();
                    UIConnection r;
                    r = new UIConnection();
                    r.ParentProxy = "TerminalLogins";
                    r.ChildProxy = "TerminalLoginsDetails";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";

                    connections.Add(r);
                }
                return connections;
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    public partial class TerminalLoginsUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.TerminalLogins";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "TerminalLogins Manage" : "码头账户管理";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return typeof(Configure.TerminalLogins.TerminalLoginsSearchPart);
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "Code";
                    pb.HeaderText = LocalData.IsEnglish ? "Code" : "代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 50;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "UserID";
                    pb.HeaderText = LocalData.IsEnglish ? "UserID" : "用户名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 50;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "Password";
                    pb.HeaderText = LocalData.IsEnglish ? "Password" : "密码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 20;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "UpdateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "UpdateBy" : "更新人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 20;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "UpdateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "UpdateDate" : "更新时间";
                    pb.PropertyType = typeof(DateTime);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                //if (actions == null)
                //{
                //    actions = new List<UIAction>();
                //    UIAction a;
                //    a = new UIAction();
                //    a.Name = "Disuse";
                //    a.Icon = SysImages.None;
                //    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                //    a.Time = ActionTime.Click;
                //    a.AppearStyle = ActionAppearStyle.ToolBar;
                //    a.Data = ActionData.Current;
                //    a.Index = 2;
                //    a.Execute = DisuseData;

                //    actions.Add(a);
                //    a = new UIAction();
                //    a.Name = "Add";
                //    a.Icon = SysImages.Add;
                //    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                //    a.Time = ActionTime.Click;
                //    a.AppearStyle = ActionAppearStyle.ToolBar;
                //    a.Data = ActionData.NULL;
                //    a.Index = 0;
                //    a.Execute = AddData;

                //    actions.Add(a);
                //}

                return actions;
            }
        }
        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

        protected virtual bool AddData(object obj)
        {
            return true;
        }

    }

    public partial class TerminalLoginsEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.TerminalLoginsEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "TerminalLoginsEdit" : "编辑码头账户";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.Configure.TerminalLogins.TerminalLoginsEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;

                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }
        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj)
        {
            return true;
        }

    }

    public partial class TerminalLoginsDataFinderUIProxy : IListUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.TerminalLoginsDataFinder";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "TerminalLogins Finder" : "码头账户搜索器";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "Code";
                    pb.HeaderText = "Code";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 50;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = LocalData.IsEnglish ? "UserID" : "用户名";
                    pb.HeaderText = LocalData.IsEnglish ? "UserID" : "用户名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 50;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    #endregion

    #region SolutionLayout

    public partial class SolutionLayoutUIProxy : ILayoutUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.SolutionLayout";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Solution" : "解决方案";
            }
        }
        List<LayoutElement> elements;
        public virtual List<LayoutElement> Elements
        {
            get
            {
                if (elements == null)
                {
                    elements = new List<LayoutElement>();
                    GroupElement G1 = new GroupElement();
                    G1.ID = "G1";
                    G1.Height = 0.5f;
                    G1.Dock = DockStyle.Fill;
                    G1.PartLayout = PartLayout.Tab;
                    G1.TabAlignment = TabAlignment.Top;
                    elements.Add(G1);

                    UIProxyElement solution = new UIProxyElement();
                    solution.ID = "SolutionUIProxy";
                    solution.Group = "G1";
                    solution.IsMainPart = true;
                    solution.ProxyType = typeof(SolutionUIProxy);
                    solution.Dock = DockStyle.Fill;
                    elements.Add(solution);

                    UIProxyElement solutionedit = new UIProxyElement();
                    solutionedit.ID = "SolutionEditUIProxy";
                    solutionedit.Group = "G1";
                    solutionedit.ProxyType = typeof(SolutionEditUIProxy);
                    solutionedit.Dock = DockStyle.Fill;
                    elements.Add(solutionedit);

                    UIProxyElement solutioncoderule = new UIProxyElement();
                    solutioncoderule.ID = "SolutionCodeRuleUIProxy";
                    solutioncoderule.Group = "G1";
                    solutioncoderule.ProxyType = typeof(SolutionCodeRuleUIProxy);
                    solutioncoderule.Dock = DockStyle.Fill;
                    elements.Add(solutioncoderule);

                    UIProxyElement solutioncurrency = new UIProxyElement();
                    solutioncurrency.ID = "SolutionCurrencyUIProxy";
                    solutioncurrency.Group = "G1";
                    solutioncurrency.ProxyType = typeof(SolutionCurrencyUIProxy);
                    solutioncurrency.Dock = DockStyle.Fill;
                    elements.Add(solutioncurrency);

                    UIProxyElement solutionexchangerate = new UIProxyElement();
                    solutionexchangerate.ID = "SolutionExchangeRateUIProxy";
                    solutionexchangerate.Group = "G1";
                    solutionexchangerate.ProxyType = typeof(SolutionExchangeRateUIProxy);
                    solutionexchangerate.Dock = DockStyle.Fill;
                    elements.Add(solutionexchangerate);

                    UIProxyElement solutionagent = new UIProxyElement();
                    solutionagent.ID = "SolutionAgentUIProxy";
                    solutionagent.Group = "G1";
                    solutionagent.ProxyType = typeof(SolutionAgentUIProxy);
                    solutionagent.Dock = DockStyle.Fill;
                    elements.Add(solutionagent);

                    GroupElement G3 = new GroupElement();
                    G3.ID = "G3";
                    G3.Title = LocalData.IsEnglish ? "Charging Code" : "费用代码";
                    G3.Parent = "G1";
                    G3.Dock = DockStyle.Fill;
                    G3.PartLayout = PartLayout.Horizontal;
                    elements.Add(G3);

                    GroupElement G2 = new GroupElement();
                    G2.ID = "G2";
                    G2.Title = LocalData.IsEnglish ? "GL" : "会计科目";
                    G2.Parent = "G1";
                    G2.Dock = DockStyle.Fill;
                    G2.PartLayout = PartLayout.Horizontal;
                    elements.Add(G2);


                    UIProxyElement solutionchargingcode = new UIProxyElement();
                    solutionchargingcode.ID = "SolutionChargingCodeUIProxy";
                    solutionchargingcode.Group = "G3";
                    solutionchargingcode.ProxyType = typeof(SolutionChargingCodeUIProxy);
                    solutionchargingcode.Dock = DockStyle.Fill;
                    elements.Add(solutionchargingcode);

                    UIProxyElement solutioncharginggroup = new UIProxyElement();
                    solutioncharginggroup.ID = "ChargingGroupUIProxy";
                    solutioncharginggroup.Group = "G3";
                    solutioncharginggroup.ProxyType = typeof(ChargingGroupUIProxy);
                    solutioncharginggroup.Dock = DockStyle.Left;
                    solutioncharginggroup.Width = 0.3f;
                    elements.Add(solutioncharginggroup);


                    UIProxyElement solutionglcode = new UIProxyElement();
                    solutionglcode.ID = "SolutionGLCodeUIProxy";
                    solutionglcode.Group = "G2";
                    solutionglcode.ProxyType = typeof(SolutionGLCodeUIProxy);
                    solutionglcode.Dock = DockStyle.Fill;
                    elements.Add(solutionglcode);


                    //
                    UIProxyElement solutionglgroup = new UIProxyElement();
                    solutionglgroup.ID = "SolutionGLGroupUIProxy";
                    solutionglgroup.Group = "G2";
                    solutionglgroup.ProxyType = typeof(SolutionGLGroupUIProxy);
                    solutionglgroup.Dock = DockStyle.Left;
                    solutionglgroup.Width = 0.3f;
                    elements.Add(solutionglgroup);

                    UIProxyElement solutionglconfig = new UIProxyElement();
                    solutionglconfig.ID = "SolutionGLConfigUIProxy";
                    solutionglconfig.Group = "G1";
                    solutionglconfig.ProxyType = typeof(SolutionGLConfigUIProxy);
                    solutionglconfig.Dock = DockStyle.Fill;
                    elements.Add(solutionglconfig);
                }
                return elements;
            }
        }
        List<UIConnection> connections;
        public List<UIConnection> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new List<UIConnection>();
                    UIConnection r;

                    r = new UIConnection();
                    r.ParentProxy = "SolutionUIProxy";
                    r.ChildProxy = "SolutionEditUIProxy";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";
                    connections.Add(r);

                    r = new UIConnection();
                    r.ParentProxy = "SolutionUIProxy";
                    r.ChildProxy = "SolutionCodeRuleUIProxy";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";
                    connections.Add(r);

                    r = new UIConnection();
                    r.ParentProxy = "SolutionUIProxy";
                    r.ChildProxy = "SolutionCurrencyUIProxy";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";
                    connections.Add(r);

                    r = new UIConnection();
                    r.ParentProxy = "SolutionUIProxy";
                    r.ChildProxy = "SolutionExchangeRateUIProxy";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";
                    connections.Add(r);

                    r = new UIConnection();
                    r.ParentProxy = "SolutionUIProxy";
                    r.ChildProxy = "SolutionAgentUIProxy";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";
                    connections.Add(r);

                    r = new UIConnection();
                    r.ParentProxy = "ChargingGroupUIProxy";
                    r.ChildProxy = "SolutionChargingCodeUIProxy";
                    r.InvokeMethod = "GroupChanged";
                    connections.Add(r);

                    r = new UIConnection();
                    r.ParentProxy = "SolutionUIProxy";
                    r.ChildProxy = "SolutionChargingCodeUIProxy";
                    r.InvokeMethod = "SolutionChanged";
                    connections.Add(r);

                    r = new UIConnection();
                    r.ParentProxy = "SolutionGLGroupUIProxy";
                    r.ChildProxy = "SolutionGLCodeUIProxy";
                    r.PreInvokeMethod = "BeforeGLGroupChanged"; r.InvokeMethod = "GLGroupChanged";
                    connections.Add(r);

                    r = new UIConnection();
                    r.ParentProxy = "SolutionUIProxy";
                    r.ChildProxy = "SolutionGLCodeUIProxy";
                    r.PreInvokeMethod = "BeforeSolutionChanged"; r.InvokeMethod = "SolutionChanged";
                    connections.Add(r);

                    r = new UIConnection();
                    r.ParentProxy = "SolutionUIProxy";
                    r.ChildProxy = "SolutionGLConfigUIProxy";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";
                    connections.Add(r);

                    r = new UIConnection();
                    r.ParentProxy = "SolutionUIProxy";
                    r.ChildProxy = "SolutionChargingCodeUIProxy";
                    r.PreInvokeMethod = "SolutionChanged"; r.InvokeMethod = "ParentChanged";
                    connections.Add(r);
                }
                return connections;
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    public partial class SolutionUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.SolutionUIProxy";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Solution" : "解决方案";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "CName";
                    pb.HeaderText = LocalData.IsEnglish ? "CName" : "中文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "EName";
                    pb.HeaderText = LocalData.IsEnglish ? "EName" : "英文名";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "InvoiceDateTypeName";
                    pb.HeaderText = LocalData.IsEnglish ? "DateType" : "日期类型";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateByName" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "创建时间";
                    pb.PropertyType = typeof(DateTime);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Refresh";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Refresh" : "刷新(&R)";
                    a.Time = ActionTime.Click | ActionTime.Showed;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 3;
                    a.Execute = Refresh;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Disuse";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 2;
                    a.Execute = DisuseData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool Refresh(object obj)
        {
            return true;
        }

        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

        protected virtual bool AddData(object obj)
        {
            return true;
        }
    }

    public partial class SolutionEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.SolutionEditUIProxy";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "SolutionEdit" : "编辑解决方案";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.Configure.Solution.SolutionEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;

                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }
        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj) { return true; }

    }

    public partial class SolutionCodeRuleUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.SolutionCodeRuleUIProxy";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "CodeRule Manage" : "代码规则";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.Configure.Solution.SolutionCodeRuleListPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                }

                return actions;
            }
        }

        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj) { return true; }

    }

    public partial class SolutionCurrencyUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.SolutionCurrencyUIProxy";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Currency" : "币种";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.Configure.Solution.SolutionCurrencyListPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                }

                return actions;
            }
        }

        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj) { return true; }

    }

    public partial class SolutionExchangeRateUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.SolutionExchangeRateUIProxy";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "ExchangeRate" : "汇率";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.Configure.Solution.SolutionExchangeRateListPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                }

                return actions;
            }
        }

        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj) { return true; }

    }

    public partial class SolutionAgentUIProxy : IListEditUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.SolutionAgentUIProxy";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Agent" : "代理";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "AgentName";
                    pb.HeaderText = LocalData.IsEnglish ? "Agent" : "代理";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 400;
                    pb.Editable = true;
                    pb.Editor = new DataFinderEditor { DataFinderName = "CustomerForwardingFinder", TextField = "AgentName", SearchField = "AgentName", ValueField = "AgentID", ResultValueField = "ID", ResultTextField = LocalData.IsEnglish ? "EName" : "CName", AllowInput = true };

                    //pb.Editor = new DataFinderEditor { DataFinderName = "ChargingCodeFinder", TextField = "ChargingCodeName", SearchField = "ChargingCodeName", ValueField = "ChargingCodeID", ResultValueField = "ID", ResultTextField = LocalData.IsEnglish ? "EName" : "CName", AllowInput = true };


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "Remark";
                    pb.HeaderText = LocalData.IsEnglish ? "Remark" : "备注";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;
                    pb.Editable = true;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Disuse";
                    a.Icon = SysImages.Delete;
                    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Selected;
                    a.Index = 2;
                    a.Execute = DisuseData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Modifies;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }

        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj) { return true; }

    }
    /// <summary>
    /// 解决方案 Child Table费用代码分组
    /// </summary>
    public partial class SolutionChargingGropUIProxy : ITreeManageUIProxy, IMultiple
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.SolutionChargingGropUIProxy";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "ChargingGroup" : "分组";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = LocalData.IsEnglish ? "EName" : "CName";
                    pb.HeaderText = LocalData.IsEnglish ? "Name" : "名称";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 0;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        public virtual string ParentFieldName
        {
            get
            {
                return "ParentID";
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Refresh";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Refresh" : "刷新(&R)";
                    a.Time = ActionTime.Click | ActionTime.Showed;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = RefreshData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool RefreshData(object obj)
        {
            return true;
        }

    }
    /// <summary>
    /// 解决方案 Child Table费用代码
    /// </summary>
    public partial class SolutionChargingCodeUIProxy : IListEditUIProxy, IMultiple
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.SolutionChargingCodeUIProxy";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "ChargingCode" : "费用代码";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "ChargingCodeName";
                    pb.HeaderText = LocalData.IsEnglish ? "ChargingCode" : "费用代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;
                    pb.Editable = true;
                    pb.Editor = new DataFinderEditor { DataFinderName = CommonFinderConstants.ChargingCodeInfoFinder, TextField = "ChargingCodeName", SearchField = "ChargingCodeName", ValueField = "ChargingCodeID", ResultValueField = "ChargingCodeID", ResultTextField = LocalData.IsEnglish ? "ChargingCodeName" : "ChargingCodeName", AllowInput = true };

                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "IsAgent";
                    pb.HeaderText = LocalData.IsEnglish ? "IsAgent" : "代理";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;
                    pb.Editable = true;

                    _pbs.Add(pb);
                }

                return _pbs;
            }
        }

        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Delete";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Delete" : "删除(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 1;
                    a.Execute = DeleteData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool DeleteData(object obj)
        {
            return true;
        }

        protected virtual bool SolutionChanged(object obj)
        {
            return true;
        }

        protected virtual bool GroupChanged(object obj)
        {
            return true;
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }

    }

    public partial class SolutionGLGroupUIProxy : ITreeManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.SolutionGLGroupUIProxy";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "GL Group" : "目录";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = LocalData.IsEnglish ? "EName" : "CName";
                    pb.HeaderText = LocalData.IsEnglish ? "Name" : "名称";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        public virtual string ParentFieldName
        {
            get
            {
                return "ParentID";
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Edit";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Edit" : "编辑(&E)";
                    a.Time = ActionTime.Click | ActionTime.DblClickItem;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 1;
                    a.Execute = EditData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Delete";
                    a.Icon = SysImages.Delete;
                    a.Text = LocalData.IsEnglish ? "&Delete" : "删除(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Selected;
                    a.Index = 2;
                    a.Execute = DeleteData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Drag";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "Drag" : "Drag";
                    a.Time = ActionTime.DataChanged;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = Drag;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Refresh";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Refresh" : "刷新(&R)";
                    a.Time = ActionTime.Click | ActionTime.Showed;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 3;
                    a.Execute = Refresh;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool AddData(object obj)
        {
            return true;
        }

        protected virtual bool EditData(object obj)
        {
            return true;
        }

        protected virtual bool DeleteData(object obj)
        {
            return true;
        }

        protected virtual bool Drag(object obj)
        {
            return true;
        }

        protected virtual bool Refresh(object obj)
        {
            return true;
        }

    }

    public partial class SolutionGLGroupEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.SolutionGLGroupEditUIProxy";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "SolutionGLGroupEdit" : "编辑目录";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.Configure.Solution.SolutionGLGroupEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool SaveData(object obj)
        {
            return true;
        }

    }

    public partial class SolutionGLCodeUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.SolutionGLCodeUIProxy";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "GL Code" : "会计科目代码";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.Configure.Solution.SolutionGLCodeListPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                }

                return actions;
            }
        }

        protected virtual bool BeforeGLGroupChanged(object obj) { return true; }
        protected virtual bool GLGroupChanged(object obj) { return true; }

        protected virtual bool BeforeSolutionChanged(object obj) { return true; }
        protected virtual bool SolutionChanged(object obj) { return true; }

    }

    public partial class SolutionGLConfigUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.SolutionGLConfigUIProxy";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "GL Config" : "会计科目配置";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.Configure.Solution.SolutionGLConfigListPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                }

                return actions;
            }
        }

        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj) { return true; }

    }

    #endregion

    #region SolutionChargeCodeFinder

    public partial class SolutionChargeCodeFinderUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.SolutionChargeCodeFinderUIProxy";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? " ChargeCode Finder" : "费用项目搜索器";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return typeof(ICP.Common.UI.Configure.Solution.SoluionChargeCodeSearchPart);
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;

                    pb = new PropertyBinding();
                    pb.PropertyName = "Code";
                    pb.HeaderText = LocalData.IsEnglish ? "Code" : "代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 50;
                    _pbs.Add(pb);


                    pb = new PropertyBinding();
                    pb.PropertyName = "ChargingCodeName";
                    pb.HeaderText = LocalData.IsEnglish ? "Name" : "名称";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 50;
                    _pbs.Add(pb);

                    pb = new PropertyBinding();
                    pb.PropertyName = "IsAgent";
                    pb.HeaderText = LocalData.IsEnglish ? "IsAgent" : "代理";
                    pb.PropertyType = typeof(bool);
                    pb.ColumnWidth = 100;
                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                }

                return actions;
            }
        }
    }

    #endregion

    #region CommpanyConfigure

    public partial class CommpanyConfigureLayoutUIProxy : ILayoutUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CommpanyConfigureLayoutUI";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Commpany Configure" : "公司配置";
            }
        }
        List<LayoutElement> elements;
        public virtual List<LayoutElement> Elements
        {
            get
            {
                if (elements == null)
                {
                    elements = new List<LayoutElement>();
                    GroupElement G1 = new GroupElement();
                    G1.ID = "G1";
                    G1.Height = 0.7f;
                    G1.Dock = DockStyle.Fill;
                    G1.PartLayout = PartLayout.Tab;
                    G1.TabAlignment = TabAlignment.Top;

                    elements.Add(G1);
                    UIProxyElement customer = new UIProxyElement();
                    customer.ID = "CommpanyConfigure";
                    customer.Group = "G1";
                    customer.IsMainPart = true;
                    customer.ProxyType = typeof(CommpanyConfigureUIProxy);
                    customer.Dock = DockStyle.Fill;

                    elements.Add(customer);
                    UIProxyElement customerContact = new UIProxyElement();
                    customerContact.ID = "CommpanyConfiguredetails";
                    customerContact.Group = "G1";
                    customerContact.ProxyType = typeof(CommpanyConfigureEditUIProxy);
                    customerContact.Dock = DockStyle.Fill;

                    elements.Add(customerContact);
                }
                return elements;
            }
        }
        List<UIConnection> connections;
        public List<UIConnection> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new List<UIConnection>();
                    UIConnection r;
                    r = new UIConnection();
                    r.ParentProxy = "CommpanyConfigure";
                    r.ChildProxy = "CommpanyConfiguredetails";
                    r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";

                    connections.Add(r);
                }
                return connections;
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }

    public partial class CommpanyConfigureUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CommpanyConfigure";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Commpany Configure" : "公司配置";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "CompanyName";
                    pb.HeaderText = LocalData.IsEnglish ? "CompanyName" : "公司";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "ShortCode";
                    pb.HeaderText = LocalData.IsEnglish ? "ShortCode" : "公司代码";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 50;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CustomerName";
                    pb.HeaderText = LocalData.IsEnglish ? "CustomerName" : "客户";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "SolutionName";
                    pb.HeaderText = LocalData.IsEnglish ? "SolutionName" : "解决方案";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "IssuePlaceName";
                    pb.HeaderText = LocalData.IsEnglish ? "IssuePlaceName" : "签发地";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "EndClosingDate";
                    pb.HeaderText = LocalData.IsEnglish ? "EndClosingDate" : "关帐日";
                    pb.PropertyType = typeof(DateTime?);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateByName";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateByName" : "创建人";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "CreateDate";
                    pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "创建时间";
                    pb.PropertyType = typeof(DateTime);
                    pb.ColumnWidth = 60;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Disuse";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 2;
                    a.Execute = DisuseData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);

                    a = new UIAction();
                    a.Name = "Refresh";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Refresh" : "刷新(&R)";
                    a.Time = ActionTime.Click | ActionTime.Showed;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 3;
                    a.Execute = Refresh;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool DisuseData(object obj)
        {
            return true;
        }

        protected virtual bool AddData(object obj)
        {
            return true;
        }

        protected virtual bool Refresh(object obj)
        {
            return true;
        }

    }

    public partial class CommpanyConfigureEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.CommpanyConfigureEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "CommpanyConfigureEdit" : "编辑公司配置";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.Configure.CommpanyConfigure.CommpanyConfigureEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;

                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }

        protected virtual bool SaveData(object obj)
        {
            return true;
        }
        protected virtual bool BeforeParentChanged(object obj) { return true; }
        protected virtual bool ParentChanged(object obj)
        {
            return true;
        }

    }

    #endregion

    #region ConfigureKeyValue

    public partial class ConfigureKeyValueUIProxy : IListManageUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.ConfigureKeyValue";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Configure Key" : "键值配置";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                    pb = new PropertyBinding();
                    pb.PropertyName = "ConfigureKeyName";
                    pb.HeaderText = LocalData.IsEnglish ? "ConfigureKeyName" : "键";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 100;


                    _pbs.Add(pb);
                    pb = new PropertyBinding();
                    pb.PropertyName = "Value";
                    pb.HeaderText = LocalData.IsEnglish ? "Value" : "值";
                    pb.PropertyType = typeof(string);
                    pb.ColumnWidth = 200;


                    _pbs.Add(pb);
                }
                return _pbs;
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Delete";
                    a.Icon = SysImages.Delete;
                    a.Text = LocalData.IsEnglish ? "&Delete" : "删除(&D)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 2;
                    a.Execute = DeleteData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Add";
                    a.Icon = SysImages.Add;
                    a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.NULL;
                    a.Index = 0;
                    a.Execute = AddData;

                    actions.Add(a);
                    a = new UIAction();
                    a.Name = "Edit";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Edit" : "编辑(&E)";
                    a.Time = ActionTime.Click | ActionTime.DblClickItem;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 1;
                    a.Execute = EditData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool DeleteData(object obj)
        {
            return true;
        }

        protected virtual bool AddData(object obj)
        {
            return true;
        }

        protected virtual bool EditData(object obj)
        {
            return true;
        }

    }

    public partial class ConfigureKeyValueEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.ConfigureKeyValueEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "Edit Configure Key" : "编辑键值配置";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.Configure.CommpanyConfigure.ConfigureKeyValueEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool SaveData(object obj)
        {
            return true;
        }

    }

    public partial class ConfigureKeyEditUIProxy : IDataUIProxy
    {
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        public virtual string Name
        {
            get
            {
                return "ICP.Common.ConfigureKeyEdit";
            }
        }
        public virtual bool AutoWidth
        {
            get
            {
                return true;
            }
        }
        public virtual string Title
        {
            get
            {
                return LocalData.IsEnglish ? "ConfigureKeyEdit" : "编辑公司配置";
            }
        }
        public virtual Type SearchPartType
        {
            get
            {
                return null;
            }
        }
        public virtual Type SimpleSearchPartType
        {
            get
            {
                return null;
            }
        }
        List<PropertyBinding> _pbs;
        public virtual List<PropertyBinding> DataBindings
        {
            get
            {
                if (_pbs == null)
                {
                    _pbs = new List<PropertyBinding>();
                    PropertyBinding pb;
                }
                return _pbs;
            }
        }
        IDataHoster _hoster;
        [ServiceDependency]
        public IDataHoster datahoster
        {
            set
            {
                _hoster = value;
                _hoster.DataContentType = typeof(ICP.Common.UI.Configure.CommpanyConfigure.CommpanyConfigureEditPart);
            }
        }
        List<UIAction> actions;
        public List<UIAction> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<UIAction>();
                    UIAction a;
                    a = new UIAction();
                    a.Name = "Save";
                    a.Icon = SysImages.None;
                    a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                    a.Time = ActionTime.Click;
                    a.AppearStyle = ActionAppearStyle.ToolBar;
                    a.Data = ActionData.Current;
                    a.Index = 0;
                    a.Execute = SaveData;

                    actions.Add(a);
                }

                return actions;
            }
        }
        protected virtual bool SaveData(object obj)
        {
            return true;
        }

    }

    #endregion
}
