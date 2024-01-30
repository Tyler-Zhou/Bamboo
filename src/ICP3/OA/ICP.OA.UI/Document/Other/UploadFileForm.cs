using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.OA.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;

namespace ICP.OA.UI.Document
{
    public partial class UploadFileForm : BasePart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public ICP.OA.ServiceInterface.Client.IDocumentClientService docService
        {
            get
            {
                return ServiceClient.GetClientService<ICP.OA.ServiceInterface.Client.IDocumentClientService>();
            }
        }

        #endregion

        #region init

        public UploadFileForm()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this._list = null;
                this._savedFileInfos = null;
                this.gcMain.DataSource = null;
                if (this.bsFile != null)
                {
                    this.bsFile.DataSource = null;
                    this.bsFile = null;
                }
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            barAdd.Caption = "新增(&A)";
            barDelete.Caption = "删除(&D)";
            btnOK.Text = "确定(&O)";
            btnCancel.Text = "取消(&C)";
            labTip.Text = "拖动文件到网格以增加文件.";

            colFileDescription.Caption = "描述";
            colFileName.Caption = "文件名";
            colUpdateFile.Caption = "文件路径";

            this.Text = "上传文件";

        }

        private void bsFile_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentRow == null) barDelete.Enabled = false;
            else barDelete.Enabled = true;
        }

        #endregion

        #region property

        List<DocumentClientFileInfo> CurrentSource
        {
            get { return bsFile.DataSource as List<DocumentClientFileInfo>; }
        }

        DocumentClientFileInfo CurrentRow
        {
            get { return bsFile.Current as DocumentClientFileInfo; }
        }

        #endregion

        #region barItem

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Multiselect = true;
            if (of.ShowDialog() != DialogResult.OK || of.FileNames == null || of.FileNames.Length <= 0) return;

            AddFiles(of.FileNames);
        }

        private void AddFiles(string[] fileNames)
        {
            List<DocumentClientFileInfo> currentSource = CurrentSource;

            for (int i = 0; i < fileNames.Length; i++)
            {
                FileInfo fi = new FileInfo(fileNames[i]);
                if (currentSource.Find(delegate(DocumentClientFileInfo fItem) { return fItem.FullPath == fileNames[i]; }) != null) continue;

                string fileSize = string.Empty;
                #region 文件大小控制
                if (fi.Length > 10000000)
                {
                    if (LocalData.IsEnglish)
                        DevExpress.XtraEditors.XtraMessageBox.Show("Add Attachment Faild:" + fi.Name + "''s Length is overstep 10M.");
                    else
                        DevExpress.XtraEditors.XtraMessageBox.Show("添加附件失败:" + fi.Name + "的大小超出10M");
                    continue;
                }
                #endregion
                DocumentClientFileInfo dcf = new DocumentClientFileInfo();
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

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CurrentRow == null) return;

            bsFile.RemoveCurrent();
        }

        #endregion

        #region interface
        List<DocumentClientFileInfo> _savedFileInfos = null;
        public List<DocumentClientFileInfo> SavedFileInfos { get { return _savedFileInfos; } }

        Guid _folderID = Guid.Empty;
        List<DocumentFolderFileList> _list = null;
        public void SetSource(Guid folderID, List<DocumentClientFileInfo> files, List<DocumentFolderFileList> list)
        {
            _folderID = folderID;
            bsFile.DataSource = files ?? new List<DocumentClientFileInfo>();

            _list = list as List<DocumentFolderFileList>;
        }

        #endregion

        #region btn

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
            this.FindForm().Close();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            string FileName = string.Empty;
            if (SameFile(ref FileName))
            {
                if (!string.IsNullOrEmpty(FileName))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "The file " + FileName + "  is exsit." : "文件 " + FileName + " 已经存在.");
                }

                return;
            }

            if (this.SaveData())
            {
                this.FindForm().DialogResult = DialogResult.OK;
                this.FindForm().Close();
            }
        }

        bool SameFile(ref  string fileName)
        {
            if (this.ValidateData() == false) return false;
            List<DocumentClientFileInfo> currentSource = CurrentSource;

            for (int j = 0; j < _list.Count; j++)
            {
                for (int i = 0; i < currentSource.Count; i++)
                {
                    string csName = currentSource[i].FileName;
                    if (_list[j].Name.Contains(csName))
                    {
                        fileName = csName;
                        return true;
                    }
                }
            }

            return false;
        }

        bool SaveData()
        {
            if (this.ValidateData() == false) return false;
            try
            {
                List<DocumentClientFileInfo> currentSource = CurrentSource;
                List<Guid?> ids = new List<Guid?>();
                List<string> fileName = new List<string>(), fileDescriptions = new List<string>();
                List<byte[]> contents = new List<byte[]>();
                List<DateTime?> updataDates = new List<DateTime?>();
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
                    updataDates.Add(null);
                    fileName.Add(item.FileName);
                    fileDescriptions.Add(item.FileDescription);
                }

                #region 有错误信息
                if (errors.Count > 0)
                {
                    StringBuilder strBulider = new StringBuilder();
                    foreach (var item in errors)
                    {
                        if (strBulider.Length > 0) strBulider.Append("\r\n");
                        strBulider.Append(LocalData.IsEnglish ? "FileName" : "文件名");
                        strBulider.Append(errors.Keys);
                        strBulider.Append(LocalData.IsEnglish ? "Failed reason" : "失败原因:");
                    }

                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), strBulider.ToString());
                    return false;
                }
                #endregion

                ManyResultData result = docService.SaveFileInfo(ids.ToArray()
                                                                , _folderID
                                                                , fileName.ToArray()
                                                                , fileDescriptions.ToArray()
                                                                , contents.ToArray()
                                                                , LocalData.UserInfo.LoginID
                                                                , updataDates.ToArray());

                for (int i = 0; i < result.ChildResults.Count; i++)
                {
                    currentSource[i].FolderID = _folderID;
                    currentSource[i].ID = result.ChildResults[i].ID;
                    currentSource[i].UpdateDate = result.ChildResults[i].UpdateDate;
                }

                _savedFileInfos = currentSource;
                return true;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); return false; }
        }

        bool ValidateData()
        {
            this.Validate();
            bsFile.EndEdit();

            if (CurrentSource == null || CurrentSource.Count == 0)
            {
                return false;
            }

            foreach (var item in CurrentSource)
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

    public class DocumentClientFileInfo : DocumentFileInfo
    {
        public string FullPath { get; set; }
    }
}
