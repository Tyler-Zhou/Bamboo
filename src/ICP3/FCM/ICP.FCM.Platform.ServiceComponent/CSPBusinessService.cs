using ICP.FCM.Common.ServiceInterface.Interfaces;
using ICP.FCM.Platform.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.Platform.ServiceComponent
{
    /// <summary>
    /// 
    /// </summary>
    public class CSPBusinessService : ICSPBusinessService
    {
        public List<CSPBookingInfo> GetALLCSPBookingList(SearchParameterBooking searchParameter)
        {
            SSOToken token=PlatformAPIHelper.GetSSOToken("admin", "123qwe");
            StringBuilder sb=new StringBuilder();
            sb.AppendFormat("BookingStatus={0}", searchParameter.BookingStatus);
            if (!searchParameter.SearchKey.IsNullOrEmpty())
            {
                sb.AppendFormat("&SearchKey={0}", searchParameter.SearchKey);
            }
            if (!searchParameter.Sorting.IsNullOrEmpty())
            {
                sb.AppendFormat("&Sorting={0}", searchParameter.Sorting);
            }
            if (searchParameter.MaxResultCount > 0)
            {
                sb.AppendFormat("&MaxResultCount={0}", searchParameter.MaxResultCount);
            }
            if (searchParameter.SkipCount > 0)
            {
                sb.AppendFormat("&SkipCount={0}", searchParameter.SkipCount);
            }
            string result = PlatformAPIHelper.GetData(token, "GET", "CSP/Booking/GetAllList", sb.ToString());
            List<CSPBookingInfo> resultList = JSONSerializerHelper.DeserializeFromJson<List<CSPBookingInfo>>(result);
            return resultList;
        }

    }
}
