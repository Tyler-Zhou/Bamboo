using System;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraEditors;

namespace ICP.Operation.Common.ServiceInterface
{
   /// <summary>
   /// 用户个人信息类
   /// </summary>
    public static   class UserPrivateInfo
    {

        /// <summary>
        /// 获取配置文件中节点的配置信息
        /// </summary>
        /// <param name="splitControl"></param>
        /// <param name="configId"></param>
        public static void SetSplitterPosition(SplitContainerControl splitControl, string configId)
        {
            SetSplitterPosition(splitControl, configId, 200);
        }

        /// <summary>
        /// 获取配置文件中节点的配置信息
        /// </summary>
        /// <param name="splitControl"></param>
        /// <param name="configId"></param>
        public static void SetSplitterPosition(SplitContainerControl splitControl, string configId,int DefaultPosition)
        {
            if (ClientConfig.Current.Contains(configId))
            {
                if (!string.IsNullOrEmpty(ClientConfig.Current.GetValue(configId)))
                {
                    int width = 0;
                    Int32.TryParse(ClientConfig.Current.GetValue(configId), out width);
                    splitControl.SplitterPosition = width == 0 ? DefaultPosition : width;
                }
            }
        }

        /// <summary>
        /// 记录分隔符位置到用户配置文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void SaveSplitterPosition(string key, string value)
        {
            ClientConfig.Current.AddValue(key, value);
        }
    }
}
