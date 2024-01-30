using System;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FRM.UI.InquireRates.Trucking
{
    public partial class InquireTruckFUEL : BasePart
    {

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IInquireRatesService InquireRatesService
        {
            get
            {
                return ServiceClient.GetService<IInquireRatesService>();
            }
        }

        public InquireTruckFUEL()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //inquireRatesService.UpdateInquireTruckingPriceFUEL(dtFrom.DateTime, dtTo.DateTime, (decimal)speFUEL.EditValue, LocalData.UserInfo.LoginID);
            //LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Update Successfully" : "更新成功");
            //Workitem.Commands[InquireRatesCommandConstants.Command_RefreshData].Execute();
            //this.FindForm().Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }
    }
}
