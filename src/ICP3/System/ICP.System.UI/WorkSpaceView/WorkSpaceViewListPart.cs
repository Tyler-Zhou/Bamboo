using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Sys.UI.WorkSpaceView
{
    public partial class WorkSpaceViewListPart : BaseListPart
    {
        public WorkSpaceViewListPart()
        {
            InitializeComponent();
        }

        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IPermissionService PermissionService
        {
            get
            {
                return ServiceClient.GetService<IPermissionService>();
            }
        }

        #endregion

        #region 属性
        public WorkSpaceList CurrentRow
        {
            get
            {
                return bsList.Current as WorkSpaceList;
            }
        }
        #endregion

        #region 绑定数据
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                Utility.ShowGridRowNo(this.gvDetails);
            }
        }

        public void BindData()
        {
            List<WorkSpaceList> list= PermissionService.GetWorkSpaceList();
            bsList.DataSource = list;
            bsList.ResetBindings(false);
        }
        [CommandHandler(WorkSpaceViewConstants.WorkSpaceView_Command_Refresh)]
        public void WorkSpaceView_Command_Refresh(object sender, EventArgs e)
        {
            BindData();
        }
        #endregion

        #region 当前行发生改变
        public override event CurrentChangedHandler CurrentChanged;
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
            {
                CurrentChanged(sender, CurrentRow);
            }
        }
        #endregion

        #region 新增&编辑数据
        public void EditPartSaved(object[] prams)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (prams == null || prams.Length == 0) return;

                WorkSpaceList data = prams[0] as WorkSpaceList;

                List<WorkSpaceList> source = bsList.DataSource as List<WorkSpaceList>;
                if (source == null || source.Count == 0)
                {
                    bsList.Add(data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    WorkSpaceList tager = source.Find(delegate(WorkSpaceList item) { return item.ID == data.ID; });
                    if (tager == null)
                    {
                        bsList.Insert(0, data);
                        bsList.ResetBindings(false);
                    }
                    else
                    {
                        Utility.CopyToValue(data, tager, typeof(WorkSpaceList));
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



    }
}
