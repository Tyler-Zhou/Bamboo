using System;
using System.Collections.Generic;
using System.Text;

namespace LongWin.BusinessInfo.ServiceInterface.DataObject
{
    [Serializable]
   public class JobInfoData
    {
        bool _selected;
        /// <summary>
        /// ѡ��
        /// </summary>
        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        Guid _ConsignId;
        /// <summary>
        /// ί��ID
        /// </summary>
        public Guid ConsignId
        {
            get { return _ConsignId; }
            set { _ConsignId = value; }
        }

        Guid _ConsignerId;
        /// <summary>
        /// ί����ID
        /// </summary>
        public Guid ConsignerId
        {
            get { return _ConsignerId; }
            set { _ConsignerId = value; }
        }

        string _SalesName;
        /// <summary>
        /// ҵ��Ա
        /// </summary>
        public string SalesName
        {
            get { return _SalesName; }
            set { _SalesName = value; }
        }

        string _VesselVoyage;
        /// <summary>
        /// ��������
        /// </summary>
        public string VesselVoyage
        {
            get { return _VesselVoyage; }
            set { _VesselVoyage = value; }
        }

        string _OperatorName;
        /// <summary>
        /// ����Ա
        /// </summary>
        public string OperatorName
        {
            get { return _OperatorName; }
            set { _OperatorName = value; }
        }

        DateTime _ETA;
        /// <summary>
        /// ETA
        /// </summary>
        public DateTime ETA
        {
            get { return _ETA; }
            set { _ETA = value; }
        }

        DateTime _ETD;
        /// <summary>
        /// ETD
        /// </summary>
        public DateTime ETD
        {
            get { return _ETD; }
            set { _ETD = value; }
        }

       Decimal _TEU;
        /// <summary>
        /// ����
        /// </summary>
       public Decimal TEU
        {
            get { return _TEU; }
            set { _TEU = value; }
        }

        decimal _AmountUSDByCR;
        /// <summary>
        /// Ӧ������
        /// </summary>
        public decimal AmountUSDByCR
        {
            get { return _AmountUSDByCR; }
            set { _AmountUSDByCR = value; }
        }

        decimal _AmountUSDByDR;
        /// <summary>
        /// Ӧ������
        /// </summary>
        public decimal AmountUSDByDR
        {
            get { return _AmountUSDByDR; }
            set { _AmountUSDByDR = value; }
        }

        decimal _ProfitByUSD;
        /// <summary>
        /// ����
        /// </summary>
        public decimal ProfitByUSD
        {
            get { return _ProfitByUSD; }
            set { _ProfitByUSD = value; }
        }

        string _AgentName;
        /// <summary>
        /// ����
        /// </summary>
        public string AgentName
        {
            get { return _AgentName; }
            set { _AgentName = value; }
        }
        string _ConsignerEName;
        /// <summary>
        /// ί��������(Ӣ��)
        /// </summary>
        public string ConsignerEName
        {
            get { return _ConsignerEName; }
            set { _ConsignerEName = value; }
        }

        string _ConsignerCName;
        /// <summary>
        /// ί��������(����)
        /// </summary>
        public string ConsignerCName
        {
            get { return _ConsignerCName; }
            set { _ConsignerCName = value; }
        }

        string _DestinationName;
        /// <summary>
        /// Ŀ�ĵ�
        /// </summary>
        public string DestinationName
        {
            get { return _DestinationName; }
            set { _DestinationName = value; }
        }

        string _LoadPortname;
        /// <summary>
        /// װ���ۿ�
        /// </summary>
        public string LoadPortname
        {
            get { return _LoadPortname; }
            set { _LoadPortname = value; }
        }

        string _DiscPortName;
        /// <summary>
        /// ж���ۿ�
        /// </summary>
        public string DiscPortName
        {
            get { return _DiscPortName; }
            set { _DiscPortName = value; }
        }

        string _ContainerNo;
        /// <summary>
        /// ���
        /// </summary>
        public string ContainerNo
        {
            get { return _ContainerNo; }
            set { _ContainerNo = value; }
        }

        string _BLNO;
        /// <summary>
        /// �ᵥ��
        /// </summary>
        public string BLNO
        {
            get { return _BLNO; }
            set { _BLNO = value; }
        }

        //bool _IsVerifyed;
        ///// <summary>
        ///// 
        ///// </summary>
        //public bool IsVerifyed
        //{
        //    get { return _IsVerifyed; }
        //    set { _IsVerifyed = value; }
        //}

        /// <summary>
        /// �տ�״̬
        /// </summary>
        public ENVerifyed ENVerifyedState { get; set; }
        /// <summary>
        /// �տ�״̬
        /// </summary>
        public CNVerifyed CNVerifyedState { get; set; }

        string _JobCode;
        /// <summary>
        /// ҵ���
        /// </summary>
        public string JobCode
        {
            get { return _JobCode; }
            set { _JobCode = value; }
        }

        decimal _weight;
        /// <summary>
        /// ��������
        /// </summary>
        public decimal Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }


        public decimal CommisionAmount { get; set; }
    }

    public enum ENVerifyed
    {
        /// <summary>
        /// δ�տ�
        /// </summary>
        UnVerifyed=0,
        /// <summary>
        /// ȫ�����տ�
        /// </summary>
        AllVerifyed=1,
        /// <summary>
        /// �������տ�
        /// </summary>
        PartVerifyed=2
    }
    public enum CNVerifyed
    {
        /// <summary>
        /// δ�տ�
        /// </summary>
        δ�տ� = 0,
        /// <summary>
        /// ȫ�����տ�
        /// </summary>
        ȫ�����տ� = 1,
        /// <summary>
        /// �������տ�
        /// </summary>
        �������տ� = 2
    }
}
