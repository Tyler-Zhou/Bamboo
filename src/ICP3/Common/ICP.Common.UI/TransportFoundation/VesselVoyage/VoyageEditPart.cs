using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using System;
using DevExpress.XtraEditors.Controls;
namespace ICP.Common.UI.TransportFoundation.VesselVoyage
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class VoyageEditPart : ICP.Common.UI.Common.BaseControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
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

        private BindingSource bindingSourceVessel = new BindingSource();
        private BackgroundWorker worker;
        #endregion
      
        #region init

        VoyageInfo CurrentData
        {
            get
            {
                return bindingSource1.Current as VoyageInfo;
            }
            set
            {
                bindingSource1.DataSource = value;
            }

        }

        List<VesselList> _vesselList = null;

        public VoyageEditPart()
        {
            InitializeComponent();

            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this.DataChanged = null;
                this._vesselList = null;
                this.dxErrorProvider1.DataSource = null;
                
                this.bindingSourceVessel.DataSource = null;
                this.bindingSourceVessel.Dispose();
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                this.cboVessel.ButtonClick -= this.cboVessel_ButtonClick;
                if (this.worker != null)
                {
                    this.worker.DoWork -= this.worker_DoWork;
                    this.worker.RunWorkerCompleted -= this.worker_RunWorkerCompleted;
                    this.worker = null;
                }
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                AddButton();
                InitBackgroundWorker();
            }
        }

        private void AddButton()
        {
            this.cboVessel.Properties.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, LocalData.IsEnglish ? "Add Vessel" : "新增船名"));

            EditorButton btnRefresh = new EditorButton();
            btnRefresh.Kind = ButtonPredefines.Glyph;
            btnRefresh.Image = ICP.Framework.ClientComponents.Properties.Resources.refresh;
            btnRefresh.ToolTip = LocalData.IsEnglish ? "Refresh to get new vessel." : "刷新以获取新增的船名";
            this.cboVessel.Properties.Buttons.Add(btnRefresh);
        }


        private void InitBackgroundWorker()
        {
            if (worker == null)
            {
                worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.WorkerSupportsCancellation = true;
                worker.RunWorkerAsync();
            }
            else
            {
                worker.RunWorkerAsync();
            }
            
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                return;
            if (e.Error != null)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), e.Error);
            }
            bindingSourceVessel.DataSource = _vesselList;
            InitLookupEditVessel();

        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _vesselList = TransportFoundationService.GetVesselList(
                  string.Empty,
                  string.Empty,
                  string.Empty,
                  true,
                  0);

            if (_vesselList == null)
            {
                _vesselList = new List<VesselList>();
            }
        }

        private void InitLookupEditVessel()
        {
           
            this.cboVessel.DataBindings.Clear();
            this.cboVessel.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "VesselID", true));
            this.cboVessel.Properties.DataSource = bindingSourceVessel;
            this.cboVessel.Properties.DisplayMember = "Name";
            this.cboVessel.Properties.ValueMember = "ID";

            if (this.cboVessel.Properties.Columns.Count <= 0)
            {
                LookUpColumnInfo colVesselName = new LookUpColumnInfo("Name", LocalData.IsEnglish ? "Vessel Name" : "船名");
                LookUpColumnInfo colCarrierName = new LookUpColumnInfo("CarrierCode", LocalData.IsEnglish ? "Carrier Code" : "船东");
                cboVessel.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboVessel.Properties.SearchMode = SearchMode.AutoComplete;
                this.cboVessel.Properties.Columns.Add(colVesselName);
                this.cboVessel.Properties.Columns.Add(colCarrierName);
            }

        }

        private void SetCnText()
        {
       
            labNo.Text = "航次";
        
            labVessel.Text = "船名";
           
        }

        private void cboVessel_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                if (LocalCommonServices.PermissionService.HaveActionPermission("VESSEL_ADD"))
                {
                    VesselInfo newData = new VesselInfo();
                    newData.CreateByID = LocalData.UserInfo.LoginID;
                    newData.CreateByName = LocalData.UserInfo.LoginName;
                    newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                    newData.IsValid = true; 
                    VesselAddPart vaPart = Workitem.Items.AddNew<VesselAddPart>();
                    vaPart.SetSource(newData);
                    //Utility.ShowDialog(vaPart, LocalData.IsEnglish ? "Add Vessel" : "新增船名");
                    vaPart.Text = LocalData.IsEnglish ? "Add Vessel" : "新增船名";
                    vaPart.ShowDialog();
                    if (vaPart.VesselInfoData != null)
                    {
                        //ImageComboBoxItem imageCmbItem = new ImageComboBoxItem(vaPart.VesselInfoData.Name, vaPart.VesselInfoData.ID);
                        //cboVessel.Properties.Items.Insert(0, imageCmbItem);
                      
                        CurrentData.VesselID = vaPart.VesselInfoData.ID;
                        CurrentData.VesselName = vaPart.VesselInfoData.Name;
                       
                        VesselList vesselNew = new VesselList();
                        vesselNew.ID = vaPart.VesselInfoData.ID;
                        vesselNew.Name = vaPart.VesselInfoData.Name;
                        _vesselList.Insert(0, vesselNew);
                        bindingSourceVessel.ResetBindings(false);
                        this.cboVessel.EditValue = CurrentData.VesselID;
                        
                    }
                }
            }
                //刷新
            else if (e.Button.Kind == ButtonPredefines.Glyph)
            {
                if (!this.worker.IsBusy)
                {
                    InitBackgroundWorker();
                }
            }
        }

        #endregion

        public void BindingData(object data)
        {
            if (data == null) { this.bindingSource1.DataSource = typeof(VoyageInfo); this.Enabled = false; }
            else
            {
                this.bindingSource1.DataSource = data;
                this.cboVessel.EditValue = CurrentData.VesselID;  
                if ((data as VoyageInfo).IsValid == false) { this.Enabled = false; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit(); }
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
            this.CurrentData.VesselName = cboVessel.Text.Trim();
            this.Validate();
            bindingSource1.EndEdit();
        }
        #endregion

        private void dteCYClosing_KeyDown(object sender, KeyEventArgs e)
        {
            DevExpress.XtraEditors.DateEdit dateControl = (DevExpress.XtraEditors.DateEdit)sender;
            if (e.KeyCode == Keys.Delete)
            {
                dateControl.EditValue = null;
                
            }
            else if (e.KeyCode == Keys.Back 
                && 
                (dateControl.Text.Trim().Length < 5 || dateControl.SelectionLength==dateControl.Text.Trim().Length))
            {
                dateControl.EditValue = null;
                //dateControl.Text = string.Empty;
            }
        }
    }
}
