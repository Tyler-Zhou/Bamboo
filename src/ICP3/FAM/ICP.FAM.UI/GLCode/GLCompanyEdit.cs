using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.FAM.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.FAM.UI.GLCode
{
    public partial class GLCompanyEdit : BaseEditPart
    {
        public Guid myGLID;
        public string Code;
        List<GL2COMPANY> myCom;

        public GLCompanyEdit()
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
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        #endregion

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls(); 
        }

        private void InitControls()
        {
            List<LocalOrganizationInfo> list = FAMUtility.GetCompanyList();

            listCompany.DataSource = list;
            listCompany.DisplayMember = LocalData.IsEnglish ? "EShortName" : "CShortName";
            listCompany.ValueMember = "ID";



            myCom = ConfigureService.GetGLOrgbyId(myGLID);

            for (int i = 0; i < listCompany.Items.Count; i++)
            {
                LocalOrganizationInfo org = listCompany.Items[i] as LocalOrganizationInfo;

                if (myCom.Exists(r=>r.CompanyID == org.ID))
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
            List<GL2COMPANY> addList = new List<GL2COMPANY>();
            List<GL2COMPANY> delList = new List<GL2COMPANY>();

            for (int i = 0; i < listCompany.Items.Count; i++)
            {
                LocalOrganizationInfo org = listCompany.Items[i] as LocalOrganizationInfo;
                if (myCom.Exists(r => r.CompanyID == org.ID) && !listCompany.GetItemChecked(i))
                {
                    delList.Add(myCom.Find(r => r.CompanyID == org.ID));
                }
                if (!myCom.Exists(r => r.CompanyID == org.ID) && listCompany.GetItemChecked(i))
                {
                    addList.Add(new GL2COMPANY() { CompanyID = org.ID, GLID = myGLID, ID = Guid.NewGuid(), Code = Code });
                }
            }

            try
            {
                if (addList.Count > 0)
                {
                    List<Guid> idList = new List<Guid>();
                    List<Guid> comList = new List<Guid>();
                    List<string> codeList = new List<string>();
                    foreach (GL2COMPANY gl2com in addList)
                    {
                        idList.Add(gl2com.ID);
                        comList.Add(gl2com.CompanyID);
                        codeList.Add(gl2com.Code);
                    }
                    ConfigureService.SaveGL2Company(idList.ToArray(), myGLID,comList.ToArray(),codeList.ToArray(),LocalData.UserInfo.LoginID);
                }

                if (delList.Count > 0)
                {
                    List<Guid> idList = new List<Guid>();
                    foreach (GL2COMPANY gl2com in delList)
                    {
                        idList.Add(gl2com.ID);
                    }
                    ConfigureService.DelGL2Company(idList.ToArray(), LocalData.UserInfo.LoginID);
                }
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
