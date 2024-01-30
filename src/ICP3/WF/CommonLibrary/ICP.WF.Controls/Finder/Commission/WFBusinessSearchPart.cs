using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Common;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.WF.ServiceInterface;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary;

namespace ICP.WF.Controls.Form.Commission
{
    [ToolboxItem(false)]
    public partial class WFBusinessSearchPart : BaseSearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IWorkFlowExtendService WorkFlowExtendService 
        {
            get 
            {
                return ServiceClient.GetService<IWorkFlowExtendService>();
            }
        }

        #endregion

        #region 初始化

        public WFBusinessSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.OnSearched = null;
                this.searchParameter = null;
                this.RemoveKeyDownHandle();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {

                SetCmbSource();
                SetKeyDownToSearch();

            }
        }
        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        private void RemoveKeyDownHandle()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                item.KeyDown -= item_KeyDown;
            }
        }
        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2) this.btnSearch.PerformClick();
            else if (e.KeyCode == Keys.F3) this.btnClear.PerformClick();
        }

        private void SetCmbSource()
        {

            #region Company
          
            List<OrganizationList> officeList=WorkFlowExtendService.GetWFAllOffice();
            chkcmbCompany.Properties.BeginUpdate();
            chkcmbCompany.Properties.Items.Clear();
            
            foreach (var item in officeList)
            {
                chkcmbCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                   CheckState.Checked, true);
            }
            chkcmbCompany.Properties.EndUpdate();
            #endregion

        }

        #endregion

        #region 属性


        public List<Guid> CompanyIDs
        {
            get
            {
                List<Guid> companyIDs = new List<Guid>();
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in chkcmbCompany.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        companyIDs.Add(new Guid(item.Value.ToString()));
                }
                return companyIDs;
            }
        }


        #endregion

        #region ISearchPart 成员

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        public override void RaiseSearched(object data)
        {
            DataPageInfo dataPageInfo = data as DataPageInfo;
            searchParameter.DataPageInfo = dataPageInfo;
            if (OnSearched != null)
                OnSearched(this, GetData());
        }

        public override void Init(IDictionary<string, object> values)
        {
            string nameList = string.Empty;
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "CustomerName")
                {
                    if (item.Value != null && !string.IsNullOrEmpty(item.Value.ToString()))
                    {
                        this.txtCustomer.Text = item.Value.ToString();
                    }
                }
                if (item.Key == "CompanyID")
                {
                    if (item.Value != null && item.Value != DBNull.Value && DataTypeHelper.IsGuid(item.Value) && DataTypeHelper.GetGuid(item.Value) != Guid.Empty)
                    {
                        foreach (CheckedListBoxItem checkBoxItem in this.chkcmbCompany.Properties.Items)
                        {
                            if (checkBoxItem.Value.ToString() == item.Value.ToString())
                            {
                                checkBoxItem.CheckState = CheckState.Checked;
                            }
                            else
                            {
                                checkBoxItem.CheckState = CheckState.Unchecked;
                            }
                        }
                    }
                }
                if (item.Key == "SelectNos")
                {
                    if (item.Value != null && !string.IsNullOrEmpty(item.Value.ToString()))
                    {
                        nameList = item.Value.ToString();
                    }
                }

            }
            if (!string.IsNullOrEmpty(nameList))
            {
                if (nameList.IndexOf(",") >= 0)
                {
                    string[] names = nameList.Split(',');
                    if (names.Length > 1)
                    {
                        this.txtOperationNo.Text = names[names.Length - 1];
                    }
                }
                else
                {
                    this.txtOperationNo.Text = nameList;
                }
              
            }

            //if (!string.IsNullOrEmpty(this.txtOperationNo.Text)||
            //    !string.IsNullOrEmpty(this.txtCustomer.Text))
            //{
            //    btnSearch.PerformClick();
            //}
        }

        public override void InitialValues(string searchValue, string property, SearchConditionCollection conditions, FinderTriggerType triggerType)
        {
            base.InitialValues(searchValue, property, conditions, triggerType);
        }

        public override object GetData()
        {
            PageList list = WorkFlowExtendService.GetCommissionBusinessList(LocalData.UserInfo.LoginID,
                                                                searchParameter.companyIDs, 
                                                                searchParameter.operationNo, 
                                                                searchParameter.blNo, 
                                                                searchParameter.ctnNo, 
                                                                searchParameter.customer, 
                                                                searchParameter.isApply, 
                                                                searchParameter.DataPageInfo,
                                                                LocalData.IsEnglish);
            return list;
        }

        #endregion

        #region btn
        /// <summary>
        /// 缓存的查询参数
        /// </summary>
        BusinessSearchParameter searchParameter = new BusinessSearchParameter();
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                searchParameter = new BusinessSearchParameter();

                searchParameter.companyIDs = CompanyIDs.ToArray();
                searchParameter.operationNo = txtOperationNo.Text.Trim();
                searchParameter.blNo = txtBLNo.Text.Trim();
                searchParameter.ctnNo = txtCtnNo.Text.Trim();
                searchParameter.customer = txtCustomer.Text.Trim();
                searchParameter.DataPageInfo.PageSize = int.Parse(numpageCount.Value.ToString());
                searchParameter.DataPageInfo.CurrentPage = 1;
                searchParameter.isApply = this.ckbisApply.Checked;

                if (string.IsNullOrEmpty(searchParameter.DataPageInfo.SortByName))
                {
                    searchParameter.DataPageInfo.SortByName = "OperationNO";
                    searchParameter.DataPageInfo.SortOrderType = SortOrderType.Desc;
                }

                if (OnSearched != null)
                {
                    PageList list = GetData() as PageList;
                    if (list != null && list.DataPageInfo != null)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "total search " + list.DataPageInfo.TotalCount.ToString() + " data." : "总共查询到 "
                                                    + list.DataPageInfo.TotalCount.ToString() + " 条数据.");
                    }
                    OnSearched(this, list);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is LWImageComboBoxEdit)
                {
                    (item as LWImageComboBoxEdit).SelectedIndex = 0;
                }
                else if (item is DevExpress.XtraEditors.TextEdit
                         && (item is DevExpress.XtraEditors.SpinEdit) == false
                         && item.Enabled == true
                         && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }
        }

        #endregion
    }

    /// <summary>
    /// 查询数据
    /// </summary>
    [Serializable]
    public class BusinessSearchParameter
    {
        public BusinessSearchParameter() { DataPageInfo = new DataPageInfo(); }

       
        public Guid[] companyIDs { get; set; }
        public string operationNo { get; set; }
        public string blNo { get; set; }
        public string ctnNo { get; set; }
        public string customer { get; set; }
        public string sales { get; set; }
        public bool? isApply { get; set; }
        public DataPageInfo DataPageInfo { get; set; }
    }

}
