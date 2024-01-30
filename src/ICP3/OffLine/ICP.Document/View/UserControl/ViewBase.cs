#region Comment

/*
 * 
 * FileName:    ViewBase.cs
 * CreatedOn:   2014/5/14 星期三 16:19:53
 * CreatedBy:   taylor
 * 
 * Description：
 *      ->自定义用户控件基类
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.ComponentModel;
using DevExpress.XtraEditors;

namespace ICP.Document
{
    public partial class ViewBase : XtraUserControl
    {
        //View展示数据的逻辑处理类
        private object _presenter;

        public ViewBase()
        {
            InitializeComponent();
            _presenter = this.CreatePresenter();
        }
        //实例化逻辑处理类，确保View实现此方法
        protected virtual object CreatePresenter()
        {
            if (LicenseManager.CurrentContext.UsageMode == LicenseUsageMode.Designtime)
            {
                return null;
            }
            else
            {
                throw new NotImplementedException(string.Format("{0} must override the CreatePresenter method.", this.GetType().FullName));
            }
        }
    }
}
