using System;
using System.Collections.Generic;
using System.Text;

using Agilelabs.Framework;

namespace LongWin.ReportCenter.ServiceInterface
{
    /// <summary>
    /// ΪCRM�ṩҵ�����ݼ�¼
    /// </summary>
    [ServiceInfomation("IREPPUBDataService", Agilelabs.Framework.ServiceType.Business)]
    public interface IREPPUBDataService
    {
        ///// <summary>
        ///// һ���ض��Ŀͻ��Ϳͻ���һ���ض��ĸ�������һ��ʱ���ڵ�ҵ������
        ///// </summary>
        ///// <param name="BeginTime">��ʼʱ��</param>
        ///// <param name="EndTime">����ʱ��</param>
        ///// <param name="CustomerId">ָ���Ŀͻ�</param>
        ///// <param name="SaleId">ָ����ҵ��Ա</param>
        ///// <returns></returns>
        //[Agilelabs.Framework.FunctionInfomation("һ���ض��Ŀͻ��Ϳͻ���һ���ض��ĸ�������һ��ʱ���ڵ�ҵ������")]
        //JobInfoSet.JobInfoDataTable GetJobInformation(DateTime BeginTime, DateTime EndTime, Guid CustomerId, Guid SaleId);
        ///// <summary>
        ///// һ���ض��ͻ���ͻ���һ��ʱ���ڵ�ҵ������
        ///// </summary>
        ///// <param name="BeginTime"></param>
        ///// <param name="EndTime"></param>
        ///// <param name="CustomerIds"></param>
        ///// <param name="SaleId"></param>
        ///// <returns></returns>
        //[Agilelabs.Framework.FunctionInfomation("һ���ض��ͻ���ͻ���һ��ʱ���ڵ�ҵ������")]
        //JobInfoSet.JobInfoDataTable GetJobInformation(DateTime BeginTime, DateTime EndTime, Guid[] CustomerIds, Guid SaleId);
        ///// <summary>
        ///// ��ȡһ��ί�е����з�����ϸ
        ///// </summary>
        ///// <param name="consignId">ί�е�Id</param>
        ///// <returns></returns>
        //[Agilelabs.Framework.FunctionInfomation("��ȡһ��ί�е����з�����ϸ")]
        //JobInfoSet.FeeOfJobDataTable GetFeesOfJob(Guid consignId);

        /// <summary>
        /// ��ȡһ���û��Ƿ���Բ鿴Ӧ�����õ�������λ
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Agilelabs.Framework.FunctionInfomation("��ȡһ���û��Ƿ���Բ鿴Ӧ�����õ�������λ")]
        bool GetIsViewShipper();

        /// <summary>
        /// ͨ�����Ż�ȡԱ��
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        [FunctionInfomation("GetStafferByCompanyID")]
        System.Data.DataSet GetStafferByCompanyID(Guid CompanyID);
    }
}
