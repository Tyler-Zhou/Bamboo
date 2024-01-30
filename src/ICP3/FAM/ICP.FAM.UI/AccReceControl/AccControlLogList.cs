using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ICP.FAM.UI.AccReceControl
{
    public partial class AccControlLogList : BaseListPart
    {
        #region 服务

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        public List<CustomerAgingLogs> _logList = null;
        #endregion

        public AccControlLogList()
        {
            InitializeComponent();

            Disposed += delegate
            {
                gcMain.DataSource = null;

                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }

            };
        }

        /// <summary>
        /// 绑定明细数据
        /// </summary>
        public void BindDataList(Guid customerID, Guid companyID)
        {
            string result = FinanceService.GetCustomerAgingLogs(customerID);
            _logList = JsonConvert.DeserializeObject<List<CustomerAgingLogs>>(result);
            gcMain.DataSource = _logList;
            gvMain.RefreshData();

            _currentCompanyid = companyID;
            _currentCustomerid = customerID;
        }

        #region IListPart 成员

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                bsList.DataSource = value;
                bsList.ResetBindings(false);
            }
        }


        public override event CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        private Guid _currentCompanyid;
        private Guid _currentCustomerid;
        #endregion

        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            AccControlEdit editpart = WorkItem.Items.AddNew<AccControlEdit>();
            string title = LocalData.IsEnglish ? "Add Log" : "新增日志";
            editpart.SetCustomerAndCompany(_currentCompanyid, _currentCustomerid);
            editpart.SetFormType(1);
            editpart.DataSource = null;
            editpart.ondelete += delegate()
                    {
                        BindDataList(_currentCustomerid, _currentCompanyid);
                    };

            PartLoader.ShowDialog(editpart, title);
        }

        private void barView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvMain.FocusedRowHandle < 0) return;

            AccControlEdit editpart = WorkItem.Items.AddNew<AccControlEdit>();
            string title = LocalData.IsEnglish ? "View Log" : "查看日志";
            editpart.DataSource = _logList[gvMain.FocusedRowHandle];
            editpart.SetFormType(0);

            PartLoader.ShowDialog(editpart, title);
        }

        private void gcMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            barView_ItemClick(null, null);
        }

        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvMain.FocusedRowHandle < 0) return;
            try
            {
                string message = LocalData.IsEnglish ? "Are you sure you want to delete the selected logs?" : "你确定要删除所选择的日志吗?";
                DialogResult result = XtraMessageBox.Show(message, "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    FinanceService.DeleteCustomerAgingLogs(_logList[gvMain.FocusedRowHandle].ID);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
                    gvMain.DeleteRow(gvMain.FocusedRowHandle);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Delete failed" + ex.Message : "删除失败" + ex.Message);
            }
        }
    }
}
