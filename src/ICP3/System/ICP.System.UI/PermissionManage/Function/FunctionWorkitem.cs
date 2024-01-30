using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Sys.UI.PermissionManage.Function
{
    public class FunctionWorkitem : WorkItem
    {
        #region Service

        public IUIBuilder UIBuilder
        {
            get
            {
                return ServiceClient.GetClientService<IUIBuilder>();
            }
        }
        #endregion

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }
        private UILayout uilayout;
        private void Show()
        {
            //child layout
            ICP.Framework.ClientComponents.UIFramework.DockPanelTabLayout childLayout = new ICP.Framework.ClientComponents.UIFramework.DockPanelTabLayout
            {
                Childs = new List<ICP.Framework.ClientComponents.UIFramework.BaseLayout>
                {
                     new ICP.Framework.ClientComponents.UIFramework.SimpleControlLayout
                     {
                          ControlType=typeof(Function.Function2RoleListPart),
                           Properties=new ICP.Framework.ClientComponents.UIFramework.ControlLayoutProperty
                           { Dock= DockStyle.Fill, Name=typeof(Function.Function2RoleListPart).Name, Text=LocalData.IsEnglish? "Role":"角色"}
                     },
                     new ICP.Framework.ClientComponents.UIFramework.SimpleControlLayout
                     {
                          ControlType=typeof(Function.FunctionUserOrgListPart),
                           Properties=new ICP.Framework.ClientComponents.UIFramework.ControlLayoutProperty
                           { Dock= DockStyle.Fill, Name=typeof(Function.FunctionUserOrgListPart).Name, Text=LocalData.IsEnglish? "User":"用户"}
                     }
                },
                Properties = new DockPanelTabLayoutProperty 
                { Dock = DockStyle.Bottom, Text = LocalData.IsEnglish ? "Function" : "权限配置", Name = "function", Height = 300f }
            };

            uilayout = UILayoutHelper.BuilLayout<FunctionToolBar, FunctionMainList, FunctionUIAdapter>(childLayout);

            UIBuilder.Build(this, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace, LocalData.IsEnglish ? "Function" : "权限配置", uilayout);
        }
        /// <summary>
        /// 关闭当前场景(在这里处理释放所有该场景创建的资源)
        /// </summary>
        public void Close()
        {
            if (this.uilayout != null)
            {
                this.uilayout.Dispose();
                this.uilayout = null;
            }
            if (this.Status != WorkItemStatus.Terminated)
            {
                this.Terminate();
            }
        }
    }

    public class FunctionWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolBarWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string OrgJobWorkspace = "OrgJobWorkspace";
    }

    public class FunctionCommonConstants
    {
        public const string Command_Refresh = "Command_Refresh";
    }
}
