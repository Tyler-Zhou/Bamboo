using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using DevExpress.XtraEditors.Controls;

namespace ICP.ReportCenter.UI.Comm.Controls
{  
    /// <summary>
    /// 币种下拉单选框
    /// </summary>
    public class CurrencyLWImageComboBoxEdit : LWImageComboBoxEdit
    {
        private bool addAllItem = true;
        private bool addItemsWhenLoad = false;
        private bool showAllItemWhenLoad = true;
        /// <summary>
        /// 是否在控件加载时默认显示全选选项，默认为是
        /// </summary>
        public bool ShowAllItemWhenLoad
        {
            get
            {
                return this.showAllItemWhenLoad;
            }
            set
            {
                this.showAllItemWhenLoad = value;
            }
        }
        /// <summary>
        /// 是否在控件加载时加载所有选项
        /// </summary>
        public bool AddItemsWhenLoad
        {
            get
            {
                return this.addItemsWhenLoad;
            }
            set
            {
                this.addItemsWhenLoad = value;
            }
        }
        /// <summary>
        /// 是否添加一个全选选项，默认为是
        /// </summary>
        public bool AddAllItem
        {
            get
            {
                return this.addAllItem;
            }
            set
            {
                this.addAllItem = value;
            }
        }
        /// <summary>
        /// 当前选择的币别Id，如果选择为"全部",则返回Guid.Empty
        /// </summary>
        public Guid SelectedCurrencyId
        {
            get
            {
                return (Guid)this.EditValue;
            }
        }
        /// <summary>
        /// 当前选择的币别名称
        /// </summary>
        public string SelectedCurrencyName
        {
            get
            {
                return (Guid)this.EditValue == Guid.Empty ? string.Empty : this.Text;
            }
        }

        public RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (this.ShowAllItemWhenLoad)
            {
                this.ShowSelectedValue(Guid.Empty, LocalData.IsEnglish ? "ALL" : " 全部");
            }
            if (!LocalData.IsDesignMode && this.AddItemsWhenLoad)
            {
                AddCurrencyItems();
            }
            if (!LocalData.IsDesignMode && !this.AddItemsWhenLoad)
            {
                this.OnFirstEnter += this.OnFirstEnterHandle;

                this.Disposed += delegate
                {
                    this.OnFirstEnter -= OnFirstEnterHandle;
                };
            }
        }
        private void OnFirstEnterHandle(object sender, EventArgs e)
        {
            AddCurrencyItems();
        }

        private void AddCurrencyItems()
        {
            List<CurrencyList> currencys = RateHelper.Currencys;
            if (this.AddAllItem)
            {
                CurrencyList emptyCurrency = new CurrencyList { Code = LocalData.IsEnglish ? "ALL" : " 全部", ID = Guid.Empty };
                currencys.Insert(0, emptyCurrency);
            }
            this.Properties.BeginUpdate();
            this.Properties.Items.Clear();
            foreach (var item in currencys)
            {
                this.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }
            this.Properties.EndUpdate();
        }


    }
}
