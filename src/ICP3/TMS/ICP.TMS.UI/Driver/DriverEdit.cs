using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.TMS.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;

namespace ICP.TMS.UI
{
    public partial class DriverEdit : BaseEditPart
    {
        #region 构造
        public DriverEdit()
        {
            InitializeComponent();
            this.Closing += new EventHandler<FormClosingEventArgs>(DriverEdit_Closing);
            this.cmbCityID.Enter += new EventHandler(cmbCityID_Enter);
            this.Disposed += delegate {
                this.Saved = null;
                this.cmbCityID.Enter -= this.cmbCityID_Enter;
                this.cmbProvinceID.EditValueChanged -= this.cmbProvinceID_EditValueChanged;
                this.Closing -= this.DriverEdit_Closing;
                this._countryProvinceList = null;
                this.drivers = null;
                this._countryProvinceList = null;
                this.bsGeography.DataSource = null;
                this.bsList.DataSource = null;
                this.bsGeography.Dispose();
                this.bsList.Dispose();
                this.drivers = null;
                this.cmbTruck.OnFirstEnter -= this.OncmbTruckFirstEnter;
                this.cmbProvinceID.Enter -= this.OncmbProvinceIDFirstEnter;
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }


        void DriverEdit_Closing(object sender, FormClosingEventArgs e)
        {
            if (drivers.IsDirty)
            {
                DialogResult dr = Utility.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!this.SaveData())
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
        #endregion

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
        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }
        #endregion

        #region 私有字段
        /// <summary>
        /// 当前选择的国家&省份
        /// </summary>
        CountryProvinceList CurrentGeography
        {
            get
            {
                return bsGeography.Current as CountryProvinceList;
            }
        }
        DriversDataList drivers = new DriversDataList();
        private List<CountryProvinceList> _countryProvinceList;
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
                InitControls();
            }
        }
        private bool isProvinceEntered = false;
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {

            //初始化国家列表
            this.cmbProvinceID.Enter += this.OncmbProvinceIDFirstEnter;

            //初始化拖车资料
            this.cmbTruck.OnFirstEnter += this.OncmbTruckFirstEnter;

        }
        private void OncmbProvinceIDFirstEnter(object sender, EventArgs e)
        {
            if (isProvinceEntered)
            {
                return;
            }
            //国家，省份控件初始化
            _countryProvinceList = GeographyService.GetCountryProvinceList(
              string.Empty,
              string.Empty,
              null,
              true,
              0);

            cmbProvinceID.AllowMultSelect = false;
            cmbProvinceID.RootValue = Guid.Empty;
            cmbProvinceID.ParentMember = "ParentID";
            cmbProvinceID.ValueMember = "ID";
            cmbProvinceID.DisplayMember = LocalData.IsEnglish ? "EName" : "CName";
            cmbProvinceID.DataSource = _countryProvinceList;
            cmbProvinceID.EditValueChanged += new EventHandler(cmbProvinceID_EditValueChanged);

            if (drivers.ProvinceID != null && drivers.ProvinceID != Guid.Empty)
            {
                cmbProvinceID.InitSelectedNode(drivers.ProvinceID);
            }
            else
            {
                cmbProvinceID.InitSelectedNode(null);
            }
            isProvinceEntered = true;
        }
        private void OncmbTruckFirstEnter(object sender, EventArgs e)
        {
            List<TruckDataList> list = TruckBookingService.GetTruckDataList(null, null, TruckDateSeachType.ALL, true, null, null, LocalData.IsEnglish);
            this.cmbTruck.Properties.BeginUpdate();
            this.cmbTruck.Properties.Items.Clear();
            this.cmbTruck.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(null, null));
            foreach (TruckDataList item in list)
            {
                this.cmbTruck.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.TruckNo, item.ID));
            }
            this.cmbTruck.Properties.EndUpdate();
        }
        /// <summary>
        /// 国家发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbProvinceID_EditValueChanged(object sender, EventArgs e)
        {
            this.cmbCityID.Properties.Items.Clear();
        }

        void cmbCityID_Enter(object sender, EventArgs e)
        {
            InitCity();
        }

        /// <summary>
        /// 初始化城市
        /// </summary>
        private void InitCity()
        {
            Guid? countryId = (Guid?)this.cmbProvinceID.GetSelectedValues("ParentID");
            Guid? provinceId = null;
            if (countryId != null)   //countryId != null 说明当前选择的是省份，否则是国家
            {
                provinceId = (Guid)cmbProvinceID.SelectedValue;
            }
            else
            {
                countryId = (Guid)cmbProvinceID.SelectedValue;
            }
            if (Utility.GuidIsNullOrEmpty(countryId))
            {
                return;
            }
            if (!Utility.GuidIsNullOrEmpty(provinceId) && !Utility.GuidIsNullOrEmpty(countryId))
            {
                this.cmbCityID.Properties.Items.Clear();
                this.cmbCityID.Properties.BeginUpdate();

                List<LocationList> locationList = GeographyService.GetLocationList(string.Empty,
                                                                                    countryId,
                                                                                    provinceId,
                                                                                    null,
                                                                                    null,
                                                                                    null,
                                                                                    true,
                                                                                    0);

                locationList.Insert(0, new LocationList() { ID = Guid.Empty, CName = string.Empty, EName = string.Empty });
                if (locationList != null && locationList.Count != 0)
                {
                    foreach (var item in locationList)
                    {
                        cmbCityID.Properties.Items.Add(
                            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
                    }
                    cmbCityID.SelectedIndex = 0;
                }

                this.cmbCityID.Properties.EndUpdate();
            }
            else
            {
                this.cmbCityID.Properties.Items.Clear();
            }
        }

        /// <summary>
        /// 注册消息
        /// </summary>
        private void InitMessage()
        {
            this.RegisterMessage("NameIsNull",LocalData.IsEnglish?"Name must input":"司机姓名:必须输入");
            this.RegisterMessage("MobileIsNull",LocalData.IsEnglish?"Moblie must input": "手机号:必须输入");
            this.RegisterMessage("AdressIsNull",LocalData.IsEnglish?"Adress must input":"地址:必须输入");
            this.RegisterMessage("CarIDIsNull", LocalData.IsEnglish?"Car must input":"身份:必须输入");

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
            DriversDataList list = data as DriversDataList;
            if (list == null)
            {
                bsList.DataSource = typeof(ICP.TMS.ServiceInterface.DriversDataList);
                this.Enabled = false;

            }
            else
            {
                drivers = new DriversDataList();
                Utility.CopyToValue(list, drivers, typeof(DriversDataList));

                drivers.IsDirty = false;
                bsList.DataSource = drivers;
                bsList.ResetBindings(false);

                cmbProvinceID.Text = drivers.CountryAndProvince;


                this.cmbCityID.ShowSelectedValue(drivers.CityID, drivers.CityName);
                this.cmbTruck.ShowSelectedValue(drivers.TruckID, drivers.TruckNo);

                if (drivers.IsValid)
                {
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = false;
                }
                drivers.BeginEdit();

            }
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            return this.SaveDriver();
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
            SaveDriver();
        }
        /// <summary>
        /// 保存拖车资料
        /// </summary>
        /// <returns></returns>
        private bool SaveDriver()
        {

            if (!ValidateData())
            {
                return false;
            }
            if (!this.drivers.IsDirty)
            {
                return false;
            }
            try
            {
                drivers.ProvinceID = (Guid?)this.cmbProvinceID.GetSelectedValues("ID");
                drivers.CountryID = (Guid?)this.cmbProvinceID.GetSelectedValues("ParentID");



                SingleResult result = TruckBookingService.SaveDriverInfo(
                  drivers.ID,
                  drivers.Name,
                  drivers.Mobile,
                  drivers.Adress,
                  drivers.CardNo,
                  drivers.CountryID,
                  drivers.ProvinceID,
                  drivers.CityID,
                  drivers.TruckID,
                  drivers.Remark,
                  LocalData.UserInfo.LoginID,
                  drivers.UpdateDate,
                  LocalData.IsEnglish);

                drivers.ID = result.GetValue<Guid>("ID");
                drivers.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                string strCountryProvince = string.Empty;
                if (!string.IsNullOrEmpty(this.cmbProvinceID.EditValue.ToString()))
                {
                    strCountryProvince += cmbProvinceID.EditValue.ToString();
                }

                drivers.CountryAndProvince = strCountryProvince;

                if (Saved != null)
                {
                    Saved(this.drivers);
                }

                this.drivers.CancelEdit();

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

                return true;
            }
            catch (Exception ex)
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
            drivers.EndEdit();

            this.labAdress.Focus();

            bsList.EndEdit();

            bool isSure = true;


            if (string.IsNullOrEmpty(drivers.Name))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "NameIsNull"));
                isSure = false;
            }

            if (string.IsNullOrEmpty(drivers.Mobile))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "MobileIsNull"));
                isSure = false;
            }
            if (string.IsNullOrEmpty(drivers.Adress))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "AdressIsNull"));
                isSure = false;
            }
            if (string.IsNullOrEmpty(drivers.CardNo))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "CarIDIsNull"));
                isSure = false;
            }

            return isSure;
        }
        #endregion





    }
}
