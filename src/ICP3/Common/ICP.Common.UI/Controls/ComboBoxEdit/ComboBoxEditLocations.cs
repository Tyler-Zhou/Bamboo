

namespace ICP.Common.UI.Controls
{
    using ICP.Framework.ClientComponents.Controls;
    using ICP.Framework.CommonLibrary.Client;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class ComboBoxEditLocations : LWImageComboBoxEdit
    {
        private bool _ShowAllItemWhenLoad = true;
        /// <summary>
        /// 是否在控件加载时默认显示全选选项，默认为是
        /// </summary>
        public bool ShowAllItemWhenLoad { get { return _ShowAllItemWhenLoad; } set { _ShowAllItemWhenLoad = value; } }

        private bool _AddItemsWhenLoad = false;
        /// <summary>
        /// 是否在控件加载时加载所有选项
        /// </summary>
        public bool AddItemsWhenLoad { get { return _AddItemsWhenLoad; } set { _AddItemsWhenLoad = value; } }

        private bool _AddAllItem = false;
        /// <summary>
        /// 是否添加一个全选选项，默认为是
        /// </summary>
        public bool AddAllItem { get { return _AddAllItem; } set { _AddAllItem = value; } }

        /// <summary>
        /// 当前选择的币别Id，如果选择为"全部",则返回Guid.Empty
        /// </summary>
        public Guid SelectedCurrencyId
        {
            get
            {
                return (Guid)EditValue;
            }
        }
        /// <summary>
        /// 当前选择的币别名称
        /// </summary>
        public string SelectedCurrencyName
        {
            get
            {
                return (Guid)EditValue == Guid.Empty ? string.Empty : Text;
            }
        }

        public ICPCommUIHelper CurrencyHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (ShowAllItemWhenLoad)
            {
                ShowSelectedValue(Guid.Empty, LocalData.IsEnglish ? "ALL" : " 全部");
            }
            if (!LocalData.IsDesignMode && AddItemsWhenLoad)
            {
                AddItems();
            }
            if (!LocalData.IsDesignMode && !AddItemsWhenLoad)
            {
                OnFirstEnter += OnFirstEnterHandle;

                Disposed += delegate
                {
                    OnFirstEnter -= OnFirstEnterHandle;
                };
            }
        }
        private void OnFirstEnterHandle(object sender, EventArgs e)
        {
            AddItems();
        }

        private void AddItems()
        {
            CurrencyHelper.SetComboxLocations(this);
        }
    }
}
