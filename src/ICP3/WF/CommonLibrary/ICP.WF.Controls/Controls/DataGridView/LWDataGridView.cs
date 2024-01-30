using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ICP.WF.Controls;
using System;
using ICP.Framework.CommonLibrary.Attributes;
using System.Drawing.Design;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.WF.ServiceInterface;
using System.Linq;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.FAM.ServiceInterface;
using ICP.WF.Controls;
using ICP.Sys.ServiceInterface.DataObjects;

namespace ICP.WF.Controls
{

    [ToolboxBitmap(typeof(LWDataGridView), "Resources.DataGridView.bmp")]
    [DefaultProperty("DataTableName"),
    SRDescription("DataGridViewDesc"),
    SRTitle("DataGridViewTitle")]
    [Serializable()]
    public class LWDataGridView : DataGridView, IDataSourceService, IValidateService, IInitiDataService, ITable
    {
        private IDisposable userNameFinder,customerFinder, userFinder, glCodeFinder;
        /// <summary>
        /// 币种ID
        /// </summary>
        private static Guid currencyID = Guid.Empty;

        /// <summary>
        /// 部门ID
        /// </summary>
        private static Guid deptID = Guid.Empty;

        string[] userResultValue = new string[] { "ID", "Code", "EName", "CName", "EMail" };
        string[] customerResultValue = new string[] { "ID", "Code", "EName", "CName", "Type", "TradeTermID", "TradeTermName", "State", "CheckedState", "Term" };
        private DataGridViewCell currentCell = null;

        #region 服务

        public ICP.Common.ServiceInterface.IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<ICP.Common.ServiceInterface.IConfigureService>();
            }
        }

        public IWorkFlowExtendService WorkFlowExtendService
        {
            get
            {
                return ServiceClient.GetService<IWorkFlowExtendService>();
            }
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        /// <summary>
        /// Finder工厂
        /// </summary>
        public IDataFinderFactory DataFinderFactory
        {
            get
            {
                return ServiceClient.GetClientService<IDataFinderFactory>();
            }
        }

        #endregion

        public static readonly string[] ResultValue;
        Guid ss = Guid.Empty;
        string s = string.Empty;
        public void Searcher()
        {
            //foreach (DataGridViewColumn col in this.Columns)
            //{
            //    if (col.Name == "LWStringColUser")
            //    {
            //        Searcher(FieldType.User);
            //    }
            //    if (col.Name == "LWStringColCustomer")
            //    {
            //        Searcher(FieldType.Customer);
            //    }
            //}
        }

        public LWDataGridView()
        {
            this.BackColor = Color.White;
            this.RowsDefaultCellStyle.BackColor = Color.White;
            this.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

            this.BackgroundColor = Color.White;
            this.GridColor = Color.AliceBlue;


            DataGridViewCellStyle cs = new DataGridViewCellStyle();
            cs.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            cs.BackColor = Color.AliceBlue;
            cs.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            cs.ForeColor = System.Drawing.SystemColors.InfoText;
            cs.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cs.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cs.WrapMode = System.Windows.Forms.DataGridViewTriState.True;


            //this.ColumnHeadersHeight = 20;

            this.ColumnHeadersDefaultCellStyle = cs;
            this.RowHeadersDefaultCellStyle = cs;

            
            
            //this.CellValidating += new DataGridViewCellValidatingEventHandler(LWDataGridView_CellValidating);
        }

        //void LWDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        //{
        //    Guid glID = Guid.Empty;
        //    if (this.Columns[e.ColumnIndex].Name == "LWCostItemColumn1")
        //    {
        //        if (e.FormattedValue is Guid)
        //        {
        //            glID = new Guid(e.FormattedValue.ToString());
        //            if (ConfigureService.IsLeafGLCode(glID, LocalData.IsEnglish) == false)
        //            {
        //                e.Cancel = true;
        //                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "请选择末级科目。");
        //            }
        //            else
        //                LocalCommonServices.ErrorTrace.Clear();
        //        }


        //    }
        //}
        
        #region 属性
        [SRCategory("DataCustom"), ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true)]
        public new bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }

        /// <summary>
        /// 只读
        /// </summary>
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("ReadOnly"), ICPBrowsable(true), SRCategory("Base"), SRDescription("ReadOnlyDesc")]
        public bool ReadOnly
        {
            get { return base.ReadOnly; }
            set
            {
                base.ReadOnly = value;
            }
        }

        /// <summary>
        /// 大小
        /// </summary>
        [SRDisplayName("DispSize"), ICPBrowsable(true), SRDescription("DescSize"), SRCategory("Base")]
        public new Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value;
            }
        }

        int columnSpan = 1;
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("ColumnSpan"),
        ICPBrowsable(true),
        SRCategory("Layout"),
        SRDescription("ColumnSpan")]
        public int ColumnSpan
        {
            get { return columnSpan; }
            set
            {
                if (value != columnSpan)
                {
                    columnSpan = value;

                    if (this.Parent != null
                        && this.Parent is LWTableLayoutPanel)
                    {
                        LWTableLayoutPanel panel = this.Parent as LWTableLayoutPanel;
                        panel.SetColumnSpan(this, value);
                    }
                }
            }
        }

        int rowSpan = 1;
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("RowSpan"),
        ICPBrowsable(true),
        SRCategory("Layout"),
        SRDescription("RowSpan")]
        public int RowSpan
        {
            get { return rowSpan; }
            set
            {
                if (value != rowSpan)
                {
                    rowSpan = value;

                    if (this.Parent != null && this.Parent is LWTableLayoutPanel)
                    {
                        LWTableLayoutPanel panel = this.Parent as LWTableLayoutPanel;
                        panel.SetRowSpan(this, value);
                    }
                }
            }
        }


        /// <summary>
        /// 名称
        /// </summary>
        [SRDisplayName("Name"), ICPBrowsable(true), SRCategory("Base")]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
        /// <summary>
        /// 数据源
        /// </summary>
        [SRDisplayName("DataSource"), ICPBrowsable(false)]
        public new object DataSource
        {
            get { return base.DataSource; }
            set { base.DataSource = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [SRDisplayName("Tag"), ICPBrowsable(false)]
        public new object Tag
        {
            get { return base.Tag; }
            set { base.Tag = value; }
        }
        /// <summary>
        /// 停靠
        /// </summary>
        [SRDisplayName("Anchor"), ICPBrowsable(true)]
        public new AnchorStyles Anchor
        {
            get { return base.Anchor; }
            set { base.Anchor = value; }
        }
        /// <summary>
        /// 布局
        /// </summary>
        [SRDisplayName("Dock"), ICPBrowsable(true), SRDescription("Dock")]
        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = value;
            }
        }

        /// <summary>
        /// Tab顺序
        /// </summary>
        [SRDisplayName("DispTabIndex"), ICPBrowsable(true), SRCategory("Base"), SRDescription("DescTabIndex")]
        public new int TabIndex
        {
            get { return base.TabIndex; }
            set { base.TabIndex = value; }
        }

        /// <summary>
        /// 背景颜色BackgroundColor
        /// </summary>
        [SRDisplayName("BackColor"), ICPBrowsable(true), SRCategory("Base"), SRDescription("DispBackColor")]
        public new Color BackgroundColor
        {
            get
            {
                return base.BackgroundColor;
            }
            set
            {
                base.BackgroundColor = value;
            }
        }

        /// <summary>
        /// 网络颜色
        /// </summary>
        [SRDisplayName("GridColor"), ICPBrowsable(true), SRCategory("Base"), SRDescription("DispGridColor")]
        public new Color GridColor
        {
            get
            {
                return base.GridColor;
            }
            set
            {
                base.GridColor = value;
            }
        }

        [SRDisplayName("Columns"), ICPBrowsable(true), SRDescription("DispColumns")]
        [Editor(typeof(EditGridColumns), typeof(UITypeEditor))]
        public new DataGridViewColumnCollection Columns
        {
            get
            {
                return base.Columns;
            }

        }






        #endregion

        #region 自定义部分

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DataTableName
        {
            get
            {
                string tableName = Utility.GetPascalProperty(this.Name);
                if (string.IsNullOrEmpty(tableName))
                {
                    tableName = this.Name;
                }

                return tableName;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object DataTable
        {
            get
            {
                this.EndEdit();
                return base.DataSource;
            }
            set
            {

                DataTable dt = value as DataTable;
                if (this.ReadOnly || !this.Enabled)
                {
                    this.AllowUserToAddRows = false;

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Utility.SetDefaultValue(dt);
                        DataRow dr = dt.NewRow();
                        foreach (DataColumn col in dt.Columns)
                        {
                            if (col.DataType == typeof(decimal) || col.DataType == typeof(int) || col.DataType == typeof(short))
                            {
                                dr[col] = dt.Compute(string.Format("SUM({0})", col.ColumnName), string.Empty);
                            }
                        }
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                    }
                }

                base.DataSource = dt;

                this.Refresh();
            }
        }

        #endregion


        #region IValidateService接口
        private List<CostItemData> DataList = new List<CostItemData>();
        private List<SolutionGLCodeList> glCodeList = new List<SolutionGLCodeList>();
        /// <summary>
        /// 运行时的验证
        /// </summary>
        /// <param name="errorTip"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool ValidateForRuntime(System.Windows.Forms.ErrorProvider errorTip, List<string> errors)
        {
            this.EndEdit();

            if (errors == null) errors = new List<string>();
            //if (DataSource == null)
            //{
            //    errors.Add(Utility.GetString("Thereisnodata", "不存在数据."));
            //    return false;
            //}


            bool isSucc = true;
            DataTable dt = this.DataSource as DataTable;
            if (dt != null)
            {
                Utility.SetDefaultValue(dt);
            }
            foreach (DataGridViewRow row in this.Rows)
            {
                if (row.IsNewRow) continue;

                foreach (DataGridViewColumn col in this.Columns)
                {

                    row.Cells[col.Name].ErrorText = "";

                    if (!col.Visible)
                    {
                        continue;
                    }

                    if (string.IsNullOrEmpty(col.DataPropertyName))
                    {
                        continue;
                    }
                    bool must = !dt.Columns[col.DataPropertyName].AllowDBNull;
                    if (!must)
                    {
                        if (col is LWChargeCodeColumn)
                        {
                            LWChargeCodeColumn colChargeCode = col as LWChargeCodeColumn;
                            if (colChargeCode != null)
                            {
                                must = colChargeCode.AllowNull;
                            }
                            DataList = WorkFlowExtendService.GetCostItemList(LocalData.UserInfo.DefaultCompanyID);
                        }
                        if (col is LWCostItemColumn)
                        {
                            LWCostItemColumn colCostItem = col as LWCostItemColumn;
                            if (colCostItem != null)
                            {
                                must = colCostItem.AllowNull;
                            }
                        }
                        if (col is LWGLCodeColumn)
                        {
                            LWGLCodeColumn colGLCode = col as LWGLCodeColumn;
                            if (colGLCode != null)
                            {
                                must = colGLCode.AllowNull;
                            }
                            
                        }
                        if (col is LWCurrencyColumn)
                        {
                            LWCurrencyColumn colCurrency = col as LWCurrencyColumn;
                            if (colCurrency != null && colCurrency.Visible)
                            {
                                must = colCurrency.AllowNull;
                            }
                        }
                        //if (col is LWDeptColumn)
                        //{
                        //    LWDeptColumn colDept = col as LWDeptColumn;
                        //    if (colDept != null && colDept.Visible)
                        //    {
                        //        must = colDept.AllowNull;
                        //    }
                        //}
                    }



                    object cellval = row.Cells[col.Name].FormattedValue;
                    if (Utility.IsDefaultValue(cellval) && must)
                    {
                        string message = "[" + col.HeaderText + "]" + Utility.GetString("MustInput", "必须填写");
                        row.Cells[col.Name].ErrorText = message;
                        errors.Add(message);
                        isSucc = false;
                    }
                    else if (col is LWCostItemColumn)
                    {
                        LWCostItemColumn colCostItem = col as LWCostItemColumn;
                        DataList = WorkFlowExtendService.GetCostItemList(LocalData.UserInfo.DefaultCompanyID);
                        Guid glID = new Guid(row.Cells[col.Name].Value.ToString());
                        if (DataList == null)
                        {
                            DataList = new List<CostItemData>();
                        }
                        int count = (from d in DataList where d.ParentID == glID select d).Count();
                        if (count>0)
                        {
                            string message = "[" + col.HeaderText + "]" + Utility.GetString(cellval.ToString() + "can't be selected because it has child subjects.", "必须选择末级科目。");
                            row.Cells[col.Name].ErrorText = message;
                            errors.Add(message);
                            isSucc = false;
                        }
                    }
                    else if (col is LWGLCodeColumn)
                    {
                        LWGLCodeColumn colCostItem = col as LWGLCodeColumn;
                        glCodeList = WorkFlowExtendService.GetSolutionGLCodeList(GetSolutionID());
                        Guid glID = new Guid(row.Cells[col.Name].Value.ToString());
                        if (glCodeList == null)
                        {
                            glCodeList = new List<SolutionGLCodeList>();
                        }
                        int count = (from d in glCodeList where d.ParentID == glID select d).Count();
                        if (count > 0)
                        {
                            string message = "[" + col.HeaderText + "]" + Utility.GetString(cellval.ToString() + "can't be selected because it has child subjects.", "必须选择末级科目。");
                            row.Cells[col.Name].ErrorText = message;
                            errors.Add(message);
                            isSucc = false;
                        }
                    }
                }
            }



            if (isSucc)
            {

                this.Parent.Select();
            }

            return isSucc;
        }


        /// <summary>
        /// 设计时的验证
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool ValidateForDesign(List<string> errors)
        {
            if (errors == null) errors = new List<string>();
            bool isSucc = true;

            //不存在数据源的情况

            if (this.Columns == null || this.Columns.Count == 0)
            {
                errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [Columns] property", this.Name, Utility.GetString("Columns", "Columns")));
                isSucc = false;
            }

            foreach (DataGridViewColumn col in this.Columns)
            {
                IValidateService validateSvc = col as IValidateService;
                if (validateSvc != null)
                {
                    bool isOk = validateSvc.ValidateForDesign(errors);
                    if (isOk == false)
                    {
                        isSucc = isOk;
                    }
                }
            }

            return isSucc;
        }
        #endregion

        #region IInitiDataService接口实现

        public void InitData(IServiceContainerManager containerService, IDictionary<string, object> vars)
        {
            foreach (DataGridViewColumn col in this.Columns)
            {
                if (col is IInitiDataService)
                {
                    IInitiDataService s = col as IInitiDataService;
                    if (s != null)
                    {
                        s.InitData(containerService, vars);
                    }
                }

            }


            //foreach (DataGridViewRow item in this.Rows)
            //{
            //    foreach (DataGridViewColumn ce in this.Columns)
            //    {
            //        if (ce is LWCurrencyColumn)
            //        {
            //            if (currencyID == Guid.Empty)
            //            {
            //                ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
            //                if (configure != null)
            //                {
            //                    currencyID = configure.StandardCurrencyID;
            //                }

            //            }
            //            item.Cells[ce.Name].Value = currencyID;
            //        }
            //    }
            //}
        }

        #endregion


        #region ITable接口实现

        /// <summary>
        /// 生成表
        /// </summary>
        /// <returns></returns>
        public DataTable BuildTable()
        {
            string tableName = Utility.GetPascalProperty(this.Name);
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = this.Name;
            }


            DataTable table = new DataTable(tableName);
            foreach (IColumn c in this.Columns)
            {
                if (string.IsNullOrEmpty(c.ColumnName)) continue;
                if (c.FiledType == FieldType.None) continue;

                DataColumn dc = new DataColumn(c.ColumnName, c.ColumnType);
                dc.AllowDBNull = c.AllowNull;
                if (c.ColumnType.Equals(typeof(string)))
                {
                    dc.MaxLength = c.MaxLength > 0 ? c.MaxLength : 50;
                }
                dc.Caption = c.Caption;
                dc.DefaultValue = Utility.GetDefaultValue(c.ColumnType);
                table.Columns.Add(dc);
            }

            return table;
        }



        #endregion


        #region 方法重载

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.IsCurrentCellInEditMode)
            {
                if (keyData == Keys.Enter)
                {
                    if (this.CurrentCell != null &&
                        this.CurrentCell.Value != null)
                    {
                        this.EndEdit();
                        IDataFinder finder = null;
                        if (this.Columns[this.CurrentCell.ColumnIndex] is LWStringColumn)
                        {
                            LWStringColumn col = this.Columns[this.CurrentCell.ColumnIndex] as LWStringColumn;
                            switch (col.FiledType)
                            {
                                case FieldType.Other:
                                    break;
                                case FieldType.Department:
                                    break;
                                case FieldType.User:
                                    this.currentCell = CurrentCell;
                                    int userColunmIndex = 0;
                                    foreach (DataGridViewColumn item in this.Columns)
                                    {
                                        if (item is LWStringColumn)
                                        {
                                            LWStringColumn column = item as LWStringColumn;
                                            if (column.FiledType == FieldType.UserID)
                                            {
                                                userColunmIndex = column.HeaderCell.ColumnIndex;
                                                break;
                                            }
                                        }
                                    }
                                    string property = "Code";
                                    string code = string.Empty;
                                    string name = string.Empty;
                                    if (Utility.IsNumAndLetter(this.currentCell.Value.ToString()))
                                    {
                                        property = "Code";
                                        code = this.currentCell.Value.ToString();
                                    }
                                    else
                                    {
                                        property = "Name";
                                        name = this.currentCell.Value.ToString();
                                    }
                                    List<UserList> list = UserService.GetUserListByList(code, name, null, null, null, null, true, true, 0);
                                    if (list != null && list.Count == 1)
                                    {
                                        this.currentCell.Value = LocalData.IsEnglish ? list[0].EName : list[0].CName;
                                        //设置UserID
                                        this.Rows[this.currentCell.RowIndex].Cells[userColunmIndex].Value = list[0].ID;
                                    }
                                    else
                                    {
                                        finder = DataFinderFactory.GetDataFinder(SystemFinderConstants.UserFinder);
                                        finder.PickOne(this.currentCell.Value.ToString(), property, null, userResultValue, FinderTriggerType.KeyEnter,
                                            ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
                                        finder.DataChoosed -= userFinder_DataChoosed;
                                        finder.DataChoosed += userFinder_DataChoosed;
                                        if (string.IsNullOrEmpty(this.currentCell.Value.ToString()))
                                            this.Rows[this.currentCell.RowIndex].Cells[userColunmIndex].Value = Guid.Empty;
                                    }
                                    break;
                                case FieldType.Job:
                                    break;
                                case FieldType.Customer:
                                    this.currentCell = CurrentCell;
                                    int customerColunmIndex = 0;
                                    foreach (DataGridViewColumn item in this.Columns)
                                    {
                                        if (item is LWStringColumn)
                                        {
                                            LWStringColumn column = item as LWStringColumn;
                                            if (column.FiledType == FieldType.CustomerID)
                                            {
                                                customerColunmIndex = column.HeaderCell.ColumnIndex;
                                                break;
                                            }
                                        }
                                    }
                                    List<CustomerInfo> lst = CustomerService.GetCustomerListBySearch(this.CurrentCell.Value.ToString(), string.Empty, string.Empty, string.Empty, string.Empty
                                                                        , Guid.Empty, null
                                                                        , CustomerStateType.Valid
                                                                        , null
                                                                        , false, null, null, null, null, null, false, null, 100);
                                    if (lst != null && lst.Count == 1)
                                    {
                                        this.currentCell.Value = LocalData.IsEnglish ? lst[0].EName : lst[0].CName;
                                        //设置CustomerID
                                        this.Rows[this.currentCell.RowIndex].Cells[customerColunmIndex].Value = lst[0].ID;
                                    }
                                    else
                                    {
                                        finder = DataFinderFactory.GetDataFinder(CommonFinderConstants.CustoemrFinder);
                                        finder.PickOne(this.currentCell.Value.ToString(), "Code/Name", null, customerResultValue, FinderTriggerType.KeyEnter,
                                            ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
                                        finder.DataChoosed -= customerFinder_DataChoosed;
                                        finder.DataChoosed += customerFinder_DataChoosed;
                                        if (string.IsNullOrEmpty(this.currentCell.Value.ToString()))
                                            this.Rows[this.currentCell.RowIndex].Cells[customerColunmIndex].Value = Guid.Empty;
                                    }
                                    break;
                                case FieldType.GLCode:
                                    break;
                                case FieldType.None:
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void userFinder_DataChoosed(object sender, DataFindEventArgs e)
        {
            this.currentCell.Tag = e.Data[0];
            this.currentCell.Value = LocalData.IsEnglish ? e.Data[2] : e.Data[3];
            //设置UserID
            foreach (DataGridViewColumn item in this.Columns)
            {
                if (item is LWStringColumn)
                {
                    LWStringColumn column = item as LWStringColumn;
                    if (column.FiledType == FieldType.UserID)
                    {
                        this.Rows[this.currentCell.RowIndex].Cells[column.HeaderCell.ColumnIndex].Value = string.IsNullOrEmpty(this.currentCell.Value.ToString()) ? Guid.Empty : new Guid(e.Data[0].ToString());
                        break;
                    }
                }
            }
            
        }

        void customerFinder_DataChoosed(object sender, DataFindEventArgs e)
        {
            this.currentCell.Tag = e.Data[0];
            this.currentCell.Value = LocalData.IsEnglish ? e.Data[2] : e.Data[3];
            //设置CustomerID
            foreach (DataGridViewColumn item in this.Columns)
            {
                if (item is LWStringColumn)
                {
                    LWStringColumn column = item as LWStringColumn;
                    if (column.FiledType == FieldType.CustomerID)
                    {
                        this.Rows[this.currentCell.RowIndex].Cells[column.HeaderCell.ColumnIndex].Value = string.IsNullOrEmpty(this.currentCell.Value.ToString()) ? Guid.Empty : new Guid(e.Data[0].ToString());
                        break;
                    }
                }
            }
        }

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception != null)
            {
                // this[e.ColumnIndex, e.RowIndex].ErrorText = e.Exception.Message;
                e.Cancel = true;
                return;
            }

            base.OnDataError(displayErrorDialogIfNoHandler, e);
        }

        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewColumn c in this.Columns)
            {
                if (c is LWCurrencyColumn)
                {

                    if (currencyID == Guid.Empty && this.Rows[e.RowIndex].Cells[c.Name].Value == null)
                    {
                        ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                        if (configure != null)
                        {
                            currencyID = configure.StandardCurrencyID;
                        }
                        this.Rows[e.RowIndex].Cells[c.Name].Value = currencyID;
                    }
                }
            }
        }

        protected override void OnDefaultValuesNeeded(DataGridViewRowEventArgs e)
        {
            base.OnDefaultValuesNeeded(e);
            foreach (DataGridViewColumn c in this.Columns)
            {
                if (c is LWCurrencyColumn)
                {

                    if (currencyID == Guid.Empty)
                    {
                        ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                        if (configure != null)
                        {
                            currencyID = configure.StandardCurrencyID;
                        }

                    }
                    e.Row.Cells[c.Name].Value = currencyID;
                }
                else if (c is LWDeptColumn)
                {
                    if (deptID == Guid.Empty)
                    {
                        List<UserOrganizationTreeList> treeList = UserService.GetUserOrganizationTreeList(LocalData.UserInfo.LoginID);
                        OrganizationList obj = (from d in treeList
                                                       select new OrganizationList
                                                       {
                                                           ID = d.ID,
                                                           Code = d.Code,
                                                           CreateBy = d.CreateBy,
                                                           CreateDate = d.CreateDate,
                                                           CShortName = d.CShortName,
                                                           EShortName = d.EShortName,
                                                           FullName = d.FullName,
                                                           HierarchyCode = d.HierarchyCode,
                                                           IsDefault = d.IsDefault,
                                                           IsValid = d.IsValid,
                                                           ParentID = d.ParentID,
                                                           Type = d.Type
                                                       }).FirstOrDefault();
                        if (obj != null)
                        {
                            deptID = obj.ID;
                        }

                    }
                    e.Row.Cells[c.Name].Value = deptID;
                }

            }
        }

        private Guid GetSolutionID()
        {
            Guid solutionID = Guid.Empty;
            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
            if (configureInfo != null)
            {
                solutionID = configureInfo.SolutionID;
            }
            return solutionID;
        }

        SearchConditionCollection GetSearchCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", GetSolutionID(), false);
            conditions.AddWithValue("IsFee", true, false);
            return conditions;
        }

        #endregion

        #region ITable接口实现

        /// <summary>
        /// 生成表
        /// </summary>
        /// <returns></returns>
        public DataTable CreateTable()
        {
            string tableName = Utility.GetPascalProperty(this.Name);
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = this.Name;
            }


            DataTable table = new DataTable(tableName);
            foreach (IColumn c in this.Columns)
            {
                if (string.IsNullOrEmpty(c.ColumnName)) continue;
                if (c.FiledType == FieldType.None) continue;

                DataColumn dc = new DataColumn(c.ColumnName, c.ColumnType);
                dc.AllowDBNull = c.AllowNull;
                if (c.ColumnType.Equals(typeof(string)))
                {
                    dc.MaxLength = c.MaxLength > 0 ? c.MaxLength : 50;
                }
                dc.Caption = c.Caption;
                dc.DefaultValue = Utility.GetDefaultValue(c.ColumnType);
                table.Columns.Add(dc);
            }

            return table;
        }



        #endregion

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // LWDataGridView
            // 
            this.RowTemplate.Height = 23;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
    }



}
