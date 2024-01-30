using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Agilelabs.Framework.Service;
using LongWin.DataWarehouseReport.ServiceInterface;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using LongWin.Framework.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using LongWin.CommonData.ServiceInterface;
using LongWin.CommonData.ServiceInterface.DataObjects;
using System.Xml;

namespace LongWin.DataWarehouseReport.WinUI
{
    /// <summary>
    ///  业务数据核对表 
    /// </summary>
    public partial class GetJobInfoForCargoTracking_Search : UserControl
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

        IInitializeService _initializeService;
        [ServiceDependency]
        public IInitializeService InitializeService
        {
            get { return _initializeService; }
            set { _initializeService = value; }
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

        public GetJobInfoForCargoTracking_Search()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(GetJobInfoForCargoTracking_Search_Disposed);
        }

        void GetJobInfoForCargoTracking_Search_Disposed(object sender, EventArgs e)
        {
            this._workItem.Items.Remove(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                if (_repBaseDataService == null || _transportFoundationService == null)
                {
                    //MessageBox.Show("服务为空,请检查");
                    _repBaseDataService = REPModuleInit.RepService;
                    _transportFoundationService = REPModuleInit.FoundationService;
                    //return;
                }
                this.ucSelectCompany.DataSource = this._repBaseDataService.GetStructureNodesByCurrentUser();

                this.labCarrier.Text = Utility.GetValueString("船公司", "船公司");

                this.mulShippingLine.DataSource = Utility.GetShppingLine(_transportFoundationService.GetShippingLineList(null, null, null, 0));

                this.cmbDateType.Items.Clear();
                this.cmbDateType.Items.AddRange(new object[] {
                                                        Utility.GetValueString("业务时间", "业务时间")});

                this.ultraExplorerBar1.Groups[0].Text = Utility.GetValueString("日期", "日期");
                this.ultraExplorerBar1.Groups[1].Text = Utility.GetValueString("常规", "常规");

                ToolTip tips = new ToolTip();
                tips.SetToolTip(this.label4, "Export:ETD,Import:ETA,Other:PostDate,CTM:PostDate ");


                ucDateTime.ConditionType = ConditionDateType.Month;


                this.cmbDateType.SelectedIndex = 0;
                this.cmbDateType.Enabled = false;
               
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
             if (this.SearchResult != null)
                {
                    this.SearchResult(sender, e);
                }
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

                writer.WriteStartElement("SalesSet");
                writer.WriteValue(this.EmployeeIDs);
                writer.WriteEndElement();

                writer.WriteStartElement("CarrierSet");
                writer.WriteValue(this.CarrierIds);
                writer.WriteEndElement();

                writer.WriteStartElement("ShippingLineSet");
                writer.WriteValue(this.ShippingLineIds);
                writer.WriteEndElement();

                writer.WriteStartElement("LoadPortSet");
                writer.WriteValue(this.LoadPortIds);
                writer.WriteEndElement();

                writer.WriteStartElement("DiscPortSet");
                writer.WriteValue(this.DiscPortIds);
                writer.WriteEndElement();

                writer.WriteStartElement("DateType");
                writer.WriteValue(this.DateType);
                writer.WriteEndElement();

                writer.WriteStartElement("SCNO");
                writer.WriteValue(this.txSCNO.Text.Trim());
                writer.WriteEndElement();

                writer.WriteStartElement("SoDateToETD");
                writer.WriteValue(this.nuSoDateToETD.Value.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("ETDToETA");
                writer.WriteValue(this.nuETDToETA.Value.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("LoadingToETD");
                writer.WriteValue(this.nuLoadingToETD.Value.ToString());
                writer.WriteEndElement();


                writer.WriteStartElement("IsEnglish");
                writer.WriteValue(Utility.IsEnglish ? "1" : "0");
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
            if (this.labCarrier.Text == "航空公司")
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

        private void txtAgent_DoSearching(object sender, SearchEventArgs e)
        {
            finderType = RepConst.FindCustomerKey;
            Guid[] guids = new Guid[] { };
            if (e.SelectedValue != null)
            {
                guids = (Guid[])e.SelectedValue;
            }
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
                || this.finderType == "DiscPort")//港口
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

            }
        }

        #endregion


        #region 根据报表类型控制条件的显示

        /// <summary>
        /// 根据报表类型控制条件的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ResetControl()
        {
            //控件的现实以及分组条件
            this.labDiscPort.Visible = true;
            this.txtDiscPort.Visible = true;
            this.labLoadPort.Visible = true;
            this.txtLoadPort.Visible = true;
            this.labShippingLine.Visible = true;
            this.mulShippingLine.Visible = true;
            this.labCarrier.Visible = true;
            this.txtShipOwner.Visible = true;
            this.ultraExplorerBar1.Groups[2].Visible = true;

            this.labCarrier.Text = "船公司";
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

    }
}
