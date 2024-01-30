using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using LongWin.CommonData.ServiceInterface;
using LongWin.CommonData.ServiceInterface.DataObjects;
using LongWin.Framework.Client;


namespace LongWin.DataWarehouseReport.WinUI.BusinessPart.REPBusiness.SearchPart
{
    public partial class AgentForOperator_SearchPart : UserControl
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IDataFindClientService _dataFinder { get; set; }

        [ServiceDependency]
        public IDataFinderFactory _finderFactory{get;set;}

        #endregion

        #region 

        public AgentForOperator_SearchPart()
        {
            InitializeComponent();
            this.Load += new EventHandler(AgentForOperator_SearchPart_Load);
        }

        void AgentForOperator_SearchPart_Load(object sender, EventArgs e)
        {
            Workitem.Commands["Search"].AddInvoker(btnSeach, "Click");
            this.InitControls();
            this.Disposed += new EventHandler(AgentForOperator_SearchPart_Disposed);
        }

        void AgentForOperator_SearchPart_Disposed(object sender, EventArgs e)
        {
            if (Workitem != null) Workitem.Items.Remove(this);
        }

        private void InitControls()
        {
            this.txtSelectCustomer.DoSearching += new EventHandler<SearchEventArgs>(txtSelectCustomer_DoSearching);
            this.txtSelectSales.DoSearching += new EventHandler<SearchEventArgs>(txtSelectSales_DoSearching);

            if(Utility.IsEnglish)
                cobSearchDate.DataSource = new List<string> { "CreatDate", "ETD" };
            else
                cobSearchDate.DataSource = new List<string> { "创建日期", "ETD" };


            Utility.SetGoodsSalesTypes(cmbGoodsType);
            this.cmbGoodsType.SelectedIndex = 2;

        }

        #endregion

        #region 属性

        IDataFinder dataFinder = null;
        string finderType = string.Empty;

        #region Customer

        private string _customerIds;
        /// <summary>
        /// 客户
        /// </summary>
        string CustomerIds
        {
            get
            {
                _customerIds = string.Empty;
                if (this.txtSelectCustomer.SelectedValue != null && !string.IsNullOrEmpty(this.txtSelectCustomer.SelectedValue.ToString()))
                {
                    Guid[] customerIds = (Guid[])this.txtSelectCustomer.SelectedValue;
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
        string CustomerNames
        {
            get { return txtSelectCustomer.SelectedText; }
        }

        #endregion

        #region 业务员
        private string _employeeIds;
        /// <summary>
        /// 业务员
        /// </summary>
        string EmployeeIDs
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
        string EmployeeNames
        {
            get { return txtSelectSales.SelectedText; }
        }
        #endregion

        #endregion

        #region 搜索器

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

            Control ctl = this.dataFinder.PickMany(returnFields, exitsValues);
            this.Workitem.Workspaces[RepConst.mainWorkspace].Show(ctl, new SmartPartInfo(finderName, finderName));
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
                    _employeeIds = this.txtSelectSales.SelectedText = text;
                    this.txtSelectSales.SelectedValue = Ids;
                }
                else if (finderType == RepConst.FindCustomerKey)
                {
                    _customerIds = this.txtSelectCustomer.SelectedText = text;
                    this.txtSelectCustomer.SelectedValue = Ids;
                }

            }
        }

        #endregion



        public AgentForOperatorForReport GetData() 
        {
            AgentForOperatorForReport afoData = new AgentForOperatorForReport();
            afoData.GoodsType =(short) cmbGoodsType.SelectedIndex;
            afoData.CustomerIDs = CustomerIds;
            afoData.SalesIDs = EmployeeIDs;
            afoData.DateFrom = ucDateETD.DateTimeFrom;
            afoData.DateTo = ucDateETD.DateTimeTo;
            afoData.isEtd = cobSearchDate.SelectedIndex == 0 ? false : true;

            return afoData;
        }
    }
    public class AgentForOperatorForReport
    {
        public short GoodsType { get; set; }
        public string CustomerIDs { get; set; }
        public string SalesIDs { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool isEtd { get; set; }
    }
}
