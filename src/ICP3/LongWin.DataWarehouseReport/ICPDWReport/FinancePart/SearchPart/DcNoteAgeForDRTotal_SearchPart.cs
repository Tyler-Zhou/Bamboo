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
    /// Ӧ���ʿ�������ѯ���
    /// </summary>
    public partial class DcNoteAgeForDRTotal_SearchPart : UserControl
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

        #endregion

        #region ��ʼ��

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
                    //MessageBox.Show("����Ϊ��,����");
                    _repBaseDataService = REPModuleInit.RepService;
                    //return;
                }
                this.cmbSalesType.Enabled = false;

                this.ucSelectCompany.DataSource = this._repBaseDataService.GetStructureNodesForAgentByCurrentUser();
                
                this.dtEtdEndTime.Value = DateTime.Now;

                this.cmbViewType.Items.Clear();
                this.cmbViewType.Items.AddRange(new string[]{
                                                  Utility.GetValueString("�ͻ�", "�ͻ�"),
                                                  Utility.GetValueString("����", "����"),
                                                  Utility.GetValueString("ȫ��", "ȫ��")
                                                   });

                Utility.SetGoodsSalesTypes(cmbSalesType);
                                
                this.cmbGroupBy.Items = new string[]{
                                                  Utility.GetValueString("�ͻ�", "�ͻ�"),
                                                  Utility.GetValueString("ҵ��Ա", "ҵ��Ա"),
                                                  Utility.GetValueString("ҵ������", "ҵ������"),
                                                  Utility.GetValueString("������ʽ", "������ʽ"),
                                                   };
                this.cmbGroupBy.Text = Utility.GetValueString("�ͻ�", "�ͻ�") + "," + Utility.GetValueString("ҵ��Ա", "ҵ��Ա");

                this.ultraExplorerBar1.Groups[0].Text = Utility.GetValueString("����", "����");
                this.ultraExplorerBar1.Groups[1].Text = Utility.GetValueString("����", "����");

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

        #region ��ѯ�¼�
        /// <summary>
        /// ��ѯ�¼�
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

        #region ����
        /// <summary>
        /// ��ʾ���ͣ���ȫ�������ͻ���������
        /// </summary>
        public int ViewType
        {
            get
            {
                return this.cmbViewType.SelectedIndex;
            }
        }

        /// <summary>
        /// true:��Ӧ����֣�
        /// false:ֻ��ʾӦ��
        /// </summary>
        public bool IsDrCr
        {
            get
            {
                return this.IsDR.Checked;
            }
        }

      

        #region ������ʽ
        /// <summary>
        /// ������ʽ��������������ָ��������ͬ�л�����ȫ��
        /// </summary>
        public int SalesType
        {
            get
            {

                return this.cmbSalesType.SelectedIndex;
            }
        }
        ///// <summary>
        ///// ������ʽ���ı���ʾ
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
        /// ����ʱ��
        /// </summary>
        public DateTime EndTime
        {
            get { return this.dtEtdEndTime.Value; }
        }

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
        private string _customerIds;
        /// <summary>
        /// �ͻ�
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
        /// �ͻ����ı���ʾ
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
        /// ������Ϣ
        /// </summary>
        public string ConditionRemark
        {
            get
            {
                string condition = "";
                condition +=Utility.GetValueString("����", "����") +this.ucSelectCompany.Name + ";";
                
                
                if (condition.EndsWith(";"))
                {
                    condition = condition.Substring(0, condition.Length - 1);
                }
                return condition;

            }
        }
        #endregion

        #region �����أͣ�
        public string XMLCondition
        {
            get
            {
               

                System.IO.StringWriter str = new System.IO.StringWriter();
                XmlTextWriter writer = new XmlTextWriter(str);
                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();
                writer.WriteComment(" ��ѯ����XML");

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

        #region ������



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
                string info = Utility.IsEnglish == false ? "�ͻ�����" : "Customer Search";
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

        void DoClearing()
        {
          
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
