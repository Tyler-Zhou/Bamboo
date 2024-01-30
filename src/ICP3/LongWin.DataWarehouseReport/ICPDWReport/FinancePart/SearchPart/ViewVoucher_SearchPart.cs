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
    ///ƾ֤�б�
    ///���ѯ���
    /// </summary>
    public partial class ViewVoucher_SearchPart : UserControl
    {
        #region ����ע��

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

        #region ��ʼ��

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
                    //MessageBox.Show("����Ϊ��,����");
                    _repBaseDataService = REPModuleInit.RepService;
                    //return;
                }
                this.ultraExplorerBar1.Groups[0].Text = Utility.GetValueString("����", "����");
                this.ultraExplorerBar1.Groups[1].Text = Utility.GetValueString("����", "����");
                this.ultraExplorerBar1.Groups[2].Text = Utility.GetValueString("����", "����");

                this.cmbVoucherType.Items = new string[]{
                                                  Utility.GetValueString("����ƾ֤", "����ƾ֤"),
                                                  Utility.GetValueString("ʵ��ʵ��ƾ֤", "ʵ��ʵ��ƾ֤"),
                                                  Utility.GetValueString("����ɱ�", "����ɱ�")
                                                };


                this.cmbVoucherType.Text = Utility.GetValueString("����ƾ֤", "����ƾ֤");

                this.ucSelectCompany.DataSource = this._repBaseDataService.GetStructureNodesForAgentByCurrentUser();
                this.ucSelectCompany.DisplayCompanyOnly = true;

                this.checkTotalForGL.Checked = false;
                this.checkTotalForGL.Enabled = true;
            }
        }

        #endregion

        #region �¼�
        /// <summary>
        /// ��ѯ�¼�
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

        #region ����

        string _onlyDisplayGLError ="0";

        /// <summary>
        /// ��������
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

        #region ����
        /// <summary>
        /// ��ҵ�����ػ���ҵ����������
        /// </summary>
        public int StructureType
        {
            get
            {
                return this.ucSelectCompany.StructType;
            }
        }

        /// <summary>
        /// ��֯�ṹ��Id
        /// </summary>
        public string StructNodeId
        { get { return this.ucSelectCompany.Value.ToString(); } }
        /// <summary>
        /// ��֯�ṹ������

        /// </summary>
        public string StructureNodeName
        {
            get { return this.ucSelectCompany.Text; }
        }
        #endregion 

        #region ʱ��
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime BeginTime
        {
            get { return this.ucDateTime.DateTimeFrom; }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime EndTime
        {
            get { return this.ucDateTime.DateTimeTo; }
        }

        #endregion

        #region ������λ
        private string _customerIds;
        /// <summary>
        /// �ͻ�
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
        /// �ͻ����ı���ʾ
        /// </summary>
        public string CustomerNames
        {
            get { return txtShipTo.SelectedText; }
        }
        #endregion


        #region �����أͣ�
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
                writer.WriteComment(" ��ѯ����XML");

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

                string IsPR = "2";//ȫ��
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

        #region ������

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
                string info = Utility.IsEnglish == false ? "�ͻ�����" : "Customer Search";
                this._workItem.Workspaces[RepConst.mainWorkspace].Show(ctl, new SmartPartInfo(info,info));
            }           
        }

        void BindSelectData(object[] result)
        {
            if (result != null)
            {
                //�ͻ�����
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
                if (this.cmbVoucherType.Text.Contains(Utility.GetValueString("����ƾ֤", "����ƾ֤")))
                    value="0";
                if (this.cmbVoucherType.Text.Contains(Utility.GetValueString("ʵ��ʵ��ƾ֤", "ʵ��ʵ��ƾ֤")))
                {
                    if (value==string.Empty)
                    value="1";
                    else
                        value=value+",1";

                }
                if (this.cmbVoucherType.Text.Contains(Utility.GetValueString("����ɱ�", "����ɱ�")))
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
                    MessageBox.Show(string.Format("����ͬʱ�� {0} ���в��������֤��",this.cmbVoucherType.Text));
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
                    MessageBox.Show("����ͬʱ������"+this.cmbVoucherType.Text);
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
                LedgerData led =  _listLedgerData.Find(d => d.GLCode == "��ƿ�Ŀ����" ||  d.GLCode == string.Empty);
                if (led != null)
                {
                    MessageBox.Show("���ڻ�ƿ�Ŀ���������,���޸ĺ��ٵ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            this.btCheckFinanceCode.Text = "��֤�������";
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
                this.btCheckFinanceCode.Text = "��ְ֤Ա��Ϣ";
            }
        }


      
        
       
    }
}
