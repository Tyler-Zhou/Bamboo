using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FRM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using System.IO;

namespace ICP.FRM.UI.SearchRate
{
    public partial class SearchOceanRemark : BaseEditPart
    {
        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOceanPriceFileClientService OceanPriceFileClientService
        {
            get
            {
                return ServiceClient.GetClientService<IOceanPriceFileClientService>();
            }
        }

        #endregion

        #region init
        public SearchOceanRemark()
        {
            InitializeComponent();
            Disposed += delegate
            {
                _CurrentInfo = null;
                gridControlFileList.DataSource = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            
            };
        }

        #endregion


        #region EventSubscription
        ShowPartMode _ShowPartMode = ShowPartMode.File;
        
        //[EventSubscription(SearchOceanEventBrokerConstants.EventBroker_ShowRemark )]
        [CommandHandler(SearchOceanCommandConstants.Command_ShowRemark)]
        public void Command_ShowRemark(object o, EventArgs e)
        {
            if (_CurrentInfo == null || _CurrentInfo.RemarkDetails.IsNullOrEmpty()) return;
            txtRemark.Rtf = _CurrentInfo.RemarkDetails;

            txtRemark.BringToFront();
            panelFile.SendToBack();

            _ShowPartMode = ShowPartMode.Remark;
        }


        Guid _LoadedOceanID = Guid.Empty;
        //[EventSubscription(SearchOceanEventBrokerConstants.EventBroker_ShowAttachment)]
        [CommandHandler(SearchOceanCommandConstants.Command_ShowAttachment)]
        public void Command_ShowAttachment(object o, EventArgs e)
        {
            if (_CurrentInfo == null) return;

            if (_LoadedOceanID.IsNullOrEmpty() && _CurrentInfo != null && _CurrentInfo.OceanID.IsNullOrEmpty() == false)
            {
                List<OceanFileList> list = null;
                if (_CurrentInfo.Type == SearchRateType.Inquiry)
                {
                    list = new List<OceanFileList>();
                }
                else
                {
                    list = OceanPriceFileClientService.GetOceanFileList(_CurrentInfo.OceanID, LocalData.UserInfo.LoginID);
                }
                _LoadedOceanID = _CurrentInfo.OceanID;
                bsList.DataSource  = list;
                bsList.ResetBindings(false);
            }

            panelFile.BringToFront();
            txtRemark.SendToBack();

            _ShowPartMode = ShowPartMode.File;
        }

        #endregion

        #region 本地成员

        /// <summary>
        /// 主数据源
        /// </summary>
        SearchOceanBaseInfo _CurrentInfo = null;

        OceanFileList CurrentRow
        {
            get { return bsList.Current as OceanFileList; }
            set
            {
                OceanFileList current = CurrentRow;
                if (current != null) current = value;
            }
        }

        private enum ShowPartMode
        {
            Remark,
            File
        }

        #endregion

        #region editPart 成员

        public override object DataSource
        {
            get
            {
                return _CurrentInfo;
            }
            set
            {
                _CurrentInfo = value as SearchOceanBaseInfo;
                if (Visible)
                {
                    if (_ShowPartMode == ShowPartMode.File)
                    {
                        List<OceanFileList> list = null;
                        if (_CurrentInfo.Type == SearchRateType.Inquiry)
                        {
                            list = new List<OceanFileList>();
                        }
                        else
                        {
                            list = OceanPriceFileClientService.GetOceanFileList(_CurrentInfo.OceanID, LocalData.UserInfo.LoginID);
                        }
                        _LoadedOceanID = _CurrentInfo.OceanID;
                        bsList.DataSource = list;
                        bsList.ResetBindings(false);
                    }
                    else
                    {
                        txtRemark.Rtf = _CurrentInfo.RemarkDetails;
                    }
                }
                else
                {
                    _LoadedOceanID = Guid.Empty;
                }
            }
        }

        #endregion

        #region 事件

        private void gvFileList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks == 2 || e.Column != colFileName) return;
            DownloadFile();
        }

        private void barDownLoad_ItemClick(object sender, ItemClickEventArgs e)
        {
            DownloadFile();
        }

        private void DownloadFile()
        {
            if (CurrentRow == null) return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = CurrentRow.FileName;
            if (sfd.ShowDialog() == DialogResult.OK && string.IsNullOrEmpty(sfd.FileName) == false)
            {
                try
                {
                    byte[] content = OceanPriceFileClientService.DownloadOceanFileList(CurrentRow.FileID);

                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write);
                    fs.Write(content, 0, content.Length);
                    fs.Close();

                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
            }
        }

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentRow == null) barDownLoad.Enabled = false;
            else barDownLoad.Enabled = true;
        }

        #endregion

        

        


    }
}
