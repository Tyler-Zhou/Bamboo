using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ICP.Common.ServiceInterface;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.DataCache.ServiceInterface;
using ICP.Business.Common.UI;
using System.IO;
using ICP.Business.Common.UI.Document;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Common;
using System.Drawing;
using ICP.FileSystem.ServiceInterface;

namespace ICP.FAM.UI.AccReceControl
{
    public partial class AccControlEdit : BaseEditPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        /// <summary>
        /// 文档列表呈现类
        /// </summary>
        private DocumentListPresenter presenter;
        /// <summary>
        /// 文档列表呈现类
        /// </summary>
        public DocumentListPresenter Presenter
        {
            get
            {
                return presenter;
            }
            set
            {
                presenter = value;
            }
        }

        /// <summary>
        /// 文档列表集合
        /// </summary>
        List<CustomerAgingLogAtts> Files = new List<CustomerAgingLogAtts>();

        public delegate void Ondelete();
        public event Ondelete ondelete;

        /// <summary>
        /// 页面操作 0 查看 1 新增 
        /// </summary>
        int formtype = 1;
        bool isSave = false;

        private Guid _currentCompanyid;
        private Guid _currentCustomerid;
        private CustomerAgingLogs currentLog = null;

        /// <summary>
        /// 前次预览文件ID
        /// </summary>
        private Guid _previousFileId;
        /// <summary>
        /// 前次预览文件路径
        /// </summary>
        private string _previousFilePath;
        /// <summary>
        /// 位置对象
        /// </summary>
        Point _location;
        /// <summary>
        /// 大小
        /// </summary>
        Size _size;
        /// <summary>
        /// 是否已经设置
        /// </summary>
        bool _isSet = false;
        #endregion

        public AccControlEdit()
        {
            InitializeComponent();

            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        private void AccControlEdit_Load(object sender, EventArgs e)
        {
            if (formtype == 0)
            {
                barSave.Enabled = false;
                barUpFiles.Enabled = false;
                barDelFile.Enabled = false;
            }

            radiosCustomer.SelectedIndex = currentLog.CustomerMark;
            cmbSubjectMod.Properties.Items.Add("！支票已寄出，支票已经在____寄出");
            cmbSubjectMod.Properties.Items.Add("！已安排付款，客人答应会在____时候安排付款");
            cmbSubjectMod.Properties.Items.Add("！对账中，客人回复还在对账单");
            cmbSubjectMod.Properties.Items.Add("！客人出差，客人____时候出差，____时候回来");
            cmbSubjectMod.Properties.Items.Add("！客人付款慢，客人每次付款金额较少/客人付款拖拉");
            cmbSubjectMod.SelectedIndex = -1;
        }

        public void SetFormType(int type)
        {
            formtype = type;
        }

        public void SetCustomerAndCompany(Guid companyid, Guid customerid)
        {
            _currentCompanyid = companyid;
            _currentCustomerid = customerid;
        }

        public override object DataSource
        {
            get
            {
                return bsAgingLog.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        private void BindingData(object value)
        {
            currentLog = value as CustomerAgingLogs;

            if (currentLog == null)
            {
                currentLog = new CustomerAgingLogs();
                currentLog.CustomerID = _currentCustomerid;
                currentLog.CompanyID = _currentCompanyid;

                bsAgingLog.DataSource = currentLog;
                bsList.DataSource = null;
                gvMain.SortInfo.Clear();
            }
            else
            {

                bsAgingLog.DataSource = currentLog;
                bsAgingLog.ResetBindings(false);

                Files = currentLog.logAtts;
                bsList.DataSource = currentLog.logAtts;
                gcMain.DataSource = currentLog.logAtts;
                gvMain.RefreshData();
            }
        }

        private void barUpFiles_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!isSave)
            {
                string message = LocalData.IsEnglish ? "You need to save the log before uploading the file. Do you want to save it immediately?" : "上传文件前需先保存日志，是否立即保存？";
                DialogResult result = XtraMessageBox.Show(message, "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    barSave_ItemClick(null, null);
                }
                else
                {
                    return;
                }
            }
            try
            {
                String[] filePaths = CommonUIUtility.SelectFilesToUpload();
                foreach (string filePath in filePaths)
                {
                    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                    byte[] buffur = new byte[fs.Length];
                    fs.Read(buffur, 0, (int)fs.Length);
                    string filename = Path.GetFileName(filePath);
                    CustomerAgingLogs current = DataSource as CustomerAgingLogs;
                    SingleResult result = FinanceService.SaveCustomerAgingLogAtts(current.ID, filename, buffur,LocalData.UserInfo.LoginID);
                    CustomerAgingLogAtts file = new CustomerAgingLogAtts();
                    if (result != null)
                    {
                        file.ID = result.GetValue<Guid>("ID");
                    }
                    file.CreateBy = LocalData.UserInfo.LoginID;
                    file.CreateByName = LocalData.UserInfo.LoginName;
                    file.CreateOn = DateTime.Now;
                    file.FileName = filename;
                    file.filebyte = buffur;
                    Files.Add(file);
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Upload files Successfully" : "上传文件成功");

                gcMain.DataSource = Files;
                gvMain.RefreshData();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Upload files failed" + ex.Message : "上传文件失败" + ex.Message);
            }
        }

        private void barDelFile_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvMain.FocusedRowHandle < 0) return;
 
            string message = LocalData.IsEnglish ? "Are you sure you want to delete the selected files?" : "你确定要删除所选择的文件吗?";
            DialogResult result = XtraMessageBox.Show(message, "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    int rowHandler = gvMain.FocusedRowHandle;
                    Guid id = Files[rowHandler].ID;
                    FinanceService.DeleteCustomerAgingLogAtts(id);

                    gvMain.DeleteRow(rowHandler);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Delete failed" + ex.Message : "删除失败" + ex.Message);
                }
            }
        }

        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            CustomerLogSaveSequest savequest = new CustomerLogSaveSequest();
            bsAgingLog.EndEdit();

            try
            {
                CustomerAgingLogs current = DataSource as CustomerAgingLogs;
                if (string.IsNullOrEmpty(current.Content) || string.IsNullOrEmpty(current.Subject))
                {
                    string message = LocalData.IsEnglish ? "Please fill in the Subject and Description!" : "请填写主题和描述！";
                    XtraMessageBox.Show(message, "Tips", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                savequest.Id = current.ID;
                savequest.CompanyId = current.CompanyID;
                savequest.CustomerId = current.CustomerID;
                savequest.Content = current.Content;
                savequest.CustomerMark = (byte)radiosCustomer.SelectedIndex;
                savequest.Priority = 1;
                savequest.Subject = current.Subject;
                savequest.SaveBy = LocalData.UserInfo.LoginID;
                savequest.Type = 1;

                //List<CustomerAgingLogAtts> logatts = gcMain.DataSource as List<CustomerAgingLogAtts>;
                //if (logatts != null)
                //{
                //    savequest.LogAttsIds = (from p in logatts select p.ID).ToArray();
                //}

                SingleResult result = FinanceService.SaveCustomerAgingLog(savequest);
                isSave = true;
                current.ID = result.GetValue<Guid>("ID");
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save failed" + ex.Message : "保存失败" + ex.Message);
            }
        }

        private void gcMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            String filePath = string.Empty;
            try
            {
                CustomerAgingLogAtts att = Files[gvMain.FocusedRowHandle];
                if (att != null && att.ID != _previousFileId)
                {
                    ContentInfo info = new ContentInfo();
                    info.Id = att.ID;
                    info.Name = att.FileName;
                    info.Content = att.filebyte;

                    filePath = DataCacheUtility.SaveFileHtmlContentToDisk(info);
                    info = null;
                }
                else
                {
                    filePath = _previousFilePath;
                }
                GetPositionAndSize();
                ServiceClient.FilePreviewService.Preview(filePath, _location, _size, true);
                _previousFileId = att.ID;
                _previousFilePath = filePath;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, ex);
            }
        }

        /// <summary>
        /// 获取预览窗格位置及其窗体大小
        /// </summary>
        private void GetPositionAndSize()
        {
            if (!_isSet)
            {
                Screen scr = null;
                scr = Screen.PrimaryScreen;
                _location = new Point((int)(scr.WorkingArea.Width * 0.4), LocalData.Height);
                int height = scr.WorkingArea.Height - LocalData.Height;
                int width = (int)(scr.WorkingArea.Width * 0.6);
                _size = new Size(width, height);
                _isSet = true;
            }
        }

        private void AccControlEdit_Closing(object sender, FormClosingEventArgs e)
        {
            if (ondelete != null)
                ondelete();
        }

        private void barDown_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvMain.FocusedRowHandle < 0)
            {
                string message = LocalData.IsEnglish ? "Please select the file you want to delete" : "请选择要删除的文件";
                XtraMessageBox.Show(message, "Tips", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            openFileDialog.ShowDialog();

            string downpath = openFileDialog.SelectedPath;
            string filename = Files[gvMain.FocusedRowHandle].FileName;
            byte[] files = Files[gvMain.FocusedRowHandle].filebyte;
            using (FileStream outputStream = new FileStream(downpath + "\\" + filename, FileMode.Create))
            {
                using (Stream ftpStream = new MemoryStream(files))
                {
                    int bufferSize = 2048;
                    int readCount;
                    byte[] buffer = new byte[bufferSize];
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                    while (readCount > 0)
                    {
                        outputStream.Write(buffer, 0, readCount);
                        readCount = ftpStream.Read(buffer, 0, bufferSize);
                    }
                }
            }
        }

        private void cmbSubjectMod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSubjectMod.SelectedIndex > -1)
            {
                txtSubject.Text = cmbSubjectMod.Text;
                txtSubject.Focus();
                int index = txtSubject.Text.IndexOf("_");
                if(index > -1)
                {
                    txtSubject.Select(index, 4);
                }
            }
        }
    }
}
