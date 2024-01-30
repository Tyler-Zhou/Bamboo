namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    using System;
    using System.Text;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    #region Cargo

    /// <summary>
    /// 货物描述
    /// </summary>
    [Serializable]
    public class CargoDescription
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CargoDescription()
        {
            Cargo = new DryCargo() { Description = string.Empty };
        }

        /// <summary>
        /// CargoDescription
        /// </summary>
        /// <param name="cargo"></param>
        public CargoDescription(CommonCargo cargo)
        {
            Cargo = cargo;
        }

        /// <summary>
        /// 货物类型
        /// </summary>
        [XmlAttribute("Type")]
        public string Type
        {
            get
            {
                if (Cargo != null)
                {
                    if (typeof(DryCargo).IsInstanceOfType(Cargo))
                    {
                        return "DryCargo";
                    }
                    else if (typeof(ReeferCargo).IsInstanceOfType(Cargo))
                    {
                        return "ReeferCargo";
                    }
                    else if (typeof(DangerousCargo).IsInstanceOfType(Cargo))
                    {
                        return "DangerousCargo";
                    }
                    else if (typeof(AwkwardCargo).IsInstanceOfType(Cargo))
                    {
                        return "AwkwardCargo";
                    }
                }

                return string.Empty;
            }
            set
            {
            }
        }

        /// <summary>
        /// 货物描述
        /// </summary>
        [XmlElement("DryCargo", typeof(DryCargo))]
        [XmlElement("ReeferCargo", typeof(ReeferCargo))]
        [XmlElement("DangerousCargo", typeof(DangerousCargo))]
        [XmlElement("AwkwardCargo", typeof(AwkwardCargo))]
        public CommonCargo Cargo { get; set; }
    }

    /// <summary>
    /// 货物抽象类
    /// </summary>
    [Serializable]
    public abstract class CommonCargo
    {
        /// <summary>
        /// 
        /// </summary>
        public CommonCargo()
        { 
            
        }
        /// <summary>
        /// 转换为字符串描述
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        /// <returns>返回字符串描述</returns>
        public virtual string ToString(bool isEnglish)
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 干货描述
    /// </summary>
    [Serializable]
    public class DryCargo : CommonCargo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DryCargo()
        {
            Description = string.Empty;
        }

        /// <summary>
        /// 描述
        /// </summary>
        [XmlAttribute("Description")]
        public string Description { get; set; }

        /// <summary>
        /// 转换为字符串描述
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        /// <returns>返回字符串描述</returns>
        public override string ToString(bool isEnglish)
        {
            return Description;
        }
    }

    /// <summary>
    /// 冷冻货描述
    /// </summary>
    [Serializable]
    public class ReeferCargo : CommonCargo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ReeferCargo()
        {
            F = 0.0m;
            C = 0.0m;
        }

        /// <summary>
        /// 华氏度
        /// </summary>
        [XmlAttribute("F")]
        public decimal F { get; set; }

        /// <summary>
        /// 摄氏度 
        /// </summary>
        [XmlAttribute("C")]
        public decimal C { get; set; }

        /// <summary>
        /// 转换为字符串描述
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        /// <returns>返回字符串描述</returns>
        public override string ToString(bool isEnglish)
        {
            StringBuilder sbValue = new StringBuilder();
            if (isEnglish)
            {
                sbValue.AppendLine("Celsius:" + C.ToString("F1"));
                sbValue.AppendLine("Fahrenheit :" + F.ToString("F1"));
            }
            else
            {
                sbValue.AppendLine("摄氏:" + C.ToString("F1"));
                sbValue.AppendLine("华氏:" + F.ToString("F1"));
            }

            return sbValue.ToString();
        }
    }

    /// <summary>
    /// 危险货描述
    /// </summary>
    [Serializable]
    public class DangerousCargo : CommonCargo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DangerousCargo()
        {
            UNNo = 0;
            Class = string.Empty;
            Property = string.Empty;
            IMDGCode = string.Empty;
        }

        /// <summary>
        /// UNNo
        /// </summary>
        [XmlAttribute("UNNo")]
        public int UNNo { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [XmlAttribute("Class")]
        public string Class { get; set; }

        /// <summary>
        /// 特性
        /// </summary>
        [XmlAttribute("Property")]
        public string Property { get; set; }

        /// <summary>
        /// IMDG代码
        /// </summary>
        [XmlAttribute("IMDGCode")]
        public string IMDGCode { get; set; }

        /// <summary>
        /// 转换为字符串描述
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        /// <returns>返回字符串描述</returns>
        public override string ToString(bool isEnglish)
        {
            StringBuilder sbValue = new StringBuilder();
            if (isEnglish)
            {
                sbValue.AppendLine("UN NO:" + UNNo);
                sbValue.AppendLine("Class:" + Class);
                sbValue.AppendLine("Property:" + Property);
                sbValue.AppendLine("IMDGCode:" + IMDGCode);
            }
            else
            {
                sbValue.AppendLine("UN号:" + UNNo);
                sbValue.AppendLine("种类:" + Class);
                sbValue.AppendLine("特性:" + Property);
                sbValue.AppendLine("IMDGCode:" + IMDGCode);
            }

            return sbValue.ToString();
        }
    }

    /// <summary>
    /// 特种货描述
    /// </summary>
    [Serializable]
    public class AwkwardCargo : CommonCargo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AwkwardCargo()
        {
            Quantity = 0;
            GrossWeight = 0.0m;
            GrossWeightUnit = string.Empty;
            NetWeight = 0.0m;
            NetWeightUnit = string.Empty;
            Commodity = string.Empty;
            Length = 0.0m;
            Width = 0.0m;
            Height = 0.0m;
            Details = string.Empty;
        }

        /// <summary>
        /// 数量
        /// </summary>
        [XmlAttribute("Quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// 毛重
        /// </summary>
        [XmlAttribute("GrossWeight")]
        public decimal GrossWeight { get; set; }

        /// <summary>
        /// 毛重单位
        /// </summary>
        [XmlAttribute("GrossWeightUnit")]
        public string GrossWeightUnit { get; set; }

        /// <summary>
        /// 净重
        /// </summary>
        [XmlAttribute("NetWeight")]
        public decimal NetWeight { get; set; }

        /// <summary>
        /// 净重单位
        /// </summary>
        [XmlAttribute("NetWeightUnit")]
        public string NetWeightUnit { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        [XmlAttribute("Commodity")]
        public string Commodity { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        [XmlAttribute("Length")]
        public decimal Length { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        [XmlAttribute("Width")]
        public decimal Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        [XmlAttribute("Height")]
        public decimal Height { get; set; }

        /// <summary>
        /// 明细
        /// </summary>
        [XmlAttribute("Details")]
        public string Details { get; set; }

        /// <summary>
        /// 转换为字符串描述
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        /// <returns>返回字符串描述</returns>
        public override string ToString(bool isEnglish)
        {
            StringBuilder sbValue = new StringBuilder();
            if (isEnglish)
            {
                sbValue.AppendLine("Quantity:" + Quantity.ToString("N00"));
                sbValue.AppendLine("Gross Weight:" + GrossWeight.ToString("F1") + GrossWeightUnit);
                sbValue.AppendLine("Net Weight:" + NetWeight.ToString("F1") + NetWeightUnit);
                sbValue.AppendLine("Length:" + Length.ToString("F1"));
                sbValue.AppendLine("Width:" + Width.ToString("F1"));
                sbValue.AppendLine("Height:" + Height.ToString("F1"));
                sbValue.AppendLine("Commodity:" + Commodity);
                sbValue.AppendLine("Details:" + Details);
            }
            else
            {
                sbValue.AppendLine("数量:" + Quantity.ToString("N00"));
                sbValue.AppendLine("毛重:" + GrossWeight.ToString("F1") + GrossWeightUnit);
                sbValue.AppendLine("净重:" + NetWeight.ToString("F1") + NetWeightUnit);
                sbValue.AppendLine("长度:" + Length.ToString("F1"));
                sbValue.AppendLine("宽度:" + Width.ToString("F1"));
                sbValue.AppendLine("高度:" + Height.ToString("F1"));
                sbValue.AppendLine("品名:" + Commodity);
                sbValue.AppendLine("明细:" + Details);
            }

            return sbValue.ToString();
        }
    }
    #endregion

    #region ContainerDescription

    /// <summary>
    /// 箱描述
    /// </summary>
    [Serializable]
    public class ContainerDescription
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ContainerDescription()
        {
            Containers = new List<Container>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="containerInfo">箱信息(2*20GP,3*40HQ)</param>
        public ContainerDescription(string containerInfo)
        {
            Containers = BuildContainer(containerInfo);
        }

        /// <summary>
        /// 箱列表
        /// </summary>
        [XmlElement("Container", typeof(Container))]
        public List<Container> Containers { get; set; }

        /// <summary>
        /// 转为字符串描述
        /// </summary>
        /// <returns>返回字符串描述</returns>
        public override string ToString()
        {
            StringBuilder sbValue = new StringBuilder();
            foreach (Container c in Containers)
            {
                if (sbValue.Length > 0)
                {
                    sbValue.Append(",");
                }

                sbValue.Append(c.ToString());
            }

            return sbValue.ToString();
        }

        /// <summary>
        /// 根据箱描述串生成箱列表
        /// </summary>
        /// <param name="containerInfo">箱信息描述</param>
        /// <returns>返回箱信息列表</returns>
        private List<Container> BuildContainer(string containerInfo)
        {
            if (string.IsNullOrEmpty(containerInfo))
            {
                return new List<Container>();
            }

            List<Container> results = new List<Container>();
            //sapce
            string sapces = @"[\x20]*";
            //Count
            string pQty = @"(?<Qty>[\d]+)";
            //Size
            string psize = @"(?<SizeB>(?<Size>20|40|45|53)[\']*)";
            //Type 
            string ptype = @"(?<Type>(GP|HQ|FR|HT|OT|RF|RF|TK|TF|RH|NOR))";
            //spe
            string pspae = @"[\x20\,]*";
            //
            string patten = @"[\x20]*(?<box>" + pQty + sapces + @"[xX\xD7\*]" + sapces + psize + sapces + ptype + @")" + pspae;
            //
            MatchCollection matchs = Regex.Matches(containerInfo.ToUpper(), patten, RegexOptions.IgnoreCase);

            Dictionary<string, int> containerQtyDictionary = new Dictionary<string, int>();
            Dictionary<string, string> containerSizeDictionary = new Dictionary<string, string>();
            Dictionary<string, string> containerTypeDictionary = new Dictionary<string, string>();

            foreach (Match match in matchs)
            {
                int qty = int.Parse(match.Groups["Qty"].Value);
                string size = match.Groups["SizeB"].Value;
                string type = match.Groups["Type"].Value;

                string key = string.Format("{0} {1}", size, type);
                if (containerQtyDictionary.ContainsKey(key))
                {
                    containerQtyDictionary[key] = int.Parse(containerQtyDictionary[key].ToString()) + 1;
                }
                else
                {
                    containerQtyDictionary.Add(key, qty);
                    containerSizeDictionary.Add(key, size);
                    containerTypeDictionary.Add(key, type);
                }
            }

            foreach (KeyValuePair<string, int> e in containerQtyDictionary)
            {
                if(e.Value>0)
                {
                    Container container = new Container();
                    container.Quantity = e.Value;
                    container.Weight = 0;
                    container.WeightUnit = string.Empty;
                    container.Size = int.Parse(containerSizeDictionary[e.Key].Trim());
                    container.Type = containerTypeDictionary[e.Key];
                    results.Add(container);
                }
            }
            return results;
        }
    }

    /// <summary>
    /// 箱信息
    /// </summary>
    [Serializable]
    public class Container
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Container()
        {
            Size = 0;
            Type = string.Empty;
            Quantity = 0;
            Weight = 0;
            WeightUnit = string.Empty;
        }

        /// <summary>
        /// 箱尺寸
        /// </summary>
        [XmlElement("Size")]
        public int Size { get; set; }

        /// <summary>
        /// 箱型
        /// </summary>
        [XmlElement("Type")]
        public string Type { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [XmlElement("Quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        [XmlElement("Weight")]
        public decimal? Weight { get; set; }

        /// <summary>
        /// 重量单位
        /// </summary>
        [XmlElement("WeightUnit")]
        public string WeightUnit { get; set; }

        /// <summary>
        /// 转为字符串描述
        /// </summary>
        /// <returns>返回字符串描述</returns>
        public override string ToString()
        {
            return Quantity.ToString()
                + " * "
                + Size
                + " "
                + Type;
        }
    }

    #endregion

    #region PickUpDescription
    /// <summary>
    /// 提货单信息
    /// </summary>
    [Serializable]
    public class PickUpDescription
    {
        /// <summary>
        /// 方式
        /// </summary>
        [XmlElement("Type")]
        public int Type { get; set; }

        /// <summary>
        /// 提货时间
        /// </summary>
        [XmlElement("Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [XmlElement("Address")]
        public string Address { get; set; }

        /// <summary>
        /// 联系人 
        /// </summary>
        [XmlElement("Contact")]
        public string Contact { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [XmlElement("Tel")]
        public string Tel { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [XmlElement("Fax")]
        public string Fax { get; set; }
    }

    #endregion
}
