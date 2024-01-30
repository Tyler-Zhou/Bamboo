using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FRM.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using System.Text;
using ICP.Common.UI;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.FRM.UI.BookingReport
{
    [ToolboxItem(false)]
    public partial class BookingReportSearchPart : BaseSearchPart
    {
        #region 构造函数
        public BookingReportSearchPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                OnSearched = null;
                
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;

                }
            
            };
        }
        #endregion

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        public IBusinessInfoService BusinessInfoService
        {
            get
            {
                return ServiceClient.GetService<IBusinessInfoService>();
            }
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
  
        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }

        #endregion

        #region 初始化
        bool isLoad = false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControls();
                SetKeyDownToSearch();
                GetGroupBy();
                isLoad = true;
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            dateMonthControl1.IsEngish = true;

            #region 公司
            Utility.SetEnterToExecuteOnec(cmbCompany, delegate {
                List<OrganizationList> orgList = OrganizationService.GetOfficeList();
                cmbCompany.BeginUpdate();
                cmbCompany.Items.Clear();

                foreach (OrganizationList item in orgList)
                {
                    string name=LocalData.IsEnglish?item.EShortName:item.CShortName;
                    cmbCompany.AddItem(item.ID, name);
                }
                cmbCompany.EndUpdate();
            });
            #endregion

            #region 航线
            Utility.SetEnterToExecuteOnec(cmbShipLine, delegate
            {
                List<ShippingLineList> list = TransportFoundationService.GetShippingLineList(string.Empty, string.Empty, true, 0);
                List<TreeCheckControlSource> tss = new List<TreeCheckControlSource>();

                foreach (ShippingLineList item in list)
                {
                    tss.Add(new TreeCheckControlSource { ID = item.ID, ParentID = item.ParentID, Name = LocalData.IsEnglish ? item.EName : item.CName });

                }
                cmbShipLine.SetSource(tss);

            });
            #endregion

            #region 船东
            DataFindClientService.RegisterMultiple(stxtCarrier, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                GetConditionsForCarrier,
         delegate(object inputSource, object[] resultData)
         {
             List<Guid> ids = new List<Guid>();
             StringBuilder names = new StringBuilder();
             foreach (var item in resultData)
             {
                 object[] data = item as object[];
                 if (data == null) continue;

                 ids.Add(new Guid(data[0].ToString()));
                 if (names.Length > 0) names.Append(GlobalConstants.ShowDividedSymbol);
                 names.Append(LocalData.IsEnglish ? data[2].ToString() : data[3].ToString());
             }
             stxtCarrier.Text = names.ToString();
             stxtCarrier.Tag = ids;

         },
         delegate
         {
             stxtCarrier.Text = string.Empty;
             stxtCarrier.Tag = null;
         },
         delegate()
         {
             List<Guid> ids = stxtCustomer.Tag as List<Guid>;
             if (ids == null) return null;
             return ids;
         },
         ClientConstants.MainWorkspace);
                
            #endregion

            #region 客户
            DataFindClientService.RegisterMultiple(stxtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
            null,
            delegate(object inputSource, object[] resultData)
            {
                List<Guid> ids = new List<Guid>();
                StringBuilder names = new StringBuilder();
                foreach (var item in resultData)
                {
                    object[] data = item as object[];
                    if (data == null) continue;

                    ids.Add(new Guid(data[0].ToString()));
                    if (names.Length > 0) names.Append(GlobalConstants.ShowDividedSymbol);
                    names.Append(LocalData.IsEnglish ? data[2].ToString() : data[3].ToString());
                }
                stxtCustomer.Text = names.ToString();
                stxtCustomer.Tag = ids;

            },
            delegate
            {
                stxtCustomer.Text = string.Empty;
                stxtCustomer.Tag = null;
            },
            delegate()
            {
                List<Guid> ids = stxtCustomer.Tag as List<Guid>;
                if (ids == null) return null;
                return ids;
            },
            ClientConstants.MainWorkspace);
                
            #endregion

            #region 揽货类型
            //揽货方式
            Utility.SetEnterToExecuteOnec(cmbSalesType, delegate
            {
                List<DataDictionaryList> salesTypes = ICPCommUIHelper.SetCmbDataDictionary(cmbSalesType, DataDictionaryType.SalesType);

                cmbSalesType.Properties.Items.Insert(0,new ImageComboBoxItem(null, null));
            });
            #endregion

            dateMonthControl1.From = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).AddDays(-15);
            dateMonthControl1.To = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).AddDays(15);

        }
        /// <summary>
        /// 搜索类型为“仓库”型的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForCarrier()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Carrier, false);
            return conditions;
        }
        /// <summary>
        /// 定义快捷键
        /// </summary>
        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is ButtonEdit)
                {
                    continue;
                }

                item.KeyDown += new KeyEventHandler(item_KeyDown);

                if (item is CheckBoxComboBox)
                {
                    ((CheckBoxComboBox)item).KeyDownEnter += new KeyEventHandler(item_KeyDown);
                }
            }

        }
        /// <summary>
        /// 实现快速键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        #endregion

        #region 清空
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbCompany.ClearEditValue();
            //this.cmbShipLine.ClearEditValue();
            stxtCustomer.Tag = null;
            stxtCustomer.Text = string.Empty;
            cmbSalesType.SelectedIndex = 0;
        }


        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (OnSearched != null)
                {
                    List<BookingReportData> list = GetData() as List<BookingReportData>;
                    if (list != null )
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "total search " + list.Count.ToString() + " data." : "总共查询到 "
                                                    + list.Count.ToString() + " 条数据.");
                    }
                    SetGroupBy();
                    OnSearched(this, list);
                }
            }
        }
        #endregion

        #region 重写
        public override event SearchResultHandler OnSearched;
        /// <summary>
        /// 热键查询
        /// </summary>
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            try
            {

                List<BookingReportData> list = BusinessInfoService.GetBookingReportDataList(companyIds,
                                                              shipLineIds,
                                                              customerIds,
                                                              carrierIDs,
                                                              salesTypeID,
                                                              chkbValid.Checked,
                                                              dateMonthControl1.From,
                                                              dateMonthControl1.To,
                                                              LocalData.IsEnglish);
                  return list;

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return null;
            }
            finally
            {
                //ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();
            }

        }
        #endregion

        #region 获得控件的查询数据
        /// <summary>
        /// 获得公司列表
        /// </summary>
        public Guid[] companyIds
        {
            get
            {
                string text = cmbCompany.SelectValues;
                return Utility.GetIds(text);

            }
        }
        /// <summary>
        /// 获得航线列表
        /// </summary>
        public Guid[] shipLineIds
        {
            get
            {
                string text = cmbShipLine.GetAllEditValue.ToSplitString(";");
                return Utility.GetIds(text);

            }
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public string customerIds
        {
            get
            {
                if (stxtCustomer.Tag == null)
                {
                    return string.Empty;
                }
                return stxtCustomer.Tag.TagToSplitString(GlobalConstants.DividedSymbol);
            }
        }

        /// <summary>
        /// 船东ID
        /// </summary>
        public string carrierIDs
        {
            get
            {
                if (stxtCarrier.Tag == null)
                {
                    return string.Empty;
                }
                return stxtCarrier.Tag.TagToSplitString(GlobalConstants.DividedSymbol);
            }
        }

        public Guid salesTypeID
        {
            get
            {
                if (cmbSalesType.EditValue == null)
                {
                    return Guid.Empty;
                }

                Guid id = (Guid)cmbSalesType.EditValue;
                return id;
            }
        }


        #endregion

        #region 分组方式发生改变时
        public event SelectedHandler GroupByChange;

        #endregion

        private void chkCompany_CheckedChanged(object sender, EventArgs e)
        {
            SetGroupBy();
        }

        private void chkShipLine_CheckedChanged(object sender, EventArgs e)
        {
            SetGroupBy();
        }
        private void chkCarrier_CheckedChanged(object sender, EventArgs e)
        {
            SetGroupBy();
        }
        private void SetGroupBy()
        {
            if (!isLoad)
            {
                return;
            }
            List<BPGroupBy> groupByList = new List<BPGroupBy>();
            if (chkVoyageName.Checked)
            {
                groupByList.Add(BPGroupBy.VoyageName);
            }
            if (chkCompany.Checked)
            {
                groupByList.Add(BPGroupBy.Company);
            }
            if (chkShipLine.Checked)
            {
                groupByList.Add(BPGroupBy.ShipLine);
            }
            if (chkCarrier.Checked)
            {
                groupByList.Add(BPGroupBy.Carrier);
            }

            if (GroupByChange != null)
            {
                GroupByChange(this, groupByList);
            }
            SaveGroupBy(groupByList);
        }


        private void SaveGroupBy(List<BPGroupBy> groupByList)
        {
            string str = string.Empty;
            foreach (BPGroupBy item in groupByList)
            {
                str += item.ToString()+",";
            }
            ClientConfig.Current.AddValue("FRMBookingReportGroupBy", str);
        }

        private void GetGroupBy()
        { 
            if( ClientConfig.Current.Contains("FRMBookingReportGroupBy"))
            {
                string list = ClientConfig.Current.GetValue("FRMBookingReportGroupBy");

                if (list.Contains("VoyageName"))
                {
                    chkVoyageName.Checked = true;
                }
                if (list.Contains("Company"))
                {
                    chkCompany.Checked = true;
                }
                if (list.Contains("ShipLine"))
                {
                    chkShipLine.Checked = true;
                }
                if (list.Contains("Carrier"))
                {
                    chkCarrier.Checked = true;
                }

            }


        }

    }

}
