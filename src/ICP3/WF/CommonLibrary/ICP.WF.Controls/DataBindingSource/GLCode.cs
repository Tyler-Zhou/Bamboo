
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
    using System;
    using ICP.FAM.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Common.ServiceInterface;

    /// <summary>
    /// 组织数据源
    /// </summary>
    [DefaultProperty("ComboBoxType")]
    [ToolboxBitmap(typeof(ICPGLCode), "Resources.database.bmp")]
    [SRDescription("AccountSubBindingSource"),
    SRTitle("AccountSubBindingSourceTitle")]
    public class ICPGLCode : System.Windows.Forms.BindingSource, IInitiDataService, IBindingSourceTypeService
    {
        #region 服务

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        #region 构造函数

        public ICPGLCode()
        {

            this.DataSource = new List<SolutionGLCodeList>();
        }

        #endregion

        #region IInitiDataService接口实现

        public void InitData(IServiceContainerManager containerService, IDictionary<string, object> pams)
        {
            if (!this.DesignMode)
            {
                if (containerService == null) return;

                Guid solutionID = Guid.Empty;
                if (pams != null)
                {
                    try
                    {
                        solutionID = (Guid)pams[WWFConstants.InitDataId];
                    }
                    catch
                    {
                        ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                        if (configureInfo != null)
                        {
                            solutionID = configureInfo.SolutionID;
                        }
                    }
                }


                this.DataSource = ConfigureService.GetSolutionGLCodeListNew(solutionID, new Guid[1] { LocalData.UserInfo.DefaultCompanyID }, string.Empty, string.Empty, GLCodeType.Unknown, true, LocalData.IsEnglish);
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
                return typeof(SolutionGLCodeList);
            }
        }
        #endregion

    }
}
