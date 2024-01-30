using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanExport.UI.Booking.Finder
{
    public class BookingS_FinderWorkitem : WorkItem
    {
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this._listPart = null;
                this._searchPart = null;
                DataChoosed = null;
            }
        }
        public event EventHandler<DataFindEventArgs> DataChoosed;
        BookingFinderSearchPart _searchPart = null;
        BookingS_FinderListPart _listPart = null;
        public void Show(IWorkspace mainWorkspace, List<OceanBookingList> list, string[] returnFields, Dictionary<string, object> initValues)
        {
            if (mainWorkspace == null)
                mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

            BookingS_FinderWorkspace mainSpace = this.Items.Get<BookingS_FinderWorkspace>("BookingS_FinderWorkspace");
            if (mainSpace == null)
            {
                mainSpace = this.Items.AddNew<BookingS_FinderWorkspace>("BookingS_FinderWorkspace");

                #region AddPart

                BookingS_FinderToolBar toolBar = this.Items.AddNew<BookingS_FinderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                _listPart = this.Items.AddNew<BookingS_FinderListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(_listPart);

                _searchPart = this.Items.AddNew<BookingFinderSearchPart>();
                _searchPart.IsLoadFromFinder = true;
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(_searchPart);


                BookingFinderFastSearchPart fastSearchPart = this.Items.AddNew<BookingFinderFastSearchPart>();
                IWorkspace fastSearchPartWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.FastSearchWorkspace];
                fastSearchPartWorkspace.Show(fastSearchPart);

                #endregion

                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Booking Finder" : "查找订舱";
                mainWorkspace.Show(mainSpace, smartPartInfo);

                _listPart.Selected += delegate(object sender, object data)
                {
                    OceanBookingList oceanBookingList = data as OceanBookingList;
                    if (oceanBookingList == null) return;
                    if (DataChoosed != null)
                    {
                        DataChoosed(sender, new DataFindEventArgs(OEUtility.GetSingleSearchResult<OceanBookingList>(oceanBookingList, returnFields)));
                    }
                };

                OEBookingFinderUIAdapter bookingAdapter = new OEBookingFinderUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(_listPart.GetType().Name, _listPart);
                dic.Add(_searchPart.GetType().Name, _searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                bookingAdapter.Init(dic);
            }
            else
            {
                mainWorkspace.Activate(mainSpace);
            }

            _searchPart.Init(initValues); 
            if(list !=null) _listPart.DataSource = list;          
        }
    }

    public class OEBookingFinderCommandConstants
    {
        public const string Command_AddData = "Command_AddData";
        public const string Command_CopyData = "Command_CopyData";
        public const string Command_EditData = "Command_EditData";
        public const string Command_Confirm = "Command_Confirm";
        public const string Command_ShowSearch = "Command_ShowSearch";

    }

    public class OEBookingFinderUIAdapter:IDisposable
    {

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        ISearchPart _fastSearchPart;
        IListPart _mainListPart;

        #endregion

        #region interface


        event EventHandler<DataFindEventArgs> DataChoosed;
        string[] _returnFields =null;

        public void InitSearch(EventHandler<DataFindEventArgs> dataChoosed, string[] returnFields)
        {
            this.DataChoosed += delegate(object sender, DataFindEventArgs e)
            {
                if (dataChoosed != null) dataChoosed(sender, e);
            };
            _returnFields = returnFields;
        }

        public void Init(Dictionary<string, object> controls)
        {

            _toolBar = (IToolBar)controls[typeof(BookingS_FinderToolBar).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(BookingFinderSearchPart).Name];
            _fastSearchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(BookingFinderFastSearchPart).Name];
            _mainListPart = (IListPart)controls[typeof(BookingS_FinderListPart).Name];

            RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region _mainListPart.CurrentChanged

            _mainListPart.Selected += delegate(object sender, object data)
            {
                OceanBookingList list = data as OceanBookingList;
                if (list == null) return;
                if (DataChoosed != null)
                {
                    DataChoosed(sender, new DataFindEventArgs(OEUtility.GetSingleSearchResult<OceanBookingList>(list, _returnFields)));
                }
            };

            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                OceanBookingList listData = data as OceanBookingList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

                #region toolBar

                RefreshBarEnabled(_toolBar, listData);

                #endregion
            };
            #endregion

            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion

            #region _fastSearchPart.OnSearched
            _fastSearchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion

            #endregion
        }

        private void RefreshBarEnabled(IToolBar toolBar, OceanBookingList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barConfirm", false);
                toolBar.SetEnable("barCopy", false);
                toolBar.SetEnable("barEdit", false);
            }
            else
            {
                if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(listData.OceanShippingOrderID))
                {
                    toolBar.SetEnable("barCopy", false);
                }
                else
                {
                    toolBar.SetEnable("barCopy", true);
                }
                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barConfirm", true);

            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            this._fastSearchPart = null;
            this._mainListPart = null;
            this._returnFields = null;
            this._searchPart = null;
            this._toolBar = null;
            this.DataChoosed = null;
        }

        #endregion
    }
}
