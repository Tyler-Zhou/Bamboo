using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.Operation.Common.ServiceInterface
{
    public class DefaultBusinessSpecialConditioner : IBusinessSpecialConditioner
    {
        #region IBusinessSpecialConditioner 成员

        public bool QueryDataFired(IBaseBusinessPart_New baseBusinessPart, string templateCode, string queryString)
        {
            return true;
        }

        public bool IsUnknownBusinessPart(string templateCode)
        {
            return false;
        }

        public void BarItemVisibility(DevExpress.XtraBars.BarManager baseBarManager, DevExpress.XtraBars.BarItemVisibility visibility)
        {
         
        }

        #endregion
    }
}
