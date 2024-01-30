using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.FCM.OceanImport.UI
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class OIBusinessFastSearch : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;
        public OIBusinessFastSearch()
        {
            InitializeComponent();
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        #region 服务
        [ServiceDependency]
        public IOceanImportService oIService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }

        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Workitem.Commands[OIBusinessCommandConstants.Command_ShowSearch].Execute();
        }

        #region 初始化

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            cmbNo.ShowSelectedValue(0,LocalData.IsEnglish?"All No":"全部单号");
            cmbCustomer.ShowSelectedValue(0, LocalData.IsEnglish ? "All Customer" : "全部客户");
            cmbPort.ShowSelectedValue(0, LocalData.IsEnglish ? "All Port" : "全部地点");
            cmbDate.ShowSelectedValue(0, LocalData.IsEnglish ? "All Date" : "全部日期");
            cmbDataValue.ShowSelectedValue(0, LocalData.IsEnglish ? "UnKnow" : "不确定");
  


            //单号
            Utility.SetEnterToExecuteOnec(cmbNo, delegate
            {
                cmbNo.Properties.BeginUpdate();
                cmbNo.Properties.Items.Clear();
                List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessNoSearchType>> noSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessNoSearchType>(LocalData.IsEnglish);
                foreach (var item in noSearchTypes)
                {
                    cmbNo.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
                }
                cmbNo.Properties.EndUpdate();
            });

            //客户
            Utility.SetEnterToExecuteOnec(cmbCustomer, delegate
           { 
              cmbCustomer.Properties.BeginUpdate();
              cmbCustomer.Properties.Items.Clear();
              List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessCustomerSearchType>> customerSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessCustomerSearchType>(LocalData.IsEnglish);
              foreach (var item in customerSearchTypes)
              {
                  cmbCustomer.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
              }
              cmbCustomer.Properties.BeginUpdate();
            });

            //地点
            Utility.SetEnterToExecuteOnec(cmbPort, delegate
            {
                cmbPort.Properties.BeginUpdate();
                cmbPort.Properties.Items.Clear();
                List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessPortSearchType>> portSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessPortSearchType>(LocalData.IsEnglish);
                foreach (var item in portSearchTypes)
                {
                    cmbPort.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
                }
                cmbPort.Properties.BeginUpdate();
            });
            
            //时间
            Utility.SetEnterToExecuteOnec(cmbDate, delegate
            {
                cmbDate.Properties.BeginUpdate();
                cmbDate.Properties.Items.Clear();
                List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessDateSearchType>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessDateSearchType>(LocalData.IsEnglish);
                foreach (var item in dateSearchTypes)
                {
                    cmbDate.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
                }
                cmbDate.Properties.BeginUpdate();
            });

            //时间内容
            Utility.SetEnterToExecuteOnec(cmbDataValue, delegate
            {
                cmbDataValue.Properties.BeginUpdate();
                cmbDataValue.Properties.Items.Clear();
                List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessDateValueSearchType>> dateValueSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessDateValueSearchType>(LocalData.IsEnglish);
                foreach (var item in dateValueSearchTypes)
                {
                    cmbDataValue.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
                }
                cmbDataValue.Properties.BeginUpdate();
            });

            cmbNo.SelectedIndex = cmbCustomer.SelectedIndex = cmbPort.SelectedIndex = cmbDate.SelectedIndex=this.cmbDataValue.SelectedIndex=0;

            SetControlsEnterToSearch();
        }

        /// <summary>
        /// 设置一些TextBox输入Enter键时,触发查询事件
        /// </summary>
        private void SetControlsEnterToSearch()
        {
            foreach (Control item in this.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit)
                {
                    item.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
                        {
                            this.btnSearch.PerformClick();
                        }
                        if (e.KeyCode == Keys.F3)
                        {
                            ClareText();
                        }
                    };
                }
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_FastSecharData)]
        public void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
                OnSearched(this, GetData());
        }

        public override object GetData()
        {

            try
            {
                int i = 0;
                if (string.IsNullOrEmpty(this.txtAddress.Text) && string.IsNullOrEmpty(this.txtCustomer.Text) && string.IsNullOrEmpty(this.txtNo.Text) && (this.cmbDataValue.SelectedIndex == 0))
                {
                    i = 100;
                }

               List<OceanBusinessList>  list= oIService.GetBusinessListByFastSearch(
                                        Utility.GetCompanyIDs(userService),
                                        (OIBusinessNoSearchType)this.cmbNo.EditValue,
                                        this.txtNo.Text,
                                        (OIBusinessCustomerSearchType)this.cmbCustomer.EditValue,
                                        this.txtCustomer.Text,
                                        (OIBusinessPortSearchType)this.cmbPort.EditValue,
                                        this.txtAddress.Text,
                                        (OIBusinessDateSearchType)this.cmbDate.EditValue,
                                        dtFrom,
                                        dtTo,
                                        null,
                                        i);
                return list;

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                return null;
            }
           
        }        

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        #endregion

        #region 清空
        private void ClareText()
        {
            this.txtAddress.Text = string.Empty;
            this.txtCustomer.Text = string.Empty;
            this.txtNo.Text = string.Empty;
            this.cmbDataValue.SelectedIndex = 0;

        }
        #endregion

        #region 属性

        public DateTime? dtFrom
        {
            get
            {
                switch (cmbDataValue.SelectedIndex)
                {
                    case 0:
                        return null;
                    case 1:
                        return DateTime.Now.Date.AddDays(-7);
                    case 2:
                        return DateTime.Now.Date.AddDays(-30);
                    case 3:
                        return DateTime.Now.Date.AddDays(-365);
                };
                return null;
            }
        }

        public DateTime? dtTo
        {
            get
            {
                if (cmbDataValue.SelectedIndex <= 0)
                    return null;
                else
                    return Utility.GetEndDate(DateTime.Now);
            }
        }

        //bool _isFirstLoad = false;
        //public Guid? FilerID
        //{
        //    get
        //    {
        //        if (_isFirstLoad)
        //        {
        //            return LocalData.UserInfo.LoginID;
        //        }

        //        return null;
        //    }
        //}

        #endregion

    }
}
