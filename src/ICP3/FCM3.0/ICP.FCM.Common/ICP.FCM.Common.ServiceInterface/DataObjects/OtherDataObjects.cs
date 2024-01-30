namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    using System;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using ICP.Framework.CommonLibrary.Common;
    using System.ComponentModel.DataAnnotations;
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
            this.Cargo = new DryCargo() { Description = string.Empty };
        }

        /// <summary>
        /// CargoDescription
        /// </summary>
        /// <param name="cargo"></param>
        public CargoDescription(CommonCargo cargo)
        {
            this.Cargo = cargo;
        }

        /// <summary>
        /// 货物类型
        /// </summary>
        [XmlAttribute("Type")]
        public string Type
        {
            get
            {
                if (this.Cargo != null)
                {
                    if (typeof(DryCargo).IsInstanceOfType(this.Cargo))
                    {
                        return "DryCargo";
                    }
                    else if (typeof(ReeferCargo).IsInstanceOfType(this.Cargo))
                    {
                        return "ReeferCargo";
                    }
                    else if (typeof(DangerousCargo).IsInstanceOfType(this.Cargo))
                    {
                        return "DangerousCargo";
                    }
                    else if (typeof(AwkwardCargo).IsInstanceOfType(this.Cargo))
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
            this.Description = string.Empty;
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
            return this.Description;
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
            this.F = 0.0m;
            this.C = 0.0m;
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
                sbValue.AppendLine("Celsius:" + this.C.ToString("F1"));
                sbValue.AppendLine("Fahrenheit :" + this.F.ToString("F1"));
            }
            else
            {
                sbValue.AppendLine("摄氏:" + this.C.ToString("F1"));
                sbValue.AppendLine("华氏:" + this.F.ToString("F1"));
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
            this.UNNo = 0;
            this.Class = string.Empty;
            this.Property = string.Empty;
            this.IMDGCode = string.Empty;
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
                sbValue.AppendLine("UN NO:" + this.UNNo);
                sbValue.AppendLine("Class:" + this.Class);
                sbValue.AppendLine("Property:" + this.Property);
                sbValue.AppendLine("IMDGCode:" + this.IMDGCode);
            }
            else
            {
                sbValue.AppendLine("UN号:" + this.UNNo);
                sbValue.AppendLine("种类:" + this.Class);
                sbValue.AppendLine("特性:" + this.Property);
                sbValue.AppendLine("IMDGCode:" + this.IMDGCode);
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
            this.Quantity = 0;
            this.GrossWeight = 0.0m;
            this.GrossWeightUnit = string.Empty;
            this.NetWeight = 0.0m;
            this.NetWeightUnit = string.Empty;
            this.Commodity = string.Empty;
            this.Length = 0.0m;
            this.Width = 0.0m;
            this.Height = 0.0m;
            this.Details = string.Empty;
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
                sbValue.AppendLine("Quantity:" + this.Quantity.ToString("N00"));
                sbValue.AppendLine("Gross Weight:" + this.GrossWeight.ToString("F1") + this.GrossWeightUnit);
                sbValue.AppendLine("Net Weight:" + this.NetWeight.ToString("F1") + this.NetWeightUnit);
                sbValue.AppendLine("Length:" + this.Length.ToString("F1"));
                sbValue.AppendLine("Width:" + this.Width.ToString("F1"));
                sbValue.AppendLine("Height:" + this.Height.ToString("F1"));
                sbValue.AppendLine("Commodity:" + this.Commodity);
                sbValue.AppendLine("Details:" + this.Details);
            }
            else
            {
                sbValue.AppendLine("数量:" + this.Quantity.ToString("N00"));
                sbValue.AppendLine("毛重:" + this.GrossWeight.ToString("F1") + this.GrossWeightUnit);
                sbValue.AppendLine("净重:" + this.NetWeight.ToString("F1") + this.NetWeightUnit);
                sbValue.AppendLine("长度:" + this.Length.ToString("F1"));
                sbValue.AppendLine("宽度:" + this.Width.ToString("F1"));
                sbValue.AppendLine("高度:" + this.Height.ToString("F1"));
                sbValue.AppendLine("品名:" + this.Commodity);
                sbValue.AppendLine("明细:" + this.Details);
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
            this.Containers = new List<Container>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="containerInfo">箱信息(2*20GP,3*40HQ)</param>
        public ContainerDescription(string containerInfo)
        {
            this.Containers = this.BuildContainer(containerInfo);
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
            foreach (Container c in this.Containers)
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
                Container container = new Container();
                container.Quantity = e.Value;
                container.Weight = 0;
                container.WeightUnit = string.Empty;
                container.Size = int.Parse(containerSizeDictionary[e.Key].Trim());
                container.Type = containerTypeDictionary[e.Key];
                results.Add(container);
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
            this.Size = 0;
            this.Type = string.Empty;
            this.Quantity = 0;
            this.Weight = 0;
            this.WeightUnit = string.Empty;
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
            return this.Quantity.ToString()
                + " * "
                + this.Size
                + " "
                + this.Type;
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
