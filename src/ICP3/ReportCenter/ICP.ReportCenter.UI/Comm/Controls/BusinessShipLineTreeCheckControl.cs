using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.ReportCenter.UI.Comm.Controls
{
    /// <summary>
    /// 业务报表航线下拉勾选框控件
    /// </summary>
    public class BusinessShipLineTreeCheckControl : TreeCheckControl
    {
        /// <summary>
        /// 是否首次加载
        /// </summary>
        private bool isFirstTimeEnter = true;
        /// <summary>
        /// 报表帮助类
        /// </summary>
        ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }

        /// <summary>
        /// 所选择的航线Ids
        /// </summary>
        public List<Guid> ItemIDs
        {
            get
            {
                return this.GetAllEditValue;
            }
        }

        /// <summary>
        /// 重写的加载事件
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                this.Enter += new EventHandler(BusinessShipLineTreeCheckControl_Enter);
                this.Disposed += delegate
                {
                    this.Enter -= this.BusinessShipLineTreeCheckControl_Enter;
                };
            }
        }
        /// <summary>
        /// 获取焦点
        /// </summary>
        void BusinessShipLineTreeCheckControl_Enter(object sender, EventArgs e)
        {
            if (!isFirstTimeEnter)
            {
                return;
            }
            ReportCenterHelper.BuildShipLines(this);
            isFirstTimeEnter = false;
        }

    }
}
