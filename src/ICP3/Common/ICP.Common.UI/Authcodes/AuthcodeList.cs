using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.Common.UI.Authcodes
{
    public partial class AuthcodeList : BaseListPart
    {

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

        public AuthcodeList()
        {
            InitializeComponent();
        }

        AuthcodeInfo CurrentRow
        {
            get { return bsList.Current as AuthcodeInfo; }
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

        private void BindingData(object value)
        {
            List<AuthcodeInfo> list = value as List<AuthcodeInfo>;
            if (list == null)
            {
                list = new List<AuthcodeInfo>();
            }
            bsList.DataSource = list;
            bsList.ResetBindings(false);

            if (CurrentChanged != null)
            {
                CurrentChanged(this, Current);
            }
        }

        public override object Current
        {
            get { return bsList.Current; }
        }

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;

        #region 私有方法
        /// <summary>
        /// 编辑界面保存数据
        /// </summary>
        /// <param name="prams"></param>
        public void EditPartSaved(object[] prams)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //if (prams == null || prams.Length == 0) return;

                //DataDictionaryList data = prams[0] as DataDictionaryList;

                //List<DataDictionaryList> source = this.DataSource as List<DataDictionaryList>;
                //if (source == null || source.Count == 0)
                //{
                //    bsList.Add(data);
                //    bsList.ResetBindings(false);
                //}
                //else
                //{
                //    DataDictionaryList tager = source.Find(delegate(DataDictionaryList item) { return item.ID == data.ID; });
                //    if (tager == null)
                //    {
                //        bsList.Insert(0, data);
                //        bsList.ResetBindings(false);
                //    }
                //    else
                //    {
                //        Utility.CopyToValue(data, tager, typeof(DataDictionaryList));

                //        bsList.ResetItem(bsList.IndexOf(tager));
                //    }

                //}
                //if (CurrentChanged != null)
                //{
                //    CurrentChanged(this, CurrentRow);
                //}
            }
        }
        #endregion

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        /// <summary>
        /// 删除MAC地址信息
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(AuthcodeCommandConstants.Command_AuthcodeDelete)]
        public void Command_AuthcodeDelete(object s, EventArgs e)
        {
            if (CurrentRow == null || gvMain.FocusedRowHandle <0) return;

            if (CommonUtility.EnquireIsDeleteCurrentData() == false)
            {
                return;
            }

            if (CurrentRow.IsNew == false)
            {
                TransportFoundationService.RemoveAuthcodeInfo(CurrentRow.ID, LocalData.UserInfo.LoginID);
            }

            bsList.RemoveCurrent();
            gvMain.RefreshData();
        }
    }
}
