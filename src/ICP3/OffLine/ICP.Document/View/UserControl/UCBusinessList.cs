#region Comment

/*
 * 
 * FileName:    UCListBusinessPart.cs
 * CreatedOn:   2014/5/14 星期三 13:54:45
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->业务数据面板，显示业务数据信息
 * History：
 * 
 * 
 * 
 * 
 */

#endregion


using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;
namespace ICP.Document
{
    public partial class UCBusinessList : ViewBase, IVBusiness
    {
        #region 成员变量
        /// <summary>
        /// 记录LoadingForm的ID
        /// </summary>
        private int tokenID = -1;
        /// <summary>
        /// 当前业务信息
        /// </summary>
        public BusinessInfo CurrentBusiness
        {
            get { return this.bdscBusiness.Current as BusinessInfo; }
        }
        #endregion

        #region 构造方法
        public UCBusinessList()
        {
            InitializeComponent();
            barItemSearch.ItemClick += new ItemClickEventHandler(barItemSearch_ItemClick);
            gdvwBusiness.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(gdvwView_CustomDrawRowIndicator);
            this.Disposed += (sender, e) =>
            {
                barItemSearch.ItemClick -= new ItemClickEventHandler(barItemSearch_ItemClick);
                gdvwBusiness.CustomDrawRowIndicator -= new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(gdvwView_CustomDrawRowIndicator);
            };
        }

        protected override object CreatePresenter()
        {
            return new PBusiness(this);
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 查询订单
        /// </summary>
        public void barItemSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                tokenID = LoadingUtility.ShowFormLoading(this, "Loading.....");
                string sParam1 = "";
                if (txtSParam1.EditValue != null)
                    sParam1 = txtSParam1.EditValue.ToString();
                this.Search_ItemClick(this, new BusEventArgs() { SearchParam1 = sParam1 });
            }
            catch { }
            finally
            {
                LoadingUtility.CloseFormLoading(tokenID);
            }
        }
        /// <summary>
        /// 绘制行号
        /// </summary>
        private void gdvwView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString().Trim();
            }
        }
        #endregion

        #region IVBusiness 成员
        /// <summary>
        /// 查询事件
        /// </summary>
        public event EventHandler<BusEventArgs> Search_ItemClick;
        /// <summary>
        /// 填充业务订单信息
        /// </summary>
        /// <param name="businessList">业务订单集合</param>
        public void FillBusinessInfo(List<BusinessInfo> businessList)
        {
            bdscBusiness.DataSource = null;
            bdscBusiness.DataSource = businessList;
        }

        #endregion
    }
}
