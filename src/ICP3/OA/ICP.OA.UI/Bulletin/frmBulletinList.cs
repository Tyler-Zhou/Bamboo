using System;
using System.Collections.Generic;
using ICP.OA.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.OA.UI.Bulletin
{   
    /// <summary>
    /// winform公告列表显示界面
    /// </summary>
    public partial class frmBulletinList : DevExpress.XtraEditors.XtraForm
    {
        #region 字段或属性
        private BulletinData _bulletin;

        //[ServiceDependency]
        //public IBulletinService BulletinService { get; set; }
        #endregion
        #region 构造函数
        public frmBulletinList()
        {
            InitializeComponent();
            Locale();
         
        }

      
        public frmBulletinList(BulletinData bulletin):this()
        {
            _bulletin = bulletin;
            ShowBulletin(BulletinShowType.Single);
        }
        //public frmBulletinList(BulletinShowType type):this()
        //{
        //    ShowBulletin(type);
        //}
        
        #endregion
        #region 工具栏事件处理
        private void barButtonClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItemToday_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowBulletin(BulletinShowType.Today);
        }

        private void barButtonItemAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowBulletin(BulletinShowType.All);
        }

      
        #endregion

        #region 辅助方法
        private void ShowBulletin(BulletinShowType showType)
        {
            List<BulletinData> bulletins = GetBulletins(showType);
            InnerShowBulletin(bulletins);
        }
        /// <summary>
        /// 当前仅处理单个通知的显示
        /// </summary>
        /// <param name="bulletins"></param>
        private void InnerShowBulletin(List<BulletinData> bulletins)
        {
            BulletinData data = _bulletin;
            this.memoEditSubject.Text = data.Subject;
            this.txtPublisherName.Text = LocalData.IsEnglish ? data.PublisherEName : data.PublisherCName;
            this.txtPublishTime.Text = data.CreateTime.ToString("yyyy-MM-dd HH:mm");
            this.txtPriority.Text = data.Priority.ToString();
            this.memoEditContent.Text = data.Content;
            this.Text = this.Text + "-" + data.Subject;
        }
        private List<BulletinData> GetBulletins(BulletinShowType showType)
        {
            List<BulletinData> bulletins = new List<BulletinData>();
            if (showType == BulletinShowType.Single)
            {
                bulletins = new List<BulletinData> { _bulletin };
            }
            //else if (showType == BulletinShowType.Today)
            //{
            //    DateTime now = DateTime.Now;
            //    DateTime fromTime = DateTime.SpecifyKind(new DateTime(now.Year, now.Month, now.Day, 0, 0, 0), DateTimeKind.Unspecified);
            //    DateTime toTime = DateTime.SpecifyKind(new DateTime(now.Year, now.Month, now.Day, 23, 59, 59), DateTimeKind.Unspecified);
            //    bulletins = BulletinService.GetBulletins(LocalData.UserInfo.LoginID, fromTime, toTime);
            //}
            //else if (showType == BulletinShowType.All)
            //{
            //    bulletins = BulletinService.GetBulletins(LocalData.UserInfo.LoginID, string.Empty, string.Empty, string.Empty, null, null, 0);
            //}
            else
            {
                throw new NotImplementedException(showType.ToString());
            }
            return bulletins;
        }
        private void Locale()
        {
            if (LocalData.IsEnglish)
            {
                this.Text = "Bulletin";
                this.lblSubject.Text = "Subject";
                this.lblPublisherName.Text = "Publisher";
                this.lblPublishTime.Text = "Publish Time";
                this.lblPriority.Text = "Priority";
                this.lblContent.Text = "Content";

            }
        }
        #endregion
    }
    public enum BulletinShowType
    { 
      Single,
        Today,
        All
    }
}