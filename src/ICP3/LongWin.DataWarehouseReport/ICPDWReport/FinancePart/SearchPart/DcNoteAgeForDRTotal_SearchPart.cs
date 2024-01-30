using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using LongWin.DataWarehouseReport.ServiceInterface;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using LongWin.Framework.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using LongWin.CommonData.ServiceInterface;
using LongWin.CommonData.ServiceInterface.DataObjects;

namespace LongWin.DataWarehouseReport.WinUI
{
    /// <summary>
    /// 应收帐款帐龄表查询面板
    /// </summary>
    public partial class DcNoteAgeForDRTotal_SearchPart : UserControl
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

        #endregion

        #region 初始化

        public DcNoteAgeForDRTotal_SearchPart()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(DcNoteAgeForDRTotal_SearchPart_Disposed);
        }

        void DcNoteAgeForDRTotal_SearchPart_Disposed(object sender, EventArgs e)
        {
            this._workItem.Items.Remove(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                if (_repBaseDataService == null )
                {
                    //MessageBox.Show("服务为空,请检查");
                    _repBaseDataService = REPModuleInit.RepService;
                    //return;
                }
                this.cmbSalesType.Enabled = false;

                this.ucSelectCompany.DataSource = this._repBaseDataService.GetStructureNodesForAgentByCurrentUser();
                
                this.dtEtdEndTime.Value = DateTime.Now;

                this.cmbViewType.Items.Clear();
                this.cmbViewType.Items.AddRange(new string[]{
                                                  Utility.GetValueString("客户", "客户"),
                                                  Utility.GetValueString("代理", "代理"),
                                                  Utility.GetValueString("全部", "全部")
                                                   });

                Utility.SetGoodsSalesTypes(cmbSalesType);
                                
                this.cmbGroupBy.Items = new string[]{
                                                  Utility.GetValueString("客户", "客户"),
                                                  Utility.GetValueString("业务员", "业务员"),
                                                  Utility.GetValueString("业务发生地", "业务发生地"),
                                                  Utility.GetValueString("揽货方式", "揽货方式"),
                                                   };
                this.cmbGroupBy.Text = Utility.GetValueString("客户", "客户") + "," + Utility.GetValueString("业务员", "业务员");

                this.ultraExplorerBar1.Groups[0].Text = Utility.GetValueString("日期", "日期");
                this.ultraExplorerBar1.Groups[1].Text = Utility.GetValueString("常规", "常规");

                this.cmbSalesType.Enabled = true;
                this.cmbViewType.SelectedIndex = 2;
                if (this._repBaseDataService.IsAgentDev())
                {
                    this.cmbViewType.SelectedIndex = 1;
                    this.cmbViewType.Enabled = false;
                }

               
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
            if (this.SearchResult != null)
            {
                this.SearchResult(sender, e);
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 显示类型，２全部；０客户；１代理
        /// </summary>
        public int ViewType
        {
            get
            {
                return this.cmbViewType.SelectedIndex;
            }
        }

        /// <summary>
        /// true:与应付相抵；
        /// false:只显示应收
        /// </summary>
        public bool IsDrCr
        {
            get
            {
                return this.IsDR.Checked;
            }
        }

      

        #region 揽货方式
        /// <summary>
        /// 揽货方式：０自揽货；１指定货；２同行货；３全部
        /// </summary>
        public int SalesType
        {
            get
            {

                return this.cmbSalesType.SelectedIndex;
            }
        }
        ///// <summary>
        ///// 揽货方式的文本显示
        ///// </summary>
        //public string SalesTypeName
        //{
        //    get {
        //        if (this.chbIsViewSalesType.Checked)
        //        {
        //            return this.cmbSalesType.SelectedText;
        //        }
        //        return string.Empty;
        //    }
        //}
        #endregion

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return this.dtEtdEndTime.Value; }
        }

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
        private string _customerIds;
        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerIds
        {
            get
            {
                _customerIds = string.Empty;
                if (this.selectCustomer.SelectedValue != null && !string.IsNullOrEmpty(this.selectCustomer.SelectedValue.ToString()))
                {
                    Guid[] customerIds = (Guid[])this.selectCustomer.SelectedValue;
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
            get
            {
                return selectCustomer.SelectedText;
            }
        }
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
        /// <summary>
        /// 附加信息
        /// </summary>
        public string ConditionRemark
        {
            get
            {
                string condition = "";
                condition +=Utility.GetValueString("部门", "部门") +this.ucSelectCompany.Name + ";";
                
                
                if (condition.EndsWith(";"))
                {
                    condition = condition.Substring(0, condition.Length - 1);
                }
                return condition;

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


                writer.WriteStartElement("ETD_Ending_Date");
                writer.WriteValue(this.EndTime.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("SalesType");
                writer.WriteValue(this.SalesType);
                writer.WriteEndElement();

                writer.WriteStartElement("SalesSet");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();

                writer.WriteStartElement("ShipperSet");
                writer.WriteValue(this.CustomerIds);
                writer.WriteEndElement();

                writer.WriteStartElement("ViewType");
                writer.WriteValue(this.ViewType);
                writer.WriteEndElement();

                writer.WriteStartElement("JobType");
                writer.WriteValue(this.BusinessTypes);
                writer.WriteEndElement();
                
                writer.WriteStartElement("IsBalance");
                writer.WriteValue(Convert.ToInt16(this.IsDrCr).ToString());
                writer.WriteEndElement();


                writer.WriteStartElement("Age");
                writer.WriteValue(9);
                writer.WriteEndElement();

                writer.WriteStartElement("GroupBy");
                writer.WriteValue(this.cmbGroupBy.Text.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("GroupByID");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();

                writer.WriteStartElement("IsEnglish");
                writer.WriteValue(Utility.IsEnglish ? "1" : "0");
                writer.WriteEndElement();

                writer.WriteStartElement("C1");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();

                
                writer.WriteStartElement("C2");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();
                
                writer.WriteStartElement("C3");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();

                
                writer.WriteStartElement("N1");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();
                
                writer.WriteStartElement("N2");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();
                
                writer.WriteStartElement("N3");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();


                writer.WriteStartElement("ConditionName");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();
                

                writer.WriteEndElement();

                writer.WriteEndDocument();

                writer.Close();

                return str.ToString();
            }
        }
        #endregion

        #region 搜索器



        private void selectCustomer_DoSearching(object sender, SearchEventArgs e)
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
                Control ctl = this.dataFinder.PickMany(returnFields, exitsValues);
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

        void DoClearing()
        {
          
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

                    this.selectCustomer.SelectedText = customerNames;
                    this.selectCustomer.SelectedValue = customerIds;
                }
               
            }
        }
        #endregion

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cmbSalesType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
        
    }
}
