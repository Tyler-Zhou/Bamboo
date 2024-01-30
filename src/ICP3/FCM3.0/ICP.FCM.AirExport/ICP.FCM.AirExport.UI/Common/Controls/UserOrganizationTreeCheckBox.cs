using System;
using System.Collections.Generic;

namespace ICP.FCM.AirExport.UI.Common.Controls
{
    public partial class UserOrganizationTreeCheckBox : DevExpress.XtraEditors.XtraUserControl
    {
        #region init

        public UserOrganizationTreeCheckBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 控制不能改变高度
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            //this.Height = 21;
        }

        #endregion

        #region interface

        public object EditValue
        {
            get
            {
                if (popControl1.Text == allText)
                    return bsSource.DataSource;
                else if (popControl1.Text == string.Empty)
                    return null;
                else
                {
                    List<object> value = new List<object>();
                    for (int i = 0; i < treeMain.AllNodesCount; i++)
                    {
                        object o = treeMain.GetDataRecordByNode(treeMain.FindNodeByID(i));
                        value.Add(o);
                    }
                    return value;
                }
            }
        }

        string allText = "ALL";
        public string AllText { get { return allText; } set { allText = value; } }

        Type sourceType = null;
        public void SetSource<T>(List<T> source,string fieldName)
        {
            sourceType = typeof(T);

            bsSource.DataSource = source;
            bsSource.ResetBindings(true);
        }

        #endregion

        #region event

        private void btnClear_Click(object sender, EventArgs e)
        {
            popControl1.Text = string.Empty;
        }

        //private void treeMain_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        //{
        //    if (e.Node.ParentNode == null)
        //    {
        //        if (e.Node.Checked)
        //            popControl1.Text = AllText;
        //        else
        //            popControl1.Text = string.Empty;
        //    }
        //    else
        //    {
        //        string str = popControl1.Text;
        //        object o = treeMain.GetDataRecordByNode(e.Node);
        //        string name = Utility.GetObjectPropertyValue(sourceType, o, colName.FieldName);
        //        if (string.IsNullOrEmpty(name)) return;

        //        if (e.Node.Checked)
        //        {
        //            if (popControl1.Text == allText)
        //            {
        //                popControl1.Text = str += ICP.Framework.CommonLibrary.Common.GlobalConstants.ShowDividedSymbol;
        //                return;
        //            }
        //            else
        //            {
        //                if (str.Length > 0) str += ICP.Framework.CommonLibrary.Common.GlobalConstants.ShowDividedSymbol;
        //                str += name;
        //            }
        //        }
        //        else
        //        {
        //            if (str.Contains(name + ICP.Framework.CommonLibrary.Common.GlobalConstants.ShowDividedSymbol))
        //                str.Replace(name + ICP.Framework.CommonLibrary.Common.GlobalConstants.ShowDividedSymbol, string.Empty);
        //            else
        //                str.Replace(name, string.Empty);
        //        }
        //        popControl1.Text = str;
        //    }
        //}

        private void treeMain_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
