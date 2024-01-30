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
using Agilelabs.Framework.Service;
using LongWin.CommonData.ServiceInterface;
using LongWin.CommonData.ServiceInterface.DataObjects;

namespace LongWin.DataWarehouseReport.WinUI
{
    /// <summary>
    ///凭证列表
    ///表查询面板
    /// </summary>
    public partial class ViewVoucher_SearchPart : UserControl
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

        public ViewVoucher_SearchPart()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(ViewVoucher_SearchPart_Disposed);
        }

        void ViewVoucher_SearchPart_Disposed(object sender, EventArgs e)
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
                this.ultraExplorerBar1.Groups[0].Text = Utility.GetValueString("日期", "日期");
                this.ultraExplorerBar1.Groups[1].Text = Utility.GetValueString("常规", "常规");
                this.ultraExplorerBar1.Groups[2].Text = Utility.GetValueString("更多", "更多");

                this.cmbVoucherType.Items = new string[]{
                                                  Utility.GetValueString("计帐凭证", "计帐凭证"),
                                                  Utility.GetValueString("实收实付凭证", "实收实付凭证"),
                                                  Utility.GetValueString("管理成本", "管理成本")
                                                };


                this.cmbVoucherType.Text = Utility.GetValueString("计帐凭证", "计帐凭证");

                this.ucSelectCompany.DataSource = this._repBaseDataService.GetStructureNodesForAgentByCurrentUser();
                this.ucSelectCompany.DisplayCompanyOnly = true;

                this.checkTotalForGL.Checked = false;
                this.checkTotalForGL.Enabled = true;
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
            this._onlyDisplayGLError = "0";
            //this._titleInfo = null;
            if (this.SearchResult != null)
            {
                this.SearchResult(sender, e);
            }
        }

         #endregion

        #region 属性

        string _onlyDisplayGLError ="0";

        /// <summary>
        /// 报表类型
        /// </summary>
        public int ReportType
        {
            get
            {
                if (this.checkTotalForGL.Checked)
                    return 1;
                else
                    return 0;
             
            }
        }

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


        #region 条件ＸＭＬ
        public string XMLCondition
        {
            get
            {
               //<root>   
               // <StructNodeId>41D7D3FE-183A-41CD-A725-EB6F728541EC</StructNodeId> 
               //   <Beginning_Date>2009-04-01</Beginning_Date>   
               // <Ending_Date>2009-04-30</Ending_Date>
               // <BillNo/><BillNo/>   
               //     <JobNo>OESZGS09030382</JobNo>  
               // <VoucherType>0</VoucherType>  
               // <IsEnglish>0</IsEnglish>  
               //     </root> 

                System.IO.StringWriter str = new System.IO.StringWriter();
                XmlTextWriter writer = new XmlTextWriter(str);
                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();
                writer.WriteComment(" 查询条件XML");

                writer.WriteStartElement("root");

                writer.WriteStartElement("StructNodeId");
                writer.WriteValue(this.StructNodeId.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("Beginning_Date");
                writer.WriteValue(this.BeginTime.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("Ending_Date");
                writer.WriteValue(this.EndTime.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("JobNo");
                writer.WriteValue(this.tbVoucherNo.Text.Trim());
                writer.WriteEndElement();


                writer.WriteStartElement("VoucherType");
                writer.WriteValue(this.VoucherType);
                writer.WriteEndElement();

                writer.WriteStartElement("IsEnglish");
                writer.WriteValue(Utility.IsEnglish?"1":"0");
                writer.WriteEndElement();

                writer.WriteStartElement("GLNO");
                writer.WriteValue(this.tbGLNo.Text.Trim());
                writer.WriteEndElement();


                writer.WriteStartElement("IsAgentPR");
                writer.WriteValue(this.cmbInnerType.SelectedIndex.ToString());
                writer.WriteEndElement();

                string IsPR = "2";//全部
                switch ((this.cbReceive.Checked ? "1" : "0") + (this.cbPay.Checked ? "1" : "0"))
                {
                    case "11":
                        IsPR = "2";
                        break;
                    case "00":
                        IsPR = "3";
                        break;
                    case "01":
                        IsPR = "1";
                        break;
                    case "10":
                        IsPR = "0";
                        break;
                }

                

                writer.WriteStartElement("IsPR");
                writer.WriteValue(IsPR);
                writer.WriteEndElement();

                writer.WriteStartElement("OnlyDisplayGLError");
                writer.WriteValue(this._onlyDisplayGLError);
                writer.WriteEndElement();

               
                
                writer.WriteEndElement();

                writer.WriteEndDocument();

                writer.Close();

                return str.ToString();
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
              }
        }

        #endregion

        public event EventHandler CheckCustomerCode;

        List<UFCustomer> _uFCustomerList = new List<UFCustomer>();

        public List<UFCustomer> UFCustomerList
        {
            get { return _uFCustomerList; }
        }

        public string VoucherType
        {
            get
            {
                string value=string.Empty;
                if (this.cmbVoucherType.Text.Contains(Utility.GetValueString("计帐凭证", "计帐凭证")))
                    value="0";
                if (this.cmbVoucherType.Text.Contains(Utility.GetValueString("实收实付凭证", "实收实付凭证")))
                {
                    if (value==string.Empty)
                    value="1";
                    else
                        value=value+",1";

                }
                if (this.cmbVoucherType.Text.Contains(Utility.GetValueString("管理成本", "管理成本")))
                {
                    if (value==string.Empty)
                    value="2";
                    else
                        value=value+",2";

                }
                if (value == string.Empty) value = "0,1,2";
                return value;
            }
        }
        private void btCheckFinanceCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.VoucherType.Contains(","))
                {
                    MessageBox.Show(string.Format("不能同时对 {0} 进行财务代码验证。",this.cmbVoucherType.Text));
                    return;
                }
                _uFCustomerList.Clear();
                this.Cursor = Cursors.WaitCursor;
                UFServerForm uform;
                if (this.VoucherType == "2")
                    uform = new UFServerForm(true);
                else
                    uform = new UFServerForm();
                DialogResult result = uform.ShowDialog(this.ParentForm);
                if (result == DialogResult.OK)
                {
                    _uFCustomerList = uform.UFCustomerList;

                    GetLedgerData();

                    this.CheckCustomerCode(sender, e);
                    //VoucherExport.CheckFinceCode(uform.UFCustomerList, _listLedgerData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.ParentForm.Cursor = Cursors.Default;
            }
        }

        List<LedgerData> _listLedgerData;

        public List<LedgerData> ListLedgerData
        {
            get { return _listLedgerData; }
            set { _listLedgerData = value; }
        }
        private void  GetLedgerData()
        {
            _listLedgerData = _repBaseDataService.GetLedgerData(this.BeginTime
                , this.EndTime
                , this.StructNodeId.ToString()
                , Convert.ToInt16(this.VoucherType));
        }


        public event EventHandler ExportVoucher;

        private void btExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.VoucherType.Contains(","))
                {
                    MessageBox.Show("不能同时导出："+this.cmbVoucherType.Text);
                    return;
                }
                 _uFCustomerList.Clear();
                 if (this.VoucherType == "2")
                 {
                     this.Cursor = Cursors.WaitCursor;
                     UFServerForm uform = new UFServerForm(true);
                     DialogResult result = uform.ShowDialog(this.ParentForm);
                     if (result == DialogResult.OK)
                     {
                         _uFCustomerList = uform.UFCustomerList;
                     }
                 }

                this.Cursor = Cursors.WaitCursor;
                GetLedgerData();
                LedgerData led =  _listLedgerData.Find(d => d.GLCode == "会计科目错误" ||  d.GLCode == string.Empty);
                if (led != null)
                {
                    MessageBox.Show("存在会计科目有误的数据,请修改后再导入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (this.SearchResult != null)
                    {
                        this._onlyDisplayGLError = "1";
                        this.SearchResult(sender, e);
                    }
                }
               this.ExportVoucher(sender, e);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

      

        private void cmbVoucherType_TextChanaged(object sender, EventArgs e)
        {
            this.btCheckFinanceCode.Text = "验证财务代码";
            if (this.VoucherType == "1")
            {
                this.cbPay.Checked = true;
                this.cbPay.Enabled = true;
                this.cbReceive.Checked = true;
                this.cbReceive.Enabled = true;
                this.cmbInnerType.Enabled = true;
                this.cmbInnerType.SelectedIndex = 2;
            }
            else
            {
                this.cbPay.Checked = false;
                this.cbPay.Enabled = false;
                this.cbReceive.Checked = false;
                this.cbReceive.Enabled = false;
                this.cmbInnerType.Enabled = false;
                this.cmbInnerType.SelectedIndex = 2;
            }

            if (this.VoucherType == "2")
            {
                this.btCheckFinanceCode.Text = "验证职员信息";
            }
        }


      
        
       
    }
}
