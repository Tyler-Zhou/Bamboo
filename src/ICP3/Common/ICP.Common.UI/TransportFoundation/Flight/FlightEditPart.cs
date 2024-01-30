using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.UI.Common;

namespace ICP.Common.UI.TransportFoundation.Flight
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class FlightEditPart : BaseControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
    {
        #region 服务注入

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        #endregion

        #region

        public FlightEditPart()
        {
            InitializeComponent();
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this.DataChanged = null;
                
                this.dxErrorProvider1.DataSource = null;
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                this.airlineFinder.Dispose();
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            SearchRegister();
        }

        FlightInfo CurrentData
        {
            get { return bindingSource1.DataSource as FlightInfo; }
            set { bindingSource1.DataSource = value; }
        }

        private void SetCnText()
        {
            labNo.Text = "航班号";
            labAirline.Text = "航空公司";
        }
        IDisposable airlineFinder;
        void SearchRegister()
        {
          airlineFinder= DataFindClientService.Register(stxtAirline, CommonFinderConstants.CustomerAirlineFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                              delegate(object inputSource, object[] resultData)
                              {
                                  stxtAirline.Text = CurrentData.AirlineName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                                  stxtAirline.Tag = CurrentData.AirlineID = new Guid(resultData[0].ToString());
                              },
                              delegate()
                              {
                                  stxtAirline.Text = CurrentData.AirlineName = string.Empty;
                                  stxtAirline.Tag = CurrentData.AirlineID = Guid.Empty;
                              },
                              ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
        }

        #endregion

        public void BindingData(object data)
        {
            if (data == null) { this.bindingSource1.DataSource =  typeof(FlightInfo);this.Enabled = false; }
            else
            {
                this.bindingSource1.DataSource = data;
                if ((data as FlightInfo).IsValid == false) { this.Enabled = false; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit(); }
                else
                {
                    this.Enabled = true; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
                }
            }
        }

        #region IDataContentPart 成员
        public object Current { get { return this.bindingSource1.Current; } }
        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataChanged;
        public object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }
        public void EndEdit()
        {
            this.Validate();
            bindingSource1.EndEdit();
        }
        #endregion

        private void dteDOCClosingDate_KeyDown(object sender, KeyEventArgs e)
        {
            DevExpress.XtraEditors.DateEdit dateControl = (DevExpress.XtraEditors.DateEdit)sender;
            if (e.KeyCode == Keys.Delete)
            {
                dateControl.EditValue = null;

            }
            else if (e.KeyCode == Keys.Back
                &&
                (dateControl.Text.Trim().Length < 5 || dateControl.SelectionLength == dateControl.Text.Trim().Length))
            {
                dateControl.EditValue = null;
            }
        }
    }
}
