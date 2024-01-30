using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI.WinForms;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.FCM.DomesticTrade.ServiceInterface;

namespace ICP.FCM.DomesticTrade.UI.Booking.Finder
{
    public class BookingS_FinderWorkitem : WorkItem
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IUserService userService { get; set; }

        #endregion

        public event EventHandler<DataFindEventArgs> DataChoosed;
        BookingFinderSearchPart _searchPart = null;
        BookingS_FinderListPart _listPart = null;
        public void Show(IWorkspace mainWorkspace, List<DTBookingList> list, string[] returnFields, Dictionary<string, object> initValues)
        {
            if (mainWorkspace == null)
                mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

            BookingS_FinderWorkspace mainSpace = this.Items.Get<BookingS_FinderWorkspace>("BookingS_FinderWorkspace");
            if (mainSpace == null)
            {
                mainSpace = this.Items.AddNew<BookingS_FinderWorkspace>("BookingS_FinderWorkspace");

                #region AddPart

                BookingS_FinderToolBar toolBar = this.Items.AddNew<BookingS_FinderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[DTBookingWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                _listPart = this.Items.AddNew<BookingS_FinderListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[DTBookingWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(_listPart);

                _searchPart = this.Items.AddNew<BookingFinderSearchPart>();
                _searchPart.IsLoadFromFinder = true;
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[DTBookingWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(_searchPart);


                BookingFinderFastSearchPart fastSearchPart = this.Items.AddNew<BookingFinderFastSearchPart>();
                IWorkspace fastSearchPartWorkspace = (IWorkspace)this.Workspaces[DTBookingWorkSpaceConstants.FastSearchWorkspace];
                fastSearchPartWorkspace.Show(fastSearchPart);

                #endregion

                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Booking Finder" : "查找订舱";
                mainWorkspace.Show(mainSpace, smartPartInfo);

                _listPart.Selected += delegate(object sender, object data)
                {
                    DTBookingList dtBookingList = data as DTBookingList;
                    if (dtBookingList == null) return;
                    if (DataChoosed != null)
                    {
                        DataChoosed(sender, new DataFindEventArgs(Utility.GetSingleSearchResult<DTBookingList>(dtBookingList, returnFields)));
                    }
                };

                DTBookingFinderUIAdapter bookingAdapter = new DTBookingFinderUIAdapter();
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
            
            if(list !=null) _listPart.DataSource = list;

            _searchPart.Init(initValues);
        }

    }

    public class DTBookingFinderCommandConstants
    {
        public const string Command_AddData = "Command_AddData";
        public const string Command_CopyData = "Command_CopyData";
        public const string Command_EditData = "Command_EditData";
        public const string Command_Confirm = "Command_Confirm";
        public const string Command_ShowSearch = "Command_ShowSearch";

    }

    public class DTBookingFinderUIAdapter
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

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
                DTBookingList list = data as DTBookingList;
                if (list == null) return;
                if (DataChoosed != null)
                {
                    DataChoosed(sender, new DataFindEventArgs(Utility.GetSingleSearchResult<DTBookingList>(list, _returnFields)));
                }
            };

            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                DTBookingList listData = data as DTBookingList;
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

        private void RefreshBarEnabled(IToolBar toolBar, DTBookingList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barConfirm", false);
                toolBar.SetEnable("barCopy", false);
                toolBar.SetEnable("barEdit", false);
            }
            else
            {
                if (Utility.GuidIsNullOrEmpty(listData.OceanShippingOrderID))
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
    }
}
