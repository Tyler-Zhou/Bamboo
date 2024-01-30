using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Common.ServiceInterface.CompositeObjects;

namespace ICP.Common.UI.Configure.CommpanyConfigure
{
    [ToolboxItem(false)]
    [SmartPart]
    public partial class CommpanyConfigureEditPart : BaseEditPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        #endregion

        #region init

        public CommpanyConfigureEditPart()
        {
            InitializeComponent();
            Enabled = false;
            if (LocalData.IsEnglish == false) SetCnText();
            Disposed += delegate {
                Saved = null;
                _solutionList = null;
                cmbSolution.SelectedIndexChanged -= cmbSolution_SelectedIndexChanged;
                popCompany.QueryPopUp -= popCompany_QueryPopUp;
                treeCompany.DoubleClick -= treeCompany_DoubleClick;
                dxErrorProvider1.DataSource = null;
                treeCompany.DataSource = null;
                bindingSource1.DataSource = null;
                bindingSource1.Dispose();
                bsCompany.DataSource = null;
                bsCompany.Dispose();
                bsGeography.DataSource = null;
                bsGeography.Dispose();
                customerFinder.Dispose();
                issuePlaceFinder.Dispose();
                
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            
            };
        }

        private void SetCnText()
        {
            labCompany.Text = "公司";
            labCustomer.Text = "客户";
            labDefaultCurrency.Text = "默认币种";
            labChargingClosingDate.Text = "计费关帐日";
            lblAccountingClosingDate.Text = "财务关帐日";
            labIssuePlace.Text = "签发地";
            labShortCode.Text = "公司代码";
            labSolution.Text = "解决方案";
            labStandardCurrency.Text = "本位币";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
            popCompany.QueryPopUp += new CancelEventHandler(popCompany_QueryPopUp);
        }

        #endregion

        #region 控制器

        /// <summary>
        /// 客户管理控制器
        /// </summary>
        public CompanyConfigureController Controller
        {
            get
            {
                return ClientHelper.Get<CompanyConfigureController, CompanyConfigureController>();
            }
        }

        #endregion

        bool _flag = false;

        List<SolutionList> _solutionList;

        ConfigureInfo CurrentData
        {
            get { return bindingSource1.DataSource as ConfigureInfo; }
            set { bindingSource1.DataSource = value; }
        }

        private void InitControls()
        {
            barSave.Glyph = Framework.ClientComponents.Resources.GlobalResource.Save_16;
            if (LocalData.IsEnglish)
                colEShortName.Visible = true;
            else
                colCShortName.Visible = true;

            cmbSolution.Properties.Items.Clear();
            _solutionList = ConfigureService.GetSolutionList(string.Empty, true, 0);
            foreach (var item in _solutionList)
            {
                cmbSolution.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                                                  (LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }

            cmbSolution.DataBindings.Add(new Binding("EditValue", bindingSource1, "SolutionID", true, DataSourceUpdateMode.OnPropertyChanged));
            cmbSolution.DataBindings.Add(new Binding("Text", bindingSource1, "SolutionName", true));
            cmbSolution.SelectedIndexChanged += new EventHandler(cmbSolution_SelectedIndexChanged);

            if (cmbSolution.EditValue != null)
            {
                Guid SolutionID = TypeConvertHelper.GetGuid(cmbSolution.EditValue);
                SetCurrencyBySolutionID(SolutionID, true);
            }
            List<ConfigureKeyList> BLTitlelist = ConfigureService.GetConfigureKeyListForBLTitle();
            foreach (ConfigureKeyList BLTitle in BLTitlelist)
            {
                cmbBLTitle.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(BLTitle.Name, BLTitle.ID));
            }

            SearchRegister();

            var data = bindingSource1.DataSource as ConfigureInfo;
            if (data != null)
            {
                data.BeginEdit();
            }
        }

        private void cmbSolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSolution.SelectedItem != null)
            {
                CurrentData.SolutionName = cmbSolution.SelectedItem.ToString();
            }

            if (cmbSolution.EditValue == null || cmbSolution.EditValue.ToString().Length == 0) return;
            cmbSolution.SelectedIndexChanged -= new EventHandler(cmbSolution_SelectedIndexChanged);

            Guid SolutionID = new Guid(cmbSolution.EditValue.ToString());
            SetCurrencyBySolutionID(SolutionID, false);

            cmbSolution.SelectedIndexChanged += new EventHandler(cmbSolution_SelectedIndexChanged);
        }

        private void SetCurrencyBySolutionID(Guid solutionID, bool isInit)
        {
            ConfigureInfo currentData = bindingSource1.DataSource as ConfigureInfo;
            if (currentData == null) return;


            if (currentData.IsDirty == false)
                currentData.CancelEdit();

            currentData.SolutionID = solutionID;
            cmbStandardCurrency.Properties.Items.Clear();
            cmbDefaultCurrency.Properties.Items.Clear();

            if (solutionID != null && solutionID != Guid.Empty)
            {
                List<SolutionCurrencyList> currencys = ConfigureService.GetSolutionCurrencyList(solutionID, true);
                if (currencys != null)
                {
                    foreach (var item in currencys)
                    {
                        cmbStandardCurrency.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                                                           (item.CurrencyName, item.CurrencyID));

                        cmbDefaultCurrency.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                                                           (item.CurrencyName, item.CurrencyID));
                    }

                    if (currencys.Count > 0 && !isInit && !_flag)
                    {
                        cmbStandardCurrency.SelectedIndex = 0;
                        cmbDefaultCurrency.SelectedIndex = 0;

                        currentData.StandardCurrencyID = currencys[0].CurrencyID;
                        currentData.DefaultCurrencyID = currencys[0].CurrencyID;
                    }

                    if (currencys.Count == 0)
                    {
                        currentData.StandardCurrencyID = currentData.DefaultCurrencyID = Guid.Empty;
                        return;
                    }

                    if (currentData.StandardCurrencyID == Guid.Empty)
                    {
                        currentData.StandardCurrencyID = currencys[0].CurrencyID;
                        cmbStandardCurrency.SelectedIndex = 0;
                    }
                    if (currentData.DefaultCurrencyID == Guid.Empty)
                    {
                        currentData.DefaultCurrencyID = currencys[0].CurrencyID;
                        cmbDefaultCurrency.SelectedIndex = 0;
                    }
                    if (currentData == null)
                    {
                        Enabled = false;
                    }

                    //if (currentData.IsDirty == false)
                    //    currentData.BeginEdit();
                }
            }
        }
        IDisposable customerFinder, issuePlaceFinder;
        void SearchRegister()
        {
         customerFinder=  DataFindClientService.Register(stxtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                              delegate(object inputSource, object[] resultData)
                              {
                                  stxtCustomer.Text = CurrentData.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                                  stxtCustomer.Tag = CurrentData.CustomerID = new Guid(resultData[0].ToString());
                              },
                              delegate()
                              {
                                  stxtCustomer.Text = CurrentData.CustomerName = string.Empty;
                                  stxtCustomer.Tag = CurrentData.CustomerID = Guid.Empty;
                              },
                              ClientConstants.MainWorkspace);


         issuePlaceFinder= DataFindClientService.Register(stxtIssuePlace, CommonFinderConstants.LocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                              delegate(object inputSource, object[] resultData)
                              {
                                  stxtIssuePlace.Text = CurrentData.IssuePlaceName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                                  stxtIssuePlace.Tag = CurrentData.IssuePlaceID = new Guid(resultData[0].ToString());
                              },
                              delegate()
                              {
                                  stxtIssuePlace.Text = CurrentData.IssuePlaceName = string.Empty;
                                  stxtIssuePlace.Tag = CurrentData.IssuePlaceID = Guid.Empty;
                              },
                              ClientConstants.MainWorkspace);

          DataFindClientService.Register(stxtVATFeeName, CommonFinderConstants.ChargingCodeFinder, SearchFieldConstants.Name, SearchFieldConstants.ChargingCodeResultValue, GetSolutionChargingCodeSearchCondition,
                              delegate(object inputSource, object[] resultData)
                              {
                                  stxtVATFeeName.Text = CurrentData.VATFeeName = resultData[2].ToString();
                                  stxtVATFeeName.Tag = CurrentData.VATFeeID = new Guid(resultData[0].ToString());
                              },
                              delegate()
                              {
                                  stxtVATFeeName.Text = CurrentData.VATFeeName = string.Empty;
                                  stxtVATFeeName.Tag = CurrentData.VATFeeID= Guid.Empty;
                              },
                              ClientConstants.MainWorkspace);
            
        }

        SearchConditionCollection GetSolutionChargingCodeSearchCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", new Guid(cmbSolution.EditValue.ToString()), false);
            return conditions;
        }

        #region  Company

        void popCompany_QueryPopUp(object sender, CancelEventArgs e)
        {
            popCompany.QueryPopUp -= new CancelEventHandler(popCompany_QueryPopUp);
            List<OrganizationList> organizationList = OrganizationService.GetOfficeList();
            bsCompany.DataSource = organizationList;
        }

        OrganizationList CurrentOrganization { get { return bsCompany.Current as OrganizationList; } }

        private void treeCompany_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentOrganization == null) return;
            if (CurrentData == null) return;
            CurrentData.CompanyID = CurrentOrganization.ID;
            popCompany.Text = LocalData.IsEnglish ? CurrentOrganization.EShortName : CurrentOrganization.CShortName;
            popCompany.ClosePopup();
        }

        /*保存*/
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EndEdit();
            if (CurrentData == null) return;

            if (CurrentData.Validate() == false)
            {
                return;
            }
         
            try
            {
                //保存数据
                if (SaveData())
                {
                    //触发保存成功事件
                    if (Saved != null)
                    {
                        Saved(bindingSource1.DataSource);
                    }

                    //提示保存成功
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                        FindForm(),
                        "保存成功!");
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                        FindForm(),
                        "保存失败!");
                }
            }
            catch (Exception ex)
            {
                //设置错误信息
                LocalCommonServices.ErrorTrace.SetErrorInfo(
                    FindForm(),
                    ex);
            }
        }

        #endregion

        public object[] BeforeParentChanged()
        {
            //EndEdit();
            object[] para = new object[2];
            para[0] = true;
            if (CurrentData == null)
            {
                para[0] = false;
                return para;
            }

            if (CurrentData.IsNew)
            {
                DialogResult dlg = CommonUtility.EnquireIsSaveCurrentDataByNew();
                if (dlg == DialogResult.Yes)
                {
                    if (CurrentData.Validate() == false)
                    {
                        para[0] = false;
                        return para;
                    }

                    RaiseSaved();
                }
                else if (dlg == DialogResult.Cancel)
                {
                    para[0] = false;
                }
                else if (dlg == DialogResult.No)
                {
                    para[0] = true;
                    para[1] = true;
                }
            }
            else if (CurrentData.IsDirty)
            {
                DialogResult dlg = CommonUtility.EnquireIsSaveCurrentDataByUpdated();
                if (dlg == DialogResult.Yes)
                {
                    if (CurrentData.Validate() == false)
                    {
                        para[0] = false;
                        return para;
                    }

                    RaiseSaved();
                }
                else if (dlg == DialogResult.Cancel)
                {
                    para[0] = false;
                    return para;
                }
                else if (dlg == DialogResult.No)
                {
                    para[0] = true;
                }
            }

            return para;
        }

        public void BindingData(object data)
        {
            ConfigureInfo currentData = data as ConfigureInfo;

            if (data == null) { bindingSource1.DataSource = typeof(ConfigureInfo); Enabled = false; }
            else
            {
                bindingSource1.DataSource = data;
                if ((data as ConfigureInfo).IsValid == false) { Enabled = false; ((BaseDataObject)data).EndEdit(); }
                else
                {
                    Enabled = true; ((BaseDataObject)data).BeginEdit();
                }
            }

            if (currentData != null)
            {
                Enabled = true;
                ((BaseDataObject)data).EndEdit();
                if (currentData.SolutionID == Guid.Empty)
                {
                    currentData.EndEdit();
                    currentData.SolutionID = (_solutionList == null && _solutionList.Count == 0) ? Guid.Empty : _solutionList[0].ID;
                }
                //SetCurrencyBySolutionID(currentData.SolutionID);
                ((BaseDataObject)data).BeginEdit();
            }

        }
       
        #region IEditPart接口

        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bindingSource1.DataSource;
            }
            set
            {
                //this.bindingSource1.DataSource = value;
                BindingData(value);
            }
        }

        /// <summary>
        /// 保存完成触发该事件
        /// </summary>
        public override event SavedHandler Saved;

        /// <summary>
        /// 结束编辑
        /// </summary>
        public override void EndEdit()
        {
            Validate();
            bindingSource1.EndEdit();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            if (values != null
                && values.ContainsKey("ConfigureList"))
            {
                ConfigureList configureList = (ConfigureList)values["ConfigureList"];
                if (configureList == null)
                {
                    Enabled = false;
                    return;
                }
                else
                {
                    if (!configureList.IsValid)
                    {
                        Enabled = false;
                    }
                    else
                    {
                        Enabled = true;
                    }
                }

                _flag = true;
                if (configureList.ID != Guid.Empty)
                {
                    ConfigureInfo info = Controller.GetConfigureInfo(configureList.ID);
                    info.IsDirty = false;
                    bindingSource1.DataSource = info;
                    //if (!string.IsNullOrEmpty(configureList.SolutionName) && cmbSolution.EditValue == null)
                    //{
                    //    cmbSolution.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(configureList.SolutionName, System.DBNull.Value));
                    //    cmbSolution.SelectedIndex = 0;
                    //}
                }
                else
                {
                    var data = configureList as ConfigureInfo;
                    if (data != null)
                    {
                        data.IsDirty = false;
                    }

                    bindingSource1.DataSource = data;
                    //this.bindingSource1.DataSource = configureList as ConfigureInfo;
                }

                (bindingSource1.DataSource as ConfigureInfo).BeginEdit();
                _flag = false;
            }
        }

        /// <summary>
        /// 触发保存事件
        /// </summary>
        public override void RaiseSaved()
        {
            SaveData();

            if (Saved != null)
            {
                Saved(DataSource);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            //获取当前对象
            ConfigureInfo currentData = (ConfigureInfo)bindingSource1.DataSource;

            ConfigureSaveRequest saveRequest = new ConfigureSaveRequest()
            {
                ID = currentData.ID,
                CompanyID = currentData.CompanyID,
                CustomerID = currentData.CustomerID,
                StandardCurrencyID = currentData.StandardCurrencyID,
                DefaultCurrencyID = currentData.DefaultCurrencyID,
                SolutionID = currentData.SolutionID,
                IssuePlaceID = currentData.IssuePlaceID,
                ChargingClosingDate = currentData.ChargingClosingdate,
                AccountingClosingDate = currentData.AccountingClosingdate,
                ShortCode = currentData.ShortCode,
                DefaultAgentDescription = currentData.DefaultAgentDescription,
                BLTitleID = currentData.BLTitleID,
                IsVATinvoice = currentData.IsVATinvoice,
                VATFEEID = currentData.VATFeeID,
                VATrateAP = currentData.VATrate,
                CMBNetComUserID = currentData.CMBNetComUserID,
                SaveByID = LocalData.UserInfo.LoginID,
                UpdateDate = currentData.UpdateDate,
            };
            SingleResultData result = Controller.SaveConfigureInfo(saveRequest);
            if (result == null)
            {
                return false;
            }
            else
            {
                //更改当前对象版本号
                currentData.ID = result.ID;
                currentData.UpdateDate = result.UpdateDate;
                currentData.SolutionName = cmbSolution.SelectedItem.ToString();
                currentData.IsDirty = false;
                return true;
            }
        }

        #endregion
    }
}
