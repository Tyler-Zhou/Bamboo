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
    /// �����
    /// </summary>
    public class DcNoteAgeForDRTotalWorkItem:WorkItem
    {
        ReportMainSpace reportMainWorkSpace;
        DcNoteAgeForDRTotal_SearchPart _SearchPart;
        ReportViewBase _ListPart;
        /// <summary>
        /// ��������
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
                info.Title = Utility.GetValueString("Ӧ���ʿ������", "Ӧ���ʿ������");

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
                    this._SearchPart = this.Items.AddNew<DcNoteAgeForDRTotal_SearchPart>();
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
           
            this._paramList.Add(new ReportParameter("Peried", this._SearchPart.EndTime.ToShortDateString()));
            this._paramList.Add(new ReportParameter("Condition", this._SearchPart.XMLCondition));

            //��ʾ�������÷�
            this._ListPart.ReportName = "RPT_DebitnoteAge_Total1";
            this._ListPart.ParamList = this._paramList;
            this._ListPart.DisplayData();
        }
    }
}