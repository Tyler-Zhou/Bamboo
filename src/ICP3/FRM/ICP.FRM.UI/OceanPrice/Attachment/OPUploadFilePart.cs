using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface;
using System.IO;
using ICP.Framework.CommonLibrary.Client;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;

namespace ICP.FRM.UI.OceanPrice
{
    [ToolboxItem(false)]
    public partial class OPUploadFilePart : BasePart
    {
        #region Service

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
        int _FileMaxLength = 10000000;
        public OPUploadFilePart()
        {
            InitializeComponent();
            Disposed += delegate {
                _savedFileInfos = null;
                gcMain.DataSource = null;
                bsFile.PositionChanged -= bsFile_PositionChanged;
                bsFile.DataSource = null;
                bsFile.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            Utility.ShowGridRowNo(gvMain);
            if (!DesignMode) { InitMessage(); }
        }
        private void InitMessage()
        {
            RegisterMessage("AttachmentLength", "Add Attachment Faild:{0}''s Length is overstep 10M.");
            RegisterMessage("FileFailedReason", "FileName {0} FailedReason:");
            RegisterMessage("SaveSuccessfully", "Save Successfully");
        }

        private void bsFile_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentRow == null) barDelete.Enabled = false;
            else barDelete.Enabled = true;
        }

        #endregion

        #region property

        List<OceanClientFileList> CurrentSource
        {
            get { return bsFile.DataSource as List<OceanClientFileList>; }
        }

        OceanClientFileList CurrentRow
        {
            get { return bsFile.Current as OceanClientFileList; }
        }

        #endregion

        #region barItem

        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Multiselect = true;
            if (of.ShowDialog() != DialogResult.OK || of.FileNames == null || of.FileNames.Length <= 0) return;

            AddFiles(of.FileNames);
        }

        private void AddFiles(string[] fileNames)
        {
            List<OceanClientFileList> currentSource = CurrentSource;

            for (int i = 0; i < fileNames.Length; i++)
            {
                FileInfo fi = new FileInfo(fileNames[i]);
                if (currentSource.Find(delegate(OceanClientFileList fItem) { return fItem.FullPath == fileNames[i]; }) != null) continue;

                string fileSize = string.Empty;
                #region 文件大小控制
                if (fi.Length > _FileMaxLength)
                {
                    MessageBox.Show(string.Format(NativeLanguageService.GetText(this, "AttachmentLength"), fi.Name));

                    continue;
                }
                #endregion
                OceanClientFileList dcf = new OceanClientFileList();
                dcf.CreateByID = LocalData.UserInfo.LoginID;
                dcf.CreateByName = LocalData.UserInfo.LoginName;
                dcf.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                dcf.FileName = fi.Name;
                dcf.FullPath = fi.FullName;

                currentSource.Add(dcf);
            }

            bsFile.DataSource = currentSource;
            bsFile.ResetBindings(false);
        }

        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentRow == null) return;

            bsFile.RemoveCurrent();
        }

        #endregion

        #region interface
        List<OceanClientFileList> _savedFileInfos = null;
        public List<OceanClientFileList> SavedFileInfos { get { return _savedFileInfos; } }

        Guid _oceanID = Guid.Empty;
        public void SetSource(Guid oceanID, List<OceanClientFileList> files)
        {
            _oceanID = oceanID;
            bsFile.DataSource = files ?? new List<OceanClientFileList>();
        }

        #endregion

        #region btn

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FindForm().DialogResult = DialogResult.Cancel;
            FindForm().Close();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                FindForm().DialogResult = DialogResult.OK;
                FindForm().Close();
            }
        }

        bool SaveData()
        {
            if (ValidateData() == false) return false;
            try
            {
                List<OceanClientFileList> currentSource = CurrentSource;
                List<Guid?> ids = new List<Guid?>();
                List<string> fileName = new List<string>(), fileDescriptions = new List<string>();
                List<byte[]> contents = new List<byte[]>();

                List<DateTime?> updateDates = new List<DateTime?>();
                Dictionary<string, string> errors = new Dictionary<string, string>();
                foreach (var item in currentSource)
                {
                    try
                    {
                        FileStream fs = new FileStream(item.FullPath, FileMode.Open, FileAccess.Read);
                        byte[] content = new byte[(int)fs.Length];
                        fs.Read(content, 0, content.Length);
                        fs.Close();
                        contents.Add(content);
                    }
                    catch (Exception ex)
                    {
                        errors.Add(item.FileName, ex.ToString());
                    }

                    ids.Add(null);
                    fileName.Add(item.FileName);
                    fileDescriptions.Add(item.Remark);
                    updateDates.Add(null);
                }

                #region 有错误信息
                if (errors.Count > 0)
                {
                    StringBuilder strBulider = new StringBuilder();
                    foreach (var item in errors)
                    {
                        if (strBulider.Length > 0) strBulider.Append("\r\n");
                        strBulider.Append(string.Format(NativeLanguageService.GetText(this, "FileFailedReason"), errors.Keys));
                    }

                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), strBulider.ToString());
                    return false;
                }
                #endregion

                ManyResult result = OceanPriceFileClientService.SaveOceanFileInfo(_oceanID
                                                                , ids.ToArray()
                                                                , fileName.ToArray()
                                                                , fileDescriptions.ToArray()
                                                                , contents.ToArray()
                                                                , LocalData.UserInfo.LoginID
                                                                , updateDates.ToArray());

                for (int i = 0; i < result.Items.Count; i++)
                {
                    currentSource[i].OceanID = _oceanID;
                    currentSource[i].ID = result.Items[i].GetValue<Guid>("ID");
                    currentSource[i].FileID = result.Items[i].GetValue<Guid>("FileID");
                    currentSource[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                }

                _savedFileInfos = currentSource;

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "SaveSuccessfully"));
                return true;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); return false; }
        }

        bool ValidateData()
        {
            Validate();
            bsFile.EndEdit();
            List<OceanClientFileList> source = CurrentSource;
            if (source == null || source.Count == 0) return false;

            foreach (var item in source)
            {
                if (item.Validate() == false) return false;
            }

            return true;
        }

        #endregion

        #region drag

        private void gcMain_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                e.Effect = DragDropEffects.Copy;
            }
            catch { }
        }

        private void gcMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            AddFiles(files);
        }

        #endregion
    }

    public class OceanClientFileList : OceanFileList
    {
        public string FullPath { get; set; }
    }
}
