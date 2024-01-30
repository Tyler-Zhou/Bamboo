using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 邮件中心业务面板搜索类型
    /// </summary>
    public enum SearchActionType
    {
        /// <summary>
        /// 系统默认自动搜索
        /// </summary>        
        Auto = 0,
        /// <summary>
        /// 高级搜索
        /// </summary>
        Advance = 1,
        /// <summary>
        /// 关键字搜索
        /// </summary>
        KeyWord = 2,
        /// <summary>
        /// 关联信息搜索
        /// </summary>
        MessageRelation = 3,
        /// <summary>
        /// 主题单号搜索
        /// </summary>
        SubjectInNO = 4,
        /// <summary>
        /// 联系人搜索
        /// </summary>
        Contact = 5,

    }
    /// <summary>
    /// 
    /// </summary>
    public enum SubGridType
    {
        /// <summary>
        /// 
        /// </summary>
        MailLink4CarrierAP,
        /// <summary>
        /// 
        /// </summary>
        MailLink4CarrierMBL,
        /// <summary>
        /// 
        /// </summary>
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
        Label = 5,
        /// <summary>
        /// 勾选框
        /// </summary>
        CheckBox = 6,
        /// <summary>
        /// 下拉选框
        /// </summary>
        ComboBox = 7,
    }
    /// <summary>
    /// 列编辑控件类型
    /// </summary>
    public enum ColumnEditType
    {
        /// <summary>
        /// 普通文本
        /// </summary>
        Text = 1,

        /// <summary>
        /// 勾选框
        /// </summary>
        Checkbox = 2,
        /// <summary>
        /// 图片下拉选框
        /// </summary>
        ImageComboBox = 3,
        /// <summary>
        /// 多行文本
        /// </summary>
        Memo = 4,
        /// <summary>
        /// 日期选择框
        /// </summary>
        DateTime,
        /// <summary>
        /// 状态多选择控件
        /// </summary>
        ItemCheckEdit = 6,
        /// <summary>
        /// 特殊字段的特殊处理
        /// </summary>
        ItemCheckEdits = 7
    }
    /// <summary>
    /// 上下文菜单项类型
    /// </summary>
    public enum ContextMenuItemType
    {
        /// <summary>
        /// 菜单项
        /// </summary>
        MenuItem = 1,
        /// <summary>
        /// 带子菜单的菜单项
        /// </summary>
        SubMenuItem = 2,
    }
    /// <summary>
    /// 列类型
    /// </summary>
    public enum ColumnType
    {
        /// <summary>
        /// 
        /// </summary>
        System,
        /// <summary>
        /// 
        /// </summary>
        Custom
    }

    /// <summary>
    /// 范围
    /// </summary>
    public enum ScopeItem
    {
        /// <summary>
        /// 
        /// </summary>
        [MemberDescription("All")]
        All,
        /// <summary>
        /// 
        /// </summary>
        [MemberDescription("No SO Copy")]
        NoSOCopy,
        /// <summary>
        /// 
        /// </summary>
        [MemberDescription("No MBL Copy")]
        NoMBLCopy,
        /// <summary>
        /// 
        /// </summary>
        [MemberDescription("No A/P Copy")]
        NoAPCopy,
        /// <summary>
        /// 
        /// </summary>
        [MemberDescription("No A/N Copy")]
        NoANCopy
    }

    /// <summary>
    /// 邮件中心工具栏命令
    /// </summary>
    public enum ToolBarCommand
    {
        /// <summary>
        /// 新增业务
        /// </summary>
        NewShipment,
        /// <summary>
        /// 关联
        /// </summary>
        Relation,
        /// <summary>
        /// 选择范围
        /// </summary>
        SelectedScope,
        /// <summary>
        /// 关键字查找
        /// </summary>
        KeyWordSearch,
        /// <summary>
        /// 高级查找
        /// </summary>
        AdvanceSearch,
    }

    /// <summary>
    /// 订舱失败枚举值
    /// </summary>
    public enum FailureBooking
    {

        /// <summary>
        /// 未知
        /// </summary>
        Unknown,
        /// <summary>
        /// 爆仓
        /// </summary>
        BlastingWarehouse,
        /// <summary>
        /// 柜型
        /// </summary>
        CubicleType,
        /// <summary>
        /// 船期
        /// </summary>
        ShippingDate,
        /// <summary>
        /// 订舱失败其他
        /// </summary>
        FailureOther,
       
    }

    /// <summary>
    /// 取消订舱枚举值
    /// </summary>
    public enum CancelBooking
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown,
        /// <summary>
        /// 货源
        /// </summary>
        Sourcing,
        /// <summary>
        /// 价格
        /// </summary>
        Price,
        /// <summary>
        /// 贸易
        /// </summary>
        Trade,
        /// <summary>
        /// 取消其他
        /// </summary>
        CancelOther
    }

}
