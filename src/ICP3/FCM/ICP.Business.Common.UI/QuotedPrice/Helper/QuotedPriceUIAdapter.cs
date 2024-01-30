using System;
using System.Collections.Generic;
using ICP.Business.Common.UI.Document;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;

namespace ICP.Business.Common.UI.QuotedPrice
{
    /// <summary>
    /// 报价界面适配器
    /// </summary>
    public class QuotedPriceUIAdapter : IDisposable
    {
        #region Services & Fields

        #region Services
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }

        /// <summary>
        /// 报价服务
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        #endregion

        #region Fields
        /// <summary>
        /// 工具栏
        /// </summary>
        IToolBar _QPToolBar;
        /// <summary>
        /// 快速搜索面板
        /// </summary>
        QuotedPriceFastSearchPart _QPFSearchPart;
        /// <summary>
        /// 查询面板
        /// </summary>
        ISearchPart _QPSearchPart;
        /// <summary>
        /// 
        /// </summary>
        QuotedPriceListPart _QPListPart;
        /// <summary>
        /// 报价价格面板
        /// </summary>
        QuotedPriceRatesPart _QPERatesList;
        /// <summary>
        /// 沟通面板
        /// </summary>
        QuotedPriceCommunicationPart _QPCommunicationPart;

        /// <summary>
        /// 文档面板
        /// </summary>
        UCDocumentList _QPDocumentListPart;

        #endregion

        #endregion

        #region Init
        /// <summary>
        /// 初始化面板
        /// </summary>
        /// <param name="controls">控件集合</param>
        public void InitPart(Dictionary<string, object> controls)
        {
            _QPToolBar = (IToolBar)controls[typeof(QuotedPriceToolBar).Name];
            _QPSearchPart = (ISearchPart)controls[typeof(QuotedPriceSearchPart).Name];
            _QPListPart = (QuotedPriceListPart)controls[typeof(QuotedPriceListPart).Name]; ;
            _QPERatesList = (QuotedPriceRatesPart)controls[typeof(QuotedPriceRatesPart).Name]; ;
            _QPCommunicationPart = (QuotedPriceCommunicationPart)controls[typeof(QuotedPriceCommunicationPart).Name]; ;
            _QPDocumentListPart = (UCDocumentList)controls[typeof(UCDocumentList).Name]; ;
            BulidConnection();
        }
        #endregion

        #region Bar Enabled
        /// <summary>
        /// 按钮可用状态设置
        /// </summary>
        /// <param name="toolBar"></param>
        /// <param name="listData"></param>
        private void RefreshBarEnabled(IToolBar toolBar, QuotedPriceOrderList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barDelete", false);
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barPrint", false);
            }
            else
            {
                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barPrint", true);
                toolBar.SetEnable("barDelete", listData.IsValid);
            }
        } 
        #endregion

        #region Connection
        private void BulidConnection()
        {
            RefreshBarEnabled(_QPToolBar, null);

            #region CurrentChanged
            _QPListPart.CurrentChanged += (sender,data)=> ListChanged(data);
            #endregion

            #region Search Part OnSearched
            _QPSearchPart.OnSearched += (sender, results) =>
            {
                _QPListPart.DataSource = results;
            };
            #endregion
        }

        private void ListChanged(object data)
        {
            QuotedPriceOrderList listData = data as QuotedPriceOrderList;
            Dictionary<string, object> keyValue = new Dictionary<string, object> {{"ParentList", data}};

            if (listData != null)
            {
                BusinessOperationContext context = new BusinessOperationContext
                {
                    OperationID = listData.ID,
                    FormId = listData.ID,
                    FormType = FormType.QuotedPrice,
                    OperationType = OperationType.QuotedPrice
                };
                //Rates List
                _QPERatesList.DataBind(context);
                //Communication
                _QPCommunicationPart.DataBind(context);
                //Document List
                _QPDocumentListPart.DataBind(context);
            }

            #region toolBar

            RefreshBarEnabled(_QPToolBar, listData);

            #endregion

        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _QPListPart = null;
            _QPSearchPart = null;
            _QPToolBar = null;
            if (RootWorkItem != null)
            {
                RootWorkItem.Items.Remove(this);
                RootWorkItem = null;
            }
        }

        #endregion
    }
}
