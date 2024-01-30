using System;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Client;
using System.Windows.Forms;
using ICP.FCM.OceanExport.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Common.UI;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using System.Collections.Generic;
using ICP.EDI.ServiceInterface;
using ICP.EDI.ServiceInterface.DataObjects;
using System.Linq;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FCM.OceanExport.UI
{
    /// <summary>
    /// 子面板
    /// </summary>
    public interface IChildPart
    {
        /// <summary>
        /// 是否改变
        /// </summary>
        bool IsChanged { get; }
        /// <summary>
        /// 数据源
        /// </summary>
        object DataSource { get; }
        /// <summary>
        /// 数据改变处理事件
        /// </summary>
        event EventHandler DataChanged;
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns>是否通过</returns>
        bool ValidateData();
        /// <summary>
        /// 保存后事件
        /// </summary>
        void AfterSaved();
        
        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <param name="value"></param>
        void SetSource(object value);
    }
    /// <summary>
    /// 
    /// </summary>
    public class OECommonUtility
    {
        /// <summary>
        /// 容器服务注入
        /// </summary>
        private static WorkItem rootWorkItem = null;
        /// <summary>
        /// 
        /// </summary>
        public static WorkItem RootWorkItem
        {
            get
            {

                if (rootWorkItem != null)
                {
                    return rootWorkItem;
                }
                else
                {
                    rootWorkItem = ServiceClient.GetClientService<WorkItem>();

                    return rootWorkItem;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static ICP.FCM.Common.ServiceInterface.IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<ICP.FCM.Common.ServiceInterface.IFCMCommonService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }

        ///<summary>
        /// 配置服务接口
        ///</summary>
        public static IConfigureService ConfigureService
        {
            get { return ServiceClient.GetService<IConfigureService>(); }

        }

        private static ICP.FAM.ServiceInterface.IFinanceClientService finClientService = null;
        /// <summary>
        /// 
        /// </summary>
        public static ICP.FAM.ServiceInterface.IFinanceClientService FinClientService
        {
            get
            {
                if (finClientService == null)
                {
                    finClientService = ServiceClient.GetClientService<ICP.FAM.ServiceInterface.IFinanceClientService>();
                    if (finClientService == null)
                    {

                    }
                }

                return finClientService;
            }
        }

        /// <summary>
        /// EDI VGM
        /// </summary>
        /// <param name="ediClientService">EDI服务</param>
        /// <param name="selectedList">所选BL</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="isSucc">是否成功</param>
        public static void InnerEVGM(IEDIClientService ediClientService, OceanBLList selectedList, Guid companyID, ref bool isSucc)
        {
            OceanBLList mbls = selectedList;
            if (mbls == null)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select current BL." : "请选择当前要发送EDI补料的提单.");
                return;
            }

            if (mbls.BLType != FCMBLType.MBL)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select MBL BL." : "请选择MBL提单进行发送.");
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

            mblNoBuilder.Append(mbls.No);
            operationNos.Add(mbls.No);
            mblIds.Add(mbls.MBLID);
            oIds.Add(mbls.OceanBookingID);

            subjuect = "Inttra VGM(" + mblNoBuilder + ")";
            key = "InttraVGM";
            tip = "Inttra VGM";

            EDISendOption sendItem = new EDISendOption();
            sendItem.ServiceKey = key;
            sendItem.EdiMode = EDIMode.VGM;
            sendItem.CompanyID = companyID;
            sendItem.Subject = subjuect;
            sendItem.IDs = oIds.ToArray();
            sendItem.FIDs = mblIds.ToArray();
            sendItem.NOs = operationNos.ToArray();
            sendItem.OperationType = OperationType.OceanExport;
            sendItem.SendByID = LocalData.UserInfo.LoginID;

            if (mblIds.Count > 0)
            {
                isSucc = ediClientService.SendEDI(sendItem);
            }
        }

        /// <summary>
        /// 发送EDI
        /// </summary>
        /// <param name="ediClientService">EDI服务</param>
        /// <param name="selectedList">所选BL列表</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="amsEntryType">AMS 类型</param>
        /// <param name="aciEntryType">ACI 类型</param>
        /// <param name="isSucc">是否成功</param>
        public static void InnerEMBL(IEDIClientService ediClientService, List<OceanBLList> selectedList, Guid companyID, AMSEntryType amsEntryType, ACIEntryType aciEntryType, ref bool isSucc)
        {
            List<OceanBLList> mbls = selectedList;
            if (mbls == null || mbls.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select current BL." : "请选择当前要发送EDI补料的提单.");
                return;
            }

            int i = mbls.FindAll(m => m.BLType == FCMBLType.HBL).Count;
            if (i > 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select MBL BL." : "请选择MBL提单进行发送.");
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
            string[] shipperFormat = new string[selectedList.Count];
            string[] consigneeFormat = new string[selectedList.Count];
            string[] notifyFormat = new string[selectedList.Count];
            string[] shipperName = new string[selectedList.Count];
            string[] consigneeName = new string[selectedList.Count];
            string[] notifyName = new string[selectedList.Count];
            string[] goodinfoFormat = new string[selectedList.Count];
            string[] markFormat = new string[selectedList.Count];
            OceanMBLInfo _mblInfo = null;
            string shipperstr = "";
            string consigneestr = "";
            string nitifystr = "";
            Guid carrier = Guid.Empty;
            Guid agentofcarrierID = Guid.Empty;

            _mblInfo = OceanExportService.GetOceanMBLInfo(selectedList[0].MBLID);
            OceanBookingInfo ob1 = OEService.GetOceanBookingInfo(_mblInfo.OceanBookingID);
            carrier = _mblInfo.CarrierID != null ? _mblInfo.CarrierID.Value : Guid.Empty;
            EDISendOption sendOption = new EDISendOption
            {
                EdiMode = EDIMode.SI,
                CompanyID = companyID,
                CarrierID = carrier,
                AgentOfCarrierID = ob1.AgentOfCarrierID,
                OperationType = OperationType.OceanExport,
                SendByID = LocalData.UserInfo.LoginID,
            };
            EDIConfigItem configOption = ediClientService.GetEDIConfigByOption(sendOption);
            if (configOption != null)
            {
                sendOption.Subject = string.Format("{0}{1}({2})", configOption.SubjectPrefix, EnumHelper.GetDescription(EDIMode.SI, true), mbls[0].No);
                key = configOption.Code;
                foreach (var item in mbls)
                {
                    if (mblNoBuilder.Length > 0)
                        mblNoBuilder.Append(",");

                    mblNoBuilder.Append(item.No);

                    operationNos.Add(item.No);

                    mblIds.Add(item.MBLID);
                    oIds.Add(item.OceanBookingID);
                }
                #region 收发通格式化处理
                string shipName1 = string.Empty;
                string conName1 = string.Empty;
                string noName1 = string.Empty;
                string goodinfo1 = string.Empty;
                string markinfo1 = string.Empty;
                //智能截取字符串 保证单词完整
                for (int z = 0; z < selectedList.Count; z++)
                {
                    _mblInfo = OceanExportService.GetOceanMBLInfo(selectedList[z].MBLID);
                    shipName1 = string.IsNullOrEmpty(_mblInfo.ShipperName) ? _mblInfo.ShipperDescription.Name : _mblInfo.ShipperName;
                    conName1 = string.IsNullOrEmpty(_mblInfo.ConsigneeName) ? _mblInfo.ConsigneeDescription.Name : _mblInfo.ConsigneeName;
                    noName1 = string.IsNullOrEmpty(_mblInfo.NotifyPartyName) ? _mblInfo.NotifyPartyDescription.Name : _mblInfo.NotifyPartyName;

                    //收发通整理格式后传入数据库
                    shipperstr += _mblInfo.ShipperDescription.Address.Replace(Environment.NewLine, " ").Replace(", ", ",");
                    shipperstr += string.IsNullOrEmpty(_mblInfo.ShipperDescription.Country) ? "" : ("," + _mblInfo.ShipperDescription.Country);
                    if (key == "GZPILSI")
                    {
                        shipperstr += Environment.NewLine + (string.IsNullOrEmpty(_mblInfo.ShipperDescription.Tel) ? "" : (" TEL:" + _mblInfo.ShipperDescription.Tel));
                        shipperstr += Environment.NewLine + (string.IsNullOrEmpty(_mblInfo.ShipperDescription.Fax) ? "" : (" FAX:" + _mblInfo.ShipperDescription.Fax));
                    }
                    else
                    {
                        shipperstr += string.IsNullOrEmpty(_mblInfo.ShipperDescription.Tel) ? "" : (" TEL:" + _mblInfo.ShipperDescription.Tel);
                        shipperstr += string.IsNullOrEmpty(_mblInfo.ShipperDescription.Fax) ? "" : (" FAX:" + _mblInfo.ShipperDescription.Fax);
                    }
                    if (key == "GZPILSI")
                    {
                        string orgstr = ediClientService.SplitString(ToDBC(shipperstr), 35, 0);
                        string[] orgarr = orgstr.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (orgarr.Length > 3)
                        {
                            for (int snum = 0; snum < orgarr.Length; snum++)
                            {
                                if (snum < 3)
                                {
                                    if (string.IsNullOrEmpty(shipperFormat[z]))
                                    {
                                        shipperFormat[z] = orgarr[snum];
                                    }
                                    else
                                    {
                                        shipperFormat[z] += '|' + orgarr[snum];
                                    }
                                }
                                else
                                {
                                    if (goodinfo1.Contains("ALSO SHIPPER"))
                                    {
                                        goodinfo1 += Environment.NewLine + orgarr[snum];
                                    }
                                    else
                                    {
                                        goodinfo1 += Environment.NewLine + " ALSO SHIPPER " + Environment.NewLine + orgarr[snum];
                                    }
                                }
                            }
                            shipperName[z] = ediClientService.SplitString(ToDBC(shipName1), 35, 0).Replace(Environment.NewLine, "|");
                        }
                        else
                        {
                            shipperFormat[z] = ediClientService.SplitString(ToDBC(shipperstr), 35, 0).Replace(Environment.NewLine, "|");
                            shipperName[z] = ediClientService.SplitString(ToDBC(shipName1), 35, 0).Replace(Environment.NewLine, "|");
                        }
                    }
                    else
                    {
                        shipperFormat[z] = ediClientService.SplitString(ToDBC(shipperstr), 32, 0).Replace(Environment.NewLine, "|");
                        shipperName[z] = ediClientService.SplitString(ToDBC(shipName1), 32, 0).Replace(Environment.NewLine, "|");
                    }


                    consigneestr += _mblInfo.ConsigneeDescription.Address.Replace(Environment.NewLine, " ").Replace(", ", ",");
                    consigneestr += string.IsNullOrEmpty(_mblInfo.ConsigneeDescription.Country) ? "" : ("," + _mblInfo.ConsigneeDescription.Country);
                    consigneestr += string.IsNullOrEmpty(_mblInfo.ConsigneeDescription.Tel) ? "" : (" TEL:" + _mblInfo.ConsigneeDescription.Tel);
                    consigneestr += string.IsNullOrEmpty(_mblInfo.ConsigneeDescription.Fax) ? "" : (" FAX:" + _mblInfo.ConsigneeDescription.Fax);

                    if (key == "GZPILSI")
                    {
                        string orgstr = ediClientService.SplitString(ToDBC(consigneestr), 35, 0);
                        string[] orgarr = orgstr.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (orgarr.Length > 3)
                        {
                            for (int snum = 0; snum < orgarr.Length; snum++)
                            {
                                if (snum < 3)
                                {
                                    if (string.IsNullOrEmpty(consigneeFormat[z]))
                                    {
                                        consigneeFormat[z] = orgarr[snum];
                                    }
                                    else
                                    {
                                        consigneeFormat[z] += '|' + orgarr[snum];
                                    }
                                }
                                else
                                {
                                    if (goodinfo1.Contains("ALSO CNEE"))
                                    {
                                        goodinfo1 += Environment.NewLine + orgarr[snum];
                                    }
                                    else
                                    {
                                        goodinfo1 += Environment.NewLine + " ALSO CNEE " + Environment.NewLine + orgarr[snum];
                                    }
                                }
                            }
                            consigneeName[z] = ediClientService.SplitString(ToDBC(conName1), 35, 0).Replace(Environment.NewLine, "|");
                        }
                        else
                        {
                            consigneeFormat[z] = ediClientService.SplitString(ToDBC(consigneestr), 35, 0).Replace(Environment.NewLine, "|");
                            consigneeName[z] = ediClientService.SplitString(ToDBC(conName1), 35, 0).Replace(Environment.NewLine, "|");
                        }
                    }
                    else
                    {
                        consigneeFormat[z] = ediClientService.SplitString(ToDBC(consigneestr), 32, 0).Replace(Environment.NewLine, "|");
                        consigneeName[z] = ediClientService.SplitString(ToDBC(conName1), 32, 0).Replace(Environment.NewLine, "|");
                    }

                    if (_mblInfo.NotifyPartyID == null || _mblInfo.NotifyPartyName == "SAME AS CONSIGNEE" || string.IsNullOrEmpty(_mblInfo.NotifyPartyName))
                    {
                        notifyFormat[z] = "SAME AS CONSIGNEE";
                        notifyName[z] = "SAME AS CONSIGNEE";
                    }
                    else
                    {
                        nitifystr += _mblInfo.NotifyPartyDescription.Address.Replace(Environment.NewLine, " ").Replace(", ", ",");
                        nitifystr += string.IsNullOrEmpty(_mblInfo.NotifyPartyDescription.Country) ? "" : ("," + _mblInfo.NotifyPartyDescription.Country);
                        nitifystr += string.IsNullOrEmpty(_mblInfo.NotifyPartyDescription.Tel) ? "" : (" TEL:" + _mblInfo.NotifyPartyDescription.Tel);
                        nitifystr += string.IsNullOrEmpty(_mblInfo.NotifyPartyDescription.Fax) ? "" : (" FAX:" + _mblInfo.NotifyPartyDescription.Fax);

                        if (key == "GZPILSI")
                        {
                            string orgstr = ediClientService.SplitString(ToDBC(nitifystr), 35, 0);
                            string[] orgarr = orgstr.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            if (orgarr.Length > 3)
                            {
                                for (int snum = 0; snum < orgarr.Length; snum++)
                                {
                                    if (snum < 3)
                                    {
                                        if (string.IsNullOrEmpty(notifyFormat[z]))
                                        {
                                            notifyFormat[z] = orgarr[snum];
                                        }
                                        else
                                        {
                                            notifyFormat[z] += '|' + orgarr[snum];
                                        }
                                    }
                                    else
                                    {
                                        if (goodinfo1.Contains("ALSO NOTIFY"))
                                        {
                                            goodinfo1 += Environment.NewLine + orgarr[snum];
                                        }
                                        else
                                        {
                                            goodinfo1 += Environment.NewLine + " ALSO NOTIFY: " + Environment.NewLine + orgarr[snum];
                                        }
                                    }
                                }
                                notifyName[z] = ediClientService.SplitString(ToDBC(noName1), 35, 0).Replace(Environment.NewLine, "|");
                            }
                            else
                            {
                                notifyFormat[z] = ediClientService.SplitString(ToDBC(nitifystr), 35, 0).Replace(Environment.NewLine, "|");
                                notifyName[z] = ediClientService.SplitString(ToDBC(noName1), 35, 0).Replace(Environment.NewLine, "|");
                            }
                        }
                        else
                        {
                            notifyFormat[z] = ediClientService.SplitString(ToDBC(nitifystr), 32, 0).Replace(Environment.NewLine, "|");
                            notifyName[z] = ediClientService.SplitString(ToDBC(noName1), 32, 0).Replace(Environment.NewLine, "|");
                        }
                    }

                    markinfo1 = ediClientService.SplitString(ToDBC(_mblInfo.Marks), 20, 0);
                    string[] markarr = markinfo1.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for (int snum = 0; snum < markarr.Length; snum++)
                    {
                        markarr[snum] = markarr[snum].PadRight(20);
                    }
                    markinfo1 = string.Join("", markarr);
                    markFormat[z] = markinfo1;

                    goodinfo1 = ediClientService.SplitString(ToDBC(_mblInfo.GoodsDescription), 30, 0) + Environment.NewLine + ediClientService.SplitString(ToDBC(_mblInfo.WoodPacking), 30, 0) + Environment.NewLine + goodinfo1;
                    string[] goodinfoarr = goodinfo1.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for (int snum = 0; snum < goodinfoarr.Length; snum++)
                    {
                        goodinfoarr[snum] = goodinfoarr[snum].PadRight(30);
                    }
                    goodinfo1 = string.Join("", goodinfoarr);
                    goodinfoFormat[z] = goodinfo1;
                }
                #endregion
                sendOption.IDs = oIds.ToArray();
                sendOption.NOs = operationNos.ToArray();
                sendOption.FIDs = mblIds.ToArray();

                sendOption.OperationType = OperationType.OceanExport;
                sendOption.AMSEntryType = amsEntryType;
                sendOption.ACIEntryType = aciEntryType;
                sendOption.SendByID = LocalData.UserInfo.LoginID;
                sendOption.ShipperFormat = shipperFormat;
                sendOption.ShipperName = shipperName;
                sendOption.ConsigneeFormat = consigneeFormat;
                sendOption.ConsigneeName = consigneeName;
                sendOption.NotifyFormat = notifyFormat;
                sendOption.NotifyName = notifyName;
                sendOption.GoodinfoFormat = goodinfoFormat;
                sendOption.MarkFormat = markFormat;
                ediClientService.ShowForm(sendOption, false);
                return;
            }

            //韩进
            OceanBLList hjMBL = null;
            //中海
            OceanBLList zhMBL = null;
            //泛洋
            OceanBLList fyMBL = null;
            //中远
            OceanBLList zyMBL = null;
            //澳航
            OceanBLList aoMBL = null;
            //CMA
            OceanBLList CMAMBL = null;
            //APL
            OceanBLList APLMBL = null;
            //USAC
            OceanBLList UASCMBL = null;
            //HPL
            OceanBLList HPLMBL = null;
            //SMLINE
            OceanBLList SMLINE = null;
            //PILLINE
            OceanBLList PILLINE = null;

            //非宁波EDI需验证船东是否[韩进]、[中海]、[泛洋]、[中远]
            if (LocalData.UserInfo.DefaultCompanyID.ToString().ToUpper() != "A62A9F8E-E69C-4E6E-AD85-E75AED3C6CF9")
            {
                //韩进
                hjMBL =
                    mbls.Find(
                        item => item.CarrierID == new Guid("0F40E9A1-B388-44CC-B27F-7A9AEC6F6D58"));
                //中海
                zhMBL =
                    mbls.Find(
                        item => item.CarrierID == new Guid("69B85E12-6208-432C-8D8E-D2E345239047"));
                //泛洋
                fyMBL =
                    mbls.Find(
                        item => item.CarrierID == new Guid("4968597D-7BFC-405C-AF9B-C521AB082C0B"));
                //中远
                zyMBL =
                    mbls.Find(
                        item => item.CarrierID == new Guid("ABBEBCEA-11AF-41C0-AEB0-61F1C9AD0E4F"));
                //CMA
                CMAMBL =
                    mbls.Find(
                        item => item.CarrierID == new Guid("979B3DA5-2FE2-4895-8F43-DE5610D20599"));
                //UASC
                UASCMBL =
                  mbls.Find(
                      item => item.CarrierID == new Guid("E2A5B70E-9D7A-47B2-9902-082D8A317548"));
                //APL
                APLMBL =
                  mbls.Find(
                      item => item.CarrierID == new Guid("FDCA28E3-7673-4803-B3C2-71E7E66B7650"));
                //HPL
                HPLMBL =
                  mbls.Find(
                      item => item.CarrierID == new Guid("68797EA6-F0BB-4035-947B-84A731E21245"));
                //SMLINE
                SMLINE =
                mbls.Find(
                    item => item.CarrierID == new Guid("5932EBBB-110A-E711-80BD-141877442141"));
                //PILLINE
                PILLINE =
                mbls.Find(
                    item => item.CarrierID == new Guid("71281FF9-9D18-49C9-A784-67F799EBD369"));


                if (hjMBL == null && zhMBL == null && fyMBL == null && zyMBL == null && aoMBL == null && CMAMBL == null && UASCMBL == null && APLMBL == null && HPLMBL == null && SMLINE == null && PILLINE == null)
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish
                        ? "Shipowners are now only supports China Shipping、COSCO、Hanjin、Hainan Pan Ocean Shipping Electronic batch."
                        : "现在只支持船东是 [SMLINE]、[中海]、[中远]、[海南泛洋]的电子补料。");
                    return;
                }



                #region 韩进

                if (hjMBL != null)
                {
                    List<OceanBLList> hjMBLs =
                        mbls.FindAll(
                            item => item.CarrierID == new Guid("0F40E9A1-B388-44CC-B27F-7A9AEC6F6D58"));
                    carrier = new Guid("0F40E9A1-B388-44CC-B27F-7A9AEC6F6D58");
                    foreach (var item in hjMBLs)
                    {
                        if (mblNoBuilder.Length > 0)
                            mblNoBuilder.Append(",");

                        mblNoBuilder.Append(item.SONO);

                        operationNos.Add(item.No);

                        mblIds.Add(item.MBLID);
                        oIds.Add(item.OceanBookingID);
                    }
                    subjuect = LocalData.IsEnglish
                        ? "HANJIN SHIPPING(" + mblNoBuilder + ")"
                        : "韩进电子补料(" + mblNoBuilder + ")";
                    key = "HANJIN_SI";
                    tip = LocalData.IsEnglish ? "HANJIN SHIPPING" : "韩进";
                }
                #endregion

                #region 泛洋

                else if (fyMBL != null)
                {
                    carrier = new Guid("4968597D-7BFC-405C-AF9B-C521AB082C0B");
                    List<OceanBLList> fyMBLs =
                        mbls.FindAll(
                            delegate (OceanBLList item)
                            {
                                return item.CarrierID == new Guid("4968597D-7BFC-405C-AF9B-C521AB082C0B");
                            });
                    foreach (var item in fyMBLs)
                    {
                        if (mblNoBuilder.Length > 0)
                            mblNoBuilder.Append(",");

                        mblNoBuilder.Append(item.SONO);

                        operationNos.Add(item.No);
                        mblIds.Add(item.MBLID);
                        oIds.Add(item.OceanBookingID);
                    }
                    subjuect = LocalData.IsEnglish
                        ? "HAINAN PAN SHIPPING(" + mblNoBuilder + ")"
                        : "海南泛洋电子补料(" + mblNoBuilder + ")";
                    key = "FYCW_SI";
                    tip = LocalData.IsEnglish ? "HAINAN PAN SHIPPING" : "海南泛洋";
                }
                #endregion

                #region 中远

                else if (zyMBL != null)
                {
                    carrier = new Guid("ABBEBCEA-11AF-41C0-AEB0-61F1C9AD0E4F");
                    List<OceanBLList> zyMBLs =
                        mbls.FindAll(
                            delegate (OceanBLList item)
                            {
                                return item.CarrierID == new Guid("ABBEBCEA-11AF-41C0-AEB0-61F1C9AD0E4F");
                            });
                    foreach (var item in zyMBLs)
                    {
                        #region EDI检查
                        OceanMBLInfo mbl = OceanExportService.GetOceanMBLInfo(item.ID);
                        if (!string.IsNullOrEmpty(mbl.FreightDescription))
                        {
                            string[] strArr = mbl.FreightDescription.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            if (strArr != null && strArr.Length > 0)
                            {
                                foreach (string str in strArr)
                                {
                                    if (str.Length >= 30)
                                    {
                                        XtraMessageBox.Show("备注中有超过30个字符的数据,发送Cosco补料EDI可能出错\n请分为两行记录：" + str, "Tips", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        break;
                                    }
                                }
                            }
                        }
                        #endregion

                        if (mblNoBuilder.Length > 0)
                            mblNoBuilder.Append(",");

                        mblNoBuilder.Append(item.SONO);

                        operationNos.Add(item.No);

                        mblIds.Add(item.MBLID);
                        oIds.Add(item.OceanBookingID);
                    }
                    subjuect = LocalData.IsEnglish
                        ? "COSCO SHIPPING(" + mblNoBuilder + ")"
                        : "中远电子补料(" + mblNoBuilder + ")";
                    key = "COSCO_SI";
                    tip = LocalData.IsEnglish ? "COSCO SHIPPING" : "中远";
                }
                #endregion

                #region 中海

                else if (zhMBL != null)
                {
                    carrier = new Guid("69B85E12-6208-432C-8D8E-D2E345239047");
                    //判断操作员的地理位置（华北/华南）
                    List<ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo> olist =
                        LocalData.UserInfo.UserOrganizationList;
                    ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo curro =
                        olist.Find(
                            delegate (ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo o)
                            {
                                return o.ID == LocalData.UserInfo.DefaultCompanyID;
                            });
                    ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo currRegion =
                        olist.Find(
                            delegate (ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo o)
                            {
                                return o.ID == curro.ParentID;
                            });
                    if (currRegion.Code == "HDQ" || currRegion.Code == "HBQ")
                    {
                        key = "CSCL_SI_NorthChina";
                    }
                    else
                    {
                        key = "CSCL_SI";
                    }
                    List<OceanBLList> zjMBLs =
                        mbls.FindAll(
                            delegate (OceanBLList item)
                            {
                                return item.CarrierID == new Guid("69B85E12-6208-432C-8D8E-D2E345239047");
                            });
                    foreach (var item in zjMBLs)
                    {
                        if (mblNoBuilder.Length > 0)
                            mblNoBuilder.Append(",");

                        mblNoBuilder.Append(item.SONO);

                        operationNos.Add(item.No);
                        mblIds.Add(item.MBLID);
                        oIds.Add(item.OceanBookingID);
                    }
                    subjuect = LocalData.IsEnglish
                        ? "CHINA SHIPPING(" + mblNoBuilder + ")"
                        : "中海电子补料(" + mblNoBuilder + ")";

                    tip = LocalData.IsEnglish ? "CHINA SHIPPING" : "中海";
                }

                #endregion

                #region CMAMBL
                if (CMAMBL != null)
                {
                    carrier = new Guid("979B3DA5-2FE2-4895-8F43-DE5610D20599");
                    List<OceanBLList> CMAMBLs =
                        mbls.FindAll(
                            delegate (OceanBLList item)
                            {
                                return item.CarrierID == new Guid("979B3DA5-2FE2-4895-8F43-DE5610D20599");
                            });
                    foreach (var item in CMAMBLs)
                    {
                        if (mblNoBuilder.Length > 0)
                            mblNoBuilder.Append(",");

                        mblNoBuilder.Append(item.SONO);

                        operationNos.Add(item.No);

                        mblIds.Add(item.MBLID);
                        oIds.Add(item.OceanBookingID);
                    }
                    subjuect = LocalData.IsEnglish
                        ? "INTTRA SHIPPING(" + mblNoBuilder + ")"
                        : "INTTRA 电子补料(" + mblNoBuilder + ")";
                    key = "InttraSi";
                    tip = LocalData.IsEnglish ? "InttraSi" : "Inttra补料";
                }
                #endregion

                #region UASCMBL
                if (UASCMBL != null)
                {
                    carrier = new Guid("E2A5B70E-9D7A-47B2-9902-082D8A317548");
                    List<OceanBLList> UASCMBLs =
                        mbls.FindAll(
                            delegate (OceanBLList item)
                            {
                                return item.CarrierID == new Guid("E2A5B70E-9D7A-47B2-9902-082D8A317548");
                            });
                    foreach (var item in UASCMBLs)
                    {
                        if (mblNoBuilder.Length > 0)
                            mblNoBuilder.Append(",");

                        mblNoBuilder.Append(item.SONO);

                        operationNos.Add(item.No);

                        mblIds.Add(item.MBLID);
                        oIds.Add(item.OceanBookingID);
                    }
                    subjuect = LocalData.IsEnglish
                        ? "INTTRA SHIPPING(" + mblNoBuilder + ")"
                        : "INTTRA 电子补料(" + mblNoBuilder + ")";
                    key = "InttraSi";
                    tip = LocalData.IsEnglish ? "InttraSi" : "Inttra补料";
                }
                #endregion

                #region APLMBL
                if (APLMBL != null)
                {
                    carrier = new Guid("FDCA28E3-7673-4803-B3C2-71E7E66B7650");
                    List<OceanBLList> APLMBLs =
                        mbls.FindAll(
                            delegate (OceanBLList item)
                            {
                                return item.CarrierID == new Guid("FDCA28E3-7673-4803-B3C2-71E7E66B7650");
                            });
                    foreach (var item in APLMBLs)
                    {
                        if (mblNoBuilder.Length > 0)
                            mblNoBuilder.Append(",");

                        mblNoBuilder.Append(item.SONO);

                        operationNos.Add(item.No);

                        mblIds.Add(item.MBLID);
                        oIds.Add(item.OceanBookingID);
                    }
                    subjuect = LocalData.IsEnglish
                        ? "INTTRA SHIPPING(" + mblNoBuilder + ")"
                        : "INTTRA 电子补料(" + mblNoBuilder + ")";
                    key = "InttraSi";
                    tip = LocalData.IsEnglish ? "InttraSi" : "Inttra补料";
                }
                #endregion

                #region HPLMBL
                if (HPLMBL != null)
                {
                    carrier = new Guid("68797EA6-F0BB-4035-947B-84A731E21245");
                    List<OceanBLList> HPLMBLs =
                        mbls.FindAll(
                            item => item.CarrierID == new Guid("68797EA6-F0BB-4035-947B-84A731E21245"));
                    foreach (var item in HPLMBLs)
                    {
                        if (mblNoBuilder.Length > 0)
                            mblNoBuilder.Append(",");

                        mblNoBuilder.Append(item.SONO);

                        operationNos.Add(item.No);

                        mblIds.Add(item.MBLID);
                        oIds.Add(item.OceanBookingID);
                    }
                    subjuect = LocalData.IsEnglish
                        ? "INTTRA SHIPPING(" + mblNoBuilder + ")"
                        : "INTTRA 电子补料(" + mblNoBuilder + ")";
                    key = "InttraSi";
                    tip = LocalData.IsEnglish ? "InttraSi" : "Inttra补料";
                }
                #endregion

                #region SMLINE

                else if (SMLINE != null)
                {

                    List<OceanBLList> hjMBLs =
                         mbls.FindAll(
                             delegate (OceanBLList item)
                             {
                                 return item.CarrierID == new Guid("5932EBBB-110A-E711-80BD-141877442141");
                             });
                    carrier = new Guid("5932EBBB-110A-E711-80BD-141877442141");
                    foreach (var item in hjMBLs)
                    {
                        if (mblNoBuilder.Length > 0)
                            mblNoBuilder.Append(",");

                        mblNoBuilder.Append(item.SONO);

                        operationNos.Add(item.No);

                        mblIds.Add(item.MBLID);
                        oIds.Add(item.OceanBookingID);
                    }
                    subjuect = LocalData.IsEnglish
                        ? "SMLINE SHIPPING(" + mblNoBuilder + ")"
                        : "SMLINE电子补料(" + mblNoBuilder + ")";
                    key = "SMLINE_SI";
                    tip = "SMLINE";
                }

                #endregion

                #region PILLINE

                else if (PILLINE != null)
                {
                    carrier = new Guid("71281FF9-9D18-49C9-A784-67F799EBD369");
                    List<OceanBLList> pilMBLs =
                         mbls.FindAll(
                             delegate (OceanBLList item)
                             {
                                 return item.CarrierID == carrier;
                             });

                    foreach (var item in pilMBLs)
                    {
                        if (mblNoBuilder.Length > 0)
                            mblNoBuilder.Append(",");

                        mblNoBuilder.Append(item.SONO);

                        operationNos.Add(item.No);

                        mblIds.Add(item.MBLID);
                        oIds.Add(item.OceanBookingID);
                    }
                    subjuect = "PIL SHIPPING(" + mblNoBuilder + ")";
                    key = "pil_si";
                    tip = "PIL 补料";
                }

                #endregion

                //Inttra SI时，青岛公司需验证船东或承运人是否存在配置：存在则通过配置项发送补料
                if (key.ToUpper().Contains("INTTRA") && LocalData.UserInfo.DefaultCompanyID.ToString().ToUpper() == "F289109A-C29E-4B0B-A41A-C22D9E70A72F")
                {
                    //船代配置
                    List<EDIConfigureList> ediList = ConfigureService.GetEDIConfigureList(null, null, true, 0).Where(fItem => fItem.EDIMode == EDIMode.SI && fItem.ReceiverType == 2).ToList();
                    //船东
                    List<EDIConfigureList> findEdiList = new List<EDIConfigureList>();
                    OceanBookingInfo bookinfo = OceanExportService.GetOceanBookingInfo(mbls[0].OceanBookingID);
                    if (bookinfo.CarrierID != null && bookinfo.CarrierID != Guid.Empty)
                    {
                        findEdiList = (from d in ediList where d.CarrierID == bookinfo.CarrierID select d).ToList();
                    }
                    if (findEdiList.Count <= 0)
                    {
                        findEdiList = (from d in ediList where d.CarrierID == bookinfo.AgentOfCarrierID select d).ToList();
                    }
                    if (findEdiList.Count > 0)
                    {
                        key = findEdiList[0].Code;
                        carrier = bookinfo.AgentOfCarrierID;
                        tip = LocalData.IsEnglish ? "WorldexSI" : "Worldex补料";
                        subjuect = "Worldex SHIPPING(" + mblNoBuilder + ")";
                    }
                }
            }
            else
            {
                #region 组织数据

                foreach (var item in mbls)
                {
                    if (mblNoBuilder.Length > 0)
                        mblNoBuilder.Append(",");

                    mblNoBuilder.Append(item.No);

                    operationNos.Add(item.No);

                    mblIds.Add(item.MBLID);
                    oIds.Add(item.OceanBookingID);
                }

                subjuect = LocalData.IsEnglish ? "NingBoEDI(" + mblNoBuilder + ")" : "宁波EDI中心(" + mblNoBuilder + ")";
                key = "NBEDICenter";
                tip = LocalData.IsEnglish ? "NBEDI" : "宁波EDI中心";

                #endregion
            }

            #region 收发通格式化处理
            string shipName = string.Empty;
            string conName = string.Empty;
            string noName = string.Empty;
            string goodinfo = string.Empty;
            string markinfo = string.Empty;
            //智能截取字符串 保证单词完整
            for (int z = 0; z < selectedList.Count; z++)
            {
                _mblInfo = OceanExportService.GetOceanMBLInfo(selectedList[z].MBLID);
                shipName = string.IsNullOrEmpty(_mblInfo.ShipperName) ? _mblInfo.ShipperDescription.Name : _mblInfo.ShipperName;
                conName = string.IsNullOrEmpty(_mblInfo.ConsigneeName) ? _mblInfo.ConsigneeDescription.Name : _mblInfo.ConsigneeName;
                noName = string.IsNullOrEmpty(_mblInfo.NotifyPartyName) ? _mblInfo.NotifyPartyDescription.Name : _mblInfo.NotifyPartyName;

                //收发通整理格式后传入数据库
                shipperstr += _mblInfo.ShipperDescription.Address.Replace(Environment.NewLine, " ").Replace(", ", ",");
                shipperstr += string.IsNullOrEmpty(_mblInfo.ShipperDescription.Country) ? "" : ("," + _mblInfo.ShipperDescription.Country);
                if (key == "pil_si")
                {
                    shipperstr += Environment.NewLine + (string.IsNullOrEmpty(_mblInfo.ShipperDescription.Tel) ? "" : (" TEL:" + _mblInfo.ShipperDescription.Tel));
                    shipperstr += Environment.NewLine + (string.IsNullOrEmpty(_mblInfo.ShipperDescription.Fax) ? "" : (" FAX:" + _mblInfo.ShipperDescription.Fax));
                }
                else
                {
                    shipperstr += string.IsNullOrEmpty(_mblInfo.ShipperDescription.Tel) ? "" : (" TEL:" + _mblInfo.ShipperDescription.Tel);
                    shipperstr += string.IsNullOrEmpty(_mblInfo.ShipperDescription.Fax) ? "" : (" FAX:" + _mblInfo.ShipperDescription.Fax);
                }
                if (key == "pil_si")
                {
                    string orgstr = ediClientService.SplitString(ToDBC(shipperstr), 35, 0);
                    string[] orgarr = orgstr.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (orgarr.Length > 3)
                    {
                        for (int snum = 0; snum < orgarr.Length; snum++)
                        {
                            if (snum < 3)
                            {
                                if (string.IsNullOrEmpty(shipperFormat[z]))
                                {
                                    shipperFormat[z] = orgarr[snum];
                                }
                                else
                                {
                                    shipperFormat[z] += '|' + orgarr[snum];
                                }
                            }
                            else
                            {
                                if (goodinfo.Contains("ALSO SHIPPER"))
                                {
                                    goodinfo += Environment.NewLine + orgarr[snum];
                                }
                                else
                                {
                                    goodinfo += Environment.NewLine + " ALSO SHIPPER " + Environment.NewLine + orgarr[snum];
                                }
                            }
                        }
                        shipperName[z] = ediClientService.SplitString(ToDBC(shipName), 35, 0).Replace(Environment.NewLine, "|");
                    }
                    else
                    {
                        shipperFormat[z] = ediClientService.SplitString(ToDBC(shipperstr), 35, 0).Replace(Environment.NewLine, "|");
                        shipperName[z] = ediClientService.SplitString(ToDBC(shipName), 35, 0).Replace(Environment.NewLine, "|");
                    }
                }
                else
                {
                    shipperFormat[z] = ediClientService.SplitString(ToDBC(shipperstr), 32, 0).Replace(Environment.NewLine, "|");
                    shipperName[z] = ediClientService.SplitString(ToDBC(shipName), 32, 0).Replace(Environment.NewLine, "|");
                }


                consigneestr += _mblInfo.ConsigneeDescription.Address.Replace(Environment.NewLine, " ").Replace(", ", ",");
                consigneestr += string.IsNullOrEmpty(_mblInfo.ConsigneeDescription.Country) ? "" : ("," + _mblInfo.ConsigneeDescription.Country);
                consigneestr += string.IsNullOrEmpty(_mblInfo.ConsigneeDescription.Tel) ? "" : (" TEL:" + _mblInfo.ConsigneeDescription.Tel);
                consigneestr += string.IsNullOrEmpty(_mblInfo.ConsigneeDescription.Fax) ? "" : (" FAX:" + _mblInfo.ConsigneeDescription.Fax);

                if (key == "pil_si")
                {
                    string orgstr = ediClientService.SplitString(ToDBC(consigneestr), 35, 0);
                    string[] orgarr = orgstr.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (orgarr.Length > 3)
                    {
                        for (int snum = 0; snum < orgarr.Length; snum++)
                        {
                            if (snum < 3)
                            {
                                if (string.IsNullOrEmpty(consigneeFormat[z]))
                                {
                                    consigneeFormat[z] = orgarr[snum];
                                }
                                else
                                {
                                    consigneeFormat[z] += '|' + orgarr[snum];
                                }
                            }
                            else
                            {
                                if (goodinfo.Contains("ALSO CNEE"))
                                {
                                    goodinfo += Environment.NewLine + orgarr[snum];
                                }
                                else
                                {
                                    goodinfo += Environment.NewLine + " ALSO CNEE " + Environment.NewLine + orgarr[snum];
                                }
                            }
                        }
                        consigneeName[z] = ediClientService.SplitString(ToDBC(conName), 35, 0).Replace(Environment.NewLine, "|");
                    }
                    else
                    {
                        consigneeFormat[z] = ediClientService.SplitString(ToDBC(consigneestr), 35, 0).Replace(Environment.NewLine, "|");
                        consigneeName[z] = ediClientService.SplitString(ToDBC(conName), 35, 0).Replace(Environment.NewLine, "|");
                    }
                }
                else
                {
                    consigneeFormat[z] = ediClientService.SplitString(ToDBC(consigneestr), 32, 0).Replace(Environment.NewLine, "|");
                    consigneeName[z] = ediClientService.SplitString(ToDBC(conName), 32, 0).Replace(Environment.NewLine, "|");
                }

                if (_mblInfo.NotifyPartyID == null || _mblInfo.NotifyPartyName == "SAME AS CONSIGNEE" || string.IsNullOrEmpty(_mblInfo.NotifyPartyName))
                {
                    notifyFormat[z] = "SAME AS CONSIGNEE";
                    notifyName[z] = "SAME AS CONSIGNEE";
                }
                else
                {
                    nitifystr += _mblInfo.NotifyPartyDescription.Address.Replace(Environment.NewLine, " ").Replace(", ", ",");
                    nitifystr += string.IsNullOrEmpty(_mblInfo.NotifyPartyDescription.Country) ? "" : ("," + _mblInfo.NotifyPartyDescription.Country);
                    nitifystr += string.IsNullOrEmpty(_mblInfo.NotifyPartyDescription.Tel) ? "" : (" TEL:" + _mblInfo.NotifyPartyDescription.Tel);
                    nitifystr += string.IsNullOrEmpty(_mblInfo.NotifyPartyDescription.Fax) ? "" : (" FAX:" + _mblInfo.NotifyPartyDescription.Fax);

                    if (key == "pil_si")
                    {
                        string orgstr = ediClientService.SplitString(ToDBC(nitifystr), 35, 0);
                        string[] orgarr = orgstr.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (orgarr.Length > 3)
                        {
                            for (int snum = 0; snum < orgarr.Length; snum++)
                            {
                                if (snum < 3)
                                {
                                    if (string.IsNullOrEmpty(notifyFormat[z]))
                                    {
                                        notifyFormat[z] = orgarr[snum];
                                    }
                                    else
                                    {
                                        notifyFormat[z] += '|' + orgarr[snum];
                                    }
                                }
                                else
                                {
                                    if (goodinfo.Contains("ALSO NOTIFY"))
                                    {
                                        goodinfo += Environment.NewLine + orgarr[snum];
                                    }
                                    else
                                    {
                                        goodinfo += Environment.NewLine + " ALSO NOTIFY: " + Environment.NewLine + orgarr[snum];
                                    }
                                }
                            }
                            notifyName[z] = ediClientService.SplitString(ToDBC(noName), 35, 0).Replace(Environment.NewLine, "|");
                        }
                        else
                        {
                            notifyFormat[z] = ediClientService.SplitString(ToDBC(nitifystr), 35, 0).Replace(Environment.NewLine, "|");
                            notifyName[z] = ediClientService.SplitString(ToDBC(noName), 35, 0).Replace(Environment.NewLine, "|");
                        }
                    }
                    else
                    {
                        notifyFormat[z] = ediClientService.SplitString(ToDBC(nitifystr), 32, 0).Replace(Environment.NewLine, "|");
                        notifyName[z] = ediClientService.SplitString(ToDBC(noName), 32, 0).Replace(Environment.NewLine, "|");
                    }
                }

                markinfo = ediClientService.SplitString(ToDBC(_mblInfo.Marks), 20, 0);
                string[] markarr = markinfo.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int snum = 0; snum < markarr.Length; snum++)
                {
                    markarr[snum] = markarr[snum].PadRight(20);
                }
                markinfo = string.Join("", markarr);
                markFormat[z] = markinfo;

                goodinfo = ediClientService.SplitString(ToDBC(_mblInfo.GoodsDescription), 30, 0) + Environment.NewLine + ediClientService.SplitString(ToDBC(_mblInfo.WoodPacking), 30, 0) + Environment.NewLine + goodinfo;
                string[] goodinfoarr = goodinfo.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int snum = 0; snum < goodinfoarr.Length; snum++)
                {
                    goodinfoarr[snum] = goodinfoarr[snum].PadRight(30);
                }
                goodinfo = string.Join("", goodinfoarr);
                goodinfoFormat[z] = goodinfo;
            }
            #endregion

            EDISendOption sendItem = new EDISendOption();
            sendItem.ServiceKey = key;
            sendItem.EdiMode = EDIMode.SI;
            sendItem.CompanyID = companyID;
            sendItem.CarrierID = carrier;
            sendItem.Subject = subjuect;
            sendItem.IDs = oIds.ToArray();
            sendItem.NOs = operationNos.ToArray();
            sendItem.FIDs = mblIds.ToArray();

            sendItem.OperationType = OperationType.OceanExport;
            sendItem.AMSEntryType = amsEntryType;
            sendItem.ACIEntryType = aciEntryType;
            sendItem.SendByID = LocalData.UserInfo.LoginID;
            sendItem.ShipperFormat = shipperFormat;
            sendItem.ShipperName = shipperName;
            sendItem.ConsigneeFormat = consigneeFormat;
            sendItem.ConsigneeName = consigneeName;
            sendItem.NotifyFormat = notifyFormat;
            sendItem.NotifyName = notifyName;
            sendItem.GoodinfoFormat = goodinfoFormat;
            sendItem.MarkFormat = markFormat;

            if (mblIds.Count > 0)
            {
                isSucc = ediClientService.SendEDI(sendItem);
            }
        }

        /// <summary>
        /// 倒角转半角
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToDBC(string input)
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

        /// <summary>
        /// 发送EDIANL
        /// </summary>
        /// <param name="ediClientService">EDI服务</param>
        /// <param name="selectedList">所选BL列表</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="amsEntryType">AMS 类型</param>
        /// <param name="aciEntryType">ACI 类型</param>
        /// <param name="isSucc">是否成功</param>
        public static void InnerEMBLANL(IEDIClientService ediClientService, List<OceanBLList> selectedList, Guid companyID, ref bool isSucc)
        {
            List<OceanBLList> mbls = selectedList;
            if (mbls == null || mbls.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select current BL." : "请选择当前要发送EDI补料的提单.");
                return;
            }

            int i = mbls.FindAll(m => m.BLType == FCMBLType.HBL).Count;
            if (i > 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select MBL BL." : "请选择MBL提单进行发送.");
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
            //澳航
            OceanBLList aoMBL = null;

            if (LocalData.UserInfo.DefaultCompanyID.ToString().ToUpper() == "A62A9F8E-E69C-4E6E-AD85-E75AED3C6CF9")
            {
                #region 组织数据

                //澳航
                aoMBL =
                    mbls.Find(
                        delegate (OceanBLList item)
                        {
                            return item.CarrierID == new Guid("FB9634B0-ECFD-450B-A0B3-D52466D30383");
                        });

                if (aoMBL == null)
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish
                        ? "Shipowners are now only supports ANL Electronic batch."
                        : "现在只支持船东是[ANL]的电子补料。");
                    return;
                }

                foreach (var item in mbls)
                {
                    if (mblNoBuilder.Length > 0)
                        mblNoBuilder.Append(",");

                    mblNoBuilder.Append(item.No);

                    operationNos.Add(item.No);

                    mblIds.Add(item.MBLID);
                    oIds.Add(item.OceanBookingID);
                }
                if (aoMBL != null)
                {
                    subjuect = LocalData.IsEnglish ? "NingBoEDIANL(" + mblNoBuilder + ")" : "宁波EDI澳航(" + mblNoBuilder + ")";
                    key = "NBEDISIANL";
                    tip = LocalData.IsEnglish ? "NBEDIForANL" : "宁波EDI澳航";
                }
                #endregion
            }

            EDISendOption sendItem = new EDISendOption();
            sendItem.ServiceKey = key;
            sendItem.EdiMode = EDIMode.SI;
            sendItem.CompanyID = companyID;
            sendItem.Subject = subjuect;
            sendItem.IDs = oIds.ToArray();
            sendItem.FIDs = mblIds.ToArray();
            sendItem.NOs = operationNos.ToArray();
            sendItem.OperationType = OperationType.OceanExport;
            sendItem.SendByID = LocalData.UserInfo.LoginID;

            if (mblIds.Count > 0)
            {
                isSucc = ediClientService.SendEDI(sendItem);
            }
        }

        /// <summary>
        /// 发送ANL预配
        /// </summary>
        /// <param name="ediClientService">EDI服务</param>
        /// <param name="selectedList">所选BL列表</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="amsEntryType">AMS 类型</param>
        /// <param name="aciEntryType">ACI 类型</param>
        /// <param name="isSucc">是否成功</param>
        public static void InnerEMBLPro(IEDIClientService ediClientService, List<OceanBLList> selectedList, Guid companyID, ref bool isSucc)
        {
            List<OceanBLList> mbls = selectedList;
            if (mbls == null || mbls.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select current BL." : "请选择当前要发送EDI补料的提单.");
                return;
            }

            int i = mbls.FindAll(m => m.BLType == FCMBLType.HBL).Count;
            if (i > 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select MBL BL." : "请选择MBL提单进行发送.");
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
            //澳航
            OceanBLList aoMBL = null;

            if (LocalData.UserInfo.DefaultCompanyID.ToString().ToUpper() == "A62A9F8E-E69C-4E6E-AD85-E75AED3C6CF9")
            {
                #region 组织数据

                //澳航
                aoMBL =
                    mbls.Find(
                        delegate (OceanBLList item)
                        {
                            return item.CarrierID == new Guid("FB9634B0-ECFD-450B-A0B3-D52466D30383");
                        });

                if (aoMBL == null)
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish
                        ? "Shipowners are now only supports ANL Electronic batch."
                        : "现在只支持船东是[ANL]的预配。");
                    return;
                }

                foreach (var item in mbls)
                {
                    if (mblNoBuilder.Length > 0)
                        mblNoBuilder.Append(",");

                    mblNoBuilder.Append(item.No);

                    operationNos.Add(item.No);

                    mblIds.Add(item.MBLID);
                    oIds.Add(item.OceanBookingID);
                }
                if (aoMBL != null)
                {
                    subjuect = LocalData.IsEnglish ? "NingBoEDIPre(" + mblNoBuilder + ")" : "宁波澳航预配(" + mblNoBuilder + ")";
                    key = "NBEDIBookingANL";
                    tip = LocalData.IsEnglish ? "NBEDIForANL" : "宁波澳航预配";
                }
                #endregion
            }

            EDISendOption sendItem = new EDISendOption();
            sendItem.ServiceKey = key;
            sendItem.EdiMode = EDIMode.SI;
            sendItem.CompanyID = companyID;
            sendItem.Subject = subjuect;
            sendItem.IDs = oIds.ToArray();
            sendItem.FIDs = mblIds.ToArray();
            sendItem.NOs = operationNos.ToArray();
            sendItem.OperationType = OperationType.OceanExport;
            sendItem.SendByID = LocalData.UserInfo.LoginID;

            if (mblIds.Count > 0)
            {
                isSucc = ediClientService.SendEDI(sendItem);
            }
        }


        /// <summary>
        /// 宁波edi中心
        /// </summary>
        /// <param name="ediClientService"></param>
        /// <param name="selectedList"></param>
        /// <param name="companyID"></param>
        /// <param name="isSucc"></param>
        public static void NBEDI(IEDIClientService ediClientService, List<OceanBLList> selectedList, Guid companyID, ref bool isSucc)
        {
            List<OceanBLList> mbls = selectedList;
            if (mbls == null || mbls.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select current BL." : "请选择当前要发送EDI补料的提单.");
                return;
            }

            int i = mbls.FindAll(m => m.BLType == FCMBLType.HBL).Count;
            if (i > 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select MBL BL." : "请选择MBL提单进行发送.");
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

            #region 组织数据

            foreach (var item in mbls)
            {
                if (mblNoBuilder.Length > 0)
                    mblNoBuilder.Append(",");

                mblNoBuilder.Append(item.No);

                operationNos.Add(item.No);

                mblIds.Add(item.MBLID);
                oIds.Add(item.OceanBookingID);
            }
            subjuect = LocalData.IsEnglish ? "NingBoEDI(" + mblNoBuilder + ")" : "宁波EDI中心(" + mblNoBuilder + ")";
            key = "NBEDICenter";
            tip = LocalData.IsEnglish ? "NBEDI" : "宁波EDI中心";

            #endregion

            EDISendOption sendItem = new EDISendOption();
            sendItem.ServiceKey = key;
            sendItem.EdiMode = EDIMode.SI;
            sendItem.CompanyID = companyID;
            sendItem.Subject = subjuect;
            sendItem.IDs = oIds.ToArray();
            sendItem.FIDs = mblIds.ToArray();
            sendItem.NOs = operationNos.ToArray();
            sendItem.OperationType = OperationType.OceanExport;
            sendItem.AMSEntryType = AMSEntryType.Unknown;
            sendItem.ACIEntryType = ACIEntryType.Unknown;
            sendItem.SendByID = LocalData.UserInfo.LoginID;

            if (mblIds.Count > 0)
            {
                isSucc = ediClientService.SendEDI(sendItem);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static IOceanExportService OEService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Workitem"></param>
        /// <param name="listData"></param>
        /// <param name="ICPCommUIHelper"></param>
        /// <param name="editPartSaved"></param>
        public static void InnerCopyMBLData(WorkItem Workitem, OceanBLList listData, ICPCommUIHelper ICPCommUIHelper, PartDelegate.EditPartSaved editPartSaved)
        {
            OceanMBLInfo CloneData = OEService.GetOceanMBLInfo(listData.ID);

            #region 需清空的数据
            CloneData.ID = Guid.Empty;
            CloneData.State = OEBLState.Draft;
            CloneData.No = string.Empty;
            CloneData.MBLID = Guid.Empty;
            CloneData.HBLNos = string.Empty;
            CloneData.ContainerDescription = string.Empty;
            CloneData.CtnQtyInfo = string.Empty;
            CloneData.Measurement = CloneData.Weight = CloneData.Quantity = 0;
            CloneData.State = OEBLState.Draft;
            CloneData.CSCLGateIn = null;
            #endregion

            #region 设置默认值
            DataDictionaryList normalDictionary = null;
            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.PaymentTerm);
            CloneData.PaymentTermID = normalDictionary.ID;
            CloneData.PaymentTermName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
            CloneData.QuantityUnitID = normalDictionary.ID;
            CloneData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
            CloneData.WeightUnitID = normalDictionary.ID;
            CloneData.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
            CloneData.MeasurementUnitID = normalDictionary.ID;
            CloneData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;
            #endregion

            #region 列表的数据

            CloneData.SONO = listData.SONO;
            CloneData.AgentOfCarrierName = listData.AgentOfCarrierName;
            CloneData.CarrierName = listData.CarrierName;
            CloneData.SalesName = listData.SalesName;
            CloneData.FilerName = listData.FilerName;
            CloneData.BookingerName = listData.BookingerName;
            CloneData.OverseasFilerName = listData.OverseasFilerName;
            #endregion

            string title = LocalData.IsEnglish ? "Copy MBL" : "复制MBL";
            PartLoader.ShowEditPart<MBL.MBLEditPart>(Workitem, CloneData, title, editPartSaved);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Workitem"></param>
        /// <param name="listData"></param>
        /// <param name="ICPCommUIHelper"></param>
        /// <param name="editPartSaved"></param>
        public static void InnerCopyHBLData(WorkItem Workitem, OceanBLList listData, ICPCommUIHelper ICPCommUIHelper, PartDelegate.EditPartSaved editPartSaved)
        {
            OceanHBLInfo CloneData = OEService.GetOceanHBLInfo(listData.ID);
            CloneData.CarrierName = listData.CarrierName;
            #region 需清空的数据
            CloneData.ID = Guid.Empty;
            CloneData.AMSNo = string.Empty;
            CloneData.ISFNo = string.Empty;
            CloneData.State = OEBLState.Draft;
            CloneData.No = string.Empty;
            CloneData.ContainerDescription = string.Empty;
            CloneData.CtnQtyInfo = string.Empty;
            CloneData.Measurement = CloneData.Weight = CloneData.Quantity = 0;
            CloneData.ReleaseDate = null;//放单时间不能复制 2013-8-28 Liliang
            #endregion

            #region 设置默认值
            DataDictionaryList normalDictionary = null;
            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.PaymentTerm);
            CloneData.PaymentTermID = normalDictionary.ID;
            CloneData.PaymentTermName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
            CloneData.QuantityUnitID = normalDictionary.ID;
            CloneData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
            CloneData.WeightUnitID = normalDictionary.ID;
            CloneData.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
            CloneData.MeasurementUnitID = normalDictionary.ID;
            CloneData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;
            #endregion

            #region 列表的数据
            CloneData.SONO = listData.SONO;
            CloneData.AgentOfCarrierName = listData.AgentOfCarrierName;
            CloneData.CarrierName = listData.CarrierName;
            CloneData.SalesName = listData.SalesName;
            CloneData.FilerName = listData.FilerName;
            CloneData.BookingerName = listData.BookingerName;
            CloneData.OverseasFilerName = listData.OverseasFilerName;
            #endregion

            string title = LocalData.IsEnglish ? "Copy HBL" : "复制HBL";
            PartLoader.ShowEditPart<ICP.FCM.OceanExport.UI.HBL.HBLEditPart>(Workitem, CloneData, title, editPartSaved);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prams"></param>
        /// <param name="DataSource"></param>
        /// <param name="bsList"></param>
        public static void InnerRefershHBLDataSource(object[] prams, object DataSource, BindingSource bsList)
        {
            if (prams == null || prams.Length == 0) return;
            OceanHBLInfo data = prams[0] as OceanHBLInfo;

            List<OceanBLList> source = DataSource as List<OceanBLList>;
            if (source == null || source.Count == 0)
            {
                List<OceanBLList> orgSource = new List<OceanBLList>();
                bsList.DataSource = orgSource;

                OceanBLList mbl = null;
                List<OceanBLList> bls = OEService.GetOceanBLListByIds(new Guid[] { data.MBLID });
                if (bls != null && bls.Count > 0) mbl = bls[0];

                if (mbl != null)
                {
                    if (!bsList.Contains(mbl))
                        bsList.Add(mbl);
                }

                if (!bsList.Contains(data))
                {
                    bsList.Add(data);
                }
                bsList.ResetBindings(false);
            }
            else
            {
                OceanBLList mbl = source.Find(delegate (OceanBLList item) { return item.ID == data.MBLID; });
                if (mbl == null)
                {
                    List<OceanBLList> bls = OEService.GetOceanBLListByIds(new Guid[] { data.MBLID });
                    if (bls != null && bls.Count > 0) mbl = bls[0];
                    if (mbl != null)
                    {
                        OceanBLList oceanBLList = FindExistItem(bsList.DataSource, mbl);
                        if (oceanBLList == null)
                        {
                            bsList.Insert(0, mbl);
                            bsList.ResetBindings(false);
                        }
                    }
                }
                else
                {
                    OceanBLList updateMbl = OEService.GetOceanBLListByIds(new Guid[] { data.MBLID })[0];

                    OEUtility.CopyToValue(updateMbl, mbl, typeof(OceanBLList));
                }

                OceanBLList tager = source.Find(delegate (OceanBLList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    OceanBLList oceanBLList = FindExistItem(bsList.DataSource, data);
                    if (!bsList.Contains(data))
                    {
                        bsList.Insert(0, data);
                        bsList.ResetBindings(false);
                    }
                }
                else
                {
                    OEUtility.CopyToValue(data, tager, typeof(OceanBLList));
                    bsList.ResetItem(bsList.IndexOf(tager));
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prams"></param>
        /// <param name="DataSource"></param>
        /// <param name="bsList"></param>
        public static void InnerRefershMBLDataSource(object[] prams, object DataSource, BindingSource bsList)
        {
            if (prams == null || prams.Length == 0) return;

            OceanBLList data = prams[0] as OceanBLList;

            List<OceanBLList> source = DataSource as List<OceanBLList>;
            if (source == null || source.Count == 0)
            {
                List<OceanBLList> orgSource = new List<OceanBLList>();
                bsList.DataSource = orgSource;

                if (!bsList.Contains(data))
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
            }
            else
            {
                OceanBLList tager = source.Find(delegate (OceanBLList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    OceanBLList oceanBLList = FindExistItem(bsList.DataSource, data);
                    if (oceanBLList == null)
                    {
                        bsList.Insert(0, data);
                        bsList.ResetBindings(false);
                    }
                }
                else
                {
                    OEUtility.CopyToValue(data, tager, typeof(OceanBLList));
                    bsList.ResetItem(bsList.IndexOf(tager));
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datasource"></param>
        /// <param name="oceanBLList"></param>
        /// <returns></returns>
        public static OceanBLList FindExistItem(object datasource, OceanBLList oceanBLList)
        {
            List<OceanBLList> list = datasource as List<OceanBLList>;
            OceanBLList _oceanBLList = list.Find(delegate (OceanBLList item) { return item.ID == oceanBLList.ID; });
            return _oceanBLList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bsList"></param>
        public static void InnerRefersh(BindingSource bsList)
        {
            List<OceanBLList> blList = bsList.DataSource as List<OceanBLList>;
            if (blList == null || blList.Count == 0) return;

            List<Guid> ids = blList.Select(item => item.ID).ToList();

            List<OceanBLList> list = ServiceClient.GetService<IOceanExportService>().GetOceanBLListByIds(ids.ToArray());
            bsList.DataSource = list;
            bsList.ResetBindings(false);
        }
        public static bool InnerDelete(OceanBLList listData, object DataSource, BindingSource bsList)
        {
            if (listData == null || (listData.State != OEBLState.Draft && listData.State != OEBLState.Checking))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Status  can be deleted when as a draft or a Checking" : "状态为草稿或对单中才允许删除."
                               , LocalData.IsEnglish ? "Tip" : "提示");
                return false;
            }

            string No = listData.No;

            if (listData.ExistFees)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "UN Done" : "若要执行此操作,请先删除提单下的费用."
                               , LocalData.IsEnglish ? "Tip" : "提示");
                return false;
            }

            if (DevExpress.XtraEditors.XtraMessageBox.Show((LocalData.IsEnglish ? "Sure Delete BL " : "你真的要删除提单") + No + "?"
                               , LocalData.IsEnglish ? "Tip" : "提示"
                               , MessageBoxButtons.YesNo
                               , MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;
            }


            if (listData.BLType == FCMBLType.MBL)
            {
                OEService.RemoveOceanMBLInfo(listData.ID, LocalData.UserInfo.LoginID, listData.UpdateDate);
                bsList.RemoveCurrent();
            }
            else
            {
                OEService.RemoveOceanHBLInfo(listData.ID, LocalData.UserInfo.LoginID, listData.UpdateDate);

                List<OceanBLList> source = DataSource as List<OceanBLList>;
                OceanBLList mbl = source.Find(delegate (OceanBLList item) { return item.ID == listData.MBLID; });
                if (mbl != null)
                {
                    OceanBLList existhbl = source.Find(delegate (OceanBLList item) { return item.BLType == FCMBLType.HBL && item.MBLID == mbl.ID && item.ID != listData.ID; });
                    if (existhbl == null) mbl.HBLCount = 0;
                    source.Remove(listData);
                }
            }

            return true;
        }


        private static void MergeDictionary(Dictionary<string, object> target, Dictionary<string, object> source)
        {

            if (source == null || source.Count == 0)
                return;
            foreach (KeyValuePair<string, object> pair in source)
            {
                target.Add(pair.Key, pair.Value);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationNo"></param>
        /// <returns></returns>
        public static string GetLineNo(string operationNo)
        {
            if (string.IsNullOrEmpty(operationNo))
            {
                return string.Empty;
            }
            else if (operationNo.Length <= 4)
            {
                return (LocalData.IsEnglish ? ":" : "：") + operationNo;
            }
            else
            {
                return (LocalData.IsEnglish ? ":" : "：") + operationNo.Substring(operationNo.Length - 4, 4);
            }
        }
    }
}
