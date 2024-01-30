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
    public partial class OEBussinesInfo_SearchPart : UserControl
    {
        #region 服务

        private WorkItem Workitem = null;
        private IDataFindClientService _dataFinder = null;
        private IDataFinderFactory _finderFactory = null;
        private IDataFindClientService _dfcService = null;

        #endregion

        #region 初始化

        public OEBussinesInfo_SearchPart(WorkItem workitem, IDataFindClientService dataFinder, IDataFinderFactory finderFactory, IDataFindClientService dfcService)
        {
            this.Workitem = workitem;
            this._dataFinder = dataFinder;
            this._finderFactory = finderFactory;
            this._dfcService = dfcService;
            InitializeComponent();
            this.Disposed += new EventHandler(OEBussinesInfo_SearchPart_Disposed);
        }

        void OEBussinesInfo_SearchPart_Disposed(object sender, EventArgs e)
        {
            if (Workitem != null) Workitem.Items.Remove(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Workitem.Commands["Search"].AddInvoker(btnSeach, "Click");
            this.InitControls();
        }

        private void InitControls()
        {
            this.txtSelectCustomer.DoSearching += new EventHandler<SearchEventArgs>(txtSelectCustomer_DoSearching);

            _dfcService.Register(txtSales,"User","Code/Name", new string[] { "ID", "Name" ,"Code"},
                                delegate(object inputSource, object[] data)
                                 {
                                     txtSales.Tag = new Guid(data[0].ToString());
                                     txtSales.Text = data[1].ToString();
                                 }, "MainWorkspace");


            if(Utility.IsEnglish)
                cmbGroupByType.DataSource = new List<string> { "Customer", "Company" };
            else
                cmbGroupByType.DataSource = new List<string> { "客户", "业务发生地" };

            cmbGroupByType.SelectedIndex = 0;

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

            else if (this.finderType == "LoadPort"
                || this.finderType == "DiscPort")//港口
            {
                returnFields = new string[] { "Id", "EName" };
                finderName = Utility.IsEnglish == false ? "港口查找" : "Port Search";
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

                _customerIds = this.txtSelectCustomer.SelectedText = text;
                this.txtSelectCustomer.SelectedValue = Ids;
            }
        }

        #endregion

        public OEBussinesInfoForReport GetData() 
        {
            OEBussinesInfoForReport oebData = new OEBussinesInfoForReport();
            oebData.GroupByType = cmbGroupByType.SelectedIndex == 0 ? "Customer" : "Company";
            oebData.CustomerIDs = CustomerIds;
            oebData.SalesID = txtSales.Tag == null ? string.Empty : txtSales.Tag.ToString();
            oebData.DateFrom = ucDateETD.DateTimeFrom;
            oebData.DateTo = ucDateETD.DateTimeTo;

            return oebData;
        }
    }
    public class OEBussinesInfoForReport
    {
        public string GroupByType { get; set; }
        public string CustomerIDs { get; set; }
        public string SalesID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
