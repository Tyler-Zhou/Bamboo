using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LongWin.ReportCenter.ServiceInterface
{
    [Serializable]
    public class ReportServerInfo
    {
        string _reportUrl;

        public string ReportUrl
        {
            get { return _reportUrl; }
            set { _reportUrl = value; }
        }
        string _reportUser;

        public string ReportUser
        {
            get { return _reportUser; }
            set { _reportUser = value; }
        }
        string _reportUserPSW;

        public string ReportUserPSW
        {
            get { return _reportUserPSW; }
            set { _reportUserPSW = value; }
        }


        public ReportServerInfo(string reportUrl
           , string reportUser
           , string reportUserPSW)
        {
            _reportUrl = reportUrl;
            _reportUser = reportUser;
            _reportUserPSW = reportUserPSW;
        }

    }
}
