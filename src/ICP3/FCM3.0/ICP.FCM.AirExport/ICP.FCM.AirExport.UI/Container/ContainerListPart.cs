using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;

using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using Microsoft.Practices.ObjectBuilder;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIFramework;
using DevExpress.XtraGrid;
using ICP.Common.UI;

namespace ICP.FCM.AirExport.UI.Container
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class ContainerListPart : BaseEditPart
    {
        #region sevice



        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelper { get; set; }

        #endregion

        #region 变量

        #region State

        public Guid? BLQuantityUnitID { get; set; }
        public Guid? BLWeightUnitID { get; set; }
        public Guid? BLMeasurementUnitID { get; set; }
        public string BLWeightUnit { get; set; }
        public string BLMeasurementUnit { get; set; }
        public string BLQtyUnit { get; set; }
        public BLType BLSourceType { get; set; }
        public Guid MBLID { get; set; }
        public Guid HBLID { get; set; }
        public Guid ShippingOrderID { get; set; }

        bool _ReadOnly = false;
        public new bool ReadOnly { get { return _ReadOnly; } set { _ReadOnly =value;} }

        #endregion

        List<ContainerList> _ctnTypes = null;

        List<AirBLContainerList> _OrgData = null;

        AirBLContainerList CurrentRow
        {
            get
            {
                if (bsContainer.DataSource == null || bsContainer.Current == null) return null;
                else return bsContainer.Current as AirBLContainerList;
            }
        }

        List<AirBLContainerList> SelectedItem
        {
            get
            {
                int[] rowIndexs = gvCtn.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<AirBLContainerList> tagers = new List<AirBLContainerList>();
                foreach (var item in rowIndexs)
                {
                    AirBLContainerList ma = gvCtn.GetRow(item) as AirBLContainerList;
                    if (ma != null) tagers.Add(ma);
                }
                return tagers;
            }
        }

        #endregion

        #region init

        public ContainerListPart()
        {
            InitializeComponent();

            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            if (LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            colRelation.Caption = "关联";
            colCommodity.Caption = "品名";
            colNo.Caption = "NO";
            colSealNo.Caption = "封条号";
            colType.Caption = "箱型";
            colQuantity.Caption = "件数";
            colMeasurement.Caption = "体积";
            colWeight.Caption = "重量";
            colIsSOC.Caption = "自有箱";
            colIsPartOf.Caption = "Part单";
            colRelation.Caption = "关联";
            colMarks.Caption = "唛头";
            labCtnNo.Text = "箱号";
            barAdd.Caption = "新增(&A)";
            barDelete.Caption = "删除(&D)";
            btnNext.Text = "下一个(&N)";
            btnOK.Text = "确定(&O)";
            btnCancel.Text = "取消(&C)";

            rdoSelectMode.Properties.Items[0].Description = "全部";
            rdoSelectMode.Properties.Items[1].Description = "已选";
            rdoSelectMode.Properties.Items[2].Description = "未选";
        }

        bool isInit = false;
        private void InitControls()
        {
            if (isInit) return;
            //箱型
            _ctnTypes = ICPCommUIHelper.SetCmbContainerType(cmbType);
            

            #region Finder Style

            gvCtn.FormatConditions.Add(commStyleFormatCondition);

            commStyleFormatCondition.Appearance.Font = new Font(colNo.AppearanceCell.Font, FontStyle.Bold);
            commStyleFormatCondition.Appearance.BackColor = Color.Aqua;
            commStyleFormatCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.None;
            commStyleFormatCondition.ApplyToRow = false;
            commStyleFormatCondition.Column = colNo;

            #endregion

            if (_ReadOnly) { btnOK.Enabled = false; }

            isInit = true;
        }

        #endregion

        #region barItem

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.ValidateData() == false) return;

            if (Saved != null) this.Saved(new object[] { _OrgData });
            
            this.FindForm().Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private bool ValidateData()
        {
            this.Validate();
            this.bsContainer.EndEdit();
            bool isScrr = true;

            foreach (var item in bsContainer.DataSource as List<AirBLContainerList>)
            {
                if (item.Validate
                    (
                       delegate(ValidateEventArgs e)
                       {
                           if (item.Relation)
                           {
                               if (item.Quantity <= 0)
                               {
                                   e.SetErrorInfo("Quantity", LocalData.IsEnglish ? "Quantity can't be 0." : "件数不能为0.");
                               }
                               if (item.Measurement <= 0)
                               {
                                   e.SetErrorInfo("Measurement", LocalData.IsEnglish ? "Measurement can't be 0." : "体积不能为0.");
                               }
                               if (item.Weight <= 0)
                               {
                                   e.SetErrorInfo("Weight", LocalData.IsEnglish ? "Weight can't be 0." : "重量不能为0.");
                               }
                           }
                       }
                    ) == false) isScrr = false;
            }

            return isScrr;
        }

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddData();
        }

        private void AddData()
        {
            AirBLContainerList newData = new AirBLContainerList();
            newData.TypeID = _ctnTypes[0].ID;
            newData.TypeName = _ctnTypes[0].Code;
            newData.No = string.Empty;
            newData.Quantity = 0;
            newData.Measurement = 0m;
            newData.Weight = 0m;
            newData.BLID = Utility.GuidIsNullOrEmpty(MBLID) ? HBLID : MBLID;
            newData.CargoCreateDate = newData.CreateDate = DateTime.Now;
            newData.CargoCreateBy  = newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;

            bsContainer.Insert(0, newData);

            if (rdoSelectMode.SelectedIndex != 0) { _OrgData.Add(newData); }
            gvCtn.RefreshData();
        }

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<AirBLContainerList> selectedItem = SelectedItem;

            if (bsContainer.Current == null || selectedItem == null || selectedItem.Count ==0) return;

            if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Srue Delete Selected?" : "是否删除选中箱?",
                LocalData.IsEnglish ? "Tip" : "提示",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                List<AirBLContainerList> ctns = bsContainer.DataSource as List<AirBLContainerList>;
                foreach (var item in selectedItem)
                {
                    ctns.Remove(item);
                }

                bsContainer.DataSource = ctns;
                bsContainer.ResetBindings(false);
            }
        }

        #endregion

        #region 生成箱信息

        string BuildContainerInfo()
        {
            StringBuilder containerInfos = new StringBuilder();

            List<AirBLContainerList> source = bsContainer.DataSource as List<AirBLContainerList>;
            if(source ==null ||source.Count ==0) return string.Empty;

            List<AirBLContainerList> list = source.FindAll(delegate(AirBLContainerList item) { return item.Relation; });
            if (list == null || list.Count == 0) return string.Empty;

            for (int i = 0; i < gvCtn.RowCount; i++)
            {
                AirBLContainerList ctn = gvCtn.GetRow(i) as AirBLContainerList;
                if (ctn == null || ctn.Relation ==false) continue;

                if (containerInfos.Length == 0) containerInfos.Append("CONTAINER NO:");

                containerInfos.Append("\r\n");

                if (ctn.IsPartOf) containerInfos.Append("PART OF ");

                containerInfos.Append(string.Format("{0} / {1} / {2}", ctn.No.ToUpper(), gvCtn.GetRowCellDisplayText(i, colType) ?? string.Empty, ctn.SealNo));

                if (list.Count > 1)
                {
                    containerInfos.Append("/ " + ctn.Quantity + " " +
                                    (
                                        ctn.Quantity > 1 ?
                                            (
                                                BLQtyUnit.ToUpper().EndsWith("S") ?
                                                BLQtyUnit.ToString() :
                                                BLQtyUnit + "S"
                                            )
                                            :
                                            BLQtyUnit
                                    ));

                    string strWeight = ctn.Weight.ToString("F3");
                    string strMeasurement = ctn.Measurement.ToString("F3");
                    containerInfos.Append("/ " + strWeight + " " + BLWeightUnit);
                    containerInfos.Append("/ " + strMeasurement + " " + BLMeasurementUnit);
                }
                //else
                //{
                //    string strMeasurement = ctn.Measurement.ToString("F3");
                //    containerInfos.Append("/ " + strMeasurement + " " + BLMeasurementUnit);
                //}
            }

            return containerInfos.ToString();
        }

        /// <summary>
        /// 生成箱总计描述信息
        /// </summary>
        /// <returns></returns>
        string BuildContainerQty(List<AirBLContainerList> containers)
        {
            string[] chars = new string[] { "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN"
                                            ,"ELEVEN","TWELVE","THIRTEEN","FOURTEEN","FIFTEEN","SIXTEEN","SEVENTEEN","EIGHTEEN","NINETEEN","TWENTY"
                                            ,"TWENTY-ONE","TWENTY-TWO","TWENTY-THREE","TWENTY-FOUR","TWENTY-FIVE","TWENTY-SIX","TWENTY-SEVEN","TWENTY-EIGHT","TWENTY-NINE","THIRTY"
                                            ,"THIRTY-ONE","THIRTY-TWO","THIRTY-THREE","THIRTY-FOUR","THIRTY-FIVE","THIRTY-SIX","THIRTY-SEVEN","THIRTY-EIGHT","THIRTY-NINE","FORTY"
                                            ,"FORTY-ONE","FORTY-TWO","FORTY-THREE","FORTY-FOUR","FORTY-FIVE","FORTY-SIX","FORTY-SEVEN","FORTY-EIGHT","FORTY-NINE","FIFTY"
                                            ,"FIFTY-ONE","FIFTY-TWO","FIFTY-THREE","FIFTY-FOUR","FIFTY-FIVE","FIFTY-SIX","FIFTY-SEVEN","FIFTY-EIGHT","FIFTY-NINE","SIXTY"
                                            ,"SIXTY-ONE","SIXTY-TWO","SIXTY-THREE","SIXTY-FOUR","SIXTY-FIVE","SIXTY-SIX","SIXTY-SEVEN","SIXTY-EIGHT","SIXTY-NINE","SEVENTY"
                                            ,"SEVENTY-ONE","SEVENTY-TWO","SEVENTY-THREE","SEVENTY-FOUR","SEVENTY-FIVE","SEVENTY-SIX","SEVENTY-SEVEN","SEVENTY-EIGHT","SEVENTY-NINE","EIGHTTY"
                                            ,"EIGHTTY-ONE","EIGHTTY-TWO","EIGHTTY-THREE","EIGHTTY-FOUR","EIGHTTY-FIVE","EIGHTTY-SIX","EIGHTTY-SEVEN","EIGHTTY-EIGHT","EIGHTTY-NINE","NINETY"
                                            ,"NINETY-ONE","NINETY-TWO","NINETY-THREE","NINETY-FOUR","NINETY-FIVE","NINETY-SIX","NINETY-SEVEN","NINETY-EIGHT","NINETY-NINE","ONE HUNDREND"
                                            ,"ONE HUNDRED AND ONE","ONE HUNDRED AND TWO","ONE HUNDRED AND THREE","ONE HUNDRED AND FOUR","ONE HUNDRED AND FIVE"
                                            ,"ONE HUNDRED AND SIX","ONE HUNDRED AND SEVEN","ONE HUNDRED AND EIGHT","ONE HUNDRED AND NINE","ONE HUNDRED AND TEN"
                                            ,"ONE HUNDRED AND ELEVEN","ONE HUNDRED AND TWELVE","ONE HUNDRED AND THIRTEEN","ONE HUNDRED AND FOURTEEN","ONE HUNDRED AND FIFTEEN"
                                            ,"ONE HUNDRED AND SIXTEEN","ONE HUNDRED AND SEVENTEEN","ONE HUNDRED AND EIGHTEEN","ONE HUNDRED AND NINETEEN","ONE HUNDRED AND TWENTY"
                                            ,"ONE HUNDRED AND TWENTY-ONE","ONE HUNDRED AND TWENTY-TWO","ONE HUNDRED AND TWENTY-THREE","ONE HUNDRED AND TWENTY-FOUR","ONE HUNDRED AND TWENTY-FIVE"
                                             ,"ONE HUNDRED AND TWENTY-SIX","ONE HUNDRED AND TWENTY-SEVEN","ONE HUNDRED AND TWENTY-EIGHT","ONE HUNDRED AND TWENTY-NINE","ONE HUNDRED AND THIRTY"
                                            ,"ONE HUNDRED AND FORTY-ONE","ONE HUNDRED AND FORTY-TWO","ONE HUNDRED AND FORTY-THREE","ONE HUNDRED AND FORTY-FOUR","ONE HUNDRED AND FORTY-FIVE"
                                             ,"ONE HUNDRED AND FORTY-SIX","ONE HUNDRED AND FORTY-SEVEN","ONE HUNDRED AND FORTY-EIGHT","ONE HUNDRED AND FORTY-NINE","ONE HUNDRED AND FIFTY"
                                             ,"ONE HUNDRED AND FIFTY-ONE","ONE HUNDRED AND FIFTY-TWO","ONE HUNDRED AND FIFTY-THREE","ONE HUNDRED AND FIFTY-FOUR","ONE HUNDRED AND FIFTY-FIVE"
                                             ,"ONE HUNDRED AND FIFTY-SIX","ONE HUNDRED AND FIFTY-SEVEN","ONE HUNDRED AND FIFTY-EIGHT","ONE HUNDRED AND FIFTY-NINE","ONE HUNDRED AND SIXTY"
                                            ,"ONE HUNDRED AND SIXTY-ONE","ONE HUNDRED AND SIXTY-TWO","ONE HUNDRED AND SIXTY-THREE","ONE HUNDRED AND SIXTY-FOUR","ONE HUNDRED AND SIXTY-FIVE"
                                             ,"ONE HUNDRED AND SIXTY-SIX","ONE HUNDRED AND SIXTY-SEVEN","ONE HUNDRED AND SIXTY-EIGHT","ONE HUNDRED AND SIXTY-NINE","ONE HUNDRED AND SEVENTY"
                                            ,"ONE HUNDRED AND SEVENTY-ONE","ONE HUNDRED AND SEVENTY-TWO","ONE HUNDRED AND SEVENTY-THREE","ONE HUNDRED AND SEVENTY-FOUR","ONE HUNDRED AND SEVENTY-FIVE"
                                             ,"ONE HUNDRED AND SEVENTY-SIX","ONE HUNDRED AND SEVENTY-SEVEN","ONE HUNDRED AND SEVENTY-EIGHT","ONE HUNDRED AND SEVENTY-NINE","ONE HUNDRED AND EIGHTTY"
                                             ,"ONE HUNDRED AND EIGHTTY-ONE","ONE HUNDRED AND EIGHTTY-TWO","ONE HUNDRED AND EIGHTTY-THREE","ONE HUNDRED AND EIGHTTY-FOUR","ONE HUNDRED AND EIGHTTY-FIVE"
                                             ,"ONE HUNDRED AND EIGHTTY-SIX","ONE HUNDRED AND EIGHTTY-SEVEN","ONE HUNDRED AND EIGHTTY-EIGHT","ONE HUNDRED AND EIGHTTY-NINE","ONE HUNDRED AND NINETY"
                                             ,"ONE HUNDRED AND NINETY-ONE","ONE HUNDRED AND NINETY-TWO","ONE HUNDRED AND NINETY-THREE","ONE HUNDRED AND NINETY-FOUR","ONE HUNDRED AND NINETY-FIVE"
                                             ,"ONE HUNDRED AND NINETY-SIX","ONE HUNDRED AND NINETY-SEVEN","ONE HUNDRED AND NINETY-EIGHT","ONE HUNDRED AND NINETY-NINE"};

            string containerString = GetContainerString(containers);
            if (containers.Count > 1)
            {
                return string.Format("TOTAL:{0} CONTAINER({1})", chars[containers.Count - 1], containerString);
            }
            else if (containers.Count == 1)
            {
                return string.Format("TOTAL:{0} CONTAINER({1}) ONLY", chars[containers.Count - 1], containerString);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取箱信息
        /// </summary>
        string GetContainerString(List<AirBLContainerList> containers)
        {
            Dictionary<string, int> cntrs = new Dictionary<string, int>();
            //
            for (int i = 0; i < containers.Count; i++)
            {
                if (containers[i].Relation == false) continue;

                string key = gvCtn.GetRowCellDisplayText(i, colType); //containers[i].Size + containers[i].Type;
                if (cntrs.ContainsKey(key))
                {
                    cntrs[key] += 1;
                }
                else
                {
                    cntrs.Add(key, 1);
                }
            }
            string containerString = string.Empty;
            foreach (KeyValuePair<string, int> k in cntrs)
            {
                if (containerString != string.Empty)
                {
                    containerString += ",";
                }
                containerString += string.Format("{0}*{1}", k.Value, k.Key);
            }
            return containerString;
        }

        #endregion

        #region  

        private void bsContainer_PositionChanged(object sender, EventArgs e)
        {
        }

        private void gvCtn_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0 ) return;

            if (e.Column == colRelation)
            {
                AirBLContainerList currentRow = CurrentRow;
                if (currentRow.Relation == false)
                {
                    currentRow.BLID = Guid.Empty;
                    currentRow.Quantity = 0;
                    currentRow.Weight = currentRow.Measurement = 0;
                    currentRow.Marks = currentRow.Commodity = string.Empty;
                }
                else
                    currentRow.BLID = MBLID == Guid.Empty ? HBLID : MBLID;
            }
            else if (e.Column == colMeasurement || e.Column == colQuantity || e.Column == colWeight
                     ||e.Column == colCommodity || e.Column == colMarks)
            {
                AirBLContainerList currentRow = CurrentRow;
                if (currentRow.Relation == true) return;

                if (currentRow.Quantity != 0 || currentRow.Weight != 0 || currentRow.Measurement != 0
                    ||string.IsNullOrEmpty(currentRow.Commodity)==false || string.IsNullOrEmpty(currentRow.Marks)==false )
                {
                    currentRow.Relation = true;
                }
            }
            bsContainer.ResetCurrentItem();
            
        }

        private void rdoSelectMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoSelectMode.SelectedIndex == 0)//ALL
            {
                bsContainer.DataSource = _OrgData;
                bsContainer.ResetBindings(false);
                txtFind.Text = string.Empty;
            }
            else if (rdoSelectMode.SelectedIndex == 1)//Selected
            {
                bsContainer.DataSource = _OrgData.FindAll(delegate(AirBLContainerList item) { return item.Relation; });
                bsContainer.ResetBindings(false);
                txtFind.Text = string.Empty;
            }
            else if (rdoSelectMode.SelectedIndex == 2)//UnSelected
            {
                bsContainer.DataSource = _OrgData.FindAll(delegate(AirBLContainerList item) { return item.Relation==false; }); 
                bsContainer.ResetBindings(false);
                txtFind.Text = string.Empty;
            }
        }

        #endregion

        #region Fast Find

        private void txtFind_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            txtFind.Text = string.Empty;
        }

        StyleFormatCondition commStyleFormatCondition = new StyleFormatCondition();
        List<int> finderIndexs = new List<int>();
        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFind.Text.Trim()))
            {
                commStyleFormatCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.None;
                finderIndexs.Clear();
            }
            else
            {
                finderIndexs.Clear();

                StringBuilder expressionBulider = new StringBuilder();
                expressionBulider.Append("Upper([No]) Like '%");
                expressionBulider.Append(txtFind.Text.Trim().ToUpper());
                expressionBulider.Append("%'");

                commStyleFormatCondition.Expression = expressionBulider.ToString();
                commStyleFormatCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;

                List<AirBLContainerList> list = bsContainer.DataSource as List<AirBLContainerList>;
                List<AirBLContainerList> tagers = list.FindAll(delegate(AirBLContainerList item)
                {
                    return item.No.ToUpper().Contains(txtFind.Text.Trim().ToUpper());
                });

                if (tagers == null || tagers.Count == 0) return;

                List<Guid> ids = new List<Guid>();
                foreach (var item in tagers) { ids.Add(item.ID); }

                for (int i = 0; i < gvCtn.RowCount; i++)
                {
                    AirBLContainerList checkData = gvCtn.GetRow(i) as AirBLContainerList;
                    if (ids.Contains(checkData.ID)) finderIndexs.Add(i);
                }
                Next();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Next();
        }
        private void Next()
        {
            if (finderIndexs == null || finderIndexs.Count == 0) return;

            int currentFoudIndex = -1;//当前行Index
            currentFoudIndex = gvCtn.FocusedRowHandle;
            if (currentFoudIndex < 0) currentFoudIndex = 0;

            int needFocusedIndex = -1;
            if (finderIndexs.Contains(currentFoudIndex) == false)
            {
                needFocusedIndex = finderIndexs[0];
            }
            else
            {
                int tempIndex = finderIndexs.IndexOf(currentFoudIndex);
                if (tempIndex < 0 || tempIndex == finderIndexs.Count - 1) needFocusedIndex = finderIndexs[0];
                else needFocusedIndex = finderIndexs[tempIndex + 1];
            }

            if (needFocusedIndex < 0) return;

            gvCtn.FocusedRowHandle = needFocusedIndex;
        }
        #endregion

        #region IEditPart 成员

        void BindingData(object data)
        {
            this.rdoSelectMode.SelectedIndex = 0;
            InitControls();
            _OrgData = Utility.Clone < List < AirBLContainerList >>(data as List<AirBLContainerList>);
            if (_OrgData == null) _OrgData = new List<AirBLContainerList>();
            bsContainer.DataSource = _OrgData;
            bsContainer.ResetBindings(false);

            foreach (var item in _OrgData)
            {
                item.CancelEdit();
                item.BeginEdit();
            }
        }

        /// <summary>
        /// AirBLContainerList
        /// </summary>
        public override object DataSource
        {
            get { return bsContainer.DataSource; }
            set { BindingData(value); }
        }

        public override void EndEdit()
        {
            this.Validate();
            bsContainer.EndEdit();
        }

        /// <summary>
        /// 返回List Guid
        /// </summary>
        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region interface

        /// <summary>
        /// 获取箱信息
        /// </summary>
        public string CTNInfo
        {
            get { return BuildContainerInfo(); }
        }
        /// <summary>
        /// 获取件数信息
        /// </summary>
        public string QtyInfo
        {
            get { return BuildContainerQty(_OrgData.FindAll(delegate(AirBLContainerList item) { return item.Relation; })); }
        }
        /// <summary>
        /// 箱拼装串
        /// </summary>
        public string ContainerString
        {
            get { return GetContainerString(_OrgData.FindAll(delegate(AirBLContainerList item) { return item.Relation; })); }
        }

        #endregion
    }
}
