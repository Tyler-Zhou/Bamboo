
namespace ICP.FCM.Common.UI
{
    //public partial class MemoListManageUIProxy : IListManageUIProxy
    //{
    //    [ServiceDependency]
    //    public WorkItem workitem { get; set; }
    //    public virtual string Title
    //    {
    //        get
    //        {
    //            return LocalData.IsEnglish ? "Memo" : "备注";
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
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "Subject";
    //                pb.HeaderText = LocalData.IsEnglish ? "Subject" : "主题";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 80;
    //                pb.Editable = true;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "Content";
    //                pb.HeaderText = LocalData.IsEnglish ? "Content" : "内容";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 80;
    //                pb.Editable = true;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "IsShowAgent";
    //                pb.HeaderText = LocalData.IsEnglish ? "IsShowAgent" : "是否显示给代理";
    //                pb.PropertyType = typeof(bool);
    //                pb.ColumnWidth = 60;
    //                pb.Editable = true;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "IsShowCustomer";
    //                pb.HeaderText = LocalData.IsEnglish ? "IsShowCustomer" : "是否显示给客户";
    //                pb.PropertyType = typeof(bool);
    //                pb.ColumnWidth = 60;
    //                pb.Editable = true;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "CreateByName";
    //                pb.HeaderText = LocalData.IsEnglish ? "CreateByName" : "建立人";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 60;
    //                pb.Editable = true;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "CreateDate";
    //                pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "建立时间";
    //                pb.PropertyType = typeof(DateTime);
    //                pb.ColumnWidth = 60;
    //                pb.Editable = true;


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
    //                a.Name = "Add";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 0;
    //                a.Execute = AddData;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "Save";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click | ICP.Framework.Client.ActionTime.DblClickItem;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 1;
    //                a.Execute = SaveData;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "Delete";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "&Delete" : "删除(&D)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 2;
    //                a.Execute = DeleteData;

    //                actions.Add(a);
    //            }

    //            return actions;
    //        }
    //    }
    //    protected virtual bool AddData(object obj)
    //    {
    //        return true;
    //    }

    //    protected virtual bool DeleteData(object obj)
    //    {
    //        return true;
    //    }

    //    protected virtual bool SaveData(object obj)
    //    {
    //        return true;
    //    }

    //}

    //public partial class MailFaxLogListManageUIProxy : IListManageUIProxy
    //{
    //    [ServiceDependency]
    //    public WorkItem workitem { get; set; }
    //    public virtual string Title
    //    {
    //        get
    //        {
    //            return LocalData.IsEnglish ? "MailFaxLog" : "传真日志";
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
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "AttachmentList";
    //                pb.HeaderText = LocalData.IsEnglish ? "AttachmentList" : "附件列表";
    //                pb.PropertyType = typeof(List<AttachmentList>);
    //                pb.ColumnWidth = 60;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "Receiver";
    //                pb.HeaderText = LocalData.IsEnglish ? "Receiver" : "收件人";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 60;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "Subject";
    //                pb.HeaderText = LocalData.IsEnglish ? "Subject" : "邮件主题";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 80;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "MailContent";
    //                pb.HeaderText = LocalData.IsEnglish ? "MailContent" : "邮件内容";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 80;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "Sender";
    //                pb.HeaderText = LocalData.IsEnglish ? "Sender" : "发件人";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 60;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "CreateDate";
    //                pb.HeaderText = LocalData.IsEnglish ? "CreateDate" : "建立时间";
    //                pb.PropertyType = typeof(DateTime);
    //                pb.ColumnWidth = 60;


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
    //            return actions;
    //        }
    //    }
    //}

    //public partial class DocumentListManageUIProxy : IListManageUIProxy
    //{
    //    [ServiceDependency]
    //    public WorkItem workitem { get; set; }
    //    public virtual string Title
    //    {
    //        get
    //        {
    //            return LocalData.IsEnglish ? "Document" : "单证";
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
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "DocumentNo";
    //                pb.HeaderText = LocalData.IsEnglish ? "DocumentNo" : "单证号";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 60;
    //                pb.Editable = true;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "TrackingNo";
    //                pb.HeaderText = LocalData.IsEnglish ? "TrackingNo" : "快递单号";
    //                pb.PropertyType = typeof(string);
    //                pb.ColumnWidth = 60;
    //                pb.Editable = true;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "ReceivedDate";
    //                pb.HeaderText = LocalData.IsEnglish ? "ReceivedDate" : "接收日期";
    //                pb.PropertyType = typeof(DateTime);
    //                pb.ColumnWidth = 60;
    //                pb.Editable = true;


    //                _pbs.Add(pb);
    //                pb = new PropertyBinding();
    //                pb.PropertyName = "ReturnDate";
    //                pb.HeaderText = LocalData.IsEnglish ? "ReturnDate" : "退回日期";
    //                pb.PropertyType = typeof(DateTime?);
    //                pb.ColumnWidth = 60;
    //                pb.Editable = true;


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
    //                a.Name = "Add";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "&Add" : "新增(&A)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 0;
    //                a.Execute = AddData;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "Save";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "&Save" : "保存(&S)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click | ICP.Framework.Client.ActionTime.DblClickItem;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 1;
    //                a.Execute = SaveData;

    //                actions.Add(a);
    //                a = new UIAction();
    //                a.Name = "Delete";
    //                a.Icon = SysImages.None;
    //                a.Text = LocalData.IsEnglish ? "&Delete" : "删除(&D)";
    //                a.Time = ICP.Framework.Client.ActionTime.Click;
    //                a.AppearStyle = ICP.Framework.Client.ActionAppearStyle.ToolBar;
    //                a.Data = ICP.Framework.Client.ActionData.Current;
    //                a.Index = 2;
    //                a.Execute = DeleteData;

    //                actions.Add(a);
    //            }

    //            return actions;
    //        }
    //    }
    //    protected virtual bool AddData(object obj)
    //    {
    //        return true;
    //    }

    //    protected virtual bool DeleteData(object obj)
    //    {
    //        return true;
    //    }

    //    protected virtual bool SaveData(object obj)
    //    {
    //        return true;
    //    }

    //}

}
