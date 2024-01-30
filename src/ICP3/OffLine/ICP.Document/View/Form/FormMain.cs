#region Comment

/*
 * 
 * FileName:    FormMain.cs
 * CreatedOn:   2014/5/14 星期三 9:40:37
 * CreatedBy:   taylor
 * 
 * Description：
 *      ->主窗体：显示业务、文档数据面板
 * History：
 * 
 */

#endregion

using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using System;

namespace ICP.Document
{
    public partial class FormMain : FormBase
    {
        #region 构造方法
        /// <summary>
        /// 构造函数
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            this.KeyUp += new KeyEventHandler(FormMain_KeyUp);
            ucBusinessList.bdscBusiness.PositionChanged += new EventHandler(BusinessPositionChanged);
            this.Disposed += (sender, e) =>
            {
                this.KeyUp -= new KeyEventHandler(FormMain_KeyUp);
                ucBusinessList.bdscBusiness.PositionChanged -= new EventHandler(BusinessPositionChanged);
            };
        }

        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体按键事件
        /// </summary>
        void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ucBusinessList.barItemSearch_ItemClick(null, null);
            }
        }
        /// <summary>
        /// 业务数据行改变事件
        /// </summary>
        void BusinessPositionChanged(object sender, EventArgs e)
        {
            ucDocumentList1.bdscDocument.DataSource = null;
            if (ucBusinessList.CurrentBusiness == null)
                return;
            ucDocumentList1.GetDocumentByOperationID(ucBusinessList.CurrentBusiness.OperationID);
        }
        #endregion
    }
}
