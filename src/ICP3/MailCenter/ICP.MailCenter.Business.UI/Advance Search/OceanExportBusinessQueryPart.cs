using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface;
using ICP.Common.UI;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using ICP.Operation.Common.UI;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.TaskCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Constants = ICP.Operation.Common.ServiceInterface.Constants;

namespace ICP.MailCenter.Business.UI
{
    /// <summary>
    /// 海出高级查询用户控件
    /// </summary>
    public partial class OceanExportBusinessQueryPart : XtraUserControl, IBusinessQueryPart
    {
        #region Service
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
        //Xml的文件名称
        private const string TempalteFileName = "QueryConditions.xml";
        /// <summary>
        /// 状态面板
        /// </summary>
        private UCOrderStates ucOrderStates;

        private UCOrderStates UserControlOrderStates
        {
            get
            {
                if (ucOrderStates == null)
                {
                    ucOrderStates = Workitem == null ? new UCOrderStates() : Workitem.Items.AddNew<UCOrderStates>();
                }
                return ucOrderStates;
            }
        }
        #endregion

        #region Property
        /// <summary>
        /// 用户所挂公司列表
        /// </summary>
        List<OrganizationList> _UserCompanyList = null;

        /// <summary>
        /// 用户所挂公司列表组织ID集合
        /// </summary>
        List<Guid> _UserCompanyIDs = new List<Guid>();

        /// <summary>
        /// 客服数据源
        /// </summary>
        List<UserList> _SOByusers = null;

        /// <summary>
        /// 海外客服数据源
        /// </summary>
        List<UserList> _OverseasCSusers = null;

        /// <summary>
        /// 文件员数据源
        /// </summary>
        List<UserList> _DocByUsers = null;

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
        /// 历史查询字符串
        /// </summary>
        public string StrOriginal
        {
            get;
            set;
        }
        /// <summary>
        /// 业务类型
        /// </summary>
        private BusinessType type = BusinessType.OE;
        /// <summary>
        /// 业务类型
        /// </summary>
        /// <remarks>海运出口</remarks>
        [DefaultValue(BusinessType.OE)]
        public BusinessType Type
        {
            get
            {
                return type;
            }
        }

        #region 获取控件值

        /// <summary>
        /// 操作口岸ID集合
        /// </summary>
        public List<Guid> CompanyIDs
        {
            get
            {
                List<Guid> companyIDs = new List<Guid>();
                foreach (CheckedListBoxItem item in cmbOpBranch.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        companyIDs.Add(new Guid(item.Value.ToString()));
                }
                return companyIDs;
            }
        }

        /// <summary>
        /// 揽货部门ID集合
        /// </summary>
        /// <returns></returns>
        public List<Guid> SalesBranchIDs
        {
            get
            {
                List<Guid> companyIDs = new List<Guid>();
                foreach (CheckedListBoxItem item in cmbSalesBranch.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        companyIDs.Add(new Guid(item.Value.ToString()));
                }
                return companyIDs;
            }
        }

        /// <summary>
        /// 揽货人ID
        /// </summary>
        /// 
        public Guid? SalesID
        {
            get
            {
                if (cmbSales.EditValue == null || cmbSales.EditValue == string.Empty) return null;
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
                if (txtDocBy.EditValue == null || txtDocBy.EditValue == string.Empty) return null;
                return new Guid(txtDocBy.EditValue.ToString());
            }
        }

        /// <summary>
        /// 订单员ID
        /// </summary>
        public Guid? SoByID
        {
            get
            {
                if (cmbSoBy.EditValue == null || cmbSoBy.EditValue == string.Empty) return null;
                return new Guid(cmbSoBy.EditValue.ToString());
            }
        }

        /// <summary>
        /// 海外客服ID
        /// </summary>
        public Guid? OverseasCsID
        {
            get
            {
                if (cmbOverseasCS.EditValue == null || cmbOverseasCS.EditValue == string.Empty) return null;
                else if ((new Guid(cmbOverseasCS.EditValue.ToString())) == Guid.Empty)
                {
                    return null;
                }

                return new Guid(cmbOverseasCS.EditValue.ToString());
            }
        }

        /// <summary>
        /// 离港时间
        /// </summary>
        public DateTime? LeaveDataTime
        {
            get
            {
                return dteETD.DateTime.Date;
            }
        }

        /// <summary>
        /// 到港时间
        /// </summary>
        public DateTime? ArriveDataTime
        {
            get
            {
                return dteETA.DateTime.Date;
            }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? FromDataTime
        {
            get
            {
                return dteFrom.DateTime.Date;
            }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? ToDataTime
        {
            get
            {
                return dteTo.DateTime.Date;
            }

        }

        #endregion
        #endregion

        #region Init
        /// <summary>
        /// 构造函数
        /// </summary>
        public OceanExportBusinessQueryPart()
        {
            InitializeComponent();
            
            Disposed += (sender, arg) =>
            {
                if (Workitem != null)
                {
                    if (ucOrderStates != null)
                    {
                        Workitem.Items.Remove(ucOrderStates);
                    }
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
            Locale();
        } 
        #endregion

        #region Custom Method
        /// <summary>
        /// 
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
            string templateFilePath = ICPPathUtility.CombineDirectory4FileName(ICPPathUtility.BusinessTemplates, TempalteFileName);
            if (!File.Exists(templateFilePath)) return stroriginal;

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
                        else if (str.Contains("$@SONO@"))
                        {
                            txtSONO.Text = Strreplace(str);
                        }
                        else if (str.Contains("$@Carrier@"))
                        {
                            txtCarrier.Text = Strreplace(str);
                        }
                        else if (str.Contains("$@Consignee@"))
                        {
                            txtConsignee.Text = Strreplace(str);
                        }
                        else if (str.Contains("$@BLNO@"))
                        {
                            txtBLNO.Text = Strreplace(str);
                        }
                        else if (str.Contains("$@Vessel@"))
                        {
                            txtVessel.Text = Strreplace(str);
                        }
                        else if (str.Contains("$@Voyage@"))
                        {
                            txtVoyage.Text = Strreplace(str);
                        }
                        else if (str.Contains("$@DocByID@"))
                        {
                            txtDocBy.EditValue = Strreplace(str);
                        }
                        else if (str.Contains("$@ETA@"))
                        {
                            cmbScope.SelectedIndex = 1;
                            if (str.Contains(">"))
                            {
                                dteFrom.EditValue = Convert.ToDateTime(Strreplace(str));
                            }
                            if (str.Contains("<"))
                            {
                                dteTo.EditValue = Convert.ToDateTime(Strreplace(str));
                            }
                        }
                        else if (str.Contains("$@ETD@"))
                        {
                            cmbScope.SelectedIndex = 0;
                            if (str.Contains(">"))
                            {
                                dteFrom.EditValue =Convert.ToDateTime(Strreplace(str));
                            }
                            if (str.Contains("<"))
                            {
                                dteTo.EditValue = Convert.ToDateTime(Strreplace(str));
                            }
                        }
                        else if (str.Contains("$@SoBy@"))
                        {
                            cmbSoBy.EditValue = Strreplace(str);
                        }
                        else if (str.Contains("$@SalesID@"))
                        {
                            cmbSales.EditValue = Strreplace(str);
                        }
                        else if (str.Contains("$@OPD@"))
                        {
                            checkBoxopd.Checked=true;
                        }
                        else if (str.Contains("$@SOA@"))
                        {
                            UserControlOrderStates.SOA=true;;
                        }
                        else if (str.Contains("$@SOD@"))
                        {
                            UserControlOrderStates.SOD=true;;
                        }
                        else if (str.Contains("$@SOS@"))
                        {
                            UserControlOrderStates.SOS=true;;
                        }
                        else if (str.Contains("$@TRKA@"))
                        {
                            UserControlOrderStates.TRKA=true;;
                        }
                        else if (str.Contains("$@SOPV@"))
                        {
                            UserControlOrderStates.SOPV=true;;
                        }
                        else if (str.Contains("$@SOC@"))
                        {
                            UserControlOrderStates.SOC=true;;
                        }
                        else if (str.Contains("$@SINC@"))
                        {
                            UserControlOrderStates.SINC = true; ;
                        }
                        else if (str.Contains("$@SIR@"))
                        {
                            UserControlOrderStates.SIR = true; ;
                        }
                        else if (str.Contains("$@BLCFM@"))
                        {
                            UserControlOrderStates.BLCFM = true; ;
                        }
                        else if (str.Contains("$@BLCfmAgt@"))
                        {
                            UserControlOrderStates.BLCfmAgt = true; ;
                        }
                        else if (str.Contains("$@MBLR@"))
                        {
                            UserControlOrderStates.MBLR = true; ;
                        }
                        else if (str.Contains("$@MBLD@"))
                        {
                            UserControlOrderStates.MBLD = true; ;
                        }
                        else if (str.Contains("$@AMS@"))
                        {
                            UserControlOrderStates.AMS = true; ;
                        }
                        else if (str.Contains("$@ISF@"))
                        {
                            UserControlOrderStates.ISF = true; ;
                        }
                        else if (str.Contains("$@ARP@"))
                        {
                            UserControlOrderStates.ARP = true; ;
                        }
                        else if (str.Contains("$@APA@"))
                        {
                            UserControlOrderStates.APA = true; ;
                        }
                        else if (str.Contains("$@APP@"))
                        {
                            UserControlOrderStates.APP = true; ;
                        }
                        else if (str.Contains("$@APInv@"))
                        {
                            UserControlOrderStates.APInv = true; ;
                        }
                        else if (str.Contains("$@ARCfm@"))
                        {
                            UserControlOrderStates.ARCfm = true; ;
                        }
                        else if (str.Contains("$@ARA@"))
                        {
                            UserControlOrderStates.ARA = true; ;
                        }
                        else if (str.Contains("$@DocS@"))
                        {
                            UserControlOrderStates.DocS = true; ;
                        }
                        else if (str.Contains("$@DocH@"))
                        {
                            UserControlOrderStates.DocH = true; ;
                        }
                        else if (str.Contains("$@DCRev@"))
                        {
                            UserControlOrderStates.DCRev = true; ;
                        }
                        else if (str.Contains("$@DcRcv@"))
                        {
                            UserControlOrderStates.DcRcv = true; ;
                        }
                        else if (str.Contains("$@RBLD@"))
                        {
                            UserControlOrderStates.RBLD = true; ;
                        }
                        else if (str.Contains("$@RC@"))
                        {
                            UserControlOrderStates.RC = true; ;
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
                          .Replace("like", "?").Replace(" ", string.Empty);

            }
            else if (replace.Contains("in"))
            {
                replace = replace.Replace("(", string.Empty).Replace("'", string.Empty)
                                 .Replace(")", string.Empty).Replace("in", "?").Replace(" ", string.Empty);

            }
            else if ((replace.Contains(">") || replace.Contains("<")) && replace.Contains(":")) //包含">"或"<"判断且包含":"为时间，不替换空格
            {
                replace = replace.Replace("'", string.Empty).Replace("=", "?");
            }
            else
            {
                replace = replace.Replace("'", string.Empty).Replace("=", "?").Replace(" ", string.Empty);
            }
            return replace.Split('?')[1];
        }

        
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            _UserCompanyList = SetCompany();

            if (!string.IsNullOrEmpty(SelectedCompanyIDs))
            {
                cmbOpBranch.EditValue = SelectedCompanyIDs;
            }
            else
            {
                cmbOpBranch.EditValue = LocalData.UserInfo.DefaultCompanyName;
            }

            foreach (var item in _UserCompanyList)
            {
                _UserCompanyIDs.Add(item.ID);
            }

            //揽货人数据源
            //	当前用户所在的操作口岸的海运订舱单中所有揽货人	数据从用户表[sm..Users]检索
            cmbSales.SetEnterToExecuteOnec( delegate
            {
                List<UserList> salesUsers = UserService.GetUnderlingUserList(null, new string[] { "海外拓展", "销售代表", "拓展员", "总裁", "副总裁", "总经理助理", "分公司总经理" }, null, true);

                UserList insertItem = new UserList();
                insertItem.ID = Guid.Empty;
                insertItem.EName = insertItem.CName = string.Empty;
                salesUsers.Insert(0, insertItem);

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                cmbSales.InitSource<UserList>(salesUsers, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            });
            SetOverseasCSDataSource();
            SetDocByDataSource();
            SetServiceDataSource();
            if (UserControlOrderStates != null)
            {
                UserControlOrderStates.Dock = DockStyle.Fill;
                grpState.Controls.Add(UserControlOrderStates);
            }
        }

        /// <summary>
        /// 设置公司ID集合
        /// </summary>
        /// <returns></returns>
        private List<OrganizationList> SetCompany()
        {
            //获取用户所挂公司列表
            List<OrganizationList> userCompanyList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
            cmbOpBranch.Properties.Items.BeginUpdate();
            cmbOpBranch.Properties.Items.Clear();
            cmbSalesBranch.Properties.Items.BeginUpdate();
            cmbSalesBranch.Properties.Items.Clear();
            foreach (var item in userCompanyList)
            {
                if (item.ID == LocalData.UserInfo.DefaultCompanyID)
                {
                    cmbOpBranch.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                   CheckState.Checked, true);


                    cmbSalesBranch.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                   CheckState.Checked, true);
                }
                else
                {
                    cmbOpBranch.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                       CheckState.Unchecked, true);
                    cmbSalesBranch.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                 CheckState.Unchecked, true);
                }
            }
            return userCompanyList;
        }

        /// <summary>
        /// 设置客服数据源
        /// </summary>
        private void SetServiceDataSource()
        {
            //客服数据源
            //	当前用户所在的部门的所有用户	包含下属部门的所有用户
            cmbSoBy.SetEnterToExecuteOnec(delegate
            {
                _SOByusers = UserService.GetUnderlingUserList(_UserCompanyIDs.ToArray(), new string[] { "订舱" }, null, true);
                UserList currentUser = _SOByusers.Find(delegate(UserList item) { return item.ID == LocalData.UserInfo.LoginID; });
                if (currentUser != null)
                {
                    _SOByusers.Remove(currentUser);
                    _SOByusers.Insert(0, currentUser);
                }
                else
                {
                    currentUser = new UserList();
                    currentUser = UserService.GetUserInfo(LocalData.UserInfo.LoginID);
                    _SOByusers.Insert(0, currentUser);
                }

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
                cmbSoBy.InitSource<UserList>(_SOByusers, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
                txtDocBy.InitSource<UserList>(_SOByusers, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            });

        }

        /// <summary>
        /// 设置海外客服数据源
        /// </summary>
        private void SetOverseasCSDataSource()
        {
            //海外部客服
            cmbOverseasCS.SetEnterToExecuteOnec(delegate
            {

                _OverseasCSusers = UserService.GetUnderlingUserList(_UserCompanyIDs.ToArray(), new string[] { "海外拓展", "客服" }, null, true);

                UserList insertItem = new UserList();
                insertItem.ID = Guid.Empty;
                insertItem.EName = insertItem.CName = string.Empty;
                _OverseasCSusers.Insert(0, insertItem);

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                cmbOverseasCS.InitSource<UserList>(_OverseasCSusers, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            });

        }

        /// <summary>
        /// 设置文件员数据源
        /// </summary>
        private void SetDocByDataSource()
        {

            txtDocBy.SetEnterToExecuteOnec(delegate
            {

                _DocByUsers = UserService.GetUnderlingUserList(_UserCompanyIDs.ToArray(), new string[] { "文件" }, null, true);

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                txtDocBy.InitSource<UserList>(_DocByUsers, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            });


        }

        /// <summary>
        /// 本地化
        /// </summary>
        public void Locale()
        {
            if (LocalData.IsEnglish)
            {
                grpScope.Text = "Scope";
                grpState.Text = "State";
                lblBLNo.Text = "BL NO";
                lblBookingParty.Text = "Booking Party";
                lblBranch.Text = "Branch";
                lblCtnNo.Text = "Ctn NO";
                lblCarrier.Text = "Carrier";
                lblConsignee.Text = "Consignee";
                lblCustomer.Text = "Customer";
                lblDelivery.Text = "Delivery";
                lblETA.Text = "ETA";
                lblETD.Text = "ETD";
                lblFiler.Text = "Doc By";
                lblFrom.Text = "From";
                lblLately.Text = "Lately";
                lblNo.Text = "No";
                lblPOD.Text = "POD";
                lblPOL.Text = "POL";
                lblSales.Text = "Sales";
                lblSalesDept.Text = "Sales Dept";
                lblOverseasSC.Text = "Overseas SC";
                lblShipper.Text = "Shipper";
                lblSoNo.Text = "So No";
                lblTo.Text = "To";
                lblVessal.Text = "Vessal";
                lblVoyage.Text = "Voyage";
            }
        }


        /// <summary>
        /// 清空界面数据
        /// </summary>
        public void Reset()
        {
            txtNO.Text = string.Empty;
            txtCustomer.Text = string.Empty;
            txtShipper.Text = string.Empty;
            txtPOL.Text = string.Empty;
            txtPOD.Text = string.Empty;
            txtSONO.Text = string.Empty;
            txtCarrier.Text = string.Empty;
            txtConsignee.Text = string.Empty;
            txtBLNO.Text = string.Empty;
            txtDelivery.Text = string.Empty;
            txtVessel.Text = string.Empty;
            txtVoyage.Text = string.Empty;
            txtDocBy.EditValue = string.Empty;
            txtContainerNO.Text = string.Empty;
            dteETD.EditValue = string.Empty;
            dteETA.EditValue = string.Empty;
            cmbSoBy.EditValue = string.Empty;
            cmbOverseasCS.EditValue = string.Empty;
            cmbSales.EditValue = string.Empty;
            cmbSalesBranch.EditValue = string.Empty;
            checkBoxopd.Checked = false;
            UserControlOrderStates.SOA = false;
            UserControlOrderStates.SOD = false;
            UserControlOrderStates.SOS = false;
            UserControlOrderStates.TRKA = false;
            UserControlOrderStates.SOPV = false;
            UserControlOrderStates.SOC = false;
            UserControlOrderStates.SINC = false;
            UserControlOrderStates.SIR = false;
            UserControlOrderStates.BLCFM = false;
            UserControlOrderStates.BLCfmAgt = false;
            UserControlOrderStates.MBLR = false;
            UserControlOrderStates.MBLD = false;
            UserControlOrderStates.AMS = false;
            UserControlOrderStates.ISF = false;
            UserControlOrderStates.ARP = false;
            UserControlOrderStates.APA = false;
            UserControlOrderStates.APP = false;
            UserControlOrderStates.APInv = false;
            UserControlOrderStates.ARCfm = false;
            UserControlOrderStates.ARA = false;
            UserControlOrderStates.DocS = false;
            UserControlOrderStates.DocH = false;
            UserControlOrderStates.DCRev = false;
            UserControlOrderStates.DcRcv = false;
            UserControlOrderStates.RBLD = false;
            UserControlOrderStates.RC = false;
        }

        /// <summary>
        /// 创建业务查询实体对象
        /// </summary>
        /// <returns></returns>
        private BusinessQueryCriteria CreatBusinessQueryCriteria()
        {
            BusinessQueryCriteria queryCriteria = new BusinessQueryCriteria();
            queryCriteria.OperationNo = SqlFilterHelper.SqlFilter(txtNO.Text.Trim());
            queryCriteria.Customer = txtCustomer.Text.Trim().Replace("'", "''");
            queryCriteria.Shipper = SqlFilterHelper.SqlFilter(txtShipper.Text.Trim());
            queryCriteria.ContainerNO = SqlFilterHelper.SqlFilter(txtContainerNO.Text.Trim());
            queryCriteria.POLName = SqlFilterHelper.SqlFilter(txtPOL.Text.Trim());
            queryCriteria.PODName = SqlFilterHelper.SqlFilter(txtPOD.Text.Trim());
            queryCriteria.SONO = SqlFilterHelper.SqlFilter(txtSONO.Text.Trim());
            queryCriteria.Carrier = SqlFilterHelper.SqlFilter(txtCarrier.Text.Trim());
            queryCriteria.Consinee = SqlFilterHelper.SqlFilter(txtConsignee.Text.Trim());
            queryCriteria.BLNo = SqlFilterHelper.SqlFilter(txtBLNO.Text.Trim());
            queryCriteria.Delivery = SqlFilterHelper.SqlFilter(txtDelivery.Text.Trim());
            queryCriteria.Vessel = SqlFilterHelper.SqlFilter(txtVessel.Text.Trim());
            queryCriteria.VoyageNo = SqlFilterHelper.SqlFilter(txtVoyage.Text.Trim());
            queryCriteria.DocBy = DocID;
            queryCriteria.EtdTime = LeaveDataTime;
            queryCriteria.EtaTime = ArriveDataTime;
            queryCriteria.OverseasCs = OverseasCsID;
            queryCriteria.SoBy = SoByID;
            queryCriteria.dateSearchType = DateSearchType.ETA;
            queryCriteria.FromTime = FromDataTime;
            queryCriteria.ToTime = ToDataTime;
            queryCriteria.companyIDs = CompanyIDs.ToArray();
            queryCriteria.SalesBrach = SalesBranchIDs.ToArray();
            queryCriteria.SalesID = SalesID;
            queryCriteria.TemplateCode = OperationViewCode;
            return queryCriteria;
        }

        /// <summary>
        /// 获取高级查询字符串
        /// </summary>
        /// <returns></returns>
        public string GetAdvanceQueryString()
        {
            if (CompanyIDs.Count == 0)
            {
                throw new ICPException(LocalData.IsEnglish ? "At least one company must be selected." : "必须至少选择一个操作口岸.");
            }
            BusinessQueryCriteria criteria = CreatBusinessQueryCriteria();
            string queryString = " 1 = 1  ";


            //业务号
            if (!string.IsNullOrEmpty(criteria.OperationNo))
            {
                queryString += " and $@NO@ like " + "'%" + criteria.OperationNo + "%'";
            }
            //客户名称
            if (!string.IsNullOrEmpty(criteria.Customer))
            {
                string Customer = OperationViewService.GetDeleteMarkerForInputStr(criteria.Customer);
                queryString += "   and   EXISTS (SELECT 1   FROM   fcm.FasterSearchCustomers fsc     INNER JOIN pub.VCustomers c   ON  c.ID = fsc.CustomerID  and CHARINDEX("
                           + "'" + Customer + "'" + ", c.CodeName) > 0" + " where  fsc.OperationID = BOKI.ID    and    fsc.CustomerType=1)";
            }
            //发货人
            if (!string.IsNullOrEmpty(criteria.Shipper))
            {
               string  shipper=OperationViewService.GetDeleteMarkerForInputStr(criteria.Shipper);
               queryString += "   and   EXISTS (SELECT 1   FROM   fcm.FasterSearchCustomers fsc     INNER JOIN pub.VCustomers c   ON  c.ID = fsc.CustomerID  and CHARINDEX("
                          + "'" + shipper + "'" + ", c.CodeName) > 0" + " where  fsc.OperationID = BOKI.ID  and    fsc.CustomerType=4 )";
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
            //订舱号
            if (!string.IsNullOrEmpty(criteria.SONO))
            {
                queryString += " and $@SONO@ like " + "'%" + criteria.SONO + "%'";
            }
            //承运人
            if (!string.IsNullOrEmpty(criteria.Carrier))
            {
                string carrier = OperationViewService.GetDeleteMarkerForInputStr(criteria.Carrier);
                queryString += "   and   EXISTS (SELECT 1   FROM   fcm.FasterSearchCustomers fsc     INNER JOIN pub.VCustomers c   ON  c.ID = fsc.CustomerID  and CHARINDEX("
                           + "'" + carrier + "'" + ", c.CodeName) > 0" + " where  fsc.OperationID = BOKI.ID  and    fsc.CustomerType in(2,3) )";
            }
            //收货人
            if (!string.IsNullOrEmpty(criteria.Consinee))
            {
                string Consinee = OperationViewService.GetDeleteMarkerForInputStr(criteria.Consinee);
                queryString += "   and   EXISTS (SELECT 1   FROM   fcm.FasterSearchCustomers fsc     INNER JOIN pub.VCustomers c   ON  c.ID = fsc.CustomerID  and CHARINDEX("
                           + "'" + Consinee + "'" + ", c.CodeName) > 0" + " where  fsc.OperationID = BOKI.ID  and    fsc.CustomerType=5 )";
            }
            //提货单
            if (!string.IsNullOrEmpty(criteria.BLNo))
            {
                queryString += " and $@BLNO@ like " + "'%" + criteria.BLNo + "%'";
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
            //文件员
            if (criteria.DocBy != null)
            {
                queryString += " and $@DocByID@=" + "'" + criteria.DocBy + "'";
            }
            //到港日
            if (criteria.EtaTime != DateTime.MinValue)
            {
                queryString += " and $@ETA@ >= " + "'" + DateTime.Parse(criteria.EtaTime.ToString())+ "'";
                queryString += " and $@ETA@ <= " + "'" + DateTime.Parse(criteria.EtaTime.ToString()).ToShortDateString() + " 23:59:59'";
            }
            //离港日
            if (criteria.EtdTime != DateTime.MinValue)
            {
                queryString += " and $@ETD@ >= " + "'" + DateTime.Parse(criteria.EtdTime.ToString()) + "'";
                queryString += " and $@ETD@ <= " + "'" + DateTime.Parse(criteria.EtdTime.ToString()).ToShortDateString() + " 23:59:59'";
            }
            //订舱员
            if (criteria.SoBy != null && criteria.SoBy != Guid.Empty)
            {
                queryString += " and $@SoByID@=" + "'" + criteria.SoBy + "'";
            }
            //揽货人
            if (criteria.SalesID != null && criteria.SalesID != Guid.Empty)
            {
                queryString += " and $@SalesID@=" + "'" + criteria.SalesID + "'";
            }

            //柜号
            if (!string.IsNullOrEmpty(criteria.ContainerNO))
            {
                queryString += " and $@ContainerNO@ like " + "'%" + criteria.ContainerNO + "%'";
            }
            //操作口岸
            if (criteria.companyIDs.Length != 0)
            {
                string strList = string.Empty;
                foreach (Guid id in criteria.companyIDs)
                {
                    strList += (strList.Length == 0 ? "" : ",") + "'" + id + "'";
                }
                if (Workitem != null)
                {
                    Workitem.State["AdvanceSearchCompanyIDs"] = criteria.companyIDs;
                }
                queryString += string.Format(" and $@CompanyID@ in ({0})", strList);
            }
            //是否为最近业务
            if (checkBoxopd.Checked)
            {
                queryString += "and $@OPD@=0";
            }
            //范围:ETD、ETA时间范围
            if (cmbScope.EditValue != null)
            {
                if (!string.IsNullOrEmpty(cmbScope.EditValue.ToString()))
                {
                    if (FromDataTime!=DateTime.MinValue)
                        queryString += " and $@" + cmbScope.EditValue + "@>=" + "'" + FromDataTime + "'  ";
                    if (ToDataTime!=DateTime.MinValue)
                        queryString += " and $@" + cmbScope.EditValue + "@<=" + "'" + ToDataTime + "'  ";
                }
            }
            //状态-SOA--申请订舱
            if (UserControlOrderStates.SOA)
            {
                queryString += "and $@SOA@=0";
            }
            //状态-SOD--订舱成功
            if (UserControlOrderStates.SOD)
            {
                queryString += "and $@SOD@=1";
            }
            //状态-SOS--通知客户订舱成功
            if (UserControlOrderStates.SOS)
            {
                queryString += "and $@SOS@=1";
            }
            //状态-TRKA--已向拖车公司下达委托单
            if (UserControlOrderStates.TRKA)
            {
                queryString += "and $@TRKA@=1";
            }
            //状态-SOPV--已审批SO毛利
            if (UserControlOrderStates.SOPV)
            {
                queryString += "and $@SOPV@=1";
            }
            //状态-SOC--取消订舱
            if (UserControlOrderStates.SOC)
            {
                queryString += "and $@SOC@=1";
            }
            //状态-SINC--已通知客户提供补料
            if (UserControlOrderStates.SINC)
            {
                queryString += "and $@SINC@=1";
            }
            //状态-SIR--已收到客户提供的补料
            if (UserControlOrderStates.SIR)
            {
                queryString += "and $@SIR@=1";
            }
            //状态-BLCFM--已发BL Copy让客户确认
            if (UserControlOrderStates.BLCFM)
            {
                queryString += "and $@BLCFM@=1";
            }
            //状态-BLCfmAgt--已发BL Copy让代理确认
            if (UserControlOrderStates.BLCfmAgt)
            {
                queryString += "and $@BLCfmAgt@=1";
            }
            //状态-MBLR--已收到MBL Copy
            if (UserControlOrderStates.MBLR)
            {
                queryString += "and $@MBLR@=1";
            }
            //状态-MBLD--已向承运人补料
            if (UserControlOrderStates.MBLD)
            {
                queryString += "and $@MBLD@=1";
            }
            //状态-AMS--AMS已发送
            if (UserControlOrderStates.AMS)
            {
                queryString += "and $@AMS@=1";
            }
            //状态-ISF--ISF已发送
            if (UserControlOrderStates.ISF)
            {
                queryString += "and $@ISF@=1";
            }
            //状态-ARP--应收账单已核销(支付)
            if (UserControlOrderStates.ARP)
            {
                queryString += "and $@ARP@=1";
            }
            //状态-APA--已申请运费付讫
            if (UserControlOrderStates.APA)
            {
                queryString += "and $@APA@=1";
            }
            //状态-APP--已支付应付账单
            if (UserControlOrderStates.APP)
            {
                queryString += "and $@APP@=1";
            }
            //状态-APInv--已通知承运人开发票
            if (UserControlOrderStates.APInv)
            {
                queryString += "and $@APInv@=1";
            }
            //状态-ARCfm--已向客户确认应收账单
            if (UserControlOrderStates.ARCfm)
            {
                queryString += "and $@ARCfm@=1";
            }
            //状态-ARA--已通知客户付款
            if (UserControlOrderStates.ARA)
            {
                queryString += "and $@ARA@=1";
            }
            //状态-DocS--文件已分给港口代理
            if (UserControlOrderStates.DocS)
            {
                queryString += "and $@DocS@=1";
            }
            //状态-DocH--港后代理已受理文件并分配
            if (UserControlOrderStates.DocH)
            {
                queryString += "and $@DocH@=1";
            }
            //状态-DCRev--港后代理已修订了代理账单
            if (UserControlOrderStates.DCRev)
            {
                queryString += "and $@DCRev@=1";
            }
            //状态-DcRcv--已签收了港后代理修订的代理账单
            if (UserControlOrderStates.DcRcv)
            {
                queryString += "and $@DcRcv@=1";
            }
            //状态-RBLD--已放单
            if (UserControlOrderStates.RBLD)
            {
                queryString += "and $@RBLD@=1";
            }
            //状态-RC--代理已收到放单通知
            if (UserControlOrderStates.RC)
            {
                queryString += "and $@RC@=1";
            }
            return queryString;
        }

        #endregion

    }
}
