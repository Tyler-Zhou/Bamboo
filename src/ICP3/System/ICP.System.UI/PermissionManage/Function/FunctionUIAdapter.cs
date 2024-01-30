using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using System.ComponentModel;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.Sys.UI.PermissionManage.Function
{
    /// <summary>
    /// 
    /// </summary>
    public class FunctionUIAdapter : IPartBridge, IDisposable
    {

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IPermissionService PermissionService
        {
            get
            {
                return ServiceClient.GetService<IPermissionService>();
            }
        }
        
        IToolBar _toolBar;
        IListPart _mainListPart;
        IListEidtPart _userOrganizationListEditPart;
        IListEidtPart _roleListEditPart;

        public void Init(ILayoutBuilderContext context, string[] partNames)
        {
            _toolBar = (IToolBar)context.Controls[typeof(Function.FunctionToolBar).Name];
            _mainListPart = (IListPart)context.Controls[typeof(Function.FunctionMainList).Name];
            _userOrganizationListEditPart = (IListEidtPart)context.Controls[typeof(Function.FunctionUserOrgListPart).Name];
            _roleListEditPart = (IListEidtPart)context.Controls[typeof(Function.Function2RoleListPart).Name];

            #region Connection

            #region _mainListPart.CurrentChanging
            _mainListPart.CurrentChanging += delegate(object sender, CancelEventArgs e)
            {
                UIConnectionHelper.ParentChangingForListPart<UserPermissionList>( _userOrganizationListEditPart.SaveData
                                                                                    , (_userOrganizationListEditPart.DataSource as List<UserPermissionList>)
                                                                                    , e
                                                                                    , LocalData.IsEnglish ? "User Permission" : "用户权限 ");

                UIConnectionHelper.ParentChangingForBaseListPart<RolePermissionList>(_roleListEditPart.SaveData
                                                                                , (_roleListEditPart.DataSource as BaseList<RolePermissionList>)
                                                                                , e
                                                                                , LocalData.IsEnglish ? "Role Permission" : "角色权限 ");
            };
            #endregion

            #region _mainListPart.CurrentChanged

            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                FunctionList listData = data as FunctionList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

                #region User2OrganizationJob

                List<UserPermissionList> userPermissionList = null;
                if (listData != null)
                {
                    userPermissionList = PermissionService.GetUserPermissionList(listData.ID);
                }
                _userOrganizationListEditPart.Init(keyValue);
                _userOrganizationListEditPart.DataSource = userPermissionList;

                #endregion

                #region RolePermission

                List<RolePermissionList> rolePermissionLists = new List<RolePermissionList>();
                if (listData != null)
                {
                    rolePermissionLists = PermissionService.GetRolePermissionList(listData.ID);
                }

                _roleListEditPart.Init(keyValue);
                _roleListEditPart.DataSource = rolePermissionLists;

                #endregion
            };            
            #endregion

            List<FunctionList> list = PermissionService.GetFunctionList(null);
            _mainListPart.DataSource = list;


            #endregion
        }


        public void Register<T>(T part, string name)
        {
        }

        #region IDisposable 成员

        public void Dispose()
        {
            this._mainListPart = null;
            this._roleListEditPart = null;
            this._toolBar = null;
            this._userOrganizationListEditPart = null;
            if (this.Workitem != null)
            {
                this.Workitem.Items.Remove(this);
                this.Workitem = null;
            }
        }

        #endregion
    }
}
