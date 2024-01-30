using DevExpress.XtraBars;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;

namespace ICP.Common.UI.Authcodes
{
    /// <summary>
    /// MAC地址编辑
    /// </summary>
    public partial class AuthcodeEdit : BaseEditPart
    {
        #region Services & Delegate
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 基础数据服务管理
        /// </summary>
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        #endregion

        #region Property
        /// <summary>
        /// 当前编辑MAC
        /// </summary>
        public AuthcodeInfo CurrentData
        {
            get
            {
                return bslist.Current as AuthcodeInfo;
            }
        } 
        #endregion

        #region Delegate
        /// <summary>
        /// 保存
        /// </summary>
        public override event SavedHandler Saved;
        #endregion 
        #endregion

        #region Init
        /// <summary>
        /// MAC地址编辑
        /// </summary>
        public AuthcodeEdit()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Enabled = false;
        }
        #endregion

        #region Windows Event
        /// <summary>
        /// 保存MAC地址
        /// </summary>
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveData();
        }
        #endregion

        #region Command handler
        /// <summary>
        /// 添加MAC项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(AuthcodeCommandConstants.Command_AuthcodeAdd)]
        public void Command_MovieProjectAdd(object sender, EventArgs e)
        {
            Enabled = true;
            AuthcodeInfo item = new AuthcodeInfo();
            bslist.DataSource = item;
            bslist.ResetBindings(false);
        } 
        #endregion

        #region Custom Method
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="data">绑定数据集合</param>
        public void BindDataList(AuthcodeInfo data)
        {
            Enabled = data != null;
            bslist.DataSource = data;
            bslist.ResetBindings(false);
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            return Save();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        private bool Save()
        {
            if (!ValidateData())
            {
                return false;
            }

            SingleResultData rusult = TransportFoundationService.SaveAuthcodeInfo(CurrentData.ID, CurrentData.AuthCode, CurrentData.PhysicalID, CurrentData.SenderRemark, LocalData.UserInfo.LoginID);
            CurrentData.ID = rusult.ID;

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

            if (Saved != null)
            {
                Saved(new object[] { CurrentData });
            }
            return true;
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            CurrentData.EndEdit();
            EndEdit();
            bslist.EndEdit();

            return true;

        } 
        #endregion
    }
}
