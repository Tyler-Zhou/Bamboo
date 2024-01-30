namespace ICP.Common.UI.Controls
{
    using ICP.Framework.ClientComponents.Controls;
    using ICP.Framework.CommonLibrary.Client;
    using System;

    /// <summary>
    /// 船东下拉勾选框
    /// </summary>
    public class CheckBoxComboBoxCarrier : COCheckBoxComboBox
    {
        private bool isFirstTimeEnter = true;
        /// <summary>
        /// 是否默认全选
        /// </summary>
        public bool IsDefaultAllCheck { get; set; }
        /// <summary>
        /// 数据帮助类
        /// </summary>
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
                Enter += CheckBoxComboBoxVessel_Enter;
                Disposed += delegate
                {
                    Enter -= CheckBoxComboBoxVessel_Enter;
                };
            }
        }
        void CheckBoxComboBoxVessel_Enter(object sender, EventArgs e)
        {
            AddItems();
        }
        private void AddItems()
        {
            if (!isFirstTimeEnter)
            {
                return;
            }
            DataHelper.SetCheckBoxComboBoxCarriers(this);
            isFirstTimeEnter = false;
            if (IsDefaultAllCheck)
            {
                DefaultAllCheck();
            }
        }
    }
}
