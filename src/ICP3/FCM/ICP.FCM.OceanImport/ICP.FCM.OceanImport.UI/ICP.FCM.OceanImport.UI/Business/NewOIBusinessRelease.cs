using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.OceanImport.UI
{
    [ToolboxItem(false)]
    public partial class NewOIBusinessRelease : BaseEditPart
    {

        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        public override object DataSource
        {
            set { BindingData(value); }
        }

        private void BindingData(object value)
        {
            if (value == null)
            {
                return;
            }
            listOceanBusinessLists = value as List<OceanBusinessList>;

        }
        public List<OceanBusinessList> listOceanBusinessLists = new List<OceanBusinessList>();

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;
        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }

        public NewOIBusinessRelease()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                this.Load += (sender, e) =>
                {
                    if (LocalData.IsEnglish) SetCnText();
                    this.dtpReleaseDate.EditValue = DateTime.Now;
                };
            }
        }


        private void SetCnText()
        {
            labOrderDate.Text = "ReleaseDate";
            btnOK.Text = "OK";
            btnClose.Text = "Cancel";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.dtpReleaseDate.EditValue == null)
            {
                string message = LocalData.IsEnglish ? "Please Select ReleaseDate" : "请选择放货日期";
                dtpReleaseDate.Focus();
                MessageBox.Show(message);
            }
            else
            {
                string nostr = string.Empty;
                foreach (var item in listOceanBusinessLists)
                {
                    SingleResult result = OceanImportService.SetOIReleaseData(item.ID, this.dtpReleaseDate.DateTime, item.UpdateDate, LocalData.UserInfo.LoginID);
                    if (result != null)
                    {
                        if (string.IsNullOrEmpty(result.GetValue<string>("ErrorMessage")))
                        {
                            if (item.State != OIOrderState.Release)
                            {
                                item.State = OIOrderState.Release;
                                item.IsTelex = item.ReleaseType == FCMReleaseType.Telex ? true : false;
                                item.ReleaseType = item.ReleaseType;
                                item.ReleaseDate = item.ReleaseDate;
                            }
                            else
                            {
                                item.State = OIOrderState.Checked;
                                item.ReleaseDate = null;
                            }

                            OceanImportService.ChangeOITrackingInfo(item.ID, item.IsReceiveNotice, item.IsNoticeRelease,
                                item.IsApplyRC, !item.IsReleaseCargo, item.IsNoticePay, item.IsAgreeRC,
                                LocalData.UserInfo.LoginID);
                        }
                        else
                        {
                            if (nostr == string.Empty)
                            {
                                nostr = item.No + " " + result.GetValue<string>("ErrorMessage");
                            }
                            else
                            {
                                nostr = nostr + "|" + item.No + " " + result.GetValue<string>("ErrorMessage");
                            }
                        }
                    }
                }
                //当有单号发生的时候 提示错误信息，如无单号不提示信息
                if (!string.IsNullOrEmpty(nostr))
                {
                    string str = LocalData.IsEnglish ? "No:" : "业务号:";
                    str = str + " " + nostr;
                    string endstr = LocalData.IsEnglish ? " No RC Conditions" : " 不满足放货条件.";
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), str + endstr);
                }
                this.FindForm().DialogResult = DialogResult.OK;
                this.FindForm().Close();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }
}
