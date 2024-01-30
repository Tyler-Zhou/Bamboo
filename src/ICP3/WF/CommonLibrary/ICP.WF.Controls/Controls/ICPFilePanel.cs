using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Attributes;
using Microsoft.Practices.CompositeUI;
using ICP.WF.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;
using System.IO;
using ICP.OA.UI.Document;
using ICP.FileSystem.ServiceInterface;
using ICP.OA.ServiceInterface.DataObjects;
using ICP.OA.UI;

namespace ICP.WF.Controls
{
    /// <summary>
    /// 附件控件
    /// </summary>
    [ToolboxBitmap(typeof(BaseEdit), "Bitmaps256.ComboBoxEdit.bmp")]
    [Designer("DevExpress.XtraEditors.Design.DateEditDesigner, DevExpress.XtraEditors.v10.1.Design")]
    [SRDescription("ICPFilePanelDesc"), SRTitle("ICPFilePanelTitle")]
    [Serializable()]
    public partial class ICPFilePanel : PanelControl, IDataSourceService, IValidateService, IFileService, ITable
    {
        public ICPFilePanel()
        {
            InitializeComponent();
        }



        #region 属性
        [SRCategory("DataCustom"), ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true)]
        public new bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }
        int columnSpan = 1;
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("ColumnSpan"),
        ICPBrowsable(true),
        SRCategory("Layout"),
        SRDescription("ColumnSpan")]
        public int ColumnSpan
        {
            get { return columnSpan; }
            set
            {
                if (value != columnSpan)
                {
                    columnSpan = value;

                    if (this.Parent != null && this.Parent is LWTableLayoutPanel)
                    {
                        LWTableLayoutPanel panel = this.Parent as LWTableLayoutPanel;
                        panel.SetColumnSpan(this, value);
                    }
                }
            }
        }

        int rowSpan = 1;
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("RowSpan"),
        ICPBrowsable(true),
        SRCategory("Layout"),
        SRDescription("RowSpan")]
        public int RowSpan
        {
            get { return rowSpan; }
            set
            {
                if (value != rowSpan)
                {
                    rowSpan = value;

                    if (this.Parent != null && this.Parent is LWTableLayoutPanel)
                    {
                        LWTableLayoutPanel panel = this.Parent as LWTableLayoutPanel;
                        panel.SetRowSpan(this, value);
                    }
                }
            }
        }

        [SRDisplayName("Name"), ICPBrowsable(true), SRCategory("Base")]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        /// <summary>
        /// 停靠
        /// </summary>
        [SRDisplayName("Anchor"), ICPBrowsable(true)]
        public new AnchorStyles Anchor
        {
            get { return base.Anchor; }
            set { base.Anchor = value; }
        }
        /// <summary>
        /// 布局
        /// </summary>
        [SRDisplayName("Dock"), ICPBrowsable(true), SRDescription("Dock")]
        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = value;
            }
        }
        /// <summary>
        /// Tab顺序
        /// </summary>
        [SRDisplayName("DispTabIndex"),
        ICPBrowsable(true),
        SRDescription("DispTabIndex")]
        public new int TabIndex
        {
            get { return base.TabIndex; }
            set { base.TabIndex = value; }
        }

        [ICPBrowsable(true)]
        public new int Width
        {
            get
            {
                return base.Width;
            }
            set
            {
                base.Width = value;
            }
        }
        [ICPBrowsable(true)]
        public new int Height
        {
            get
            {
                return base.Height;
            }
            set
            {
                base.Height = value;
            }
        }
        #endregion

        #region 服务

        LWBaseForm GetWFParentForm(Control control)
        {
            if (control.Parent is LWBaseForm)
            {
                return control.Parent as LWBaseForm;
            }
            else
            {
                return GetWFParentForm(control.Parent);
            }
        }

        public ICP.OA.ServiceInterface.Client.IDocumentClientService docService
        {
            get
            {
                return ServiceClient.GetClientService<ICP.OA.ServiceInterface.Client.IDocumentClientService>();
            }
        }

        public IFileSystemService FileServiceWCF
        {
            get
            {
                return ServiceClient.GetService<IFileSystemService>();
            }
        }

        #endregion

        #region IDataSourceService 接口实现

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DataTableName
        {
            get
            {
                string tableName = Utility.GetPascalProperty(this.Name);
                if (string.IsNullOrEmpty(tableName))
                {
                    tableName = this.Name;
                }

                return tableName;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object DataTable
        {
            get
            {

                return bsList.DataSource;
            }
            set
            {
                bsList.DataSource = value;
            }
        }

        #endregion

        #region ITableService 接口实现
        public DataTable BuildTable()
        {
            string tableName = Utility.GetPascalProperty(this.Name);
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = this.Name;
            }

            DataColumn dcID = new DataColumn("ID", typeof(Guid));
            DataColumn dcName = new DataColumn("FileName");

            DataTable table = new DataTable(tableName);

            table.Columns.Add(dcID);
            table.Columns.Add(dcName);

            return table;
        }

        public DataTable GetTable()
        {
            DataTable dt = null;
            if (bsList.DataSource is DataTable)
            {
                dt = bsList.DataSource as DataTable;
            }
            else
            {
                dt = BuildTable();
            }

            return dt;
        }
        #endregion

        #region IFileService接口成员
        /// <summary>
        /// 设置按钮为只读
        /// </summary>
        public void SetButtonReadonly()
        {
            this.btnUpLoad.Enabled = false;
            this.btnDelete.Enabled = false;
        }
        #endregion

        #region IValidateService接口实现
        /// <summary>
        /// 验证(运行时)
        /// </summary>
        /// <param name="errorTip">错误提示控件</param>
        /// <param name="errors">错误里表</param>
        /// <returns>通过验证返回true,没通过返回false</returns>
        public bool ValidateForRuntime(ErrorProvider errorTip, List<string> errors)
        {
            bool isSucc = true;
            if (errors == null) errors = new List<string>();

            return isSucc;
        }

        /// <summary>
        /// 验证(对于设计时)
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool ValidateForDesign(List<string> errors)
        {
            return true;
        }
        #endregion

        #region 按钮

        protected override void OnLoaded()
        {
            base.OnLoaded();

            if (LocalData.IsEnglish)
            {
                this.btnUpLoad.Text = "Upload";
                this.btnDownLoad.Text = "Download";
                this.btnDelete.Text = "Delete";
            }
        }

        LWBaseForm vParent = null;
        IWorkFlowExtendService feService = null;
        /// <summary>
        ///  设置主面板及服务
        /// </summary>
        private void SetParentService()
        {
            if (vParent == null)
            {
                vParent = GetWFParentForm(this);
            }
            if (vParent != null && feService == null)
            {
                feService = (IWorkFlowExtendService)vParent.ServiceContainer.Get(typeof(IWorkFlowExtendService));
            }

        }
        private ICP.OA.ServiceInterface.DataObjects.FTPServerConfig ftpConfig;
        private ICP.OA.ServiceInterface.DataObjects.FTPServerConfig FtpConfig
        {
            get
            {
                if (ftpConfig == null)
                {
                    ftpConfig = feService.GetFTPServerConfig();
                }
                return ftpConfig;
            }
        }
        private WFFileItem CurrentData
        {
            get
            {
                if (bsList.Current == null)
                {
                    return null;
                }
                DataRowView dr = bsList.Current as DataRowView;
                if (dr == null)
                {
                    return null;
                }
                WFFileItem item = new WFFileItem();
                if (dr["ID"] != null)
                {
                    item.ID = new Guid(dr["ID"].ToString());
                }
                if (dr["FileName"] != null)
                {
                    item.FileName = dr["FileName"].ToString();
                }
                return item;
            }
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            try
            {
                SetParentService();
                if (feService == null)
                {
                    return;
                }
                OpenFileDialog openFile = new OpenFileDialog();
                if (openFile.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                string fileName = openFile.SafeFileName;

                Guid ID = Guid.NewGuid();

                ManyResultData result = new ManyResultData();
                List<DocumentClientFileInfo> currentSource = new List<DocumentClientFileInfo>();
                for (int i = 0; i < openFile.FileNames.Length; i++)
                {
                    FileInfo fi = new FileInfo(openFile.FileNames[i]);
                    if (currentSource.Find(delegate(DocumentClientFileInfo fItem) { return fItem.FullPath == openFile.FileNames[i]; }) != null) continue;

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
                    dcf.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    dcf.FileName = fi.Name;
                    dcf.FullPath = fi.FullName;

                    currentSource.Add(dcf);
                }

                List<Guid?> ids = new List<Guid?>();
                List<string> fileNames = new List<string>(), fileDescriptions = new List<string>();
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

                    ids.Add(ID);
                    updataDates.Add(null);
                    fileNames.Add(item.FileName);
                    fileDescriptions.Add(item.FileDescription);
                }
                //上传文档
                Guid folderID = new Guid("8562CB85-C9D3-45FF-91DD-EFE361C66C40");//月结协议
                result = docService.SaveFileInfo(ids.ToArray()
                                           , folderID
                                           , fileNames.ToArray()
                                           , fileDescriptions.ToArray()
                                           , contents.ToArray()
                                           , LocalData.UserInfo.LoginID
                                           , updataDates.ToArray());

                if (result.ChildResults.Count > 0)
                {
                    DataTable dt = GetTable();
                    DataRow dr = dt.NewRow();
                    dr["ID"] = result.ChildResults[0].ID;
                    dr["FileName"] = fileName;
                    dt.Rows.InsertAt(dr, 0);

                    bsList.DataSource = dt;
                    bsList.ResetBindings(false);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Upload success" : "上传成功");
                }

            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
                return;
            }

        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            if (bsList.Current == null || CurrentData == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Please select the file to be downloaded" : "请选择要下载的文件");
                return;
            }
            SetParentService();
            if (feService == null)
            {
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = CurrentData.FileName;
            if (sfd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(sfd.FileName))
            {
                DocumentFileInfo docFileInfo = docService.GetFileInfo(CurrentData.ID);
                string tempFileName = sfd.FileName;
                if (File.Exists(tempFileName)) File.Delete(tempFileName);
                FileStream fs = new FileStream(tempFileName, FileMode.Create, FileAccess.Write);
                fs.Write(docFileInfo.Content, 0, docFileInfo.Content.Length);
                fs.Close();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Download success!" : "下载成功!");
            }

        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridList.Rows.Count == 0)
            {
                return;
            }
            if (bsList.Current == null || CurrentData == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Please select the file to be delete" : "请选择要删除的文件");
                return;
            }
            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(
                                  LocalData.IsEnglish ? "Confirm delete the file?" : "确认删除该文件?",
                                  LocalData.IsEnglish ? "Tip" : "Tip",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            SetParentService();
            if (feService == null)
            {
                return;
            }

            docService.RemoveFileInfo(CurrentData.ID, LocalData.UserInfo.LoginID, null);
            //FTPClient ftp = new FTPClient();
            //try
            //{
            //    ftp.RemoteHost = FtpConfig.Host;
            //    ftp.RemotePath = FtpConfig.BasePath;
            //    ftp.UserName = FtpConfig.User;
            //    ftp.Password = FtpConfig.Password;
            //    ftp.Login();
            //    ftp.DeleteRemoteFile(CurrentData.ID.ToString());
            //}
            //catch (Exception ex)
            //{
            //    DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            //    return;
            //}
            //finally
            //{
            //    ftp.Close();
            //}

            DataTable dt = GetTable();
            DataRow removedr = null;
            foreach (DataRow dr in dt.Rows)
            {
                if (new Guid(dr["ID"].ToString()) == CurrentData.ID)
                {
                    removedr = dr;
                    dt.Rows.Remove(dr);
                    break;
                }
            }
            bsList.DataSource = dt;
            bsList.ResetBindings(false);

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Delete success!" : "删除成功!");
        }
        /// <summary>
        /// 单击查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridList_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (CurrentData == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(CurrentData.FileName))
            {
                return;
            }
            SetParentService();
            if (feService == null)
            {
                return;
            }
            string tempPath = System.Windows.Forms.Application.StartupPath + "\\DocumentTempFiles\\";
            if (Directory.Exists(tempPath) == false)
            {
                Directory.CreateDirectory(tempPath);
            }
            string tempFileName = tempPath + CurrentData.FileName;
            if (File.Exists(tempFileName))
            {
                File.Delete(tempFileName);
            }


            FTPClient ftp = new FTPClient();
            try
            {
                ftp.RemoteHost = FtpConfig.Host;
                ftp.RemotePath = FtpConfig.BasePath;
                ftp.UserName = FtpConfig.User;
                ftp.Password = FtpConfig.Password;
                ftp.Login();
                ftp.Download(CurrentData.ID.ToString(), tempFileName, false);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                ftp.Close();
            }
            if (File.Exists(tempFileName))
            {
                System.Diagnostics.Process.Start(tempFileName);
            }


        }
        #endregion
    }
    /// <summary>
    /// 附件
    /// </summary>
    public class WFFileItem
    {
        public WFFileItem() { }
        public WFFileItem(string fileName, Guid id)
        {
            FileName = fileName;
            ID = id;
        }
        /// <summary>
        /// 文件ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
    }
}
