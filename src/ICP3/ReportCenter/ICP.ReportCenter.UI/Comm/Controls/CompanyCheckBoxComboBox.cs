using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.ReportCenter.UI.Comm.Controls
{
    /// <summary>
    /// 操作口岸下拉框：复选框
    /// </summary>
    public class CompanyCheckBoxComboBox : CheckBoxComboBox
    {
        /// <summary>
        /// 操作口岸下拉框
        /// </summary>
        private ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }
        /// <summary>
        /// 重写的加载事件
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                AddCompanyItems();
            }
        }
        /// <summary>
        /// 添加项
        /// </summary>
        private void AddCompanyItems()
        {
            List<OrganizationList> companys = ReportCenterHelper.UserCompanyList;
            this.BeginUpdate();
            foreach (var item in companys)
            {
                if (item.Type == OrganizationType.Company)
                    AddItem(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName, true);
            }
            this.EndUpdate();
            RefreshText();
        }
        /// <summary>
        /// 选择的公司IDS
        /// </summary>
        public List<Guid> CompanyIDs
        {
            get
            {
                List<object> values = this.EditValue;
                if (values == null) return new List<Guid>();
                List<Guid> ids = new List<Guid>();
                foreach (var item in values)
                {
                    ids.Add(new Guid(item.ToString()));
                }
                return ids;
            }
        }

        /// <summary>
        /// 选择的公司名称字符串表示
        /// </summary>
        /// <returns></returns>
        public string CompanyString
        {
            get
            {
                if (this.CheckCount == 1)
                {
                    return this.EditText;
                }
                else
                    return string.Empty;
            }
        }
    
    }
}
