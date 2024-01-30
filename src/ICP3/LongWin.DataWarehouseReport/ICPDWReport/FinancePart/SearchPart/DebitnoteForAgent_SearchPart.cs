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
using Agilelabs.Framework.Service;
using LongWin.CommonData.ServiceInterface;
using LongWin.CommonData.ServiceInterface.DataObjects;

namespace LongWin.DataWarehouseReport.WinUI
{
    /// <summary>
    /// 代理信息表查询面板
    /// </summary>
    public partial class DebitnoteForAgent_SearchPart : UserControl
    {
        #region 服务注入

        IREPBaseDataService _repBaseDataService;
        [ServiceDependency]
        public IREPBaseDataService RepBaseDataService
        {
            set { this._repBaseDataService = value; }
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
            set { _initializeService = value; }
        }
        #endregion

        #region 初始化

        public DebitnoteForAgent_SearchPart()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(DebitnoteForAgent_SearchPart_Disposed);
            this.label6.Visible = false;
            this.cmbShowAttach.Visible = false;
        }

        void DebitnoteForAgent_SearchPart_Disposed(object sender, EventArgs e)
        {
            this._workItem.Items.Remove(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                if (_repBaseDataService == null)
                {
                    //MessageBox.Show("服务为空,请检查");
                    _repBaseDataService = REPModuleInit.RepService;
                    //return;
                }
                this.ultraExplorerBar1.Groups[0].Text = Utility.GetValueString("日期", "日期");
                this.ultraExplorerBar1.Groups[1].Text = Utility.GetValueString("常规", "常规");
                this.ultraExplorerBar1.Groups[2].Text = Utility.GetValueString("更多", "更多");

                this.cmbRecoupFlag.Items.Clear();
                this.cmbRecoupFlag.Items.AddRange(new string[]{
                                                  Utility.GetValueString("是", "是"),
                                                  Utility.GetValueString("否", "否"),
                                                  Utility.GetValueString("全部", "全部")
                                                   });
                this.cmbShowAttach.Items.Clear();
                this.cmbShowAttach.Items.AddRange(new string[]{
                                                  Utility.GetValueString("否", "否"),
                                                  Utility.GetValueString("是", "是"),
                                                   });



                this.cmbPrePayType.Items.Clear();
                this.cmbPrePayType.Items.AddRange(new string[]{
                                                  Utility.GetValueString("否", "否"),
                                                  Utility.GetValueString("是", "是"),                                                  
                                                  Utility.GetValueString("全部", "全部")
                                                   });

                
                this.cmbPrePayType.SelectedIndex = 0;

                this.cmbShowAttach.SelectedIndex = 0;
                this.cmbCurrencyType.SelectedIndex = 0;
                this.cmbRecoupFlag.SelectedIndex = 2;
                this.cmbCurrencyType.Enabled = false;
                this.ucSelectCompany.DataSource = this._repBaseDataService.GetStructureNodesForAgentByCurrentUser();

                if (!this._repBaseDataService.GetUserIsManange())
                {
                    this.txtSales.Enabled = false;
                    this.txtSales.SelectedText = _initializeService.GetUserInfo().DispalyName;
                    txtSales.SelectedValue = new Guid[] { new Guid(_initializeService.GetUserInfo().UserId) };
                }
            }
        }

        #endregion

        #region 事件
        /// <summary>
        /// 查询事件
        /// </summary>
        public event EventHandler SearchResult;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //this._titleInfo = null;
            if (this.SearchResult != null)
            {
                this.SearchResult(sender, e);
            }
        }

        private void rabTotal_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rabTotal.Checked)
            {
                //this.ucSelectSales.Enabled = false;
                //this.ucSelectBusinessType.Enabled = false;
                this.cmbCurrencyType.Enabled = false;
                this.cmbCurrencyType.SelectedIndex = 0;

                this.label6.Visible = false;
                this.cmbShowAttach.Visible = false;
            }
      
        }

        private void rabDetail_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rabDetail.Checked)
            {
                
                    //this.ucSelectSales.Enabled = true;
                    //this.ucSelectBusinessType.Enabled = true;
                    this.cmbCurrencyType.Enabled = true;

                this.label6.Visible = true;
                this.cmbShowAttach.Visible = true;
               
            }
        }
        #endregion

        #region 属性

        #region 是详细表还是统计表
        /// <summary>
        /// 是详细表还是统计表
        /// </summary>
        public bool IsTotal
        {
            get
            {
                if (this.rabTotal.Checked)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        #region 时间
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
                if (this.txtSales.SelectedValue != null && !string.IsNullOrEmpty(this.txtSales.SelectedValue.ToString()))
                {
                    Guid[] customerIds = (Guid[])this.txtSales.SelectedValue;
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
            get { return txtSales.SelectedText; }
        }
        #endregion

        #region 业务类型
        private string _businessTypes;
        /// <summary>
        /// 业务类型的值
        /// </summary>
        public string BusinessTypes
        {
            get
            {
                if (Utility.IsEnglish)
                {
                    if (this.mulBusiness.Value.Trim() != string.Empty)
                    {
                        this._businessTypes = "'" + this.mulBusiness.Value.Trim() + "'";
                        _businessTypes = _businessTypes.Replace(",", "','");
                        return _businessTypes;
                    }
                }
                else
                {

                    if (this.mulBusiness.Text.Trim() != string.Empty)
                    {
                        this._businessTypes = "'" + this.mulBusiness.Text.Trim() + "'";
                        _businessTypes = _businessTypes.Replace(",", "','");
                        return _businessTypes;
                    }
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// 业务类型的文本
        /// </summary>
        public string BusinessTypeNames
        {
            get { return mulBusiness.Text; }
        }
        #endregion

        #region 往来单位
        private string _customerIds;
        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerIds
        {
            get
            {

                _customerIds = string.Empty;
                if (this.txtShipTo.SelectedValue != null && !string.IsNullOrEmpty(this.txtShipTo.SelectedValue.ToString()))
                {
                    Guid[] customerIds = (Guid[])this.txtShipTo.SelectedValue;
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
            get { return txtShipTo.SelectedText; }
        }
        #endregion

        #region 预付状态
        /// <summary>
        /// 预付状态０－不是预支付；１－预付；２－全部
        /// </summary>
        public int PrePayFlag
        {
            get { return this.cmbPrePayType.SelectedIndex; }
        }

        #endregion

        #region 核销
        /// <summary>
        /// 核销标志０－已核销；１－未核销；２－全部
        /// </summary>
        public int RecoupFlag
        {
            get { return this.cmbRecoupFlag.SelectedIndex; }
        }
        /// <summary>
        /// 核销标志文本
        /// </summary>
        public string RecoupFlagName
        {
            get { return this.cmbRecoupFlag.SelectedText; }
        }
        #endregion

        #region 币种
        /// <summary>
        /// 币种０原始币种；１折合成美金
        /// </summary>
        public int CurrrencyType
        {
            get { return this.cmbCurrencyType.SelectedIndex; }
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
        public string CompanyCName
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

        #region 描述信息
        /// <summary>
        /// 描述信息
        /// </summary>
        public string JobPlace
        {
            get {
                if (this.ucSelectCompany.StructType==0)
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
                string condition =Utility.GetValueString("部门", "部门") +"  : "+this.ucSelectCompany.Text;
                return condition;

            }
        }
        #endregion

        #region 是否显示附件
        public string ShowAttach
        {
            get
            {
                return this.cmbShowAttach.SelectedIndex.ToString();
            }
        }
        #endregion

        #endregion

        #region 搜索器

        private void txtSales_DoSearching(object sender, SearchEventArgs e)
        {
            finderType = RepConst.FindUserKey;
            Guid[] guids = new Guid[] { };
            if (e.SelectedValue != null)
            {
                guids = (Guid[])e.SelectedValue;
            }
            this.DoSearching("Name", guids);
        }

        private void txtShipTo_DoSearching(object sender, SearchEventArgs e)
        {
            finderType = RepConst.FindCustomerKey;
            Guid[] guids = new Guid[] { };
            if (e.SelectedValue != null)
            {
                guids = (Guid[])e.SelectedValue;
            }
            this.DoSearching(Utility.IsEnglish ? "EName" : "CName", guids);
        }

        void DoSearching(string searchField, Guid[] exitsValues)
        {
            this.dataFinder = this._finderFactory.GetDataFinder(finderType);

            dataFinder.DataChoosed += delegate(object sender, DataFindEventArgs arg)
            {
                this.BindSelectData(arg.Data);
                this.dataFinder.Unwrap.FindForm().Close();
            };
            if (this.finderType == RepConst.FindCustomerKey)
            {
                string[] returnFields = new string[] { "Id", "CName", "EName" };
                Control ctl = this.dataFinder.PickMany(returnFields,exitsValues);                 
                string info = Utility.IsEnglish == false ? "客户查找" : "Customer Search";
                this._workItem.Workspaces[RepConst.mainWorkspace].Show(ctl, new SmartPartInfo(info,info));
            }
            else
            {
                string[] returnFields = new string[] { "Id", "Name" };
                Control ctl = this.dataFinder.PickMany(returnFields, exitsValues);
                string info = Utility.IsEnglish == false ? "用户查找" : "User Search";
                this._workItem.Workspaces[RepConst.mainWorkspace].Show(ctl, new SmartPartInfo(info,info));
            }
        }

        void BindSelectData(object[] result)
        {
            if (result != null)
            {
                //客户搜索
                if (this.finderType == RepConst.FindCustomerKey)
                {
                    string customerNames = string.Empty;
                    Guid[] customerIds = new Guid[result.Length];
                    for (int i = 0; i < result.Length; i++)
                    {
                        if (Utility.IsEnglish)
                        {
                            customerNames += (result[i] as object[])[1].ToString() + ",";
                        }
                        else { customerNames += (result[i] as object[])[2].ToString() + ","; }

                        customerIds[i] = (Guid)((result[i] as object[])[0]);
                    }

                    this.txtShipTo.SelectedText = customerNames;
                    this.txtShipTo.SelectedValue = customerIds;
                }
                //搜索业务员
                else if (this.finderType == RepConst.FindUserKey)
                {
                    string userText = string.Empty;
                    Guid[] userIds = new Guid[result.Length];
                    for (int i = 0; i < result.Length; i++)
                    {
                        userText += (result[i] as object[])[1].ToString() + ",";

                        userIds[i] = (Guid)((result[i] as object[])[0]);
                    }

                    this.txtSales.SelectedText = userText;
                    this.txtSales.SelectedValue = userIds;
                }
            }
        }

        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
    }
}
