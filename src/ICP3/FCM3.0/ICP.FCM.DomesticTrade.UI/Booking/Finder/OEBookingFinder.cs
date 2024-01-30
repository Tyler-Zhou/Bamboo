﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI.WinForms;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.DomesticTrade.ServiceInterface;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.DomesticTrade.UI.Booking.Finder
{
    /// <summary>
    /// 订舱的搜索器,目前只实现了Single的Finder
    /// </summary>
    public class DTBookingFinder : IDataFinder,IDisposable
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IDomesticTradeService oeService { get; set; }

        [ServiceDependency]
        public IUserService userService { get; set; }

        #endregion

        private const string DTBookingFinderWorkspace = "DTBookingFinderWorkspace";

        /// <summary>
        /// 缓存一个公司列表
        /// </summary>
        List<OrganizationList> _userCompanyList = null;


        #region ChildWorkitem

        BookingS_FinderWorkitem singleFinderWorkitem = null;
        //UserMultiFinderWorkitem multiFinderWorkitem = null;

        #endregion

        #region IDataFinder 成员

        public event EventHandler<DataFindEventArgs> DataChoosed;

        #region One

        public void PickOne(string searchValue, string property, SearchConditionCollection conditions, string[] returnFields, FinderTriggerType triggerType, System.Windows.Forms.Control container)
        {
            DeckWorkspace workspce = this.Workitem.Workspaces.Get<DeckWorkspace>(DTBookingFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                if (this.Workitem.Workspaces.Contains(DTBookingFinderWorkspace))
                {
                    this.Workitem.Workspaces.Remove(workspce);
                }

                workspce = this.Workitem.Workspaces.AddNew<DeckWorkspace>(DTBookingFinderWorkspace);
                workspce.Dock = System.Windows.Forms.DockStyle.Fill;
                workspce.BringToFront();
            }
            container.Controls.Clear();
            container.Controls.Add(workspce);
            PickOne(searchValue, property,conditions, returnFields,triggerType, DTBookingFinderWorkspace);
        }

        public void PickOne(string searchValue, string property, SearchConditionCollection conditions, string[] returnFields, FinderTriggerType triggerType, string workspaceName)
        {
            if (string.IsNullOrEmpty(workspaceName))
                workspaceName = ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace;

            IWorkspace workspace = this.Workitem.Workspaces[workspaceName];

            #region

            string bookingNo = searchValue;

            if (_userCompanyList == null) _userCompanyList = userService.GetUserCompanyList(LocalData.UserInfo.LoginID, null);

            List<Guid> companyIDs = new List<Guid>();
            foreach (var item in _userCompanyList)
            {
                companyIDs.Add(item.ID);
            }

            List<DTBookingList> list = oeService.GetDTBookingList(companyIDs.ToArray(),
                                                                       bookingNo,
                                                                       string.Empty,
                                                                       string.Empty,
                                                                       string.Empty,
                                                                       string.Empty,
                                                                       string.Empty,
                                                                       string.Empty,
                                                                       string.Empty,
                                                                       string.Empty,
                                                                       string.Empty,
                                                                       string.Empty,
                                                                       null,
                                                                       null,
                                                                       null,
                                                                       true,
                                                                       DTOrderState.BookingConfirmed,
                                                                       DateSearchType.All,
                                                                       null,
                                                                       null,
                                                                       50,
                                                                       LocalData.IsEnglish);

            if (list != null && list.Count == 1)
            {
                if (DataChoosed != null)
                    DataChoosed(this, new DataFindEventArgs(Utility.GetSingleSearchResult<DTBookingList>(list[0], returnFields)));

                return;
            }

            #endregion

            singleFinderWorkitem = Workitem.WorkItems.Get<BookingS_FinderWorkitem>(this.GetHashCode().ToString() + "DTBookingSingleFinderWorkitem");
            if (singleFinderWorkitem == null)
            {
                singleFinderWorkitem = Workitem.WorkItems.AddNew<BookingS_FinderWorkitem>(this.GetHashCode().ToString() + "OEBookingSingleFinderWorkitem");
                singleFinderWorkitem.DataChoosed += delegate(object sender, DataFindEventArgs e)
                {
                    if (this.DataChoosed != null) DataChoosed(sender, e);
                };
            }

            Dictionary<string, object> initValues = new Dictionary<string, object>();
            initValues.Add(SearchFieldConstants.BookingNO, bookingNo);
            singleFinderWorkitem.Show(workspace, list, returnFields, initValues);
        }

        #endregion

        #region Many

        public void PickMany(string searchValue, string property, SearchConditionCollection conditions, string[] returnFields, FinderTriggerType triggerType, GetExistValueHandler getExistValueHandler, System.Windows.Forms.Control container)
        {
          
        }

        public void PickMany(string searchValue, string property, SearchConditionCollection conditions, string[] returnFields, FinderTriggerType triggerType, GetExistValueHandler getExistValueHandler, string workspaceName)
        {

        }

        #endregion

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(Boolean isDisposing)
        {
            if (isDisposing)
            {
                if (Workitem != null)
                {
                    //if (singleFinderWorkitem != null)
                    //    Workitem.WorkItems.Remove(singleFinderWorkitem);


                    if (singleFinderWorkitem != null)
                        singleFinderWorkitem.Dispose();

                    Workitem.Items.Remove(this);
                }
            }
        }

        #endregion
    }
}
