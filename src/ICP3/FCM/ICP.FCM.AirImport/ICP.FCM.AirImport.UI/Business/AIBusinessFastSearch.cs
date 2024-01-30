using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary;

namespace ICP.FCM.AirImport.UI
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class OIBusinessFastSearch : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;
        public OIBusinessFastSearch()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.OnSearched = null;
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        #region 服务
        public IAirImportService AirImportService
        {
            get
            {
                return ServiceClient.GetService<IAirImportService>();
            }
        }

        public ICP.Sys.ServiceInterface.IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<ICP.Sys.ServiceInterface.IUserService>();
            }
        }

        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Workitem.Commands[AIBusinessCommandConstants.Command_ShowSearch].Execute();
        }

        #region 初始化

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }

            //_isFirstLoad = true;
            //btnSearch_Click(null, null);
            //    //OnSearched(this, GetData());
            // _isFirstLoad = false;
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            //单号
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessNoSearchType>> noSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessNoSearchType>(LocalData.IsEnglish);
            foreach (var item in noSearchTypes)
            {
                cmbNo.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            //客户
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessCustomerSearchType>> customerSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessCustomerSearchType>(LocalData.IsEnglish);
            foreach (var item in customerSearchTypes)
            {
                cmbCustomer.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            //地点
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessPortSearchType>> portSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessPortSearchType>(LocalData.IsEnglish);
            foreach (var item in portSearchTypes)
            {
                cmbPort.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            //时间
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessDateSearchType>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessDateSearchType>(LocalData.IsEnglish);
            foreach (var item in dateSearchTypes)
            {
                cmbDate.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            //时间内容
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessDateValueSearchType>> dateValueSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessDateValueSearchType>(LocalData.IsEnglish);
            foreach (var item in dateValueSearchTypes)
            {
                cmbDataValue.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
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
        [CommandHandler(AIBusinessCommandConstants.Command_FastSecharData)]
        public void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    OnSearched(this, GetData());
                }
        }

        public override object GetData()
        {
            try
            {
               List<AirBusinessList>  list= AirImportService.GetBusinessListByFastSearch(
                                        Utility.GetCompanyIDs(UserService),
                                        (OIBusinessNoSearchType)this.cmbNo.EditValue,
                                        this.txtNo.Text.Trim(),
                                        (OIBusinessCustomerSearchType)this.cmbCustomer.EditValue,
                                        this.txtCustomer.Text.Trim(),
                                        (OIBusinessPortSearchType)this.cmbPort.EditValue,
                                        this.txtAddress.Text.Trim(),
                                        (OIBusinessDateSearchType)this.cmbDate.EditValue,
                                        dtFrom,
                                        dtTo,
                                        null,
                                        0);
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
                    return DateTime.Now.DateAttachEndTime();
            }
        }

        #endregion

    }
}
