using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace ICP.ReportCenter.UI.Comm.Controls
{
    /// <summary>
    /// 
    /// </summary>
   public class CompanyTreeCheckControl:TreeCheckControl
    {
        /// <summary>
        /// 
        /// </summary>
       public ReportCenterHelper ReportCenterHelper
       {
           get
           {
               return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
           }
       }

        /// <summary>
        /// 忽略权限控制
        /// </summary>
        [Browsable(true)]
        [Editor(typeof(BooleanDesignProperty), typeof(UITypeEditor))]
        public bool IgnorePermissionsControl
        {
            get;
            set;
        }

        /// <summary>
        /// 是否显示部门，默认不显示，只显示公司
        /// </summary>
        [Browsable(true)]
       [Editor(typeof(BooleanDesignProperty), typeof(UITypeEditor))]
       public bool ShowDepartment
       {
           get;
           set;
       }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
       protected override void OnLoad(EventArgs e)
       {
           base.OnLoad(e);
           if (!LocalData.IsDesignMode)
           {
               AddCompanyItems();
           }
       }
        /// <summary>
        /// 
        /// </summary>
       public void AddCompanyItems()
       {     
           List<OrganizationList> companys =null;

            if (IgnorePermissionsControl)
            {
                companys = ReportCenterHelper.GetOrganizationList;
            }
            else
            {
                if (!ShowDepartment)
                {
                    companys = ReportCenterHelper.UserCompanyList;
                }
                else
                {
                    companys = ReportCenterHelper.GetUserOrganizationList;
                }
            }

            List<TreeCheckControlSource> tss = new List<TreeCheckControlSource>();
           foreach (var item in companys)
           {
               tss.Add(new TreeCheckControlSource { ID = item.ID, ParentID = item.ParentID, Name = LocalData.IsEnglish ? item.EShortName : item.CShortName });
           }
           this.SetSource(tss);
           if (!ShowDepartment)
           {
               this.EditValue = new List<Guid>() { Utility.UserDefaultCompanyID };
           }
           else
           {
               this.EditValue = new List<Guid>() { LocalData.UserInfo.DefaultDepartmentID };
           }

       }
       /// <summary>
       /// 所选择的公司Ids
       /// </summary>
       public List<Guid> CompanyIDs
       {
           get
           {
              this.GetAllEditValue.Remove(new Guid("701ACD43-D49B-422B-83A9-ACB56B696995"));
              return this.GetAllEditValue;
           }
       }
    }
   public class BooleanDesignProperty : UITypeEditor
   {
       public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
       {
           return UITypeEditorEditStyle.DropDown;
       }

       public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
       {
           var edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

           ListBox lb = new ListBox();
           lb.Items.Add(true);
           lb.Items.Add(false);
           if (value != null)
           {
               lb.SelectedItem = value;
           }


           edSvc.DropDownControl(lb);

           value = lb.SelectedItem;

           return value;
       }
   }
}
