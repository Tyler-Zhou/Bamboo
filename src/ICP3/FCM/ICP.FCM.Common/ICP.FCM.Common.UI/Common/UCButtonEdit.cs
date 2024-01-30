using System;
using DevExpress.XtraEditors;
using System.ComponentModel;
namespace ICP.FCM.Common.UI
{     
   /// <summary>
   /// 自定义ButtonEdit控件
   /// 覆盖基类Tag属性，以增添Tag值改变事件
   /// </summary>
    [ToolboxItem(true)]
     public class UCButtonEdit : ButtonEdit
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.TagChanged = null;
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// Tag属性值改变时触发值改变事件
        /// </summary>
        public event EventHandler TagChanged;
        /// <summary>
        /// 获取或设置包含有关控件的数据的对象。
        /// </summary>
        [Bindable(true)]
        [DefaultValue("")]
        [Localizable(false)]
        [TypeConverter(typeof(StringConverter))]
        public new object Tag
        {
            get { return base.Tag; }
            set {
                base.Tag = value;
                if (TagChanged != null)
                {
                    TagChanged(this, EventArgs.Empty);
                }
               
            }
        } 
            
            
    }
}
