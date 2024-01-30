using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FRM.UI.InquireRates
{
    [ToolboxItem(false)]
    public partial class RateUnitPart : BasePart
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        public RateUnitPart()
        {
            InitializeComponent();
            Disposed += delegate {
                CurruntUnitList = null;
                _ctnLists = null;
                if (rcmbRateUnit != null)
                {
                    rcmbRateUnit.SelectedIndexChanged -= rcmbRateUnit_SelectedIndexChanged;
                }
                gcRate.DataSource = null;
                bsRateUnit.DataSource = null;
                bsRateUnit.Dispose();
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
            RegisterMessage("AccountToolTip", "You can use semicolons for dividing multi-Account, e.g. IBM; Google");
        }

        InquireUnit CurrentUnit
        {
            get { return bsRateUnit.Current as InquireUnit; }
            set
            {
                InquireUnit unit = CurrentUnit;
                unit = value;
            }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public List<InquireUnit> UnitList
        {
            get
            {
                List<InquireUnit> list = bsRateUnit.DataSource as List<InquireUnit>;
                if (list == null)
                {
                    list = new List<InquireUnit>();
                }

                return list;
            }
        }

        List<ContainerList> _ctnLists = null;

        private bool _isChanged = false;

        /// <summary>
        /// 是否有数据发生改变
        /// </summary>
        public bool IsChanged
        {
            get
            {
                if (_isChanged)
                {
                    return true;
                }

                foreach (var item in UnitList)
                {
                    if (item.IsDirty)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public List<InquireUnit> CurruntUnitList = null;

        public void SetSouce(List<InquireUnit> units, InquierType t)
        {
            #region Container

            if (t == InquierType.AirRates)
            {
                var result = TransportFoundationService.GetDataDictionaryList(string.Empty, string.Empty, DataDictionaryType.AirUnit, true, 0);
                _ctnLists = new List<ContainerList>();
                foreach (var sub in result)
                {
                    _ctnLists.Add(new ContainerList(){ID=sub.ID, Code=sub.Code});
                }
            }
            else if (t == InquierType.OceanRates)
                _ctnLists = TransportFoundationService.GetContainerList(string.Empty, true, 0);

            rcmbRateUnit.Properties.BeginUpdate();
            foreach (var item in _ctnLists)
            {
                rcmbRateUnit.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }

            rcmbRateUnit.Properties.EndUpdate();
            #endregion

            CurruntUnitList = Utility.Clone<List<InquireUnit>>(units);
            bsRateUnit.DataSource = CurruntUnitList;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //验证，单位不能重复。
            List<string> accumulate = new List<string>();
            foreach (var sub in CurruntUnitList)
            {
                if (accumulate.Contains(sub.UnitID.ToString()) == true)
                {
                    LocalCommonServices.ErrorTrace.ShowMessageBox("Duplicated units.", string.Format("Can not save, {0} are duplicated!", sub.UnitName), MessageBoxIcon.Error);
                    return;
                }

                accumulate.Add(sub.UnitID.ToString());
            }

            FindForm().DialogResult = DialogResult.OK;
            FindForm().Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }


        #region RateUnit

        private void barInsertRateUnit_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddRateUnit();
        }

        private void AddRateUnit()
        {
            //List<InquireUnit> lists = bsRateUnit.DataSource as List<InquireUnit>;
            //if (lists == null)
            //{
            //    CurruntUnitList = new List<InquireUnit>();
            //    bsRateUnit.DataSource = lists = CurruntUnitList;
            //}

            InquireUnit unit = new InquireUnit();
            unit.ID = new Guid();
            //unit.OceanID = _CurrentData.ID;
            CurruntUnitList.Add(unit);
            bsRateUnit.DataSource = CurruntUnitList;
            bsRateUnit.ResetBindings(false);
            _isChanged = true;         
        }

        private void barRemoveRateUnit_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteRateUnit();
        }

        private void DeleteRateUnit()
        {
            if (CurrentUnit == null) return;
            //All of the container's rates will be removed, are you sure?
            //if (_CurrentData.IsNew == false)
            //{

            DialogResult result = XtraMessageBox.Show(
                                            NativeLanguageService.GetText(this, "RemoveUnit"),
                                             "Tip",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            //}

            //InquireUnit unit = CurrentOceanUnit;
            //List<InquireUnit> lists = bsRateUnit.DataSource as List<InquireUnit>;
            //lists.Remove(unit);
            //bsRateUnit.DataSource = lists;
            bsRateUnit.RemoveCurrent();
            //bsRateUnit.ResetBindings(false);

            _isChanged = true;
        }

        private void rcmbRateUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ctnLists == null || _ctnLists.Count == 0) return;
            gvRate.CloseEditor();
            foreach (var item in _ctnLists)
            {
                if (item.ID == CurrentUnit.UnitID)
                {
                    CurrentUnit.UnitName = item.Code;
                    break;
                }
            }
        } 

        #endregion
    }
}
