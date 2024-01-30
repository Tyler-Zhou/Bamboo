using System;
using System.Collections.Generic;
using System.Text;

namespace LongWin.BusinessInfo.ServiceInterface.DataObject
{
    [Serializable]
  public  class JobFeeData
    {
        string _CustomerCName;
        /// <summary>
        /// ������λ����(����)
        /// </summary>
        public string CustomerCName
        {
            get { return _CustomerCName; }
            set { _CustomerCName = value; }
        }

        string _CustomerEName;
        /// <summary>
        /// ������λ����(Ӣ��)
        /// </summary>
        public string CustomerEName
        {
            get { return _CustomerEName; }
            set { _CustomerEName = value; }
        }

        string _CurrencyName;
        /// <summary>
        /// ��������
        /// </summary>
        public string CurrencyName
        {
            get { return _CurrencyName; }
            set { _CurrencyName = value; }
        }

        string _FeeEName;
        /// <summary>
        /// ��������(Ӣ��)
        /// </summary>
        public string FeeEName
        {
            get { return _FeeEName; }
            set { _FeeEName = value; }
        }

        string _FeeCName;
        /// <summary>
        /// ��������(����)
        /// </summary>
        public string FeeCName
        {
            get { return _FeeCName; }
            set { _FeeCName = value; }
        }

        short _DrCrFlag;
        /// <summary>
        /// ��/����־��0:Ӧ�� 1:Ӧ����
        /// </summary>
        public short DrCrFlag
        {
            get { return _DrCrFlag; }
            set { _DrCrFlag = value; }
        }

        bool _RecoupFlag;
        /// <summary>
        /// ����
        /// </summary>
        public bool RecoupFlag
        {
            get { return _RecoupFlag; }
            set { _RecoupFlag = value; }
        }

        bool _IsPaid;
        /// <summary>
        /// ����
        /// </summary>
        public bool IsPaid
        {
            get { return _IsPaid; }
            set { _IsPaid = value; }
        }

        decimal _Rate;
        /// <summary>
        /// ����
        /// </summary>
        public decimal Rate
        {
            get { return _Rate; }
            set { _Rate = value; }
        }

        decimal _Amount;
        /// <summary>
        /// ���
        /// </summary>
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
    }
}
