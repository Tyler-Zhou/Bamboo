using System;
using System.Collections.Generic;
using ICP.Message.ServiceInterface;
using uf = ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Common.UI.Configure.CommpanyConfigure
{
    /// <summary>
    /// 公司配置桥-管理各个面板之间的交互
    /// </summary>
    internal class CommpanyConfigureBridge : uf.IPartBridge,IDisposable
    {
        #region 服务

        private WorkItem WorkItem { get; set; }

        /// <summary>
        /// 公司配置服务
        /// </summary>
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        public IFaxService FaxService
        {
            get
            {
                return ServiceClient.GetService<IFaxService>();
            }
        }
        #endregion

        #region 本地属性

        uf.IToolBar _mainToolBar;  //工具栏
        uf.IListPart _mainListPart; //主列表
        uf.ISearchPart _mainSearchPart; //查询面板
        CommpanyConfigureEditPart _editPart; //编辑面板
        uf.IListEidtPart _ediEditPart;  //EDI配置面板
        uf.IListEidtPart _reportEditPart;  //报表格式配置面板
        uf.IListEidtPart _faxEditPart;

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
            WorkItem = context.WorkItem;
            _mainToolBar = (uf.IToolBar)context.Controls[typeof(CommpanyConfigureToolBarPart).Name];
            _mainSearchPart = (uf.ISearchPart)context.Controls[typeof(CompanyConfigureSearchPart).Name];
            _mainListPart = (uf.IListPart)context.Controls[typeof(CommpanyConfigureListPart).Name];
            _editPart = (CommpanyConfigureEditPart)context.Controls[typeof(CommpanyConfigureEditPart).Name];
            _ediEditPart = (uf.IListEidtPart)context.Controls[typeof(CommpanyEDIConfigureListPart).Name];
            _reportEditPart = (uf.IListEidtPart)context.Controls[typeof(CommpanyReportConfigureListPart).Name];
            _faxEditPart = (uf.IListEidtPart)context.Controls[typeof(CompanyFaxConfigureListPart).Name];
            //交互事件
            _mainSearchPart.OnSearched += new ICP.Framework.ClientComponents.UIFramework.SearchResultHandler(_mainSearchPart_OnSearched);
            _mainListPart.CurrentChanged += new ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler(_mainListPart_CurrentChanged);
            _mainListPart.CurrentChanging += new System.ComponentModel.CancelEventHandler(_mainListPart_CurrentChanging);
            _editPart.Saved += new SavedHandler(_editPart_Saved);

            //List<ConfigureList> list = ConfigureService.GetConfigureListByList(null, null, true, 0);
            //_mainListPart.DataSource = list;
            //if (list != null && list.Count > 0)
            //{
            //    _mainListPart_CurrentChanged(null, list[0]);
            //}
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

        /// <summary>
        /// 添加公司
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommpanyConfigureConstants.CMD_AddCommpany)]
        public void CMD_AddCommpany(object s, EventArgs e)
        {
            //if (_mainListPart.Current != null)
            //{
            //    object[] para = _editPart.BeforeParentChanged();
            //    if (para != null)
            //    {
            //        if (para[0] != null && !(bool)para[0])
            //        {
            //            return;
            //        }

            //        if (para[1] != null && (bool)para[1])
            //        {
            //            _mainListPart.RemoveItem(0);
            //        }
            //    }
            //}

            _editPart.Focus();// Added by Royal at 2011-07-02 20:26

            ConfigureInfo newData = new ConfigureInfo();
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            newData.IsDirty = false;
            newData.IsValid = true;
            newData.ChargingClosingdate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Date.AddMonths(1);
            newData.AccountingClosingdate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Date.AddMonths(1);
            _mainListPart.InsertItem(0, newData);
        }

        #endregion

        #region 各个部件之间的交互逻辑

        void _mainListPart_CurrentChanging(object sender, System.ComponentModel.CancelEventArgs e)
        {
            object[] para = _editPart.BeforeParentChanged();
            if (para != null)
            {
                if (para[0] != null && !(bool)para[0])
                {
                    e.Cancel = true;
                }

                if (para[1] != null && (bool)para[1])
                {
                    _mainListPart.RemoveItem(0);
                }
            }
        }

        void _mainListPart_CurrentChanged(object sender, object data)
        {
            ConfigureList configure = (ConfigureList)data;
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("ConfigureList", configure);
            #region toolBar
            RefreshBarEnabled(_mainToolBar, configure);
            #endregion

            _editPart.Init(values);
            _ediEditPart.Init(values);
            _reportEditPart.Init(values);

            values.Add("ConfigureObjects", FaxService.GetConfigureInfoByCompanyID(configure.CompanyID));
            _faxEditPart.Init(values);
        }

        void _mainSearchPart_OnSearched(object sender, object results)
        {
            _mainListPart.DataSource = results;
        }

        void _editPart_Saved(params object[] prams)
        {
            if (_mainListPart.Current == null || prams == null) return;

            ConfigureInfo info = prams[0] as ConfigureInfo;
            ConfigureList currentRow = _mainListPart.Current as ConfigureList;
            ICP.Framework.CommonLibrary.Helper.ObjectHelper.CopyData(info, currentRow);
            //_mainListPart_CurrentChanged(null, currentRow);          
        }

        #endregion

        private void RefreshBarEnabled(IToolBar toolBar, ConfigureList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barDisuse", false);
            }
            else
            {
                toolBar.SetEnable("barDisuse", true);
                if (listData.IsValid)
                    toolBar.SetText("barDisuse", LocalData.IsEnglish ? "Disuse(&D)" : "作废(&D)");
                else
                    toolBar.SetText("barDisuse", LocalData.IsEnglish ? "Available(&D)" : "激活(&D)");

            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            this._editPart.Saved -= this._editPart_Saved;
            this._ediEditPart = null;
            
            this._faxEditPart = null;
            this._mainListPart.CurrentChanged -= this._mainListPart_CurrentChanged;
            this._mainListPart.CurrentChanging -= this._mainListPart_CurrentChanging;
            this._mainSearchPart.OnSearched -= this._mainSearchPart_OnSearched;
            this._mainListPart = null;
            this._mainSearchPart = null;
            
            this._mainToolBar = null;
            this._reportEditPart = null;
            if (this.WorkItem != null)
            {
                this.WorkItem.Items.Remove(this);
                this.WorkItem = null;
            }
        }

        #endregion
    }
}
