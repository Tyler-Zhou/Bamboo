#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/6/14 14:49:12
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace Cityocean.Crawl.ServiceInterface
{
    #region 网站返回内容类型
    /// <summary>
    /// 网站返回内容类型
    /// </summary>
    [Serializable]
    public enum PageReturnType
    {
        /// <summary>
        /// HTML内容 0
        /// </summary>
        HtmlText = 0,
        /// <summary>
        /// JSON内容 1
        /// </summary>
        JSONText = 1,
        /// <summary>
        /// 文档 2
        /// </summary>
        Document = 2,
        /// <summary>
        /// (特殊)云当网 3
        /// </summary>
        YunDang = 3,
        /// <summary>
        /// (特殊)通知 4
        /// </summary>
        Notice = 4,
    }
    #endregion

    #region 抓取(数据)类型
    /// <summary>
    /// 抓取(数据)类型
    /// </summary>
    [Serializable]
    public enum CrawlType
    {
        /// <summary>
        /// 船期
        /// </summary>
        SailingSchedule = 0,
        /// <summary>
        /// 货物跟踪
        /// </summary>
        CargoTracking = 1,
        /// <summary>
        /// 码头
        /// </summary>
        Terminal = 2,
    }
    #endregion

    #region 网站参数键类型
    /// <summary>
    /// 网站参数键类型
    /// </summary>
    [Serializable]
    public enum Website_KeyType
    {
        /// <summary>
        /// 常量
        /// </summary>
        HTTPParam = 0,
        /// <summary>
        /// 凭证(验证)元素
        /// </summary>
        CredentialElement = 1,
        /// <summary>
        /// 表单元素
        /// </summary>
        FormElement = 2,
        /// <summary>
        /// 表达式
        /// </summary>
        Expression = 3,
    }
    #endregion

    #region 网站参数键值类型
    /// <summary>
    /// 网站参数键值类型
    /// </summary>
    [Serializable]
    public enum Website_KeyValueType
    {
        /// <summary>
        /// 常量 0
        /// </summary>
        Param = 0,
        /// <summary>
        /// 等待 1
        /// </summary>
        Wait = 1,
        /// <summary>
        /// 文本框 2
        /// </summary>
        TextBox = 2,
        /// <summary>
        /// 选择框 3
        /// </summary>
        SelectBox = 3,
        /// <summary>
        /// 点击(可忽略) 4
        /// </summary>
        ClickIgnorable = 4,
        /// <summary>
        /// 点击 5
        /// </summary>
        Click = 5,
        /// <summary>
        /// 内嵌框架 6
        /// </summary>
        Frame = 6,
        /// <summary>
        /// 窗口 7
        /// </summary>
        Window = 7,
        /// <summary>
        /// 执行脚本 8
        /// </summary>
        JavaScript = 8,
        /// <summary>
        /// 导航 9
        /// </summary>
        Navigate = 9,
        /// <summary>
        /// 登录表单 10
        /// </summary>
        LoginForm = 10,
        /// <summary>
        /// 异常 11
        /// </summary>
        Exception = 11,
    }
    #endregion

    #region 网站参数类型
    /// <summary>
    /// 网站参数类型
    /// </summary>
    [Serializable]
    public enum Website_ParamType
    {
        /// <summary>
        /// 常量
        /// </summary>
        Constants = 0,
        /// <summary>
        /// 属性
        /// </summary>
        Property = 1,
    }
    #endregion

    #region 动态排序类型
    /// <summary>
    /// 动态排序类型
    /// </summary>
    [Serializable]
    public enum DynamicSortType : byte
    {
        /// <summary>
        /// 降序
        /// </summary>
        DESC = 0,
        /// <summary>
        /// 升序
        /// </summary>
        ASC = 1,
    }
    #endregion

    #region 处理状态
    /// <summary>
    /// 处理状态
    /// </summary>
    [Serializable]
    public enum HandleStatus
    {
        /// <summary>
        /// 异常
        /// </summary>
        Exception = -1,
        /// <summary>
        /// 未处理
        /// </summary>
        Untreated = 0,
        /// <summary>
        /// 完成
        /// </summary>
        Complete = 1,
        /// <summary>
        /// 失败
        /// </summary>
        Failure = 2,
    }
    #endregion

    #region 操作业务类型
    /// <summary>
    /// 操作业务类型
    /// 1 出口业务；2 进口业务
    /// </summary>
    [Serializable]
    public enum OperationType
    {
        /// <summary>
        /// 出口业务
        /// </summary>
        OceanExport = 1,
        /// <summary>
        /// 进口业务
        /// </summary>
        OceanImport = 2,
    } 
    #endregion

    #region 船东
    /// <summary>
    /// 船东(承运人)所属(公司)
    /// </summary>
    [Serializable]
    public enum CarrierOwner
    {
        /// <summary>
        /// Inttra
        /// </summary>
        INTTRA = 1,
        /// <summary>
        /// 云当
        /// </summary>
        YunDang = 2
    } 
    #endregion

    #region 集装箱状态
    /// <summary>
    /// 集装箱状态
    /// </summary>
    [Serializable]
    public enum ContainerState
    {
        /// <summary>
        /// 未知
        /// </summary>
        UnKnown = 0,
        /// <summary>
        /// 提空柜
        /// </summary>
        EmptyPickUp = 1,
        /// <summary>
        /// 提重柜
        /// </summary>
        FullPickUp = 2,
        /// <summary>
        /// 装船
        /// </summary>
        LOBD = 3,
        /// <summary>
        /// 卸船
        /// </summary>
        UNLOBD = 4,
        /// <summary>
        /// 还空柜
        /// </summary>
        REC = 5,
    } 
    #endregion
}
