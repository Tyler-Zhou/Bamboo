using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;

using ICP.TMS.ServiceInterface;

namespace ICP.TMS.UI
{
    /// <summary>
    /// 拖车资料查询界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class TruckSearchPanel : BaseSearchPart
    {
        public TruckSearchPanel()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.OnSearched = null;
                foreach (Control item in navBarGroupBaseInfo.Controls)
                {
                    item.KeyDown -= item_KeyDown;
                }
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public ITruckBookingService TruckBookingService
        {
            get
            {
                return ServiceClient.GetService<ITruckBookingService>();
            }
        }

        #endregion

        #region 初始化 
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                InitControls();
                SetKeyDownToSearch();
            }
        }
        /// <summary>
        /// 注册控件的快捷查询
        /// </summary>
        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupBaseInfo.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        /// <summary>
        /// 执行快捷查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
            {
                this.btnSearch.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                this.btnClear.PerformClick();
            }
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<TruckDateSeachType>> dataType = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<TruckDateSeachType>(LocalData.IsEnglish);
            this.cmbDataType.Properties.BeginUpdate();
            foreach (var item in dataType)
            {
                cmbDataType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDataType.Properties.EndUpdate();

            this.cmbDataType.SelectedIndex = 0;

        }
        #endregion


        #region 重写
        /// <summary>
        /// 查询
        /// </summary>
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        public override event SearchResultHandler OnSearched;

        /// <summary>
        /// 获得查询的数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            try
            {
               List<TruckDataList>  list = TruckBookingService.GetTruckDataList(
                    this.txtTruckNo.Text,
                    this.txtType.Text,
                    (TruckDateSeachType)this.cmbDataType.SelectedIndex,
                    this.ckbValid.Checked,
                    this.dmcTime.From,
                    this.dmcTime.To,
                    LocalData.IsEnglish);

               return list;

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); 
                return null;
            }
         
        }

        #endregion
        #region 清空
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtTruckNo.Text = string.Empty;
            this.txtType.Text = string.Empty;

            this.cmbDataType.SelectedIndex = 0;

        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        #endregion


    }
}
