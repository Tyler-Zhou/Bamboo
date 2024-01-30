using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace ICP.ReportCenter.UI.Comm.Controls
{   
    /// <summary>
    /// 带多选客户搜索器的ButtonEdit
    /// </summary>
   public class MutipleCustomerFinderButtonEdit:DevExpress.XtraEditors.ButtonEdit
    {
       public IDataFindClientService DataFindClientService
       {
           get
           {
               return ServiceClient.GetClientService<IDataFindClientService>();
           }
       }
       /// <summary>
       /// 关联的搜索器名称
       /// </summary>
       [Browsable(true)]
       [Editor(typeof(FinderNameDesignProperty), typeof(UITypeEditor))]
       public string FinderName
       {
           get;
           set;
       }
       
       protected override void OnLoaded()
       {
           base.OnLoaded();
           if (!LocalData.IsDesignMode &&!string.IsNullOrEmpty(this.FinderName))
           {
             IDisposable finder=  SearchBoxAdapter.RegisterMultipleSearchBox(DataFindClientService, this, this.FinderName);
             this.Disposed += delegate {
                 if (finder != null)
                 {
                     finder.Dispose();
                     finder = null;
                 }
             
             };
           }
       }
    }
   public class FinderNameDesignProperty : UITypeEditor
   {
       public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
       {
           return UITypeEditorEditStyle.DropDown;
       }

       public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
       {
           var edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

           
  
           ICP.Sys.ServiceInterface.SystemFinderConstants classInstance=new ICP.Sys.ServiceInterface.SystemFinderConstants();
           System.Reflection.FieldInfo[] fields= typeof(ICP.Sys.ServiceInterface.SystemFinderConstants).GetFields();
           List<object> finderNames = new List<object>();
           foreach (var item in fields)
           {
               finderNames.Add(item.GetValue(classInstance));
           }
           ICP.Common.ServiceInterface.CommonFinderConstants classInstance2 = new ICP.Common.ServiceInterface.CommonFinderConstants();
           System.Reflection.FieldInfo[] fields2 = typeof(ICP.Common.ServiceInterface.CommonFinderConstants).GetFields();
           foreach (var item in fields2)
           {
               finderNames.Add(item.GetValue(classInstance2));
           }
           finderNames.Sort();
           ListBox lb = new ListBox();
           foreach (var finder in finderNames)
           {
               lb.Items.Add(finder);
           }
           
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
