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
    /// ��Ӷͳ�ƺ���ϸ���Ĳ�ѯ���
    /// </summary>
    public partial class CommisionSearchPart : UserControl
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

        public CommisionSearchPart()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(CommisionSearchPart_Disposed);
        }

        void CommisionSearchPart_Disposed(object sender, EventArgs e)
        {
            this._workItem.Items.Remove(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.cmbRecoupFlag.SelectedIndex = 0;
            if (!this.DesignMode)
            {
                if (_repBaseDataService == null )
                {
                    //MessageBox.Show("����Ϊ��,����");
                    _repBaseDataService = REPModuleInit.RepService;
                   
                    //return;
                }
                this.cmbGroupBy.Visible = false;
                this.lbGroupby.Visible = false;
                this.ucSelectCompany.DataSource = this._repBaseDataService.GetStructureNodesByCurrentUser();

                if (!this._repBaseDataService.GetUserIsManange())
                {
                    this.txtSales.Enabled = false;
                    this.txtSales.SelectedText = _initializeService.GetUserInfo().DispalyName;
                    txtSales.SelectedValue = new Guid[] { new Guid(_initializeService.GetUserInfo().UserId) };
                }

                this.ultraExplorerBar1.Groups[0].Text = Utility.GetValueString("����", "����");
                this.ultraExplorerBar1.Groups[1].Text = Utility.GetValueString("����", "����");

                this.cmbRecoupFlag.Items.Clear();
                this.cmbRecoupFlag.Items.AddRange(new string[]{
                                                  Utility.GetValueString("��", "��"),
                                                  Utility.GetValueString("��", "��"),
                                                  Utility.GetValueString("ȫ��", "ȫ��")
                                                   });
                this.cmbRecoupFlag.SelectedIndex = 2;

                this.cmbLocal.Items.Clear();
                this.cmbLocal.Items.AddRange(new string[]{
                                                  Utility.GetValueString("��", "��"),
                                                  Utility.GetValueString("��", "��"),
                                                  Utility.GetValueString("ȫ��", "ȫ��")
                                                   });
                this.cmbLocal.SelectedIndex = 2;
                 

                this.cmbGroupBy.Items.Clear();
                this.cmbGroupBy.Items.AddRange(new string[]{
                                                  Utility.GetValueString("ҵ��Ա,������λ", "Sales,Shipper"),
                                                  Utility.GetValueString("ҵ��Ա", "Sales"),
                                                   });
                this.cmbGroupBy.SelectedIndex = 0;

                this.cmbFeeType.SelectedIndex = 1;

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
            //UFServerForm form = new UFServerForm();
            //form.Show();

            if (this.SearchResult != null)
            {
                this.SearchResult(sender, e);
            }
        }
        #endregion

        #region ����

        #region ��֯�ṹ
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

        #region ��������
        /// <summary>
        /// ����ϸ������ͳ�Ʊ�
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
        /// ҵ��Ա����
        /// </summary>
        public string EmployeeNames
        {
            get { return txtSales.SelectedText; }
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
        /// ҵ�����͵��ı�
        /// </summary>
        public string BusinessTypeNames
        {
            get { return mulBusiness.Text; }
        }
        #endregion

        #region ������־
        /// <summary>
        /// ������־�����Ѻ���������δ����������ȫ��
        /// </summary>
        public int RecoupFlag
        {
            get { return this.cmbRecoupFlag.SelectedIndex; }
        }
        /// <summary>
        /// ���ط��ú���
        /// </summary>
        public int LocalRecoupFlag
        {
            get { return this.cmbLocal.SelectedIndex;}
        }

        /// <summary>
        /// �����־
        /// </summary>
        public int GroupByFlag
        {
            get { return this.cmbGroupBy.SelectedIndex; }
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
        /// ������Ϣ
        /// </summary>
        public string JobPlace
        {
            get
            {
                if (this.ucSelectCompany.StructType==0)
                {
                    return Utility.GetValueString("ҵ������", "ҵ������") +"  : " +this.ucSelectCompany.Text.Trim();
                }
                return Utility.GetValueString("ҵ����������", "ҵ����������") +"  : "+this.ucSelectCompany.Text.Trim();
            }
        }
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
                string condition =Utility.GetValueString("����", "����") +"  : "+this.ucSelectCompany.Text + ";";
                return condition;
            }
        }
        #endregion
        /// <summary>
        /// �Ƿ�ֻ��ʾӶ��
        /// </summary>
        public string IsOnlyCommision
        {
            get { 
                return this.cmbFeeType.SelectedIndex.ToString(); }            
        }
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
                Control ctl = this.dataFinder.PickMany(returnFields, exitsValues);
                string info = Utility.IsEnglish==false ? "�ͻ�����" : "Customer Search";
                this._workItem.Workspaces[RepConst.mainWorkspace].Show(ctl, new SmartPartInfo(info,info));
            }
            else
            {
                string[] returnFields = new string[] { "Id", "Name" };
                Control ctl = this.dataFinder.PickMany(returnFields, exitsValues);
                string info = Utility.IsEnglish == false ? "�û�����" : "User Search";
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

                    this.txtSales.SelectedText = userText;
                    this.txtSales.SelectedValue = userIds;
                }
            }
        }
        #endregion

        private void rabDetail_CheckedChanged(object sender, EventArgs e)
        {

            this.cmbGroupBy.Visible = rabDetail.Checked;
            this.lbGroupby.Visible = rabDetail.Checked;
            this.cmbGroupBy.SelectedIndex = 0;

        }

    }
}