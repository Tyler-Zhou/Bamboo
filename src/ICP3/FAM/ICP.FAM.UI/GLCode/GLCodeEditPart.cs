using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Common;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FAM.UI
{
    public partial class GLCodeEditPart : BaseEditPart
    {
        public GLCodeEditPart()
        {
            InitializeComponent();
            Disposed += delegate {
                Saved = null;
                cmbForeignCurrencyID.OnFirstEnter -= OnCmbForeignCurrenyEnter;
                cmbTreeParentID.OnFirstEnter -= OnCmbTreeParentIDEnter;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }
  

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }

        #endregion

        #region 属性
        //private Guid YDTopID = new Guid("478A6CA2-07BD-4A38-ACB9-8B2982F8E0C8");
        //private Guid USATopID = new Guid("057514D0-FAC4-4E23-9375-77C5ED1B5B03");
        //private Guid CANTopID = new Guid("8EA3E43C-0141-48A6-8FA5-BA0DBDF22DAE");

        /// <summary>
        /// 公司ID集合
        /// </summary>
        public List<Guid> CompanyIDs
        {
            get;
            set;
        }

        #endregion

        #region  窗体事件
        bool isLoad = false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                /// 初始化控件 
                InitControls();
                /// 延迟加载
                SetDelayLoadData();

                isLoad = true;

                DataInfo.IsDirty = false;
                DataInfo.BeginEdit();

               // this.SmartPartClosing += new EventHandler<Microsoft.Practices.CompositeUI.SmartParts.WorkspaceCancelEventArgs>(GLCodeEditPart_SmartPartClosing);
                ActivateSmartPartClosingEvent(Workitem);
            }
        }

        private void InitControls()
        {
            List<EnumHelper.ListItem<GLCodeType>> glCodeType = EnumHelper.GetEnumValues<GLCodeType>(LocalData.IsEnglish);
            foreach (var item in glCodeType)
            {
                if (item.Value != GLCodeType.Unknown)
                {
                    cmbGlCodeType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
            }
            cmbGlCodeType.Properties.EndUpdate();
            cmbGlCodeType.SelectedIndex = 0;


            List<EnumHelper.ListItem<GLCodeLedgerStyle>> amountType = EnumHelper.GetEnumValues<GLCodeLedgerStyle>(LocalData.IsEnglish);
            foreach (var item in amountType)
            {
                if (item.Value != GLCodeLedgerStyle.Unknown)
                {
                    cmbCodeLedgerStyle.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
            }
            cmbCodeLedgerStyle.Properties.EndUpdate();
            cmbCodeLedgerStyle.SelectedIndex = 0;

            cmbForeignCurrencyID.ShowSelectedValue(DataInfo.ForeignCurrencyID, DataInfo.ForeignCurrencyName);
            cmbTreeParentID.EditValue = DataInfo.ParentID;
            cmbTreeParentID.Text = DataInfo.ParentName;

            if (DataInfo.GLCodeProperty == GLCodeProperty.Credit)
            {
                rgpGLCodeProperty.SelectedIndex = 1;
            }
            else
            {
                rgpGLCodeProperty.SelectedIndex = 0;
            }
            List<OrganizationList> orgList = OrganizationService.GetOfficeList();
            foreach (var item in orgList)
            {
                icmbCompany.Properties.Items.Add(new ImageComboBoxItem(item.FullName, item.ID));
            }

            if (!LocalCommonServices.PermissionService.HaveActionPermission("COMMON_EDITGLCode"))
            {
                barSave.Enabled = false;
            }
        }
        private void OnCmbForeignCurrenyEnter(object sender, EventArgs e)
        {
            List<SolutionCurrencyList> currencyList = ConfigureService.GetSolutionCurrencyList(SolutionID, true);
            cmbForeignCurrencyID.Properties.BeginUpdate();
            cmbForeignCurrencyID.Properties.Items.Clear();
            cmbForeignCurrencyID.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));
            foreach (SolutionCurrencyList item in currencyList)
            {
                //取消本位币不显示，因为登陆人是深圳公司的，但越南公司可以增加RMB的外币
                //if (item.CurrencyID != ConfigureInfo.StandardCurrencyID)
                //{
                cmbForeignCurrencyID.Properties.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.CurrencyID));
                //}
            }
            cmbForeignCurrencyID.Properties.EndUpdate();
        }
        private void OnCmbTreeParentIDEnter(object sender, EventArgs e)
        {
            List<SolutionGLCodeList> glCodeList;
            if(CompanyIDs != null)
            {
                glCodeList = ConfigureService.GetSolutionGLCodeListNew(SolutionID, CompanyIDs.ToArray(), string.Empty, string.Empty, GLCodeType.Unknown, true, LocalData.IsEnglish);
            }
            else
            {
                glCodeList = ConfigureService.GetSolutionGLCodeListNew(SolutionID, new Guid[0], string.Empty, string.Empty, GLCodeType.Unknown, true, LocalData.IsEnglish);
            }


            cmbTreeParentID.AllowMultSelect = false;
            cmbTreeParentID.RootValue = Guid.Empty;
            cmbTreeParentID.ParentMember = "ParentID";
            cmbTreeParentID.ValueMember = "ID";
            cmbTreeParentID.DisplayMember = LocalData.IsEnglish ? "EName" : "CName";
            cmbTreeParentID.DataSource = glCodeList;
                  
        }
        /// <summary>
        /// 延迟加载数据
        /// </summary>
        private void SetDelayLoadData()
        {
            
            //加载币种列表
            cmbForeignCurrencyID.OnFirstEnter += OnCmbForeignCurrenyEnter;

            //加载会计科目列表
            cmbTreeParentID.OnFirstEnter += OnCmbTreeParentIDEnter;
           
                
        }


        private void ckbIsForeignCheck_CheckedChanged(object sender, EventArgs e)
        {
            pnlIsForeignCheck.Enabled = ckbIsForeignCheck.Checked;
        }

        private void chkIsNumberCheck_CheckedChanged(object sender, EventArgs e)
        {
            pnlIsNumberCheck.Enabled = chkIsNumberCheck.Checked;
        }
        private void cmbGlCodeType_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoad)
            {
                return;
            }
            GLCodeType type = ((GLCodeType)cmbGlCodeType.EditValue);

            if (type == GLCodeType.ASSETS || type == GLCodeType.COST || type == GLCodeType.EQUITY)
            {
                rgpGLCodeProperty.SelectedIndex = 0;
            }
            else
            {
                rgpGLCodeProperty.SelectedIndex = 1;
            }
        }

        private void GetGLCodeProperty()
        {
            GLCodeProperty property= (GLCodeProperty)(rgpGLCodeProperty.SelectedIndex + 1);
            if (DataInfo.GLCodeProperty != property)
            {
                DataInfo.GLCodeProperty = property;
            }
        }

        #endregion

        #region  属性
        /// <summary>
        /// 解决方案ID
        /// </summary>
        public Guid SolutionID
        {
            get;
            set;
        }
        /// <summary>
        /// 配置信息
        /// </summary>
        public ConfigureInfo ConfigureInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 数据信息
        /// </summary>
        public SolutionGLCodeList DataInfo
        {
            get
            {
                return bsList.DataSource as SolutionGLCodeList;
            }
        }
        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                SolutionGLCodeList data = value as SolutionGLCodeList;
                if (data == null)
                {
                    data = new SolutionGLCodeList();
                }
                SolutionID = data.SolutionID;
                bsList.DataSource = data;
                bsList.ResetBindings(false);

            }
        }

        #endregion

        #region 保存
        public override event SavedHandler Saved;
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (SaveData())
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                }
            }
        }
        private bool SaveData()
        {
            bsList.EndEdit();

            GetGLCodeProperty();

            if (!DataInfo.Validate())
            {
                return false;
            }

            try
            {
                SingleResult result = ConfigureService.SaveSolutionGLCodeInfoNew(SolutionID,
                                                            DataInfo.ID,
                                                            DataInfo.Code,
                                                            DataInfo.CName,
                                                            DataInfo.EName,
                                                            DataInfo.GLCodeType,
                                                            DataInfo.LedgerStyle,
                                                            DataInfo.GLCodeProperty,
                                                            DataInfo.IsForeignCheck,
                                                            DataInfo.ForeignCurrencyID,
                                                            DataInfo.IsNumberCheck,
                                                            DataInfo.UnitName,
                                                            DataInfo.IsDepartmentCheck,
                                                            DataInfo.IsPersonalCheck,
                                                            DataInfo.IsCustomerCheck,
                                                            DataInfo.IsJournal,
                                                            DataInfo.IsBankAccount,
                                                            DataInfo.IsFee,
                                                            DataInfo.ParentID,
                                                            DataInfo.Description,
                                                            LocalData.UserInfo.LoginID,
                                                            DataInfo.UpdateDate,
                                                            DataInfo.CompanyID);

                DataInfo.ID = result.GetValue<Guid>("ID");
                DataInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                DataInfo.HierarchyCode = result.GetValue<String>("HierarchyCode");
                DataInfo.LevelCode = result.GetValue<Int32?>("LevelCode");

                DataInfo.IsDirty = false;
                DataInfo.BeginEdit();

                bsList.ResetBindings(false);

                if (Saved != null)
                {
                    if (ckbIsForeignCheck.Checked)
                    {
                        DataInfo.ForeignCurrencyName = cmbForeignCurrencyID.Text;
                    }
                    else
                    {
                        DataInfo.ForeignCurrencyName = string.Empty;
                    }

                    Saved(new object[] { DataInfo });
                }

                if (!chkIsPub.Checked)
                {
                    if (CompanyIDs != null && CompanyIDs.Count > 0)
                    {
                        Guid[] ids = new Guid[CompanyIDs.Count];
                        string[] codes = new string[CompanyIDs.Count];

                        for (int i = 0; i < CompanyIDs.Count; i++)
                        {
                            ids[i] = Guid.NewGuid();
                            codes[i] = DataInfo.Code;
                        }

                        ConfigureService.SaveGL2Company(ids, DataInfo.ID, CompanyIDs.ToArray(), codes, LocalData.UserInfo.LoginID);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                return false;
            }     
        }
        #endregion

        #region  关闭
        void GLCodeEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (barSave.Enabled&&DataInfo.IsDirty)
            {
                e.Cancel = !Leaving();
            }
        }
        public bool Leaving()
        {
            if (DataInfo.IsDirty)
            {
                if (FAMUtility.EnquireIsSaveCurrentDataByUpdated() == DialogResult.Yes)
                {
                    return SaveData();
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }
        #endregion




    }
}
