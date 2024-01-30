using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FRM.UI.InquireRates
{
    [ToolboxItem(false)]
    public partial class DiscussingPart : XtraUserControl
    {
        public DiscussingPart()
        {
            InitializeComponent();
            Utility.SetXraGridViewColWordrap(gvMain, "DiscussingFromName");
            Utility.SetXraGridViewColWordrap(gvMain, "BizContent");
            Utility.SetXraGridViewColWordrap(gvMain, "BizSentTime");
            Disposed += delegate
            {
                _currentInquireRate = null;
                gcMain.DataSource = null;
                bsList.DataSource = null;
                bsList.Dispose();
               
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            
            
            };
            
        }

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

        #region  本地变量  
   
        ClientBaseInquireRate _currentInquireRate = null;

        #endregion

       

        #region 发送消息

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (_currentInquireRate == null) return;
            if (txtContent.Text.Trim() == string.Empty) return;

            Guid? toID = Guid.Empty;
            if (LocalData.UserInfo.LoginID == _currentInquireRate.InquireByID)
            {
                toID = _currentInquireRate.RespondByID;
            }
            else if (LocalData.UserInfo.LoginID == _currentInquireRate.RespondByID)
            {
                toID = _currentInquireRate.InquireByID;
            }

            try
            {
                InquireRatesService.SendDiscussings(Guid.Empty,
                       _currentInquireRate.ID,
                       LocalData.UserInfo.LoginID,
                       toID,
                       DateTimeOffset.Now,
                       txtContent.Text.Trim());

                List<InquireDiscussing> dataSource = InquireRatesService.GetInquireRateDiscussingList(_currentInquireRate.ID, LocalData.UserInfo.LoginID);
                bsList.DataSource = dataSource.OrderByDescending(i=> i.SentTime); 
                bsList.ResetBindings(false);
                txtContent.Text = string.Empty;
                //List<InquireDiscussing> dataSource = bsList.DataSource as List<InquireDiscussing>;
                //InquireDiscussing newDiscussing = new InquireDiscussing();
                //newDiscussing.ID = result.ID;
                //newDiscussing.u .SentTime = result.UpdateDate.Value;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        #endregion

        #region IChildPart 成员
       
        public void SetSource(object value)
        {
            bsList.DataSource = value;
            bsList.ResetBindings(false);
        }

        //public void SetService(WorkItem workitem, string toName)
        //{
        //    Workitem = workitem;
        //    labToMan.Text = "To " + toName + ":";
        //}
        public void SetService(WorkItem workitem, ClientBaseInquireRate inquireRate)
        {
            Workitem = workitem;
            _currentInquireRate = inquireRate;
            if (LocalData.UserInfo.LoginID == inquireRate.InquireByID)
            {
                //this.discussingPart.SetService(workitem, inquireRate.RespondByName);
                labToMan.Text = "To " + inquireRate.RespondByName + ":";
            }
            else if (LocalData.UserInfo.LoginID == inquireRate.RespondByID)
            {
                //this.discussingPart.SetService(workitem, inquireRate.InquireByName);
                labToMan.Text = "To " + inquireRate.InquireByName + ":";
            }
        }

        #endregion

        private void txtContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.Enter)
            {
                btnSend_Click(null, null);
            }
        }

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (bsList.DataSource == null) return;
            if (e.RowHandle == -1) return;

            InquireDiscussing item = (InquireDiscussing)bsList[e.RowHandle];
            e.Appearance.ForeColor = item.IsRead == false ? Color.Blue : Color.Black;

        }

        private void gvMain_CalcRowHeight(object sender, RowHeightEventArgs e)
        {
            if (bsList.DataSource == null) return;
            if (e.RowHandle == -1) return;

            InquireDiscussing item = (InquireDiscussing)bsList[e.RowHandle];
            if (item.Content.IndexOf("the original data is logged as below") > 1)
                e.RowHeight = 35;
        }
    }
}
