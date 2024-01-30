using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using System;
using System.Collections.Generic;


namespace ICP.Business.Common.UI.CSPBooking
{
    /// <summary>
    /// UI适配器
    /// </summary>
    public class CSPBKUIAdapter : IDisposable
    {
        #region parts

        IToolBar _toolBar;
        IListPart _mainListPart;
        ISearchPart _searchPart;
        ISearchPart _fastSearchPart;
        PartMainWorkspace _mainSpace;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(PartToolBar).Name];
            _searchPart = (ISearchPart)controls[typeof(PartSearch).Name];
            _fastSearchPart = (ISearchPart)controls[typeof(PartFastSearch).Name];
            _mainListPart = (IListPart)controls[typeof(PartList).Name];

            RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                BookingDelegateList listData = data as BookingDelegateList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

                if (listData != null)
                {

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

        }

        private void RefreshBarEnabled(IToolBar toolBar, BookingDelegateList listData)
        {
            if (listData == null)
            {
                toolBar.SetEnable("barDownload", false);
            }
            else
            {
                toolBar.SetEnable("barDownload", true);
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _fastSearchPart = null;
            _mainListPart = null;
            _searchPart = null;
            _toolBar = null;
            _mainSpace = null;
        }

        #endregion
    }
}
