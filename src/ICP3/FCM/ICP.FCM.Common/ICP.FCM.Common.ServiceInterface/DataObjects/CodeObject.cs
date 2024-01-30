namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    /// <summary>
    /// 事件列表Code实体类
    /// </summary>
   public  class CodeObject
    {
        private string codeType;

        /// <summary>
        /// Code类型
        /// </summary>
        public string CodeType
        {
            get { return codeType; }
            set { codeType = value; }
        }

        private string code;
        /// <summary>
        /// Code
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
    }
}
