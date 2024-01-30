using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ICP.FAM.UI.BankTransaction
{
    public class BankTransactionUIAdapter : IDisposable
    {
        IToolBar _toolBar;
        ISearchPart _searchPart;
        ListPart _mainListPart;
        EditPart _editPart;

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(ToolBar).Name];
            _searchPart = (ISearchPart)controls[typeof(SearchPart).Name];
            _mainListPart = (ListPart)controls[typeof(ListPart).Name];
            _editPart = (EditPart)controls[typeof(EditPart).Name];

            RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate (object sender, object data)
            {
                BankTransactionInfo listData = data as BankTransactionInfo;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

                #region toolBar

                RefreshBarEnabled(_toolBar, listData);

                #endregion

                _editPart.DataSource = listData;
            };

            _mainListPart.CurrentChanging += delegate (object sender, CancelEventArgs e)
            {
               
            };
            _mainListPart.KeyDown += new KeyEventHandler(_mainListPart_KeyDown);

            _editPart.Saved += delegate (object[] data)
            {
                _mainListPart.Refresh(data);
            };
            #endregion

            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate (object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion

            #endregion
        }

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
        private void RefreshBarEnabled(IToolBar toolBar, BankTransactionInfo listData)
        {
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _editPart = null;
            _mainListPart.KeyDown -= _mainListPart_KeyDown;
            _mainListPart = null;
            _searchPart = null;
            _toolBar = null;

        }

        #endregion
    }
}
