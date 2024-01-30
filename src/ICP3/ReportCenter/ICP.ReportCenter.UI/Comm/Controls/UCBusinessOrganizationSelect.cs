using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ICP.ReportCenter.UI.Comm.Controls
{  
    /// <summary>
    /// 业务发生地、业务发生部门单选框选择用户控件
    /// </summary>
    public partial class UCBusinessOrganizationSelect : XtraUserControl
    {
        public UCBusinessOrganizationSelect()
        {
            InitializeComponent();
        }
        void rdoDepartment_CheckedChanged(object sender, EventArgs e)
        {

            bool showDepartment = true;
            if (rdoOperationOrgial.Checked)
            {
                showDepartment = false;
            }
            this.treeBoxSalesDep.ShowDepartment = showDepartment;
            this.treeBoxSalesDep.AddCompanyItems();
        }
        /// <summary>
        /// 选择的组织类型
        /// 0:业务发生地
        /// 1:业务发生部门
        /// </summary>
        public Int32 OrganizationType
        {
            get
            {
                return rdoOperationOrgial.Checked ? 0 : 1;
            }
        }
        /// <summary>
        /// 选择的组织Id列表
        /// </summary>
        public List<Guid> SelectedOrganizationIds
        {
            get
            {
                return this.treeBoxSalesDep.GetAllEditValue;
            }
        }
        /// <summary>
        /// 选择的组织Id字符串，中间用逗号分隔
        /// </summary>
        public string SelectedOrganizationString
        {
            get
            {
                return treeBoxSalesDep.GetAllEditValue.ToSplitString(",");
            }
        }
        public string EditText
        {
            get
            {
                return this.treeBoxSalesDep.EditText;
            }
        }
        public string EditValueString
        {
            get
            {
                return this.treeBoxSalesDep.EditValue.ToSplitString(",");
            }
        }
        public string JobPlace
        {
            get
            {
                StringBuilder strBuilder = new StringBuilder();
                if (this.rdoOperationDepartment.Checked)
                {
                    strBuilder.Append(rdoOperationDepartment.Text + "  : ");
                }
                else if (this.rdoOperationOrgial.Checked)
                {
                    strBuilder.Append(rdoOperationOrgial.Text + "  : ");
                }
                strBuilder.Append(treeBoxSalesDep.EditText);
                return strBuilder.ToString();
            }
        }
    }
}
