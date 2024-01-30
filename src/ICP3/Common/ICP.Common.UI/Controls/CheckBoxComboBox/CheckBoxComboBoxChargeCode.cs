namespace ICP.Common.UI.Controls
{
    using ICP.Framework.ClientComponents.Controls;
    using ICP.Framework.CommonLibrary.Client;
    using System;

    /// <summary>
    /// 船名下拉勾选框
    /// </summary>
    public class CheckBoxComboBoxChargeCode : COCheckBoxComboBox
    {
        private bool isFirstTimeEnter = true;

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
                Enter += CheckBoxComboBoxChargingCode_Enter;
                Disposed += delegate
                {
                    Enter -= CheckBoxComboBoxChargingCode_Enter;
                };
            }
        }

        void CheckBoxComboBoxChargingCode_Enter(object sender, EventArgs e)
        {
            if (isFirstTimeEnter)
            {
                AddItems();
                isFirstTimeEnter = false;
            }
        }
        private void AddItems()
        {
            DataHelper.SetCheckBoxComboBoxChargeCode(this);
        }
    }
}
