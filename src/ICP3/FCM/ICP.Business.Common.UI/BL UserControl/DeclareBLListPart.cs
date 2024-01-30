using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Business.Common.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.EDI.ServiceInterface;
using ICP.EDI.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ICP.Business.Common.UI.BL
{
    /// <summary>
    /// 提单用户控件类
    /// </summary>
    [SmartPart]
    public partial class DeclareBLListPart : UserControl, IBaseBLPart, IDataBind
    {
        #region Fields & Property & Services & Contact

        #region 变量
        /// <summary>
        /// OceanBLList 提单列表数据对象
        /// </summary>
        private OceanBLList oceanBlList = null;
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
        /// 海出客户端服务
        /// </summary>
        public IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }

        }

        /// <summary>
        /// 海出服务
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        /// <summary>
        /// ICP通用UI辅助类
        /// </summary>
        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private IEDIClientService EDIClientService
        {
            get
            {
                return ServiceClient.GetClientService<IEDIClientService>();
            }
        }

        /// <summary>
        /// EDI服务
        /// </summary>
        private IEDIService EDIService
        {
            get
            {
                return ServiceClient.GetService<IEDIService>();
            }
        }

        #endregion

        #region 属性
        /// <summary>
        /// bsBLList数据源
        /// </summary>
        public object DataSource
        {
            get
            {
                return bsBLList.DataSource;
            }
            set
            {
                bsBLList.DataSource = value;
                bsBLList.ResetBindings(false);
                Workitem.Commands[BLWorkSpaceConstants.BLCommand_PositionChanged].Execute();
            }
        }

        /// <summary>
        /// 业务操作上下文
        /// </summary>
        public BusinessOperationContext CurrentContext { get; set; }

        /// <summary>
        /// ICP公用操作
        /// </summary>
        public IICPCommonOperationService IicpCommonOperation
        {
            get { return ServiceClient.GetClientService<IICPCommonOperationService>(); }
        }

        /// <summary>
        /// 当前行
        /// </summary>
        public OceanBLList CurrentRow { get { return bsBLList.Current == null ? null : bsBLList.Current as OceanBLList; } }

        /// <summary>
        /// gvBLList选中行
        /// </summary>
        List<OceanBLList> SelectedItems
        {
            get
            {
                List<OceanBLList> tagers = new List<OceanBLList>();

                int[] Handle = gvBLList.GetSelectedRows();
                for (int i = 0; i < Handle.Length; i++)
                {
                    OceanBLList dataRow = (OceanBLList)gvBLList.GetRow(Handle[i]);
                    tagers.Add(dataRow);
                }
                return tagers;
            }
        }

        /// <summary>
        /// 模板编号
        /// </summary>
        public string TemplateCode { get; set; }
        #endregion

        #region 常量
        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();
        #endregion

        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public DeclareBLListPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                _barItemDic.Clear();
                _barItemDic = null;

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            if (!LocalData.IsEnglish && !LocalData.IsDesignMode)
            {
                barAdd.Caption = "新增";
                barCopy.Caption = "复制";
                barDelete.Caption = "删除";
                barPrint.Caption = "打印";
                barRefersh.Caption = "刷新";
                barSplitAndMerge.Caption = "分单/合单";
                barEdit.Caption = "编辑";
                barProfit.Caption = "利润打印";
                barPrintLoadGoods.Caption = "打印装箱单";
                barSplitBL.Caption = "分单";
                barMergeBL.Caption = "合单";
                barbl.Caption = "客户确认补料";
                barchs.Caption = "向客户确认提单(中文版)";
                bareng.Caption = "向客户确认提单(英文版)";
                bartoAgentchs.Caption = "向代理确认所有提单(中文版)";
                bartoAgenteng.Caption = "向代理确认所有提单(英文版)";
                barPrintBL.Caption = "打印提单";
            }
            RegisterEvents();

        }
        #endregion

        #region 初始化

        #region USBLToolPart
        /// <summary>
        /// 注册事件
        /// </summary>
        void RegisterEvents()
        {
            barAddHBL.ItemClick += barAddHBL_ItemClick;
            barAddMBL.ItemClick += barAddMBL_ItemClick;
            barEdit.ItemClick += barEdit_ItemClick;
            barCopy.ItemClick += barCopy_ItemClick;
            barDelete.ItemClick += barDelete_ItemClick;
            barPrintBL.ItemClick += barPrintBL_ItemClick;
            barRefersh.ItemClick += barRefersh_ItemClick;
            barBooking.ItemClick += barBooking_ItemClick;
            barContainerLoad.ItemClick += barContainerLoad_ItemClick;
            barPreplan.ItemClick += barPreplan_ItemClick;
            barSupplement.ItemClick += barSupplement_ItemClick;
            barWharf.ItemClick += barWharf_ItemClick;
            barVGM.ItemClick += barVGM_ItemClick;
            barAPICLP.ItemClick += BarAPICLP_ItemClick;
        }

        



        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barRefersh_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_Refersh].Execute();
        }

        /// <summary>
        /// 打印提单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barPrintBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_PrintBL].Execute();
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_Remove].Execute();
        }
        /// <summary>
        /// 添加HBL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barAddHBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_AddDeclareHBL].Execute();
        }
        /// <summary>
        /// 添加MBL
        /// </summary>  
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barAddMBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_AddMBL].Execute();
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_Edit].Execute();
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_Copy].Execute();
        }

        /// <summary>
        /// 订舱EDI
        /// </summary>
        void barBooking_ItemClick(object sender, ItemClickEventArgs e)
        {
            Guid[] mblid;
            OceanBLList ocean;
            if (SelectedItems == null || SelectedItems.Count == 0)
            {
                ocean = (OceanBLList)gvBLList.GetRow(0);
                mblid = new Guid[] { ocean.MBLID };
            }
            else
            {
                ocean = SelectedItems[0];
                mblid = new Guid[] { SelectedItems[0].MBLID };
            }



            bool flag = IicpCommonOperation.ExecGetEDIDataSourceForNBEDIInfos(0, mblid);
            if (flag)
            {
                List<Guid> mblIds = new List<Guid>();
                List<Guid> ids = new List<Guid>(1);
                mblIds.Add(ocean.MBLID);
                ids.Add(ocean.OceanBookingID);
                List<string> nos = new List<string>(1);
                nos.Add(ocean.No);

                EDISendOption sendItem = new EDISendOption();
                sendItem.ServiceKey = "NBEDIBookingANL";
                sendItem.EdiMode = EDIMode.Booking;
                sendItem.CompanyID = ocean.CompanyID;
                sendItem.Subject = "电子订舱(";
                sendItem.Subject += nos[0];
                sendItem.Subject += ")";
                sendItem.IDs = ids.ToArray();
                sendItem.FIDs = mblIds.ToArray();
                sendItem.NOs = nos.ToArray();
                sendItem.OperationType = OperationType.OceanExport;

                bool isSucc = EDIClientService.SendEDI(sendItem);
                if (isSucc)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                }
            }


        }

        /// <summary>
        /// 电子装箱
        /// </summary>
        void barContainerLoad_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentRow == null) return;
            if (CurrentRow.BLType != FCMBLType.MBL) return;
            List<Guid> customerIDList = new List<Guid>();
            if (CurrentRow.CarrierID != null)
            {
                customerIDList.Add(CurrentRow.CarrierID.Value);
            }
            if (CurrentRow.AgentOfCarrierID != null)
            {
                customerIDList.Add(CurrentRow.AgentOfCarrierID.Value);
            }
            EDISendOption sendItem = new EDISendOption
            {
                ServiceKey = "",
                EdiMode = EDIMode.ContainerLoad,
                CompanyID = CurrentRow.CompanyID,
                CarrierID = CurrentRow.CarrierID!=null?CurrentRow.CarrierID.Value:Guid.Empty,
                AgentOfCarrierID = CurrentRow.AgentOfCarrierID != null ? CurrentRow.AgentOfCarrierID.Value : Guid.Empty,
                Subject = string.Format("电子装箱({0})", CurrentRow.No),
                IDs = new[] { CurrentContext.OperationID },
                NOs = new[] { CurrentContext.OperationNO },
                FIDs = new[] { CurrentRow.ID },
                FNOs = new[] { CurrentRow.No },
                OperationType = OperationType.OceanExport,
                SendByID = LocalData.UserInfo.LoginID,
            };

            bool isSucc = EDIClientService.ShowForm(sendItem,true);
            if (isSucc)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
            }
        }

        /// <summary>
        /// 预配EDI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barPreplan_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<OceanBLList> list = SelectedItems.FindAll(r => r.ID != r.MBLID);
            if (list == null || list.Count == 0)
            {
                MessageBoxService.ShowWarning("请选择正确的报关单发送订舱EDI", "Tips");
                return;
            }

            Guid[] ids = new Guid[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                ids[i] = list[i].ID;
            }

            bool flag = IicpCommonOperation.ExecGetEDIDataSourceForNBEDIInfos(1, ids);

            if (flag)
            {
                string subjuect = string.Empty;
                string toEmail = string.Empty;
                System.Text.StringBuilder mblNoBuilder = new System.Text.StringBuilder();
                List<string> operationNos = new List<string>();
                List<Guid> mblIds = new List<Guid>();
                List<Guid> oIds = new List<Guid>();
                string key = string.Empty;
                string tip = string.Empty;

                foreach (OceanBLList bl in list)
                {
                    mblNoBuilder.Append(bl.No + ";");
                    operationNos.Add(bl.No);
                    mblIds.Add(bl.ID);
                    oIds.Add(bl.OceanBookingID);
                }
                mblNoBuilder.Remove(mblNoBuilder.Length - 1, 1);

                subjuect = LocalData.IsEnglish ? "APLPre(" + mblNoBuilder.ToString() + ")" : "APL预配(" + mblNoBuilder.ToString() + ")";
                key = "NBEDIBookingANL";
                tip = LocalData.IsEnglish ? "NBEDIForAPL" : "宁波APL预配";

                EDISendOption sendItem = new EDISendOption();
                sendItem.ServiceKey = key;
                sendItem.EdiMode = EDIMode.SI;
                sendItem.CompanyID = list[0].CompanyID;
                sendItem.Subject = subjuect;
                sendItem.IDs = oIds.ToArray();
                sendItem.FIDs = mblIds.ToArray();
                sendItem.NOs = operationNos.ToArray();
                sendItem.OperationType = OperationType.OceanExport;
                sendItem.SendByID = LocalData.UserInfo.LoginID;

                bool isSucc = EDIClientService.SendEDI(sendItem);
                if (isSucc)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                }
            }
        }

        /// <summary>
        /// 补料EDI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barSupplement_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<OceanBLList> list = (bsBLList.DataSource as List<OceanBLList>);

            List<OceanBLList> findlist = list.FindAll(r => r.ID == r.MBLID);
            if (findlist == null || findlist.Count == 0)
            {
                MessageBoxService.ShowWarning("该业务下没有需要发送的MBL", "Tips");
                return;
            }

            Guid[] ids = new Guid[findlist.Count];
            for (int i = 0; i < findlist.Count; i++)
            {
                ids[i] = findlist[i].ID;
            }

            bool flag = IicpCommonOperation.ExecGetEDIDataSourceForNBEDIInfos(2, ids);

            if (flag)
            {
                string subjuect = string.Empty;
                string toEmail = string.Empty;
                System.Text.StringBuilder mblNoBuilder = new System.Text.StringBuilder();
                List<string> operationNos = new List<string>();
                List<Guid> mblIds = new List<Guid>();
                List<Guid> oIds = new List<Guid>();
                string key = string.Empty;
                string tip = string.Empty;
                string[] shipArr = new string[findlist.Count];
                string[] ConsigneeArr = new string[findlist.Count];
                string[] NoticeArr = new string[findlist.Count];
                string[] otherArr = new string[findlist.Count];

                int i = 0;
                string strTemp = string.Empty;
                string strOther = string.Empty;
                string[] arrs;
                foreach (OceanBLList bl in findlist)
                {
                    mblNoBuilder.Append(bl.No + ";");
                    operationNos.Add(bl.No);
                    mblIds.Add(bl.ID);
                    oIds.Add(bl.OceanBookingID);

                    OceanMBLInfo mbl = OceanExportService.GetOceanMBLInfo(bl.ID);
                    strTemp = CutStringByLength(mbl.ShipperDescription.BaseToString(false).Replace("<", "").Replace(">", ""), 34);
                    arrs = strTemp.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < arrs.Length; j++)//只取5行
                    {
                        if (j == 0)
                        {
                            strTemp = "<text>" + arrs[j] + "</text>";
                        }
                        else if (j < 4)
                        {
                            strTemp += Environment.NewLine + "<text>" + arrs[j] + "</text>";
                        }
                        else if (j == 4 && arrs.Length > 5)
                        {
                            strTemp += Environment.NewLine + "<text>" + arrs[j] + "*" + "</text>";
                        }
                        else if (j == 4)
                        {
                            strTemp += Environment.NewLine + "<text>" + arrs[j] + "</text>";
                        }
                        else if (j == 5)
                        {
                            strOther += Environment.NewLine + "*" + arrs[j];
                        }
                        else
                        {
                            strOther += Environment.NewLine + arrs[j];
                        }
                    }
                    shipArr[i] = strTemp;

                    strTemp = CutStringByLength(mbl.ConsigneeDescription.BaseToString(false).Replace("<", "").Replace(">", ""), 33);
                    arrs = strTemp.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < arrs.Length; j++)//只取5行
                    {
                        if (j < arrs.Length)
                        {
                            if (j == 0)
                            {
                                strTemp = "<text>" + arrs[j] + "</text>";
                            }
                            else if (j < 4)
                            {
                                strTemp += Environment.NewLine + "<text>" + arrs[j] + "</text>";
                            }
                            else if (j == 4 && arrs.Length > 5)
                            {
                                strTemp += Environment.NewLine + "<text>" + arrs[j] + "**" + "</text>";
                            }
                            else if (j == 4)
                            {
                                strTemp += Environment.NewLine + "<text>" + arrs[j] + "</text>";
                            }
                            else if (j == 5)
                            {
                                strOther += Environment.NewLine + "**" + arrs[j];
                            }
                            else
                            {
                                strOther += Environment.NewLine + arrs[j];
                            }
                        }
                    }
                    ConsigneeArr[i] = strTemp;

                    strTemp = CutStringByLength(mbl.NotifyPartyDescription.BaseToString(false).Replace("<", "").Replace(">", ""), 32);
                    arrs = strTemp.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < arrs.Length; j++)//只取5行
                    {
                        if (j < arrs.Length)
                        {
                            if (j == 0)
                            {
                                strTemp = "<text>" + arrs[j] + "</text>";
                            }
                            else if (j < 4)
                            {
                                strTemp += Environment.NewLine + "<text>" + arrs[j] + "</text>";
                            }
                            else if (j == 4 && arrs.Length > 5)
                            {
                                strTemp += Environment.NewLine + "<text>" + arrs[j] + "***" + "</text>";
                            }
                            else if (j == 4)
                            {
                                strTemp += Environment.NewLine + "<text>" + arrs[j] + "</text>";
                            }
                            else if (j == 5)
                            {
                                strOther += Environment.NewLine + "***" + arrs[j];
                            }
                            else
                            {
                                strOther += Environment.NewLine + arrs[j];
                            }
                        }
                    }
                    NoticeArr[i] = strTemp;

                    otherArr[i] = strOther;
                    i++;
                }
                mblNoBuilder.Remove(mblNoBuilder.Length - 1, 1);

                subjuect = LocalData.IsEnglish ? "APLSI(" + mblNoBuilder.ToString() + ")" : "APL补料(" + mblNoBuilder.ToString() + ")";
                key = "NBEDISIANL";
                tip = LocalData.IsEnglish ? "NBEDIForSI" : "宁波EDI补料";

                EDISendOption sendItem = new EDISendOption();
                sendItem.ServiceKey = key;
                sendItem.EdiMode = EDIMode.SI;
                sendItem.CompanyID = list[0].CompanyID;
                sendItem.Subject = subjuect;
                sendItem.IDs = oIds.ToArray();
                sendItem.FIDs = mblIds.ToArray();
                sendItem.NOs = operationNos.ToArray();
                sendItem.OperationType = OperationType.OceanExport;
                sendItem.SendByID = LocalData.UserInfo.LoginID;


                sendItem.ShipperFormat = shipArr;
                sendItem.ConsigneeFormat = ConsigneeArr;
                sendItem.NotifyFormat = NoticeArr;
                sendItem.OtherFormat = otherArr;

                bool isSucc = EDIClientService.SendEDI(sendItem);
                if (isSucc)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                }
            }
        }

        /// <summary>
        /// 倒角转半角
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32; continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        private string CutStringByLength(string strInput, int cutLength)
        {
            string returnStr = string.Empty;
            //string text = strInput.Replace(Environment.NewLine, " ");
            string[] stringArr = strInput.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            List<string> stringList = stringArr.ToList();
            string obj = string.Empty;
            for (int i = 0; i < stringArr.Length; i++)
            {
                if (stringArr[i].Length > cutLength)
                {
                    stringArr[i].Replace(",", ", ");
                    string[] larger = stringArr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    obj = string.Empty;
                    foreach (string str in larger)
                    {
                        if ((obj + str).Length > cutLength)
                        {
                            if (string.IsNullOrEmpty(returnStr))
                            {
                                returnStr = obj;
                            }
                            else
                            {
                                returnStr += Environment.NewLine + obj;
                            }

                            obj = str;
                        }
                        else
                        {
                            obj += " " + str;
                        }
                    }

                    if (string.IsNullOrEmpty(returnStr))
                    {
                        returnStr = obj;
                    }
                    else
                    {
                        returnStr += Environment.NewLine + obj;
                    }
                }
                else
                {
                    returnStr += Environment.NewLine + stringArr[i];
                }
            }


            return returnStr;
        }


        /// <summary>
        /// 码头EDI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barWharf_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<OceanBLList> list = (bsBLList.DataSource as List<OceanBLList>);
            if (list == null || list.Count == 0)
            {
                return;
            }

            List<OceanBLList> findlist = list.FindAll(r => r.ID != r.MBLID);
            if (findlist == null || findlist.Count == 0)
            {
                MessageBoxService.ShowWarning("该业务下没有需要发送的报关单", "Tips");
                return;
            }

            Guid[] ids = new Guid[findlist.Count];
            for (int i = 0; i < findlist.Count; i++)
            {
                ids[i] = findlist[i].ID;
            }

            bool flag = IicpCommonOperation.ExecGetEDIDataSourceForNBEDIInfos(3, ids);

            if (flag)
            {
                string subjuect = string.Empty;
                string toEmail = string.Empty;
                System.Text.StringBuilder mblNoBuilder = new System.Text.StringBuilder();
                List<string> operationNos = new List<string>();
                List<Guid> mblIds = new List<Guid>();
                List<Guid> oIds = new List<Guid>();
                string key = string.Empty;
                string tip = string.Empty;

                foreach (OceanBLList bl in findlist)
                {
                    mblNoBuilder.Append(bl.No + ";");
                    operationNos.Add(bl.No);
                    mblIds.Add(bl.ID);
                    oIds.Add(bl.OceanBookingID);
                }

                mblNoBuilder.Remove(mblNoBuilder.Length - 1, 1);

                subjuect = LocalData.IsEnglish ? "NingBoEDI(" + mblNoBuilder.ToString() + ")" : "宁波EDI中心(" + mblNoBuilder.ToString() + ")";
                key = "NBEDICenter";
                tip = LocalData.IsEnglish ? "NBEDI" : "宁波EDI中心";

                EDISendOption sendItem = new EDISendOption();
                sendItem.ServiceKey = key;
                sendItem.EdiMode = EDIMode.SI;
                sendItem.CompanyID = list[0].CompanyID;
                sendItem.Subject = subjuect;
                sendItem.IDs = oIds.ToArray();
                sendItem.FIDs = mblIds.ToArray();
                sendItem.NOs = operationNos.ToArray();
                sendItem.OperationType = OperationType.OceanExport;
                sendItem.SendByID = LocalData.UserInfo.LoginID;

                bool isSucc = EDIClientService.SendEDI(sendItem);
                if (isSucc)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                }
            }
        }

        /// <summary>
        /// VGM EDI
        /// </summary>
        void barVGM_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<OceanBLList> list = (bsBLList.DataSource as List<OceanBLList>);
            if (list == null || list.Count == 0)
            {
                return;
            }

            List<OceanBLList> findlist = list.FindAll(r => r.ID == SelectedItems[0].MBLID);
            if (findlist == null || findlist.Count != 1)
            {
                MessageBoxService.ShowWarning("请选择MBL发送VGM", "Tips");
                return;
            }

            string subjuect = string.Empty;
            string toEmail = string.Empty;
            System.Text.StringBuilder mblNoBuilder = new System.Text.StringBuilder();
            List<string> operationNos = new List<string>();
            List<Guid> mblIds = new List<Guid>();
            List<Guid> oIds = new List<Guid>();
            string key = string.Empty;
            string tip = string.Empty;

            string[] shipArr = new string[findlist.Count];
            string[] ConsigneeArr = new string[findlist.Count];
            string[] NoticeArr = new string[findlist.Count];
            string[] otherArr = new string[findlist.Count];

            int i = 0;
            string strTemp = string.Empty;
            string strOther = string.Empty;
            string[] arrs;

            foreach (OceanBLList bl in findlist)
            {
                mblNoBuilder.Append(bl.No + ";");
                operationNos.Add(bl.No);
                mblIds.Add(bl.ID);
                oIds.Add(bl.OceanBookingID);

                OceanMBLInfo mbl = OceanExportService.GetOceanMBLInfo(bl.ID);
                strTemp = CutStringByLength(mbl.ShipperDescription.BaseToString(false).Replace("<", "").Replace(">", ""), 34);
                arrs = strTemp.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < arrs.Length; j++)//只取5行
                {
                    if (j == 0)
                    {
                        strTemp = "<text>" + arrs[j] + "</text>";
                    }
                    else if (j < 4)
                    {
                        strTemp += Environment.NewLine + "<text>" + arrs[j] + "</text>";
                    }
                    else if (j == 4 && arrs.Length > 5)
                    {
                        strTemp += Environment.NewLine + "<text>" + arrs[j] + "*" + "</text>";
                    }
                    else if (j == 4)
                    {
                        strTemp += Environment.NewLine + "<text>" + arrs[j] + "</text>";
                    }
                    else if (j == 5)
                    {
                        strOther += Environment.NewLine + "*" + arrs[j];
                    }
                    else
                    {
                        strOther += Environment.NewLine + arrs[j];
                    }
                }

                shipArr[i] = strTemp;

                strTemp = CutStringByLength(mbl.ConsigneeDescription.BaseToString(false).Replace("<", "").Replace(">", ""), 33);
                arrs = strTemp.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < arrs.Length; j++)//只取5行
                {
                    if (j < arrs.Length)
                    {
                        if (j == 0)
                        {
                            strTemp = "<text>" + arrs[j] + "</text>";
                        }
                        else if (j < 4)
                        {
                            strTemp += Environment.NewLine + "<text>" + arrs[j] + "</text>";
                        }
                        else if (j == 4 && arrs.Length > 5)
                        {
                            strTemp += Environment.NewLine + "<text>" + arrs[j] + "**" + "</text>";
                        }
                        else if (j == 4)
                        {
                            strTemp += Environment.NewLine + "<text>" + arrs[j] + "</text>";
                        }
                        else if (j == 5)
                        {
                            strOther += Environment.NewLine + "**" + arrs[j];
                        }
                        else
                        {
                            strOther += Environment.NewLine + arrs[j];
                        }
                    }
                }

                ConsigneeArr[i] = strTemp;

                strTemp = CutStringByLength(mbl.NotifyPartyDescription.BaseToString(false).Replace("<", "").Replace(">", ""), 32);
                arrs = strTemp.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < arrs.Length; j++)//只取5行
                {
                    if (j < arrs.Length)
                    {
                        if (j == 0)
                        {
                            strTemp = "<text>" + arrs[j] + "</text>";
                        }
                        else if (j < 4)
                        {
                            strTemp += Environment.NewLine + "<text>" + arrs[j] + "</text>";
                        }
                        else if (j == 4 && arrs.Length > 5)
                        {
                            strTemp += Environment.NewLine + "<text>" + arrs[j] + "***" + "</text>";
                        }
                        else if (j == 4)
                        {
                            strTemp += Environment.NewLine + "<text>" + arrs[j] + "</text>";
                        }
                        else if (j == 5)
                        {
                            strOther += Environment.NewLine + "***" + arrs[j];
                        }
                        else
                        {
                            strOther += Environment.NewLine + arrs[j];
                        }
                    }
                }

                NoticeArr[i] = strTemp;

                otherArr[i] = strOther;
                i++;
            }
            mblNoBuilder.Remove(mblNoBuilder.Length - 1, 1);

            subjuect = LocalData.IsEnglish ? "NingBoEDIVGM(" + mblNoBuilder.ToString() + ")" : "宁波EDIVGM(" + mblNoBuilder.ToString() + ")";
            key = "NBEDIVGMANL";
            tip = LocalData.IsEnglish ? "NBEDIVGM" : "宁波EDIVGM";

            EDISendOption sendItem = new EDISendOption();
            sendItem.ServiceKey = key;
            sendItem.EdiMode = EDIMode.SI;
            sendItem.CompanyID = list[0].CompanyID;
            sendItem.Subject = subjuect;
            sendItem.IDs = oIds.ToArray();
            sendItem.FIDs = mblIds.ToArray();
            sendItem.NOs = operationNos.ToArray();
            sendItem.OperationType = OperationType.OceanExport;
            sendItem.SendByID = LocalData.UserInfo.LoginID;

            sendItem.ShipperFormat = shipArr;
            sendItem.ConsigneeFormat = ConsigneeArr;
            sendItem.NotifyFormat = NoticeArr;
            sendItem.OtherFormat = otherArr;

            bool isSucc = EDIClientService.SendEDI(sendItem);
            if (isSucc)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
            }

        }

        /// <summary>
        /// EDI CLP(宁波无纸化箱单)
        /// </summary>
        void BarAPICLP_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentRow == null) return;
            if (CurrentRow.BLType != FCMBLType.MBL) return;
            List<Guid> customerIDList = new List<Guid>();
            if (CurrentRow.CarrierID != null)
            {
                customerIDList.Add(CurrentRow.CarrierID.Value);
            }
            if (CurrentRow.AgentOfCarrierID != null)
            {
                customerIDList.Add(CurrentRow.AgentOfCarrierID.Value);
            }
            EDISendOption sendItem = new EDISendOption
            {
                ServiceKey = "",
                EdiMode = EDIMode.PCLP,
                CompanyID = CurrentRow.CompanyID,
                CarrierID = CurrentRow.CarrierID != null ? CurrentRow.CarrierID.Value : Guid.Empty,
                AgentOfCarrierID = CurrentRow.AgentOfCarrierID != null ? CurrentRow.AgentOfCarrierID.Value : Guid.Empty,
                Subject = string.Format("无纸化箱单({0})", CurrentRow.No),
                IDs = new[] { CurrentContext.OperationID },
                NOs = new[] { CurrentContext.OperationNO },
                FIDs = new[] { CurrentRow.ID },
                FNOs = new[] { CurrentRow.No },
                OperationType = OperationType.OceanExport,
                SendByID = LocalData.UserInfo.LoginID,
            };

            bool isSucc = EDIClientService.ShowForm(sendItem,true);
            if (isSucc)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
            }
        }
        #endregion

        #region USBLListPart
        /// <summary>
        /// 重写加载方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && !LocalData.IsEnglish)
            {
                InitControls();
            }

            List<EnumHelper.ListItem<OECompletionStatus>> oeCompletionStatus =
                EnumHelper.GetEnumValues<OECompletionStatus>(LocalData.IsEnglish);
            repBLCfm.Items.Clear();
            repMBLD.Items.Clear();
            repReleaseState.Items.Clear();
            foreach (var item in oeCompletionStatus)
            {
                repBLCfm.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                repMBLD.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            List<EnumHelper.ListItem<FCMReleaseState>> fcmReleaseState =
              EnumHelper.GetEnumValues<FCMReleaseState>(LocalData.IsEnglish);
            foreach (var listItem in fcmReleaseState)
            {
                if (listItem.Value != FCMReleaseState.Unknown)
                {
                    repReleaseState.Items.Add(new ImageComboBoxItem(listItem.Name,
                                                                                               listItem.Value));
                }
            }

            List<EnumHelper.ListItem<FCMReleaseType>> fcmReleasetype =
             EnumHelper.GetEnumValues<FCMReleaseType>(LocalData.IsEnglish);
            foreach (var listItem in fcmReleasetype)
            {
                if (listItem.Value != FCMReleaseType.Unknown)
                {
                    repReleasetype.Items.Add(new ImageComboBoxItem(listItem.Name,
                                                                                               listItem.Value));
                }
            }

            gvBLList.KeyDown += gvBLList_KeyDown;
        }

        /// <summary>
        /// 初始化控件(设置控件显示文本)
        /// </summary>
        void InitControls()
        {
            if (pnlGridList.Controls.Count == 0)
                pnlGridList.Controls.Add(gcBLList);

            colBLNo.Caption = "提单号";
            colShipperName.Caption = "发货人";
            colConsigneeName.Caption = "收货人";
            colNotifyPartyName.Caption = "通知人";
            colReleaseState.Caption = "放单类型";
            colReleaseType.Caption = "放货类型";
            colTelexNo.Caption = "电放号";
            ToolStripMenuItemF5.Text = "按选中查询(F5)";

        }
        #endregion

        void RefershToolBar()
        {
            Workitem.State[BLWorkSpaceConstants.BLCommandState_BLInfo] = CurrentRow;
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_PositionChanged].Execute();
        }
        #endregion

        #region 窗体事件

        /// <summary>
        /// 网格焦点行改变
        /// </summary>
        private void gvBLList_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            oceanBlList = gvBLList.GetRow(e.FocusedRowHandle) as OceanBLList;
        }

        /// <summary>
        /// 网格按键
        /// </summary>
        private void gcBLList_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                try
                {
                    Clipboard.SetText(gvBLList.GetFocusedDisplayText());
                }
                catch
                {
                    try
                    {
                        Clipboard.SetText(gvBLList.GetFocusedDisplayText());
                    }
                    catch (Exception ex)
                    {

                    }
                }
                e.Handled = true;
            }
        }

        /// <summary>
        /// 行双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvBLList_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (CurrentRow != null) Workitem.Commands[BLWorkSpaceConstants.BLCommand_Edit].Execute();
            }
        }
        #endregion

        #region DataBind
        /// <summary>
        /// bsBLList绑定数据
        /// </summary>
        /// <param name="blList">数据源</param>
        public void BindData(List<OceanBLList> blList)
        {
            DataSource = null;
            DataSource = blList;
        }

        /// <summary>
        /// gcBLList绑定数据
        /// </summary>
        /// <param name="context"></param>
        public void DataBind(BusinessOperationContext context)
        {
            CurrentContext = context;
            if (CurrentContext.OperationID == Guid.Empty)
            {
                DataSource = new List<OceanBLList>();
                return;
            }

            WaitCallback callback = (temp) =>
            {
                try
                {
                    ClientHelper.SetApplicationContext();
                    List<OceanBLList> blList = OceanExportService.GetDeclareBLListByIds(context.OperationID);
                    if (IsDisposed)
                        return;
                    BLListBindDataDelegate bindDelegate = BindData;
                    if (blList.Any())
                    {
                        if (blList[0].OceanBookingID != context.OperationID)
                        {
                            blList = new List<OceanBLList>();
                        }
                    }
                    Invoke(bindDelegate, new object[] { blList });

                }
                catch (Exception ex)
                {
                    if (!IsDisposed)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                    }
                }


            };
            ThreadPool.QueueUserWorkItem(callback);
        }
        /// <summary>
        /// 控件只读
        /// </summary>
        /// <param name="flg">是否只读</param>
        public void ControlsReadOnly(bool flg)
        {
            //TODO:启用禁用菜单栏
        }
        #endregion

        #region 自定义方法
        /// <summary>
        /// 控件设置启用禁用
        /// </summary>
        /// <param name="name"></param>
        /// <param name="enable"></param>
        public void SetEnable(string name, bool enable)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Enabled = enable;
        }

        /// <summary>
        /// 当前的TemplateCode和上下文中的TemplateCode是否相等
        /// </summary>
        /// <returns></returns>
        public bool IsEqual()
        {
            return IicpCommonOperation.TemplateCode == TemplateCode;
        }

        /// <summary>
        /// 当前选中行项是否为空
        /// </summary>
        /// <returns></returns>
        private bool IsNullObject()
        {
            bool isNull = true;
            if (CurrentRow != null && CurrentContext != null)
            {
                isNull = false;
            }
            return isNull;
        }

        /// <summary>
        /// 创造业务参数
        /// </summary>
        /// <param name="actionType">动作类型</param>
        /// <param name="isNewOrder">是否新单</param>
        /// <returns></returns>
        public Dictionary<string, object> CreateBusinessParameter(ActionType actionType, bool isNewOrder)
        {
            BusinessOperationParameter businessOperation = new BusinessOperationParameter();

            if (actionType == ActionType.Edit)
            {

                BusinessOperationContext context = new BusinessOperationContext();
                context.OperationNO = CurrentContext.OperationNO;
                context.OperationID = CurrentContext.OperationID;
                context.OperationType = CurrentContext.OperationType;
                context.FormId = CurrentContext.FormId;
                context.SONO = CurrentContext.SONO;
                businessOperation.Context = context;
            }
            else
            {
                if (isNewOrder)
                    businessOperation.Context = new BusinessOperationContext();
                else
                {
                    BusinessOperationContext context = new BusinessOperationContext();
                    context.OperationNO = CurrentContext.OperationNO;
                    context.OperationID = CurrentContext.OperationID;
                    context.OperationType = CurrentContext.OperationType;
                    context.FormId = CurrentContext.FormId;
                    context.SONO = CurrentContext.SONO;
                    businessOperation.Context = context;
                }
            }
            businessOperation.ActionType = actionType;
            businessOperation.TemplateCode = IicpCommonOperation.TemplateCode;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("businessOperationParameter", businessOperation);
            return dic;
        }

        /// <summary>
        /// MBL电子补料
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="selectedList"></param>
        /// <param name="companyID"></param>
        /// <param name="isSucc"></param>
        public void EMBLBusiness(OperationType operationType, List<OceanBLList> selectedList, Guid companyID, ref bool isSucc)
        {
            switch (operationType)
            {
                case OperationType.OceanExport:
                    IicpCommonOperation.InnerEMBL(selectedList, companyID, AMSEntryType.Unknown, ACIEntryType.Unknown, ref isSucc, CurrentContext);
                    break;
                case OperationType.AirExport:
                    //AirExportEMBL();
                    break;
            }
        }

        /// <summary>
        /// MBL电子补料
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="listData"></param>
        public void EISFBusiness(OperationType operationType, OceanBLList listData)
        {
            switch (operationType)
            {
                case OperationType.OceanExport:
                    OceanExportEISF();
                    break;
                case OperationType.AirExport:
                    //AirExportEISF();
                    break;
            }
        }
        void OceanExportEISF()
        {
            // AddOceanExportBLItem();
            Workitem.Commands["Command_OELISF"].Execute();
        }

        /// <summary>
        /// 发送AMS/AIC/ISF的方法
        /// </summary>
        /// <param name="key"></param>
        /// <param name="subjuect"></param>
        /// <param name="oIds"></param>
        /// <param name="hblIds"></param>
        /// <param name="operationNos"></param>
        /// <param name="isSucc"></param>
        /// <param name="ediMode"></param>
        public void SendEDI(string key, string subjuect, List<Guid> oIds, List<Guid> hblIds, List<string> operationNos, bool isSucc, EDIMode ediMode)
        {

            IicpCommonOperation.SendEdiamsaicisf(key, subjuect, oIds, hblIds, operationNos, isSucc, ediMode, CurrentRow.CompanyID, CurrentContext);
        }

        /// <summary>
        /// 发送至代理
        /// </summary>
        /// <param name="oceanBlList"></param>
        /// <param name="flg"></param>
        public void SendToAgent(OceanBLList oceanBlList, bool flg)
        {
            if (oceanBlList != null)
            {
                string result = IicpCommonOperation.MailALLBLCopyToAgent(flg, oceanBlList.OceanBookingID);
                if (!string.IsNullOrEmpty(result))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), result);
                    return;
                }
            }
        }

        /// <summary>
        /// 发送邮件的方法
        /// </summary>
        /// <param name="oceanBlList"></param>
        /// <param name="flg"></param>
        public void Send(OceanBLList oceanBlList, bool flg)
        {
            if (oceanBlList != null)
            {
                OceanMBLInfo oceanMblInfo = null;
                OceanHBLInfo oceanHbl = null;
                if (oceanBlList.BLType == FCMBLType.MBL)
                {
                    //MBL
                    oceanMblInfo = new OceanMBLInfo
                    {
                        No = oceanBlList.No,
                        ReleaseType = oceanBlList.ReleaseType,
                        ID = oceanBlList.MBLID
                    };
                }
                else
                {
                    //HBL;
                    oceanHbl = new OceanHBLInfo
                    {
                        No = oceanBlList.No,
                        ReleaseType = oceanBlList.ReleaseType,
                        ID = oceanBlList.ID
                    };
                }

                string result = IicpCommonOperation.MailCustomerAskForConfirmSI(flg, oceanBlList.OceanBookingID,
                                                                                  oceanHbl, oceanMblInfo);
                if (!string.IsNullOrEmpty(result))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), result);
                }
            }
        }
        #endregion

        #region 工具栏事件
        /// <summary>
        /// 设置工具栏启用状态
        /// </summary>
        /// <param name="listData"></param>
        void SetToolEnable(OceanBLList listData)
        {
            if (listData == null || listData.IsNew)
            {
                barSplitAndMerge.Enabled = false;
                barSplitBL.Enabled = false;
                barMergeBL.Enabled = false;
            }
            else
            {
                barSplitAndMerge.Enabled = false;
                if (listData.IsValid == false)
                {
                }
                else
                {
                    barSplitBL.Enabled = false;
                    barMergeBL.Enabled = false;
                    if (listData.State != OEBLState.Checked && listData.State != OEBLState.Release)
                    {
                        barMergeBL.Enabled = false;
                    }
                    if (listData.State != OEBLState.Checked && listData.State != OEBLState.Release && listData.HBLCount <= 0)
                    {
                        barSplitBL.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// 设置工具栏
        /// </summary>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_PositionChanged)]
        public void BLCommand_PositionChanged(object sender, EventArgs e)
        {
            if (Workitem.State[BLWorkSpaceConstants.BLCommandState_BLInfo] == null) return;
            OceanBLList listData = (OceanBLList)Workitem.State[BLWorkSpaceConstants.BLCommandState_BLInfo];
            SetToolEnable(listData);
        }

        #region 添加HBL
        [CommandHandler(BLWorkSpaceConstants.BLCommand_AddDeclareHBL)]
        public void BLCommand_AddDeclareHBL(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!string.IsNullOrEmpty(CurrentContext.SONO))
                {
                    using (new CursorHelper(Cursors.WaitCursor))
                    {
                        Dictionary<string, object> dic = CreateBusinessParameter(ActionType.Create, false);
                        IicpCommonOperation.AddDeclareHBL(dic);
                    }
                }

                else
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, LocalData.IsEnglish ? "HBL could not be created due to SONO is not filled." : "当前订舱单没有SONO无法建立HBL.");
                }
            }
        }

        #endregion

        #region 添加MBL
        [CommandHandler(BLWorkSpaceConstants.BLCommand_AddMBL)]
        public void BLCommand_AddMBL(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!string.IsNullOrEmpty(CurrentContext.SONO))
                {
                    using (new CursorHelper(Cursors.WaitCursor))
                    {
                        Dictionary<string, object> dic = CreateBusinessParameter(ActionType.Create, false);
                        IicpCommonOperation.AddMBL(dic);
                    }
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, LocalData.IsEnglish ? "MBL could not be created due to SONO is not filled." : "当前订舱单没有SONO无法建立MBL.");
                }
            }

        }

        #endregion

        #region Copy
        [CommandHandler(BLWorkSpaceConstants.BLCommand_Copy)]
        public void BLCommand_Copy(object sender, EventArgs e)
        {

            if (!IsNullObject())
            {
                Copy(CurrentRow);
            }
        }
        public void Copy(OceanBLList listData)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {

                if (listData == null) return;

                if (listData.BLType == FCMBLType.MBL)
                {
                    IicpCommonOperation.InnerCopyMBLData(listData);
                }
                else
                {
                    IicpCommonOperation.InnerCopyDeclareHBLData(listData);
                }
            }
        }
        #endregion

        #region Edit
        /// <summary>
        /// 
        /// </summary>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_Edit)]
        public void BLCommand_Edit(object sender, EventArgs e)
        {
            if (!IsNullObject())
            {
                Edit();
            }
        }
        void Edit()
        {
            if (CurrentRow.BLType == FCMBLType.MBL)
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Dictionary<string, object> dictionary = CreateBusinessParameter(ActionType.Edit, false);
                    IicpCommonOperation.EditMBL(CurrentRow.RefNo, CurrentRow.No, dictionary);
                    Operationlog("TemplateCode=" + IicpCommonOperation.TemplateCode + "      NO=" + CurrentRow.No + "       BLType=MBL" + "         Bookid=" + CurrentRow.OceanBookingID, "BL列表记录日志");
                }
            }
            else
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Dictionary<string, object> dic = CreateBusinessParameter(ActionType.Edit, false);
                    IicpCommonOperation.EditDeclareHBL(CurrentRow.ID, CurrentRow.No, dic);
                    Operationlog("TemplateCode=" + IicpCommonOperation.TemplateCode + "       NO=" + CurrentRow.No + "        BLType=DeclareHBL" + "         Bookid=" + CurrentRow.OceanBookingID, "BL列表记录日志");
                }
            }
        }
        #endregion

        #region Print
        [CommandHandler(BLWorkSpaceConstants.BLCommand_PrintBL)]
        public void BLCommand_PrintBL(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!IsNullObject())
                {
                    IicpCommonOperation.PrintBillOfLoading(CurrentRow.ID);
                }
            }
        }
        #endregion

        #region Refersh
        [CommandHandler(BLWorkSpaceConstants.BLCommand_Refersh)]
        public void BLCommand_Refersh(object sender, EventArgs e)
        {
            Refersh();
        }

        public void Refersh()
        {
            try
            {
                DataSource = OceanExportService.GetDeclareBLListByIds(CurrentContext.OperationID);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }

        }

        #endregion

        #region Remove
        [CommandHandler(BLWorkSpaceConstants.BLCommand_Remove)]
        public void BLCommand_Remove(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!IsNullObject())
                {
                    try
                    {
                        OceanBLList oceanBl = CurrentRow;
                        if (IicpCommonOperation.InnerDeclareDelete(CurrentRow, DataSource, bsBLList, CurrentContext) == true)
                        {
                            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(),
                               oceanBl.No + " " + (LocalData.IsEnglish ? "Delete Successfully" : "删除成功"));
                            Refersh();
                        }
                    }
                    catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
                }
            }
        }
        #endregion

        #endregion

        #region 记录日志

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="msg">操作轨迹</param>
        /// <param name="name"></param>
        public void Operationlog(string msg, string name)
        {
            StreamWriter sw = File.AppendText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, name) + ".Log");
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss: ") + msg);
            sw.Close();
        }
        #endregion

        #region   列表F5操作
        void gvBLList_KeyDown(object sender, KeyEventArgs e)
        {
            if (
             e.KeyCode == Keys.F5
             && gvBLList.FocusedColumn != null
             && gvBLList.FocusedValue != null)
            {
                F5Query();
            }
        }

        private void ToolStripMenuItemF5_Click(object sender, EventArgs e)
        {
            F5Query();
        }
        /// <summary>
        /// 
        /// </summary>
        public void F5Query()
        {
            string text = gvBLList.GetFocusedDisplayText().Replace("'", "''");
            if (text.ToLower().Contains("checked")) return;
            BusinessOperationContext businessOperation = new BusinessOperationContext();
            businessOperation.OperationNO = text;
            businessOperation.OperationID = Guid.NewGuid();
            ServiceClient.GetService<IICPCommonOperationService>().OpenTaskCenter(businessOperation);
        }
        #endregion

        #region Comment Code
        
        private void PositionChanged(object sender, EventArgs e)
        {
            RefershToolBar();
        }
        #endregion
    }
}
