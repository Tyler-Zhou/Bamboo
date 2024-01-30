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
namespace LongWin.DataWarehouseReport.WinUI
{
    /// <summary>
    /// �������
    /// </summary>
    public class DebitnoteForAgentWorkItem:WorkItem
    {
        ReportMainSpace reportMainWorkSpace;
        DebitnoteForAgent_SearchPart _SearchPart;
        ReportViewBase _ListPart;
        /// <summary>
        /// �������
        /// </summary>
        List<ReportParameter> _paramList;

        internal void Show(IWorkspace workspace)
        {
            if (this.reportMainWorkSpace == null)
            {
                this.reportMainWorkSpace = this.Items.AddNew<ReportMainSpace>((new Guid()).ToString());
                this.reportMainWorkSpace.Disposed += new EventHandler(reportMainWorkSpace_Disposed);
                this.reportMainWorkSpace.Dock = DockStyle.Fill;
                SmartPartInfo info = new SmartPartInfo();
                info.Title = Utility.GetValueString("�����ʿ��", "�����ʿ��"); 

                workspace.Show(this.reportMainWorkSpace, info);
                //������ʾ���
                if (this._ListPart == null)
                {
                    this._ListPart = this.Items.AddNew<ReportViewBase>();
                    this._ListPart.Dock = DockStyle.Fill;
                }
                this.reportMainWorkSpace.ListSpace.Show(this._ListPart);

                //��ѯ���
                if (this._SearchPart == null)
                {
                    this._SearchPart = this.Items.AddNew<DebitnoteForAgent_SearchPart>();
                    this._SearchPart.Dock = DockStyle.Fill;
                    this._SearchPart.SearchResult += new EventHandler(_SearchPart_SearchResult);
                }
                this.reportMainWorkSpace.SearchSpace.Show(this._SearchPart);

            }
            else { workspace.Show(this.reportMainWorkSpace); }
        }

        void reportMainWorkSpace_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _SearchPart_SearchResult(object sender, EventArgs e)
        {
            this.reportMainWorkSpace.Cursor = Cursors.WaitCursor;
            if (this._paramList == null)
            {
                this._paramList = new List<ReportParameter>();
            }

            this._paramList.Clear();

            this._paramList.Add(new ReportParameter("StructType", this._SearchPart.StructureType.ToString()));
            this._paramList.Add(new ReportParameter("StructNodeId", this._SearchPart.StructNodeId.ToString()));
            this._paramList.Add(new ReportParameter("ETD_Beginning_Date", this._SearchPart.BeginTime.ToString()));
            this._paramList.Add(new ReportParameter("ETD_Ending_Date", this._SearchPart.EndTime.ToString()));
            this._paramList.Add(new ReportParameter("ShipperSet", this._SearchPart.CustomerIds));
            this._paramList.Add(new ReportParameter("VerifyFlag", this._SearchPart.RecoupFlag.ToString()));
            this._paramList.Add(new ReportParameter("JobType", this._SearchPart.BusinessTypes));           
            this._paramList.Add(new ReportParameter("CompanyAddress",this._SearchPart.CompanyCAddress));
            this._paramList.Add(new ReportParameter("TelOrFax", this._SearchPart.TelOrFax));
            
            this._paramList.Add(new ReportParameter("CompanyEAddress", this._SearchPart.CompanyEAddress));
            this._paramList.Add(new ReportParameter("CompanyName", this._SearchPart.CompanyCName));
            this._paramList.Add(new ReportParameter("CompanyEName", this._SearchPart.CompanyEname));
            this._paramList.Add(new ReportParameter("IsEnglish", Utility.IsEnglish ? "1" : "0"));
            this._paramList.Add(new ReportParameter("IsDisplayPrePay", this._SearchPart.PrePayFlag.ToString()) );


            //�����ͳ�Ʊ�
            if (this._SearchPart.IsTotal)
            {
                this._ListPart.ReportName = "RPT_DebitnoteForAgent_Total";
                this._paramList.Add(new ReportParameter("JobPlace", this._SearchPart.JobPlace));
                this._paramList.Add(new ReportParameter("ConditionRemark", this._SearchPart.ConditionRemark));
            }
            //�����ϸ��
            else
            {
               
                this._ListPart.ReportName = "RPT_DebitnoteForAgent_Detail";               
                this._paramList.Add(new ReportParameter("SalesSet", this._SearchPart.EmployeeIDs.ToString()));
                this._paramList.Add(new ReportParameter("CurrencyType", this._SearchPart.CurrrencyType.ToString()));
                this._paramList.Add(new ReportParameter("DisplayAttach", this._SearchPart.ShowAttach));
            }

            //��ʾ������÷�

            this._ListPart.ParamList = this._paramList;
            this._ListPart.DisplayData();
        }
    }
}
