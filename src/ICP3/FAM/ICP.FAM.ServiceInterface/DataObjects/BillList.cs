
namespace ICP.FAM.ServiceInterface.DataObjects   
{
   public partial class BillList
    {
        /// <summary>
        /// 组装类型(列表排序用)
        /// </summary>
        public string BizType
        {
            get
            {
                if (this.Type == BillType.None)
                {
                    return string.Empty;
                }
                else if (this.Type == BillType.AR)
                {
                    return "A";
                }
                else if (this.Type == BillType.DC)
                {
                    return "B";
                }
                else 
                {
                    return "C";
                }
            }
        }
    }
}


