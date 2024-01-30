//-----------------------------------------------------------------------
// <copyright file="ENum.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Sys.ServiceInterface.DataObjects
{
    using ICP.Framework.CommonLibrary.Attributes;

    /// <summary>
    /// 性别
    /// </summary>
    public enum GenderType
    {
        /// <summary>
        /// 女
        /// </summary>
        [MemberDescription("女")]
        Female = 0,

        /// <summary>
        /// 男
        /// </summary>
        [MemberDescription("男")]
        Male = 1
    }

    /// <summary>
    /// 组织结构类型
    /// </summary>
    public enum OrganizationType
    {
        /// <summary>
        /// 公司主节点
        /// </summary>
        [MemberDescription("总部")]
        Root = 1,

        /// <summary>
        /// 区域
        /// </summary>
        [MemberDescription("区域")]
        Section = 2,

        /// <summary>
        /// 公司
        /// </summary>
        [MemberDescription("公司")]
        Company = 3,

        /// <summary>
        /// 部门
        /// </summary>
        [MemberDescription("部门")]
        Department = 4,

        /// <summary>
        /// 组
        /// </summary>
        [MemberDescription("组")]
        Group = 5
    }

    /// <summary>
    /// OrganizationJobType
    /// </summary>
    public enum OrganizationJobType
    {
        /// <summary>
        /// 组织结构
        /// </summary>
        Organization,

        /// <summary>
        /// 职位
        /// </summary>
        Job
    }

    /// <summary>
    /// 菜单节点项类型
    /// </summary>
    public enum FunctionType
    {
        /// <summary>
        /// 菜单组
        /// </summary>
        Module=1,

        /// <summary>
        /// 菜单项
        /// </summary>
        Function=2,

        /// <summary>
        /// 动作
        /// </summary>
        Action=3,
    }

    /// <summary>
    /// 界面节点项类型
        /// </summary>
    public enum UIConfigItemType
    {
        /// <summary>
        /// 容器
        /// </summary>
        [MemberDescription("容器")]
        Container =0,

        /// <summary>
        /// 菜单组
        /// </summary>
        [MemberDescription("菜单组")]
        MenuGroup =1,

        /// <summary>
        /// 菜单项
        /// </summary>
        [MemberDescription("菜单项")]
        MenuItem =2,

        /// <summary>
        /// 动作
        /// </summary>
        [MemberDescription("动作")]
        Action=3,
    }

    /// <summary>
    /// 网卡认证申请状态
    /// </summary>
    public enum AuthcodeState
    {
        /// <summary>
        /// 申请中..
        /// </summary>
        [MemberDescription("申请中")]
        Processing,

        /// <summary>
        /// 同意
        /// </summary>
        [MemberDescription("同意")]
        Agree,

        /// <summary>
        /// 不同意
        /// </summary>
        [MemberDescription("不同意")]
        Disagree
    }

    /// <summary>
    /// 获取权限可操作的数据范围
    /// </summary>
    public enum PermissionRangeType
    {
        /// <summary>
        /// 包括范围内的所有子节点
        /// </summary>
        All,

        /// <summary>
        /// 包括公司列表
        /// </summary>
        Company
    }

    /// <summary>
    /// 组所呈现的位置
    /// </summary>
    public enum SiteType
    {
        /// <summary>
        /// 呈现在菜单栏中 
        /// </summary>
        Menu=1,

        /// <summary>
        /// 呈现在工具栏中
        /// </summary>
        Toolbar=2,

        /// <summary>
        /// 呈现在状态栏中
        /// </summary>
        Statusbar=3
    }

    /// <summary>
    /// 邮件协议
    /// </summary>
    public enum MailProtocol
    {
        /// <summary>
        /// POP
        /// </summary>
        POP=0,
        /// <summary>
        /// SMTP
        /// </summary>
        SMTP=1
    }
    public enum UntieLockType
    { 
        /// <summary>
        /// 销账单
        /// </summary>
        Check=1,
        /// <summary>
        /// 凭证
        /// </summary>
        Ledger = 2,
        /// <summary>
        /// 日记账
        /// </summary>
        Journal = 3,
        /// <summary>
        /// 揽货人
        /// </summary>
        Sales = 4
    }

}
