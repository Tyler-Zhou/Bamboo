using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.FAM.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI
{
    public partial class SetBankAndCompany : BaseEditPart
    {
        public SetBankAndCompany()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            
            };
        }

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
        #endregion

        #region 属性
        /// <summary>
        /// 银行ID
        /// </summary>
        public Guid BankID
        {
            get;
            set;
        }

        #endregion

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }

        private void InitControls()
        {
            List<LocalOrganizationInfo> list = FAMUtility.GetCompanyList();

            listCompany.DataSource = list;
            listCompany.DisplayMember = LocalData.IsEnglish ? "EShortName" : "CShortName";
            listCompany.ValueMember = "ID";

            if (BankID == null || BankID == Guid.Empty)
            {
                return ;
            }
            List<Guid> idList = FinanceService.GetBankAndCompany(BankID);

            if (idList == null || idList.Count == 0)
            {
                return;
            }

            for (int i = 0; i < listCompany.Items.Count; i++)
            {
                LocalOrganizationInfo org = listCompany.Items[i] as LocalOrganizationInfo;

                if (idList.Contains(org.ID))
                {
                    listCompany.SetItemChecked(i, true);
                }
            }
        }
        #endregion


        #region 按钮
        private void btnClose_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listCompany.Items.Count; i++)
            {
                listCompany.SetItemChecked(i, true);
            }
        }

        private void btnClare_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listCompany.Items.Count; i++)
            {
                listCompany.SetItemChecked(i, false);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<Guid> selectIdList = new List<Guid>();
            for (int i = 0; i < listCompany.Items.Count; i++)
            {
                if (listCompany.CheckedItems.Contains(listCompany.Items[i]))
                {
                    LocalOrganizationInfo org = listCompany.Items[i] as LocalOrganizationInfo;
                    selectIdList.Add(org.ID);
                }
            }

            if (BankID==null||BankID == Guid.Empty)
            {
                return;
            }
            try
            {
                FinanceService.SaveBankAndCompany(BankID, selectIdList.ToArray(), LocalData.IsEnglish,LocalData.UserInfo.LoginID);
                FindForm().DialogResult = DialogResult.OK;
            }
            catch (Exception ex) 
            { 
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); 
            }
           
        }
        #endregion


    }
}
