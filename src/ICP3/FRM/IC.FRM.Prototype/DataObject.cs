using System;

namespace IC.FRM.Prototype
{
    [Serializable]
    public class InquireOceanListData
    {
        public string Carrier
        {
            get;
            set;
        }

        public string POL
        {
            get;
            set;
        }

        public string POD
        {
            get;
            set;
        }

        public string Delivery
        {
            get;
            set;
        }

        public string Currency
        {
            get;
            set;
        }

        public string _20GP
        {
            get;
            set;
        }

        public string _40GP
        {
            get;
            set;
        }

        public string _40HQ
        {
            get;
            set;
        }

        public string Commodity
        {
            get;
            set;
        }

        public string Term
        {
            get;
            set;
        }

        public string SurCharge
        {
            get;
            set;
        }

        public string Duration_From
        {
            get;
            set;
        }

        public string Duration_To
        {
            get;
            set;
        }
    }

    public class InquireDiscussingData
    {
        public string From
        {
            get;
            set;
        }

        public string Contect
        {
            set;
            get;
        }
    }

    public class ContainerPrice
    {
        public string Container
        {
            get;
            set;
        }

        public string Volumn
        {
            get;
            set;
        }

        public string ExpectedPrice
        {
            get;
            set;
        }
    }
}
