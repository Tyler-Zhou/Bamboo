using System.Data.Odbc;
using System.Data;
using System.Data.OleDb;

namespace ICP.FAM.UI.Comm
{
    public abstract class OdbcHelper
    {
        //private static string odbcConStr;

        //public static string OdbcConStr
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(odbcConStr))
        //        {
        //            string fileName = Application.StartupPath + @"\销项发票明细.db";
        //            dbConnStr = "Driver={Microsoft Paradox Driver (*.db )}; DriverID=538; Fil=Paradox 5.X; "
        //            + " DefaultDir=" + fileName.Substring(0, startPosition + 1)
        //            + " ; Dbq=" + fileName.Substring(0, startPosition + 1) + "; CollatingSequence=ASCII; PWD=1FFKEC123Q4C26G;";
        //        }
        //        return dbConnStr;
        //    }
        //}

        public static string GetDBConnString(string dbFileName)
        {
            int startPosition = dbFileName.LastIndexOf("\\");
            //return "Driver={Microsoft Paradox Driver (*.db )}; DriverID=538; Fil=Paradox 5.X; "
            //    + " DefaultDir=" + dbFileName.Substring(0, startPosition + 1)
            //    + " ; Dbq=" + dbFileName.Substring(0, startPosition + 1) + "; CollatingSequence=ASCII; PWD=1FFKEC123Q4C26G;";

            //return "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + dbFileName.Substring(0, startPosition + 1)
            //    + "; Extended Properties=Paradox 5.x; Jet OLEDB:Database Password=1FFKEC123Q4C26G ";//Persist SecurityInfo=False;";

            return "Provider=Microsoft.Ace.OleDb.12.0; Data Source="
                + dbFileName.Substring(0, startPosition + 1) + "; Extended Properties='Paradox 5.x; HDR=Yes; IMEX=1'; Jet OLEDB:Database Password=1FFKEC123Q4C26G; ";//Persist SecurityInfo=False;";
        }

        public static DataSet ExecuteDataSet(string connectionString,string queryString)
        {
            //////OdbcConnection odbcConn = new OdbcConnection(connectionString);
            ////ADODB.Connection adodbConn = new ADODB.Connection();
            ////adodbConn.ConnectionString = connectionString;
            ////DataSet ds=new DataSet();
            ////try
            ////{
                
            ////    //odbcConn.Open();
            ////    //OdbcDataAdapter adapter = new OdbcDataAdapter(queryString, odbcConn);
            ////    //adapter.Fill(ds);
            ////    object obj;
            ////    adodbConn.Open(connectionString, "ASCII", "1FFKEC123Q4C26G", 0);
            ////    //adodbConn.Execute(queryString, out ds, -1);
            ////    ADODB.Recordset record = adodbConn.Execute(queryString, out obj, 0);
            ////    return ds;
            ////}
            ////catch
            ////{
            ////    throw;
            ////}
            ////finally
            ////{
            ////    //odbcConn.Close();
            ////    adodbConn.Close();
            ////}
            OleDbConnection conn = new OleDbConnection(connectionString);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(queryString, conn);
                adapter.Fill(ds);
                return ds;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }


        }

        public static int ExecuteNonQuery(string connectionString,string queryString)
        {
            //OdbcCommand command = new OdbcCommand(queryString);

            //using (OdbcConnection odbcConn = new OdbcConnection(connectionString))
            //{
            //    try
            //    {
            //        command.Connection = odbcConn;
            //        odbcConn.Open();
            //        return command.ExecuteNonQuery();
            //    }
            //    catch
            //    {
            //        throw;
            //    }
            //    finally
            //    {
            //        odbcConn.Close();
            //    }
            //}
            
            

            using (OdbcConnection odbcConn = new OdbcConnection(connectionString))
            {
                try
                {
                    OdbcCommand myCommand = new OdbcCommand(queryString, odbcConn);
                    myCommand.Connection.Open();
                    return myCommand.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    odbcConn.Close();
                }
            }

        }
    }
}
