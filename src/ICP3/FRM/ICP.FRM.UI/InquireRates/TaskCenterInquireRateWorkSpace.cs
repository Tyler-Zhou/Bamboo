#region Comment

/*
 * 
 * FileName:    TaskCenterInquireRateWorkSpace.cs
 * CreatedOn:   2014/10/21 20:58:04
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.FRM.UI.InquireRates;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;

namespace ICP.FRM.UI
{
    public partial class TaskCenterInquireRateWorkSpace : XtraUserControl
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        
        #endregion

        #region 成员变量

        private InquireRatesSearchParameter para;

        /// <summary>
        /// 
        /// </summary>
        public string ViewCode
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string StrQuery
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public InquierType CurrentInquierType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string StrUnReply
        {
            get;
            set;
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_SearchData)]
        public event EventHandler<DataEventArgs<InquireRatesSearchParameter>> SearchDataEvent;
        #endregion

        #region 构造函数
        public TaskCenterInquireRateWorkSpace()
        {
            InitializeComponent();
            para = new InquireRatesSearchParameter();
            para.No = "";
            para.pod = "";
            para.pol = "";
            para.delivery = "";
            para.commodity = "";
            para.durationFrom = null;
            para.durationTo = null;
            para.StrQuery = "";
            StrUnReply = "";
        } 
        #endregion

        #region 窗体加载
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                #region 询价类型
                if (!string.IsNullOrEmpty(ViewCode))
                {
                    string[] vcs = ViewCode.Split('_');
                    switch (vcs[1])
                    {
                        case "OceanInquireRate":
                            CurrentInquierType = InquierType.OceanRates;
                            break;
                        case "TruckingInquireRate":
                            CurrentInquierType = InquierType.TruckingRates;
                            break;
                        case "AirInquireRate":
                            CurrentInquierType = InquierType.AirRates;
                            break;
                        default:
                            CurrentInquierType = InquierType.None;
                            break;
                    }
                    StrUnReply = vcs[2];
                } 
                #endregion

                #region 查询面板Reply类型
                switch (StrUnReply)
                {
                    case "UnReply":
                        para.isUnReply = true;
                        para.StrQuery = CurrentInquierType == InquierType.OceanRates 
                            ? " ($@IsHasRates@=0 OR $@InquirePriceOceanConfirmedBy@ is NULL) " 
                            : " $@IsHasRates@=0 ";
                        break;
                    case "Reply":
                        para.isUnReply = false;
                        para.StrQuery = "$@IsHasRates@=1 "; 
                        break;
                    default:
                        para.isUnReply = null;
                        para.StrQuery = "1=1 "; 
                        break;
                } 
                #endregion

                #region 高级查询

                if (!string.IsNullOrEmpty(StrQuery) && StrQuery.Contains("$@"))
                {
                    //StrQuery = StrQuery.Replace("$", string.Empty).Replace("@", string.Empty);
                    //StrQuery = StrQuery.Replace("No", "ip.No");
                    //StrQuery = StrQuery.Replace("Commodity", "ip.Commodity");
                    //StrQuery = StrQuery.Replace("IsHasRates", "ip.IsHasRates");
                    //StrQuery = StrQuery.Replace("UpdateDate", "ip.UpdateDate");
                    //StrQuery = StrQuery.Replace("POLEName", "POL_l.EName");
                    //StrQuery = StrQuery.Replace("POLCName", "POL_l.CName");
                    //StrQuery = StrQuery.Replace("PODEName", "POD_l.EName");
                    //StrQuery = StrQuery.Replace("PODCName", "POD_l.CName");
                    //StrQuery = StrQuery.Replace("DeliveryEName", "Delivery_l.EName");
                    //StrQuery = StrQuery.Replace("DeliveryCName", "Delivery_l.CName");
                    para.StrQuery = StrQuery;
                }
                //根据登录用户过滤
                para.StrQuery += " AND ($@RespondByID@='" + LocalData.UserInfo.LoginID + "' OR $@InquireByID@='" + LocalData.UserInfo.LoginID + "') ";
                #endregion

                para.inquireOrRespondBy = LocalData.UserInfo.LoginID;

                //清空原有面板
                if (Workitem.WorkItems.Contains("InquireOceanRatesWorkitem"))
                    Workitem.WorkItems["InquireOceanRatesWorkitem"].Dispose();
                if (Workitem.WorkItems.Contains("InquireTruckingRatesWorkitem"))
                    Workitem.WorkItems["InquireTruckingRatesWorkitem"].Dispose();
                if (Workitem.WorkItems.Contains("InquireAirRatesWorkitem"))
                    Workitem.WorkItems["InquireAirRatesWorkitem"].Dispose();
                Controls.Clear();
                //工具栏显示
                InquireRatesToolBar toolPart = Workitem.Items.AddNew<InquireRatesToolBar>();
                toolPart.IsTaskCenter = true;
                toolPart.CurInquierType = CurrentInquierType;
                toolPart.Dock = DockStyle.Top;
                Controls.Add(toolPart);
                
                switch (CurrentInquierType)
                {
                    case InquierType.OceanRates:
                        InquireOceanRatesWorkitem oceanWorkitem = Workitem.WorkItems.AddNew<InquireOceanRatesWorkitem>("InquireOceanRatesWorkitem");
                        oceanWorkitem.Run();
                
                        if (oceanWorkitem.OceanRateMainWorkSpace != null)
                        {
                            oceanWorkitem.OceanRateMainWorkSpace.Dock = DockStyle.Fill;
                            Controls.Add(oceanWorkitem.OceanRateMainWorkSpace);
                            para.type = InquierType.OceanRates;
                        }
                        break;
                    case InquierType.TruckingRates:
                        InquireTruckingRatesWorkitem truckWorkitem = Workitem.WorkItems.AddNew<InquireTruckingRatesWorkitem>("InquireTruckingRatesWorkitem");
                        truckWorkitem.Run();

                        if (truckWorkitem.TruckingRatesMainWorkspace != null)
                        {
                            truckWorkitem.TruckingRatesMainWorkspace.Dock = DockStyle.Fill;
                            Controls.Add(truckWorkitem.TruckingRatesMainWorkspace);
                            para.type = InquierType.TruckingRates;
                        }
                        break;
                    case InquierType.AirRates:
                        InquireAirRatesWorkitem airWorkitem = Workitem.WorkItems.AddNew<InquireAirRatesWorkitem>("InquireAirRatesWorkitem");
                        airWorkitem.Run();

                        if (airWorkitem.AirRatesMainWorkspace != null)
                        {
                            airWorkitem.AirRatesMainWorkspace.Dock = DockStyle.Fill;
                            Controls.Add(airWorkitem.AirRatesMainWorkspace);
                            para.type = InquierType.AirRates;
                        }
                        break;
                }
                toolPart.SendToBack();
                if (SearchDataEvent != null)
                {
                    SearchDataEvent(this, new DataEventArgs<InquireRatesSearchParameter>(para));
                }
            }

        }

        #endregion
    }
}
