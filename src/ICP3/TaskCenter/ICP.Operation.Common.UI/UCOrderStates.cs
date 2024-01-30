using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Operation.Common.ServiceInterface;
using System;
using System.Data;

namespace ICP.Operation.Common.UI
{
    /// <summary>
    /// 订单状态
    /// </summary>
    public partial class UCOrderStates : BaseEditPart
    {
        #region Services
        /// <summary>
        /// 业务查询接口
        /// </summary>
        public IBusinessQueryService BusinessQueryService
        {
            get
            {
                return ServiceClient.GetService<IBusinessQueryService>();
            }
        } 
        #endregion

        #region Property
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID
        {
            get;
            set;
        }

        #region 订舱
        /// <summary>
        /// 申请订舱
        /// </summary>
        public bool SOA
        {
            get { return checkEditSOA.Checked; }
            set { checkEditSOA.Checked = value; }
        }
        /// <summary>
        /// 订舱成功
        /// </summary>
        public bool SOD
        {
            get { return checkEditSOD.Checked; }
            set { checkEditSOD.Checked = value; }
        }
        /// <summary>
        /// 通知客户订舱成功
        /// </summary>
        public bool SOS
        {
            get { return checkEditSOS.Checked; }
            set { checkEditSOS.Checked = value; }
        }
        /// <summary>
        /// 已向拖车公司下达委托单
        /// </summary>
        public bool TRKA
        {
            get { return checkEditTRKA.Checked; }
            set { checkEditTRKA.Checked = value; }
        }
        /// <summary>
        /// 已审批SO毛利
        /// </summary>
        public bool SOPV
        {
            get { return checkEditSOPV.Checked; }
            set { checkEditSOPV.Checked = value; }
        }
        /// <summary>
        /// 取消订舱
        /// </summary>
        public bool SOC
        {
            get { return checkEditSOC.Checked; }
            set { checkEditSOC.Checked = value; }
        }
        #endregion

        #region 提单
        /// <summary>
        /// 已通知客户提供补料
        /// </summary>
        public bool SINC
        {
            get { return checkEditSINC.Checked; }
            set { checkEditSINC.Checked = value; }
        }
        /// <summary>
        /// 已收到客户提供的补料
        /// </summary>
        public bool SIR
        {
            get { return checkEditSIR.Checked; }
            set { checkEditSIR.Checked = value; }
        }
        /// <summary>
        /// 已发BL Copy让客户确认
        /// </summary>
        public bool BLCFM
        {
            get { return checkEditBLCFM.Checked; }
            set { checkEditBLCFM.Checked = value; }
        }
        /// <summary>
        /// 已发BL Copy让代理确认
        /// </summary>
        public bool BLCfmAgt
        {
            get { return checkEditBLCfmAgt.Checked; }
            set { checkEditBLCfmAgt.Checked = value; }
        }
        /// <summary>
        /// 已收到MBL Copy
        /// </summary>
        public bool MBLR
        {
            get { return checkEditMBLR.Checked; }
            set { checkEditMBLR.Checked = value; }
        }
        /// <summary>
        /// 已向承运人补料
        /// </summary>
        public bool MBLD
        {
            get { return checkEditMBLD.Checked; }
            set { checkEditMBLD.Checked = value; }
        }
        /// <summary>
        /// AMS已发送
        /// </summary>
        public bool AMS
        {
            get { return checkEditAMS.Checked; }
            set { checkEditAMS.Checked = value; }
        }
        /// <summary>
        /// ISF已发送
        /// </summary>
        public bool ISF
        {
            get { return checkEditISF.Checked; }
            set { checkEditISF.Checked = value; }
        }
        #endregion

        #region 核对账单
        /// <summary>
        /// 应收账单已核销(支付)
        /// </summary>
        public bool ARP
        {
            get { return checkEditARP.Checked; }
            set { checkEditARP.Checked = value; }
        }
        /// <summary>
        /// 已申请运费付讫
        /// </summary>
        public bool APA
        {
            get { return checkEditAPA.Checked; }
            set { checkEditAPA.Checked = value; }
        }
        /// <summary>
        /// 已支付应付账单
        /// </summary>
        public bool APP
        {
            get { return checkEditAPP.Checked; }
            set { checkEditAPP.Checked = value; }
        }
        /// <summary>
        /// 已通知承运人开发票
        /// </summary>
        public bool APInv
        {
            get { return checkEditAPInv.Checked; }
            set { checkEditAPInv.Checked = value; }
        }
        /// <summary>
        /// 已向客户确认应收账单
        /// </summary>
        public bool ARCfm
        {
            get { return checkEditARCfm.Checked; }
            set { checkEditARCfm.Checked = value; }
        }
        /// <summary>
        /// 已通知客户付款
        /// </summary>
        public bool ARA
        {
            get { return checkEditARA.Checked; }
            set { checkEditARA.Checked = value; }
        }
        #endregion

        #region 放单
        /// <summary>
        /// 文件已分给港口代理
        /// </summary>
        public bool DocS
        {
            get { return checkEditDocS.Checked; }
            set { checkEditDocS.Checked = value; }
        }
        /// <summary>
        /// 港后代理已受理文件并分配
        /// </summary>
        public bool DocH
        {
            get { return checkEditDocH.Checked; }
            set { checkEditDocH.Checked = value; }
        }
        /// <summary>
        /// 港后代理已修订了代理账单
        /// </summary>
        public bool DCRev
        {
            get { return checkEditDCRev.Checked; }
            set { checkEditDCRev.Checked = value; }
        }
        /// <summary>
        /// 已签收了港后代理修订的代理账单
        /// </summary>
        public bool DcRcv
        {
            get { return checkEditDcRcv.Checked; }
            set { checkEditDcRcv.Checked = value; }
        }
        /// <summary>
        /// 已放单
        /// </summary>
        public bool RBLD
        {
            get { return checkEditRBLD.Checked; }
            set { checkEditRBLD.Checked = value; }
        }
        /// <summary>
        /// 代理已收到放单通知
        /// </summary>
        public bool RC
        {
            get { return checkEditRC.Checked; }
            set { checkEditRC.Checked = value; }
        }
        #endregion

        #endregion

        #region Init
        /// <summary>
        /// 订单状态
        /// </summary>
        public UCOrderStates()
        {
            InitializeComponent();
            Load += UCOrderStates_Load;
        }

        void UCOrderStates_Load(object sender, EventArgs e)
        {
            try
            {
                if (OperationID.IsNullOrEmpty()) return;
                BusinessQueryCriteria criteria = new BusinessQueryCriteria
                {
                    TemplateCode = "TaskCenter_OceanExport_ViewOrderStates",
                    OperationType = OperationType.OceanExport,
                    AdvanceQueryString = " [oViewCache].[OperationID]='" + OperationID + "'",
                    TopCount = 00
                };
                DataTable dt =BusinessQueryService.Get(criteria);
                if (dt != null && dt.Rows.Count > 0)
                {
                    SOA = ConvertToBoolean(dt.Rows[0]["SOA"].ToString());
                    SOD = ConvertToBoolean(dt.Rows[0]["SOD"].ToString());
                    SOS = ConvertToBoolean(dt.Rows[0]["SOS"].ToString());
                    TRKA = ConvertToBoolean(dt.Rows[0]["TRKA"].ToString());
                    SOPV = ConvertToBoolean(dt.Rows[0]["SOPV"].ToString()) ;
                    SOC = ConvertToBoolean(dt.Rows[0]["SOC"].ToString()) ;
                    SINC = ConvertToBoolean(dt.Rows[0]["SINC"].ToString()) ;
                    SIR = ConvertToBoolean(dt.Rows[0]["SIR"].ToString()) ;
                    BLCFM = ConvertToBoolean(dt.Rows[0]["BLCFM"].ToString());
                    BLCfmAgt = ConvertToBoolean(dt.Rows[0]["BLCfmAgt"].ToString());
                    MBLR = ConvertToBoolean(dt.Rows[0]["MBLR"].ToString());
                    MBLD = ConvertToBoolean(dt.Rows[0]["MBLD"].ToString());
                    AMS = ConvertToBoolean(dt.Rows[0]["AMS"].ToString());
                    ISF = ConvertToBoolean(dt.Rows[0]["ISF"].ToString());
                    ARP = ConvertToBoolean(dt.Rows[0]["ARP"].ToString());
                    APA = ConvertToBoolean(dt.Rows[0]["APA"].ToString());
                    APP = ConvertToBoolean(dt.Rows[0]["APP"].ToString());
                    APInv = ConvertToBoolean(dt.Rows[0]["APInv"].ToString());
                    ARCfm = ConvertToBoolean(dt.Rows[0]["ARCfm"].ToString());
                    ARA = ConvertToBoolean(dt.Rows[0]["ARA"].ToString()) ;
                    DocS = ConvertToBoolean(dt.Rows[0]["DocS"].ToString());
                    DocH = ConvertToBoolean(dt.Rows[0]["DocH"].ToString());
                    DCRev = ConvertToBoolean(dt.Rows[0]["DCRev"].ToString());
                    DcRcv = ConvertToBoolean(dt.Rows[0]["DcRcv"].ToString());
                    RBLD = ConvertToBoolean(dt.Rows[0]["RBLD"].ToString());
                    RC = ConvertToBoolean(dt.Rows[0]["BLRC"].ToString());
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        bool ConvertToBoolean(string strState)
        {
            if (string.IsNullOrEmpty(strState))
                return false;
            if ("True".Equals(strState))
                return true;
            if ("False".Equals(strState))
                return false;
            if (Convert.ToInt32(strState)>0)
                return true;
            return false;
        }
        #endregion
    }
}
