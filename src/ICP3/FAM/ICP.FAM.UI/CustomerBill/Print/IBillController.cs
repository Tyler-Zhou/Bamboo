using System;
using System.Collections.Generic;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.UI.CustomerBill.Print
{
    public interface IBillController
    {
        /// <summary>
        /// 打印费用清单
        /// </summary>
        /// <param name="operationID">operationID</param>
        void PrintFeeList(Guid operationID);


        /// <summary>
        /// 打印合并帐单
        /// </summary>
        /// <param name="billIDs"></param>
        /// <param name="operationCommonInfo"></param>
        /// <param name="configureInfo"></param>
        /// <param name="rates"></param>
        void PrintCombinBill(List<BillList> billLists, OperationCommonInfo operationCommonInfo, ConfigureInfo configureInfo,
            List<SolutionExchangeRateList> rates);

        
        /// <summary>
        /// 打印帐单
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="operationCommonInfo"></param>
        /// <param name="configureInfo"></param>
        /// <param name="rates"></param>
        void PrintBill(BillList billList, BillType billType, OperationCommonInfo operationCommonInfo, ConfigureInfo configureInfo,
            List<SolutionExchangeRateList> rates);

        /// <summary>
        /// 在控制器中更新了帐单
        /// </summary>
        event BillUpdatedHandler UpdatedBill;
        
    }

    public delegate void BillUpdatedHandler(object o, ManyResult result);

    public class BillControllerFactory
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        Dictionary<OperationType, Type> controllers = new Dictionary<OperationType, Type>();

        public void Register<T>(OperationType operationType) where T : IBillController
        {
            controllers.Add(operationType, typeof(T));
        }

        public IBillController GetBillController(OperationType operationType)
        {
            if (controllers.ContainsKey(operationType) == false) return null;

            IBillController provider = Workitem.Services.Get(controllers[operationType]) as IBillController;
            if (provider == null)
            {
                provider = Workitem.Services.AddNew(controllers[operationType]) as IBillController;
            }

            return provider;
        }
    }
}
