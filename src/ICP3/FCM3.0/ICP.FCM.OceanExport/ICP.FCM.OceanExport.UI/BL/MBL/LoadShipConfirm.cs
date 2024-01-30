using System;
using System.ComponentModel;
using System.Windows.Forms;

using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface;

namespace ICP.FCM.OceanExport.UI.MBL
{
    [ToolboxItem(false)]
    public partial class LoadShipConfirm : BaseEditPart
    {
        #region service

        [ServiceDependency]
        public IDataFindClientService dfService { get; set; }

        [ServiceDependency]
        public IOceanExportService oeService { get; set; }

        #endregion

        #region init

        public LoadShipConfirm()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            labPreVoyage.Text = "船名航次";
            labVoyage.Text = "船名航次";
            groupPreVoyage.Text = "驳船";
            groupVoyage.Text = "大船";
            chkConfirmPreVoyage.Text = chkConfirmVoyage.Text = "确认";
            btnOK.Text = "确认(&O)";
            btnCancel.Text = "取消(&C)";
        }

        protected override void OnLoad(EventArgs e)
        {
            if (_MBLInfo.ConfirmOnBoardType == ConfirmOnBoardType.All)
                chkConfirmVoyage.Checked = chkConfirmPreVoyage.Checked = true;
            else if (_MBLInfo.ConfirmOnBoardType == ConfirmOnBoardType.Unknown)
                chkConfirmVoyage.Checked = chkConfirmPreVoyage.Checked = false;
            else if (_MBLInfo.ConfirmOnBoardType == ConfirmOnBoardType.Confirm)
                chkConfirmVoyage.Checked = true;
            else
                chkConfirmPreVoyage.Checked = true;

            SearchRegister();

        }

        #endregion

        #region
        /// <summary>
        /// 搜索器注册
        /// </summary>
        void SearchRegister()
        {
            #region Voyage

            //驳船 搜索的默认条件为 装货港=当前收货地and卸货港=当前装货港
            dfService.Register(stxtPreVoyage,
                CommonFinderConstants.VesselVoyageFinder,
                SearchFieldConstants.VesselVoyage,
                SearchFieldConstants.VesselResultValue,
                this.GetConditionsForSearchPreVoyage,
                delegate(object inputSource, object[] resultData)
                {
                    stxtPreVoyage.Text = _MBLInfo.PreVesselVoyage = resultData[1].ToString() + "/" + resultData[2].ToString();
                    stxtPreVoyage.Tag = _MBLInfo.PreVoyageID = new Guid(resultData[0].ToString());
                },
                delegate
                {
                    stxtPreVoyage.Text = _MBLInfo.PreVesselVoyage = string.Empty;
                    stxtPreVoyage.Tag = _MBLInfo.PreVoyageID = null;
                },
                null);

            //大船 筛选：装货港=当前装货港and卸货港=当前卸货港
            dfService.Register(stxtVoyage,
                CommonFinderConstants.VesselVoyageFinder,
                SearchFieldConstants.VesselVoyage,
                SearchFieldConstants.VesselResultValue,
                this.GetConditionsForSearchVoyage,
                delegate(object inputSource, object[] resultData)
                {
                    stxtVoyage.Text = _MBLInfo.VesselVoyage = resultData[1].ToString() + "/" + resultData[2].ToString();
                    stxtVoyage.Tag = _MBLInfo.VoyageID = new Guid(resultData[0].ToString());
                },
                delegate
                {
                    stxtVoyage.Text = _MBLInfo.VesselVoyage = string.Empty;
                    stxtVoyage.Tag = _MBLInfo.VoyageID = null;
                },
                null);

            #endregion
        }

        //大船 筛选：装货港=当前装货港and卸货港=当前卸货港
        SearchConditionCollection GetConditionsForSearchVoyage()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("POLID", _MBLInfo.POLID, false);
            conditions.AddWithValue("POLName", _MBLInfo.POLCode, false);
            conditions.AddWithValue("PODID", _MBLInfo.PODID, false);
            conditions.AddWithValue("PODName", _MBLInfo.PODCode, false);
            return conditions;
        }

        //驳船 搜索的默认条件为 装货港=当前收货地and卸货港=当前装货港
        SearchConditionCollection GetConditionsForSearchPreVoyage()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();

            conditions.AddWithValue("POLID", _MBLInfo.PlaceOfReceiptID, false);
            conditions.AddWithValue("POLName", _MBLInfo.PlaceOfReceiptCode, false);
            conditions.AddWithValue("PODID", _MBLInfo.POLID, false);
            conditions.AddWithValue("PODName", _MBLInfo.POLCode, false);
            return conditions;
        }

        #endregion

        #region btn

        bool ValidateData()
        {

            //原数据已装船和现在未装船，判定为取消装船
            if (_MBLInfo.ConfirmOnBoardType != ConfirmOnBoardType.Unknown
                && chkConfirmPreVoyage.Checked == false && chkConfirmVoyage.Checked == false)
            {
                //如果没有权限
                if (LocalCommonServices.PermissionService.HaveActionPermission(ActionsConstants.OceanExport_CancelLoadShip) == false)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ?"You have no right to cancel load ship":"您没有取消装船的权限,请联系你的经理."
                        ,LocalData.IsEnglish? "Tip":"提示");

                    return false;
                }
            }

            bool isSucc = true;

            dxErrorProvider1.ClearErrors();
            if (chkConfirmPreVoyage.Checked && Utility.GuidIsNullOrEmpty(_MBLInfo.PreVoyageID))
            {
                dxErrorProvider1.SetError(this.stxtPreVoyage, LocalData.IsEnglish ? "Must Input." : "必须输入.");
                stxtPreVoyage.Focus();
                isSucc = false;
            }
            if (chkConfirmVoyage.Checked && Utility.GuidIsNullOrEmpty(_MBLInfo.VoyageID))
            {
                dxErrorProvider1.SetError(this.stxtVoyage, LocalData.IsEnglish ? "Must Input." : "必须输入.");
                stxtVoyage.Focus();
                isSucc = false;
            }
            return isSucc;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;

            ConfirmOnBoardType confirmOnBoardType = ConfirmOnBoardType.Unknown;
            if (chkConfirmPreVoyage.Checked && chkConfirmVoyage.Checked)
                confirmOnBoardType = ConfirmOnBoardType.All;
            else if (chkConfirmPreVoyage.Checked ==false&& chkConfirmVoyage.Checked==false)
                confirmOnBoardType = ConfirmOnBoardType.Unknown;
            else if (chkConfirmVoyage.Checked)
                confirmOnBoardType = ConfirmOnBoardType.Confirm;
            else
                confirmOnBoardType = ConfirmOnBoardType.PreConfirm;
            try
            {
                SingleResult result = oeService.ConfirmOnBoardType(_MBLInfo.ShippingOrderID.Value
                                                                , _MBLInfo.PreVoyageID
                                                                , _MBLInfo.VoyageID
                                                                , confirmOnBoardType
                                                                , LocalData.UserInfo.LoginID
                                                                , _MBLInfo.ShippingOrderUpdateDate);

                _MBLInfo.ConfirmOnBoardType = confirmOnBoardType;

                ////IF将订舱单.是否大船装船 = True & 订舱单.非顺签提单 = True THEN
                ////SET 订舱单.提单列表.状态 = 对单完成
                //if (_MBLInfo.ConfirmOnBoardType == ConfirmOnBoardType.Confirm
                //     && _MBLInfo.IssueType != IssueType.Post_date
                //     && _MBLInfo.State != BLState.Release)
                //{
                //    _MBLInfo.State = BLState.Checked;
                //}

                _MBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                _MBLInfo.ShippingOrderUpdateDate = result.GetValue<DateTime?>("ShippingOrderUpdateDate");

                if (Saved != null) Saved(new object[] { _MBLInfo.ShippingOrderID });
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功.");
                this.FindForm().Close();
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
            this.FindForm().Close();
        }

        #endregion

        #region IEditPart 成员

        OceanMBLInfo _MBLInfo = null;
        void BindingData(object data)
        {
            _MBLInfo = data as OceanMBLInfo;
            this.bindingSource1.DataSource = _MBLInfo;
        }

        public override object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }

        public override void EndEdit()
        {
            this.Validate();
            bindingSource1.EndEdit();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

       
    }
}
