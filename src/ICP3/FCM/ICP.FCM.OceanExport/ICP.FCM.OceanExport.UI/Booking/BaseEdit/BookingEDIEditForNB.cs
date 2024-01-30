using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.EDI.ServiceInterface;
using ICP.EDI.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanExport.UI.Booking.BaseEdit
{
    [ToolboxItem(false)]
    [SmartPart]
    public partial class BookingEDIEditForNB : BaseEditPart
    {
        public NBEDIANLBOOKINGObj _BookingInfo = null;
        /// <summary>
        /// 缓存国家列表,只获取一次.现只用于客户弹出式描述框
        /// </summary>
        List<CountryList> _countryList = null;
        CustomerFinderBridge shipperBridge;
        CustomerFinderBridge consigneeBridge;
        CustomerFinderBridge notifyPartyBridge;
        LocationFinderBridge pfbPlaceOfReceipt;
        Guid orgVa = Guid.Empty;
        bool show = true;
        string ContainerDescription;

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        public IEDIClientService EDIClientService
        {
            get
            {
                return ServiceClient.GetClientService<IEDIClientService>();
            }
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }


        public BookingEDIEditForNB()
        {
            InitializeComponent();
        }

        public override object DataSource
        {
            get
            {
                return bindingSource1.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        public void BindingData(object data)
        {
            SuspendLayout();
            _BookingInfo = data as NBEDIANLBOOKINGObj;

            InitControls();
            SetLazyLoaders();
            if (!string.IsNullOrEmpty(_BookingInfo.No))
            {
                string path = @"E:\EDILOG\" + _BookingInfo.No + ".XML";
                if (File.Exists(path))
                {
                    ReadXml(path);
                }
            }
            BindSource();
        }

        private void ReadXml(string path)
        {
            if (_BookingInfo == null)
                _BookingInfo = new NBEDIANLBOOKINGObj();
            if (_BookingInfo.ShipperDescription == null)
                _BookingInfo.ShipperDescription = new CustomerDescription();
            if (_BookingInfo.ConsigneeDescription == null)
                _BookingInfo.ConsigneeDescription = new CustomerDescription();
            if (_BookingInfo.NotifyPartydescription == null)
                _BookingInfo.NotifyPartydescription = new CustomerDescription();

            //1、创建XmlDocument对象
            XmlDocument xmlDoc = new XmlDocument();
            //2、加载源文件

            xmlDoc.Load(path);
            //3、获取根结点

            XmlElement xmlRoot = xmlDoc.DocumentElement;
            //4、获取根结点下的子节点

            foreach (XmlNode node in xmlRoot.ChildNodes)
            {
                switch (node.Name)
                {
                    case "ShipperID":
                        if (string.IsNullOrEmpty(node.InnerText))
                        {
                            _BookingInfo.ShipperID = null;
                        }
                        else
                        {
                            _BookingInfo.ShipperID = new Guid(node.InnerText);
                        }
                        break;
                    case "ShipperName":
                        _BookingInfo.ShipperName = node.InnerText;
                        break;
                    case "ShipperDescriptionName":
                        _BookingInfo.ShipperDescription.Name = node.InnerText;
                        break;
                    case "ShipperDescriptionAddress":
                        _BookingInfo.ShipperDescription.Address = node.InnerText;
                        break;
                    case "ShipperDescriptionTel":
                        _BookingInfo.ShipperDescription.Tel = node.InnerText;
                        break;
                    case "ShipperDescriptionFax":
                        _BookingInfo.ShipperDescription.Fax = node.InnerText;
                        break;
                    case "ConsigneeID":
                        if (string.IsNullOrEmpty(node.InnerText))
                        {
                            _BookingInfo.ConsigneeID = null;
                        }
                        else
                        {
                            _BookingInfo.ConsigneeID = new Guid(node.InnerText);
                        }
                        break;
                    case "ConsigneeName":
                        _BookingInfo.ConsigneeName = node.InnerText;
                        break;
                    case "ConsigneeDescriptionName":
                        _BookingInfo.ConsigneeDescription.Name = node.InnerText;
                        break;
                    case "ConsigneeDescriptionAddress":
                        _BookingInfo.ConsigneeDescription.Address = node.InnerText;
                        break;
                    case "ConsigneeDescriptionTel":
                        _BookingInfo.ConsigneeDescription.Tel = node.InnerText;
                        break;
                    case "ConsigneeDescriptionFax":
                        _BookingInfo.ConsigneeDescription.Fax = node.InnerText;
                        break;
                    case "NotifyPartyID":
                        if (string.IsNullOrEmpty(node.InnerText))
                        {
                            _BookingInfo.NotifyPartyID = null;
                        }
                        else
                        {
                            _BookingInfo.NotifyPartyID = new Guid(node.InnerText);
                        }
                        break;
                    case "NotifyPartyname":
                        _BookingInfo.NotifyPartyname = node.InnerText;
                        break;
                    case "NotifyPartydescriptionName":
                        _BookingInfo.NotifyPartydescription.Name = node.InnerText;
                        break;
                    case "NotifyPartydescriptionAddress":
                        _BookingInfo.NotifyPartydescription.Address = node.InnerText;
                        break;
                    case "NotifyPartydescriptionTel":
                        _BookingInfo.NotifyPartydescription.Tel = node.InnerText;
                        break;
                    case "NotifyPartydescriptionFax":
                        _BookingInfo.NotifyPartydescription.Fax = node.InnerText;
                        break;
                    case "BookNo":
                        _BookingInfo.BookNo = node.InnerText;
                        break;
                    case "VoyageID":
                        if (string.IsNullOrEmpty(node.InnerText))
                        {
                            _BookingInfo.VoyageID = null;
                        }
                        else
                        {
                            _BookingInfo.VoyageID = new Guid(node.InnerText);
                        }
                        break;
                    case "POLID":
                        if (!string.IsNullOrEmpty(node.InnerText))
                        {
                            _BookingInfo.POLID = new Guid(node.InnerText);
                        }
                        break;
                    case "POLName":
                        _BookingInfo.POLName = node.InnerText;
                        break;
                    case "PODID":
                        if (!string.IsNullOrEmpty(node.InnerText))
                        {
                            _BookingInfo.PODID = new Guid(node.InnerText);
                        }
                        break;
                    case "PODName":
                        _BookingInfo.PODName = node.InnerText;
                        break;
                    case "PlaceOfDeliveryID":
                        if (!string.IsNullOrEmpty(node.InnerText))
                        {
                            _BookingInfo.PlaceOfDeliveryID = new Guid(node.InnerText);
                        }
                        break;
                    case "PlaceOfDeliveryName":
                        _BookingInfo.PlaceOfDeliveryName = node.InnerText;
                        break;
                    case "PlaceOfReceiptID":
                        if (!string.IsNullOrEmpty(node.InnerText))
                        {
                            _BookingInfo.PlaceOfReceiptID = new Guid(node.InnerText);
                        }
                        break;
                    case "PlaceOfReceiptName":
                        _BookingInfo.PlaceOfReceiptName = node.InnerText;
                        break;
                    case "TransportClauseID":
                        if (!string.IsNullOrEmpty(node.InnerText))
                        {
                            _BookingInfo.TransportClauseID = new Guid(node.InnerText);
                        }
                        break;
                    case "TransportClauseName":
                        _BookingInfo.TransportClauseName = node.InnerText;
                        break;
                    case "PaymentTermID":
                        if (!string.IsNullOrEmpty(node.InnerText))
                        {
                            _BookingInfo.PaymentTermID = new Guid(node.InnerText);
                        }
                        break;
                    case "PaymentTermName":
                        _BookingInfo.PaymentTermName = node.InnerText;
                        break;
                    case "Container":
                        _BookingInfo.Container = node.InnerText;
                        break;
                    case "Marks":
                        _BookingInfo.Marks = node.InnerText;
                        break;
                    case "SealNo":
                        _BookingInfo.SealNo = node.InnerText;
                        break;
                    case "Commodity":
                        _BookingInfo.Commodity = node.InnerText;
                        break;
                    case "SQNO":
                        _BookingInfo.SQNO = node.InnerText;
                        break;
                    case "Quantity":
                        _BookingInfo.Quantity = Convert.ToInt32(node.InnerText);
                        break;
                    case "Weight":
                        _BookingInfo.Weight = Convert.ToDecimal(node.InnerText);
                        break;
                    case "Measurement":
                        _BookingInfo.Measurement = Convert.ToDecimal(node.InnerText);
                        break;
                    case "CagoType":
                        _BookingInfo.CagoType = Convert.ToInt32(node.InnerText);
                        break;
                    case "CentigradeF":
                        _BookingInfo.CentigradeF = node.InnerText;
                        break;
                    case "Centigrade":
                        _BookingInfo.Centigrade = node.InnerText;
                        break;
                    case "DangerousClass":
                        _BookingInfo.DangerousClass = node.InnerText;
                        break;
                    case "DangerousNo":
                        _BookingInfo.DangerousNo = node.InnerText;
                        break;
                    case "DangerousPage":
                        _BookingInfo.DangerousPage = node.InnerText;
                        break;
                    case "DangerousProperty":
                        _BookingInfo.DangerousProperty = node.InnerText;
                        break;
                    case "ContainerDescription":
                        ContainerDescription = node.InnerText;
                        break;
                    case "carriage":
                        stxtcarriage.Text = node.InnerText;
                        break;
                }
            }
        }

        private void BookingEDIEditForNB_Load(object sender, EventArgs e)
        {
            //InitControls();
            //SetLazyLoaders();
            //BindSource();
            this.stxtVoyage.EditValueChanged += new System.EventHandler(this.stxtVoyage_EditValueChanged);
            this.Disposed += delegate
            {
                this.stxtVoyage.EditValueChanged -= new System.EventHandler(this.stxtVoyage_EditValueChanged);
            };
        }

        private void InitControls()
        {
            //shipper
            OEUtility.SetEnterToExecuteOnec(stxtShipper, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                shipperBridge = new CustomerFinderBridge(
                stxtShipper,
                _countryList,
                DataFindClientService,
                CustomerService,
                _BookingInfo.ShipperDescription,
                ICPCommUIHelper,
                LocalData.IsEnglish);
                shipperBridge.Init();
            });
            stxtShipper.OnOk += stxtShipper_OnOk;

            //Consignee
            OEUtility.SetEnterToExecuteOnec(stxtConsignee, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                consigneeBridge = new CustomerFinderBridge(
                stxtConsignee,
                _countryList,
                DataFindClientService,
                CustomerService,
                _BookingInfo.ConsigneeDescription,
                ICPCommUIHelper,
                LocalData.IsEnglish);
                consigneeBridge.Init();
            });
            stxtConsignee.OnOk += stxtConsignee_OnOk;

            OEUtility.SetEnterToExecuteOnec(stxtNotifyParty, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                notifyPartyBridge = new CustomerFinderBridge(
                stxtNotifyParty,
                _countryList,
                DataFindClientService,
                CustomerService,
                _BookingInfo.NotifyPartydescription,
                ICPCommUIHelper,
                LocalData.IsEnglish);
                notifyPartyBridge.Init();
            });
            stxtNotifyParty.OnOk += stxtNotifyParty_OnOk;
        }

        void SetLazyLoaders()
        {
            //运输条款
            OEUtility.SetEnterToExecuteOnec(cmbTransportClause, delegate
            {
                List<TransportClauseList> transportClauseList = ICPCommUIHelper.SetCmbTransportClause(cmbTransportClause);
            });

            OEUtility.SetEnterToExecuteOnec(cmbPaymentTerm, delegate
            {
                List<DataDictionaryList> payments = ICPCommUIHelper.SetCmbDataDictionary(
                                                    cmbPaymentTerm,
                                                    DataDictionaryType.PaymentTerm, DataBindType.EName, true);
            });
        }

        private void BindSource()
        {
            bindingSource1.DataSource = _BookingInfo;
            bindingSource1.ResetBindings(false);
            if (!string.IsNullOrEmpty(ContainerDescription))
            {
                textBox1.Text = ContainerDescription;
            }
            cmbPaymentTerm.ShowSelectedValue(_BookingInfo.PaymentTermID, _BookingInfo.PaymentTermName);
            cmbTransportClause.ShowSelectedValue(_BookingInfo.TransportClauseID, _BookingInfo.TransportClauseName);
        }

        void stxtShipper_OnOk(object sender, EventArgs e)
        {
            if (stxtShipper.CustomerDescription != null && _BookingInfo != null)
            {
                _BookingInfo.ShipperDescription = stxtShipper.CustomerDescription;
            }
        }

        void stxtConsignee_OnOk(object sender, EventArgs e)
        {
            if (stxtConsignee.CustomerDescription != null && _BookingInfo != null)
            {
                _BookingInfo.ConsigneeDescription = stxtConsignee.CustomerDescription;
            }
        }

        void stxtNotifyParty_OnOk(object sender, EventArgs e)
        {
            if (stxtNotifyParty.CustomerDescription != null && _BookingInfo != null)
            {
                _BookingInfo.NotifyPartydescription = stxtNotifyParty.CustomerDescription;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            show = false;
            btSave_Click(sender, e);
            show = true;
            bindingSource1.EndEdit();
            string errMess = ValidateData();
            if (!string.IsNullOrEmpty(errMess))
            {
                MessageBox.Show(errMess, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NBEDIANLBOOKINGObj newobj = bindingSource1.DataSource as NBEDIANLBOOKINGObj;
            newobj.CagoType = rdoTyoe.SelectedIndex;
            newobj.PaymentTermName = cmbPaymentTerm.Text;
            newobj.TransportClauseName = cmbTransportClause.Text;
            if (newobj.ID == null || newobj.ID == Guid.Empty)
                newobj.ID = Guid.NewGuid();
            newobj.VoyageName = stxtVoyage.EditText;

            List<Guid> ids = new List<Guid>(1);
            ids.Add(newobj.ID);
            List<string> nos = new List<string>(1);
            nos.Add(newobj.No == null ? newobj.BookNo : newobj.No);

            EDISendOption sendItem = new EDISendOption();
            sendItem.ServiceKey = "NBEDIBookingANL";
            sendItem.EdiMode = EDIMode.Booking;
            sendItem.CompanyID = newobj.CompanyID == Guid.Empty ? LocalData.UserInfo.DefaultCompanyID : newobj.CompanyID;
            sendItem.Subject = "电子订舱(";
            sendItem.Subject += newobj.No == null ? newobj.BookNo : newobj.No;
            sendItem.Subject += ")";
            sendItem.IDs = ids.ToArray();
            sendItem.FIDs = ids.ToArray();
            sendItem.NOs = nos.ToArray();
            sendItem.OperationType = OperationType.OceanExport;

            bool isSucc = EDIClientService.SendEDI(sendItem);
            if (isSucc)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                this.FindForm().Close();
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private string ValidateData()
        {
            NBEDIANLBOOKINGObj newobj = bindingSource1.DataSource as NBEDIANLBOOKINGObj;
            string errorMessage = string.Empty;

            if (string.IsNullOrEmpty(newobj.BookNo))
            {
                errorMessage = "请正确填写BOOKNO！";
                txtBookNo.Focus();
            }
            else if (string.IsNullOrEmpty(newobj.POLName))
            {
                errorMessage = "请填写装货港！";
                stxtPOL.Focus();
            }
            else if (string.IsNullOrEmpty(newobj.PODName))
            {
                errorMessage = "请填写卸货港！";
                stxtPOD.Focus();
            }
            else if (string.IsNullOrEmpty(newobj.PlaceOfReceiptName))
            {
                errorMessage = "请填写收货地址！";
                stxtReceipt.Focus();
            }
            else if (string.IsNullOrEmpty(newobj.PlaceOfDeliveryName))
            {
                errorMessage = "请填写交货地址！";
                stxtDelivery.Focus();
            }
            else if (string.IsNullOrEmpty(newobj.Container))
            {
                if (string.IsNullOrEmpty(containerDemandControl1.Text))
                {
                    errorMessage = "请选择择箱型！";
                    containerDemandControl1.Focus();
                }
                else
                    newobj.Container = containerDemandControl1.Text;

            }
            else if (string.IsNullOrEmpty(newobj.SQNO))
            {
                errorMessage = "请填填写约号！";
                txtSQNO.Focus();
            }
            else if (newobj.Quantity <= 0)
            {
                errorMessage = "请填写正确件数！";
                numQuantity.Focus();
            }
            else if (newobj.Weight <= 0)
            {
                errorMessage = "请填写正确毛重！";
                numWeight.Focus();
            }
            else if (newobj.Measurement <= 0)
            {
                errorMessage = "请填写正确体积！";
                numMeasurement.Focus();
            }

            return errorMessage;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            bindingSource1.EndEdit();
            NBEDIANLBOOKINGObj newobj = bindingSource1.DataSource as NBEDIANLBOOKINGObj;
            XmlDocument xmlDoc = CreateXml(newobj);
            string xpath = @"E:\EDILOG\";
            if (!System.IO.Directory.Exists(xpath))
            {
                xpath = System.Windows.Forms.Application.StartupPath + @"EDILOG\";
            }
            if (!System.IO.Directory.Exists(xpath))
            {
                System.IO.Directory.CreateDirectory(xpath);
            }
            string fullpath = xpath + newobj.No + ".XML";
            xmlDoc.Save(fullpath);
            if (show)
            {
                XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void btTemplate_Click(object sender, EventArgs e)
        {
            bindingSource1.EndEdit();
            NBEDIANLBOOKINGObj newobj = bindingSource1.DataSource as NBEDIANLBOOKINGObj;
            XmlDocument xmlDoc = CreateXml(newobj);
            string xpath = @"E:\EDILOG\Template\";
            if (!System.IO.Directory.Exists(xpath))
            {
                xpath = System.Windows.Forms.Application.StartupPath + @"EDILOG\Template\";
            }
            if (!System.IO.Directory.Exists(xpath))
            {
                System.IO.Directory.CreateDirectory(xpath);
            }

           
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            string fullpath = xpath + newobj.PODName + @"--" + newobj.PlaceOfDeliveryName + ".XML";
            xmlDoc.Save(fullpath);
            if (show)
            {
                XtraMessageBox.Show(newobj.PODName + @"--" + newobj.PlaceOfDeliveryName + "  模板保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML文件(*.xml)|*.xml";
            string xpath = @"E:\EDILOG\Template\";
            if (!System.IO.Directory.Exists(xpath))
            {
                ofd.InitialDirectory = @"E:\EDILOG\Template\";
            }
            else
            {
                ofd.InitialDirectory = System.Windows.Forms.Application.StartupPath + @"EDILOG\Template\";
            }
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ReadXml(ofd.FileName);
            }
            ofd.Dispose();
            BindSource();
        }

        private XmlDocument CreateXml(NBEDIANLBOOKINGObj newobj)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建第一级节点
            XmlNode EDI = xmlDoc.CreateElement("EDI");
            xmlDoc.AppendChild(EDI);
            //创建ShipperID节点
            XmlNode ShipperID = xmlDoc.CreateNode(XmlNodeType.Element, "ShipperID", null);
            ShipperID.InnerText = newobj.ShipperID.ToString();
            EDI.AppendChild(ShipperID);
            //创建ShipperName节点
            XmlNode ShipperName = xmlDoc.CreateNode(XmlNodeType.Element, "ShipperName", null);
            ShipperName.InnerText = newobj.ShipperName;
            EDI.AppendChild(ShipperName);
            //创建ShipperDescription.Name节点
            XmlNode ShipperDescriptionName = xmlDoc.CreateNode(XmlNodeType.Element, "ShipperDescriptionName", null);
            ShipperDescriptionName.InnerText = newobj.ShipperDescription.Name;
            EDI.AppendChild(ShipperDescriptionName);
            //创建ShipperDescriptionAddress节点
            XmlNode ShipperDescriptionAddress = xmlDoc.CreateNode(XmlNodeType.Element, "ShipperDescriptionAddress", null);
            ShipperDescriptionAddress.InnerText = newobj.ShipperDescription.Address;
            EDI.AppendChild(ShipperDescriptionAddress);
            //创建ShipperDescriptionTel节点
            XmlNode ShipperDescriptionTel = xmlDoc.CreateNode(XmlNodeType.Element, "ShipperDescriptionTel", null);
            ShipperDescriptionTel.InnerText = newobj.ShipperDescription.Tel;
            EDI.AppendChild(ShipperDescriptionTel);
            //创建ShipperDescriptionFax节点
            XmlNode ShipperDescriptionFax = xmlDoc.CreateNode(XmlNodeType.Element, "ShipperDescriptionFax", null);
            ShipperDescriptionFax.InnerText = newobj.ShipperDescription.Fax;
            EDI.AppendChild(ShipperDescriptionFax);
            //创建ConsigneeID节点
            XmlNode ConsigneeID = xmlDoc.CreateNode(XmlNodeType.Element, "ConsigneeID", null);
            ConsigneeID.InnerText = newobj.ConsigneeID.ToString();
            EDI.AppendChild(ConsigneeID);
            //创建ConsigneeName节点
            XmlNode ConsigneeName = xmlDoc.CreateNode(XmlNodeType.Element, "ConsigneeName", null);
            ConsigneeName.InnerText = newobj.ConsigneeName;
            EDI.AppendChild(ConsigneeName);
            //创建ConsigneeDescriptionName.Name节点
            XmlNode ConsigneeDescriptionName = xmlDoc.CreateNode(XmlNodeType.Element, "ConsigneeDescriptionName", null);
            ConsigneeDescriptionName.InnerText = newobj.ConsigneeDescription.Name;
            EDI.AppendChild(ConsigneeDescriptionName);
            //创建ConsigneeDescriptionAddress节点
            XmlNode ConsigneeDescriptionAddress = xmlDoc.CreateNode(XmlNodeType.Element, "ConsigneeDescriptionAddress", null);
            ConsigneeDescriptionAddress.InnerText = newobj.ConsigneeDescription.Address;
            EDI.AppendChild(ConsigneeDescriptionAddress);
            //创建ConsigneeDescriptionTel节点
            XmlNode ConsigneeDescriptionTel = xmlDoc.CreateNode(XmlNodeType.Element, "ConsigneeDescriptionTel", null);
            ConsigneeDescriptionTel.InnerText = newobj.ConsigneeDescription.Tel;
            EDI.AppendChild(ConsigneeDescriptionTel);
            //创建ShipperDescriptionFax节点
            XmlNode ConsigneeDescriptionFax = xmlDoc.CreateNode(XmlNodeType.Element, "ConsigneeDescriptionFax", null);
            ConsigneeDescriptionFax.InnerText = newobj.ConsigneeDescription.Fax;
            EDI.AppendChild(ConsigneeDescriptionFax);
            //创建ConsigneeID节点
            XmlNode NotifyPartyID = xmlDoc.CreateNode(XmlNodeType.Element, "NotifyPartyID", null);
            NotifyPartyID.InnerText = newobj.NotifyPartyID.ToString();
            EDI.AppendChild(NotifyPartyID);
            //创建NotifyPartyname节点
            XmlNode NotifyPartyname = xmlDoc.CreateNode(XmlNodeType.Element, "NotifyPartyname", null);
            NotifyPartyname.InnerText = newobj.NotifyPartyname;
            EDI.AppendChild(NotifyPartyname);
            //创建NotifyPartydescriptionName.Name节点
            XmlNode NotifyPartydescriptionName = xmlDoc.CreateNode(XmlNodeType.Element, "NotifyPartydescriptionName", null);
            NotifyPartydescriptionName.InnerText = newobj.NotifyPartydescription.Name;
            EDI.AppendChild(NotifyPartydescriptionName);
            //创建NotifyPartydescriptionAddress节点
            XmlNode NotifyPartydescriptionAddress = xmlDoc.CreateNode(XmlNodeType.Element, "NotifyPartydescriptionAddress", null);
            NotifyPartydescriptionAddress.InnerText = newobj.NotifyPartydescription.Address;
            EDI.AppendChild(NotifyPartydescriptionAddress);
            //创建NotifyPartydescriptionTel节点
            XmlNode NotifyPartydescriptionTel = xmlDoc.CreateNode(XmlNodeType.Element, "NotifyPartydescriptionTel", null);
            NotifyPartydescriptionTel.InnerText = newobj.NotifyPartydescription.Tel;
            EDI.AppendChild(NotifyPartydescriptionTel);
            //创建NotifyPartydescriptionFax节点
            XmlNode NotifyPartydescriptionFax = xmlDoc.CreateNode(XmlNodeType.Element, "NotifyPartydescriptionFax", null);
            NotifyPartydescriptionFax.InnerText = newobj.NotifyPartydescription.Fax;
            EDI.AppendChild(NotifyPartydescriptionFax);
            //创建BookNo节点
            XmlNode BookNo = xmlDoc.CreateNode(XmlNodeType.Element, "BookNo", null);
            BookNo.InnerText = newobj.BookNo;
            EDI.AppendChild(BookNo);
            //创建VoyageID节点
            XmlNode VoyageID = xmlDoc.CreateNode(XmlNodeType.Element, "VoyageID", null);
            VoyageID.InnerText = newobj.VoyageID.ToString();
            EDI.AppendChild(VoyageID);
            //创建POLID节点
            XmlNode POLID = xmlDoc.CreateNode(XmlNodeType.Element, "POLID", null);
            POLID.InnerText = newobj.POLID.ToString();
            EDI.AppendChild(POLID);
            //创建POLName节点
            XmlNode POLName = xmlDoc.CreateNode(XmlNodeType.Element, "POLName", null);
            POLName.InnerText = newobj.POLName.ToString();
            EDI.AppendChild(POLName);
            //创建PODID节点
            XmlNode PODID = xmlDoc.CreateNode(XmlNodeType.Element, "PODID", null);
            PODID.InnerText = newobj.PODID.ToString();
            EDI.AppendChild(PODID);
            //创建PODName节点
            XmlNode PODName = xmlDoc.CreateNode(XmlNodeType.Element, "PODName", null);
            PODName.InnerText = newobj.PODName.ToString();
            EDI.AppendChild(PODName);
            //创建PlaceOfDeliveryID节点
            XmlNode PlaceOfDeliveryID = xmlDoc.CreateNode(XmlNodeType.Element, "PlaceOfDeliveryID", null);
            PlaceOfDeliveryID.InnerText = newobj.PlaceOfDeliveryID.ToString();
            EDI.AppendChild(PlaceOfDeliveryID);
            //创建PlaceOfDeliveryName节点
            XmlNode PlaceOfDeliveryName = xmlDoc.CreateNode(XmlNodeType.Element, "PlaceOfDeliveryName", null);
            PlaceOfDeliveryName.InnerText = newobj.PlaceOfDeliveryName;
            EDI.AppendChild(PlaceOfDeliveryName);
            //创建PlaceOfReceiptID节点
            XmlNode PlaceOfReceiptID = xmlDoc.CreateNode(XmlNodeType.Element, "PlaceOfReceiptID", null);
            PlaceOfReceiptID.InnerText = newobj.PlaceOfReceiptID.ToString();
            EDI.AppendChild(PlaceOfReceiptID);
            //创建PlaceOfReceiptName节点
            XmlNode PlaceOfReceiptName = xmlDoc.CreateNode(XmlNodeType.Element, "PlaceOfReceiptName", null);
            PlaceOfReceiptName.InnerText = newobj.PlaceOfReceiptName;
            EDI.AppendChild(PlaceOfReceiptName);
            //创建TransportClauseID节点
            XmlNode TransportClauseID = xmlDoc.CreateNode(XmlNodeType.Element, "TransportClauseID", null);
            TransportClauseID.InnerText = newobj.TransportClauseID.ToString();
            EDI.AppendChild(TransportClauseID);
            //创建TransportClauseName节点
            XmlNode TransportClauseName = xmlDoc.CreateNode(XmlNodeType.Element, "TransportClauseName", null);
            TransportClauseName.InnerText = newobj.TransportClauseName.ToString();
            EDI.AppendChild(TransportClauseName);
            //创建PaymentTermID节点
            XmlNode PaymentTermID = xmlDoc.CreateNode(XmlNodeType.Element, "PaymentTermID", null);
            PaymentTermID.InnerText = newobj.PaymentTermID.ToString();
            EDI.AppendChild(PaymentTermID);
            //创建PaymentTermName节点
            XmlNode PaymentTermName = xmlDoc.CreateNode(XmlNodeType.Element, "PaymentTermName", null);
            PaymentTermName.InnerText = newobj.PaymentTermName;
            EDI.AppendChild(PaymentTermName);
            //创建Container节点
            XmlNode Container = xmlDoc.CreateNode(XmlNodeType.Element, "Container", null);
            Container.InnerText = newobj.Container;
            EDI.AppendChild(Container);
            //创建Marks节点
            XmlNode Marks = xmlDoc.CreateNode(XmlNodeType.Element, "Marks", null);
            Marks.InnerText = newobj.Marks;
            EDI.AppendChild(Marks);
            //创建ContainerDescription节点
            XmlNode ContainerDescription = xmlDoc.CreateNode(XmlNodeType.Element, "ContainerDescription", null);
            ContainerDescription.InnerText = textBox1.Text;
            EDI.AppendChild(ContainerDescription);
            //创建SealNo节点
            XmlNode SealNo = xmlDoc.CreateNode(XmlNodeType.Element, "SealNo", null);
            SealNo.InnerText = newobj.SealNo;
            EDI.AppendChild(SealNo);
            //创建Commodity节点
            XmlNode Commodity = xmlDoc.CreateNode(XmlNodeType.Element, "Commodity", null);
            Commodity.InnerText = newobj.Commodity;
            EDI.AppendChild(Commodity);
            //创建SQNO节点
            XmlNode SQNO = xmlDoc.CreateNode(XmlNodeType.Element, "SQNO", null);
            SQNO.InnerText = newobj.SQNO;
            EDI.AppendChild(SQNO);
            //创建Quantity节点
            XmlNode Quantity = xmlDoc.CreateNode(XmlNodeType.Element, "Quantity", null);
            Quantity.InnerText = newobj.Quantity.ToString();
            EDI.AppendChild(Quantity);
            //创建Weight节点
            XmlNode Weight = xmlDoc.CreateNode(XmlNodeType.Element, "Weight", null);
            Weight.InnerText = newobj.Weight.ToString("F3");
            EDI.AppendChild(Weight);
            //创建Measurement节点
            XmlNode Measurement = xmlDoc.CreateNode(XmlNodeType.Element, "Measurement", null);
            Measurement.InnerText = newobj.Measurement.ToString("F3");
            EDI.AppendChild(Measurement);
            //创建CagoType节点
            XmlNode CagoType = xmlDoc.CreateNode(XmlNodeType.Element, "CagoType", null);
            CagoType.InnerText = newobj.CagoType.ToString();
            EDI.AppendChild(CagoType);
            //创建CentigradeF节点
            XmlNode CentigradeF = xmlDoc.CreateNode(XmlNodeType.Element, "CentigradeF", null);
            CentigradeF.InnerText = newobj.CentigradeF;
            EDI.AppendChild(CentigradeF);
            //创建Centigrade节点
            XmlNode Centigrade = xmlDoc.CreateNode(XmlNodeType.Element, "Centigrade", null);
            Centigrade.InnerText = newobj.Centigrade;
            EDI.AppendChild(Centigrade);
            //创建DangerousClass节点
            XmlNode DangerousClass = xmlDoc.CreateNode(XmlNodeType.Element, "DangerousClass", null);
            DangerousClass.InnerText = newobj.DangerousClass;
            EDI.AppendChild(DangerousClass);
            //创建DangerousNo节点
            XmlNode DangerousNo = xmlDoc.CreateNode(XmlNodeType.Element, "DangerousNo", null);
            DangerousNo.InnerText = newobj.DangerousNo;
            EDI.AppendChild(DangerousNo);
            //创建DangerousPage节点
            XmlNode DangerousPage = xmlDoc.CreateNode(XmlNodeType.Element, "DangerousPage", null);
            DangerousPage.InnerText = newobj.DangerousPage;
            EDI.AppendChild(DangerousPage);
            //创建DangerousProperty节点
            XmlNode DangerousProperty = xmlDoc.CreateNode(XmlNodeType.Element, "DangerousProperty", null);
            DangerousProperty.InnerText = newobj.DangerousProperty;
            EDI.AppendChild(DangerousProperty);
            //创建DangerousProperty节点
            XmlNode carriage = xmlDoc.CreateNode(XmlNodeType.Element, "carriage", null);
            carriage.InnerText = stxtcarriage.Text;
            EDI.AppendChild(carriage);

            return xmlDoc;
        }

        private void stxtPOD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string path = @"E:\EDILOG\Template\";
                if (!System.IO.Directory.Exists(path))
                {
                    path = System.Windows.Forms.Application.StartupPath + @"EDILOG\Template\";
                }

                path += stxtPOD.Text + @"--" + stxtDelivery.Text + ".XML";
                if(File.Exists(path))
                {
                    ReadXml(path);
                    BindSource();
                }
            }
        }

        private void stxtDelivery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string path = @"E:\EDILOG\Template\";
                if (!System.IO.Directory.Exists(path))
                {
                    path = System.Windows.Forms.Application.StartupPath + @"EDILOG\Template\";
                }

                path += stxtPOD.Text + @"--" + stxtDelivery.Text + ".XML";
                if (File.Exists(path))
                {
                    ReadXml(path);
                    BindSource();
                }
            }
        }

        private void stxtVoyage_EditValueChanged(object sender, EventArgs e)
        {
           
        }

    }
}
