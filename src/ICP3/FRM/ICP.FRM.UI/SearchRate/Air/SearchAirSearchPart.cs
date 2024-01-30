using System;
using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Client;
using System.ComponentModel;

namespace ICP.FRM.UI.SearchRate
{
    [ToolboxItem(false)]
    public partial class SearchAirSearchPart : SearchRateSearchPart
    {
        public SearchAirSearchPart()
        {
            Disposed += delegate
            {
                OnSearched = null;
            };
        }
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            //ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");

            try
            {
                PageList list = SearchRatesService.GetSearchAirList(
                                                     LocalData.UserInfo.LoginID,
                                                     searchOceanParameter.ShiplineIDs,
                                                     searchOceanParameter.CarrierIDs,
                                                     searchOceanParameter.Pol,
                                                     searchOceanParameter.Pod,
                                                     searchOceanParameter.DurationStart,
                                                     searchOceanParameter.DurationEnd,
                                                     searchOceanParameter.Status,
                                                     searchOceanParameter.DataPageInfo
                                                    );

                return list;

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return null;
            }


        }

        public override event SearchResultHandler OnSearched;
        protected override void AfterClickSearch()
        {
            if (OnSearched != null)
            {
                PageList list = GetData() as PageList;
                if (list != null && list.DataPageInfo != null)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "total search " + list.DataPageInfo.TotalCount.ToString() + " data." : "总共查询到 "
                                                + list.DataPageInfo.TotalCount.ToString() + " 条数据.");
                }
                OnSearched(this, list);
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="data"></param>
        public override void RaiseSearched(object data)
        {
            DataPageInfo dataPageInfo = data as DataPageInfo;
            searchOceanParameter.DataPageInfo = dataPageInfo;
            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    OnSearched(this, GetData());
                }
            }
        }

        protected override Dictionary<Guid, string> GetCarrierList()
        {
            Dictionary<Guid, string> list = new Dictionary<Guid, string>();

            Guid[] shipLineIDs = GetShipLineIDs;
            if (shipLineIDs != null && shipLineIDs.Length > 0)
                list = SearchRatesService.GetAirRateCarrierList(GetStatus, base.dateMonthControl1.From, base.dateMonthControl1.To, shipLineIDs);

            return list;
        }
    }
}
