using System;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.TMS.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.TMS.UI
{
    /// <summary>
    /// 拖车资料编辑面板
    /// </summary>
    [ToolboxItem(false)]
    public partial class TruckEdit : BaseEditPart
    {
        public TruckEdit()
        {
            InitializeComponent();
            this.Closing += new EventHandler<FormClosingEventArgs>(TruckEdit_Closing);
            this.Disposed += delegate
            {
                this.Saved = null;
                
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                this.truckList = null;
                this.Closing -= this.TruckEdit_Closing;
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TruckEdit_Closing(object sender, FormClosingEventArgs e)
        {
            if (truckList.IsDirty)
            {
                DialogResult dr = Utility.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!this.SaveTruck())
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        #region 服务
        /// <summary>
        /// Workitem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 拖车服务
        /// </summary>
   
        public ITruckBookingService TruckBookingService
        {
            get
            {
                return ServiceClient.GetService<ITruckBookingService>();
            }
        }
        #endregion

        #region 重写
        /// <summary>
        /// 保存事件
        /// </summary>
        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;
        /// <summary>
        /// 数据源
        /// </summary>
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
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="data"></param>
        private void BindingData(object data)
        {
            TruckDataList list = data as TruckDataList;
            if (list == null)
            {
                bsList.DataSource = typeof(ICP.TMS.ServiceInterface.TruckDataList);
                this.Enabled = false;
                   
            }
            else
            {
                truckList = new TruckDataList();
                Utility.CopyToValue(list,truckList, typeof(TruckDataList));

                truckList.IsDirty = false;
                bsList.DataSource = truckList;
                bsList.ResetBindings(false);

                this.txtTruckNo.Focus();

                if (truckList.IsValid)
                {
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = false;
                }
                truckList.BeginEdit();

            }
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            return this.SaveTruck();
        }


        #endregion

        #region  私有字段
        private TruckDataList truckList = new TruckDataList();
        #endregion

        #region 初始化
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMessage();
            }
        }
        /// <summary>
        /// 注册消息
        /// </summary>
        private void InitMessage()
        {
            this.RegisterMessage("TruckNoIsNull",LocalData.IsEnglish?"TruckNo must input":"车牌号:必须输入");
            this.RegisterMessage("BuyDateIsNull",LocalData.IsEnglish?"BuyDate must input":"购买日期：必须输入");
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveTruck();
        }
        /// <summary>
        /// 保存拖车资料
        /// </summary>
        /// <returns></returns>
        private bool SaveTruck()
        {
           
            if (!ValidateData())
            {
                return false;
            }
            if (!this.truckList.IsDirty)
            {
                return false;
            }
            try
            {
                SingleResult result = TruckBookingService.SaveCarInfo(
                    truckList.ID, 
                    truckList.TruckNo, 
                    truckList.TypeName, 
                    truckList.BuyDate, 
                    truckList.Remark,
                    LocalData.UserInfo.LoginID, 
                    truckList.UpdateDate,
                    LocalData.IsEnglish);

                truckList.ID = result.GetValue<Guid>("ID");
                truckList.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                if (Saved != null)
                {
                    Saved(this.truckList);
                }

                this.truckList.CancelEdit();

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

                return true;
            }
            catch(Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            truckList.EndEdit();
            this.txtCreateName.Focus();
            bsList.EndEdit();

            bool isSure = true;

            if (string.IsNullOrEmpty(truckList.TruckNo))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "TruckNoIsNull"));
                isSure= false;
            }
            if (truckList.BuyDate == null || truckList.BuyDate == DateTime.MaxValue || truckList.BuyDate == DateTime.MinValue)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "BuyDateIsNull"));
                isSure= false;
            }

            return isSure;   
        }
        #endregion

     




    }
}
