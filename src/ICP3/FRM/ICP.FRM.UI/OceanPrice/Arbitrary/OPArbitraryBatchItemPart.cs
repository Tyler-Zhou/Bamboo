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
using ICP.FRM.UI.Comm;

namespace ICP.FRM.UI.OceanPrice
{
    [ToolboxItem(false)]
    public partial class OPArbitraryBatchItemForm : BasePart
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

        #endregion

        ArbitraryBatchItem CurrentData
        {
            get { return bindingSource1.DataSource as ArbitraryBatchItem; }
            set { bindingSource1.DataSource= value; }
        }

        public ArbitraryBatchItem BatchItem { get { return CurrentData; } }

        #region init

        public OPArbitraryBatchItemForm()
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
            Utility.SetEnterToExecuteOnec(cmbTransportClause, delegate
            {
                List<TransportClauseList> transportClauses = TransportFoundationService.GetTransportClauseList(string.Empty, string.Empty, true, 0);
                TransportClauseList emptyTransportClause = new TransportClauseList();
                emptyTransportClause.ID = Guid.Empty;
                emptyTransportClause.Code = string.Empty;
                transportClauses.Insert(0, emptyTransportClause);
                cmbTransportClause.Properties.BeginUpdate();
                foreach (var item in transportClauses)
                {
                    cmbTransportClause.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
                }
                cmbTransportClause.Properties.EndUpdate();
            });
            #endregion

            #region GeographyType
            Utility.SetEnterToExecuteOnec(cmbGeographyType, delegate
            {
                List<EnumHelper.ListItem<GeographyType>> accountTypes = EnumHelper.GetEnumValues<GeographyType>(LocalData.IsEnglish);
                foreach (var item in accountTypes)
                {
                    if (item.Value == GeographyType.None)
                        cmbGeographyType.Properties.Items.Add(new ImageComboBoxItem(string.Empty, item.Value));
                    else
                        cmbGeographyType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));

                }
            });
            #endregion
        }

        void SearchRegister()
        {
            ArbitraryBatchItem currentData = CurrentData;

            OceanPortSearchRegister(SearchPortType.POL,stxtPOL);
            OceanPortSearchRegister(SearchPortType.POD,stxtPOD);


            #region Port

            //dfService.Register(stxtPOL, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
            //      delegate(object inputSource, object[] resultData)
            //      {
            //          stxtPOL.Text = currentData.POLName= resultData[2].ToString();
            //          stxtPOL.Tag = currentData.POLID= new Guid(resultData[0].ToString());
            //      }, Guid.Empty,null);

            //dfService.Register(stxtPOD, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
            //  delegate(object inputSource, object[] resultData)
            //  {
            //      stxtPOD.Text = currentData.PODName = resultData[2].ToString();
            //      stxtPOD.Tag = currentData.PODID = new Guid(resultData[0].ToString());
            //  }, Guid.Empty, null);

            #endregion
        }


        #region Port

        private void OceanPortSearchRegister(SearchPortType type, ButtonEdit textEdit)
        {
            textEdit.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SearchOceanPort(type, textEdit);
                }
            };
            textEdit.ButtonClick += delegate(object sender, ButtonPressedEventArgs e)
            {
                if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    SearchOceanPort(type, textEdit);
                }
            };
        }


        private void SearchOceanPort(SearchPortType type, TextEdit textEdit)
        {

            FRMOceanFinderWorkItem finder = Workitem.Items.AddNew<FRMOceanFinderWorkItem>();

            string[] returnFields = SearchFieldConstants.ResultValue;
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
                }
            };
            #endregion


            finder.Run();

        }

        #endregion
        #endregion

        #region 接口

        public List<OceanClientUnitToString> RateList
        {
            get;
            set;
        }
        public void SetSource(OceanList oceanList)
        {
            CurrentData = new ArbitraryBatchItem();
            CurrentData.OceanClientUnits = new List<OceanClientUnit>();

            List<OceanClientUnitToString> RateList = new List<OceanClientUnitToString>();

            foreach (var item in oceanList.OceanUnits)
            {

                OceanClientUnitToString ocu = new OceanClientUnitToString();
                ocu.UnitID = item.UnitID;
                ocu.UnitName = item.UnitName;
                ocu.Rate = string.Empty;
                RateList.Add(ocu);
            }

            bsRateUnit.DataSource = RateList;
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
            RateList = bsRateUnit.DataSource as List<OceanClientUnitToString>;
            if (RateList == null)
            {
                RateList = new List<OceanClientUnitToString>();
            }

            foreach (OceanClientUnitToString item in RateList)
            {
                if (!string.IsNullOrEmpty(item.Rate))
                {
                    try
                    {
                        decimal rate = Convert.ToDecimal(item.Rate);
                    }
                    catch (Exception ex)
                    {
                        string message = item.UnitName + "的价格请输入数字";
                        XtraMessageBox.Show(message);
                        return false; ;
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
    }

  
}
