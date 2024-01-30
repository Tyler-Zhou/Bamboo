using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Controls;
using ICP.WF.ServiceInterface;

namespace ICP.WF.Controls.Form.CustomerExpense
{
    [ToolboxItem(false)]
    public partial class WFCustomerExpenseSearchPart : BaseSearchPart
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

        public WFCustomerExpenseSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.RemoveKeyDownHandle();
                this.OnSearched = null;
                this.searchParameter = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                SetKeyDownToSearch();
            }
        }
        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                item.KeyDown +=item_KeyDown;
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


        #endregion


        #region ISearchPart 成员

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "CustomerName")
                {
                    if (item.Value != null && !string.IsNullOrEmpty(item.Value.ToString()))
                    {
                        if (LocalData.IsEnglish)
                        {
                            this.txtEName.Text = item.Value.ToString();
                        }
                        else
                        {
                            this.txtCName.Text = item.Value.ToString();
                        }
                    }
                }
            }
        }



        public override object GetData()
        {
            List<WFCECRMCustomerList> list = WorkFlowExtendService.GetWFCRMCustomerList(LocalData.UserInfo.LoginID,
                                                                searchParameter.Code, 
                                                                searchParameter.KeyWord, 
                                                                searchParameter.CName, 
                                                                searchParameter.EName, 
                                                                searchParameter.Contact, 
                                                                searchParameter.EMail, 
                                                                searchParameter.Country,
                                                                searchParameter.MaxRow,
                                                                LocalData.IsEnglish);
            return list;
        }

        #endregion

        #region btn
        /// <summary>
        /// 缓存的查询参数
        /// </summary>
        CustomerExpenseSearchParameter searchParameter = new CustomerExpenseSearchParameter();
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                searchParameter = new  CustomerExpenseSearchParameter();

                searchParameter.CName = this.txtCName.Text;
                searchParameter.Code = this.txtCode.Text;
                searchParameter.Contact = this.txtContact.Text;
                searchParameter.Country = this.txtCountry.Text;
                searchParameter.EMail = this.txtEMail.Text;
                searchParameter.EName = this.txtEName.Text;
                searchParameter.KeyWord = this.txtKeyWord.Text;
                searchParameter.MaxRow = Convert.ToInt32(this.numMaxCount.Value.ToString());

          
                if (OnSearched != null)
                {
                    List<WFCECRMCustomerList> list = GetData() as List<WFCECRMCustomerList>;
                    if (list != null )
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? 
                                                    "total search " + list.Count.ToString()+ " data." : 
                                                    "总共查询到 " + list.Count.ToString() + " 条数据.");
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
    public class CustomerExpenseSearchParameter
    {
        public string Code{get;set;}
        public string KeyWord{get;set;}
        public string CName{get;set;}
        public string EName{get;set;}
        public string Contact{get;set;}
        public string EMail{get;set;}
        public string Country{get;set;}
        public Int32 MaxRow{get;set;}

    }

}
