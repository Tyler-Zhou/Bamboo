using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.Common.UI.Document
{
    /// <summary>
    /// 海出签收海进账单修订历史比对查询页面
    /// </summary>
    public partial class BillReviseHistoryPart : BillReviseBasePart
    {
        public BillReviseHistoryPart()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 签收前签收ID
        /// </summary>

        public Guid BeforeApplyID;
        /// <summary>
        ///签收前签收ID
        /// </summary>
        public Guid AfterApplyeID;

        public override List<ICP.FCM.Common.ServiceInterface.Fee> GetCompareBillAndChargeInfo()
        {
            try
            {
                if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(NewOperationID) == false)
                {
                    return OceanImportService.uspGetHistoryCompareReviseBillAndCharge(NewOperationID, BeforeApplyID, AfterApplyeID);
                }
                return null;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return null; }
            
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //profitComparePart1.Visible = false;
        }
    }
}
