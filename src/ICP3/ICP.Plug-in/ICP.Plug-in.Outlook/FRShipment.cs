using ICP.MailCenterFramework.UI;
using Microsoft.Office.Tools.Outlook;
using System;
using System.Windows.Forms;

namespace ICP.Plug_in.Outlook
{
    partial class FRShipment
    {
        /// <summary>
        /// 延迟加载控件计时器
        /// </summary>
        Timer _SetPartHeight;
        /// <summary>
        /// 关联邮件面板
        /// </summary>
        MailCenterUI _PartMailCenter;

        #region 窗体区域工厂

        [FormRegionMessageClass(FormRegionMessageClassAttribute.Note)]
        [FormRegionName("Shipment")]
        public partial class FRShipmentFactory
        {
            // 在初始化窗体区域之前发生。
            // 若要阻止窗体区域出现，请将 e.Cancel 设置为 True。
            // 使用 e.OutlookItem 获取对当前 Outlook 项的引用。
            private void FRShipmentFactory_FormRegionInitializing(object sender, FormRegionInitializingEventArgs e)
            {
            }
        }

        #endregion

        // 在显示窗体区域之前发生。
        // 使用 this.OutlookItem 获取对当前 Outlook 项的引用。
        // 使用 this.OutlookFormRegion 获取对窗体区域的引用。
        private void FRShipment_FormRegionShowing(object sender, EventArgs e)
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
                    _SetPartHeight = new Timer();
                    _SetPartHeight.Interval = 1000;
                    _SetPartHeight.Tick += SetPartHeight_Tick;
                }
                _SetPartHeight.Start();
            }
        }

        // 在关闭窗体区域时发生。
        // 使用 this.OutlookItem 获取对当前 Outlook 项的引用。
        // 使用 this.OutlookFormRegion 获取对窗体区域的引用。
        private void FRShipment_FormRegionClosed(object sender, EventArgs e)
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
        private void SetPartHeight_Tick(object sender, EventArgs e)
        {
            try
            {
                _SetPartHeight.Stop();
                _SetPartHeight.Enabled = false;
                if (!_PartMailCenter.Enabled)
                    return;
                if (Parameter.FlagFinish != 0 && Parameter.FlagFinish != 2) 
                    return;
                Parameter.FlagFinish = 1;
                object outlookItem = OutlookItem;
                AutoAssociate(outlookItem);
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
        private void AutoAssociate(object item)
        {
            int num = 0;
            _PartMailCenter.OnCurrentMail_Changed(item, out num);
            Height = num <= 2 ? 45 : 90;
            OutlookFormRegion.Reflow();//滚动条自动出现
        }
    }
}
