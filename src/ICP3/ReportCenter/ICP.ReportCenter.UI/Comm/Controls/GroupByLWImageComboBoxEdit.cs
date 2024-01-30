using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.ReportCenter.UI.Comm.Controls
{ 
    /// <summary>
    /// 业务报表分组类型下拉选框
    /// </summary>
  public class GroupByLWImageComboBoxEdit:ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit
    {
      protected override void OnLoaded()
      {
          base.OnLoaded();
          if (!LocalData.IsDesignMode)
          {
              this.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Job Type" : "业务类型"));
              this.SelectedIndex = 0;
              this.OnFirstEnter += this.OnFirstEnterHandle;
              this.Disposed += delegate
              {
                  this.OnFirstEnter -= this.OnFirstEnterHandle;
              };
          }
      }
   
        public ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }

        void OnFirstEnterHandle(object sender, EventArgs e)
        {
            BuildGroupByItems();
        }
        private void BuildGroupByItems()
        {
            this.Properties.BeginUpdate();
            this.Properties.Items.Clear();
            this.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Job Type" : "业务类型"));
            this.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Sales Type" : "揽货方式"));
            this.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Ship Owner" : "船公司"));
            this.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Agent" : "代理人"));
            this.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Line" : "航线"));
            this.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Company" : "业务发生地"));
            this.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Sales" : "业务员"));
            this.Properties.EndUpdate();
            this.SelectedIndex = 0;
        }
    }
}
