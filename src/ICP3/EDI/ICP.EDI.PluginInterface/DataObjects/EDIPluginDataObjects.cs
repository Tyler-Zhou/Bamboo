namespace ICP.EDI.PluginInterface
{
    /// <summary>
    /// EDI插件输入
    /// </summary>
    public class EDIPluginInput
    {
        /// <summary>
        /// 
        /// </summary>
        public string DataSetXML { get; set; }
    }

    /// <summary>
    /// EDI插件输出
    /// </summary>
    public class EDIPluginOut
    {
        /// <summary>
        /// 
        /// </summary>
        public string EDIData { get; set; }
    }
}
