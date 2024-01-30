using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.Odbc;

namespace LongWin.DataWarehouseReport.ToolsMethod
{
    public class Tools
    {

        public static DateTime GetDateForInputStr(string dateStr,bool IsDate,bool returnMaxDay)
        {
            if (IsDate)
            {
                return Convert.ToDateTime(dateStr);
            }
            else
            {
                if (!returnMaxDay)
                {
                    return Convert.ToDateTime(dateStr.Substring(0, 4) + "-" + dateStr.Substring(4, 2) + "-01");
                }
                else
                {
                    return Convert.ToDateTime(dateStr.Substring(0, 4) + "-" + dateStr.Substring(4, 2) + "-01").AddMonths(1).AddDays(-1);
                }
            }

        }

         /// <summary>
        /// 根据传入的参数替换条件的值
        /// </summary>
        /// <returns></returns>
        public static string GetConditionForCostValue(string XMLCondition, string Keys, string Values)
        {

            string[] keys = Keys.Split(new string[] { "," }, StringSplitOptions.None);
            string[] values = Values.Split(new string[] { "!" }, StringSplitOptions.None);

            Hashtable htab = new Hashtable();
            htab.Add("员工", "UserID");
            htab.Add("费用项目", "CostItemID");
            htab.Add("组织结构", "StructNodeId");
            htab.Add("开始时间", "Beginning_Date");
            htab.Add("结束时间", "Ending_Date");
            htab.Add("层次标记", "GroupByTotalType");
            htab.Add("时间类别", "DateType");
            htab.Add("Employee", "UserID");
            htab.Add("Cost Item", "CostItemID");
            htab.Add("Department", "StructNodeId");
            htab.Add("Beginning_Date", "Beginning_Date");
            htab.Add("Ending_Date", "Ending_Date");
            htab.Add("GroupByTotalType", "GroupByTotalType");
            htab.Add("DateType", "DateType");
            
            XmlTextReader xmlreader = new XmlTextReader(new StringReader(XMLCondition));
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlreader);
            dataSet.Tables[0].Rows[0].BeginEdit();
            int j = 0;
            int min = 0;
            if (keys.Length <= 3)
            {
                min = 0;
            }
            else
            {
                min = keys.Length - 3;
            }
            for (int i = min; i < keys.Length; i++)
            {
                string key = keys[i];
                if (key.Trim() != string.Empty)
                {
                    if (key.Trim() != "开始时间"
                        && key.Trim() != "结束时间"
                        && (values[j].Trim() == string.Empty
                    || values[j].Trim() == "''"
                    || values[j].Trim() == "'"))//处理数据库中为空的值
                    {
                        values[j] = "'00000000-0000-0000-0000-000000000000'";
                    }
                    dataSet.Tables[0].Rows[0][htab[key].ToString()] = values[j].Trim();
                   
                }
                dataSet.Tables[0].Rows[0].EndEdit();
                 j++;
            }

            System.IO.StringWriter str = new System.IO.StringWriter();
            XmlTextWriter writer = new XmlTextWriter(str);
            writer.Formatting = Formatting.Indented;
            dataSet.WriteXml(writer);
            writer.Close();


            //string XMLText = "<XML>my data</XML>";

            //StringReader strReader = new  StringReader(XMLText);

            //XmlTextReader reader = new  XmlTextReader(strReader);
            //string id = reader.GetAttribute("XML");
            return str.ToString();

        }

          /// <summary>
        /// 根据传入的参数获取值
        /// </summary>
        /// <returns></returns>
        public static string GetConditionForValue(string XMLCondition, string Key)
        {
   
//             XMLCondition =  @"<root> <StructType>0</StructType> 
//            <StructNodeId>B13FAC2D-8250-4990-A622-5ECA00D3A030</StructNodeId>  
//                <ETD_Beginning_Date>2007-01-01</ETD_Beginning_Date>  
//                    <ETD_Ending_Date>2009-01-01</ETD_Ending_Date>  
//                        <SalesType>3</SalesType> 
//                            <SalesSet></SalesSet> 
//                                <ConsignerSet></ConsignerSet>  
//                                    <ShipAgentSet></ShipAgentSet>  
//                                        <CarrierSet></CarrierSet>
//                                            <ShippingLineSet></ShippingLineSet> 
//                                                <JobType>0,1,2,3,4,5,6,7,8,9</JobType> 
//                                                    <LoadPortSet></LoadPortSet>  
//                                                        <DiscPortSet></DiscPortSet> 
//                                                            <ProfitType>0</ProfitType>  
//                                                                <GroupString>业务员</GroupString> 
//                                                                    <AgentSet>船东</AgentSet>  
//                                                                        <DateType>0</DateType> 
//                                                                            </root>";

             if (XMLCondition == null || XMLCondition.Trim() == string.Empty)
                 return string.Empty;

            XmlTextReader xmlreader = new XmlTextReader(new StringReader(XMLCondition));
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlreader);

            return dataSet.Tables[0].Rows[0][Key] == null ? string.Empty : dataSet.Tables[0].Rows[0][Key].ToString();

        }

        /// <summary>
        /// 根据传入的参数替换条件的值
        /// </summary>
        /// <returns></returns>
        public static string SetConditionForValue(string XMLCondition, string NodeKey, string NodeValue)
        {        
            XmlTextReader xmlreader = new XmlTextReader(new StringReader(XMLCondition));
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlreader);
            dataSet.Tables[0].Rows[0].BeginEdit();
            if (dataSet.Tables[0].Columns.Contains(NodeKey))
            {
                foreach (DataRow drow in dataSet.Tables[0].Rows)
                {
                    drow[NodeKey] = NodeValue;
                }
            }
            dataSet.Tables[0].Rows[0].EndEdit();

            System.IO.StringWriter str = new System.IO.StringWriter();
            XmlTextWriter writer = new XmlTextWriter(str);
            writer.Formatting = Formatting.Indented;
            dataSet.WriteXml(writer);
            writer.Close();


            //string XMLText = "<XML>my data</XML>";

            //StringReader strReader = new  StringReader(XMLText);

            //XmlTextReader reader = new  XmlTextReader(strReader);
            //string id = reader.GetAttribute("XML");
            return str.ToString();
        }


        /// <summary>
        /// 根据传入的参数替换条件的值
        /// </summary>
        /// <returns></returns>
        public static string GetConditionForValue(string XMLCondition, string Keys, string Values)
        {
             
            
            string[] keys = Keys.Split(new string[]{","},StringSplitOptions.None);
            string[] values = Values.Split(new string[]{"!"},StringSplitOptions.None);
            

            Hashtable htab = new Hashtable();
            htab.Add("业务员", "SalesSet");
            htab.Add("客户", "ConsignerSet");
            htab.Add("承运人", "ShipAgentSet");
            htab.Add("代理人", "AgentSet");
            htab.Add("航线", "ShippingLineSet");
            htab.Add("船东", "CarrierSet");
            htab.Add("业务发生地", "CompanyID");
            htab.Add("业务所属部门", "SalesDepartmentID");
            htab.Add("组织结构", "StructNodeId");
            htab.Add("揽货方式", "SalesType");
            htab.Add("装货港", "LoadPortSet");
            htab.Add("交货地", "DestPortSet");
            htab.Add("航空公司", "CarrierSet");
            htab.Add("开始时间", "ETD_Beginning_Date");
            htab.Add("结束时间", "ETD_Ending_Date");
            htab.Add("合约号", "SCNO");

            htab.Add("Sales", "SalesSet");
            htab.Add("Customer", "ConsignerSet");
            htab.Add("Carrier", "ShipAgentSet");
            htab.Add("Agent", "AgentSet");
            htab.Add("Line", "ShippingLineSet");
            htab.Add("Ship Owner", "CarrierSet");
            htab.Add("Company", "CompanyID");
            htab.Add("Sales Department", "SalesDepartmentID");
            htab.Add("StructNode", "StructNodeId");
            htab.Add("Sales Type", "SalesType");
            htab.Add("POL", "LoadPortSet");
            htab.Add("Destination", "DestPortSet");
            htab.Add("Beginning Date", "ETD_Beginning_Date");
            htab.Add("Ending Date", "ETD_Ending_Date");
            htab.Add("S/C No.", "SCNO");
            htab.Add("Age", "Age");
            htab.Add("GroupBy", "GroupBy");
            htab.Add("GroupByID", "GroupByID");
            htab.Add("GroupByName", "GroupByName");
            
            if ( !XMLCondition.Contains("CompanyID") )
            {
               XMLCondition =  XMLCondition.Replace("</root>", " <CompanyID></CompanyID> </root>");
            }

            if (!XMLCondition.Contains("SalesDepartmentID"))
            {
                XMLCondition = XMLCondition.Replace("</root>", " <SalesDepartmentID></SalesDepartmentID> </root>");
            }
               

            XmlTextReader xmlreader = new XmlTextReader(new StringReader(XMLCondition));
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlreader);
            dataSet.Tables[0].Rows[0].BeginEdit();
            int j = 0;
            int min = 0;
            if (keys.Length <= 3)
            {
                min = 0;
            }
            else
            {
                min = keys.Length - 3;
            }
            for (int i = min; i < keys.Length; i++)
            {
                string key = keys[i];
                if (key.Trim() != string.Empty)
                {
                    if ((key.Trim() != "开始时间" || key.Trim() != "Beginning Date")
                        && (key.Trim() != "结束时间" || key.Trim() != "Ending Date")
                        && (values[j].Trim() == string.Empty
                    || values[j].Trim() == "''"
                    || values[j].Trim() == "'"))//处理数据库中为空的值
                    {
                        values[j] = "'00000000-0000-0000-0000-000000000000'";
                    }                    
                    dataSet.Tables[0].Rows[0][htab[key].ToString()] = values[j].Trim();                 
                    
                }
                dataSet.Tables[0].Rows[0].EndEdit();
                j++;
            }

            System.IO.StringWriter str = new System.IO.StringWriter();
            XmlTextWriter writer = new XmlTextWriter(str);
            writer.Formatting = Formatting.Indented;
            dataSet.WriteXml(writer);
            writer.Close();


            //string XMLText = "<XML>my data</XML>";

            //StringReader strReader = new  StringReader(XMLText);

            //XmlTextReader reader = new  XmlTextReader(strReader);
            //string id = reader.GetAttribute("XML");
            return str.ToString();
        }

        static DataSet GetDataset(string strFilePath)
        {
            if (!File.Exists(strFilePath))
            {
                return null;
            }
            string strFolderPath = Path.GetDirectoryName(strFilePath);
            string strCSVFile = Path.GetFileName(strFilePath);

            DataSet ds = null;
            string strConnection = "Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + strFolderPath + ";Extensions=asc,csv,tab,txt;Persist Security Info=False";
            try
            {

                using (OdbcConnection conn = new OdbcConnection(strConnection.Trim()))
                {
                    conn.Open();
                    string strSql = "select * from [" + strCSVFile + "]";
                    OdbcDataAdapter odbcDAdapter = new OdbcDataAdapter(strSql, conn);
                    ds = new DataSet();
                    odbcDAdapter.Fill(ds, "table");
                    conn.Close();
                    
                    
                }
                return ds;
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }




        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            GetDataset(@"C:\Documents and Settings\Administrator\桌面\V_05.TXT");
            
            //string XMLText = "<XML>my data</XML>";
            foreach (DataRow dr in SqlDataSourceEnumerator.Instance.GetDataSources().Rows)
            {
                string servername = dr["ServerName"].ToString();
            }
            //StringReader strReader = new StringReader(XMLText);

            //XmlTextReader reader = new XmlTextReader(strReader);
            //string id = reader.GetAttribute("XML");

            //string xmlStr = string.Format(@"<root> <StructType>0</StructType>  <StructNodeId>B13FAC2D-8250-4990-A622-5ECA00D3A030</StructNodeId>  <ETD_Beginning_Date>2007-01-01</ETD_Beginning_Date>  <ETD_Ending_Date>2007-01-11</ETD_Ending_Date>  <SalesType>3</SalesType>  <SalesSet></SalesSet>  <ConsignerSet></ConsignerSet>  <ShipAgentSet></ShipAgentSet>  <CarrierSet></CarrierSet>  <ShippingLineSet></ShippingLineSet>  <JobType>0,1,2,3,4,5,6,7,8,9</JobType>  <LoadPortSet></LoadPortSet>  <DiscPortSet></DiscPortSet>  <ProfitType>0</ProfitType>  <GroupString>业务员</GroupString>  <AgentSet>船东</AgentSet>  <DateType>0</DateType>  </root>");
            //XmlTextReader xmlreader = new XmlTextReader(new StringReader(xmlStr));
            //DataSet dataSet = new DataSet();
            //dataSet.ReadXml(xmlreader);
            ////new System.IO.MemoryStream(System.Text.Encoding.Default.GetBytes(xmlStr))); 

            //System.IO.StringWriter str = new System.IO.StringWriter();
            //XmlTextWriter writer = new XmlTextWriter(str);
            //writer.Formatting = Formatting.Indented;
            //dataSet.WriteXml(writer);
            //writer.Close();
            //Tools.GetDateForInputStr("200710", false, true);
            string xml = " <root>   <StructType>0</StructType>  <StructNodeId>41d7d3fe-183a-41cd-a725-eb6f728541ec</StructNodeId>  <ETD_Beginning_Date>2008-09-01</ETD_Beginning_Date>  <ETD_Ending_Date>2008-09-08</ETD_Ending_Date>  <SalesType>4</SalesType>  <SalesSet />  <ConsignerSet />  <ShipAgentSet />  <CarrierSet />  <ShippingLineSet />  <JobType>0,1,2,3</JobType>  <LoadPortSet />  <DiscPortSet />  <ProfitType>0</ProfitType>  <GroupString>交货地</GroupString>  <AgentSet />  <DateType>0</DateType></root>";
            LongWin.DataWarehouseReport.ToolsMethod.Tools.SetConditionForValue(xml, "Age", "4");
           LongWin.DataWarehouseReport.ToolsMethod.Tools.GetConditionForValue(xml
, ",,,Age,GroupBy,GroupByID,GroupByName"
, "4!90!be761af9-af01-4efa-a0f1-6d498e618df2!Customer:珠海市维佳联运国际货运代理有限公司深圳分公司!");

        }
    }
}
