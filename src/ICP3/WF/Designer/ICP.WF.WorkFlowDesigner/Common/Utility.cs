

using ICP.WF.WorkFlowDesigner.Properties;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
namespace ICP.WF.WorkFlowDesigner
{
    internal class Utility
    {
        /// <summary>
        /// 判断是否英文环境
        /// </summary>
        public static bool IsEnglish
        {
            get
            {
                return ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish;
            }
        }

        /// <summary>
        /// 根据关键字查找中英文资源
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="defaultValue">如果在当前语言环境中，没找到，就设置为当前的默认值</param>
        /// <returns></returns>
        public static string GetString(string key, string defaultValue)
        {
            try
            {
                if (IsEnglish)
                {//查找英文资源
                    string enVal = ICP.WF.WorkFlowDesigner.Resources.Resource_EN.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(enVal)==false)
                    {
                        return enVal;
                    }
                }
                else
                {//查找中文资源
                    string cnVal = ICP.WF.WorkFlowDesigner.Resources.Resource_CN.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(cnVal)==false)
                    {
                        return cnVal;
                    }
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }


        public static string GetString(string key, string defaultValue, params object[] args)
        {
            try
            {
                if (IsEnglish)
                {//查找英文资源
                    string enVal = ICP.WF.WorkFlowDesigner.Resources.Resource_EN.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(enVal)==false)
                    {
                        return string.Format(enVal, args);
                    }
                }
                else
                {//查找中文资源
                    string cnVal = ICP.WF.WorkFlowDesigner.Resources.Resource_CN.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(cnVal)==false)
                    {
                        return string.Format(cnVal, args); ;
                    }
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 弹出一个是提示"是否删除"的对话框
        /// </summary>
        /// <returns></returns>
        public static bool EnquireIsDeleteCurrentData()
        {
            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Srue Delete Current Data?" : "是否删除当前数据?",
                                                 LocalData.IsEnglish ? "Tip" : "提示",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }

    }

  
}
