#region Comment

/*
 * 
 * FileName:    InquireTruckingRatesEmailPart.cs
 * CreatedOn:   2014/7/11 18:14:41
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using ICP.Business.Common.UI.Communication;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FRM.UI.InquireRates
{
    public partial class InquireTruckingRatesEmailPart : BaseEditPart, IInquierRateDataBind
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// Root Work Item
        /// </summary>
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        #endregion

        #region 构造函数
		public InquireTruckingRatesEmailPart()
        {
            InitializeComponent();
            EmailPart.Presenter = new CommunicationHistoryListPresenter();
            Disposed += delegate
            {
                EmailPart.Presenter = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        } 
	    #endregion

        #region 成员变量
        /// <summary>
        /// 当前询价单对象
        /// </summary>
        public ClientBaseInquireRate CurrentInquierRate { get; set; }
        #endregion

        #region 方法定义
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="data">绑定数据</param>
        public void DataSourceBind(object data)
        {
            BusinessOperationContext contex = new BusinessOperationContext();
            ClientBaseInquireRate obj = data as ClientBaseInquireRate;
            contex.OperationID = obj == null ? Guid.Empty : obj.ID;
            CurrentInquierRate = obj;

            EmailPart.DataBind(contex);

            Enabled = (data != null);
        }
        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <param name="data"></param>
        public void SetDataSource(object data)
        {

        }
        #endregion
    }
}
