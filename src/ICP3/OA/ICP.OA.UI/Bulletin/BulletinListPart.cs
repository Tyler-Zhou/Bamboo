using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.OA.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.OA.ServiceInterface;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary;

namespace ICP.OA.UI.Bulletin
{   
    /// <summary>
    /// 公告列表界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class BulletinListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public IBulletinService BulletinService
        {
            get
            {
                return ServiceClient.GetService<IBulletinService>();
            }
        }


        public BulletinUIDataHelper BulletinUIDataHelper
        {
            get
            {
                return ClientHelper.Get<BulletinUIDataHelper, BulletinUIDataHelper>();
            }
        }

        #endregion

        #region init

        public BulletinListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.CurrentChanged = null;
              
                this.gcMain.DataSource = null;
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

            if (!LocalData.IsDesignMode) InitMessage();
        }

        private void InitMessage()
        {
            this.RegisterMessage("DeleteSuccessfully",LocalData.IsEnglish?"Delete Successfully":"删除成功");
            this.RegisterMessage("EditBulletinTitel",LocalData.IsEnglish?"Edit Bulletin":"编辑公告");
        }

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        private void gvMain_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                Command_EditData(null, null);
            }
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected BulletinData CurrentRow
        {
            get { return Current as BulletinData; }
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
                if (CurrentChanged != null) CurrentChanged(this, Current);
            }
        }

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;

        #endregion

        #region Workitem Common

        [CommandHandler(BulletinCommonConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs e)
        {
            BulletinData newData = new BulletinData();
            newData.Priority = BulletinPriority.Normal;
            newData.Publisher = LocalData.UserInfo.LoginName;
            newData.BulletinType = BulletinUIDataHelper.BulletinTypes[0].ID;
            newData.Departments = new List<Guid>();
            newData.CreateTime = newData.FromTime = newData.ToTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);

            BulletinEditPart editPart = Workitem.Items.AddNew<BulletinEditPart>();
            editPart.DataSource = newData;
            string text = LocalData.IsEnglish ? "Add Bulletin" : "新增公告";
            DialogResult dr = PartLoader.ShowDialog(editPart,text, FormBorderStyle.Sizable);
            if (dr != DialogResult.OK) return;
            EditPartSaved(editPart.DataSource as BulletinData);
        }

        [CommandHandler(BulletinCommonConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            BulletinEditPart editPart = Workitem.Items.AddNew<BulletinEditPart>();
            editPart.DataSource = CurrentRow;
            string text = LocalData.IsEnglish ? "Edit Bulletin" : "编辑公告";
            DialogResult dr = PartLoader.ShowDialog(editPart, text, FormBorderStyle.Sizable);
            if (dr != DialogResult.OK) return;
            EditPartSaved(editPart.DataSource as BulletinData);
            
        }

        void EditPartSaved(BulletinData data)
        {
            List<BulletinData> source = this.DataSource as List<BulletinData>;
            if (source == null || source.Count == 0)
            {
                bsList.Add(data);
                bsList.ResetBindings(false);
            }
            else
            {
                BulletinData tager = source.Find(delegate(BulletinData item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    Utility.CopyToValue(data, tager, typeof(BulletinData));
                    bsList.ResetItem(bsList.IndexOf(tager));
                }
            }
        }

        [CommandHandler(BulletinCommonConstants.Command_DeleteData)]
        public void Command_DeleteData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                if (DataTypeHelper.GetString(CurrentRow.Publisher, string.Empty).ToUpper() != LocalData.UserInfo.LoginName
                    &&DataTypeHelper.GetString(CurrentRow.Publisher, string.Empty).ToUpper()!=LocalData.UserInfo.UserName
                    && DataTypeHelper.GetString(CurrentRow.Publisher, string.Empty).ToUpper() != LocalData.UserInfo.UserEname)
                {
                    return;
                }

                if (Utility.EnquireIsDeleteCurrentData() == false) return;

                if (Utility.GuidIsNullOrEmpty(CurrentRow.ID) == false)
                    BulletinService.DeleteBulletinByID(CurrentRow.ID);

                bsList.RemoveCurrent();
                if (CurrentChanged != null) CurrentChanged(this, Current);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), NativeLanguageService.GetText(this, "DeleteSuccessfully"));
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        #endregion
    }
}
