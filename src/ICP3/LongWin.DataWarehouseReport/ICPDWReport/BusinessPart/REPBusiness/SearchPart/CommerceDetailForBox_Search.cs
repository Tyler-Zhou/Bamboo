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
using LongWin.Framework.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using LongWin.CommonData.ServiceInterface;
using LongWin.CommonData.ServiceInterface.DataObjects;
using Agilelabs.Framework.Service;
using System.Xml;

namespace LongWin.DataWarehouseReport.WinUI
{
    /// <summary>
    ///  商务箱量利润详细表(detail) 
    /// </summary>
    public partial class CommerceDetailForBox_Search : UserControl
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


        string finderType = string.Empty;
        IDataFinder dataFinder = null;

        IDataFinderFactory _finderFactory;
        [ServiceDependency]
        public IDataFinderFactory FinderFactory
        {
            get { return _finderFactory; }
            set { _finderFactory = value; }
        }

        WorkItem _workItem;
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get { return _workItem; }
            set { _workItem = value; }
        }


        IInitializeService _initializeService;
        [ServiceDependency]
        public IInitializeService InitializeService
        {
            get { return _initializeService; }
            set
            {
                _initializeService = value;
                if (_initializeService != null)
                    REPModuleInit.InitializeService = _initializeService; 
                

            }
        }

        ITransportFoundationService _transportFoundationService;
        [ServiceDependency]
        public ITransportFoundationService TransportFoundationService
        {
            get { return _transportFoundationService; }
            set
            {

                _transportFoundationService = value;
                if (_transportFoundationService != null)
                    REPModuleInit.FoundationService = _transportFoundationService;
            }
        }

        #endregion

        #region 初始化

        public CommerceDetailForBox_Search()
        {
            InitializeComponent();
            this.cmbGroupBy.Items = null;
            if (!this.DesignMode)
            {
                this.Disposed += new EventHandler(CommerceDetailForBox_Search_Disposed);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitData();
        }

        private void InitData()
        {

            if (_repBaseDataService == null || _transportFoundationService == null)
            {
                //MessageBox.Show("服务为空,请检查");
                _repBaseDataService = REPModuleInit.RepService;
                _transportFoundationService = REPModuleInit.FoundationService;
                //return;
            }
            DataSet ds = this._repBaseDataService.GetStructureNodesForAgentByCurrentUser();
            this.ucSelectCompany.DataSource = ds;

            //MessageBox.Show(ds.Tables[0].Rows.Count.ToString());
            this.cmbChangeData.Items.Clear();
            this.cmbChangeData.Items.AddRange(new string[] {
                                                        Utility.GetValueString("全部", "全部") ,
                                                        Utility.GetValueString("是", "是") , 
                                                        Utility.GetValueString("否", "否") , 
                                                        });
            this.cmbChangeData.SelectedIndex = 0;

            this.ccmbJobType.Items = new string[] {
                                                        Utility.GetValueString("出口业务", "出口业务") ,
                                                        Utility.GetValueString("进口业务", "进口业务") , 
                                                        Utility.GetValueString("其他业务", "其他业务") , 
                                                        Utility.GetValueString("集运业务", "集运业务") ,
                                                        Utility.GetValueString("报关业务", "报关业务")
                                                        };


            this.cmbGroupBy.Text = Utility.GetValueString("业务员", "业务员") + "," + Utility.GetValueString("客户", "客户");
            this.mulShippingLine.DataSource = Utility.GetShppingLine(_transportFoundationService.GetShippingLineList(null, null, null, 0));


            ccmbJobType.TextChanaged += new EventHandler(ccmbJobType_TextChanaged);
            this.cmbGroupBy.TextChanaged += new EventHandler(cmbGroupBy_TextChanaged);
            ccmbJobType.Text = Utility.GetValueString("出口业务", "出口业务");

            this.cmbDateType.Items.Clear();
            this.cmbDateType.Items.AddRange(new object[] {
                                                        Utility.GetValueString("业务时间", "业务时间")});

            this.ultraExplorerBar1.Groups[0].Text = Utility.GetValueString("日期", "日期");
            this.ultraExplorerBar1.Groups[1].Text = Utility.GetValueString("常规", "常规");
            this.ultraExplorerBar1.Groups[2].Text = Utility.GetValueString("更多", "更多");

            ToolTip tips = new ToolTip();
            tips.SetToolTip(this.label4, "Export:ETD,Import:ETA,Other:PostDate,CTM:PostDate ");

            tips.SetToolTip(this.chbFCL, Utility.GetValueString("整箱业务", "整箱业务"));
            tips.SetToolTip(this.chbLCL, Utility.GetValueString("拼箱业务", "拼箱业务"));
            tips.SetToolTip(this.chbBulk, Utility.GetValueString("散货业务", "散货业务"));
            tips.SetToolTip(this.chbAIR, Utility.GetValueString("空运业务", "空运业务"));
            tips.SetToolTip(this.chbOther, Utility.GetValueString("其他业务", "其他业务"));
            tips.SetToolTip(this.chbCTM, Utility.GetValueString("集运业务", "集运业务"));


            //this.cmbSalesType.Items.Clear();
            Utility.SetGoodsSalesTypes(cmbSalesType);

            this.cmbGroupBy.Items = new string[] { Utility.GetValueString("业务员", "业务员") ,
                                                  Utility.GetValueString("客户", "客户") ,
                                                  Utility.GetValueString("承运人", "承运人") ,
                                                  Utility.GetValueString("代理人", "代理人"),
                                                  Utility.GetValueString("航线", "航线"),
                                                  Utility.GetValueString("船公司", "船公司") ,
                                                  Utility.GetValueString("业务类型", "业务类型"),
                                                  Utility.GetValueString("业务发生地", "业务发生地"),
                                                  Utility.GetValueString("揽货方式", "揽货方式"),
                                                  Utility.GetValueString("装货港", "装货港") ,
                                                  Utility.GetValueString("交货地", "交货地") ,
                                                  Utility.GetValueString("合约号", "合约号")  
                                                        };



            ucDateTime.ConditionType = ConditionDateType.Month;

            this.cmbIsProfit.Items.Clear();
            this.cmbIsProfit.Items.AddRange(new string[] {
                                                 Utility.GetValueString("全部", "全部"),
                                                 Utility.GetValueString("盈利", "盈利"),
                                                 Utility.GetValueString("亏损", "亏损")
                                            });


            this.cmbDateType.SelectedIndex = 0;
            this.cmbDateType.Enabled = false;
            this.cmbIsProfit.SelectedIndex = 0;

        }
       

        void CommerceDetailForBox_Search_Disposed(object sender, EventArgs e)
        {
            this._workItem.Items.Remove(this);
        }

    
         
        #endregion

        #region 事件
        /// <summary>
        /// 查询事件
        /// </summary>
        public event EventHandler SearchResult;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.ValidateData())
            {
                if (this.SearchResult != null)
                {
                    this.SearchResult(sender, e);
                }
            }
        }



        bool ValidateData()
        {
            bool isSucc = true;
            if (this.JobType == string.Empty)
            {
                MessageBox.Show(Utility.GetValueString("请选择业务类型", "请选择业务类型") ,"ICP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.ccmbJobType.Focus();
                isSucc = false;
            }

            if (string.IsNullOrEmpty(this.GroupBy))
            {
                MessageBox.Show(Utility.GetValueString("请选择业务类型", "请选择业务类型"), "ICP", MessageBoxButtons.OK, MessageBoxIcon.Information);                
                isSucc = false;
            }

            return isSucc;
        }
        #endregion



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

        #region 分组条件
        /// <summary>
        /// 分组条件
        /// </summary>
        public string GroupBy
        {
            get { return this.cmbGroupBy.Text; }
        }
        /// <summary>
        /// 分组数
        /// </summary>
        public int GroupCount
        {
            get
            {
                string[] strs = this.cmbGroupBy.Text.Split(',');
                return strs.Length;
            }
        }

        public string Group1
        {
            get
            {
                string[] strs = this.cmbGroupBy.Text.Split(',');
                if (strs.Length == 3)
                {
                    return strs[0];
                }
                return string.Empty;
            }
        }
        public string Group2
        {
            get
            {
                string[] strs = this.cmbGroupBy.Text.Split(',');
                if (strs.Length == 3)
                {
                    return strs[1];
                }
                else if (strs.Length == 2)
                {
                    return strs[0];
                }
                return string.Empty;
            }
        }
        public string Group3
        {
            get
            {
                string[] strs = this.cmbGroupBy.Text.Split(',');
                if (strs.Length == 3)
                {
                    return strs[2];
                }
                else if (strs.Length == 2)
                {
                    return strs[1];
                }
                else if (strs.Length == 1)
                {
                    return strs[0];
                }
                return string.Empty;
            }
        }
        #endregion

        #region 部门
        /// <summary>
        /// 是业务发生地还是业务所属部门
        /// </summary>
        public int StructureType
        {
            get
            {
                return this.ucSelectCompany.StructType;
            }
        }
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

        #region 业务类型位
        /// <summary>
        /// 业务类型位
        /// </summary>
        public string JobTypeBit
        {
            get
            {

                string strBit = Convert.ToInt16(this.chbFCL.Checked).ToString() +
                    Convert.ToInt16(this.chbLCL.Checked).ToString() +
                    Convert.ToInt16(this.chbBulk.Checked).ToString() +
                    Convert.ToInt16(this.chbAIR.Checked).ToString() +
                    Convert.ToInt16(this.chbOther.Checked).ToString() +
                    Convert.ToInt16(this.chbCTM.Checked).ToString();
                return strBit;
            }
        }

        /// <summary>
        /// 业务类型
        /// </summary>
        public string JobType
        {
            get
            {
                string jobTypeStr = string.Empty;
                //"0,1,2,3,4,5,6,7,8,9";
                string jobType = ccmbJobType.Text;
                if (jobType.Contains(Utility.GetValueString("出口业务", "出口业务") )
                   )
                {
                    if (this.chbFCL.Checked)
                        jobTypeStr = jobTypeStr + ",0";
                    if (this.chbLCL.Checked)
                        jobTypeStr = jobTypeStr + ",1";
                    if (this.chbBulk.Checked)
                        jobTypeStr = jobTypeStr + ",2";
                    if (this.chbAIR.Checked)
                        jobTypeStr = jobTypeStr + ",3";
                }
                if (jobType.Contains(Utility.GetValueString("进口业务", "进口业务")))
                {
                    if (this.chbFCL.Checked)
                        jobTypeStr = jobTypeStr + ",5";
                    if (this.chbLCL.Checked)
                        jobTypeStr = jobTypeStr + ",6";
                    if (this.chbBulk.Checked)
                        jobTypeStr = jobTypeStr + ",7";
                    if (this.chbAIR.Checked)
                        jobTypeStr = jobTypeStr + ",8";
                }

                if (jobType.Contains( Utility.GetValueString("其他业务", "其他业务") ))
                {
                    jobTypeStr = jobTypeStr + ",4";
                }


                if (jobType.Contains( Utility.GetValueString("集运业务", "集运业务") ))
                {
                    jobTypeStr = jobTypeStr + ",9";
                }

                if (jobType.Contains(Utility.GetValueString("报关业务", "报关业务")))
                {
                    jobTypeStr = jobTypeStr + ",10";
                }

                if (jobTypeStr.Trim() != string.Empty)
                {
                    return jobTypeStr.Substring(1);
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        #endregion

        #region 业务员
        private string _employeeIds;
        /// <summary>
        /// 业务员
        /// </summary>
        public string EmployeeIDs
        {
            get
            {
                _employeeIds = string.Empty;
                if (this.txtSelectSales.SelectedValue != null && !string.IsNullOrEmpty(this.txtSelectSales.SelectedValue.ToString()))
                {
                    Guid[] customerIds = (Guid[])this.txtSelectSales.SelectedValue;
                    foreach (Guid id in customerIds)
                    {
                        _employeeIds += "'" + id.ToString() + "',";
                    }
                }
                if (_employeeIds.EndsWith(","))
                {
                    _employeeIds = _employeeIds.Substring(0, _employeeIds.Length - 1);
                }
                return _employeeIds;
            }
        }
        /// <summary>
        /// 业务员姓名
        /// </summary>
        public string EmployeeNames
        {
            get { return txtSelectSales.SelectedText; }
        }
        #endregion

        #region 是否盈利
        /// <summary>
        /// 是否盈利０－全部；１－盈利；２－亏损
        /// </summary>
        public int ProfitFlag
        {
            get { return this.cmbIsProfit.SelectedIndex; }
        }
        /// <summary>
        /// 是否盈利文本
        /// </summary>
        public string ProfitFlagName
        {
            get { return this.cmbIsProfit.SelectedText; }
        }
        #endregion

        #region 代理人
        private string _agentids;
        /// <summary>
        /// 代理人ID串
        /// </summary>
        public string Agentids
        {
            get
            {
                _agentids = string.Empty;
                if (this.txtAgent.SelectedValue != null && !string.IsNullOrEmpty(this.txtAgent.SelectedValue.ToString()))
                {
                    Guid[] customerIds = (Guid[])this.txtAgent.SelectedValue;
                    foreach (Guid id in customerIds)
                    {
                        _agentids += "'" + id.ToString() + "',";
                    }
                }
                if (_agentids.EndsWith(","))
                {
                    _agentids = _agentids.Substring(0, _agentids.Length - 1);
                }
                return _agentids;
            }
        }
        #endregion

        #region 时间类型

        /// <summary>
        /// 时间类型
        /// </summary>
        public short DateType
        {
            get
            {
                return (short)this.cmbDateType.SelectedIndex;
            }

        }
        #endregion

        #region 客户
        private string _customerIds;
        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerIds
        {
            get
            {
                _customerIds = string.Empty;
                if (this.txtSelectCustomer.SelectedValue != null && !string.IsNullOrEmpty(this.txtSelectCustomer.SelectedValue.ToString()))
                {
                    Guid[] customerIds = (Guid[])this.txtSelectCustomer.SelectedValue;
                    foreach (Guid id in customerIds)
                    {
                        _customerIds += "'" + id.ToString() + "',";
                    }
                }
                if (_customerIds.EndsWith(","))
                {
                    _customerIds = _customerIds.Substring(0, _customerIds.Length - 1);
                }
                return _customerIds;
            }
        }
        /// <summary>
        /// 客户的文本显示
        /// </summary>
        public string CustomerNames
        {
            get { return txtSelectCustomer.SelectedText; }
        }
        #endregion

        #region 承运人
        private string _shipAgentIds;
        /// <summary>
        /// 承运人
        /// </summary>
        public string ShipAgentIds
        {
            get
            {
                _shipAgentIds = string.Empty;
                if (this.txtCarrier.SelectedValue != null && !string.IsNullOrEmpty(this.txtCarrier.SelectedValue.ToString()))
                {
                    Guid[] customerIds = (Guid[])this.txtCarrier.SelectedValue;
                    foreach (Guid id in customerIds)
                    {
                        _shipAgentIds += "'" + id.ToString() + "',";
                    }
                }
                if (_shipAgentIds.EndsWith(","))
                {
                    _shipAgentIds = _shipAgentIds.Substring(0, _shipAgentIds.Length - 1);
                }
                return _shipAgentIds;
            }
        }
        /// <summary>
        /// 承运人文本显示
        /// </summary>
        public string ShipAgentNames
        {
            get { return txtCarrier.SelectedText; }
        }
        #endregion

        #region 交货地
        private string _destPortIds;
        /// <summary>
        /// 装货港
        /// </summary>
        public string DestPortIds
        {
            get
            {
                _destPortIds = string.Empty;
                if (this.txtDestPort.SelectedValue != null && !string.IsNullOrEmpty(this.txtDestPort.SelectedValue.ToString()))
                {
                    Guid[] customerIds = (Guid[])this.txtDestPort.SelectedValue;
                    foreach (Guid id in customerIds)
                    {
                        _destPortIds += "'" + id.ToString() + "',";
                    }
                }
                if (_destPortIds.EndsWith(","))
                {
                    _destPortIds = _destPortIds.Substring(0, _destPortIds.Length - 1);
                }
                return _destPortIds;
            }
        }
        /// <summary>
        /// 装货港文本显示
        /// </summary>
        public string DestPortNames
        {
            get { return txtDestPort.SelectedText; }
        }
        #endregion

        #region 装货港
        private string _loadPortIds;
        /// <summary>
        /// 装货港
        /// </summary>
        public string LoadPortIds
        {
            get
            {
                _loadPortIds = string.Empty;
                if (this.txtLoadPort.SelectedValue != null && !string.IsNullOrEmpty(this.txtLoadPort.SelectedValue.ToString()))
                {
                    Guid[] customerIds = (Guid[])this.txtLoadPort.SelectedValue;
                    foreach (Guid id in customerIds)
                    {
                        _loadPortIds += "'" + id.ToString() + "',";
                    }
                }
                if (_loadPortIds.EndsWith(","))
                {
                    _loadPortIds = _loadPortIds.Substring(0, _loadPortIds.Length - 1);
                }
                return _loadPortIds;
            }
        }
        /// <summary>
        /// 装货港文本显示
        /// </summary>
        public string LoadPortNames
        {
            get { return txtLoadPort.SelectedText; }
        }
        #endregion

        #region 卸货港
        private string _discPortIds;
        /// <summary>
        /// 卸货港
        /// </summary>
        public string DiscPortIds
        {
            get
            {
                _discPortIds = string.Empty;
                if (this.txtDiscPort.SelectedValue != null && !string.IsNullOrEmpty(this.txtDiscPort.SelectedValue.ToString()))
                {
                    Guid[] customerIds = (Guid[])this.txtDiscPort.SelectedValue;
                    foreach (Guid id in customerIds)
                    {
                        _discPortIds += "'" + id.ToString() + "',";
                    }
                }
                if (_discPortIds.EndsWith(","))
                {
                    _discPortIds = _discPortIds.Substring(0, _discPortIds.Length - 1);
                }
                return _discPortIds;
            }
        }
        /// <summary>
        /// 装货港文本显示
        /// </summary>
        public string DiscPortNames
        {
            get { return txtDiscPort.SelectedText; }
        }
        #endregion

        #region 航线
        private string _shippingLineIds;
        /// <summary>
        /// 航线
        /// </summary>
        public string ShippingLineIds
        {
            get
            {

                if (this.mulShippingLine.Value.Trim() != string.Empty && this.mulShippingLine.Visible == true)
                {
                    _shippingLineIds = "'" + this.mulShippingLine.Value.Trim() + "'";
                    _shippingLineIds = _shippingLineIds.Replace(",", "','");
                    return _shippingLineIds;
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// 航线的显示文本
        /// </summary>
        public string ShippingLineNames
        {
            get
            {
                return mulShippingLine.Text;
            }
        }
        #endregion

        #region 揽货方式
        /// <summary>
        /// 揽货方式：０自揽货；１指定货；２同行货；３全部
        /// </summary>
        public int SalesType
        {
            get { return this.cmbSalesType.SelectedIndex; }
        }
        /// <summary>
        /// 揽货方式的文本显示
        /// </summary>
        public string SalesTypeName
        {
            get { return this.cmbSalesType.SelectedText; }
        }
        #endregion

        #region 船公司
        private string _carrierIds;
        /// <summary>
        /// 船公司
        /// </summary>
        public string CarrierIds
        {
            get
            {
                _carrierIds = string.Empty;
                if (this.txtShipOwner.SelectedValue != null && !string.IsNullOrEmpty(this.txtShipOwner.SelectedValue.ToString()))
                {
                    Guid[] customerIds = (Guid[])this.txtShipOwner.SelectedValue;
                    foreach (Guid id in customerIds)
                    {
                        _carrierIds += "'" + id.ToString() + "',";
                    }
                }
                if (_carrierIds.EndsWith(","))
                {
                    _carrierIds = _carrierIds.Substring(0, _carrierIds.Length - 1);
                }
                return _carrierIds;
            }
        }
        /// <summary>
        /// 船公司的文本显示
        /// </summary>
        public string CarrierNames
        {
            get { return txtShipOwner.SelectedText; }
        }
        #endregion

        #region 公司信息


        IConfigureService _configureService;
        [ServiceDependency]
        public IConfigureService ConfigureService
        {
            get { return _configureService; }
            set { _configureService = value; }
        }


        SystemConfigureData _configureDate;
        /// <summary>
        /// 公司的名称
        /// </summary>
        public  string CompanyCName
        {
            get
            {
                if (_configureDate == null)
                {
                    System.Guid ID = _initializeService.GetUserInfo().GetNormalCompany().Id; if (ID == null || ID == Guid.Empty) { ID = new Guid(this.ucSelectCompany.Value.ToString()); } _configureDate = _configureService.GetSystemConfig(ID);
                }
               
                if (_configureDate != null)
                    return _configureDate.CompanyCName;
                else
                    return this.ucSelectCompany.Text;
            }
        }
        /// <summary>
        /// 公司的英文名称
        /// </summary>
        public string CompanyEname
        {
            get
            {
                if (_configureDate == null)
                {
                    System.Guid ID = _initializeService.GetUserInfo().GetNormalCompany().Id; if (ID == null || ID == Guid.Empty) { ID = new Guid(this.ucSelectCompany.Value.ToString()); } _configureDate = _configureService.GetSystemConfig(ID);
                }
                if (_configureDate != null)
                    return _configureDate.CompanyName;
                else
                    return this.ucSelectCompany.Text;
            }
        }
        /// <summary>
        /// 电话传真
        /// </summary>
        public string TelOrFax
        {
            get
            {
                if (_configureDate == null)
                {
                    System.Guid ID = _initializeService.GetUserInfo().GetNormalCompany().Id; if (ID == null || ID == Guid.Empty) { ID = new Guid(this.ucSelectCompany.Value.ToString()); } _configureDate = _configureService.GetSystemConfig(ID);
                }
                if (_configureDate == null)
                    return string.Empty;
                else
                    return "Tel:" + _configureDate.Tel + ", Fax: " + _configureDate.Fax;
            }
        }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyCAddress
        {
            get
            {
                if (_configureDate == null)
                {
                    System.Guid ID = _initializeService.GetUserInfo().GetNormalCompany().Id; if (ID == null || ID == Guid.Empty) { ID = new Guid(this.ucSelectCompany.Value.ToString()); } _configureDate = _configureService.GetSystemConfig(ID);
                }
                if (_configureDate == null)
                    return string.Empty;
                else
                    return _configureDate.CAddress;
            }
        }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyEAddress
        {
            get
            {
                if (_configureDate == null)
                {
                    System.Guid ID = _initializeService.GetUserInfo().GetNormalCompany().Id; if (ID == null || ID == Guid.Empty) { ID = new Guid(this.ucSelectCompany.Value.ToString()); } _configureDate = _configureService.GetSystemConfig(ID);
                }
                if (_configureDate == null)
                    return string.Empty;
                else
                    return _configureDate.Address;

            }
        }
        #endregion

        #region 附加信息
        /// <summary>
        /// 描述信息
        /// </summary>
        public string JobPlace
        {
            get
            {
                if (this.ucSelectCompany.StructType == 0)
                {
                    return Utility.GetValueString("业务发生地", "业务发生地") +"  : " +this.ucSelectCompany.Text.Trim();
                }
                return Utility.GetValueString("业务所属部门", "业务所属部门") +"  : "+this.ucSelectCompany.Text.Trim();
            }
        }
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
                string condition =Utility.GetValueString("部门", "部门") +"  : "+this.ucSelectCompany.Text + " ";
                return condition;
            }
        }
        #endregion
           
        #region 条件ＸＭＬ
        public string XMLCondition
        {
            get
            {
                //return "<root> <StructType>0</StructType> 
                //<StructNodeId>B13FAC2D-8250-4990-A622-5ECA00D3A030</StructNodeId>  
                //    <ETD_Beginning_Date>2007-01-01</ETD_Beginning_Date>  
                //        <ETD_Ending_Date>2009-01-01</ETD_Ending_Date>  
                //            <SalesType>3</SalesType> 
                //                <SalesSet></SalesSet> 
                //                    <ConsignerSet></ConsignerSet>  
                //                        <ShipAgentSet></ShipAgentSet>  
                //                            <CarrierSet></CarrierSet>
                //                                <ShippingLineSet></ShippingLineSet> 
                //                                    <JobType>0,1,2,3,4,5,6,7,8,9</JobType> 
                //                                        <LoadPortSet></LoadPortSet>  
                //                                            <DiscPortSet></DiscPortSet> 
                //                                                <ProfitType>0</ProfitType>  
                //                                                    <GroupString>业务员</GroupString> 
                //                                                        <AgentSet>船公司</AgentSet>  
                //                                                            <DateType>0</DateType> 
                //                                                                </root>";                



                System.IO.StringWriter str = new System.IO.StringWriter();
                XmlTextWriter writer = new XmlTextWriter(str);
                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();
                writer.WriteComment(" 查询条件XML");

                writer.WriteStartElement("root");

                writer.WriteStartElement("StructType");
                writer.WriteValue(this.StructureType.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("StructNodeId");
                writer.WriteValue(this.StructNodeId.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("ETD_Beginning_Date");
                writer.WriteValue(this.BeginTime.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("ETD_Ending_Date");
                writer.WriteValue(this.EndTime.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("SalesType");
                writer.WriteValue(this.SalesType);
                writer.WriteEndElement();

                writer.WriteStartElement("SalesSet");
                writer.WriteValue(this.EmployeeIDs);
                writer.WriteEndElement();


                writer.WriteStartElement("ConsignerSet");
                writer.WriteValue(this.CustomerIds);
                writer.WriteEndElement();


                writer.WriteStartElement("ShipAgentSet");
                writer.WriteValue(this.ShipAgentIds);
                writer.WriteEndElement();

                writer.WriteStartElement("CarrierSet");
                writer.WriteValue(this.CarrierIds);
                writer.WriteEndElement();

                writer.WriteStartElement("ShippingLineSet");
                writer.WriteValue(this.ShippingLineIds);
                writer.WriteEndElement();

                writer.WriteStartElement("JobType");
                writer.WriteValue(this.JobType);
                writer.WriteEndElement();

                writer.WriteStartElement("LoadPortSet");
                writer.WriteValue(this.LoadPortIds);
                writer.WriteEndElement();

                writer.WriteStartElement("DiscPortSet");
                writer.WriteValue(this.DiscPortIds);
                writer.WriteEndElement();

                writer.WriteStartElement("DestPortSet");
                writer.WriteValue(this.DestPortIds);
                writer.WriteEndElement();

                writer.WriteStartElement("ProfitType");
                writer.WriteValue(this.ProfitFlag);
                writer.WriteEndElement();


                writer.WriteStartElement("GroupString");
                writer.WriteValue(this.GroupBy);
                writer.WriteEndElement();

                writer.WriteStartElement("AgentSet");
                writer.WriteValue(this.Agentids);
                writer.WriteEndElement();

                writer.WriteStartElement("DateType");
                writer.WriteValue(this.DateType);
                writer.WriteEndElement();

                writer.WriteStartElement("SCNO");
                writer.WriteValue(this.txSCNO.Text.Trim());
                writer.WriteEndElement();

                writer.WriteStartElement("IsChangeData");
                writer.WriteValue(this.cmbChangeData.SelectedIndex.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("IsEnglish");
                writer.WriteValue(Utility.IsEnglish ? "1" : "0");
                writer.WriteEndElement();

                writer.WriteStartElement("CommodityForMBL");
                writer.WriteValue(this.tbCommodity.Text.Trim());
                writer.WriteEndElement();


                

                writer.WriteEndElement();

                writer.WriteEndDocument();

                writer.Close();

                return str.ToString();
            }
        }


        #endregion

        #region 搜索器


        private void txtCarrier_DoSearching(object sender, SearchEventArgs e)
        {
            finderType = RepConst.CarrierCustomerKey;
            Guid[] guids = new Guid[] { };
            if (e.SelectedValue != null)
            {
                guids = (Guid[])e.SelectedValue;
            }
            if (this.labCarrier.Text == Utility.GetValueString("航空公司", "航空公司"))
            {
                this.DoSearching(Utility.IsEnglish ? "EName" : "CName", guids, RepConst.FINDAirCommpanyCustomer);
            }
            else
            {
                this.DoSearching(Utility.IsEnglish ? "EName" : "CName", guids, RepConst.CarrierCustomerKey);
            }
        }

        private void txtShipOwner_DoSearching(object sender, SearchEventArgs e)
        {
            finderType = RepConst.FindShipOwnerKey;
            Guid[] guids = new Guid[] { };
            if (e.SelectedValue != null)
            {
                guids = (Guid[])e.SelectedValue;
            }
            this.DoSearching(Utility.IsEnglish ? "EName" : "CName", guids, RepConst.FindShipOwnerKey);
        }

        private void txtLoadPort_DoSearching(object sender, SearchEventArgs e)
        {
            finderType = "LoadPort";
            Guid[] guids = new Guid[] { };
            if (e.SelectedValue != null)
            {
                guids = (Guid[])e.SelectedValue;
            }
            this.DoSearching("EName", guids, RepConst.FindPortKey);
        }

        private void txtDestPort_DoSearching(object sender, SearchEventArgs e)
        {
            finderType = "DestPort";
            Guid[] guids = new Guid[] { };
            if (e.SelectedValue != null)
            {
                guids = (Guid[])e.SelectedValue;
            }
            this.DoSearching("EName", guids, RepConst.FindPortKey);
        }

        private void txtDiscPort_DoSearching(object sender, SearchEventArgs e)
        {
            finderType = "DiscPort";
            Guid[] guids = new Guid[] { };
            if (e.SelectedValue != null)
            {
                guids = (Guid[])e.SelectedValue;
            }
            this.DoSearching("EName", guids, RepConst.FindPortKey);
        }

        private void txtSelectSales_DoSearching(object sender, SearchEventArgs e)
        {
            finderType = RepConst.FindUserKey;
            Guid[] guids = new Guid[] { };
            if (e.SelectedValue != null)
            {
                guids = (Guid[])e.SelectedValue;
            }
            this.DoSearching("Name", guids, RepConst.FindUserKey);

        }

        bool isAgent = false;
        private void txtAgent_DoSearching(object sender, SearchEventArgs e)
        {
            finderType = RepConst.FindCustomerKey;
            Guid[] guids = new Guid[] { };
            if (e.SelectedValue != null)
            {
                guids = (Guid[])e.SelectedValue;
            }
            isAgent = true;
            this.DoSearching(Utility.IsEnglish ? "EName" : "CName", guids, RepConst.FindCustomerKey);

        }

        private void txtSelectCustomer_DoSearching(object sender, SearchEventArgs e)
        {
            finderType = RepConst.FindCustomerKey;
            Guid[] guids = new Guid[] { };
            if (e.SelectedValue != null)
            {
                guids = (Guid[])e.SelectedValue;
            }
            this.DoSearching(Utility.IsEnglish ? "EName" : "CName", guids, RepConst.FindCustomerKey);

        }


        void DoSearching(string searchField, Guid[] exitsValues, string finderName)
        {
            this.dataFinder = this._finderFactory.GetDataFinder(finderName);

            dataFinder.DataChoosed += delegate(object sender, DataFindEventArgs arg)
            {
                this.BindSelectData(arg.Data);
                this.dataFinder.Unwrap.FindForm().Close();
            };

            string[] returnFields = new string[] { };
            if (this.finderType == RepConst.FindCustomerKey
                || this.finderType == RepConst.CarrierCustomerKey
                || this.finderType == RepConst.FindShipOwnerKey) //客户
            {
                returnFields = new string[] { "Id", "CName", "EName" };
                finderName = Utility.IsEnglish == false ? "客户查找" : "Customer Search";
            }
            else if (this.finderType == RepConst.FindUserKey) //用户
            {
                returnFields = new string[] { "Id", "Name" };
                finderName = Utility.IsEnglish == false ? "用户查找" : "User Search";
            }

            else if (this.finderType == "LoadPort"
                || this.finderType == "DiscPort"
                || this.finderType == "DestPort")//港口
            {
                returnFields = new string[] { "Id", "EName" };
                finderName = Utility.IsEnglish == false ? "港口查找" : "Port Search";
            }

            Control ctl = this.dataFinder.PickMany(returnFields, exitsValues);
            this._workItem.Workspaces[RepConst.mainWorkspace].Show(ctl, new SmartPartInfo(finderName, finderName));
        }

        void BindSelectData(object[] result)
        {
            if (result != null)
            {
                Guid[] Ids = new Guid[result.Length];
                string text = string.Empty;

                for (int i = 0; i < result.Length; i++)
                {
                    if (this.finderType == RepConst.FindCustomerKey
                        || this.finderType == RepConst.CarrierCustomerKey
                        || this.finderType == RepConst.FindShipOwnerKey)
                    {
                        if (Utility.IsEnglish)
                        {
                            text += (result[i] as object[])[1].ToString() + ",";
                        }
                        else { text += (result[i] as object[])[2].ToString() + ","; }

                    }
                    else
                    {
                        text += (result[i] as object[])[1].ToString() + ",";
                    }
                    Ids[i] = (Guid)((result[i] as object[])[0]);
                }


                if (finderType == RepConst.FindUserKey)
                {
                    this.txtSelectSales.SelectedText = text;
                    this.txtSelectSales.SelectedValue = Ids;
                }
                else if (finderType == RepConst.FindCustomerKey)
                {
                    if (isAgent == true)
                    {
                        this.txtAgent.SelectedText = text;
                        this.txtAgent.SelectedValue = Ids;
                        isAgent = false;
                    }
                    else
                    {
                        this.txtSelectCustomer.SelectedText = text;
                        this.txtSelectCustomer.SelectedValue = Ids;
                    }
                }
                else if (finderType == RepConst.CarrierCustomerKey)
                {
                    this.txtCarrier.SelectedText = text;
                    this.txtCarrier.SelectedValue = Ids;
                }
                else if (finderType == RepConst.FindShipOwnerKey)
                {
                    this.txtShipOwner.SelectedText = text;
                    this.txtShipOwner.SelectedValue = Ids;
                }
                else if (finderType == "LoadPort")
                {
                    this.txtLoadPort.SelectedText = text;
                    this.txtLoadPort.SelectedValue = Ids;
                }
                else if (finderType == "DiscPort")
                {
                    this.txtDiscPort.SelectedText = text;
                    this.txtDiscPort.SelectedValue = Ids;
                }
                else if (finderType == "DestPort")
                {
                    this.txtDestPort.SelectedText = text;
                    this.txtDestPort.SelectedValue = Ids;
                }

            }
        }

        #endregion


        #region 根据报表类型控制条件的显示

        void ccmbJobType_TextChanaged(object sender, EventArgs e)
        {
            string jobType = ccmbJobType.Text;
            this.chbFCL.Enabled = false;
            this.chbLCL.Enabled = false;
            this.chbBulk.Enabled = false;
            this.chbAIR.Enabled = false;

            this.chbFCL.Checked = false;
            this.chbLCL.Checked = false;
            this.chbBulk.Checked = false;
            this.chbAIR.Checked = false;

            this.chbOther.Enabled = false;
            this.chbOther.Checked = false;

            this.chbCTM.Enabled = false;
            this.chbCTM.Checked = false;


            if (jobType.Contains( Utility.GetValueString("出口业务", "出口业务") )
                || jobType.Contains( Utility.GetValueString("进口业务", "进口业务") ))
            {
                this.chbFCL.Enabled = true;
                this.chbLCL.Enabled = true;
                this.chbBulk.Enabled = true;
                this.chbAIR.Enabled = true;

                this.chbFCL.Checked = true;
                this.chbLCL.Checked = true;
                this.chbBulk.Checked = true;
                this.chbAIR.Checked = true;

                //this.cmbGroupBy.Items = new string[] {
                //                                "业务员",
                //                                "客户",
                //                                "承运人",
                //                                "航线",
                //                                "船公司",                                                  
                //                                "业务发生地",
                //                                "揽货方式",
                //                                "装货港"};
            }

            if (jobType.Contains( Utility.GetValueString("其他业务", "其他业务") ))
            {
                this.chbOther.Enabled = true;
                this.chbOther.Checked = true;
            }

            if (jobType.Contains( Utility.GetValueString("集运业务", "集运业务") ))
            {
                this.chbCTM.Enabled = true;
                this.chbCTM.Checked = true;
            }
        }
        /// <summary>
        /// 根据报表类型控制条件的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ResetControl()
        {
            //控件的现实以及分组条件
            this.labAgent.Visible = true;
            this.txtAgent.Visible = true;
            this.labDiscPort.Visible = true;
            this.txtDiscPort.Visible = true;
            this.labLoadPort.Visible = true;
            this.txtLoadPort.Visible = true;
            this.labShippingLine.Visible = true;
            this.mulShippingLine.Visible = true;
            this.labCarrier.Visible = true;
            this.txtShipOwner.Visible = true;
            this.labShipAgent.Visible = true;
            this.txtCarrier.Visible = true;
            this.ultraExplorerBar1.Groups[2].Visible = true;

            labShipAgent.Text = Utility.GetValueString("承运人", "承运人");
            this.labCarrier.Text = Utility.GetValueString("船公司", "船公司");
            this.cmbGroupBy.MaxChecked = 3;
            string[] strGroupbyOT = new string[] {Utility.GetValueString("业务员", "业务员") ,
                                                  Utility.GetValueString("客户", "客户") ,
                                                  Utility.GetValueString("承运人", "承运人") ,
                                                  Utility.GetValueString("代理人", "代理人"),
                                                  Utility.GetValueString("航线", "航线"),
                                                  Utility.GetValueString("船公司", "船公司") ,
                                                  Utility.GetValueString("业务发生地", "业务发生地"),
                                                  Utility.GetValueString("揽货方式", "揽货方式"),
                                                  Utility.GetValueString("装货港", "装货港") ,
                                                  Utility.GetValueString("交货地", "交货地") ,
                                                  Utility.GetValueString("合约号", "合约号")   };

            string[] strGroupbyAT = new string[] { Utility.GetValueString("业务员", "业务员") ,
                                                   Utility.GetValueString("客户", "客户") ,
                                                  Utility.GetValueString("承运人", "承运人") ,
                                                  Utility.GetValueString("代理人", "代理人"),
                                                  Utility.GetValueString("航线", "航线"),
                                                  Utility.GetValueString("船公司", "船公司") ,
                                                  Utility.GetValueString("业务发生地", "业务发生地"),
                                                  Utility.GetValueString("装货港", "装货港") ,
                                                  Utility.GetValueString("交货地", "交货地") ,
                                                  Utility.GetValueString("合约号", "合约号")
                                                    };

            string[] strGroupbyOther = new string[] {
                                                    Utility.GetValueString("业务员", "业务员") ,
                                                    Utility.GetValueString("航线", "航线"),
                                                    Utility.GetValueString("客户", "客户") ,                                                
                                                  Utility.GetValueString("业务发生地", "业务发生地")
                                                    };
            if (this.chbCTM.Checked ||
                this.chbOther.Checked)
            {
                this.cmbGroupBy.Items = strGroupbyOther;

                this.chbOther.Enabled = true;
                this.chbCTM.Enabled = true;
                if (JobTypeBit == "000010")
                {
                    this.ultraExplorerBar1.Groups[2].Visible = false;
                    this.chbOther.Enabled = false;
                }
                else if (JobTypeBit == "000001")
                {
                    this.labAgent.Visible = false;
                    this.txtAgent.Visible = false;
                    this.txtAgent.SelectedValue = null;
                    this.labDiscPort.Visible = false;
                    this.txtDiscPort.Visible = false;
                    this.txtDiscPort.SelectedValue = null;

                    this.labLoadPort.Visible = false;
                    this.txtLoadPort.Visible = false;
                    this.txtLoadPort.SelectedValue = null;

                    this.labShippingLine.Visible = false;
                    this.mulShippingLine.Visible = false;
                    //this.mulShippingLine.Value = string.Empty;


                    this.labCarrier.Visible = false;
                    this.txtCarrier.Visible = false;
                    this.txtCarrier.SelectedValue = null;

                    this.txtShipOwner.Visible = false;
                    this.labShipAgent.Visible = true;
                    this.txtShipOwner.SelectedValue = null;

                    this.txtCarrier.Visible = true;
                    labShipAgent.Text = Utility.GetValueString("司机", "司机") ;
                    this.chbCTM.Enabled = false;
                }
                else
                {
                    this.ultraExplorerBar1.Groups[2].Visible = false;
                }
            }
            else if (this.chbAIR.Checked)
            {

                this.cmbGroupBy.Items = strGroupbyAT;
                if (JobTypeBit == "000100")
                {
                    this.labCarrier.Text = Utility.GetValueString("航空公司", "航空公司");
                }
            }
            else if (this.chbFCL.Checked ||
                this.chbLCL.Checked ||
                this.chbBulk.Checked)
            {
                this.cmbGroupBy.Items = strGroupbyOT;
            }
            this.cmbGroupBy.MaxChecked = 3;
            this.cmbGroupBy.Text =Utility.GetValueString("业务员", "业务员")+","+Utility.GetValueString("客户", "客户");
        }
        #endregion

        private void chbFCL_CheckedChanged(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void cmbGroupBy_TextChanaged(object sender, EventArgs e)
        {
            //if (this.cmbGroupBy.Text.Trim() == string.Empty)
            //{
            //    this.cmbGroupBy.Text = Utility.GetValueString("业务员", "业务员")+","+Utility.GetValueString("客户", "客户") ;
            //}
        }



      

    }
}
