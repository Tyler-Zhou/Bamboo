namespace ICP.Common.UI.GoTo
{
    /// <summary>
    /// Goto界面-控件实体对象
    /// </summary>
    public class GoToControl
    {
        /// <summary>
        ///  出现的位置
        /// </summary>
        public int Order { get; set; }
         /// <summary>
        /// 方法名
         /// </summary>
        public string Methods { get; set; }
        /// <summary>
        /// 使用的类
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 使用的类库
        /// </summary>
        public string Assembly { get; set; }
        /// <summary>
        /// 当前控件显示的Text
        /// </summary>
        public string Name { get; set; }
    }
}
