using DevExpress.XtraBars;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.CompositeObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.FAM.UI.BankTransaction
{
    [ToolboxItem(false)]
    public partial class EditPart : BaseListEditPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 银行流水对象
        /// </summary>
        BankTransactionInfo _BankTransactionInfo { get; set; }
        #endregion

        #region Event
        /// <summary>
        /// 
        /// </summary>
        public EditPart()
        {
            InitializeComponent();
            barAssociation.ItemClick += barAssociation_ItemClick;
            Disposed += delegate {
                dxErrorProvider1.DataSource = null;
                bsEidt.DataSource = null;
                bsEidt.Dispose();
                
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                InitMessage();
            }
        }

        private void barAssociation_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                UCWriteOff associationPart = Workitem.RootWorkItem.Items.AddNew<UCWriteOff>();
                associationPart.TransactionID = _BankTransactionInfo.ID;
                string title = LocalData.IsEnglish ? "Association Write Off" : "关联销账";
                PartLoader.ShowDialog(associationPart, title);
            }
        }

        #endregion

        #region Init
        private void InitControls()
        {
            SetDataLazyLoaders();
            SearchRegister();
        }

        private void InitMessage()
        {
        }
        #endregion

        #region 搜索器注册
        void SearchRegister()
        {
        }

        #endregion

        #region 下拉列表数据延迟加载

        void SetDataLazyLoaders()
        {
        }

        #endregion

        #region Save

        private bool Save()
        {
            if (ValidateData() == false)
            {
                return false;
            }
            SaveRequestBankTransaction2Checks saveRequest = null;
            List<BankTransaction2Checks> listData = bsEidt.DataSource as List<BankTransaction2Checks>;
            if(listData!=null)
            {
                List<BankTransaction2Checks> selectItems = listData.Where(fItem => fItem.IsDirty).ToList();
                saveRequest = new SaveRequestBankTransaction2Checks
                {
                    BankTransactionID = _BankTransactionInfo.ID,
                    ChecksIDs = selectItems.Select(ecItem => ecItem.ChecksID).ToArray(),
                    SaveByID = LocalData.UserInfo.LoginID,
                    UpdateDate = DateTime.Now,
                };
            }
            
            try
            {
                if(saveRequest!=null)
                {
                    FinanceService.BankTransactionAssociationChecks(saveRequest);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                }
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return false;
            }
        }

        private bool ValidateData()
        {
            EndEdit();
            bsEidt.EndEdit();

            return true;
        }

        #endregion

        #region IEditPart 成员
        /// <summary>
        /// 
        /// </summary>
        public override object DataSource
        {
            get { return bsEidt.DataSource; }
            set { BindingData(value); }
        }
        void BindingData(object data)
        {
            if (data == null)
            {
                Enabled = false;
            }
            else
            {
                Enabled = true;
                _BankTransactionInfo = data as BankTransactionInfo;
                if(_BankTransactionInfo != null)
                {
                    List<BankTransaction2Checks> listData= FinanceService.GetBankTransaction2Checks(
                            new BankTransaction2ChecksSearchParameter() {
                                BankTransactionID = _BankTransactionInfo.ID,
                            });
                    bsEidt.DataSource = listData;
                    bsEidt.ResetBindings(false);

                }
            }
        }

        public override bool SaveData()
        {
            return Save();
        }

        public override void EndEdit()
        {
            bsEidt.EndEdit();
        }
        #endregion
        
    }
}
