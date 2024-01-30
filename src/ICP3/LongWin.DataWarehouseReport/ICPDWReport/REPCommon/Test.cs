using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;

using Microsoft.Reporting.WinForms;
using LongWin.DataWarehouseReport.ServiceInterface;

namespace LongWin.DataWarehouseReport.WinUI
{
    public class Test : WorkItem
    {
        ReportMainSpace reportMainWorkSpace;
        ReportViewBase _ListPart;
        /// <summary>
        /// 报表参数
        /// </summary>
        //List<ReportParameter> _paramList;

       
       

        internal void Show1(IWorkspace workspace)
        {
            if (this.reportMainWorkSpace == null)
            {
                this.reportMainWorkSpace = this.Items.AddNew<ReportMainSpace>((new Guid()).ToString());
                this.reportMainWorkSpace.Dock = DockStyle.Fill;
                SmartPartInfo info = new SmartPartInfo();
                info.Title = "测试";

                if (this._ListPart == null)
                {
                    this._ListPart = this.Items.AddNew<ReportViewBase>();
                    this._ListPart.Dock = DockStyle.Fill;
                }
                this.reportMainWorkSpace.ListSpace.Show(this._ListPart);

                workspace.Show(this.reportMainWorkSpace, info);

                this.Testb();
            }
        }


        private void Testb()
        {
            Guid costFeeId = Guid.NewGuid();
            DateTime happenDate = DateTime.Now;
            Guid deptId = new Guid("3C20304C-6804-401F-B96C-0ADA22970358");
            Guid userId = new Guid("15CB8E09-BA2F-4AEF-82B6-8FB8B0291DF1");
            decimal amount = System.Convert.ToDecimal(200.56);
            //string no = "001";

            int Count = 4;

            Guid[] costItemids = new Guid[Count];
            short[] feePropertys = new short[Count];
            decimal[] amounts = new decimal[Count];
            string[] remarks = new string[Count];

            for (int i = 0; i < Count; i++)
            {
                costItemids[i] = Guid.NewGuid();
                feePropertys[i] = 1;
                amounts[i] = System.Convert.ToDecimal(5.89);
                remarks[i] = amounts[i].ToString();
            }

            //Guid id = this._buildDataService.SaveCostFee(costFeeId, happenDate, deptId, userId, amount, feeProperty, no,
            //    costItemids, amounts, remarks);
            //MessageBox.Show(id.ToString() + "/" + costFeeId.ToString());

            for (int i = 0; i < Count; i++)
            {
                if (i == 0 || i == 2)
                {
                    amounts[i] = System.Convert.ToDecimal(6.89);
                }
            }

            //Guid id1 = this._buildDataService.SaveCostFee(costFeeId, happenDate, deptId, userId, amount, feeProperty, no,
            //    costItemids, amounts, remarks);
            //MessageBox.Show(id1.ToString() + "/" + costFeeId.ToString());

        }


        Common.TestService testservice;
        internal void Show(IWorkspace workspace)
        {
            if (this.testservice == null)
            {
                this.testservice = this.Items.AddNew<Common.TestService>((new Guid()).ToString());
                this.testservice.Dock = DockStyle.Fill;
                SmartPartInfo info = new SmartPartInfo();
                info.Title = "测试服务";

                workspace.Show(this.testservice, info);

            }
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            UFServerForm form = new UFServerForm();
            form.Show();
        }
    }
}
