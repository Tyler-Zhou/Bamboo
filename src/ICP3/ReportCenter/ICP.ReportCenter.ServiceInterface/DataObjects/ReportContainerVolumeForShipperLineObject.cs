using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.ReportCenter.ServiceInterface
{
    /// <summary>
    /// 箱量统计Report实体对象
    /// </summary>
    public class ReportContainerVolumeForShipperLine
    {
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 口岸公司
        /// </summary>
        public string CompanyName
        {
            get;
            set;
        }

        /// <summary>
        /// 统计周
        /// </summary>
        public string Week
        {
            get;
            set;
        }

        /// <summary>
        /// POL
        /// </summary>
        public string POL
        {
            get;
            set;
        }

        /// <summary>
        /// POL T量统计
        /// </summary>
        public string POLVolume
        {
            get;
            set;
        }

        /// <summary>
        /// POD
        /// </summary>
        public string POD
        {
            get;
            set;
        }

        /// <summary>
        /// POD T量统计
        /// </summary>
        public int PODVolume
        {
            get;
            set;
        }

        /// <summary>
        /// CSCL T量统计
        /// </summary>
        public int CSCLVolumn
        {
            get;
            set;
        }

        /// <summary>
        /// ZIM T量统计
        /// </summary>
        public int ZIMVolunm
        {
            get;
            set;
        }

        /// <summary>
        /// CSAV T量统计
        /// </summary>
        public int CSAVVolunm
        {
            get;
            set;
        }

        /// <summary>
        /// COSCO T量统计
        /// </summary>
        public int COSCOVolunm
        {
            get;
            set;
        }

        /// <summary>
        /// PIL T量统计
        /// </summary>
        public int PILVolunm
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// 其它船公司 T量统计
        /// </summary>
        public int OthersVolunm
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 销售员
        /// </summary>
        public string SalesName
        {
            get;
            set;
        }
    }
    
    [Serializable]
    public class ReportContainerVolumeTotal 
    {
        /// <summary>
        /// POD T量统计
        /// </summary>
        public int PODVolume
        {
            get;
            set;
        }

        /// <summary>
        /// CSCL T量统计
        /// </summary>
        public int CSCLVolumn
        {
            get;
            set;
        }

        /// <summary>
        /// ZIM T量统计
        /// </summary>
        public int ZIMVolunm
        {
            get;
            set;
        }

        /// <summary>
        /// CSAV T量统计
        /// </summary>
        public int CSAVVolunm
        {
            get;
            set;
        }

        /// <summary>
        /// COSCO T量统计
        /// </summary>
        public int COSCOVolunm
        {
            get;
            set;
        }

        /// <summary>
        /// PIL T量统计
        /// </summary>
        public int PILVolunm
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// 其它船公司 T量统计
        /// </summary>
        public int OthersVolunm
        {
            get;
            set;
        }
    }
}
