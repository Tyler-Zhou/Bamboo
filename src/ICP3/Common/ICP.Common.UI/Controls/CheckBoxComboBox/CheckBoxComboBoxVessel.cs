namespace ICP.Common.UI.Controls
{
    using ICP.Framework.ClientComponents.Controls;
    using ICP.Framework.CommonLibrary.Client;
    using System;

    /// <summary>
    /// 船名下拉勾选框
    /// </summary>
    public class CheckBoxComboBoxVessel : COCheckBoxComboBox
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
                Enter += CheckBoxComboBoxVessel_Enter;
                Disposed += delegate
                {
                    Enter -= CheckBoxComboBoxVessel_Enter;
                };
            }
        }

        void CheckBoxComboBoxVessel_Enter(object sender, EventArgs e)
        {
            if (isFirstTimeEnter)
            {
                AddItems();
                isFirstTimeEnter = false;
            }
        }
        private void AddItems()
        {
            DataHelper.SetCheckBoxComboBoxVessels(this);
        }
    }
}
