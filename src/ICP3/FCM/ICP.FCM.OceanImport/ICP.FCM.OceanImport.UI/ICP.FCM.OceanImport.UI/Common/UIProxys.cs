
namespace ICP.FCM.OceanImport.UI
{


    //public partial class OIBLLayoutUIProxy : ILayoutUIProxy
    //{
    //    [ServiceDependency]
    //    public WorkItem workitem { get; set; }
    //    public virtual string Name
    //    {
    //        get
    //        {
    //            return "ICPFCMOceanExport.OIBLLayout";
    //        }
    //    }
    //    public virtual bool AutoWidth
    //    {
    //        get
    //        {
    //            return true;
    //        }
    //    }
    //    public virtual string Title
    //    {
    //        get
    //        {
    //            return "�����ᵥ�б�";
    //        }
    //    }
    //    List<LayoutElement> elements;
    //    public virtual List<LayoutElement> Elements
    //    {
    //        get
    //        {
    //            if (elements == null)
    //            {
    //                elements = new List<LayoutElement>();
    //                UIProxyElement oibllistmanage = new UIProxyElement();
    //                oibllistmanage.ID = "oibllistmanage";
    //                oibllistmanage.IsMainPart = true;
    //                oibllistmanage.ProxyType = typeof(OIBLListManageUIProxy);
    //                oibllistmanage.Dock = DockStyle.Fill;

    //                elements.Add(oibllistmanage);
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
    //            }
    //            return connections;
    //        }
    //    }
    //    public virtual Type SearchPartType
    //    {
    //        get
    //        {
    //            return null;
    //        }
    //    }
    //    public virtual Type SimpleSearchPartType
    //    {
    //        get
    //        {
    //            return null;
    //        }
    //    }
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
    //    public List<UIAction> Actions
    //    {
    //        get
    //        {
    //            return actions;
    //        }
    //    }
    //}

    //public partial class OIBLListManageUIProxy : ITreeManageUIProxy
    //{
    //    [ServiceDependency]
    //    public WorkItem workitem { get; set; }
    //    public virtual string Name
    //    {
    //        get
    //        {
    //            return "ICPFCMOceanExport.OIBLListManage";
    //        }
    //    }
    //    public virtual bool AutoWidth
    //    {
    //        get
    //        {
    //            return false;
    //        }
    //    }
    //    public virtual string Title
    //    {
    //        get
    //        {
    //            return "�����ᵥ�б�";
    //        }
    //    }
    //    public virtual Type SearchPartType
    //    {
    //        get
    //        {
    //            return typeof(ICP.FCM.OceanImport.UI.BL.BLSearchPart);
    //        }
    //    }
    //    public virtual Type SimpleSearchPartType
    //    {
    //        get
    //        {
    //            return typeof(ICP.FCM.OceanImport.UI.BL.BLFastSearchPart);
    //        }
    //    }
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
    //                pb.PropertyName = "SentAN";
    //                pb.HeaderText = "����֪ͨ";
    //                pb.Fixed = true;
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 60;
    //                pb.Editable = true;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "Paid";
    //                pb.HeaderText = "�Ѹ�";
    //                pb.Fixed = false;
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 35;
    //                pb.Editable = true;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "TelexRelease";
    //                pb.HeaderText = "���";
    //                pb.Fixed = false;
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 35;
    //                pb.Editable = true;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "OperationTypeDescription";
    //                pb.HeaderText = "����";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 50;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "RefNo";
    //                pb.HeaderText = "ҵ���";
    //                pb.Fixed = true;
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 100;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "No";
    //                pb.HeaderText = "�ᵥ��";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 100;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "CustomerName";
    //                pb.HeaderText = "�ͻ�";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 150;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "CustomerRefNo";
    //                pb.HeaderText = "�ͻ��ο���";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 100;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "CarrierName";
    //                pb.HeaderText = "����";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 150;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "POLName";
    //                pb.HeaderText = "װ����";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 150;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "ETD";
    //                pb.HeaderText = "�����";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 60;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "PODName";
    //                pb.HeaderText = "ж����";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 150;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "ETA";
    //                pb.HeaderText = "������";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 60;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "PlaceOfDeliveryName";
    //                pb.HeaderText = "������";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 150;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "FETA";
    //                pb.HeaderText = "������";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 60;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "OperatorName";
    //                pb.HeaderText = "����";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 50;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "CreateByName";
    //                pb.HeaderText = "������";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 50;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "CreateDate";
    //                pb.HeaderText = "����ʱ��";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 60;


    //                _pbs.Add(pb);
    //            }
    //            return _pbs;
    //        }
    //    }
    //    public virtual string ParentFieldName
    //    {
    //        get
    //        {
    //            return "MBLID";
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
    //                a.Name = "DownBL";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "D&own BL" : "����(&O)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 0;
    //                a.Execute = DownBL;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "AddHBL";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "Add &HBL" : "����HBL(&H)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 1;
    //                a.Execute = AddHBL;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "AddMBL";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "Add &MBL" : "����MBL(&M)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 2;
    //                a.Execute = AddMBL;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "CopyBL";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "&Copy" : "����(&C)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 3;
    //                a.Execute = CopyData;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "EditBL";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "&Edit" : "�༭(&E)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 4;
    //                a.Execute = EditData;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "DeleteBL";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "&Delete" : "ɾ��(&D)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 5;
    //                a.Execute = DeleteData;

    //                actions.Add(a);

    //                a = new UIAction();
    //                a.Name = "Print";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "&Print" : "��ӡ(&P)";
    //                // a.Time = ICP.Framework.Client.ActionTime.Click;
    //                //  a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 6;


    //                UIAction child = new UIAction();
    //                child.Name = "PrintBL";
    //                child.Icon = SysImages.None;
    //                child.Text = LocalData.IsEnglish ? "&Print" : "��ӡ(&P)";
    //                child.Time = ICP.Framework.Client.ActionTime.Click;
    //                child.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                child.Data = ICP.Framework.Client.ActionData.Current;
    //                child.Index = 7;
    //                child.Execute = this.PrintBL;
    //                a.ChildActions.Add(child);


    //                child = new UIAction();
    //                child.Name = "PrintProfit";
    //                child.Icon = SysImages.None;
    //                child.Text = LocalData.IsEnglish ? "&Print Profit" : "��ӡ����(&P)";
    //                child.Time = ICP.Framework.Client.ActionTime.Click;
    //                child.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                child.Data = ICP.Framework.Client.ActionData.Current;
    //                child.Index = 8;
    //                child.Execute = this.PrintBL;
    //                a.ChildActions.Add(child);


    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "BeginGroup1";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "|" : "|";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 7;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "OpenTruckServiceList";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "&Truck" : "�ɳ�(&T)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 8;
    //                a.Execute = OpenTruckServiceList;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "BeginGroup1";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "|" : "|";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 9;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "OpenBillList";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "&Bill" : "�ʵ�(&B)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 10;
    //                a.Execute = OpenBillList;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "OpenFeeList";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "&Fee" : "����(&F)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 11;
    //                a.Execute = OpenFeeList;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "BeginGroup2";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "|" : "|";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 12;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "OpenDocumentList";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "Doc&ument" : "��֤(&U)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 13;
    //                a.Execute = OpenDocumentList;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "OpenFaxEmailList";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "EMail/Fax" : "�����ʼ�(&I)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 14;
    //                a.Execute = OpenFaxEmailList;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "OpenMemoList";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "Mem&o" : "��ע(&O)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 15;
    //                a.Execute = OpenMemoList;

    //                actions.Add(a);
    //            }

    //            return actions;
    //        }
    //    }
    //    protected virtual bool DownBL(object obj)
    //    {
    //        return true;
    //    }

    //    protected virtual bool AddHBL(object obj)
    //    {
    //        return true;
    //    }

    //    protected virtual bool AddMBL(object obj)
    //    {
    //        return true;
    //    }

    //    protected virtual bool EditData(object obj)
    //    {
    //        return true;
    //    }

    //    protected virtual bool CopyData(object obj)
    //    {
    //        return true;
    //    }

    //    protected virtual bool DeleteData(object obj)
    //    {
    //        return true;
    //    }

    //    protected virtual bool PrintBL(object obj)
    //    {
    //        return true;
    //    }

    //    protected virtual bool OpenTruckServiceList(object obj)
    //    {
    //        return true;
    //    }

    //    protected virtual bool OpenBillList(object obj)
    //    {
    //        return true;
    //    }

    //    protected virtual bool OpenFeeList(object obj)
    //    {
    //        return true;
    //    }

    //    protected virtual bool OpenDocumentList(object obj)
    //    {
    //        return true;
    //    }

    //    protected virtual bool OpenFaxEmailList(object obj)
    //    {
    //        return true;
    //    }

    //    protected virtual bool OpenMemoList(object obj)
    //    {
    //        return true;
    //    }

    //}

}
