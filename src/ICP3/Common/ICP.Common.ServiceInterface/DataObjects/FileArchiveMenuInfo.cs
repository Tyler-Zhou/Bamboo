using System;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.ServiceInterface.DataObjects
{   
    /// <summary>
    /// 报表预览归档文档菜单项信息类
    /// </summary>
    [Serializable]
   public class FileArchiveMenuInfo
    {  
        /// <summary>
        /// 菜单项中文标题
        /// </summary>
        public string CCaption { get; set ; }
        /// <summary>
        /// 菜单项英文标题
        /// </summary>
        public string ECaption { get; set; }
        /// <summary>
        /// 根据当前登录语言获取标题
        /// </summary>
        public string Caption {

            get
            {
                return (LocalData.IsEnglish ? this.ECaption : this.CCaption);
            }
        }
        /// <summary>
        /// 菜单项对应归档文档类型
        /// </summary>
        public string DocumentType
        {
            get;
            set;
        }
        /// <summary>
        /// 归档文件的名称
        /// </summary>
        public string FileName
        {
            get;
            set;
        }
    }
}
