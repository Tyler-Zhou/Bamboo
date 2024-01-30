using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;

namespace ICP.Sys.UI.WorkSpaceView
{
    public partial class WorkSpaceViewRolePart : BaseListPart
    {
        public WorkSpaceViewRolePart()
        {
            InitializeComponent();
        }


        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IPermissionService PermissionService
        {
            get
            {
                return ServiceClient.GetService<IPermissionService>();
            }
        }
        public IRoleService RoleService
        {
            get
            {
                return ServiceClient.GetService<IRoleService>();
            }
        }
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        #endregion

        #region 属性
        public Guid WorkSpaceID
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
            Dictionary<string, object> dic = new Dictionary<string, object>();
            List<RoleList> roles = RoleService.GetRoleList(string.Empty, true, 0);
            dic.Add("OriginalList", roles);
            roleSelectPart1.Init(dic);
        }

        public void BindData()
        {
            List<Guid> list = PermissionService.GetWorkSpaceRoleList(WorkSpaceID);
            roleSelectPart1.DataSource = list;
        }
        #endregion

        #region  保存
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<Guid> list = roleSelectPart1.DataSource as List<Guid>;
            if (list == null)
            {
                return;
            }
            try
            {
                PermissionService.SaveWorkSpacewRoleList(WorkSpaceID, list.ToArray(), LocalData.UserInfo.LoginID);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(),"保存成功!");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(),ex);
            }

        }
        #endregion

    }
}
