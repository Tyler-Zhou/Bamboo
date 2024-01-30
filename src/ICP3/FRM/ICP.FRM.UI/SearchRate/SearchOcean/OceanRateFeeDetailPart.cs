using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.FRM.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using System.Threading;

namespace ICP.FRM.UI.SearchRate
{
    /// <summary>
    /// 运的费用组成明细
    /// </summary>
    [ToolboxItem(false)]
    public partial class OceanRateFeeDetailPart : BaseListPart
    {
        /// <summary>
        /// 
        /// </summary>
        public OceanRateFeeDetailPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                gcAMain.DataSource = null;
                gcBMain.DataSource = null;
                gcCMain.DataSource = null;
                bsAList.DataSource = null;
                bsAList.Dispose();
                bsBList.DataSource = null;
                bsBList.Dispose();
                bsCList.DataSource = null;
                bsCList.Dispose();
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            
            };
        }

        #region 服务
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public IOceanPriceService OceanPriceService
        {
            get
            {
                return ServiceClient.GetService<IOceanPriceService>();
            }
        }
        #endregion

        #region MyRegion
        /// <summary>
        /// 佣金
        /// </summary>
        List<string> CommissionChargingCodes
        {
            get
            {
                return new List<string>()
                {
                    "CUF(L)", //CUF(L)	业务管理成本_(L)
                    "CUF(F)", //CUF(F)	业务管理成本_(F)
                    "CUF(C)", //CUF(C)	业务管理成本_(C)
                    "PRO", //PRO	利润
                };
            }
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 
        /// </summary>
        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                BindDataList(value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="feeDetail"></param>
        private delegate void DataBindDelegate(OceanRateFeeDetail feeDetail);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="feeDetail"></param>
        private void InnerBindData(OceanRateFeeDetail feeDetail)
        {
            BulidGridViewColumnsByOceanUnits(feeDetail.UnitList);

            #region 包含佣金费用重新计算
            if (!LocalCommonServices.PermissionService.HaveActionPermission(PermissionCommandConstants.SEARCHOCEAN_VIEWCONTRACTNO))
            {
                int addFeeCount=feeDetail.AdditionalFees.Count;
                for (int index = 0; index < addFeeCount; index++)
			    {
			        ClientAdditionalFeeList item = feeDetail.AdditionalFees[index];
                    if (CommissionChargingCodes.Contains(item.ChargingCodeDescription))
                    {
                        feeDetail.BasePortInfo.Rate_20FR += item.Rate_20FR;
                        feeDetail.BasePortInfo.Rate_20GP += item.Rate_20GP;
                        feeDetail.BasePortInfo.Rate_20HQ += item.Rate_20HQ;
                        feeDetail.BasePortInfo.Rate_20HT += item.Rate_20HT;
                        feeDetail.BasePortInfo.Rate_20NOR += item.Rate_20NOR;
                        feeDetail.BasePortInfo.Rate_20OT += item.Rate_20OT;
                        feeDetail.BasePortInfo.Rate_20RF += item.Rate_20RF;
                        feeDetail.BasePortInfo.Rate_20RH += item.Rate_20RH;
                        feeDetail.BasePortInfo.Rate_20TK += item.Rate_20TK;
                        feeDetail.BasePortInfo.Rate_40FR += item.Rate_40FR;
                        feeDetail.BasePortInfo.Rate_40GP += item.Rate_40GP;
                        feeDetail.BasePortInfo.Rate_40HQ += item.Rate_40HQ;
                        feeDetail.BasePortInfo.Rate_40HT += item.Rate_40HT;
                        feeDetail.BasePortInfo.Rate_40NOR += item.Rate_40NOR;
                        feeDetail.BasePortInfo.Rate_40OT += item.Rate_40OT;
                        feeDetail.BasePortInfo.Rate_40RF += item.Rate_40RF;
                        feeDetail.BasePortInfo.Rate_40RH += item.Rate_40RH;
                        feeDetail.BasePortInfo.Rate_40TK += item.Rate_40TK;
                        feeDetail.BasePortInfo.Rate_45FR += item.Rate_45FR;
                        feeDetail.BasePortInfo.Rate_45GP += item.Rate_45GP;
                        feeDetail.BasePortInfo.Rate_45HQ += item.Rate_45HQ;
                        feeDetail.BasePortInfo.Rate_45HT += item.Rate_45HT;
                        feeDetail.BasePortInfo.Rate_45OT += item.Rate_45OT;
                        feeDetail.BasePortInfo.Rate_45RF += item.Rate_45RF;
                        feeDetail.BasePortInfo.Rate_45RH += item.Rate_45RH;
                        feeDetail.BasePortInfo.Rate_45TK += item.Rate_45TK;
                        feeDetail.BasePortInfo.Rate_53HQ += item.Rate_53HQ;

                        feeDetail.AdditionalFees.Remove(item);
                        index--;
                        addFeeCount--;
                    }
                }
            } 
            #endregion
            bsAList.DataSource = feeDetail.BasePortInfo;
            bsBList.DataSource = feeDetail.Arbitrarys;
            bsCList.DataSource = feeDetail.AdditionalFees;

            bsAList.ResetBindings(false);
            bsBList.ResetBindings(false);
            bsCList.ResetBindings(false);

            if (feeDetail.Arbitrarys == null || feeDetail.Arbitrarys.Count == 0)
            {
                bgArbitrary.Expanded = false;
            }

            if (feeDetail.AdditionalFees == null || feeDetail.AdditionalFees.Count == 0)
            {
                bgAdditional.Expanded = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void BindDataList(object data)
        {

            if (data == null)
            {
                return;
            }
            WaitCallback callback = (parameter) => {
                try
                {
                    ClientHelper.SetApplicationContext();
                    OceanRateFeeDetail feeDetail = OceanPriceService.GetOceanRateFeeDetail(new Guid(parameter.ToString()));
                    if (!IsDisposed)
                    {
                        DataBindDelegate bindDelegate = new DataBindDelegate(InnerBindData);
                        Invoke(bindDelegate, feeDetail);
                    }
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
                }
            };
            ThreadPool.QueueUserWorkItem(callback, data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitsNameList"></param>
        private void BulidGridViewColumnsByOceanUnits(List<string> unitsNameList)
        {
            #region Base Port
            colRemark.Visible = false;
            colCarrier.Visible = true;
            colPOL.Visible = true;
            colPOD.Visible = true;
            colPlaceOfDelivery.Visible = true;
            colCurrencyID.Visible = true;
            colItemCode.Visible = true;
            colTransportClauseID.Visible = true;

            colCarrier.VisibleIndex = 0;
            colPOL.VisibleIndex = 1;
            colPOD.VisibleIndex = 2;
            colPlaceOfDelivery.VisibleIndex = 3;
            colCurrencyID.VisibleIndex = 4;
            colItemCode.VisibleIndex = 5;
            colTransportClauseID.VisibleIndex = 6;


            #region  SetVisible= false;
            colRateA_45FR.Visible = false;
            colRateA_40RF.Visible = false;
            colRateA_45HT.Visible = false;
            colRateA_20RF.Visible = false;
            colRateA_20HQ.Visible = false;
            colRateA_20TK.Visible = false;
            colRateA_20GP.Visible = false;
            colRateA_40TK.Visible = false;
            colRateA_40OT.Visible = false;
            colRateA_20FR.Visible = false;
            colRateA_45GP.Visible = false;
            colRateA_40GP.Visible = false;
            colRateA_45RF.Visible = false;
            colRateA_20RH.Visible = false;
            colRateA_45OT.Visible = false;
            colRateA_40NOR.Visible = false;
            colRateA_40FR.Visible = false;
            colRateA_20OT.Visible = false;
            colRateA_45TK.Visible = false;
            colRateA_20NOR.Visible = false;
            colRateA_40HT.Visible = false;
            colRateA_40RH.Visible = false;
            colRateA_45RH.Visible = false;
            colRateA_45HQ.Visible = false;
            colRateA_20HT.Visible = false;
            colRateA_40HQ.Visible = false;
            colRateA_53HQ.Visible = false;
            #endregion

            int AIndex = 7;

            foreach (var item in unitsNameList)
            {
                #region  SetVisible= true;

                switch (item)
                {
                    case "20GP": colRateA_20GP.VisibleIndex = AIndex + 1; break;
                    case "40GP": colRateA_40GP.VisibleIndex = AIndex + 2; break;
                    case "40HQ": colRateA_40HQ.VisibleIndex = AIndex + 3; break;
                    case "45HQ": colRateA_45HQ.VisibleIndex = AIndex + 4; break;
                    case "20NOR": colRateA_20NOR.VisibleIndex = AIndex + 5; break;
                    case "40NOR": colRateA_40NOR.VisibleIndex = AIndex + 6; break;

                    case "20FR": colRateA_20FR.VisibleIndex = AIndex + 7; break;
                    case "20RH": colRateA_20RH.VisibleIndex = AIndex + 8; break;
                    case "20RF": colRateA_20RF.VisibleIndex = AIndex + 9; break;
                    case "20HQ": colRateA_20HQ.VisibleIndex = AIndex + 19; break;
                    case "20TK": colRateA_20TK.VisibleIndex = AIndex + 10; break;
                    case "20OT": colRateA_20OT.VisibleIndex = AIndex + 11; break;
                    case "20HT": colRateA_20HT.VisibleIndex = AIndex + 12; break;

                    case "40TK": colRateA_40TK.VisibleIndex = AIndex + 13; break;
                    case "40OT": colRateA_40OT.VisibleIndex = AIndex + 14; break;
                    case "40FR": colRateA_40FR.VisibleIndex = AIndex + 15; break;
                    case "40HT": colRateA_40HT.VisibleIndex = AIndex + 16; break;
                    case "40RH": colRateA_40RH.VisibleIndex = AIndex + 17; break;
                    case "40RF": colRateA_40RF.VisibleIndex = AIndex + 18; break;

                    case "45GP": colRateA_45GP.VisibleIndex = AIndex + 19; break;
                    case "45RF": colRateA_45RF.VisibleIndex = AIndex + 20; break;
                    case "45HT": colRateA_45HT.VisibleIndex = AIndex + 21; break;
                    case "45FR": colRateA_45FR.VisibleIndex = AIndex + 22; break;
                    case "45OT": colRateA_45OT.VisibleIndex = AIndex + 23; break;
                    case "45TK": colRateA_45TK.VisibleIndex = AIndex + 24; break;
                    case "45RH": colRateA_45RH.VisibleIndex = AIndex + 25; break;

                    case "53HQ": colRateA_53HQ.VisibleIndex = AIndex + 26; break;
                }

                #endregion
            }

            colSurCharge.VisibleIndex=AIndex+27;
            colFromDate.VisibleIndex=AIndex+28;
            colToDate.VisibleIndex = AIndex + 29;


            #endregion

            #region Arbitrary
            #region  SetVisible= false;
            colRateB_45FR.Visible = false;
            colRateB_40RF.Visible = false;
            colRateB_45HT.Visible = false;
            colRateB_20RF.Visible = false;
            colRateB_20HQ.Visible = false;
            colRateB_20TK.Visible = false;
            colRateB_20GP.Visible = false;
            colRateB_40TK.Visible = false;
            colRateB_40OT.Visible = false;
            colRateB_20FR.Visible = false;
            colRateB_45GP.Visible = false;
            colRateB_40GP.Visible = false;
            colRateB_45RF.Visible = false;
            colRateB_20RH.Visible = false;
            colRateB_45OT.Visible = false;
            colRateB_40NOR.Visible = false;
            colRateB_40FR.Visible = false;
            colRateB_20OT.Visible = false;
            colRateB_45TK.Visible = false;
            colRateB_20NOR.Visible = false;
            colRateB_40HT.Visible = false;
            colRateB_40RH.Visible = false;
            colRateB_45RH.Visible = false;
            colRateB_45HQ.Visible = false;
            colRateB_20HT.Visible = false;
            colRateB_40HQ.Visible = false;
            colRateB_53HQ.Visible = false;
            #endregion
            colForm.Visible = true;
            col_To.Visible = true;
            col_B_Term.Visible = true;
            colForm.VisibleIndex = 0;
            col_To.VisibleIndex = 1;
            col_B_Term.VisibleIndex = 2;
            int BIndex = 3;

            foreach (var item in unitsNameList)
            {
                #region  SetVisible= true;

                switch (item)
                {
                    case "20GP": colRateB_20GP.VisibleIndex = BIndex + 1; break;
                    case "40GP": colRateB_40GP.VisibleIndex = BIndex + 2; break;
                    case "40HQ": colRateB_40HQ.VisibleIndex = BIndex + 3; break;
                    case "45HQ": colRateB_45HQ.VisibleIndex = BIndex + 4; break;
                    case "20NOR": colRateB_20NOR.VisibleIndex = BIndex + 5; break;
                    case "40NOR": colRateB_40NOR.VisibleIndex = BIndex + 6; break;

                    case "20FR": colRateB_20FR.VisibleIndex = BIndex + 7; break;
                    case "20RH": colRateB_20RH.VisibleIndex = BIndex + 8; break;
                    case "20RF": colRateB_20RF.VisibleIndex = BIndex + 9; break;
                    case "20HQ": colRateB_20HQ.VisibleIndex = BIndex + 19; break;
                    case "20TK": colRateB_20TK.VisibleIndex = BIndex + 10; break;
                    case "20OT": colRateB_20OT.VisibleIndex = BIndex + 11; break;
                    case "20HT": colRateB_20HT.VisibleIndex = BIndex + 12; break;

                    case "40TK": colRateB_40TK.VisibleIndex = BIndex + 13; break;
                    case "40OT": colRateB_40OT.VisibleIndex = BIndex + 14; break;
                    case "40FR": colRateB_40FR.VisibleIndex = BIndex + 15; break;
                    case "40HT": colRateB_40HT.VisibleIndex = BIndex + 16; break;
                    case "40RH": colRateB_40RH.VisibleIndex = BIndex + 17; break;
                    case "40RF": colRateB_40RF.VisibleIndex = BIndex + 18; break;

                    case "45GP": colRateB_45GP.VisibleIndex = BIndex + 19; break;
                    case "45RF": colRateB_45RF.VisibleIndex = BIndex + 20; break;
                    case "45HT": colRateB_45HT.VisibleIndex = BIndex + 21; break;
                    case "45FR": colRateB_45FR.VisibleIndex = BIndex + 22; break;
                    case "45OT": colRateB_45OT.VisibleIndex = BIndex + 23; break;
                    case "45TK": colRateB_45TK.VisibleIndex = BIndex + 24; break;
                    case "45RH": colRateB_45RH.VisibleIndex = BIndex + 25; break;

                    case "53HQ": colRateB_53HQ.VisibleIndex = BIndex + 26; break;
                }

                #endregion
            }
            #endregion

            #region Additional
            #region  SetVisible= false;
            colRateC_45FR.Visible = false;
            colRateC_40RF.Visible = false;
            colRateC_45HT.Visible = false;
            colRateC_20RF.Visible = false;
            colRateC_20HQ.Visible = false;
            colRateC_20TK.Visible = false;
            colRateC_20GP.Visible = false;
            colRateC_40TK.Visible = false;
            colRateC_40OT.Visible = false;
            colRateC_20FR.Visible = false;
            colRateC_45GP.Visible = false;
            colRateC_40GP.Visible = false;
            colRateC_45RF.Visible = false;
            colRateC_20RH.Visible = false;
            colRateC_45OT.Visible = false;
            colRateC_40NOR.Visible = false;
            colRateC_40FR.Visible = false;
            colRateC_20OT.Visible = false;
            colRateC_45TK.Visible = false;
            colRateC_20NOR.Visible = false;
            colRateC_40HT.Visible = false;
            colRateC_40RH.Visible = false;
            colRateC_45RH.Visible = false;
            colRateC_45HQ.Visible = false;
            colRateC_20HT.Visible = false;
            colRateC_40HQ.Visible = false;
            colRateC_53HQ.Visible = false;
            #endregion
            colFeeName.Visible = true;
            colFeeName.VisibleIndex = 1;
            colCustomerID.Visible = true;
            colCustomerID.VisibleIndex = 2;
            colCurrencyID.Visible = true;
            colCurrencyID.VisibleIndex = 3;

            int CIndex = 3;

            foreach (var item in unitsNameList)
            {
                #region  SetVisible= true;

                switch (item)
                {
                    case "20GP": colRateC_20GP.VisibleIndex = CIndex + 1; break;
                    case "40GP": colRateC_40GP.VisibleIndex = CIndex + 2; break;
                    case "40HQ": colRateC_40HQ.VisibleIndex = CIndex + 3; break;
                    case "45HQ": colRateC_45HQ.VisibleIndex = CIndex + 4; break;
                    case "20NOR": colRateC_20NOR.VisibleIndex = CIndex + 5; break;
                    case "40NOR": colRateC_40NOR.VisibleIndex = CIndex + 6; break;

                    case "20FR": colRateC_20FR.VisibleIndex = CIndex + 7; break;
                    case "20RH": colRateC_20RH.VisibleIndex = CIndex + 8; break;
                    case "20RF": colRateC_20RF.VisibleIndex = CIndex + 9; break;
                    case "20HQ": colRateC_20HQ.VisibleIndex = CIndex + 19; break;
                    case "20TK": colRateC_20TK.VisibleIndex = CIndex + 10; break;
                    case "20OT": colRateC_20OT.VisibleIndex = CIndex + 11; break;
                    case "20HT": colRateC_20HT.VisibleIndex = CIndex + 12; break;

                    case "40TK": colRateC_40TK.VisibleIndex = CIndex + 13; break;
                    case "40OT": colRateC_40OT.VisibleIndex = CIndex + 14; break;
                    case "40FR": colRateC_40FR.VisibleIndex = CIndex + 15; break;
                    case "40HT": colRateC_40HT.VisibleIndex = CIndex + 16; break;
                    case "40RH": colRateC_40RH.VisibleIndex = CIndex + 17; break;
                    case "40RF": colRateC_40RF.VisibleIndex = CIndex + 18; break;

                    case "45GP": colRateC_45GP.VisibleIndex = CIndex + 19; break;
                    case "45RF": colRateC_45RF.VisibleIndex = CIndex + 20; break;
                    case "45HT": colRateC_45HT.VisibleIndex = CIndex + 21; break;
                    case "45FR": colRateC_45FR.VisibleIndex = CIndex + 22; break;
                    case "45OT": colRateC_45OT.VisibleIndex = CIndex + 23; break;
                    case "45TK": colRateC_45TK.VisibleIndex = CIndex + 24; break;
                    case "45RH": colRateC_45RH.VisibleIndex = CIndex + 25; break;

                    case "53HQ": colRateC_53HQ.VisibleIndex = CIndex + 26; break;
                }

                #endregion
            }
            #endregion

        }


        #endregion




    }
}
