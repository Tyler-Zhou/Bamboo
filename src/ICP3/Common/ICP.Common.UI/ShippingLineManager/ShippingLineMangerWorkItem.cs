using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Common.UI.ShippingLineManager;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Common.ServiceInterface.DataObjects;


namespace ICP.Common.UI
{
    public class ShippingLineMangerWorkItem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            ShippingLineMainWorkspace mainSpce = this.SmartParts.Get<ShippingLineMainWorkspace>("OEOrderMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<ShippingLineMainWorkspace>("OEOrderMainWorkspace");

                #region AddPart

                ShippingLineToolBarPart toolBar = this.SmartParts.AddNew<ShippingLineToolBarPart>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[ShippingLineWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                ShippingLineListPart listPart = this.SmartParts.AddNew<ShippingLineListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[ShippingLineWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                ShippingLineSearchPart searchPart = this.SmartParts.AddNew<ShippingLineSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[ShippingLineWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                SippingLineEditPart editPart = this.SmartParts.AddNew<SippingLineEditPart>();
                IWorkspace editWorkspace = (IWorkspace)this.Workspaces[ShippingLineWorkSpaceConstants.EditListWorkspace];
                editWorkspace.Show(editPart);

                NationAndPortListEditPart nationAndPortPart = this.SmartParts.AddNew<NationAndPortListEditPart>();
                IWorkspace nationAndPortWorkspace = (IWorkspace)this.Workspaces[ShippingLineWorkSpaceConstants.NationAndPortListWorkspace];
                nationAndPortWorkspace.Show(nationAndPortPart);

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "ShippingLine Manger" : "航线管理";
                mainWorkspace.Show(mainSpce, smartPartInfo);

                ShippingLineUIAdapter shippingLineAdapter = new ShippingLineUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(editPart.GetType().Name, editPart);
                dic.Add(nationAndPortPart.GetType().Name, nationAndPortPart);

                shippingLineAdapter.Init(dic);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }

        }

    }
    public class ShippingLineWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string EditListWorkspace = "EditListWorkspace";
        public const string NationAndPortListWorkspace = "NationAndPortListWorkspace";
    }


    public class ShippingLineUIAdapter : IDisposable
    {
        #region parts
        IToolBar _toolBar;
        ISearchPart _searchPart;
        ShippingLineListPart _mainListPart;
        SippingLineEditPart _sippingLineEditPart;
        NationAndPortListEditPart _nationAndPortListEditPart;
        #endregion

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(ShippingLineToolBarPart).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(ShippingLineSearchPart).Name];
            _mainListPart = (ShippingLineListPart)controls[typeof(ShippingLineListPart).Name];
            _sippingLineEditPart = (SippingLineEditPart)controls[typeof(SippingLineEditPart).Name];
            _nationAndPortListEditPart = (NationAndPortListEditPart)controls[typeof(NationAndPortListEditPart).Name];
            #region Connection

            #region _mainListPart.CurrentChanged

            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                ShippingLineList listdata = data as ShippingLineList;
                if (listdata != null)
                {
                    _sippingLineEditPart.DataSource = listdata;
                    _nationAndPortListEditPart.DataSource = listdata;
                }
            };

            #endregion

            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion

            #endregion

            _sippingLineEditPart.Saved += new SavedHandler(_mainListPart.EditPartSaved);

        }


        private void RefreshBarEnabled(IToolBar toolBar, ShippingLineList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barAdd", false);
                toolBar.SetEnable("barInvalid", false);
            }
            else
            {
                if (listData.IsValid == true)
                {
                    toolBar.SetEnable("barInvalid", true);
                    toolBar.SetText("barInvalid", LocalData.IsEnglish ? "Invalid(&I)" : "作废(&I)");
                }
                else 
                {
                    toolBar.SetEnable("barInvalid", false);
                    toolBar.SetText("barInvalid", LocalData.IsEnglish ? "Available(&D)" : "激活(&D)");
                }
                   
            }
        }

        public void Dispose()
        {
            //this._eventlistPart = null;
            //this._fastSearchPart = null;
            //this._mainListPart.KeyDown -= this._mainListPart_KeyDown;
            //this._mainListPart = null;
            //this._searchPart = null;
            //this._toolBar = null;
            //this._ucCommunicationHistory = null;
            //this._ucDocumentList = null;
        }

    }

    public class ShippingLineCommandConstants
    {
        public const string Command_AddData = "Command_ShippingLineAddData";
        public const string Command_Activation = "Command_ShippingLineActivation";
        public const string Command_Invalid = "Command_ShippingLineInvalid";
    }
}
