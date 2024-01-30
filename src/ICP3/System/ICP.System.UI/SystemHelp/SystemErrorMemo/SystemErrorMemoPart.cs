using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using sys = ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System.IO;

namespace ICP.Sys.UI.SystemHelp
{
    /// <summary>
    /// 记录系统错误日志
    /// </summary>
    public partial class SystemErrorMemoPart : BasePart
    {
        private int time_Sapce = -2;
        private List<SystemErrorLogObject> dataList = null;

        #region Service
        [ServiceDependency]
        public WorkItem workItem
        {
            get;
            set;

        }

        public sys.IOperationMemoService SystemErrorService
        {
            get { return ServiceClient.GetService<sys.IOperationMemoService>(); }
        }
        #endregion

        #region 属性
        public object DataSource
        {
            get { return bsErrorLogs.DataSource; }
            set
            {
                bsErrorLogs.DataSource = value;
                bsErrorLogs.ResetBindings(false);
            }
        }

        public int RowCount { get; set; }

        public SystemErrorLogObject CurrentRow
        {
            get { return bsErrorLogs.Current as SystemErrorLogObject; }
        }
        #endregion

        #region 初始化
        public SystemErrorMemoPart()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                if (!LocalData.IsEnglish)
                    InitCtl();
            }
            this.Disposed += delegate { DisposedComponent(); };
        }

        private void DisposedComponent()
        {
            this.dataList = null;
            this.gridErrorLogList.DoubleClick -= this.gridErrorLogList_DoubleClick;
            this.gridErrorLogList.DataSource = null;
            this.gvErrorLogList.Click -= this.gvErrorLogList_Click;
            this.bsErrorLogs.DataSource = null;
            this.bsErrorLogs.Dispose();
            if (this.workItem != null)
            {
                workItem.Items.Remove(this);
                this.workItem = null;
            }
        }

        private void InitCtl()
        {
            this.lblFrom.Text = "从";
            this.lblTo.Text = "到";
            this.btnSearch.Text = "搜索(&S)";
            this.btnTop.Text = "最顶端";
            this.btnBottom.Text = "最底端";
            this.txtUserName.Properties.NullText = " -- 用户名 -- ";
            this.colPreview.Caption = string.Empty;
            this.colPreview.Caption = " ";
            this.colUserName.Caption = "用户";
            this.colDescription.Caption = "描述";
            this.colCreateTime.Caption = "时间";

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
                Init();
        }
        #endregion

        #region 方法
        private void Init()
        {
            SetDataEditDataSource();
            WaitCallback callback = (obj) => this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    try
                    {
                        BindingData(string.Empty, DateTime.Now.AddDays(time_Sapce), DateTime.Now);
                    }
                    catch (System.Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                    }
                });
            ThreadPool.QueueUserWorkItem(callback);

        }

        private string GetUserName()
        {
            return this.txtUserName.EditValue == null ? "" :
            txtUserName.EditValue.ToString().Trim();
        }

        void SetDataEditDataSource()
        {
            this.rtxt_100.PopupFormWidth = this.colDescription.Width + 200;
            this.dateFrom.EditValue = DateTime.Now.AddDays(time_Sapce);
            this.dateTo.EditValue = DateTime.Now;
        }

        private void BindingData(string userName, DateTime fromDate, DateTime toDate)
        {
            dataList = SystemErrorService.GetSystemErrorLogList(userName, fromDate, toDate);
            this.DataSource = dataList;
            RowCount = dataList.Count;
        }

        private DateTime ConvertStringToDate(object date)
        {
            if (date == null || string.IsNullOrEmpty(date.ToString()))
                return DateTime.Now;
            else
            {
                DateTime time;
                DateTime.TryParse(date.ToString(), out time);
                return time;
            }
        }

        public string WriteStreamToImage(byte[] bytes)
        {
            string filePath = string.Empty;
            if (bytes == null)
                return string.Empty;
            filePath = Path.Combine(Path.GetTempPath(), string.Format("{0}{1}", CurrentRow.SessionID.ToString(), ".jpeg"));
            if (!CheckFileExsits(filePath))
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes))
                {
                    try
                    {
                        System.Drawing.Image.FromStream(ms).Save(filePath, ImageFormat.Jpeg);
                        ms.Close();
                    }
                    catch { }
                }
            }

            return filePath;
        }

        private bool CheckFileExsits(string filePath)
        {
            return File.Exists(filePath);
        }

        private void ShowImage()
        {
            if (CurrentRow == null)
                return;
            SystemErrorLogObject entity = SystemErrorService.GetSystemErrorLogInfoById(CurrentRow.ID);
            if (entity != null)
            {
                string filePath = WriteStreamToImage(entity.ScreenCapture);
                if (File.Exists(filePath))
                    InnerPreview(filePath);
                else
                    throw new NullReferenceException(LocalData.IsEnglish ? "The preview image isn't exsit!" : "预览的图片不存在！");
            }
            else
            {
                throw new NullReferenceException(LocalData.IsEnglish ? "The preview image isn't exsit!" : "预览的图片不存在！");
            }
        }

        Point _location;
        Size _size;
        bool _isSet = false;
        private void GetPositionAndSize()
        {
            if (!_isSet)
            {
                Screen scr = Screen.FromPoint(this.gridErrorLogList.Location);
                _location = new Point((int)(scr.WorkingArea.Width * 0.4), LocalData.Height);
                int height = scr.WorkingArea.Height - LocalData.Height;
                int width = (int)(scr.WorkingArea.Width * 0.6);
                _size = new Size(width, height);
                _isSet = true;
            }
        }

        private void InnerPreview(string filePath)
        {
            GetPositionAndSize();
            ServiceClient.FilePreviewService.Preview(filePath, _location, _size, true);
        }

        private void OnSearched()
        {
            int theradID = 0;
            theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm();
            BindingData(GetUserName(), ConvertStringToDate(dateFrom.EditValue), ConvertStringToDate(dateTo.EditValue));
            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
        }

        private void SetSelectRow(int rowIndex)
        {
            gvErrorLogList.FocusedRowHandle = rowIndex;
            gvErrorLogList.SelectRow(rowIndex);
        }

        private void OnShow(bool isDoubleClick)
        {
            if (gvErrorLogList.GetFocusedDisplayText().Equals(LocalData.IsEnglish ? "Preview" : "预览") || isDoubleClick)
            {
                try
                {
                    ShowImage();
                }
                catch (System.Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                }
            }
        }

        #endregion

        #region 事件
        private void btnSearch_Click(object sender, EventArgs e)
        {
            OnSearched();
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            SetSelectRow(0);
        }

        private void Bottom_Click(object sender, EventArgs e)
        {
            SetSelectRow(RowCount - 1);

        }
        private void gvErrorLogList_Click(object sender, EventArgs e)
        {
            OnShow(false);
        }

        private void gridErrorLogList_DoubleClick(object sender, EventArgs e)
        {
            OnShow(true);
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                OnSearched();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

    }
}
