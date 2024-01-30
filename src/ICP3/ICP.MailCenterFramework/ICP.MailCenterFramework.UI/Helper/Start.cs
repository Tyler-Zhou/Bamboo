/**
 *  创建时间:2014-07-09
 *  创建人:Joabwang    
 *  描  述:打开OotLook时判断是否需要打开ICPMain.exe文件进行登录
 **/

using System;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Win32;

namespace ICP.MailCenterFramework.UI
{
    public class Start
    {
        [DllImport("User32")]
        private static extern int SetForegroundWindow(IntPtr hwnd);
        /// <summary>
        /// 判断当前是否需要进行登录操作
        /// </summary>
        /// <returns>是否需要登录</returns>
        public bool IsLogin()
        {
            bool IsLogin = false;
            DataSet dataSet = new DataSet();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "server=CNdb.cityocean.com,3344;database=ICP3;user id=ICP3;Pwd=LONGWINicp@1124;integrated security=true;Trusted_Connection=false;Connect Timeout=1;Application Name=ICP";
            if (sqlConnection.State == ConnectionState.Closed)
            {
                //如果连接的状态是关闭的话  
                sqlConnection.Open();//打开连接  
            }
            string sql = "Select IsLogin  from sm.Login";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
            sqlDataAdapter.Fill(dataSet);
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();//关闭连接
                sqlConnection.Dispose();//释放资源
            }
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                IsLogin = bool.Parse(dataSet.Tables[0].Rows[0]["IsLogin"].ToString());
            }
            HelpMailStore.IsNeedLoginICP = IsLogin;
            return IsLogin;
        }



        /// <summary>
        /// 是否在启动OutLook时，需要启动ICPMain.exe文件
        /// </summary>
        public void IcpMainStart()
        {
            if (IsLogin())
            {
                //进程中是否包含ICPMain.exe 如无启动ICPMain.exe
                if (ClientHelper.IsAppExists("ICPMain.exe") == false)
                {
                    //IcpmainLogin();
                    MessageBox.Show(LocalData.IsEnglish ? "You must launch ICP first!" : "您必须先启动ICP!");
                    ExitOutLook();
                }
            }
        }
        public void PluginStart()
        {
            LocalData.IsplugIn= true;
            HelpMailStore.IsNeedLoginICP = true;
            //IcpMainStart();
            //FolderWrapper.RegisterEvent();
        }

        /// <summary>
        /// Icp.Main打开登录
        /// </summary>
        /// <returns></returns>
        public void IcpmainLogin()
        {
            string strKeyName = string.Empty;
            string softPath = @"Software\ICP";
            RegistryKey regKey = Registry.CurrentUser;
            RegistryKey regSubKey = regKey.OpenSubKey(softPath, true);
            if (regSubKey != null)
            {
                object objResult = regSubKey.GetValue("Path");
                if (!string.IsNullOrEmpty(objResult.ToString()))
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = objResult.ToString() + "\\ICPMain.exe";
                    proc.Start();
                    //得到句柄
                    IntPtr hWnd = proc.MainWindowHandle;
                    //窗口置前
                    SetForegroundWindow(hWnd);
                }
            }
            else
            {
                MessageBox.Show(LocalData.IsEnglish ? "You must launch ICP first!" : "您必须先启动ICP!");
                ExitOutLook();
            }
        }
        /// <summary>
        /// 退出Outlook
        /// </summary>
        public void ExitOutLook()
        {
            if (ClientHelper.IsAppExists("OutLook"))
            {
                // 退出的时候关闭
                Process[] processe = Process.GetProcessesByName("OutLook");
                processe[0].CloseMainWindow();
            }
        }
    }
}
