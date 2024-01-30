using System.Collections.Generic;
using uf = ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using System;

namespace ICP.Common.UI.CustomerManager
{
    /// <summary>
    /// 客户管理桥-管理各个面板之间的交互
    /// </summary>
    internal class CustomerManagerBridge : uf.IPartBridge,IDisposable
    {
        #region 服务

        private WorkItem _workItem { get; set; }

        #endregion

        #region 本地属性

        uf.IToolBar _mainToolBar;  //工具栏
        uf.IListPart _mainListPart; //主列表
        uf.ISearchPart _mainSearchPart; //查询面板
        //CustomerManagerEditPart _editPart; //编辑面板
        uf.IListEidtPart _contactListEditPart;  //联系人面板
        uf.IListEidtPart _partnerListEditPart;  //合作伙伴面板
        uf.IListEidtPart _combineListEditPart;  //合并面板
        uf.IListEidtPart _memoListEditPart;  //备注面板
        uf.IListEidtPart _invoiceTitlePart;    //发票抬头面板

        #endregion

        #region IPartBridge 接口

        /// <summary>
        /// 初始化
        /// <remarks>
        /// 把由布局生成的面板信息初始化到该处,以便在此处理各个部件之间的交互.
        /// 由布局生成类调用该接口
        /// </remarks>
        /// </summary>
        /// <param name="context">生成上下文</param>
        /// <param name="partNames">处理那几个部件之间的交互</param>
        public void Init(
            uf.ILayoutBuilderContext context,
            string[] partNames)
        {
            //设置要交互的面版
            _workItem = context.WorkItem;
            _mainToolBar = (uf.IToolBar)context.Controls[typeof(CustomerManagerToolBarPart).Name];
            _mainListPart = (uf.IListPart)context.Controls[typeof(CustomerManagerListPart).Name];
            //_editPart = (CustomerManagerEditPart)context.Controls[typeof(CustomerManagerEditPart).Name];
            _mainSearchPart = (uf.ISearchPart)context.Controls[typeof(CustomerManagerSearchPart).Name];
            _partnerListEditPart = (uf.IListEidtPart)context.Controls[typeof(CustomerManagerPartnerListEditPart).Name];
            _contactListEditPart = (uf.IListEidtPart)context.Controls[typeof(CustomerManagerContactListEditPart).Name];
            _combineListEditPart = (uf.IListEidtPart)context.Controls[typeof(CustomerManagerCombineListEditPart).Name];
            _memoListEditPart = (uf.IListEidtPart)context.Controls[typeof(CustomerManagerMemoListEditPart).Name];
            _invoiceTitlePart = (uf.IListEidtPart)context.Controls[typeof(CustomerInvoiceTitleListPart).Name];
            //交互事件
            _mainSearchPart.OnSearched += new ICP.Framework.ClientComponents.UIFramework.SearchResultHandler(_mainSearchPart_OnSearched);
            _mainListPart.CurrentChanged += new ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler(_mainListPart_CurrentChanged);
            _mainListPart.CurrentChanging += new System.ComponentModel.CancelEventHandler(_mainListPart_CurrentChanging);
            _mainListPart.Selected += new ICP.Framework.ClientComponents.UIFramework.SelectedHandler(_mainListPart_Selected);
        }

        /// <summary>
        /// 动态要注入交互面板
        /// </summary>
        /// <typeparam name="T">面板类型</typeparam>
        /// <param name="part">面板</param>
        /// <param name="name">面板名称</param>
        public void Register<T>(
            T part,
            string name)
        {
        }

        #endregion

        #region 主菜单命令处理

        ///// <summary>
        ///// 添加客户
        ///// </summary>
        ///// <param name="s"></param>
        ///// <param name="e"></param>
        //[CommandHandler(CustomerManagerConstants.CMD_AddCustomer)]
        //public void CMD_AddCustomer(object s, EventArgs e)
        //{      
        //    CustomerManagerCheckForm checkForm = _workItem.Items.AddNew<CustomerManagerCheckForm>();
        //    DialogResult dlg = checkForm.ShowDialog();
        //    if (dlg != DialogResult.OK)
        //    {
        //        return;
        //    }

        //    CustomerList customer = new CustomerList();
        //    if (LocalData.IsEnglish)
        //    {
        //        customer.EName = checkForm.CustomerName;
        //    }
        //    else
        //    {
        //        customer.CName = checkForm.CustomerName;
        //    }

        //    ISmartPartInfo info = new SmartPartInfo();
        //    info.Title = "新增客户";
        //    ShowEditCustomerForm(customer, info);
        //    _mainListPart.InsertItem(0, customer); 
        //}

        ///// <summary>
        ///// 编辑客户
        ///// </summary>
        ///// <param name="s"></param>
        ///// <param name="e"></param>
        //[CommandHandler(CustomerManagerConstants.CMD_EditCustomer)]
        //public void CMD_EditCustomer(object s, EventArgs e)
        //{
        //    if (_mainListPart.Current == null) return;
        //    var customer = _mainListPart.Current as CustomerList;
        //    ISmartPartInfo info = new SmartPartInfo();
        //    info.Title = "客户编辑";
        //    ShowEditCustomerForm(customer, info);
        //}

        #endregion

        #region 各个部件之间的交互逻辑

        void _mainListPart_CurrentChanging(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (!_editPart.BeforeParentChanged())
            //{
            //    e.Cancel = true;
            //}
        }

        void _mainListPart_CurrentChanged(object sender, object data)
        {
            CustomerList customer = (CustomerList)data;
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("CustomerList", customer);
            RefreshBarEnabled(_mainToolBar, customer);
            RefreshBarDangerousText(_mainToolBar, customer);
            _contactListEditPart.Init(values);
            _partnerListEditPart.Init(values);
            _combineListEditPart.Init(values);
            _memoListEditPart.Init(values);
            _invoiceTitlePart.Init(values);
        }

        void _mainListPart_Selected(object sender, object data)
        {
            CustomerList customer = (CustomerList)data;
            if (customer != null)
            {
                _combineListEditPart.AddItem(customer);
            }
        }

        void _mainSearchPart_OnSearched(object sender, object results)
        {
            _mainListPart.DataSource = results;
        }

        //void editPart_Saved(params object[] prams)
        //{
        //    if (_mainListPart.Current == null || prams == null) return;

        //    CustomerInfo info = prams[0] as CustomerInfo;
        //    CustomerList currentRow = _mainListPart.Current as CustomerList;
        //    bool isNew = currentRow.IsNew;
        //    ICP.Framework.CommonLibrary.Helper.ObjectHelper.CopyData(info, currentRow);
        //    if (isNew)
        //    {
        //        _mainListPart_CurrentChanged(null, currentRow);
        //    }
        //}

        #endregion

        //private void ShowEditCustomerForm(CustomerList customer, ISmartPartInfo info)
        //{
        //    if (customer == null) return;
        //    CustomerManagerEditPart editPart = _workItem.Items.AddNew<CustomerManagerEditPart>();
        //    editPart.Saved += new SavedHandler(editPart_Saved);
        //    Dictionary<string, object> values = new Dictionary<string, object>();
        //    values.Add("CustomerList", customer);
        //    editPart.Init(values);
        //    IWorkspace mainWorkspace = _workItem.RootWorkItem.Workspaces[ClientConstants.MainWorkspace];
        //    mainWorkspace.Show(editPart, info);
        //}

        private void RefreshBarEnabled(IToolBar toolBar, CustomerList listData)
        {
            if (listData == null || listData.State == CustomerStateType.Invalid)
            {
                toolBar.SetEnable("barEdit", false);
            }
            else
            {
                toolBar.SetEnable("barEdit", true);
            }

            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barApplyCode", false);
                toolBar.SetEnable("barDisuse", false);
            }
            else
            {
                toolBar.SetEnable("barApplyCode", true);
                toolBar.SetEnable("barDisuse", true);
                if (listData.State != CustomerStateType.Invalid)
                    toolBar.SetText("barDisuse", LocalData.IsEnglish ? "Disuse(&D)" : "作废(&D)");
                else
                    toolBar.SetText("barDisuse", LocalData.IsEnglish ? "Available(&D)" : "激活(&D)");
            }
        }

        private void RefreshBarDangerousText(IToolBar toolBar, CustomerList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barSetDangerous", false);
            }
            else
            {
                toolBar.SetEnable("barSetDangerous", true);
                if (listData.IsDangerous)
                    toolBar.SetText("barSetDangerous", LocalData.IsEnglish ? "Cancel(&C)" : "取消危险设置(&C)");
                else
                    toolBar.SetText("barSetDangerous", LocalData.IsEnglish ? "Set to dangerous(&S)" : "设为危险客户(&S)");
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            this._mainListPart.CurrentChanged -= this._mainListPart_CurrentChanged;
            this._mainListPart.CurrentChanging -= this._mainListPart_CurrentChanging;
            this._mainListPart.Selected -= this._mainListPart_Selected;
            this._mainSearchPart.OnSearched -= this._mainSearchPart_OnSearched;
            
            this._combineListEditPart = null;
            this._contactListEditPart = null;
            this._invoiceTitlePart = null;
            this._mainListPart = null;
            this._mainSearchPart = null;
            this._mainToolBar = null;
            this._memoListEditPart = null;
            this._partnerListEditPart = null;
            this._workItem = null;
        }

        #endregion
    }
}
