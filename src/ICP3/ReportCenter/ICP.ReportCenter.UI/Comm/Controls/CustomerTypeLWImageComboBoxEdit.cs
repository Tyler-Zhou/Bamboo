using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraEditors.Controls;

namespace ICP.ReportCenter.UI.Comm.Controls
{  
    /// <summary>
    /// CRM报表客户类型下拉选框
    /// </summary>
   public class CustomerTypeLWImageComboBoxEdit:ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit
    {
       protected override void OnLoaded()
       {
           base.OnLoaded();
           if (!LocalData.IsDesignMode)
           {
               this.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "All" : "全部","4"));
               this.SelectedIndex = 0;
               this.OnFirstEnter += this.OnFirstEnterHandle;
               this.Disposed += delegate
               {
                   this.OnFirstEnter -= this.OnFirstEnterHandle;
               };
           }
       }
       private void OnFirstEnterHandle(object sender, EventArgs e)
       {
           this.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "NVOCC" : "同行","0"));
           this.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "Direct Client" : "直客","1"));
           this.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "Business" : "商务","2"));
           this.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "Other" : "其它","3"));
       }
    }
}
