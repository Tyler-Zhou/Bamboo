using System;
using System.Collections.Generic;
using System.Text;
using LongWin.BusinessInfo.ServiceInterface.DataObject;
using Agilelabs.Framework;
namespace LongWin.BusinessInfo.ServiceInterface
{
    [ServiceInfomation("IOTEBusinessInfoService", Agilelabs.Framework.ServiceType.Business)]
    public interface IOTEBusinessInfoService
    {
        /// <summary>
        /// ��ȡһ���û��Ƿ���Բ鿴Ӧ�����õ�������λ
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [FunctionInfomation("��ȡһ���û��Ƿ���Բ鿴Ӧ�����õ�������λ")]
        bool GetIsViewShipper(Guid userID);


        /// <summary>
        /// һ���ض��Ŀͻ��Ϳͻ���һ���ض��ĸ�������һ��ʱ���ڵ�ҵ������
        /// </summary>
        /// <param name="BeginTime">��ʼʱ��</param>
        /// <param name="EndTime">����ʱ��</param>
        /// <param name="CustomerId">ָ���Ŀͻ�</param>
        /// <param name="SaleId">ָ����ҵ��Ա</param>
        /// <returns></returns>
        [Agilelabs.Framework.FunctionInfomation("һ���ض��Ŀͻ��Ϳͻ���һ���ض��ĸ�������һ��ʱ���ڵ�ҵ������")]
        List<JobInfoData> GetJobInformation(DateTime BeginTime, DateTime EndTime, Guid CustomerId, Guid SaleId);
        
        /// <summary>
        /// ��ȡһ��ί�е����з�����ϸ
        /// </summary>
        /// <param name="consignId">ί�е�Id</param>
        /// <returns></returns>
        [Agilelabs.Framework.FunctionInfomation("��ȡһ��ί�е����з�����ϸ")]
        List<JobFeeData> GetFeesOfJob(Guid consignId);

        


       
    }
}
