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
    /// ҵ����Ϣ
    /// </summary>
    public partial class JobInformation_SearchPart : UserControl
    {
        #region ����ע��

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
            set { _initializeService = value; }
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

        #region ��ʼ��
        public JobInformation_SearchPart()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(JobInformation_SearchPart_Disposed);
        }

        void JobInformation_SearchPart_Disposed(object sender, EventArgs e)
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
                    //MessageBox.Show("����Ϊ��,����");
                    _repBaseDataService = REPModuleInit.RepService;
                    _transportFoundationService = REPModuleInit.FoundationService;
                    if (REPModuleInit.InitializeService != null)
                        _initializeService = REPModuleInit.InitializeService;
                }
                this.ucSelectCompany.DataSource = this._repBaseDataService.GetStructureNodesByCurrentUser();

                this.cmbDateType.Items.Clear();
                this.cmbDateType.Items.AddRange(new object[] {
                                                        Utility.GetValueString("ҵ��ʱ��", "ҵ��ʱ��")
                                                        });



                this.ultraExplorerBar1.Groups[0].Text = Utility.GetValueString("����", "����");
                this.ultraExplorerBar1.Groups[1].Text = Utility.GetValueString("����", "����");
                this.ultraExplorerBar1.Groups[2].Text = Utility.GetValueString("����", "����");

                this.txtSearchField.Enabled = false;
                this.cmbSearchType.SelectedIndex = 0;
                this.cmbDateType.SelectedIndex = 0;
                if (!this._repBaseDataService.GetUserIsManange())
                {
                    this.txtSelectSales.Enabled = false;
                    this.txtSelectSales.SelectedText = _initializeService.GetUserInfo().DispalyName;
                    txtSelectSales.SelectedValue = new Guid[] { new Guid(_initializeService.GetUserInfo().UserId) };
                }
            }
        }

        #endregion

        #region �¼�
        private void cmbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbSearchType.SelectedIndex == 0)
            {
                this.txtSearchField.Enabled = false;
            }
            else { this.txtSearchField.Enabled = true; }
        }
        /// <summary>
        /// ��ѯ�¼�
        /// </summary>
        public event EventHandler SearchResult;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this._configureDate = null;
            if (this.SearchResult != null)
            {
                this.SearchResult(sender, e);
            }
        }
        #endregion

        #region ����

        #region ʱ�䷶Χ
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

        #region ����
        
        /// <summary>
        /// ��֯�ṹ��Id
        /// </summary>
        public string StructNodeId
        {
            get
            {
                if (this.txtSelectSales.Enabled == false)
                {
                    return "701ACD43-D49B-422B-83A9-ACB56B696995";
                }
                else
                {
                    return this.ucSelectCompany.Value.ToString();
                }
            }
        }
        /// <summary>
        /// ��֯�ṹ������

        /// </summary>
        public string StructureNodeName
        {
            get { return this.ucSelectCompany.Text; }
        }
        #endregion

        #region ҵ��Ա
        private string _employeeIds;
        /// <summary>
        /// ҵ��Ա
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
        /// ҵ��Ա����
        /// </summary>
        public string EmployeeNames
        {
            get { return txtSelectCustomer.SelectedText; }
        }
        #endregion

        #region �Ƿ�ӯ��
        /// <summary>
        /// �Ƿ�ӯ������ȫ��������ӯ������������
        /// </summary>
        public int ProfitFlag
        {
            get {
                if (this.rabAll.Checked)
                {
                    return 0;
                }
                else if (this.rabProfit.Checked)
                {
                    return 1;
                }
                return 2;
            }
        }
        #endregion

        #region �ͻ�
        private string _customerIds;
        /// <summary>
        /// �ͻ�
        /// </summary>
        public string CustomerIds
        {
            get
            {
                _customerIds = string.Empty;
                if (this.txtSelectCustomer.SelectedValue != null && !string.IsNullOrEmpty(this.txtSelectCustomer.SelectedValue.ToString()))
                {
                    Guid[] customerIds = (Guid[])this.txtSelectCustomer.SelectedValue;
                     foreach(Guid id in customerIds)
                     {
                         _customerIds += "'"+id.ToString() + "',";
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
            get { return this.txtSelectCustomer.Text; }
        }
        #endregion

        #region ҵ������
        private string _businessTypes;
        /// <summary>
        /// ҵ�����͵�ֵ
        /// </summary>
        public string BusinessTypes
        {
            get
            {
                if (this.mulBusiness.Value.Trim() != string.Empty)
                {
                    this._businessTypes = "'" + this.mulBusiness.Value.Trim() + "'";
                    _businessTypes = _businessTypes.Replace(",", "','");
                    return _businessTypes;
                }
                return string.Empty;
            }
        }
        #endregion

        /// <summary>
        /// ʱ������
        /// </summary>
        public short DateType
        {
            get
            {
                return (short)this.cmbDateType.SelectedIndex;
            }

        }

        #region ����
        /// <summary>
        /// ��ʲô����
        /// </summary>
        public string SearchValue
        {
            get {
                if (this.cmbSearchType.SelectedIndex > 0)
                {
                    return this.txtSearchField.Text.Trim();
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string SearchField
        {
            get
            {
                if (this.cmbSearchType.SelectedIndex > 0)
                {
                    return this.cmbSearchType.Text;
                }
                return string.Empty;
            }
        }
        #endregion

        #region �Ƿ���Կ���Ӧ���ʿ��������λ
        /// <summary>
        /// �Ƿ���Կ���Ӧ���ʿ��������λ
        /// </summary>
        public bool IsViewShipper
        {
            get {
                return this._repBaseDataService.GetUserIsManange();

            }
        }
        #endregion


        #region ��˾��Ϣ


        IConfigureService _configureService;
        [ServiceDependency]
        public IConfigureService ConfigureService
        {
            get { return _configureService; }
            set { _configureService = value; }
        }


        SystemConfigureData _configureDate;
        /// <summary>
        /// ��˾������
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
        /// ��˾��Ӣ������
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
        /// �绰����
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
        /// ��˾��ַ
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
        /// ��˾��ַ
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

        #region ������Ϣ
       
        /// <summary>
        /// �ڼ�
        /// </summary>
        public string Peried
        {
            get { return this.ucDateTime.DateTimeFrom.ToShortDateString() + "-" + this.ucDateTime.DateTimeTo.ToShortDateString(); }
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string ConditionRemark
        {
            get
            {
                string condition = "";
                condition +=Utility.GetValueString("����", "����") +"  : "+this.ucSelectCompany.Text + ";";
                
                if (condition.EndsWith(";"))
                {
                    condition = condition.Substring(0, condition.Length - 1);
                }
                return condition;

            }
        }
        #endregion


        #region ������

        private void txtSelectSales_DoSearching(object sender, SearchEventArgs e)
        {
            finderType = RepConst.FindUserKey;
            Guid[] guids = new Guid[] { };
            if (e.SelectedValue != null)
            {
                guids = (Guid[])e.SelectedValue;
            }
            this.DoSearching("Name", guids);
        }
        void txtSelectCustomer_DoSearching(object sender, SearchEventArgs e)
        {
            finderType = RepConst.FindCustomerKey;
            Guid[] guids = new Guid[] { };
            if (e.SelectedValue != null)
            {
                guids = (Guid[])e.SelectedValue;
            }
            this.DoSearching(Utility.IsEnglish ? "EName" : "CName", guids);
        }


        void DoSearching(string searchField,Guid[] exitsValues)
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
                string info =  Utility.IsEnglish==false ? "�ͻ�����" : "Customer Search";
                this._workItem.Workspaces[RepConst.mainWorkspace].Show(ctl, new SmartPartInfo(info, info));
            }
            else
            {
                string[] returnFields = new string[] { "Id", "Name"};
                Control ctl = this.dataFinder.PickMany(returnFields, exitsValues);
                string info = Utility.IsEnglish == false ? "�û�����" : "User Search";
                this._workItem.Workspaces[RepConst.mainWorkspace].Show(ctl, new SmartPartInfo(info, info));
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

                    this.txtSelectCustomer.SelectedText = customerNames;
                    this.txtSelectCustomer.SelectedValue = customerIds;
                }
                //����ҵ��Ա
                else if (this.finderType == RepConst.FindUserKey)
                {
                    string userText = string.Empty;
                    Guid[] userIds = new Guid[result.Length];
                    for (int i = 0; i < result.Length; i++)
                    {
                        userText += (result[i] as object[])[1].ToString() + ",";

                        userIds[i] = (Guid)((result[i] as object[])[0]);
                    }

                    this.txtSelectSales.SelectedText = userText;
                    this.txtSelectSales.SelectedValue = userIds;
                }
            }
        }

        #endregion

        private void label6_Click(object sender, EventArgs e)
        {

        }

        

        #endregion
    }
}
