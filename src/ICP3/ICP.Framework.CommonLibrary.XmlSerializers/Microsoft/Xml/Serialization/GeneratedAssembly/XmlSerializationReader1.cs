namespace Microsoft.Xml.Serialization.GeneratedAssembly
{
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Server;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Xml;
    using System.Xml.Serialization;

    public class XmlSerializationReader1 : XmlSerializationReader
    {
        private string id1_UnloadSession;
        private string id10_ChangePassword;
        private string id100_AllowRoles;
        private string id101_AuthDataInfo;
        private string id102_SessionId;
        private string id103_LoginUserInfo;
        private string id104_CultureName;
        private string id105_ApplicationType;
        private string id106_AdministratorId;
        private string id107_ClientId;
        private string id108_IsEnglish;
        private string id109_DataSyncFinished;
        private string id11_oldPassword;
        private string id110_EnableCustomDataGrid;
        private string id111_SkinName;
        private string id112_SystemVersionNo;
        private string id113_SystemNGenVersionNo;
        private string id114_SystemUpdateVersionNo;
        private string id115_EmailHost;
        private string id116_IsDesignMode;
        private string id117_SysFontName;
        private string id118_MenuFontName;
        private string id119_UserInfo;
        private string id12_newPassword;
        private string id120_PortalType;
        private string id121_SystemConfigInfoList;
        private string id122_SystemConfigurationInfo;
        private string id123_Height;
        private string id124_Key;
        private string id125_Value;
        private string id126_Value2;
        private string id127_Value3;
        private string id128_UpdateTime;
        private string id129_LoginInfo;
        private string id13_ChangePasswordResponse;
        private string id130_LoginID;
        private string id131_LoginName;
        private string id132_DefaultCompanyID;
        private string id133_DefaultCompanyName;
        private string id134_DefaultDepartmentID;
        private string id135_DefaultDepartmentName;
        private string id136_Password;
        private string id137_MacAddress;
        private string id138_LocalIpAddress;
        private string id139_PublicIpAddress;
        private string id14_ValidateUser;
        private string id140_UserName;
        private string id141_UserOrganizationList;
        private string id142_LocalOrganizationInfo;
        private string id143_EmailAddress;
        private string id144_Code;
        private string id145_IsDefault;
        private string id146_CShortName;
        private string id147_EShortName;
        private string id148_FullName;
        private string id149_IsValid;
        private string id15_password;
        private string id150_ParentID;
        private string id16_networkAdapter;
        private string id17_ValidateUserResponse;
        private string id18_ValidateUserResult;
        private string id19_GetAllFunctionsFromUser;
        private string id2_httptempuriorg;
        private string id20_user;
        private string id21_Item;
        private string id22_GetAllFunctionsFromUserResult;
        private string id23_string;
        private string id24_Log;
        private string id25_userId;
        private string id26_code;
        private string id27_ip;
        private string id28_netWorkNo;
        private string id29_loginTime;
        private string id3_sessionID;
        private string id30_description;
        private string id31_LogResponse;
        private string id32_GetRolesForUser;
        private string id33_GetRolesForUserResponse;
        private string id34_GetRolesForUserResult;
        private string id35_Save;
        private string id36_userName;
        private string id37_sessionId;
        private string id38_screenCapture;
        private string id39_createTime;
        private string id4_loginoutTime;
        private string id40_Get;
        private string id41_GetResponse;
        private string id42_GetResult;
        private string id43_Subscribe;
        private string id44_eventOperation;
        private string id45_userDepartmentIds;
        private string id46_guid;
        private string id47_clientId;
        private string id48_Unsubscribe;
        private string id49_AuthUser;
        private string id5_UnloadSessionResponse;
        private string id50_usercode;
        private string id51_macAddress;
        private string id52_AuthUserResponse;
        private string id53_AuthUserResult;
        private string id54_GetPermissionPackage;
        private string id55_userid;
        private string id56_GetPermissionPackageResponse;
        private string id57_GetPermissionPackageResult;
        private string id58_SynchronizeToDatabase;
        private string id59_SynchronizeToDatabaseResponse;
        private string id6_GetUserDisplayName;
        private string id60_AddWithContextParameter;
        private string id61_operationTime;
        private string id62_content;
        private string id63_Add;
        private string id64_internetIP;
        private string id65_intarnetIP;
        private string id66_assemblyNames;
        private string id67_functionName;
        private string id68_isEnglish;
        private string id69_BatchAdd;
        private string id7_username;
        private string id70_dt;
        private string id71_GetAllServices;
        private string id72_GetAllServicesResponse;
        private string id73_GetAllServicesResult;
        private string id74_ServiceInfo;
        private string id75_PermissionPackage;
        private string id76_AllowActions;
        private string id77_BuildTime;
        private string id78_UserId;
        private string id79_Modules;
        private string id8_GetUserDisplayNameResponse;
        private string id80_Module;
        private string id81_Commands;
        private string id82_UICommand;
        private string id83_RoleList;
        private string id84_SimpleRoleInfo;
        private string id85_ID;
        private string id86_CName;
        private string id87_EName;
        private string id88_CompanyID;
        private string id89_Text;
        private string id9_GetUserDisplayNameResult;
        private string id90_Name;
        private string id91_Site;
        private string id92_RegisterSite;
        private string id93_Type;
        private string id94_Image;
        private string id95_AssemblyName;
        private string id96_HasPermission;
        private string id97_OrderNo;
        private string id98_Assembly;
        private string id99_UpdateLocation;
        private string id200_MainPath;
        protected override void InitCallbacks()
        {
        }

        protected override void InitIDs()
        {
            this.id92_RegisterSite = base.Reader.NameTable.Add("RegisterSite");
            this.id142_LocalOrganizationInfo = base.Reader.NameTable.Add("LocalOrganizationInfo");
            this.id93_Type = base.Reader.NameTable.Add("Type");
            this.id87_EName = base.Reader.NameTable.Add("EName");
            this.id146_CShortName = base.Reader.NameTable.Add("CShortName");
            this.id144_Code = base.Reader.NameTable.Add("Code");
            this.id63_Add = base.Reader.NameTable.Add("Add");
            this.id84_SimpleRoleInfo = base.Reader.NameTable.Add("SimpleRoleInfo");
            this.id6_GetUserDisplayName = base.Reader.NameTable.Add("GetUserDisplayName");
            this.id138_LocalIpAddress = base.Reader.NameTable.Add("LocalIpAddress");
            this.id95_AssemblyName = base.Reader.NameTable.Add("AssemblyName");
            this.id56_GetPermissionPackageResponse = base.Reader.NameTable.Add("GetPermissionPackageResponse");
            this.id98_Assembly = base.Reader.NameTable.Add("Assembly");
            this.id143_EmailAddress = base.Reader.NameTable.Add("EmailAddress");
            this.id23_string = base.Reader.NameTable.Add("string");
            this.id64_internetIP = base.Reader.NameTable.Add("internetIP");
            this.id10_ChangePassword = base.Reader.NameTable.Add("ChangePassword");
            this.id108_IsEnglish = base.Reader.NameTable.Add("IsEnglish");
            this.id13_ChangePasswordResponse = base.Reader.NameTable.Add("ChangePasswordResponse");
            this.id131_LoginName = base.Reader.NameTable.Add("LoginName");
            this.id140_UserName = base.Reader.NameTable.Add("UserName");
            this.id137_MacAddress = base.Reader.NameTable.Add("MacAddress");
            this.id8_GetUserDisplayNameResponse = base.Reader.NameTable.Add("GetUserDisplayNameResponse");
            this.id45_userDepartmentIds = base.Reader.NameTable.Add("userDepartmentIds");
            this.id14_ValidateUser = base.Reader.NameTable.Add("ValidateUser");
            this.id29_loginTime = base.Reader.NameTable.Add("loginTime");
            this.id81_Commands = base.Reader.NameTable.Add("Commands");
            this.id12_newPassword = base.Reader.NameTable.Add("newPassword");
            this.id88_CompanyID = base.Reader.NameTable.Add("CompanyID");
            this.id90_Name = base.Reader.NameTable.Add("Name");
            this.id125_Value = base.Reader.NameTable.Add("Value");
            this.id55_userid = base.Reader.NameTable.Add("userid");
            this.id46_guid = base.Reader.NameTable.Add("guid");
            this.id20_user = base.Reader.NameTable.Add("user");
            this.id134_DefaultDepartmentID = base.Reader.NameTable.Add("DefaultDepartmentID");
            this.id38_screenCapture = base.Reader.NameTable.Add("screenCapture");
            this.id51_macAddress = base.Reader.NameTable.Add("macAddress");
            this.id7_username = base.Reader.NameTable.Add("username");
            this.id48_Unsubscribe = base.Reader.NameTable.Add("Unsubscribe");
            this.id17_ValidateUserResponse = base.Reader.NameTable.Add("ValidateUserResponse");
            this.id100_AllowRoles = base.Reader.NameTable.Add("AllowRoles");
            this.id32_GetRolesForUser = base.Reader.NameTable.Add("GetRolesForUser");
            this.id54_GetPermissionPackage = base.Reader.NameTable.Add("GetPermissionPackage");
            this.id2_httptempuriorg = base.Reader.NameTable.Add("http://tempuri.org/");
            this.id1_UnloadSession = base.Reader.NameTable.Add("UnloadSession");
            this.id52_AuthUserResponse = base.Reader.NameTable.Add("AuthUserResponse");
            this.id44_eventOperation = base.Reader.NameTable.Add("eventOperation");
            this.id49_AuthUser = base.Reader.NameTable.Add("AuthUser");
            this.id82_UICommand = base.Reader.NameTable.Add("UICommand");
            this.id124_Key = base.Reader.NameTable.Add("Key");
            this.id128_UpdateTime = base.Reader.NameTable.Add("UpdateTime");
            this.id111_SkinName = base.Reader.NameTable.Add("SkinName");
            this.id61_operationTime = base.Reader.NameTable.Add("operationTime");
            this.id123_Height = base.Reader.NameTable.Add("Height");
            this.id101_AuthDataInfo = base.Reader.NameTable.Add("AuthDataInfo");
            this.id22_GetAllFunctionsFromUserResult = base.Reader.NameTable.Add("GetAllFunctionsFromUserResult");
            this.id34_GetRolesForUserResult = base.Reader.NameTable.Add("GetRolesForUserResult");
            this.id21_Item = base.Reader.NameTable.Add("GetAllFunctionsFromUserResponse");
            this.id145_IsDefault = base.Reader.NameTable.Add("IsDefault");
            this.id68_isEnglish = base.Reader.NameTable.Add("isEnglish");
            this.id39_createTime = base.Reader.NameTable.Add("createTime");
            this.id85_ID = base.Reader.NameTable.Add("ID");
            this.id18_ValidateUserResult = base.Reader.NameTable.Add("ValidateUserResult");
            this.id133_DefaultCompanyName = base.Reader.NameTable.Add("DefaultCompanyName");
            this.id57_GetPermissionPackageResult = base.Reader.NameTable.Add("GetPermissionPackageResult");
            this.id110_EnableCustomDataGrid = base.Reader.NameTable.Add("EnableCustomDataGrid");
            this.id116_IsDesignMode = base.Reader.NameTable.Add("IsDesignMode");
            this.id129_LoginInfo = base.Reader.NameTable.Add("LoginInfo");
            this.id60_AddWithContextParameter = base.Reader.NameTable.Add("AddWithContextParameter");
            this.id66_assemblyNames = base.Reader.NameTable.Add("assemblyNames");
            this.id115_EmailHost = base.Reader.NameTable.Add("EmailHost");
            this.id59_SynchronizeToDatabaseResponse = base.Reader.NameTable.Add("SynchronizeToDatabaseResponse");
            this.id112_SystemVersionNo = base.Reader.NameTable.Add("SystemVersionNo");
            this.id73_GetAllServicesResult = base.Reader.NameTable.Add("GetAllServicesResult");
            this.id103_LoginUserInfo = base.Reader.NameTable.Add("LoginUserInfo");
            this.id41_GetResponse = base.Reader.NameTable.Add("GetResponse");
            this.id5_UnloadSessionResponse = base.Reader.NameTable.Add("UnloadSessionResponse");
            this.id77_BuildTime = base.Reader.NameTable.Add("BuildTime");
            this.id40_Get = base.Reader.NameTable.Add("Get");
            this.id139_PublicIpAddress = base.Reader.NameTable.Add("PublicIpAddress");
            this.id11_oldPassword = base.Reader.NameTable.Add("oldPassword");
            this.id16_networkAdapter = base.Reader.NameTable.Add("networkAdapter");
            this.id72_GetAllServicesResponse = base.Reader.NameTable.Add("GetAllServicesResponse");
            this.id76_AllowActions = base.Reader.NameTable.Add("AllowActions");
            this.id141_UserOrganizationList = base.Reader.NameTable.Add("UserOrganizationList");
            this.id102_SessionId = base.Reader.NameTable.Add("SessionId");
            this.id130_LoginID = base.Reader.NameTable.Add("LoginID");
            this.id104_CultureName = base.Reader.NameTable.Add("CultureName");
            this.id4_loginoutTime = base.Reader.NameTable.Add("loginoutTime");
            this.id107_ClientId = base.Reader.NameTable.Add("ClientId");
            this.id36_userName = base.Reader.NameTable.Add("userName");
            this.id89_Text = base.Reader.NameTable.Add("Text");
            this.id136_Password = base.Reader.NameTable.Add("Password");
            this.id83_RoleList = base.Reader.NameTable.Add("RoleList");
            this.id86_CName = base.Reader.NameTable.Add("CName");
            this.id65_intarnetIP = base.Reader.NameTable.Add("intarnetIP");
            this.id69_BatchAdd = base.Reader.NameTable.Add("BatchAdd");
            this.id109_DataSyncFinished = base.Reader.NameTable.Add("DataSyncFinished");
            this.id147_EShortName = base.Reader.NameTable.Add("EShortName");
            this.id43_Subscribe = base.Reader.NameTable.Add("Subscribe");
            this.id117_SysFontName = base.Reader.NameTable.Add("SysFontName");
            this.id30_description = base.Reader.NameTable.Add("description");
            this.id9_GetUserDisplayNameResult = base.Reader.NameTable.Add("GetUserDisplayNameResult");
            this.id74_ServiceInfo = base.Reader.NameTable.Add("ServiceInfo");
            this.id62_content = base.Reader.NameTable.Add("content");
            this.id99_UpdateLocation = base.Reader.NameTable.Add("UpdateLocation");
            this.id135_DefaultDepartmentName = base.Reader.NameTable.Add("DefaultDepartmentName");
            this.id120_PortalType = base.Reader.NameTable.Add("PortalType");
            this.id132_DefaultCompanyID = base.Reader.NameTable.Add("DefaultCompanyID");
            this.id80_Module = base.Reader.NameTable.Add("Module");
            this.id25_userId = base.Reader.NameTable.Add("userId");
            this.id28_netWorkNo = base.Reader.NameTable.Add("netWorkNo");
            this.id47_clientId = base.Reader.NameTable.Add("clientId");
            this.id53_AuthUserResult = base.Reader.NameTable.Add("AuthUserResult");
            this.id114_SystemUpdateVersionNo = base.Reader.NameTable.Add("SystemUpdateVersionNo");
            this.id15_password = base.Reader.NameTable.Add("password");
            this.id126_Value2 = base.Reader.NameTable.Add("Value2");
            this.id127_Value3 = base.Reader.NameTable.Add("Value3");
            this.id19_GetAllFunctionsFromUser = base.Reader.NameTable.Add("GetAllFunctionsFromUser");
            this.id149_IsValid = base.Reader.NameTable.Add("IsValid");
            this.id50_usercode = base.Reader.NameTable.Add("usercode");
            this.id96_HasPermission = base.Reader.NameTable.Add("HasPermission");
            this.id119_UserInfo = base.Reader.NameTable.Add("UserInfo");
            this.id106_AdministratorId = base.Reader.NameTable.Add("AdministratorId");
            this.id31_LogResponse = base.Reader.NameTable.Add("LogResponse");
            this.id118_MenuFontName = base.Reader.NameTable.Add("MenuFontName");
            this.id121_SystemConfigInfoList = base.Reader.NameTable.Add("SystemConfigInfoList");
            this.id3_sessionID = base.Reader.NameTable.Add("sessionID");
            this.id26_code = base.Reader.NameTable.Add("code");
            this.id79_Modules = base.Reader.NameTable.Add("Modules");
            this.id97_OrderNo = base.Reader.NameTable.Add("OrderNo");
            this.id71_GetAllServices = base.Reader.NameTable.Add("GetAllServices");
            this.id42_GetResult = base.Reader.NameTable.Add("GetResult");
            this.id122_SystemConfigurationInfo = base.Reader.NameTable.Add("SystemConfigurationInfo");
            this.id75_PermissionPackage = base.Reader.NameTable.Add("PermissionPackage");
            this.id70_dt = base.Reader.NameTable.Add("dt");
            this.id78_UserId = base.Reader.NameTable.Add("UserId");
            this.id148_FullName = base.Reader.NameTable.Add("FullName");
            this.id91_Site = base.Reader.NameTable.Add("Site");
            this.id37_sessionId = base.Reader.NameTable.Add("sessionId");
            this.id33_GetRolesForUserResponse = base.Reader.NameTable.Add("GetRolesForUserResponse");
            this.id105_ApplicationType = base.Reader.NameTable.Add("ApplicationType");
            this.id24_Log = base.Reader.NameTable.Add("Log");
            this.id58_SynchronizeToDatabase = base.Reader.NameTable.Add("SynchronizeToDatabase");
            this.id113_SystemNGenVersionNo = base.Reader.NameTable.Add("SystemNGenVersionNo");
            this.id35_Save = base.Reader.NameTable.Add("Save");
            this.id150_ParentID = base.Reader.NameTable.Add("ParentID");
            this.id94_Image = base.Reader.NameTable.Add("Image");
            this.id67_functionName = base.Reader.NameTable.Add("functionName");
            this.id27_ip = base.Reader.NameTable.Add("ip");
            this.id200_MainPath = base.Reader.NameTable.Add("MainPath");
        }

        private ApplicationType Read10_ApplicationType(string s)
        {
            switch (s)
            {
                case "ICP":
                    return ApplicationType.ICP;

                case "EmailCenter":
                    return ApplicationType.EmailCenter;
            }
            throw base.CreateUnknownConstantException(s, typeof(ApplicationType));
        }

        private LocalOrganizationType Read11_LocalOrganizationType(string s)
        {
            switch (s)
            {
                case "Root":
                    return LocalOrganizationType.Root;

                case "Section":
                    return LocalOrganizationType.Section;

                case "Company":
                    return LocalOrganizationType.Company;

                case "Department":
                    return LocalOrganizationType.Department;

                case "Group":
                    return LocalOrganizationType.Group;
            }
            throw base.CreateUnknownConstantException(s, typeof(LocalOrganizationType));
        }

        private Guid? Read12_NullableOfGuid(bool checkType)
        {
            Guid? nullable = null;
            if (base.ReadNull())
            {
                return nullable;
            }
            return new Guid?(XmlConvert.ToGuid(base.Reader.ReadElementString()));
        }

        private LocalOrganizationInfo Read14_LocalOrganizationInfo(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? base.GetXsiType() : null;
            bool flag = false;
            if (isNullable)
            {
                flag = base.ReadNull();
            }
            if (checkType && ((type != null) && ((type.Name != this.id142_LocalOrganizationInfo) || !(type.Namespace == this.id2_httptempuriorg))))
            {
                throw base.CreateUnknownTypeException(type);
            }
            if (flag)
            {
                return null;
            }
            LocalOrganizationInfo o = new LocalOrganizationInfo();
            bool[] flagArray = new bool[9];
            while (base.Reader.MoveToNextAttribute())
            {
                if (!base.IsXmlnsAttribute(base.Reader.Name))
                {
                    base.UnknownNode(o);
                }
            }
            base.Reader.MoveToElement();
            if (base.Reader.IsEmptyElement)
            {
                base.Reader.Skip();
                return o;
            }
            base.Reader.ReadStartElement();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.NodeType == XmlNodeType.Element)
                {
                    if (!(flagArray[0] || ((base.Reader.LocalName != this.id85_ID) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.ID = XmlConvert.ToGuid(base.Reader.ReadElementString());
                        flagArray[0] = true;
                    }
                    else if (!(flagArray[1] || ((base.Reader.LocalName != this.id144_Code) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Code = base.Reader.ReadElementString();
                        flagArray[1] = true;
                    }
                    else if (!(flagArray[2] || ((base.Reader.LocalName != this.id145_IsDefault) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.IsDefault = XmlConvert.ToBoolean(base.Reader.ReadElementString());
                        flagArray[2] = true;
                    }
                    else if (!(flagArray[3] || ((base.Reader.LocalName != this.id146_CShortName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.CShortName = base.Reader.ReadElementString();
                        flagArray[3] = true;
                    }
                    else if (!(flagArray[4] || ((base.Reader.LocalName != this.id147_EShortName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.EShortName = base.Reader.ReadElementString();
                        flagArray[4] = true;
                    }
                    else if (!(flagArray[5] || ((base.Reader.LocalName != this.id148_FullName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.FullName = base.Reader.ReadElementString();
                        flagArray[5] = true;
                    }
                    else if (!(flagArray[6] || ((base.Reader.LocalName != this.id93_Type) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Type = this.Read11_LocalOrganizationType(base.Reader.ReadElementString());
                        flagArray[6] = true;
                    }
                    else if (!(flagArray[7] || ((base.Reader.LocalName != this.id149_IsValid) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.IsValid = XmlConvert.ToBoolean(base.Reader.ReadElementString());
                        flagArray[7] = true;
                    }
                    else if (!(flagArray[8] || ((base.Reader.LocalName != this.id150_ParentID) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.ParentID = this.Read12_NullableOfGuid(true);
                        flagArray[8] = true;
                    }
                    else
                    {
                        base.UnknownNode(o, "http://tempuri.org/:ID, http://tempuri.org/:Code, http://tempuri.org/:IsDefault, http://tempuri.org/:CShortName, http://tempuri.org/:EShortName, http://tempuri.org/:FullName, http://tempuri.org/:Type, http://tempuri.org/:IsValid, http://tempuri.org/:ParentID");
                    }
                }
                else
                {
                    base.UnknownNode(o, "http://tempuri.org/:ID, http://tempuri.org/:Code, http://tempuri.org/:IsDefault, http://tempuri.org/:CShortName, http://tempuri.org/:EShortName, http://tempuri.org/:FullName, http://tempuri.org/:Type, http://tempuri.org/:IsValid, http://tempuri.org/:ParentID");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            base.ReadEndElement();
            return o;
        }

        private LoginInfo Read15_LoginInfo(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? base.GetXsiType() : null;
            bool flag = false;
            if (isNullable)
            {
                flag = base.ReadNull();
            }
            if (checkType && ((type != null) && ((type.Name != this.id129_LoginInfo) || !(type.Namespace == this.id2_httptempuriorg))))
            {
                throw base.CreateUnknownTypeException(type);
            }
            if (flag)
            {
                return null;
            }
            LoginInfo o = new LoginInfo();
            if (o.UserOrganizationList == null)
            {
                o.UserOrganizationList = new List<LocalOrganizationInfo>();
            }
            List<LocalOrganizationInfo> userOrganizationList = o.UserOrganizationList;
            bool[] flagArray = new bool[13];
            while (base.Reader.MoveToNextAttribute())
            {
                if (!base.IsXmlnsAttribute(base.Reader.Name))
                {
                    base.UnknownNode(o);
                }
            }
            base.Reader.MoveToElement();
            if (base.Reader.IsEmptyElement)
            {
                base.Reader.Skip();
                return o;
            }
            base.Reader.ReadStartElement();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.NodeType == XmlNodeType.Element)
                {
                    if (!(flagArray[0] || ((base.Reader.LocalName != this.id130_LoginID) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.LoginID = XmlConvert.ToGuid(base.Reader.ReadElementString());
                        flagArray[0] = true;
                    }
                    else if (!(flagArray[1] || ((base.Reader.LocalName != this.id131_LoginName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.LoginName = base.Reader.ReadElementString();
                        flagArray[1] = true;
                    }
                    else if (!(flagArray[2] || ((base.Reader.LocalName != this.id132_DefaultCompanyID) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.DefaultCompanyID = XmlConvert.ToGuid(base.Reader.ReadElementString());
                        flagArray[2] = true;
                    }
                    else if (!(flagArray[3] || ((base.Reader.LocalName != this.id133_DefaultCompanyName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.DefaultCompanyName = base.Reader.ReadElementString();
                        flagArray[3] = true;
                    }
                    else if (!(flagArray[4] || ((base.Reader.LocalName != this.id134_DefaultDepartmentID) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.DefaultDepartmentID = XmlConvert.ToGuid(base.Reader.ReadElementString());
                        flagArray[4] = true;
                    }
                    else if (!(flagArray[5] || ((base.Reader.LocalName != this.id135_DefaultDepartmentName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.DefaultDepartmentName = base.Reader.ReadElementString();
                        flagArray[5] = true;
                    }
                    else if (!(flagArray[6] || ((base.Reader.LocalName != this.id136_Password) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Password = base.Reader.ReadElementString();
                        flagArray[6] = true;
                    }
                    else if (!(flagArray[7] || ((base.Reader.LocalName != this.id137_MacAddress) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.MacAddress = base.Reader.ReadElementString();
                        flagArray[7] = true;
                    }
                    else if (!(flagArray[8] || ((base.Reader.LocalName != this.id138_LocalIpAddress) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.LocalIpAddress = base.Reader.ReadElementString();
                        flagArray[8] = true;
                    }
                    else if (!(flagArray[9] || ((base.Reader.LocalName != this.id139_PublicIpAddress) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.PublicIpAddress = base.Reader.ReadElementString();
                        flagArray[9] = true;
                    }
                    else if (!(flagArray[10] || ((base.Reader.LocalName != this.id140_UserName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.UserName = base.Reader.ReadElementString();
                        flagArray[10] = true;
                    }
                    else if ((base.Reader.LocalName == this.id141_UserOrganizationList) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                    {
                        if (!base.ReadNull())
                        {
                            if (o.UserOrganizationList == null)
                            {
                                o.UserOrganizationList = new List<LocalOrganizationInfo>();
                            }
                            List<LocalOrganizationInfo> list2 = o.UserOrganizationList;
                            if (base.Reader.IsEmptyElement)
                            {
                                base.Reader.Skip();
                            }
                            else
                            {
                                base.Reader.ReadStartElement();
                                base.Reader.MoveToContent();
                                int num3 = 0;
                                int num4 = base.ReaderCount;
                                while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                                {
                                    if (base.Reader.NodeType == XmlNodeType.Element)
                                    {
                                        if ((base.Reader.LocalName == this.id142_LocalOrganizationInfo) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                                        {
                                            if (list2 == null)
                                            {
                                                base.Reader.Skip();
                                            }
                                            else
                                            {
                                                list2.Add(this.Read14_LocalOrganizationInfo(true, true));
                                            }
                                        }
                                        else
                                        {
                                            base.UnknownNode(null, "http://tempuri.org/:LocalOrganizationInfo");
                                        }
                                    }
                                    else
                                    {
                                        base.UnknownNode(null, "http://tempuri.org/:LocalOrganizationInfo");
                                    }
                                    base.Reader.MoveToContent();
                                    base.CheckReaderCount(ref num3, ref num4);
                                }
                                base.ReadEndElement();
                            }
                        }
                    }
                    else if (!(flagArray[12] || ((base.Reader.LocalName != this.id143_EmailAddress) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.EmailAddress = base.Reader.ReadElementString();
                        flagArray[12] = true;
                    }
                    else
                    {
                        base.UnknownNode(o, "http://tempuri.org/:LoginID, http://tempuri.org/:LoginName, http://tempuri.org/:DefaultCompanyID, http://tempuri.org/:DefaultCompanyName, http://tempuri.org/:DefaultDepartmentID, http://tempuri.org/:DefaultDepartmentName, http://tempuri.org/:Password, http://tempuri.org/:MacAddress, http://tempuri.org/:LocalIpAddress, http://tempuri.org/:PublicIpAddress, http://tempuri.org/:UserName, http://tempuri.org/:UserOrganizationList, http://tempuri.org/:EmailAddress");
                    }
                }
                else
                {
                    base.UnknownNode(o, "http://tempuri.org/:LoginID, http://tempuri.org/:LoginName, http://tempuri.org/:DefaultCompanyID, http://tempuri.org/:DefaultCompanyName, http://tempuri.org/:DefaultDepartmentID, http://tempuri.org/:DefaultDepartmentName, http://tempuri.org/:Password, http://tempuri.org/:MacAddress, http://tempuri.org/:LocalIpAddress, http://tempuri.org/:PublicIpAddress, http://tempuri.org/:UserName, http://tempuri.org/:UserOrganizationList, http://tempuri.org/:EmailAddress");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            base.ReadEndElement();
            return o;
        }

        private PortalType Read16_PortalType(string s)
        {
            switch (s)
            {
                case "Web":
                    return PortalType.Web;

                case "MailCenter":
                    return PortalType.MailCenter;

                case "WorkCenter":
                    return PortalType.WorkCenter;
            }
            throw base.CreateUnknownConstantException(s, typeof(PortalType));
        }

        private DateTime? Read17_NullableOfDateTime(bool checkType)
        {
            DateTime? nullable = null;
            if (base.ReadNull())
            {
                return nullable;
            }
            return new DateTime?(XmlSerializationReader.ToDateTime(base.Reader.ReadElementString()));
        }

        private SystemConfigurationInfo Read18_SystemConfigurationInfo(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? base.GetXsiType() : null;
            bool flag = false;
            if (isNullable)
            {
                flag = base.ReadNull();
            }
            if (checkType && ((type != null) && ((type.Name != this.id122_SystemConfigurationInfo) || !(type.Namespace == this.id2_httptempuriorg))))
            {
                throw base.CreateUnknownTypeException(type);
            }
            if (flag)
            {
                return null;
            }
            SystemConfigurationInfo o = new SystemConfigurationInfo();
            bool[] flagArray = new bool[6];
            while (base.Reader.MoveToNextAttribute())
            {
                if (!base.IsXmlnsAttribute(base.Reader.Name))
                {
                    base.UnknownNode(o);
                }
            }
            base.Reader.MoveToElement();
            if (base.Reader.IsEmptyElement)
            {
                base.Reader.Skip();
                return o;
            }
            base.Reader.ReadStartElement();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.NodeType == XmlNodeType.Element)
                {
                    if (!(flagArray[0] || ((base.Reader.LocalName != this.id85_ID) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.ID = XmlConvert.ToGuid(base.Reader.ReadElementString());
                        flagArray[0] = true;
                    }
                    else if (!(flagArray[1] || ((base.Reader.LocalName != this.id124_Key) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Key = base.Reader.ReadElementString();
                        flagArray[1] = true;
                    }
                    else if (!(flagArray[2] || ((base.Reader.LocalName != this.id125_Value) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Value = base.Reader.ReadElementString();
                        flagArray[2] = true;
                    }
                    else if (!(flagArray[3] || ((base.Reader.LocalName != this.id126_Value2) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Value2 = base.Reader.ReadElementString();
                        flagArray[3] = true;
                    }
                    else if (!(flagArray[4] || ((base.Reader.LocalName != this.id127_Value3) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Value3 = base.Reader.ReadElementString();
                        flagArray[4] = true;
                    }
                    else if (!(flagArray[5] || ((base.Reader.LocalName != this.id128_UpdateTime) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.UpdateTime = this.Read17_NullableOfDateTime(true);
                        flagArray[5] = true;
                    }
                    else
                    {
                        base.UnknownNode(o, "http://tempuri.org/:ID, http://tempuri.org/:Key, http://tempuri.org/:Value, http://tempuri.org/:Value2, http://tempuri.org/:Value3, http://tempuri.org/:UpdateTime");
                    }
                }
                else
                {
                    base.UnknownNode(o, "http://tempuri.org/:ID, http://tempuri.org/:Key, http://tempuri.org/:Value, http://tempuri.org/:Value2, http://tempuri.org/:Value3, http://tempuri.org/:UpdateTime");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            base.ReadEndElement();
            return o;
        }

        private Module Read19_Module(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? base.GetXsiType() : null;
            bool flag = false;
            if (isNullable)
            {
                flag = base.ReadNull();
            }
            if (checkType && ((type != null) && ((type.Name != this.id80_Module) || !(type.Namespace == this.id2_httptempuriorg))))
            {
                throw base.CreateUnknownTypeException(type);
            }
            if (flag)
            {
                return null;
            }
            Module o = new Module();
            if (o.AllowRoles == null)
            {
                o.AllowRoles = new List<string>();
            }
            List<string> allowRoles = o.AllowRoles;
            bool[] flagArray = new bool[4];
            while (base.Reader.MoveToNextAttribute())
            {
                if (!base.IsXmlnsAttribute(base.Reader.Name))
                {
                    base.UnknownNode(o);
                }
            }
            base.Reader.MoveToElement();
            if (base.Reader.IsEmptyElement)
            {
                base.Reader.Skip();
                return o;
            }
            base.Reader.ReadStartElement();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.NodeType == XmlNodeType.Element)
                {
                    if (!(flagArray[0] || ((base.Reader.LocalName != this.id97_OrderNo) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.OrderNo = XmlConvert.ToInt32(base.Reader.ReadElementString());
                        flagArray[0] = true;
                    }
                    else if (!(flagArray[1] || ((base.Reader.LocalName != this.id98_Assembly) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Assembly = base.Reader.ReadElementString();
                        flagArray[1] = true;
                    }
                    else if (!(flagArray[2] || ((base.Reader.LocalName != this.id99_UpdateLocation) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.UpdateLocation = base.Reader.ReadElementString();
                        flagArray[2] = true;
                    }
                    else if ((base.Reader.LocalName == this.id100_AllowRoles) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                    {
                        if (!base.ReadNull())
                        {
                            if (o.AllowRoles == null)
                            {
                                o.AllowRoles = new List<string>();
                            }
                            List<string> list2 = o.AllowRoles;
                            if (base.Reader.IsEmptyElement)
                            {
                                base.Reader.Skip();
                            }
                            else
                            {
                                base.Reader.ReadStartElement();
                                base.Reader.MoveToContent();
                                int num3 = 0;
                                int num4 = base.ReaderCount;
                                while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                                {
                                    if (base.Reader.NodeType == XmlNodeType.Element)
                                    {
                                        if ((base.Reader.LocalName == this.id23_string) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                                        {
                                            if (base.ReadNull())
                                            {
                                                list2.Add(null);
                                            }
                                            else
                                            {
                                                list2.Add(base.Reader.ReadElementString());
                                            }
                                        }
                                        else
                                        {
                                            base.UnknownNode(null, "http://tempuri.org/:string");
                                        }
                                    }
                                    else
                                    {
                                        base.UnknownNode(null, "http://tempuri.org/:string");
                                    }
                                    base.Reader.MoveToContent();
                                    base.CheckReaderCount(ref num3, ref num4);
                                }
                                base.ReadEndElement();
                            }
                        }
                    }
                    else
                    {
                        base.UnknownNode(o, "http://tempuri.org/:OrderNo, http://tempuri.org/:Assembly, http://tempuri.org/:UpdateLocation, http://tempuri.org/:AllowRoles");
                    }
                }
                else
                {
                    base.UnknownNode(o, "http://tempuri.org/:OrderNo, http://tempuri.org/:Assembly, http://tempuri.org/:UpdateLocation, http://tempuri.org/:AllowRoles");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            base.ReadEndElement();
            return o;
        }

        private ServiceInfo Read2_ServiceInfo(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? base.GetXsiType() : null;
            bool flag = false;
            if (isNullable)
            {
                flag = base.ReadNull();
            }
            if (checkType && ((type != null) && ((type.Name != this.id74_ServiceInfo) || !(type.Namespace == this.id2_httptempuriorg))))
            {
                throw base.CreateUnknownTypeException(type);
            }
            if (flag)
            {
                return null;
            }
            ServiceInfo o = new ServiceInfo();
            bool[] flagArray = new bool[0];
            while (base.Reader.MoveToNextAttribute())
            {
                if (!base.IsXmlnsAttribute(base.Reader.Name))
                {
                    base.UnknownNode(o);
                }
            }
            base.Reader.MoveToElement();
            if (base.Reader.IsEmptyElement)
            {
                base.Reader.Skip();
                return o;
            }
            base.Reader.ReadStartElement();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.NodeType == XmlNodeType.Element)
                {
                    base.UnknownNode(o, "");
                }
                else
                {
                    base.UnknownNode(o, "");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            base.ReadEndElement();
            return o;
        }

        private UICommand Read20_UICommand(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? base.GetXsiType() : null;
            bool flag = false;
            if (isNullable)
            {
                flag = base.ReadNull();
            }
            if (checkType && ((type != null) && ((type.Name != this.id82_UICommand) || !(type.Namespace == this.id2_httptempuriorg))))
            {
                throw base.CreateUnknownTypeException(type);
            }
            if (flag)
            {
                return null;
            }
            UICommand o = new UICommand();
            bool[] flagArray = new bool[8];
            while (base.Reader.MoveToNextAttribute())
            {
                if (!base.IsXmlnsAttribute(base.Reader.Name))
                {
                    base.UnknownNode(o);
                }
            }
            base.Reader.MoveToElement();
            if (base.Reader.IsEmptyElement)
            {
                base.Reader.Skip();
                return o;
            }
            base.Reader.ReadStartElement();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.NodeType == XmlNodeType.Element)
                {
                    if (!(flagArray[0] || ((base.Reader.LocalName != this.id89_Text) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Text = base.Reader.ReadElementString();
                        flagArray[0] = true;
                    }
                    else if (!(flagArray[1] || ((base.Reader.LocalName != this.id90_Name) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Name = base.Reader.ReadElementString();
                        flagArray[1] = true;
                    }
                    else if (!(flagArray[2] || ((base.Reader.LocalName != this.id91_Site) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Site = base.Reader.ReadElementString();
                        flagArray[2] = true;
                    }
                    else if (!(flagArray[3] || ((base.Reader.LocalName != this.id92_RegisterSite) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.RegisterSite = base.Reader.ReadElementString();
                        flagArray[3] = true;
                    }
                    else if (!(flagArray[4] || ((base.Reader.LocalName != this.id93_Type) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Type = base.Reader.ReadElementString();
                        flagArray[4] = true;
                    }
                    else if (!(flagArray[5] || ((base.Reader.LocalName != this.id94_Image) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Image = base.ToByteArrayBase64(false);
                        flagArray[5] = true;
                    }
                    else if (!(flagArray[6] || ((base.Reader.LocalName != this.id95_AssemblyName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.AssemblyName = base.Reader.ReadElementString();
                        flagArray[6] = true;
                    }
                    else if (!(flagArray[7] || ((base.Reader.LocalName != this.id96_HasPermission) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.HasPermission = XmlConvert.ToBoolean(base.Reader.ReadElementString());
                        flagArray[7] = true;
                    }
                    else
                    {
                        base.UnknownNode(o, "http://tempuri.org/:Text, http://tempuri.org/:Name, http://tempuri.org/:Site, http://tempuri.org/:RegisterSite, http://tempuri.org/:Type, http://tempuri.org/:Image, http://tempuri.org/:AssemblyName, http://tempuri.org/:HasPermission");
                    }
                }
                else
                {
                    base.UnknownNode(o, "http://tempuri.org/:Text, http://tempuri.org/:Name, http://tempuri.org/:Site, http://tempuri.org/:RegisterSite, http://tempuri.org/:Type, http://tempuri.org/:Image, http://tempuri.org/:AssemblyName, http://tempuri.org/:HasPermission");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            base.ReadEndElement();
            return o;
        }

        private SimpleRoleInfo Read21_SimpleRoleInfo(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? base.GetXsiType() : null;
            bool flag = false;
            if (isNullable)
            {
                flag = base.ReadNull();
            }
            if (checkType && ((type != null) && ((type.Name != this.id84_SimpleRoleInfo) || !(type.Namespace == this.id2_httptempuriorg))))
            {
                throw base.CreateUnknownTypeException(type);
            }
            if (flag)
            {
                return null;
            }
            SimpleRoleInfo o = new SimpleRoleInfo();
            bool[] flagArray = new bool[4];
            while (base.Reader.MoveToNextAttribute())
            {
                if (!base.IsXmlnsAttribute(base.Reader.Name))
                {
                    base.UnknownNode(o);
                }
            }
            base.Reader.MoveToElement();
            if (base.Reader.IsEmptyElement)
            {
                base.Reader.Skip();
                return o;
            }
            base.Reader.ReadStartElement();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.NodeType == XmlNodeType.Element)
                {
                    if (!(flagArray[0] || ((base.Reader.LocalName != this.id85_ID) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.ID = XmlConvert.ToGuid(base.Reader.ReadElementString());
                        flagArray[0] = true;
                    }
                    else if (!(flagArray[1] || ((base.Reader.LocalName != this.id86_CName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.CName = base.Reader.ReadElementString();
                        flagArray[1] = true;
                    }
                    else if (!(flagArray[2] || ((base.Reader.LocalName != this.id87_EName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.EName = base.Reader.ReadElementString();
                        flagArray[2] = true;
                    }
                    else if (!(flagArray[3] || ((base.Reader.LocalName != this.id88_CompanyID) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.CompanyID = this.Read12_NullableOfGuid(true);
                        flagArray[3] = true;
                    }
                    else
                    {
                        base.UnknownNode(o, "http://tempuri.org/:ID, http://tempuri.org/:CName, http://tempuri.org/:EName, http://tempuri.org/:CompanyID");
                    }
                }
                else
                {
                    base.UnknownNode(o, "http://tempuri.org/:ID, http://tempuri.org/:CName, http://tempuri.org/:EName, http://tempuri.org/:CompanyID");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            base.ReadEndElement();
            return o;
        }

        private PermissionPackage Read22_PermissionPackage(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? base.GetXsiType() : null;
            bool flag = false;
            if (isNullable)
            {
                flag = base.ReadNull();
            }
            if (checkType && ((type != null) && ((type.Name != this.id75_PermissionPackage) || !(type.Namespace == this.id2_httptempuriorg))))
            {
                throw base.CreateUnknownTypeException(type);
            }
            if (flag)
            {
                return null;
            }
            PermissionPackage o = new PermissionPackage();
            List<Module> modules = o.Modules;
            List<UICommand> commands = o.Commands;
            List<SimpleRoleInfo> roleList = o.RoleList;
            bool[] flagArray = new bool[6];
            while (base.Reader.MoveToNextAttribute())
            {
                if (!base.IsXmlnsAttribute(base.Reader.Name))
                {
                    base.UnknownNode(o);
                }
            }
            base.Reader.MoveToElement();
            if (base.Reader.IsEmptyElement)
            {
                base.Reader.Skip();
                return o;
            }
            base.Reader.ReadStartElement();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.NodeType == XmlNodeType.Element)
                {
                    if (!(flagArray[0] || ((base.Reader.LocalName != this.id76_AllowActions) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.AllowActions = (SerializableDictionary<string, bool>) base.ReadSerializable(new SerializableDictionary<string, bool>());
                        flagArray[0] = true;
                    }
                    else if (!(flagArray[1] || ((base.Reader.LocalName != this.id77_BuildTime) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.BuildTime = XmlSerializationReader.ToDateTime(base.Reader.ReadElementString());
                        flagArray[1] = true;
                    }
                    else if (!(flagArray[2] || ((base.Reader.LocalName != this.id78_UserId) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.UserId = XmlConvert.ToGuid(base.Reader.ReadElementString());
                        flagArray[2] = true;
                    }
                    else if ((base.Reader.LocalName == this.id79_Modules) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                    {
                        if (!base.ReadNull())
                        {
                            List<Module> list4 = o.Modules;
                            if ((list4 == null) || base.Reader.IsEmptyElement)
                            {
                                base.Reader.Skip();
                            }
                            else
                            {
                                base.Reader.ReadStartElement();
                                base.Reader.MoveToContent();
                                int num3 = 0;
                                int num4 = base.ReaderCount;
                                while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                                {
                                    if (base.Reader.NodeType == XmlNodeType.Element)
                                    {
                                        if ((base.Reader.LocalName == this.id80_Module) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                                        {
                                            if (list4 == null)
                                            {
                                                base.Reader.Skip();
                                            }
                                            else
                                            {
                                                list4.Add(this.Read19_Module(true, true));
                                            }
                                        }
                                        else
                                        {
                                            base.UnknownNode(null, "http://tempuri.org/:Module");
                                        }
                                    }
                                    else
                                    {
                                        base.UnknownNode(null, "http://tempuri.org/:Module");
                                    }
                                    base.Reader.MoveToContent();
                                    base.CheckReaderCount(ref num3, ref num4);
                                }
                                base.ReadEndElement();
                            }
                        }
                    }
                    else if ((base.Reader.LocalName == this.id81_Commands) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                    {
                        if (!base.ReadNull())
                        {
                            List<UICommand> list5 = o.Commands;
                            if ((list5 == null) || base.Reader.IsEmptyElement)
                            {
                                base.Reader.Skip();
                            }
                            else
                            {
                                base.Reader.ReadStartElement();
                                base.Reader.MoveToContent();
                                int num5 = 0;
                                int num6 = base.ReaderCount;
                                while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                                {
                                    if (base.Reader.NodeType == XmlNodeType.Element)
                                    {
                                        if ((base.Reader.LocalName == this.id82_UICommand) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                                        {
                                            if (list5 == null)
                                            {
                                                base.Reader.Skip();
                                            }
                                            else
                                            {
                                                list5.Add(this.Read20_UICommand(true, true));
                                            }
                                        }
                                        else
                                        {
                                            base.UnknownNode(null, "http://tempuri.org/:UICommand");
                                        }
                                    }
                                    else
                                    {
                                        base.UnknownNode(null, "http://tempuri.org/:UICommand");
                                    }
                                    base.Reader.MoveToContent();
                                    base.CheckReaderCount(ref num5, ref num6);
                                }
                                base.ReadEndElement();
                            }
                        }
                    }
                    else if ((base.Reader.LocalName == this.id83_RoleList) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                    {
                        if (!base.ReadNull())
                        {
                            List<SimpleRoleInfo> list6 = o.RoleList;
                            if ((list6 == null) || base.Reader.IsEmptyElement)
                            {
                                base.Reader.Skip();
                            }
                            else
                            {
                                base.Reader.ReadStartElement();
                                base.Reader.MoveToContent();
                                int num7 = 0;
                                int num8 = base.ReaderCount;
                                while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                                {
                                    if (base.Reader.NodeType == XmlNodeType.Element)
                                    {
                                        if ((base.Reader.LocalName == this.id84_SimpleRoleInfo) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                                        {
                                            if (list6 == null)
                                            {
                                                base.Reader.Skip();
                                            }
                                            else
                                            {
                                                list6.Add(this.Read21_SimpleRoleInfo(true, true));
                                            }
                                        }
                                        else
                                        {
                                            base.UnknownNode(null, "http://tempuri.org/:SimpleRoleInfo");
                                        }
                                    }
                                    else
                                    {
                                        base.UnknownNode(null, "http://tempuri.org/:SimpleRoleInfo");
                                    }
                                    base.Reader.MoveToContent();
                                    base.CheckReaderCount(ref num7, ref num8);
                                }
                                base.ReadEndElement();
                            }
                        }
                    }
                    else
                    {
                        base.UnknownNode(o, "http://tempuri.org/:AllowActions, http://tempuri.org/:BuildTime, http://tempuri.org/:UserId, http://tempuri.org/:Modules, http://tempuri.org/:Commands, http://tempuri.org/:RoleList");
                    }
                }
                else
                {
                    base.UnknownNode(o, "http://tempuri.org/:AllowActions, http://tempuri.org/:BuildTime, http://tempuri.org/:UserId, http://tempuri.org/:Modules, http://tempuri.org/:Commands, http://tempuri.org/:RoleList");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            base.ReadEndElement();
            return o;
        }

        private LoginUserInfo Read23_LoginUserInfo(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? base.GetXsiType() : null;
            bool flag = false;
            if (isNullable)
            {
                flag = base.ReadNull();
            }
            if (checkType && ((type != null) && ((type.Name != this.id103_LoginUserInfo) || !(type.Namespace == this.id2_httptempuriorg))))
            {
                throw base.CreateUnknownTypeException(type);
            }
            if (flag)
            {
                return null;
            }
            LoginUserInfo o = new LoginUserInfo();
            if (o.SystemConfigInfoList == null)
            {
                o.SystemConfigInfoList = new List<SystemConfigurationInfo>();
            }
            List<SystemConfigurationInfo> systemConfigInfoList = o.SystemConfigInfoList;
            bool[] flagArray = new bool[0x15];
            while (base.Reader.MoveToNextAttribute())
            {
                if (!base.IsXmlnsAttribute(base.Reader.Name))
                {
                    base.UnknownNode(o);
                }
            }
            base.Reader.MoveToElement();
            if (base.Reader.IsEmptyElement)
            {
                base.Reader.Skip();
                return o;
            }
            base.Reader.ReadStartElement();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.NodeType == XmlNodeType.Element)
                {
                    if (!(flagArray[0] || ((base.Reader.LocalName != this.id104_CultureName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.CultureName = base.Reader.ReadElementString();
                        flagArray[0] = true;
                    }
                    else if (!(flagArray[1] || ((base.Reader.LocalName != this.id105_ApplicationType) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.ApplicationType = this.Read10_ApplicationType(base.Reader.ReadElementString());
                        flagArray[1] = true;
                    }
                    else if (!(flagArray[2] || ((base.Reader.LocalName != this.id106_AdministratorId) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.AdministratorId = XmlConvert.ToGuid(base.Reader.ReadElementString());
                        flagArray[2] = true;
                    }
                    else if (!(flagArray[3] || ((base.Reader.LocalName != this.id102_SessionId) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.SessionId = base.Reader.ReadElementString();
                        flagArray[3] = true;
                    }
                    else if (!(flagArray[4] || ((base.Reader.LocalName != this.id107_ClientId) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.ClientId = XmlConvert.ToGuid(base.Reader.ReadElementString());
                        flagArray[4] = true;
                    }
                    else if (!(flagArray[5] || ((base.Reader.LocalName != this.id108_IsEnglish) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.IsEnglish = XmlConvert.ToBoolean(base.Reader.ReadElementString());
                        flagArray[5] = true;
                    }
                    else if (!(flagArray[6] || ((base.Reader.LocalName != this.id109_DataSyncFinished) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.DataSyncFinished = XmlConvert.ToBoolean(base.Reader.ReadElementString());
                        flagArray[6] = true;
                    }
                    else if (!(flagArray[7] || ((base.Reader.LocalName != this.id110_EnableCustomDataGrid) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.EnableCustomDataGrid = XmlConvert.ToBoolean(base.Reader.ReadElementString());
                        flagArray[7] = true;
                    }
                    else if (!(flagArray[8] || ((base.Reader.LocalName != this.id111_SkinName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.SkinName = base.Reader.ReadElementString();
                        flagArray[8] = true;
                    }
                    else if (!(flagArray[9] || ((base.Reader.LocalName != this.id112_SystemVersionNo) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.SystemVersionNo = XmlConvert.ToInt32(base.Reader.ReadElementString());
                        flagArray[9] = true;
                    }
                    else if (!(flagArray[10] || ((base.Reader.LocalName != this.id113_SystemNGenVersionNo) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.SystemNGenVersionNo = XmlConvert.ToInt32(base.Reader.ReadElementString());
                        flagArray[10] = true;
                    }
                    else if (!(flagArray[11] || ((base.Reader.LocalName != this.id114_SystemUpdateVersionNo) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.SystemUpdateVersionNo = XmlConvert.ToInt32(base.Reader.ReadElementString());
                        flagArray[11] = true;
                    }
                    else if (!(flagArray[12] || ((base.Reader.LocalName != this.id115_EmailHost) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.EmailHost = base.Reader.ReadElementString();
                        flagArray[12] = true;
                    }
                    else if (!(flagArray[13] || ((base.Reader.LocalName != this.id116_IsDesignMode) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.IsDesignMode = XmlConvert.ToBoolean(base.Reader.ReadElementString());
                        flagArray[13] = true;
                    }
                    else if (!(flagArray[14] || ((base.Reader.LocalName != this.id117_SysFontName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.SysFontName = base.Reader.ReadElementString();
                        flagArray[14] = true;
                    }
                    else if (!(flagArray[15] || ((base.Reader.LocalName != this.id118_MenuFontName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.MenuFontName = base.Reader.ReadElementString();
                        flagArray[15] = true;
                    }
                    else if (!(flagArray[0x10] || ((base.Reader.LocalName != this.id119_UserInfo) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.UserInfo = this.Read15_LoginInfo(false, true);
                        flagArray[0x10] = true;
                    }
                    else if (!(flagArray[0x11] || ((base.Reader.LocalName != this.id120_PortalType) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.PortalType = this.Read16_PortalType(base.Reader.ReadElementString());
                        flagArray[0x11] = true;
                    }
                    else if ((base.Reader.LocalName == this.id121_SystemConfigInfoList) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                    {
                        if (!base.ReadNull())
                        {
                            if (o.SystemConfigInfoList == null)
                            {
                                o.SystemConfigInfoList = new List<SystemConfigurationInfo>();
                            }
                            List<SystemConfigurationInfo> list2 = o.SystemConfigInfoList;
                            if (base.Reader.IsEmptyElement)
                            {
                                base.Reader.Skip();
                            }
                            else
                            {
                                base.Reader.ReadStartElement();
                                base.Reader.MoveToContent();
                                int num3 = 0;
                                int num4 = base.ReaderCount;
                                while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                                {
                                    if (base.Reader.NodeType == XmlNodeType.Element)
                                    {
                                        if ((base.Reader.LocalName == this.id122_SystemConfigurationInfo) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                                        {
                                            if (list2 == null)
                                            {
                                                base.Reader.Skip();
                                            }
                                            else
                                            {
                                                list2.Add(this.Read18_SystemConfigurationInfo(true, true));
                                            }
                                        }
                                        else
                                        {
                                            base.UnknownNode(null, "http://tempuri.org/:SystemConfigurationInfo");
                                        }
                                    }
                                    else
                                    {
                                        base.UnknownNode(null, "http://tempuri.org/:SystemConfigurationInfo");
                                    }
                                    base.Reader.MoveToContent();
                                    base.CheckReaderCount(ref num3, ref num4);
                                }
                                base.ReadEndElement();
                            }
                        }
                    }
                    else if (!(flagArray[0x13] || ((base.Reader.LocalName != this.id123_Height) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Height = XmlConvert.ToInt32(base.Reader.ReadElementString());
                        flagArray[0x13] = true;
                    }
                    else if (!(flagArray[20] || ((base.Reader.LocalName != this.id75_PermissionPackage) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.PermissionPackage = this.Read22_PermissionPackage(false, true);
                        flagArray[20] = true;
                    }
                    else
                    {
                        base.UnknownNode(o, "http://tempuri.org/:CultureName, http://tempuri.org/:ApplicationType, http://tempuri.org/:AdministratorId, http://tempuri.org/:SessionId, http://tempuri.org/:ClientId, http://tempuri.org/:IsEnglish, http://tempuri.org/:DataSyncFinished, http://tempuri.org/:EnableCustomDataGrid, http://tempuri.org/:SkinName, http://tempuri.org/:SystemVersionNo, http://tempuri.org/:SystemNGenVersionNo, http://tempuri.org/:SystemUpdateVersionNo, http://tempuri.org/:EmailHost, http://tempuri.org/:IsDesignMode, http://tempuri.org/:SysFontName, http://tempuri.org/:MenuFontName, http://tempuri.org/:UserInfo, http://tempuri.org/:PortalType, http://tempuri.org/:SystemConfigInfoList, http://tempuri.org/:Height, http://tempuri.org/:PermissionPackage,http://tempuri.org/:MainPath");
                    }
                }
                else
                {
                    base.UnknownNode(o, "http://tempuri.org/:CultureName, http://tempuri.org/:ApplicationType, http://tempuri.org/:AdministratorId, http://tempuri.org/:SessionId, http://tempuri.org/:ClientId, http://tempuri.org/:IsEnglish, http://tempuri.org/:DataSyncFinished, http://tempuri.org/:EnableCustomDataGrid, http://tempuri.org/:SkinName, http://tempuri.org/:SystemVersionNo, http://tempuri.org/:SystemNGenVersionNo, http://tempuri.org/:SystemUpdateVersionNo, http://tempuri.org/:EmailHost, http://tempuri.org/:IsDesignMode, http://tempuri.org/:SysFontName, http://tempuri.org/:MenuFontName, http://tempuri.org/:UserInfo, http://tempuri.org/:PortalType, http://tempuri.org/:SystemConfigInfoList, http://tempuri.org/:Height, http://tempuri.org/:PermissionPackage,http://tempuri.org/:MainPath");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            base.ReadEndElement();
            return o;
        }

        public object[] Read24_UnloadSession()
        {
            base.Reader.MoveToContent();
            object[] o = new object[2];
            o[1] = new DateTime();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id1_UnloadSession, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[2];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id3_sessionID) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = base.Reader.ReadElementString();
                                flagArray[0] = true;
                            }
                            else if (!(flagArray[1] || ((base.Reader.LocalName != this.id4_loginoutTime) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[1] = XmlSerializationReader.ToDateTime(base.Reader.ReadElementString());
                                flagArray[1] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:sessionID, http://tempuri.org/:loginoutTime");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:sessionID, http://tempuri.org/:loginoutTime");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:UnloadSession");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read25_UnloadSessionResponse()
        {
            base.Reader.MoveToContent();
            object[] o = new object[0];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id5_UnloadSessionResponse, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[0];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            base.UnknownNode(o, "");
                        }
                        else
                        {
                            base.UnknownNode(o, "");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:UnloadSessionResponse");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read26_GetUserDisplayName()
        {
            base.Reader.MoveToContent();
            object[] o = new object[1];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id6_GetUserDisplayName, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[1];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id7_username) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = base.Reader.ReadElementString();
                                flagArray[0] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:username");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:username");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:GetUserDisplayName");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read27_GetUserDisplayNameResponse()
        {
            base.Reader.MoveToContent();
            object[] o = new object[1];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id8_GetUserDisplayNameResponse, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[1];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id9_GetUserDisplayNameResult) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = base.Reader.ReadElementString();
                                flagArray[0] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:GetUserDisplayNameResult");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:GetUserDisplayNameResult");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:GetUserDisplayNameResponse");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read28_ChangePassword()
        {
            base.Reader.MoveToContent();
            object[] o = new object[3];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id10_ChangePassword, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[3];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id7_username) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = base.Reader.ReadElementString();
                                flagArray[0] = true;
                            }
                            else if (!(flagArray[1] || ((base.Reader.LocalName != this.id11_oldPassword) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[1] = base.Reader.ReadElementString();
                                flagArray[1] = true;
                            }
                            else if (!(flagArray[2] || ((base.Reader.LocalName != this.id12_newPassword) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[2] = base.Reader.ReadElementString();
                                flagArray[2] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:username, http://tempuri.org/:oldPassword, http://tempuri.org/:newPassword");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:username, http://tempuri.org/:oldPassword, http://tempuri.org/:newPassword");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:ChangePassword");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read29_ChangePasswordResponse()
        {
            base.Reader.MoveToContent();
            object[] o = new object[0];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id13_ChangePasswordResponse, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[0];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            base.UnknownNode(o, "");
                        }
                        else
                        {
                            base.UnknownNode(o, "");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:ChangePasswordResponse");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read30_ValidateUser()
        {
            base.Reader.MoveToContent();
            object[] o = new object[3];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id14_ValidateUser, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[3];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id7_username) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = base.Reader.ReadElementString();
                                flagArray[0] = true;
                            }
                            else if (!(flagArray[1] || ((base.Reader.LocalName != this.id15_password) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[1] = base.Reader.ReadElementString();
                                flagArray[1] = true;
                            }
                            else if (!(flagArray[2] || ((base.Reader.LocalName != this.id16_networkAdapter) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[2] = base.Reader.ReadElementString();
                                flagArray[2] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:username, http://tempuri.org/:password, http://tempuri.org/:networkAdapter");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:username, http://tempuri.org/:password, http://tempuri.org/:networkAdapter");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:ValidateUser");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read31_ValidateUserResponse()
        {
            base.Reader.MoveToContent();
            object[] o = new object[1];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id17_ValidateUserResponse, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[1];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id18_ValidateUserResult) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = base.Reader.ReadElementString();
                                flagArray[0] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:ValidateUserResult");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:ValidateUserResult");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:ValidateUserResponse");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read32_GetAllFunctionsFromUser()
        {
            base.Reader.MoveToContent();
            object[] o = new object[1];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id19_GetAllFunctionsFromUser, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[1];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id20_user) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = base.Reader.ReadElementString();
                                flagArray[0] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:user");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:user");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:GetAllFunctionsFromUser");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read33_Item()
        {
            base.Reader.MoveToContent();
            object[] o = new object[1];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id21_Item, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[1];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!flagArray[0] && ((base.Reader.LocalName == this.id22_GetAllFunctionsFromUserResult) && (base.Reader.NamespaceURI == this.id2_httptempuriorg)))
                            {
                                if (!base.ReadNull())
                                {
                                    string[] a = null;
                                    int index = 0;
                                    if (base.Reader.IsEmptyElement)
                                    {
                                        base.Reader.Skip();
                                    }
                                    else
                                    {
                                        base.Reader.ReadStartElement();
                                        base.Reader.MoveToContent();
                                        int num6 = 0;
                                        int num7 = base.ReaderCount;
                                        while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                                        {
                                            if (base.Reader.NodeType == XmlNodeType.Element)
                                            {
                                                if ((base.Reader.LocalName == this.id23_string) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                                                {
                                                    if (base.ReadNull())
                                                    {
                                                        a = (string[]) base.EnsureArrayIndex(a, index, typeof(string));
                                                        a[index++] = null;
                                                    }
                                                    else
                                                    {
                                                        a = (string[]) base.EnsureArrayIndex(a, index, typeof(string));
                                                        a[index++] = base.Reader.ReadElementString();
                                                    }
                                                }
                                                else
                                                {
                                                    base.UnknownNode(null, "http://tempuri.org/:string");
                                                }
                                            }
                                            else
                                            {
                                                base.UnknownNode(null, "http://tempuri.org/:string");
                                            }
                                            base.Reader.MoveToContent();
                                            base.CheckReaderCount(ref num6, ref num7);
                                        }
                                        base.ReadEndElement();
                                    }
                                    o[0] = (string[]) base.ShrinkArray(a, index, typeof(string), false);
                                }
                                flagArray[0] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:GetAllFunctionsFromUserResult");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:GetAllFunctionsFromUserResult");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:GetAllFunctionsFromUserResponse");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read34_Log()
        {
            base.Reader.MoveToContent();
            object[] o = new object[6];
            o[0] = new Guid();
            o[4] = new DateTime();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id24_Log, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[6];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id25_userId) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = XmlConvert.ToGuid(base.Reader.ReadElementString());
                                flagArray[0] = true;
                            }
                            else if (!(flagArray[1] || ((base.Reader.LocalName != this.id26_code) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[1] = base.Reader.ReadElementString();
                                flagArray[1] = true;
                            }
                            else if (!(flagArray[2] || ((base.Reader.LocalName != this.id27_ip) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[2] = base.Reader.ReadElementString();
                                flagArray[2] = true;
                            }
                            else if (!(flagArray[3] || ((base.Reader.LocalName != this.id28_netWorkNo) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[3] = base.Reader.ReadElementString();
                                flagArray[3] = true;
                            }
                            else if (!(flagArray[4] || ((base.Reader.LocalName != this.id29_loginTime) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[4] = XmlSerializationReader.ToDateTime(base.Reader.ReadElementString());
                                flagArray[4] = true;
                            }
                            else if (!(flagArray[5] || ((base.Reader.LocalName != this.id30_description) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[5] = base.Reader.ReadElementString();
                                flagArray[5] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:userId, http://tempuri.org/:code, http://tempuri.org/:ip, http://tempuri.org/:netWorkNo, http://tempuri.org/:loginTime, http://tempuri.org/:description");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:userId, http://tempuri.org/:code, http://tempuri.org/:ip, http://tempuri.org/:netWorkNo, http://tempuri.org/:loginTime, http://tempuri.org/:description");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:Log");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read35_LogResponse()
        {
            base.Reader.MoveToContent();
            object[] o = new object[0];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id31_LogResponse, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[0];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            base.UnknownNode(o, "");
                        }
                        else
                        {
                            base.UnknownNode(o, "");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:LogResponse");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read36_GetRolesForUser()
        {
            base.Reader.MoveToContent();
            object[] o = new object[1];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id32_GetRolesForUser, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[1];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id7_username) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = base.Reader.ReadElementString();
                                flagArray[0] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:username");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:username");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:GetRolesForUser");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read37_GetRolesForUserResponse()
        {
            base.Reader.MoveToContent();
            object[] o = new object[1];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id33_GetRolesForUserResponse, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[1];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!flagArray[0] && ((base.Reader.LocalName == this.id34_GetRolesForUserResult) && (base.Reader.NamespaceURI == this.id2_httptempuriorg)))
                            {
                                if (!base.ReadNull())
                                {
                                    string[] a = null;
                                    int index = 0;
                                    if (base.Reader.IsEmptyElement)
                                    {
                                        base.Reader.Skip();
                                    }
                                    else
                                    {
                                        base.Reader.ReadStartElement();
                                        base.Reader.MoveToContent();
                                        int num6 = 0;
                                        int num7 = base.ReaderCount;
                                        while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                                        {
                                            if (base.Reader.NodeType == XmlNodeType.Element)
                                            {
                                                if ((base.Reader.LocalName == this.id23_string) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                                                {
                                                    if (base.ReadNull())
                                                    {
                                                        a = (string[]) base.EnsureArrayIndex(a, index, typeof(string));
                                                        a[index++] = null;
                                                    }
                                                    else
                                                    {
                                                        a = (string[]) base.EnsureArrayIndex(a, index, typeof(string));
                                                        a[index++] = base.Reader.ReadElementString();
                                                    }
                                                }
                                                else
                                                {
                                                    base.UnknownNode(null, "http://tempuri.org/:string");
                                                }
                                            }
                                            else
                                            {
                                                base.UnknownNode(null, "http://tempuri.org/:string");
                                            }
                                            base.Reader.MoveToContent();
                                            base.CheckReaderCount(ref num6, ref num7);
                                        }
                                        base.ReadEndElement();
                                    }
                                    o[0] = (string[]) base.ShrinkArray(a, index, typeof(string), false);
                                }
                                flagArray[0] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:GetRolesForUserResult");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:GetRolesForUserResult");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:GetRolesForUserResponse");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read38_Save()
        {
            base.Reader.MoveToContent();
            object[] o = new object[6];
            o[0] = new Guid();
            o[5] = new DateTime();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id35_Save, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[6];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id25_userId) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = XmlConvert.ToGuid(base.Reader.ReadElementString());
                                flagArray[0] = true;
                            }
                            else if (!(flagArray[1] || ((base.Reader.LocalName != this.id36_userName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[1] = base.Reader.ReadElementString();
                                flagArray[1] = true;
                            }
                            else if (!(flagArray[2] || ((base.Reader.LocalName != this.id37_sessionId) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[2] = base.Reader.ReadElementString();
                                flagArray[2] = true;
                            }
                            else if (!(flagArray[3] || ((base.Reader.LocalName != this.id38_screenCapture) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[3] = base.ToByteArrayBase64(false);
                                flagArray[3] = true;
                            }
                            else if (!(flagArray[4] || ((base.Reader.LocalName != this.id30_description) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[4] = base.Reader.ReadElementString();
                                flagArray[4] = true;
                            }
                            else if (!(flagArray[5] || ((base.Reader.LocalName != this.id39_createTime) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[5] = XmlSerializationReader.ToDateTime(base.Reader.ReadElementString());
                                flagArray[5] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:userId, http://tempuri.org/:userName, http://tempuri.org/:sessionId, http://tempuri.org/:screenCapture, http://tempuri.org/:description, http://tempuri.org/:createTime");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:userId, http://tempuri.org/:userName, http://tempuri.org/:sessionId, http://tempuri.org/:screenCapture, http://tempuri.org/:description, http://tempuri.org/:createTime");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:Save");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read39_Get()
        {
            base.Reader.MoveToContent();
            object[] o = new object[0];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id40_Get, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[0];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            base.UnknownNode(o, "");
                        }
                        else
                        {
                            base.UnknownNode(o, "");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:Get");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        private AuthDataInfo Read4_AuthDataInfo(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? base.GetXsiType() : null;
            bool flag = false;
            if (isNullable)
            {
                flag = base.ReadNull();
            }
            if (checkType && ((type != null) && ((type.Name != this.id101_AuthDataInfo) || !(type.Namespace == this.id2_httptempuriorg))))
            {
                throw base.CreateUnknownTypeException(type);
            }
            if (flag)
            {
                return null;
            }
            AuthDataInfo o = new AuthDataInfo();
            bool[] flagArray = new bool[2];
            while (base.Reader.MoveToNextAttribute())
            {
                if (!base.IsXmlnsAttribute(base.Reader.Name))
                {
                    base.UnknownNode(o);
                }
            }
            base.Reader.MoveToElement();
            if (base.Reader.IsEmptyElement)
            {
                base.Reader.Skip();
                return o;
            }
            base.Reader.ReadStartElement();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.NodeType == XmlNodeType.Element)
                {
                    if (!(flagArray[0] || ((base.Reader.LocalName != this.id78_UserId) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.UserId = base.Reader.ReadElementString();
                        flagArray[0] = true;
                    }
                    else if (!(flagArray[1] || ((base.Reader.LocalName != this.id102_SessionId) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.SessionId = base.Reader.ReadElementString();
                        flagArray[1] = true;
                    }
                    else
                    {
                        base.UnknownNode(o, "http://tempuri.org/:UserId, http://tempuri.org/:SessionId");
                    }
                }
                else
                {
                    base.UnknownNode(o, "http://tempuri.org/:UserId, http://tempuri.org/:SessionId");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            base.ReadEndElement();
            return o;
        }

        public object[] Read40_GetResponse()
        {
            base.Reader.MoveToContent();
            object[] o = new object[1];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id41_GetResponse, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[1];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id42_GetResult) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = this.Read23_LoginUserInfo(false, true);
                                flagArray[0] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:GetResult");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:GetResult");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:GetResponse");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read41_Subscribe()
        {
            base.Reader.MoveToContent();
            object[] o = new object[3];
            o[2] = new Guid();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id43_Subscribe, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[3];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id44_eventOperation) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = base.Reader.ReadElementString();
                                flagArray[0] = true;
                            }
                            else if ((base.Reader.LocalName == this.id45_userDepartmentIds) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                            {
                                if (!base.ReadNull())
                                {
                                    if (o[1] == null)
                                    {
                                        o[1] = new List<Guid>();
                                    }
                                    List<Guid> list = (List<Guid>) o[1];
                                    if (base.Reader.IsEmptyElement)
                                    {
                                        base.Reader.Skip();
                                    }
                                    else
                                    {
                                        base.Reader.ReadStartElement();
                                        base.Reader.MoveToContent();
                                        int num5 = 0;
                                        int num6 = base.ReaderCount;
                                        while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                                        {
                                            if (base.Reader.NodeType == XmlNodeType.Element)
                                            {
                                                if ((base.Reader.LocalName == this.id46_guid) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                                                {
                                                    list.Add(XmlConvert.ToGuid(base.Reader.ReadElementString()));
                                                }
                                                else
                                                {
                                                    base.UnknownNode(null, "http://tempuri.org/:guid");
                                                }
                                            }
                                            else
                                            {
                                                base.UnknownNode(null, "http://tempuri.org/:guid");
                                            }
                                            base.Reader.MoveToContent();
                                            base.CheckReaderCount(ref num5, ref num6);
                                        }
                                        base.ReadEndElement();
                                    }
                                }
                            }
                            else if (!(flagArray[2] || ((base.Reader.LocalName != this.id47_clientId) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[2] = XmlConvert.ToGuid(base.Reader.ReadElementString());
                                flagArray[2] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:eventOperation, http://tempuri.org/:userDepartmentIds, http://tempuri.org/:clientId");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:eventOperation, http://tempuri.org/:userDepartmentIds, http://tempuri.org/:clientId");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:Subscribe");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read42_Unsubscribe()
        {
            base.Reader.MoveToContent();
            object[] o = new object[1];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id48_Unsubscribe, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[1];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id44_eventOperation) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = base.Reader.ReadElementString();
                                flagArray[0] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:eventOperation");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:eventOperation");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:Unsubscribe");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read43_AuthUser()
        {
            base.Reader.MoveToContent();
            object[] o = new object[4];
            o[3] = new DateTime();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id49_AuthUser, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[4];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id50_usercode) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = base.Reader.ReadElementString();
                                flagArray[0] = true;
                            }
                            else if (!(flagArray[1] || ((base.Reader.LocalName != this.id15_password) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[1] = base.Reader.ReadElementString();
                                flagArray[1] = true;
                            }
                            else if (!(flagArray[2] || ((base.Reader.LocalName != this.id51_macAddress) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[2] = base.Reader.ReadElementString();
                                flagArray[2] = true;
                            }
                            else if (!(flagArray[3] || ((base.Reader.LocalName != this.id29_loginTime) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[3] = XmlSerializationReader.ToDateTime(base.Reader.ReadElementString());
                                flagArray[3] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:usercode, http://tempuri.org/:password, http://tempuri.org/:macAddress, http://tempuri.org/:loginTime");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:usercode, http://tempuri.org/:password, http://tempuri.org/:macAddress, http://tempuri.org/:loginTime");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:AuthUser");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read44_AuthUserResponse()
        {
            base.Reader.MoveToContent();
            object[] o = new object[1];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id52_AuthUserResponse, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[1];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id53_AuthUserResult) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = this.Read4_AuthDataInfo(false, true);
                                flagArray[0] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:AuthUserResult");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:AuthUserResult");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:AuthUserResponse");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read45_GetPermissionPackage()
        {
            base.Reader.MoveToContent();
            object[] o = new object[] { new Guid() };
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id54_GetPermissionPackage, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[1];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id55_userid) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = XmlConvert.ToGuid(base.Reader.ReadElementString());
                                flagArray[0] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:userid");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:userid");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:GetPermissionPackage");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read46_GetPermissionPackageResponse()
        {
            base.Reader.MoveToContent();
            object[] o = new object[1];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id56_GetPermissionPackageResponse, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[1];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id57_GetPermissionPackageResult) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = this.Read9_PermissionPackage(false, true);
                                flagArray[0] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:GetPermissionPackageResult");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:GetPermissionPackageResult");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:GetPermissionPackageResponse");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read47_SynchronizeToDatabase()
        {
            base.Reader.MoveToContent();
            object[] o = new object[0];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id58_SynchronizeToDatabase, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[0];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            base.UnknownNode(o, "");
                        }
                        else
                        {
                            base.UnknownNode(o, "");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:SynchronizeToDatabase");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read48_SynchronizeToDatabaseResponse()
        {
            base.Reader.MoveToContent();
            object[] o = new object[0];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id59_SynchronizeToDatabaseResponse, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[0];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            base.UnknownNode(o, "");
                        }
                        else
                        {
                            base.UnknownNode(o, "");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:SynchronizeToDatabaseResponse");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read49_AddWithContextParameter()
        {
            base.Reader.MoveToContent();
            object[] o = new object[2];
            o[0] = new DateTime();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id60_AddWithContextParameter, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[2];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id61_operationTime) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = XmlSerializationReader.ToDateTime(base.Reader.ReadElementString());
                                flagArray[0] = true;
                            }
                            else if (!(flagArray[1] || ((base.Reader.LocalName != this.id62_content) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[1] = base.Reader.ReadElementString();
                                flagArray[1] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:operationTime, http://tempuri.org/:content");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:operationTime, http://tempuri.org/:content");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:AddWithContextParameter");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        private Module Read5_Module(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? base.GetXsiType() : null;
            bool flag = false;
            if (isNullable)
            {
                flag = base.ReadNull();
            }
            if (checkType && ((type != null) && ((type.Name != this.id80_Module) || !(type.Namespace == this.id2_httptempuriorg))))
            {
                throw base.CreateUnknownTypeException(type);
            }
            if (flag)
            {
                return null;
            }
            Module o = new Module();
            if (o.AllowRoles == null)
            {
                o.AllowRoles = new List<string>();
            }
            List<string> allowRoles = o.AllowRoles;
            bool[] flagArray = new bool[4];
            while (base.Reader.MoveToNextAttribute())
            {
                if (!base.IsXmlnsAttribute(base.Reader.Name))
                {
                    base.UnknownNode(o);
                }
            }
            base.Reader.MoveToElement();
            if (base.Reader.IsEmptyElement)
            {
                base.Reader.Skip();
                return o;
            }
            base.Reader.ReadStartElement();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.NodeType == XmlNodeType.Element)
                {
                    if (!(flagArray[0] || ((base.Reader.LocalName != this.id97_OrderNo) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.OrderNo = XmlConvert.ToInt32(base.Reader.ReadElementString());
                        flagArray[0] = true;
                    }
                    else if (!(flagArray[1] || ((base.Reader.LocalName != this.id98_Assembly) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Assembly = base.Reader.ReadElementString();
                        flagArray[1] = true;
                    }
                    else if (!(flagArray[2] || ((base.Reader.LocalName != this.id99_UpdateLocation) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.UpdateLocation = base.Reader.ReadElementString();
                        flagArray[2] = true;
                    }
                    else if ((base.Reader.LocalName == this.id100_AllowRoles) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                    {
                        if (!base.ReadNull())
                        {
                            if (o.AllowRoles == null)
                            {
                                o.AllowRoles = new List<string>();
                            }
                            List<string> list2 = o.AllowRoles;
                            if (base.Reader.IsEmptyElement)
                            {
                                base.Reader.Skip();
                            }
                            else
                            {
                                base.Reader.ReadStartElement();
                                base.Reader.MoveToContent();
                                int num3 = 0;
                                int num4 = base.ReaderCount;
                                while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                                {
                                    if (base.Reader.NodeType == XmlNodeType.Element)
                                    {
                                        if ((base.Reader.LocalName == this.id23_string) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                                        {
                                            if (base.ReadNull())
                                            {
                                                list2.Add(null);
                                            }
                                            else
                                            {
                                                list2.Add(base.Reader.ReadElementString());
                                            }
                                        }
                                        else
                                        {
                                            base.UnknownNode(null, "http://tempuri.org/:string");
                                        }
                                    }
                                    else
                                    {
                                        base.UnknownNode(null, "http://tempuri.org/:string");
                                    }
                                    base.Reader.MoveToContent();
                                    base.CheckReaderCount(ref num3, ref num4);
                                }
                                base.ReadEndElement();
                            }
                        }
                    }
                    else
                    {
                        base.UnknownNode(o, "http://tempuri.org/:OrderNo, http://tempuri.org/:Assembly, http://tempuri.org/:UpdateLocation, http://tempuri.org/:AllowRoles");
                    }
                }
                else
                {
                    base.UnknownNode(o, "http://tempuri.org/:OrderNo, http://tempuri.org/:Assembly, http://tempuri.org/:UpdateLocation, http://tempuri.org/:AllowRoles");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            base.ReadEndElement();
            return o;
        }

        public object[] Read50_Add()
        {
            base.Reader.MoveToContent();
            object[] o = new object[9];
            o[0] = new Guid();
            o[4] = new DateTime();
            o[8] = false;
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id63_Add, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[9];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id25_userId) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = XmlConvert.ToGuid(base.Reader.ReadElementString());
                                flagArray[0] = true;
                            }
                            else if (!(flagArray[1] || ((base.Reader.LocalName != this.id64_internetIP) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[1] = base.Reader.ReadElementString();
                                flagArray[1] = true;
                            }
                            else if (!(flagArray[2] || ((base.Reader.LocalName != this.id65_intarnetIP) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[2] = base.Reader.ReadElementString();
                                flagArray[2] = true;
                            }
                            else if (!(flagArray[3] || ((base.Reader.LocalName != this.id51_macAddress) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[3] = base.Reader.ReadElementString();
                                flagArray[3] = true;
                            }
                            else if (!(flagArray[4] || ((base.Reader.LocalName != this.id61_operationTime) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[4] = XmlSerializationReader.ToDateTime(base.Reader.ReadElementString());
                                flagArray[4] = true;
                            }
                            else if (!(flagArray[5] || ((base.Reader.LocalName != this.id66_assemblyNames) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[5] = base.Reader.ReadElementString();
                                flagArray[5] = true;
                            }
                            else if (!(flagArray[6] || ((base.Reader.LocalName != this.id67_functionName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[6] = base.Reader.ReadElementString();
                                flagArray[6] = true;
                            }
                            else if (!(flagArray[7] || ((base.Reader.LocalName != this.id62_content) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[7] = base.Reader.ReadElementString();
                                flagArray[7] = true;
                            }
                            else if (!(flagArray[8] || ((base.Reader.LocalName != this.id68_isEnglish) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[8] = XmlConvert.ToBoolean(base.Reader.ReadElementString());
                                flagArray[8] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:userId, http://tempuri.org/:internetIP, http://tempuri.org/:intarnetIP, http://tempuri.org/:macAddress, http://tempuri.org/:operationTime, http://tempuri.org/:assemblyNames, http://tempuri.org/:functionName, http://tempuri.org/:content, http://tempuri.org/:isEnglish");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:userId, http://tempuri.org/:internetIP, http://tempuri.org/:intarnetIP, http://tempuri.org/:macAddress, http://tempuri.org/:operationTime, http://tempuri.org/:assemblyNames, http://tempuri.org/:functionName, http://tempuri.org/:content, http://tempuri.org/:isEnglish");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:Add");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read51_BatchAdd()
        {
            base.Reader.MoveToContent();
            object[] o = new object[1];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id69_BatchAdd, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[1];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!(flagArray[0] || ((base.Reader.LocalName != this.id70_dt) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                            {
                                o[0] = (DataTable) base.ReadSerializable(new DataTable());
                                flagArray[0] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:dt");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:dt");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:BatchAdd");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read52_GetAllServices()
        {
            base.Reader.MoveToContent();
            object[] o = new object[0];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id71_GetAllServices, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[0];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            base.UnknownNode(o, "");
                        }
                        else
                        {
                            base.UnknownNode(o, "");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:GetAllServices");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        public object[] Read53_GetAllServicesResponse()
        {
            base.Reader.MoveToContent();
            object[] o = new object[1];
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.IsStartElement(this.id72_GetAllServicesResponse, this.id2_httptempuriorg))
                {
                    bool[] flagArray = new bool[1];
                    if (base.Reader.IsEmptyElement)
                    {
                        base.Reader.Skip();
                        base.Reader.MoveToContent();
                        continue;
                    }
                    base.Reader.ReadStartElement();
                    base.Reader.MoveToContent();
                    int num3 = 0;
                    int num4 = base.ReaderCount;
                    while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                    {
                        if (base.Reader.NodeType == XmlNodeType.Element)
                        {
                            if (!flagArray[0] && ((base.Reader.LocalName == this.id73_GetAllServicesResult) && (base.Reader.NamespaceURI == this.id2_httptempuriorg)))
                            {
                                if (!base.ReadNull())
                                {
                                    ServiceInfo[] a = null;
                                    int index = 0;
                                    if (base.Reader.IsEmptyElement)
                                    {
                                        base.Reader.Skip();
                                    }
                                    else
                                    {
                                        base.Reader.ReadStartElement();
                                        base.Reader.MoveToContent();
                                        int num6 = 0;
                                        int num7 = base.ReaderCount;
                                        while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                                        {
                                            if (base.Reader.NodeType == XmlNodeType.Element)
                                            {
                                                if ((base.Reader.LocalName == this.id74_ServiceInfo) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                                                {
                                                    a = (ServiceInfo[]) base.EnsureArrayIndex(a, index, typeof(ServiceInfo));
                                                    a[index++] = this.Read2_ServiceInfo(true, true);
                                                }
                                                else
                                                {
                                                    base.UnknownNode(null, "http://tempuri.org/:ServiceInfo");
                                                }
                                            }
                                            else
                                            {
                                                base.UnknownNode(null, "http://tempuri.org/:ServiceInfo");
                                            }
                                            base.Reader.MoveToContent();
                                            base.CheckReaderCount(ref num6, ref num7);
                                        }
                                        base.ReadEndElement();
                                    }
                                    o[0] = (ServiceInfo[]) base.ShrinkArray(a, index, typeof(ServiceInfo), false);
                                }
                                flagArray[0] = true;
                            }
                            else
                            {
                                base.UnknownNode(o, "http://tempuri.org/:GetAllServicesResult");
                            }
                        }
                        else
                        {
                            base.UnknownNode(o, "http://tempuri.org/:GetAllServicesResult");
                        }
                        base.Reader.MoveToContent();
                        base.CheckReaderCount(ref num3, ref num4);
                    }
                    base.ReadEndElement();
                }
                else
                {
                    base.UnknownNode(null, "http://tempuri.org/:GetAllServicesResponse");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            return o;
        }

        private UICommand Read6_UICommand(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? base.GetXsiType() : null;
            bool flag = false;
            if (isNullable)
            {
                flag = base.ReadNull();
            }
            if (checkType && ((type != null) && ((type.Name != this.id82_UICommand) || !(type.Namespace == this.id2_httptempuriorg))))
            {
                throw base.CreateUnknownTypeException(type);
            }
            if (flag)
            {
                return null;
            }
            UICommand o = new UICommand();
            bool[] flagArray = new bool[8];
            while (base.Reader.MoveToNextAttribute())
            {
                if (!base.IsXmlnsAttribute(base.Reader.Name))
                {
                    base.UnknownNode(o);
                }
            }
            base.Reader.MoveToElement();
            if (base.Reader.IsEmptyElement)
            {
                base.Reader.Skip();
                return o;
            }
            base.Reader.ReadStartElement();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.NodeType == XmlNodeType.Element)
                {
                    if (!(flagArray[0] || ((base.Reader.LocalName != this.id89_Text) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Text = base.Reader.ReadElementString();
                        flagArray[0] = true;
                    }
                    else if (!(flagArray[1] || ((base.Reader.LocalName != this.id90_Name) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Name = base.Reader.ReadElementString();
                        flagArray[1] = true;
                    }
                    else if (!(flagArray[2] || ((base.Reader.LocalName != this.id91_Site) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Site = base.Reader.ReadElementString();
                        flagArray[2] = true;
                    }
                    else if (!(flagArray[3] || ((base.Reader.LocalName != this.id92_RegisterSite) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.RegisterSite = base.Reader.ReadElementString();
                        flagArray[3] = true;
                    }
                    else if (!(flagArray[4] || ((base.Reader.LocalName != this.id93_Type) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Type = base.Reader.ReadElementString();
                        flagArray[4] = true;
                    }
                    else if (!(flagArray[5] || ((base.Reader.LocalName != this.id94_Image) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.Image = base.ToByteArrayBase64(false);
                        flagArray[5] = true;
                    }
                    else if (!(flagArray[6] || ((base.Reader.LocalName != this.id95_AssemblyName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.AssemblyName = base.Reader.ReadElementString();
                        flagArray[6] = true;
                    }
                    else if (!(flagArray[7] || ((base.Reader.LocalName != this.id96_HasPermission) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.HasPermission = XmlConvert.ToBoolean(base.Reader.ReadElementString());
                        flagArray[7] = true;
                    }
                    else
                    {
                        base.UnknownNode(o, "http://tempuri.org/:Text, http://tempuri.org/:Name, http://tempuri.org/:Site, http://tempuri.org/:RegisterSite, http://tempuri.org/:Type, http://tempuri.org/:Image, http://tempuri.org/:AssemblyName, http://tempuri.org/:HasPermission");
                    }
                }
                else
                {
                    base.UnknownNode(o, "http://tempuri.org/:Text, http://tempuri.org/:Name, http://tempuri.org/:Site, http://tempuri.org/:RegisterSite, http://tempuri.org/:Type, http://tempuri.org/:Image, http://tempuri.org/:AssemblyName, http://tempuri.org/:HasPermission");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            base.ReadEndElement();
            return o;
        }

        private Guid? Read7_NullableOfGuid(bool checkType)
        {
            Guid? nullable = null;
            if (base.ReadNull())
            {
                return nullable;
            }
            return new Guid?(XmlConvert.ToGuid(base.Reader.ReadElementString()));
        }

        private SimpleRoleInfo Read8_SimpleRoleInfo(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? base.GetXsiType() : null;
            bool flag = false;
            if (isNullable)
            {
                flag = base.ReadNull();
            }
            if (checkType && ((type != null) && ((type.Name != this.id84_SimpleRoleInfo) || !(type.Namespace == this.id2_httptempuriorg))))
            {
                throw base.CreateUnknownTypeException(type);
            }
            if (flag)
            {
                return null;
            }
            SimpleRoleInfo o = new SimpleRoleInfo();
            bool[] flagArray = new bool[4];
            while (base.Reader.MoveToNextAttribute())
            {
                if (!base.IsXmlnsAttribute(base.Reader.Name))
                {
                    base.UnknownNode(o);
                }
            }
            base.Reader.MoveToElement();
            if (base.Reader.IsEmptyElement)
            {
                base.Reader.Skip();
                return o;
            }
            base.Reader.ReadStartElement();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.NodeType == XmlNodeType.Element)
                {
                    if (!(flagArray[0] || ((base.Reader.LocalName != this.id85_ID) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.ID = XmlConvert.ToGuid(base.Reader.ReadElementString());
                        flagArray[0] = true;
                    }
                    else if (!(flagArray[1] || ((base.Reader.LocalName != this.id86_CName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.CName = base.Reader.ReadElementString();
                        flagArray[1] = true;
                    }
                    else if (!(flagArray[2] || ((base.Reader.LocalName != this.id87_EName) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.EName = base.Reader.ReadElementString();
                        flagArray[2] = true;
                    }
                    else if (!(flagArray[3] || ((base.Reader.LocalName != this.id88_CompanyID) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.CompanyID = this.Read7_NullableOfGuid(true);
                        flagArray[3] = true;
                    }
                    else
                    {
                        base.UnknownNode(o, "http://tempuri.org/:ID, http://tempuri.org/:CName, http://tempuri.org/:EName, http://tempuri.org/:CompanyID");
                    }
                }
                else
                {
                    base.UnknownNode(o, "http://tempuri.org/:ID, http://tempuri.org/:CName, http://tempuri.org/:EName, http://tempuri.org/:CompanyID");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            base.ReadEndElement();
            return o;
        }

        private PermissionPackage Read9_PermissionPackage(bool isNullable, bool checkType)
        {
            XmlQualifiedName type = checkType ? base.GetXsiType() : null;
            bool flag = false;
            if (isNullable)
            {
                flag = base.ReadNull();
            }
            if (checkType && ((type != null) && ((type.Name != this.id75_PermissionPackage) || !(type.Namespace == this.id2_httptempuriorg))))
            {
                throw base.CreateUnknownTypeException(type);
            }
            if (flag)
            {
                return null;
            }
            PermissionPackage o = new PermissionPackage();
            List<Module> modules = o.Modules;
            List<UICommand> commands = o.Commands;
            List<SimpleRoleInfo> roleList = o.RoleList;
            bool[] flagArray = new bool[6];
            while (base.Reader.MoveToNextAttribute())
            {
                if (!base.IsXmlnsAttribute(base.Reader.Name))
                {
                    base.UnknownNode(o);
                }
            }
            base.Reader.MoveToElement();
            if (base.Reader.IsEmptyElement)
            {
                base.Reader.Skip();
                return o;
            }
            base.Reader.ReadStartElement();
            base.Reader.MoveToContent();
            int whileIterations = 0;
            int readerCount = base.ReaderCount;
            while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
            {
                if (base.Reader.NodeType == XmlNodeType.Element)
                {
                    if (!(flagArray[0] || ((base.Reader.LocalName != this.id76_AllowActions) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.AllowActions = (SerializableDictionary<string, bool>) base.ReadSerializable(new SerializableDictionary<string, bool>());
                        flagArray[0] = true;
                    }
                    else if (!(flagArray[1] || ((base.Reader.LocalName != this.id77_BuildTime) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.BuildTime = XmlSerializationReader.ToDateTime(base.Reader.ReadElementString());
                        flagArray[1] = true;
                    }
                    else if (!(flagArray[2] || ((base.Reader.LocalName != this.id78_UserId) || !(base.Reader.NamespaceURI == this.id2_httptempuriorg))))
                    {
                        o.UserId = XmlConvert.ToGuid(base.Reader.ReadElementString());
                        flagArray[2] = true;
                    }
                    else if ((base.Reader.LocalName == this.id79_Modules) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                    {
                        if (!base.ReadNull())
                        {
                            List<Module> list4 = o.Modules;
                            if ((list4 == null) || base.Reader.IsEmptyElement)
                            {
                                base.Reader.Skip();
                            }
                            else
                            {
                                base.Reader.ReadStartElement();
                                base.Reader.MoveToContent();
                                int num3 = 0;
                                int num4 = base.ReaderCount;
                                while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                                {
                                    if (base.Reader.NodeType == XmlNodeType.Element)
                                    {
                                        if ((base.Reader.LocalName == this.id80_Module) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                                        {
                                            if (list4 == null)
                                            {
                                                base.Reader.Skip();
                                            }
                                            else
                                            {
                                                list4.Add(this.Read5_Module(true, true));
                                            }
                                        }
                                        else
                                        {
                                            base.UnknownNode(null, "http://tempuri.org/:Module");
                                        }
                                    }
                                    else
                                    {
                                        base.UnknownNode(null, "http://tempuri.org/:Module");
                                    }
                                    base.Reader.MoveToContent();
                                    base.CheckReaderCount(ref num3, ref num4);
                                }
                                base.ReadEndElement();
                            }
                        }
                    }
                    else if ((base.Reader.LocalName == this.id81_Commands) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                    {
                        if (!base.ReadNull())
                        {
                            List<UICommand> list5 = o.Commands;
                            if ((list5 == null) || base.Reader.IsEmptyElement)
                            {
                                base.Reader.Skip();
                            }
                            else
                            {
                                base.Reader.ReadStartElement();
                                base.Reader.MoveToContent();
                                int num5 = 0;
                                int num6 = base.ReaderCount;
                                while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                                {
                                    if (base.Reader.NodeType == XmlNodeType.Element)
                                    {
                                        if ((base.Reader.LocalName == this.id82_UICommand) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                                        {
                                            if (list5 == null)
                                            {
                                                base.Reader.Skip();
                                            }
                                            else
                                            {
                                                list5.Add(this.Read6_UICommand(true, true));
                                            }
                                        }
                                        else
                                        {
                                            base.UnknownNode(null, "http://tempuri.org/:UICommand");
                                        }
                                    }
                                    else
                                    {
                                        base.UnknownNode(null, "http://tempuri.org/:UICommand");
                                    }
                                    base.Reader.MoveToContent();
                                    base.CheckReaderCount(ref num5, ref num6);
                                }
                                base.ReadEndElement();
                            }
                        }
                    }
                    else if ((base.Reader.LocalName == this.id83_RoleList) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                    {
                        if (!base.ReadNull())
                        {
                            List<SimpleRoleInfo> list6 = o.RoleList;
                            if ((list6 == null) || base.Reader.IsEmptyElement)
                            {
                                base.Reader.Skip();
                            }
                            else
                            {
                                base.Reader.ReadStartElement();
                                base.Reader.MoveToContent();
                                int num7 = 0;
                                int num8 = base.ReaderCount;
                                while ((base.Reader.NodeType != XmlNodeType.EndElement) && (base.Reader.NodeType != XmlNodeType.None))
                                {
                                    if (base.Reader.NodeType == XmlNodeType.Element)
                                    {
                                        if ((base.Reader.LocalName == this.id84_SimpleRoleInfo) && (base.Reader.NamespaceURI == this.id2_httptempuriorg))
                                        {
                                            if (list6 == null)
                                            {
                                                base.Reader.Skip();
                                            }
                                            else
                                            {
                                                list6.Add(this.Read8_SimpleRoleInfo(true, true));
                                            }
                                        }
                                        else
                                        {
                                            base.UnknownNode(null, "http://tempuri.org/:SimpleRoleInfo");
                                        }
                                    }
                                    else
                                    {
                                        base.UnknownNode(null, "http://tempuri.org/:SimpleRoleInfo");
                                    }
                                    base.Reader.MoveToContent();
                                    base.CheckReaderCount(ref num7, ref num8);
                                }
                                base.ReadEndElement();
                            }
                        }
                    }
                    else
                    {
                        base.UnknownNode(o, "http://tempuri.org/:AllowActions, http://tempuri.org/:BuildTime, http://tempuri.org/:UserId, http://tempuri.org/:Modules, http://tempuri.org/:Commands, http://tempuri.org/:RoleList");
                    }
                }
                else
                {
                    base.UnknownNode(o, "http://tempuri.org/:AllowActions, http://tempuri.org/:BuildTime, http://tempuri.org/:UserId, http://tempuri.org/:Modules, http://tempuri.org/:Commands, http://tempuri.org/:RoleList");
                }
                base.Reader.MoveToContent();
                base.CheckReaderCount(ref whileIterations, ref readerCount);
            }
            base.ReadEndElement();
            return o;
        }
    }
}
