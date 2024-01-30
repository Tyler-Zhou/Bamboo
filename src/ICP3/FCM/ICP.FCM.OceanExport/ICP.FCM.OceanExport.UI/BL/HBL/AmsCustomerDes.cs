using System;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using System.Drawing;

namespace ICP.FCM.OceanExport.UI.BL.HBL
{
    public partial class AmsCustomerDes : UserControl
    {
        bool c = false;
        public IGeographyService geographyService { get; set; }

        public event EventHandler OnOk;

        public AmsCustomerDes()
        {
            InitializeComponent();

            cmbCountry.ButtonClick += new ButtonPressedEventHandler(cmbCountry_ButtonClick);
            cmbCityState.ButtonClick += new ButtonPressedEventHandler(cmbCityState_ButtonClick);
            cmbZip.ButtonClick += new ButtonPressedEventHandler(cmbZip_ButtonClick);
            this.txtName.EditValueChanged += new System.EventHandler(this.txtName_EditValueChanged);
            this.cmbCityState.SelectedIndexChanged += new System.EventHandler(this.cmbCityState_SelectedIndexChanged);
            this.cmbCountry.SelectedIndexChanged += new System.EventHandler(this.cmbCountry_SelectedIndexChanged);
            txtAddress.TextChanged += txtAddress_TextChanged;
            this.Disposed += delegate
            {
                this.geographyService = null;
                this.bindingSource.DataSource = null;
                this.bindingSource.Dispose();
                cmbCountry.ButtonClick -= new ButtonPressedEventHandler(cmbCountry_ButtonClick);
                cmbCityState.ButtonClick -= new ButtonPressedEventHandler(cmbCityState_ButtonClick);
                cmbZip.ButtonClick -= new ButtonPressedEventHandler(cmbZip_ButtonClick);
                this.txtName.EditValueChanged -= new System.EventHandler(this.txtName_EditValueChanged);
                txtAddress.TextChanged -= txtAddress_TextChanged;
                this.cmbCityState.SelectedIndexChanged -= new System.EventHandler(this.cmbCityState_SelectedIndexChanged);
                this.cmbCountry.SelectedIndexChanged -= new System.EventHandler(this.cmbCountry_SelectedIndexChanged);
            };
        }

        public void SetDataBinding(CustomerDescriptionForAMS customerDescriptionForAMS)
        {
            this.cmbCityState.SelectedIndexChanged -= new System.EventHandler(this.cmbCityState_SelectedIndexChanged);
            this.cmbCountry.SelectedIndexChanged -= new System.EventHandler(this.cmbCountry_SelectedIndexChanged);
            customerDescriptionForAMS = customerDescriptionForAMS ?? new CustomerDescriptionForAMS();
            this.bindingSource.DataSource = customerDescriptionForAMS;
            this.bindingSource.ResetBindings(false);

            if (customerDescriptionForAMS.Address.Length > 70)
            {
                customerDescriptionForAMS.Address = customerDescriptionForAMS.Address.Substring(0, 70);
            }
            if (customerDescriptionForAMS.Address.Length > 70)
            {
                customerDescriptionForAMS.Address = customerDescriptionForAMS.Address.Trim();
            }

            txtAddress.Text = customerDescriptionForAMS.Address.Trim();
            txtAddress_Leave(null, null);

            this.cmbCityState.SelectedIndexChanged += new System.EventHandler(this.cmbCityState_SelectedIndexChanged);
            this.cmbCountry.SelectedIndexChanged += new System.EventHandler(this.cmbCountry_SelectedIndexChanged);
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
                //LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "正在加载城市信息...");
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
                            //cmbCityState.ShowPopup();
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
                //LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "正在获取邮编...");
                //选择城市的所有街道及邮编信息
                List<LocationList> list = geographyService.GetLocationList(city, null, null, true, null, null, null, 0);
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
                }
            }
            OnOk(this, new EventArgs());
        }

        private void txtName_EditValueChanged(object sender, EventArgs e)
        {
            if (CheckContext(txtName.Text, "Name"))
            {
                txtName.EditValueChanged -= new System.EventHandler(txtName_EditValueChanged);
                CustomerDescriptionForAMS.Name = txtName.Text;
                txtName.EditValueChanged += new System.EventHandler(txtName_EditValueChanged);
                //txtName.ToolTip = "";
            }
            else
            {
                txtName.EditValueChanged -= new System.EventHandler(txtName_EditValueChanged);
                txtName.Text = txtName.Text.Substring(0, 35);
                txtName.EditValueChanged += new System.EventHandler(txtName_EditValueChanged);
                //txtName.ToolTip = "[客户名称]长度能超过35字符,将自动截取前35字符";
            }
            OnOk(this, new EventArgs());
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            string[] textArr = txtAddress.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (textArr.Length > 2)
            {
                txtAddress.ForeColor = Color.Red;
                return;
            }

            foreach (string text in textArr)
            {
                if (text.Length > 35)
                {
                    txtAddress.ForeColor = Color.Red;
                    return;
                }
            }

            txtAddress.ForeColor = Color.Black;
        }

        private bool CheckContext(string context, string site)
        {
            if (context.Length > 35)
            {
                //MessageBoxService.ShowInfo("输入的[" + site + "]长度能超过35字符,将自动截取前35字符！");
                return false;
            }

            return true;
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAddress.Text))
                return;

            string text = txtAddress.Text.Replace(Environment.NewLine, " ");
            string org = string.Empty;

            string[] str = text.Split(' ');
            string first = string.Empty;
            string second = string.Empty;
            foreach (string s in str)
            {
                if (string.IsNullOrEmpty(s))
                    continue;

                if (string.IsNullOrEmpty(first))
                {
                    first += s;
                }
                else
                {
                    if (first.Length + s.Length + 1 < 35)
                    {
                        first += " " + s;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(second))
                        {
                            second += s;
                        }
                        else
                        {
                            if (second.Length + s.Length + 1 < 35)
                            {
                                second += " " + s;
                            }
                            else
                                break;
                        }
                    }
                }
            }

            txtAddress.TextChanged -= txtAddress_TextChanged;
            if (string.IsNullOrEmpty(second))
            {
                txtAddress.Text = first;
            }
            else
            {
                txtAddress.Text = first.PadRight(35, ' ') + Environment.NewLine + second;
            }

            CustomerDescriptionForAMS.Address = txtAddress.Text;
            txtAddress.TextChanged += txtAddress_TextChanged;
            OnOk(this, new EventArgs());
        }
    }
}
