
//-----------------------------------------------------------------------
// <copyright file="Currency.cs" company="LongWin">
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
    using ICP.Common.ServiceInterface.DataObjects;

    /// <summary>
    /// 币种数据源    /// </summary>
    [ToolboxBitmap(typeof(LWCurrencys), "Resources.database.bmp")]
    [SRDescriptionAttribute("CurrencyBindingSource"), SRTitle("CurrencyBindingSourceTitle")]
    public class LWCurrencys : System.Windows.Forms.BindingSource, IInitiDataService, IBindingSourceTypeService
    {
        #region 构造函数

        public LWCurrencys()
        {
            this.DataSource = new List<ICP.Common.ServiceInterface.DataObjects.CurrencyList>();
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
                    this.DataSource = extendService.GetCurrencys();
                }
                else
                {
                    //根据代理接口从服务获取数据
                    this.DataSource = new List<ICP.Common.ServiceInterface.DataObjects.CurrencyList>();
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
                return typeof(ICP.Common.ServiceInterface.DataObjects.CurrencyList);
            }
        }

        #endregion
    }
}
