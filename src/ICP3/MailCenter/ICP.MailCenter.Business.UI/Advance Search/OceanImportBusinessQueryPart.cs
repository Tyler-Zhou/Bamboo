using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface;
using ICP.Common.UI;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using ICP.Operation.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.TaskCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Constants = ICP.Operation.Common.ServiceInterface.Constants;

namespace ICP.MailCenter.Business.UI
{
    /// <summary>
    /// 海出高级查询用户控件
    /// </summary>
    public partial class OceanImportBusinessQueryPart : XtraUserControl, IBusinessQueryPart
    {
        #region 服务注入
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 用户服务
        /// </summary>
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        /// <summary>
        /// ICP Common界面辅助类
        /// </summary>
        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        /// <summary>
        /// 操作视图服务
        /// </summary>
        public IOperationViewService OperationViewService
        {
            get
            {
                return ServiceClient.GetService<IOperationViewService>();
            }
        }
        #endregion

        #region Fields
        /// <summary>
        /// Xml的文件名称
        /// </summary>
        private const string TempalteFileName = "OIQueryConditions.xml";
        #endregion

        #region Property
        /// <summary>
        /// 用户所挂公司列表
        /// </summary>
        List<OrganizationList> _UserCompanyList = null;

        /// <summary>
        /// 客服数据源
        /// </summary>
        List<UserList> _SOByUsers = null;

        /// <summary>
        /// 海外客服数据源
        /// </summary>
        List<UserList> _OverSeasCSusers = null;

        /// <summary>
        /// 文件员数据源
        /// </summary>
        List<UserList> _DocByusers = null;

        /// <summary>
        /// 揽货人数据源
        /// </summary>
        List<UserList> _SalesUsers = null;
        /// <summary>
        /// 查询所对应的模板代码
        /// </summary>
        public string OperationViewCode
        {
            get;
            set;
        }

        /// <summary>
        /// 查询所对应的公司IDs
        /// </summary>
        public string SelectedCompanyIDs
        {
            get;
            set;
        }

        /// <summary>
        /// 操作口岸ID
        /// </summary>
        public Guid CompanyID
        {
            get
            {
                if (cmbCompany.EditValue == null || (Guid)cmbCompany.EditValue == Guid.Empty)
                {
                    return LocalData.UserInfo.DefaultCompanyID;
                }
                else
                {
                    return (Guid)cmbCompany.EditValue;
                }
            }
        }
        /// <summary>
        /// 业务类型
        /// </summary>
        private BusinessType type = BusinessType.OI;
        /// <summary>
        /// 业务类型
        /// </summary>
        /// <remarks>海运进口</remarks>
        [DefaultValue(BusinessType.OI)]
        public BusinessType Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// 历史查询条件
        /// </summary>
        public string Stroriginal
        {
            get;
            set;
        }

        #region 获取控件值

        /// <summary>
        /// 揽货人ID
        /// </summary>
        /// 
        public Guid? SalesID
        {
            get
            {
                if (cmbSales.EditValue == null) return null;
                return new Guid(cmbSales.EditValue.ToString());
            }
        }
        /// <summary>
        /// 文件员Id
        /// </summary>
        public Guid? DocID
        {
            get
            {
                if (cmbFiler.EditValue == null) return null;
                return new Guid(cmbFiler.EditValue.ToString());
            }
        }
        #endregion
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public OceanImportBusinessQueryPart()
        {
            InitializeComponent();

            if (LocalData.IsDesignMode) return;

            Disposed += delegate
            {

                cmbDate.OnFirstEnter -= OncmbDateFirstEnter;
                cmbFiler.OnFirstEnter -= OncmbFilerFirstEnter;
                //this.cmbprogram.SelectedIndexChanged -= this.cmbProgram_SelectedIndexChanged;
                cmbprogram.OnFirstEnter -= OncmbprogramFirstEnter;
                cmbSales.Enter -= cmbSales_Enter;
                cmbState.OnFirstEnter -= OncmbStateFirstEnter;

                if (Workitem != null)
                {

                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        } 
        #endregion

        #region Form Event
        /// <summary>
        /// 重写加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        /// <summary>
        /// 方案变更
        /// </summary>
        private void cmbprogram_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReleaseCargoProgram program = (ReleaseCargoProgram)cmbprogram.SelectedIndex;
            switch (program)
            {
                case ReleaseCargoProgram.Custom:
                    SetCustomStyle();
                    break;
                case ReleaseCargoProgram.NotApliedReleaseCargo:
                    SetNotApliedReleaseCargo();
                    break;
                case ReleaseCargoProgram.AppliedReleaseCargo:
                    SetAppliedReleaseCargo();
                    break;
                case ReleaseCargoProgram.NotReleasedBL:
                    SetNotReleasedBL();
                    break;
                case ReleaseCargoProgram.ReleasedBL:
                    SetReleasedBL();
                    break;
                case ReleaseCargoProgram.AcceptedReleaseBL:
                    SetAcceptedReleaseBL();
                    break;
                case ReleaseCargoProgram.ReleasedCargo:
                    SetReleasedCargo();
                    break;
                default:
                    break;
            }
        }

        #region 方案发生改变时

        /// <summary>
        /// 设置自定义方案的样式
        /// </summary>
        private void SetCustomStyle()
        {
            rdoApply.Enabled = true;
            rdoApply.SelectedIndex = 0;

            rdoReceive.Enabled = true;
            rdoReceive.SelectedIndex = 0;

            rdoReleaseRC.Enabled = true;
            rdoReleaseRC.SelectedIndex = 0;

            rdoRelease.Enabled = true;
            rdoRelease.SelectedIndex = 0;

            rdoIsUDN.Enabled = true;
            rdoIsUDN.SelectedIndex = 0;

            rdoIsACCLOS.Enabled = true;
            rdoIsACCLOS.SelectedIndex = 0;

            rdoIsURB.Enabled = true;
            rdoIsURB.SelectedIndex = 0;

            rdoARC.Enabled = true;
            rdoARC.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置未申请放货方案的样式
        /// </summary>
        private void SetNotApliedReleaseCargo()
        {
            rdoApply.Enabled = false;
            rdoApply.SelectedIndex = 2;

            rdoReceive.Enabled = true;
            rdoReceive.SelectedIndex = 0;

            rdoReleaseRC.Enabled = true;
            rdoReleaseRC.SelectedIndex = 0;

            rdoRelease.Enabled = true;
            rdoRelease.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置已申请放货方案的样式
        /// </summary>
        private void SetAppliedReleaseCargo()
        {
            rdoApply.Enabled = false;
            rdoApply.SelectedIndex = 1;

            rdoReceive.Enabled = true;
            rdoReceive.SelectedIndex = 0;

            rdoReleaseRC.Enabled = true;
            rdoReleaseRC.SelectedIndex = 0;

            rdoRelease.Enabled = false;
            rdoRelease.SelectedIndex = 1;
        }
        /// <summary>
        /// 设置未下达放单指令方案的样式
        /// </summary>
        private void SetNotReleasedBL()
        {
            rdoApply.Enabled = false;
            rdoApply.SelectedIndex = 0;

            rdoReceive.Enabled = false;
            rdoReceive.SelectedIndex = 0;

            rdoReleaseRC.Enabled = false;
            rdoReleaseRC.SelectedIndex = 0;

            rdoRelease.Enabled = false;
            rdoRelease.SelectedIndex = 2;
        }
        /// <summary>
        /// 设置已下达放单指令方案的样式
        /// </summary>
        private void SetReleasedBL()
        {
            rdoApply.Enabled = true;
            rdoApply.SelectedIndex = 0;

            rdoReceive.Enabled = true;
            rdoReceive.SelectedIndex = 2;

            rdoReleaseRC.Enabled = true;
            rdoReleaseRC.SelectedIndex = 0;

            rdoRelease.Enabled = false;
            rdoRelease.SelectedIndex = 1;
        }
        /// <summary>
        /// 设置已签收放单指令方案的样式
        /// </summary>
        private void SetAcceptedReleaseBL()
        {
            rdoApply.Enabled = true;
            rdoApply.SelectedIndex = 0;

            rdoReceive.Enabled = false;
            rdoReceive.SelectedIndex = 1;

            rdoReleaseRC.Enabled = true;
            rdoReleaseRC.SelectedIndex = 0;

            rdoRelease.Enabled = false;
            rdoRelease.SelectedIndex = 1;
        }
        /// <summary>
        /// 设置已放货样式
        /// </summary>
        private void SetReleasedCargo()
        {
            rdoApply.Enabled = true;
            rdoApply.SelectedIndex = 0;

            rdoReceive.Enabled = true;
            rdoReceive.SelectedIndex = 0;

            rdoReleaseRC.Enabled = false;
            rdoReleaseRC.SelectedIndex = 1;

            rdoRelease.Enabled = false;
            rdoRelease.SelectedIndex = 1;
        }

        #endregion

        /// <summary>
        /// 操作口岸更改
        /// </summary>
        /// <remarks>操作口岸更改时重新绑定文件员、客服数据源</remarks>
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindFiler();
            BindCustomerService();
        }
        /// <summary>
        /// 加载客服
        /// </summary>
        private void OncmbCustomerServiceFirstEnter(object sender, EventArgs e)
        {
            BindCustomerService();
        }
        /// <summary>
        /// 加载文件
        /// </summary>
        private void OncmbFilerFirstEnter(object sender, EventArgs e)
        {
            BindFiler();
        }
        /// <summary>
        /// 加载订单状态
        /// </summary>
        private void OncmbStateFirstEnter(object sender, EventArgs e)
        {
            BindState();
        }
        /// <summary>
        /// 加载解决方案
        /// </summary>
        private void OncmbprogramFirstEnter(object sender, EventArgs e)
        {
            BindProgram();
        }
        /// <summary>
        /// 加载进口业务管理日期搜索类型
        /// </summary>
        private void OncmbDateFirstEnter(object sender, EventArgs e)
        {
            BindDate();
        }
        /// <summary>
        /// 加载操作口岸
        /// </summary>
        private void OncmbCompanyFirstEnter(object sender, EventArgs e)
        {
            List<LocalOrganizationInfo> userCompanyList = LocalData.UserInfo.UserOrganizationList.FindAll(item => item.Type == LocalOrganizationType.Company);
            cmbCompany.Properties.BeginUpdate();
            cmbCompany.Properties.Items.Clear();
            cmbCompany.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));

            foreach (var item in userCompanyList)
            {
                cmbCompany.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EShortName : item.CShortName, item.ID));
            }

            cmbCompany.Properties.EndUpdate();
        }

        /// <summary>
        /// 加载揽货人
        /// </summary>
        void cmbSales_Enter(object sender, EventArgs e)
        {
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = (Guid)cmbCompany.EditValue;
            }
            ICPCommUIHelper.SetComboxUsers(cmbSales, depID, string.Empty, string.Empty, true);
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="initValues"></param>
        public void Init(Dictionary<string, object> initValues)
        {
            OperationViewCode = initValues[Constants.OperationViewCodeKey].ToString();

            if (initValues.ContainsKey(Constants.SelectedCompnayKey))
            {
                SelectedCompanyIDs = initValues[Constants.SelectedCompnayKey].ToString();
            }

        }
        
        /// <summary>
        /// 获取当前用户的上一次的查询条件
        /// </summary>
        /// <returns></returns>
        public string SetQueryConditions()
        {
            string stroriginal = string.Empty;
            var templateFilePath = ICPPathUtility.CombineDirectory4FileName(ICPPathUtility.BusinessTemplates, TempalteFileName);
           if(!File.Exists(templateFilePath))  return stroriginal;

            var document = XDocument.Load(templateFilePath);
            //根据当前的代码读取对应的XML段落
            var xmldocment = from ent in document.Descendants("Item") select ent;
            if (xmldocment.Any())
            {
                foreach (var xElement in xmldocment)
                {
                    Guid Userid = new Guid(xElement.Attribute("UserId").Value);
                    if (Userid == LocalData.UserInfo.LoginID)
                    {
                        stroriginal = xElement.Attribute("Stroriginal").Value;
                    }
                }
            }
            return stroriginal;
        }

        /// <summary>
        /// 初始化上一次查询结果集合的赋值操作
        /// </summary>
        /// <param name="initValues"></param>
        public void LastAdvanceQueryString(Dictionary<string, object> initValues)
        {
            string stroriginal = SetQueryConditions();
            if (!string.IsNullOrEmpty(stroriginal))
            {
                var strReplace = stroriginal.Replace("and", "*").Replace("1=1", string.Empty);
                var strSplit = strReplace.Split('*');
                foreach (string str in strSplit)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (str.Contains("$@NO@"))
                        {
                            txtNO.Text = Strreplace(str);
                        }
                        else if (str.Contains("$@Customer@"))
                        {
                            txtCustomer.Text = Strreplace(str);
                        }
                        else if (str.Contains("$@Shipper@"))
                        {
                            txtShipper.Text = Strreplace(str);
                        }
                        else if (str.Contains("$@POL@"))
                        {
                            txtPOL.Text = Strreplace(str);
                        }
                        else if (str.Contains("$@POD@"))
                        {
                            txtPOD.Text = Strreplace(str);
                        }
                        else if (str.Contains("$@Consignee@"))
                        {
                            txtConsignee.Text = Strreplace(str);
                        }
                        else if (str.Contains("$@BLNO@"))
                        {
                            txtBLNO.Text = Strreplace(str);
                        }
                        else if (str.Contains("$@SalesID@"))
                        {
                            cmbSales.EditValue = Strreplace(str);
                        }
                        else if (str.Contains("$@OPD@"))
                        {
                            checkBoxopd.Checked = true;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 返回替换好的字符串
        /// </summary>
        /// <param name="replace">替换的字符串</param>
        /// <returns></returns>
        public string Strreplace(string replace)
        {
            if (replace.Contains("like"))
            {
                replace = replace.Replace("%", string.Empty).Replace("'", string.Empty)
                          .Replace("like", "?");

            }
            else if (replace.Contains("in"))
            {
                replace = replace.Replace("(", string.Empty).Replace("'", string.Empty)
                                 .Replace(")", string.Empty).Replace("in", "?");

            }
            else if ((replace.Contains(">") || replace.Contains("<")) && replace.Contains(":")) //包含">"或"<"判断且包含":"为时间，不替换空格
            {
                replace = replace.Replace("'", string.Empty).Replace("=", "?");
            }
            else
            {
                replace = replace.Replace("'", string.Empty).Replace("=", "?");
            }
            return replace.Split('?')[1];
        }
        
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            Locale();
            //注册延迟加载
            SetLazyLoaders();

            cmbCustomerService.OnFirstEnter += OncmbCustomerServiceFirstEnter;
            //文件
            cmbFiler.OnFirstEnter += OncmbFilerFirstEnter;
            //状态
            cmbState.ShowSelectedValue(DBNull.Value, LocalData.IsEnglish ? "ALL" : "全部");
            cmbState.OnFirstEnter += OncmbStateFirstEnter;

            //放货方案
            cmbprogram.ShowSelectedValue(ReleaseCargoProgram.Custom, LocalData.IsEnglish ? "Custom" : "自定义");
            cmbprogram.OnFirstEnter += OncmbprogramFirstEnter;

            //时间
            cmbDate.ShowSelectedValue(OIBusinessDateSearchType.CreateDate, LocalData.IsEnglish ? "Create Date" : "创建日期");
            cmbDate.OnFirstEnter += OncmbDateFirstEnter;

            //揽货人
            cmbSales.Enter += new EventHandler(cmbSales_Enter);
           

        }
        /// <summary>
        /// 中英文设置
        /// </summary>
        public void Locale()
        {
            if (LocalData.IsEnglish)
            {
                lblBLNo.Text = "BL No";
                lblAgent.Text = "Agent";
                lblBranch.Text = "Branch";
                lblCainterNo.Text = "Ctn No";
                lblConsignee.Text = "Consignee";
                lblCustomer.Text = "Customer";
                lblCustomeServicer.Text = "Cust Service";
                lblDelivery.Text = "Delivery";
                lblFiler.Text = "Doc By";
                lblLately.Text = "Lately";
                lblNo.Text = "No";
                lblNotice.Text = "Notice";
                lblPOD.Text = "POD";
                lblPOL.Text = "POL";
                lblSales.Text = "Sales";
                lblShippe.Text = "Shippe";
                lblState.Text = "State";
                lblTelexNo.Text = "Telex No";
                lblValid.Text = "Is Valid";
                lblVessel.Text = "Vessel";
                lblVoyage.Text = "Veyage";
                gcDate.Text = "Date";
                grpScope.Text = "Release BL/Cargo";
                lblFrom.Text = "From";
                lblTo.Text = "To";
                lblWith.Text = "With";
                rdoRelease.Properties.Items[0].Description = "All";
                rdoRelease.Properties.Items[1].Description = "RBLD";
                rdoRelease.Properties.Items[2].Description = "NO";

                rdoReceive.Properties.Items[0].Description = "All";
                rdoReceive.Properties.Items[1].Description = "RBLRcv";
                rdoReceive.Properties.Items[2].Description = "NO";

                rdoApply.Properties.Items[0].Description = "All";
                rdoApply.Properties.Items[1].Description = "RCA";
                rdoApply.Properties.Items[2].Description = "NO";

                rdoReleaseRC.Properties.Items[0].Description = "All";
                rdoReleaseRC.Properties.Items[1].Description = "RC";
                rdoReleaseRC.Properties.Items[2].Description = "NO";

                rdoARC.Properties.Items[0].Description = "All";
                rdoARC.Properties.Items[1].Description = "ARC";
                rdoARC.Properties.Items[2].Description = "NO";

                rdoIsURB.Properties.Items[0].Description = "All";
                rdoIsURB.Properties.Items[1].Description = "RBLNt";
                rdoIsURB.Properties.Items[2].Description = "NO";

                rdoIsUDN.Properties.Items[0].Description = "All";
                rdoIsUDN.Properties.Items[1].Description = "PayNt";
                rdoIsUDN.Properties.Items[2].Description = "NO";

                rdoIsACCLOS.Properties.Items[0].Description = "ALL";
                rdoIsACCLOS.Properties.Items[1].Description = "ACCLOS";
                rdoIsACCLOS.Properties.Items[2].Description = "NO";



            }
        }

        /// <summary>
        /// 清空界面数据
        /// </summary>
        public void Reset()
        {
            foreach (Control item in Controls)
            {
                if (item is LWImageComboBoxEdit)
                {
                    (item as LWImageComboBoxEdit).SelectedIndex = 0;
                }
                else if (item is MultiSearchCommonBox)
                {
                    (item as MultiSearchCommonBox).EditText = "";
                }
                else if (item is TextEdit
                    && (item is SpinEdit) == false
                    && item.Enabled == true
                    && (item as TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }

            lwchkIsValid.Checked = null;
            checkBoxopd.Checked = false;
            cmbprogram.SelectedIndex = 0;
            cmbDate.SelectedIndex = 0;

        }

        /// <summary>
        /// 注册延迟加载的数据源
        /// </summary>
        void SetLazyLoaders()
        {
            cmbCompany.SelectedIndexChanged -= cmbCompany_SelectedIndexChanged;
            cmbCompany.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.DefaultCompanyName);
            cmbCompany.SelectedIndexChanged += cmbCompany_SelectedIndexChanged;
            ////操作公司列表   
            cmbCompany.OnFirstEnter += OncmbCompanyFirstEnter;

        }

        /// <summary>
        /// 创建业务查询实体对象
        /// </summary>
        /// <returns></returns>
        private BusinessQueryCriteria CreatBusinessQueryCriteria()
        {
            BusinessQueryCriteria queryCriteria = new BusinessQueryCriteria();


            queryCriteria.OperationNo = SqlFilterHelper.SqlFilter(txtNO.Text.Trim());
            queryCriteria.BLNo = SqlFilterHelper.SqlFilter(txtBLNO.Text.Trim());
            queryCriteria.companyIDs = new Guid[] { CompanyID };
            queryCriteria.Customer = txtCustomer.Text.Trim().Replace("'", "''");
            queryCriteria.TelxNo = SqlFilterHelper.SqlFilter(txtTelexNo.Text.Trim());
            queryCriteria.ContainerNO = SqlFilterHelper.SqlFilter(txtContainerNO.Text.Trim());
            queryCriteria.Consinee = SqlFilterHelper.SqlFilter(txtConsignee.Text.Trim());
            queryCriteria.Shipper = SqlFilterHelper.SqlFilter(txtShipper.Text.Trim());
            queryCriteria.NotifyPart = SqlFilterHelper.SqlFilter(txtNotifier.Text.Trim());
            queryCriteria.CustomerServiceID = (cmbCustomerService.EditValue == null) ? Guid.Empty : new Guid(cmbCustomerService.EditValue.ToString());
            queryCriteria.DocBy = (cmbFiler.EditValue == null) ? Guid.Empty : new Guid(cmbFiler.EditValue.ToString());
            queryCriteria.SalesID = (cmbSales.EditValue == null) ? Guid.Empty : new Guid(cmbSales.EditValue.ToString());

            queryCriteria.POLName = SqlFilterHelper.SqlFilter(txtPOL.Text.Trim());
            queryCriteria.PODName = SqlFilterHelper.SqlFilter(txtPOD.Text.Trim());
            queryCriteria.Delivery = SqlFilterHelper.SqlFilter(txtDelivery.Text.Trim());

            //船名
            queryCriteria.Vessel = SqlFilterHelper.SqlFilter(txtVesselName.Text.Trim());


            //航次
            queryCriteria.VoyageNo = SqlFilterHelper.SqlFilter(txtVoyageNo.Text.Trim());

            //代理 待定
            queryCriteria.Agent = SqlFilterHelper.SqlFilter(txtAgent.Text.Trim());


            if (cmbState.SelectedIndex != 0)
                queryCriteria.State = (int)(OIOrderState)Enum.Parse(typeof(OIOrderState), cmbState.EditValue.ToString());
            else
                queryCriteria.State = 0;

            queryCriteria.IsValid = lwchkIsValid.Checked;

            queryCriteria.RBLD = rdoRelease.SelectedIndex;

            queryCriteria.RBLRcv = rdoReceive.SelectedIndex;
            queryCriteria.RCA = rdoApply.SelectedIndex;
            queryCriteria.RC = rdoReleaseRC.SelectedIndex;

            queryCriteria.ARC = rdoARC.SelectedIndex;


            queryCriteria.URB = rdoIsURB.SelectedIndex;

            queryCriteria.UDN = rdoIsUDN.SelectedIndex;

            queryCriteria.ACCLOS = rdoIsACCLOS.SelectedIndex;


            queryCriteria.OIDateSearchType = (OIBusinessDateSearchType)cmbDate.EditValue;
            queryCriteria.FromTime = fromToDateMonthControl1.From;
            queryCriteria.ToTime = fromToDateMonthControl1.To;
            queryCriteria.TemplateCode = OperationViewCode;
            //queryCriteria.AdvanceQueryString = GetAdvanceQueryString(queryCriteria);
            return queryCriteria;
        }

        public string GetAdvanceQueryString()
        {
            BusinessQueryCriteria criteria = CreatBusinessQueryCriteria();
            string queryString = " 1=1  ";
            string tmp = "";

            //业务号

            if (!string.IsNullOrEmpty(criteria.OperationNo))
            {
                queryString += " and $@NO@ like " + "'%" + criteria.OperationNo + "%'";
            }
            //提货单

            if (!string.IsNullOrEmpty(criteria.BLNo))
            {
                queryString += " and $@BLNO@ like " + "'%" + criteria.BLNo + "%'";
            }

            //操作口岸
            if (Guid.Empty != CompanyID)
            {
                queryString += " and $@CompanyID@ = '" + CompanyID + "'";
            }



            //客户名称

            if (!string.IsNullOrEmpty(criteria.Customer))
            {
                string Customer = OperationViewService.GetDeleteMarkerForInputStr(criteria.Customer);
                queryString += "   and   EXISTS (SELECT 1   FROM   fcm.FasterSearchCustomers fsc     INNER JOIN pub.VCustomers c   ON  c.ID = fsc.CustomerID  AND CHARINDEX("
                           + "'" + Customer + "'" + ", c.CodeName) > 0" + " where  fsc.OperationID = BOKI.ID    and    fsc.CustomerType=1)";
            }

            //电 放 号

            if (!string.IsNullOrEmpty(criteria.TelxNo))
            {
                queryString += " and $@TelxNo@ like " + "'%" + criteria.TelxNo + "%'";
            }

            //箱    号
            if (!string.IsNullOrEmpty(criteria.ContainerNO))
            {
                queryString += " and $@ContainerNO@ like " + "'%" + criteria.ContainerNO + "%'";
            }

            //收货人

            if (!string.IsNullOrEmpty(criteria.Consinee))
            {
                string consinee = OperationViewService.GetDeleteMarkerForInputStr(criteria.Consinee);
                queryString += "   and   EXISTS (SELECT 1   FROM   fcm.FasterSearchCustomers fsc     INNER JOIN pub.VCustomers c   ON  c.ID = fsc.CustomerID  AND CHARINDEX("
                           + "'" + consinee + "'" + ", c.CodeName) > 0" + " where  fsc.OperationID = BOKI.ID  and    fsc.CustomerType=5 )";
            }

            //发货人

            if (!string.IsNullOrEmpty(criteria.Shipper))
            {
                string shipper = OperationViewService.GetDeleteMarkerForInputStr(criteria.Shipper);
                queryString += "   and   EXISTS (SELECT 1   FROM   fcm.FasterSearchCustomers fsc     INNER JOIN pub.VCustomers c   ON  c.ID = fsc.CustomerID  AND CHARINDEX("
                           + "'" + shipper + "'" + ", c.CodeName) > 0" + " where  fsc.OperationID = BOKI.ID  and    fsc.CustomerType=4 )";
            }


            // 通知人 待定

            if (!string.IsNullOrEmpty(criteria.NotifyPart))
            {
                string notifyPart = OperationViewService.GetDeleteMarkerForInputStr(criteria.NotifyPart);
                queryString += "   and   EXISTS (SELECT 1   FROM   fcm.FasterSearchCustomers fsc     INNER JOIN pub.VCustomers c   ON  c.ID = fsc.CustomerID  AND CHARINDEX("
                           + "'" + notifyPart + "'" + ", c.CodeName) > 0" + " where  fsc.OperationID = BOKI.ID  and    fsc.CustomerType=6 )";
            }

            //客服 待定

            if (Guid.Empty != criteria.CustomerServiceID && criteria.CustomerServiceID != null)
            {
                queryString += " and $@CustomerServiceID@ = '" + criteria.CustomerServiceID + "'";
            }

            //文件员
            if (cmbFiler.EditValue != null)
            {
                queryString += " and $@DocByID@=" + "'" + cmbFiler.EditValue + "'";
            }
            //揽货人
            if (cmbSales.EditValue != null)
            {
                queryString += " and $@SalesID@=" + "'" + cmbSales.EditValue + "'";
            }

            //装货港名称

            if (!string.IsNullOrEmpty(criteria.POLName))
            {
                queryString += " and $@POL@ like " + "'%" + criteria.POLName + "%'";
            }

            //卸货港名称

            if (!string.IsNullOrEmpty(criteria.PODName))
            {
                queryString += " and $@POD@ like " + "'%" + criteria.PODName + "%'";
            }
            //交货地名称

            if (!string.IsNullOrEmpty(criteria.Delivery))
            {
                queryString += " and $@PlaceOfDelivery@ like " + "'%" + criteria.Delivery + "%'";
            }

            //船名
            if (!string.IsNullOrEmpty(criteria.Vessel))
            {
                queryString += " and $@Vessel@ like " + "'%" + criteria.Vessel + "%'";
            }

            //航次

            if (!string.IsNullOrEmpty(criteria.VoyageNo))
            {
                queryString += " and $@Voyage@ like " + "'%" + criteria.VoyageNo + "%'";
            }

            //代理 待定
            tmp = SqlFilterHelper.SqlFilter(txtAgent.Text.Trim());
            if (!string.IsNullOrEmpty(criteria.Agent))
            {
                queryString += " and $@Agent@ like " + "'%" + criteria.Agent + "%'";
            }

            //状态
            if (criteria.State != 0)
            {
                queryString += " and $@State@ = " + criteria.State;
            }

            //是否有效
            if (lwchkIsValid.Checked == true)
            {
                queryString += " and $@IsValid@ = 1";
            }
            else if (lwchkIsValid.Checked == false)
            {
                queryString += " and $@IsValid@ =0";
            }


            //是否放单
            if (rdoRelease.SelectedIndex != 0)
            {
                queryString += " and $@RBLD@ " + (rdoRelease.SelectedIndex == 1 ? " > 1" : " = 1");
                //queryString += " and $@RBLD@ = " + rdoRelease.SelectedIndex.ToString();
            }
            //是否已签收
            if (rdoReceive.SelectedIndex != 0)
            {
                queryString += " and $@RBLRcv@ = " + rdoReceive.SelectedIndex.ToString();
            }

            //是否已申请
            if (rdoApply.SelectedIndex != 0)
            {
                queryString += " and $@RCA@ = " + rdoApply.SelectedIndex.ToString();
            }
            //是否已放货
            if (rdoReleaseRC.SelectedIndex != 0)
            {
                queryString += " and $@RC@ = " + rdoReleaseRC.SelectedIndex.ToString();
            }
            //是否已同意
            if (rdoARC.SelectedIndex != 0)
            {
                queryString += " and $@ARC@ = " + rdoARC.SelectedIndex.ToString();
            }

            //是否已催单
            if (rdoIsURB.SelectedIndex != 0)
            {
                queryString += " and $@URB@ = " + rdoIsURB.SelectedIndex.ToString();
            }

            //是否已催款
            if (rdoIsUDN.SelectedIndex != 0)
            {
                queryString += " and $@UDN@ = " + rdoIsUDN.SelectedIndex.ToString();
            }
            //是否已关帐
            if (rdoIsACCLOS.SelectedIndex != 0)
            {
                queryString += " and $@ACCLOS@ = " + rdoIsACCLOS.SelectedIndex.ToString();
            }


            if (cmbDate.EditValue.ToString() != OIBusinessDateSearchType.All.ToString())
            {
                OIBusinessDateSearchType dateSearchType = (OIBusinessDateSearchType)cmbDate.EditValue;
                DateTime? dtFrom = fromToDateMonthControl1.From;
                DateTime? dtTo = fromToDateMonthControl1.To;
                if (dtFrom != null && dtTo != null && dateSearchType != OIBusinessDateSearchType.All)
                {
                    tmp = Enum.GetName(typeof(OIBusinessDateSearchType), dateSearchType);

                    queryString += " and $@" + tmp + "@ >= '" + dtFrom.ToString() + "'";

                    queryString += " and $@" + tmp + "@ < '" + dtTo.ToString() + "'";
                }

            }

            //是否为最近业务
            if (checkBoxopd.Checked)
            {
                queryString += "and $@OPD@=0 ";
            }

            return queryString;
        }

        /// <summary>
        /// 绑定客服
        /// </summary>
        private void BindCustomerService()
        {
            ICPCommUIHelper.SetMcmbUsers(cmbCustomerService, CompanyID, "客服", string.Empty, true);
        }
        /// <summary>
        /// 绑定文件员
        /// </summary>
        private void BindFiler()
        {
            ICPCommUIHelper.SetMcmbUsers(cmbFiler, CompanyID, "文件", string.Empty, true);

        }
        /// <summary>
        /// 绑定订单状态
        /// </summary>
        private void BindState()
        {
            //状态
            List<EnumHelper.ListItem<OIOrderState>> orderStates = EnumHelper.GetEnumValues<OIOrderState>(LocalData.IsEnglish);
            cmbState.Properties.BeginUpdate();

            foreach (var item in orderStates)
            {
                if (item.Value == OIOrderState.Unknown) continue;
                cmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbState.Properties.EndUpdate();
        }
        private void BindProgram()
        {
            cmbprogram.Properties.BeginUpdate();
            List<EnumHelper.ListItem<ReleaseCargoProgram>> releasePrograms = EnumHelper.GetEnumValues<ReleaseCargoProgram>(LocalData.IsEnglish);
            foreach (var item in releasePrograms)
            {
                if (item.Value == ReleaseCargoProgram.Custom)
                    continue;
                cmbprogram.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbprogram.Properties.EndUpdate();
        }
        /// <summary>
        /// 
        /// </summary>
        private void BindDate()
        {
            //时间
            List<EnumHelper.ListItem<OIBusinessDateSearchType>> dateSearchTypes = EnumHelper.GetEnumValues<OIBusinessDateSearchType>(LocalData.IsEnglish);
            cmbDate.Properties.BeginUpdate();
            foreach (var item in dateSearchTypes)
            {
                if (item.Value == OIBusinessDateSearchType.All || item.Value == OIBusinessDateSearchType.CreateDate)
                {
                    continue;
                }
                cmbDate.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDate.Properties.EndUpdate();
        }
        #endregion
    }
}
