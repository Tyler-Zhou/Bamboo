using ICP.Framework.CommonLibrary.Helper;

namespace ICP.Common.ServiceInterface.DataObjects
{
    public partial class DataDictionaryList
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName
        {
            get
            {
                return EnumHelper.GetDescription<DataDictionaryType>(this.Type, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
            }
        }
    }

    public partial class SolutionList
    {
        public string InvoiceDateTypeName
        {
            get
            {
                return EnumHelper.GetDescription<InvoiceDateType>(this.InvoiceDateType, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
            }
        }
    }

    //public partial class SolutionGLConfigList
    //{
    //    public string TypeName
    //    {
    //        get
    //        {
    //            return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<GLConfigType>(this.Type, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
    //        }
    //    }
    //}
}
