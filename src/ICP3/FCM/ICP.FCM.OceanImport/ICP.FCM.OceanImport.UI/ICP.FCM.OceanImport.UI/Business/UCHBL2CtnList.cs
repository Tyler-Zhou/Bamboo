using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.OceanImport.UI.Business
{
    public partial class UCHBL2CtnList : ICP.Framework.ClientComponents.UIFramework.BasePart
    {
        public UCHBL2CtnList()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                this.Disposed += delegate
                {
                    this.gcBoxList.DataSource = null;
                    this.gvBoxList.CustomDrawRowIndicator -= this.gvBoxList_CustomDrawRowIndicator;
                    this.gvBoxList.RowCellClick -= this.gvBoxList_RowCellClick;
                    this.bsContainerInfo.DataSource = null;
                    this.bsContainerInfo.Dispose();

                    if (Workitem != null)
                    {
                        Workitem.Items.Remove(this);
                        Workitem = null;
                    }
                };
                
            }
        }

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        IOceanImportService OceanImportService
        {
            get
            {
               return ServiceClient.GetService<IOceanImportService>();
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// HBLID
        /// </summary>
        public Guid HBLID
        {
            set;
            get;
        }
        private bool isChanged = false;
        /// <summary>
        /// 是否有数据发生改变
        /// </summary>
        public bool IsChanged
        {
            get
            {
                if (isChanged)
                {
                    return true;
                }
                else
                {
                    if (DataSource == null)
                    {
                        return false;
                    }
                    foreach (OIBusinessContainerList box in DataSource)
                    {
                        if (box.IsDirty)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            set
            {
                isChanged = value;
            }
        }

        #endregion

        #region 窗体事件
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            //if (tfService != null)
            //{
            //    List<ContainerList> containerList = tfService.GetContainerList(string.Empty, true, 0);

            //    cmbBoxType.Properties.BeginUpdate();
            //    foreach (ContainerList container in containerList)
            //    {
            //        cmbBoxType.Properties.Items.Add(new ImageComboBoxItem(container.Code, container.ID));
            //    }
            //    cmbBoxType.Properties.EndUpdate();
            //}

            List<OIBusinessContainerList> list = OceanImportService.GetOIHBL2Container(HBLID);
            this.bsContainerInfo.DataSource = list;
            this.bsContainerInfo.ResetBindings(false);

            this.gvBoxList.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gvBoxList_RowCellClick);
        }

        void gvBoxList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

            if (CurrentRow == null)
            {
                return;
            }
            CurrentRow.IsSelected = !CurrentRow.IsSelected;
            CurrentRow.IsDirty = true;
            bsContainerInfo.ResetBindings(false);
        }

        private void ckbIsRelation_Click(object sender, System.EventArgs e)
        {
            //    if (CurrentRow == null)
            //    {
            //        return;
            //    }
            //    CurrentRow.IsSelected = !CurrentRow.IsSelected;
            //    CurrentRow.IsDirty = true;
            //    bsContainerInfo.ResetBindings(false);
        }

        private void gvBoxList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        #endregion

        #region 私有字段

        /// <summary>
        /// 数据源
        /// </summary>
        private List<OIBusinessContainerList> DataSource
        {
            get
            {
                return bsContainerInfo.DataSource as List<OIBusinessContainerList>;
            }
            set
            {
                bsContainerInfo.DataSource = value;
            }
        }
        /// <summary>
        /// 当前行
        /// </summary>
        OIBusinessContainerList CurrentRow
        {
            get
            {
                if (bsContainerInfo.Current == null)
                    return null;
                else
                    return bsContainerInfo.Current as OIBusinessContainerList;
            }
        }
        /// <summary>
        /// 所有选择的行
        /// </summary>
        List<OIBusinessContainerList> SelectRows
        {
            get
            {
                int[] indexs = gvBoxList.GetSelectedRows();
                if (indexs == null || indexs.Length == 0) return null;

                List<OIBusinessContainerList> list = new List<OIBusinessContainerList>();
                foreach (var item in indexs)
                {
                    OIBusinessContainerList tager = gvBoxList.GetRow(item) as OIBusinessContainerList;
                    if (tager != null) list.Add(tager);
                }
                return list;
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DataSource == null || DataSource.Count == 0)
            {
                return;
            }

            List<Guid> idList = new List<Guid>();

            foreach (OIBusinessContainerList item in DataSource)
            {
                if (item.IsSelected)
                {
                    idList.Add(item.ID.Value);
                }
            }

            ManyResult many = OceanImportService.SaveOIHBL2Container(HBLID, idList.ToArray(), LocalData.UserInfo.LoginID);

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

        }
        #endregion

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

    }
}
