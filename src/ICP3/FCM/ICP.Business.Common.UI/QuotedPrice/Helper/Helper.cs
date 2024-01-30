using System;
using System.Collections.Generic;
using System.Linq;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.Business.Common.UI.QuotedPrice
{
    /// <summary>
    /// 报价UI数据帮助类
    /// </summary>
    public class QuotedPriceUIDataHelper : Controller, IDisposable
    {
        /// <summary>
        /// 基础数据服务
        /// </summary>
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }


        List<ShippingLineList> _ShippingLines = null;
        /// <summary>
        /// 航线集合
        /// </summary>
        public List<ShippingLineList> ShippingLines
        {
            get
            {
                if (_ShippingLines != null) return _ShippingLines;
                _ShippingLines = TransportFoundationService.GetShippingLineList(string.Empty, string.Empty, true, 0);
                _ShippingLines = (from d in _ShippingLines where d.ParentID == d.ID select d).ToList();
                return _ShippingLines;
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _ShippingLines = null;
        }

        #endregion
    }
}
