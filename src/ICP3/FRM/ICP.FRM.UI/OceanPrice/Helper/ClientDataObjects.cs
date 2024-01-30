using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using ICP.FRM.ServiceInterface.DataObjects;
using System.Text;


namespace ICP.FRM.UI.OceanPrice
{

    #region 批处理对象

    /// <summary>
    /// 批处理BasePort Item对象
    /// </summary>
    public class BasePortBatchItem : BaseDataObject
    {
        public string Account { get; set; }
        public bool CleanAccount { get; set; }

        public AccountType AccountType { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public bool CleanFromDate { get; set; }
        public bool CleanToDate { get; set; }

        public Guid? POLID { get; set; }
        public string POLName { get; set; }
        public Guid? VIAID { get; set; }
        public string VIAName { get; set; }
        public Guid? PODID { get; set; }
        public string PODName { get; set; }
        public Guid? PlaceOfDeliveryID { get; set; }
        public string PlaceOfDeliveryName { get; set; }

        public Guid? TransportClauseListID { get; set; }

        public List<OceanClientUnit> OceanClientUnits { get; set; }
        public List<OceanClientUnitToString> OceanClientUnitList { get; set; }
        public bool RateAppend { get; set; }

        public string SurCharge { get; set; }
        public ChangeOperation SurchargeOperation { get; set; }

        public bool? OriginArb { get; set; }
        public bool? DestArb { get; set; }
        public string ItemCode { get; set; }

        public string Comm { get; set; }
        public ChangeOperation CommOperation { get; set; }
        public string Description { get; set; }
        public ChangeOperation DescriptionOperation { get; set; }

        public string TransitTime { get; set; }
        public ChangeOperation TransitTimeOperation { get; set; }
        public string CLS { get; set; }
    }

    /// <summary>
    /// 批处理Arbitrary Item对象
    /// </summary>
    public class ArbitraryBatchItem : BaseDataObject
    {
        public GeographyType GeographyType { get; set; }

        public Guid? POLID { get; set; }
        public string POLName { get; set; }
       
        public Guid? PODID { get; set; }
        public string PODName { get; set; }

        public Guid? TransportClauseListID { get; set; }

        public List<OceanClientUnit> OceanClientUnits { get; set; }
        public bool RateAppend { get; set; }

        public string ItemCode { get; set; }
    }

    public enum ChangeOperation
    {
        Append = 1, Clean = 2, Override = 3

    }

    public class OceanClientUnit
    {
        public Guid UnitID { get; set; }
        public string UnitName { get; set; }
        public decimal? Rate { get; set; }
    }

    public class OceanClientUnitToString
    {
        public Guid UnitID { get; set; }
        public string UnitName { get; set; }
        public string Rate { get; set; }
    }

    #endregion

    #region  Filter对象

    public class BasePortFilterObject
    {
        public string Account { get; set; }
        public bool AccountExcl { get; set; }

        public string POL { get; set; }
        public bool POLExcl { get; set; }

        public string VIA { get; set; }
        public bool VIAExcl { get; set; }

        public string POD { get; set; }
        public bool PODExcl { get; set; }

        public string Delivery { get; set; }
        public bool DeliveryExcl { get; set; }

        public string ItemCode { get; set; }
        public bool ItemCodeExcl { get; set; }

        public string Comm { get; set; }
        public bool CommExcl { get; set; }

        public string Term { get; set; }
        public bool TermExcl { get; set; }

        public string SurCharge { get; set; }
        public bool SurChargeExcl { get; set; }

        public string Description { get; set; }
        public bool DescriptionExcl { get; set; }

        public bool OnlyShowError { get; set; }

        public string BulidFilterString()
        {
            StringBuilder activeFilter = new StringBuilder();
            //[POLName] Like '%123%' And [POLName] Not Like '%23%'

            BulidFilterString(activeFilter, Account, AccountExcl, "Account");
            BulidFilterString(activeFilter, POL, POLExcl, "POLName");
            BulidFilterString(activeFilter, VIA, VIAExcl, "VIAName");
            BulidFilterString(activeFilter, POD, PODExcl, "PODName");
            BulidFilterString(activeFilter, Delivery, DeliveryExcl, "PlaceOfDeliveryName");

            BulidFilterString(activeFilter, ItemCode, ItemCodeExcl, "ItemCode");
            BulidSingleFilterString(activeFilter, Comm, CommExcl, "Comm");
            BulidFilterString(activeFilter, Term, TermExcl, "TransportClauseName");
            BulidFilterString(activeFilter, SurCharge, SurChargeExcl, "SurCharge");
            BulidFilterString(activeFilter, Description, DescriptionExcl, "Description");

            if (OnlyShowError)
            {
                if (activeFilter.Length > 0)
                {
                    activeFilter.Append(" AND ");
                }
                activeFilter.Append(" [HasError]='True' ");
            }
            
            return activeFilter.ToString();
        }


        private void BulidSingleFilterString(StringBuilder activeFilter, string text, bool isExcl, string filterName)
        {
            if (text == null || text.Trim().Length == 0) return;
            //string[] conditions = text.Trim().Split(new string[] { GlobalConstants.ShowDividedSymbol }, StringSplitOptions.None);

            StringBuilder itemFilter = new StringBuilder();

            if (isExcl)
                itemFilter.Append("[" + filterName + "]" + "Not like '%" + text.Trim() + "%'");
            else
                itemFilter.Append("[" + filterName + "]" + "like '%" + text.Trim() + "%'");

            if (activeFilter.Length > 0) activeFilter.Append(" AND ");

            activeFilter.Append(itemFilter.ToString());
        }

        private void BulidFilterString(StringBuilder activeFilter, string text, bool isExcl, string filterName)
        {
            if (text == null || text.Trim().Length == 0) return;
            string[] conditions = text.Trim().Split(new string[] { GlobalConstants.ShowDividedSymbol }, StringSplitOptions.None);

            if (conditions.Length <= 0) return;


            StringBuilder itemFilter = new StringBuilder();
            itemFilter.Append("(");

            for (int i = 0; i < conditions.Length; i++)
            {
                if (!string.IsNullOrEmpty(conditions[i]))
                {
                    if (itemFilter.Length > 1)
                    {
                        itemFilter.Append(isExcl ? " AND " : " OR ");
                    }
                    if (isExcl)
                    {
                        itemFilter.Append("[" + filterName + "]" + "Not like '%" + conditions[i] + "%'");
                    }
                    else
                    {
                        itemFilter.Append("[" + filterName + "]" + "like '%" + conditions[i] + "%'");
                    }
                }
            }
            itemFilter.Append(")");


            if (activeFilter.Length > 0) activeFilter.Append(" AND ");

            activeFilter.Append(itemFilter.ToString());
        }
    }
    
    public class ArbitraryFilterObject
    {
        public string Account { get; set; }
        public bool AccountExcl { get; set; }

        public string POL { get; set; }
        public bool POLExcl { get; set; }

        public string POD { get; set; }
        public bool PODExcl { get; set; }

        public string ItemCode { get; set; }
        public bool ItemCodeExcl { get; set; }

        public string Term { get; set; }
        public bool TermExcl { get; set; }

        public bool OnlyShowError { get; set; }

        public string BulidFilterString()
        {
            StringBuilder activeFilter = new StringBuilder();
            //[POLName] Like '%123%' And [POLName] Not Like '%23%'

            BulidFilterString(activeFilter, POL, POLExcl, "POLName");
            BulidFilterString(activeFilter, POD, PODExcl, "PODName");
            BulidFilterString(activeFilter, ItemCode, ItemCodeExcl, "ItemCode");
            BulidFilterString(activeFilter, Term, TermExcl, "TransportClauseName");


            if (OnlyShowError)
            {
                if (activeFilter.Length > 0)
                {
                    activeFilter.Append(" AND ");
                }
                activeFilter.Append(" [HasError]='True' ");
            }
            return activeFilter.ToString();
        }

        private void BulidFilterString(StringBuilder activeFilter, string text, bool isExcl, string filterName)
        {
            if (text == null || text.Trim().Length == 0) return;
            string[] conditions = text.Trim().Split(new string[] { GlobalConstants.ShowDividedSymbol }, StringSplitOptions.None);

            if (conditions.Length <= 0) return;


            StringBuilder itemFilter = new StringBuilder();
            itemFilter.Append("(");

            for (int i = 0; i < conditions.Length; i++)
            {
                if (!string.IsNullOrEmpty(conditions[i]))
                {
                    if (itemFilter.Length > 1)
                    {
                        itemFilter.Append(isExcl ? " AND " : " OR ");
                    }
                    if (isExcl)
                    {
                        itemFilter.Append("[" + filterName + "]" + "Not like '%" + conditions[i] + "%'");
                    }
                    else
                    {
                        itemFilter.Append("[" + filterName + "]" + "like '%" + conditions[i] + "%'");
                    }
                }
            }
            itemFilter.Append(")");


            if (activeFilter.Length > 0) activeFilter.Append(" AND ");

            activeFilter.Append(itemFilter.ToString());
        }
    }

    #endregion

    #region 客户端枚举


    #endregion
}
