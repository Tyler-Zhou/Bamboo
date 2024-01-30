#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/6/15 星期五 13:56:24
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

namespace ICP.Common.ServiceInterface.DataObjects
{
    using System;
    using System.Text;
    using System.Xml.Serialization;
    using Framework.CommonLibrary.Common;

    /// <summary>
    /// 客户描述信息
    /// </summary>
    [Serializable]
    [XmlRoot("CustomerDescription")]
    public class CustomerDescriptionForNew : BaseDataObject, IConvertible, IEquatable<CustomerDescriptionForNew>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerDescriptionForNew()
        {
            Name = string.Empty;
            Address = string.Empty;
            Country = string.Empty;
            City = string.Empty;
            EnterpriseCodeType = string.Empty;
            EnterpriseCode = string.Empty;
            Tel = string.Empty;
            Fax = string.Empty;
            Remark = string.Empty;
            Contact = string.Empty;
            IsNew = false;
        }

        #region XmlElement
        string _Name;
        /// <summary>
        /// 名称
        /// </summary>
        [XmlElement("Name")]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    base.OnPropertyChanged("Name", value);
                }
            }

        }

        string _Address;
        /// <summary>
        /// 地址
        /// </summary>
        [XmlElement("Address")]
        public string Address
        {
            get { return _Address; }
            set
            {

                if (_Address != value)
                {
                    _Address = value;
                    base.OnPropertyChanged("Address", value);
                }
            }
        }

        string _Country;
        /// <summary>
        /// 国家
        /// </summary>
        [XmlElement("Country")]
        public string Country
        {
            get { return _Country; }
            set
            {
                if (_Country != value)
                {
                    _Country = value;
                    base.OnPropertyChanged("Country", value);
                }
            }
        }

        string _EnterpriseCodeType;
        /// <summary>
        /// 企业代码类型
        /// </summary>
        [XmlElement("EnterpriseCodeType")]
        public string EnterpriseCodeType
        {
            get { return _EnterpriseCodeType; }
            set
            {

                if (_EnterpriseCodeType != value)
                {
                    _EnterpriseCodeType = value;
                    base.OnPropertyChanged("EnterpriseCodeType", value);
                }
            }
        }

        string _EnterpriseCode;
        /// <summary>
        /// 企业代码
        /// </summary>
        [XmlElement("EnterpriseCode")]
        public string EnterpriseCode
        {
            get { return _EnterpriseCode; }
            set
            {

                if (_EnterpriseCode != value)
                {
                    _EnterpriseCode = value;
                    base.OnPropertyChanged("EnterpriseCode", value);
                }
            }
        }

        string _City;
        /// <summary>
        /// 城市
        /// </summary>
        [XmlElement("City")]
        public string City
        {
            get { return _City; }
            set
            {

                if (_City != value)
                {
                    _City = value;
                    base.OnPropertyChanged("City", value);
                }
            }
        }

        string _Tel;
        /// <summary>
        /// 电话
        /// </summary>
        [XmlElement("Tel")]
        public string Tel
        {
            get { return _Tel; }
            set
            {
                if (_Tel != value)
                {
                    _Tel = value;
                    base.OnPropertyChanged("Tel", value);
                }

            }
        }

        string _Fax;
        /// <summary>
        /// 传真
        /// </summary>
        [XmlElement("Fax")]
        public string Fax
        {
            get { return _Fax; }
            set
            {

                if (_Fax != value)
                {
                    _Fax = value;
                    base.OnPropertyChanged("Fax", value);
                }
            }
        }

        string _Contact;
        /// <summary>
        /// 联系人
        /// </summary>
        [XmlElement("Contact")]
        public string Contact
        {

            get { return _Contact; }
            set
            {
                if (_Contact != value)
                {
                    _Contact = value;
                    base.OnPropertyChanged("Contact", value);
                }
            }
        }

        string _Remark;
        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("Remark")]
        public string Remark
        {
            get
            {

                return _Remark;

            }
            set
            {

                if (_Remark != value)
                {
                    _Remark = value;
                    base.OnPropertyChanged("Remark", value);
                }
            }
        } 
        #endregion

        /// <summary>
        /// 是否新增
        /// </summary>
        [XmlIgnore]
        public new bool IsNew { get; set; }

        string _CountryCode;
        /// <summary>
        /// 国家编码
        /// </summary>
        [XmlIgnore]
        public string CountryCode
        {
            get { return _CountryCode; }
            set
            {
                if (_CountryCode != value)
                {
                    _CountryCode = value;
                    base.OnPropertyChanged("CountryCode", value);
                }
            }
        }

        Guid _CountryID;
        /// <summary>
        /// 国家ID
        /// </summary>
        [XmlIgnore]
        public Guid CountryID
        {
            get { return _CountryID; }
            set
            {
                if (_CountryID != value)
                {
                    _CountryID = value;
                    base.OnPropertyChanged("CountryID", value);
                }
            }
        }

        /// <summary>
        /// 基本信息转换为字符串描述
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        /// <returns>返回字符串描述</returns>
        public string BaseToString(bool isEnglish)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(Name) && Name.Trim().Length > 0)
            {
                sb.AppendLine(Name);
            }
            if (IsEnglish(Address))
            {
                #region 地址中没有中文
                string addressList = string.Empty;
                if (!string.IsNullOrEmpty(Address))
                {
                    addressList = Address;
                }
                if (!string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(Country))
                {
                    string str = City.ToUpper() + "," + Country.ToUpper();
                    if (addressList.ToUpper().EndsWith(City.ToUpper()))
                    {
                        addressList = addressList + "," + Country.ToUpper();
                    }
                    else if (addressList.ToUpper().EndsWith(Country.ToUpper()) && !addressList.ToUpper().EndsWith(str))
                    {
                        addressList = addressList.Replace(Country.ToUpper(), str);
                    }
                    else if (!addressList.ToUpper().EndsWith(str))
                    {
                        addressList = addressList + "," + str;
                    }

                }
                else if (string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(Country))
                {
                    string str = Country.ToUpper();
                    if (!addressList.ToUpper().EndsWith(str))
                    {
                        addressList = addressList + "," + str;
                    }
                }
                else if (!string.IsNullOrEmpty(City) && string.IsNullOrEmpty(Country))
                {
                    string str = City.ToUpper();
                    if (!addressList.ToUpper().EndsWith(str))
                    {
                        addressList = addressList + "," + str;
                    }
                }
                if (!string.IsNullOrEmpty(addressList))
                {
                    sb.AppendLine(addressList);
                }
                #endregion
            }
            else
            {
                #region 地址中包含中了文
                if (!string.IsNullOrEmpty(Address))
                {
                    sb.AppendLine(Address);
                }
                if (!string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(Country))
                {
                    sb.AppendLine(City.ToUpper() + "," + Country.ToUpper());
                }
                else if (!string.IsNullOrEmpty(City) && string.IsNullOrEmpty(Country))
                {
                    sb.AppendLine(City.ToUpper());
                }
                else if (string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(Country))
                {
                    sb.AppendLine(Country.ToUpper());
                }

                #endregion
            }

            if (!string.IsNullOrEmpty(Tel) && Tel.Trim().Length > 0)
            {
                sb.AppendLine("TEL:" + Tel);
            }
            if (!string.IsNullOrEmpty(Fax) && Fax.Trim().Length > 0)
            {
                sb.AppendLine("FAX:" + Fax);
            }
            if (!string.IsNullOrEmpty(Remark) && Remark.Trim().Length > 0)
            {
                sb.AppendLine(Remark);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 转换为字符串描述
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        /// <returns>返回字符串描述</returns>
        public string ToString(bool isEnglish)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(Name) && Name.Trim().Length > 0)
            {
                sb.AppendLine(Name);
            }
            if (IsEnglish(Address))
            {
                #region 地址中没有中文
                string addressList = string.Empty;
                if (!string.IsNullOrEmpty(Address))
                {
                    addressList = Address;
                }
                if (!string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(Country))
                {
                    string str = City.ToUpper() + "," + Country.ToUpper();
                    if (addressList.ToUpper().EndsWith(City.ToUpper()))
                    {
                        addressList = addressList + "," + Country.ToUpper();
                    }
                    else if (addressList.ToUpper().EndsWith(Country.ToUpper()) && !addressList.ToUpper().EndsWith(str))
                    {
                        addressList = addressList.Replace(Country.ToUpper(), str);
                    }
                    else if (!addressList.ToUpper().EndsWith(str))
                    {
                        addressList = addressList + "," + str;
                    }

                }
                else if (string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(Country))
                {
                    string str = Country.ToUpper();
                    if (!addressList.ToUpper().EndsWith(str))
                    {
                        addressList = addressList + "," + str;
                    }
                }
                else if (!string.IsNullOrEmpty(City) && string.IsNullOrEmpty(Country))
                {
                    string str = City.ToUpper();
                    if (!addressList.ToUpper().EndsWith(str))
                    {
                        addressList = addressList + "," + str;
                    }
                }
                if (!string.IsNullOrEmpty(addressList))
                {
                    sb.AppendLine(addressList);
                }
                #endregion
            }
            else
            {
                #region 地址中包含中了文
                if (!string.IsNullOrEmpty(Address))
                {
                    sb.AppendLine(Address);
                }
                if (!string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(Country))
                {
                    sb.AppendLine(City.ToUpper() + "," + Country.ToUpper());
                }
                else if (!string.IsNullOrEmpty(City) && string.IsNullOrEmpty(Country))
                {
                    sb.AppendLine(City.ToUpper());
                }
                else if (string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(Country))
                {
                    sb.AppendLine(Country.ToUpper());
                }

                #endregion
            }

            if (!string.IsNullOrEmpty(Tel) && Tel.Trim().Length > 0)
            {
                sb.AppendLine("TEL:" + Tel);
            }
            if (!string.IsNullOrEmpty(Fax) && Fax.Trim().Length > 0)
            {
                sb.AppendLine("FAX:" + Fax);
            }
            if (!string.IsNullOrEmpty(Remark) && Remark.Trim().Length > 0)
            {
                sb.AppendLine(Remark);
            }
            if (!string.IsNullOrEmpty(EnterpriseCode) && EnterpriseCode.Trim().Length > 0)
            {
                sb.AppendLine(EnterpriseCodeType + "+" + EnterpriseCode);
            }
            return sb.ToString();
        }
        //判断是否包含中文字符
        private bool IsEnglish(string arrString)
        {
            bool BoolValue = false;

            for (int i = 0; i < arrString.Length; i++)
            {

                if (Convert.ToInt32(Convert.ToChar(arrString.Substring(i, 1))) < Convert.ToInt32(Convert.ToChar(128)))
                {
                    BoolValue = true;
                }

                else
                {
                    return BoolValue = false;
                }
            }

            return BoolValue;
        }
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns>返回字符串描述</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(Name) == false)
            {
                sb.AppendLine(Name);
                sb.AppendLine(Address);
                if (string.IsNullOrEmpty(Tel) == false && Tel.Trim().Length > 0)
                    sb.AppendLine("TEL:" + Tel);

                if (string.IsNullOrEmpty(Fax) == false && Fax.Trim().Length > 0)
                    sb.AppendLine("FAX:" + Fax);
            }
            return sb.ToString();
        }

        #region IConvertible 成员

        /// <summary>
        /// 未实现
        /// </summary>
        /// <returns></returns>
        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 分四行显示名称、地址、电话和传真
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public string ToString(IFormatProvider provider)
        {
            return ToString();
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="conversionType"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEquatable<CustomerDescription> 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CustomerDescriptionForNew other)
        {
            if (other != null)
            {
                bool result = Address.Equals(other.Address) && City.Equals(other.City) && Contact.Equals(other.Contact) && Country.Equals(other.Country) && Fax.Equals(other.Fax)
                            && Name.Equals(other.Name) && Remark.Equals(other.Remark) && Tel.Equals(other.Tel);
                return result;
            }
            return false;
        }

        #endregion
    }
}
