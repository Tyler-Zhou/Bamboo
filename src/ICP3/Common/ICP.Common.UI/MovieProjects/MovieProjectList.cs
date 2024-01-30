using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.Common.UI
{
    [ToolboxItem(false)]
    public partial class MovieProjectList : BaseListPart
    {

        public MovieProjectList()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.CurrentChanged = null;
                this.gcMain.DataSource = null;
                this.bsList.CurrentChanged -= this.bsBanksList_CurrentChanged;
                this.bsList.PositionChanged -= this.bsBanksList_PositionChanged;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        #endregion

        #region 初始化 
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMessage();
                InitControls();


                List<DataDictionaryList> DataList = TransportFoundationService.GetDataDictionaryList("","",DataDictionaryType.MovieProjects,true,0);
                bsList.DataSource = DataList;
                bsList.ResetBindings(false);

                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }
            }
        }

        private void InitMessage()
        {
            this.RegisterMessage("1108100001", LocalData.IsEnglish ? "Are you sure to invalidate the selected time?" : "确认要作废该数据？");
            this.RegisterMessage("1108100002", LocalData.IsEnglish ? "Are you sure to resume the selected time?" : "确认要激活该数据？");
        }

        private void InitControls()
        {
            CommonUtility.ShowGridRowNo(this.gvMain);
        }

        #endregion

        #region IListPart 成员
        public override event InvokeGetDataHandler InvokeGetData;

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected DataDictionaryList CurrentRow
        {
            get { return Current as DataDictionaryList; }
        }

        public override object DataSource
        {
            get
            {
                return this.bsList.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        public List<DataDictionaryList> DataSourceList
        {
            get
            {
                List<DataDictionaryList> list = DataSource as List<DataDictionaryList>;
                if (list == null)
                {
                    list = new List<DataDictionaryList>();
                }
                return list;
            }
        }

        private void BindingData(object value)
        {
            List<DataDictionaryList> list =value as List<DataDictionaryList>;
            if (list == null)
            {
                list =new List<DataDictionaryList>();
            }
            bsList.DataSource = list;
            bsList.ResetBindings(false);

            if (CurrentChanged != null)
            {
                CurrentChanged(this, Current);
            }   
        }
        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        #endregion

        #region 按钮方法
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(MovieProjectCommandConstants.Command_MovieProjectAdd)]
        public void Command_BankAdd(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
               // Utility.ShowEditPart<BankEdit>(Workitem, null, LocalData.IsEnglish ? "Add Bank" : "新增银行", EditPartSaved);
            }
        }

        [CommandHandler(MovieProjectCommandConstants.Command_MovieProjectCancel)]
        public void Command_MovieProjectCancel(object sender, EventArgs e)
        {
            if (this.CurrentRow != null)
            {
                if (CurrentRow.IsValid)
                {
                    if (!CommonUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1108100001")))
                    {
                        return;
                    }
                }
                else
                {
                    if (!CommonUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1108100002")))
                    {
                        return;
                    }
                }


                try
                {
                    SingleResultData result = TransportFoundationService.ChangeDataDictionaryState(
                        this.CurrentRow.ID,
                        !this.CurrentRow.IsValid,
                        LocalData.UserInfo.LoginID,
                        this.CurrentRow.UpdateDate);

                    CurrentRow.IsValid = !CurrentRow.IsValid;
                    CurrentRow.ID = result.ID;
                    CurrentRow.UpdateDate = result.UpdateDate;

                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, CurrentRow);
                    }

                    if (this.CurrentRow.IsValid)
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Cancel Booking Successfully." : "项目信息已经成功作废.");
                    else
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Available Booking Successfully." : "项目信息已经成功激活.");

                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(),  ex.Message);
                }
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 编辑界面保存数据
        /// </summary>
        /// <param name="prams"></param>
        public void EditPartSaved(object[] prams)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (prams == null || prams.Length == 0) return;

                DataDictionaryList data = prams[0] as DataDictionaryList;

                List<DataDictionaryList> source = this.DataSource as List<DataDictionaryList>;
                if (source == null || source.Count == 0)
                {
                    bsList.Add(data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    DataDictionaryList tager = source.Find(delegate(DataDictionaryList item) { return item.ID == data.ID; });
                    if (tager == null)
                    {
                        bsList.Insert(0, data);
                        bsList.ResetBindings(false);
                    }
                    else
                    {
                        CommonUtility.CopyToValue(data, tager, typeof(DataDictionaryList));

                        bsList.ResetItem(bsList.IndexOf(tager));
                    }

                }
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }
            }
        }
        #endregion

        #region Grid Event
        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            DataDictionaryList list = gvMain.GetRow(e.RowHandle) as DataDictionaryList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            }

        }
        private void bsBanksList_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void gvMain_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            //if (CurrentChanging != null)
            //{
            //    CancelEventArgs ce = new CancelEventArgs();
            //    CurrentChanging(this, ce);
            //    e.Allow = !ce.Cancel;
            //}
        }


        private void bsBanksList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }
        #endregion

    }
}
