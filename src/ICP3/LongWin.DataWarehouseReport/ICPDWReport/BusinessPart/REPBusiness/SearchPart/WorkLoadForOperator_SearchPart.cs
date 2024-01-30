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

namespace LongWin.DataWarehouseReport.WinUI
{
    /// <summary>
    /// ����������ͳ�Ʋ�ѯ���
    /// </summary>
    public partial class WorkLoadForOperator_SearchPart : UserControl
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

        WorkItem _workitem;
        [ServiceDependency]
        public WorkItem Workitem
        {
            get { return _workitem; }
            set { _workitem = value; }
        }
        #endregion

        #region ��ʼ��

        public WorkLoadForOperator_SearchPart()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(WorkLoadForOperator_SearchPart_Disposed);

            this.cmbGroupBy.MaxChecked = 3;
            this.cmbGroupBy.Items = new string[] {      Utility.GetValueString("ҵ������", "ҵ������"),
                                                        Utility.GetValueString("����", "����"),
                                                        Utility.GetValueString("ҵ������", "ҵ������"),
                                                        Utility.GetValueString("����", "����"),
                                                        
                                                        };



        }

        void WorkLoadForOperator_SearchPart_Disposed(object sender, EventArgs e)
        {
            this._workitem.Items.Remove(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                if (_repBaseDataService == null )
                {
                    //MessageBox.Show("����Ϊ��,����");
                    _repBaseDataService = REPModuleInit.RepService;
                    //return;
                }
                this.ucSelectCompany.DataSource = this._repBaseDataService.GetStructureNodesByCurrentUser();

                this.cmbGroupBy.Text = Utility.GetValueString("ҵ������", "ҵ������") + ","
                    + Utility.GetValueString("����", "����") + ","
                    + Utility.GetValueString("ҵ������", "ҵ������");

                this.cmbUserState.SelectedIndex = 1;
                if (this.ucSelectCompany.DataSource.Tables[0].Rows.Count == 1)
                {
                    this.cmbGroupBy.Enabled = false;
                }
            }
        }

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
        public string GroupBy
        {
            get
            {
                return this.cmbGroupBy.Text.Trim();
               // this.cmbGroupBy.Text.Split(new string[] { "o" }, StringSplitOptions.None);
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
                condition +=Utility.GetValueString("����", "����") +"  : "+this.ucSelectCompany.Text;
                return condition;
            }
        }
        #endregion

        public string GetUserState
        {
            get {
                return this.cmbUserState.SelectedIndex.ToString();
            }
        }
        #endregion

        private void cmbGroupBy_Load(object sender, EventArgs e)
        {

        }
    }
}
