using System;
using System.ComponentModel;

namespace ICP.Common.UI.CC
{
    public class CCUIAdapter:IDisposable
    {
        #region 变量
        CCMainWorkspace _CCMainWorkspace;
        CCToolBar _CCToolBar;
        CCSearchPart _CCSearchPart;
        CCListPart _CCListPart;
        CCArchivesPart _CCCustomerInfoPart;
        CCPartnerPart _CCPartnerPart;
        CCBusinessPart _CCBusinessPart;
        CCReportPart _CCReportPart;
        #endregion

        internal void InitPart(CCMainWorkspace crmMainWorkspace
                           , CCToolBar crmToolBar
                           , CCSearchPart crmSearchPart
                           , CCListPart crmMainListPart
                           , CCArchivesPart crmCustomerInfoPart
                           , CCPartnerPart crmPartnerPart
                           , CCBusinessPart crmBusinessPart
                           , CCReportPart crmReportPart)
        {
            _CCMainWorkspace = crmMainWorkspace;
            _CCToolBar = crmToolBar;
            _CCSearchPart = crmSearchPart;
            _CCListPart = crmMainListPart;

            _CCCustomerInfoPart = crmCustomerInfoPart;
            _CCPartnerPart = crmPartnerPart;
            _CCBusinessPart = crmBusinessPart;
            _CCReportPart = crmReportPart;
            BulidConnection();

        }

        private void BulidConnection()
        {
            #region Connection

            #region CurrentChanging
            _CCListPart.CurrentChanging += delegate(object sender, CancelEventArgs e)
            {
                e.Cancel = false ;
            };
            #endregion

            #region OnSearched
            _CCSearchPart.OnSearched += delegate(object sender, object results)
            {
                _CCListPart.DataSource = results;
            };
            #endregion

            #endregion
        }





        #region IDisposable 成员

        public void Dispose()
        {
            this._CCBusinessPart = null;
            this._CCCustomerInfoPart = null;
            this._CCListPart = null;
            this._CCMainWorkspace = null;
            this._CCPartnerPart = null;
            this._CCReportPart = null;
            this._CCSearchPart = null;
            this._CCToolBar = null;
            
        }

        #endregion
    }
}
