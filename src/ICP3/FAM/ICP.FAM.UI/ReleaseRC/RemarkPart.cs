using System;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.UI
{
    public partial class RemarkPart : BaseEditPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        #endregion

        public RemarkPart()
        {
            InitializeComponent();
            Disposed += delegate {
                Saved = null;
                bsList.DataSource = null;
                bsList.Dispose();
                if (Workitem != null) 
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }
        #region IEditPart 成员

        public override object DataSource
        {
            get { return bsList.DataSource; }
            set { bsList.DataSource = value; }
        }

        public override event SavedHandler Saved;

        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ReleaseRCList list = bsList.DataSource as ReleaseRCList;

                SingleResult result = FinanceService.SaveReleaseRemark(list.ID, list.RcRemark
                                                                     , list.UpdateDate, LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                list.ID = result.GetValue<Guid>("ID");
                list.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                if (Saved != null) Saved(new object[] { bsList.DataSource });
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Update Successfully" : "修改成功");
                FindForm().Close();
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }
    }
}
