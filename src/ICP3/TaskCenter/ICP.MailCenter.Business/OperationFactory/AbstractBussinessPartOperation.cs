using System.Threading;
using System.Data;
using ICP.MailCenter.Business.ServiceInterface;

namespace ICP.Common.Business.ServiceInterface
{
    /// <summary>
    /// 列表数据加载的抽象类
    /// </summary>
    public abstract class AbstractBussinessPartOperation : IBussinessPartOperation
    {
        #region IBussinessPartOperation 成员

        ///// <summary>
        /////  构造函数
        ///// </summary>
        ///// <param name="baseBusinessPart">当前业务面板</param>
        ///// <param name="operationSourceType">当前业务面板来源</param>
        //public AbstractBussinessPartOperation(BaseBusinessPart baseBusinessPart, OperationSourceType operationSourceType)
        //{
        //    this.BaseBusinessPart = baseBusinessPart;
        //    this.BaseBusinessPart.OperationSourceType = operationSourceType;
        //}

        public BaseBusinessPart BaseBusinessPart
        {
            get;
            set;
        }

        /// <summary>
        /// 业务面板初始化入口
        /// 动作:1.解析邮件信息
        ///      2.注册工具栏，右键菜单栏拓展点
        ///      3.获取工具栏命令实体，构造工具栏
        ///      4.获取列实体列表，构造列表列
        ///      5.挂接命令处理
        /// </summary>
        /// <param name="initObj"></param>
        public void init(string templateCode)
        {
            init();
            BaseBusinessPart.TemplateCode = templateCode;
            BaseBusinessPart.Init();
        }
        /// <summary>
        /// 初始化不同类型
        /// </summary>
        protected abstract void init();
        /// <summary>
        /// 列表数据绑定
        /// </summary>
        public void BindData()
        {
            PreBindData();
            QueryAndBindData(false);
        }
        /// <summary>
        /// 绑定数据前
        /// </summary>
        public abstract void PreBindData();

        /// <summary>
        /// 绑定数据
        /// </summary>
        // public abstract void QueryAndBindData(bool mergeAdvanceQueryString);

        public void QueryAndBindData(bool mergeAdvanceQueryString)
        {
            BusinessQueryCriteria criteria = null;
            if (BaseBusinessPart.NeedBindData)
            {
                criteria = GetQueryCriteria(mergeAdvanceQueryString);
            }
            WaitCallback callback = (data) =>
            {
                //显示加载中
                ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");
                var temp = data as BindParameter;
                //查询数据
                if (temp == null) return;
                var result = GetData(temp.QueryCriteria);
                BaseBusinessPart.AddCustomColumn(result.Dt);
                temp.Dt = result.Dt;
                InnerBindData(result, temp);
                //关闭加载中
                ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();
            };
            var parameter = new BindParameter { NeedBindData = BaseBusinessPart.NeedBindData, Nos = BaseBusinessPart.Nos, QueryCriteria = criteria };
            ThreadPool.QueueUserWorkItem(callback, parameter);
        }

        protected abstract BusinessQueryCriteria GetQueryCriteria(bool mergeAdvanceQueryString);

        protected abstract BusinessQueryResult GetData(BusinessQueryCriteria criteria);

        protected abstract void InnerBindData(BusinessQueryResult result, BindParameter parameter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        protected delegate void DataBindDelegate(BusinessQueryResult dt);

        /// <summary>
        /// 设置数据绑定到列表
        /// </summary>
        /// <param name="result"></param>
        protected void SetBindingSource(BusinessQueryResult result)
        {

            BaseBusinessPart.bindingSource.DataSource = result.Dt;
            (BaseBusinessPart.bindingSource.DataSource as DataTable).AcceptChanges();
            BaseBusinessPart.bindingSource.EndEdit();
            AfterDataBind();
        }

        /// <summary>
        /// 数据绑定后期处理动作
        /// </summary>
        protected virtual void AfterDataBind()
        {

        }

        #endregion

    }
}
