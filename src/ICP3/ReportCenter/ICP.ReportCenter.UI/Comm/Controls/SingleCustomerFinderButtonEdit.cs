using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using System.ComponentModel;
using System.Drawing.Design;

namespace ICP.ReportCenter.UI.Comm.Controls
{   
    /// <summary>
    /// 带单选客户搜索器的ButtonEdit
    /// </summary>
    public class SingleCustomerFinderButtonEdit : DevExpress.XtraEditors.ButtonEdit
    {
        private string finderName = CommonFinderConstants.CustoemrFinder;
        /// <summary>
        /// 关联的搜索器名称
        /// </summary>
        [Bindable(true)]
        [Editor(typeof(FinderNameDesignProperty), typeof(UITypeEditor))]
        public string FinderName
        {
            get
            {
               return this.finderName;
            }
            set
            {
                this.finderName = value;
            }
        }
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (!LocalData.IsDesignMode)
            {
              IDisposable customerFinder=  SearchBoxAdapter.RegisterSingleSearchBox(DataFindClientService, this, this.FinderName);
              this.Disposed += delegate
              {
                  if (customerFinder != null)
                  {
                      customerFinder.Dispose();
                      customerFinder = null;
                  }
              };
            }
        }
    }
}
