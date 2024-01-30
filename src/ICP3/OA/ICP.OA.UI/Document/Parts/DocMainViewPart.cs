using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ICP.OA.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using ICP.Framework.CommonLibrary.Common;
using System.IO;
namespace ICP.OA.UI.Document
{
    [ToolboxItem(false)]
    public partial class DocMainViewPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public ICP.OA.ServiceInterface.Client.IDocumentClientService DocumentClientService
        {
            get
            {
                return ServiceClient.GetClientService<ICP.OA.ServiceInterface.Client.IDocumentClientService>();
            }
        }

        #endregion

        #region init

        public DocMainViewPart()
        {
            InitializeComponent();
            IsShowLanguageMenu = false;
            this.Disposed += delegate {
                this.CurrentChanged = null;
                this.CurrentChanging = null;
                this._originalSource = null;
                this._lvwColumnSorter = null;
                this.treeMain.DataSource = null;
                if (this.bsList != null)
                {
                    this.bsList.DataSource = null;
                    this.bsList = null;
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
            barBreak.Caption = "向上";
            barAddlvFolderBar.Caption = barLvNewFolder.Caption = barAddFolder.Caption = "新增文件夹(&N)";
            barDeleteBar.Caption = barLvDelete.Caption = barDelete.Caption = "删除(&D)";
            barLvUpLoadFile.Caption = barUpLoadFile.Caption = "上传文件(&U)";
            barLvDownLoadFile.Caption = barDownLoadFile.Caption = "下载文件(&L)";
            barLvReName.Caption = barReName.Caption = "重命名(&E)";

            barClose.Caption = "关闭(&C)";
            barRefresh.Caption = "刷新(&R)";
            //barSearch.Caption = "查询(&H)";

            barViewStyle.Caption = "大图标";
            barLargeIcon.Caption = "大图标";
            barDetails.Caption = "详细";
            barSmallIcon.Caption = "小图标";
            barList.Caption = "列表";
            barTile.Caption = "平铺";

            colhCreateDate.Text = "创建日期";
            colhCreateBy.Text = "创建人";
            colhName.Text = "名称";
            colhUpdateDate.Text = "更新日期";

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
            RefreshData();
        }

        ListViewColumnSorter _lvwColumnSorter = new ListViewColumnSorter();
        private void InitControls()
        {
            this.lvMain.ListViewItemSorter = _lvwColumnSorter;
            treeMain.LayoutUpdated += new EventHandler(treeMain_LayoutUpdated);
        }

        #endregion

        #region property

        DocumentFolderFileList CurrentRow
        {
            get { return bsList.Current as DocumentFolderFileList; }
        }

        List<DocumentFolderFileList> CurrentTreeSource
        {
            get { return DataSource as List<DocumentFolderFileList>; }
        }

        List<DocumentFolderFileList> CurrentListViewSelectedItem
        {
            get
            {
                if (lvMain.SelectedItems == null || lvMain.SelectedItems.Count == 0)
                {
                    if (lvMain.FocusedItem == null || lvMain.FocusedItem.Selected == false) return null;
                    List<DocumentFolderFileList> docs = new List<DocumentFolderFileList> { lvMain.FocusedItem.Tag as DocumentFolderFileList };
                    return docs;

                }
                else
                {
                    List<DocumentFolderFileList> docs = new List<DocumentFolderFileList>();
                    foreach (ListViewItem item in lvMain.SelectedItems)
                    {
                        docs.Add(item.Tag as DocumentFolderFileList);
                    }
                    return docs;
                }
            }
        }

        #endregion

        #region barItem

        #region Add Folder
        private void barAddFolder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DocumentFolderFileList newData = AddFolder();
            if (newData == null) return;

            if (newData.DocumentType == OADocumentType.Folder)
            {
                TreeListNode tn = treeMain.FindNodeByFieldValue("ID", newData.ID);
                if (tn != null)
                {
                    treeMain.FocusedNode = tn;
                    ReNameTree();
                }

            }
        }

        private void barAddlvFolderBar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddLvFolder();
        }
        private void barLvNewFolder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddLvFolder();
        }
        private void AddLvFolder()
        {
            DocumentFolderFileList newData = AddFolder();
            if (newData == null) return;

            TreeListNode currentNode = treeMain.FocusedNode;
            if (currentNode != null) currentNode.ExpandAll();

            if (newData.DocumentType == OADocumentType.Folder)
            {
                foreach (ListViewItem item in lvMain.Items)
                {
                    if ((item.Tag as DocumentFolderFileList).ID == newData.ID)
                    {
                        item.Focused = true;
                        item.BeginEdit();
                        break;
                    }
                }
            }
        }

        int _newFolderIndex = -1;
        string _newFolderName = LocalData.IsEnglish ? "New Folder" : "新建文件夹";
        private DocumentFolderFileList AddFolder()
        {
            if (CurrentRow == null) return null;

            if (CurrentRow.Permission <= DocuentPermission.View)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), CurrentRow.Name + (LocalData.IsEnglish ? ":You have no right to operate this document" : "您没有足够的权限操作此文件"));
                return null;
            }

            //docService.GetDocumentFolderList(
            DocumentFolderFileList newData = new DocumentFolderFileList();
            newData.DocumentType = OADocumentType.Folder;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.Name = BulidNewFolderName();

            if (CurrentRow != null)
            {
                newData.ParentID = CurrentRow.ID;
                newData.ParentName = CurrentRow.Name;
            }


            try
            {
                SingleHierarchyResultData result = DocumentClientService.SaveFolderInfo(null
                                                                            , newData.Name
                                                                            , newData.ParentID
                                                                            , LocalData.UserInfo.LoginID
                                                                            , null);

                newData.ID = result.ID;
                newData.UpdateDate = result.UpdateDate;
                newData.HierarchyCode = result.HierarchyCode;
                newData.Permission = DocuentPermission.Manager;
                newData.BeginEdit();

                _originalSource.Add(newData);
                bsList.Add(newData);
                if (CurrentRow.DocumentType == OADocumentType.Folder)
                    AddListViewItemByDocObject(newData);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Set Successfully" : "设置成功");
                return newData;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); return null; }

        }
        string BulidNewFolderName()
        {
            string newFoderName = _newFolderName;
            if (CurrentRow == null) return newFoderName;

            _newFolderIndex = CurrentTreeSource.IndexOf(CurrentRow);
            List<DocumentFolderFileList> currentFolderChilds = CurrentTreeSource.FindAll(delegate(DocumentFolderFileList item) { return item.ParentID == CurrentRow.ID; });
            if (currentFolderChilds == null || currentFolderChilds.Count == 0) return newFoderName;

            List<DocumentFolderFileList> currentFolderChildsNewFoldeName
                = currentFolderChilds.FindAll(delegate(DocumentFolderFileList item) { return item.Name.Contains(_newFolderName); });

            if (currentFolderChildsNewFoldeName != null && currentFolderChildsNewFoldeName.Count != 0)
            {
                int indexNum = -1;
                foreach (var item in currentFolderChildsNewFoldeName)
                {
                    string nameNum = item.Name.Replace(_newFolderName, string.Empty);
                    int num = -1;
                    if (string.IsNullOrEmpty(nameNum)) num = 0;
                    else int.TryParse(nameNum, out num);

                    if (num > indexNum) indexNum = num;
                }
                if (indexNum >= 0)
                {
                    indexNum++;
                    newFoderName = _newFolderName + indexNum.ToString();
                }
            }

            return newFoderName;
        }

        #endregion

        #region UpLoadFile

        private void barUpLoadFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenUpLoadFile(string.Empty);

        }
        private void barLvUpLoadFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenUpLoadFile(string.Empty);
        }
        private void OpenUpLoadFile(string fileName)
        {
            UploadFileForm uff = this.Workitem.Items.AddNew<UploadFileForm>();
            uff.SetSource(CurrentRow.ID, null,tager);
            string title = LocalData.IsEnglish ? "UPLoad File" : "上传文件";
            DialogResult dialogResult = ICP.Framework.ClientComponents.Controls.PartLoader.ShowDialog(uff, title);

            if (dialogResult == DialogResult.OK && uff.SavedFileInfos != null && uff.SavedFileInfos.Count >0)
            {
                List<DocumentFolderFileList> docs = new List<DocumentFolderFileList>();
                foreach (var item in uff.SavedFileInfos)
                {
                    DocumentFolderFileList doc = new DocumentFolderFileList();
                    doc.ID = item.ID;
                    doc.CreateByID = item.CreateByID;
                    doc.CreateByName = item.CreateByName;
                    doc.CreateDate = item.CreateDate;
                    doc.Description = item.FileDescription;
                    doc.DocumentType = OADocumentType.File;
                    doc.Name = item.FileName;
                    doc.ParentID = item.FolderID;
                    doc.ParentName = item.FolderName;
                    doc.Permission = DocuentPermission.Manager;
                    docs.Add(doc);
                   
                }
                List<DocumentFolderFileList> currentSource = bsList.DataSource as List<DocumentFolderFileList>;
                currentSource.AddRange(docs);
                bsList.DataSource = currentSource;
                bsList.ResetBindings(false);

                if (CurrentRow.DocumentType == OADocumentType.Folder)
                {
                    foreach (var item in docs)
                        AddListViewItemByDocObject(item);
                }
                
            }
        }

        #endregion

        #region Delete

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteTreeData();
        }
        private void DeleteTreeData()
        {
            if (Utility.EnquireIsDeleteCurrentData() == false) return;

            try
            {
                if (CurrentRow.DocumentType == OADocumentType.Folder)
                    DocumentClientService.RemoveFolderInfo(CurrentRow.ID, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                else
                    DocumentClientService.RemoveFileInfo(CurrentRow.ID, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                TreeListNode tn = treeMain.FocusedNode;
                TreeListNode nearTn = FindNearNode(treeMain.FocusedNode);

                List<Guid> ids = GetChildIdsById(_originalSource, CurrentRow.ID);
                List<DocumentFolderFileList> needRemove = _originalSource.FindAll(delegate(DocumentFolderFileList item) { return ids.Contains(item.ID); });
                List<DocumentFolderFileList> needTreeRemove = CurrentTreeSource.FindAll(delegate(DocumentFolderFileList item) { return ids.Contains(item.ID); });

                foreach (var item in needRemove)
                    _originalSource.Remove(item);

                foreach (var item in needTreeRemove)
                    bsList.Remove(item);

                treeMain.FocusedNode.ExpandAll();

                if (nearTn != null)
                    treeMain.FocusedNode = nearTn;
                else
                    bsFolderFile_PositionChanged(null, null);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Remove Successfully" : "删除成功");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }
        private TreeListNode FindNearNode(TreeListNode tn)
        {
            if (tn.PrevNode != null) return tn.PrevNode;
            if (tn.ParentNode != null) return tn.ParentNode;

            return null;
        }

        private void barLvDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteLvData();
        }
        private void barDeleteBar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteLvData();
        }
        private void DeleteLvData()
        {
            List<DocumentFolderFileList> currentListViewSelectedItem = CurrentListViewSelectedItem;
            if (currentListViewSelectedItem == null || currentListViewSelectedItem.Count == 0) return;

            if (Utility.EnquireIsDeleteCurrentData() == false) return;

            DocumentFolderFileList data = CurrentListViewSelectedItem[0];

            try
            {
                if (data.DocumentType == OADocumentType.Folder)
                    DocumentClientService.RemoveFolderInfo(data.ID, LocalData.UserInfo.LoginID, data.UpdateDate);
                else
                    DocumentClientService.RemoveFileInfo(data.ID, LocalData.UserInfo.LoginID, data.UpdateDate);


                ListViewItem needRemoveLvItem = null;
                foreach (ListViewItem item in lvMain.Items)
                {
                    if ((item.Tag as DocumentFolderFileList).ID == data.ID)
                    {
                        needRemoveLvItem = item;
                        break;
                    }
                }
                if (needRemoveLvItem != null) lvMain.Items.Remove(needRemoveLvItem);

                TreeListNode ftn = treeMain.FocusedNode;

                //List<DocumentFolderFileList> currentSource = _originalSource;
                List<Guid> ids = GetChildIdsById(_originalSource, data.ID);
                List<DocumentFolderFileList> needRemove = _originalSource.FindAll(delegate(DocumentFolderFileList item) { return ids.Contains(item.ID); });
                foreach (var item in needRemove)
                    bsList.Remove(item);

                treeMain.FocusedNode.ExpandAll();

                treeMain.FocusedNode = ftn;
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Remove Successfully" : "删除成功");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        #endregion

        #region DownLoadFile

        private void barLvDownLoadFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DownLoadFile(CurrentListViewSelectedItem[0]);
        }
        private void barDownLoadFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            DownLoadFile(CurrentRow);
        }

        private void DownLoadFile(DocumentFolderFileList data)
        {
            if (data == null || data.DocumentType == OADocumentType.Folder) return;

            FolderDialog fd = new FolderDialog();
            DialogResult dr = fd.DisplayDialog(LocalData.IsEnglish ? "Select a Folder." : "请选择一个文件夹");
            if (dr != DialogResult.OK) return;

            try
            {
                DocumentFileInfo docFileInfo = DocumentClientService.GetFileInfo(data.ID);

                string tempFileName = fd.Path + "\\" + docFileInfo.FileName;
                if (File.Exists(tempFileName)) File.Delete(tempFileName);

                FileStream fs = new FileStream(tempFileName, FileMode.Create, FileAccess.Write);
                fs.Write(docFileInfo.Content, 0, docFileInfo.Content.Length);
                fs.Close();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "DownLoad Successfully" : "下载成功");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "DownLoad File Failed" : "下载文件失败") + ex.Message); }

        }
        #endregion

        #region Search  Refresh Close

        private void barSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshData();
        }

        List<DocumentFolderFileList> _originalSource = new List<DocumentFolderFileList>();
        private void RefreshData()
        {
            _originalSource = DocumentClientService.GetDocumentFolderFileList(LocalData.UserInfo.LoginID);
            if (_originalSource == null) _originalSource = new List<DocumentFolderFileList>();
            DocumentFolderFileList tager = _originalSource.Find(delegate(DocumentFolderFileList item) { return Utility.GuidIsNullOrEmpty(item.ParentID); });
            if (tager != null) _originalSource.Remove(tager);


            _originalSource.FindAll(delegate(DocumentFolderFileList item) { return item.Permission == DocuentPermission.Edit && item.CreateByID == LocalData.UserInfo.LoginID; });


            List<DocumentFolderFileList> list = _originalSource.FindAll(delegate(DocumentFolderFileList item) { return item.DocumentType == OADocumentType.Folder; });
            bsList.DataSource = list;

            //首两层展开
            foreach (TreeListNode tn in treeMain.Nodes)
            {
                tn.Expanded = true;
            }
            if (treeMain.Nodes.Count != 0 && treeMain.Nodes.FirstNode.Nodes.Count != 0)
                treeMain.FocusedNode = treeMain.Nodes.FirstNode.Nodes.FirstNode;


            BulidListViewItem();
        }

        private void treeMain_LayoutUpdated(object sender, EventArgs e)
        {
            treeMain.LayoutUpdated -= new EventHandler(treeMain_LayoutUpdated);
            treeMain.FocusedNode = treeMain.Nodes.FirstNode.Nodes.FirstNode;

        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        #endregion

        #region Lv Style

        View _lvView = View.LargeIcon;

        private void barViewStyle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_lvView == View.Tile)
                _lvView = 0;
            else
                _lvView += 1;

            SetLvView();
        }

        private void SetLvView()
        {
            lvMain.View = _lvView;

            if (_lvView == View.LargeIcon)
            {
                barViewStyle.Caption = barLargeIcon.Caption;
                lvMain.ShowItemToolTips = true;
            }
            else if (_lvView == View.Details)
            {
                barViewStyle.Caption = barDetails.Caption;
                lvMain.ShowItemToolTips = false;
            }
            else if (_lvView == View.SmallIcon)
            {
                barViewStyle.Caption = barSmallIcon.Caption;
                lvMain.ShowItemToolTips = true;
            }
            else if (_lvView == View.List)
            {
                barViewStyle.Caption = barList.Caption;
                lvMain.ShowItemToolTips = true;
            }
            else if (_lvView == View.Tile)
            {
                barViewStyle.Caption = barTile.Caption;
                lvMain.ShowItemToolTips = false;
            }

        }

        private void barLargeIcon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _lvView = View.LargeIcon;
            SetLvView();
        }

        private void barDetails_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _lvView = View.Details;
            SetLvView();
        }

        private void barSmallIcon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _lvView = View.SmallIcon;
            SetLvView();
        }

        private void barList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _lvView = View.List;
            SetLvView();
        }

        private void barTile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _lvView = View.Tile;
            SetLvView();
        }

        #endregion

        #region ReName
        string _beforeReNameName = string.Empty;
        private void barReName_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ReNameTree();
        }

        private void treeMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && colName.OptionsColumn.AllowEdit == false)
            {
                ReNameTree();
            }
        }

        private void ReNameTree()
        {
            if (CurrentRow.DocumentType == OADocumentType.File) return;
            _beforeReNameName = CurrentRow.Name;
            colName.OptionsColumn.AllowEdit = true;

            this.treeMain.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.treeMain.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;

            treeMain.ShowEditor();
        }

        private void barLvReName_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lvMain.SelectedItems[0].BeginEdit();
        }

        #endregion

        private void barBreak_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CurrentRow != null)
            {
                TreeListNode tn = treeMain.FindNodeByKeyID(CurrentRow.ParentID);
                if (tn == null) return;
                else treeMain.FocusedNode = tn;
            }
        }

        #endregion

        #region tree Event

        private void bsFolderFile_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);

            BulidListViewItem();

            RefreshEnabled();
        }

        List<DocumentFolderFileList> tager = null;
        private void BulidListViewItem()
        {
            lvMain.Items.Clear();

            if (CurrentRow == null) return;

            tager = _originalSource.FindAll(delegate(DocumentFolderFileList item) { return item.ParentID == CurrentRow.ID; });
            foreach (var item in tager)
            {
                AddListViewItemByDocObject(item);
            }

            RefreshDeleteBarEnabled();

        }

        private void AddListViewItemByDocObject(DocumentFolderFileList item)
        {
            ListViewItem lvItem = new ListViewItem(new string[]
                                                    { item.Name.ToString()
                                                      ,item.UpdateDate==null?string.Empty :item.UpdateDate.Value.ToShortDateString()
                                                      ,item.CreateByName
                                                      ,item.CreateDate.ToShortDateString()
                                                    });
            StringBuilder toolTipBulider = new StringBuilder();
            toolTipBulider.Append(item.Name);
            if (item.UpdateDate != null)
            {
                toolTipBulider.Append("\r\n" + colhUpdateDate.Text + ": " + item.UpdateDate.Value.ToShortDateString());
            }
            toolTipBulider.Append("\r\n" + colhCreateBy.Text + ": " + item.CreateByName);
            toolTipBulider.Append("\r\n" + colhCreateDate.Text + ": " + item.CreateDate.ToShortDateString());
            lvItem.ToolTipText = toolTipBulider.ToString();
            lvItem.ImageIndex = GetImageIndexByDocObj(item);
            lvItem.Tag = item;
            lvMain.Items.Add(lvItem);
        }
        private void RefreshEnabled()
        {
            if (CurrentRow == null)
            {
                barReName.Enabled = barDownLoadFile.Enabled = barUpLoadFile.Enabled = barDelete.Enabled = false;
            }
            else //if (CurrentRow.DocumentType == OADocumentType.Folder)
            {
                barReName.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barDownLoadFile.Enabled = false;
                if (CurrentRow.Permission == DocuentPermission.Manager) barReName.Enabled = barDelete.Enabled = true;
                else barReName.Enabled = barDelete.Enabled = false;

                if (CurrentRow.Permission <= DocuentPermission.View)
                    barAddlvFolderBar.Enabled = barAddFolder.Enabled = barUpLoadFile.Enabled = false;
                else
                    barAddlvFolderBar.Enabled = barAddFolder.Enabled = barUpLoadFile.Enabled = true;

            }
            //else
            //{
            //    if (CurrentRow == null || CurrentRow.Permission <= DocuentPermission.View)
            //        barAddlvFolderBar.Enabled=barAddFolder.Enabled = barUpLoadFile.Enabled = false;
            //    else
            //        barAddlvFolderBar.Enabled=barAddFolder.Enabled = barUpLoadFile.Enabled = true;

            //    if (CurrentRow.Permission == DocuentPermission.Manager)  barDelete.Enabled = true;
            //    else barDelete.Enabled = false;

            //    barReName.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            //    barDownLoadFile.Enabled = true;
            //}


        }

        #region Tree Image
        private void treeMain_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            DocumentFolderFileList documentFolderFileList = treeMain.GetDataRecordByNode(e.Node) as DocumentFolderFileList;
            e.Node.StateImageIndex = GetImageIndexByDocObj(documentFolderFileList);
        }

        private static int GetImageIndexByDocObj(DocumentFolderFileList doc)
        {
            if (doc == null) return 0;

            int permissionNum = 0;
            if (doc.Permission == DocuentPermission.Manager)
                permissionNum = 1;
            else if (doc.Permission == DocuentPermission.Edit)
                permissionNum = 2;
            else
                permissionNum = 3;
            return (doc.DocumentType == OADocumentType.Folder ? 0 : 3) + permissionNum;
        }
        #endregion

        #region focus Node change row stly
        private void treeMain_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            colName.OptionsColumn.AllowEdit = false;
            this.treeMain.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeMain.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;

            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.CanFocus = !ce.Cancel;
            }
        }
        private void treeMain_Leave(object sender, EventArgs e)
        {
            colName.OptionsColumn.AllowEdit = false;
            this.treeMain.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeMain.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
        }

        #endregion

        private void treeMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //Point pt = treeMain.PointToClient(new Point(e.X, e.Y));
                TreeListHitInfo tnhitInfo = treeMain.CalcHitInfo(new Point(e.X, e.Y));
                if (tnhitInfo == null || tnhitInfo.Node == null) return;

                treeMain.FocusedNode = tnhitInfo.Node;

                popupMenuTree.ShowPopup(MousePosition);
            }
        }

        #region Drag Node

        private void treeMain_BeforeDragNode(object sender, DevExpress.XtraTreeList.BeforeDragNodeEventArgs e)
        {
            beforeDrageParentID = Guid.Empty;
            DocumentFolderFileList dragingData = treeMain.GetDataRecordByNode(e.Node) as DocumentFolderFileList;
            if (dragingData == null || dragingData.Permission <= DocuentPermission.View) e.CanDrag = false;
            else
            {
                beforeDrageParentID = dragingData.ParentID;
            }
        }
        Guid beforeDrageParentID = Guid.Empty;
        private void treeMain_AfterDragNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            DocumentFolderFileList dragingData = treeMain.GetDataRecordByNode(e.Node) as DocumentFolderFileList;
            if (dragingData == null) return;

            if (dragingData.ParentID == beforeDrageParentID)
            {
                beforeDrageParentID = Guid.Empty;
                return;
            }

            List<DocumentFolderFileList> list = DataSource as List<DocumentFolderFileList>;
            DocumentFolderFileList dragTo = list.Find(delegate(DocumentFolderFileList item) { return item.ID == dragingData.ParentID; });
            if (dragTo == null
                || dragTo.DocumentType == OADocumentType.File
                || dragTo.Permission <= DocuentPermission.View
                || ValidateHasSameName(dragingData, dragTo))
            {
                dragingData.ParentID = beforeDrageParentID;
                treeMain.RefreshDataSource();
                beforeDrageParentID = Guid.Empty;
            }
            else if (Utility.EnquireIsSaveCurrentData() == false)
            {
                dragingData.ParentID = beforeDrageParentID;
                treeMain.RefreshDataSource();
                beforeDrageParentID = Guid.Empty;
            }
            else
            {
                try
                {
                    ICP.Framework.CommonLibrary.Common.ManyHierarchyResultData result = DocumentClientService.SetParentFolder(new Guid[] { dragingData.ID }
                                                , new OADocumentType[] { dragingData.DocumentType }
                                                , dragTo.ID
                                                , LocalData.UserInfo.LoginID
                                                 , new DateTime?[] { dragingData.UpdateDate });

                    UpdateChangedChildList(list, result);
                    bsFolderFile_PositionChanged(null, null);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Set Successfully" : "设置成功");
                }
                catch (Exception ex)
                {
                    dragingData.ParentID = beforeDrageParentID;
                    treeMain.RefreshDataSource();
                    beforeDrageParentID = Guid.Empty;
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                }
            }
            beforeDrageParentID = Guid.Empty;

        }

        private bool ValidateHasSameName(DocumentFolderFileList dragingData, DocumentFolderFileList dragTo)
        {
            List<DocumentFolderFileList> currentSource = bsList.DataSource as List<DocumentFolderFileList>;
            List<DocumentFolderFileList> childs = currentSource.FindAll(delegate(DocumentFolderFileList item) { return item.ID != dragingData.ID && item.ParentID == dragTo.ID; });
            if (childs == null || childs.Count == 0) return false;
            DocumentFolderFileList existItem = childs.Find(delegate(DocumentFolderFileList item) { return item.Name == dragingData.Name; });
            if (existItem == null) return false;
            else return true;
        }

        void UpdateChangedChildList(List<DocumentFolderFileList> sourcelist, ICP.Framework.CommonLibrary.Common.ManyHierarchyResultData result)
        {
            if (sourcelist == null || sourcelist.Count == 0) return;

            List<DocumentFolderFileList> returnlist = new List<DocumentFolderFileList>();
            foreach (ICP.Framework.CommonLibrary.Common.SingleHierarchyResultData sr in result.ChildResults)
            {
                DocumentFolderFileList value = sourcelist.Find(delegate(DocumentFolderFileList v) { return v.ID == sr.ID; });
                if (value != null)
                {
                    value.UpdateDate = sr.UpdateDate;
                    value.HierarchyCode = sr.HierarchyCode;

                    returnlist.Add(value);
                }
            }

            bsList.ResetBindings(false);
        }

        #endregion

        #region drag Enter

        private void treeMain_DragEnter(object sender, DragEventArgs e)
        {
            TreeListNode tn = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
            ListView.SelectedListViewItemCollection lvSelectItem = e.Data.GetData(typeof(ListView.SelectedListViewItemCollection)) as ListView.SelectedListViewItemCollection;

            if (tn == null && lvSelectItem == null) return;

            e.Effect = DragDropEffects.Copy;
        }

        private void treeMain_DragDrop(object sender, DragEventArgs e)
        {
            TreeListNode tn = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
            if (tn != null) return;

            ListView.SelectedListViewItemCollection lvSelectItem = e.Data.GetData(typeof(ListView.SelectedListViewItemCollection)) as ListView.SelectedListViewItemCollection;
            if (lvSelectItem == null) return;

            List<DocumentFolderFileList> dragList = new List<DocumentFolderFileList>();
            foreach (ListViewItem item in lvSelectItem)
            {
                dragList.Add(item.Tag as DocumentFolderFileList);
            }
            if (dragList.Count == 0) return;

            Point pt = treeMain.PointToClient(new Point(e.X, e.Y));
            TreeListHitInfo tnhitInfo = treeMain.CalcHitInfo(pt);
            if (tnhitInfo == null || tnhitInfo.Node == null) return;

            DocumentFolderFileList tagerDoc = treeMain.GetDataRecordByNode(tnhitInfo.Node) as DocumentFolderFileList;
            if (tagerDoc.DocumentType == OADocumentType.File) return;
            if (tagerDoc.Permission <= DocuentPermission.View)
            {
                //LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), tagerDoc.Name + (LocalData.IsEnglish ? ":You have no right to operate this document" : "您没有足够的权限操作此文件"));
                return;
            }

            if (ValidateIsChild(dragList, tagerDoc) == true)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Can't move in the child." : "不能移到子项当中");
                return;
            }

            SetDrag2Parent(dragList, tagerDoc);
        }

        /// <summary>
        /// 验证tager是否dragList其中一个的子项
        /// </summary>
        private bool ValidateIsChild(List<DocumentFolderFileList> dragList, DocumentFolderFileList tager)
        {
            List<DocumentFolderFileList> currentSource = _originalSource;
            foreach (var item in dragList)
            {
                List<Guid> ids = GetChildIdsById(currentSource, item.ID);
                if (ids.Count == 0) continue;

                if (ids.Contains(tager.ID)) return true;
            }

            return false;
        }

        /// <summary>
        /// 获取所有子项(包括自身)ID
        /// </summary>
        List<Guid> GetChildIdsById(List<DocumentFolderFileList> data, Guid currentId)
        {
            List<Guid> childIds = new List<Guid>();
            childIds.Add(currentId);

            while (true)
            {
                List<DocumentFolderFileList> childs = data.FindAll(delegate(DocumentFolderFileList item)
                { return childIds.Contains(item.ParentID) && childIds.Contains(item.ID) == false; });

                if (childs == null || childs.Count == 0)
                    break;
                else
                {
                    foreach (DocumentFolderFileList item in childs)
                    {
                        childIds.Add(item.ID);
                    }
                }
            }
            return childIds;
        }

        private void SetDrag2Parent(List<DocumentFolderFileList> dragList, DocumentFolderFileList tagerDoc)
        {
            //1.排除自身
            DocumentFolderFileList needRemove = dragList.Find(delegate(DocumentFolderFileList item) { return item.ID == tagerDoc.ID; });
            if (needRemove != null) dragList.Remove(needRemove);

            //2.排除已是目标文件的子项
            List<DocumentFolderFileList> needDragList = dragList.FindAll(delegate(DocumentFolderFileList item) { return item.ParentID != tagerDoc.ID; });
            if (needDragList == null || needDragList.Count == 0) return;

            try
            {
                List<Guid> ids = new List<Guid>();
                List<OADocumentType> docTypes = new List<OADocumentType>();
                List<DateTime?> updates = new List<DateTime?>();
                foreach (var item in needDragList)
                {
                    ids.Add(item.ID);
                    docTypes.Add(item.DocumentType);
                    updates.Add(item.UpdateDate);
                }


                ICP.Framework.CommonLibrary.Common.ManyHierarchyResultData result = DocumentClientService.SetParentFolder(ids.ToArray()
                                                                           , docTypes.ToArray()
                                                                           , tagerDoc.ID
                                                                           , LocalData.UserInfo.LoginID
                                                                           , updates.ToArray());

                for (int i = 0; i < result.ChildResults.Count; i++)
                {
                    needDragList[i].ID = result.ChildResults[i].ID;
                    needDragList[i].UpdateDate = result.ChildResults[i].UpdateDate;
                    needDragList[i].HierarchyCode = result.ChildResults[i].HierarchyCode;
                }

                Guid currentFolderID = CurrentRow.ID;

                foreach (var needDragItem in needDragList)
                {
                    DocumentFolderFileList tager = _originalSource.Find(delegate(DocumentFolderFileList item) { return item.ID == needDragItem.ID; });
                    if (tager != null)
                    {
                        tager.ParentID = tagerDoc.ID;
                        tager.UpdateDate = needDragItem.UpdateDate;
                        tager.HierarchyCode = needDragItem.HierarchyCode;
                    }
                }

                List<DocumentFolderFileList> currentSource = CurrentTreeSource;
                foreach (var needDragItem in needDragList)
                {
                    DocumentFolderFileList tager = currentSource.Find(delegate(DocumentFolderFileList item) { return item.ID == needDragItem.ID; });
                    if (tager != null)
                    {
                        tager.ParentID = tagerDoc.ID;
                        tager.UpdateDate = needDragItem.UpdateDate;
                        tager.HierarchyCode = needDragItem.HierarchyCode;
                    }
                }
                bsList.DataSource = currentSource;
                bsList.ResetBindings(false);
                BulidListViewItem();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Set Successfully" : "设置成功");

            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        #endregion

        private void treeMain_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            colName.OptionsColumn.AllowEdit = false;
            this.treeMain.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeMain.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;

            if (ValidateCanReName() == false)
            {
                _beforeReNameName = string.Empty;
                treeMain.Focus();
                return;
            }


            try
            {
                SingleResultData result = null;
                if (CurrentRow.DocumentType == OADocumentType.Folder)
                {
                    result = DocumentClientService.SaveFolderInfo(CurrentRow.ID
                                                        , CurrentRow.Name
                                                        , CurrentRow.ParentID
                                                        , LocalData.UserInfo.LoginID
                                                        , CurrentRow.UpdateDate);
                }
                else
                {
                    result = DocumentClientService.SaveFileInfo(CurrentRow.ID
                                                        , CurrentRow.Name
                                                        , CurrentRow.Description
                                                        , CurrentRow.ParentID
                                                        , LocalData.UserInfo.LoginID
                                                        , CurrentRow.UpdateDate);
                }

                CurrentRow.ID = result.ID;
                CurrentRow.UpdateDate = result.UpdateDate;
                CurrentRow.BeginEdit();
                _beforeReNameName = string.Empty;
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Set Successfully" : "设置成功");
            }
            catch (Exception ex)
            {
                DocumentFolderFileList currentRow = CurrentRow;
                currentRow.Name = _beforeReNameName;
                bsList.ResetCurrentItem();
                _beforeReNameName = string.Empty;
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }

        }

        bool ValidateCanReName()
        {
            if (CurrentRow.Name == _beforeReNameName) return false;
            if (treeMain.FocusedNode == null || CurrentRow == null || CurrentRow.Permission < DocuentPermission.Manager)
            {
                DocumentFolderFileList currentRow = CurrentRow;
                currentRow.Name = _beforeReNameName;
                bsList.ResetCurrentItem();
                return false;
            }

            List<DocumentFolderFileList> childs = _originalSource.FindAll(delegate(DocumentFolderFileList item)
            { return item.ParentID == CurrentRow.ParentID && item.ID != CurrentRow.ID; });

            bool hasExist = false;
            if (childs != null && childs.Count > 0)
            {
                foreach (var item in childs)
                {
                    if (item.Name == CurrentRow.Name) { hasExist = true; break; }
                }
            }
            if (hasExist)
            {
                DocumentFolderFileList currentRow = CurrentRow;
                currentRow.Name = _beforeReNameName;
                bsList.ResetBindings(false);
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Exist Name." : "文件名重复.");
                return false;
            }
            return true;
        }

        #endregion

        #region listView Event

        private void lvMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = lvMain.GetItemAt(e.X, e.Y);

            if (item == null) return;
            DocumentFolderFileList data = item.Tag as DocumentFolderFileList;
            if (data == null) return;
            if (data.DocumentType == OADocumentType.Folder)
            {
                TreeListNode tn = treeMain.FindNodeByKeyID(data.ID);
                if (tn != null)
                {
                    treeMain.FocusedNode = tn;
                    treeMain.Refresh();
                }
            }
            else
            {
                try
                {
                    DocumentFileInfo docFileInfo = DocumentClientService.GetFileInfo(data.ID);

                    string tempPath = System.Windows.Forms.Application.StartupPath + "\\DocumentTempFiles\\";
                    if (Directory.Exists(tempPath) == false) Directory.CreateDirectory(tempPath);

                    string tempFileName = tempPath + docFileInfo.FileName;

                    if (File.Exists(tempFileName)) File.Delete(tempFileName);

                    FileStream fs = new FileStream(tempFileName, FileMode.Create, FileAccess.Write);
                    fs.Write(docFileInfo.Content, 0, docFileInfo.Content.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(tempFileName);
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Open File Failed" : "打开文件失败") + ex.Message); }
            }

        }

        #region ReName Doc
        private void lvMain_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Item < 0 || lvMain.Items.Count < e.Item || lvMain.Items[e.Item] == null)
            {
                e.CancelEdit = true;
                return;
            }

            DocumentFolderFileList listItem = lvMain.Items[e.Item].Tag as DocumentFolderFileList;
            if (listItem == null || listItem.Permission < DocuentPermission.Manager || listItem.DocumentType == OADocumentType.File)
            {
                e.CancelEdit = true;
                return;
            }
        }

        private void lvMain_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            ReNameDoc(e);
        }

        private void ReNameDoc(LabelEditEventArgs e)
        {
            DocumentFolderFileList listItem = lvMain.Items[e.Item].Tag as DocumentFolderFileList;

            if (string.IsNullOrEmpty(e.Label) || e.Label.Trim().Length == 0)
            {
                e.CancelEdit = true;
                return;
            }

            try
            {
                SingleResultData result = null;
                if (listItem.DocumentType == OADocumentType.Folder)
                {
                    result = DocumentClientService.SaveFolderInfo(listItem.ID
                                                        , e.Label
                                                        , listItem.ParentID
                                                        , LocalData.UserInfo.LoginID
                                                        , listItem.UpdateDate);
                }
                else
                {
                    result = DocumentClientService.SaveFileInfo(listItem.ID
                                                    , e.Label
                                                    , listItem.Description
                                                    , listItem.ParentID
                                                    , LocalData.UserInfo.LoginID
                                                    , listItem.UpdateDate);
                }
                listItem.ID = result.ID;
                listItem.UpdateDate = result.UpdateDate;
                listItem.BeginEdit();

                List<DocumentFolderFileList> source = bsList.DataSource as List<DocumentFolderFileList>;
                if (source == null || source.Count == 0) return;
                DocumentFolderFileList tager = source.Find(delegate(DocumentFolderFileList item) { return item.ID == listItem.ID; });
                if (tager != null)
                {
                    tager.Name = e.Label;
                    int index = bsList.IndexOf(tager);
                    if (index < 0) return;
                    bsList.ResetItem(index);
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Set Successfully" : "设置成功");
            }
            catch (Exception ex)
            {

                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        #endregion

        #region Lv Drag Out
        bool _lvHiting = false;
        private void lvMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                _lvHiting = false;

                ListViewItem lvItem = lvMain.GetItemAt(e.X, e.Y);
                if (lvItem == null) return;
                _lvHiting = true;
            }
        }

        private void lvMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (_lvHiting)
            {
                if (lvMain.SelectedItems.Count == 0) return;
                foreach (ListViewItem item in lvMain.SelectedItems)
                {
                    DocumentFolderFileList doc = item.Tag as DocumentFolderFileList;
                    if (doc.Permission <= DocuentPermission.View) return;
                }

                lvMain.DoDragDrop(lvMain.SelectedItems, DragDropEffects.Copy);
            }
        }

        private void lvMain_MouseLeave(object sender, EventArgs e)
        {
            _lvHiting = false;
        }
        private void lvMain_MouseUp(object sender, MouseEventArgs e)
        {
            _lvHiting = false;
            if (e.Button == MouseButtons.Right)
            {
                if (RefreshLvBarItemEnabled() == false) return;

                popupMenuLv.ShowPopup(MousePosition);
            }
        }
        private bool RefreshLvBarItemEnabled()
        {
            if (CurrentRow == null || CurrentRow.Permission <= DocuentPermission.View)
                barLvReName.Enabled = barLvUpLoadFile.Enabled = barLvNewFolder.Enabled = false;
            else
                barLvReName.Enabled = barLvUpLoadFile.Enabled = barLvNewFolder.Enabled = true;

            List<DocumentFolderFileList> currentListViewSelectedItem = CurrentListViewSelectedItem;
            if (currentListViewSelectedItem == null || currentListViewSelectedItem.Count == 0)
            {
                barLvReName.Enabled = barLvDelete.Enabled = barLvDownLoadFile.Enabled = false;
            }
            else
            {
                DocumentFolderFileList fristItem = currentListViewSelectedItem[0];
                if (fristItem.Permission < DocuentPermission.Manager)
                    barLvReName.Enabled = barLvDelete.Enabled = false;
                else
                    barLvReName.Enabled = barLvDelete.Enabled = true;

                if (fristItem.DocumentType == OADocumentType.Folder)
                {
                    barLvDownLoadFile.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barLvDownLoadFile.Enabled = false;
                    barLvReName.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else
                {
                    barLvDownLoadFile.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    barLvDownLoadFile.Enabled = true;
                    barLvReName.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
            }

            bool enbled = false;
            for (int i = 0; i < popupMenuLv.ItemLinks.Count; i++)
            {
                if (popupMenuLv.ItemLinks[i].Enabled) { enbled = true; break; }
            }

            return enbled;
        }

        #endregion

        #region lv Drag Enter

        private void lvMain_DragEnter(object sender, DragEventArgs e)
        {
            ListView.SelectedListViewItemCollection lvSelectItem = e.Data.GetData(typeof(ListView.SelectedListViewItemCollection)) as ListView.SelectedListViewItemCollection;
            if (lvSelectItem == null || lvSelectItem.Count == 0) return;

            e.Effect = DragDropEffects.Copy;
        }

        private void lvMain_DragDrop(object sender, DragEventArgs e)
        {
            ListView.SelectedListViewItemCollection lvSelectItem = e.Data.GetData(typeof(ListView.SelectedListViewItemCollection)) as ListView.SelectedListViewItemCollection;
            if (lvSelectItem == null || lvSelectItem.Count == 0) return;

            Point clientPoint = lvMain.PointToClient(new Point(e.X, e.Y));
            ListViewItem tagerItem = lvMain.GetItemAt(clientPoint.X, clientPoint.Y);
            if (tagerItem == null) return;

            List<DocumentFolderFileList> currentListViewSelectedItem = CurrentListViewSelectedItem;
            DocumentFolderFileList tagerDoc = tagerItem.Tag as DocumentFolderFileList;

            SetDrag2Parent(CurrentListViewSelectedItem, tagerDoc);
        }

        #endregion

        private void lvMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDeleteBarEnabled();
        }

        private void RefreshDeleteBarEnabled()
        {
            if (lvMain.SelectedItems == null || lvMain.SelectedItems.Count == 0)
                barDeleteBar.Enabled = false;
            else
            {
                foreach (ListViewItem item in lvMain.SelectedItems)
                {
                    DocumentFolderFileList data = item.Tag as DocumentFolderFileList;
                    if (data.Permission >= DocuentPermission.Manager)
                    {
                        barDeleteBar.Enabled = true;
                    }
                }
            }
        }

        private void lvMain_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == _lvwColumnSorter.SortColumn)
            {
                // 重新设置此列的排序方法.
                if (_lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    _lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    _lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // 设置排序列，默认为正向排序
                _lvwColumnSorter.SortColumn = e.Column;
                _lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // 用新的排序方法对ListView排序
            this.lvMain.Sort();

        }

        private void lvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && lvMain.SelectedItems != null && lvMain.SelectedItems.Count > 0)
            {
                lvMain.SelectedItems[0].BeginEdit();
            }
            else if (e.KeyCode == Keys.Back && CurrentRow != null)
            {
                TreeListNode tn = treeMain.FindNodeByKeyID(CurrentRow.ParentID);
                if (tn == null) return;
                else treeMain.FocusedNode = tn;
            }
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                bsList.DataSource = value;
                bsList.ResetBindings(false);
                //首两层展开
                foreach (TreeListNode tn in treeMain.Nodes)
                {
                    tn.Expanded = true;
                    foreach (TreeListNode item in tn.Nodes)
                    {
                        item.Expanded = true;
                    }
                }
            }
        }

        public override event CancelEventHandler CurrentChanging;
        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;

        #endregion

        private void btnUpgrade_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormUpgradeCloud uc = this.Workitem.Items.AddNew<FormUpgradeCloud>();
            ICP.Framework.ClientComponents.Controls.PartLoader.ShowDialog(uc, "Upgrade");
        }

    }
}
