using System.Collections.Generic;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FRM.UI.SearchRate
{
    public class ClientSearchAirRateList : SearchAirRateList
    {
        #region Rates

        public decimal? Rate_45 { get; set; }
        public decimal? Rate_100 { get; set; }
        public decimal? Rate_300 { get; set; }
        public decimal? Rate_500 { get; set; }
        public decimal? Rate_800 { get; set; }

        public decimal? Rate_1000 { get; set; }
        public decimal? Rate_1300 { get; set; }

        #endregion
    }

    public class SearchPriceTransformHelper
    {
        public static ClientSearchAirRateList TransformS2C(SearchAirRateList orgList, ref Dictionary<string,decimal> units)
        {
            if (orgList == null) return null;

            ClientSearchAirRateList newItem = new ClientSearchAirRateList();
            Utility.CopyToValue(orgList, newItem, typeof(SearchAirRateList));

            #region BulidRate
            foreach (var item in orgList.UnitList)
            {
                switch (item.UnitName)
                {
                    case "+45": newItem.Rate_45 = item.Rate; break;
                    case "+100": newItem.Rate_100 = item.Rate; break;
                    case "+300": newItem.Rate_300 = item.Rate; break;
                    case "+500": newItem.Rate_500 = item.Rate; break;
                    case "+800": newItem.Rate_800 = item.Rate; break;
                    case "+1000": newItem.Rate_1000 = item.Rate; break;
                    case "+1300": newItem.Rate_1300 = item.Rate; break;
                }

                if (units.ContainsKey(item.UnitName) == false) units.Add(item.UnitName, item.Rate);

                if(item.Rate>0)
                {
                    if (units[item.UnitName] == -1 || units[item.UnitName] > item.Rate) units[item.UnitName] = item.Rate;
                }
            }
            #endregion

            return newItem;

        }

        public static PageList TransformS2C(PageList orgList, ref Dictionary<string, decimal> units)
        {

            List<ClientSearchAirRateList> list = TransformListS2C(orgList.GetList<SearchAirRateList>(), ref units);
            if (list == null) return null;
             PageList pageList = PageList.Create<ClientSearchAirRateList>(list, orgList.DataPageInfo);
             return pageList;          
        }


        public static List<ClientSearchAirRateList> TransformListS2C(List<SearchAirRateList> orgList, ref Dictionary<string, decimal> units)
        {
 
            if (orgList == null) return null;

            List<ClientSearchAirRateList> list = new List<ClientSearchAirRateList>();
            foreach (var item in orgList)
            {
                ClientSearchAirRateList clientItem = TransformS2C(item, ref  units);
                if (clientItem != null) list.Add(clientItem);
            }

            return list;
        }
    }
}
