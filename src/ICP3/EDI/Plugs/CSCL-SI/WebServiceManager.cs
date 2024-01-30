using System.Collections;
using Altova.Mapforce;

namespace CSCL.SI
{
    /// <summary>
    /// WebService服务调用接口
    /// </summary>
    public class WebServiceManager
    {
        public static IMFNode CallWebserviceParseStringToXml(IEnumerable childNodes)
        {
            string webServiceUrl = System.Configuration.ConfigurationSettings.AppSettings["WebServiceUrl"];
            if (string.IsNullOrEmpty(webServiceUrl))
            {
                webServiceUrl = "http://app.cityocean.com:83/EDIService.asmx";
            }
            else
            {
                webServiceUrl = webServiceUrl.ToLower().Replace("webservice.asmx", "EDIService.asmx");
            }
            IMFNode node = Altova.Xml.InternalXML.CallWebservice(new Altova.Mapforce.MFElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/", "", Altova.CoreTypes.castToEnumerable(Altova.Functions.Core.Box(new Altova.Mapforce.MFElement("Body", "http://schemas.xmlsoap.org/soap/envelope/", "", Altova.CoreTypes.castToEnumerable(Altova.Functions.Core.Box(new Altova.Mapforce.MFElement("ParseStringToXml", "http://www.CityOcean.com/", "", childNodes))))))), Altova.CoreTypes.CastToString("EDIService"), Altova.CoreTypes.CastToString("ParseStringToXml"), Altova.CoreTypes.CastToQName("{http://www.CityOcean.com/}ParseStringToXml"), Altova.CoreTypes.CastToString("http://www.CityOcean.com/ParseStringToXml"), Altova.CoreTypes.CastToString(webServiceUrl), Altova.CoreTypes.CastToString("document"), Altova.CoreTypes.CastToString(""), Altova.CoreTypes.CastToString(""), Altova.CoreTypes.CastToInt("0"), Altova.CoreTypes.CastToString(""), Altova.CoreTypes.CastToString(""), Altova.CoreTypes.CastToInt("1"));
            return node;
        }


        public static IMFNode CallWebserviceGetGoodsInfo(IEnumerable childNodes)
        {

            string webServiceUrl = System.Configuration.ConfigurationSettings.AppSettings["WebServiceUrl"];
            if (string.IsNullOrEmpty(webServiceUrl))
            {
                webServiceUrl = "http://app.cityocean.com:83/EDIService.asmx";
            }
            else
            {
                webServiceUrl = webServiceUrl.ToLower().Replace("webservice.asmx", "EDIService.asmx");
            }

            IMFNode node = Altova.Xml.InternalXML.CallWebservice(new Altova.Mapforce.MFElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/", "", Altova.CoreTypes.castToEnumerable(Altova.Functions.Core.Box(new Altova.Mapforce.MFElement("Body", "http://schemas.xmlsoap.org/soap/envelope/", "", Altova.CoreTypes.castToEnumerable(Altova.Functions.Core.Box(new Altova.Mapforce.MFElement("GetGoodsInfo", "http://www.CityOcean.com/", "", childNodes))))))), Altova.CoreTypes.CastToString("EDIService"), Altova.CoreTypes.CastToString("GetGoodsInfo"), Altova.CoreTypes.CastToQName("{http://www.CityOcean.com/}GetGoodsInfo"), Altova.CoreTypes.CastToString("http://www.CityOcean.com/GetGoodsInfo"), Altova.CoreTypes.CastToString(webServiceUrl), Altova.CoreTypes.CastToString("document"), Altova.CoreTypes.CastToString(""), Altova.CoreTypes.CastToString(""), Altova.CoreTypes.CastToInt("0"), Altova.CoreTypes.CastToString(""), Altova.CoreTypes.CastToString(""), Altova.CoreTypes.CastToInt("1"));
            return node;
        }
    }
}
