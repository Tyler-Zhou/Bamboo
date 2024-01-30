using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.TransportFoundation.DataDictionary
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class DataDictionarySearchPart : DevExpress.XtraEditors.XtraUserControl, ICP.Framework.ClientComponents.UIManagement.ISearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        #endregion

        #region init

        public DataDictionarySearchPart()
        {
            InitializeComponent();
            this.Load += new EventHandler(DataDictionarySeachPart_Load);

            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this.DataReturned = null;
                this.cmbType.OnFirstEnter -= this.OncmbTypeFirstEnter;
                CommonUtility.RemoveSearchPartKeyEnterToSearch(new List<Control> { this.txtCode, this.txtName }, this.KeyEventHandle);
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        private void SetCnText()
        {
            labCode.Text = "代码";
            labIsValid.Text = "有效";
            labMax.Text = "最大行数";
            labName.Text = "名称";
            labType.Text = "类型";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.UnCheckedText = "无效";
            lwchkIsValid.NULLText = "全部";
            this.btnClear.Text = "清空(&L)";
            this.btnSearch.Text = "查询(&R)";
            nbarBase.Caption = "基本信息";

        }
        private void KeyEventHandle(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
        }
        private void OncmbTypeFirstEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DataDictionaryType>> types = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DataDictionaryType>(LocalData.IsEnglish);
            types.RemoveAll(item => item.Value == DataDictionaryType.None);
            this.cmbType.Properties.BeginUpdate();
            cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, null));
            foreach (var item in types)
            {
                cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbType.Properties.EndUpdate();
        }
        void DataDictionarySeachPart_Load(object sender, EventArgs e)
        {
            CommonUtility.SearchPartKeyEnterToSearch(new List<Control> { this.txtCode, this.txtName }, this.btnSearch, this.KeyEventHandle);
            this.cmbType.OnFirstEnter += this.OncmbTypeFirstEnter;
           
        }

        #endregion

        #region ISearchPart 成员

        public object GetData()
        {
            DataDictionaryType? dataDictonaryType = null;
            if (cmbType.EditValue != null && cmbType.EditValue!= System.DBNull.Value)
            {
                dataDictonaryType = (DataDictionaryType)cmbType.EditValue;
            }

           List<DataDictionaryList> list =TransportFoundationService. GetDataDictionaryList(txtCode.Text.Trim(),
                                                                           txtName.Text.Trim(),
                                                                           dataDictonaryType,
                                                                           lwchkIsValid.Checked,
                                                                           int.Parse(numMax.Value.ToString()));
           return list;
        }
        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataReturned;
        public virtual void InitialValues(string searchValue, string property, SearchConditionCollection conditions, FinderTriggerType triggerType)
        {
            btnClear.PerformClick();

            if (triggerType == FinderTriggerType.KeyEnter)
            {
                if (property.Contains(SearchFieldConstants.Code))
                    txtCode.Text = searchValue == null ? string.Empty : searchValue.ToString();
                else
                    txtName.Text = searchValue == null ? string.Empty : searchValue.ToString();
            }
        }


        #endregion

        #region btn
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (DataReturned != null)
            {
                using (new CursorHelper())
                {
                    DataReturned(this, new ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>(GetData()));
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                    && (item is DevExpress.XtraEditors.SpinEdit) == false
                    && (item is DevExpress.XtraEditors.ComboBoxEdit) == false
                    
                    && item.Enabled == true
                    && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }

            lwchkIsValid.Checked = true;
            txtCode.Focus();
        }

        #endregion
    }
}
