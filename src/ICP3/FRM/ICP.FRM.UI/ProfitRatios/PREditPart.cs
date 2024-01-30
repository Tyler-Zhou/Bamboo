using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Common.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace ICP.FRM.UI.ProfitRatios
{
    /// <summary>
    /// 利润配比
    /// </summary>
    [ToolboxItem(false)]
    public partial class PREditPart : BaseListPart
    {
        #region 本地变量
        /// <summary>
        /// 
        /// </summary>
        IDictionary<string, object> _values;
        #endregion

        #region 服务
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }

        /// <summary>
        /// 基础数据服务管理
        /// </summary>
        private IProfitRatiosService ProfitRatiosService
        {
            get
            {
                return ServiceClient.GetService<IProfitRatiosService>();
            }
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        private IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public override object DataSource
        {
            set
            {
                BindingData(value);
            }
        }

        /// <summary>
        /// 数据源 - 配比调整
        /// </summary>
        public List<ProfitRatiosAdjustment> DataSourceAdjustment
        {
            get
            {
                List<ProfitRatiosAdjustment> tmpList = _BSEdit.DataSource as List<ProfitRatiosAdjustment>;
                return tmpList ?? new List<ProfitRatiosAdjustment>();
            }
            set
            {
                _BSEdit.DataSource = value;
                _BSEdit.ResetBindings(false);
            }
        }

        /// <summary>
        /// 列表当前焦点行
        /// </summary>
        private List<BusinessStatisticsList> ListSelectRow { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 海运编辑界面
        /// </summary>
        public PREditPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }

            };

            if (!LocalData.IsDesignMode)
            {
                Load += (sender, e) =>
                {
                    InitMessage();
                    Disposed += PREditPart_Disposed;
                };
                gvMain.RowStyle += GvMain_RowStyle;
            }

        }

       

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            _values = values;
            if (_values == null) return;

            foreach (var item in _values)
            {
                if (item.Key.ToUpper() == "Message".ToUpper())
                {
                    break;
                }
            }
        }

        #endregion

        #region 显示信息

        /// <summary>
        /// 显示信息
        /// </summary>
        private void InitControls()
        {
            #region Adjustment Type
            riicmbAdjustmentType.BeginUpdate();
            riicmbAdjustmentType.Items.Clear();
            List<EnumHelper.ListItem<AdjustmnetType>> list = EnumHelper.GetEnumValues<AdjustmnetType>(LocalData.IsEnglish);
            foreach (var item in list)
            {
                if (Convert.ToInt32(item.Value) == 0)
                {
                    continue;
                }
                riicmbAdjustmentType.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            riicmbAdjustmentType.EndUpdate();
            cmbAdjustmentType.EditValue = AdjustmnetType.Difference;
            #endregion
        }

        #endregion

        #region 注册延迟加载的数据源
        /// <summary>
        /// 注册消息
        /// </summary>
        private void InitMessage()
        {
            
        }

        #endregion

        #region 事件
        private void GvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                ProfitRatiosAdjustment data = gvMain.GetRow(e.RowHandle) as ProfitRatiosAdjustment;
                if (data != null && data.AdjustmentAfterAmount !=data.OriginalAmount)
                {
                    e.Appearance.BackColor = Color.Pink;
                }else
                {
                    e.Appearance.BackColor = Color.Transparent;
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        } 
        #endregion

        #region IEditPart 成员
        /// <summary>
        /// 
        /// </summary>
        void GetData()
        {
            DataSourceAdjustment = new List<ProfitRatiosAdjustment>();
            QueryCriteria4Adjustment queryParameter = new QueryCriteria4Adjustment
            {
                OperationID = ListSelectRow.First().OperationID,
                ContractBaseItemID = ListSelectRow.First().ContractBaseItemID,
            };
            List<ProfitRatiosAdjustment> items = ProfitRatiosService.GetProfitRatiosAdjustmentList(queryParameter);
            DataSourceAdjustment = items;
        }
        private void InnerBindData()
        {
            SuspendLayout();
            InitControls();
            GetData();
            ResumeLayout();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void BindingData(object data)
        {
            if (data is BusinessStatisticsList)
            {
                ListSelectRow = new List<BusinessStatisticsList> { data as BusinessStatisticsList };
            }
            else
            {
                ListSelectRow = data as List<BusinessStatisticsList>;
            }
            InnerBindData();
        }

        #endregion

        #region 工具栏事件

        #region 界面输入验证

        bool ValidateData()
        {
            List<bool> isScrrs = new List<bool> { true, true, true };
            bool isScrr = true;
            foreach (var item in isScrrs)
            {
                if (item == false) isScrr = false;
            }
            return isScrr;
        }

        #endregion

        #region 保存
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Save(false);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSavingAs"></param>
        /// <returns></returns>
        private bool Save(bool isSavingAs)
        {
            _BSEdit.EndEdit();
            if (ValidateData() == false)
            {
                return false;
            }
            try
            {
                barSave.Enabled = false;
                ProfitRatiosAdjustmentSaveRequest saveRequest = BuildSaveData();
                ProfitRatiosService.SaveProfitRatiosAdjustment(saveRequest);
                GetData();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { barSave.Enabled = true; }
        }

        private void Reset()
        {
            try
            {
                barReset.Enabled = false;
                ProfitRatiosAdjustmentSaveRequest saveRequest = BuildResetData();
                ProfitRatiosService.ResetProfitRatiosAdjustment(saveRequest);
                GetData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { barReset.Enabled = true; }
        }
        private ProfitRatiosAdjustmentSaveRequest BuildSaveData()
        {
            List<ProfitRatiosAdjustment> items = DataSourceAdjustment;

            #region 收集数据
            List<Guid> OperationIDs = new List<Guid>()
                ,ContractBaseItemIDs = new List<Guid>()
                , ContainerTypeIDs = new List<Guid>()
                ;
            List<decimal> Amounts = new List<decimal>();
            List<DateTime?> UpdateDates = new List<DateTime?>();

            foreach (var selectItem in ListSelectRow)
            {
                OperationIDs.Add(selectItem.OperationID);
                ContractBaseItemIDs.Add(selectItem.ContractBaseItemID);
            }

            AdjustmnetType _AdjustmnetType =(AdjustmnetType)cmbAdjustmentType.EditValue;
            foreach (ProfitRatiosAdjustment item in items.Where(fItem=> (_AdjustmnetType == AdjustmnetType.AverageValue && fItem.Amount > 0)||(_AdjustmnetType == AdjustmnetType.Difference && fItem.Amount != 0)))
            {
                ContainerTypeIDs.Add(item.ContainerTypeID);
                Amounts.Add(item.Amount);
            }
            #endregion

            ProfitRatiosAdjustmentSaveRequest saveRequest = new ProfitRatiosAdjustmentSaveRequest()
            {
                AdjustmnetType = _AdjustmnetType,
                OperationIDs = OperationIDs.ToArray(),
                ContractBaseItemIDs = ContractBaseItemIDs.ToArray(),
                ContainerTypeIDs = ContainerTypeIDs.ToArray(),
                Amounts = Amounts.ToArray(),
                SaveByID = LocalData.UserInfo.LoginID,
                UpdateDate = DateTime.Now,
            };
            return saveRequest;
        }

        private ProfitRatiosAdjustmentSaveRequest BuildResetData()
        {
            List<ProfitRatiosAdjustment> items = DataSourceAdjustment;

            #region 收集数据
            List<Guid> OperationIDs = new List<Guid>()
                , ContractBaseItemIDs = new List<Guid>();

            foreach (var selectItem in ListSelectRow)
            {
                OperationIDs.Add(selectItem.OperationID);
                ContractBaseItemIDs.Add(selectItem.ContractBaseItemID);
            }
            #endregion

            ProfitRatiosAdjustmentSaveRequest saveRequest = new ProfitRatiosAdjustmentSaveRequest()
            {
                OperationIDs = OperationIDs.ToArray(),
                ContractBaseItemIDs = ContractBaseItemIDs.ToArray(),
                SaveByID = LocalData.UserInfo.LoginID,
                UpdateDate = DateTime.Now,
            };
            return saveRequest;
        }
        #endregion

        #region 重置
        private void barReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Reset();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        } 
        #endregion

        #endregion

        #region 资源回收

        /// <summary>
        /// 从工作项集合中移除自己
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PREditPart_Disposed(object sender, EventArgs e)
        {
            gvMain.RowStyle -= GvMain_RowStyle;
            if (Workitem != null)
            {
                Workitem.Items.Remove(this);
            }
        }

        /// <summary>
        /// 防闪烁
        /// </summary>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x0014) return;// 禁掉清除背景消息
            base.WndProc(ref m);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        #endregion

        
    }
}
