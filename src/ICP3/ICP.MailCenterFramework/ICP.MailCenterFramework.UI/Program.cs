/**
 *  创建时间:2014-07-16
 *  创建人:Joabwang    
 *  描  述:加载服务基类
 **/
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Windows.Forms;

namespace ICP.MailCenterFramework.UI
{
    public static class Program
    {
        private static Start start = null;

        public static void Main()
        {
            Stopwatch stopwatch = null;
            ServiceHost host;
            try
            {
                AddInConfig.Current.SetValue("PluginInstalled", "True");
                stopwatch = StopwatchHelper.StartStopwatch();
                InitializeEnvironment();
                //获取到主程序路径时写入启用ICP智能面板记录，统计ICP智能面板使用记录
                if (!string.IsNullOrEmpty(LocalData.MainPath))
                {
                    #region 从ICP获取数据及其记录日志
                    IDataCacheOperationService DataCacheOperationService = null;
                    try
                    {
                        DataCacheOperationService = ServiceClient.GetService<IDataCacheOperationService>();
                    }
                    catch (Exception ex)
                    {
                        ToolUtility.WriteLog("Program DataCacheOperationService", ex);
                    }
                    if (DataCacheOperationService != null)
                    {
                        //获取联系人信息
                        if (HelpMailStore.TableOperationContact != null)
                            HelpMailStore.TableOperationContact.Clear();
                        HelpMailStore.TableOperationContact = DataCacheUtility.ConvertTableToOperationContactInfo(DataCacheOperationService.GetAllContact());
                        //获取关联信息
                        if (HelpMailStore.TableMessageRelation != null)
                            HelpMailStore.TableMessageRelation.Clear();
                        HelpMailStore.TableMessageRelation = MessageUtility.ConvertDataTableToOperationMessageRelation(DataCacheOperationService.GetAllOperationMessageRelation());
                        //获取业务信息
                        if (HelpMailStore.TableBusiness != null)
                            HelpMailStore.TableBusiness.Clear();
                        HelpMailStore.TableBusiness = DataCacheOperationService.GetAllOperationViewInfo();
                        //记录启动日志

                        MethodBase methodother = MethodBase.GetCurrentMethod();
                        stopwatch.Stop();
                        DataCacheOperationService.WriteStopwatchLog(methodother.DeclaringType.FullName,"PLUGIN", "Enable ICP Smart Panel",stopwatch.ElapsedMilliseconds.ToString());

                        host = new ServiceHost(typeof(OutlookOperateService));
                        host.Open();
                        DataCacheOperationService.WriteStopwatchLog(methodother.DeclaringType.FullName, "PLUGIN", "ICP Smart Panel Service has started", stopwatch.ElapsedMilliseconds.ToString());

                    } 
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("Program:Main", ex);
                ClientHelper.ReleaseWaitHandle(ClientHelper.GetAppSettingValue(ClientConstants.EmailCenterNameKey));
            }
            finally
            {
                if (stopwatch!=null)
                    stopwatch = null;
            }
        }

        private static void SetLoginUserInfo()
        {
            try
            {
                ILoginUserInfoService loginUserInfoService = ServiceClient.GetService<ILoginUserInfoService>();
                LoginUserInfo userInfo = loginUserInfoService.Get();
                LocalData.AdministratorId = userInfo.AdministratorId;
                LocalData.ClientId = userInfo.ClientId;
                LocalData.CultureName = userInfo.CultureName;
                LocalData.EmailHost = userInfo.EmailHost;
                LocalData.EnableCustomDataGrid = userInfo.EnableCustomDataGrid;
                LocalData.Height = userInfo.Height;
                LocalData.IsDesignMode = userInfo.IsDesignMode;
                LocalData.PortalType = userInfo.PortalType;
                LocalData.SessionId = userInfo.SessionId;
                LocalData.SkinName = userInfo.SkinName;
                LocalData.SystemConfigInfoList = userInfo.SystemConfigInfoList;
                LocalData.SystemNGenVersionNo = userInfo.SystemNGenVersionNo;
                LocalData.SystemUpdateVersionNo = userInfo.SystemUpdateVersionNo;
                LocalData.SystemVersionNo = userInfo.SystemVersionNo;
                LocalData.UserInfo = userInfo.UserInfo;
                LocalData.DataSyncFinished = userInfo.DataSyncFinished;
                LocalData.MainPath = userInfo.MainPath;
                LocalData.NeedBackUpMail = bool.Parse(AddInConfig.Current.GetValue("NeedBackUpMail", "False"));
                LocalCommonServices.PermissionService = userInfo.PermissionPackage;
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("SetLoginUserInfo", ex);
                if (start == null)
                {
                    start = new Start();
                }
                start.PluginStart();
            }
        }
        private static void InitializeEnvironment()
        {
            SetLoginUserInfo();
            SavePlugInfomation();
            ClientHelper.SetApplicationContext();
        }

        /// <summary>
        /// 保存插件信息
        /// </summary>
        private static void SavePlugInfomation()
        {
            #region 1.插件路径写入注册表
            try
            {
                
                bool needTip = false;
                string domainDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string strPath = string.Empty;
                string strServerAddInNo = string.Empty;
                //用户是否需要安装插件：判断用户是否已安装插件
                bool installation = !string.IsNullOrEmpty(AddInConfig.Current.GetValue("Installation"))
                    && bool.Parse(AddInConfig.Current.GetValue("Installation"));

                //插件路径：获取到的已写入路径为空或与当前路径不相同才写入注册表，防止注册表多次写入
                strPath = "" + AddInConfig.Current.GetValue("OutLookAddInPath"); ;
                if (string.IsNullOrEmpty(strPath) || !strPath.Equals(domainDirectory))
                {
                    AddInConfig.Current.SetValue("OutLookAddInPath", domainDirectory);
                    needTip = true;
                }
                //插件版本：获取到的已写入版本号为空或与服务器版本号不相同才写入注册表，防止注册表多次写入
                if (LocalData.SystemConfigInfoList != null && LocalData.SystemConfigInfoList.Count > 0)
                {
                    var systemConfigInfoList = LocalData.SystemConfigInfoList;
                    //服务器版本号
                    var outlookAddInNo =
                        systemConfigInfoList.FirstOrDefault(n => n.Key == "OutlookAddInNO");
                    strServerAddInNo = outlookAddInNo.Value;
                }
                if (!string.IsNullOrEmpty(strServerAddInNo))
                {
                    string strLocalAddInNo = "" + AddInConfig.Current.GetValue("OutlookAddInNO");
                    if (string.IsNullOrEmpty(strLocalAddInNo)) //初始化版本号
                    {
                        AddInConfig.Current.SetValue("OutlookAddInNO", "0");
                        strLocalAddInNo = "0";
                    }
                    if (!strLocalAddInNo.Equals(strServerAddInNo))
                    {
                        AddInConfig.Current.SetValue("OutlookAddInUpdate", "True");
                        needTip = true;
                    }
                }
                AddInConfig.Dispose();

                //必须安装插件用户才提示
                if (installation && needTip)
                {
                    DialogResult drResult = MessageBox.Show(OutlookUtility.IsEnglish ?
                        "You need to restart ICP and Outlook."
                        : "请重启 ICP 和 Outlook"
                            , OutlookUtility.IsEnglish ? "Tip" : "提示"
                            , MessageBoxButtons.OK
                            , MessageBoxIcon.Information);
                    //强制退出ICP和Outlook保持插件dll最新
                    ClientHelper.NormalExitOutlook();
                    ClientHelper.NormalExitICP();
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("SavePlugInfomation", ex);
            }
            #endregion
        }

    }
}
