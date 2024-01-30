using System;
using System.Reflection;
using System.Xml.Linq;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    /// <summary>
    /// 客户承运实体类
    /// </summary>
    [Serializable]
    public class CustomerCarrierObjects : BaseDataObject
    {
        /// <summary>
        /// 当Id为null或Guid.Empty时，IsNew取值为true，认为对象是新建对象
        /// </summary>
        public override bool IsNew
        {
            get
            {
                return Id == null || Id == Guid.Empty;
            }
        }
        private Guid id;
        /// <summary>
        /// 
        /// </summary>
        public Guid Id
        {
            get { return id; }
            set
            {
                id = value;
                base.OnPropertyChanged("Id", value);
            }
        }

        private ContactType type;
        /// <summary>
        /// 
        /// </summary>
        public ContactType Type
        {
            get { return type; }
            set
            {
                type = value;
                base.OnPropertyChanged("Type", value);
            }
        }

        private Guid oceanBookingID;
        /// <summary>
        /// 
        /// </summary>
        public Guid OceanBookingID
        {
            get { return oceanBookingID; }
            set
            {
                oceanBookingID = value;
                base.OnPropertyChanged("OceanBookingID", value);
            }
        }

        private OperationType? operationType;
        /// <summary>
        /// 
        /// </summary>
        public OperationType? OperationType
        {
            get { return operationType; }
            set
            {
                operationType = value;
                base.OnPropertyChanged("OperationType", value);
            }
        }
        private Guid? customerID;
        /// <summary>
        /// 业务联系人所属的客户ID
        /// </summary>
        public Guid? CustomerID
        {
            get { return customerID; }
            set
            {
                customerID = value;
                base.OnPropertyChanged("CustomerID", value);
            }
        }
        private string customerName;
        /// <summary>
        /// 业务联系人所属的客户名称
        /// </summary>
        public string CustomerName
        {
            get { return customerName; }
            set
            {
                if (customerName != value)
                {
                    customerName = value;
                    base.OnPropertyChanged("CustomerName", value);
                }
            }
        }

        private bool isSO;
        /// <summary>
        /// 是否沟通阶段
        /// </summary>
        public bool SO
        {
            get { return isSO; }
            set
            {
                isSO = value;
                base.OnPropertyChanged("SO", value);
            }
        }

        private bool isTrk;
        /// <summary>
        /// 是否运输阶段
        /// </summary>
        public bool Trk
        {
            get { return isTrk; }
            set
            {
                isTrk = value;
                base.OnPropertyChanged("Trk", value);
            }
        }

        private bool isCF;
        /// <summary>
        /// 是否报关阶段
        /// </summary>
        public bool CF
        {
            get { return isCF; }
            set
            {
                isCF = value;
                base.OnPropertyChanged("CF", value);
            }
        }

        private bool isFU;
        /// <summary>
        /// 是否熏蒸
        /// </summary>
        public bool FU
        {
            get { return isFU; }
            set
            {
                isFU = value;
                base.OnPropertyChanged("FU", value);
            }
        }


        private bool isWhs;
        /// <summary>
        /// 
        /// </summary>
        public bool Whs
        {
            get { return isWhs; }
            set
            {
                isWhs = value;
                base.OnPropertyChanged("Whs", value);
            }
        }

        private bool isIN;
        /// <summary>
        /// 是否保险
        /// </summary>
        public bool IN
        {
            get { return isIN; }
            set
            {
                isIN = value;
                base.OnPropertyChanged("IN", value);
            }
        }

        private bool isSI;
        /// <summary>
        /// 是否补料阶段
        /// </summary>
        public bool SI
        {
            get { return isSI; }
            set
            {
                isSI = value;
                base.OnPropertyChanged("SI", value);
            }
        }

        private bool isCC;
        /// <summary>
        /// 
        /// </summary>
        public bool IsCC
        {
            get { return isCC; }
            set
            {
                isCC = value;
                base.OnPropertyChanged("IsCC", value);
            }
        }

        private bool isBL;
        /// <summary>
        /// 是否提单阶段
        /// </summary>
        public bool BL
        {
            get { return isBL; }
            set
            {
                isBL = value;
                base.OnPropertyChanged("BL", value);
            }
        }
        private bool isAN;
        /// <summary>
        /// 是否提货通知书阶段
        /// </summary>
        public bool AN
        {
            get { return isAN; }
            set
            {
                isAN = value;
                base.OnPropertyChanged("AN", value);
            }
        }

        private bool isAR;
        /// <summary>
        /// 是否收款客户
        /// </summary>
        public bool AR
        {
            get { return isAR; }
            set
            {
                isAR = value;
                base.OnPropertyChanged("AR", value);
            }
        }

        private bool isRelease;
        /// <summary>
        /// 是否收款客户
        /// </summary>
        public bool Release
        {
            get { return isRelease; }
            set
            {
                isRelease = value;
                base.OnPropertyChanged("Release", value);
            }
        }

        private string name;

        /// <summary>
        /// 名字
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                base.OnPropertyChanged("Name", value);
            }
        }
        private string stage;
        /// <summary>
        /// 联系人所属的沟通阶段
        /// </summary>
        public string Stage
        {
            get { return stage; }
            set
            {
                stage = value;
                base.OnPropertyChanged("Stage", value);
            }
        }

        private string mail;
        /// <summary>
        /// 邮件
        /// </summary>
        public string Mail
        {
            get { return mail; }
            set
            {
                mail = value;
                base.OnPropertyChanged("Mail", value);
            }
        }

        private string tel;
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel
        {
            get { return tel; }
            set
            {
                tel = value;
                base.OnPropertyChanged("Tel", value);
            }
        }

        private string fax;
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax
        {
            get { return fax; }
            set
            {
                fax = value;
                base.OnPropertyChanged("Fax", value);
            }
        }

        private string subject;
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject
        {
            get { return subject; }
            set
            {
                subject = value;
                base.OnPropertyChanged("Subject", value);
            }
        }

        private string mobile;
        /// <summary>
        /// 
        /// </summary>
        public string Mobile
        {
            get { return mobile; }
            set
            {
                mobile = value;
                base.OnPropertyChanged("Mobile", value);
            }
        }

        private DateTime? updateDate;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateDate
        {
            get { return updateDate; }
            set
            {
                updateDate = value;
                base.OnPropertyChanged("UpdateDate", value);
            }
        }

        private Guid? updateByID;
        /// <summary>
        /// 
        /// </summary>
        public Guid? UpdateByID
        {
            get { return updateByID; }
            set
            {
                updateByID = value;
                base.OnPropertyChanged("UpdateByID", value);
            }
        }

        private DateTime? createDate;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDate
        {
            get { return createDate; }
            set
            {
                createDate = value;
                base.OnPropertyChanged("CreateDate", value);
            }
        }

        private Guid? createByID;
        /// <summary>
        /// 
        /// </summary>
        public Guid? CreateByID
        {
            get { return createByID; }
            set
            {
                createByID = value;
                base.OnPropertyChanged("CreateByID", value);
            }
        }

        private Guid? billID;
        /// <summary>
        /// 
        /// </summary>
        public Guid? BillID
        {
            get { return billID; }
            set
            {
                billID = value;
                base.OnPropertyChanged("BillID", value);
            }
        }

        #region ISerializable 成员

        private PropertyInfo[] propertyInfos = null;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PropertyInfo[] GetPropertyInfos()
        {
            if (propertyInfos == null)
            {
                propertyInfos = GetType().GetProperties();
            }
            return propertyInfos;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetXmlDataNode()
        {
            XElement element = new XElement("row");
            PropertyInfo[] propertyInfos = GetPropertyInfos();
            foreach (PropertyInfo p in propertyInfos)
            {
                if (p.PropertyType == typeof(bool))
                    continue;

                if (p.Name.Equals("CustomerName")
                    || p.Name.Equals("Subject") ||
                    p.Name.Equals("Undoable"))
                    continue;

                object value = p.GetValue(this, null);
                if (value != null)
                {
                    if (p.PropertyType == typeof(OperationType?))
                        value = OperationType.GetHashCode();

                    if (p.PropertyType == typeof(ContactType))
                        value = Type.GetHashCode();

                    XAttribute nameAttribute = new XAttribute(p.Name, value);
                    element.Add(nameAttribute);
                }
            }
            return element.ToString();
        }

        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    public class CustomerCarrierParam
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid OceanBookingID;
    }
    /// <summary>
    /// 
    /// </summary>
    public class ContactStageInfo
    {
        /// <summary>
        /// 沟通阶段值
        /// </summary>
        public int Stage { get; set; }
        /// <summary>
        /// 沟通阶段名称
        /// </summary>
        public string StageName { get; set; }
        /// <summary>
        /// 沟通阶段所属的业务类型
        /// </summary>
        public OperationType OperationType { get; set; }

    }
}
