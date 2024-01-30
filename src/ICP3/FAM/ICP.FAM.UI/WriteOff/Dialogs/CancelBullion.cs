using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.FAM.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.UI.WriteOff
{
    public partial class CancelBullion : XtraUserControl
    {
        public CancelBullion()
        {
            InitializeComponent();
            Disposed += delegate {
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            
            };
        }

        #region 服务

        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }


        #endregion

        #region 属性
        /// <summary>
        /// 销账信息
        /// </summary>
        public WriteOffItemList ItemList
        {
            get;
            set;
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
              FindForm().DialogResult = DialogResult.Cancel;
              FindForm().Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string remark = LocalData.UserInfo.LoginName + DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).ToString() + " 取消到账";
            if (!string.IsNullOrEmpty(txtRemark.Text))
            {
                remark += Environment.NewLine + " Remark: " + txtRemark.Text;
            }

            try
            {
                ManyResult result = FinanceService.CancelReached(
                     new Guid[] { ItemList.ID },
                     remark,
                     LocalData.UserInfo.LoginID,
                     new DateTime?[] { ItemList.UpdateDate });

                ItemList.UpdateDate = result.Items[0].GetValue<DateTime?>("UpdateDate");
                ItemList.BankByName = string.Empty;
                ItemList.ReachedDate = null;


                FindForm().DialogResult = DialogResult.OK;
                FindForm().Close();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }


        }

    }
}
