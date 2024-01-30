using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.Common.UI.Finder
{
    [ToolboxItem(false)]
    public partial class BusinessListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }
      
        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService { get; set; }

        #endregion

        #region Init

        public BusinessListPart()
        {
            InitializeComponent();

            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            this.Load += new EventHandler(BookingListPart_Load);         
        }

        bool _shown = false;
        void BookingListPart_Load(object sender, EventArgs e)
        {
            this.DataSource = new List<BusinessData>();
            
            _shown = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }

        private void InitControls()
        {
            //OrderState
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OEOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OEOrderState>(LocalData.IsEnglish);
            rcmbState.Properties.BeginUpdate();
            foreach (var item in orderStates)
            {
                rcmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            rcmbState.Properties.EndUpdate();


            //OEOperationType
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OperationType>> oeoperationType = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OperationType>(LocalData.IsEnglish);
            cmbOEOperationType.Properties.BeginUpdate();
            foreach (var item in oeoperationType)
            {
                cmbOEOperationType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            cmbOEOperationType.Properties.EndUpdate();
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }

        protected BusinessData CurrentRow
        {
            get { return Current as BusinessData; }
        }

        private List<BusinessData> SelectRows
        {
            get
            {
                int[] rowIndexs = gvMain.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<BusinessData> tagers = new List<BusinessData>();
                foreach (var item in rowIndexs)
                {
                    BusinessData dr = gvMain.GetRow(item) as BusinessData;
                    if (dr != null) tagers.Add(dr);
                }

                return tagers;
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
                this.gcMain.BeginUpdate();
                this.gvMain.BeginUpdate();

                bsList.DataSource = value;
                bsList.ResetBindings(false);

                #region 对一些枚举类型，获取对应语言的描述信息

                List<BusinessData> list = value as List<BusinessData>;

                this.gvMain.EndUpdate();
                this.gcMain.EndUpdate();

                #endregion

                gvMain.BestFitColumns();
                string message = string.Empty;

                if (list.Count > 0)
                {
                    if (LocalData.IsEnglish)
                    {
                        if (list.Count > 1)
                        {
                            message = string.Format("{0} records found", list.Count);
                        }
                        else
                        {
                            message = string.Format("{0} record found", list.Count);
                        }
                    }
                    else
                    {
                        message = string.Format("查询到 {0} 条记录", list.Count);
                    }
                }
                else
                {

                    message = LocalData.IsEnglish ? "Nothing found!" : "没有查询到任何结果。";
                }

                if (this._shown)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
                }

                if (bsList.List.Count > 0)
                {
                    gvMain.Focus();
                    gvMain.SelectRow(0);
                }
            }
        }

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;

            if (values.ContainsKey("IsMulti"))
            {
                //_IsMulti = (bool)values["IsMulti"];
                //单选的搜索器需在此实现一下
            }
            //if (values.ContainsKey("IsValidateCustomer"))
            //{

            //    IsValidateCustomer = (bool)values["IsValidateCustomer"];
            //}
        }

        //public override void Refresh(object items)
        //{
        //    List<OceanBookingList> list = this.DataSource as List<OceanBookingList>;
        //    if (list == null) return;
        //    List<OceanBookingList> newLists = items as List<OceanBookingList>;

        //    foreach (var item in newLists)
        //    {
        //        OceanBookingList tager = list.Find(delegate(OceanBookingList jItem) { return item.ID == jItem.ID; });
        //        if (tager == null) continue;
                
        //        Utility.CopyToValue(item, tager, typeof(OceanBookingList));
        //    }
        //    bsList.ResetBindings(false);
                   
        //}

        //protected override void GvMainDoubleClick()
        //{
        //    if (CurrentRow != null)
        //    {
        //        Workitem.Commands[BusinessFinderConstants.Command_FinderConfirm].Execute();
        //    }
        //}

        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
        }

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region GridView Event

        public new event KeyEventHandler KeyDown;

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    GvMainDoubleClick();
                }
                else if (this.KeyDown != null
                    && e.KeyCode == Keys.F5
                    && this.gvMain.FocusedColumn != null
                    && this.gvMain.FocusedValue != null)
                {
                    string text = gvMain.GetFocusedDisplayText();//.GetDisplayText(treeMain.FocusedColumn);
                    Dictionary<string, object> keyValue = new Dictionary<string, object>();
                    keyValue.Add(gvMain.FocusedColumn.FieldName, text);
                    this.KeyDown(keyValue, null);
                }
                else if (e.KeyCode == Keys.F6)
                {
                    Workitem.Commands[BusinessFinderConstants.Command_ShowSearch].Execute();
                }
            }
        }

        private void GvMainDoubleClick()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow != null)
                {
                    Workitem.Commands[BusinessFinderConstants.Command_FinderConfirm].Execute();
                }
            }
        }

        private void gvMain_DoubleClick(object sender, System.EventArgs e)
        {
            GvMainDoubleClick();
        }

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            BusinessData list = gvMain.GetRow(e.RowHandle) as BusinessData;
            if (list == null) return;
            
            //if (list.IsValid == false)
            //{
            //    ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            //}
            if (list.State == OEOrderState.NewOrder)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
            }
            else if (list.State == OEOrderState.Checked)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Confirmed);
            }
            else if (list.State == OEOrderState.Rejected)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Error);
            }
        }

        #endregion

        #region Workitem Common
      
        private void gvMain_MouseEnter(object sender, EventArgs e)
        {
        }

        private void gvMain_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        #endregion
    }
}
