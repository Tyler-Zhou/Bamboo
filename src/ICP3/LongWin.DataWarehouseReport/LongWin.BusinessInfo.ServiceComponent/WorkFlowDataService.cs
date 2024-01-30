using System;
using System.Collections.Generic;
using System.Data;
using LongWin.BusinessInfo.ServiceInterface;
using LongWin.OrganizationStructure.ServiceInterface;
using LongWin.ReportCenter.Model;
using LongWin.CommonData.ServiceInterface;
using LongWin.CommonData.ServiceInterface.DataObjects;
using LongWin.Framework.Server;
using System.Data.Linq;
using System.Linq;
using LongWin.BusinessInfo.ServiceInterface.DataObject;
using System.Xml.Linq;

namespace LongWin.BusinessInfo.ServiceComponent
{
    public class WorkFlowDataService:IWorkFlowDataService   
    {
        IOrganizationService _organizationService;
        IDataContextService _dcService;
        ITransportFoundationService _tfService;
      
     

        public WorkFlowDataService( IOrganizationService organizationService
            ,IDataContextService dcService
            ,ITransportFoundationService tfService)
        {
            this._dcService = dcService;
            this._organizationService = organizationService;
            this._tfService = tfService;
        }



        public Guid SaveCostFee(Guid CostFeeId, DateTime happenDate, Guid deptID, Guid userID, decimal amount, short feeProperty, string no, Guid[] costItemIDs, decimal[] amounts, string[] remarks)
        {
            using (DataContext dc = _dcService.GetDataContext(false))
            {
                var fee = dc.GetTable<CostFee>().SingleOrDefault(f => f.ID == CostFeeId);
                if (fee == null)
                {
                    fee = new CostFee();
                    fee.ID = CostFeeId;
                    fee.HappenDate = happenDate;
                    dc.GetTable<CostFee>().InsertOnSubmit(fee);
                }
                fee.HappenPeriod = System.Convert.ToInt32(happenDate.Year.ToString().Trim() + happenDate.Month.ToString().Trim().PadLeft(2, '0'));
                fee.DeptID = deptID;
                fee.UserID = userID;
               // fee.CompanyID=d

                LongWin.OrganizationStructure.ServiceInterface.DataObjects.StructureNodeData comp=  this._organizationService.GetOfficeInfo(deptID).FirstOrDefault();
                if(comp!=null)
                {
                    fee.CompanyID=comp.Id;
                }
                else 
                { 
                    fee.CompanyID = deptID; 
                }
                fee.Amount = amount;
                fee.no = no;
                fee.FeeProperty = feeProperty;

                Guid[] firstCostItemIDs = new Guid[costItemIDs.Length];
                for (int i = 0; i < costItemIDs.Length; i++)
                {
                    firstCostItemIDs[i] = this.GetTopCostFeitemForID(costItemIDs[i]);
                }

                  //加入
                for (int i = 0; i < costItemIDs.Length; i++)
                {
                    CostFeeDetail detail = new CostFeeDetail();
                    detail.ID=Guid.NewGuid();
                    detail.CostFeeID = fee.ID;
                    detail.CostItemID = costItemIDs[i];
                    detail.FeeProperty = fee.FeeProperty;
                    detail.FirstCostItemID = costItemIDs[i];
                    detail.Amount = amounts[i];
                    detail.Remark = remarks[i];
                    fee.CostFeeDetails.Add(detail);
                }
                dc.SubmitChanges();

                 return fee.ID;

            }

        }


        public Guid SaveFee(Guid CostFeeId, DateTime happenDate, Guid deptID, Guid userID, decimal amount, short feeProperty, string no, Guid[] costItemIDs, string[] currencys,decimal[] amounts, string[] remarks)
        {
            using (DataContext dc = _dcService.GetDataContext(false))
            {
                var fee = dc.GetTable<CostFee>().SingleOrDefault(f => f.ID == CostFeeId);
                if (fee == null)
                {
                    fee = new CostFee();
                    fee.ID = CostFeeId;
                    fee.HappenDate = happenDate;
                    dc.GetTable<CostFee>().InsertOnSubmit(fee);
                }
                fee.HappenPeriod = System.Convert.ToInt32(happenDate.Year.ToString().Trim() + happenDate.Month.ToString().Trim().PadLeft(2, '0'));
                fee.DeptID = deptID;
                fee.UserID = userID;
                // fee.CompanyID=d

                LongWin.OrganizationStructure.ServiceInterface.DataObjects.StructureNodeData comp = this._organizationService.GetOfficeInfo(deptID).FirstOrDefault();
                if (comp != null)
                {
                    fee.CompanyID = comp.Id;
                }
                else
                {
                    fee.CompanyID = deptID;
                }
                fee.Amount = amount;
                fee.no = no;
                fee.FeeProperty = feeProperty;

                Guid[] firstCostItemIDs = new Guid[costItemIDs.Length];
                for (int i = 0; i < costItemIDs.Length; i++)
                {
                    firstCostItemIDs[i] = this.GetTopCostFeitemForID(costItemIDs[i]);
                }

                //加入
                for (int i = 0; i < costItemIDs.Length; i++)
                {
                    CostFeeDetail detail = new CostFeeDetail();
                    detail.ID = Guid.NewGuid();
                    detail.CostFeeID = fee.ID;
                    detail.CostItemID = costItemIDs[i];
                    detail.FeeProperty = fee.FeeProperty;
                    detail.FirstCostItemID = costItemIDs[i];
                    detail.Currency = currencys[i];
                    detail.Amount = amounts[i];
                    detail.Remark = remarks[i];
                    fee.CostFeeDetails.Add(detail);
                }
                dc.SubmitChanges();

                return fee.ID;

            }

           
        }


        List<CostItemData> _costList;
        /// <summary>
        /// 获取一个费用项目的一级费用项目
        /// </summary>
        /// <param name="childID"></param>
        /// <returns></returns>
        Guid GetTopCostFeitemForID(Guid childID)
        {
            if (_costList == null || _costList.Count == 0)
            {
                _costList = _tfService.GetAllCostItems();
            }
            CostItemData findData = _costList.Find(delegate(CostItemData sourceitem) { return sourceitem.Id == childID; });
            if (findData == null) return Guid.Empty;
            string nodeCode = findData.NodeCode.Trim().Substring(0, 2);
            CostItemData topData = _costList.Find(delegate(CostItemData sourceitem) { return sourceitem.NodeCode == nodeCode; });
            if (topData == null) return Guid.Empty;
            return topData.Id;
        }


        /// <summary>
        /// 删除取消的工作申请费用记录
        /// </summary>
        /// <param name="CostFeeId">主键</param>
        /// <returns></returns>
        public void DeleteCostFee(Guid CostFeeId)
        {
            using (DataContext dc = _dcService.GetDataContext(false))
            {
                var fee = dc.GetTable<CostFee>().SingleOrDefault(f => f.ID == CostFeeId);
                if (fee != null)
                {
                    dc.GetTable<CostFee>().DeleteOnSubmit(fee);
                    dc.SubmitChanges();
                }
            }
           
        }



        public void SaveDeficitOperationWorkFlowLog(Guid operationId, string operationType, Guid workflowId,Guid createBy)
        {
            using (DataContext dc = _dcService.GetDataContext(false))
            {
                try
                {
                    DeficitOperationWorkFlowLog log = dc.GetTable<DeficitOperationWorkFlowLog>().FirstOrDefault(l => l.OperationId == operationId);
                    if (log == null)
                    {
                        log = new DeficitOperationWorkFlowLog();
                        log.Id = Guid.NewGuid();
                        dc.GetTable<DeficitOperationWorkFlowLog>().InsertOnSubmit(log);
                    }

                    log.CreateBy = createBy;
                    log.CreateDate = DateTime.Now;
                    log.OperationId = operationId;
                    log.OperationType = operationType;
                    log.WorkFlowId = workflowId;

                    dc.SubmitChanges();
                }
                catch(Exception ex)
                {
                    _dcService.Rollback();
                    throw ex;
                }
            }
        }

        public void RemoveDeficitOperationWorkFlowLog(Guid operationId)
        {
            using (DataContext dc = _dcService.GetDataContext(false))
            {
                try
                {
                    DeficitOperationWorkFlowLog log = dc.GetTable<DeficitOperationWorkFlowLog>().FirstOrDefault(l => l.OperationId == operationId);
                    if (log != null)
                    {
                        dc.GetTable<DeficitOperationWorkFlowLog>().DeleteOnSubmit(log);
                        dc.SubmitChanges();
                    }
                }
                catch (Exception ex)
                {
                    _dcService.Rollback();
                    throw ex;
                }
            }
        }

        public Guid? GetWorkFlowIdByOperationId(Guid operationId)
        {
            using (DataContext dc = _dcService.GetDataContext(false))
            {
                try
                {
                    Guid? flowId = (from l in dc.GetTable<DeficitOperationWorkFlowLog>()
                                    where l.OperationId == operationId
                                    select l.WorkFlowId).FirstOrDefault();

                    return flowId;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<OperationSearchResult> GetOperationIdByOperationNo(string[] oprationNos, Guid[] companyIds)
        {
            using (DataContext dc = _dcService.GetDataContext(false))
            {
                System.Xml.Linq.XDocument doc = CreateXMLDocument(oprationNos, companyIds);
                LongWin.ReportCenter.Model.ReportCenterDataClassDataContext tc = new LongWin.ReportCenter.Model.ReportCenterDataClassDataContext(dc.Connection);


                var results = tc.SPR_FIN_GetOperationIdForCommission(doc);
                List<OperationSearchResult> datas = (from r in results
                                                     select new OperationSearchResult
                                                       {
                                                           OperationId = r.Id,
                                                           OperationNo = r.ReferenceNO
                                                       }).ToList();


                return datas;

            }
        }


        private XDocument CreateXMLDocument(string[] oprationNos, Guid[] companyIds)
        {
            XDocument d = new XDocument();
            XElement m = new XElement("Operations");
            for (int i = 0; i < oprationNos.Length; i++)
            {
                XElement cm = new XElement("Operation");


                XAttribute operationNode = new XAttribute("OperationNo", oprationNos[i]);
                cm.Add(operationNode);

                XAttribute companyNode = new XAttribute("CompanyId", companyIds[i].ToString());
                cm.Add(companyNode);

                m.Add(cm);
            }

            d.Add(m);
            
            return d;
        }

    }
}
