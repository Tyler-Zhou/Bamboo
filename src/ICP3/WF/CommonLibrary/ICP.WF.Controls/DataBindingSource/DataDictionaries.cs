﻿
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

    /// <summary>
    /// 数据字典数据源
    /// </summary>
    [DefaultProperty("ComboBoxType")]
    [ToolboxBitmap(typeof(LWBanks), "Resources.database.bmp")]
    [SRDescription("DataDictionariesSourceDesc"), 
    SRTitle("DataDictionariesSourceTitle")]
    public class ICPDataDictionaries : System.Windows.Forms.BindingSource, IInitiDataService, IBindingSourceTypeService
    {
        #region 构造函数

        public ICPDataDictionaries()
        {

            this.DataSource = new List<DataDictionaryList>();
        }

        #endregion

        #region IInitiDataService接口实现

        public void InitData(IServiceContainerManager containerService, IDictionary<string, object> pams)
        {
            if (!this.DesignMode )
            {
                if (containerService == null) return;

                IWorkFlowExtendService extendService = (IWorkFlowExtendService)containerService.Get(typeof(IWorkFlowExtendService));
                if (extendService != null)
                {
                    this.DataSource = extendService.GetDataDictionaryList(ComboBoxType);
                }
                else
                {
                    this.DataSource = new List<DataDictionaryList>();
                }
            }
        }
        DataDictionaryType comboBoxType = DataDictionaryType.None;
        [SRCategory("Custom"), SRDescription("DataDictionaryTypeDescription")]
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true), SRDisplayName("ComboBoxType")]
        public DataDictionaryType ComboBoxType
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
                return typeof(DataDictionaryList);
            }
        }
        #endregion

    }
}
