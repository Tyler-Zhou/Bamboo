//-----------------------------------------------------------------------
// <copyright file="CustomerDescriptionForAMSControl.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.Controls
{
    using System;
    using System.Windows.Forms;
    using DevExpress.XtraEditors.Controls;
    using System.Collections.Generic;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Common.ServiceInterface;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 客户描述信息弹出控件(AMS)
    /// </summary>
    internal partial class CustomerDescriptionForAMSControl : UserControl
    {
        public IGeographyService geographyService { get; set; }

        bool c = false;
        public CustomerDescriptionForAMSControl()
        {
            c = false;
            InitializeComponent();
            cmbCountry.ButtonClick += new ButtonPressedEventHandler(cmbCountry_ButtonClick);
            cmbCityState.ButtonClick += new ButtonPressedEventHandler(cmbCityState_ButtonClick);
            cmbZip.ButtonClick += new ButtonPressedEventHandler(cmbZip_ButtonClick);
            this.Disposed += delegate
            {
                this.geographyService = null;
                this.bindingSource.DataSource = null;
                this.bindingSource.Dispose();
                cmbCountry.ButtonClick -= new ButtonPressedEventHandler(cmbCountry_ButtonClick);
                cmbCityState.ButtonClick -= new ButtonPressedEventHandler(cmbCityState_ButtonClick);
                cmbZip.ButtonClick -= new ButtonPressedEventHandler(cmbZip_ButtonClick);
                this.webBrowser1.DocumentCompleted -= this.webBrowser1_DocumentCompleted;

            };
        }
        public void SetDataBinding(CustomerDescriptionForAMS customerDescriptionForAMS)
        {
            customerDescriptionForAMS = customerDescriptionForAMS ?? new CustomerDescriptionForAMS();
            this.bindingSource.DataSource = customerDescriptionForAMS;
            this.bindingSource.ResetBindings(false);
        }
        void cmbCountry_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            c = true;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                cmbCountry.Text = string.Empty;
                cmbZip.Text = string.Empty;
                cmbCityState.Text = string.Empty;
                this.bindingSource.EndEdit();
                this.bindingSource.ResetBindings(false);
            }
        }

        void cmbCityState_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            c = true;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                this.bindingSource.EndEdit();
                this.bindingSource.ResetBindings(false);
            }
            //加载城市信息
            if (e.Button.Kind == ButtonPredefines.Combo)
            {
                //如果列表为空加载城市信息
                if (cmbCityState.Properties.Items.Count <= 1)
                    LoadCity();
            }
        }

        void cmbZip_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            c = true;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                this.bindingSource.EndEdit();
                this.bindingSource.ResetBindings(false);
            }
        }

        public event EventHandler OnClear;

        public event EventHandler OnOk;

        /// <summary>
        /// 国家数据列表
        /// </summary>
        public ComboBoxItemCollection CountryItems
        {
            get
            {
                return cmbCountry.Properties.Items;
            }
        }

        public void SetLanguage(bool isEnglish)
        {
            if (isEnglish == false)
            {
                labAddress.Text = "地址";
                labCityZip.Text = "城市";
                labCountry.Text = "国家";
                lblZip.Text = "邮编";
                labName.Text = "名称";
                labCityZip.Text = "城市,州";

                btnClear.Text = "清除(&L)";
                btnOK.Text = "确定(&O)";
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public CustomerDescriptionForAMS CustomerDescriptionForAMS
        {
            get
            {
                this.bindingSource.EndEdit();
                this.bindingSource.ResetBindings(false);
                return this.bindingSource.DataSource as CustomerDescriptionForAMS;
            }
            set
            {
                if (value != null)
                {
                    this.bindingSource.DataSource = value;
                }
                else
                {
                    this.bindingSource.DataSource = new CustomerDescriptionForAMS();
                }
            }
        }

        public void Clear()
        {
            this.txtName.EditValue = string.Empty;
            this.txtAddress.EditValue = string.Empty;
            this.cmbCountry.EditValue = null;
            this.cmbCityState.EditValue = null;
            this.cmbZip.EditValue = string.Empty;
            this.bindingSource.EndEdit();
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Clear();

            if (OnClear != null)
            {
                OnClear(this, new EventArgs());
            }
        }

        /// <summary>
        /// 城市ID
        /// </summary>
        public Guid CityID { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// 邮编列表
        /// </summary>
        public List<PostalCodeInfo> PostalCodeList { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (OnOk != null)
            {
                //保存邮编信息
                if (ZipCode != null)
                {
                    if (CityID != null && CityID != Guid.Empty)
                    {
                        //判断在List里是否已经存在
                        if (PostalCodeList.Count > 0)
                        {
                            PostalCodeInfo zipObj = PostalCodeList.Find(delegate(PostalCodeInfo zip) { return zip.PostalCode == ZipCode; });
                            if (zipObj == null)
                            {
                                geographyService.SavePostalCodeInfo(CityID, ZipCode);
                            }
                        }
                        else
                            geographyService.SavePostalCodeInfo(CityID, ZipCode);
                    }
                }
                OnOk(this, new EventArgs());
            }
        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCountry.EditValue == null)
                return;
            //if (c == false) return;//第一次加载
            LoadCity();
        }

        private void LoadCity()
        {
            CustomerDescriptionForAMS.Country = cmbCountry.EditValue.ToString();
            if (string.IsNullOrEmpty(CustomerDescriptionForAMS.Country))
            {
                return;
            }
            //CustomerDescriptionForAMS.City = string.Empty;
            //CustomerDescriptionForAMS.Zip = string.Empty;
            //this.bindingSource.ResetBindings(false);
            using (new CursorHelper(Cursors.WaitCursor))
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "正在加载城市信息...");
                //根据国家 加载城市信息
                cmbCityState.Properties.Items.Clear();
                //cmbCityState.Text = string.Empty;
                //cmbZip.Text = string.Empty;

                string countryName = cmbCountry.SelectedItem.ToString();
                if (countryName != string.Empty)
                {
                    List<CountryList> list = geographyService.GetCountryList(string.Empty, countryName, true, 0);
                    if (list.Count > 0)
                    {
                        List<LocationList> listCity = geographyService.GetLocationList(string.Empty,
                                                                               list[0].ID,
                                                                               null,
                                                                               true,
                                                                               null,
                                                                               null,
                                                                               true,
                                                                               0);

                        foreach (LocationList ll in listCity)
                        {
                            cmbCityState.Properties.Items.Add(ll.EName);
                        }
                        cmbCityState.Properties.Items.Insert(0, string.Empty);
                        if (string.IsNullOrEmpty(CustomerDescriptionForAMS.City))
                        {
                            cmbCityState.SelectedIndex = 0;
                            cmbCityState.ShowPopup();
                            cmbCityState.Focus();
                        }
                        else
                            cmbCityState.Text = CustomerDescriptionForAMS.City;
                    }
                    else
                    {
                        cmbZip.Properties.Items.Clear();
                        cmbCityState.Properties.Items.Clear();
                    }
                }
            }
        }

        private void cmbCityState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (c == false) return;//第一次加载
            cmbZip.Properties.Items.Clear();
            cmbZip.EditValue = string.Empty;
            CustomerDescriptionForAMS.Zip = string.Empty;
            string city = cmbCityState.Text.Trim();
            if (!string.IsNullOrEmpty(city))
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "正在获取邮编...");
                //选择城市的所有街道及邮编信息
                List<LocationList> list = geographyService.GetLocationList(city, null, null, true, null, null, null, 0);
                string add = txtAddress.Text.Trim() + "," + city + "," + cmbCountry.Text.Trim();
                if (list.Count > 0)
                {
                    CityID = list[0].ID;//城市id
                    //邮编列表
                    PostalCodeList = geographyService.GetPostalCodeList(CityID);
                    if (PostalCodeList.Count > 0)
                    {
                        foreach (PostalCodeInfo sz in PostalCodeList)
                            cmbZip.Properties.Items.Add(sz.PostalCode);
                    }
                    //加载网络上的zip
                    webBrowser1.Navigate("http://maps.googleapis.com/maps/api/geocode/xml?address=" + add + "&sensor=false");
                }
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string temp = webBrowser1.DocumentText;
                int i = temp.IndexOf("postal_code</SPAN><SPAN class=\"m\">");
                if (i == -1)
                {
                    cmbZip.Text = string.Empty;
                }
                else
                {
                    if (cmbCountry.Text == "United States")
                    {
                        ZipCode = temp.Substring(i - 308, 5);
                        //判断数据库里是否已经存在此邮编
                        PostalCodeInfo postalCode = null;
                        if (PostalCodeList.Count > 0)
                            postalCode = PostalCodeList.Find(delegate(PostalCodeInfo pc) { return pc.PostalCode == ZipCode; });
                        if (postalCode == null)
                            cmbZip.Properties.Items.Add(ZipCode);
                    }
                    if (cmbCountry.Text == "Canada")
                    {
                        //temp = temp.Substring(i - 310);
                        ZipCode = temp.Substring(i - 310, 7);

                        if (Regex.IsMatch(temp, "^[A-Za-z][0-9][A-Za-z][ ][0-9][A-Za-z][0-9]$"))
                        {
                            //判断数据库里是否已经存在此邮编
                            PostalCodeInfo postalCode = null;
                            if (PostalCodeList.Count > 0)
                                postalCode = PostalCodeList.Find(delegate(PostalCodeInfo pc) { return pc.PostalCode == ZipCode; });
                            if (postalCode == null)
                                cmbZip.Properties.Items.Add(ZipCode);
                        }
                    }
                    cmbZip.EditValue = ZipCode;
                    CustomerDescriptionForAMS.Zip = ZipCode;
                    cmbZip.SelectedIndex = 0;
                }

            }
        }
    }

}
