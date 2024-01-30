using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.WF.Controls;

namespace ICP.WF.Controls
{
    public partial class GridAddColumn : DevExpress.XtraEditors.XtraForm
    {
        public GridAddColumn()
        {
            InitializeComponent();
        }


        #region 属性
        /// <summary>
        /// 列表控件
        /// </summary>
        public LWDataGridView GridView
        {
            get;
            set;
        }
        /// <summary>
        /// 是否英文环境
        /// </summary>
        public bool IsEnglish
        {
            get
            {
                return ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish;
            }
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (!this.DesignMode)
            {
                InitControls();
            }
            base.OnLoad(e);
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            int c = this.GridView.Columns.Count+1;

            this.txtName.Text=this.txtHeaderText.Text = "Column"+c.ToString();

            cmbColumnType.Properties.Items.Clear();

            if (IsEnglish)
            {
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("TextColumn", "LWStringColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("DateTimeColumn", "LWDateTimeColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("ComboxColumn", "LWComboxColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("NumericColumn", "LWNumericColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("CurrencyColumn", "LWCurrencyColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("ChargeCodeColumn", "LWChargeCodeColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("VehicleHeadColumn", "LWVehicleHeadColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("FindTextColumn", "LWFindTextColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("DeptColumn", "LWDeptColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("GlCodeColumn", "LWGLCodeColumn"));

            }
            else
            {
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("文本列", "LWStringColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("时间列", "LWDateTimeColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("下拉框列", "LWComboxColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("数字列", "LWNumericColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("币种列", "LWCurrencyColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("费用项目列", "LWChargeCodeColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("车辆信息列", "LWVehicleHeadColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("成本费用列", "LWCostItemColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("搜索器列", "LWFindTextColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("部门列", "LWDeptColumn"));
                cmbColumnType.Properties.Items.Add(new ImageComboBoxItem("会计科目列", "LWGLCodeColumn"));

            }
            this.cmbColumnType.SelectedIndex = 0;
        }

        #endregion

        #region 确定与取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtName.Text))
            {
                this.txtName.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show(Utility.GetString("ColumnNameIsNull", "ColumnNameIsNull"));
                return;
            }
            if (string.IsNullOrEmpty(this.txtHeaderText.Text))
            {
                this.txtHeaderText.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show(Utility.GetString("ColumnHeaderTextIsNull", "ColumnHeaderTextIsNull"));
                return;
            }

            if (this.cmbColumnType.SelectedItem==null)
            {
                this.cmbColumnType.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show(Utility.GetString("ColumnTypsIsNull", "ColumnTypsIsNull"));
                return;
            }

            string columnType = cmbColumnType.EditValue.ToString();
            string name = this.txtName.Text;
            string headerText = this.txtHeaderText.Text;

            switch (columnType)
            {
                case "LWStringColumn":

                    LWStringColumn txt = new LWStringColumn();
                    txt.Name = name;
                    txt.HeaderText = headerText;
                    if (IsEnglish)
                    {
                        txt.EText =headerText;                        
                    }
                    else 
                    {
                        txt.CText = headerText;
                    }
                    GridView.Columns.Add(txt);

                    break;

                case "LWDateTimeColumn":

                    LWDateTimeColumn date = new LWDateTimeColumn();
                    date.Name = name;
                    date.HeaderText = headerText;
                    if (IsEnglish)
                    {
                        date.EText = headerText;
                    }
                    else
                    {
                        date.CText = headerText;
                    }
                    GridView.Columns.Add(date);

                    break;

                case "LWCurrencyColumn":

                    LWCurrencyColumn currency = new LWCurrencyColumn();
                    currency.Name = name;
                    currency.HeaderText = headerText;
                    if (IsEnglish)
                    {
                        currency.EText = headerText;
                    }
                    else
                    {
                        currency.CText = headerText;
                    }
                    GridView.Columns.Add(currency);

                    break;
                case "LWDeptColumn":

                    LWDeptColumn dept = new LWDeptColumn();
                    dept.Name = name;
                    dept.HeaderText = headerText;
                    if (IsEnglish)
                    {
                        dept.EText = headerText;
                    }
                    else
                    {
                        dept.CText = headerText;
                    }
                    GridView.Columns.Add(dept);

                    break;
                case "LWComboxColumn":

                    LWComboxColumn comBox = new LWComboxColumn();
                    comBox.Name = name;
                    comBox.HeaderText = headerText;
                    if (IsEnglish)
                    {
                        comBox.EText = headerText;
                    }
                    else
                    {
                        comBox.CText = headerText;
                    }
                    GridView.Columns.Add(comBox);

                    break;

                case "LWNumericColumn":

                    LWNumericColumn num = new LWNumericColumn();
                    num.Name = name;
                    num.HeaderText = headerText;
                    if (IsEnglish)
                    {
                        num.EText = headerText;
                    }
                    else
                    {
                        num.CText = headerText;
                    }
                    GridView.Columns.Add(num);

                    break;

                case "LWChargeCodeColumn":

                    LWChargeCodeColumn chargeCode = new LWChargeCodeColumn();
                    chargeCode.Name = name;
                    chargeCode.HeaderText = headerText;
                    if (IsEnglish)
                    {
                        chargeCode.EText = headerText;
                    }
                    else
                    {
                        chargeCode.CText = headerText;
                    }
                    GridView.Columns.Add(chargeCode);

                    break;


                case "LWVehicleHeadColumn":
                    LWVehicleHeadColumn vehicle = new LWVehicleHeadColumn();
                    vehicle.Name = name;
                    vehicle.HeaderText = headerText;
                    if (IsEnglish)
                    {
                        vehicle.EText = headerText;
                    }
                    else
                    {
                        vehicle.CText = headerText;
                    }
                    GridView.Columns.Add(vehicle);

                    break;

                case "LWCostItemColumn":
                    LWCostItemColumn colstItem = new LWCostItemColumn();
                    colstItem.Name = name;
                    colstItem.HeaderText = headerText;
                    if (IsEnglish)
                    {
                        colstItem.EText = headerText;
                    }
                    else
                    {
                        colstItem.CText = headerText;
                    }
                    GridView.Columns.Add(colstItem);

                    break;
                case "LWGLCodeColumn":
                    LWGLCodeColumn glCode = new LWGLCodeColumn();
                    glCode.Name = name;
                    glCode.HeaderText = headerText;
                    if (IsEnglish)
                    {
                        glCode.EText = headerText;
                    }
                    else
                    {
                        glCode.CText = headerText;
                    }
                    GridView.Columns.Add(glCode);
                    break;
                case "LWFindTextColumn":
                    LWFindTextColumn colFindText = new LWFindTextColumn();
                    colFindText.Name = name;
                    colFindText.HeaderText = headerText;
                    if (IsEnglish)
                        colFindText.EText = headerText;
                    else
                        colFindText.CText = headerText;
                    GridView.Columns.Add(colFindText);
                    break;

                default:
                    break;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #endregion

        #region 下拉框的值发生改变时
        /// <summary>
        /// 选择的类型发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbColumnType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbColumnType.EditValue != null)
            {
                string columnType = cmbColumnType.EditValue.ToString();
                this.txtName.Text=this.txtHeaderText.Text = cmbColumnType.EditValue.ToString() +(GridView.Columns.Count+1).ToString();

            }
        }
        #endregion

    }
}
