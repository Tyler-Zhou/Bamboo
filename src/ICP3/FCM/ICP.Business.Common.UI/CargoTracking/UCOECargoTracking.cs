using DevExpress.XtraEditors;
using HtmlAgilityPack;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.Crawler.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Spire.Pdf;
using Spire.Pdf.HtmlConverter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ICP.Business.Common.UI
{
    public partial class UCOECargoTracking : BaseEditPart, IDataBind
    {
        #region

        private ITerminalService TerminalService
        {
            get
            {
                return ServiceClient.GetService<ITerminalService>();
            }
        }
        private ICrawlerService CrawlerService
        {
            get
            {
                return ServiceClient.GetService<ICrawlerService>();
            }
        }
        private IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }
        private IFCMCommonService OperationService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        public IClientMessageService ClientMessageService
        {
            get
            {
                return ServiceClient.GetClientService<IClientMessageService>();
            }
        }
        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        public WorkItem Workitem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
        #endregion

        #region 全局变量

        private bool isEnglish = LocalData.IsEnglish;
        private static List<ICP.Crawler.ServiceInterface.DataObjects.Terminals> terminalList = new List<ICP.Crawler.ServiceInterface.DataObjects.Terminals>();
        /// <summary>
        /// 上下文
        /// </summary>
        public BusinessOperationContext Context { get; set; }

        public UCOECargoTracking()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                Locale();
            }
        }

        private void GetTerminalList()
        {
            if (DataSource != null)
            {
                string vessel = DataSource.Voyage.Substring(0, DataSource.Voyage.IndexOf('/'));
                terminalList = TerminalService.GeTerminalByContainerList(CurrentCargoTrackingContainer.ContainerID, vessel, DataSource.POLCode, DataSource.ReturnLocID);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public CargoTrackingInfo DataSource
        {
            get { return bsOperationinfo.DataSource as CargoTrackingInfo; }
            set
            {
                bsOperationinfo.Clear();
                containersBindingSource.Clear();
                if (value != null)
                {
                    bsOperationinfo.DataSource = value;
                    bsOperationinfo.ResetBindings(false);
                }
            }
        }

        private string blNO;
        /// <summary>
        /// 主提单号
        /// </summary>
        public string BLNO
        {
            get { return blNO; }
            set
            {
                string str = value.Substring(0, 4).ToUpper();
                if (str == "CHNJ" || str == "CHHK" || str == "UASU" || str == "ANLC" || str == "CMDU" || str == "COSU" || str == "PABV" || str == "MAEU")
                    blNO = value.Substring(4);
                else
                    blNO = value;
            }
        }
        /// <summary>
        /// 用于判断是否同一业务，避免重复加载
        /// </summary>
        public Guid TempOperationID { get; set; }
        /// <summary>
        /// 箱号
        /// </summary>
        public string CNO { get; set; }
        /// <summary>
        /// 离港日
        /// </summary>
        public DateTime? NetETD { get; set; }
        /// <summary>
        /// 码头(根据箱号查询)
        /// </summary>
        public string DOCK { get; set; }
        public string DOCKID { get; set; }
        /// <summary>
        /// 码头 （根据船名航次查询）
        /// </summary>
        public string DOCKForVessel { get; set; }
        /// <summary>
        /// 箱信息
        /// </summary>
        public CargoTrackingContainerInfo CurrentCargoTrackingContainer
        {
            get
            {
                return containersBindingSource.Current as CargoTrackingContainerInfo;
            }
        }
        /// <summary>
        /// 进港
        /// </summary>
        public string CurrentGateIn { get; set; }
        /// <summary>
        /// 装船
        /// </summary>
        public string CurrentLoadship { get; set; }
        /// <summary>
        /// 海关放行
        /// </summary>
        public string CurrentCustomsRelease { get; set; }
        private string vesselIGZPlus;
        /// <summary>
        /// 船名
        /// </summary>
        public string VesselIGZPlus
        {
            get
            {
                return vesselIGZPlus;
            }
            set
            {
                vesselIGZPlus = value.Replace("!20", "+");
            }
        }
        private string vesselIGZ20;
        public string VesselIGZ20
        {
            get
            {
                return vesselIGZ20;
            }
            set
            {
                vesselIGZ20 = value.Replace(" ", "!20");
            }
        }
        /// <summary>
        /// 航次
        /// </summary>
        public string Voyage { get; set; }
        /// <summary>
        /// 需要更新海关放行和进场信息
        /// </summary>
        public bool IsUpdateCustomsAndGateIn { get; set; }

        public string IGZURL { get; set; }

        /// <summary>
        /// 配置信息
        /// </summary>
        public CargoTrackingConfig Config { get; set; }

        private bool loopCNT = true;
        /// <summary>
        /// 是否循环箱，默认是
        /// </summary>
        public bool LoopCNT
        {
            get { return loopCNT; }
            set { loopCNT = value; }
        }


        #endregion

        private void InitControl()
        {
            if (DataSource != null)
            {
                //船公司
                this.txtCarrier.ShowSelectedValue(DataSource.CarrierID, this.DataSource.Carrier);
                //大船
                this.txtVoyage.ShowSelectedValue(DataSource.VoyageID, DataSource.Voyage);
                //驳船
                this.txtPreVoyage.ShowSelectedValue(DataSource.PreVoyageID, DataSource.PreVoyage);

                //船公司
                ICP.FCM.Common.UI.FCMUIUtility.SetEnterToExecuteOnec(txtCarrier, delegate
                {
                    ICPCommUIHelper.BindCustomerList(this.txtCarrier, CustomerType.Carrier);
                });

                this.DataFindClientService.Register(this.txtReturnLoc, CommonFinderConstants.CustoemrFinder,
                    SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                    GetConditionsForReturnLocation,
                    delegate(object inputSouce, object[] resultData)
                    {
                        txtReturnLoc.Tag = this.DataSource.ReturnLocID = (Guid)resultData[0];
                        txtReturnLoc.EditValue = this.DataSource.ReturnLoc = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    },
                    delegate
                    {
                        this.txtReturnLoc.Tag = this.DataSource.ReturnLocID = null;
                        this.txtReturnLoc.EditValue = this.DataSource.ReturnLoc = string.Empty;
                    },
                    ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

                ICP.FCM.Common.UI.FCMUIUtility.SetCustomerTextEditNullValuePrompt(new List<TextEdit>
                    {
                        txtReturnLoc,
                    });

                this.txtReturnLoc.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
            }
        }

        /// <summary>
        /// “还柜地点”是类型为“码头”或“堆场”的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForReturnLocation()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Storage, false);
            conditions.AddWithValue("CustomerType", CustomerType.Terminal, false);
            return conditions;
        }

        private void Locale()
        {
            if (!LocalData.IsDesignMode && isEnglish)
            {
                labelControl14.Text = "OperationNO";
                labelControl15.Text = "SONO";
                labelControl25.Text = "Carrier";
                labelControl17.Text = "PreVoyage";
                labelControl19.Text = "Voyage";
                labelControl21.Text = "CY/RAIL CUT";
                labelControl20.Text = "AES CUT";
                labelControl22.Text = "DOC/SI CUT";
                labelControl24.Text = "AMS CLOS.";
                labelControl26.Text = "Return Loc";
                colMBLNO.Caption = "ContainerNO";
                colStatus.Caption = "Current state";
                colCustomsRelease.Caption = "Customs release";
                colLoadship.Caption = "Loadship";
                colMBLNO.Caption = "MBLNO";
                btnAvailability.Caption = "Terminal Availability";
                colAvailability.Caption = "Terminal Availability";
            }
        }


        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.Context == null) return;
            bsOperationinfo.EndEdit();
            SingleResult sr = OperationService.SaveCargoTrackingInfo(DataSource);
            this.DataSource.OperationID = sr.GetValue<Guid>("ID");
            this.DataSource.UpdateDate = sr.GetValue<DateTime?>("UpdateDate");

            if (Saved != null) Saved(this.DataSource);
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
            lblETD.Visible = false;
        }

        #region IDataBind 成员

        public void ControlsReadOnly(bool flg)
        {
        }

        private BaseTracking BaseTracking = new BaseTracking();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void InitDataSource()
        {
            IsUpdateCustomsAndGateIn = false;
            //int threadID = 0;
            //if (threadID != 0)
            //    ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(threadID);
            //threadID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(isEnglish ? "Loading……" : "正在加载海关放行&&码头动态……");
            try
            {
                string xmlPath = Application.StartupPath + "\\BusinessTemplates\\CargoTrackingConfig.xml";
                CargoTrackingInfo cargoTrackingInfo = null;
                if (Context.OperationID != null && Context.OperationID != Guid.Empty)
                {
                    //处理数据，从网络中拉取（进港日，装船，海关放行,截柜日cy，etd，eta，）
                    cargoTrackingInfo = OperationService.GetCargoTrackingInfo(Context.OperationID);
                    List<CargoTrackingContainerInfo> ContainerList = null;
                    if (cargoTrackingInfo != null)
                    {
                        ContainerList = cargoTrackingInfo.Containers;
                        NetETD = cargoTrackingInfo.NetETD;
                    }
                    if (ContainerList != null && ContainerList.Count > 0)
                    {
                        //根据操作口岸获取配置信息
                        object obj;
                        Context.TryGetValue("CompanyID", out obj);
                        Guid companyID = new Guid(obj.ToString());
                        if (Config == null || Config.ID != companyID.ToString())
                            Config = ReadXML(xmlPath, "/CargoTracks", companyID.ToString());
                        if (Config != null)
                        {
                            #region IGZ船名/航次

                            string vv = cargoTrackingInfo.Voyage;
                            if (vv.Length <= 0)
                            {
                                cargoTrackingInfo.Containers = ContainerList;
                                DataSource = cargoTrackingInfo;
                                //if (threadID != 0)
                                //    ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(threadID);
                                return;
                            }
                            VesselIGZ20 = vv.Substring(0, vv.IndexOf("/")).Trim();
                            VesselIGZPlus = VesselIGZ20;
                            //航次
                            Voyage = vv.Substring(vv.IndexOf("/") + 1).Replace("V.", "").Trim();
                            if (Voyage.Length > 5)
                                if (Voyage.IndexOf('-') > 0)
                                    Voyage = Voyage.Substring(Voyage.IndexOf('-') + 1).Trim();
                                else
                                    Voyage = Voyage.Substring(0, 5).Trim();

                            #endregion

                            #region etd和DOCK

                            ////上海
                            //if (companyID.ToString().ToLower() == "b13fac2d-8250-4990-a622-5eca00D3a030".ToLower())
                            //{
                            //    GetSHETDAndDOCK();
                            //}
                            ////宁波
                            //else if (companyID.ToString().ToLower() == "a62a9f8e-e69c-4e6e-ad85-e75aed3c6cf9".ToLower())
                            //{
                            //    GetNBETDAndDOCK();
                            //}
                            ////深圳
                            //else if (companyID.ToString().ToLower() == "41D7D3FE-183A-41CD-A725-EB6F728541EC".ToLower())
                            //{
                            //    //盐田
                            //    if (cargoTrackingInfo.POLCode == "CNYTN" && (string.IsNullOrEmpty(cargoTrackingInfo.Containers[0].CustomsRelease) || NetETD == null))
                            //        GetSZYTALL(cargoTrackingInfo);
                            //}

                            #endregion
                            //if (LoopCNT)
                            //    foreach (CargoTrackingContainerInfo ctn in ContainerList)
                            //    {
                            //        if (!ctn.IsUpdate)
                            //            continue;
                            //        CurrentCustomsRelease = ctn.CustomsRelease.Trim();
                            //        CurrentLoadship = ctn.Loadship.Trim();
                            //        CurrentGateIn = ctn.GateIn.Trim();
                            //        string[] str = ctn.CustomsRelease.Trim().Split(' ');
                            //        if (str.Length > 1)
                            //            DOCK = str[1];
                            //        /******查询海关放行**/
                            //        CNO = ctn.ContainerNO;
                            //        BLNO = ctn.MBLNO;
                            //        if (string.IsNullOrEmpty(CNO))
                            //            break;
                            //        if (ctn.IsUpdate) IsUpdateCustomsAndGateIn = true;
                            //        #region 海关放行/重箱进场/确认装船
                            //        //上海
                            //        if (companyID.ToString().ToLower() == "b13fac2d-8250-4990-a622-5eca00D3a030".ToLower())
                            //        {
                            //            GetCustomsRelease(ctn);
                            //            GetGateInAndLoadship(ctn);
                            //            FormatLoadship(CurrentGateIn, "yy-MM-dd HH:mm", CurrentLoadship, string.Empty);
                            //        }
                            //        //宁波
                            //        else if (companyID.ToString().ToLower() == "a62a9f8e-e69c-4e6e-ad85-e75aed3c6cf9".ToLower())
                            //        {
                            //            GetNBCustomsRelease(ctn);
                            //            GetNBGateInAndLoadship(ctn);
                            //            FormatLoadship(CurrentGateIn, CurrentGateIn.Length == 14 ? "yy-MM-dd HH:mm" : string.Empty, CurrentLoadship, string.Empty);
                            //        }
                            //        //青岛
                            //        else if (companyID.ToString().ToLower() == "F289109A-C29E-4B0B-A41A-C22D9E70A72F".ToLower())
                            //        {
                            //            GetQDCustomsReleaseAndGateInLoadship(ctn);
                            //        }
                            //        //天津
                            //        else if (companyID.ToString().ToLower() == "D8D57403-D663-4A93-A927-144907B7963B".ToLower())
                            //        {
                            //            GetTJCustomsRelease(ctn);
                            //            GetTJGateInAndLoadship(ctn);
                            //        }
                            //        //大连
                            //        else if (companyID.ToString().ToLower() == "B1AFAD8F-55DD-4E29-A250-EB82AB3971FE".ToLower())
                            //        {
                            //            GetDLCustomsRelease(ctn);
                            //        }
                            //        //深圳
                            //        else if (companyID.ToString().ToLower() == "41D7D3FE-183A-41CD-A725-EB6F728541EC".ToLower())
                            //        {
                            //            //赤湾
                            //            if (cargoTrackingInfo.POLCode == "CNCWN")
                            //                GetSZCWGateInAndETD(ctn, cargoTrackingInfo.POLCode, cargoTrackingInfo.SONO);
                            //            //蛇口
                            //            else if (cargoTrackingInfo.POLCode == "CNSHK")
                            //                GetSZSKGateInAndETD(ctn);
                            //        }
                            //        #endregion
                            //        ctn.CustomsRelease = CurrentCustomsRelease.Trim();
                            //        ctn.GateIn = CurrentGateIn;
                            //        ctn.Loadship = CurrentLoadship;
                            //        if (!string.IsNullOrEmpty(CurrentLoadship.Trim()) && !string.IsNullOrEmpty(CurrentGateIn.Trim()))
                            //            ctn.IsUpdate = false;
                            //    }
                            //IGZURL = Config.IGZPath;
                            #region 开/截港日

                            //if (cargoTrackingInfo.OpenPort == null && cargoTrackingInfo.ClosePort == null)
                            //{
                            //    //上海
                            //    if (companyID.ToString().ToLower() == "b13fac2d-8250-4990-a622-5eca00D3a030".ToLower())
                            //    {
                            //        GetOpenAndClosePort(cargoTrackingInfo);
                            //    }
                            //    //宁波GetNBETDAndDOCK
                            //    else if (companyID.ToString().ToLower() == "a62a9f8e-e69c-4e6e-ad85-e75aed3c6cf9".ToLower())
                            //    {
                            //        if (cargoTrackingInfo.OpenPort == null || cargoTrackingInfo.ClosePort == null)
                            //            GetNBOpenAndClosePort(cargoTrackingInfo);
                            //    }
                            //}

                            #endregion
                        }
                    }
                    if (cargoTrackingInfo != null)
                    {
                        cargoTrackingInfo.Containers = ContainerList;
                        DataSource = cargoTrackingInfo;
                        //保存开截港日
                        SaveOpenPortAndClosePort(cargoTrackingInfo);
                    }
                }
                //ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(threadID);
                //更新
                if (IsUpdateCustomsAndGateIn)
                {
                    CargoTrackingInfo cti = bsOperationinfo.DataSource as CargoTrackingInfo;
                    if (cti != null)
                    {
                        List<CargoTrackingContainerInfo> savelist = cti.Containers;
                        OperationService.SaveCustomsAndGateIn(savelist, LocalData.UserInfo.LoginID);
                    }
                }

                #region ETD操作

                if (NetETD != null)
                {
                    NetETD = Convert.ToDateTime(NetETD.ToString().Substring(0, NetETD.ToString().IndexOf(' ')));
                    if (cargoTrackingInfo.ETD != NetETD)
                    {
                        lblETD.Visible = true;
                        txtETD.ForeColor = Color.Red;
                        lblETD.ToolTip = string.Format("ETD错误！\n点击此处改变ETD为{0}后，请保存！", NetETD);
                    }
                }

                #endregion
            }
            catch (Exception e)
            {
                //ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(threadID);
                throw e;
            }
        }
        /// <summary>
        /// 深圳蛇口
        /// </summary>
        /// <param name="ctn"></param>
        private void GetSZSKGateInAndETD(CargoTrackingContainerInfo ctn)
        {
            if (string.IsNullOrEmpty(ctn.GateIn.Trim()) || string.IsNullOrEmpty(NetETD.ToString()))
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                string time = DateTime.Now.ToString("yyyyMMddHHmmss");
                string geturl = "http://iport.sctcn.com/oi/action?SSOToken=portal%7EUFVCTElD%7E" + time + "%7EehiQ%2Bj5mLTUxXNhgP9q5Ig%3D%3D&ParameterType=ContainerInfo&ParameterValue=&LocaleCode=zhs&moduleName=oi&logout=&loginUrl=";
                string posturl = "http://iport.sctcn.com/oi/action?SSOToken=portal%7EUFVCTElD%7E20140108090110%7EehiQ%2Bj5mLTUxXNhgP9q5Ig%3D%3D&ParameterType=ContainerInfo&ParameterValue=&LocaleCode=zhs&moduleName=oi&logout=&loginUrl=";
                PostData = "_SERAILIZEBEAN_queryResult=rO0ABXNyAC9jb20uc2N0Y24uaXBvcnQub2kud2ViLmZvcm1iZWFuLlF1ZXJ5UmVzdWx0QmVhbq4F%0D%0AvohRN9m1AgAFSgAKZnVuY3Rpb25JZEwADGRhdGFHcmlkSW5mb3QAEExqYXZhL3V0aWwvTGlzdDtM%0D%0AAA9xdWVyeVJlc3VsdFNldHNxAH4AAUwACXNoaXBEaXZJZHQAEkxqYXZhL2xhbmcvU3RyaW5nO0wA%0D%0ACnNoaXBJZExpc3RxAH4AAnhwAAAAAAAAAA9zcgATamF2YS51dGlsLkFycmF5TGlzdHiB0h2Zx2Gd%0D%0AAwABSQAEc2l6ZXhwAAAABHcEAAAACnNyAC1jb20uc2N0Y24uZnJhbWV3b3JrLndlYi5kYXRhZ3Jp%0D%0AZC5EYXRhR3JpZEluZm%2FvObF79iAuqQIAB0kACHJvd0NvdW50WwANZnVuY3Rpb25Hcm91cHQAE1tM%0D%0AamF2YS9sYW5nL1N0cmluZztMAAtwYWdlU2V0dGluZ3QALkxjb20vc2N0Y24vZnJhbWV3b3JrL3dl%0D%0AYi9kYXRhZ3JpZC9QYWdlU2V0dGluZztbAAtyb3dFZGl0YWJsZXQAAltaWwAQcm93SXNQdWJsaWNR%0D%0AdWVyeXEAfgAJWwALcm93U2VsZWN0ZWRxAH4ACUwAC3NvcnRTZXR0aW5ndAAuTGNvbS9zY3Rjbi9m%0D%0AcmFtZXdvcmsvd2ViL2RhdGFncmlkL1NvcnRTZXR0aW5nO3hwAAAAAXVyABNbTGphdmEubGFuZy5T%0D%0AdHJpbmc7rdJW5%2Bkde0cCAAB4cAAAAAFwc3IALGNvbS5zY3Rjbi5mcmFtZXdvcmsud2ViLmRhdGFn%0D%0AcmlkLlBhZ2VTZXR0aW5nk%2BgHVIb0d9wCAARJAAtjdXJyZW50UGFnZUkACHBhZ2VTaXplWgAIcGFn%0D%0AZWFibGVKAAtyZWNvcmRDb3VudHhwAAAAAQAAABkAAAAAAAAAAAF1cgACW1pXjyA5FLhd4gIAAHhw%0D%0AAAAAAQB1cQB%2BABAAAAABAHVxAH4AEAAAAAEAc3IALGNvbS5zY3Rjbi5mcmFtZXdvcmsud2ViLmRh%0D%0AdGFncmlkLlNvcnRTZXR0aW5naVwwRQK1w74CAAJaAANhc2NMAApzb3J0Q29sdW1ucQB%2BAAJ4cABw%0D%0Ac3EAfgAGAAAAAHVxAH4ADAAAAABzcQB%2BAA4AAAABAAAAGQAAAAAAAAAAAHVxAH4AEAAAAAB1cQB%2B%0D%0AABAAAAAAdXEAfgAQAAAAAHNxAH4AFABwc3EAfgAGAAAADnVxAH4ADAAAAA5wcHBwcHBwcHBwcHBw%0D%0AcHNxAH4ADgAAAAEAAAAZAAAAAAAAAAAOdXEAfgAQAAAADgAAAAAAAAAAAAAAAAAAdXEAfgAQAAAA%0D%0ADgAAAAAAAAAAAAAAAAAAdXEAfgAQAAAADgAAAAAAAAAAAAAAAAAAc3EAfgAUAHBzcQB%2BAAYAAAAA%0D%0AdXEAfgAMAAAAAHNxAH4ADgAAAAEAAAAAAAAAAAAAAAAAdXEAfgAQAAAAAHVxAH4AEAAAAAB1cQB%2B%0D%0AABAAAAAAc3EAfgAUAHB4c3EAfgAEAAAABHcEAAAACnNyAC1jb20uc2N0Y24uZnJhbWV3b3JrLnF1%0D%0AZXJ5LkJhc2ljUXVlcnlSZXN1bHRTZXRI77Dp5fhNnQIABEwACmF0dHJpYnV0ZXNxAH4AAUwAEWNv%0D%0AbHVtbkRlc2NyaXB0b3JzcQB%2BAAFMAAhjcml0ZXJpYXQAKUxjb20vc2N0Y24vZnJhbWV3b3JrL3F1%0D%0AZXJ5L1F1ZXJ5Q3JpdGVyaWE7WwAEZGF0YXQAFFtbTGphdmEvbGFuZy9PYmplY3Q7eHBzcQB%2BAAQA%0D%0AAAAAdwQAAAAKeHNxAH4ABAAAACl3BAAAADpzcgAqY29tLnNjdGNuLmZyYW1ld29yay5xdWVyeS5D%0D%0Ab2x1bW5EZXNjcmlwdG9yZ4iS1O7SkuICAAlJAApJbmRleEluU3FsSQALZGlzcGxheVNpemVKAAJp%0D%0AZEkABWluZGV4TAAUYXNzb2NpYXRlZENvbHVtbk5hbWVxAH4AAkwAFWFzc29jaWF0ZWRDb2x1bW5W%0D%0AYWx1ZXEAfgACTAANZ3JvdXBGdW5jdGlvbnQAOkxjb20vc2N0Y24vZnJhbWV3b3JrL3F1ZXJ5L0Nv%0D%0AbHVtbkRlc2NyaXB0b3IkR3JvdXBGdW5jdGlvbjtMAAV2YWx1ZXQAEkxqYXZhL2xhbmcvT2JqZWN0%0D%0AO0wADnZpc2liaWxpdHlNb2RldAA7TGNvbS9zY3Rjbi9mcmFtZXdvcmsvcXVlcnkvQ29sdW1uRGVz%0D%0AY3JpcHRvciRWaXNpYmlsaXR5TW9kZTt4cgAqY29tLnNjdGNuLmZyYW1ld29yay5jb25maWcuUGVy%0D%0Ac2lzdFByb3BlcnR5cOKdxDDJPlUCAAlaAAptdWxpdFZhbHVlTAAOY29tcG9uZW50Q2xhc3N0ABFM%0D%0AamF2YS9sYW5nL0NsYXNzO0wAEmNvbXBvbmVudENsYXNzTmFtZXEAfgACTAALZm9ybWF0U3R5bGVx%0D%0AAH4AAkwABG5hbWVxAH4AAkwACnNlcmlhbGl6ZXJ0ACpMY29tL3NjdGNuL2ZyYW1ld29yay9zZXJp%0D%0AYWxpemUvU2VyaWFsaXplcjtMAAl0eXBlQ2xhc3NxAH4AN0wABXZhbHVlcQB%2BADRMAAl2YWx1ZVRl%0D%0AeHRxAH4AAnhwAHZyABBqYXZhLmxhbmcuU3RyaW5noPCkOHo7s0ICAAB4cHQAEGphdmEubGFuZy5T%0D%0AdHJpbmdwdAAGRVFfTkJSc3IAL2NvbS5zY3Rjbi5mcmFtZXdvcmsuc2VyaWFsaXplLkRlZmF1bHRT%0D%0AZXJpYWxpemVyxLndQvQBbGECAAJMAApzZXJpYWxpemVycQB%2BADhMABBzaW1wbGVTZXJpYWxpemVy%0D%0AdAA4TGNvbS9zY3Rjbi9mcmFtZXdvcmsvc2VyaWFsaXplL1NpbXBsZVByb3BlcnR5U2VyaWFsaXpl%0D%0Acjt4cHNyADFjb20uc2N0Y24uZnJhbWV3b3JrLnNlcmlhbGl6ZS5qc29uLkpTT05TZXJpYWxpemVy%0D%0A867QUrN0UkwCAAB4cHNyADZjb20uc2N0Y24uZnJhbWV3b3JrLnNlcmlhbGl6ZS5TaW1wbGVQcm9w%0D%0AZXJ0eVNlcmlhbGl6ZXJMRfJ9ozWKDwIAAHhwcQB%2BADtwcAAAAAEAAAAMAAAAAAAAbkkAAAAAcHB%2B%0D%0AcgA4Y29tLnNjdGNuLmZyYW1ld29yay5xdWVyeS5Db2x1bW5EZXNjcmlwdG9yJEdyb3VwRnVuY3Rp%0D%0Ab24AAAAAAAAAABIAAHhyAA5qYXZhLmxhbmcuRW51bQAAAAAAAAAAEgAAeHB0AAVDT1VOVHB%2BcgA5%0D%0AY29tLnNjdGNuLmZyYW1ld29yay5xdWVyeS5Db2x1bW5EZXNjcmlwdG9yJFZpc2liaWxpdHlNb2Rl%0D%0AAAAAAAAAAAASAAB4cQB%2BAEZ0AA9ERUZBVUxUX1ZJU0lCTEVzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxh%0D%0AbmcuU3RyaW5ncHQAB0xJTkVfSURxAH4AQHEAfgA7cHAAAAACAAAABAAAAAAAAG5KAAAAAXBwcQB%2B%0D%0AAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQABlNaVFBIVHEAfgBAcQB%2B%0D%0AADtwcAAAAAMAAAAJAAAAAAAAbksAAAACcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEu%0D%0AbGFuZy5TdHJpbmdwdAAISVNPX0NPREVxAH4AQHEAfgA7cHAAAAAEAAAABAAAAAAAAG5MAAAAA3Bw%0D%0AcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQAB0RBTUFHRURxAH4A%0D%0AQHEAfgA7cHAAAAAFAAAABAAAAAAAAG5NAAAABHBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBq%0D%0AYXZhLmxhbmcuU3RyaW5ncHQACENBVEVHT1JZcQB%2BAEBxAH4AO3BwAAAABgAAABoAAAAAAABuTgAA%0D%0AAAVwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0AAZTVEFUVVNx%0D%0AAH4AQHEAfgA7cHAAAAAHAAAABQAAAAAAAG5PAAAABnBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0%0D%0AABBqYXZhLmxhbmcuU3RyaW5ncHQADlJFTEVBU0VfU1RBVFVTcQB%2BAEBxAH4AO3BwAAAACAAAAAgA%0D%0AAAAAAABuUAAAAAdwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0%0D%0AAAhMT0NBVElPTnEAfgBAcQB%2BADtwcAAAAAkAAAAeAAAAAAAAblEAAAAIcHBxAH4AR3BxAH4ASnNx%0D%0AAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAAJSEFaX0NMQVNTcQB%2BAEBxAH4AO3BwAAAA%0D%0ACgAAAAQAAAAAAABuUgAAAAlwcHEAfgBHcHEAfgBKc3EAfgAyAHZyABBqYXZhLmxhbmcuRG91Ymxl%0D%0AgLPCSilr%2BwQCAAFEAAV2YWx1ZXhyABBqYXZhLmxhbmcuTnVtYmVyhqyVHQuU4IsCAAB4cHQAEGph%0D%0AdmEubGFuZy5Eb3VibGVwdAAEVU5ER3EAfgBAcQB%2BAGpwcAAAAAsAAAAWAAAAAAAAblMAAAAKcHBx%0D%0AAH4AR3BxAH4ASnNxAH4AMgBxAH4AanQAEGphdmEubGFuZy5Eb3VibGVwdAANVEVNUF9SRVFVSVJF%0D%0ARHEAfgBAcQB%2BAGpwcAAAAAwAAAAWAAAAAAAAblQAAAALcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4A%0D%0AanQAEGphdmEubGFuZy5Eb3VibGVwdAAMR1JPU1NfV0VJR0hUcQB%2BAEBxAH4AanBwAAAADQAAABYA%0D%0AAAAAAABuVQAAAAxwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0%0D%0AAAlQT0xfQUxJQVNxAH4AQHEAfgA7cHAAAAAOAAAAHgAAAAAAAG5WAAAADXBwcQB%2BAEdwcQB%2BAEpz%0D%0AcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQACVBPRF9BTElBU3EAfgBAcQB%2BADtwcAAA%0D%0AAA8AAAAeAAAAAAAAblcAAAAOcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFuZy5T%0D%0AdHJpbmdwdAALREVTVElOQVRJT05xAH4AQHEAfgA7cHAAAAAQAAAAFAAAAAAAAG5YAAAAD3BwcQB%2B%0D%0AAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQADEFSUl9MT0NfVFlQRXEA%0D%0AfgBAcQB%2BADtwcAAAABEAAAAEAAAAAAAAblkAAAAQcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QA%0D%0AEGphdmEubGFuZy5TdHJpbmdwdAARQVJSX1ZFU1NFTF9WT1lBR0VxAH4AQHEAfgA7cHAAAAASAAAA%0D%0AFwAAAAAAAG5aAAAAEXBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5n%0D%0AcHQADUFSUl9UUlVDS19OQlJxAH4AQHEAfgA7cHAAAAATAAAADAAAAAAAAG5bAAAAEnBwcQB%2BAEdw%0D%0AcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQAE0FSUl9WRVNTRUxfUE9TSVRJ%0D%0AT05xAH4AQHEAfgA7cHAAAAAUAAAADAAAAAAAAG5cAAAAE3BwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2B%0D%0AADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQADERFUF9MT0NfVFlQRXEAfgBAcQB%2BADtwcAAAABUAAAAE%0D%0AAAAAAAAAbl0AAAAUcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdw%0D%0AdAARREVQX1ZFU1NFTF9WT1lBR0VxAH4AQHEAfgA7cHAAAAAWAAAAFwAAAAAAAG5eAAAAFXBwcQB%2B%0D%0AAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQADURFUF9UUlVDS19OQlJx%0D%0AAH4AQHEAfgA7cHAAAAAXAAAADAAAAAAAAG5fAAAAFnBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0%0D%0AABBqYXZhLmxhbmcuU3RyaW5ncHQAE0RFUF9WRVNTRUxfUE9TSVRJT05xAH4AQHEAfgA7cHAAAAAY%0D%0AAAAADAAAAAAAAG5gAAAAF3BwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3Ry%0D%0AaW5ncHQAC0lOX0xPQ19UWVBFcQB%2BAEBxAH4AO3BwAAAAGQAAAAQAAAAAAABuYQAAABhwcHEAfgBH%0D%0AcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0ABBJTl9WRVNTRUxfVk9ZQUdF%0D%0AcQB%2BAEBxAH4AO3BwAAAAGgAAABcAAAAAAABuYgAAABlwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7%0D%0AdAAQamF2YS5sYW5nLlN0cmluZ3B0AAxJTl9UUlVDS19OQlJxAH4AQHEAfgA7cHAAAAAbAAAADAAA%0D%0AAAAAAG5jAAAAGnBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQA%0D%0AEklOX1ZFU1NFTF9QT1NJVElPTnEAfgBAcQB%2BADtwcAAAABwAAAAMAAAAAAAAbmQAAAAbcHBxAH4A%0D%0AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAAHSU5fVElNRXEAfgBAcQB%2B%0D%0AADtwcAAAAB0AAAAQAAAAAAAAbmUAAAAccHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEu%0D%0AbGFuZy5TdHJpbmdwdAAMT1VUX0xPQ19UWVBFcQB%2BAEBxAH4AO3BwAAAAHgAAAAQAAAAAAABuZgAA%0D%0AAB1wcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0ABFPVVRfVkVT%0D%0AU0VMX1ZPWUFHRXEAfgBAcQB%2BADtwcAAAAB8AAAAXAAAAAAAAbmcAAAAecHBxAH4AR3BxAH4ASnNx%0D%0AAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAANT1VUX1RSVUNLX05CUnEAfgBAcQB%2BADtw%0D%0AcAAAACAAAAAMAAAAAAAAbmgAAAAfcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFu%0D%0AZy5TdHJpbmdwdAATT1VUX1ZFU1NFTF9QT1NJVElPTnEAfgBAcQB%2BADtwcAAAACEAAAAMAAAAAAAA%0D%0AbmkAAAAgcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAAIT1VU%0D%0AX1RJTUVxAH4AQHEAfgA7cHAAAAAiAAAAEAAAAAAAAG5qAAAAIXBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIA%0D%0AcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQACVNFQUxfTkJSMXEAfgBAcQB%2BADtwcAAAACMAAAAP%0D%0AAAAAAAAAbmsAAAAicHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdw%0D%0AdAAJU0VBTF9OQlIycQB%2BAEBxAH4AO3BwAAAAJAAAAA8AAAAAAABubAAAACNwcHEAfgBHcHEAfgBK%0D%0Ac3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0AANPT0dxAH4AQHEAfgA7cHAAAAAlAAAA%0D%0AHgAAAAAAAG5tAAAAJHBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5n%0D%0AcHQAC0JPT0tJTkdfRURPcQB%2BAEBxAH4AO3BwAAAAJgAAABEAAAAAAABubgAAACVwcHEAfgBHcHEA%0D%0AfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0AAhHUk9VUF9JRHEAfgBAcQB%2BADtw%0D%0AcAAAACcAAAAJAAAAAAAAbm8AAAAmcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFu%0D%0AZy5TdHJpbmdwdAAHQ0lDX1BPU3EAfgBAcQB%2BADtwcAAAACgAAAAEAAAAAAAAbnAAAAAncHBxAH4A%0D%0AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAAIU0hBTkRfSURxAH4AQHEA%0D%0AfgA7cHAAAAApAAAABAAAAAAAAG5xAAAAKHBwcQB%2BAEdwcQB%2BAEp4c3IALGNvbS5zY3Rjbi5mcmFt%0D%0AZXdvcmsucXVlcnkuQmFzaWNRdWVyeUNyaXRlcmlhEwMHOie11owCAAVMAAtwYWdlU2V0dGluZ3EA%0D%0AfgAITAANcGFyYW1ldGVyQmVhbnQAKExjb20vc2N0Y24vZnJhbWV3b3JrL2JlYW5zL1Byb3BlcnR5%0D%0AQmVhbjtMAAlxdWVyeU5hbWVxAH4AAkwADHF1ZXJ5U2V0dGluZ3QAKExjb20vc2N0Y24vZnJhbWV3%0D%0Ab3JrL3F1ZXJ5L1F1ZXJ5U2V0dGluZztMAAtzb3J0U2V0dGluZ3EAfgAKeHBxAH4AD3NyACZjb20u%0D%0Ac2N0Y24uZnJhbWV3b3JrLmJlYW5zLlByb3BlcnR5QmVhbtKVu%2B%2BdM38yAgACTAAKc2VyaWFsaXpl%0D%0AcnEAfgA4TAAIdmFsdWVNYXB0ABNMamF2YS91dGlsL0hhc2hNYXA7eHBzcQB%2BAENzcgARamF2YS51%0D%0AdGlsLkhhc2hNYXAFB9rBwxZg0QMAAkYACmxvYWRGYWN0b3JJAAl0aHJlc2hvbGR4cD9AAAAAAAAM%0D%0AdwgAAAAQAAAAAXQADENvbnRhaW5lciBOb3NyAC1jb20uc2N0Y24uZnJhbWV3b3JrLnF1ZXJ5LlBh%0D%0AcmFtZXRlckRlc2NyaXB0b3IX3lJusPFfIAIACkoAAmlkSQAFaW5kZXhaAAhudWxsYWJsZUwADmF2%0D%0AYWlsYWJsZUl0ZW1zcQB%2BAAFMAAtpbnB1dE1ldGhvZHQAO0xjb20vc2N0Y24vZnJhbWV3b3JrL3F1%0D%0AZXJ5L1BhcmFtZXRlckRlc2NyaXB0b3IkSW5wdXRNZXRob2Q7TAAJaW5wdXRNb2RldAA5TGNvbS9z%0D%0AY3Rjbi9mcmFtZXdvcmsvcXVlcnkvUGFyYW1ldGVyRGVzY3JpcHRvciRJbnB1dE1vZGU7TAAQaXRl%0D%0AbVByb3ZpZGVyTmFtZXEAfgACTAAQaXRlbVByb3ZpZGVyVHlwZXQAQExjb20vc2N0Y24vZnJhbWV3%0D%0Ab3JrL3F1ZXJ5L1BhcmFtZXRlckRlc2NyaXB0b3IkSXRlbVByb3ZpZGVyVHlwZTtMAAZyZW1hcmtx%0D%0AAH4AAkwAFHZhbGlkYXRpb25FeHByZXNzaW9ucQB%2BAAJ4cQB%2BADYAcQB%2BADt0ABBqYXZhLmxhbmcu%0D%0AU3RyaW5ncHEAfgDRc3EAfgA%2Bc3EAfgBBc3EAfgBDcQB%2BADt0AAtDQ0xVMzE2ODIyMXEAfgDbAAAA%0D%0AAAAAEu8AAAAAAHB%2BcgA5Y29tLnNjdGNuLmZyYW1ld29yay5xdWVyeS5QYXJhbWV0ZXJEZXNjcmlw%0D%0AdG9yJElucHV0TWV0aG9kAAAAAAAAAAASAAB4cQB%2BAEZ0AARURVhUfnIAN2NvbS5zY3Rjbi5mcmFt%0D%0AZXdvcmsucXVlcnkuUGFyYW1ldGVyRGVzY3JpcHRvciRJbnB1dE1vZGUAAAAAAAAAABIAAHhxAH4A%0D%0ARnQAClVTRVJfSU5QVVRwcHQAASp0ABhjb21tb25DaGFyQEBtYXhMZW5ndGg6MTN4dAANQ29udGFp%0D%0AbmVySW5mb3BxAH4AFXVyABRbW0xqYXZhLmxhbmcuT2JqZWN0Oxi%2F%2B1Pka9vKAgAAeHAAAAABdXIA%0D%0AE1tMamF2YS5sYW5nLk9iamVjdDuQzlifEHMpbAIAAHhwAAAAKXQAC0NDTFUzMTY4MjIxdAADQ1ND%0D%0AdAAIMjAvR1AvODZ0AAQyMkcxcHQABkV4cG9ydHQABEZ1bGx0AAhSRUxFQVNFRHQAElZlc3NlbCBD%0D%0ATEVIIDQ3MTUxMnBwcHNxAH4AaEA9T1wo9cKPdAAFQ05TSEt0AAVFR1BSU3QABUVHQUxZdAABVHB0%0D%0AAAlZR0RCNjE4NThwdAABVnQACkNMRUgvMTM1MldwdAAGNDcxNTEydAABVHB0AAlZR0RCNjE4NThw%0D%0AdAAQMjAxMy0xMi0yOSAxMTozNXQAAVZ0AApDTEVILzEzNTJXcHQABjQ3MTUxMnQAEDIwMTQtMDEt%0D%0AMDcgMTQ6NTN0AAdSOTI4MTE5cHB0AAxTSEtBTFkxMDQyMjRwdAABTnBzcQB%2BACxzcQB%2BAAQAAAAA%0D%0AdwQAAAAKeHNxAH4ABAAAAAZ3BAAAAAZzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQA%0D%0AA0lNT3NxAH4APnNxAH4AQXNxAH4AQ3BwcAAAAAEAAAAZAAAAAAAAe%2F0AAAAAcHBxAH4AR3BxAH4A%0D%0ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAASSU5fQlVTSU5FU1NfVk9ZQUdFcQB%2B%0D%0AAQpwcHAAAAACAAAAEQAAAAAAAHv%2BAAAAAXBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZh%0D%0ALmxhbmcuU3RyaW5ncHQAE09VVF9CVVNJTkVTU19WT1lBR0VxAH4BCnBwcAAAAAMAAAARAAAAAAAA%0D%0Ae%2F8AAAACcHBxAH4AR3BxAH4ASnNxAH4AMgB2cgAOamF2YS51dGlsLkRhdGVoaoEBS1l0GQMAAHhw%0D%0AdAAOamF2YS51dGlsLkRhdGVwdAADQ0lRcQB%2BAQpwcHAAAAAEAAAABwAAAAAAAHwAAAAABHBwcQB%2B%0D%0AAEdwcQB%2BAEpzcQB%2BADIAcQB%2BARV0AA5qYXZhLnV0aWwuRGF0ZXB0AANDVVNxAH4BCnBwcAAAAAUA%0D%0AAAAHAAAAAAAAfAEAAAAFcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4BFXQADmphdmEudXRpbC5EYXRl%0D%0AcHQAB1ZPVUNIRVJxAH4BCnBwcAAAAAYAAAAHAAAAAAAAfAIAAAAGcHBxAH4AR3BxAH4ASnhzcQB%2B%0D%0AAMdxAH4AGHNxAH4Ay3NxAH4AQ3NxAH4Azz9AAAAAAAAMdwgAAAAQAAAAAXQADENvbnRhaW5lciBO%0D%0Ab3NxAH4A0gBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwcQB%2BASJzcQB%2BAD5zcQB%2BAEFzcQB%2BAENx%0D%0AAH4AO3QAC0NDTFUzMTY4MjIxcQB%2BASgAAAAAAAAS7wAAAAAAcHEAfgDdcQB%2BAOBwcHQAASp0ABhj%0D%0Ab21tb25DaGFyQEBtYXhMZW5ndGg6MTN4dAALUmVsZWFzZUluZm9zcgAmY29tLnNjdGNuLmZyYW1l%0D%0Ad29yay5xdWVyeS5RdWVyeVNldHRpbmfxMjQAamWdXAIAAlsAEWRldGFpbENvbHVtbk5hbWVzcQB%2B%0D%0AAAdbABJzdW1tYXJ5Q29sdW1uTmFtZXNxAH4AB3hwdXEAfgAMAAAAAHBxAH4AHHBzcQB%2BACxzcQB%2B%0D%0AAAQAAAAAdwQAAAAKeHNxAH4ABAAAAAZ3BAAAAApzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3Ry%0D%0AaW5ncHQAB0xJTkVfSURzcQB%2BAD5zcQB%2BAEFzcQB%2BAENxAH4AO3BwAAAAAQAAAAYAAAAAAABN5gAA%0D%0AAABwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0AAlQRVJGT1JN%0D%0ARURxAH4BNXEAfgA7cHAAAAACAAAAEAAAAAAAAE3nAAAAAXBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2B%0D%0AADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQACFRTRVJWX0lEcQB%2BATVxAH4AO3BwAAAAAwAAAAoAAAAA%0D%0AAABN6AAAAAJwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0AAhG%0D%0AUk9NX0xPQ3EAfgE1cQB%2BADtwcAAAAAQAAAAKAAAAAAAATekAAAADcHBxAH4AR3BxAH4ASnNxAH4A%0D%0AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAAGVE9fTE9DcQB%2BATVxAH4AO3BwAAAABQAAAAoA%0D%0AAAAAAABN6gAAAARwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0%0D%0AAAVOT1RFU3EAfgE1cQB%2BADtwcAAAAAYAAAAUAAAAAAAATesAAAAFcHBxAH4AR3BxAH4ASnhzcQB%2B%0D%0AAMdxAH4AH3NxAH4Ay3NxAH4AQ3NxAH4Azz9AAAAAAAAMdwgAAAAQAAAAAXQADENvbnRhaW5lciBO%0D%0Ab3NxAH4A0gBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwcQB%2BAUtzcQB%2BAD5zcQB%2BAEFzcQB%2BAENx%0D%0AAH4AO3QAC0NDTFUzMTY4MjIxcQB%2BAVEAAAAAAAAS7wAAAAAAcHEAfgDdcQB%2BAOBwcHQAASp0ABhj%0D%0Ab21tb25DaGFyQEBtYXhMZW5ndGg6MTN4dAAQQ29udGFpbmVySGlzdG9yeXNxAH4BLHVxAH4ADAAA%0D%0AAAZ0AAdMSU5FX0lEdAAJUEVSRk9STUVEdAAIVFNFUlZfSUR0AAhGUk9NX0xPQ3QABlRPX0xPQ3QA%0D%0ABU5PVEVTcHEAfgAjdXEAfgDlAAAADnVxAH4A5wAAAAZ0AANDU0N0ABAyMDE0LTAxLTA3IDE0OjUz%0D%0AdAAETE9BRHQADFNISy9DSEVUSjA4MXQAC0NMRUgvNDcxNTEycHVxAH4A5wAAAAZ0AANDU0N0ABAy%0D%0AMDE0LTAxLTA3IDE0OjMzdAAJWUFSRCBNT1ZFdAAMU0hLLzhEMDU5Ni4xdAAMU0hLL0NIRVRKMDgx%0D%0AcHVxAH4A5wAAAAZ0AANDU0N0ABAyMDE0LTAxLTAzIDE5OjIxdAAJWUFSRCBNT1ZFdAAMU0hLLzhE%0D%0AMDU5My42dAAMU0hLLzhEMDU5Ni4xcHVxAH4A5wAAAAZ0AANDU0N0ABAyMDE0LTAxLTAzIDA1OjU0%0D%0AdAAKWUFSRCBTSElGVHQADFNISy84RDA1OTYuM3QADFNISy84RDA1OTMuNnB1cQB%2BAOcAAAAGdAAD%0D%0AQ1NDdAAQMjAxNC0wMS0wMSAxMzozOHQACllBUkQgU0hJRlR0AAxTSEsvOEQwNTk0LjV0AAxTSEsv%0D%0AOEQwNTk2LjNwdXEAfgDnAAAABnQAA0NTQ3QAEDIwMTMtMTItMzEgMTY6MjB0AAVBVURJVHBwdAAt%0D%0AQ1VTVE9NUyBSRUxFQVNFIGNoYW5nZWQgZnJvbSBIRUxEIHRvIFJFTEVBU0VEdXEAfgDnAAAABnQA%0D%0AA0NTQ3QAEDIwMTMtMTItMzEgMTU6NTR0AAlZQVJEIE1PVkV0AApTSEsvQ0hFMTM5dAAMU0hLLzhE%0D%0AMDU5NC41cHVxAH4A5wAAAAZ0AANDU0N0ABAyMDEzLTEyLTMxIDE1OjE4dAAJWUFSRCBNT1ZFdAAM%0D%0AU0hLL1pBMDExMy43dAAKU0hLL0NIRTEzOXB1cQB%2BAOcAAAAGdAADQ1NDdAAQMjAxMy0xMi0zMSAx%0D%0AMjoyNHQAClNFQUxDSEFOR0VwcHQAJ1NFQUwgIzEgY2hhbmdlZCBmcm9tIFQ1NTI5OTUgdG8gUjky%0D%0AODExOXVxAH4A5wAAAAZ0AANDU0N0ABAyMDEzLTEyLTMwIDIyOjU5dAAJWUFSRCBNT1ZFdAAMU0hL%0D%0AL0NIRVRIMDEydAAMU0hLL1pBMDExMy43cHVxAH4A5wAAAAZ0AANDU0N0ABAyMDEzLTEyLTMwIDIx%0D%0AOjM2dAAJWUFSRCBNT1ZFdAAMU0hLLzhEMDQ3NC41dAAMU0hLL0NIRVRIMDEycHVxAH4A5wAAAAZ0%0D%0AAANDU0N0ABAyMDEzLTEyLTI5IDEyOjA3dAAKVFJVQ0syWUFSRHQADVlHREI2MTg1OC9USVB0AAxT%0D%0ASEsvOEQwNDc0LjVwdXEAfgDnAAAABnQAA0NTQ3QAEDIwMTMtMTItMjkgMTE6MzZ0AAZSRVBBSVJw%0D%0AcHB1cQB%2BAOcAAAAGdAADQ1NDdAAQMjAxMy0xMi0yOSAxMTozNnQAB0ZVTEwgSU50AA1DT01NOllH%0D%0AREI2MTgvdAAMU0hLLzhEMDQ3NC41cHNxAH4ALHNxAH4ABAAAAAB3BAAAAAp4c3EAfgAEAAAAB3cE%0D%0AAAAACnNxAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAAKUFJJTlRfQ09ERXNxAH4APnNx%0D%0AAH4AQXNxAH4AQ3BwcAAAAAEAAAAMAAAAAAAARhcAAAAAcHBxAH4AR3B%2BcQB%2BAEl0AAZISURERU5z%0D%0AcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQACExPQ0FUSU9OcQB%2BAbRwcHAAAAACAAAA%0D%0ABAAAAAAAAEYYAAAAAXBwcQB%2BAEdwcQB%2BAbdzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5n%0D%0AcHQACkNPTlRBSU5FUjFxAH4BtHBwcAAAAAMAAAAMAAAAAAAARhkAAAACcHBxAH4AR3BxAH4ASnNx%0D%0AAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAAKQ09OVEFJTkVSMnEAfgG0cHBwAAAABAAA%0D%0AAAwAAAAAAABGGgAAAANwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmlu%0D%0AZ3B0AAtQQ0NfQ0FSRF9OT3EAfgG0cHBwAAAABQAAAAYAAAAAAABGGwAAAARwcHEAfgBHcHEAfgBK%0D%0Ac3EAfgAyAHEAfgBqdAAQamF2YS5sYW5nLkRvdWJsZXB0AA5XRUlHSElOR19WQUxVRXEAfgG0cHBw%0D%0AAAAABgAAABYAAAAAAABGHAAAAAVwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgEVdAAOamF2YS51dGls%0D%0ALkRhdGVwdAAKV0VJR0hfVElNRXEAfgG0cHBwAAAABwAAAAsAAAAAAABGHQAAAAZwcHEAfgBHcHEA%0D%0AfgBKeHNxAH4Ax3EAfgAmc3EAfgDLc3EAfgBDc3EAfgDPP0AAAAAAAAx3CAAAABAAAAABdAAMQ29u%0D%0AdGFpbmVyIE5vc3EAfgDSAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3BxAH4Bz3NxAH4APnNxAH4A%0D%0AQXNxAH4AQ3EAfgA7dAALQ0NMVTMxNjgyMjFxAH4B1QAAAAAAABLvAAAAAABwcQB%2BAN1xAH4A4HBw%0D%0AdAABKnQAGGNvbW1vbkNoYXJAQG1heExlbmd0aDoxM3h0AAtXZWlnaEJyaWRnZXBxAH4AKnVxAH4A%0D%0A5QAAAAB4cHA%3D&parameters%5B'Container%20No'%5D=$CNO&dataGrid_2.pageSetting.pageSize=25";
                oSendHead.Cookies = null;
                geturl = this.ReplaceParam(geturl);//添加参数$CNO箱号/$BLNO主提单号/$DOCK码头
                Uri actionUrl = new Uri(geturl);
                oSendHead.Method = "GET";
                oSendHead.Referer = geturl;
                oSendHead.Host = "http://" + actionUrl.Host;//主机
                if (geturl.IndexOf("https://") >= 0) //网址
                    oSendHead.Host = "https://" + actionUrl.Host;
                if (actionUrl.Port != 80) //主机+端口
                    oSendHead.Host = oSendHead.Host + ":" + actionUrl.Port;
                oSendHead.Action = actionUrl.PathAndQuery;//
                oSendHead.AcceptLanguage = "zh-cn,zh;q=0.8,en-us;q=0.5,en;q=0.3";
                oSendHead.EncodeName = "UTF-8";
                oHttp.Send(ref oSendHead);
                oSendHead.Cookies = oHttp.Cookies;

                //oSendHead.Referer = posturl;
                //actionUrl = new Uri(posturl);
                //oSendHead.Action = actionUrl.PathAndQuery;
                //oSendHead.Host = "http://" + actionUrl.Host; 
                //if (actionUrl.Port != 80) //主机+端口
                //    oSendHead.Host = oSendHead.Host + ":" + actionUrl.Port;

                oSendHead.Method = "post";
                oSendHead.PostData = ReplaceParam(PostData);
                oHttp.Send(ref oSendHead);
                string html = oSendHead.Html;





                string path = "	http://iport.sctcn.com/oi/action?SSOToken=portal%7EUFVCTElD%7E" + DateTime.Now.ToString("yyyyMMddHHmmss") + "%7EehiQ%2Bj5mLTUxXNhgP9q5Ig%3D%3D&ParameterType=ContainerInfo&ParameterValue=&LocaleCode=zhs&moduleName=oi&logout=&loginUrl=";
                PostData = @"_SERAILIZEBEAN_queryResult=rO0ABXNyAC9jb20uc2N0Y24uaXBvcnQub2kud2ViLmZvcm1iZWFuLlF1ZXJ5UmVzdWx0QmVhbq4F%0D%0AvohRN9m1AgAFSgAKZnVuY3Rpb25JZEwADGRhdGFHcmlkSW5mb3QAEExqYXZhL3V0aWwvTGlzdDtM%0D%0AAA9xdWVyeVJlc3VsdFNldHNxAH4AAUwACXNoaXBEaXZJZHQAEkxqYXZhL2xhbmcvU3RyaW5nO0wA%0D%0ACnNoaXBJZExpc3RxAH4AAnhwAAAAAAAAAA9zcgATamF2YS51dGlsLkFycmF5TGlzdHiB0h2Zx2Gd%0D%0AAwABSQAEc2l6ZXhwAAAABHcEAAAACnNyAC1jb20uc2N0Y24uZnJhbWV3b3JrLndlYi5kYXRhZ3Jp%0D%0AZC5EYXRhR3JpZEluZm%2FvObF79iAuqQIAB0kACHJvd0NvdW50WwANZnVuY3Rpb25Hcm91cHQAE1tM%0D%0AamF2YS9sYW5nL1N0cmluZztMAAtwYWdlU2V0dGluZ3QALkxjb20vc2N0Y24vZnJhbWV3b3JrL3dl%0D%0AYi9kYXRhZ3JpZC9QYWdlU2V0dGluZztbAAtyb3dFZGl0YWJsZXQAAltaWwAQcm93SXNQdWJsaWNR%0D%0AdWVyeXEAfgAJWwALcm93U2VsZWN0ZWRxAH4ACUwAC3NvcnRTZXR0aW5ndAAuTGNvbS9zY3Rjbi9m%0D%0AcmFtZXdvcmsvd2ViL2RhdGFncmlkL1NvcnRTZXR0aW5nO3hwAAAAAXVyABNbTGphdmEubGFuZy5T%0D%0AdHJpbmc7rdJW5%2Bkde0cCAAB4cAAAAAFwc3IALGNvbS5zY3Rjbi5mcmFtZXdvcmsud2ViLmRhdGFn%0D%0AcmlkLlBhZ2VTZXR0aW5nk%2BgHVIb0d9wCAARJAAtjdXJyZW50UGFnZUkACHBhZ2VTaXplWgAIcGFn%0D%0AZWFibGVKAAtyZWNvcmRDb3VudHhwAAAAAQAAABkAAAAAAAAAAAF1cgACW1pXjyA5FLhd4gIAAHhw%0D%0AAAAAAQB1cQB%2BABAAAAABAHVxAH4AEAAAAAEAc3IALGNvbS5zY3Rjbi5mcmFtZXdvcmsud2ViLmRh%0D%0AdGFncmlkLlNvcnRTZXR0aW5naVwwRQK1w74CAAJaAANhc2NMAApzb3J0Q29sdW1ucQB%2BAAJ4cABw%0D%0Ac3EAfgAGAAAAAHVxAH4ADAAAAABzcQB%2BAA4AAAABAAAAGQAAAAAAAAAAAHVxAH4AEAAAAAB1cQB%2B%0D%0AABAAAAAAdXEAfgAQAAAAAHNxAH4AFABwc3EAfgAGAAAADnVxAH4ADAAAAA5wcHBwcHBwcHBwcHBw%0D%0AcHNxAH4ADgAAAAEAAAAZAAAAAAAAAAAOdXEAfgAQAAAADgAAAAAAAAAAAAAAAAAAdXEAfgAQAAAA%0D%0ADgAAAAAAAAAAAAAAAAAAdXEAfgAQAAAADgAAAAAAAAAAAAAAAAAAc3EAfgAUAHBzcQB%2BAAYAAAAA%0D%0AdXEAfgAMAAAAAHNxAH4ADgAAAAEAAAAAAAAAAAAAAAAAdXEAfgAQAAAAAHVxAH4AEAAAAAB1cQB%2B%0D%0AABAAAAAAc3EAfgAUAHB4c3EAfgAEAAAABHcEAAAACnNyAC1jb20uc2N0Y24uZnJhbWV3b3JrLnF1%0D%0AZXJ5LkJhc2ljUXVlcnlSZXN1bHRTZXRI77Dp5fhNnQIABEwACmF0dHJpYnV0ZXNxAH4AAUwAEWNv%0D%0AbHVtbkRlc2NyaXB0b3JzcQB%2BAAFMAAhjcml0ZXJpYXQAKUxjb20vc2N0Y24vZnJhbWV3b3JrL3F1%0D%0AZXJ5L1F1ZXJ5Q3JpdGVyaWE7WwAEZGF0YXQAFFtbTGphdmEvbGFuZy9PYmplY3Q7eHBzcQB%2BAAQA%0D%0AAAAAdwQAAAAKeHNxAH4ABAAAACl3BAAAADpzcgAqY29tLnNjdGNuLmZyYW1ld29yay5xdWVyeS5D%0D%0Ab2x1bW5EZXNjcmlwdG9yZ4iS1O7SkuICAAlJAApJbmRleEluU3FsSQALZGlzcGxheVNpemVKAAJp%0D%0AZEkABWluZGV4TAAUYXNzb2NpYXRlZENvbHVtbk5hbWVxAH4AAkwAFWFzc29jaWF0ZWRDb2x1bW5W%0D%0AYWx1ZXEAfgACTAANZ3JvdXBGdW5jdGlvbnQAOkxjb20vc2N0Y24vZnJhbWV3b3JrL3F1ZXJ5L0Nv%0D%0AbHVtbkRlc2NyaXB0b3IkR3JvdXBGdW5jdGlvbjtMAAV2YWx1ZXQAEkxqYXZhL2xhbmcvT2JqZWN0%0D%0AO0wADnZpc2liaWxpdHlNb2RldAA7TGNvbS9zY3Rjbi9mcmFtZXdvcmsvcXVlcnkvQ29sdW1uRGVz%0D%0AY3JpcHRvciRWaXNpYmlsaXR5TW9kZTt4cgAqY29tLnNjdGNuLmZyYW1ld29yay5jb25maWcuUGVy%0D%0Ac2lzdFByb3BlcnR5cOKdxDDJPlUCAAlaAAptdWxpdFZhbHVlTAAOY29tcG9uZW50Q2xhc3N0ABFM%0D%0AamF2YS9sYW5nL0NsYXNzO0wAEmNvbXBvbmVudENsYXNzTmFtZXEAfgACTAALZm9ybWF0U3R5bGVx%0D%0AAH4AAkwABG5hbWVxAH4AAkwACnNlcmlhbGl6ZXJ0ACpMY29tL3NjdGNuL2ZyYW1ld29yay9zZXJp%0D%0AYWxpemUvU2VyaWFsaXplcjtMAAl0eXBlQ2xhc3NxAH4AN0wABXZhbHVlcQB%2BADRMAAl2YWx1ZVRl%0D%0AeHRxAH4AAnhwAHZyABBqYXZhLmxhbmcuU3RyaW5noPCkOHo7s0ICAAB4cHQAEGphdmEubGFuZy5T%0D%0AdHJpbmdwdAAGRVFfTkJSc3IAL2NvbS5zY3Rjbi5mcmFtZXdvcmsuc2VyaWFsaXplLkRlZmF1bHRT%0D%0AZXJpYWxpemVyxLndQvQBbGECAAJMAApzZXJpYWxpemVycQB%2BADhMABBzaW1wbGVTZXJpYWxpemVy%0D%0AdAA4TGNvbS9zY3Rjbi9mcmFtZXdvcmsvc2VyaWFsaXplL1NpbXBsZVByb3BlcnR5U2VyaWFsaXpl%0D%0Acjt4cHNyADFjb20uc2N0Y24uZnJhbWV3b3JrLnNlcmlhbGl6ZS5qc29uLkpTT05TZXJpYWxpemVy%0D%0A867QUrN0UkwCAAB4cHNyADZjb20uc2N0Y24uZnJhbWV3b3JrLnNlcmlhbGl6ZS5TaW1wbGVQcm9w%0D%0AZXJ0eVNlcmlhbGl6ZXJMRfJ9ozWKDwIAAHhwcQB%2BADtwcAAAAAEAAAAMAAAAAAAAbkkAAAAAcHB%2B%0D%0AcgA4Y29tLnNjdGNuLmZyYW1ld29yay5xdWVyeS5Db2x1bW5EZXNjcmlwdG9yJEdyb3VwRnVuY3Rp%0D%0Ab24AAAAAAAAAABIAAHhyAA5qYXZhLmxhbmcuRW51bQAAAAAAAAAAEgAAeHB0AAVDT1VOVHB%2BcgA5%0D%0AY29tLnNjdGNuLmZyYW1ld29yay5xdWVyeS5Db2x1bW5EZXNjcmlwdG9yJFZpc2liaWxpdHlNb2Rl%0D%0AAAAAAAAAAAASAAB4cQB%2BAEZ0AA9ERUZBVUxUX1ZJU0lCTEVzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxh%0D%0AbmcuU3RyaW5ncHQAB0xJTkVfSURxAH4AQHEAfgA7cHAAAAACAAAABAAAAAAAAG5KAAAAAXBwcQB%2B%0D%0AAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQABlNaVFBIVHEAfgBAcQB%2B%0D%0AADtwcAAAAAMAAAAJAAAAAAAAbksAAAACcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEu%0D%0AbGFuZy5TdHJpbmdwdAAISVNPX0NPREVxAH4AQHEAfgA7cHAAAAAEAAAABAAAAAAAAG5MAAAAA3Bw%0D%0AcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQAB0RBTUFHRURxAH4A%0D%0AQHEAfgA7cHAAAAAFAAAABAAAAAAAAG5NAAAABHBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBq%0D%0AYXZhLmxhbmcuU3RyaW5ncHQACENBVEVHT1JZcQB%2BAEBxAH4AO3BwAAAABgAAABoAAAAAAABuTgAA%0D%0AAAVwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0AAZTVEFUVVNx%0D%0AAH4AQHEAfgA7cHAAAAAHAAAABQAAAAAAAG5PAAAABnBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0%0D%0AABBqYXZhLmxhbmcuU3RyaW5ncHQADlJFTEVBU0VfU1RBVFVTcQB%2BAEBxAH4AO3BwAAAACAAAAAgA%0D%0AAAAAAABuUAAAAAdwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0%0D%0AAAhMT0NBVElPTnEAfgBAcQB%2BADtwcAAAAAkAAAAeAAAAAAAAblEAAAAIcHBxAH4AR3BxAH4ASnNx%0D%0AAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAAJSEFaX0NMQVNTcQB%2BAEBxAH4AO3BwAAAA%0D%0ACgAAAAQAAAAAAABuUgAAAAlwcHEAfgBHcHEAfgBKc3EAfgAyAHZyABBqYXZhLmxhbmcuRG91Ymxl%0D%0AgLPCSilr%2BwQCAAFEAAV2YWx1ZXhyABBqYXZhLmxhbmcuTnVtYmVyhqyVHQuU4IsCAAB4cHQAEGph%0D%0AdmEubGFuZy5Eb3VibGVwdAAEVU5ER3EAfgBAcQB%2BAGpwcAAAAAsAAAAWAAAAAAAAblMAAAAKcHBx%0D%0AAH4AR3BxAH4ASnNxAH4AMgBxAH4AanQAEGphdmEubGFuZy5Eb3VibGVwdAANVEVNUF9SRVFVSVJF%0D%0ARHEAfgBAcQB%2BAGpwcAAAAAwAAAAWAAAAAAAAblQAAAALcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4A%0D%0AanQAEGphdmEubGFuZy5Eb3VibGVwdAAMR1JPU1NfV0VJR0hUcQB%2BAEBxAH4AanBwAAAADQAAABYA%0D%0AAAAAAABuVQAAAAxwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0%0D%0AAAlQT0xfQUxJQVNxAH4AQHEAfgA7cHAAAAAOAAAAHgAAAAAAAG5WAAAADXBwcQB%2BAEdwcQB%2BAEpz%0D%0AcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQACVBPRF9BTElBU3EAfgBAcQB%2BADtwcAAA%0D%0AAA8AAAAeAAAAAAAAblcAAAAOcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFuZy5T%0D%0AdHJpbmdwdAALREVTVElOQVRJT05xAH4AQHEAfgA7cHAAAAAQAAAAFAAAAAAAAG5YAAAAD3BwcQB%2B%0D%0AAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQADEFSUl9MT0NfVFlQRXEA%0D%0AfgBAcQB%2BADtwcAAAABEAAAAEAAAAAAAAblkAAAAQcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QA%0D%0AEGphdmEubGFuZy5TdHJpbmdwdAARQVJSX1ZFU1NFTF9WT1lBR0VxAH4AQHEAfgA7cHAAAAASAAAA%0D%0AFwAAAAAAAG5aAAAAEXBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5n%0D%0AcHQADUFSUl9UUlVDS19OQlJxAH4AQHEAfgA7cHAAAAATAAAADAAAAAAAAG5bAAAAEnBwcQB%2BAEdw%0D%0AcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQAE0FSUl9WRVNTRUxfUE9TSVRJ%0D%0AT05xAH4AQHEAfgA7cHAAAAAUAAAADAAAAAAAAG5cAAAAE3BwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2B%0D%0AADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQADERFUF9MT0NfVFlQRXEAfgBAcQB%2BADtwcAAAABUAAAAE%0D%0AAAAAAAAAbl0AAAAUcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdw%0D%0AdAARREVQX1ZFU1NFTF9WT1lBR0VxAH4AQHEAfgA7cHAAAAAWAAAAFwAAAAAAAG5eAAAAFXBwcQB%2B%0D%0AAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQADURFUF9UUlVDS19OQlJx%0D%0AAH4AQHEAfgA7cHAAAAAXAAAADAAAAAAAAG5fAAAAFnBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0%0D%0AABBqYXZhLmxhbmcuU3RyaW5ncHQAE0RFUF9WRVNTRUxfUE9TSVRJT05xAH4AQHEAfgA7cHAAAAAY%0D%0AAAAADAAAAAAAAG5gAAAAF3BwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3Ry%0D%0AaW5ncHQAC0lOX0xPQ19UWVBFcQB%2BAEBxAH4AO3BwAAAAGQAAAAQAAAAAAABuYQAAABhwcHEAfgBH%0D%0AcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0ABBJTl9WRVNTRUxfVk9ZQUdF%0D%0AcQB%2BAEBxAH4AO3BwAAAAGgAAABcAAAAAAABuYgAAABlwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7%0D%0AdAAQamF2YS5sYW5nLlN0cmluZ3B0AAxJTl9UUlVDS19OQlJxAH4AQHEAfgA7cHAAAAAbAAAADAAA%0D%0AAAAAAG5jAAAAGnBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQA%0D%0AEklOX1ZFU1NFTF9QT1NJVElPTnEAfgBAcQB%2BADtwcAAAABwAAAAMAAAAAAAAbmQAAAAbcHBxAH4A%0D%0AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAAHSU5fVElNRXEAfgBAcQB%2B%0D%0AADtwcAAAAB0AAAAQAAAAAAAAbmUAAAAccHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEu%0D%0AbGFuZy5TdHJpbmdwdAAMT1VUX0xPQ19UWVBFcQB%2BAEBxAH4AO3BwAAAAHgAAAAQAAAAAAABuZgAA%0D%0AAB1wcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0ABFPVVRfVkVT%0D%0AU0VMX1ZPWUFHRXEAfgBAcQB%2BADtwcAAAAB8AAAAXAAAAAAAAbmcAAAAecHBxAH4AR3BxAH4ASnNx%0D%0AAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAANT1VUX1RSVUNLX05CUnEAfgBAcQB%2BADtw%0D%0AcAAAACAAAAAMAAAAAAAAbmgAAAAfcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFu%0D%0AZy5TdHJpbmdwdAATT1VUX1ZFU1NFTF9QT1NJVElPTnEAfgBAcQB%2BADtwcAAAACEAAAAMAAAAAAAA%0D%0AbmkAAAAgcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAAIT1VU%0D%0AX1RJTUVxAH4AQHEAfgA7cHAAAAAiAAAAEAAAAAAAAG5qAAAAIXBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIA%0D%0AcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQACVNFQUxfTkJSMXEAfgBAcQB%2BADtwcAAAACMAAAAP%0D%0AAAAAAAAAbmsAAAAicHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdw%0D%0AdAAJU0VBTF9OQlIycQB%2BAEBxAH4AO3BwAAAAJAAAAA8AAAAAAABubAAAACNwcHEAfgBHcHEAfgBK%0D%0Ac3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0AANPT0dxAH4AQHEAfgA7cHAAAAAlAAAA%0D%0AHgAAAAAAAG5tAAAAJHBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5n%0D%0AcHQAC0JPT0tJTkdfRURPcQB%2BAEBxAH4AO3BwAAAAJgAAABEAAAAAAABubgAAACVwcHEAfgBHcHEA%0D%0AfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0AAhHUk9VUF9JRHEAfgBAcQB%2BADtw%0D%0AcAAAACcAAAAJAAAAAAAAbm8AAAAmcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFu%0D%0AZy5TdHJpbmdwdAAHQ0lDX1BPU3EAfgBAcQB%2BADtwcAAAACgAAAAEAAAAAAAAbnAAAAAncHBxAH4A%0D%0AR3BxAH4ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAAIU0hBTkRfSURxAH4AQHEA%0D%0AfgA7cHAAAAApAAAABAAAAAAAAG5xAAAAKHBwcQB%2BAEdwcQB%2BAEp4c3IALGNvbS5zY3Rjbi5mcmFt%0D%0AZXdvcmsucXVlcnkuQmFzaWNRdWVyeUNyaXRlcmlhEwMHOie11owCAAVMAAtwYWdlU2V0dGluZ3EA%0D%0AfgAITAANcGFyYW1ldGVyQmVhbnQAKExjb20vc2N0Y24vZnJhbWV3b3JrL2JlYW5zL1Byb3BlcnR5%0D%0AQmVhbjtMAAlxdWVyeU5hbWVxAH4AAkwADHF1ZXJ5U2V0dGluZ3QAKExjb20vc2N0Y24vZnJhbWV3%0D%0Ab3JrL3F1ZXJ5L1F1ZXJ5U2V0dGluZztMAAtzb3J0U2V0dGluZ3EAfgAKeHBxAH4AD3NyACZjb20u%0D%0Ac2N0Y24uZnJhbWV3b3JrLmJlYW5zLlByb3BlcnR5QmVhbtKVu%2B%2BdM38yAgACTAAKc2VyaWFsaXpl%0D%0AcnEAfgA4TAAIdmFsdWVNYXB0ABNMamF2YS91dGlsL0hhc2hNYXA7eHBzcQB%2BAENzcgARamF2YS51%0D%0AdGlsLkhhc2hNYXAFB9rBwxZg0QMAAkYACmxvYWRGYWN0b3JJAAl0aHJlc2hvbGR4cD9AAAAAAAAM%0D%0AdwgAAAAQAAAAAXQADENvbnRhaW5lciBOb3NyAC1jb20uc2N0Y24uZnJhbWV3b3JrLnF1ZXJ5LlBh%0D%0AcmFtZXRlckRlc2NyaXB0b3IX3lJusPFfIAIACkoAAmlkSQAFaW5kZXhaAAhudWxsYWJsZUwADmF2%0D%0AYWlsYWJsZUl0ZW1zcQB%2BAAFMAAtpbnB1dE1ldGhvZHQAO0xjb20vc2N0Y24vZnJhbWV3b3JrL3F1%0D%0AZXJ5L1BhcmFtZXRlckRlc2NyaXB0b3IkSW5wdXRNZXRob2Q7TAAJaW5wdXRNb2RldAA5TGNvbS9z%0D%0AY3Rjbi9mcmFtZXdvcmsvcXVlcnkvUGFyYW1ldGVyRGVzY3JpcHRvciRJbnB1dE1vZGU7TAAQaXRl%0D%0AbVByb3ZpZGVyTmFtZXEAfgACTAAQaXRlbVByb3ZpZGVyVHlwZXQAQExjb20vc2N0Y24vZnJhbWV3%0D%0Ab3JrL3F1ZXJ5L1BhcmFtZXRlckRlc2NyaXB0b3IkSXRlbVByb3ZpZGVyVHlwZTtMAAZyZW1hcmtx%0D%0AAH4AAkwAFHZhbGlkYXRpb25FeHByZXNzaW9ucQB%2BAAJ4cQB%2BADYAcQB%2BADt0ABBqYXZhLmxhbmcu%0D%0AU3RyaW5ncHEAfgDRc3EAfgA%2Bc3EAfgBBc3EAfgBDcQB%2BADt0AAtDQ0xVMzE2ODIyMXEAfgDbAAAA%0D%0AAAAAEu8AAAAAAHB%2BcgA5Y29tLnNjdGNuLmZyYW1ld29yay5xdWVyeS5QYXJhbWV0ZXJEZXNjcmlw%0D%0AdG9yJElucHV0TWV0aG9kAAAAAAAAAAASAAB4cQB%2BAEZ0AARURVhUfnIAN2NvbS5zY3Rjbi5mcmFt%0D%0AZXdvcmsucXVlcnkuUGFyYW1ldGVyRGVzY3JpcHRvciRJbnB1dE1vZGUAAAAAAAAAABIAAHhxAH4A%0D%0ARnQAClVTRVJfSU5QVVRwcHQAASp0ABhjb21tb25DaGFyQEBtYXhMZW5ndGg6MTN4dAANQ29udGFp%0D%0AbmVySW5mb3BxAH4AFXVyABRbW0xqYXZhLmxhbmcuT2JqZWN0Oxi%2F%2B1Pka9vKAgAAeHAAAAABdXIA%0D%0AE1tMamF2YS5sYW5nLk9iamVjdDuQzlifEHMpbAIAAHhwAAAAKXQAC0NDTFUzMTY4MjIxdAADQ1ND%0D%0AdAAIMjAvR1AvODZ0AAQyMkcxcHQABkV4cG9ydHQABEZ1bGx0AAhSRUxFQVNFRHQAElZlc3NlbCBD%0D%0ATEVIIDQ3MTUxMnBwcHNxAH4AaEA9T1wo9cKPdAAFQ05TSEt0AAVFR1BSU3QABUVHQUxZdAABVHB0%0D%0AAAlZR0RCNjE4NThwdAABVnQACkNMRUgvMTM1MldwdAAGNDcxNTEydAABVHB0AAlZR0RCNjE4NThw%0D%0AdAAQMjAxMy0xMi0yOSAxMTozNXQAAVZ0AApDTEVILzEzNTJXcHQABjQ3MTUxMnQAEDIwMTQtMDEt%0D%0AMDcgMTQ6NTN0AAdSOTI4MTE5cHB0AAxTSEtBTFkxMDQyMjRwdAABTnBzcQB%2BACxzcQB%2BAAQAAAAA%0D%0AdwQAAAAKeHNxAH4ABAAAAAZ3BAAAAAZzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQA%0D%0AA0lNT3NxAH4APnNxAH4AQXNxAH4AQ3BwcAAAAAEAAAAZAAAAAAAAe%2F0AAAAAcHBxAH4AR3BxAH4A%0D%0ASnNxAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAASSU5fQlVTSU5FU1NfVk9ZQUdFcQB%2B%0D%0AAQpwcHAAAAACAAAAEQAAAAAAAHv%2BAAAAAXBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2BADt0ABBqYXZh%0D%0ALmxhbmcuU3RyaW5ncHQAE09VVF9CVVNJTkVTU19WT1lBR0VxAH4BCnBwcAAAAAMAAAARAAAAAAAA%0D%0Ae%2F8AAAACcHBxAH4AR3BxAH4ASnNxAH4AMgB2cgAOamF2YS51dGlsLkRhdGVoaoEBS1l0GQMAAHhw%0D%0AdAAOamF2YS51dGlsLkRhdGVwdAADQ0lRcQB%2BAQpwcHAAAAAEAAAABwAAAAAAAHwAAAAABHBwcQB%2B%0D%0AAEdwcQB%2BAEpzcQB%2BADIAcQB%2BARV0AA5qYXZhLnV0aWwuRGF0ZXB0AANDVVNxAH4BCnBwcAAAAAUA%0D%0AAAAHAAAAAAAAfAEAAAAFcHBxAH4AR3BxAH4ASnNxAH4AMgBxAH4BFXQADmphdmEudXRpbC5EYXRl%0D%0AcHQAB1ZPVUNIRVJxAH4BCnBwcAAAAAYAAAAHAAAAAAAAfAIAAAAGcHBxAH4AR3BxAH4ASnhzcQB%2B%0D%0AAMdxAH4AGHNxAH4Ay3NxAH4AQ3NxAH4Azz9AAAAAAAAMdwgAAAAQAAAAAXQADENvbnRhaW5lciBO%0D%0Ab3NxAH4A0gBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwcQB%2BASJzcQB%2BAD5zcQB%2BAEFzcQB%2BAENx%0D%0AAH4AO3QAC0NDTFUzMTY4MjIxcQB%2BASgAAAAAAAAS7wAAAAAAcHEAfgDdcQB%2BAOBwcHQAASp0ABhj%0D%0Ab21tb25DaGFyQEBtYXhMZW5ndGg6MTN4dAALUmVsZWFzZUluZm9zcgAmY29tLnNjdGNuLmZyYW1l%0D%0Ad29yay5xdWVyeS5RdWVyeVNldHRpbmfxMjQAamWdXAIAAlsAEWRldGFpbENvbHVtbk5hbWVzcQB%2B%0D%0AAAdbABJzdW1tYXJ5Q29sdW1uTmFtZXNxAH4AB3hwdXEAfgAMAAAAAHBxAH4AHHBzcQB%2BACxzcQB%2B%0D%0AAAQAAAAAdwQAAAAKeHNxAH4ABAAAAAZ3BAAAAApzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3Ry%0D%0AaW5ncHQAB0xJTkVfSURzcQB%2BAD5zcQB%2BAEFzcQB%2BAENxAH4AO3BwAAAAAQAAAAYAAAAAAABN5gAA%0D%0AAABwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0AAlQRVJGT1JN%0D%0ARURxAH4BNXEAfgA7cHAAAAACAAAAEAAAAAAAAE3nAAAAAXBwcQB%2BAEdwcQB%2BAEpzcQB%2BADIAcQB%2B%0D%0AADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQACFRTRVJWX0lEcQB%2BATVxAH4AO3BwAAAAAwAAAAoAAAAA%0D%0AAABN6AAAAAJwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0AAhG%0D%0AUk9NX0xPQ3EAfgE1cQB%2BADtwcAAAAAQAAAAKAAAAAAAATekAAAADcHBxAH4AR3BxAH4ASnNxAH4A%0D%0AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAAGVE9fTE9DcQB%2BATVxAH4AO3BwAAAABQAAAAoA%0D%0AAAAAAABN6gAAAARwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3B0%0D%0AAAVOT1RFU3EAfgE1cQB%2BADtwcAAAAAYAAAAUAAAAAAAATesAAAAFcHBxAH4AR3BxAH4ASnhzcQB%2B%0D%0AAMdxAH4AH3NxAH4Ay3NxAH4AQ3NxAH4Azz9AAAAAAAAMdwgAAAAQAAAAAXQADENvbnRhaW5lciBO%0D%0Ab3NxAH4A0gBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwcQB%2BAUtzcQB%2BAD5zcQB%2BAEFzcQB%2BAENx%0D%0AAH4AO3QAC0NDTFUzMTY4MjIxcQB%2BAVEAAAAAAAAS7wAAAAAAcHEAfgDdcQB%2BAOBwcHQAASp0ABhj%0D%0Ab21tb25DaGFyQEBtYXhMZW5ndGg6MTN4dAAQQ29udGFpbmVySGlzdG9yeXNxAH4BLHVxAH4ADAAA%0D%0AAAZ0AAdMSU5FX0lEdAAJUEVSRk9STUVEdAAIVFNFUlZfSUR0AAhGUk9NX0xPQ3QABlRPX0xPQ3QA%0D%0ABU5PVEVTcHEAfgAjdXEAfgDlAAAADnVxAH4A5wAAAAZ0AANDU0N0ABAyMDE0LTAxLTA3IDE0OjUz%0D%0AdAAETE9BRHQADFNISy9DSEVUSjA4MXQAC0NMRUgvNDcxNTEycHVxAH4A5wAAAAZ0AANDU0N0ABAy%0D%0AMDE0LTAxLTA3IDE0OjMzdAAJWUFSRCBNT1ZFdAAMU0hLLzhEMDU5Ni4xdAAMU0hLL0NIRVRKMDgx%0D%0AcHVxAH4A5wAAAAZ0AANDU0N0ABAyMDE0LTAxLTAzIDE5OjIxdAAJWUFSRCBNT1ZFdAAMU0hLLzhE%0D%0AMDU5My42dAAMU0hLLzhEMDU5Ni4xcHVxAH4A5wAAAAZ0AANDU0N0ABAyMDE0LTAxLTAzIDA1OjU0%0D%0AdAAKWUFSRCBTSElGVHQADFNISy84RDA1OTYuM3QADFNISy84RDA1OTMuNnB1cQB%2BAOcAAAAGdAAD%0D%0AQ1NDdAAQMjAxNC0wMS0wMSAxMzozOHQACllBUkQgU0hJRlR0AAxTSEsvOEQwNTk0LjV0AAxTSEsv%0D%0AOEQwNTk2LjNwdXEAfgDnAAAABnQAA0NTQ3QAEDIwMTMtMTItMzEgMTY6MjB0AAVBVURJVHBwdAAt%0D%0AQ1VTVE9NUyBSRUxFQVNFIGNoYW5nZWQgZnJvbSBIRUxEIHRvIFJFTEVBU0VEdXEAfgDnAAAABnQA%0D%0AA0NTQ3QAEDIwMTMtMTItMzEgMTU6NTR0AAlZQVJEIE1PVkV0AApTSEsvQ0hFMTM5dAAMU0hLLzhE%0D%0AMDU5NC41cHVxAH4A5wAAAAZ0AANDU0N0ABAyMDEzLTEyLTMxIDE1OjE4dAAJWUFSRCBNT1ZFdAAM%0D%0AU0hLL1pBMDExMy43dAAKU0hLL0NIRTEzOXB1cQB%2BAOcAAAAGdAADQ1NDdAAQMjAxMy0xMi0zMSAx%0D%0AMjoyNHQAClNFQUxDSEFOR0VwcHQAJ1NFQUwgIzEgY2hhbmdlZCBmcm9tIFQ1NTI5OTUgdG8gUjky%0D%0AODExOXVxAH4A5wAAAAZ0AANDU0N0ABAyMDEzLTEyLTMwIDIyOjU5dAAJWUFSRCBNT1ZFdAAMU0hL%0D%0AL0NIRVRIMDEydAAMU0hLL1pBMDExMy43cHVxAH4A5wAAAAZ0AANDU0N0ABAyMDEzLTEyLTMwIDIx%0D%0AOjM2dAAJWUFSRCBNT1ZFdAAMU0hLLzhEMDQ3NC41dAAMU0hLL0NIRVRIMDEycHVxAH4A5wAAAAZ0%0D%0AAANDU0N0ABAyMDEzLTEyLTI5IDEyOjA3dAAKVFJVQ0syWUFSRHQADVlHREI2MTg1OC9USVB0AAxT%0D%0ASEsvOEQwNDc0LjVwdXEAfgDnAAAABnQAA0NTQ3QAEDIwMTMtMTItMjkgMTE6MzZ0AAZSRVBBSVJw%0D%0AcHB1cQB%2BAOcAAAAGdAADQ1NDdAAQMjAxMy0xMi0yOSAxMTozNnQAB0ZVTEwgSU50AA1DT01NOllH%0D%0AREI2MTgvdAAMU0hLLzhEMDQ3NC41cHNxAH4ALHNxAH4ABAAAAAB3BAAAAAp4c3EAfgAEAAAAB3cE%0D%0AAAAACnNxAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAAKUFJJTlRfQ09ERXNxAH4APnNx%0D%0AAH4AQXNxAH4AQ3BwcAAAAAEAAAAMAAAAAAAARhcAAAAAcHBxAH4AR3B%2BcQB%2BAEl0AAZISURERU5z%0D%0AcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5ncHQACExPQ0FUSU9OcQB%2BAbRwcHAAAAACAAAA%0D%0ABAAAAAAAAEYYAAAAAXBwcQB%2BAEdwcQB%2BAbdzcQB%2BADIAcQB%2BADt0ABBqYXZhLmxhbmcuU3RyaW5n%0D%0AcHQACkNPTlRBSU5FUjFxAH4BtHBwcAAAAAMAAAAMAAAAAAAARhkAAAACcHBxAH4AR3BxAH4ASnNx%0D%0AAH4AMgBxAH4AO3QAEGphdmEubGFuZy5TdHJpbmdwdAAKQ09OVEFJTkVSMnEAfgG0cHBwAAAABAAA%0D%0AAAwAAAAAAABGGgAAAANwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgA7dAAQamF2YS5sYW5nLlN0cmlu%0D%0AZ3B0AAtQQ0NfQ0FSRF9OT3EAfgG0cHBwAAAABQAAAAYAAAAAAABGGwAAAARwcHEAfgBHcHEAfgBK%0D%0Ac3EAfgAyAHEAfgBqdAAQamF2YS5sYW5nLkRvdWJsZXB0AA5XRUlHSElOR19WQUxVRXEAfgG0cHBw%0D%0AAAAABgAAABYAAAAAAABGHAAAAAVwcHEAfgBHcHEAfgBKc3EAfgAyAHEAfgEVdAAOamF2YS51dGls%0D%0ALkRhdGVwdAAKV0VJR0hfVElNRXEAfgG0cHBwAAAABwAAAAsAAAAAAABGHQAAAAZwcHEAfgBHcHEA%0D%0AfgBKeHNxAH4Ax3EAfgAmc3EAfgDLc3EAfgBDc3EAfgDPP0AAAAAAAAx3CAAAABAAAAABdAAMQ29u%0D%0AdGFpbmVyIE5vc3EAfgDSAHEAfgA7dAAQamF2YS5sYW5nLlN0cmluZ3BxAH4Bz3NxAH4APnNxAH4A%0D%0AQXNxAH4AQ3EAfgA7dAALQ0NMVTMxNjgyMjFxAH4B1QAAAAAAABLvAAAAAABwcQB%2BAN1xAH4A4HBw%0D%0AdAABKnQAGGNvbW1vbkNoYXJAQG1heExlbmd0aDoxM3h0AAtXZWlnaEJyaWRnZXBxAH4AKnVxAH4A%0D%0A5QAAAAB4cHA%3D&parameters%5B'Container%20No'%5D=$CNO&dataGrid_2.pageSetting.pageSize=25";


                //解析数据
                doc.LoadHtml(html);
                HtmlNode hGateInLoadship = doc.DocumentNode.SelectSingleNode("//table/tr[2]/td/table/tr/td/table");
                if (hGateInLoadship != null)
                {
                    HtmlNodeCollection trCollection = hGateInLoadship.ChildNodes;
                    int trCount = trCollection.Count;
                    if (trCount < 2)
                        return;
                    //for (int i = 2; i < trCount - 1; i += 1)
                    //{
                    //    HtmlNodeCollection tdCollection = trCollection[i].ChildNodes;//列
                    //    if (tdCollection != null)
                    //    {
                    //        string netpol = BaseTracking.GetAppearText(tdCollection[15].InnerText);
                    //        if (netpol == pol)
                    //        {
                    //            string inOut = BaseTracking.GetAppearText(tdCollection[2].InnerText);
                    //            string no = BaseTracking.GetAppearText(tdCollection[2].InnerText);
                    //            if (soNo.IndexOf('-') > -1)
                    //                soNo = soNo.Substring(0, soNo.IndexOf('-'));
                    //            if (inOut == "IN" && no.IndexOf(soNo) > -1)//进场
                    //            {
                    //                CurrentGateIn = BaseTracking.GetAppearText(tdCollection[3].InnerText);
                    //                ctn.GateInDesc = GetTable(hGateInLoadship.InnerHtml);
                    //            }
                    //            else if (inOut == "OUT" && no.IndexOf(soNo) > -1)
                    //            {
                    //                NetETD = GetDate(BaseTracking.GetAppearText(tdCollection[3].InnerText), string.Empty, string.Empty);
                    //            }
                    //        }
                    //    }
                    //}
                }
            }
        }
        /// <summary>
        /// 深圳赤湾
        /// </summary>
        /// <param name="ctn"></param>
        private void GetSZCWGateInAndETD(CargoTrackingContainerInfo ctn, string pol, string soNo)
        {
            #region 重箱进场

            if (string.IsNullOrEmpty(ctn.GateIn.Trim()) || string.IsNullOrEmpty(NetETD.ToString()))
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                string path = "http://uport.cwcct.com/Portal/Ship/CN/Public/Pub_cntr_history_info.aspx";
                PostData = "__VIEWSTATE=dDwxMTc4MTA2NDc4Ozs%2BkzyK84akacjFCu5L61yle5umLtM%3D&txtContainer_no=$CNO&rdoDisplay=HTML&btnSearch=%B2%E9++%D1%AF";
                string html = GetHTML(path, "post", "gbk");
                //解析数据
                doc.LoadHtml(html);
                HtmlNode hGateInLoadship = doc.DocumentNode.SelectSingleNode("//table/tr[2]/td/table/tr/td/table");
                if (hGateInLoadship != null)
                {
                    HtmlNodeCollection trCollection = hGateInLoadship.ChildNodes;
                    int trCount = trCollection.Count;
                    if (trCount < 2)
                        return;
                    for (int i = 2; i < trCount - 1; i += 1)
                    {
                        HtmlNodeCollection tdCollection = trCollection[i].ChildNodes;//列
                        if (tdCollection != null)
                        {
                            string netpol = BaseTracking.GetAppearText(tdCollection[15].InnerText);
                            if (netpol == pol)
                            {
                                string inOut = BaseTracking.GetAppearText(tdCollection[2].InnerText);
                                string no = BaseTracking.GetAppearText(tdCollection[2].InnerText);
                                if (soNo.IndexOf('-') > -1)
                                    soNo = soNo.Substring(0, soNo.IndexOf('-'));
                                if (inOut == "IN" && no.IndexOf(soNo) > -1)//进场
                                {
                                    CurrentGateIn = BaseTracking.GetAppearText(tdCollection[3].InnerText);
                                    ctn.GateInDesc = GetTable(hGateInLoadship.InnerHtml);
                                }
                                else if (inOut == "OUT" && no.IndexOf(soNo) > -1)
                                {
                                    NetETD = GetDate(BaseTracking.GetAppearText(tdCollection[3].InnerText), string.Empty, string.Empty);
                                }
                            }
                        }
                    }
                }
            }

            #endregion
        }
        /// <summary>
        /// 深圳盐田
        /// </summary>
        /// <param name="cargoTrackingInfo"></param>
        private void GetSZYTALL(CargoTrackingInfo cargoTrackingInfo)
        {
            LoopCNT = false;
            string path = "http://www.yesinfo.com.cn/pqs_revision/pages/jsp/pgCntrHistEnquiry.jsp?";
            string bgDate = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
            string endDate = "1" + DateTime.Now.ToString("yy-MM-dd");
            PostData = "CNTR_ID=&SO_NUM=" + cargoTrackingInfo.SONO + "&START_DATE=" + bgDate + "&TO_DATE=114-01-02&submit_enquiry=%B2%E9%D1%AF";
            string html = GetHTML(path, "post", "GB2312");
            //解析数据
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            HtmlNode hGateInLoadship = doc.DocumentNode.SelectSingleNode("//div/table");
            if (hGateInLoadship != null)
            {
                HtmlNodeCollection trCollection = hGateInLoadship.ChildNodes;
                int trCount = trCollection.Count;
                if (trCount < 3)
                    return;
                for (int i = 3; i < trCount - 2; i += 2)
                {
                    HtmlNodeCollection tdCollection = trCollection[i].ChildNodes;//列
                    if (tdCollection != null)
                    {
                        string CNT = BaseTracking.GetAppearText(tdCollection[3].InnerText);
                        foreach (CargoTrackingContainerInfo cci in cargoTrackingInfo.Containers)
                        {
                            if (cci.ContainerNO == CNT)
                            {
                                //由于数据是在循环箱之前
                                cci.CustomsRelease = BaseTracking.GetAppearText(tdCollection[21].InnerText);
                                cci.GateIn = BaseTracking.GetAppearText(tdCollection[9].InnerText);
                                cci.CustomsReleaseDesc = cci.GateInDesc = GetTable(hGateInLoadship.InnerHtml);
                                NetETD = GetDate(BaseTracking.GetAppearText(tdCollection[11].InnerText), string.Empty, string.Empty);
                                break;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 大连海关放行
        /// </summary>
        /// <param name="ctn"></param>
        private void GetDLCustomsRelease(CargoTrackingContainerInfo ctn)
        {

        }
        /// <summary>
        /// 天津重箱进场/确认装船
        /// </summary>
        /// <param name="ctn"></param>
        private void GetTJGateInAndLoadship(CargoTrackingContainerInfo ctn)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            #region 重箱进场

            if (string.IsNullOrEmpty(ctn.GateIn.Trim()))
            {
                string html = RequestWeb(Config.GateInPath, Config.GateInPathPostData);
                //解析数据
                doc.LoadHtml(html);
                HtmlNode hGateInLoadship = doc.DocumentNode.SelectSingleNode("//table");
                if (hGateInLoadship != null)
                {
                    HtmlNodeCollection trCollection = hGateInLoadship.ChildNodes;
                    int trCount = trCollection.Count;
                    if (trCount < 3)
                        return;
                    for (int i = 3; i < trCount; i += 2)
                    {
                        HtmlNodeCollection tdCollection = trCollection[i].ChildNodes;//列
                        if (tdCollection != null)
                        {
                            string bl = BaseTracking.GetAppearText(tdCollection[7].InnerText);
                            if (bl.IndexOf(BLNO) > -1)
                            {
                                CurrentGateIn = BaseTracking.GetAppearText(tdCollection[23].InnerText);
                                ctn.GateInDesc = GetTable(hGateInLoadship.InnerHtml);
                                break;
                            }
                        }
                    }
                }
            }

            #endregion

            #region 确认装船

            if (string.IsNullOrEmpty(ctn.Loadship.Trim()))
            {
                string str = RequestWeb(Config.LoadshipPath, Config.LoadshipPathPostData);
                //解析数据
                doc.LoadHtml(str);
                HtmlNode h = doc.DocumentNode.SelectSingleNode("//table/tr[2]/td[12]");
                if (h != null)
                {
                    CurrentLoadship = h.InnerText;
                    ctn.LoadshipDesc = str;
                }
            }

            #endregion
        }
        /// <summary>
        /// 天津海关放行
        /// </summary>
        /// <param name="Config"></param>
        /// <param name="ctn"></param>
        private void GetTJCustomsRelease(CargoTrackingContainerInfo ctn)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            string str = RequestWeb(Config.CustomsReleasePath, Config.CustomsReleasePostData);
            //解析数据
            doc.LoadHtml(str);
            if (string.IsNullOrEmpty(ctn.CustomsRelease.Trim()))
            {
                HtmlNode hGateInLoadship = doc.DocumentNode.SelectSingleNode("//table");
                if (hGateInLoadship != null)
                {
                    HtmlNodeCollection trCollection = hGateInLoadship.ChildNodes;
                    int trCount = trCollection.Count;
                    if (trCount < 3)
                        return;
                    for (int i = 3; i < trCount; i += 2)
                    {
                        HtmlNodeCollection tdCollection = trCollection[i].ChildNodes;//列
                        if (tdCollection != null)
                        {
                            string bl = BaseTracking.GetAppearText(tdCollection[7].InnerText);
                            if (bl.IndexOf(BLNO) > -1)
                            {
                                CurrentCustomsRelease = GetDate(BaseTracking.GetAppearText(tdCollection[11].InnerText), "yyyyMMdd HHmmss", string.Empty).ToString();
                                ctn.CustomsReleaseDesc = GetTable(hGateInLoadship.InnerHtml);
                                break;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 格式化重箱进场/确认装船日期
        /// </summary>
        /// <param name="CurrentGateIn"></param>
        /// <param name="CurrentLoadship"></param>
        /// <param name="code"></param>
        private void FormatLoadship(string gatein, string gateInType, string loadship, string loadShipType)
        {
            if (string.IsNullOrEmpty(gatein) || string.IsNullOrEmpty(loadship)) return;
            DateTime gi = GetDate(gatein, gateInType, string.Empty);
            if (loadship.Length == 14)
            {
                loadship = gi.Year.ToString().Substring(0, 2) + "-" + loadship;
            }
            else if (loadship.Length == 11)
            {
                loadship = gi.Year + "-" + loadship;
            }
            DateTime ls = GetDate(loadship, loadShipType, string.Empty);
            if (ls < gi)
                ls = ls.AddYears(1);
            CurrentGateIn = gi.ToString();
            CurrentLoadship = ls.ToString();
        }
        /// <summary>
        /// 青岛海关放行/进场/上船
        /// </summary>
        /// <param name="cargoTrackingInfo"></param>
        /// <param name="config"></param>
        private void GetQDCustomsReleaseAndGateInLoadship(CargoTrackingContainerInfo ctn)
        {
            if (ctn.IsUpdate)
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                string str = RequestWeb(Config.CustomsReleasePath, Config.CustomsReleasePostData);
                //解析数据
                doc.LoadHtml(str);
                #region 重箱进场/装船

                HtmlNode hGateInLoadship = doc.DocumentNode.SelectSingleNode("//table[2]");
                if (hGateInLoadship != null)
                {
                    HtmlNodeCollection trCollection = hGateInLoadship.ChildNodes;
                    int trCount = trCollection.Count;
                    if (trCount < 5)
                        return;
                    for (int i = 5; i < trCount; i += 2)
                    {
                        HtmlNodeCollection tdCollection = trCollection[i].ChildNodes;//列
                        if (tdCollection != null)
                        {
                            //航次与航向对照
                            string bl = BaseTracking.GetAppearText(tdCollection[11].InnerText);
                            if (BLNO == bl)
                            {
                                string type = BaseTracking.GetAppearText(tdCollection[3].InnerText).Trim();
                                //进场
                                if (type.IndexOf("入") == 0)
                                {
                                    CurrentGateIn = BaseTracking.GetAppearText(tdCollection[5].InnerText).Trim();
                                    DOCK = BaseTracking.GetAppearText(tdCollection[9].InnerText.Trim());
                                }
                                if (type == "装大船")
                                {
                                    CurrentLoadship = BaseTracking.GetAppearText(tdCollection[5].InnerText.Trim());
                                }
                                ctn.GateInDesc = ctn.LoadshipDesc = GetTable(hGateInLoadship.InnerHtml);
                            }
                        }
                    }
                }
                #endregion
                #region 海关放行
                if (string.IsNullOrEmpty(ctn.CustomsRelease.Trim()))
                {
                    HtmlNode hCustoms = doc.DocumentNode.SelectSingleNode("//table[5]/tr[3]/td[3]");
                    if (hCustoms != null)
                    {
                        CurrentCustomsRelease = BaseTracking.GetAppearText(hCustoms.InnerText) + " " + DOCK;
                        ctn.CustomsReleaseDesc = GetTable(doc.DocumentNode.SelectSingleNode("//table[5]").InnerHtml);
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// 宁波开截港日
        /// </summary>
        /// <param name="cargoTrackingInfo"></param>
        /// <param name="config"></param>
        private void GetNBOpenAndClosePort(CargoTrackingInfo cargoTrackingInfo)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            switch (DOCK)
            {
                /*
                 三期/北二15
                四期16
                大榭17
                五期19
                梅山18
                北仑山132
                甬舟133
                二期14
                 */
                case "北二集司(三期)":
                    DOCKID = "15";
                    break;
                case "港吉(四期)":
                    DOCKID = "16";
                    break;
                case "远东(五期)":
                    DOCKID = "19";
                    break;
                case "梅山码头":
                    DOCKID = "18";
                    break;
                default:
                    Message.ServiceInterface.Message msg = new Message.ServiceInterface.Message();
                    msg.Body = "宁波：" + DOCK + " :" + CNO;
                    msg.SendTo = LocalData.UserInfo.EmailAddress;
                    msg.SendFrom = LocalData.UserInfo.EmailAddress;
                    msg.CreateBy = LocalData.UserInfo.LoginID;
                    ClientMessageService.Send(msg);
                    break;
            }
            string str = RequestWeb(Config.OpenPortPath, Config.OpenPortPostData);
            //解析数据
            doc.LoadHtml(str);
            if (doc.DocumentNode.SelectSingleNode("//table") != null)
            {
                HtmlNode h = doc.DocumentNode.SelectSingleNode("//table/tbody/tr/td[7]");
                if (h != null)
                {
                    cargoTrackingInfo.OpenPort = GetDate(BaseTracking.GetAppearText(h.InnerText.Trim()), string.Empty, string.Empty);
                    cargoTrackingInfo.ClosePort = GetDate(BaseTracking.GetAppearText(doc.DocumentNode.SelectSingleNode("//table/tbody/tr/td[8]").InnerText.Trim()), string.Empty, string.Empty);
                    cargoTrackingInfo.OpenAndClosePortDesc = str;
                }
            }
        }
        /// <summary>
        /// 宁波重箱进场/上船
        /// </summary>
        /// <param name="config"></param>
        /// <param name="ctn"></param>
        private void GetNBGateInAndLoadship(CargoTrackingContainerInfo ctn)
        {
            if (ctn.IsUpdate)
            {
                #region 进场
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                string str = RequestWeb(Config.GateInPath, Config.GateInPathPostData);
                //解析数据
                doc.LoadHtml(str);
                if (doc.DocumentNode.SelectSingleNode("//table") != null)
                {
                    HtmlNode h = doc.DocumentNode.SelectSingleNode("//table/tr[2]/td[3]");
                    if (h != null)
                    {
                        CurrentGateIn = BaseTracking.GetAppearText(h.InnerText.Trim());
                        ctn.GateInDesc = str;
                        DOCK = doc.DocumentNode.SelectSingleNode("//table/tr[2]/td[7]").InnerText.Trim();
                    }
                }
                #endregion

                #region 装船
                str = RequestWeb(Config.LoadshipPath, Config.LoadshipPathPostData);
                //解析数据
                doc.LoadHtml(str);
                if (doc.DocumentNode.SelectSingleNode("//table") != null)
                {
                    HtmlNode h = doc.DocumentNode.SelectSingleNode("//table/tr[2]/td[5]");
                    if (h != null)
                    {
                        CurrentLoadship = BaseTracking.GetAppearText(h.InnerText.Trim());
                        ctn.LoadshipDesc = str;
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// 宁波海关放行
        /// </summary>
        /// <param name="config"></param>
        /// <param name="ctn"></param>
        private void GetNBCustomsRelease(CargoTrackingContainerInfo ctn)
        {
            if (string.IsNullOrEmpty(ctn.CustomsRelease.Trim()))
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                string str = RequestWeb(Config.CustomsReleasePath, Config.CustomsReleasePostData);
                //解析数据
                doc.LoadHtml(str);
                if (doc.DocumentNode.SelectSingleNode("//table") != null)
                {
                    HtmlNode tableNode = doc.DocumentNode.SelectSingleNode("//table");
                    HtmlNodeCollection trCollection = tableNode.ChildNodes;
                    int trCount = trCollection.Count;
                    if (trCount < 3)
                        return;
                    for (int i = 3; i < trCount - 2; i += 2)
                    {
                        HtmlNodeCollection tdCollection = trCollection[i].ChildNodes;//列
                        if (tdCollection != null)
                        {
                            //航次与航向对照
                            string voyage = BaseTracking.GetAppearText(tdCollection[7].InnerText);
                            string course = BaseTracking.GetAppearText(tdCollection[9].InnerText).Trim();
                            string blNo = BaseTracking.GetAppearText(tdCollection[13].InnerText).Trim();
                            if (voyage == Voyage && course == "E" && blNo == BLNO)
                            {
                                DOCK = BaseTracking.GetAppearText(tdCollection[19].InnerText.Trim());
                                CurrentCustomsRelease += BaseTracking.GetAppearText(tdCollection[15].InnerText) + " " + DOCK;
                                ctn.CustomsReleaseDesc = str;
                                break;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 宁波港口、ETD、开截港
        /// </summary>
        /// <param name="config"></param>
        private void GetNBETDAndDOCK()
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            string str = RequestWeb(Config.NetETDPath, Config.NetETDPostData);
            //解析数据
            doc.LoadHtml(str);
            if (doc.DocumentNode.SelectSingleNode("//table") != null)
            {
                DOCKForVessel = doc.DocumentNode.SelectSingleNode("//table/caption").InnerText;
                string h = doc.DocumentNode.SelectSingleNode("//table/tbody/tr/td[8]").InnerText.Trim();
                if (h.Length > 0)
                    NetETD = GetDate(BaseTracking.GetAppearText(h), string.Empty, string.Empty);
                //string open = doc.DocumentNode.SelectSingleNode("//table/tbody/tr/td[9]").InnerText.Trim();
                //if (open.Length > 0)
                //    cargoTrackingInfo.OpenPort = GetDate(BaseTracking.GetAppearText(open), string.Empty, string.Empty);
                //string close = doc.DocumentNode.SelectSingleNode("//table/tbody/tr/td[10]").InnerText.Trim();
                //if (close.Length > 0)
                //    cargoTrackingInfo.ClosePort = GetDate(BaseTracking.GetAppearText(close), string.Empty, string.Empty);
                //cargoTrackingInfo.OpenAndClosePortDesc = str;
            }
        }
        /// <summary>
        /// 上海港口和ETD查询
        /// </summary>
        /// <param name="config"></param>
        private void GetSHETDAndDOCK()
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            string str = RequestWeb(Config.NetETDPath, Config.NetETDPostData);
            //解析数据
            doc.LoadHtml(str);
            if (doc.DocumentNode.SelectSingleNode("//table") != null)
            {
                DOCK = ASCToGB2312(doc.DocumentNode.SelectSingleNode("//table/tr[2]/td[6]").InnerText);
                string h = doc.DocumentNode.SelectSingleNode("//table/tr[2]/td[7]").InnerText.Trim();
                if (h.Length > 0)
                    NetETD = GetDate(BaseTracking.GetAppearText(h), "yyyyMMdd", string.Empty);
            }
        }
        /// <summary>
        /// 请求web
        /// </summary>
        /// <param name="path"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        private string RequestWeb(string path, string postData)
        {
            PostData = postData;
            string html = GetHTML(path, "post", "UTF-8");
            XmlDocument xmlDoc = JSONSerializerHelper.DeserializeJSONToXmlDocument(html, "Track");
            return (xmlDoc.SelectSingleNode("Track/Data")).InnerText;
        }

        /// <summary>
        /// 上海海关放行
        /// </summary>
        /// <param name="config"></param>
        /// <param name="ctn"></param>
        /// <returns></returns>
        private void GetCustomsRelease(CargoTrackingContainerInfo ctn)
        {
            if (ctn.IsUpdate)
            {
                string html = string.Empty;
                #region 上海海关放行
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

                if (string.IsNullOrEmpty(ctn.CustomsRelease.Trim()))
                {
                    html = GetHTML(Config.CustomsReleasePath, "get", "UTF-8");
                    //解析数据
                    doc.LoadHtml(html);

                    if (doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/body[1]/table[3]/tr[1]/td[1]/table[2]").ChildNodes.Count > 3)
                    {
                        //放行日期
                        CurrentCustomsRelease = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/body[1]/table[3]/tr[1]/td[1]/table[2]/tr[2]/td[8]").InnerText;
                        //码头
                        DOCK = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/body[1]/table[3]/tr[1]/td[1]/table[2]/tr[2]/td[6]").InnerText;
                        CurrentCustomsRelease += " " + DOCK;
                        //是否放行
                        CurrentCustomsRelease += " " + doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/body[1]/table[3]/tr[1]/td[1]/table[2]/tr[2]/td[7]").InnerText;

                        ctn.CustomsReleaseDesc = GetTable(doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/body[1]/table[3]/tr[1]/td[1]/table[2]").InnerHtml);
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// 上海重箱进场/确认上船
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="config"></param>
        /// <param name="ctn"></param>
        private void GetGateInAndLoadship(CargoTrackingContainerInfo ctn)
        {
            if (ctn.IsUpdate)
            {
                #region 重箱进场/确认上船
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                string html = string.Empty;
                if (!string.IsNullOrEmpty(DOCK) || !string.IsNullOrEmpty(DOCKForVessel))
                {
                    string d = string.IsNullOrEmpty(DOCK) ? DOCKForVessel : DOCK;
                    /*******进港日,装船日查询**/
                    html = GetHTML(Config.GateInPath, "get", d == "外二期" || d == "外四期" || d == "外五期" ? "UTF-8" : "GB2312");
                    //装船&进场详细
                    ctn.GateInDesc = ctn.LoadshipDesc = html;
                    //解析
                    doc.LoadHtml(html);
                    GetGateInByHTML(doc);
                }

                #endregion
            }
        }
        /// <summary>
        /// 上海开/截港日
        /// </summary>
        /// <param name="cargoTrackingInfo"></param>
        /// <param name="doc"></param>
        /// <param name="config"></param>
        private void GetOpenAndClosePort(CargoTrackingInfo cargoTrackingInfo)
        {
            #region 开/截港日

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            switch (DOCK)
            {
                case "外二期":
                    DOCKID = "6";
                    break;
                case "外一期":
                    DOCKID = "11";
                    break;
                case "外四期":
                    DOCKID = "10";
                    break;
                case "外五期":
                    DOCKID = "9";
                    break;
                case "洋山一期":
                    DOCKID = "8";
                    break;
                case "洋山三期":
                    DOCKID = "12";
                    break;
                default://宝山
                    DOCKID = string.Empty;
                    break;
            }
            if (string.IsNullOrEmpty(DOCKID)) return;
            string html = RequestWeb(Config.OpenPortPath, Config.OpenPortPostData);
            //解析数据
            doc.LoadHtml(html);
            if (doc.DocumentNode.SelectSingleNode("//tr/td[7]") != null && doc.DocumentNode.SelectSingleNode("//tr/td[8]") != null)
            {
                string datetime1 = string.Empty;
                string datetime2 = string.Empty;
                HtmlNode h = doc.DocumentNode.SelectSingleNode("/table[1]/tbody[1]/tr/td[7]");
                if (h != null)
                {
                    datetime1 = h.InnerText;
                    datetime2 = doc.DocumentNode.SelectSingleNode("/table[1]/tbody[1]/tr/td[8]").InnerText;
                }

                FormatDateTime(datetime1, "yyyy-MM-dd HH:mm", datetime2, "yyyy-MM-dd HH:mm", cargoTrackingInfo);
                cargoTrackingInfo.OpenAndClosePortDesc = html;
            }

            #endregion
        }
        /// <summary>
        /// 保存开截港日
        /// </summary>
        /// <param name="cargoTrackingInfo"></param>
        private void SaveOpenPortAndClosePort(CargoTrackingInfo cargoTrackingInfo)
        {
            //保存开截港日
            if (cargoTrackingInfo.OpenPort != null && cargoTrackingInfo.ClosePort != null)
            {
                //上船后保存开截港日期
                if (!string.IsNullOrEmpty(cargoTrackingInfo.Containers[0].Loadship))
                    OperationService.SaveOpenAndClosePort(cargoTrackingInfo.OpenClosePortId, cargoTrackingInfo.OpenPort, cargoTrackingInfo.ClosePort, cargoTrackingInfo.OpenAndClosePortDesc, isEnglish, LocalData.UserInfo.LoginID, cargoTrackingInfo.OpenAndClosePortUpdateDate, NetETD, DOCKForVessel);
            }
        }
        /// <summary>
        /// 格式化开截港日期
        /// </summary>
        /// <param name="datetime1">yyyy-MM-dd HH:mm</param>
        /// <param name="datetime2">MM-dd HH:mm</param>
        private void FormatDateTime(string datetime1, string date1Type, string datetime2, string date2Type, CargoTrackingInfo cargoTrackingInfo)
        {
            if (string.IsNullOrEmpty(datetime1) || string.IsNullOrEmpty(datetime2))
                return;
            DateTime date1 = GetDate(datetime1, date1Type, string.Empty);
            if (date1 != null)
            {
                int y = date1.Year;
                if (datetime2.Length <= 11)
                    datetime2 = y + "-" + datetime2;
                DateTime date2 = GetDate(datetime2, date2Type, string.Empty);
                if (date1 > date2)
                    date2 = date2.AddYears(1);
                cargoTrackingInfo.OpenPort = date1;
                cargoTrackingInfo.ClosePort = date2;
            }
        }

        public void DataBind(BusinessOperationContext business)
        {
            Context = business;
            if (Context == null)
                return;
            if (TempOperationID == Context.OperationID) return;
            TempOperationID = Context.OperationID;
            lblETD.Visible = false;
            txtETD.ForeColor = Color.Black;
            InitControl();
            InitDataSource();
            //SetTerminalList();
        }
        /// <summary>
        /// 上海进港日，装船日
        /// </summary>
        /// <param name="doc"></param>
        private void GetGateInByHTML(HtmlAgilityPack.HtmlDocument doc)
        {
            HtmlNode tableNode;
            HtmlNodeCollection trCollection;
            int trCount;
            List<DockClass> dockList = new List<DockClass>();
            Message.ServiceInterface.Message msg = new Message.ServiceInterface.Message();

            if (string.IsNullOrEmpty(DOCK) && string.IsNullOrEmpty(DOCKForVessel)) return;

            string d = string.IsNullOrEmpty(DOCK) ? DOCKForVessel : DOCK;
            switch (d)
            {
                case "外二期":
                    #region
                    tableNode = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/span[1]/html[1]/body[1]/div[1]/table[1]");
                    if (tableNode == null)
                    {
                        return;
                    }
                    trCollection = tableNode.ChildNodes;
                    trCount = trCollection.Count;
                    if (trCount < 4)
                        return;
                    for (int i = 2; i < trCount - 1; i++)
                    {
                        HtmlNodeCollection tdCollection = trCollection[i].ChildNodes;//列
                        if (tdCollection != null)
                        {
                            DockClass dc = new DockClass();
                            dc.JiChangDate = BaseTracking.GetAppearText(tdCollection[1].InnerText);//进场日
                            dc.ChuChangDate = BaseTracking.GetAppearText(tdCollection[2].InnerText);//出场日
                            dc.JiChangType = BaseTracking.GetAppearText(tdCollection[3].InnerText);//进场方式
                            dc.ChuChangType = BaseTracking.GetAppearText(tdCollection[4].InnerText);//出场方式
                            dc.PeiChuan = BaseTracking.GetAppearText(tdCollection[9].InnerText);//是否配船
                            dockList.Add(dc);
                        }
                    }
                    GetGateInDateAndLoadshipDate(dockList, DockType.WG2);
                    #endregion
                    break;
                case "外一期":
                    #region
                    tableNode = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/span[1]/table/tr/td/table");
                    if (tableNode == null)
                    {
                        return;
                    }
                    trCollection = tableNode.ChildNodes;
                    trCount = trCollection.Count;
                    if (trCount < 3)
                        return;
                    for (int i = 3; i < trCount - 1; i++)
                    {
                        HtmlNodeCollection tdCollection = trCollection[i].ChildNodes;//列
                        if (tdCollection != null)
                        {
                            DockClass dc = new DockClass();
                            dc.JiChangDate = BaseTracking.GetAppearText(tdCollection[2].InnerText);//进场日
                            dc.ChuChangDate = BaseTracking.GetAppearText(tdCollection[5].InnerText);//出场日
                            dc.JiChangType = BaseTracking.GetAppearText(tdCollection[3].InnerText);//进场方式
                            dc.ChuChangType = BaseTracking.GetAppearText(tdCollection[17].InnerText);//箱动态
                            dc.PeiChuan = BaseTracking.GetAppearText(tdCollection[19].InnerText);//是否配船
                            dc.BLNO = BaseTracking.GetAppearText(tdCollection[7].InnerText);//提单号
                            dockList.Add(dc);
                        }
                    }
                    GetGateInDateAndLoadshipDate(dockList, DockType.WG1);
                    #endregion
                    break;
                case "外四期":
                    #region
                    tableNode = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/span[1]/html[1]/body[1]/div[1]/table[1]");
                    if (tableNode == null)
                    {
                        return;
                    }
                    trCollection = tableNode.ChildNodes;
                    trCount = trCollection.Count;
                    if (trCount < 4)
                        return;
                    for (int i = 2; i < trCount - 1; i++)
                    {
                        HtmlNodeCollection tdCollection = trCollection[i].ChildNodes;//列
                        if (tdCollection != null)
                        {
                            DockClass dc = new DockClass();
                            dc.JiChangDate = BaseTracking.GetAppearText(tdCollection[1].InnerText);//进场日
                            dc.ChuChangDate = BaseTracking.GetAppearText(tdCollection[2].InnerText);//出场日
                            dc.JiChangType = BaseTracking.GetAppearText(tdCollection[3].InnerText);//进场方式
                            dc.ChuChangType = BaseTracking.GetAppearText(tdCollection[4].InnerText);//出场方式
                            //dc.FangGuan = BaseTracking.GetAppearText(tdCollection[6].InnerText);//是否【放关】外四无配船项
                            dockList.Add(dc);
                        }
                    }
                    GetGateInDateAndLoadshipDate(dockList, DockType.WG4);
                    #endregion
                    break;
                case "外五期":
                    #region
                    tableNode = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/span[1]/html[1]/body[1]/div[1]/table[1]");
                    if (tableNode == null)
                    {
                        return;
                    }
                    trCollection = tableNode.ChildNodes;
                    trCount = trCollection.Count;
                    if (trCount < 4)
                        return;
                    for (int i = 2; i < trCount - 1; i++)
                    {
                        HtmlNodeCollection tdCollection = trCollection[i].ChildNodes;//列
                        if (tdCollection != null)
                        {
                            DockClass dc = new DockClass();
                            dc.JiChangDate = BaseTracking.GetAppearText(tdCollection[1].InnerText);//进场日
                            dc.ChuChangDate = BaseTracking.GetAppearText(tdCollection[2].InnerText);//出场日
                            dc.JiChangType = BaseTracking.GetAppearText(tdCollection[3].InnerText);//进场方式
                            dc.ChuChangType = BaseTracking.GetAppearText(tdCollection[4].InnerText);//出场方式
                            dc.PeiChuan = BaseTracking.GetAppearText(tdCollection[7].InnerText);//是否配船
                            dockList.Add(dc);
                        }
                    }
                    GetGateInDateAndLoadshipDate(dockList, DockType.WG5);
                    #endregion
                    break;
                case "洋山一期":
                    #region
                    tableNode = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/span[1]/html[1]/body[1]/table[1]/tr[4]/td[1]/table/tr/td/table");
                    if (tableNode == null)
                    {
                        return;
                    }
                    trCollection = tableNode.ChildNodes;
                    trCount = trCollection.Count;
                    if (trCount < 3)
                        return;
                    for (int i = 3; i < trCount - 1; i += 2)
                    {
                        HtmlNodeCollection tdCollection = trCollection[i].ChildNodes;//列
                        if (tdCollection != null)
                        {
                            DockClass dc = new DockClass();
                            dc.JiChangDate = BaseTracking.GetAppearText(tdCollection[2].InnerText);//进场日
                            dc.ChuChangDate = BaseTracking.GetAppearText(tdCollection[3].InnerText);//出场日
                            dc.JiChangType = BaseTracking.GetAppearText(tdCollection[4].InnerText);//进场方式
                            dc.ChuChangType = BaseTracking.GetAppearText(tdCollection[5].InnerText);//出场方式
                            dc.PeiChuan = BaseTracking.GetAppearText(tdCollection[7].InnerText);//是否配船
                            dockList.Add(dc);
                        }
                    }
                    GetGateInDateAndLoadshipDate(dockList, DockType.YS1);
                    #endregion
                    break;
                case "洋山三期":
                    #region
                    tableNode = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/span[1]/html[1]/body[1]/table[1]/tr[4]/td[1]/table[1]/tr[3]/td[1]/table[1]");
                    if (tableNode == null)
                    {
                        return;
                    }
                    trCollection = tableNode.ChildNodes;
                    trCount = trCollection.Count;
                    if (trCount < 3)
                        return;

                    for (int i = 3; i < trCount; i += 4)
                    {
                        HtmlNodeCollection tdCollection = trCollection[i].ChildNodes;//列
                        if (tdCollection != null)
                        {
                            DockClass dc = new DockClass();
                            dc.JiChangDate = BaseTracking.GetAppearText(tdCollection[3].InnerText);//进场日
                            dc.ChuChangDate = BaseTracking.GetAppearText(tdCollection[5].InnerText);//出场日
                            dc.JiChangType = BaseTracking.GetAppearText(tdCollection[7].InnerText);//进场方式
                            dc.ChuChangType = BaseTracking.GetAppearText(tdCollection[9].InnerText);//出场方式
                            dc.PeiChuan = BaseTracking.GetAppearText(tdCollection[13].InnerText);//是否配船
                            //tdCollection[11].InnerText.Trim();//是否放关
                            dockList.Add(dc);
                        }
                    }
                    GetGateInDateAndLoadshipDate(dockList, DockType.YS3);
                    #endregion
                    break;
                default://宝山
                    msg.Body = "上海宝山箱号：" + CNO;
                    msg.SendTo = LocalData.UserInfo.EmailAddress;
                    msg.SendFrom = LocalData.UserInfo.EmailAddress;
                    msg.CreateBy = LocalData.UserInfo.LoginID;
                    ClientMessageService.Send(msg);
                    break;
            }
        }

        private void GetGateInDateAndLoadshipDate(List<DockClass> dockList, DockType dockType)
        {
            if (dockList.Count > 0)
            {
                DockClass dockClass = null;
                DateTime? temp = null;
                string dateType = "yy-MM-dd HH:mm";
                if (dockType == DockType.WG1)
                    dateType = string.Empty;
                foreach (DockClass dc in dockList)
                {
                    DateTime? currdt = null;
                    if (dc.JiChangType == "出口重箱进场")
                    {
                        if (dockType == DockType.WG1)
                        {
                            CurrentGateIn = dc.JiChangDate;
                            if (dc.ChuChangType == "55已装船启航" && dc.PeiChuan == "Y" && dc.BLNO == BLNO)
                                CurrentLoadship = dc.ChuChangDate;
                        }
                        else
                        {
                            currdt = GetDate(dc.JiChangDate, dateType, string.Empty);
                            if (temp == null || currdt > temp)
                            {
                                temp = currdt;
                                dockClass = dc;
                            }
                        }
                    }
                }
                if (dockType != DockType.WG1)
                    if (temp != null)
                    {
                        CurrentGateIn = dockClass.JiChangDate;
                        if (!string.IsNullOrEmpty(dockClass.ChuChangDate))
                        {
                            ////判断 月/日/时 装船出场时间要大于出口重箱进场
                            string chuchang = dockClass.ChuChangDate;
                            if (!string.IsNullOrEmpty(chuchang))
                            {
                                if (dockType == DockType.WG4 && dockClass.ChuChangType == "装船出场")
                                    CurrentLoadship = chuchang;
                                else if (dockClass.ChuChangType == "装船出场" && dockClass.PeiChuan == "--")
                                    CurrentLoadship = chuchang;
                            }
                        }
                    }
            }
        }

        /// <summary>
        /// 替换$CNO/$BLNO/$DOCK
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private string ReplaceParam(string config)
        {
            config = config.Replace("$CNO", CNO).Replace("$BLNO", BLNO).Replace("$VesselIGZPlus", VesselIGZPlus)
                .Replace("$VesselIGZ20", VesselIGZ20).Replace("$Voyage", Voyage).Replace("$DOCKID", DOCKID);

            if (string.IsNullOrEmpty(DOCK) && string.IsNullOrEmpty(DOCKForVessel)) return config;

            string d = string.IsNullOrEmpty(DOCK) ? DOCKForVessel : DOCK;
            switch (d)
            {
                case "外二期":
                    config = config.Replace("$DOCK", "wg2");
                    break;
                case "外一期":
                    config = config.Replace("$DOCK", "wg1");
                    break;
                case "外四期":
                    config = config.Replace("$DOCK", "wg4");
                    break;
                case "外五期":
                    config = config.Replace("$DOCK", "wg5");
                    break;
                case "洋山一期":
                    config = config.Replace("$DOCK", "ys1");
                    break;
                case "洋山三期":
                    config = config.Replace("$DOCK", "ys3");
                    break;
                default:
                    config = config.Replace("$DOCK", "sct");
                    break;
            }
            return config;
        }

        #endregion

        /// <summary>
        /// * 使用示列:
        /// * XmlHelper.Read(path, "/Node", "")
        /// * XmlHelper.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
        /// </summary>
        /// <param name="path"></param>
        /// <param name="node"></param>
        /// <param name="id">公司ID</param>
        /// <returns></returns>
        public static CargoTrackingConfig ReadXML(string path, string node, string id)
        {
            string value = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNodeList list = xmlDoc.SelectSingleNode(node).ChildNodes;
            CargoTrackingConfig config = null;
            if (list != null && list.Count > 0)
            {
                foreach (XmlNode xn in list)
                {
                    if (id.ToLower() == (xn["ID"]).InnerText.ToLower())
                    {
                        config = new CargoTrackingConfig();
                        config.ID = xn["ID"].InnerText;
                        config.LoadshipPath = (xn["Loadship"]["Path"]).InnerText;
                        config.LoadshipPathPostData = (xn["Loadship"]["PostData"]).InnerText;
                        config.GateInPath = (xn["GateIn"]["Path"]).InnerText;
                        config.GateInPathPostData = (xn["GateIn"]["PostData"]).InnerText;
                        config.CustomsReleasePath = (xn["CustomsRelease"]["Path"]).InnerText;
                        config.CustomsReleasePostData = (xn["CustomsRelease"]["PostData"]).InnerText;
                        config.IGZPath = (xn["IGZ"]).InnerText;
                        config.OpenPortPath = (xn["OpenPort"]["Path"]).InnerText;
                        config.OpenPortPostData = (xn["OpenPort"]["PostData"]).InnerText;
                        config.NetETDPath = (xn["ETD"]["Path"]).InnerText;
                        config.NetETDPostData = (xn["ETD"]["PostData"]).InnerText;
                    }
                }
            }
            return config;
        }

        #region HTML操作

        SendHead oSendHead = new SendHead();
        Http oHttp = new Http();
        public string PostData { get; set; }
        public string GetHTML(string url, string requestType, string encodeName)
        {
            oSendHead.Cookies = null;
            url = this.ReplaceParam(url);//添加参数$CNO箱号/$BLNO主提单号/$DOCK码头
            Uri actionUrl = new Uri(url);
            oSendHead.Method = "GET";
            oSendHead.Referer = url;
            oSendHead.Host = "http://" + actionUrl.Host;//主机
            if (url.IndexOf("https://") >= 0) //网址
                oSendHead.Host = "https://" + actionUrl.Host;
            if (actionUrl.Port != 80) //主机+端口
                oSendHead.Host = oSendHead.Host + ":" + actionUrl.Port;
            oSendHead.Action = actionUrl.PathAndQuery;//
            oSendHead.AcceptLanguage = "zh-cn,zh;q=0.8,en-us;q=0.5,en;q=0.3";
            if (encodeName != string.Empty)
                oSendHead.EncodeName = encodeName;
            oHttp.Send(ref oSendHead);
            oSendHead.Cookies = oHttp.Cookies;
            if (requestType.ToLower() == "post")
            {
                oSendHead.Method = "post";
                oSendHead.PostData = ReplaceParam(PostData);
                oHttp.Send(ref oSendHead);
            }
            return oSendHead.Html;
        }
        /// <summary>
        /// 返回table
        /// </summary>
        /// <param name="doc">HtmlAgilityPack.HtmlDocument</param>
        /// <returns></returns>
        public string GetTable(string tableTr)
        {
            return "<table border='1'>" + tableTr + "</table>";
        }

        public DateTime GetDate(string originalDate, string dateType, string dateLocation)
        {
            if (dateType == string.Empty)
            {
                return DateTime.Parse(originalDate, new System.Globalization.CultureInfo(dateLocation, true));//.ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                return DateTime.ParseExact(originalDate, dateType, new System.Globalization.CultureInfo(dateLocation, true));//.ToString("yyyy-MM-dd HH:mm");
            }
        }

        /// <summary>
        /// 将ASCII编码转换为汉字字符串
        /// </summary>
        /// <param name="str">ascII编码字符串</param>
        /// <returns>汉字字符串</returns>
        public static char ToGB2312(string str)
        {
            return (char)Convert.ToInt32(str);
        }

        public static string ASCToGB2312(string str)
        {
            str = str.Replace("&#", string.Empty);
            string[] strList = str.Split(';');
            string res = string.Empty;
            foreach (string s in strList)
                if (!string.IsNullOrEmpty(s))
                    res += ToGB2312(s);
            return res;
        }

        #endregion

        #region 点击GridView显示详细

        private void GateIn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CurrentCargoTrackingContainer.GateIn))
                PartLoader.ShowEditPartInDialog<ShowWeb>(Workitem, CurrentCargoTrackingContainer.GateInDesc, isEnglish ? "GateInDetails" : "进场详细", null);
        }

        private void CustomsRelease_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CurrentCargoTrackingContainer.CustomsRelease.Trim()))
                PartLoader.ShowEditPartInDialog<ShowWeb>(Workitem, CurrentCargoTrackingContainer.CustomsReleaseDesc, isEnglish ? "CustomsReleaseDetails" : "海关放行详细", null);
        }

        private void Loadship_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CurrentCargoTrackingContainer.Loadship))
                PartLoader.ShowEditPartInDialog<ShowWeb>(Workitem, CurrentCargoTrackingContainer.LoadshipDesc, isEnglish ? "LoadshipDetails" : "确认上船详细", null);
        }

        private void CurrentState_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CurrentCargoTrackingContainer.StatusALL))
                PartLoader.ShowEditPartInDialog<ShowWeb>(Workitem, CurrentCargoTrackingContainer.StatusALL, isEnglish ? "State" : "船东箱动态详细", null);
        }

        private void barSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PartLoader.ShowEditPart<ShowWeb>(Workitem, "url" + IGZURL, isEnglish ? "Cargo Tracking" : "箱货跟踪", null);
        }

        private void txtOpenPort_MouseDown(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(DataSource.OpenAndClosePortDesc))
                PartLoader.ShowEditPartInDialog<ShowWeb>(Workitem, DataSource.OpenAndClosePortDesc, isEnglish ? "Open Port" : "开/截港日", null);
        }

        private void lblETD_Click(object sender, EventArgs e)
        {
            txtETD.EditValue = NetETD;
            txtETD.ForeColor = Color.Black;
        }

        #endregion

        //Thread threadCargoTracking;

        /// <summary>
        /// 位置对象
        /// </summary>
        Point _location;
        /// <summary>
        /// 大小
        /// </summary>
        Size _size;
        /// <summary>
        /// 是否已经设置
        /// </summary>
        bool _isSet = false;
        /// <summary>
        /// 获取预览窗格位置及其窗体大小
        /// </summary>
        private void GetPositionAndSize()
        {
            if (!_isSet)
            {
                Screen scr = null;
                scr = Screen.FromPoint(this.Location);
                _location = new Point((int)(scr.WorkingArea.Width * 0.4), LocalData.Height);
                int height = scr.WorkingArea.Height - LocalData.Height;
                int width = (int)(scr.WorkingArea.Width * 0.6);
                _size = new Size(width, height);
                _isSet = true;
            }
        }

        void CrawlerAvailability(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentCargoTrackingContainer.ContainerNO))
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select a ContainerNO." : "请选择一个箱号.");
                return;
            }
            if (DataSource == null)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "CargoTrackingInfo is null." : "货物跟踪信息为空.");
                return;
            }
            if (!string.IsNullOrEmpty(DataSource.POLCode))
            {
               LocationInfo location = GeographyService.GetLocationInfo(DataSource.POLID);
               if (location != null &&
                   location.CountryID != new Guid("37F06C2D-E5F6-4A6F-BB55-9DA3EC3B42A4")   //美国
                   && location.CountryID != new Guid("AF94FF33-AFAC-4D2E-B26D-66DF7AD6119E"))//中国
               {
                   MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Only China and the United States can get the CargoTrackingInfo." : "只有中国和美国可以获取箱动态.");
                   return;
               }
            }
            GetPositionAndSize();
            string searching = isEnglish ? "Searching the containerNo:" : "正在查询箱号:";
            int threadID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(string.Format(searching + "{0}", CurrentCargoTrackingContainer.ContainerNO));
            try
            {
                //    WaitDialogForm waitForm = new WaitDialogForm(string.Format(searching + "{0}", "TCNU3205490"), isEnglish ? "Loading..." : "请稍候...");
                //    Thread.Sleep(1500);
                //    waitForm.Close();
                //threadCargoTracking = new Thread(CargoTracking);
                //threadCargoTracking.Start();

                //获得码头列表
                GetTerminalList();
                //terminalList = terminalService.GetTerminalList(SearchUsing.ExportConfig);
                //if (terminalList.Count == 0)
                //{
                    //terminalList = TerminalService.GetTerminalList(ICP.Crawler.CommonLibrary.Enum.SearchUsing.ExportConfig);
                    //MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Loading the Terminal List,Please wait..." : "正在加载码头列表，请稍候...");
                    //return;
                //}
                ICP.Crawler.ServiceInterface.DataObjects.Terminals obj = null;
                if (terminalList.Count == 0)
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Can't find the Terminal." : "找不到码头.");
                    return;
                }
                else if (terminalList.Count == 1)
                {
                    obj = TerminalService.GetTerminalInfo(terminalList[0].ID, Crawler.CommonLibrary.Enum.SearchUsing.ExportConfig);
                }
                else
                {
                    contextMenuStrip1.Items.Clear();
                    foreach (var item in terminalList)
                    {
                        contextMenuStrip1.Items.Add(item.Code);
                    }
                    contextMenuStrip1.Show(sender as Control, new Point(e.X, e.Y));
                }
                if (obj != null)
                {
                    //保存船名POL默认码头
                    //string vessel = DataSource.Voyage.Substring(0, DataSource.Voyage.IndexOf('/'));
                    ////terminalService.SaveVesselTerminals(vessel, DataSource.POLCode, obj.ID);
                    //TerminalService.SaveVesselTerminals(vessel, DataSource.POLCode, obj.ID);
                    //箱动态快捷访问
                    CargoTracking(obj);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
            finally
            {
                ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(threadID);
            }
        }

        private void btnAvailability_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //CrawlerAvailability();
        }
        public IFileConvertService FileConvertService
        {
            get
            {
                return ServiceClient.GetService<IFileConvertService>();
            }
        }
        private static String GetTargetFilePath(String inputFile)
        {
            string rootPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, IOHelper.HtmlTempPath);
            return System.IO.Path.Combine(rootPath, System.IO.Path.GetFileNameWithoutExtension(inputFile) + ".pdf");

        }

        /// <summary>
        /// 将html转换为pdf文件
        /// </summary>
        /// <param name="htmlFilePath">html文件路径</param>
        /// <returns></returns>
        public string ConvertHtml2PDF(string htmlFilePath)
        {
            IOHelper.CheckFileExists(htmlFilePath, true);
            string outputFilePath = string.Empty;
            try
            {

                Aspose.Pdf.Generator.Pdf pdf = new Aspose.Pdf.Generator.Pdf();
                // add the section to PDF document sections collection
                Aspose.Pdf.Generator.Section section = pdf.Sections.Add();


                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(htmlFilePath);
                //System.Text.Encoding encoding = doc.DetectEncoding(htmlFilePath);

                using (System.IO.StreamReader reader = new System.IO.StreamReader(htmlFilePath))
                {
                    //Create text paragraphs containing HTML text
                    Aspose.Pdf.Generator.Text text2 = new Aspose.Pdf.Generator.Text(section, reader.ReadToEnd());
                    // enable the property to display HTML contents within their own formatting
                    text2.IsHtmlTagSupported = true;
                    //Add the text paragraphs containing HTML text to the section
                    section.Paragraphs.Add(text2);
                    pdf.IsAutoFontAdjusted = true;


                    outputFilePath = GetTargetFilePath(htmlFilePath);

                    pdf.Save(outputFilePath);
                    doc = null;
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0}{1}", "ConvertHtml2PDF Failure", ex.Message));
            }

            return outputFilePath;
        }

        void CargoTracking(ICP.Crawler.ServiceInterface.DataObjects.Terminals terminal)
        {
            try
            {
                terminal.Reference = CurrentCargoTrackingContainer.ContainerNO;
                object o = null;
                //判断码头的获取动态时间（RipperLastDate）
                ICP.Crawler.ServiceInterface.DataObjects.TerminalContainer terminalContainer = TerminalService.GetTerminalContainersInfo(terminal.ID, CurrentCargoTrackingContainer.ContainerID);
                if (terminalContainer != null && terminalContainer.RipperLastDate.Value.Date == DateTime.Now.Date)
                {
                    o = terminalContainer.CurrentAvailability;
                }
                else
                    o = CrawlerService.StartCrawler(terminal, Crawler.CommonLibrary.Enum.CrawlerMode.Quick);
                //object o = crawlerService.QuickCrawler(CurrentCargoTrackingContainer.ContainerNO, lstTerminalList.SelectedItem.ToString());
                if (o != null)
                {
                    string path = Application.StartupPath + "\\PDF";
                    if (!System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);
                    string filePath = string.Format(path + string.Format("\\{0}.pdf", CurrentCargoTrackingContainer.ContainerNO));
                    //string htmlPath=path + string.Format("\\{0}.html", CurrentCargoTrackingContainer.ContainerNO);
                    //System.IO.StreamWriter sw = new System.IO.StreamWriter(htmlPath, true);
                    //sw.WriteLine(o);
                    //sw.Close();
                    //ConvertHtml2PDF(htmlPath);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<!DOCTYPE html>");
                    sb.Append("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
                    sb.Append("<head>");
                    sb.Append("<meta charset=\"utf-8\" />");
                    sb.Append("<title></title>");
                    sb.Append("</head>");
                    sb.Append("<body>");
                    sb.Append(o.ToString());
                    sb.Append("</body>");
                    sb.Append("</html>");
                    String htmlpath = path + string.Format("\\{0}.html", CurrentCargoTrackingContainer.ContainerNO);
                    StreamWriter log = new StreamWriter(htmlpath, true);
                    log.WriteLine(sb.ToString());
                    log.Close();
                    ////PartLoader.ShowEditPart<ShowWeb>(Workitem, sb, isEnglish ? "Cargo Tracking" : "箱货跟踪", null);

                    //Create a pdf document.
                    PdfDocument doc = new PdfDocument();
                    PdfPageSettings pgSt = new PdfPageSettings();
                    pgSt.Size = PdfPageSize.A3;

                    PdfHtmlLayoutFormat htmlLayoutFormat = new PdfHtmlLayoutFormat();
                    htmlLayoutFormat.IsWaiting = false;

                    string source = File.ReadAllText(htmlpath);
                    doc.LoadFromHTML(source, true, pgSt, htmlLayoutFormat);

                    //Save pdf file.
                    doc.SaveToFile("FromHTML.pdf");
                    doc.Close();

                    //Launch the file.
                    //PDFDocumentViewer("FromHTML.pdf");

                    //EO.Pdf.PdfDocument result = new EO.Pdf.PdfDocument();
                    //EO.Pdf.HtmlToPdf.ConvertHtml(o.ToString(), result);
                    //result.Save(filePath);
                    ServiceClient.FilePreviewService.Preview("FromHTML.pdf", _location, _size, true);
                    //删除文件
                    if (!System.IO.Directory.Exists(htmlpath))
                    {
                        File.Delete(htmlpath);
                        File.Delete("FromHTML.pdf");
                    }

                    //CommonUIUtility.OpenWith("D:\\PDF.pdf");
                    //txtReturnLoc.Text = o.ToString();
                    ////ImageHelper.BytesToImage((byte[])o).Save("d:\\eft.jpg");
                    //if (o is string)  //返回异常信息
                    //    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), o.ToString());
                    //else
                    //    PartLoader.ShowEditPart<ImageEditPart>(Workitem, o, isEnglish ? "Availability" : "码头箱动态", null);
                }
                else
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), isEnglish ? "Can not find the data" : "没有查到相关数据");
                //}
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
        }

        private void gvContainer_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
        }

        //void SetTerminalList()
        //{
        //    try
        //    {
        //        this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
        //        //if (Context.OperationID != null && Context.OperationID != Guid.Empty)
        //        //{
        //        //CargoTrackingInfo cargoTrackingInfo = OperationService.GetCargoTrackingInfo(Context.OperationID);
        //        if (DataSource != null)
        //        {
        //            //获得码头列表
        //            //terminalList = terminalService.GetTerminalList(SearchUsing.ExportConfig);
        //            if (terminalList.Count == 0)
        //            {
        //                GetTerminalList();
        //                //terminalList = TerminalService.GetTerminalList(ICP.Crawler.CommonLibrary.Enum.SearchUsing.ExportConfig);
        //                //MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Loading the Terminal List,Please wait..." : "正在加载码头列表，请稍候...");
        //                //return;
        //            }
        //            List<ICP.Crawler.ServiceInterface.DataObjects.Terminals> list = terminalList.FindAll(o => o.LocationCode == DataSource.POLCode);
        //            //List<Terminals> list = terminalList.FindAll(o => o.LocationCode == DataSource.POLCode);
        //            if (list != null)
        //            {
        //                lstTerminalList.Items.Clear();
        //                foreach (var item in list)
        //                {
        //                    lstTerminalList.Items.Add(item.Code);
        //                }
        //                //设置默认值
        //                string vessel = string.Empty;
        //                if (!string.IsNullOrEmpty(DataSource.Voyage))
        //                    vessel = DataSource.Voyage.Substring(0, DataSource.Voyage.IndexOf('/'));
        //                ICP.Crawler.ServiceInterface.DataObjects.VesselTerminals vt = TerminalService.GetVesselTerminalsInfo(vessel, DataSource.POLCode);
        //                //VesselTerminals vt = terminalService.GetVesselTerminalsInfo(vessel, DataSource.POLCode);
        //                if (vt != null)
        //                    lstTerminalList.SelectedItem = terminalList.Find(o => o.ID == vt.TerminalID).Code;
        //                else if (lstTerminalList.Items.Count > 0)
        //                    lstTerminalList.SelectedIndex = 0;
        //            }
        //        }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
        //    }
        //    finally
        //    {
        //        this.Cursor = System.Windows.Forms.Cursors.Default;
        //    }
        //}

        private void gvContainer_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                if (e.Column == colAvailability)
                    e.Value = LocalData.IsEnglish ? "Get" : "获取";
            }
        }

        private void repositoryItemAvailability_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                //单击左键
                if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks == 1)
                {
                    CrawlerAvailability(sender, e);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ICP.Crawler.ServiceInterface.DataObjects.Terminals obj = null;
            Terminals t = terminalList.Find(o => o.Code == e.ClickedItem.Text);
            if (t != null)
            {
                obj = TerminalService.GetTerminalInfo(t.ID, Crawler.CommonLibrary.Enum.SearchUsing.ExportConfig);
            }
            if (obj != null)
            {
                //箱动态快捷访问
                CargoTracking(obj);
            }
        }
    }
    /// <summary>
    /// 配置信息
    /// </summary>
    public class CargoTrackingConfig
    {
        public string ID { get; set; }
        /// <summary>
        /// 海关放行
        /// </summary>
        public string CustomsReleasePath { get; set; }
        public string CustomsReleasePostData { get; set; }
        /// <summary>
        /// 确认上船
        /// </summary>
        public string LoadshipPath { get; set; }
        public string LoadshipPathPostData { get; set; }
        /// <summary>
        /// 进港
        /// </summary>
        public string GateInPath { get; set; }
        public string GateInPathPostData { get; set; }
        /// <summary>
        /// 综合查询url
        /// </summary>
        public string IGZPath { get; set; }
        /// <summary>
        /// 开/截港日url
        /// </summary>
        public string OpenPortPath { get; set; }
        /// <summary>
        /// 开/截港PostData
        /// </summary>
        public string OpenPortPostData { get; set; }
        /// <summary>
        /// 获取etd的url
        /// </summary>
        public string NetETDPath { get; set; }
        /// <summary>
        /// 获取etd的PostData
        /// </summary>
        public string NetETDPostData { get; set; }
    }
    /// <summary>
    /// 用于分析进场时间和确认上船
    /// </summary>
    public class DockClass
    {
        /// <summary>
        /// 提单号
        /// </summary>
        public string BLNO { get; set; }
        /// <summary>
        /// 进场日期
        /// </summary>
        public string JiChangDate { get; set; }
        /// <summary>
        /// 出场日期
        /// </summary>
        public string ChuChangDate { get; set; }
        /// <summary>
        /// 进场类型
        /// </summary>
        public string JiChangType { get; set; }
        /// <summary>
        /// 出场类型
        /// </summary>
        public string ChuChangType { get; set; }
        /// <summary>
        /// 是否配船
        /// </summary>
        public string PeiChuan { get; set; }
        /// <summary>
        /// 放关
        /// </summary>
        public string FangGuan { get; set; }
    }
    /// <summary>
    /// 港口（外一、外二……）
    /// </summary>
    public enum DockType
    {
        /// <summary>
        /// 外一
        /// </summary>
        WG1,
        /// <summary>
        /// 外二
        /// </summary>
        WG2,
        /// <summary>
        /// 外四
        /// </summary>
        WG4,
        /// <summary>
        /// 外五
        /// </summary>
        WG5,
        /// <summary>
        /// 洋一
        /// </summary>
        YS1,
        /// <summary>
        /// 洋三
        /// </summary>
        YS3,
        /// <summary>
        /// 宝山
        /// </summary>
        SCT
    }
}
