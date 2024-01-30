using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using System.Windows.Forms;
using System.ComponentModel;
using ICP.Sys.ServiceInterface.DataObjects;
using System;

namespace ICP.Sys.UI.Organization
{
    public class OrganizationWorkitem : WorkItem
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
            ICP.Framework.ClientComponents.UIFramework.DockPanelLayout childLayout = new ICP.Framework.ClientComponents.UIFramework.DockPanelLayout
            {
                Childs = new List<ICP.Framework.ClientComponents.UIFramework.BaseLayout>
                {
                     new ICP.Framework.ClientComponents.UIFramework.SimpleControlLayout
                     {
                        ControlType = typeof(OrganizationEditPart),
                        Properties = new ICP.Framework.ClientComponents.UIFramework.ControlLayoutProperty 
                        { Dock = DockStyle.Fill, Name = typeof(OrganizationEditPart).Name, Text = LocalData.IsEnglish ? "Edit" : "编辑" }
                     }
                },
                Properties = new DockPanelControlLayoutProperty { Dock = DockStyle.Bottom, Text = LocalData.IsEnglish ? "Edit" : "编辑", Name = "organizationEdit", Height = 300f }
            };

           

            uilayout = UILayoutHelper.BuilLayout<OrganizationToolBar, OrganizationSearchPart, OrganizationListPart, OrganizationUIAdapter>(childLayout);

            UIBuilder.Build(this, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace, LocalData.IsEnglish ? "Organization" : "组织结构", uilayout);
        }
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


    public class OrganizationCommonConstants
    {
        public const string Common_AddData = "Common_AddData";
        public const string Common_DisuseData = "Common_DisuseData";
    }


    /// <summary>
    /// 
    /// </summary>
    public class OrganizationUIAdapter : IPartBridge,IDisposable
    {

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }

        IToolBar _toolBar;
        ICP.Framework.ClientComponents.UIFramework.ISearchPart _searchPart;
        IListPart _mainListPart;
        IEditPart _editPart;

        public void Init(ILayoutBuilderContext context, string[] partNames)
        {
            _toolBar = (IToolBar)context.Controls[typeof(OrganizationToolBar).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)context.Controls[typeof(OrganizationSearchPart).Name];
            _mainListPart = (IListPart)context.Controls[typeof(OrganizationListPart).Name];
            _editPart = (IEditPart)context.Controls[typeof(OrganizationEditPart).Name];

            #region Connection

            #region _mainListPart.CurrentChanging
            _mainListPart.CurrentChanging += delegate(object sender, CancelEventArgs e)
            {
                UIConnectionHelper.ParentChangingForEditPart(_mainListPart
                                                              , _editPart.SaveData
                                                              , (_editPart.DataSource as OrganizationInfo)
                                                              , e
                                                              , LocalData.IsEnglish ? "Organization Edit" : "编辑组织结构");
            };
            #endregion

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                OrganizationList listData = data as OrganizationList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

                #region toolBar

                RefreshBarEnabled(_toolBar,listData);

                #endregion

                #region editPart

                OrganizationInfo info = null;
                if (listData != null)
                {
                    if (listData.IsNew)
                    {
                        info=new OrganizationInfo();
                        Utility.CopyToValue(listData, info, typeof(OrganizationInfo));
                    }
                    else
                    {
                        info = OrganizationService.GetOrganizationInfo(((OrganizationList)data).ID);
                    }
                }
                _editPart.DataSource = info;
                #endregion
            };
            #endregion

            #region  _editPart.Saved
            _editPart.Saved += delegate(object[] prams)
            {
                if (_mainListPart.Current == null || prams == null) return;

                OrganizationList organizationlist = prams[0] as OrganizationList;
                OrganizationList currentRow = _mainListPart.Current as OrganizationList;

                Utility.CopyToValue(organizationlist, currentRow, typeof(OrganizationList));
                RefreshBarEnabled(_toolBar, currentRow);
            };
            #endregion

            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion

            #endregion

            _searchPart.RaiseSearched();
        }

        private void RefreshBarEnabled(IToolBar toolBar, OrganizationList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barDisuse", false);
            }
            else
            {
                toolBar.SetEnable("barDisuse", true);
                if (listData.IsValid)
                    toolBar.SetText("barDisuse", LocalData.IsEnglish ? "Disuse(&D)" : "作废(&D)");
                else
                    toolBar.SetText("barDisuse", LocalData.IsEnglish ? "Available(&D)" : "激活(&D)");

            }
        }


        public void Register<T>(T part, string name)
        {
        }

        #region IDisposable 成员

        public void Dispose()
        {
            this._editPart = null;
            this._mainListPart = null;
            this._searchPart = null;
            this._toolBar = null;
            if (this.Workitem != null)
            {
                this.Workitem.Items.Remove(this);
                this.Workitem = null;
            }
        }

        #endregion
    }
}
