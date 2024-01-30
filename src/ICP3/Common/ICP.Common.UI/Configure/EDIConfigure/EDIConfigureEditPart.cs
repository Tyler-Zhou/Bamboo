using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.Common.UI.Configure.EDIConfigure
{
    [System.ComponentModel.ToolboxItem(false)]
    [SmartPart]
    public partial class EDIConfigureEditPart : BaseEditPart
    {
        #region 服务注入

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        #endregion

        #region init

        public EDIConfigureEditPart()
        {
            InitializeComponent();
            this.Enabled = false;
            this.Disposed += delegate {
                cmbCarrier.SelectedIndexChanged -= this.OncmbCarrierSelectedIndexChanged;
                this.cmbCarrier.OnFirstEnter -= this.OncmbCarrierFirstEnter;
                this.icbServerName.OnFirstEnter -= this.OnicbServerNameFirstEnter;
                this.icbItemType.OnFirstEnter -= this.OnicbItemTypeFirstEnter;
                this.dxErrorProvider1.DataSource = null;
                this.bsDataSource.DataSource = null;
                this.bsDataSource.Dispose();
                this.Saved = null;
               
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }



        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        #endregion

        #region 控制器

        public EDIConfigureController Controller
        {
            get
            {
                return ClientHelper.Get<EDIConfigureController, EDIConfigureController>();
            }
        }

        #endregion

        public EDIConfigureList CurrentData
        {
            get { return bsDataSource.DataSource as EDIConfigureList; }
            set { bsDataSource.DataSource = value; }
        }
        private bool isicbServerNameEntered = false;
        private void OnicbServerNameFirstEnter(object sender, EventArgs e)
        {
            List<ConfigureKeyList> keyList = Controller.GetConfigureKeyListForType(ConfigureType.EDI);
            this.icbServerName.Properties.BeginUpdate();
            this.icbServerName.Properties.Items.Clear();
            icbServerName.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, Guid.Empty));
            foreach (var item in keyList)
            {
                icbServerName.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.ID));
            }
            this.icbServerName.Properties.EndUpdate();
            isicbServerNameEntered = true;
        }
        private bool isicbItemTypeEntered = false;
        private void OnicbItemTypeFirstEnter(object sender, EventArgs e)
        {
            List<EnumHelper.ListItem<EDIMode>> itemType = EnumHelper.GetEnumValues<EDIMode>(LocalData.IsEnglish);
            this.icbItemType.Properties.BeginUpdate();
            this.icbItemType.Properties.Items.Clear();
            icbItemType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, System.DBNull.Value));
            foreach (var mode in itemType)
            {
                icbItemType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(mode.Name, mode.Value));
            }
            this.icbItemType.Properties.EndUpdate();
            isicbItemTypeEntered = true;
        }
        private bool iscmbCarrierEntered = false;
        private void OncmbCarrierFirstEnter(object sender, EventArgs e)
        {
            List<CustomerList> customers = CustomerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty, string.Empty,
                                                                                     string.Empty, string.Empty, null, null, null,
                                                                                     CustomerType.Carrier, null, null, null, null, null, 0);

            CustomerList emptyCustomer = new CustomerList();
            emptyCustomer.CName = emptyCustomer.EName = string.Empty;
            emptyCustomer.ID = Guid.Empty;
            customers.Insert(0, emptyCustomer);
            this.cmbCarrier.Properties.BeginUpdate();
            this.cmbCarrier.Properties.Items.Clear();
            foreach (CustomerList item in customers)
            {
                cmbCarrier.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }
            this.cmbCarrier.Properties.EndUpdate();
            iscmbCarrierEntered = true;
        }
        private void InitControls()
        {
            barSave.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Save_16;
            this.icbServerName.OnFirstEnter += this.OnicbServerNameFirstEnter;
            
           

            List<EnumHelper.ListItem<EDIUploadMode>> uploadModes = EnumHelper.GetEnumValues<EDIUploadMode>(LocalData.IsEnglish);
            this.icbSendMode.Properties.BeginUpdate();
            icbSendMode.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, System.DBNull.Value));
            foreach (var mode in uploadModes)
            {
                icbSendMode.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(mode.Name, mode.Value));
            }
            this.icbSendMode.Properties.EndUpdate();
            this.icbItemType.OnFirstEnter += this.OnicbItemTypeFirstEnter;

            this.cmbCarrier.OnFirstEnter += this.OncmbCarrierFirstEnter;


            cmbCarrier.SelectedIndexChanged += this.OncmbCarrierSelectedIndexChanged;
          
            

            var data = this.bsDataSource.DataSource as EDIConfigureList;
            if (data != null)
            {
                //data.BeginEdit();
                data.IsDirty = false;
                if (data.ReceiverType > 1)
                {
                    checkCarrier.Checked = true;
                    txtCustomer.Tag = data.CarrierID;
                    txtCustomer.Text = data.CarrierName;
                }
                else
                {
                    checkCarrier.Checked = false;
                }
            }
        }
        private void OncmbCarrierSelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentData != null)
                CurrentData.CarrierName = cmbCarrier.Text.Trim();
        }

        #region  Company

        /*保存*/
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EndEdit();
            if (CurrentData == null) return;

            if (CurrentData.Validate() == false)
            {
                return;
            }

            try
            {
                //保存数据
                if (this.SaveData())
                {
                    //触发保存成功事件
                    if (this.Saved != null)
                    {
                        this.Saved(this.bsDataSource.DataSource);
                    }

                    CurrentData.IsDirty = false;
                    //提示保存成功
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                        this.FindForm(),
                        "保存成功!");
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                       this.FindForm(),
                       "保存失败!");
                }
            }
            catch (Exception ex)
            {
                //设置错误信息
                LocalCommonServices.ErrorTrace.SetErrorInfo(
                    this.FindForm(),
                    ex);
            }
        }

        #endregion

        public object[] BeforeParentChanged()
        {
            object[] para = new object[2];
            para[0] = true;
            if (CurrentData == null)
            {
                para[0] = false;
                return para;
            }

            if (CurrentData.IsDirty)
            {
                DialogResult dlg = CommonUtility.EnquireIsSaveCurrentDataByUpdated();
                if (dlg == DialogResult.Yes)
                {
                    if (CurrentData.Validate() == false)
                    {
                        para[0] = false;
                        return para;
                    }

                    RaiseSaved();
                }
                else if (dlg == DialogResult.Cancel)
                {
                    CurrentData.CancelEdit();
                    para[0] = false;
                    return para;
                }
                else if (dlg == DialogResult.No)
                {
                    para[0] = true;
                }
            }
            else if (CurrentData.IsNew)
            {
                DialogResult dlg = CommonUtility.EnquireIsSaveCurrentDataByNew();
                if (dlg == DialogResult.Yes)
                {
                    if (CurrentData.Validate() == false)
                    {
                        para[0] = false;
                        return para;
                    }

                    RaiseSaved();
                }
                else if (dlg == DialogResult.Cancel)
                {
                    para[0] = false;
                }
                else if (dlg == DialogResult.No)
                {
                    para[0] = true;
                    para[1] = true;
                }
            }

            return para;
        }

        #region IEditPart接口

        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return this.bsDataSource.DataSource;
            }
            set
            {
                SetLazyBindingControlValues(value as EDIConfigureList);
                this.bsDataSource.DataSource = value;
                this.CurrentData.IsDirty = false;
                //BindingData(value);
            }
        }

        /// <summary>
        /// 保存完成触发该事件
        /// </summary>
        public override event SavedHandler Saved;

        /// <summary>
        /// 结束编辑
        /// </summary>
        public override void EndEdit()
        {
            this.Validate();
            bsDataSource.EndEdit();
        }

        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            if (values != null
                && values.ContainsKey("EDIConfigureList"))
            {
                EDIConfigureList currentEDIConfigure = (EDIConfigureList)values["EDIConfigureList"];
                if (currentEDIConfigure == null)
                {
                    currentEDIConfigure = new EDIConfigureList();
                    this.Enabled = false;
                 
                }
                else
                {
                    if (!currentEDIConfigure.IsValid)
                    {
                        this.Enabled = false;
                    }
                    else
                    {
                        this.Enabled = true;
                    }
                }
                EDIConfigureList _currentEditConfigure=new EDIConfigureList();
                ObjectHelper.CopyData(currentEDIConfigure, _currentEditConfigure);
                SetLazyBindingControlValues(_currentEditConfigure);
                this.bsDataSource.DataSource = _currentEditConfigure;
                this.CurrentData.IsDirty = false;
                (this.bsDataSource.DataSource as EDIConfigureList).BeginEdit();
                if (currentEDIConfigure.ReceiverType > 1)
                {
                    checkCarrier.Checked = true;
                    txtCustomer.Tag = currentEDIConfigure.CarrierID;
                    txtCustomer.Text = currentEDIConfigure.CarrierName;
                }
                else
                {
                    checkCarrier.Checked = false;
                }
            }
        }
        private void SetLazyBindingControlValues(EDIConfigureList ediConfigure)
        { 
         if(!iscmbCarrierEntered && !CommonUtility.GuidIsNullOrEmpty(ediConfigure.CarrierID))
         {
          this.cmbCarrier.ShowSelectedValue(ediConfigure.CarrierID,ediConfigure.CarrierName);
         }
         if (!isicbItemTypeEntered && (int)ediConfigure.EDIMode!=0)
         {   
             this.icbItemType.ShowSelectedValue(ediConfigure.EDIMode,ediConfigure.EDIMode.ToString());
         }
         if (!isicbServerNameEntered)
         {
             this.icbServerName.ShowSelectedValue(ediConfigure.ServiceConfigureKeyID, ediConfigure.ServiceConfigureKeyName);
         }
        }

        /// <summary>
        /// 触发保存事件
        /// </summary>
        public override void RaiseSaved()
        {
            this.SaveData();

            if (this.Saved != null)
            {
                this.Saved(this.DataSource);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            //获取当前对象
            var currentEDIConfigure = bsDataSource.DataSource as EDIConfigureList;
            if (currentEDIConfigure != null)
            {
                Guid?[] ids = new Guid?[1];
                string[] Components = new string[1];
                string[] FTPs = new string[1];
                string[] FileFormats = new string[1];
                string[] DataFormats = new string[1];
                string[] RegularFiles = new string[1];
                string[] StoredProcedures = new string[1];
                Guid[] configureKeyIDs = new Guid[1];
                Guid?[] carrierIDs = new Guid?[1];
                EDIUploadMode[] modes = new EDIUploadMode[1];
                EDIMode[] itemType = new EDIMode[1];
                string[] serverAddresses = new string[1];
                string[] userNames = new string[1];
                string[] passwords = new string[1];
                string[] receiveAddresses = new string[1];
                DateTime?[] updateDates = new DateTime?[1];

                ids[0] = currentEDIConfigure.ID;
                //Codes[0]=currentEDIConfigure.Code;
                Components[0] = currentEDIConfigure.Component;
                FTPs[0] = currentEDIConfigure.FTP;
                FileFormats[0] = currentEDIConfigure.FileFormat;
                DataFormats[0] = currentEDIConfigure.DataFormat;
                RegularFiles[0] = currentEDIConfigure.RegularFile;
                StoredProcedures[0] = currentEDIConfigure.StoredProcedure;
                configureKeyIDs[0] = currentEDIConfigure.ServiceConfigureKeyID;
                if (checkCarrier.Checked)
                {
                    carrierIDs[0] = txtCustomer.Tag == null ? Guid.Empty : new Guid(txtCustomer.Tag.ToString());
                }
                else
                {
                    carrierIDs[0] = currentEDIConfigure.CarrierID;
                }
                modes[0] = currentEDIConfigure.UploadMode;
                itemType[0] = currentEDIConfigure.EDIMode;
                serverAddresses[0] = currentEDIConfigure.ServerAddress;
                userNames[0] = currentEDIConfigure.UserName;
                passwords[0] = currentEDIConfigure.Password;
                //if (string.IsNullOrEmpty(currentEDIConfigure.ReceiveAddress))
                //{
                receiveAddresses[0] = currentEDIConfigure.ReceiveAddress;
                //}
                updateDates[0] = currentEDIConfigure.UpdateDate;

                ManyResultData mans = this.Controller.SaveEDIConfigureInfo(
               ids,
                    //Codes,
               Components,
               FTPs,
               FileFormats,
               DataFormats,
               RegularFiles,
               StoredProcedures,
               configureKeyIDs,
               carrierIDs,
               modes,
               itemType,
               serverAddresses,
               userNames,
               passwords,
               receiveAddresses,
               LocalData.UserInfo.LoginID,
               checkCarrier.Checked?(byte)2:(byte)1,
               updateDates);

                if (mans == null || mans.ChildResults == null || mans.ChildResults.Count == 0)
                {
                    return false;
                }
                else
                {
                    int i = 0;
                    foreach (EDIConfigureList cid in bsDataSource.List)
                    {
                        cid.ID = mans.ChildResults[i].ID;
                        cid.UpdateDate = mans.ChildResults[i].UpdateDate;
                        i++;
                    }
                }
            }

            currentEDIConfigure.EndEdit();

            return true;
        }

        #endregion

        private void checkCarrier_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCarrier.Checked)
            {
                txtCustomer.Visible = true;
            }
            else
            {
                txtCustomer.Visible = false;
            }
        }

        private void txtCustomer_EditValueChanged(object sender, EventArgs e)
        {

        }

        //#region 本地方法    

        //void SearchRegister()
        //{
        //    dfService.Register(btnCarrierName, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
        //                      delegate(object inputSource, object[] resultData)
        //                      {
        //                          btnCarrierName.Text = CurrentData.CarrierName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
        //                          btnCarrierName.Tag = CurrentData.CarrierID = new Guid(resultData[0].ToString());
        //                      },
        //                      delegate()
        //                      {
        //                          btnCarrierName.Text = CurrentData.CarrierName = string.Empty;
        //                          btnCarrierName.Tag = CurrentData.CarrierID = Guid.Empty;
        //                      },
        //                      ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
        //}    

        //#endregion
    }
}
