using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.CompositeObjects;
using ICP.FAM.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface;
using ICP.Framework.ClientComponents;

namespace ICP.FAM.UI
{
    /// <summary>
    /// 关联销账
    /// </summary>
    public partial class UCWriteOff : BaseEditPart
    {
        #region Service
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public IFinanceService FinanceService { get; set; }
        #endregion

        #region Member
        /// <summary>
        /// 银行流水ID
        /// </summary>
        public Guid TransactionID { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 关联销账
        /// </summary>
        public UCWriteOff()
        {
            InitializeComponent();
        } 
        #endregion

        #region Event
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                InitControls();
            }
        }
        void btnCancel_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            string writeOffNO = txtWriteOffNO.Text;
            if (string.IsNullOrEmpty(writeOffNO))
            {
                return;
            }
            AssociationSearchParameter searchParameter = new AssociationSearchParameter()
            {
                BankTransactionID = TransactionID,
                CompanyID = LocalData.UserInfo.DefaultCompanyID,
                WriteOffNO = txtWriteOffNO.Text,
                SaveByID = LocalData.UserInfo.LoginID,
                UpdateDate = DateTime.Now,
            };
            AssociationInfo associationInfo = FinanceService.GetAssociationInfo(searchParameter);
            if (associationInfo == null)
            {
                MessageBoxService.ShowWarning("未找到销账数据");
                return;
            }
            if (associationInfo.AssociationWriteOffDate != null && associationInfo.AssociationWriteOffDate <= associationInfo.ChargingClosingDate)
            {
                MessageBoxService.ShowError("关联销账已计费关账，禁止修改");
                return;
            }
            if(!associationInfo.AssociationWriteOffNO.IsNullOrEmpty())
            {
                DialogResult dr = MessageBoxService.ShowQuestion(string.Format("当前流水已与[{0}]销账关联，是否继续", associationInfo.AssociationWriteOffNO), "提示", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                    return;
            }
            if(associationInfo.WriteOffDate<=associationInfo.ChargingClosingDate)
            {
                MessageBoxService.ShowError("待关联销账已计费关账，禁止关联");
                return;
            }

            if (SaveData())
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Association Successfully" : "关联成功");
            }
            FindForm().Close();
        }
        #endregion

        #region Methods
        /// <summary>
        /// 初始化
        /// </summary>
        void InitControls()
        {
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns>是否保存成功</returns>
        public override bool SaveData()
        {
            
            AssociationSaveRequest saveRequest = new AssociationSaveRequest()
            {
                BankTransactionID = TransactionID,
                CompanyID = LocalData.UserInfo.DefaultCompanyID,
                WriteOffNO = txtWriteOffNO.Text,
                SaveByID = LocalData.UserInfo.LoginID,
                UpdateDate = DateTime.Now,
            };
            return FinanceService.AssociationTransactionToWriteOff(saveRequest);
        }
        #endregion
    }
}
