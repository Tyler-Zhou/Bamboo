//using System;
//using System.Linq;
//using System.Xml.Linq;
//using ICP.EDI.ServiceInterface.DataObjects;

//namespace ICP.EDI.ServiceComponent
//{
//    /// <summary>
//    /// 已过时，转到由从数据库中去查找
//    /// </summary>
//    [Obsolete("已过时，转到由从数据库中去查找")]
//    public class ConfigHelper
//    {
//        public static ConfigItem  GetConfig(string path,string carrier,System.Guid companyId)
//        {

//            string filename = path + "\\EDIConfig.xml";
//            //加载配置
//            XDocument doc = XDocument.Load(filename);

//            var rs = (from e in doc.Element("Services").Elements("Service")
//                      select e).ToList();

//            var results = (from e in doc.Element("Services")
//                           .Elements("Service")
//                           where e.Element("Key").Value.ToUpper().Contains(carrier.ToUpper())
//                           select e).FirstOrDefault();

//            if (results == null) return null;

//            //通过反射加载对应的edi处理模块

//            ConfigItem config = new ConfigItem();
//            config.Carrier = carrier;

//            System.Collections.Generic.IEnumerable<XElement> companys= results.Element("Companys").Elements("Company");
//            XElement defaultElement = companys.FirstOrDefault(e => e.Attribute("Id").Value.ToLower() == companyId.ToString().ToLower());
//            if (defaultElement == null && companys.Count()>0)
//            {
//                defaultElement = companys.FirstOrDefault();
//            }

//            if (defaultElement == null)
//            {
//                throw new System.Exception("请正确配置Companys/Company");
//            }

//            //如果是通过邮件发送Edi取出邮件地址用于发送EMail
//            XElement emailNode = defaultElement.Element("EMail");
//            if (emailNode != null)
//            {
//                config.EMail = new EMailItem();
//                config.EMail.ServerAddress = emailNode.Element("ServerAddress").Value;
//                config.EMail.UserName = emailNode.Element("UserName").Value;
//                config.EMail.Password = emailNode.Element("PassWord").Value;
//                config.EMail.EMailAddress = emailNode.Element("EMailAddress").Value;
//            }

//            //如果是通过FTP发送EDI，则取出对应的FTP服务器地址，账号信息
//            var ftpnode = defaultElement.Element("FTP");
//            if (ftpnode != null)
//            {
//                config.Ftp = new FTPItem();
//                config.Ftp.Host = ftpnode.Element("Host").Value;
//                config.Ftp.Path = ftpnode.Element("Path").Value;
//                config.Ftp.UserName = ftpnode.Element("UserName").Value;
//                config.Ftp.Password = ftpnode.Element("PassWord").Value;
//            }


//            var modelnode = results.Element("Assembly");
//            if (modelnode != null)
//            {
//                config.Assembly = modelnode.Value;
//            }

//            var dataBaseNode = results.Element("DataBase");
//            if (dataBaseNode != null)
//            {
//                config.DataBase = new DataBaseItem();
//                config.DataBase.ConnectString = dataBaseNode.Element("ConnectString").Value;
//                config.DataBase.DataSchema = dataBaseNode.Element("DataSchema").Value;

//                XElement ruleElement = dataBaseNode.Element("RuleFile");
//                if (ruleElement != null)
//                {
//                    config.DataBase.RuleFile = ruleElement.Value;
//                }
//                config.DataBase.StoreProduce = dataBaseNode.Element("StoreProduce").Value;
//            }

//            var documentformartnode = results.Element("DocumentFormart");
//            if (documentformartnode != null)
//            {
//                config.EDIDocumentFormart = documentformartnode.Value;
//            }

//            return config;
//        }

//    }
//}
