//-----------------------------------------------------------------------
// <copyright file="CustomerDescriptionForAMS.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.ServiceInterface.DataObjects
{
    using System;
    using System.Text;
    using System.Xml.Serialization;
    using ICP.Framework.CommonLibrary.Common;

    /// <summary>
    /// 客户描述信息
    /// </summary>
    [Serializable]
    [XmlRoot("CustomerDescription")]
    public class CustomerDescriptionForAMS : BaseDataObject, IConvertible, IEquatable<CustomerDescription>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerDescriptionForAMS()
        {
            this.Name = string.Empty;
            this.Address = string.Empty;
            this.Country = string.Empty;
            this.City = string.Empty;
            this.Tel = string.Empty;
            this.Fax = string.Empty;
            this.Remark = string.Empty;
            this.Contact = string.Empty;
            this.Zip = string.Empty;
        }

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
                    string t = value;
                    string[] str = t.Split(' ');
                    string res = string.Empty;
                    foreach (string s in str)
                        if (!string.IsNullOrEmpty(s))
                            res += s+" ";
                    _Name = res;
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
                //_Address = _Address.Replace(Environment.NewLine, " ");
                if (_Address != value)
                {
                    //value = value.Replace(Environment.NewLine, " ");
                    //设置换行时不切断单词
                    string t = value;
                    //if (value.Length > 35)
                    //{
                    //    t = string.Empty;
                    //    string[] str = value.Split(' ');
                    //    bool b = true;
                    //    foreach (string s in str)
                    //    {
                    //        if (string.IsNullOrEmpty(s))
                    //            continue;
                    //        t += s;
                    //        if (t.Length > 35)
                    //        {
                    //            if (b)
                    //            {
                    //                int i = t.LastIndexOf(' ');
                    //                if (i == -1) i = t.Length;
                    //                string temp = t.Substring(i);
                    //                t = t.Substring(0, i);
                    //                for (int j = 0; j < 34 - i; j++)
                    //                    t += " ";
                    //                t += temp;
                    //                b = false;
                    //            }
                    //        }
                    //        t += " ";
                    //    }
                    //}
                    _Address = t;
                    base.OnPropertyChanged("Address", t);
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

        string _Zip;
        /// <summary>
        /// 邮编
        /// </summary>
        [XmlElement("Zip")]
        public string Zip
        {
            get { return _Zip; }
            set
            {
                if (_Zip != value)
                {
                    _Zip = value;
                    base.OnPropertyChanged("Zip", value);
                }
            }
        }

        /// <summary>
        /// 转换为字符串描述
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        /// <returns>返回字符串描述</returns>
        public string ToString(bool isEnglish)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(Name) && this.Name.Trim().Length > 0)
            {
                sb.AppendLine(this.Name);
            }
            #region 地址中没有中文
            string addressList = string.Empty;
            if (!string.IsNullOrEmpty(this.Address))
            {
                addressList = this.Address;
            }
            if (!string.IsNullOrEmpty(this.City) && !string.IsNullOrEmpty(this.Country))
            {
                string str = this.City.ToUpper() + "," + this.Zip + "," + this.Country.ToUpper();
                if (addressList.ToUpper().EndsWith(this.City.ToUpper()))
                {
                    addressList = addressList + "," + this.Country.ToUpper();
                }
                else if (addressList.ToUpper().EndsWith(this.Country.ToUpper()) && !addressList.ToUpper().EndsWith(str))
                {
                    addressList = addressList.Replace(this.Country.ToUpper(), str);
                }
                else if (!addressList.ToUpper().EndsWith(str))
                {
                    addressList = addressList + "," + str;
                }

            }
            else if (string.IsNullOrEmpty(this.City) && !string.IsNullOrEmpty(this.Country))
            {
                string str = this.Country.ToUpper();
                if (!addressList.ToUpper().EndsWith(str))
                {
                    addressList = addressList + "," + str;
                }
            }
            else if (!string.IsNullOrEmpty(this.City) && string.IsNullOrEmpty(this.Country))
            {
                string str = this.City.ToUpper();
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

            return sb.ToString().Replace(",,", ",").Replace(", ,", ",");
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
            if (string.IsNullOrEmpty(this.Name) == false)
            {
                sb.AppendLine(this.Name);
                sb.AppendLine(this.Address);
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
            if (this == null)
            {
                return string.Empty;
            }
            else
            {
                return this.ToString();
            }
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

        public bool Equals(CustomerDescription other)
        {
            if (other != null)
            {
                bool result = this.Address.Equals(other.Address) && this.City.Equals(other.City) && this.Contact.Equals(other.Contact) && this.Country.Equals(other.Country) && this.Fax.Equals(other.Fax)
                            && this.Name.Equals(other.Name) && this.Remark.Equals(other.Remark) && this.Tel.Equals(other.Tel);
                return result;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
