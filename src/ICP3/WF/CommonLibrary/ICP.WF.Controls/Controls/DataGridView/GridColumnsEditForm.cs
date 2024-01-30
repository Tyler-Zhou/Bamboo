using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ICP.WF.Controls
{
    public partial class GridColumnsEditForm : DevExpress.XtraEditors.XtraForm
    {
        public GridColumnsEditForm()
        {
            InitializeComponent();
        }

        #region 属性
        /// <summary>
        /// 对应的列表控件
        /// </summary>
        public LWDataGridView GridView
        {
            get;
            set;
        }
        /// <summary>
        /// 列信息
        /// </summary>
        public List<ColumnInfo> ColumnList
        {
            get;
            set;
        }
        #endregion

        #region 窗体事件

        protected override void OnLoad(EventArgs e)
        {
            if (!DesignMode)
            {
                InitControl();
            }
            base.OnLoad(e);
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            if (GridView != null)
            {
                ColumnList = new List<ColumnInfo>();

                foreach (DataGridViewColumn dc in GridView.Columns)
                {
                    ColumnInfo cl = new ColumnInfo();
                    cl.Name = dc.Name;
                    cl.HeaderText = dc.HeaderText;
                    cl.Tag = dc;
                    cl.Index = dc.DisplayIndex;

                    ColumnList.Add(cl);
                }
                var a = from c in ColumnList orderby c.Index select c;
                ColumnList = a.ToList<ColumnInfo>();

                listColumns.DisplayMember = "HeaderText";
                listColumns.ValueMember = "Tag";

                listColumns.DataSource = ColumnList;
            }
        }
        #endregion


        #region 选择的列发生改变时
        /// <summary>
        /// 选择的列发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listColumns_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listColumns.SelectedValue != null)
            {
                ColumnInfo dc = listColumns.SelectedItem as ColumnInfo;
                if (dc != null)
                {
                    this.lwXtraPropertyGrid1.PropertyGrid.SelectedObject = dc.Tag;
                }
            }
            else
            {
                this.lwXtraPropertyGrid1.PropertyGrid.SelectedObject = null;
            }
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
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #endregion


        #region 新增  移除
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            GridAddColumn ac = new GridAddColumn();
            ac.GridView = GridView;

            if (ac.ShowDialog() == DialogResult.OK)
            {
                GridView = ac.GridView;

                InitControl();
            }
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.listColumns.SelectedItem != null)
            {
                ColumnInfo dc = listColumns.SelectedItem as ColumnInfo;
                if (dc != null)
                {
                    if (ColumnList.Contains(dc))
                    {
                        if (dc.Index >= 0 && dc.Index < GridView.Columns.Count)
                        {
                            DataGridViewColumn dgvColumns = GridView.Columns[dc.Index];
                            try
                            {
                                GridView.Columns.Remove(dgvColumns);

                                InitControl();
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
        }

        #endregion

        private void GridColumnsEditForm_Load(object sender, EventArgs e)
        {
          
        }

        #region 移动位置 
        /// <summary>
        /// 向上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            if (this.listColumns.SelectedItem != null)
            {
                ColumnInfo dc = listColumns.SelectedItem as ColumnInfo;
                if (dc != null)
                {
                    DataGridViewColumn dgvColumns = GridView.Columns[dc.Name];
                    if (dgvColumns.DisplayIndex == 0)
                    {
                        return;
                    }
                    dgvColumns.DisplayIndex = dgvColumns.DisplayIndex - 1;

                    this.InitControl();

                    this.listColumns.SelectedIndex = dgvColumns.DisplayIndex;
                }
            }
        }
        /// <summary>
        /// 向下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            if (this.listColumns.SelectedItem != null)
            {
                ColumnInfo dc = listColumns.SelectedItem as ColumnInfo;
                if (dc != null)
                {
                    DataGridViewColumn dgvColumns = GridView.Columns[dc.Name];
                    if (dgvColumns.DisplayIndex+1 == GridView.Columns.Count)
                    {
                        return;
                    }
                    dgvColumns.DisplayIndex = dgvColumns.DisplayIndex + 1;

                    this.InitControl();

                    this.listColumns.SelectedIndex = dgvColumns.DisplayIndex;
                }
            }
        }
        #endregion
    }


    public class ColumnInfo
    {
        public string Name
        {
            get;
            set;
        }


        public string HeaderText
        {
            get;
            set;
        }

        public int Index
        {
            get;
            set;
        }

        public object Tag
        {
            get;
            set;
        }
    }
}
