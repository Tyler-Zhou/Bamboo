
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

    /// <summary>
    /// 组织数据源
    /// </summary>
    [DefaultProperty("ComboBoxType")]
    [ToolboxBitmap(typeof(LWCompanys), "Resources.database.bmp")]
    [SRDescription("JobBindingSourceDesc"), 
    SRTitle("JobBindingSourceTitle")]
    public class LWJobs : System.Windows.Forms.BindingSource, IInitiDataService, IBindingSourceTypeService
    {
        #region 构造函数

        public LWJobs()
        {

            this.DataSource = new List<JobList>();
        }

        #endregion

        #region IInitiDataService接口实现

        public void InitData(IServiceContainerManager containerService, IDictionary<string, object> pams)
        {
            if (!this.DesignMode )
            {
                if (containerService == null) return;

                Guid departmentId = Guid.Empty;
                if (pams != null)
                {
                    try
                    {
                        departmentId = (Guid)pams[WWFConstants.InitDataId];
                    }
                    catch
                    {
                        departmentId = Guid.Empty;
                    }


                }

                IWorkFlowExtendService extendService = (IWorkFlowExtendService)containerService.Get(typeof(IWorkFlowExtendService));
                if (extendService != null)
                {
                    if (departmentId == Guid.Empty)
                    {

                        this.DataSource = extendService.GetJobList(null, null, true, 0);
                    }
                    else
                    {
                        this.DataSource = extendService.GetJobListByOrg(departmentId);
                    }
                }
                else
                {
                    //根据代理接口从服务获取数据

                    this.DataSource = new List<JobList>();
                }
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
                return typeof(JobList);
            }
        }
        #endregion

    }
}
