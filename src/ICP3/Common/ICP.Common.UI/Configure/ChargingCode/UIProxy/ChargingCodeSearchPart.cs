using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.Configure.ChargingCode
{
    [ToolboxItem(false)]
    public partial class ChargingCodeSearchPart : DevExpress.XtraEditors.XtraUserControl, ICP.Framework.ClientComponents.UIManagement.ISearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        public IConfigureService configureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        public ChargingCodeSearchPart()
        {
            InitializeComponent();
           
            this.Disposed += delegate
            {
                CommonUtility.RemoveSearchPartKeyEnterToSearch(new List<Control> { this.txtName, txtCode }, KeyEventHandle);
                this.popGroup.QueryPopUp -= this.popGroup_QueryPopUp;
                this.treeListGroup.DoubleClick -= this.treeListGroup_DoubleClick;
                this.DataReturned = null;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            popGroup.QueryPopUp += new CancelEventHandler(popGroup_QueryPopUp);

            if (LocalData.IsEnglish)
            {
                colEName.Visible = true;
            }
            else
            {
                SetCnText();
                colCName.Visible = true;
            }

            if (DataReturned != null)
                DataReturned(this, new ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>(GetData()));

            CommonUtility.SearchPartKeyEnterToSearch(new List<Control> { this.txtName, txtCode }, this.btnSearch, KeyEventHandle);
        }
        private void KeyEventHandle(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
        }
        private void SetCnText()
        {
            colCName.Caption = "名称";
            colEName.Caption = "名称";
            labCode.Text = "代码";
            labIsCommission.Text = "佣金";
            labIsValid.Text = "有效";
            labMax.Text = "最大行数";
            labName.Text = "名称";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.UnCheckedText = "无效";
            lwchkIsValid.NULLText = "全部";
            labGroup.Text = "组";

            lwchkIsCommission.CheckedText = "是";
            lwchkIsCommission.UnCheckedText = "否";
            lwchkIsCommission.NULLText = "全部";

            nbarBase.Caption = "基本信息";

            btnClear.Text = "清空(&L)";

            btnSearch.Text = "查询(&R)";
            btnClearPop.Text = "清空";
            btnClosePop.Text = "关闭";
        }


        #region ISearchPart 成员

        public object GetData()
        {
            try
            {
                List<ChargingCodeList> codeList = configureService.GetChargingCodeListBySearch(txtCode.Text.Trim()
                                                                                        , txtName.Text.Trim()
                                                                                        , _groupId
                                                                                        , lwchkIsCommission.Checked
                                                                                        , lwchkIsValid.Checked
                                                                                        , int.Parse(numMax.Value.ToString()));
                return codeList;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                return new List<ChargingCodeList>();
            }
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
                DataReturned(this, new ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>(GetData()));
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

            lwchkIsCommission.Checked = null;
            lwchkIsValid.Checked = true;
            _groupId = null;
            popGroup.Text = "";

            txtCode.Focus();
        }
        #endregion

        #region POP

        Guid? _groupId = null;

        void popGroup_QueryPopUp(object sender, CancelEventArgs e)
        {
            popGroup.QueryPopUp -= new CancelEventHandler(popGroup_QueryPopUp);
            List<ChargingGroupList> groupList = configureService.GetChargingGroupList(string.Empty, string.Empty, null, 0);
            bsChargingGroup.DataSource = groupList;
        }


        ChargingGroupList CurrentChargingGroup { get { return bsChargingGroup.Current as ChargingGroupList; } }

        private void treeListGroup_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentChargingGroup == null) return;

            popGroup.Text = LocalData.IsEnglish ? CurrentChargingGroup.EName : CurrentChargingGroup.CName;
            _groupId = CurrentChargingGroup.ID;
            popGroup.ClosePopup();

        }

        private void btnClearPop_Click(object sender, EventArgs e)
        {
            _groupId = null;
            popGroup.Text = "";
            popGroup.ClosePopup();
        }

        private void btnClosePop_Click(object sender, EventArgs e)
        {
            popGroup.ClosePopup();
        }

        #endregion

    }
}
