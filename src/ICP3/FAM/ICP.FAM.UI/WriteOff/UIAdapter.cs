using ICP.Business.Common.UI.EventList;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.UI.WriteOff.Parts;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ICP.FAM.UI.WriteOff
{
    #region 销账界面适配器
    /// <summary>
    /// 销账界面适配器
    /// </summary>
    public class WriteOffUIAdapter : IDisposable
    {

        IToolBar _toolBar;
        ISearchPart _searchPart;
        WriteOffList _mainListPart;
        ISearchPart _fastSearchPart;
        MultiSelections _multiSelection;
        EventListPart _eventListPart;

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(ToolBar).Name];
            _searchPart = (ISearchPart)controls[typeof(SearchPanel).Name];
            _mainListPart = (WriteOffList)controls[typeof(WriteOffList).Name];
            _multiSelection = (MultiSelections)controls[typeof(MultiSelections).Name];
            _eventListPart = (EventListPart)controls[typeof(EventListPart).Name];

            RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region 主列表选择的行发生改变
            _mainListPart.CurrentChanged += delegate (object sender, object data)
            {
                WriteOffItemList listData = data as WriteOffItemList;
                if (listData != null)
                {
                    BusinessOperationContext context = new BusinessOperationContext();
                    context.OperationID = listData.ID;
                    context.FormId = listData.ID;
                    context.FormType = FormType.Unknown;
                    context.OperationType = OperationType.OceanExport;
                    _eventListPart.DataSource = context;
                }

                RefreshBarEnabled(_toolBar, listData);
                _multiSelection.RefreshControl();
            };
            #endregion

            #region 勾选的内容发生改变时
            _mainListPart.Selected += delegate (object sender, object data)
            {
                _multiSelection.DataSource = data;
            };
            #endregion

            #region 查询
            _searchPart.OnSearched += delegate (object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion

            #region 移除/清空 已选中的数据
            _multiSelection.Selected += delegate (object sender, object data)
            {
                _mainListPart.MultiListRemoveData(sender, data);
            };
            #endregion

            #region 分页
            _mainListPart.InvokeGetData += delegate (object sender, object data)
            {
                _searchPart.RaiseSearched(data);
            };
            #endregion

            #region 热键

            _mainListPart.KeyDown += new KeyEventHandler(_mainListPart_KeyDown);

            #endregion

            #region 多选列表中操作事件发生改变
            _multiSelection.ListOperating += delegate (object sender, object data)
            {
                _mainListPart.RefreshUI(sender, data);
            };

            #endregion

            #endregion
        }

        public void AdvanceSearch()
        {
            _searchPart.RaiseSearched();
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
        private void RefreshBarEnabled(IToolBar toolBar, WriteOffItemList listData)
        {
            if (listData == null)
            {
                toolBar.SetEnable("barVoid", false);
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barDelete", false);
                toolBar.SetEnable("bbiBullion", false);
                toolBar.SetEnable("bbiCancelBullion", false);
                toolBar.SetEnable("bbiAudit", false);
                toolBar.SetEnable("bbiCancelAudit", false);
                toolBar.SetEnable("bbiListCredentials", false);
                toolBar.SetEnable("barPrint", false);
                toolBar.SetEnable("barAllCheck", false);
                toolBar.SetEnable("barUntieLock", false);
                toolBar.SetEnable("barDirectBank", false);
            }
            else
            {
                toolBar.SetEnable("barVoid", true);
                toolBar.SetEnable("barUntieLock", true);
                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barDelete", true);
                toolBar.SetEnable("bbiListCredentials", true);
                toolBar.SetEnable("barPrint", true);
                toolBar.SetEnable("barAllCheck", true);
                toolBar.SetEnable("barDirectBank", false);

                //到账
                if (string.IsNullOrEmpty(listData.BankByName))
                {
                    #region 到帐
                    toolBar.SetEnable("bbiBullion", true);
                    toolBar.SetEnable("bbiCancelBullion", false);
                    toolBar.SetEnable("bbiAudit", false);
                    toolBar.SetEnable("bbiCancelAudit", false);
                    toolBar.SetEnable("barDirectBank", true);
                    #endregion
                }
                else
                {
                    #region 审核
                    if (string.IsNullOrEmpty(listData.ApprovalByName))
                    {
                        toolBar.SetEnable("bbiAudit", true);
                        toolBar.SetEnable("bbiCancelBullion", true);

                        toolBar.SetEnable("bbiBullion", false);
                        toolBar.SetEnable("bbiCancelAudit", false);
                    }
                    else
                    {
                        toolBar.SetEnable("bbiCancelAudit", true);

                        toolBar.SetEnable("bbiBullion", false);
                        toolBar.SetEnable("bbiCancelBullion", false);
                        toolBar.SetEnable("bbiAudit", false);
                    }
                    #endregion
                }

                #region 作废
                if (!listData.IsValid)
                {
                    toolBar.SetEnable("barEdit", false);
                    toolBar.SetEnable("barDelete", false);
                    toolBar.SetEnable("bbiBullion", false);
                    toolBar.SetEnable("bbiCancelBullion", false);
                    toolBar.SetEnable("bbiAudit", false);
                    toolBar.SetEnable("bbiCancelAudit", false);
                    toolBar.SetEnable("barPrint", false);
                    toolBar.SetEnable("barAllCheck", false);
                    toolBar.SetEnable("barVoid", false);
                    toolBar.SetEnable("barUntieLock", false);

                    toolBar.SetText("barVoid", LocalData.IsEnglish ? "Void" : "作废");
                }
                #endregion

            }

        }

        #region IDisposable 成员

        public void Dispose()
        {
            _fastSearchPart = null;
            _mainListPart.KeyDown -= _mainListPart_KeyDown;
            _mainListPart = null;
            _multiSelection = null;
            _searchPart = null;
            _toolBar = null;
        }

        #endregion
    }
    #endregion
}
