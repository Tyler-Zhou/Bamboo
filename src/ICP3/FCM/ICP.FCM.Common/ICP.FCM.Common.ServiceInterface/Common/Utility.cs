using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    public class FCMInterfaceUtility
    {

        private static Dictionary<OperationType, List<ContactStageInfo>> stageInfoList = new Dictionary<OperationType, List<ContactStageInfo>>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public static List<ContactStageInfo> GetStageInfoSource(OperationType operationType)
        {
            lock (stageInfoList)
            {
                if (stageInfoList.ContainsKey(operationType))
                {
                    return stageInfoList[operationType];
                }
                else
                {


                    List<ContactStage> stageList = GetContactStage(operationType);
                    List<ContactStageInfo> list = new List<ContactStageInfo>();
                    foreach (ContactStage stage in stageList)
                    {
                        ContactStageInfo stageInfo = new ContactStageInfo();
                        stageInfo.StageName = stage.ToString();
                        stageInfo.OperationType = operationType;
                        stageInfo.Stage = (int)stage;
                        list.Add(stageInfo);
                    }
                    stageInfoList.Add(operationType, list);

                    return list;
                }
            }

        }
        private static List<ContactStage> GetOceanExportContactStage()
        {
            return new List<ContactStage> { ContactStage.SO, ContactStage.Trk, ContactStage.CF, ContactStage.FU, ContactStage.Whs, ContactStage.IN, ContactStage.SI, ContactStage.BL, ContactStage.AR, ContactStage.Release };
        }
        private static List<ContactStage> GetOceanImportContactStage()
        {
            return new List<ContactStage> { ContactStage.AN, ContactStage.Trk, ContactStage.CF, ContactStage.Whs, ContactStage.BL };
        }
        private static List<ContactStage> GetInquireRateStage()
        {
            return new List<ContactStage> { ContactStage.IQ };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public static List<ContactStage> GetContactStage(OperationType operationType)
        {
            if (operationType == OperationType.OceanExport)
            {
                return GetOceanExportContactStage();
            }
            else if (operationType == OperationType.OceanImport)
            {
                return GetOceanImportContactStage();
            }
            else if (operationType == OperationType.InquireRate)
            {
                return GetInquireRateStage();
            }
            else
                throw new ICPException("ICP.FCM.Common.ServiceInterface.Utility.GetContactStage方法还未提供除了海进和海出的沟通阶段定义");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceStage"></param>
        /// <param name="childStage"></param>
        /// <returns></returns>
        public static bool IsContainsStage(string sourceStage, ContactStage childStage)
        {
            if (string.IsNullOrEmpty(sourceStage) && childStage == ContactStage.Unknown)
            {
                return true;
            }
            List<string> sourceList = sourceStage.Split(',').ToList();
            List<string> temp = new List<string>();
            sourceList.ForEach(item => temp.Add(item.Trim()));
            List<string> childList = GetStageName(childStage).Split(',').ToList();
            bool result = true;
            foreach (string child in childList)
            {
                if (!temp.Contains(child.Trim()))
                {
                    result = false;
                    break;
                }
            }
            return result;

        }

        /// <summary>
        /// 将DataTable的一行转换成Table
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public static DataTable SingleRowAsTable(DataTable dt, int rowIndex)
        {
            return SingleRowAsTable(dt.Rows[rowIndex]);
        }
        /// <summary>
        /// 将DataRow转换成DataTable        
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static DataTable SingleRowAsTable(DataRow row)
        {
            DataTable newTable = new DataTable();
            DataColumnCollection columnCollection = row.Table.Columns;
            int count = columnCollection.Count;
            for (int i = 0; i < count; i++)
                newTable.Columns.Add(columnCollection[i].ColumnName, columnCollection[i].DataType);
            newTable.ImportRow(row);

            return newTable;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="stageNames"></param>
        public static void SetStage(CustomerCarrierObjects contact, string stageNames)
        {
            if (string.IsNullOrEmpty(stageNames))
                return;
            List<string> names = stageNames.Split(',').ToList();
            ContactStage stages = ContactStage.Unknown;
            foreach (string name in names)
            {
                ContactStage temp = (ContactStage)Enum.Parse(typeof(ContactStage), name.Trim(), true);
                stages |= temp;
            }
            SetStage(contact, stages);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactStage"></param>
        /// <returns></returns>
        public static string GetStageName(ContactStage contactStage)
        {

            if (contactStage == ContactStage.Unknown)
            {
                return string.Empty;
            }
            List<string> enumNames = GetContactStageEnumNames();
            StringBuilder build = new StringBuilder();
            foreach (string name in enumNames)
            {
                ContactStage temp = (ContactStage)(Enum.Parse(typeof(ContactStage), name, true));
                if ((contactStage & temp) == temp)
                {
                    build.Append(temp.ToString());
                    build.Append(", ");
                }
            }
            if (build.Length > 0)
            {
                return build.ToString().Substring(0, build.Length - 2);
            }
            else
                return string.Empty;

        }

        private static List<string> _ContactStageName;
        private static List<string> ContactStageNames
        {
            get
            {
                if (_ContactStageName == null)
                {
                    List<string> enumNames = Enum.GetNames(typeof(ContactStage)).ToList();
                    enumNames.Remove(ContactStage.Unknown.ToString());
                    _ContactStageName = enumNames;
                }
                return _ContactStageName;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private static PropertyInfo[] _ContactProperties;
        /// <summary>
        /// 
        /// </summary>
        public static PropertyInfo[] ContactProperties
        {
            get
            {
                if (_ContactProperties == null)
                {
                    List<string> enumNames = GetContactStageEnumNames();
                    _ContactProperties =
                          typeof(CustomerCarrierObjects).GetProperties().Where(p => enumNames.Contains(p.Name)).ToArray();
                }

                return _ContactProperties;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<string> GetContactStageEnumNames()
        {
            return ContactStageNames;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactInfo"></param>
        /// <param name="stage"></param>
        public static void SetStage(CustomerCarrierObjects contactInfo, ContactStage stage)
        {
            SetStage(contactInfo, new ContactStage[1] { stage });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactInfo"></param>
        /// <param name="contactStages"></param>
        public static void SetStage(CustomerCarrierObjects contactInfo, ContactStage[] contactStages)
        {
            PropertyInfo[] properties = ContactProperties;
            StringBuilder builder = new StringBuilder();
            int length = contactStages.Length;
            foreach (PropertyInfo property in properties)
            {
                ContactStage temp = (ContactStage)Enum.Parse(typeof(ContactStage), property.Name, true);

                for (int i = 0; i < length; i++)
                {
                    bool result = (contactStages[i] & temp) == temp;
                    property.SetValue(contactInfo, ((contactStages[i] & temp) == temp), null);
                    if (result)
                    {
                        builder.Append((int)temp);
                        builder.Append(", ");
                    }
                }
            }
            if (builder.Length > 0)
            {
                contactInfo.Stage = builder.ToString().Substring(0, builder.Length - 2);
            }
            else
            {
                contactInfo.Stage = string.Empty;
            }
        }

        /// <summary>
        /// 追加联系人
        /// </summary>
        /// <param name="contactInfo"></param>
        /// <param name="contactStages"></param>
        public static void AppendContactStage(CustomerCarrierObjects contactInfo, ContactStage?[] contactStages)
        {
            string oldStage = contactInfo.Stage;
            var strBuf = new StringBuilder();
            Array.ForEach(contactStages, item =>
                {
                    if (item.HasValue && item.Value != ContactStage.Unknown)
                    {
                        PropertyInfo propertyInfo = ContactProperties.FirstOrDefault(c => c.Name.Equals(item.ToString()));
                        propertyInfo.SetValue(contactInfo, true, null);
                        strBuf.Append(string.Format("{0}, ", ((int)item).ToString()));
                    }
                });

            string newStage = string.Empty;
            if (strBuf.Length > 2)
            {
                newStage = strBuf.ToString(0, strBuf.Length - 2);
            }
            //去除重复的阶段
            if (!string.IsNullOrEmpty(oldStage))
            {
                string[] arrStages = oldStage.Split(new char[1] { ',' });
                if (arrStages != null)
                {
                    int length = arrStages.Length;
                    if (length > 0)
                    {
                        for (int i = 0; i < length; i++)
                        {
                            if (arrStages[i] != null && !string.IsNullOrEmpty(arrStages[i].Trim()))
                            {
                                newStage = Regex.Replace(newStage, arrStages[i].Trim(), "", RegexOptions.IgnoreCase);
                            }
                        }
                    }
                }


                if (!string.IsNullOrEmpty(newStage))
                    contactInfo.Stage = string.Format("{0}, {1}", oldStage, newStage);
                else
                    contactInfo.Stage = oldStage;
            }
            else
            {
                contactInfo.Stage = newStage;
            }
        }


        /// <summary>
        /// 获取联系人实体中对应所有沟通阶段的字符串表示
        /// </summary>
        /// <param name="contactInfo"></param>
        /// <returns></returns>
        public static string GetStage(CustomerCarrierObjects contactInfo)
        {
            List<string> enumNames = GetContactStageEnumNames();
            Dictionary<string, bool> dic = new Dictionary<string, bool>();

            PropertyInfo[] properties = typeof(CustomerCarrierObjects).GetProperties().Where(p => enumNames.Contains(p.Name)).ToArray();
            foreach (PropertyInfo property in properties)
            {
                dic.Add(property.Name, (bool)property.GetValue(contactInfo, null));
            }
            StringBuilder build = new StringBuilder();
            foreach (KeyValuePair<string, bool> pair in dic)
            {
                if (pair.Value)
                {
                    build.Append((int)(Enum.Parse(typeof(ContactStage), pair.Key)));
                    build.Append(", ");

                }
            }
            if (build.Length <= 0)
                return string.Empty;
            else
            {
                return build.ToString().Substring(0, build.Length - 2);
            }

        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            try
            {
                //序列化对象
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static object Deserialize(Type type, string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 创建一个客户承运人对象
        /// </summary>
        /// <param name="isSO"></param>
        /// <param name="isSI"></param>
        /// <param name="isBL"></param>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static CustomerCarrierObjects CreateCustomerCarrierInfo(bool isSO, bool isSI, bool isBL, string email, string name)
        {
            CustomerCarrierObjects customerCarrier = new CustomerCarrierObjects();
            customerCarrier.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            customerCarrier.CreateByID = LocalData.UserInfo.LoginID;
            customerCarrier.UpdateDate = null;
            customerCarrier.UpdateByID = LocalData.UserInfo.LoginID;
            customerCarrier.SO = isSO;
            customerCarrier.BL = isBL;
            customerCarrier.SI = isSI;
            customerCarrier.Mail = email;
            customerCarrier.Name = GetMailSenderName(name, email);

            return SetContactStage(customerCarrier, isSI, isBL, isSO);
        }
        /// <summary>
        /// 将邮件所有外部联系人转换成键值对形式存储
        /// </summary>
        /// <param name="contacts"></param>
        /// <returns></returns>
        public static string ConvertContactsToXml(List<CustomerCarrierObjects> contacts)
        {
            if (contacts == null || contacts.Count <= 0)
                return string.Empty;

            IEnumerator<CustomerCarrierObjects> enumeratorContacts = contacts.GetEnumerator();
            var strBuf = new StringBuilder();
            while (enumeratorContacts.MoveNext())
            {
                CustomerCarrierObjects item = enumeratorContacts.Current;
                if (item != null)
                {
                    string dataXml = item.GetXmlDataNode();
                    strBuf.Append(dataXml);
                }
            }

            return strBuf.ToString();
        }

        /// <summary>
        /// 将邮件所有外部联系人地址转换成业务联系人
        /// </summary>
        /// <param name="mailContactInfos"></param>
        /// <returns></returns>
        public static List<CustomerCarrierObjects> ConvertContacts(List<MailContactInfo> mailContactInfos)
        {
            if (mailContactInfos == null || mailContactInfos.Count <= 0)
                return new List<CustomerCarrierObjects>();

            List<MailContactInfo> _mailContactInfos = mailContactInfos;
            int count = _mailContactInfos.Count;
            List<CustomerCarrierObjects> newContacts = new List<CustomerCarrierObjects>();
            for (int j = 0; j < count; j++)
            {
                CustomerCarrierObjects temp = CreateCustomerCarrierInfo(false, false, false, string.Empty, string.Empty);
                temp.Type = ContactType.Customer;
                temp.IsCC = (_mailContactInfos[j].ContactType == MailContactType.olCC);
                temp.Mail = _mailContactInfos[j].EmailAddress;
                temp.Name = _mailContactInfos[j].Name;

                newContacts.Add(temp);
                temp = null;
            }


            return newContacts;
        }

        /// <summary>
        /// 转换MailContactInfo到CustomerCarrierObjects
        /// </summary>
        /// <param name="mailContactInfos">List MailConatctInfo</param>
        /// <param name="businessContactType"></param>
        /// <returns></returns>
        public static List<CustomerCarrierObjects> ConvertCustomerCarrierObjectsFromContacts(List<MailContactInfo> mailContactInfos,int businessContactType)
        {
            if (mailContactInfos == null || mailContactInfos.Count <= 0)
                return new List<CustomerCarrierObjects>();

            List<MailContactInfo> _mailContactInfos = mailContactInfos;
            int count = _mailContactInfos.Count;
            List<CustomerCarrierObjects> newContacts = new List<CustomerCarrierObjects>();
            for (int j = 0; j < count; j++)
            {
                if (businessContactType == 3)
                {
                    CustomerCarrierObjects temp = CreateCustomerCarrierInfo(false, false, false, string.Empty, string.Empty);
                    temp.Type = ContactType.Customer;
                    temp.IsCC = (_mailContactInfos[j].ContactType == MailContactType.olCC);
                    temp.Mail = _mailContactInfos[j].EmailAddress;
                    temp.Name = _mailContactInfos[j].Name;

                    newContacts.Add(temp);
                    temp = null;

                    CustomerCarrierObjects temp2 = CreateCustomerCarrierInfo(false, false, false, string.Empty, string.Empty);
                    temp2.Type = ContactType.Carrier;
                    temp2.IsCC = (_mailContactInfos[j].ContactType == MailContactType.olCC);
                    temp2.Mail = _mailContactInfos[j].EmailAddress;
                    temp2.Name = _mailContactInfos[j].Name;

                    newContacts.Add(temp2);
                    temp = null;
                }
                else
                {
                    CustomerCarrierObjects temp = CreateCustomerCarrierInfo(false, false, false, string.Empty, string.Empty);
                    temp.Type = businessContactType == 1 ? ContactType.Customer : ContactType.Carrier;
                    temp.IsCC = (_mailContactInfos[j].ContactType == MailContactType.olCC);
                    temp.Mail = _mailContactInfos[j].EmailAddress;
                    temp.Name = _mailContactInfos[j].Name;

                    newContacts.Add(temp);
                    temp = null;
                }
            }


            return newContacts;
        }

        private static CustomerCarrierObjects SetContactStage(CustomerCarrierObjects customerCarrier, bool isSI, bool isBL, bool isSO)
        {
            List<ContactStage> contactStages = new List<ContactStage>();
            if (isSO)
            {
                contactStages.Add(ContactStage.SO);
            } if (isBL)
            {
                contactStages.Add(ContactStage.BL);
            } if (isSI)
            {
                contactStages.Add(ContactStage.SI);
            }
            if (contactStages.Count > 0)
            {
                SetStage(customerCarrier, contactStages.ToArray());
            }

            return customerCarrier;
        }

        /// <summary>
        /// 获取邮件地址名称
        /// </summary>
        /// <param name="senderName"></param>
        /// <param name="senderAddress"></param>
        /// <returns></returns>
        public static string GetMailSenderName(string senderName, string senderAddress)
        {
            string name = senderName;
            if (!string.IsNullOrEmpty(name))
            {
                if (name.StartsWith("'") && name.EndsWith("'"))
                {
                    name = name.Trim("'".ToCharArray());
                }

                if (name.Contains("@"))
                {
                    name = GetSplitValue(name, "@".ToCharArray());
                }

                if (name.Contains("-"))
                {
                    name = GetSplitValue(name, "-".ToCharArray());
                }

                if (name.Contains("("))
                {
                    name = GetSplitValue(name, "(".ToCharArray());
                }

                if (name.Contains("."))
                {
                    name = GetSplitValue(name, ".".ToCharArray());
                }
            }
            else
            {
                name = GetSplitValue(senderAddress, "@".ToCharArray());
            }

            return name;
        }

        private static string GetSplitValue(string name, char[] charArray)
        {
            string[] arrNames = name.Split(charArray);
            if (arrNames != null && arrNames.Length > 0)
                return arrNames[0];

            return name;
        }

        private static Dictionary<string, bool> dicMails = new Dictionary<string, bool>();
        /// <summary>
        /// 是否存在内部联系人
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static bool ExsitsInternalContact(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                dicMails.Add(address, true);
                return true;
            }


            string lowerAddress = address.ToLower();
            if (dicMails.ContainsKey(lowerAddress))
            {
                bool internalMail = false;
                dicMails.TryGetValue(lowerAddress, out internalMail);
                return internalMail;
            }

            if (lowerAddress.Contains("@"))
            {
                string[] splitAddress = lowerAddress.Split(new char[1] { '@' });
                if (splitAddress != null && splitAddress.Length == 2)
                {
                    bool internalMail = InternalDomains.Contains(splitAddress[1]);
                    dicMails.Add(lowerAddress, internalMail);
                    return internalMail;
                }
            }
            else
            {
                bool internalMail = InternalDomains.Contains(lowerAddress);
                dicMails.Add(lowerAddress, internalMail);
                return internalMail;
            }

            return true;
        }

        /// <summary>
        /// 构建关联信息实体
        /// </summary>
        /// <param name="operationMessageRelationID">关联GUID</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="operationID">业务操作ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="messageID">Mail MessageID</param>
        /// <param name="iMessageID">Mail Entry GUID</param>
        /// <param name="contactStage">沟通阶段</param>
        /// <returns></returns>
        public static OperationMessageRelation CreateOperationMessageRelationInfo(Guid operationMessageRelationID, DateTime? updateDate, Guid operationID
            , OperationType operationType, string messageID, Guid iMessageID, ContactStage? contactStage)
        {
            return CreateOperationMessageRelationInfo(operationMessageRelationID, updateDate, operationID, operationType,
                messageID, iMessageID, contactStage, "");
        }

        /// <summary>
        /// 构建关联信息实体
        /// </summary>
        /// <param name="operationMessageRelationID">关联GUID</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="operationID">业务操作ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="messageID">Mail MessageID</param>
        /// <param name="iMessageID">Mail Entry GUID</param>
        /// <param name="contactStage">沟通阶段</param>
        /// <param name="strEntryID">Mail Item EntryID</param>
        /// <returns></returns>
        public static OperationMessageRelation CreateOperationMessageRelationInfo(Guid operationMessageRelationID, DateTime? updateDate, Guid operationID
            , OperationType operationType, string messageID, Guid iMessageID, ContactStage? contactStage,string strEntryID)
        {
            return new OperationMessageRelation()
            {
                HasData = true,
                ID = operationMessageRelationID,
                UpdateDate = updateDate,
                UpdateBy = LocalData.UserInfo.LoginID,
                OperationID = operationID,
                OperationType = operationType,
                MessageId = messageID,
                IMessageId = iMessageID,
                ContactStage = contactStage,
                CreateDate = DateTime.Now,
                CreateBy = LocalData.UserInfo.LoginID,
                RelationType = MessageRelationType.Auto,
                EntryID=strEntryID,
                UpdateDataType = UpdateDataType.AddNew
            };
        }

        /// <summary>
        ///将邮件的联系人转换成联系人，如果业务中存在该联系人，则覆盖邮件的联系人
        /// </summary>
        /// <param name="messageInfo"></param>
        /// <param name="operationContext"></param>
        /// <param name="isSO"></param>
        /// <param name="isSI"></param>
        /// <param name="isBL"></param>
        /// <param name="sameContact"></param>
        /// <returns></returns>
        public static List<CustomerCarrierObjects> GetMailContacts(
            Message.ServiceInterface.Message messageInfo, BusinessOperationContext operationContext, bool isSO, bool isSI, bool isBL, ref bool sameContact)
        {
            return ConvertMailAddressToContactList(true, messageInfo, operationContext, isSO, isSI, isBL, ref sameContact);
        }

        /// <summary>
        /// 根据发件人获取联系人列表
        /// </summary>
        /// <returns></returns>
        public static List<OperationContactInfo> GetOperationContactListByEmails(List<string> emails)
        {
            if (emails != null && emails.Count > 0)
                return ServiceClient.GetService<IBusinessContactService>().GetOperationContactByEmails(emails);
            else
                return new List<OperationContactInfo>();
        }

        /// <summary>
        /// 将邮件的外部联系人和业务现有的联系人（追加联系人）
        /// </summary>
        /// <param name="messageInfo"></param>
        /// <param name="operationContext"></param>
        /// <param name="isSO"></param>
        /// <param name="isSI"></param>
        /// <param name="isBL"></param>
        /// <returns></returns>
        public static List<CustomerCarrierObjects> GetOperationContactsAndMailContacts(
            Message.ServiceInterface.Message messageInfo, BusinessOperationContext operationContext, bool isSO,
            bool isSI, bool isBL)
        {
            bool sameContact = false;
            return ConvertMailAddressToContactList(false, messageInfo, operationContext, isSO, isSI, isBL, ref sameContact);
        }
        /// <summary>
        /// 将邮件地址转换成联系人
        /// </summary>
        /// <param name="isCover"></param>
        /// <param name="message"></param>
        /// <param name="operationContext"></param>
        /// <param name="isSO"></param>
        /// <param name="isSI"></param>
        /// <param name="isBL"></param>
        /// <param name="sameContact"></param>
        /// <returns></returns>
        private static List<CustomerCarrierObjects> ConvertMailAddressToContactList(bool isCover, Message.ServiceInterface.Message message, BusinessOperationContext operationContext, bool isSO, bool isSI, bool isBL, ref bool sameContact)
        {
            List<CustomerCarrierObjects> customerList = new List<CustomerCarrierObjects>();
            List<bool> sameContacts = new List<bool>();
            //业务的联系人
            if (operationContext.OperationID != Guid.Empty)
            {
                ContactObjects list = ServiceClient.GetService<IFCMCommonService>().GetContactList(operationContext.OperationID, operationContext.OperationType);
                if (list != null && list.CustomerCarrier != null)
                {
                    customerList = list.CustomerCarrier;
                }
            }

            List<CustomerCarrierObjects> customerCarrierList = GetExternalMailToContactList(message, operationContext.OperationID, operationContext.OperationType, isSO, isSI, isBL);
            if (customerCarrierList != null && customerCarrierList.Count > 0)
            {
                int count = customerCarrierList.Count;
                //去除邮件中重复的联系人
                for (int i = 0; i < count; i++)
                {

                    CustomerCarrierObjects info =
                        customerList.Find(
                            cust => cust.Mail.Trim().Equals(customerCarrierList[i].Mail.Trim(), StringComparison.CurrentCultureIgnoreCase));

                    //如果业务联系人不存在该邮件的联系人，直接追加联系人
                    if (info == null)
                    {
                        customerList.Add(customerCarrierList[i]);
                        sameContacts.Add(false);
                    }
                    else
                    {
                        if (isCover)
                        {
                            customerCarrierList[i] = info;
                            sameContacts.Add(true);
                        }
                    }
                }
            }

            if (isCover)
            {
                //判断邮件的联系人信息是否与业务中的联系人信息完全匹配
                //如果完全匹配，就返回联系人集合
                if (!sameContacts.Contains(false))
                {
                    if (!customerCarrierList.Any(item => string.IsNullOrEmpty(item.Stage)))
                    {
                        sameContact = true;
                    }
                }


                return customerCarrierList;
            }
            else
                return customerList;
        }

        /// <summary>
        /// 将邮件的所有外部邮件联系人转换成业务联系人
        /// </summary>
        /// <param name="messageInfo"></param>
        /// <param name="operationID"></param>
        /// <param name="operationType"></param>
        /// <param name="isCustomer"></param>
        /// <param name="isCarrier"></param>
        /// <param name="isBL"></param>
        /// <returns></returns>
        public static List<CustomerCarrierObjects> GetExternalMailToContactList(Message.ServiceInterface.Message messageInfo, Guid operationID, OperationType operationType, bool isCustomer, bool isCarrier, bool isBL)
        {
            List<CustomerCarrierObjects> customerCarrierList = new List<CustomerCarrierObjects>();
            List<MailContactInfo> allMailContacts = MailContactInfo.Convert(messageInfo);
            int count = allMailContacts.Count;
            for (int i = 0; i < count; i++)
            {
                //判断是否包含@字符，为了过滤加密联系人
                if (!string.IsNullOrEmpty(allMailContacts[i].EmailAddress) && allMailContacts[i].EmailAddress.Contains("@"))
                {
                    string emailAddress = allMailContacts[i].EmailAddress.Trim();
                    if (!ExsitsInternalContact(emailAddress))
                    {

                        CustomerCarrierObjects entity = CreateCustomerCarrierInfo(isCustomer, isCarrier, isBL,
                                                                                  emailAddress,
                                                                                  allMailContacts[i].Name);
                        entity.OperationType = operationType;
                        entity.OceanBookingID = operationID;

                        if (allMailContacts[i].ContactType == MailContactType.olCC)
                            entity.IsCC = true;

                        customerCarrierList.Add(entity);
                    }
                }
            }

            //过滤重复联系人
            if (customerCarrierList.Count > 0)
                customerCarrierList = customerCarrierList.GroupBy(s => s.Mail.ToLower()).Select(s => s.FirstOrDefault()).ToList();

            return customerCarrierList;
        }


        private static string[] _InternalDomains;
        /// <summary>
        /// 
        /// </summary>
        public static string[] InternalDomains
        {
            get
            {
                if (_InternalDomains == null || _InternalDomains.Length == 0)
                {
                    string internalDomain = ClientHelper.GetAppSettingValue("InternalDomain");
                    if (!string.IsNullOrEmpty(internalDomain))
                    {
                        _InternalDomains = internalDomain.Split(new char[1] { ',' });
                    }
                }
                return _InternalDomains;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetSenderName(string name)
        {
            string senderName = name;
            if (!string.IsNullOrEmpty(senderName) && senderName.Contains("@"))
            {
                string[] arrName = Regex.Split(senderName, "@");
                if (arrName != null && arrName.Length > 1)
                {
                    senderName = arrName[0];
                }
            }

            return senderName;
        }
    }
    /// <summary>
    /// 邮件联系人信息
    /// </summary>
    public class MailContactInfo
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email Address
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// Outlook Originator、Outlook To、Outlook CC、Outlook BCC
        /// </summary>
        public MailContactType ContactType { get; set; }

        /// <summary>
        /// 缓存一封邮件的所有联系人（包括内部联系人）
        /// </summary>
        private static Dictionary<string, List<MailContactInfo>> dicCache = new Dictionary<string, List<MailContactInfo>>();
        /// <summary>
        /// 缓存一封邮件所有的外部联系人
        /// </summary>
        private static Dictionary<string, List<MailContactInfo>> dicExternalContactCache = new Dictionary<string, List<MailContactInfo>>();

        /// <summary>
        /// 缓存一封邮件的所有外部联系人地址
        /// </summary>
        private static Dictionary<string, List<string>> dicExternalMailCache = new Dictionary<string, List<string>>();

        /// <summary>
        /// 将Message对象转换成联系人对象集合
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static List<MailContactInfo> Convert(Message.ServiceInterface.Message message)
        {
            if (message == null || string.IsNullOrEmpty(message.MessageId))
            {
                return new List<MailContactInfo>();
            }
            if (dicCache.ContainsKey(message.MessageId))
            {
                return dicCache[message.MessageId];
            }
            List<MailContactInfo> contactList = new List<MailContactInfo>();
            if (!string.IsNullOrEmpty(message.SendFrom))
            {
                MailContactInfo contactInfo = new MailContactInfo { ContactType = MailContactType.olOriginator, Name = message.CreatorName
                    , EmailAddress = message.SendFrom};
                contactList.Add(contactInfo);
            }
            contactList.AddRange(Convert(message.SendTo, message.ToName, MailContactType.olTo));
            contactList.AddRange(Convert(message.CC, message.CCName, MailContactType.olCC));
            contactList.AddRange(Convert(message.BCC, message.BCCName, MailContactType.olBCC));
            //去除重复的联系人地址
            contactList = contactList.GroupBy(s => s.EmailAddress.ToLower()).Select(s => s.FirstOrDefault()).ToList();

            if (!dicCache.ContainsKey(message.MessageId))
                dicCache.Add(message.MessageId, contactList);

            return contactList;
        }

        /// <summary>
        /// 将邮箱地址转换成联系人对象集合
        /// </summary>
        /// <param name="emailAddress">邮件地址</param>
        /// <param name="name">用户名</param>
        /// <param name="contactType">联系人类型:Outlook Originator、Outlook To、Outlook CC、Outlook BCC</param>
        /// <returns></returns>
        public static List<MailContactInfo> Convert(string emailAddress, string name, MailContactType contactType)
        {
            List<MailContactInfo> contactList = new List<MailContactInfo>();
            if (!string.IsNullOrEmpty(emailAddress))
            {
                string[] tos = emailAddress.Split(';').ToArray();
                string[] toNames = name.Split(';').ToArray();
                int nameLength = toNames.Length;
                int length = tos.Length;
                bool isSameLength = (nameLength == length);
                string arrName = string.Empty;
                for (int i = 0; i < length; i++)
                {
                    if (isSameLength)
                    {
                        arrName = toNames[i];
                    }
                    else
                    {
                        if (nameLength > 0 && i <= nameLength - 1)
                        {
                            arrName = toNames[i];
                        }
                    }


                    MailContactInfo contactInfo = new MailContactInfo { EmailAddress = tos[i].Trim(), Name = arrName, ContactType = contactType};
                    contactList.Add(contactInfo);
                }
            }
            return contactList;
        }

        /// <summary>
        /// 获取一封邮件所有外部联系人
        /// </summary>
        /// <param name="messageInfo"></param>
        /// <returns></returns>
        public static List<MailContactInfo> GetAllExternalContacts(Message.ServiceInterface.Message messageInfo)
        {
            if (string.IsNullOrEmpty(messageInfo.MessageId))
                return new List<MailContactInfo>();
            if (dicExternalContactCache.ContainsKey(messageInfo.MessageId))
                return dicExternalContactCache[messageInfo.MessageId];

            List<MailContactInfo> externalContacts = new List<MailContactInfo>();
            List<MailContactInfo> mailContacts = Convert(messageInfo);
            mailContacts.ForEach(item =>
            {
                if (!string.IsNullOrEmpty(item.EmailAddress) && item.EmailAddress.Contains("@"))
                {
                    string emailAddress = item.EmailAddress.Trim();
                    if (!FCMInterfaceUtility.ExsitsInternalContact(emailAddress))
                    {
                        externalContacts.Add(item);
                    }
                }
            });

            lock (dicExternalContactCache)
            {
                if (!dicExternalContactCache.ContainsKey(messageInfo.MessageId))
                    dicExternalContactCache.Add(messageInfo.MessageId, externalContacts);
            }
            return externalContacts;
        }

        /// <summary>
        /// 获取邮件的所有外部联系人地址
        /// </summary>
        /// <param name="messageInfo"></param>
        /// <returns></returns>
        public static List<string> GetAllExternalMails(Message.ServiceInterface.Message messageInfo)
        {
            if (string.IsNullOrEmpty(messageInfo.MessageId))
                return new List<string>();
            if (dicExternalMailCache.ContainsKey(messageInfo.MessageId))
                return dicExternalMailCache[messageInfo.MessageId];

            List<string> externalMails = new List<string>();
            List<MailContactInfo> mailContacts = Convert(messageInfo);
            if (mailContacts != null && mailContacts.Count > 0)
            {
                mailContacts.ForEach(item =>
                {
                    if (!string.IsNullOrEmpty(item.EmailAddress) && item.EmailAddress.Contains("@"))
                    {
                        string emailAddress = item.EmailAddress.Trim();
                        if (!FCMInterfaceUtility.ExsitsInternalContact(emailAddress))
                        {
                            externalMails.Add(emailAddress);
                        }
                    }
                });
            }
            if (!dicExternalMailCache.ContainsKey(messageInfo.MessageId))
                dicExternalMailCache.Add(messageInfo.MessageId, externalMails);

            return externalMails;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public enum MailContactType
    {
        /// <summary>
        /// 
        /// </summary>
        olOriginator = 0,
        /// <summary>
        /// 
        /// </summary>
        olTo = 1,
        /// <summary>
        /// 
        /// </summary>
        olCC = 2,
        /// <summary>
        /// 
        /// </summary>
        olBCC = 3,
    }
}
