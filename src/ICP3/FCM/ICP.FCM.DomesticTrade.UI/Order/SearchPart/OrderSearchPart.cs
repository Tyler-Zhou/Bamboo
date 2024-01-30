using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.FCM.DomesticTrade.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;

using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.FCM.Common.ServiceInterface.Common;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FCM.DomesticTrade.UI.Order
{
    [ToolboxItem(false)]
    [SmartPart]
    public partial class OrderSearchPart : BaseSearchPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IDomesticTradeService DomesticTradeService
        {
            get
            {
                return ServiceClient.GetService<IDomesticTradeService>();
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

        #region init 

        public OrderSearchPart()
        {
            InitializeComponent();
            if(LocalData.IsEnglish==false)SetCnText();
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
        private void SetCnText()
        {
            labCarrier.Text = "船公司";
            btnClear.Text = "清空(&L)";
            btnSearch.Text = "查询(&S)";
            labSalesDepartment.Text = "揽货部门";
            labCustomer.Text = "客户";
            labDestination.Text = "交货地";
            labFrom.Text = "从";
            labTo.Text = "到";
            fromToDateMonthControl1.IsEngish = false;

            labMax.Text = "最大行数";
            labOperationNo .Text = "业务号";
            labPOD.Text = "卸货港";
            labPOL.Text = "装货港";
            labSales.Text = "揽货人";
            labState.Text = "状态";

            labIsValid.Text = "有效性";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.NULLText = "全部";
            lwchkIsValid.UnCheckedText = "无效";

            treeBoxSalesDep.AllText = "全选";
            nbarBase.Caption = "基本信息";
            nbarDate.Caption = "日期";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();

            SetKeyDownToSearch();
        }
        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                item.KeyDown += new KeyEventHandler(item_KeyDown);
            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2) btnSearch.PerformClick();
            else if (e.KeyCode == Keys.F3) btnClear.PerformClick();
        }

        private void InitControls()
        {
            List<EnumHelper.ListItem<DTOrderState>> orderStates = EnumHelper.GetEnumValues<DTOrderState>(LocalData.IsEnglish);
            cmbState.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish?"ALL":"全部" , DBNull.Value));
            foreach (var item in orderStates)
            {
                if (item.Value == DTOrderState.Unknown) continue;
                cmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbState.SelectedIndex = 0;



            //船公司
            Utility.SetEnterToExecuteOnec(mcmbCarrier, delegate
            {
                ICPCommUIHelper.BindCustomerList(mcmbCarrier,CustomerType.Carrier);
            });

            //揽货人
            Utility.SetEnterToExecuteOnec(mcmbSales, delegate
            {
                ICPCommUIHelper.SetMcmbUsersByCompanys(mcmbSales);
            });

            if (LocalCommonServices.PermissionService.HaveActionPermission(FCMPermissionsConstants.FCM_NAServices))
            {
                ICPCommUIHelper.BindDepartmentByAll(treeBoxSalesDep);
            }
            else
            {
                ICPCommUIHelper.BindCompanyByUser(treeBoxSalesDep, CheckState.Checked);
                mcmbSales.ShowSelectedValue(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName);
            }
            mcmbSales.SelectedRow += new EventHandler(mcmbSales_SelectedRow);

            List<EnumHelper.ListItem<DateSearchType>> dateSearchTypes = EnumHelper.GetEnumValues<DateSearchType>(LocalData.IsEnglish);
            foreach (var item in dateSearchTypes)
            {
                if (item.Value == DateSearchType.All) continue;
                cmbDateSearchType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDateSearchType.SelectedIndex = 0;
            cmbDateSearchType.SelectedIndexChanged +=new EventHandler(cmbDateSearchType_SelectedIndexChanged);

            SetControlsEnterToSearch();

        }

        void mcmbSales_SelectedRow(object sender, EventArgs e)
        {
            Workitem.State["SalesId"] = SalesID;
        }

        private void SetControlsEnterToSearch()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is TextEdit
                && (item is SpinEdit) == false)
                {
                    item.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
                    };
                }
            }
        }

        void cmbDateSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDateSearchType.EditValue.ToString() == DateSearchType.All.ToString())
                fromToDateMonthControl1.Enabled = false;
            else
                fromToDateMonthControl1.Enabled = true;
        }

        #endregion

        #region 属性

        public Guid? SalesID
        {
            get
            {
                if (mcmbSales.EditValue == null) return null;
                return new Guid(mcmbSales.EditValue.ToString());
            }
        }

        #endregion

        #region ISearchPart 成员

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }

            txtOperationNo.Text = string.Empty;
            stxtCustomer.Text = string.Empty;
            stxtPOL.Text = string.Empty;
            stxtPOD.Text = string.Empty;
            stxtDestination.Text = string.Empty;

            foreach (var item in values)
            {
                string value = item.Value == null ? string.Empty : item.Value.ToString();
                switch (item.Key)
                {
                    case "RefNo":
                        txtOperationNo.Text = value;
                        break;
                    case "CustomerName":
                        stxtCustomer.Text = value;
                        break;
                    case "POLName":
                        stxtPOL.Text = value;
                        break;
                    case "PODName":
                        stxtPOD.Text = value;
                        break;
                    case "PlaceOfDeliveryName":
                        stxtDestination.Text = value;
                        break;
                }
            }
        }

        public override object GetData()
        {
            DateTime? dtFrom = null, dtTo = null;
            DateSearchType dateSearchType = DateSearchType.All;
            DTOrderState? orderState = null;

            if (cmbState.EditValue != null && cmbState.EditValue != DBNull.Value)
            {
                orderState = (DTOrderState)cmbState.EditValue;
            }

            if (nbarDate.Expanded && cmbDateSearchType.EditValue != null && cmbDateSearchType.EditValue.ToString() != DateSearchType.All.ToString())
            {
                dateSearchType = (DateSearchType)cmbDateSearchType.EditValue;
                dtFrom = fromToDateMonthControl1.From;
                dtTo = fromToDateMonthControl1.To;
            }

            List<Guid> companyIDs = treeBoxSalesDep.EditValue;
            

            //List<OrganizationList> userCompanyList = userService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
            //foreach (var item in userCompanyList)
            //{
            //    companyIDs.Add(item.ID);
            //}

            if (treeBoxSalesDep.EditValue.Count == 0)
            {                
                    companyIDs = treeBoxSalesDep.GetAllAvailableValues();                
            }

            try
            {
                List<DTOrderList> list = DomesticTradeService.GetDTOrderList(companyIDs.ToArray(),
                                                                            txtOperationNo.Text.Trim(),
                                                                            stxtCustomer.Text.Trim(),
                                                                            stxtPOL.Text.Trim(),
                                                                            stxtPOD.Text.Trim(),
                                                                            stxtDestination.Text.Trim(),
                                                                            mcmbCarrier.EditText,
                                                                            lwchkIsValid.Checked,
                                                                            orderState,
                                                                            SalesID,
                                                                            dateSearchType,
                                                                            dtFrom,
                                                                            dtTo,
                                                                            int.Parse(numMax.Value.ToString()));

                return list;
            }
            catch (Exception ex) 
            { 
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); 
                return null; 
            }
        }

        public override event SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

        #region btn

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    OnSearched(this, GetData());
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
                else if (item is MultiSearchCommonBox)
                {
                    MultiSearchCommonBox msc = (MultiSearchCommonBox)item;
                    msc.ShowSelectedValue(null, string.Empty);
                }
                else if (item is TextEdit
                    && (item is SpinEdit) == false
                    && item.Enabled == true
                    && (item as TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
                
            }

            cmbState.SelectedIndex = 0;
            cmbDateSearchType.SelectedIndex = 0;
        }

        #endregion
    }

    public class OrderFinderSearchPart : OrderSearchPart
    { 
        
    }
}
