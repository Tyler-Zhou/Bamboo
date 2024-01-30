using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using ICP.MailCenterFramework.UI;
using System.Windows.Forms;
using System.IO;
namespace ICP_OutlookAddIn
{
    public partial class ShipmentFormRegion
    {
        /// <summary>
        /// 延迟加载控件计时器
        /// </summary>
        System.Windows.Forms.Timer _SetPartHeight;
        /// <summary>
        /// 关联邮件面板
        /// </summary>
        MailCenterUI _PartMailCenter;

        #region 窗体区域工厂
        private object syncObj = new object();
        [Microsoft.Office.Tools.Outlook.FormRegionMessageClass(Microsoft.Office.Tools.Outlook.FormRegionMessageClassAttribute.Note)]
        [Microsoft.Office.Tools.Outlook.FormRegionName("ICP-OutlookAddIn.ShipmentFormRegion")]
        public partial class ShipmentFormRegionFactory
        {
            // 在初始化窗体区域之前发生。
            // 若要阻止窗体区域出现，请将 e.Cancel 设置为 True。
            // 使用 e.OutlookItem 获取对当前 Outlook 项的引用。
            private void ShipmentFormRegionFactory_FormRegionInitializing(object sender, Microsoft.Office.Tools.Outlook.FormRegionInitializingEventArgs e)
            {


            }
        }
        #endregion

        // 在显示窗体区域之前发生
        // 使用 this.OutlookItem 获取对当前 Outlook 项的引用。
        // 使用 this.OutlookFormRegion 获取对窗体区域的引用。
        private void ShipmentFormRegion_FormRegionShowing(object sender, System.EventArgs e)
        {
            if (HelpMailStore.IsNeedLoginICP)
            {
                if (_PartMailCenter == null)
                    _PartMailCenter = new MailCenterUI { Dock = DockStyle.Fill };
                if (!Controls.Contains(_PartMailCenter))
                    Controls.Add(_PartMailCenter);
                //设置高度
                if (_SetPartHeight == null)
                {
                    _SetPartHeight = new System.Windows.Forms.Timer();
                    _SetPartHeight.Interval = 1000;
                    _SetPartHeight.Tick += SetPartHeight_Tick;
                }
                _SetPartHeight.Start();
            }
        }

        // 在关闭窗体区域时发生
        // 使用 this.OutlookItem 获取对当前 Outlook 项的引用。
        // 使用 this.OutlookFormRegion 获取对窗体区域的引用。
        private void ShipmentFormRegion_FormRegionClosed(object sender, System.EventArgs e)
        {
            if (_PartMailCenter != null)
                _PartMailCenter.Dispose();
            _PartMailCenter = null;
            if (_SetPartHeight != null)
            {
                _SetPartHeight.Enabled = false;
                _SetPartHeight.Dispose();
            }
            _SetPartHeight = null;
        }

        /// <summary>
        /// 刷新子面板数据并设置面板高度
        /// </summary>
        public void SetPartHeight_Tick(object sender, EventArgs e)
        {
            try
            {
                _SetPartHeight.Stop();
                _SetPartHeight.Enabled = false;
                if (!_PartMailCenter.Enabled)
                    return;
                if (Parameter.FlagFinish == 0 || Parameter.FlagFinish == 2)
                {
                    Parameter.FlagFinish = 1;
                    object outlookItem = this.OutlookItem;
                    AutoAssociate(outlookItem);
                }
            }
            catch
            {
                Parameter.FlagFinish = 2;
            }
        }
        /// <summary>
        /// 自动关联邮件并设置插件高度
        /// </summary>
        /// <param name="item"></param>
        public void AutoAssociate(object item)
        {
            int num = 0;
            _PartMailCenter.OnCurrentMail_Changed(item, out num);
            if (num <= 2)
            {
                this.Height = 45;
            }
            else
            {
                this.Height = 90;
            }
            this.OutlookFormRegion.Reflow();//滚动条自动出现
        }
    }
}
