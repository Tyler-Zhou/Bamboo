using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using LongWin.DataWarehouseReport.ServiceInterface;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;

namespace LongWin.DataWarehouseReport.WinUI
{
    /// <summary>
    /// 操作工作量统计查询面板
    /// </summary>
    public partial class WorkLoadForOperator_SearchPart : UserControl
    {
        #region 服务注入
        IREPBaseDataService _repBaseDataService;
        [ServiceDependency]
        public IREPBaseDataService RepBaseDataService
        {
            get { return _repBaseDataService; }
            set
            {

                _repBaseDataService = value;
                if (_repBaseDataService != null)
                    REPModuleInit.RepService = value;
            }
        }

        WorkItem _workitem;
        [ServiceDependency]
        public WorkItem Workitem
        {
            get { return _workitem; }
            set { _workitem = value; }
        }
        #endregion

        #region 初始化

        public WorkLoadForOperator_SearchPart()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(WorkLoadForOperator_SearchPart_Disposed);

            this.cmbGroupBy.MaxChecked = 3;
            this.cmbGroupBy.Items = new string[] {      Utility.GetValueString("业务发生地", "业务发生地"),
                                                        Utility.GetValueString("操作", "操作"),
                                                        Utility.GetValueString("业务类型", "业务类型"),
                                                        Utility.GetValueString("航线", "航线"),
                                                        
                                                        };



        }

        void WorkLoadForOperator_SearchPart_Disposed(object sender, EventArgs e)
        {
            this._workitem.Items.Remove(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                if (_repBaseDataService == null )
                {
                    //MessageBox.Show("服务为空,请检查");
                    _repBaseDataService = REPModuleInit.RepService;
                    //return;
                }
                this.ucSelectCompany.DataSource = this._repBaseDataService.GetStructureNodesByCurrentUser();

                this.cmbGroupBy.Text = Utility.GetValueString("业务发生地", "业务发生地") + ","
                    + Utility.GetValueString("操作", "操作") + ","
                    + Utility.GetValueString("业务类型", "业务类型");

                this.cmbUserState.SelectedIndex = 1;
                if (this.ucSelectCompany.DataSource.Tables[0].Rows.Count == 1)
                {
                    this.cmbGroupBy.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        public event EventHandler SearchResult;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.SearchResult != null)
            {
                this.SearchResult(sender, e);
            }
        }

        #endregion

        #region 属性

        #region 时间范围
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime
        {
            get { return this.ucDateTime.DateTimeFrom; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return this.ucDateTime.DateTimeTo; }
        }
        #endregion

        #region 部门
        /// <summary>
        /// 组织结构的Id
        /// </summary>
        public string StructNodeId
        { get { return this.ucSelectCompany.Value.ToString(); } }
        /// <summary>
        /// 组织结构的名字

        /// </summary>
        public string StructureNodeName
        {
            get { return this.ucSelectCompany.Text; }
        }
        #endregion

        #region 分组条件
        public string GroupBy
        {
            get
            {
                return this.cmbGroupBy.Text.Trim();
               // this.cmbGroupBy.Text.Split(new string[] { "o" }, StringSplitOptions.None);
            }
        }
        #endregion

        #region 描述信息

        /// <summary>
        /// 期间
        /// </summary>
        public string Peried
        {
            get { return this.ucDateTime.DateTimeFrom.ToShortDateString() + "-" + this.ucDateTime.DateTimeTo.ToShortDateString(); }
        }
        /// <summary>
        /// 附加信息
        /// </summary>
        public string ConditionRemark
        {
            get
            {
                string condition = "";
                condition +=Utility.GetValueString("部门", "部门") +"  : "+this.ucSelectCompany.Text;
                return condition;
            }
        }
        #endregion

        public string GetUserState
        {
            get {
                return this.cmbUserState.SelectedIndex.ToString();
            }
        }
        #endregion

        private void cmbGroupBy_Load(object sender, EventArgs e)
        {

        }
    }
}
