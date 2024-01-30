using System;

namespace ICP.WF.ServiceInterface.DataObject
{
    /// <summary>
    /// 表单配置文件对象
    /// </summary>
    [Serializable]
    public class FormProfileList
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// 表单xml
        /// </summary>
        public string PorfileContent { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }

        public string CName { get; set; }

        public string EName { get; set; }


    }

    /// <summary>
    /// 表单配置文件对象
    /// </summary>
    [Serializable]
    public class FormProfileInfo : FormProfileList
    {
        /// <summary>
        /// 数据结构
        /// </summary>
        public string DataSchame { get; set; }

        System.Data.DataSet data;
        /// <summary>
        /// 数据
        /// </summary>
        public System.Data.DataSet Data {
            get
            {
                if (data == null
                    && string.IsNullOrEmpty(DataSchame) == false)
                {
                    data = new System.Data.DataSet();
                    data.EnforceConstraints = false;

                    data.ReadXmlSchema(new System.IO.StringReader(DataSchame));
                }

                return data;
            }
            set
            {
                data = value;
                if (data != null)
                {
                    this.DataSchame = data.GetXml();
                }
            }
        }
    }
}
