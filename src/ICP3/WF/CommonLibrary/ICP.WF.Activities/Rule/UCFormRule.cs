using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;


namespace ICP.WF.Activities
{
	public partial class UCFormRule: XtraUserControl
	{
        public UCFormRule()
		{
            InitializeComponent();
		}
        /// <summary>
        /// 表单下拉框属性列表
        /// </summary>
        public List<string> FormExpressionList = new List<string>();


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                cmbOperator.Properties.Items.Add("like");
                cmbOperator.Properties.Items.Add("equal");
                cmbOperator.Properties.Items.Add("is null");
                cmbOperator.Properties.Items.Add(">=");
                cmbOperator.Properties.Items.Add("<=");
                cmbOperator.Properties.Items.Add("<>");
            }

        }

        #region 事件
        /// <summary>
        /// 获得"表达式"下拉框中的值
        /// </summary>.
        [Description("条件发生改变时，获得条件")]
        public event OperatorTextChang GetOperatorText;

        /// <summary>
        /// 获得"值"文本框中的值
        /// </summary>.
        [Description("获得值文本框中的Text")]
        public event ValueTextChang GetValueText;

        /// <summary>
        /// 获得表单属性的值
        /// </summary>.
        [Description("获得值文本框中的Text")]
        public event ValueTextChang GetFormExpressionText;

        private void cmbOperator_SelectedValueChanged(object sender, EventArgs e)
        {
            if (GetOperatorText != null)
            {
                GetOperatorText(cmbOperator.Text);
            }
        }
      
        /// <summary>
        /// 获得"值"文本框中的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtValue_EditValueChanged(object sender, EventArgs e)
        {
            if (GetValueText != null && !string.IsNullOrEmpty(txtValue.Text))
            {
                GetValueText(txtValue.Text);
            }
        }
        /// <summary>
        /// 获得表单属性的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFormExpression_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GetFormExpressionText != null)
            {
                GetFormExpressionText(cmbFormExpression.Text);
            }
        }

        #endregion

        /// <summary>
        /// 绑定下拉框
        /// </summary>
        public void BindComBox(List<string> strList)
        {
            foreach (string str in strList)
            {
                cmbFormExpression.Properties.Items.Add(str);
            }
        }


    }

    #region 委托

    public delegate void OperatorTextChang(string text);
    public delegate void ValueTextChang(string text);
    public delegate void FormExpressionTextChang(string text);

    #endregion
}
