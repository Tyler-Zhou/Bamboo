
//-----------------------------------------------------------------------
// <copyright file="Company.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using ICP.WF.ServiceInterface;
    using ICP.WF.ServiceInterface.DataObject;
    using ICP.Sys.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Client;

    /// <summary>
    /// 组织数据源
    /// </summary>
    [DefaultProperty("ComboBoxType")]
    [ToolboxBitmap(typeof(LWCompanys), "Resources.DepartMent.ico")]
    [SRDescription("CompanyBindingSourceDesc"), SRTitle("CompanyBindingSourceTitle")]
    public class LWCompanys : System.Windows.Forms.BindingSource, IInitiDataService, IBindingSourceTypeService
    {
        #region 构造函数

        public LWCompanys()
        {
            this.DataSource = new List<OrganizationList>();
        }

        #endregion

        #region IInitiDataService接口实现

        public void InitData(IServiceContainerManager containerService, IDictionary<string, object> pams)
        {
            if (this.DesignMode == false)
            {
                if (containerService == null) return;
                IWorkFlowExtendService extendService = (IWorkFlowExtendService)containerService.Get(typeof(IWorkFlowExtendService));
                if (extendService != null)
                {
                    string departmentCode = string.Empty;
                    string companyCode = string.Empty;

                    if (pams != null)
                    {
                        departmentCode = pams[WWFConstants.ProposerDepartmentIDCode].ToString();
                        if (pams[WWFConstants.ProposerCompanyIDCode] != null)
                        {
                            companyCode = pams[WWFConstants.ProposerCompanyIDCode].ToString();
                        }
                    }
                    if (comboBoxType == DataScope.Department)
                    {
                        this.DataSource = extendService.GetOrganizationListByDepartment(LocalData.UserInfo.LoginID);
                    }
                    else if (comboBoxType == DataScope.Company)
                    {
                        this.DataSource = extendService.GetOrganizationListByUserCompany(LocalData.UserInfo.LoginID);
                    }
                    else if (comboBoxType == DataScope.AllCompany)
                    {
                        this.DataSource = extendService.GetOrganizationListByCompany();
                    }
                    else if (comboBoxType == DataScope.CompanyList)
                    {
                        this.DataSource = extendService.GetWFAllOffice();
                    }
                    else
                    {
                        this.DataSource = extendService.GetOrganizationList(null, null, true, 0);
                    }
                }
                else
                {
                    this.DataSource = new List<OrganizationList>();
                }
            }
        }

        #endregion

        #region 自定义属性
        ///// <summary>
        ///// 名称
        ///// </summary>
        //[SRDisplayName("Name"), ICPBrowsable(true), SRDescription("Name"), SRCategory("Base")]
        //public new string Name
        //{
        //    get { return base.Name; }
        //    set { base.Name = value; }
        //}

        DataScope comboBoxType = DataScope.All;
        [SRCategory("Custom"), SRDescription("CompanySourceTypeDescription")]
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true), SRDisplayName("ComboBoxType")]
        public DataScope ComboBoxType
        {
            get { return comboBoxType; }
            set
            {
                comboBoxType = value;
            }
        }
        #endregion

        #region 屏蔽属性


        [Browsable(false)]
        public new object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                base.DataSource = value;
            }
        }


        [Browsable(false)]
        public new string DataMember
        {
            get
            {
                return base.DataMember;
            }
            set
            {
                base.DataMember = value;
            }
        }

        [Browsable(false)]
        public override string Filter
        {
            get
            {
                return base.Filter;
            }
            set
            {
                base.Filter = value;
            }
        }

        [Browsable(false)]
        public override bool AllowNew
        {
            get
            {
                return base.AllowNew;
            }
            set
            {
                base.AllowNew = value;
            }
        }

        [Browsable(false)]
        public new string Sort
        {
            get
            {
                return base.Sort;
            }
            set
            {
                base.Sort = value;
            }
        }
        #endregion

        #region IBindingSourceTypeService 接口实现
        [Browsable(false)]
        public System.Type DataType
        {
            get
            {
                return typeof(OrganizationList);
            }
        }

        #endregion

    }
}
