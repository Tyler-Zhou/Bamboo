
//-----------------------------------------------------------------------
// <copyright file="WFForm.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Data;
    using System.Drawing.Design;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using ICP.WF.ServiceInterface;
    using DevExpress.Utils;
    using DevExpress.Utils.Drawing;
    using DevExpress.XtraEditors;
    using ICP.Framework.CommonLibrary.Attributes;

    /// <summary>
    /// 主表单控件
    /// </summary>
    [ComVisible(true)]
    [Designer("System.Windows.Forms.Design.UserControlDocumentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [Designer("System.Windows.Forms.Design.ControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [DesignerCategory("UserControl")]
    [DefaultProperty("DataFile")]
    public partial class LWBaseForm : DevExpress.XtraEditors.XtraUserControl, IServiceProvider, IServiceContainer, ITable
    {
        #region 本地变量
        private Dictionary<string, Control> dataBindingPropertys = new Dictionary<string, Control>();
        private List<IFileService> fileServices = new List<IFileService>();
        private List<IValidateService> validateServices = new List<IValidateService>();
        private List<ITitle> titleServices = new List<ITitle>();
        private List<IDataSourceService> dataSourceServices = new List<IDataSourceService>();
        private List<IInitiDataService> initiDataServices = new List<IInitiDataService>();
        private List<IColumn> columnServices = new List<IColumn>();
        private List<ITable> tableServices = new List<ITable>();
        public List<LWDataGridView> gridViews = new List<LWDataGridView>();

        private DataTable currentTable = null;
        private DataSet dataSource = null;

        #endregion

        #region 初始化


        public LWBaseForm()
        {
            InitializeComponent();
            this.Disposed += delegate {
                if (this.dataBindingPropertys != null)
                {
                    this.dataBindingPropertys.Clear();
                    this.dataBindingPropertys = null;
                }
                if (this.fileServices != null)
                {
                    this.fileServices.Clear();
                    this.fileServices = null;
                }
                if (this.validateServices != null)
                {
                    this.validateServices.Clear();
                    this.validateServices = null;
                }
                if (this.titleServices != null)
                {
                    this.titleServices.Clear();
                    this.titleServices = null;
                }
                if (this.dataSourceServices != null)
                {
                    this.dataSourceServices.Clear();
                    this.dataSourceServices = null;
                }
                if (this.initiDataServices != null)
                {
                    this.initiDataServices.Clear();
                    this.initiDataServices = null;
                }
                if (this.columnServices != null)
                {
                    this.columnServices.Clear();
                    this.columnServices = null;
                }
                if (this.tableServices != null)
                {
                    this.tableServices.Clear();
                    this.tableServices = null;
                }
                this.currentTable = null;
                this.dataSource = null;
                this.obs = null;
                this._hideFileds = null;
                if (this.errorTip != null)
                {
                    this.errorTip.DataSource = null;
                    this.errorTip = null;
                }
            
            };
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode == false)
            {
                //初始变量
                InitServicesForRuntime(this);

                //初始化bingdingsource数据
                InitBindingSourceProperty();

                //初始化子控件的数据源
                InitData();

                //控件绑定数据源 
                BindingProperty(currentTable);

                foreach (LWDataGridView grid in gridViews)
                {
                    grid.Searcher();
                }
            }
            //else
            //{
            //    InitDesignModeServiceListProperty(this);
            //}
        }

        #endregion

        #region 公共方法,属性

        public bool IsShowLedgerPart
        {
            get;
            set;
        }

        /// <summary>
        /// 设置容器控件的所有子控件只读
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="isReadOnly"></param>
        public void SetReadOnly(bool isReadOnly)
        {
            SetReadOnly(this, isReadOnly);
          
        }
       
        /// <summary>
        /// 设置控件只读
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="isReadOnly"></param>
        private void SetReadOnly(Control parentControl, bool isReadOnly)
        {
            if (IsContainer(parentControl) == false)
            {
                if (parentControl is LWTextBox)
                {
                    (parentControl as LWTextBox).ReadOnly = isReadOnly;
                }
                else if (parentControl is LWMultiTextBox)
                {
                    (parentControl as LWMultiTextBox).ReadOnly = isReadOnly;
                }
                else if (parentControl is LWComboBoxTreeView)
                {
                    (parentControl as LWComboBoxTreeView).ReadOnly = !isReadOnly;
                }
                else if (parentControl is LWNumericCalcEdit)
                { 
                     (parentControl as LWNumericCalcEdit).ReadOnly = isReadOnly;
                }
                else if (parentControl is LWRadioGroup)
                {
                    (parentControl as LWRadioGroup).Enabled = isReadOnly;
                }
                else if (parentControl is LWRadioButton)
                {
                    (parentControl as LWRadioButton).Enabled = isReadOnly;
                }
                else if (parentControl is LWDatePicker)
                {
                    (parentControl as LWDatePicker).ReadOnly = isReadOnly;
                }
                else if (parentControl is LWCheckBox)
                {
                    (parentControl as LWCheckBox).ReadOnly = isReadOnly;
                }
                else if (parentControl is LWComBox)
                {
                    (parentControl as LWComBox).ReadOnly = isReadOnly;
                }
                else if (parentControl is LWDataGridView)
                {
                    (parentControl as LWDataGridView).AllowUserToAddRows = false;
                    (parentControl as LWDataGridView).ReadOnly = isReadOnly;
                }
            }
            else
            {
                foreach (Control ctrl in parentControl.Controls)
                {
                    SetReadOnly(ctrl, isReadOnly);
                }
            }
        }

        /// <summary>
        /// 是否为容器
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        private bool IsContainer(Control ctrl)
        {
            if (ctrl.HasChildren == false) return false;
            if (ctrl is LWDataGridView) return false;
            if (ctrl is LWRadioGroup) return false;
            if (ctrl is ICPFilePanel) return false;

            //如果是FlowLayoutPancel
            if (ctrl is FlowLayoutPanel && ctrl.Controls.Count > 0)
            {
                bool containerOther = false;
                foreach (Control c in ctrl.Controls)
                {
                    if (!(c is RadioButton))
                    {
                        containerOther = true;
                    }
                }
                if (!containerOther) return false;
            }
            //该控件为容器
            return true;
        }
        
        /// <summary>
        /// 隐藏字段列表
        /// </summary>
        DataColumnItemCollection _hideFileds = new DataColumnItemCollection();
        [Browsable(true), Editor(typeof(DataItemTypeEditor), typeof(UITypeEditor)), RefreshProperties(RefreshProperties.All), DefaultValue((string)null), SRCategory("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [SRDisplayName("DispHideFileds"), ICPBrowsable(true)]
        public DataColumnItemCollection HideFileds
        {
            get { return _hideFileds; }
            set { _hideFileds = value; }
        }


        private Dictionary<string, object> obs = new Dictionary<string, object>();
        /// <summary>
        /// 该页面所有控件数据源列表
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Dictionary<string, object> BindingSources
        {
            get { return obs; }
            set { obs = value; }
        }



        /// <summary>
        /// 名称
        /// </summary>
        [SRDisplayName("Name"),
        SRDescription("Name"),
        ICPBrowsable(true),
        SRCategory("Base")]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        string eTitle;
        /// <summary>
        /// 英文环境下表单呈现的时候显示的英文标题
        /// </summary>
        [Browsable(true)]
        [SRCategory("Custom"), SRDescription("EnglishDescription")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("EText"), ICPBrowsable(true)]
        public string ETitle
        {
            get { return eTitle; }
            set { eTitle = value; }
        }



        string cTitle;
        /// <summary>
        /// 中文环境下表单呈现的时候显示的标题
        /// </summary>
        [Browsable(true)]
        [SRCategory("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("CText"), ICPBrowsable(true),  SRDescription("ChineseDescription")]
        public string CTitle
        {
            get { return cTitle; }
            set { cTitle = value; }
        }


        System.Collections.Generic.List<string> controlNames = new System.Collections.Generic.List<string>();
        /// <summary>
        /// 控件名列表
        /// </summary>
        [Browsable(false)]
        public System.Collections.Generic.List<string> ControlNames
        {
            get
            {
                controlNames.Clear();
                GetChildControlNames(this);
                return controlNames;
            }
        }

        /// <summary>
        /// 递归获取所有子控件
        /// </summary>
        /// <returns></returns>
        public List<string> GetTitleColumns()
        {
            List<string> titles = new List<string>();
            LWTableLayoutPanel tlp = null;
            foreach (Control ctr in this.Controls)
            {
                if (ctr is LWTableLayoutPanel)
                {
                    tlp = (LWTableLayoutPanel)ctr;
                }
            }

            if (tlp == null) return titles;

            foreach (Control ctrl in tlp.Controls)
            {
                IColumn service = ctrl as IColumn;
                if (service != null)
                {
                    if (string.IsNullOrEmpty(service.Caption))
                    {
                        continue;
                    }

                    if (titles.Contains(service.Caption) == false)
                    {
                        titles.Add(service.Caption);
                    }
                }
            }

            if (_hideFileds != null)
            {
                foreach (DataColumnItem c in HideFileds)
                {
                    if (string.IsNullOrEmpty(c.Caption)) continue;

                    if (titles.Contains(c.Caption) == false)
                    {
                        titles.Add(c.Caption);
                    }
                }
            }

            return titles;
        }



        private ErrorProvider errorTip;
        /// <summary>
        /// 错误提示控件
        /// </summary>
        protected ErrorProvider ErrorTip
        {
            get
            {
                if (errorTip == null)
                {
                    errorTip = new ErrorProvider(this);
                }

                return errorTip;
            }
        }

        /// <summary>
        ///数据源
        /// </summary>
        [Browsable(false)]
        public DataSet DataSource
        {
            get
            {
                this.Select();
                if (currentTable != null)
                {
                    this.BindingContext[currentTable].EndCurrentEdit();
                }

                if (dataSource != null)
                {
                    foreach (IDataSourceService service in dataSourceServices)
                    {
                        if (service.DataTable != null)
                        {
                            dataSource.Merge(service.DataTable as DataTable);
                        }
                    }
                }

                return dataSource;
            }
            set
            {
                if (value == null) return;

                dataSource = value;
            }
        }


        /// <summary>
        /// 根据表单生成对应的数据源
        /// </summary>
        /// <param name="datasetName"></param>
        /// <returns></returns>
        public DataSet BuildDataSet(string datasetName)
        {
            //_dataFile = datasetName;
            InitServicesForDesign(this);

            DataSet ds = new DataSet(datasetName);
            DataTable mainTable = this.BuildTable();
            ds.Tables.Add(mainTable);
            foreach (ITable t in tableServices)
            {
                ds.Tables.Add(t.BuildTable());
            }

            return ds;
        }


        /// <summary>
        /// 获取选择表的字段列表
        /// </summary>
        /// <returns></returns>
        public string[] GetFields()
        {
            if (dataSource == null) return new string[] { };

            System.Collections.Generic.List<string> fs = new System.Collections.Generic.List<string>();
            foreach (DataColumn col in dataSource.Tables[0].Columns)
            {
                fs.Add(col.ColumnName);
            }

            return fs.ToArray();
        }


        /// <summary>
        /// 获取选择表的字段列表
        /// </summary>
        /// <returns></returns>
        public string[] GetFieldCaptions()
        {
            if (dataSource == null) return new string[] { };

            System.Collections.Generic.List<string> fs = new System.Collections.Generic.List<string>();
            foreach (DataColumn col in dataSource.Tables[0].Columns)
            {
                fs.Add(col.Caption);
            }

            return fs.ToArray();
        }

        /// <summary>
        /// 获取数据集里的表列表
        /// </summary>
        /// <returns></returns>
        public string[] GetTableNames()
        {
            System.Collections.Generic.List<string> tabs = new System.Collections.Generic.List<string>();
            if (dataSource != null)
            {
                foreach (DataTable dt in dataSource.Tables)
                {
                    tabs.Add(dt.TableName);
                }
            }

            return tabs.ToArray();
        }

        private Dictionary<string, object> commonConstants = new Dictionary<string, object>();
        /// <summary>
        /// 通用常量
        /// </summary>
        [Browsable(false)]
        public Dictionary<string, object> CommonConstants
        {
            get { return commonConstants; }
            set { commonConstants = value; }
        }
        #endregion

        #region 私有方法

        /*初始化所有服务接口列表*/
        private void InitServicesForRuntime(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                /*找出所有实现IValidateService的控件*/
                IValidateService vservice = ctrl as IValidateService;
                if (vservice != null)
                {
                    if (validateServices.Contains(vservice) == false)
                    {
                        validateServices.Add(vservice);
                    }

                }

                //获取初始化数据源的服务
                IInitiDataService initDataService = ctrl as IInitiDataService;
                if (initDataService != null)
                {
                    if (initiDataServices.Contains(initDataService) == false)
                    {
                        initiDataServices.Add(initDataService);
                    }
                }


                ////获取生成单号的服务
                //IBuildNumberService ibuildNumber = ctrl as IBuildNumberService;
                //if (ibuildNumber != null)
                //{
                //    if (buildNumberServices.Contains(ibuildNumber) == false)
                //    {
                //        buildNumberServices.Add(ibuildNumber);
                //    }
                //}

                /*获取所有有子集数据源的服务*/
                IDataSourceService dataService = ctrl as IDataSourceService;
                if (dataService != null)
                {
                    if (dataSourceServices.Contains(dataService) == false)
                    {
                        dataSourceServices.Add(dataService);
                    }
                }
                //文件服务
                IFileService fileService = ctrl as IFileService;
                if (fileService != null)
                {
                    if (!fileServices.Contains(fileService))
                    {
                        fileServices.Add(fileService);
                    }
                }

                /*获取所有需要绑定数据的控件*/
                IBindingService service = ctrl as IBindingService;
                IColumn clservice = ctrl as IColumn;
                if (service != null && clservice != null)
                {
                    if (string.IsNullOrEmpty(service.DataProperty) == false
                        && string.IsNullOrEmpty(service.ControlProperty) == false
                        && clservice.FiledType != FieldType.None
                        && dataBindingPropertys.ContainsKey(service.DataProperty) == false)
                    {
                        dataBindingPropertys.Add(service.DataProperty, ctrl);
                    }
                }

                if (ctrl.HasChildren && (ctrl is LWDataGridView) == false)
                {
                   
                    InitServicesForRuntime(ctrl);
                }

                if ((ctrl is LWDataGridView) && !gridViews.Contains(ctrl as LWDataGridView))
                {
                    gridViews.Add(ctrl as LWDataGridView);
                }
                
            }
        }


        /*初始化设计所有服务接口列表*/
        private void InitServicesForDesign(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                IValidateService vservice = ctrl as IValidateService;
                if (vservice != null)
                {
                    if (validateServices.Contains(vservice) == false)
                    {
                        validateServices.Add(vservice);
                    }
                }

                //获取该
                IColumn colservice = ctrl as IColumn;
                if (colservice != null)
                {
                    if (columnServices.Contains(colservice) == false)
                    {
                        columnServices.Add(colservice);
                    }
                }

                //获取初始化数据源的服务
                ITitle titleSvc = ctrl as ITitle;
                if (titleSvc != null)
                {
                    if (titleServices.Contains(titleSvc) == false)
                    {
                        titleServices.Add(titleSvc);
                    }
                }

                //获取可以生成表的服务
                ITable tbservice = ctrl as ITable;
                if (tbservice != null)
                {
                    if (tableServices.Contains(tbservice) == false)
                    {
                        tableServices.Add(tbservice);
                    }
                }
                //文件服务
                IFileService fileService = ctrl as IFileService;
                if (fileService != null)
                {
                    if (!fileServices.Contains(fileService))
                    {
                        fileServices.Add(fileService);
                    }
                }

                if (ctrl.HasChildren && (ctrl is LWDataGridView) == false)
                {
                    InitServicesForDesign(ctrl);
                }
            }
        }

        /*初始化BindingSource数据*/
        public void InitBindingSourceProperty()
        {
            foreach (object v in BindingSources.Values)
            {
                IInitiDataService service = v as IInitiDataService;
                if (service != null)
                {
                    if (initiDataServices.Contains(service) == false)
                    {
                        initiDataServices.Add(service);
                    }
                }
            }
        }

        /*初始化所有控件的数源*/
        public void InitData()
        {
            if (dataSource != null)
            {
                currentTable = dataSource.Tables["MainTable"];
                Utility.SetDefaultValue(currentTable);
                if (currentTable != null && currentTable.Rows.Count == 0)
                {
                    DataRow dr = currentTable.NewRow();
                    currentTable.Rows.Add(dr);
                    currentTable.AcceptChanges();
                }
            }

            //初始化所有实现IDataSourceService接口的控件的数据源
            foreach (IDataSourceService service in dataSourceServices)
            {
                if (dataSource != null)
                {
                    service.DataTable = dataSource.Tables[service.DataTableName];
                }
            }

            //初始化所有实现IInitiDataService接口的控件的数据源
            foreach (IInitiDataService s in initiDataServices)
            {
                if (s != null)
                {
                    s.InitData(ServiceContainer, this.CommonConstants);
                }
            }

        }
        /// <summary>
        /// 设置按钮只读
        /// </summary>
        public void SetButtonReadonly()
        {
            foreach (IFileService s in fileServices)
            {
                s.SetButtonReadonly();
            }
        }

        /*绑定控件属性与数据源*/
        public void BindingProperty(DataTable source)
        {
            foreach (Control ctrl in dataBindingPropertys.Values)
            {
                IBindingService service = ctrl as IBindingService;
                if (service != null)
                {
                    service.Binding(source);
                }
            }
        }

        /// <summary>
        /// 递归获取所有子控件
        /// </summary>
        /// <param name="parent">父控件</param>
        private void GetChildControlNames(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                IBindingService service = ctrl as IBindingService;
                IColumn colService = ctrl as IColumn;
                if (service != null && colService != null)
                {
                    if (colService.FiledType == FieldType.None) continue;

                    if (controlNames.Contains(ctrl.Name) == false)
                    {
                        controlNames.Add(ctrl.Name);
                    }
                }
                else if (ctrl.HasChildren)
                {
                    GetChildControlNames(ctrl);
                }
            }
        }

        #endregion



        #region 隐藏在属性栏位的显示

        [Browsable(false)]
        private bool Locked { get; set; }
        

        [Browsable(false)]
        private new AutoScaleMode AutoScaleMode
        {
            get
            {
                return base.AutoScaleMode;
            }
            set
            {
                base.AutoScaleMode = value;
            }
        }

        [Browsable(false)]
        public new AppearanceObject Appearance
        {
            get
            {
                return base.Appearance;
            }
        }

        [Browsable(false)]
        public override DevExpress.LookAndFeel.UserLookAndFeel LookAndFeel
        {
            get
            {
                return base.LookAndFeel;
            }
        }

        [Browsable(false)]
        public override RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }
            set
            {
                base.RightToLeft = value;
            }
        }

        [Browsable(false)]
        protected override bool DoubleBuffered
        {
            get
            {
                return base.DoubleBuffered;
            }
            set
            {
                base.DoubleBuffered = value;
            }
        }

        /// <summary>
        /// 停靠方式
        /// </summary>
        [Browsable(false)]
        public override System.Windows.Forms.DockStyle Dock
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
        /// 是否许拖放
        /// </summary>
        [Browsable(false)]
        public override bool AllowDrop
        {
            get
            {
                return base.AllowDrop;
            }
            set
            {
                base.AllowDrop = value;
            }
        }

        [Browsable(false)]
        public override AnchorStyles Anchor
        {
            get
            {
                return base.Anchor;
            }
            set
            {
                base.Anchor = value;
            }
        }

        [Browsable(false)]
        public override System.Drawing.Point AutoScrollOffset
        {
            get
            {
                return base.AutoScrollOffset;
            }
            set
            {
                base.AutoScrollOffset = value;
            }
        }

        [Browsable(false)]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = value;
            }
        }

        [Browsable(false)]
        public override System.Drawing.Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        [Browsable(false)]
        public override System.Drawing.Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }

        [Browsable(false)]
        public override ImageLayout BackgroundImageLayout
        {
            get
            {
                return base.BackgroundImageLayout;
            }
            set
            {
                base.BackgroundImageLayout = value;
            }
        }

        [Browsable(false)]
        public override BindingContext BindingContext
        {
            get
            {
                return base.BindingContext;
            }
            set
            {
                base.BindingContext = value;
            }
        }

        //[Browsable(false)]
        //protected override bool CanEnableIme
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        [Browsable(false)]
        protected override bool CanRaiseEvents
        {
            get
            {
                return base.CanRaiseEvents;
            }
        }

        [Browsable(false)]
        public override ContextMenu ContextMenu
        {
            get
            {
                return base.ContextMenu;
            }
            set
            {
                base.ContextMenu = value;
            }
        }

        [Browsable(false)]
        public override ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return base.ContextMenuStrip;
            }
            set
            {
                base.ContextMenuStrip = value;
            }
        }

        [Browsable(false)]
        public override Cursor Cursor
        {
            get
            {
                return base.Cursor;
            }
            set
            {
                base.Cursor = value;
            }
        }

        [Browsable(false)]
        public override System.Drawing.Size MaximumSize
        {
            get
            {
                return base.MaximumSize;
            }
            set
            {
                base.MaximumSize = value;
            }
        }

        [Browsable(false)]
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



        [Browsable(false)]
        public new ControlBindingsCollection DataBindings
        {
            get
            {
                return base.DataBindings;
            }

        }


        [Browsable(false)]
        public new System.Drawing.Point Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                base.Location = value;
            }
        }


        [Browsable(false)]
        public new ISite Site
        {
            get
            {
                return base.Site;
            }
            set
            {
                base.Site = value;
            }
        }



        [Browsable(false)]
        public new int Width
        {
            get
            {
                return base.Width;
            }
            set
            {
                base.Width = value;
            }
        }








        [Browsable(false)]
        public new Control TopLevelControl
        {
            get
            {
                return base.TopLevelControl;
            }

        }


        [Browsable(false)]
        public new bool TabStop
        {
            get
            {
                return base.TabStop;
            }
            set
            {
                base.TabStop = value;
            }
        }



        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new System.Drawing.Size Size
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

        [Browsable(false)]
        public new Padding Margin
        {
            get
            {
                return base.Margin;
            }
            set
            {
                base.Margin = value;
            }
        }





        [Browsable(false)]
        public new string AccessibleDescription
        {
            get
            {
                return base.AccessibleDescription;
            }
            set
            {
                base.AccessibleDescription = value;
            }
        }


        [Browsable(false)]
        public new string AccessibleName
        {
            get
            {
                return base.AccessibleName;
            }
            set
            {
                base.AccessibleName = value;
            }
        }


        [Browsable(false)]
        public new AccessibleRole AccessibleRole
        {
            get
            {
                return base.AccessibleRole;
            }
            set
            {
                base.AccessibleRole = value;
            }
        }


        [Browsable(false)]
        public new System.Drawing.Size MinimumSize
        {
            get
            {
                return base.MinimumSize;
            }
            set
            {
                base.MinimumSize = value;
            }
        }


        [Browsable(false)]
        public new bool CausesValidation
        {
            get
            {
                return base.CausesValidation;
            }
            set
            {
                base.CausesValidation = value;
            }
        }






        [Browsable(false)]
        public new object Tag
        {
            get
            {
                return base.Tag;
            }
            set
            {
                base.Tag = value;
            }
        }


        [Browsable(false)]
        public new bool UseWaitCursor
        {
            get
            {
                return base.UseWaitCursor;
            }
            set
            {
                base.UseWaitCursor = value;
            }

        }


        [Browsable(false)]
        public new ImeMode ImeMode
        {
            get
            {
                return base.ImeMode;
            }
            set
            {
                base.ImeMode = value;
            }

        }


        [Browsable(false)]
        public new System.Drawing.SizeF AutoScaleDimensions
        {
            get
            {
                return base.AutoScaleDimensions;
            }
            set
            {
                base.AutoScaleDimensions = value;
            }

        }






        [Browsable(false)]
        public new bool AutoScroll
        {
            get
            {
                return base.AutoScroll;
            }
            set
            {
                base.AutoScroll = value;
            }




        }



        [Browsable(false)]
        public new System.Drawing.Size AutoScrollMargin
        {
            get
            {
                return base.AutoScrollMargin;
            }
            set
            {
                base.AutoScrollMargin = value;
            }

        }



        [Browsable(false)]
        public new System.Drawing.Size AutoScrollMinSize
        {
            get
            {
                return base.AutoScrollMinSize;
            }
            set
            {
                base.AutoScrollMinSize = value;
            }

        }



        [Browsable(false)]
        public new System.Drawing.Point AutoScrollPosition
        {
            get
            {
                return base.AutoScrollPosition;
            }
            set
            {
                base.AutoScrollPosition = value;
            }

        }


        [Browsable(false)]
        public new AutoSizeMode AutoSizeMode
        {
            get
            {
                return base.AutoSizeMode;
            }
            set
            {
                base.AutoSizeMode = value;
            }

        }


        [Browsable(false)]
        public new AutoValidate AutoValidate
        {
            get
            {
                return base.AutoValidate;
            }
            set
            {
                base.AutoValidate = value;
            }

        }



        [Browsable(false)]
        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
            }

        }


        [Browsable(false)]
        public new Padding Padding
        {
            get
            {
                return base.Padding;
            }
            set
            {
                base.Padding = value;
            }

        }
        #endregion

        #region IServiceContainer接口实现

        private IServiceContainerManager serviceContainer;
        /// <summary>
        /// 服务容器
        /// </summary>
        [Browsable(false)]
        public IServiceContainerManager ServiceContainer
        {
            get { return serviceContainer; }
            set { serviceContainer = value; }
        }


        #endregion

        #region IServiceProvider接口实现

        public new object GetService(Type service)
        {
            return base.GetService(service);
        }

        #endregion

        #region ITable接口实现

        /// <summary>
        /// 根据表单生成表结构
        /// </summary>
        /// <returns></returns>
        public DataTable BuildTable()
        {
            DataTable table = new DataTable("MainTable");
            foreach (IColumn c in columnServices)
            {
                if (string.IsNullOrEmpty(c.ColumnName))
                {
                    continue;
                }
                if (c.FiledType == FieldType.None)
                {
                    continue;
                }
                if( table.Columns.Contains(c.ColumnName))
                {
                    continue;
                }

                DataColumn dc = new DataColumn(c.ColumnName, c.ColumnType);
                dc.AllowDBNull = c.AllowNull;
                if (c.ColumnType.Equals(typeof(string)))
                {
                    dc.MaxLength = c.MaxLength > 0 ? c.MaxLength : 50;
                }
                dc.Caption = c.Caption;
                table.Columns.Add(dc);
            }

            if (_hideFileds != null)
            {
                foreach (DataColumnItem c in HideFileds)
                {
                    if (string.IsNullOrEmpty(c.ColumnName))
                    {
                        continue;
                    }

                    DataColumn dc = new DataColumn(c.ColumnName, GetDataType(c.ColumnType, c.AllowNull == false));
                    dc.AllowDBNull = c.AllowNull;
                    if (c.ColumnType.Equals(DataItemType.String.ToString()))
                    {
                        dc.MaxLength = c.MaxLength > 0 ? c.MaxLength : 50;
                    }
                    dc.AllowDBNull = !c.AllowNull;
                    dc.Caption = c.Caption;
                    table.Columns.Add(dc);
                }
            }

            return table;
        }

        /// <summary>
        /// 根据子定义类型返回对应系统的数据类型
        /// </summary>
        /// <param name="columnType">列类型</param>
        /// <param name="must">是否必输项</param>
        /// <returns>返回对应系统的数据类型</returns>
        Type GetDataType(string columnType, bool must)
        {
            if (DataItemType.Boolean.ToString().Equals(columnType))
            {
                return must ? typeof(bool) : typeof(bool?);
            }
            else if (DataItemType.DateTime.ToString().Equals(columnType))
            {
                return must ? typeof(DateTime) : typeof(DateTime?);
            }
            else if (DataItemType.Decimal.ToString().Equals(columnType))
            {
                return must ? typeof(Decimal) : typeof(Decimal?);
            }
            else if (DataItemType.Guid.ToString().Equals(columnType))
            {
                return must ? typeof(Guid) : typeof(Guid?);

            }
            else if (DataItemType.Int.ToString().Equals(columnType))
            {
                return must ? typeof(int) : typeof(int?);
            }
            else if (DataItemType.String.ToString().Equals(columnType))
            {
                return typeof(string);
            }
            else if (DataItemType.Short.ToString().Equals(columnType))
            {
                return must ? typeof(short) : typeof(short?);
            }

            return typeof(string);
        }

        #endregion


        #region 验证方法

        /// <summary>
        /// 验证窗体的数据正确性
        /// </summary>
        /// <returns></returns>
        public bool ValidateForRuntime()
        {
            if (this.DesignMode)
            {
                throw new ApplicationException("This method is called only in the run mode.");
            }

            ErrorTip.Clear();
            bool isSucc = true;
            if (currentTable == null)
            {
                isSucc = false;

                

                XtraMessageBox.Show(Utility.GetString("Pleasesetthedatasource", "请先设置数据源"));

                return isSucc;
            }

            //验证实现IValidateService接口的控件
            List<string> errs = new List<string>();
            foreach (IValidateService s in validateServices)
            {
                s.ValidateForRuntime(errorTip, errs);
            }

            if (errs.Count > 0)
            {
                XtraMessageBox.Show(errs.ToCustomString("错误:"));
                isSucc = false;
            }


            return isSucc;
        }


        /// <summary>
        /// 设计时验证方法
        /// </summary>
        /// <returns></returns>
        public List<string> ValidateForDesign()
        {
            if (this.DesignMode == false)
            {
                throw new ApplicationException("This method is called only in the design mode.");
            }

            InitServicesForDesign(this);

            //验证实现IValidateService接口的控件
            List<string> errs = new List<string>();
            foreach (IValidateService s in validateServices)
            {
                s.ValidateForDesign(errs);
            }

            foreach (ITitle t in titleServices)
            {
                if (string.IsNullOrEmpty(t.CText)) continue;
                if (string.IsNullOrEmpty(t.EText)) continue;

                if (titleServices.FindAll(delegate(ITitle sv)
                                            {
                                                return string.IsNullOrEmpty(sv.CText) == false
                                                        && sv.CText.Equals(t.CText);
                                            }).Count > 1)
                {
                    errs.Add(Utility.GetString("CTextRepeat", "中文标题存在重复[" + t.CText + "]", this.Name, t.CText));
                }

                if (titleServices.FindAll(delegate(ITitle sv)
                                            {
                                                return string.IsNullOrEmpty(sv.CText) == false
                                                      && sv.EText.Equals(t.EText);
                                            }).Count > 1)
                {
                    errs.Add(Utility.GetString("ETextRepeat", "英文标题存在重复[" + t.EText + "]", this.Name, t.EText));
                }
            }
            bool columnIsNull = true;
            foreach (IColumn cl in columnServices)
            {
                if (cl.FiledType == FieldType.None)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(cl.ColumnName))
                {
                    continue;
                }
                columnIsNull = false;
                if (columnServices.FindAll(delegate(IColumn sv)
                {
                    return string.IsNullOrEmpty(sv.ColumnName) == false
                          && sv.ColumnName.Equals(cl.ColumnName) && sv.FiledType != FieldType.None;
                }).Count > 1)
                {
                    errs.Add(Utility.GetString("ColumnNameRepeat", "列名存在重复[" + cl.ColumnName + "]", this.Name, cl.ColumnName));
                    break;
                }

            }
            if (columnIsNull)
            {
                errs.Add(Utility.GetString("FormNotColumn", "表单{0}没有任何列", this.Name));
            }


            return errs;
        }

        #endregion


    }


}
