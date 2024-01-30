using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Common.UI;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICP.FCM.OtherBusiness.UI.Common
{
    /// <summary>
    /// 查询面板
    /// </summary>
    [ToolboxItem(false)]
    public partial class OBSearchPart : BaseSearchPart
    {
        #region 字段 & 属性 & 委托
        /// <summary>
        /// 
        /// </summary>
        public virtual bool OperationNoEnabled
        {
            get { return true; }
        }

        /// <summary>
        /// 选择默认操作口岸
        /// </summary>
        public virtual bool CheckedDefaultCompany { get; set; }
        /// <summary>
        /// 可用操作口岸列表
        /// </summary>
        public virtual List<OrganizationEntry> CompanyList
        {
            get;
            set;
        }
        /// <summary>
        /// 已选口岸
        /// </summary>
        public List<Guid> CompanyIDs
        {
            get
            {
                return (from CheckedListBoxItem item in chkcmbCompany.Properties.Items where item.CheckState == CheckState.Checked select new Guid(item.Value.ToString())).ToList();
            }
        }
        /// <summary>
        /// 揽货人
        /// </summary>
        public virtual Guid? SalesID
        {
            get
            {
                if (mcmbSales.EditValue == null) return null;
                return new Guid(mcmbSales.EditValue.ToString());
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        public virtual int[] OTOperationType
        {
            get { return new[] {1, 2, 3, 4, 5, 6}; }
        }

       
        /// <summary>
        /// 海外部客服
        /// </summary>
        public Guid? OverseasFilerID
        {
            get
            {
                if (mcmbOverseasFiler.EditValue == null) return null;
                else if ((new Guid(mcmbOverseasFiler.EditValue.ToString())) == Guid.Empty)
                {
                    return null;
                }

                return new Guid(mcmbOverseasFiler.EditValue.ToString());
            }
        }
        /// <summary>
        /// 查询事件
        /// </summary>
        public override event SearchResultHandler OnSearched;
        #endregion

        #region 服务注入
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IOtherBusinessService OtherBusinessService
        {
            get
            {
                return ServiceClient.GetService<IOtherBusinessService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public OtherUIHelper ExportUIHelper
        {
            get
            {
                return ClientHelper.Get<OtherUIHelper, OtherUIHelper>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 查询面板
        /// </summary>
        public OBSearchPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();
            dmcDataLimit.IsEngish = LocalData.IsEnglish;
            Disposed += delegate
            {
                OnSearched = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        } 
        #endregion

        #region 事件
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {

                InitControls();
                SetKeyDownToSearch();
            }
        }
        void mcmbSales_SelectedRow(object sender, EventArgs e)
        {
            Workitem.State["SalesId"] = SalesID;
        }
        /// <summary>
        /// 日期搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDateSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dmcDataLimit.Enabled = cmbDateSearchType.EditValue.ToString() != DateSearchType.All.ToString();
        }
        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2) btnSearch.PerformClick();
            else if (e.KeyCode == Keys.F3) btnClear.PerformClick();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (OnSearched != null)
                    OnSearched(this, GetData());
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is LWImageComboBoxEdit)
                {
                    (item as LWImageComboBoxEdit).SelectedIndex = 0;
                }
                else if (item is MultiSearchCommonBox)
                {
                    MultiSearchCommonBox msc = (MultiSearchCommonBox)item;
                    msc.ShowSelectedValue(null, string.Empty);
                }
                else if (item is TextEdit
                    && (item is SpinEdit) == false
                    && item.Enabled == true
                    && (item as TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;

            }

            cmbState.SelectedIndex = 0;
            cmbDateSearchType.SelectedIndex = 0;
        } 
        #endregion

        #region 方法
        /// <summary>
        /// 重写初始化
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }

            txtOperationNo.Text = string.Empty;
            stxtCustomer.Text = string.Empty;
            stxtPOL.Text = string.Empty;
            stxtPOD.Text = string.Empty;
            stxtDestination.Text = string.Empty;

            foreach (var item in values)
            {
                string value = item.Value == null ? string.Empty : item.Value.ToString();
                switch (item.Key)
                {
                    case "RefNo":
                        txtOperationNo.Text = value;
                        break;
                    case "CustomerName":
                        stxtCustomer.Text = value;
                        break;
                    case "POLName":
                        stxtPOL.Text = value;
                        break;
                    case "PODName":
                        stxtPOD.Text = value;
                        break;
                    case "PlaceOfDeliveryName":
                        stxtDestination.Text = value;
                        break;
                }
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            txtOperationNo.Enabled = OperationNoEnabled;

            List<EnumHelper.ListItem<OBOrderState>> orderStates = EnumHelper.GetEnumValues<OBOrderState>(LocalData.IsEnglish);
            cmbState.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", DBNull.Value));
            foreach (var item in orderStates)
            {
                if (item.Value == OBOrderState.Unknown) continue;
                cmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbState.SelectedIndex = 0;

            SetCompany();

            //揽货人
            Utility.SetEnterToExecuteOnec(mcmbSales, delegate
            {
                ExportUIHelper.SetMcmbUsers(mcmbSales, true, true);
            });

            //海外部客服
            Utility.SetEnterToExecuteOnec(mcmbOverseasFiler, delegate
            {
                List<Guid> companyIDs = tcbSalesDepartment.EditValue;
                List<UserList> userList = UserService.GetUnderlingUserList(companyIDs.ToArray(), new string[] { "海外拓展", "客服" }, null, true);

                UserList insertItem = new UserList();
                insertItem.ID = Guid.Empty;
                insertItem.EName = insertItem.CName = string.Empty;
                userList.Insert(0, insertItem);

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                mcmbOverseasFiler.InitSource(userList, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            });

            mcmbSales.SelectedRow += mcmbSales_SelectedRow;

            SetSalesInfo();


            //日期类型
            List<EnumHelper.ListItem<DateSearchType>> dateSearchTypes = EnumHelper.GetEnumValues<DateSearchType>(LocalData.IsEnglish);
            foreach (var item in dateSearchTypes)
            {
                if (item.Value == DateSearchType.All) continue;
                cmbDateSearchType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDateSearchType.SelectedIndex = 0;
            cmbDateSearchType.SelectedIndexChanged += cmbDateSearchType_SelectedIndexChanged;

            SetControlsEnterToSearch();

        }

        /// <summary>
        /// 
        /// </summary>
        private void SetCompany()
        {
            chkcmbCompany.Properties.BeginUpdate();
            chkcmbCompany.Properties.Items.Clear();
            foreach (var item in CompanyList)
            {
                if (item.ID == LocalData.UserInfo.DefaultCompanyID)
                {
                    chkcmbCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EName : item.CName,
                        CheckedDefaultCompany ? CheckState.Checked : CheckState.Unchecked, true);
                }
                else
                {
                    chkcmbCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EName : item.CName,
                                                       CheckState.Unchecked, true);
                }
            }

            chkcmbCompany.Properties.EndUpdate();
        }
        
        /// <summary>
        /// 设置揽货人相关信息
        /// </summary>
        public virtual void SetSalesInfo()
        {
            //if (addType == AddType.OtherBusinessOrder)
            //{

            //    if (LocalCommonServices.PermissionService.HaveActionPermission(FCMPermissionsConstants.FCM_NAServices))
            //    {
            //        ICPCommUIHelper.BindDepartmentByAll(tcbSalesDepartment);
            //    }
            //    else
            //    {
            //        ICPCommUIHelper.BindCompanyByUser(tcbSalesDepartment, CheckState.Checked);
            //        mcmbSales.ShowSelectedValue(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName);
            //    }

            //}
            //else
            //{
            //    //业务的，要绑定全部的部门
            //    ICPCommUIHelper.BindDepartmentByAll(tcbSalesDepartment);

            //}
        }

        /// <summary>
        /// 设置CN
        /// </summary>
        private void SetCnText()
        {
            lblCompany.Text = "揽货部门";
            lblShipper.Text = "发货人";
            lblConsignee.Text = "收货人";
            lblHawb.Text = "HBL";
            lblMAWB.Text = "MBL";
            lblNotify.Text = "通知人";
            lblVessel.Text = "船名";
            lblVoyage.Text = "航次";
            labCustomer.Text = "客户";
            labDestination.Text = "交货地";
            labelContainer.Text = "箱号";
            labFrom.Text = "从";
            labIsValid.Text = "是否有效";
            labMax.Text = "最大记录数";
            labOperationNo.Text = "业务号";
            labPOL.Text = "装货港";
            labPOD.Text = "卸货港";
            labSales.Text = "揽货人";
            labOverseasFiler.Text = "海外部客服";
            labState.Text = "状态";
            labTo.Text = "到";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.NULLText = "全部";
            lwchkIsValid.UnCheckedText = "无效";
        }
        /// <summary>
        /// 设置热键
        /// </summary>
        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        /// <summary>
        /// 设置查询热键
        /// </summary>
        private void SetControlsEnterToSearch()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is TextEdit
                && (item is SpinEdit) == false)
                {
                    item.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
                    };
                }
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            DateTime? dtFrom = null, dtTo = null;
            DateSearchType dateSearchType = DateSearchType.All;
            OBOrderState? orderState = OBOrderState.Unknown;

            if (cmbState.EditValue != null && cmbState.EditValue != DBNull.Value)
            {
                orderState = (OBOrderState)cmbState.EditValue;

            }

            if (nbarDate.Expanded && cmbDateSearchType.EditValue != null && cmbDateSearchType.EditValue.ToString() != DateSearchType.All.ToString())
            {
                dateSearchType = (DateSearchType)cmbDateSearchType.EditValue;
                dtFrom = dmcDataLimit.From;
                dtTo = dmcDataLimit.To;
            }

            List<Guid> salesDepIDs = tcbSalesDepartment.EditValue;

            if (tcbSalesDepartment.EditValue.Count == 0)
            {
                salesDepIDs = tcbSalesDepartment.GetAllAvailableValues();
            }



            try
            {
                List<OtherBusinessList> list = OtherBusinessService.GetOtherBusinessList(CompanyIDs.ToArray(),
                                                                             salesDepIDs.ToArray(),
                                                                            OTOperationType,
                                                                            txtOperationNo.Text.Trim(),
                                                                            txtMBL.Text.Trim(),
                                                                            txtHBL.Text.Trim(),
                                                                            txtContainerNo.Text.Trim(),
                                                                            txtConsign.Text.Trim(),
                                                                            txtShipper.Text.Trim(),
                                                                            txtNotify.Text.Trim(),
                                                                            stxtCustomer.Text.Trim(),
                                                                            stxtPOL.Text.Trim(),
                                                                            stxtPOD.Text.Trim(),
                                                                            stxtDestination.Text.Trim(),
                                                                            txtVessel.Text.Trim(),
                                                                            txtVoyage.Text.Trim(),
                                                                            lwchkIsValid.Checked,
                                                                            orderState,
                                                                            SalesID,
                                                                            OverseasFilerID,
                                                                            dateSearchType,
                                                                            dmcDataLimit.From,
                                                                            dmcDataLimit.To,
                                                                            int.Parse(numMax.Value.ToString()));
                return list;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return null;
            }
        } 
        #endregion
    }
}
