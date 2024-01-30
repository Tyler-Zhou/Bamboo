using System.Collections.Generic;

namespace ICP.Common.ServiceInterface.DataObjects
{
    /// <summary>
    /// 国家口岸List集合
    /// </summary>
    public class CountryPortList
    {
        public List<ShippingCountryInfo> Country
        {
            get;set;
        }

        public List<ShippingPortInfo> Port
        {
            get;
            set;
        }
    }
}
