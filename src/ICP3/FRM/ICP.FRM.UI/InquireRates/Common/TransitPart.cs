using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FRM.UI.InquireRates
{
    [ToolboxItem(false)]
    public partial class TransitPart : BasePart
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        IInquireRatesService InquireRatesService
        {
            get {
                return ServiceClient.GetService<IInquireRatesService>();
            }
        }
      
        public TransitPart()
        {
            InitializeComponent();
            Disposed += delegate {
                CurrentRate = null;
                
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (!DesignMode) { InitMessage(); }
        }
        private void InitMessage()
        {
            RegisterMessage("AccountToolTip", "You can use semicolons for dividing multi-Account, e.g. IBM; Google");
        }

        protected override void OnLoad(EventArgs e)
        {
            InitControls();
            base.OnLoad(e);
        }

        public bool IsChanged = false;

        public ClientBaseInquireRate CurrentRate = null;

        public void SetSouce(ClientBaseInquireRate data)
        {
            //CurruntUnitList = Utility.Clone<List<InquireUnit>>(units);
            //this.bsRateUnit.DataSource = CurruntUnitList;

            CurrentRate = data;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Guid newRspBy = new Guid(cmbRespondBy.EditValue.ToString());
            if (newRspBy == CurrentRate.RespondByID)
            {
                XtraMessageBox.Show("An invalid transition, you have specified a duplicated person.", "Tip");
                return;
            }

            InquireRatesService.TransitRespondMan(CurrentRate.ID,
                CurrentRate.InquireByID,
                CurrentRate.RespondByID,
                newRspBy,
                txtDiscussing.Text.Trim(),
                DateTimeOffset.Now,
                LocalData.UserInfo.LoginID);

            CurrentRate.RespondByID = newRspBy;
            CurrentRate.RespondByName = cmbRespondBy.EditText;
            FindForm().DialogResult = DialogResult.OK;
            FindForm().Close();
        }    

        private void btnClose_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }

        private void InitControls()
        {
            txtCurrentRespondBy.Text = CurrentRate.RespondByName;
            cmbRespondBy.EditValue = CurrentRate.RespondByID;
            cmbRespondBy.EditText = CurrentRate.RespondByName;

            Utility.SetEnterToExecuteOnec(cmbRespondBy, delegate
          {
              //Dictionary<Guid, UserList> userSource = new Dictionary<Guid, UserList>();
              //List<OrganizationList> userCompanyList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);

              //foreach (var item in userCompanyList)
              //{
              //    List<UserList> userList = UserService.GetUserListBySearch(item.ID, "商务", string.Empty, true, true, 0);
              //    foreach (var user in userList)
              //    {
              //        if (!userSource.Keys.Contains(user.ID))
              //        {
              //            userSource.Add(user.ID, user);
              //        }
              //    }
              //}
             
              //if (userSource.Values != null)
              //{
              //    Dictionary<string, string> col = new Dictionary<string, string>();
              //    col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
              //    col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
              //    cmbRespondBy.InitSource<UserList>(userSource.Values.OrderBy(i => i.EName).ToList(), col, LocalData.IsEnglish ? "EName" : "CName", "ID");
              //}

              List<UserList> userList = UserService.GetUnderlingUserList(new Guid[0], new string[] { "商务员", "远东区商务员" }, new string[0], true).OrderBy(i => (LocalData.IsEnglish ? i.EName : i.CName)).ToList();
              Dictionary<string, string> col = new Dictionary<string, string>();
              col.Add(LocalData.IsEnglish ? "EName" : "CName", "Name");
              col.Add("Code", "Code");
              cmbRespondBy.InitSource<UserList>(userList, col, (LocalData.IsEnglish ? "EName" : "CName"), "ID");
          });
        }
    }
}
