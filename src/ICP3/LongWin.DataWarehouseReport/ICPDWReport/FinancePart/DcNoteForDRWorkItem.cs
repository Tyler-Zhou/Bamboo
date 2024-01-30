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
    /// Ӧ���ʿ��
    /// </summary>
    public class DcNoteForDRWorkItem:WorkItem
    {
        ReportMainSpace reportMainWorkSpace;
        DcNoteForDRCR_SearchPart _SearchPart;
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
                info.Title = Utility.GetValueString("Ӧ���ʿ��", "Ӧ���ʿ��"); 

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
                    this._SearchPart = this.Items.AddNew<DcNoteForDRCR_SearchPart>();
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
            this._paramList.Add(new ReportParameter("SalesSet", this._SearchPart.EmployeeIDs.ToString()));
            this._paramList.Add(new ReportParameter("ShipperSet", this._SearchPart.CustomerIds));
            this._paramList.Add(new ReportParameter("JobType", this._SearchPart.BusinessTypes));
            this._paramList.Add(new ReportParameter("VerifyFlag", this._SearchPart.RecoupFlag.ToString()));

            this._paramList.Add(new ReportParameter("CompanyName", Utility.IsEnglish ? this._SearchPart.CompanyEname : this._SearchPart.CompanyCName));
            this._paramList.Add(new ReportParameter("CompanyAddress", Utility.IsEnglish ? this._SearchPart.CompanyEname : this._SearchPart.CompanyCName));
            this._paramList.Add(new ReportParameter("TelOrFax", this._SearchPart.TelOrFax));
            this._paramList.Add(new ReportParameter("JobPlace", this._SearchPart.JobPlace));
            this._paramList.Add(new ReportParameter("ConditionRemark", this._SearchPart.ConditionRemark));
            this._paramList.Add(new ReportParameter("IsEnglish", Utility.IsEnglish ? "1" : "0"));
            //�����ͳ�Ʊ�
            if (this._SearchPart.IsTotal)
            {
                this._ListPart.ReportName = "RPT_DebitnoteForDR_Total";
                this._paramList.Add(new ReportParameter("GroupFields", this._SearchPart.GroupField));
                this._paramList.Add(new ReportParameter("GroupCount", this._SearchPart.GroupCount.ToString()));
            }
            //�����ϸ��
            else
            {
                this._ListPart.ReportName = "RPT_Debitnote_DetailDR";
                this._paramList.Add(new ReportParameter("OtherFields", this._SearchPart.GroupField));
                this._paramList.Add(new ReportParameter("ShippingLineSet", this._SearchPart.ShippingLineIds));
                this._paramList.Add(new ReportParameter("Type", "0"));
                
            }

            //��ʾ������÷�

            this._ListPart.ParamList = this._paramList;
            this._ListPart.DisplayData();
        }
    }
}
