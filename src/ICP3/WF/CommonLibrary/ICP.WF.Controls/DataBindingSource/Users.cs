//-----------------------------------------------------------------------
// <copyright file="User.cs" company="LongWin">
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
    using ICP.Sys.ServiceInterface.DataObjects;
    using ICP.WF.ServiceInterface.DataObject;
    using System;

    /// <summary>
    /// 用户数据源
    /// </summary>
    [DefaultProperty("ComboBoxType")]
    [ToolboxBitmap(typeof(LWUsers), "Resources.user.ico")]
    [SRDescriptionAttribute("UserBindingSource"), SRTitle("UserBindingSourceTitle")]
    public class LWUsers : System.Windows.Forms.BindingSource, IInitiDataService, IBindingSourceTypeService
    {
        #region 构造函数


        public LWUsers()
        {
            this.DataSource = new List<UserList>();
        }

        #endregion

        #region IInitiDataService接口实现

        public void InitData(IServiceContainerManager containerService,IDictionary<string,object> pams)
        {
            if (this.DesignMode == false)
            {
                if (containerService == null) return;
                IWorkFlowExtendService extendService = (IWorkFlowExtendService)containerService.Get(typeof(IWorkFlowExtendService));
                if (extendService != null)
                {
                    string departmentID = string.Empty;
                    string companyID = string.Empty;
                    if (pams != null)
                    {
                        departmentID = pams[WWFConstants.ProposerDepartmentIDCode].ToString();
                        if (pams[WWFConstants.ProposerCompanyIDCode] != null)
                        {
                            companyID = pams[WWFConstants.ProposerCompanyIDCode].ToString();
                        }
                    }

                    if (this.comboBoxType == DataScope.Department && Utility.IsGuid(departmentID))
                    {
                        this.DataSource = extendService.GetUserList(null, null, null, null, null, new Guid(departmentID), true, 0);
                    }
                    else if (this.comboBoxType == DataScope.Company && Utility.IsGuid(companyID))
                    {
                        this.DataSource = extendService.GetUserList(null, null, null, null, null, new Guid(companyID), true, 0);
                    }
                    else
                    {
                        this.DataSource = extendService.GetUserList(null, null, null, null, null, null, true, 0);
                    }
                }
                else
                {
                    //根据代理接口从服务获取数据

                    this.DataSource = new List<UserList>();
                }
            }
        }

        #endregion

        #region 自定义属性


        DataScope comboBoxType = DataScope.All;
        [SRCategory("Custom"), SRDescription("UserSourceTypeDescription")]
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
                return typeof(UserList);
            }
        }

        #endregion
    }
}
