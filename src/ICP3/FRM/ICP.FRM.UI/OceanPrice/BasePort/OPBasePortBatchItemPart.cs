using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface.DataObjects;

using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.FRM.UI.Comm;

namespace ICP.FRM.UI.OceanPrice
{
    [ToolboxItem(false)]
    public partial class OPBasePortBatchItemForm : BasePart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        public OceanPriceUIDataHelper OceanPriceUIDataHelper
        {
            get
            {
                return ClientHelper.Get<OceanPriceUIDataHelper, OceanPriceUIDataHelper>();
            }
        }

        #endregion

        BasePortBatchItem CurrentData
        {
            get { return bindingSource1.DataSource as BasePortBatchItem; }
            set { bindingSource1.DataSource= value; }
        }

        public BasePortBatchItem BatchItem { get { return CurrentData; } }

        #region init

        public OPBasePortBatchItemForm()
        {
            InitializeComponent();
            Disposed += delegate {
                gcRate.DataSource = null;
                bindingSource1.DataSource = null;
                bindingSource1.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (!DesignMode) { InitMessage(); }
        }
        private void InitMessage()
        {
            RegisterMessage("SearchCommPartTitel", "Comm");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
            SearchRegister();
        }

        private void InitControls()
        {
            #region 运输条款
            List<TransportClauseList> transportClauses = TransportFoundationService.GetTransportClauseList(string.Empty, string.Empty, true, 0);
            TransportClauseList emptyTransportClause = new TransportClauseList();
            emptyTransportClause.ID = Guid.Empty;
            emptyTransportClause.Code = string.Empty;
            transportClauses.Insert(0,emptyTransportClause);

            foreach (var item in transportClauses)
            {
                cmbTransportClause.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }
            #endregion

            #region AccountType
            List<EnumHelper.ListItem<AccountType>> accountTypes = EnumHelper.GetEnumValues<AccountType>(LocalData.IsEnglish);
            foreach (var item in accountTypes)
            {
                if (item.Value == AccountType.None)
                    cmbAccountType.Properties.Items.Add(new ImageComboBoxItem(string.Empty, item.Value));
                else
                    cmbAccountType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));

            }
            #endregion

            #region ChangeOperation
            List<EnumHelper.ListItem<ChangeOperation>> changeOpertions = EnumHelper.GetEnumValues<ChangeOperation>(LocalData.IsEnglish);
            foreach (var item in changeOpertions)
            {
                cmbDescriptionOperation.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                cmbCommOperation.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                cmbSurchargeOperation.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                cmbTTOperation.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                
            }
            cmbDescriptionOperation.SelectedIndex = cmbCommOperation.SelectedIndex = cmbSurchargeOperation.SelectedIndex= cmbTTOperation.SelectedIndex = 2;
            #endregion
        }

        void SearchRegister()
        {
            BasePortBatchItem currentData = CurrentData;

            OceanPortSearchRegister(SearchPortType.POD,stxtPOD);
            OceanPortSearchRegister(SearchPortType.POL, stxtPOL);
            OceanPortSearchRegister(SearchPortType.VIA, stxtVIA);
            OceanPortSearchRegister(SearchPortType.Delivery, stxtDelivery);

            #region Port

            //dfService.Register(stxtPOL, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
            //      delegate(object inputSource, object[] resultData)
            //      {
            //          stxtPOL.Text = currentData.POLName= resultData[2].ToString();
            //          stxtPOL.Tag = currentData.POLID= new Guid(resultData[0].ToString());
            //      }, Guid.Empty,null);

            //dfService.Register(stxtVIA, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
            //delegate(object inputSource, object[] resultData)
            //{
            //    stxtVIA.Text = currentData.PODName = resultData[2].ToString();
            //    stxtVIA.Tag = currentData.PODID = new Guid(resultData[0].ToString());
            //}, Guid.Empty, null);

            //dfService.Register(stxtPOD, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
            //  delegate(object inputSource, object[] resultData)
            //  {
            //      stxtPOD.Text = currentData.PODName = resultData[2].ToString();
            //      stxtPOD.Tag = currentData.PODID = new Guid(resultData[0].ToString());
            //  }, Guid.Empty, null);

            //dfService.Register(stxtDelivery, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
            //    delegate(object inputSource, object[] resultData)
            //    {
            //        stxtDelivery.Text = currentData.PlaceOfDeliveryName = resultData[2].ToString();
            //        stxtDelivery.Tag = currentData.PlaceOfDeliveryID = new Guid(resultData[0].ToString());
            //    }, Guid.Empty, null);

            #endregion
        }

        #endregion

        #region 接口

        public void SetSource(OceanList oceanList, ClientBasePortList selectBasePort)
        {
            CurrentData = new BasePortBatchItem();
            CurrentData.CommOperation = ChangeOperation.Override;
            CurrentData.SurchargeOperation = ChangeOperation.Override;
            CurrentData.DescriptionOperation = ChangeOperation.Override;
            CurrentData.TransitTimeOperation = ChangeOperation.Override;

            CurrentData.OceanClientUnits = new List<OceanClientUnit>();
            CurrentData.OceanClientUnitList = new List<OceanClientUnitToString>();
            if (selectBasePort.TransitTime.IsNullOrEmpty())
            {
                CurrentData.TransitTime = "no special free time filed";
            }
            foreach (var item in oceanList.OceanUnits)
            {
                OceanClientUnitToString ocu = new OceanClientUnitToString();
                ocu.UnitID = item.UnitID;
                ocu.UnitName = item.UnitName;
                ocu.Rate = string.Empty;
                CurrentData.OceanClientUnitList.Add(ocu);
            }



            bsRateUnit.DataSource = CurrentData.OceanClientUnitList;
        }

        #endregion

        #region event

        private void btnOK_Click(object sender, EventArgs e)
        {
            Validate();

            bindingSource1.EndEdit();
            if (!ValidateRateList())
            {
                return;
            }

            FindForm().DialogResult = DialogResult.OK;
            FindForm().Close();
        }

        private bool ValidateRateList()
        {
            CurrentData.OceanClientUnits = new List<OceanClientUnit>();
            foreach (OceanClientUnitToString item in CurrentData.OceanClientUnitList)
            {
                if (!string.IsNullOrEmpty(item.Rate))
                {
                    try
                    {
                         decimal rate =Convert.ToDecimal(item.Rate);
                    }
                    catch(Exception ex)
                    {
                        string message = item.UnitName + "的价格请输入数字";
                        XtraMessageBox.Show(message);
                        return false;;
                    }

                }
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FindForm().DialogResult = DialogResult.Cancel;
            FindForm().Close();
        }

        #endregion

        private void labComm_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OPSearchCommPart scf = Workitem.Items.AddNew<OPSearchCommPart>();
            scf.SetSource(OceanPriceUIDataHelper.Commoditys, txtComm.Text.Trim());
            DialogResult dr = PartLoader.ShowDialog(scf, NativeLanguageService.GetText(this, "SearchCommPartTitel"), FormBorderStyle.Sizable);
            if (dr == DialogResult.OK)
            {
                txtComm.Text = scf.CommString;
                Validate();
            }
        }

        #region Port

        private void OceanPortSearchRegister(SearchPortType type, ButtonEdit textEdit)
        {
            textEdit.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SearchOceanPort(type,textEdit);
                }
            };
            textEdit.ButtonClick += delegate(object sender, ButtonPressedEventArgs e)
            {
                if(e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                     SearchOceanPort(type,textEdit);
                }
            };
        }

       
        private void SearchOceanPort(SearchPortType type,TextEdit textEdit)
        {

            FRMOceanFinderWorkItem finder = Workitem.Items.AddNew<FRMOceanFinderWorkItem>();
            
            string[] returnFields= SearchFieldConstants.ResultValue;
            Dictionary<string, object> initValues = new Dictionary<string, object>();
            initValues.Add("ISOCEAN", true);
            initValues.Add("NAME", textEdit.Text);

            finder.TextValue = textEdit.Text;
            finder.ReturnFields = returnFields;
            finder.InitValues = initValues;


            #region 选择数据

            finder.DataChoosed += delegate(object sender, DataFindEventArgs e)
            {
                if (e.Data != null)
                {
                    if (type == SearchPortType.POL)
                    {
                        stxtPOL.Text = CurrentData.POLName = e.Data[2].ToString();
                        stxtPOL.Tag = CurrentData.POLID = new Guid(e.Data[0].ToString());
                    }
                    else if (type == SearchPortType.POD)
                    {
                        stxtPOD.Text = CurrentData.PODName = e.Data[2].ToString();
                        stxtPOD.Tag = CurrentData.PODID = new Guid(e.Data[0].ToString());
                    }
                    else if (type == SearchPortType.VIA)
                    {
                        stxtVIA.Text = CurrentData.VIAName = e.Data[2].ToString();
                        stxtVIA.Tag = CurrentData.VIAID = new Guid(e.Data[0].ToString());
                    }
                    else if (type == SearchPortType.Delivery)
                    {
                        stxtDelivery.Text = CurrentData.PlaceOfDeliveryName = e.Data[2].ToString();
                        stxtDelivery.Tag = CurrentData.PlaceOfDeliveryID = new Guid(e.Data[0].ToString());
                    }
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    if (type == SearchPortType.POL)
                    {
                        stxtPOL.Text = CurrentData.POLName = string.Empty;
                        stxtPOL.Tag = CurrentData.POLID = null;
                    }
                    else if (type == SearchPortType.POD)
                    {
                        stxtPOD.Text = CurrentData.PODName = string.Empty;
                        stxtPOD.Tag = CurrentData.PODID = null;
                    }
                    else if (type == SearchPortType.VIA)
                    {
                        stxtVIA.Text = CurrentData.VIAName = string.Empty;
                        stxtVIA.Tag = CurrentData.VIAID = null;
                    }
                    else if (type == SearchPortType.Delivery)
                    {
                        stxtDelivery.Text = CurrentData.PlaceOfDeliveryName = string.Empty;
                        stxtDelivery.Tag = CurrentData.PlaceOfDeliveryID = null;
                    }
                }
            };
            #endregion


            finder.Run();

        }

        #endregion


        #region 清空时间

        private void chkFromDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFromDate.Checked)
            {
                dteFrom.EditValue = null;
                dteFrom.Enabled = false;
            }
            else
            {
                dteFrom.Enabled = true;
            }
        }

        private void chkToDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkToDate.Checked)
            {
                dteTo.EditValue = null;
                dteTo.Enabled = false;
            }
            else
            {
                dteTo.Enabled = true;
            }
        }

        #endregion

    }


    public enum SearchPortType
    { 
        POL=1,
        POD=2,
        Delivery=3,
        VIA=4
    }

}
