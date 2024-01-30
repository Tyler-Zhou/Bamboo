using System;
using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Common.UI.ShippingLineManager
{
    public partial class NationAndPortListEditPart : BaseListEditPart
    {

        #region service
        [ServiceDependency]

        public WorkItem WorkItem
        {
            get;
            set;
        }

        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }


        #endregion


        /// <summary>
        /// 国家列表数据
        /// </summary>
        private List<ShippingCountryInfo> DataSourceCountry
        {
            get
            {
                return bsCountry.DataSource as List<ShippingCountryInfo>;
            }
        }

        public ShippingLineList listdata { get; set; }

        /// <summary>
        /// 航线ID
        /// </summary>
        private Guid ShippingLineID { get; set; }

        public NationAndPortListEditPart()
        {
            InitializeComponent();
            InitControls();
            this.repositoryItemImageComboBox1.EditValueChanged += new System.EventHandler(this.repositoryItemImageComboBox5_EditValueChanged);
        }

        private void repositoryItemImageComboBox5_EditValueChanged(object sender, EventArgs e)
        {
            gridView1.PostEditor();

            if (DataSourceCountry != null && DataSourceCountry.Count > 0)
            {
                List<Guid> countryIds = new List<Guid>();
                foreach (var item in DataSourceCountry)
                {
                    countryIds.Add((Guid)item.CountryID);
                }
                List<PortNames> portlist = new List<PortNames>();
                portlist = GeographyService.GetPortForCountryID(countryIds.ToArray());

                repositoryItemImageComboBox2.Items.Clear();
                foreach (var item in portlist)
                {
                    repositoryItemImageComboBox2.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
                }
            }
        }

        public override object DataSource
        {
            set { BindingData(value); }
        }

        private void BindingData(object data)
        {
            if (data == null)
            {
                this.bsPort.DataSource = typeof(List<ShippingPortInfo>);
                this.bsCountry.DataSource = typeof(List<ShippingCountryInfo>);
                this.Enabled = false;
            }
            else
            {
                listdata = data as ShippingLineList;
                CountryPortList countryPort = TransportFoundationService.GetGetShiLineReationCountryList(listdata.ID, LocalData.IsEnglish);
                if (countryPort != null)
                {
                    List<Guid> countryIds = new List<Guid>();
                    foreach (var item in countryPort.Country)
                    {
                        countryIds.Add(item.CountryID);
                    }
                    if (countryIds.Count > 0)
                    {
                        repositoryItemImageComboBox2.Items.Clear();
                        List<PortNames> portlist = new List<PortNames>();
                        portlist = GeographyService.GetPortForCountryID(countryIds.ToArray());

                        foreach (var item in portlist)
                        {
                            repositoryItemImageComboBox2.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
                        }
                    }
                    this.bsCountry.DataSource = countryPort.Country;
                    this.bsPort.DataSource = countryPort.Port;

                    this.bsCountry.ResetBindings(false);
                    this.bsPort.ResetBindings(false);
                }
                else
                {
                    List<ShippingPortInfo> port = new List<ShippingPortInfo>();
                    List<ShippingCountryInfo> country = new List<ShippingCountryInfo>();

                    this.bsCountry.DataSource = country;
                    this.bsPort.DataSource = port;
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShippingCountryInfo nation = new ShippingCountryInfo();
            bsCountry.Insert(bsCountry.Count, nation);
            bsCountry.ResetBindings(false);
        }

        private void InitControls()
        {
            List<Guid> countrys = new List<Guid>();
            List<CountryList> list = GeographyService.GetCountryList(string.Empty, string.Empty, null, 0);
            foreach (var item in list)
            {
                repositoryItemImageComboBox1.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
                countrys.Add(item.ID);
            }
        }

        /// <summary>
        /// 国家下拉框选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.PostEditor();

            if (DataSourceCountry != null && DataSourceCountry.Count > 0)
            {
                List<Guid> countryIds = new List<Guid>();
                foreach (var item in DataSourceCountry)
                {
                    countryIds.Add((Guid)item.CountryID);
                }
                List<PortNames> portlist = new List<PortNames>();
                portlist = GeographyService.GetPortForCountryID(countryIds.ToArray());

                repositoryItemImageComboBox2.Items.Clear();
                foreach (var item in portlist)
                {
                    repositoryItemImageComboBox2.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
                }
            }

            ShippingPortInfo port = new ShippingPortInfo();
            bsPort.Insert(bsPort.Count, port);
            bsPort.ResetBindings(false);
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //List<CountryList> list = DataSourceCountry;
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gridView1.PostEditor();
                gridView2.PostEditor();
                SaveShippingCountry();
                SaveShippingPort();
                //提示保存成功
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                    this.FindForm(),
                    "保存成功!");
            }
            catch (Exception ex)
            {
                //设置错误信息
                LocalCommonServices.ErrorTrace.SetErrorInfo(
                    this.FindForm(),
                    ex);
            }
        }

        private bool SaveShippingCountry()
        {
            int ListCount = bsCountry.Count;
            Guid?[] ids = new Guid?[ListCount];
            Guid[] shippingLineIDs = new Guid[ListCount];
            Guid[] countryIDs = new Guid[ListCount];
            DateTime?[] updateDates = new DateTime?[ListCount];

            int i = 0;
            foreach (ShippingCountryInfo cid in bsCountry.List)
            {
                ids[i] = cid.ID;  //
                shippingLineIDs[i] = cid.ShippingLineID;
                countryIDs[i] = cid.CountryID;
                updateDates[i] = cid.UpdateDate;
                i++;
            }

            ManyResultData mans = this.TransportFoundationService.SaveShiLineReationCountry(
                ids,
                listdata.ID,
                countryIDs,
                updateDates,
                LocalData.UserInfo.LoginID,
                true);

            if (mans == null || mans.ChildResults == null || mans.ChildResults.Count == 0)
            {
                return false;
            }
            else
            {
                i = 0;
                foreach (ShippingCountryInfo cid in bsCountry.List)
                {
                    cid.ID = mans.ChildResults[i].ID;
                    cid.UpdateDate = mans.ChildResults[i].UpdateDate;
                    i++;
                }
                return true;
            }
        }


        private bool SaveShippingPort()
        {
            if (bsPort == null || bsPort.List.Count == 0)
            {
                return true;
            }
            int ListCount = bsPort.Count;
            Guid?[] ids = new Guid?[ListCount];
            Guid[] shippingLineIDs = new Guid[ListCount];
            Guid[] portIds = new Guid[ListCount];
            DateTime?[] updateDates = new DateTime?[ListCount];

            int i = 0;
            foreach (ShippingPortInfo cid in bsPort.List)
            {
                ids[i] = cid.ID;  //
                shippingLineIDs[i] = cid.ShippingLineID;
                portIds[i] = cid.PortID;
                updateDates[i] = cid.UpdateDate;
                i++;
            }

            ManyResultData mans = this.TransportFoundationService.SavePortReationShippingLine(
                ids,
                listdata.ID,
                portIds,
                updateDates,
                LocalData.UserInfo.LoginID,
                true);

            if (mans == null || mans.ChildResults == null || mans.ChildResults.Count == 0)
            {
                return false;
            }
            else
            {
                i = 0;
                foreach (ShippingPortInfo cid in bsPort.List)
                {
                    cid.ID = mans.ChildResults[i].ID;
                    cid.UpdateDate = mans.ChildResults[i].UpdateDate;
                    i++;
                }
                return true;
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bsCountry.Current != null)
            {
                var country = bsCountry.Current as ShippingCountryInfo;
                if (country != null)
                {
                    if (!CommonUtility.GuidIsNullOrEmpty(country.ID))
                    {
                        TransportFoundationService.RemoveCountryReationShipping(new Guid[] { (Guid)country.ID }, LocalData.UserInfo.LoginID, new DateTime?[] { country.UpdateDate });
                    }
                    bsCountry.RemoveCurrent();
                    bsCountry.MoveFirst();
                }
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bsPort.Current != null)
            {
                var port = bsPort.Current as ShippingPortInfo;
                if (port != null)
                {
                    if (!CommonUtility.GuidIsNullOrEmpty(port.ID))
                    {
                        TransportFoundationService.RemovePortReationShipping(new Guid[] { (Guid)port.ID }, LocalData.UserInfo.LoginID, new DateTime?[] { port.UpdateDate });
                    }
                    bsPort.RemoveCurrent();
                    bsPort.MoveFirst();
                }
            }
        }
    }
}
