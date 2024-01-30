#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/3/13 星期二 14:22:22
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using ICP.Business.Common.UI.Communication;
using ICP.Business.Common.UI.Document;
using ICP.Business.Common.UI.EventList;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.FCM.OtherBusiness.UI.Order
{
    /// <summary>
    /// 其他业务订单
    /// </summary>
    public class OBOrderUIAdapter
    {
        #region Service
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        OBOrderListPart _mainListPart;
        ISearchPart _fastSearchPart;
        IListPart _memolistPart;
        UCCommunicationHistory _faxMailEDIListPart;
        UCDocumentList _ucDocumentList;
        #endregion

        #region interface
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controls"></param>
        public void Init(Dictionary<string, object> controls)
        {

            _toolBar = (IToolBar)controls[typeof(OBOrderMainToolBar).Name];
            _searchPart = (ISearchPart)controls[typeof(OBOrderSearchPart).Name];
            _mainListPart = (OBOrderListPart)controls[typeof(OBOrderListPart).Name];
            _fastSearchPart = (ISearchPart)controls[typeof(OBOrderFastSearchPart).Name];
            _memolistPart = (IListPart)controls[typeof(EventListPart).Name];
            _faxMailEDIListPart = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];
            _ucDocumentList = (UCDocumentList)controls[typeof(UCDocumentList).Name];

            RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                OtherBusinessList listData = data as OtherBusinessList;
                Dictionary<string, object> keyValue = new Dictionary<string, object> {{"ParentList", data}};

                if (listData != null)
                {

                    BusinessOperationContext context = new BusinessOperationContext();
                    context.OperationID = listData.ID;
                    context.FormId = listData.ID;
                    context.FormType = FormType.Booking;
                    context.OperationType = OperationType.Other;

                    _memolistPart.DataSource = context;

                    _faxMailEDIListPart.BindData(context);
                    FCM.Common.UI.FCMUIUtility.SetDocumentListDataSource(_ucDocumentList, context);
                }
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

            _mainListPart.KeyDown += _mainListPart_KeyDown;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _mainListPart_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender != null)
            {
                Dictionary<string, object> keyValue = sender as Dictionary<string, object>;
                if (keyValue != null)
                {
                    _searchPart.Init(keyValue);
                    _searchPart.RaiseSearched();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toolBar"></param>
        /// <param name="listData"></param>
        private void RefreshBarEnabled(IToolBar toolBar, OtherBusinessList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barCopy", false);
                toolBar.SetEnable("barCancel", false);
                toolBar.SetEnable("barPrint", false);
                toolBar.SetEnable("barBill", false);
                toolBar.SetEnable("barRefresh", false);
            }
            else
            {
                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barCopy", true);
                toolBar.SetEnable("barCancel", true);
                toolBar.SetEnable("barPrint", true);
                toolBar.SetEnable("barBill", true);
                toolBar.SetEnable("barRefresh", true);
            }
        }
        #endregion
    }
}
