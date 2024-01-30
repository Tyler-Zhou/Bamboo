using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraTab;
using ICP.Common.ServiceInterface;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Client;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.Operation.Common.ServiceInterface;
using System.Drawing;
using ICP.FileSystem.ServiceInterface;

namespace ICP.Operation.Common.UI
{
    /// <summary>
    /// 带子Tab面板的业务面板控件
    /// </summary>
    public partial class TabBaseBusinessPart : XtraUserControl, IListBaseBusinessPart
    {
        #region 属性
        /// <summary>
        /// Tab选项卡集合信息
        /// </summary>
        public List<TabItemConfigInfo> TabItems
        {
            get;
            set;
        }
        public DataRow FocusedDataRow
        {
            get
            {
                return listBaseBusinessPart.FocusedDataRow;
            }
        }
        /// <summary>
        /// 操作上下文
        /// </summary>
        public BusinessOperationContext BusinessOperationContext
        {
            get
            {
                return listBaseBusinessPart.BusinessOperationContext;
            }
            set
            {
                listBaseBusinessPart.BusinessOperationContext = value;
            }
        }
        /// <summary>
        /// 获取当前绑定列表的数据源
        /// </summary>
        public DataTable CurrentDataSource
        {
            get { return listBaseBusinessPart.CurrentDataSource; }
        }

        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        /// <summary>
        /// 加载数据时是否显示进度动画
        /// </summary>
        public bool IsShowLoadingForm
        {
            get;
            set;
        }
        /// <summary>
        /// 订舱号
        /// </summary>
        public string SONO
        {
            get { return listBaseBusinessPart.SONO; }
        }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        public TabBaseBusinessPart()
        {
            InitializeComponent();
            xtraTabControl.SelectedPageChanged += new TabPageChangedEventHandler(SelectedPageChanged);
            listBaseBusinessPart.FocusedRowChangedHandler += listBaseBusinessPart_FocusedRowChangedHandler;

            Disposed += delegate
            {
                if (listBaseBusinessPart != null)
                {
                    listBaseBusinessPart.FocusedRowChangedHandler -= listBaseBusinessPart_FocusedRowChangedHandler;
                }
            };
            Load += delegate
            {
                listBaseBusinessPart.WorkItem = WorkItem;

                //WorkItem.RootWorkItem.State["AdventQueryString"] = "Test=Test";
                //WorkItem.Commands[CommonCommandName.Command_F5_Search].Execute();
            };
        }


        /// <summary>
        /// 设置列表获得焦点的行
        /// </summary>
        /// <param name="rowHandle"></param>
        public void SetFocusedRowHandle(int rowHandle)
        {
            listBaseBusinessPart.SetFocusedRowHandle(rowHandle);
        }
        void listBaseBusinessPart_FocusedRowChangedHandler(object sender, FocusedRowChangedEventArgs e)
        {
            SelectedPageChanged(FocusedDataRow, null);

        }
        /// <summary>
        /// 保存列表数据更改
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            return listBaseBusinessPart.Save();
        }

        #region tab操作
        /// <summary>
        /// 构造Tab选项卡
        /// </summary>
        /// <param name="code">当前的节点code</param>
        public void InitTabControl(string code)
        {

            TabItems = TabControlConfigController.Current.GetTabItemList(code);
            if (TabItems.Any() == false)
            {
                xtraTabControl.Visible = false;
                return;
            }
            var pageList = new List<XtraTabPage>();
            var xtraTab = new XtraTabPage();

            foreach (var control in TabItems)
            {
                var xtraTabPage = new XtraTabPage
                {
                    Text = LocalData.IsEnglish ? control.EName : control.CName,
                    Name = control.EName,
                    TabIndex = control.Order,
                    Dock = DockStyle.Fill
                };
                pageList.Add(xtraTabPage);
                if (control.Selected)
                {
                    xtraTab = xtraTabPage;
                }

            }
            xtraTabControl.SelectedPageChanged -= new TabPageChangedEventHandler(SelectedPageChanged);
            xtraTabControl.TabPages.AddRange(pageList.ToArray());
            xtraTabControl.SelectedPageChanged += new TabPageChangedEventHandler(SelectedPageChanged);
            xtraTabControl.SelectedTabPage = xtraTab;

        }



        /// <summary>
        /// 选项卡发生改变时触发 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (xtraTabControl.SelectedTabPage == null)
                return;
            var name = xtraTabControl.SelectedTabPage.Name;
            var indextabControl = TabItems.Find(item => item.EName == name);

            if (indextabControl != null)
            {
                Databind(indextabControl);
                listBaseBusinessPart.CurrentTabItem = indextabControl;
            }
        }

        private IDataBind EnsureTabItemControlExists(TabItemConfigInfo tabItemConfig)
        {

            if (!tabItemConfig.IsControlInit)
            {
                var partType = Type.GetType(tabItemConfig.ControlFullName);
                if (partType != null)
                {

                    var userControl = WorkItem.Items.AddNew(partType) as UserControl;
                    tabItemConfig.Control = userControl;
                    if (userControl.GetType().GetProperty("TemplateCode") != null)
                    {
                        userControl.GetType().GetProperty("TemplateCode").SetValue(userControl, TemplateCode, null);
                    }
                    //加入当前自定义控件
                    userControl.Dock = DockStyle.Fill;
                    //当前选项卡加载面板
                    xtraTabControl.SelectedTabPage.Controls.Add(userControl);
                    return userControl as IDataBind;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return tabItemConfig.Control as IDataBind;
            }
        }
        /// <summary>
        /// 控件数据绑定的方法
        /// </summary>
        /// <param name="tab">对象集合</param>
        public void Databind(TabItemConfigInfo tabItemConfig)
        {
            var dataBindControl = EnsureTabItemControlExists(tabItemConfig);
            if (dataBindControl == null)
            {
                return;
            }

            var business = new BusinessOperationContext();
            if (CurrentDataSource == null)
            {
                business.OperationID = Guid.Empty;

            }
            else
            {
                business.FormType = FormType.Unknown;
                business.OperationType = OperationType;
                business.Add("DocumentState", DocumentState.Pending);
                business.UpdateDate = UpdateDate;
                business.OperationID = OperationID;
                business.OperationNO = OperationNo;
                business.FormId = business.OperationID;
                business.UpdateDate = UpdateDate;
                business.SONO = SONO;
                if (listBaseBusinessPart.FocusedDataRow != null && listBaseBusinessPart.FocusedDataRow.Table.Columns.Contains("ANSC"))
                {
                    if (business.ContainsKey("ANSC"))
                    {
                        business["ANSC"] = bool.Parse(listBaseBusinessPart.FocusedDataRow["ANSC"].ToString());
                    }
                    else
                    {
                        business.Add("ANSC", bool.Parse(listBaseBusinessPart.FocusedDataRow["ANSC"].ToString()));
                    }
                }
                if (listBaseBusinessPart.FocusedDataRow != null && listBaseBusinessPart.FocusedDataRow.Table.Columns.Contains("CompanyID"))
                {
                    business.CompanyID = CompanyID;
                    if (business.ContainsKey("CompanyID"))
                    {
                        business["CompanyID"] = CompanyID;
                    }
                    else
                    {
                        business.Add("CompanyID", CompanyID);
                    }
                }
                if (listBaseBusinessPart.FocusedDataRow != null && listBaseBusinessPart.FocusedDataRow.Table.Columns.Contains("ExpandOperationType"))
                {
                    ExpandOperationType ExpandOperationType= (ExpandOperationType)byte.Parse(listBaseBusinessPart.FocusedDataRow["ExpandOperationType"].ToString());
                    if(ExpandOperationType==ExpandOperationType.ECommerce)
                    {
                        business.FormType = FormType.ECommerceOrder;
                        business.Add("RefOperationID", listBaseBusinessPart.FocusedDataRow["RefOperationID"]+"");
                        business.Add("RefOperationNo", listBaseBusinessPart.FocusedDataRow["RefOperationNo"] + "");
                        business.Add("RefOperationType",listBaseBusinessPart.FocusedDataRow["RefOperationType"] + "");
                        business.Add("RefSONO", listBaseBusinessPart.FocusedDataRow["RefSONO"] + "");
                    }
                }
            }
            dataBindControl.DataBind(business);
            dataBindControl.ControlsReadOnly(tabItemConfig.ReadOnly);



        }

        void splitContainerControl_SplitterPositionChanged(object sender, EventArgs e)
        {
            UserPrivateInfo.SaveSplitterPosition(TemplateCode + "SplitPositionKey", splitContainerControl.SplitterPosition.ToString());
        }
        #endregion



        #region IListBaseBusinessPart 成员

        public void SaveCustomColumnInfo()
        {
            listBaseBusinessPart.SaveCustomColumnInfo();
        }

        public UserCustomGridInfo GetUserCustomGridInfo()
        {
            return listBaseBusinessPart.GetUserCustomGridInfo();
        }

        public List<BusinessColumnInfo> GetColumnInfos(string templateCode)
        {
            return listBaseBusinessPart.GetColumnInfos(templateCode);
        }

        /// <summary>
        /// 事件列表操作类
        /// </summary>
        public EventObjects EventObjects
        {
            get
            {
                return listBaseBusinessPart.EventObjects;
            }
            set
            {
                listBaseBusinessPart.EventObjects = value;
            }
        }

        public SearchActionType SearchType { get; set; }
        public bool NeedSearchInSQLServer { get; set; }
        public string ServerQueryString { get; set; }

        #endregion

        #region IBaseBusinessPart_New 成员

        public List<OperationToolbarCommand> GetToolbarCommands(string templateCode)
        {
            return listBaseBusinessPart.GetToolbarCommands(templateCode);
        }

        public void Init(object parameter)
        {
            listBaseBusinessPart.Init(parameter);
            UserPrivateInfo.SetSplitterPosition(splitContainerControl, TemplateCode + "SplitPositionKey", 360);
            InitTabControl(TemplateCode);
        }



        public string OperationNo
        {
            get
            {
                return listBaseBusinessPart.OperationNo;
            }
            set
            {
                listBaseBusinessPart.OperationNo = value;
            }
        }

        public Guid OperationID
        {
            get
            {
                return listBaseBusinessPart.OperationID;
            }
            set
            {
                listBaseBusinessPart.OperationID = value;
            }
        }

        public Guid CompanyID
        {
            get
            {
                return listBaseBusinessPart.CompanyID;
            }
            set
            {
                listBaseBusinessPart.CompanyID = value;
            }
        }

        public OperationType OperationType
        {
            get
            {
                return listBaseBusinessPart.OperationType;
            }
            set
            {
                listBaseBusinessPart.OperationType = value;
            }
        }

        public Guid FormID
        {
            get
            {
                return listBaseBusinessPart.FormID;
            }
            set
            {
                listBaseBusinessPart.FormID = value;
            }
        }
        public string SelectedCompanyIds
        {
            get
            {
                return listBaseBusinessPart.SelectedCompanyIds;
            }
            set
            {
                listBaseBusinessPart.SelectedCompanyIds = value;
            }
        }

        public DateTime? UpdateDate
        {
            get { return listBaseBusinessPart.Updatetime; }
            set { listBaseBusinessPart.Updatetime = value; }
        }

        public string TemplateCode
        {
            get
            {
                return listBaseBusinessPart.TemplateCode;
            }
            set
            {
                listBaseBusinessPart.TemplateCode = value;
            }
        }
        /// <summary>
        /// 高级查询面板传递过来的高级查询条件字符串
        /// </summary>
        public string AdvanceQueryString
        {
            get
            {
                return listBaseBusinessPart.AdvanceQueryString;
            }
            set
            {
                listBaseBusinessPart.AdvanceQueryString = value;
            }
        }

        public void GetInfo(object parameter)
        {
            listBaseBusinessPart.GetInfo(parameter);
        }

        public BusinessQueryCriteria GetQueryCriteria(bool mergeAdvanceQueryString)
        {
            return listBaseBusinessPart.GetQueryCriteria(mergeAdvanceQueryString);
        }

        public void QueryData(bool mergeAdvanceQueryString)
        {
            listBaseBusinessPart.QueryData(mergeAdvanceQueryString);
        }
        /// <summary>
        /// 数据绑定以后更新TAB页
        /// </summary>
        /// <param name="parameter"></param>
        public void BindData(object parameter)
        {
            if (parameter == null) return;
            listBaseBusinessPart.IsShowLoadingForm = true;
            listBaseBusinessPart.BindData(parameter);

        }
        int tokenID = -1;
        protected void BeginShowAnimation(string tip)
        {
            if (tokenID != -1)
            {
                LoadingServce.CloseLoadingForm(tokenID);
                tokenID = -1;
            }
            tokenID = LoadingServce.ShowLoadingForm(xtraTabControl, tip);
        }
        protected void EndShowAnimation()
        {
            LoadingServce.CloseLoadingForm(tokenID);
        }

        public void RegisterExtensionSite()
        {
            listBaseBusinessPart.RegisterExtensionSite();
        }

        public void AddCustomColumn(DataTable dt)
        {
            listBaseBusinessPart.AddCustomColumn(dt);
        }
        public void SetBaseBindSource(object data)
        {
            listBaseBusinessPart.SetBaseBindSource(data);
        }
        public void SetDataSource(object data)
        {
            listBaseBusinessPart.SetDataSource(data);
        }

        public void UnRegisterExtensionSite()
        {
            listBaseBusinessPart.UnRegisterExtensionSite();
        }

        #endregion

        #region IBaseBusinessPart_New 成员


        public DateTime? Updatetime
        {
            get { return listBaseBusinessPart.Updatetime; }
            set { listBaseBusinessPart.Updatetime = value; }
        }

        #endregion

        #region IBaseBusinessPart_New 成员


        public List<string> RegisterSiteNames
        {
            get
            {
                return listBaseBusinessPart.RegisterSiteNames;
            }
            set
            {
                listBaseBusinessPart.RegisterSiteNames = value;
            }
        }

        /// <summary>
        /// 列表行字体
        /// </summary>
        public Font RowFont
        {
            get
            {
                return listBaseBusinessPart.RowFont;
            }
        }
        #endregion

        #region IBaseBusinessPart_New 成员


        public void AddToolBarElement(OperationToolbarCommand toolBarEntity)
        {
        }

        #endregion

    }
}
