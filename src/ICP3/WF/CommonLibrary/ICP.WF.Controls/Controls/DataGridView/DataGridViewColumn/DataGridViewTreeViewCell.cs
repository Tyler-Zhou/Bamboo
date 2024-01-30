using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace ICP.WF.Controls
{
    /// <summary>
    /// 树控件单元格
    /// </summary>
    public class DataGridViewTreeViewCell : DataGridViewTextBoxCell
    {
        public DataGridViewTreeViewCell()
        {

        }

        public override void InitializeEditingControl(int rowIndex
            , object initialFormattedValue
            , DataGridViewCellStyle dataGridViewCellStyle)
        {

            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            DataGridViewTreeViewEditingControl ctl = EditingControl;
            ctl.Text = initialFormattedValue.ToString();
            if (this.OwningColumn is LWCostItemColumn)
            {
                //if (ctl.DataSource == null)
                //{
                    LWCostItemColumn col = this.OwningColumn as LWCostItemColumn;

                    if (Utility.IsEnglish)
                    {
                        ctl.DisplayMember = col.EDisplayMember;
                    }
                    else
                    {
                        ctl.DisplayMember = col.CDisplayMember;
                    }

                    ctl.DataSource = col.DataSource;
                    ctl.CDisplayMember = col.CDisplayMember;
                    ctl.EDisplayMember = col.EDisplayMember;
                    ctl.ValueMember = col.ValueMember;
                    ctl.SelectedValue = this.Value;
                //}
            }
            else if (this.OwningColumn is LWDeptColumn)
            {
                    LWDeptColumn col = this.OwningColumn as LWDeptColumn;

                    if (Utility.IsEnglish)
                    {
                        ctl.DisplayMember = col.EDisplayMember;
                    }
                    else
                    {
                        ctl.DisplayMember = col.CDisplayMember;
                    }

                    ctl.DataSource = col.DataSource;
                    ctl.CDisplayMember = col.CDisplayMember;
                    ctl.EDisplayMember = col.EDisplayMember;
                    ctl.ValueMember = col.ValueMember;
                    ctl.SelectedValue = this.Value;
            }
            else if (this.OwningColumn is LWGLCodeColumn)
            {
                LWGLCodeColumn col = this.OwningColumn as LWGLCodeColumn;

                if (Utility.IsEnglish)
                {
                    ctl.DisplayMember = col.EDisplayMember;
                }
                else
                {
                    ctl.DisplayMember = col.CDisplayMember;
                }

                ctl.DataSource = col.DataSource;
                ctl.CDisplayMember = col.CDisplayMember;
                ctl.EDisplayMember = col.EDisplayMember;
                ctl.ValueMember = col.ValueMember;
                ctl.SelectedValue = this.Value;
            }
            else
            {

            }
        }

        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewTreeViewEditingControl);
            }
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            if (value == null) return "";
            if (this.OwningColumn is LWCostItemColumn)
            {
                LWCostItemColumn col = this.OwningColumn as LWCostItemColumn;

                DataTable dt = this.GetDataTable();
                if (dt == null)
                {
                    return "";
                }
                DataRow dr = dt.Rows.Find(value);
                if (dr != null)
                {
                    string member = string.Empty;
                    if (string.IsNullOrEmpty(member))
                    {
                        member = Utility.IsEnglish ? col.EFullDisplayMember : col.CFullDisplayMember;
                    }

                    return dr[member].ToString();
                }
            }
            else if (this.OwningColumn is LWDeptColumn)
            {
                LWDeptColumn col = this.OwningColumn as LWDeptColumn;

                DataTable dt = this.GetDataTable();
                if (dt == null)
                {
                    return "";
                }
                DataRow dr = dt.Rows.Find(value);
                if (dr != null)
                {
                    string member = string.Empty;
                    if (string.IsNullOrEmpty(member))
                    {
                        member = Utility.IsEnglish ? col.EFullDisplayMember : col.CFullDisplayMember;
                    }

                    return dr[member].ToString();
                }
            }
            else if (this.OwningColumn is LWGLCodeColumn)
            {
                LWGLCodeColumn col = this.OwningColumn as LWGLCodeColumn;

                DataTable dt = this.GetDataTable();
                if (dt == null)
                {
                    return "";
                }
                DataRow dr = dt.Rows.Find(value);
                if (dr != null)
                {
                    string member = string.Empty;
                    if (string.IsNullOrEmpty(member))
                    {
                        member = Utility.IsEnglish ? col.EFullDisplayMember : col.CFullDisplayMember;
                    }

                    return dr[member].ToString();
                }
            }
            return "";
        }
        public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
        {
            return formattedValue;
        }
        public DataTable GetDataTable()
        {
            DataTable dt=null;
            if (this.OwningColumn is LWCostItemColumn)
            {
                LWCostItemColumn col = this.OwningColumn as LWCostItemColumn;
                dt = DataTableConverter.ConvertToDataTable(col.DataSource);
            }
            else if (this.OwningColumn is LWDeptColumn)
            {
                LWDeptColumn col = this.OwningColumn as LWDeptColumn;
                dt = DataTableConverter.ConvertToDataTable(col.DataSource);
            }
            else if (this.OwningColumn is LWGLCodeColumn)
            {
                LWGLCodeColumn col = this.OwningColumn as LWGLCodeColumn;
                dt = DataTableConverter.ConvertToDataTable(col.DataSource);
            }
            return dt;
        }
        public DataGridViewTreeViewEditingControl EditingControl
        {
            get
            {
                return DataGridView.EditingControl as DataGridViewTreeViewEditingControl;
            }
        }
    }
}
