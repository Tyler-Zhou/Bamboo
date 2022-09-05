using Client.DataAccess;
using System;
using System.Text;

namespace Client.Helpers
{
    /// <summary>
    /// 构建资源字典帮助类
    /// </summary>
    public class BuildResourcesHelper
    {
        /// <summary>
        /// 构建字典
        /// </summary>
        /// <returns></returns>
        public static void BuildResources()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in Repository.Monsters)
            {
                //stringBuilder.AppendLine($"<sys:String x:Key=\"MonsterItem{item.Item.Replace(" ","")}\">{item.Item}</sys:String>");
                //stringBuilder.AppendLine($"new MonsterModel(){{Key=\"{item.Key.Replace(" ","")}\",Level = {item.Level} ,Item=\"{(string.IsNullOrWhiteSpace(item.Item) ? "" : "MonsterItem")}{item.Item.Replace(" ", "")}\"}},");
            }
            Console.WriteLine(stringBuilder.ToString());
        }
    }
}
