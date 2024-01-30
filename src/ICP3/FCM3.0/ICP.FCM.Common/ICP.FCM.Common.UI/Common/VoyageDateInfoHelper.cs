using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using System.Windows.Forms;

namespace ICP.FCM.Common.UI
{
    #region 船名航次应用到的表单类型
    /// <summary>
    ///船名航次应用到的表单类型
    /// </summary>
    public enum VoyageFormType
    {
        /// <summary>
        /// Ocean Export MBL
        /// </summary>
        MBL,
        /// <summary>
        /// Ocean Export HBL
        /// </summary>
        HBL,
        /// <summary>
        /// Ocean Export Booking
        /// </summary>
        Booking,
        /// <summary>
        /// Ocean Import Business
        /// </summary>
        OIBusiness,
        /// <summary>
        /// Domestic Trade Booking
        /// </summary>
        DomesticTrade
    }
    #endregion

    #region 辅助类
    /// <summary>
    /// 获取航次日期信息委托
    /// </summary>
    /// <param name="voyageId">航次Id</param>
    /// <param name="portId">港口Id</param>
    /// <returns></returns>
    public delegate List<VoyageDateInfo> VoyageDateDelegate(List<Guid?> voyageIds);
    /// <summary>
    /// 根据航次ID获取航次日期信息辅助类
    /// </summary>
    public sealed class VoyageDateInfoGetter
    {

        private object objSyn = new object();
        static ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService;
        /// <summary>
        /// 获取航次信息
        /// </summary>
        /// <param name="voyageId"></param>
        /// <param name="portId"></param>
        /// <returns></returns>
        public List<VoyageDateInfo> GetVoyageDateInfoList(List<Guid?> voyageIds)
        {
            EnsureCommonServiceExists();
            return fcmCommonService.GetVoyageDateInfo(voyageIds);
        }
        /// <summary>
        /// 
        /// </summary>
        private void EnsureCommonServiceExists()
        {
            if (fcmCommonService == null)
            {
                lock (objSyn)
                {
                    if (fcmCommonService == null)
                    {
                        fcmCommonService = ServiceClient.GetService<ICP.FCM.Common.ServiceInterface.IFCMCommonService>();
                    }
                }
            }
        }
    }
    #endregion
    /// <summary>
    /// 船名航次辅助类
    /// </summary>
    public sealed class VoyageDateInfoHelper : IDisposable
    {
        #region 字段及属性定义
        /// <summary>
        /// 航次相关日期信息
        /// </summary>
        List<VoyageDateInfo> listVoyageDateInfo = new List<VoyageDateInfo>();
        /// <summary>
        /// 驳船和大船选择控件
        /// </summary>
        UCVoyageLookupEdit _preVoyageEdit, _voyageEdit;
        /// <summary>
        /// 港口选择控件（POR，POL，POD）
        /// </summary>
        UCButtonEdit _porButtonEdit, _polButtonEdit, _podButtonEdit;
        /// <summary>
        /// 对应港口的ETD或ETA日期选择控件
        /// </summary>
        DateEdit _porDateEdit, _polDateEdit, _podDateEdit;
        /// <summary>
        /// 截止日相关日期选择控件
        /// </summary>
        DateEdit _closingDateEdit, _cyClosingDateEdit, _docClosingDateEdit;
        /// <summary>
        /// 数据是否改变 如果未改变 则数据不做验证
        /// </summary>
        private bool isDataChanged = false;
        /// <summary>
        /// 目标窗体类型
        /// </summary>
        private VoyageFormType _formType;
        /// <summary>
        /// 目标窗体数据是否已保存
        /// </summary>
        private bool isDataSaved = false;
        /// <summary>
        /// 是否显示驳船，大船勾选控件
        /// </summary>
        private CheckEdit _cbxShowPrevoyage, _cbxShowVoyage;
        /// <summary>
        /// 界面数据是否保存
        /// </summary>
        public bool IsDataSaved
        {
            get { return this.isDataSaved; }
            set { this.isDataSaved = value; }
        }
        #endregion
        #region 方法定义
        /// <summary>
        /// 初始化控件值
        /// </summary>
        /// <param name="type">船名航次应用到的表单类型</param>
        /// <param name="preVoyageEdit">驳船Id</param>
        /// <param name="voyageEdit">大船Id</param>
        /// <param name="editValueChangeHandler">航次改变事件处理程序</param>
        /// <param name="porButton">PreETD港口</param>
        /// <param name="polButton">ETD港口</param>
        /// <param name="podButton">ETA港口</param>
        /// <param name="porEdit">PreETD</param>
        /// <param name="polEdit">ETD</param>
        /// <param name="podEdit">ETA</param>
        /// <param name="closingDateEdit">截关日</param>
        /// <param name="cyClosingDateEdit">截柜日</param>
        /// <param name="docClosingDateEdit">截文件日</param>
        /// <param name="cbxShowPrevoyage">显示驳船勾选框</param>
        /// <param name="cbxShowVoyage">显示大船勾选框</param>
        public void Init(VoyageFormType type, UCVoyageLookupEdit preVoyageEdit, UCVoyageLookupEdit voyageEdit, UCButtonEdit porButton, UCButtonEdit polButton, UCButtonEdit podButton, DateEdit porEdit, DateEdit polEdit, DateEdit podEdit, DateEdit closingDateEdit, DateEdit cyClosingDateEdit, DateEdit docClosingDateEdit,CheckEdit cbxShowPrevoyage,CheckEdit cbxShowVoyage)
        {
           // SetTempValue(type, preVoyageEdit, voyageEdit, porButton, polButton, podButton, porEdit, polEdit, podEdit, closingDateEdit, cyClosingDateEdit, docClosingDateEdit,cbxShowPrevoyage,cbxShowVoyage);
           // AddVoyageEditValueChangeEventHandler();

           
        }
        public void VoyageChange(UCVoyageLookupEdit edit)
        {
            if (edit.Tag == null || edit.Tag == DBNull.Value)
                return;

            try
            {
              
                isDataSaved = false;
                Guid? voyageId = (Guid?)edit.Tag;
                VoyageDateInfoGetter getter = new VoyageDateInfoGetter();
                VoyageDateDelegate voyageDelegate = getter.GetVoyageDateInfoList;
                IAsyncResult asyncResult = voyageDelegate.BeginInvoke(new List<Guid?>() { voyageId }, null, null);

                RemoveEditValueChangeHandler();

                //removed by tom
                //暂时屏蔽此功能，因为一直无法解决。
               // SetDateEditEnablity();

                VoyageSelectionInfo selectionInfo = new VoyageSelectionInfo();
                selectionInfo.VoyageId = voyageId;
                if (edit == _preVoyageEdit)
                {
                    selectionInfo.VoyageType = VoyageType.PreVoyage;
                }
                else
                {
                    selectionInfo.VoyageType = VoyageType.Voyage;
                }
                List<VoyageDateInfo> tempInfo = voyageDelegate.EndInvoke(asyncResult);
                listVoyageDateInfo.AddRange(tempInfo);

                //removed by tom
                //暂时屏蔽此功能，因为一直无法解决。
               // FillVoyageDateInfo(selectionInfo);
            }
            finally
            {

                AddEventChangeHandler();
                isDataChanged = true;
            }


        }

        private void SetTempValue(VoyageFormType formType, UCVoyageLookupEdit preVoyageEdit, UCVoyageLookupEdit voyageEdit, UCButtonEdit porButton, UCButtonEdit polButton, UCButtonEdit podButton, DateEdit porEdit, DateEdit polEdit, DateEdit podEdit, DateEdit closingDateEdit, DateEdit cyClosingDateEdit, DateEdit docClosingDateEdit,CheckEdit cbxShowPrevoyage,CheckEdit cbxShowVoyage)
        {

            _preVoyageEdit = preVoyageEdit;
            _voyageEdit = voyageEdit;
            _porButtonEdit = porButton;
            _polButtonEdit = polButton;
            _podButtonEdit = podButton;
            _porDateEdit = porEdit;
            _polDateEdit = polEdit;
            _podDateEdit = podEdit;

            _closingDateEdit = closingDateEdit;
            _cyClosingDateEdit = cyClosingDateEdit;
            _docClosingDateEdit = docClosingDateEdit;
            _formType = formType;

            _cbxShowPrevoyage = cbxShowPrevoyage;
            _cbxShowVoyage = cbxShowVoyage;
          

        }

        private void AddVoyageEditValueChangeEventHandler()
        {  
            if (_preVoyageEdit != null)
            {

                _preVoyageEdit.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(_preVoyageEdit_EditValueChanging);

                _preVoyageEdit.EditValueChanged += voyageEdit_EditValueChanged;
            }
            _voyageEdit.EditValueChanging += _preVoyageEdit_EditValueChanging;
            _voyageEdit.EditValueChanged += voyageEdit_EditValueChanged;
        }

        void voyageEdit_EditValueChanged(object sender, EventArgs e)
        {
            UCVoyageLookupEdit edit = sender as UCVoyageLookupEdit;
            if (edit == null || edit.EditValue == null || edit.EditValue == DBNull.Value)
            {
                return;
            }
            
            VoyageChange(sender as UCVoyageLookupEdit);
        }
        private void AddEventChangeHandler()
        {
            new List<UCButtonEdit>() { _porButtonEdit, _polButtonEdit, _podButtonEdit }.ForEach(item =>
            {
                if (item != null)
                {
                    item.TagChanged += DateEditValueChange;
                }
            });


        }
        /// <summary>
        /// 由于缓存中包含了驳船和大船相关的路线日期信息，在路线改变发生时，需要清空对应船某个航次的路线信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _preVoyageEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

            if (e.OldValue == null || e.OldValue == DBNull.Value)
            {
                return;

            }

            Guid voyageId = new Guid(e.OldValue.ToString());
            listVoyageDateInfo.RemoveAll(item => item.VoyageId == voyageId);
        }

        /// <summary>
        /// 
        /// </summary>
        private void RemoveEditValueChangeHandler()
        {
            new List<UCButtonEdit>() { _porButtonEdit, _polButtonEdit, _podButtonEdit }.ForEach(item =>
            {
                if (item != null)
                {
                    item.TagChanged -= DateEditValueChange;
                }
            });



        }
        private void EnsureVoyageListExists()
        {
            if (listVoyageDateInfo == null || listVoyageDateInfo.Count <= 0)
            {
                GetVoyageRoutesData();
            }
        }
        /// <summary>
        /// 港口改变时，刷新对应港口的ETD或ETA，并根据需要刷新相关截止日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void DateEditValueChange(object sender, EventArgs args)
        {
            ButtonEdit edit = sender as ButtonEdit;
            if (edit == null)
                return;
            EnsureVoyageListExists();
            DateEdit dateEdit = null;

            Guid? portId = null;
            if (edit.Tag !=null && edit.Tag != DBNull.Value)
            {
                portId = new Guid(edit.Tag.ToString());
            }
            Guid? voyageId = null;
            VoyageDateInfo info = null;
            DateTime? dtETDorETA = null;
            bool isNeedSetCloseRelativeDate=false;
            string editName = edit.Name.ToLower();

            if (editName.Contains("Receipt".ToLower()))
            {
                dateEdit = _porDateEdit;

                if (this._preVoyageEdit != null && this._preVoyageEdit.Tag != null && this._preVoyageEdit.Tag != DBNull.Value)
                {
                   
                   voyageId =new Guid(this._preVoyageEdit.Tag.ToString());
                   isNeedSetCloseRelativeDate=true;
                }
            }
            else if (editName.Contains("pol"))
            {
                dateEdit = _polDateEdit;
                if ((this._preVoyageEdit == null || this._preVoyageEdit.Tag == null || this._preVoyageEdit.Tag==DBNull.Value) && this._voyageEdit != null && this._voyageEdit.Tag != null && this._voyageEdit.Tag !=DBNull.Value)
                {
                    voyageId = new Guid(this._voyageEdit.Tag.ToString());
                    isNeedSetCloseRelativeDate=true;
                }
            }
            else if (editName.Contains("pod"))
            {
                dateEdit = _podDateEdit;
            }
            info = listVoyageDateInfo.Find(item => item.PortId == portId && item.VoyageId == voyageId);
            if (info != null)
            {
                dtETDorETA = info.ETD;
            }
            SetDefaultDate(sender as ButtonEdit, dateEdit,info,dtETDorETA,isNeedSetCloseRelativeDate);

        }

        private void SetDefaultDate(ButtonEdit btnEdit, DateEdit dateEdit,VoyageDateInfo dateInfo,DateTime? dtETDorETA,bool isNeedSetCloseRelativeDate)
        {

            if (dateEdit != null)
            {
                dateEdit.EditValue = dtETDorETA;
            }
            if (isNeedSetCloseRelativeDate)
            {
                if (dateInfo != null)
                {

                    if (_closingDateEdit != null)
                    {

                        _closingDateEdit.EditValue = dateInfo.ClosingDate;

                    }
                    if (_cyClosingDateEdit != null)
                    {
                        _cyClosingDateEdit.EditValue = dateInfo.CYClosingDate;
                    }
                    if (_docClosingDateEdit != null)
                    {
                        _docClosingDateEdit.EditValue = dateInfo.DOCClosingDate;
                    }


                }
                else
                {
                    List<DateEdit> dateEdits = new List<DateEdit>() { _closingDateEdit, _cyClosingDateEdit, _docClosingDateEdit };
                    dateEdits.ForEach(item =>
                    {
                        if (item != null)

                            item.EditValue = null;
                    });
                }

            }

        }
        private void FillVoyageDateInfo(VoyageSelectionInfo info)
        {
            if (info == null)
            {
                return;
            }
            SetShowCheckEditState(info);
            if (info.VoyageId == null)
            {
                return;
            }
            List<VoyageDateInfo> items = listVoyageDateInfo.Where(item => item.VoyageId == info.VoyageId.Value).ToList();
            //如果选择了驳船，且POR有选择，则相关截止日以POR对应的截止日为准
            if (info.VoyageType == VoyageType.PreVoyage)
            {
                SetCloseRelativeDate(info, _porButtonEdit, items, _porDateEdit);
            }
            //如果选择了大船而同时没有选择驳船，且POL有选择，则相关截止日以POL对应的截止日为准
            if ((_preVoyageEdit == null || _preVoyageEdit.EditValue == null) && info.VoyageType == VoyageType.Voyage)
            {
                SetCloseRelativeDate(info, _polButtonEdit, items, _polDateEdit);
            }
           
            if (_podButtonEdit != null && _podButtonEdit.Tag != null && _podButtonEdit.Tag !=DBNull.Value)
            {
                Guid podPortId = (Guid)_podButtonEdit.Tag;
                VoyageDateInfo detail = items.Find(item => item.PortId == podPortId);
                if (detail != null)
                {
                    _podDateEdit.EditValue = detail.ETA;
                }
                else
                {
                    _podDateEdit.EditValue =null;
                }
            }
        }

        private void SetShowCheckEditState(VoyageSelectionInfo info)
        {
            if (info == null)
            {
                return;
            }
            CheckEdit cbxEdit = null;
            if (info.VoyageType == VoyageType.PreVoyage)
            {
                cbxEdit = _cbxShowPrevoyage;
            }
            else
            {
                cbxEdit = _cbxShowVoyage;
            }
            if (cbxEdit != null)
            {
                cbxEdit.Checked = !(info.VoyageId == null || info.VoyageId == Guid.Empty);
            }
        }
       /// <summary>
       /// 设置相关截止日日期
       /// </summary>
       /// <param name="selectionInfo"></param>
       /// <param name="buttonEdit"></param>
       /// <param name="items"></param>
       /// <param name="dateEdit"></param>
        private void SetCloseRelativeDate(VoyageSelectionInfo selectionInfo, ButtonEdit buttonEdit, List<VoyageDateInfo> items, DateEdit dateEdit)
        {
            if (buttonEdit != null && buttonEdit.Tag != null && buttonEdit.Tag !=DBNull.Value)
            {
                Guid portId = new Guid(buttonEdit.Tag.ToString());
                VoyageDateInfo detail = items.Find(item => item.PortId == portId);

                if (detail != null)
                {
                    if (dateEdit != null)
                    {
                        dateEdit.EditValue = detail.ETD;
                    }
                    if (_closingDateEdit != null)
                    {
                        _closingDateEdit.EditValue = detail.ClosingDate;
                    }

                    if (_docClosingDateEdit != null)
                    {
                        _docClosingDateEdit.EditValue = detail.DOCClosingDate;
                    }
                    if (_cyClosingDateEdit != null)
                    {
                        _cyClosingDateEdit.EditValue = detail.CYClosingDate;
                    }
                }
                else
                {
                    List<DateEdit> dateEdits = new List<DateEdit>() { dateEdit,_closingDateEdit, _cyClosingDateEdit, _docClosingDateEdit };
                    dateEdits.ForEach(item =>
                    {
                        if (item != null)

                            item.EditValue = null;
                    });
                }
            }
        }
        /// <summary>
        /// 获取验证信息
        /// </summary>
        /// <returns></returns>
        public bool ValidateDateInfo()
        {
            //remove by tom
            return true;
            //暂时去掉，不实现同步其它业务的ETD。
            

            if (!isDataChanged)
            {
                return true;
            }
            if (isDataSaved)
            {
                GetVoyageRoutesData();
            }
            if (_formType == VoyageFormType.Booking)
            {
                return ValidateBookingInfo();
            }
            else if (_formType == VoyageFormType.MBL)
            {
                return ValidateMBLDateInfo();
            }
            else if (_formType == VoyageFormType.HBL)
            {
                return ValidateHBLDateInfo();
            }
            else if (_formType == VoyageFormType.OIBusiness)
            {
                return ValidateOIBusinessInfo();
            }
            else if (_formType == VoyageFormType.DomesticTrade)
            {
                return ValidateBookingInfo();
            }
            return true;
        }
        /// <summary>
        /// 获取航次下的所有路线信息
        /// </summary>
        private void GetVoyageRoutesData()
        {
            List<Guid?> voyageIds = GetVoyageIds();
            listVoyageDateInfo.Clear();
            VoyageDateInfoGetter getter = new VoyageDateInfoGetter();
            VoyageDateDelegate voyageDelegate = getter.GetVoyageDateInfoList;
            IAsyncResult asyncResult = voyageDelegate.BeginInvoke(voyageIds, null, null);
            listVoyageDateInfo = voyageDelegate.EndInvoke(asyncResult);
        }

        private List<Guid?> GetVoyageIds()
        {
            List<Guid?> ids = new List<Guid?>();
            if (this._preVoyageEdit == null && this._voyageEdit == null)
            {
                throw new NullReferenceException();
            }
            if (this._preVoyageEdit != null && this._preVoyageEdit.Tag != null&& this._preVoyageEdit.Tag !=DBNull.Value)
            {
                ids.Add((Guid?)this._preVoyageEdit.Tag);
            }
            if (this._voyageEdit != null && this._voyageEdit.Tag != null && this._voyageEdit.Tag != DBNull.Value)
            {
                ids.Add((Guid?)this._voyageEdit.Tag);
            }
            return ids;
        }

        private bool ValidateHBLDateInfo()
        {
            return ValidateMBLDateInfo();
        }

        private bool ValidateMBLDateInfo()
        {
            List<string> voyageDateValidationInfo = new List<string>();
            //如果修改了收货地，装货地的ETD或者卸货港的ETA,或者修改了截柜日，截文件日，截关日，则提示
            //您修改了XXX,将会影响同航次下的其他业务，是否确定？

            //收货地Id
            Guid porId = (this._porButtonEdit.Tag == null || string.IsNullOrEmpty(this._porButtonEdit.Tag.ToString())) ? Guid.Empty : (Guid)this._porButtonEdit.Tag;
            Guid polId = (this._polButtonEdit.Tag == null || string.IsNullOrEmpty(this._polButtonEdit.Tag.ToString())) ? Guid.Empty : (Guid)this._polButtonEdit.Tag;
            VoyageDateInfo detailInfo = new VoyageDateInfo();
            if (this._preVoyageEdit != null && this._preVoyageEdit.EditValue != null)
            {
                detailInfo = listVoyageDateInfo.Find(item => item.PortId == porId);
            }
            else if (this._voyageEdit.EditValue != null)
            {
                detailInfo = listVoyageDateInfo.Find(item => item.PortId == polId);
            }
            DateTime date = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            if (detailInfo != null)
            {
                if (detailInfo.ETD.HasValue)
                {
                    date = detailInfo.ETD.Value;
                    if (date.Date != this._porDateEdit.DateTime.Date)
                    {
                        voyageDateValidationInfo.Add("ETD of POR");
                    }
                }
            }
            VoyageDateInfo info2 = listVoyageDateInfo.Find(item => item.PortId == polId);
            if (info2 != null)
            {
                if (info2.ETD.HasValue)
                {
                    date = info2.ETD.Value;
                    if (date.Date != this._polDateEdit.DateTime.Date)
                    {
                        voyageDateValidationInfo.Add("ETD of POL");

                    }
                }
            }
            if (this._podButtonEdit.Tag != null && string.IsNullOrEmpty(this._podButtonEdit.Tag.ToString()))
            {
                Guid podId = (Guid)this._podButtonEdit.Tag;
                info2 = listVoyageDateInfo.Find(item => item.PortId == podId);
                if (info2 != null)
                {
                    if (info2.ETA.HasValue)
                    {
                        date = info2.ETA.Value;
                        if (date.Date != this._podDateEdit.DateTime.Date)
                        {
                            voyageDateValidationInfo.Add("ETA of POD");

                        }
                    }
                }
            }
            if (voyageDateValidationInfo.Count > 0)
            {
                string dateErrorInfo = voyageDateValidationInfo.Select(o => o).Aggregate((a, b) => a + "," + b);
                string text = string.Format("{0}{1}{2}", LocalData.IsEnglish ? "You have modified " : "您修改了", dateErrorInfo, LocalData.IsEnglish ? ",which will affect other businesses of the same voyage,are you sure?" : "，将会影响同航次下的其他业务，是否确定？");
                DialogResult result = XtraMessageBox.Show(text, LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return false;
                }

            }
            return true;
        }

        private bool ValidateOIBusinessInfo()
        {
            List<string> voyageDateValidationInfo = new List<string>();
            //如果修改了装货地的ETD或者卸货港的ETA,
            //您修改了XXX,将会影响同航次下的其他业务，是否确定？

            Guid polId = (Guid)this._voyageEdit.EditValue;
            VoyageDateInfo detailInfo = new VoyageDateInfo();

            if (polId != null)
            {
                detailInfo = listVoyageDateInfo.Find(item => item.PortId == polId);
            }


            detailInfo = listVoyageDateInfo.Find(item => item.PortId == polId);
            DateTime date = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            if (detailInfo != null)
            {
                if (detailInfo.ETD.HasValue)
                {
                    date = detailInfo.ETD.Value;
                    if (date.Date != this._polDateEdit.DateTime.Date)
                    {
                        voyageDateValidationInfo.Add("ETD of POL");

                    }
                }
            }
            if (this._podButtonEdit != null && this._podButtonEdit.Tag != null && !string.IsNullOrEmpty(this._podButtonEdit.Tag.ToString()))
            {
                Guid podId = (Guid)this._podButtonEdit.Tag;
                detailInfo = listVoyageDateInfo.Find(item => item.PortId == podId);
                if (detailInfo != null)
                {
                    if (detailInfo.ETA.HasValue)
                    {
                        date = detailInfo.ETA.Value;
                        if (date.Date != this._podDateEdit.DateTime.Date)
                        {
                            voyageDateValidationInfo.Add("ETA of POD");
                        }
                    }
                }
            }
            if (voyageDateValidationInfo.Count > 0)
            {
                string dateErrorInfo = voyageDateValidationInfo.Select(o => o).Aggregate((a, b) => a + "," + b);
                string text = string.Format("{0}{1}{2}", LocalData.IsEnglish ? "You have modified " : "您修改了", dateErrorInfo, LocalData.IsEnglish ? ",which will affect other businesses of the same voyage,are you sure?" : "，将会影响同航次下的其他业务，是否确定？");
                DialogResult result = XtraMessageBox.Show(text, LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return false;
                }

            }

            return true;
        }
        /// <summary>
        /// 如果修改了收货地，装货地的ETD或者卸货港的ETA,或者修改了截柜日，截文件日，截关日，则提示
        ///您修改了XXX,将会影响同航次下的其他业务，是否确定？
        /// </summary>
        /// <param name="validateInfo"></param>
        /// <returns></returns>
        private bool ValidateBookingInfo()
        {
            List<string> validateInfo = new List<string>();

            //收货地Id
            Guid porId = (this._porButtonEdit.Tag == null || string.IsNullOrEmpty(this._porButtonEdit.Tag.ToString())) ? Guid.Empty : (Guid)this._porButtonEdit.Tag;
            Guid polId = (this._polButtonEdit.Tag == null || string.IsNullOrEmpty(this._polButtonEdit.Tag.ToString())) ? Guid.Empty : (Guid)this._polButtonEdit.Tag;
            VoyageDateInfo detailInfo = new VoyageDateInfo();
            if (this._preVoyageEdit != null && this._preVoyageEdit.EditValue != null && (Guid)this._preVoyageEdit.EditValue != Guid.Empty)
            {
                detailInfo = listVoyageDateInfo.Find(item => item.PortId == porId);
            }
            else if (this._voyageEdit.EditValue != null && (Guid)this._voyageEdit.EditValue != Guid.Empty)
            {
                detailInfo = listVoyageDateInfo.Find(item => item.PortId == polId);
            }
            VoyageDateInfo detailInfo2 = new VoyageDateInfo();
            DateTime date = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            if (detailInfo != null)
            {
                if (detailInfo.ETD.HasValue)
                {
                    date = detailInfo.ETD.Value;
                    if (date.Date != this._porDateEdit.DateTime.Date)
                    {
                        validateInfo.Add("ETD of POR");
                    }
                }
            }
            detailInfo2 = listVoyageDateInfo.Find(item => item.PortId == polId);
            if (detailInfo2 != null)
            {
                if (detailInfo2.ETD.HasValue)
                {
                    date = detailInfo2.ETD.Value;
                    if (date.Date != this._polDateEdit.DateTime.Date)
                    {
                        validateInfo.Add("ETD of POL");

                    }
                }
            }
            Guid podId = (Guid)this._podButtonEdit.Tag;
            detailInfo2 = listVoyageDateInfo.Find(item => item.PortId == podId);
            if (detailInfo2 != null)
            {
                if (detailInfo2.ETA.HasValue)
                {
                    date = detailInfo2.ETA.Value;
                    if (date.Date != this._podDateEdit.DateTime.Date)
                    {
                        validateInfo.Add("ETA of POD");

                    }
                }
            }



            if (detailInfo != null)
            {
                if (_closingDateEdit != null)
                {
                    DateTime dtClose = this._closingDateEdit.DateTime;
                    if (dtClose == null && !detailInfo.ClosingDate.HasValue)
                    {

                    }
                    else if (dtClose == null || detailInfo.ClosingDate.HasValue)
                    {
                        validateInfo.Add(LocalData.IsEnglish ? "Closing Date" : "截关日");
                    }
                    else if (detailInfo.ClosingDate != null && dtClose.Date != detailInfo.ClosingDate.Value.Date)
                    {
                        validateInfo.Add(LocalData.IsEnglish ? "Closing Date" : "截关日");
                    }
                }
                if (_cyClosingDateEdit != null)
                {
                    DateTime dtCYClose = this._cyClosingDateEdit.DateTime;
                    if (dtCYClose == null && !detailInfo.CYClosingDate.HasValue)
                    {

                    }
                    else if (dtCYClose == null || detailInfo.CYClosingDate.HasValue)
                    {
                        validateInfo.Add(LocalData.IsEnglish ? "CYClosing Date" : "截柜日");
                    }
                    else if (detailInfo.CYClosingDate != null && dtCYClose.Date != detailInfo.CYClosingDate.Value.Date)
                    {
                        validateInfo.Add(LocalData.IsEnglish ? "CYClosing Date" : "截柜日");
                    }
                }
                if (_docClosingDateEdit != null)
                {
                    DateTime dtDOCClose = this._docClosingDateEdit.DateTime;
                    if (dtDOCClose == null && !detailInfo.DOCClosingDate.HasValue)
                    {

                    }
                    else if (dtDOCClose == null || detailInfo.DOCClosingDate.HasValue)
                    {
                        validateInfo.Add(LocalData.IsEnglish ? "DOCClosing Date" : "截文件日");
                    }
                    else if (detailInfo.DOCClosingDate != null && dtDOCClose.Date != detailInfo.DOCClosingDate.Value.Date)
                    {
                        validateInfo.Add(LocalData.IsEnglish ? "DOCClosing Date" : "截文件日");
                    }
                }
            }
            if (validateInfo.Count > 0)
            {
                string errorInfo = validateInfo.Select(o => o).Aggregate((a, b) => a + "," + b);
                string text = string.Format("{0}{1}{2}", LocalData.IsEnglish ? "You have modified " : "您修改了", errorInfo, LocalData.IsEnglish ? ",which will affect other businesses of the same voyage,are you sure?" : "，将会影响同航次下的其他业务，是否确定？");
                DialogResult result = XtraMessageBox.Show(text, LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {

                    return false;
                }

            }
            return true;
        }

        private void SetDateEditEnablity()
        {
            if ((_preVoyageEdit == null && _voyageEdit == null) || (_polDateEdit == null))
                return;
            bool isEditValueNull = false;
            if ((_preVoyageEdit != null && _preVoyageEdit.EditValue == null) && (_voyageEdit != null && _voyageEdit.EditValue == null))
            {
                isEditValueNull = true;
            }
            if (_preVoyageEdit == null && (_voyageEdit != null && _voyageEdit.EditValue == null))
            {
                isEditValueNull = true;
            }
            List<DateEdit> dateEdits = new List<DateEdit>() { _porDateEdit, _polDateEdit, _podDateEdit };
            if (isEditValueNull)
            {
                dateEdits.ForEach(item =>
                {
                    if (item != null)
                    { item.Enabled = false; item.EditValue = null; }
                });
            }
            else
            {
                dateEdits.ForEach(item =>
                    {
                        if (item != null)
                            item.Enabled = true;
                    });
            }
        }
        #endregion



        #region IDisposable 成员
        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        /// <summary>
        /// 资源释放
        /// </summary>
        /// <param name="isDisposing"></param>
        public void Dispose(Boolean isDisposing)
        {
            if (isDisposing)
            {
                if (this._preVoyageEdit != null)
                {
                    _preVoyageEdit.EditValueChanging -= _preVoyageEdit_EditValueChanging;

                    _preVoyageEdit.EditValueChanged -= voyageEdit_EditValueChanged;
                }
                _voyageEdit.EditValueChanging -= _preVoyageEdit_EditValueChanging;
                _voyageEdit.EditValueChanged -= voyageEdit_EditValueChanged;
                RemoveEditValueChangeHandler();
            }
        }

        #endregion
    }
}
