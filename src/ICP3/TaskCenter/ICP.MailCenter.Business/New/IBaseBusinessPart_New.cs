using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Common;
using System.Data;
using ICP.MailCenter.Business.ServiceInterface;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 基础业务面板接口
    /// 包含逻辑:
    /// 1.构建工具栏
    /// 2.查询及绑定数据
    /// </summary>
    public interface IBaseBusinessPart_New
    {
        /// <summary>
        /// 获取工具栏按钮数据
        /// </summary>
        /// <param name="templateCode">模板代码</param>
        List<OperationToolbarCommand> GetToolbarCommands(string templateCode);
        /// <summary>
        /// 初始化
        /// </summary>
        void Init(object parameter);
        /// <summary>
        /// 当前行业务号
        /// </summary>
        string OperationNo
        {
            get;
            set;
        }
        /// <summary>
        /// 业务ID
        /// </summary>
        Guid OperationID
        {
            get;
            set;
        }
        /// <summary>
        /// 业务类型
        /// </summary>
        OperationType OperationType
        {
            get;
            set;
        }
        /// <summary>
        /// 表单ID
        /// </summary>
        Guid FormID
        {
            get;
            set;
        }

        /// <summary>
        /// 模板代码
        /// </summary>
        string TemplateCode
        {
            get;
            set;
        }
        /// <summary>
        /// 用户选择的数据所属分公司
        /// </summary>
        string SelectedCompanyIds
        {
            get;
            set;
        }
        /// <summary>
        ///  高级查询面板传递过来的高级查询条件字符串
        /// </summary>
        string AdvanceQueryString
        {
            get;
            set;
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        DateTime? Updatetime
        {
            get;
            set;
        }
        List<string> RegisterSiteNames
        {
            get;
            set;
        }

        /// <summary>
        /// 抽取面板构造信息
        /// </summary>
        /// <param name="parameter">驱动面板构造的参数，如任务中心的视图节点数据，邮件中心当前选择邮件转换后的消息实体</param>
        /// <returns></returns>
        void GetInfo(object parameter);
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="mergeAdvanceQueryString"></param>
        /// <returns></returns>
        BusinessQueryCriteria GetQueryCriteria(bool mergeAdvanceQueryString);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        void QueryData(bool mergeAdvanceQueryString);
        /// <summary>
        /// 绑定数据
        /// </summary>
        void BindData(object parameter);

        /// <summary>
        ///注册拓展点
        /// </summary>
        void RegisterExtensionSite();
        /// <summary>
        /// 解除拓展点注册
        /// </summary>
        void UnRegisterExtensionSite();

        /// <summary>
        /// 向数据库返回的表结构添加自定义列
        /// </summary>
        /// <param name="dt"></param>
        void AddCustomColumn(DataTable dt);
        /// <summary>
        /// 设置基类面板数据源
        /// </summary>
        /// <param name="data"></param>
        void SetBaseBindSource(object data);



    }
}
