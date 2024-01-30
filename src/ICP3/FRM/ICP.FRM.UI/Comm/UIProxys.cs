

namespace ICP.FRM.UI
{
    //public class OceanPriceLayoutUIProxy : ILayoutUIProxy
    //{
    //    [ServiceDependency]
    //    public WorkItem workitem { get; set; }
    //    public virtual string Name{get {return "ICP.FRM.OceanPriceLayoutUIProxy";}}
    //    public virtual bool AutoWidth { get { return true; } }
    //    public virtual string Title{get {return LocalData.IsEnglish ? "OceanPrice" : "海运运价";}}
    //    List<LayoutElement> elements;
    //    public virtual List<LayoutElement> Elements
    //    {
    //        get
    //        {
    //            if (elements == null)
    //            {
    //                elements = new List<LayoutElement>();
    //                GroupElement G1 = new GroupElement();
    //                G1.ID = "G1";
    //                G1.Height = 0.7f;
    //                G1.Dock = DockStyle.Fill;
    //                G1.PartLayout = PartLayout.Tab;
    //                G1.TabAlignment = TabAlignment.Top;
    //                elements.Add(G1);

    //                UIProxyElement OceanPriceUIProxy = new UIProxyElement();
    //                OceanPriceUIProxy.ID = "OceanPriceUIProxy";
    //                OceanPriceUIProxy.Group = "G1";
    //                OceanPriceUIProxy.IsMainPart = true;
    //                OceanPriceUIProxy.ProxyType = typeof(OceanPriceUIProxy);
    //                OceanPriceUIProxy.Dock = DockStyle.Fill;
    //                elements.Add(OceanPriceUIProxy);

    //                UIProxyElement OceanPriceEditUIProxy = new UIProxyElement();
    //                OceanPriceEditUIProxy.ID = "OceanPriceEditUIProxy";
    //                OceanPriceEditUIProxy.Group = "G1";
    //                OceanPriceEditUIProxy.ProxyType = typeof(OceanPriceEditUIProxy);
    //                OceanPriceEditUIProxy.Dock = DockStyle.Fill;
    //                elements.Add(OceanPriceEditUIProxy);

    //                UIProxyElement OceanPriceItemUIProxy = new UIProxyElement();
    //                OceanPriceItemUIProxy.ID = "OceanPriceItemUIProxy";
    //                OceanPriceItemUIProxy.Group = "G1";
    //                OceanPriceItemUIProxy.ProxyType = typeof(OceanPriceItemUIProxy);
    //                OceanPriceItemUIProxy.Dock = DockStyle.Fill;
    //                elements.Add(OceanPriceItemUIProxy);
    //                //
    //                UIProxyElement OceanPriceAdditionalUIProxy = new UIProxyElement();
    //                OceanPriceAdditionalUIProxy.ID = "OceanPriceAdditionalUIProxy";
    //                OceanPriceAdditionalUIProxy.Group = "G1";
    //                OceanPriceAdditionalUIProxy.ProxyType = typeof(OceanPriceAdditionalUIProxy);
    //                OceanPriceAdditionalUIProxy.Dock = DockStyle.Fill;
    //                elements.Add(OceanPriceAdditionalUIProxy);

    //                UIProxyElement OceanPriceFileListUIProxy = new UIProxyElement();
    //                OceanPriceFileListUIProxy.ID = "OceanPriceFileListUIProxy";
    //                OceanPriceFileListUIProxy.Group = "G1";
    //                OceanPriceFileListUIProxy.ProxyType = typeof(OceanPriceFileListUIProxy);
    //                OceanPriceFileListUIProxy.Dock = DockStyle.Fill;
    //                elements.Add(OceanPriceFileListUIProxy);
    //            }
    //            return elements;
    //        }
    //    }
    //    List<UIConnection> connections;
    //    public List<UIConnection> Connections
    //    {
    //        get
    //        {
    //            if (connections == null)
    //            {
    //                connections = new List<UIConnection>();
    //                UIConnection r;

    //                r = new UIConnection();
    //                r.ParentProxy = "OceanPriceUIProxy";
    //                r.ChildProxy = "OceanPriceEditUIProxy";
    //                r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";
    //                connections.Add(r);

    //                r = new UIConnection();
    //                r.ParentProxy = "OceanPriceUIProxy";
    //                r.ChildProxy = "OceanPriceItemUIProxy";
    //                r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";
    //                connections.Add(r);

    //                r = new UIConnection();
    //                r.ParentProxy = "OceanPriceUIProxy";
    //                r.ChildProxy = "OceanPriceAdditionalUIProxy";
    //                r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";
    //                connections.Add(r);

    //                r = new UIConnection();
    //                r.ParentProxy = "OceanPriceUIProxy";
    //                r.ChildProxy = "OceanPriceFileListUIProxy";
    //                r.PreInvokeMethod = "BeforeParentChanged"; r.InvokeMethod = "ParentChanged";
    //                connections.Add(r);
    //            }
    //            return connections;
    //        }
    //    }
    //    public virtual Type SearchPartType { get { return null; } }
    //    public virtual Type SimpleSearchPartType { get { return null; } }
    //    List<PropertyBinding> _pbs;
    //    public virtual List<PropertyBinding> DataBindings
    //    {
    //        get
    //        {
    //            if (_pbs == null)
    //            {
    //                _pbs = new List<PropertyBinding>();
    //                PropertyBinding pb;
    //            }
    //            return _pbs;
    //        }
    //    }
    //    List<UIAction> actions;
    //    public List<UIAction> Actions{get {return actions;}}
    //}

    //public partial class OceanPriceUIProxy : IListManageUIProxy
    //{
    //    [ServiceDependency]
    //    public WorkItem workitem { get; set; }
    //    public virtual string Name{get {return "ICP.FRM.OceanPriceUIProxy";}}
    //    public virtual bool AutoWidth { get { return false; } }
    //    public virtual string Title { get { return "OceanPrice"; } }
    //    public virtual Type SearchPartType{get{return typeof(ICP.FRM.UI.OceanPrice.OPSearchPart);}}
    //    public virtual Type SimpleSearchPartType { get { return null; } }
    //    List<PropertyBinding> _pbs;
    //    public virtual List<PropertyBinding> DataBindings
    //    {
    //        get
    //        {
    //            if (_pbs == null)
    //            {
    //                _pbs = new List<PropertyBinding>();
    //                PropertyBinding pb;
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "ContractName";
    //                pb.HeaderText = LocalData.IsEnglish ? "Name" : "合约名";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 100;
    //                _pbs.Add(pb);

    //                pb = new PropertyBinding();
    //                pb.PropertyName = "ShippingLineName";
    //                pb.HeaderText = LocalData.IsEnglish ? "ShippingLine" : "航线";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 100;
    //                _pbs.Add(pb);

    //                pb = new PropertyBinding();
    //                pb.PropertyName = "ContractNo";
    //                pb.HeaderText = LocalData.IsEnglish ? "ContractNo" : "合约号";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 100;
    //                _pbs.Add(pb);

    //                pb = new PropertyBinding();
    //                pb.PropertyName = "CarrierName";
    //                pb.HeaderText = LocalData.IsEnglish ? "Carrier" : "船东";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 120;
    //                _pbs.Add(pb);

    //                pb = new PropertyBinding();
    //                pb.PropertyName = "ConsigneeNames";
    //                pb.HeaderText = LocalData.IsEnglish ? "ConsigneeNames" : "收货人";
    //                pb.PropertyType = typeof(DateTime);
    //                pb.ColumnWidth = 120;
    //                _pbs.Add(pb);

    //                pb = new PropertyBinding();
    //                pb.PropertyName = "NotifyNames";
    //                pb.HeaderText = LocalData.IsEnglish ? "NotifyNames" : "通知人";
    //                pb.PropertyType = typeof(DateTime);
    //                pb.ColumnWidth = 120;
    //                _pbs.Add(pb);

    //                pb = new PropertyBinding();
    //                pb.PropertyName = "PaymentTermName";
    //                pb.HeaderText = LocalData.IsEnglish ? "PaymentTerm" : "运输条款";
    //                pb.PropertyType = typeof(DateTime);
    //                pb.ColumnWidth = 80;
    //                _pbs.Add(pb);

    //                pb = new PropertyBinding();
    //                pb.PropertyName = "CurrencyName";
    //                pb.HeaderText = LocalData.IsEnglish ? "Currency" : "币种";
    //                pb.PropertyType = typeof(DateTime);
    //                pb.ColumnWidth = 80;
    //                _pbs.Add(pb);

    //                pb = new PropertyBinding();
    //                pb.PropertyName = "ContractType";
    //                pb.HeaderText = LocalData.IsEnglish ? "Type" : "类型";
    //                pb.PropertyType = typeof(DateTime);
    //                pb.ColumnWidth = 80;
    //                _pbs.Add(pb);

    //                pb = new PropertyBinding();
    //                pb.PropertyName = "FromDate";
    //                pb.HeaderText = LocalData.IsEnglish ? "FromDate" : "开始日期";
    //                pb.PropertyType = typeof(DateTime);
    //                pb.ColumnWidth = 80;
    //                _pbs.Add(pb);

    //                pb = new PropertyBinding();
    //                pb.PropertyName = "ToDate";
    //                pb.HeaderText = LocalData.IsEnglish ? "ToDate" : "结束日期";
    //                pb.PropertyType = typeof(DateTime);
    //                pb.ColumnWidth = 80;
    //                _pbs.Add(pb);

    //                pb = new PropertyBinding();
    //                pb.PropertyName = "State";
    //                pb.HeaderText = LocalData.IsEnglish ? "State" : "状态";
    //                pb.PropertyType = typeof(DateTime);
    //                pb.ColumnWidth = 80;
    //                _pbs.Add(pb);

    //                pb = new PropertyBinding();
    //                pb.PropertyName = "PublisherName";
    //                pb.HeaderText = LocalData.IsEnglish ? "Publisher" : "发布人";
    //                pb.PropertyType = typeof(DateTime);
    //                pb.ColumnWidth = 80;
    //                _pbs.Add(pb);
    //            }
    //            return _pbs;
    //        }
    //    }
    //    List<UIAction> actions;
    //    public List<UIAction> Actions
    //    {
    //        get
    //        {
    //            if (actions == null)
    //            {
    //                actions = new List<UIAction>();

    //                UIAction a;
    //                a = new UIAction();
    //                a.Name = "New";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ?"&New":"新增(&N)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.NULL;
    //                a.Index = 0;
    //                a.Execute = NewData;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "Delete";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ?"&Delete":"删除(&D)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 1;
    //                a.Execute = DeleteDate;
    //                actions.Add(a);

    //                a = new UIAction();
    //                a.Name = "Copy";
    //                a.Icon = SysImages.Add;
    //                a.Text = LocalData.IsEnglish ?"&Copy":"复制(&C)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 2;
    //                a.Execute = CopyData;
    //                actions.Add(a);

    //                a = new UIAction();
    //                a.Name = "Pause";
    //                a.Icon = SysImages.Add;
    //                a.Text = LocalData.IsEnglish ? "Cancel(&P)" : "取消(&P)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 3;
    //                a.Execute = PauseData;
    //                actions.Add(a);

    //                a = new UIAction();
    //                a.Name = "Search";
    //                a.Icon = SysImages.Add;
    //                a.Text = LocalData.IsEnglish ?"Searc&h":"查询(&H)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.NULL;
    //                a.Index = 4;
    //                a.Execute = ShowSearch;
    //                actions.Add(a);

    //                a = new UIAction();
    //                a.Name = "Inquiery";
    //                a.Icon = SysImages.Add;
    //                a.Text = LocalData.IsEnglish ? "&Inquiery" : "询价(&I)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 5;
    //                a.Execute = InquieryData;
    //                actions.Add(a);

    //            }

    //            return actions;
    //        }
    //    }
    //    protected virtual bool NewData(object obj) { return true; }
    //    protected virtual bool DeleteDate(object obj) { return true; }
    //    protected virtual bool CopyData(object obj) { return true; }

    //    protected virtual bool InquieryData(object obj) { return true; }
    //    protected virtual bool ShowSearch(object obj) { return true; }
    //    protected virtual bool PauseData(object obj) { return true; }
    //}

    //public partial class OceanPriceEditUIProxy : IDataUIProxy
    //{
    //    [ServiceDependency]
    //    public WorkItem workitem { get; set; }
    //    public virtual string Name { get { return "ICP.FRM.OceanPriceEditUIProxy"; } }
    //    public virtual bool AutoWidth { get { return true; } }
    //    public virtual string Title { get { return LocalData.IsEnglish ? "Ocean Info" : "编辑合约"; ; } }
    //    public virtual Type SearchPartType { get { return null; } }
    //    public virtual Type SimpleSearchPartType { get { return null; } }
    //    List<PropertyBinding> _pbs;
    //    public virtual List<PropertyBinding> DataBindings
    //    {
    //        get
    //        {
    //            if (_pbs == null)
    //            {
    //                _pbs = new List<PropertyBinding>();
    //                PropertyBinding pb;
    //            }
    //            return _pbs;
    //        }
    //    }
    //    IDataHoster _hoster;
    //    [ServiceDependency]
    //    public IDataHoster datahoster
    //    {
    //        set
    //        {
    //            _hoster = value;
    //            _hoster.DataContentType = typeof(ICP.FRM.UI.OceanPrice.OPContractEditPart);
    //        }
    //    }
    //    List<UIAction> actions;
    //    public List<UIAction> Actions
    //    {
    //        get
    //        {
    //            if (actions == null)
    //            {
    //                actions = new List<UIAction>();
    //                UIAction a;

    //                a = new UIAction();
    //                a.Name = "Save";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 0;
    //                a.Execute = SaveData;

    //                actions.Add(a);
    //            }

    //            return actions;
    //        }
    //    }

    //    protected virtual bool SaveData(object obj) { return true; }
    //    protected virtual bool BeforeParentChanged(object obj) { return true; }
    //    protected virtual bool ParentChanged(object obj) { return true; }

    //}

    //public partial class OceanPriceItemUIProxy : IDataUIProxy
    //{
    //    [ServiceDependency]
    //    public WorkItem workitem { get; set; }
    //    public virtual string Name { get { return "ICP.FRM.OceanPriceItemUIProxy"; } }
    //    public virtual bool AutoWidth { get { return true; } }
    //    public virtual string Title { get { return LocalData.IsEnglish ? "Ocean Item" : "运价明细"; } }
    //    public virtual Type SearchPartType { get { return null; } }
    //    public virtual Type SimpleSearchPartType { get { return null; } }
    //    List<PropertyBinding> _pbs;
    //    public virtual List<PropertyBinding> DataBindings
    //    {
    //        get
    //        {
    //            if (_pbs == null)
    //            {
    //                _pbs = new List<PropertyBinding>();
    //                PropertyBinding pb;
    //            }
    //            return _pbs;
    //        }
    //    }
    //    IDataHoster _hoster;
    //    [ServiceDependency]
    //    public IDataHoster datahoster
    //    {
    //        set
    //        {
    //            _hoster = value;
    //            _hoster.DataContentType = typeof(ICP.FRM.UI.OceanPrice.OPOceanItemPart);
    //        }
    //    }
    //    List<UIAction> actions;
    //    public List<UIAction> Actions
    //    {
    //        get
    //        {
    //            if (actions == null)
    //            {
    //                actions = new List<UIAction>();
    //            }

    //            return actions;
    //        }
    //    }

    //    protected virtual bool BeforeParentChanged(object obj) { return true; }
    //    protected virtual bool ParentChanged(object obj) { return true; }

    //}

    //public partial class OceanPriceAdditionalUIProxy : IDataUIProxy
    //{
    //    [ServiceDependency]
    //    public WorkItem workitem { get; set; }
    //    public virtual string Name { get { return "ICP.FRM.OceanPriceAdditionalUIProxy"; } }
    //    public virtual bool AutoWidth { get { return true; } }
    //    public virtual string Title { get { return LocalData.IsEnglish ? "Additional Fee" : "附加费"; } }
    //    public virtual Type SearchPartType { get { return null; } }
    //    public virtual Type SimpleSearchPartType { get { return null; } }
    //    List<PropertyBinding> _pbs;
    //    public virtual List<PropertyBinding> DataBindings
    //    {
    //        get
    //        {
    //            if (_pbs == null)
    //            {
    //                _pbs = new List<PropertyBinding>();
    //                PropertyBinding pb;
    //            }
    //            return _pbs;
    //        }
    //    }
    //    IDataHoster _hoster;
    //    [ServiceDependency]
    //    public IDataHoster datahoster
    //    {
    //        set
    //        {
    //            _hoster = value;
    //            _hoster.DataContentType = typeof(ICP.FRM.UI.OceanPrice.OPAdditionalFeePart);
    //        }
    //    }
    //    List<UIAction> actions;
    //    public List<UIAction> Actions
    //    {
    //        get
    //        {
    //            if (actions == null)
    //            {
    //                actions = new List<UIAction>();
    //            }

    //            return actions;
    //        }
    //    }

    //    protected virtual bool BeforeParentChanged(object obj) { return true; }
    //    protected virtual bool ParentChanged(object obj) { return true; }

    //}

    //public partial class OceanPriceFileListUIProxy : IDataUIProxy
    //{
    //    [ServiceDependency]
    //    public WorkItem workitem { get; set; }
    //    public virtual string Name { get { return "ICP.FRM.OceanPriceFileListUIProxy"; } }
    //    public virtual bool AutoWidth { get { return true; } }
    //    public virtual string Title { get { return LocalData.IsEnglish ? "File List" : "文件列表"; } }
    //    public virtual Type SearchPartType { get { return null; } }
    //    public virtual Type SimpleSearchPartType { get { return null; } }
    //    List<PropertyBinding> _pbs;
    //    public virtual List<PropertyBinding> DataBindings
    //    {
    //        get
    //        {
    //            if (_pbs == null)
    //            {
    //                _pbs = new List<PropertyBinding>();
    //                PropertyBinding pb;
    //            }
    //            return _pbs;
    //        }
    //    }
    //    IDataHoster _hoster;
    //    [ServiceDependency]
    //    public IDataHoster datahoster
    //    {
    //        set
    //        {
    //            _hoster = value;
    //            _hoster.DataContentType = typeof(ICP.FRM.UI.OceanPrice.OPFileListPart);
    //        }
    //    }
    //    List<UIAction> actions;
    //    public List<UIAction> Actions
    //    {
    //        get
    //        {
    //            if (actions == null)
    //            {
    //                actions = new List<UIAction>();
    //            }

    //            return actions;
    //        }
    //    }

    //    protected virtual bool BeforeParentChanged(object obj) { return true; }
    //    protected virtual bool ParentChanged(object obj) { return true; }

    //}
}
