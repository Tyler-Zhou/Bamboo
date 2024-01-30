#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/3/15 星期四 17:56:36
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

namespace ICP.FCM.OtherBusiness.UI.ECommerce
{
    /// <summary>
    /// 
    /// </summary>
    public class OBECUIAdapter
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
        OBECListPart _mainListPart;
        ISearchPart _fastSearchPart;
        IListPart _memolistPart;
        UCCommunicationHistory _faxMailEDIListPart;
        UCDocumentList _DocumentListPart;

        #endregion

        #region interface
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controls"></param>
        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(OBECMainToolBar).Name];
            _searchPart = (ISearchPart)controls[typeof(OBECSearchPart).Name];
            _mainListPart = (OBECListPart)controls[typeof(OBECListPart).Name];
            _fastSearchPart = (ISearchPart)controls[typeof(OBECFastSearchPart).Name];
            _memolistPart = (IListPart)controls[typeof(EventListPart).Name];
            _faxMailEDIListPart = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];
            _DocumentListPart = (UCDocumentList)controls[typeof(UCDocumentList).Name];

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
                    //设置沟通历史记录数据源
                    _faxMailEDIListPart.BindData(context);
                    //设置文档中心数据源
                    FCM.Common.UI.FCMUIUtility.SetDocumentListDataSource(_DocumentListPart, context);
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
                toolBar.SetEnable("barCancel", false);
                toolBar.SetEnable("barPrint", false);
                toolBar.SetEnable("barBill", false);
                toolBar.SetEnable("barVerifiSheet", false);
                toolBar.SetEnable("barRefresh", false);
            }
            else
            {
                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barCancel", true);
                toolBar.SetEnable("barPrint", true);
                toolBar.SetEnable("barBill", true);
                toolBar.SetEnable("barVerifiSheet", true);
                toolBar.SetEnable("barRefresh", true);
            }
        }

        #endregion
    }
}
