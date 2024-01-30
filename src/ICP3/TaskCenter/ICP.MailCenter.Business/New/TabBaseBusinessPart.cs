using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.WinForms;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;

namespace ICP.Common.Business.ServiceInterface
{  
    /// <summary>
    /// 带子Tab面板的业务面板控件
    /// </summary>
    public partial class TabBaseBusinessPart : XtraUserControl,IListBaseBusinessPart
    {
        #region 属性
        /// <summary>
        /// Tab选项卡集合信息
        /// </summary>
        public List<TabControl> TabControls 
        {
            get; 
            set; 
        }
        public DataRow CurrentRow
        {
            get {
                return this.listBaseBusinessPart.CurrentRow;
            }
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        #endregion
        /// <summary>
       /// 构造函数
       /// </summary>
        public TabBaseBusinessPart()
        {
            InitializeComponent();
            xtraTabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(SelectedPageChanged);
            this.listBaseBusinessPart.FocusedRowChangedHandler +=listBaseBusinessPart_FocusedRowChangedHandler;
            this.Disposed += delegate
            {
                if (this.listBaseBusinessPart != null)
                {
                    this.listBaseBusinessPart.FocusedRowChangedHandler -= listBaseBusinessPart_FocusedRowChangedHandler;
                }
            };
        }

        void listBaseBusinessPart_FocusedRowChangedHandler(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SelectedPageChanged(CurrentRow, null);
        }

        #region tab操作
        /// <summary>
        /// 构造Tab选项卡
        /// </summary>
        /// <param name="code">当前的节点code</param>
        public void StructuretabWork(string code)
        {
            var xtra = new List<XtraTabPage>();
            var xtraTab = new XtraTabPage();
            var tabControlReadXml = WorkItem.Items.AddNew<TabControlReadXml>();
            TabControls = tabControlReadXml.GetTabControlList(code);
            if (TabControls.Any() == false)
            {
                return;
            }
            foreach (var control in TabControls)
            {
                var xtraTabPage = new XtraTabPage
                {
                    Text = LocalData.IsEnglish ? control.Ename : control.Cname,
                    Name = control.Ename,
                    TabIndex = control.Order,
                    Dock = DockStyle.Fill
                };
                xtra.Add(xtraTabPage);
                if (control.Selected)
                {
                    xtraTab = xtraTabPage;
                }

            }
          
            xtraTabControl.TabPages.AddRange(xtra.ToArray());
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
            var indextabControl = (from t in TabControls where t.Ename == name select t).FirstOrDefault();
            if (indextabControl != null)
            {
                Databind(indextabControl);
            }
        }

        /// <summary>
        /// 控件数据绑定的方法
        /// </summary>
        /// <param name="tab">对象集合</param>
        public void Databind(TabControl tab)
        {
            #region 构造面板加载控件
            if (tab.Dictionary.Values.Any(item => item == false))
            {
                //添加面板控件
                var deck = new DeckWorkspace
                {
                    Dock = DockStyle.Fill,
                    Name = "DeckWorkspace" + tab.Ename,
                    TabIndex = 1,
                    Text = "DeckWorkspace" + tab.Ename,
                    BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241))))),
                    Location = new System.Drawing.Point(0, 0)
                };
                //加入当前自定义控件
                tab.Control.Dock = DockStyle.Fill;
                deck.Controls.Add(tab.Control);
                //当前选项卡加载面板
                xtraTabControl.SelectedTabPage.Controls.Add(deck);
                tab.Dictionary.Remove(tab.Order);
                var dictionary = new Dictionary<int, bool> { { tab.Order, true } };
                tab.Dictionary = dictionary;
            }
            #endregion

            #region  执行查询

            if (CurrentRow == null)
            {
                return;
            }
            var business = new BusinessOperationContext()
            {
                FormType = FormType.Booking,
                OperationType = OperationType,
                State = DocumentState.Pending,
                OperationID =this.OperationID,
                FormId = this.OperationID
            };
            var taskCenterDataBind = tab.Control as Common.ServiceInterface.IDataBind;
            if (taskCenterDataBind != null)
            {
                taskCenterDataBind.DataBind(business);
            }

            #endregion

        }

        void splitContainerControl_SplitterPositionChanged(object sender, System.EventArgs e)
        {
            UserPrivateInfo.SaveSplitterPosition(TemplateCode + "SplitPositionKey", this.splitContainerControl.SplitterPosition.ToString());
        }
        #endregion



        #region IListBaseBusinessPart 成员

        public void SaveCustomColumnInfo()
        {
            this.listBaseBusinessPart.SaveCustomColumnInfo();
        }

        public UserCustomGridInfo GetUserCustomGridInfo()
        {
            return this.listBaseBusinessPart.GetUserCustomGridInfo();
        }

        public List<BusinessColumnInfo> GetColumnInfos(string templateCode)
        {
            return this.listBaseBusinessPart.GetColumnInfos(templateCode);
        }

     

        #endregion

        #region IBaseBusinessPart_New 成员

        public List<OperationToolbarCommand> GetToolbarCommands(string templateCode)
        {
            return this.listBaseBusinessPart.GetToolbarCommands(templateCode);
        }

        public void Init(object parameter)
        {
            this.listBaseBusinessPart.Init(parameter);
            UserPrivateInfo.SetSplitterPosition(this.splitContainerControl, TemplateCode + "SplitPositionKey", 360);
            StructuretabWork(TemplateCode);
        }

        public string OperationNo
        {
            get
            {
                return this.listBaseBusinessPart.OperationNo;
            }
            set
            {
                this.listBaseBusinessPart.OperationNo = value;
            }
        }

        public Guid OperationID
        {
            get
            {
                return this.listBaseBusinessPart.OperationID;
            }
            set
            {
                this.listBaseBusinessPart.OperationID = value;
            }
        }

        public OperationType OperationType
        {
            get
            {
                return this.listBaseBusinessPart.OperationType;
            }
            set
            {
                this.listBaseBusinessPart.OperationType = value;
            }
        }

        public Guid FormID
        {
            get
            {
                return this.listBaseBusinessPart.FormID;
            }
            set
            {
                this.listBaseBusinessPart.FormID = value;
            }
        }
        public string SelectedCompanyIds
        {
            get
            {
                return this.listBaseBusinessPart.SelectedCompanyIds;
            }
            set
            {
                this.listBaseBusinessPart.SelectedCompanyIds = value;
            }
        }

        public string TemplateCode
        {
            get
            {
                return this.listBaseBusinessPart.TemplateCode;
            }
            set
            {
                this.listBaseBusinessPart.TemplateCode = value;
            }
        }
        /// <summary>
        /// 高级查询面板传递过来的高级查询条件字符串
        /// </summary>
        public string AdvanceQueryString
        {
            get
            {
                return this.listBaseBusinessPart.AdvanceQueryString;
            }
            set
            {
                this.listBaseBusinessPart.AdvanceQueryString = value;
            }
        }

        public void GetInfo(object parameter)
        {
            this.listBaseBusinessPart.GetInfo(parameter);
        }

        public BusinessQueryCriteria GetQueryCriteria(bool mergeAdvanceQueryString)
        {
            return this.listBaseBusinessPart.GetQueryCriteria(mergeAdvanceQueryString);
        }

        public void QueryData(bool mergeAdvanceQueryString)
        {
            this.listBaseBusinessPart.QueryData(mergeAdvanceQueryString);
        }

        public void BindData(object parameter)
        {
            this.listBaseBusinessPart.BindData(parameter);
        }

        public void RegisterExtensionSite()
        {
            this.listBaseBusinessPart.RegisterExtensionSite();
        }

        public void AddCustomColumn(DataTable dt)
        {
            this.listBaseBusinessPart.AddCustomColumn(dt);
        }
        public void SetBaseBindSource(object data)
        {
            this.listBaseBusinessPart.SetBaseBindSource(data);
        }
        public void SetDataSource(object data)
        {
            this.listBaseBusinessPart.SetDataSource(data);
            SelectedPageChanged(CurrentRow, null);
        }

        public void UnRegisterExtensionSite()
        {
            this.listBaseBusinessPart.UnRegisterExtensionSite();
        }
     
        #endregion

        #region IBaseBusinessPart_New 成员


        public DateTime? Updatetime
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
