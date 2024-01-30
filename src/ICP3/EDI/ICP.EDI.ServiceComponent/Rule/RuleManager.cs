using System.Xml;
namespace ICP.EDI.ServiceComponent.Rule
{
    /// <summary>
    /// 规则管理对象
    /// </summary>
    public class RuleManager
    {
        public DataSetRule GetDataSetRule(string ruleConfig)
        {
            XmlDocument document = new XmlDocument();    //创建xml文档对象
            document.Load(ruleConfig);

           //获取数据集规则
           XmlNode datasetnode= document.SelectSingleNode("datasetrule");
           
            DataSetRule dsRule = new DataSetRule();
            dsRule.InitRule(datasetnode);
            return dsRule;

        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="errorInfo"></param>
        public static void Validate(string ruleConfig,System.Data.DataSet ds, ref System.Text.StringBuilder errorInfo)
        {
            RuleManager ruleManager = new RuleManager();
            DataSetRule dsRule = ruleManager.GetDataSetRule(ruleConfig);
            dsRule.Validate(ds, ref errorInfo);
        }
 
    }
}
