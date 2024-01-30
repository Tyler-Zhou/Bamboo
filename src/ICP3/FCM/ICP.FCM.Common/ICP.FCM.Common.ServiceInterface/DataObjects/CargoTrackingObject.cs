using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{

    /// <summary>
    /// 货物跟踪类
    /// </summary>
    [Serializable]
    public class CargoTrackingInfo
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID { get; set; }

        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNO { get; set; }

        /// <summary>
        /// 订舱号
        /// </summary>
        public string SONO { get; set; }

        /// <summary>
        /// 主提单号
        /// </summary>
        public string MBLNO { get; set; }

        /// <summary>
        /// 船东
        /// </summary>
        public string Carrier { get; set; }

        /// <summary>
        /// 船东
        /// </summary>
        public Guid? CarrierID { get; set; }

        /// <summary>
        /// 头程船（驳船）船名航次
        /// </summary>
        public string PreVoyage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? PreVoyageID { get; set; }

        /// <summary>
        /// 头程船（驳船）离港日
        /// </summary>
        public DateTime? PreETD { get; set; }

        /// <summary>
        /// 大船船名航次
        /// </summary>
        public string Voyage { get; set; }
        /// <summary>
        /// 大船船名航次
        /// </summary>
        public Guid? VoyageID { get; set; }

        #region 开/截港
        /// <summary>
        /// VoyageID
        /// </summary>
        public Guid? OpenClosePortId { get; set; }
        /// <summary>
        /// 开港日
        /// </summary>
        public DateTime? OpenPort { get; set; }
        /// <summary>
        /// 截港日
        /// </summary>
        public DateTime? ClosePort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OpenAndClosePortDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OpenAndClosePortUpdateDate { get; set; }
        /// <summary>
        /// 网络上获取的ETD
        /// </summary>
        public DateTime? NetETD { get; set; }
        /// <summary>
        /// 码头
        /// </summary>
        public string DOCK { get; set; }
        /// <summary>
        /// 是否要查询码头
        /// </summary>
        public bool IsSearchDock { get; set; }
        #endregion

        /// <summary>
        /// 大船离港日
        /// </summary>
        public DateTime? ETD { get; set; }

        /// <summary>
        /// 大船到港日
        /// </summary>
        public DateTime? ETA { get; set; }

        /// <summary>
        /// 截柜日
        /// </summary>
        public DateTime? CY_RailCUT { get; set; }

        /// <summary>
        /// 截关日
        /// </summary>
        public DateTime? AESCUT { get; set; }

        /// <summary>
        /// 截文件日
        /// </summary>
        public DateTime? DOC_SICUT { get; set; }

        /// <summary>
        /// 截AMS日
        /// </summary>
        public DateTime? AMSClose { get; set; }

        /// <summary>
        /// 还柜地
        /// </summary>
        public string ReturnLoc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? ReturnLocID { get; set; }

        /// <summary>
        /// 装货港ID
        /// </summary>
        public Guid POLID { get; set; }

        /// <summary>
        /// 卸货港ID
        /// </summary>
        public Guid PODID { get; set; }

        /// <summary>
        /// 装货港代码
        /// </summary>
        public string POLCode { get; set; }

        /// <summary>
        /// 卸货港代码
        /// </summary>
        public string PODCode { get; set; }

        /// <summary>
        /// 装货港
        /// </summary>
        public string POL { get; set; }

        /// <summary>
        /// 卸货港
        /// </summary>
        public string POD { get; set; }

        /// <summary>
        /// 交货地
        /// </summary>
        public string PlaceOfDelivery { get; set; }

        /// <summary>
        /// 到交货地日
        /// </summary>
        public DateTime? DETA { get; set; }

        /// <summary>
        /// 最终目的地
        /// </summary>
        public string FinalDestination { get; set; }

        /// <summary>
        /// 到最终目的地日
        /// </summary>
        public DateTime? FETA { get; set; }

        /// <summary>
        /// 提柜地
        /// </summary>
        public string PickUpPlace { get; set; }

        /// <summary>
        /// 提柜地
        /// </summary>
        public Guid? PickUpID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// 箱信息
        /// </summary>
        public List<CargoTrackingContainerInfo> Containers { get; set; }
    }

    /// <summary>
    /// 货物跟踪箱信息
    /// </summary>
    [Serializable]
    public class CargoTrackingContainerInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid MBLId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// 主提单号
        /// </summary>
        public string MBLNO { get; set; }


        /// <summary>
        /// 箱ID
        /// </summary>
        public Guid ContainerID { get; set; }

        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNO { get; set; }

        /// <summary>
        /// 海关放行
        /// </summary>
        public string CustomsRelease { get; set; }
        /// <summary>
        /// 海关放行详细
        /// </summary>
        public string CustomsReleaseDesc { get; set; }

        /// <summary>
        /// 进场日
        /// </summary>
        public string GateIn { get; set; }
        /// <summary>
        /// 进场日详细
        /// </summary>
        public string GateInDesc { get; set; }

        /// <summary>
        /// 确认上船
        /// </summary>
        public string Loadship { get; set; }
        /// <summary>
        /// 确认上船详细
        /// </summary>
        public string LoadshipDesc { get; set; }

        /// <summary>
        /// 是否需要更新
        /// </summary>
        public bool IsUpdate { get; set; }

        /// <summary>
        /// 可以提柜日
        /// </summary>
        public DateTime? AvailableDate { get; set; }

        /// <summary>
        /// 免堆日
        /// </summary>
        public DateTime? LastFreeDate { get; set; }

        /// <summary>
        /// 提货单号
        /// </summary>
        public string PickUpNo { get; set; }
        /// <summary>
        /// 提柜时间
        /// </summary>
        public DateTime? PickUpDate { get; set; }

        /// <summary>
        /// OIContainerTracking.GenerantTime 还空时间
        /// </summary>
        public DateTime? ReturnDate { get; set; }


        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? AvailableTime { get; set; }

        /// <summary>
        /// 到达目的地时间
        /// </summary>
        public DateTime? DeliveryTime { get; set; }

        private string status;
        /// <summary>
        /// 箱子当前状态
        /// </summary>
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                List<string> s = GetContainerStatusByXMLStr(value, false);
                if (s.Count > 0)
                    status = s[0];
            }
        }
        private string statusALL;
        /// <summary>
        /// 
        /// </summary>
        public string StatusALL
        {
            get
            {
                return statusALL;
            }
            set
            {
                List<string> str = GetContainerStatusByXMLStr(value, true);
                if (str.Count > 0)
                {
                    statusALL += "<table border='1' width='100%'><tr><td>时间</td><td>地点</td><td>事件</td></tr>";
                    foreach (string s in str)
                    {
                        statusALL += "<tr>" + s + "</tr>";
                    }
                    statusALL += "</table>";
                }
            }
        }


        private List<string> GetContainerStatusByXMLStr(string xmlStr, bool isAll)
        {
            xmlStr = "<Trackings>" + xmlStr + "</Trackings>";
            List<string> res = new List<string>();
            if (!string.IsNullOrEmpty(xmlStr))
            {
                XmlDocument xmlDoc = new XmlDocument();
                byte[] bs = Encoding.UTF8.GetBytes(xmlStr);
                MemoryStream ms = new MemoryStream(bs);
                xmlDoc.Load(ms); //加载XML文档   
                XmlNodeList list = xmlDoc.SelectSingleNode("Trackings").ChildNodes;

                if (list != null && list.Count > 0)
                {
                    foreach (XmlNode node in list)
                    {
                        string s = string.Empty;
                        if (!isAll)
                            s = (node["Date"]).InnerText + " " + (node["Place"]).InnerText + " " + (node["Event"]).InnerText;
                        else
                            s = "<td>" + (node["Date"]).InnerText + "</td><td>" + (node["Place"]).InnerText + "</td><td>" + (node["Event"]).InnerText + "</td>";
                        res.Add(s);
                    }
                }
            }
            return res;
        }
    }

}
