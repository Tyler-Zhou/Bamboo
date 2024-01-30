

namespace ICP.Common.UI.Controls
{
    using System;
    using System.Collections.Generic;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.ClientComponents.Controls;
    using ICP.Framework.CommonLibrary.Client;

    /// <summary>
    /// 业务报表航线下拉勾选框控件
    /// </summary>
    public class TreeCheckControlShippingLine : TreeCheckControl
    {
        /// <summary>
        /// 是否默认全选
        /// </summary>
        public bool IsDefaultAllCheck { get; set; }

        private List<ShippingLineList> shippingLines;
        /// <summary>
        /// 数据源
        /// </summary>
        private List<ShippingLineList> DataSource
        {
            get
            {
                if (shippingLines == null)
                    return new List<ShippingLineList>();
                return shippingLines;
            }
            set
            {
                shippingLines = value;
            }
        }

        private ICPCommUIHelper DataHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                this.Enter += new EventHandler(TreeCheckControl_Enter);
                this.Disposed += delegate
                {
                    this.Enter -= this.TreeCheckControl_Enter;
                };
            }
        }

        private bool isFirstTimeEnter = true;
        void TreeCheckControl_Enter(object sender, EventArgs e)
        {
            if (!isFirstTimeEnter)
            {
                return;
            }
            DataSource = DataHelper.SetTreeCheckShippingLine(this);
            isFirstTimeEnter = false;
            if (IsDefaultAllCheck)
            {
                AllCheck();
            }
        }
    }
}
