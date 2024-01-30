using System;
using System.Collections.Generic;
using System.Text;
using Agilelabs.Framework;
using System.Windows.Forms;
namespace LongWin.DataWarehouseReport.WinUI
{
    public class DataGridViewSortHelp
    {
        public static void Sort<T>(BindingSource bindSouce, DataGridView dgv, int colIndex)
        {
            if (dgv.DataSource == null || dgv.RowCount < 2)
            {
                return;
            }
            try
            {
                if (dgv.Tag == null) dgv.Tag = System.Windows.Forms.SortOrder.Ascending;

                string sortField = string.Empty;

                if ((System.Windows.Forms.SortOrder)dgv.Tag == System.Windows.Forms.SortOrder.Ascending)
                {
                    sortField = dgv.Columns[colIndex].DataPropertyName.Trim() + " DESC ";
                }
                else
                {
                    sortField = dgv.Columns[colIndex].DataPropertyName.Trim() + " ASC ";
                }

                dgv.SuspendLayout();
                DynamicComparer<T> comparer = new DynamicComparer<T>(sortField);
                (bindSouce.List as List<T>).Sort(new Comparison<T>(comparer.Compare));
                bindSouce.ResetBindings(false);
                dgv.ResumeLayout();


                dgv.CurrentCell = dgv.Rows[0].Cells[colIndex];
                dgv.Columns[colIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                if ((System.Windows.Forms.SortOrder)dgv.Tag == System.Windows.Forms.SortOrder.Ascending)
                {
                    dgv.Columns[colIndex].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Descending;
                    dgv.Tag = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    dgv.Columns[colIndex].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Ascending;
                    dgv.Tag = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("≤ªƒ‹≈≈–Ú" + ex.Message);
            }
        }

    }
}
