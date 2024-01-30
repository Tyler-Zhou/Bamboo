using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;

namespace ICP.WF.Controls
{
    class DataGridViewComBoxCell : DataGridViewTextBoxCell
    {
        public DataGridViewComBoxCell()
        {
        }
        public override void InitializeEditingControl(int rowIndex
            , object initialFormattedValue
            , DataGridViewCellStyle dataGridViewCellStyle)
        {

            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            DataGridViewComBoxEditingControl ctl = EditingControl;
            ctl.Text = initialFormattedValue.ToString();

            if (this.OwningColumn is LWCurrencyColumn)
            {
                LWCurrencyColumn col = this.OwningColumn as LWCurrencyColumn;


                ctl.DataSource = col.DataSource;
                ctl.CDisplayMember = col.CDisplayMember;
                ctl.EDisplayMember = col.EDisplayMember;
                ctl.ValueMember = col.ValueMember;
                ctl.EditValue = this.Value;
            }
            else if (this.OwningColumn is LWChargeCodeColumn)
            {
                LWChargeCodeColumn col = this.OwningColumn as LWChargeCodeColumn;

                ctl.DataSource = col.DataSource;
                ctl.CDisplayMember = col.CDisplayMember;
                ctl.EDisplayMember = col.EDisplayMember;
                ctl.ValueMember = col.ValueMember;
                ctl.EditValue = this.Value;
            }
            else if (this.OwningColumn is LWDeptColumn)
            {
                LWDeptColumn col = this.OwningColumn as LWDeptColumn;

                ctl.DataSource = col.DataSource;
                ctl.CDisplayMember = col.CDisplayMember;
                ctl.EDisplayMember = col.EDisplayMember;
                ctl.ValueMember = col.ValueMember;
                ctl.EditValue = this.Value;
            }



        }

        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewComBoxEditingControl);
            }
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            if (value == null)
            {
                return "";
            }

            if (this.OwningColumn is LWCurrencyColumn)
            {
                LWCurrencyColumn col = this.OwningColumn as LWCurrencyColumn;
                DataTable dt = this.GetDataTable();

                if (dt == null)
                {
                    return "";
                }
                DataRow dr = dt.Rows.Find(value);
                if (dr != null)
                {
                    string member = string.Empty;
                   
                    member = col.EDisplayMember;

                    return dr[member].ToString();
                }
            }
            else if (this.OwningColumn is LWChargeCodeColumn)
            {
                LWChargeCodeColumn col = this.OwningColumn as LWChargeCodeColumn;
                DataTable dt = this.GetDataTable();
                if (dt == null)
                {
                    return "";
                }
                DataRow dr = dt.Rows.Find(value);
                if (dr != null)
                {
                    string member = string.Empty;
                    if (Utility.IsEnglish)
                    {
                        member = col.EDisplayMember;
                    }
                    else
                    {
                        member = col.CDisplayMember;
                    }
                    if (string.IsNullOrEmpty(member))
                    {
                        member = Utility.IsEnglish ? col.EDisplayMember : col.CDisplayMember;
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
                    if (Utility.IsEnglish)
                    {
                        member = col.EDisplayMember;
                    }
                    else
                    {
                        member = col.CDisplayMember;
                    }
                    if (string.IsNullOrEmpty(member))
                    {
                        member = Utility.IsEnglish ? col.EDisplayMember : col.CDisplayMember;
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
            if (this.OwningColumn is LWCurrencyColumn)
            {
                LWCurrencyColumn col = this.OwningColumn as LWCurrencyColumn;
                return DataTableConverter.ConvertToDataTable(col.DataSource);
            }
            else if (this.OwningColumn is LWChargeCodeColumn)
            {
                LWChargeCodeColumn col = this.OwningColumn as LWChargeCodeColumn;
                return DataTableConverter.ConvertToDataTable(col.DataSource);
            }
            else if (this.OwningColumn is LWDeptColumn)
            {
                LWDeptColumn col = this.OwningColumn as LWDeptColumn;
                return DataTableConverter.ConvertToDataTable(col.DataSource);
            }
            return new DataTable();
        }
        public DataGridViewComBoxEditingControl EditingControl
        {
            get
            {
                return DataGridView.EditingControl as DataGridViewComBoxEditingControl;
            }
        }
    }
}