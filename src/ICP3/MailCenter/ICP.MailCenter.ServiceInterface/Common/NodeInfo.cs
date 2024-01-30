using System;
using System.Drawing;

namespace ICP.MailCenter.ServiceInterface
{
    /// <summary>
    /// 文件夹节点属性类
    /// </summary>
    public class NodeInfo
    {
        /// <summary>
        /// 前景色
        /// </summary>
        public Color ForeColor { get; set; }
        /// <summary>
        /// 节点显示文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 节点提示消息
        /// </summary>
        public string ToolTip { get; set; }
        /// <summary>
        /// 节点文本字体
        /// </summary>
        public Font NodeFont { get; set; }
        public NodeInfo() { }
        public NodeInfo(Color foreColor, string text, Font nodeFont)
        {
            this.ForeColor = foreColor;
            this.Text = text;
            this.NodeFont = nodeFont;
        }
        /// <summary>
        /// 获取单个节点信息
        /// </summary>
        /// <param name="isTotalCountFolder"></param>
        /// <param name="count"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static NodeInfo GetNodeInfo(bool isTotalCountFolder, int count, string folderName)
        {
            NodeInfo nodeInfo = new NodeInfo();
            if (isTotalCountFolder)
            {
                if (count > 0)
                {
                    nodeInfo.ForeColor = MailUtility.TotalCountColor;
                    nodeInfo.Text = String.Format("{0}[{1}]", folderName, count.ToString());
                    nodeInfo.NodeFont = MailUtility.SpecialFont;
                }
                else
                {
                    nodeInfo.ForeColor = MailUtility.NormalColor;
                    nodeInfo.NodeFont = MailUtility.NormalFont;
                    nodeInfo.Text = folderName;
                }
            }
            else
            {
                if (count > 0)
                {
                    nodeInfo.ForeColor = MailUtility.UnreadCountColor;
                    nodeInfo.Text = String.Format("{0}({1})", folderName, count.ToString());
                    nodeInfo.NodeFont = MailUtility.SpecialFont;
                }
                else
                {
                    nodeInfo.ForeColor = MailUtility.NormalColor;
                    nodeInfo.NodeFont = MailUtility.NormalFont;
                    nodeInfo.Text = folderName;
                }
            }

            nodeInfo.ToolTip = count.ToString();
            return nodeInfo;
        }

        /// <summary>
        /// 通过上次设置文件夹的数量跟现在需要设置文件夹的数量对比，如果相同，就不需要在去设置文件夹了
        /// </summary>
        /// <param name="toolTipText">上次设置过的文件夹数量值</param>
        /// <param name="count">当前设置的文件夹数量值</param>
        /// <returns></returns>
        public static bool HasSameTreeNodeCount(string toolTipText, int count)
        {
            if (string.IsNullOrEmpty(toolTipText))
                return false;

            int lastCount = -1;
            int.TryParse(toolTipText, out lastCount);
            return lastCount == count;
        }
    }

}
