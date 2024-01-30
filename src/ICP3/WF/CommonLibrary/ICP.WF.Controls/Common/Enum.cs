//-----------------------------------------------------------------------
// <copyright file="Enum.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    using System;
    using ICP.Framework.CommonLibrary.Attributes;


    /// <summary>
    /// 数据范围
    /// </summary>
    public enum DataScope
    {
        /// <summary>
        /// 整个鹏城海的所有部门节点
        /// </summary>
        All,

        /// <summary>
        /// 当前用户所在整个公司下的部门
        /// </summary>
        Company,

        /// <summary>
        /// 当前用户所在部门
        /// </summary>
        Department,

        /// <summary>
        /// 所有公司
        /// </summary>
        AllCompany,
        /// <summary>
        /// 公司列表
        /// </summary>
        CompanyList
    }

    /// <summary>
    /// 事件类型
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// 不处理
        /// </summary>
        [MemberDescription("不处理", "None")]
        None,

        /// <summary>
        /// 选择改变后
        /// </summary>
        [MemberDescription("选择改变后", "SelectedValueChanged")]
        SelectedValueChanged,
        
        /// <summary>
        /// 失去焦点
        /// </summary>
        [MemberDescription("失去焦点", "LostFocus")]
        LostFocus
    }


    /// <summary>
    /// FindTextBox类型
    /// </summary>
    public enum FindTextBoxType
    {
        /// <summary>
        /// 客户搜索
        /// </summary>
        Customer,

        /// <summary>
        /// 用户搜索
        /// </summary>
        User,

        /// <summary>
        /// 会计科目
        /// </summary>
        GLCode
    }


    /// <summary>
    /// 搜索字段枚举
    /// </summary>
    public enum SearchMember
    {
        /// <summary>
        /// 按代码搜索
        /// </summary>
        Code,

        /// <summary>
        /// 按名称搜索
        /// </summary>
        Name
    }

    /// <summary>
    /// 显示成员
    /// </summary>
    [Serializable]
    public enum DipalyMember
    {
        /// <summary>
        /// 按代码搜索
        /// </summary>
        Code,

        /// <summary>
        /// 按名称搜索
        /// </summary>
        Name
    }

    /// <summary>
    /// 值成员
    /// </summary>
    [Serializable]
    public enum ValueMember
    {
        /// <summary>
        /// Id
        /// </summary>
        ID,

        /// <summary>
        /// 按代码搜索
        /// </summary>
        Code,

        /// <summary>
        /// 按名称搜索
        /// </summary>
        Name
    }

    /// <summary>
    /// 字段类型
    /// </summary>
    [Serializable]
    public enum FieldType
    {
       
        /// <summary>
        /// 其它
        /// </summary>
        Other,

        /// <summary>
        /// 部门字段
        /// </summary>
        Department,

        /// <summary>
        /// 用户
        /// </summary>
        User,

        /// <summary>
        /// 职位
        /// </summary>
        Job,

        /// <summary>
        /// 客户
        /// </summary>
        Customer,

        /// <summary>
        /// 会计科目
        /// </summary>
        GLCode,

        /// <summary>
        /// 不生成表格列
        /// </summary>
        None,

        /// <summary>
        /// 用户ID
        /// </summary>
        UserID,

        /// <summary>
        /// 客户ID
        /// </summary>
        CustomerID,

        /// <summary>
        /// 会计科目ID
        /// </summary>
        GLCodeID
        
    }
}
