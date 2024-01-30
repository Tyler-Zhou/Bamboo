using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.MailCenter.Business.ServiceInterface
{

    public enum SubGridType
    {
        MailLink4CarrierAP,
        MailLink4CarrierMBL,
        MailLink4CarrierSO
    }
    /// <summary>
    /// 工具栏项类型
    /// </summary>
    public enum MenuItemType
    {
        /// <summary>
        /// 下拉分裂按钮
        /// </summary>
        SubButton = 1,
        /// <summary>
        /// 按钮
        /// </summary>
        Button = 2,
        /// <summary>
        /// 文本框
        /// </summary>
        TextBox = 3,
  
        /// <summary>
        /// 标签
        /// </summary>
        Label=5,
        /// <summary>
        /// 勾选框
        /// </summary>
        CheckBox=6,
        /// <summary>
        /// 下拉选框
        /// </summary>
        ComboBox=7,
    }
    /// <summary>
    /// 列编辑控件类型
    /// </summary>
    public enum ColumnEditType
    {  
        /// <summary>
        /// 普通文本
        /// </summary>
       Text=1,

        /// <summary>
        /// 勾选框
        /// </summary>
       Checkbox=2,
        /// <summary>
        /// 图片下拉选框
        /// </summary>
       ImageComboBox=3,
        /// <summary>
        /// 多行文本
        /// </summary>
        Memo=4,
        
    }
    /// <summary>
    /// 上下文菜单项类型
    /// </summary>
    public enum ContexuMenuItemType
    {  
        /// <summary>
        /// 菜单项
        /// </summary>
       MenuItem=1,
        /// <summary>
        /// 分隔符
        /// </summary>
       Separator=2,
    }
    /// <summary>
    /// 业务类型
    /// </summary>
    public enum BusinessType
    { 
     [MemberDescription("海运出口","Ocean Export")]
     OE=1,
    [MemberDescription("海运进口","Ocean Import")]
     OI=2,
    [MemberDescription("空运出口", "Air Export")]
     AE=3,
    [MemberDescription("空运进口", "Air Import")]
     AI=4,
    [MemberDescription("其他", "Other")]
     Other=5
    }
    
}
