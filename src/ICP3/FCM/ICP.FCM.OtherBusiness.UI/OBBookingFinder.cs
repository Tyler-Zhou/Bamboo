using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.OtherBusiness.UI.Booking.Finder
{
    /// <summary>
    /// 订舱的搜索器,目前只实现了Single的Finder
    /// </summary>
    public class OBBookingFinder : IDataFinder,IDisposable
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOtherBusinessService OtherBusinessService
        {
            get
            {
                return ServiceClient.GetService<IOtherBusinessService>();
            }
        }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        #endregion

        private const string OBBookingFinderWorkspace = "OBBookingFinderWorkspace";

        /// <summary>
        /// 缓存一个公司列表
        /// </summary>
        List<OrganizationList> _userCompanyList = null;

  

        #region IDataFinder 成员

        public event EventHandler<DataFindEventArgs> DataChoosed;

        #region One

        public void PickOne(string searchValue, string property, SearchConditionCollection conditions, string[] returnFields, FinderTriggerType triggerType, Control container)
        {
            DeckWorkspace workspce = Workitem.Workspaces.Get<DeckWorkspace>(OBBookingFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                if (Workitem.Workspaces.Contains(OBBookingFinderWorkspace))
                {
                    Workitem.Workspaces.Remove(workspce);
                }

                workspce = Workitem.Workspaces.AddNew<DeckWorkspace>(OBBookingFinderWorkspace);
                workspce.Dock = DockStyle.Fill;
                workspce.BringToFront();
            }
            container.Controls.Clear();
            container.Controls.Add(workspce);
            PickOne(searchValue, property,conditions, returnFields,triggerType, OBBookingFinderWorkspace);
        }

        public void PickOne(string searchValue, string property, SearchConditionCollection conditions, string[] returnFields, FinderTriggerType triggerType, string workspaceName)
        {
            if (string.IsNullOrEmpty(workspaceName))
                workspaceName = ClientConstants.MainWorkspace;

            IWorkspace workspace = Workitem.Workspaces[workspaceName];

            #region

            string bookingNo = searchValue;

            if (_userCompanyList == null) _userCompanyList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, null);

            List<OtherBusinessList> list = OtherBusinessService.GetOtherBusinessList(_userCompanyList.Select(item => item.ID).ToArray(),
                                                                       null,
                                                                       new[] { 1, 2, 3, 4 },
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
                                                                       null,
                                                                       null,
                                                                       null,
                                                                       true,
                                                                       OBOrderState.NewOrder,
                                                                       Guid.Empty,
                                                                       null,
                                                                       DateSearchType.All,
                                                                       null,
                                                                       null,
                                                                       50);
                                                   


            if (list != null && list.Count == 1)
            {
                if (DataChoosed != null)
                    DataChoosed(this, new DataFindEventArgs(Utility.GetSingleSearchResult<OtherBusinessList>(list[0], returnFields)));

                return;
            }

            #endregion

            //singleFinderWorkitem = Workitem.WorkItems.Get<BookingS_FinderWorkitem>(this.GetHashCode().ToString() + "OEBookingSingleFinderWorkitem");
            //if (singleFinderWorkitem == null)
            //{
            //    singleFinderWorkitem = Workitem.WorkItems.AddNew<BookingS_FinderWorkitem>(this.GetHashCode().ToString() + "OEBookingSingleFinderWorkitem");
            //    singleFinderWorkitem.DataChoosed += delegate(object sender, DataFindEventArgs e)
            //    {
            //        if (this.DataChoosed != null) DataChoosed(sender, e);
            //    };
            //}

            Dictionary<string, object> initValues = new Dictionary<string, object>
            {
                {SearchFieldConstants.BookingNO, bookingNo}
            };
            //singleFinderWorkitem.Show(workspace, list, returnFields, initValues);
        }

        #endregion

        #region Many

        public void PickMany(string searchValue, string property, SearchConditionCollection conditions, string[] returnFields, FinderTriggerType triggerType, GetExistValueHandler getExistValueHandler, Control container)
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
            
        }
        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                DataChoosed = null;
                _userCompanyList = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            }
        }
        #endregion
    }
}
