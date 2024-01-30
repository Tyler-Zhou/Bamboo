using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.Business.Common.UI.QuotedPrice
{
    /// <summary>
    /// 快速查询面板
    /// </summary>
    public partial class QuotedPriceFastSearchPart : BaseSearchPart
    {
        #region Services & Property & Delegate
        #region Service
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 报价服务
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        #endregion

        #region Property
        /// <summary>
        /// 单号搜索类型
        /// </summary>
        protected QPNoSearchType PartNoSearchType
        { get { return (QPNoSearchType)Enum.Parse(typeof(QPNoSearchType), cmbNoSearchType.EditValue.ToString()); } }
        /// <summary>
        /// 港口查询类型
        /// </summary>
        protected QPPortSearchType PartPortSearchType
        { get { return (QPPortSearchType)Enum.Parse(typeof(QPPortSearchType), cmbPortSearchType.EditValue.ToString()); } }
        /// <summary>
        /// 日期搜索类型
        /// </summary>
        protected QPDateSearchType PartDateSearchType
        { get { return (QPDateSearchType)Enum.Parse(typeof(QPDateSearchType), cmbDateSearchType.EditValue.ToString()); } }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? From
        {
            get
            {
                switch (cmbDateType.SelectedIndex)
                {
                    case 0:
                        return null;
                    case 1:
                        return DateTime.Now.Date.AddDays(-7);
                    case 2:
                        return DateTime.Now.Date.AddDays(-30);
                    case 3:
                        return DateTime.Now.Date.AddDays(-365);
                };
                return null;
            }
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? To
        {
            get
            {
                if (cmbDateType.SelectedIndex == 0)
                    return null;
                else
                    return DateTime.Now.DateAttachEndTime();
            }
        }

        #endregion 

        #region Delegate
        /// <summary>
        /// 查询
        /// </summary>
        public override event SearchResultHandler OnSearched; 
        #endregion
        #endregion

        #region Init
        /// <summary>
        /// 快速查询面板
        /// </summary>
        public QuotedPriceFastSearchPart()
        {
            InitializeComponent();
            InitControls();
            RegisterEvent();
            Disposed += (sender, arg) =>
            {
                UnRegisterEvent();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        } 
        #endregion

        #region Control Event
        /// <summary>
        /// 显示查询面板
        /// </summary>
        protected void OnClickMore()
        {
            Workitem.Commands[QPCommandConstants.Command_ShowSearch].Execute();
        }
        /// <summary>
        /// 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    OnSearched(this, GetData());
                }
            }
        }

        /// <summary>
        /// 单号查询类型
        /// </summary>
        private void OncmbNoSearchTypeEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<QPNoSearchType>> noSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<QPNoSearchType>(LocalData.IsEnglish);
            cmbNoSearchType.Properties.BeginUpdate();
            this.cmbNoSearchType.Properties.Items.Clear();
            foreach (var item in noSearchTypes)
            {
                cmbNoSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbNoSearchType.Properties.EndUpdate();
        }
        /// <summary>
        /// 港口查询类型
        /// </summary>
        private void OncmbPortSearchTypeEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<QPPortSearchType>> portSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<QPPortSearchType>(LocalData.IsEnglish);
            cmbPortSearchType.Properties.BeginUpdate();
            this.cmbPortSearchType.Properties.Items.Clear();
            foreach (var item in portSearchTypes)
            {
                cmbPortSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbPortSearchType.Properties.EndUpdate();
        }
        /// <summary>
        /// 日期搜索类型
        /// </summary>
        private void OncmbDateSearchTypeEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<QPDateSearchType>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<QPDateSearchType>(LocalData.IsEnglish);
            cmbDateSearchType.Properties.BeginUpdate();
            this.cmbDateSearchType.Properties.Items.Clear();
            foreach (var item in dateSearchTypes)
            {
                cmbDateSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDateSearchType.Properties.EndUpdate();
        }
        /// <summary>
        /// 日期范围类型
        /// </summary>
        private void OncmbDateTypeEnter(object sender, EventArgs e)
        {
            cmbDateType.Properties.BeginUpdate();
            this.cmbDateType.Properties.Items.Clear();
            cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "UnKnow" : "不确定", 0));
            cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Last Week" : "一周内", 1));
            cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "This Month" : "一月内", 2));
            cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Last Year" : "一年内", 3));
            cmbDateType.Properties.EndUpdate();
        }
        /// <summary>
        /// 文本框内按回车直接查询
        /// </summary>
        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2 || e.KeyCode == Keys.F5) btnSearch.PerformClick();
        }
        #endregion

        #region Custom Method

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            cmbNoSearchType.ShowSelectedValue(QPNoSearchType.All, LocalData.IsEnglish ? "All No" : "全部单号");

            //单号
            cmbNoSearchType.OnFirstEnter += this.OncmbNoSearchTypeEnter;

            cmbPortSearchType.ShowSelectedValue(QPPortSearchType.All, LocalData.IsEnglish ? "All Location" : "全部地点");
            cmbPortSearchType.OnFirstEnter += this.OncmbPortSearchTypeEnter;

            cmbDateSearchType.ShowSelectedValue(QPDateSearchType.All, LocalData.IsEnglish ? "All Date" : "全部日期");
            cmbDateSearchType.OnFirstEnter += this.OncmbDateSearchTypeEnter;

            cmbDateType.ShowSelectedValue(0, LocalData.IsEnglish ? "UnKnow" : "不确定");
            cmbDateType.OnFirstEnter += this.OncmbDateTypeEnter;
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
            txtNo.KeyDown += TextBox_KeyDown;
            stxtPort.KeyDown += TextBox_KeyDown;
            KeyDown += TextBox_KeyDown;

            btnSearch.Click += btnSearch_Click;
        }
        /// <summary>
        /// 移除事件
        /// </summary>
        private void UnRegisterEvent()
        {
            txtNo.KeyDown -= TextBox_KeyDown;
            stxtPort.KeyDown -= TextBox_KeyDown;
            KeyDown -= TextBox_KeyDown;

            btnSearch.Click -= btnSearch_Click;
        }
        /// <summary>
        /// 查询
        /// </summary>
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            int i = 0;
            if (string.IsNullOrEmpty(txtNo.Text.Trim()) && string.IsNullOrEmpty(stxtPort.Text.Trim()) && cmbDateType.SelectedIndex <= 0)
            {
                i = 100;
            }
            return new List<QuotedPriceOrderList>();
        }
        #endregion
    }
}
