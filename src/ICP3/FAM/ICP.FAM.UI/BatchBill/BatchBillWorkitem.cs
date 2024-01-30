using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FAM.UI.BatchBill
{
    /// <summary>
    /// 批量账单WorkItem
    /// </summary>
    public class BatchBillWorkitem : WorkItem
    {
        #region Show
        /// <summary>
        /// 
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }
        /// <summary>
        /// 显示面板
        /// </summary>
        public void Show()
        {
            BatchBillMainWorkspace batchBillMainWorkspace = SmartParts.Get<BatchBillMainWorkspace>(BatchBillWorkSpaceConstants.MainWorkspaceName);
            if (batchBillMainWorkspace == null)
            {
                batchBillMainWorkspace = SmartParts.AddNew<BatchBillMainWorkspace>(BatchBillWorkSpaceConstants.MainWorkspaceName);
                //Tool Bar Part
                BatchCustomerBillToolBarPart batchBillToolBarPart = SmartParts.AddNew<BatchCustomerBillToolBarPart>();
                IWorkspace toolBarWorkspace = Workspaces[BatchBillWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(batchBillToolBarPart);
                //Fast Search part
                BatchCustomerBillFastSearchPart fastSearchPart = SmartParts.AddNew<BatchCustomerBillFastSearchPart>();
                IWorkspace fastSearchWorkspace = Workspaces[BatchBillWorkSpaceConstants.FastSearchWorkspace];
                fastSearchWorkspace.Show(fastSearchPart);
                //Search Part
                BatchCustomerBillSearchPart searchPart = SmartParts.AddNew<BatchCustomerBillSearchPart>();
                IWorkspace searchWorkspace = Workspaces[BatchBillWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);
                //List Part
                BatchCustomerBillListPart listPart = SmartParts.AddNew<BatchCustomerBillListPart>();
                IWorkspace listWorkspace = Workspaces[BatchBillWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo
                {
                    Title = LocalData.IsEnglish ? "Batch Customer Bill" : "批量业务帐单"
                };

                batchBillMainWorkspace.Disposed += delegate { Dispose(); };
                mainWorkspace.Show(batchBillMainWorkspace, smartPartInfo);

                BatchBillUIAdapter bookingAdapter = new BatchBillUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {fastSearchPart.GetType().Name, fastSearchPart},
                    {searchPart.GetType().Name, searchPart},
                    {listPart.GetType().Name, listPart}
                };
                bookingAdapter.Init(dic);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(batchBillMainWorkspace);
            }
        }
        #endregion
    }
    /// <summary>
    /// UI适配器
    /// 界面关联
    /// </summary>
    public class BatchBillUIAdapter : IDisposable
    {
        #region parts
        /// <summary>
        /// 查询面板
        /// </summary>
        ISearchPart _searchPart;
        /// <summary>
        /// 快速搜索面板
        /// </summary>
        ISearchPart _fastSearchPart;
        /// <summary>
        /// 批量账单列表面板
        /// </summary>
        BatchCustomerBillListPart _mainListPart;

        #endregion

        #region interface
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="controls"></param>
        public void Init(Dictionary<string, object> controls)
        {
            _searchPart = (ISearchPart)controls[typeof(BatchCustomerBillSearchPart).Name];
            _fastSearchPart = (ISearchPart)controls[typeof(BatchCustomerBillFastSearchPart).Name];
            _mainListPart = (BatchCustomerBillListPart)controls[typeof(BatchCustomerBillListPart).Name];

            #region Connection

            #region _mainListPart.CurrentChanged
           
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

            _mainListPart.KeyDown += _mainListPart_KeyDown;

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
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _fastSearchPart = null;
            _mainListPart.KeyDown -= _mainListPart_KeyDown;
            _mainListPart = null;
            _searchPart = null;
        }

        #endregion
    }
}
