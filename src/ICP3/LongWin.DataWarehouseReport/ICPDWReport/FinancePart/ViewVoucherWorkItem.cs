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
    /// <summary>
    /// ƾ֤�鿴
    /// </summary>
    public class ViewVoucherWorkItem:WorkItem
    {
        ReportMainSpace reportMainWorkSpace;
        ViewVoucher_SearchPart _SearchPart;
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
                info.Title = Utility.GetValueString("ƾ֤��ϸ��", "ƾ֤��ϸ��");

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
                    this._SearchPart = this.Items.AddNew<ViewVoucher_SearchPart>();
                    this._SearchPart.Dock = DockStyle.Fill;
                    this._SearchPart.SearchResult += new EventHandler(_SearchPart_SearchResult);
                    this._SearchPart.CheckCustomerCode += new EventHandler(_SearchPart_CheckCustomerCode);
                    this._SearchPart.ExportVoucher += new EventHandler(_SearchPart_ExportVoucher);
                }
                this.reportMainWorkSpace.SearchSpace.Show(this._SearchPart);

            }
            else { workspace.Show(this.reportMainWorkSpace); }
        }

        void _SearchPart_ExportVoucher(object sender, EventArgs e)
        {
            try
            {
                _SearchPart.Cursor = Cursors.WaitCursor;

                string strVoucher = VoucherExport.BuilderPlanVoucher(_SearchPart.ListLedgerData,_SearchPart.UFCustomerList);
                _SearchPart.Cursor = Cursors.Default;
                System.Windows.Forms.SaveFileDialog sDialog = new SaveFileDialog();

                //��������Ի���

                sDialog.CheckPathExists = true;
                sDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                sDialog.RestoreDirectory = true;

                if (sDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.Stream stream = sDialog.OpenFile();
                    byte[] buffer = System.Text.Encoding.Default.GetBytes(strVoucher);
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                _SearchPart.Cursor = Cursors.Default;
            }
        }

        void _SearchPart_CheckCustomerCode(object sender, EventArgs e)
        {

            List<LedgerData> _customerList = new List<LedgerData>();
           
            if (_SearchPart.VoucherType == "2")
            {
                _customerList = VoucherExport.CheckUserDepartCode(_SearchPart.UFCustomerList, _SearchPart.ListLedgerData);
            }
            else
            {
                _customerList = VoucherExport.CheckFinceCode(_SearchPart.UFCustomerList, _SearchPart.ListLedgerData);
            }

           if (_customerList.Count == 0)
           {
               MessageBox.Show("�����ڶ�Ӧ���ϵ�����","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
               return;
           }

            this.reportMainWorkSpace.Cursor = Cursors.WaitCursor;
            
            if (this._paramList == null)
            {                
                this._paramList = new List<ReportParameter>();
            }
            this._paramList.Clear();                    
            
           string  reportName = "LongWin.DataWarehouseReport.WinUI.FinancePart.SearchPart.CustomerFinanceCodeList.rdlc";

           List<ReportDataSource> ds = new List<ReportDataSource>();
            ds.Add(new ReportDataSource("LedgerData",_customerList));
            //��ʾ������÷�

            this._ListPart.ParamList = this._paramList;
            this._ListPart.DisplayLocalReport(reportName, ds,_paramList);
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

            this._paramList.Add(new ReportParameter("Condition", this._SearchPart.XMLCondition));

            if (this._SearchPart.ReportType == 1)
                this._ListPart.ReportName = "RPT_GetVoucherInfoGroupByGL";  
            else
            this._ListPart.ReportName = "RPT_ALLGetVoucherInfo";  
     

            //��ʾ������÷�
            
            this._ListPart.ParamList = this._paramList;
            this._ListPart.DisplayData();
        }
    }
}
