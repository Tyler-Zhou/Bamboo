using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI.BatchBill
{
    /// <summary>
    /// 选择柜
    /// </summary>
    public partial class ContainersFinderPart : BaseEditPart
    {
        #region Services & Fields & Property & Delegate
        #region Fields
        #endregion

        #region Services
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem
        {
            get;
            set;
        }
        /// <summary>
        /// 空运出口服务
        /// </summary>
        public IAirExportService AirExportService
        {
            get { return ServiceClient.GetService<IAirExportService>(); }
        }
        /// <summary>
        /// 海运出口服务
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get { return ServiceClient.GetService<IOceanExportService>(); }
        }
        /// <summary>
        /// 海运进口服务
        /// </summary>
        public IOceanImportService OceanImportService
        {
            get
            { return ServiceClient.GetService<IOceanImportService>(); }
        }
        #endregion

        #region Property

        FormData _CurrentRow
        {
            get
            {
                return bsContainers.Current as FormData;
            }
        }
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID{ get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType OperationType { get; set; }
        /// <summary>
        /// 选择柜
        /// </summary>
        public FormData SelectContainer { get; set; }

        #endregion
        #endregion

        #region Init
        /// <summary>
        /// 选择柜
        /// </summary>
        public ContainersFinderPart()
        {
            InitializeComponent();
            RegisterEvent();
            Disposed += (sender, arg) =>
            {
                if (RootWorkItem != null)
                {
                    RootWorkItem.Items.Remove(this);
                    RootWorkItem = null;
                }
                UnRegisterEvent();
            };
        }

        /// <summary>
        /// OnLoad
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }
        #endregion

        #region Controls Event
        /// <summary>
        /// 
        /// </summary>
        void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectAndCloseForm();
            }
            if (e.KeyCode == Keys.Escape)
            {
                SelectContainer = null;
                CloseForm(DialogResult.Cancel);
            }
        }
        /// <summary>
        /// 网格双击
        /// </summary>
        void gvMain_DoubleClick(object sender, EventArgs e)
        {
            SelectAndCloseForm();
        }
        /// <summary>
        /// 失去焦点关闭窗体
        /// </summary>
        void gvMain_LostFocus(object sender, EventArgs e)
        {
            if (SelectContainer==null)
                CloseForm(DialogResult.Cancel);
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 初始化控件
        /// </summary>
        void InitControls()
        {
            
            try
            {
                List<FormData> _Containers = new List<FormData>();
                if (!_Containers.Select(fdItem => fdItem.OperationID.Equals(OperationID)).Any())
                {
                    switch (OperationType)
                    {
                        case OperationType.AirExport:
                            List<AirContainerList> aeContainers = AirExportService.GetAirContainerList(OperationID);
                            _Containers.AddRange(aeContainers.Select(airItem => new FormData { OperationID = OperationID, ID = airItem.ID, No = airItem.No }));
                            break;
                        case OperationType.OceanExport:
                            List<OceanContainerList> oeContainers = OceanExportService.GetOceanContainerList(OperationID);
                            _Containers.AddRange(oeContainers.Select(oeItem => new FormData { OperationID = OperationID, ID = oeItem.ID, No = oeItem.No }));
                            break;
                        case OperationType.OceanImport:
                            List<OIBusinessContainerList> oiContainers = OceanImportService.GetOIContainerList(OperationID);
                            _Containers.AddRange(from oiItem in oiContainers where oiItem.ID != null select new FormData { OperationID = OperationID, ID = oiItem.ID.Value, No = oiItem.No });
                            break;
                    }
                }
                bsContainers.DataSource = _Containers;
                bsContainers.ResetBindings(false);
            }
            catch(Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
            
        }
        /// <summary>
        /// 注册事件
        /// </summary>
        void RegisterEvent()
        {
            gvMain.KeyDown += gvMain_KeyDown;
            gvMain.DoubleClick += gvMain_DoubleClick;
            gvMain.LostFocus += gvMain_LostFocus;
        }
        /// <summary>
        /// 移除事件
        /// </summary>
        void UnRegisterEvent()
        {
            gvMain.KeyDown -= gvMain_KeyDown;
            gvMain.DoubleClick -= gvMain_DoubleClick;
            gvMain.LostFocus -= gvMain_LostFocus;
        }

        void SelectAndCloseForm()
        {
            if (_CurrentRow != null)
            {
                SelectContainer = _CurrentRow;
                CloseForm(DialogResult.OK);
            }
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="dialogResult"></param>
        void CloseForm(DialogResult dialogResult)
        {
            if (dialogResult == DialogResult.Cancel)
            {
            }
            var findForm = FindForm();
            if (findForm != null)
            {
                findForm.DialogResult = dialogResult;
                findForm.Close();
            }
        }
        #endregion
    }
}
