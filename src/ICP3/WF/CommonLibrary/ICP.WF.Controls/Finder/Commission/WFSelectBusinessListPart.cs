using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.WF.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary;

namespace ICP.WF.Controls.Form.Commission
{
    public partial class WFSelectBusinessListPart : BaseListPart
    {
        public WFSelectBusinessListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.Selected = null;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                SetDataSource(value);
            }
        }
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<WFBusinessList> DataList
        {
            get
            {
                List<WFBusinessList> list=bsList.DataSource as List<WFBusinessList>;
                if(list==null)
                {
                    list=new List<WFBusinessList>();
                }
                return list;
            }
        }

        public WFBusinessList CurrentRow
        {
            get
            {
                return bsList.Current as WFBusinessList;
            }
        }

        private void SetDataSource(object value)
        {
           WFBusinessList item = value as  WFBusinessList;
           if (item == null)
           {
               return;
           }

           int n = (from d in DataList where d.CustomerID != item.CustomerID select d).Count();
           if (n > 0)
           {
               DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Customer is not the same" : "不是相同的客户无法选择到一起");
               
               return;
           }
           int c = (from d in DataList where d.CompanyID != item.CompanyID select d).Count();
           if (c > 0)
           {
               DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Company is not the same" : "不是相同的口岸无法选择到一起");

               return;
           }

           int i = (from d in DataList where d.ID == item.ID && d.CommissionCurrencyID==item.CommissionCurrencyID select d).Count();
           if (i == 0)
           {
               DataList.Add(item);
               bsList.ResetBindings(false);
           }
        }

        void barClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string message = LocalData.IsEnglish ? "Sure Clare Data" : "确认清空数据";
            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(message,
                                              LocalData.IsEnglish ? "Tip" : "提示",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                bsList.DataSource = new List<WFBusinessList>();
                bsList.ResetBindings(false);
            }
        }

        void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CurrentRow != null && DataList.Contains(CurrentRow))
            {
                DataList.Remove(CurrentRow);
                bsList.ResetBindings(false);
            }

        }       
        
        public override event SelectedHandler Selected;

        void barOK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DataList == null || DataList.Count == 0)
            {
                string message = LocalData.IsEnglish ? "Please select a business data" : "请选择业务数据";
                DevExpress.XtraEditors.XtraMessageBox.Show(message);
                return;            
            }


            Workitem.Commands[CommissionOperactionFinderConstants.Command_Commission_FinderConfirm].Execute();
        }
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;

            List<WFBusinessList> list = new List<WFBusinessList>();
            string idList = string.Empty;
            string nameList = string.Empty;
            Guid customerID = Guid.Empty, companyID=Guid.Empty;
            string customerName = string.Empty;

            foreach (var item in values)
            {
                if (item.Key == "CustomerID")
                {
                    if (item.Value != null && !string.IsNullOrEmpty(item.Value.ToString()))
                    {
                        customerID = new Guid(item.Value.ToString());
                    }
                }
                if (item.Key == "CompanyID")
                {
                    if (item.Value != null && !string.IsNullOrEmpty(item.Value.ToString()))
                    {
                        companyID = DataTypeHelper.GetGuid(item.Value);
                    }
                }
                if (item.Key == "CustomerName")
                {
                    if (item.Value != null && !string.IsNullOrEmpty(item.Value.ToString()))
                    {
                        customerName = item.Value.ToString();
                    }
                }
                if (item.Key == "SelectIDs")
                {
                    if (item.Value != null && !string.IsNullOrEmpty(item.Value.ToString()))
                    {
                        idList = item.Value.ToString();
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

            string[] ids = idList.Split(',');
            string[] names = nameList.Split(',');
            int i=0;
            foreach (string str in ids)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    WFBusinessList item = new WFBusinessList();
                    item.ID = new Guid(str);
                    if (names.Length > i)
                    {
                        item.OperationNO = names[i];
                    }
                    if (string.IsNullOrEmpty(item.OperationNO))
                    {
                        continue;
                    }
                    item.CustomerID = customerID;
                    item.CustomerName = customerName;
                    item.CompanyID = companyID;

                    list.Add(item);
                }
                i++;
            }



            bsList.DataSource = list;
            bsList.ResetBindings(false);

        }

    }
}
