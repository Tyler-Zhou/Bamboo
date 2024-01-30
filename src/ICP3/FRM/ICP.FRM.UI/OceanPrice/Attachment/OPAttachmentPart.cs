using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace ICP.FRM.UI.OceanPrice
{
    /// <summary>
    /// 合约附件编辑界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class OPAttachmentPart : BaseListEditPart
    {
        #region 服务注入

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

        #region 属性

        List<OceanFileList> SelectedItem
        {
            get
            {
                int[] rowIndexs = gvFileList.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<OceanFileList> tagers = new List<OceanFileList>();
                foreach (var item in rowIndexs)
                {
                    OceanFileList data = gvFileList.GetRow(item) as OceanFileList;
                    if (data != null) tagers.Add(data);
                }

                return tagers;
            }
        }

        #endregion

        #region init
        /// <summary>
        /// 合约附件编辑界面
        /// </summary>
        public OPAttachmentPart()
        {
            InitializeComponent();
            Enabled = false;
            Disposed += delegate
            {
                _parentList = null;
                gridControlFileList.DataSource = null;
                bsList.PositionChanged -= bsFileList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (!DesignMode) { InitMessage(); }
        }

        private void InitMessage()
        {
            RegisterMessage("UploadAttachmentPartTitel", "Upload Attachment");
            RegisterMessage("EditAttachmentPartTitel", "Edit Attachment");
            RegisterMessage("RemoveSelectedAttachment", "Are you sure you want to remove the selected attachment?");
            RegisterMessage("DeleteSuccessfully", "Delete Successfully");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                Utility.ShowGridRowNo(gvFileList);
                Utility.SetGridViewClickIndicatorHeader2SelectAll(gvFileList);
            }
        }

        #endregion

        #region Commond

        Guid _LoadedOceanID = Guid.Empty;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OPCommonConstants.Command_TabChanged)]
        public void Command_TabChanged(object sender, EventArgs e)
        {
            try
            {
                if (Visible == false || _LoadedOceanID.IsNullOrEmpty() == false) return;
                Enabled = _parentList != null;

                if (_parentList != null && _parentList.ID.IsNullOrEmpty() == false)
                {

                    List<OceanFileList> list = OceanPriceFileClientService.GetOceanFileList(_parentList.ID, LocalData.UserInfo.LoginID);
                    _LoadedOceanID = _parentList.ID;
                    DataSource = list;                    
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }

        #endregion

        #region listEvent

        private void bsFileList_PositionChanged(object sender, EventArgs e)
        {
            RefreshEnabled();
        }

        private void RefreshEnabled()
        {
            if (CurrentRow == null)
            {
                barRemove.Enabled = barDownLoad.Enabled = barEdit.Enabled = false;
                return;
            }
            if (Utility.GuidIsNullOrEmpty(CurrentRow.ID) == false)
            {
                barDownLoad.Enabled = true;
            }
            if (CurrentRow.Permission >= OceanPermission.Edit)
            {
                barEdit.Enabled = true;
            }
            if (CurrentRow.Permission >= OceanPermission.Manager)
            {
                barRemove.Enabled = true;
            }

        }

        #endregion

        #region 工作流

        private void barUpLoad_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_parentList == null) return;
            OPUploadFilePart uff = Workitem.Items.AddNew<OPUploadFilePart>();
            uff.SetSource(_parentList.ID, null);


            DialogResult dialogResult = PartLoader.ShowDialog(uff, NativeLanguageService.GetText(this, "UploadAttachmentPartTitel"), FormBorderStyle.Sizable);

            if (dialogResult != DialogResult.OK || uff.SavedFileInfos == null || uff.SavedFileInfos.Count <= 0) return;
            List<OceanFileList> docs = uff.SavedFileInfos.Select(item => new OceanFileList
            {
                ID = item.ID, 
                FileID = item.FileID, 
                CreateByID = item.CreateByID, 
                CreateByName = item.CreateByName, 
                CreateDate = item.CreateDate, 
                Remark = item.Remark, 
                FileName = item.FileName, 
                OceanID = item.OceanID, 
                UpdateDate = item.UpdateDate,
                Permission = OceanPermission.Manager
            }).ToList();
            List<OceanFileList> currentSource = bsList.DataSource as List<OceanFileList>;
            if (currentSource != null)
            {
                currentSource.AddRange(docs);
                bsList.DataSource = currentSource;
            }
            bsList.ResetBindings(false);
        }

        private void gridControlFileList_DoubleClick(object sender, EventArgs e)
        {
            EditData();
        }

        private void barEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditData();
        }

        private void EditData()
        {
            if (Workitem == null)
            {
                return;
            }
            if (CurrentRow == null) return;

            OPFileEditPart oeFrom = Workitem.Items.AddNew<OPFileEditPart>();
            oeFrom.SetSource(CurrentRow);
            oeFrom.ChangedFileHandler += delegate(object data, EventArgs er)
            {
                OceanFileList savedData = data as OceanFileList;
                if (savedData != null)
                {
                    List<OceanFileList> list = bsList.DataSource as List<OceanFileList>;
                    OceanFileList tager = list.Find(delegate(OceanFileList item) { return item.ID == savedData.ID; });
                    if (tager != null)
                    {
                        tager.UpdateDate = savedData.UpdateDate;
                        tager.Remark = savedData.Remark;
                        bsList.ResetCurrentItem();
                    }
                }
            };

            DialogResult dialogResult = PartLoader.ShowDialog(oeFrom, NativeLanguageService.GetText(this, "EditAttachmentPartTitel"), FormBorderStyle.Sizable);
        }

        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<OceanFileList> selectedItem = SelectedItem;
            if (selectedItem == null || selectedItem.Count == 0) return;

            #region 询问
            DialogResult result = XtraMessageBox.Show(
                                             NativeLanguageService.GetText(this, "RemoveSelectedAttachment"),
                                              LocalData.IsEnglish ? "Tip" : "Tip",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            #endregion

            try
            {
                List<Guid> ids = new List<Guid>();
                List<DateTime?> updateDatas = new List<DateTime?>();
                foreach (var item in selectedItem)
                {
                    if (item.IsNew) continue;
                    ids.Add(item.ID);
                    updateDatas.Add(item.UpdateDate);
                }

                OceanPriceFileClientService.RemoveOceanFileInfo(ids.ToArray(), LocalData.UserInfo.LoginID, updateDatas.ToArray());

                List<OceanFileList> currentData = bsList.DataSource as List<OceanFileList>;
                foreach (var item in selectedItem)
                {
                    currentData.Remove(item);
                }
                bsList.DataSource = currentData;
                bsList.ResetBindings(false);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "DeleteSuccessfully"));
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }

        private void barDownLoad_ItemClick(object sender, ItemClickEventArgs e)
        {
            DownLoadFile();
        }

        private void DownLoadFile()
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

        private void barPublish_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[OPCommonConstants.Command_PublishPauseData].Execute();
        }

        #endregion

        #region interface

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }

        OceanFileList CurrentRow
        {
            get { return bsList.Current as OceanFileList; }
            set
            {
                OceanFileList current = CurrentRow;
                if (current != null) current = value;
            }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }
        void BindingData(object data)
        {
            if (data == null) { bsList.DataSource = new List<OceanFileList>(); }
            else
            {
                List<OceanFileList> datas = data as List<OceanFileList>;
                if (datas != null && datas.Count > 0)
                {
                    foreach (var item in datas) { item.BeginEdit(); }
                }

                bsList.DataSource = datas;
                bsList.ResetBindings(false);
            }
        }

        #endregion

        #region IEditPart 成员

        public override void EndEdit()
        {
            Validate();
            bsList.EndEdit();
        }

        #endregion

        #region IPart 成员
        OceanList _parentList = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "ParentList")
                {
                    _parentList = item.Value as OceanList;
                    if (_parentList == null
                        || _parentList.IsNew
                        || _parentList.Permission < OceanPermission.Edit
                        || _parentList.OceanUnits == null
                        || _parentList.OceanUnits.Count == 0)
                        Enabled = false;
                    else
                    {
                        Enabled = true;
                    }


                    if (Visible == true && Enabled == true)
                    {
                        List<OceanFileList> list = OceanPriceFileClientService.GetOceanFileList(_parentList.ID, LocalData.UserInfo.LoginID);
                        _LoadedOceanID = _parentList.ID;
                        DataSource = list;

                    }
                    else _LoadedOceanID = Guid.Empty;

                    #region  刷新 Publish按钮状态
                    if (_parentList == null)
                    {
                        barPublish.Enabled = false;
                    }
                    else
                    {
                        barPublish.Enabled = _parentList.State != OceanState.Expired;

                        if (_parentList.State == OceanState.Expired || _parentList.State == OceanState.Invalidated || _parentList.State == OceanState.Draft)
                        {
                            barPublish.Caption = LocalData.IsEnglish ? "&Publish" : "&Publish";
                        }
                        else
                        {
                            barPublish.Caption = LocalData.IsEnglish ? "&Pause" : "&Pause";
                        }
                    }

                    #endregion
                }
            }
        }
        #endregion



        #endregion

        #region Search Ocean Rate中调用
        /// <summary>
        /// 设置工具栏的按钮隐藏
        /// </summary>
        public void SetToolsVisible()
        {
            barUpLoad.Visibility = BarItemVisibility.Never;
            barRemove.Visibility = BarItemVisibility.Never;
            barEdit.Visibility = BarItemVisibility.Never;
            barPublish.Visibility = BarItemVisibility.Never;
        }

        #endregion

        #region 启用发布按钮
        /// <summary>
        /// 设置为可以发布
        /// </summary>
        public void SetPublish()
        {
            barPublish.Enabled = true;
            barPublish.Caption = NativeLanguageService.GetText(this, "Publish");

        }
        #endregion
    }
}
