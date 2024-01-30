using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.Common.UI
{
    [ToolboxItem(false)]
    public partial class MovieProjectSearch : BaseSearchPart
    {
        public MovieProjectSearch()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.OnSearched = null;
                this.RemovekKeyDownHandle();
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        #region 服务

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        #endregion

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.DesignMode)
            {
                SetKeyDownToSearch();
            }
        }

        private void SetKeyDownToSearch()
        {
            foreach (Control item in bgcBase.Controls)
            {
                item.KeyDown += new KeyEventHandler(item_KeyDown);
            }
        }
        private void RemovekKeyDownHandle()
        {
            foreach (Control item in bgcBase.Controls)
            {
                item.KeyDown -= item_KeyDown;
            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
            {
                this.btnSearch.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                this.btnClare.PerformClick();
            }
        }
        #endregion

        #region 重写

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;
        #endregion

        #region 查询

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<DataDictionaryList> DataList=TransportFoundationService.GetDataDictionaryList(this.txtCode.Text,
                                                                 this.txtName.Text,
                                                                 DataDictionaryType.MovieProjects,
                                                                 this.chkIsValid.Checked,
                                                                 0);
                if (OnSearched != null)
                {
                    OnSearched(sender,DataList);
                }
            }
        }

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

        #region 清空
        private void btnClare_Click(object sender, EventArgs e)
        {
            this.txtName.Text = string.Empty;
            this.txtCode.Text = string.Empty;
            this.chkIsValid.Checked = null;
        }
        #endregion

    }

}
