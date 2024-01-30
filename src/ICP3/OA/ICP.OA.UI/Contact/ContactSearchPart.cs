using System;
using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.ReportCenter.ServiceInterface.DataObjects;
using ICP.ReportCenter.UI;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;


namespace ICP.OA.UI.Contact
{
    public partial class ContactSearchPart : BaseSearchPart
    {

        #region Services

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        public ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }

        #endregion

        #region 本地常量
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;
        List<ICP.ReportCenter.ServiceInterface.DataObjects.UserDetailInfo> uDetailTemp = new List<ICP.ReportCenter.ServiceInterface.DataObjects.UserDetailInfo>();
        /// <summary>
        /// 新增一个临时部门集合，过滤相同项
        /// </summary>
        List<FullDepartmentObject> DepartmentTempList = new List<FullDepartmentObject>();
        /// <summary>
        /// 用户信息临时集合，过滤相同项 
        /// </summary>
        List<ICP.ReportCenter.ServiceInterface.DataObjects.UserDetailInfo> UserDetailTempList = new List<ICP.ReportCenter.ServiceInterface.DataObjects.UserDetailInfo>();
        /// <summary>
        /// 所有部门集合
        /// </summary>
        List<FullDepartmentObject> DepartmentList = null;
        /// <summary>
        /// 有所用户信息集合
        /// </summary>
        List<ICP.ReportCenter.ServiceInterface.DataObjects.UserDetailInfo> UserDetailInfoList = null;

        #endregion

        #region 构造函数
        public ContactSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
              
                this.OnSearched = null;
                this.UserDetailInfoList = null;
                this.UserDetailTempList = null;
                this.DepartmentList = null;
                this.DepartmentTempList = null;
                this.uDetailTemp = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
              
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }
        #endregion

        #region 事件
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.btnSearch.Enabled = false;
          
            try
            {
                if (OnSearched != null)
                {
                    using (new CursorHelper())
                    {
                        OnSearched(this, GetData());
                    }
                }
            }
            finally
            {
                this.btnSearch.Enabled = true;
               
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化控件值
        /// </summary>
        public void InitControls()
        {
            //初始化部门列表
            List<OrganizationList> companys = ReportCenterHelper.GetOrganizationList;
            treeDepartment.SetSource<OrganizationList>(companys, LocalData.IsEnglish ? "EShortName" : "CShortName");
            Guid rootGuid = new Guid("701ACD43-D49B-422B-83A9-ACB56B696995");
            treeDepartment.ShowRootValue(rootGuid);//.ShowSelectedValue(Guid.Empty, "PCH 国际");

            //初始化通讯录列表
            object obj = GetData();
            ContactWorkItem contactWorkItem = Workitem as ContactWorkItem;
            contactWorkItem.OnSearchPartSearched(contactWorkItem.reportViewBase, obj);
        }

        public override object GetData()
        {
            try
            {
                //获取通讯录所有信息 
                //ContactObject result = UserService.GetAllContactList(GetSelectNode());
                //FullDepartmentObject firstFullDepartmentObject = new FullDepartmentObject();
                ////部门集合
                //DepartmentList = result.FullDepartmentList;
                //if (DepartmentList != null && DepartmentList.Count > 0)
                //{
                //    firstFullDepartmentObject = DepartmentList[0];
                //}
                ////员工详细信息集合
                //UserDetailInfoList = result.UserDetailInfoList;

                //UserDetailTempList.Clear();
                //DepartmentTempList.Clear();
                //uDetailTemp.Clear();
                ////过滤通讯录信息
                //FixContactData(DepartmentList, firstFullDepartmentObject);
                //DepartmentList.Clear();
                //UserDetailInfoList.Clear();

                //设置报表模板和数据
                //ReportData rd = new ReportData();
                //rd.IsLocalReport = true;
                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("OrganizationID", GetSelectNode().ToString()));
                paramList.Add(new ReportParameter("IsEnglish", LocalData.IsEnglish ? "1" : "0"));
                ReportData rd = new ReportData { Parameters = paramList, ReportName = "ContactDetailReport" };
          
                //rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.ContactDetailReport.rdlc";
                //List<ReportDataSource> ds = new List<ReportDataSource>();
                //ds.Add(new ReportDataSource("UserDetailInfo", uDetailTemp));
                //rd.DataSource = ds;
                return rd;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, (LocalData.IsEnglish ? "Get Report Data Failed." : "获取报表数据失败.") + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取选择的部门ID
        /// </summary>
        /// <returns></returns>
        private Guid GetSelectNode()
        {
            Guid OrganizationID;

            object obj = treeDepartment.EditValue;
            if (obj == null)
            {
                OrganizationID = Guid.Empty;
            }
            else
            {
                OrganizationID = new Guid(treeDepartment.EditValue.ToString());
            }

            return OrganizationID;
        }


        private void FixContactData(List<FullDepartmentObject> fullDepartmentList, FullDepartmentObject firstFullDepartmentObejct)
        {
            foreach (FullDepartmentObject departmentObj in fullDepartmentList)
            {
                //将未添加的部门添加到集合中
                List<FullDepartmentObject> lstDpt = DepartmentTempList.FindAll(f => f.ID == departmentObj.ID);
                if (lstDpt == null || lstDpt.Count == 0)
                    DepartmentTempList.Add(departmentObj);
                else
                    continue;

                //找到当前公司或部门下的所有员工
                List<ICP.ReportCenter.ServiceInterface.DataObjects.UserDetailInfo> _userDetailInfo = UserDetailInfoList.FindAll(u => u.ParentID == departmentObj.ID);
                if (_userDetailInfo != null && _userDetailInfo.Count > 0)
                {
                    foreach (ICP.ReportCenter.ServiceInterface.DataObjects.UserDetailInfo uDetail in _userDetailInfo)
                    {
                        UserDetailTempList.Add(uDetail);
                        //找到某部门下从一个员工开始后就不需要显示部门名称了
                        List<ICP.ReportCenter.ServiceInterface.DataObjects.UserDetailInfo> list = UserDetailTempList.FindAll(t => t.ParentID == uDetail.ParentID && departmentObj.Type == 4);
                        if (list.Count > 1)
                            uDetail.Department = string.Empty;
                        else
                        {
                            string obj = string.Empty, area = string.Empty, department = string.Empty;
                            FixUserDetail(firstFullDepartmentObejct.FullName, departmentObj.FullName, ref area, ref department);
                            uDetail.Department = department;
                            uDetail.Area = area;
                            uDetail.CompanyAddress = firstFullDepartmentObejct.CompanyAddress;
                            uDetail.CompanyFax = firstFullDepartmentObejct.CompanyFax;
                            uDetail.CompanyTel = firstFullDepartmentObejct.CompanyTel;
                        }
                        uDetailTemp.Add(uDetail);
                    }
                }
                //找到部门下的子部门
                List<FullDepartmentObject> _fullDepartmentObject = DepartmentList.FindAll(f => f.ParentID == departmentObj.ID);
                if (_fullDepartmentObject.Count > 0)
                {
                    FixContactData(_fullDepartmentObject, firstFullDepartmentObejct);
                }
            }
        }
        /// <summary>
        /// 过滤部门名称 
        /// </summary>
        /// <param name="firstFullName"></param>
        /// <param name="_FullName"></param>
        /// <param name="area"></param>
        /// <param name="department"></param>
        void FixUserDetail(string firstFullName, string _FullName, ref string area, ref string department)
        {
            string obj = string.Empty;
            //过滤第一个节点名称
            if (!string.IsNullOrEmpty(firstFullName))
            {
                if (firstFullName.EndsWith("PCH 国际") || firstFullName.EndsWith("PCH International"))
                {
                    obj = _FullName;
                }
                else
                {
                    if (!string.IsNullOrEmpty(_FullName))
                    {
                        if (!_FullName.Equals(firstFullName))
                        {
                            obj = _FullName.Substring(firstFullName.Length + 2, _FullName.Length - firstFullName.Length - 2);
                        }
                        else
                        {
                            obj = _FullName;
                        }
                    }
                }

                FillOrganization(obj, ref area, ref department);
            }
        }

        /// <summary>
        /// 过滤角色名称
        /// </summary>
        /// <param name="dptFullName"></param>
        /// <param name="area"></param>
        /// <param name="department"></param>
        private void FillOrganization(string dptFullName, ref string area, ref string department)
        {
            if (string.IsNullOrEmpty(dptFullName)) return;

            if ((dptFullName.EndsWith("部") || dptFullName.ToLower().EndsWith("department") || dptFullName.ToLower().EndsWith("dept")))
            {
                string[] arrs = dptFullName.Split(new char[] { '>' });
                if (arrs != null && arrs.Length > 1 && dptFullName.Length > department.Length)
                {
                    department = arrs[arrs.Length - 1];
                    area = dptFullName.Substring(0, dptFullName.Length - department.Length - 2);
                }
                else
                {
                    if (dptFullName.Equals("物流部") || dptFullName.ToLower().Equals("logistics department") || dptFullName.ToLower().Equals("logistics dept"))
                    {
                        area = dptFullName;
                    }
                    else
                    {
                        department = dptFullName;
                    }
                }
            }
            else
            {
                area = dptFullName;
            }
        }

        #endregion

    }
}
