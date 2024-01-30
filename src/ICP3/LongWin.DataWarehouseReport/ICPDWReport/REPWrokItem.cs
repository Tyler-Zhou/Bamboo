using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.ObjectBuilder;
using Agilelabs.Framework.Service;
using Agilelabs.Framework.Client;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.SmartParts;

using LongWin.DataWarehouseReport.ServiceInterface;

namespace LongWin.DataWarehouseReport.WinUI
{
    public class REPWrokItem : WorkItem
    {
        

        Microsoft.Practices.CompositeUI.WorkItem _rootWorkItem;
        public REPWrokItem([ServiceDependency] WorkItem rootWorkItem)
        {
            _rootWorkItem = rootWorkItem;
        }

        ISystemService _systemService;
        [ServiceDependency]
        public ISystemService SystemService
        {
            set { _systemService = value; }
        }

        IREPBaseDataService _repBaseDataService;
        [ServiceDependency]
        public IREPBaseDataService RepBaseDataService
        {
            set { this._repBaseDataService = value; }
        }

        //IUIElementLoader _uiElementLoader;
        //[ServiceDependency]
        //public IUIElementLoader UIElementLoader
        //{
        //    set
        //    {
        //        _uiElementLoader = value;

        //    }
        //}

        internal void Show()
        {
            //_uiElementLoader.LoadIn(this);
        }


        public void InitREPData()
        {
            if (Utility.ReportServerInfos == null )
            {
                Utility.ReportServerInfos = this._repBaseDataService.GetReportServerUrl();
            }
        }
    }

    public enum ReportTypes
    {
        /// <summary>
        /// 运出口
        /// </summary>
        SEAE,
        /// <summary>
        /// 海运进口
        /// </summary>
        SEAI,
        /// <summary>
        /// 空运出口
        /// </summary>
        AIRE,
        /// <summary>
        /// 空运进口
        /// </summary>
        AIRI,
        /// <summary>
        /// 其它业务
        /// </summary>
        OTHER,
        /// <summary>
        /// 集运业务
        /// </summary>
        CTM,
        /// <summary>
        /// 报关业务
        /// </summary>
        ACM

    }
}
