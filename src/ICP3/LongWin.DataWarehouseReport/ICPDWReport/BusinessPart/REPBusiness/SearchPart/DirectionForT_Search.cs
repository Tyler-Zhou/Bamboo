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
using System.Xml;
namespace LongWin.DataWarehouseReport.WinUI
{
    /// <summary>
    /// 箱量趋势
    /// </summary>
    public partial class DirectionForT_Search : UserControl
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

        public DirectionForT_Search()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(DirectionForT_Search_Disposed);
        }

        void DirectionForT_Search_Disposed(object sender, EventArgs e)
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
                this.ucSelectCompany.DataSource = _repBaseDataService.GetStructureNodesByCurrentUser();
                this.mulShippingLine.DataSource = Utility.GetShppingLine(_transportFoundationService.GetShippingLineList(null, null, null, 0));

                this.cmbViewMode.Items.Clear();
                this.cmbViewMode.Items.AddRange(new object[] {
                                                         Utility.GetValueString("年", "年"),
                                                         Utility.GetValueString("月", "月")});


                this.ccmbJobType.Items = new string[] {
                                                        Utility.GetValueString("出口业务", "出口业务") ,
                                                        Utility.GetValueString("进口业务", "进口业务") , 
                                                        Utility.GetValueString("集运业务", "集运业务") , 
                                                        };
                ccmbJobType.TextChanaged += new EventHandler(ccmbJobType_TextChanaged);
                ccmbJobType.Text = Utility.GetValueString("出口业务", "出口业务");

                this.cmbDateType.Items.Clear();
                this.cmbDateType.Items.AddRange(new object[] {
                                                        Utility.GetValueString("业务时间", "业务时间")
                                                       });


                //this.ultraExplorerBar1
                this.ultraExplorerBar1.Groups[0].Text = Utility.GetValueString("日期", "日期");
                this.ultraExplorerBar1.Groups[1].Text = Utility.GetValueString("常规", "常规");


                ToolTip tips = new ToolTip();
                tips.SetToolTip(this.label4, "Export:ETD,Import:ETA,Other:PostDate,CTM:PostDate ");
                tips.SetToolTip(this.chbFCL, Utility.GetValueString("整箱业务", "整箱业务"));
                tips.SetToolTip(this.chbLCL, Utility.GetValueString("拼箱业务", "拼箱业务"));
                tips.SetToolTip(this.chbCTM, Utility.GetValueString("集运业务", "集运业务"));

                //this.cmbSalesType.Items.Clear();
                Utility.SetGoodsSalesTypes(cmbSalesType);

                this.cmbGroupByField.Items.Clear();
                this.cmbGroupByField.Items.AddRange(new string[] {
                                                        Utility.GetValueString("业务类型", "业务类型"),
                                                        Utility.GetValueString("揽货方式", "揽货方式"),
                                                        Utility.GetValueString("船公司", "船公司"),
                                                        Utility.GetValueString("代理人", "代理人"),
                                                        Utility.GetValueString("航线", "航线"),
                                                        Utility.GetValueString("业务发生地", "业务发生地"),
                                                        Utility.GetValueString("业务员", "业务员"),
                                                        });

                this.cmbDateType.SelectedIndex = 0;
                ucDateTime.ConditionType = ConditionDateType.Year;
                cmbViewMode.SelectedIndex = 0;
                cmbGroupByField.SelectedIndex = 0;
                //this.cmbSalesType.SelectedIndex = 4;
                  
            }
        }

        #endregion

        #region 查询事件
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
            return isSucc;
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

        #region 组织结构
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


        /// <summary>
        /// 描述信息
        /// </summary>
        public string JobPlace
        {
            get
            {
                if (this.ucSelectCompany.StructType==0)
                {
                    return Utility.GetValueString("业务发生地", "业务发生地") +"  : " +this.ucSelectCompany.Text.Trim();
                }
                return Utility.GetValueString("业务所属部门", "业务所属部门") +"  : "+this.ucSelectCompany.Text.Trim();
            }
        }
        #endregion

        #region 客户

        

        /// <summary>
        /// 代理
        /// </summary>
        public string Agentids
        {
            get
            {
                string agentIds = string.Empty;
                if (this.txtAgent.SelectedValue != null && !string.IsNullOrEmpty(this.txtAgent.SelectedValue.ToString()))
                {
                    Guid[] customerIds = (Guid[])this.txtAgent.SelectedValue;
                    foreach (Guid id in customerIds)
                    {
                        agentIds += "'" + id.ToString() + "',";
                    }
                }
                if (agentIds.EndsWith(","))
                {
                    agentIds = agentIds.Substring(0, agentIds.Length - 1);
                }
                return agentIds;
            }
        }

        private string _customerIds;
        /// <summary>
        /// 客户
        /// </summary>
        public string CarrierIds
        {
            get
            {
                _customerIds = string.Empty;
                if (this.txtShipOwner.SelectedValue != null && !string.IsNullOrEmpty(this.txtShipOwner.SelectedValue.ToString()))
                {
                    Guid[] customerIds = (Guid[])this.txtShipOwner.SelectedValue;
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
            get { return txtShipOwner.SelectedText; }
        }
        #endregion

        #region 业务类型
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
                if (jobType.Contains( Utility.GetValueString("出口业务", "出口业务") ))
                {
                    if (this.chbFCL.Checked)
                        jobTypeStr = jobTypeStr + ",0";
                    if (this.chbLCL.Checked)
                        jobTypeStr = jobTypeStr + ",1";                   
                }

                if (jobType.Contains(Utility.GetValueString("进口业务", "进口业务")))
                {
                    if (this.chbFCL.Checked)
                        jobTypeStr = jobTypeStr + ",5";
                    if (this.chbLCL.Checked)
                        jobTypeStr = jobTypeStr + ",6";
                 
                }

                if (jobType.Contains( Utility.GetValueString("其他业务", "其他业务") ))
                {
                    jobTypeStr = jobTypeStr + ",4";
                }

                if (jobType.Contains( Utility.GetValueString("集运业务", "集运业务") ))
                {
                    jobTypeStr = jobTypeStr + ",9";
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


        #region 航线
        private string _shippingLineIds;
        /// <summary>
        /// 航线
        /// </summary>
        public string ShippingLineIds
        {
            get
            {
                if (this.mulShippingLine.Value.Trim() != string.Empty)
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

        /// <summary>
        /// 业务类型位
        /// </summary>
        public string JobTypeBit
        {
            get
            {

                string strBit = Convert.ToInt16(this.chbFCL.Checked).ToString() +
                    Convert.ToInt16(this.chbLCL.Checked).ToString() +
                    "000" +                   
                    Convert.ToInt16(this.chbCTM.Checked).ToString();
                return strBit;
            }
        }

        /// <summary>
        /// 分组字段
        /// </summary>
        public string GroupByField
        {
            get
            {
                return this.cmbGroupByField.SelectedIndex.ToString();
            }
        }

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
        #endregion

        #region 条件ＸＭＬ
        public string XMLCondition
        {
            get
            {
               

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

  
                writer.WriteStartElement("CarrierSet");
                writer.WriteValue(this.CarrierIds);
                writer.WriteEndElement();

                writer.WriteStartElement("ShippingLineSet");
                writer.WriteValue(this.ShippingLineIds);
                writer.WriteEndElement();

                writer.WriteStartElement("JobType");
                writer.WriteValue(this.JobType);
                writer.WriteEndElement();


                writer.WriteStartElement("SCNO");
                writer.WriteValue(this.txSCNO.Text.Trim());
                writer.WriteEndElement();

                writer.WriteStartElement("AgentSet");
                writer.WriteValue(this.Agentids);
                writer.WriteEndElement();

                writer.WriteStartElement("DateType");
                writer.WriteValue(this.DateType);
                writer.WriteEndElement();

                writer.WriteStartElement("GroupByField");
                writer.WriteValue(this.GroupByField);
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
                string condition = Utility.GetValueString("部门", "部门") +"  : "+this.ucSelectCompany.Text;
                return condition;
            }
        }

        #endregion

        

        #endregion

        #region 搜索器

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

        void DoSearching(string searchField, Guid[] exitsValues, string finderName)
        {
            this.dataFinder = this._finderFactory.GetDataFinder(finderName);

            dataFinder.DataChoosed += delegate(object sender, DataFindEventArgs arg)
            {
                this.BindSelectData(arg.Data);
                this.dataFinder.Unwrap.FindForm().Close();
            };

            string[] returnFields = new string[] { };
            if (this.finderType == RepConst.FindShipOwnerKey
                ) //客户
            {
                returnFields = new string[] { "Id", "CName", "EName" };
                finderName = Utility.IsEnglish ? "ShipOwner Search" : "船公司查找";
            }
            else if (this.finderType == RepConst.FindUserKey) //用户
            {
                returnFields = new string[] { "Id", "Name" };
                finderName = Utility.IsEnglish == false ? "用户查找" : "User Search";
            }

            if (this.finderType == RepConst.FindCustomerKey) //客户
            {
                returnFields = new string[] { "Id", "CName", "EName" };
                finderName = Utility.IsEnglish ? "Agent Search" : "代理查找";
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
                        || this.finderType == RepConst.FindForwardingKey
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

                if (finderType == RepConst.FindShipOwnerKey)
                {
                    this.txtShipOwner.SelectedText = text;
                    this.txtShipOwner.SelectedValue = Ids;
                }

                if (finderType == RepConst.FindCustomerKey)
                {
                    this.txtAgent.SelectedText = text;
                    this.txtAgent.SelectedValue = Ids;
                }
            }
        }

        #endregion

        public string ViewMode
        {
            get            
            {
                return this.cmbViewMode.SelectedIndex.ToString();
            }
        }

        #region 根据报表类型控制条件的显示

        void ccmbJobType_TextChanaged(object sender, EventArgs e)
        {
            string jobType = ccmbJobType.Text;
            this.chbFCL.Enabled = false;
            this.chbLCL.Enabled = false;

            this.chbFCL.Checked = false;
            this.chbLCL.Checked = false;


            this.chbCTM.Enabled = false;
            this.chbCTM.Checked = false;


            if (jobType.Contains( Utility.GetValueString("出口业务", "出口业务") )
                || jobType.Contains( Utility.GetValueString("进口业务", "进口业务") ))
            {
                this.chbFCL.Enabled = true;
                this.chbLCL.Enabled = true;

                this.chbFCL.Checked = true;
                this.chbLCL.Checked = true;

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
            if (jobType.Contains( Utility.GetValueString("集运业务", "集运业务") ))
            {
                this.chbCTM.Enabled = true;
                this.chbCTM.Checked = true;
            }
        }
        #endregion 根据报表类型控制条件的显示

        private void chbFCL_CheckedChanged(object sender, EventArgs e)
        {
            ResetControl();
        }
              /// <summary>
        /// 根据报表类型控制条件的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ResetControl()
        {
            if (this.chbCTM.Checked)
            {
                //this.chbOther.Enabled = true;
                this.chbCTM.Enabled = true;
                if (JobTypeBit.Substring(5, 1) == "1")
                {
                    this.mulShippingLine.Enabled = false;
                    this.mulShippingLine.Text = string.Empty;

                    this.txtShipOwner.Enabled = false;
                    this.txtShipOwner.SelectedValue = null;


                    this.txtAgent.Enabled = false;
                    this.txtAgent.SelectedValue = null;

                    this.chbCTM.Enabled = false;
                }

            }
            else if (this.chbFCL.Checked ||
                  this.chbLCL.Checked)
            {
                this.mulShippingLine.Enabled = true;
                this.mulShippingLine.Text = string.Empty;

                this.txtShipOwner.Enabled = true;
                this.txtShipOwner.SelectedValue = null;



                this.txtAgent.Enabled = true;
                this.txtAgent.SelectedValue = null;


                this.chbCTM.Enabled = false;
            }
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

        private void label5_Click(object sender, EventArgs e)
        {

        }
        

    }
}
