using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FRM.ServiceInterface;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;

namespace ICP.FRM.UI.InquireRates
{
    public partial class InquireTruckingRatesDiscussingPart : XtraUserControl
    {
        #region 服务

        WorkItem Workitem = null;

        IInquireRatesService InquireRatesService
        {
            get
            {
                return ServiceClient.GetService<IInquireRatesService>();
            }
        }
       
       #endregion

        #region 本地变量

        List<InquireDiscussing> _curruntDiscussingList = null;

        #endregion

        #region 构造函数

        public InquireTruckingRatesDiscussingPart()
        {
            InitializeComponent();
            Enabled = false;
            Disposed += delegate {
                _curruntDiscussingList = null;
                CurrentInquierRate = null;
                
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        #endregion     

        #region 属性

        //public string FormType
        //{
        //    get;
        //    set;
        //}
        public ClientInquierTruckingRate CurrentInquierRate = null;

        #endregion

        #region IChildPart 成员

        //public object DataSource
        //{
        //    //get
        //    //{
        //    //    //List<TruckingImportFeeList> list = new List<TruckingImportFeeList>();
        //    //    //if (feePartAR.DataSource != null)
        //    //    //    list.AddRange(feePartAR.DataSource as List<TruckingImportFeeList>);

        //    //    //return list;
        //    //    return null;
        //    //}
        //    set 
        //    { 
        //        if (value!= null)
        //        {
        //            this.discussingPart.SetSource(value as List<InquireDiscussing>);
        //        }
        //    }
        //}

        //public void SetSource(object value)
        //{
        //    if (DesignMode || value == null) return;
        //    List<InquireDiscussing> list = value as List<InquireDiscussing>;
        //    this.discussingPart.SetSource(list);
        //}

        //[EventSubscription(InquireRatesCommandConstants.Command_AddDiscussing)]
        //public void Command_AddDiscussing(object sender, DataEventArgs<List<InquireDiscussing>> e)
        //{
        //    List<InquireDiscussing> discussingListNew = e.Data as List<InquireDiscussing>;
        //    if (discussingListNew != null && discussingListNew.Count > 0)
        //    {
        //        if (_curruntDiscussingList == null || _curruntDiscussingList.Count == 0)
        //        {
        //            _curruntDiscussingList = discussingListNew;
        //        }
        //        else
        //        {
        //            _curruntDiscussingList.AddRange(discussingListNew);
        //            _curruntDiscussingList.OrderBy(i=> i.SentTime);
        //        }

        //        this.discussingPart.SetSource(_curruntDiscussingList);
        //    }
        //}
        [EventSubscription(InquireRatesCommandConstants.Command_Transit)]
        public void Command_Transit(object sender, DataEventArgs<object> e)
        {
            try
            {
                _curruntDiscussingList = InquireRatesService.GetInquireRateDiscussingList(CurrentInquierRate.ID, LocalData.UserInfo.LoginID);
                discussingPart.SetSource(_curruntDiscussingList.OrderByDescending(i => i.SentTime));
                InquireRatesService.ChangeDiscussingToHadRead(CurrentInquierRate.ID, LocalData.UserInfo.LoginID);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        [EventSubscription(InquireRatesCommandConstants.Command_RefreshTruckingDiscussingPart)]
        public void RefreshTruckingDiscussingPart(object sender, DataEventArgs<object> e)
        {
            try
            {
                _curruntDiscussingList = InquireRatesService.GetInquireRateDiscussingList(CurrentInquierRate.ID, LocalData.UserInfo.LoginID);
                discussingPart.SetSource(_curruntDiscussingList.OrderByDescending(i => i.SentTime));
                //inquireRatesService.ChangeDiscussingToHadRead(CurrentInquierRate.ID, LocalData.UserInfo.LoginID);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        public void SetData(WorkItem workitem, ClientInquierTruckingRate inquierTruckingRate)
        {
            Workitem = workitem;
            CurrentInquierRate = inquierTruckingRate;
            if (inquierTruckingRate == null)
            {               
                discussingPart.SetSource(null);
                //this.discussingPart.SetService(workitem, null);
                Enabled = false;
                return;
            }
            
            Enabled = true;        
            try
            {
                _curruntDiscussingList = InquireRatesService.GetInquireRateDiscussingList(inquierTruckingRate.ID, LocalData.UserInfo.LoginID);
                discussingPart.SetSource(_curruntDiscussingList.OrderByDescending(i => i.SentTime));
                if (LocalData.UserInfo.LoginID == inquierTruckingRate.InquireByID)
                {
                    discussingPart.SetService(workitem, inquierTruckingRate);
                }
                else if (LocalData.UserInfo.LoginID == inquierTruckingRate.RespondByID)
                {
                    discussingPart.SetService(workitem, inquierTruckingRate);
                }

                if (inquierTruckingRate.HasUnRead)
                {
                    InquireRatesService.ChangeDiscussingToHadRead(inquierTruckingRate.ID, LocalData.UserInfo.LoginID);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        } 

        #endregion
    }
}
