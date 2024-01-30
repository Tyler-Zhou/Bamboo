#region Comment

/*
 * 
 * FileName:    FormBase.cs
 * CreatedOn:   2014/5/14 星期三 15:33:20
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->窗体基类
 *      ->1.继承dev窗体
 *      ->2.窗体居中显示
 *      ->3.窗体ICON设置
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using DevExpress.XtraEditors;

namespace ICP.Document
{
    public partial class FormBase : XtraForm
    {
        public FormBase()
        {
            InitializeComponent();
        }
    }
}
