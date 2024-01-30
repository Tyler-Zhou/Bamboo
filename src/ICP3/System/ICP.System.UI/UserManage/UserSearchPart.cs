using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
namespace ICP.Sys.UI.UserManage
{
    [ToolboxItem(false)]
    public partial class UserSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        #endregion

        #region Finder

        protected virtual bool? IsValid { get { return lwchkIsValid.Checked; } }

        protected virtual bool IsFinder { get { return false; } }

        Dictionary<string, Control> _propertyControlDic = new Dictionary<string, Control>();

        #endregion

        #region init

        public UserSearchPart()
        {
            InitializeComponent();
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();

            BulidBarItemDictionary();
            this.Disposed += delegate {
                Utility.RemoveSetControlKeyEnterToClickButton(new List<Control> { this.txtCode, this.txtName }, this.OnKeyDownHandle);
                this._propertyControlDic = null;
                this.OnSearched = null;
                if (this.organizationFinder != null)
                {
                    this.organizationFinder.Dispose();
                    this.organizationFinder = null;
                }
                if (this.jobFinder != null)
                {
                    this.jobFinder.Dispose();
                    this.jobFinder = null;
                }
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        private void BulidBarItemDictionary()
        {
            _propertyControlDic.Add("Code", txtCode);
            _propertyControlDic.Add("Name", txtName);
        }

        private void SetCnText()
        {
            labName.Text = "名称";
            labCode.Text = "代码";
            labCompany.Text = "公司";
            labIsValid.Text = "状态";
            labMax.Text = "最大行数";
            labSex.Text = "性别";
            labJob.Text = "职位";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.NULLText = "全部";
            lwchkIsValid.UnCheckedText = "无效";

            nbarBase.Caption = "基本信息";

            this.btnClear.Text = "清空(&L)";
            this.btnSearch.Text = "查询(&R)";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<GenderType>> sexTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<GenderType>(LocalData.IsEnglish);
            this.cmbSex.Properties.BeginUpdate();
            cmbSex.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", System.DBNull.Value));
            foreach (var item in sexTypes)
            {
                cmbSex.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbSex.Properties.EndUpdate();
            cmbSex.SelectedIndex = 0;

            SearchRegister();

            txtName.Focus();

            if (IsFinder)
            {
                this.lwchkIsValid.Checked = IsValid;
                //this.lwchkIsValid.Enabled = false;
            }

            if (IsValid == null && OnSearched != null) OnSearched(this, GetData());

            Utility.SetControlKeyEnterToClickButton(new List<Control> { this.txtCode, this.txtName }, this.btnSearch,this.OnKeyDownHandle);
        }
        private void OnKeyDownHandle(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                this.btnSearch.PerformClick();
            }

        }
        private IDisposable organizationFinder, jobFinder;
        void SearchRegister()
        {

          organizationFinder=  DataFindClientService.RegisterMiniFinder(stxtOrganization, SystemFinderConstants.OrganizationFinder, SearchFieldConstants.CodeName
                                      , LocalData.IsEnglish ? "EShortName" : "CShortName", "ID", new string[] { "ID", "EShortName","CShortName" },
                            delegate(object inputSource, object[] resultData)
                            {
                                stxtOrganization.Text = LocalData.IsEnglish ? resultData[1].ToString() : resultData[2].ToString();
                                stxtOrganization.Tag = new Guid(resultData[0].ToString());
                            }, null);


          jobFinder=  DataFindClientService.RegisterMiniFinder(stxtJob, SystemFinderConstants.JobFinder, SearchFieldConstants.CodeName
                                        , LocalData.IsEnglish ? "EName" : "CName", "ID", new string[] { "ID", "EName", "CName" },
                              delegate(object inputSource, object[] resultData)
                              {
                                  stxtJob.Text = LocalData.IsEnglish ? resultData[1].ToString() : resultData[2].ToString();
                                  stxtJob.Tag = new Guid(resultData[0].ToString());
                              }, null);

        }

        #endregion

        #region ISearchPart成员

        public override object GetData()
        {
            GenderType? sexType = null;
            if (cmbSex.EditValue != System.DBNull.Value)
            {
                sexType = (GenderType)Enum.Parse(typeof(GenderType), cmbSex.EditValue.ToString());
            }

            Guid? organizationId = null;
            if (stxtOrganization.Tag != null)
                organizationId = new Guid(stxtOrganization.Tag.ToString());

            Guid? jobId = null;
            if (stxtJob.Tag != null)
                jobId = new Guid(stxtJob.Tag.ToString());

            List<UserList> data = UserService.GetUserListByList(txtCode.Text.Trim(), txtName.Text.Trim(),
                                                             sexType,
                                                             jobId,
                                                             null,
                                                             organizationId,
                                                             null,
                                                             this.IsValid,
                                                            int.Parse(numMax.Value.ToString()));
            return data;
        }
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void Init(IDictionary<string, object> values)
        {
            this.btnClear.PerformClick();

            foreach (var item in values)
            {
                if (_propertyControlDic.Keys.Contains(item.Key) && _propertyControlDic[item.Key] != null)
                {
                    _propertyControlDic[item.Key].Text = item.Value.ToString();
                }
            }
        }
        public void Init(string userCode)
        {
            if (!string.IsNullOrEmpty(userCode))
            {
                this.txtCode.Text = userCode;
                this.txtCode.Properties.ReadOnly = true;
            }
        }

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

        #region btn

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


            //默认设置全部
            lwchkIsValid.Checked = true;
            cmbSex.SelectedIndex = 0;
            stxtJob.Tag = null;
            stxtOrganization.Tag = null;
            stxtOrganization.Text = stxtJob.Text = string.Empty;

            stxtOrganization.Tag = null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    OnSearched(this, GetData());
                }
            }
        }

        #endregion
    }

    public class UserFinderSearchPart : UserSearchPart
    {
        //protected override bool? IsValid { get { return true; } }

        protected override bool IsFinder { get { return true; } }
    }
}
