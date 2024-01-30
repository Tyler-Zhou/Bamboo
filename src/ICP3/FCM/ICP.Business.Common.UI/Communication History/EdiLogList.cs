using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.EDI.ServiceInterface.DataObjects;
using ICP.EDI.ServiceInterface;
using System.IO;
using ICP.Message.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.Business.Common.UI.Communication_History
{
    public partial class EdiLogList : BaseEditPart
    {
        /// <summary>
        /// EDI客户端服务
        /// </summary>
        public IEDIService ediService
        {
            get
            {
                return ServiceClient.GetService<IEDIService>();
            }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        List<LogData> dataSourse = null;

        /// <summary>
        /// 查询条件
        /// </summary>
        string query = string.Empty;

        DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = new DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo();

        public EdiLogList()
        {
            InitializeComponent();
        }

        private void EdiLogList_Load(object sender, EventArgs e)
        {
            gridViewList.IndicatorWidth = 40;
            if (dataSourse == null)
            {
                string josn = ediService.GetLogStateList(LocalData.UserInfo.LoginID, query);
                dataSourse = JSONSerializerHelper.DeserializeFromJson<List<LogData>>(josn);
                gridControlList.DataSource = dataSourse;
                gridViewList.RefreshData();
            }

            if (query == "AMS")
            {
                btWeb.Visible = true;
            }
            else
            {
                btWeb.Visible = false;
            }
        }

        public void SetQuery(string query)
        {
            this.query = query;
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            if (gridViewList.FocusedRowHandle < 0)
            {
                return;
            }

            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            openFileDialog.ShowDialog();

            string downpath = openFileDialog.SelectedPath;
            string filename = dataSourse[gridViewList.FocusedRowHandle].FileName;
            byte[] files = dataSourse[gridViewList.FocusedRowHandle].Filebyte;
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

        private void gridViewList_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Name == "colState")
            {
                switch (Convert.ToInt32(e.Value))
                {
                    case 1:
                        e.DisplayText = "发送中";
                        break;
                    case 2:
                        e.DisplayText = "发送成功";
                        break;
                    case 3:
                        e.DisplayText = "发送失败";
                        break;
                    case 4:
                        e.DisplayText = "草稿";
                        break;
                    case 5:
                        e.DisplayText = "已发送";
                        break;
                    case 6:
                        e.DisplayText = "EDI失败";
                        break;
                    case 7:
                        e.DisplayText = "EDI成功";
                        break;
                    case 8:
                        e.DisplayText = "船东认可";
                        break;
                }
            }
        }

        private void gridViewList_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {

            //.DefaultCellStyle.Font = new Font("宋体", 9, FontStyle.Strikeout);
            // gridControlList.SelectedRows[0].DefaultCellStyle.ForeColor = Color.Blue;
            //  gridControlList.SelectedRows[0].DefaultCellStyle.BackColor = Color.Red;
        }

        private void gridViewList_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                LogData data = dataSourse[e.RowHandle];
                if (data.State == MessageState.EdiSuccess)
                {
                    e.Appearance.ForeColor = Color.Green;
                }
                else if (data.State == MessageState.EdiFailure)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void gridViewList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void btWeb_Click(object sender, EventArgs e)
        {
            AmsConfirm imoflag = new AmsConfirm();
            imoflag.Show();
        }


        private void gridViewList_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewList.FocusedRowHandle < 0) return;
            try
            {
                //双击某一单元格，执行代码  
                if (hInfo.InRowCell)
                {
                    if (btWeb.Visible)
                    {
                        btWeb_Click(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void gridViewList_MouseDown(object sender, MouseEventArgs e)
        {
            hInfo = gridViewList.CalcHitInfo(e.Y, e.Y);
        }
    }
}
