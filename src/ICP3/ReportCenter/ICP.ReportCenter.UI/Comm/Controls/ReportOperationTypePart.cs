using System;
using System.Collections.Generic;
using System.Text;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.ReportCenter.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.ReportCenter.UI.Comm.Controls
{
    public partial class ReportOperationTypePart : BasePart
    {
        #region init
        public ReportOperationTypePart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.EditValueChanged = null;
                if (this.treeCheckControl1 != null)
                {
                    this.treeCheckControl1.CheckedChanged -= this.treeCheckControl1_CheckedChanged;
                    this.treeCheckControl1 = null;
                }
                
            
            };
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode) treeCheckControl1.SplitString = _SplitString;
        }
        /// <summary>
        /// 控制不能改变高度
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Height = 21;
        }

        #endregion

        #region Event

        void treeCheckControl1_CheckedChanged(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (EditValueChanged != null) EditValueChanged(this, EventArgs.Empty);
        }
        public event EventHandler EditValueChanged;

        #endregion

        #region interFace
        List<ReportOperationType> _ReportOperationTypes = null;
        public void SetSource(List<ReportOperationType> reportOperationTypes)
        {
            _ReportOperationTypes = reportOperationTypes;
            List<TreeCheckControlSource> tss = new List<TreeCheckControlSource>();
            foreach (var item in _ReportOperationTypes)
            {
                tss.Add(new TreeCheckControlSource { ID = item.ID, ParentID = item.ParentID, Name = LocalData.IsEnglish ? item.EName : item.CName });
            }
            this.treeCheckControl1.SetSource(tss);

            treeCheckControl1.CheckedChanged -= new DevExpress.XtraTreeList.NodeEventHandler(treeCheckControl1_CheckedChanged);
            treeCheckControl1.CheckedChanged += new DevExpress.XtraTreeList.NodeEventHandler(treeCheckControl1_CheckedChanged);
        }

        public string EditValue
        {
            get
            {
                if (_ReportOperationTypes == null) return string.Empty;

                StringBuilder str = new StringBuilder();
                List<Guid> ids = treeCheckControl1.EditValue;
                foreach (var item in ids)
                {
                    ReportOperationType tager = _ReportOperationTypes.Find(r => r.ID == item);
                    if (tager != null)
                    {
                        if (!string.IsNullOrEmpty(tager.Value))
                        {
                            if (str.Length > 0)
                            {
                                str.Append(",");
                            }
                            str.Append(tager.Value);
                        }
                        if (tager.ValueList != null && tager.ValueList.Count > 0)
                        {
                            foreach (string stringItem in tager.ValueList)
                            {
                                if (str.Length > 0)
                                {
                                    str.Append(",");
                                }
                                str.Append(stringItem);
                            }
                        }
                        
                    }
                }

                return str.ToString();
            }
        }

        public List<Guid> CheckedItems
        {
            get
            {
                return treeCheckControl1.EditValue;
            }
        }

        string _SplitString = ";";
        public string SplitString
        {
            get { return _SplitString; }
            set { _SplitString  = value; }
        }
        public void CheckItem(Guid value)
        {
            List<Guid> ids = treeCheckControl1.EditValue;
            if (ids == null) ids = new List<Guid>();

            if (ids.Contains(value) == false)
            {
                ids.Add(value);
                treeCheckControl1.EditValue = ids;
            }
        }

        public void CheckAll()
        {
            List<Guid> ids= new List<Guid>();
            foreach (var item in treeCheckControl1.Source)
            {
                ids.Add(item.ID);
            }
            treeCheckControl1.EditValue = ids;
        }

        #endregion
    }
}
