using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.ReportCenter.UI.FinanceOEReports._7凭证明细列表
{
    [ToolboxItem(false)]
    public partial class UCBackups : BasePart
    {
        public UCBackups()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
            for (int i = 1; i < 13; i++)
            {
                cmbMonth.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(i.ToString(), (object)i));
            }
            for (int i = 0; i < 4; i++)
            {
                cmbYear.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem((DateTime.Now.Year - i).ToString(), (object)(DateTime.Now.Year - i)));
            }
        }

        public List<Guid> CompanyIDs
        {
            get;
            set;
        }

        public string YearMonth
        {
            get;
            set;
        }

        /// <summary>
        /// 确认备份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbYear.SelectedIndex == -1 || cmbMonth.SelectedIndex == -1)
            {
                MessageBox.Show("日期信息不完整！");
                return;
            }

            string month = cmbMonth.Text.Length == 1 ? ("0" + cmbMonth.Text) : cmbMonth.Text;
            YearMonth = cmbYear.Text + month;
            CompanyIDs = treeBoxSalesDep.GetAllEditValue;
            this.FindForm().DialogResult = DialogResult.OK;
            this.FindForm().Close();
        }

        /// <summary>
        /// 取消备份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
