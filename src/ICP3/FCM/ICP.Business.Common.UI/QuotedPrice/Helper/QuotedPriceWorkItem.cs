using System.Collections.Generic;
using ICP.Business.Common.UI.Document;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.Business.Common.UI.QuotedPrice
{
    /// <summary>
    /// WorkItem
    /// </summary>
    public class QuotedPriceWorkItem : WorkItem
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
        /// 
        /// </summary>
        private void Show()
        {
            QuotedPriceMainWorkspace qpMainWorkspace = SmartParts.Get<QuotedPriceMainWorkspace>(QuotedPriceConstants.QuotedPriceMainWorkspace);
            if (qpMainWorkspace == null)
            {
                qpMainWorkspace = SmartParts.AddNew<QuotedPriceMainWorkspace>(QuotedPriceConstants.QuotedPriceMainWorkspace);

                #region AddPart
                //Tool
                QuotedPriceToolBar qpToolBar = SmartParts.AddNew<QuotedPriceToolBar>();
                IWorkspace toolBarWorkspace = Workspaces[QuotedPriceConstants.QuotedPriceToolBar];
                toolBarWorkspace.Show(qpToolBar);

                //Search part
                QuotedPriceSearchPart qpSearchPart = SmartParts.AddNew<QuotedPriceSearchPart>();
                IWorkspace searchWorkspace = Workspaces[QuotedPriceConstants.QuotedPriceSearchPart];
                searchWorkspace.Show(qpSearchPart);

                //List Part
                QuotedPriceListPart qpMainListPart = SmartParts.AddNew<QuotedPriceListPart>();
                IWorkspace listWorkspace = Workspaces[QuotedPriceConstants.QuotedPriceListPart];
                listWorkspace.Show(qpMainListPart);

                //Rates List Part
                QuotedPriceRatesPart qpRatesListPart = SmartParts.AddNew<QuotedPriceRatesPart>();
                qpRatesListPart.IsView = true;
                IWorkspace editPartWorkspace = Workspaces[QuotedPriceConstants.QuotedPriceRatesListPart];
                editPartWorkspace.Show(qpRatesListPart);

                //Communication Part
                QuotedPriceCommunicationPart qpCommunicationPart = SmartParts.AddNew<QuotedPriceCommunicationPart>();
                IWorkspace communicationPartWorkspace = Workspaces[QuotedPriceConstants.QuotedPriceCommunicationPart];
                communicationPartWorkspace.Show(qpCommunicationPart);

                //DocumentList Part
                UCDocumentList documentListPart = SmartParts.AddNew<UCDocumentList>();
                IWorkspace documentListPartWorkspace = Workspaces[QuotedPriceConstants.QuotedPriceDocumentListPart];
                documentListPartWorkspace.Show(documentListPart);
                #endregion

                QuotedPriceUIAdapter uiAdapter = Items.AddNew<QuotedPriceUIAdapter>();
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {qpToolBar.GetType().Name, qpToolBar},
                    {qpSearchPart.GetType().Name, qpSearchPart},
                    {qpMainListPart.GetType().Name, qpMainListPart},
                    {qpRatesListPart.GetType().Name, qpRatesListPart},
                    {qpCommunicationPart.GetType().Name, qpCommunicationPart},
                    {documentListPart.GetType().Name, documentListPart},
                };

                uiAdapter.InitPart(dic);

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo {Title = LocalData.IsEnglish ? "Quoted Price" : "报价管理"};
                mainWorkspace.Show(qpMainWorkspace, smartPartInfo);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(qpMainWorkspace);
            }
        }

        #endregion
    }
}
