namespace Microsoft.Xml.Serialization.GeneratedAssembly
{
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Server;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Xml;
    using System.Xml.Serialization;

    public class XmlSerializationWriter1 : XmlSerializationWriter
    {
        protected override void InitCallbacks()
        {
        }

        private string Write10_LocalOrganizationType(LocalOrganizationType v)
        {
            switch (v)
            {
                case LocalOrganizationType.Root:
                    return "Root";

                case LocalOrganizationType.Section:
                    return "Section";

                case LocalOrganizationType.Company:
                    return "Company";

                case LocalOrganizationType.Department:
                    return "Department";

                case LocalOrganizationType.Group:
                    return "Group";
            }
            long num = (long) v;
            throw base.CreateInvalidEnumValueException(num.ToString(CultureInfo.InvariantCulture), "ICP.Framework.CommonLibrary.Server.LocalOrganizationType");
        }

        private void Write12_LocalOrganizationInfo(string n, string ns, LocalOrganizationInfo o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
            }
            else
            {
                if (!needType && (o.GetType() != typeof(LocalOrganizationInfo)))
                {
                    throw base.CreateUnknownTypeException(o);
                }
                base.WriteStartElement(n, ns, o, false, null);
                if (needType)
                {
                    base.WriteXsiType("LocalOrganizationInfo", "http://tempuri.org/");
                }
                base.WriteElementStringRaw("ID", "http://tempuri.org/", XmlConvert.ToString(o.ID));
                base.WriteElementString("Code", "http://tempuri.org/", o.Code);
                base.WriteElementStringRaw("IsDefault", "http://tempuri.org/", XmlConvert.ToString(o.IsDefault));
                base.WriteElementString("CShortName", "http://tempuri.org/", o.CShortName);
                base.WriteElementString("EShortName", "http://tempuri.org/", o.EShortName);
                base.WriteElementString("FullName", "http://tempuri.org/", o.FullName);
                base.WriteElementString("Type", "http://tempuri.org/", this.Write10_LocalOrganizationType(o.Type));
                base.WriteElementStringRaw("IsValid", "http://tempuri.org/", XmlConvert.ToString(o.IsValid));
                if (o.ParentID.HasValue)
                {
                    base.WriteNullableStringLiteralRaw("ParentID", "http://tempuri.org/", XmlConvert.ToString(o.ParentID.Value));
                }
                else
                {
                    base.WriteNullTagLiteral("ParentID", "http://tempuri.org/");
                }
                base.WriteEndElement(o);
            }
        }

        private void Write13_LoginInfo(string n, string ns, LoginInfo o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
            }
            else
            {
                if (!needType && (o.GetType() != typeof(LoginInfo)))
                {
                    throw base.CreateUnknownTypeException(o);
                }
                base.WriteStartElement(n, ns, o, false, null);
                if (needType)
                {
                    base.WriteXsiType("LoginInfo", "http://tempuri.org/");
                }
                base.WriteElementStringRaw("LoginID", "http://tempuri.org/", XmlConvert.ToString(o.LoginID));
                base.WriteElementString("LoginName", "http://tempuri.org/", o.LoginName);
                base.WriteElementStringRaw("DefaultCompanyID", "http://tempuri.org/", XmlConvert.ToString(o.DefaultCompanyID));
                base.WriteElementString("DefaultCompanyName", "http://tempuri.org/", o.DefaultCompanyName);
                base.WriteElementStringRaw("DefaultDepartmentID", "http://tempuri.org/", XmlConvert.ToString(o.DefaultDepartmentID));
                base.WriteElementString("DefaultDepartmentName", "http://tempuri.org/", o.DefaultDepartmentName);
                base.WriteElementString("Password", "http://tempuri.org/", o.Password);
                base.WriteElementString("MacAddress", "http://tempuri.org/", o.MacAddress);
                base.WriteElementString("LocalIpAddress", "http://tempuri.org/", o.LocalIpAddress);
                base.WriteElementString("PublicIpAddress", "http://tempuri.org/", o.PublicIpAddress);
                base.WriteElementString("UserName", "http://tempuri.org/", o.UserName);
                List<LocalOrganizationInfo> userOrganizationList = o.UserOrganizationList;
                if (userOrganizationList != null)
                {
                    base.WriteStartElement("UserOrganizationList", "http://tempuri.org/", null, false);
                    for (int i = 0; i < userOrganizationList.Count; i++)
                    {
                        this.Write12_LocalOrganizationInfo("LocalOrganizationInfo", "http://tempuri.org/", userOrganizationList[i], true, false);
                    }
                    base.WriteEndElement();
                }
                base.WriteElementString("EmailAddress", "http://tempuri.org/", o.EmailAddress);
                base.WriteEndElement(o);
            }
        }

        private string Write14_PortalType(PortalType v)
        {
            switch (v)
            {
                case PortalType.Web:
                    return "Web";

                case PortalType.MailCenter:
                    return "MailCenter";

                case PortalType.WorkCenter:
                    return "WorkCenter";
            }
            long num = (long) v;
            throw base.CreateInvalidEnumValueException(num.ToString(CultureInfo.InvariantCulture), "ICP.Framework.CommonLibrary.Client.PortalType");
        }

        private void Write15_SystemConfigurationInfo(string n, string ns, SystemConfigurationInfo o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
            }
            else
            {
                if (!needType && (o.GetType() != typeof(SystemConfigurationInfo)))
                {
                    throw base.CreateUnknownTypeException(o);
                }
                base.WriteStartElement(n, ns, o, false, null);
                if (needType)
                {
                    base.WriteXsiType("SystemConfigurationInfo", "http://tempuri.org/");
                }
                base.WriteElementStringRaw("ID", "http://tempuri.org/", XmlConvert.ToString(o.ID));
                base.WriteElementString("Key", "http://tempuri.org/", o.Key);
                base.WriteElementString("Value", "http://tempuri.org/", o.Value);
                base.WriteElementString("Value2", "http://tempuri.org/", o.Value2);
                base.WriteElementString("Value3", "http://tempuri.org/", o.Value3);
                if (o.UpdateTime.HasValue)
                {
                    base.WriteNullableStringLiteralRaw("UpdateTime", "http://tempuri.org/", XmlSerializationWriter.FromDateTime(o.UpdateTime.Value));
                }
                else
                {
                    base.WriteNullTagLiteral("UpdateTime", "http://tempuri.org/");
                }
                base.WriteEndElement(o);
            }
        }

        private void Write16_Module(string n, string ns, Module o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
            }
            else
            {
                if (!needType && (o.GetType() != typeof(Module)))
                {
                    throw base.CreateUnknownTypeException(o);
                }
                base.WriteStartElement(n, ns, o, false, null);
                if (needType)
                {
                    base.WriteXsiType("Module", "http://tempuri.org/");
                }
                base.WriteElementStringRaw("OrderNo", "http://tempuri.org/", XmlConvert.ToString(o.OrderNo));
                base.WriteElementString("Assembly", "http://tempuri.org/", o.Assembly);
                base.WriteElementString("UpdateLocation", "http://tempuri.org/", o.UpdateLocation);
                List<string> allowRoles = o.AllowRoles;
                if (allowRoles != null)
                {
                    base.WriteStartElement("AllowRoles", "http://tempuri.org/", null, false);
                    for (int i = 0; i < allowRoles.Count; i++)
                    {
                        base.WriteNullableStringLiteral("string", "http://tempuri.org/", allowRoles[i]);
                    }
                    base.WriteEndElement();
                }
                base.WriteEndElement(o);
            }
        }

        private void Write17_UICommand(string n, string ns, UICommand o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
            }
            else
            {
                if (!needType && (o.GetType() != typeof(UICommand)))
                {
                    throw base.CreateUnknownTypeException(o);
                }
                base.WriteStartElement(n, ns, o, false, null);
                if (needType)
                {
                    base.WriteXsiType("UICommand", "http://tempuri.org/");
                }
                base.WriteElementString("Text", "http://tempuri.org/", o.Text);
                base.WriteElementString("Name", "http://tempuri.org/", o.Name);
                base.WriteElementString("Site", "http://tempuri.org/", o.Site);
                base.WriteElementString("RegisterSite", "http://tempuri.org/", o.RegisterSite);
                base.WriteElementString("Type", "http://tempuri.org/", o.Type);
                base.WriteElementStringRaw("Image", "http://tempuri.org/", XmlSerializationWriter.FromByteArrayBase64(o.Image));
                base.WriteElementString("AssemblyName", "http://tempuri.org/", o.AssemblyName);
                base.WriteElementStringRaw("HasPermission", "http://tempuri.org/", XmlConvert.ToString(o.HasPermission));
                base.WriteEndElement(o);
            }
        }

        private void Write18_SimpleRoleInfo(string n, string ns, SimpleRoleInfo o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
            }
            else
            {
                if (!needType && (o.GetType() != typeof(SimpleRoleInfo)))
                {
                    throw base.CreateUnknownTypeException(o);
                }
                base.WriteStartElement(n, ns, o, false, null);
                if (needType)
                {
                    base.WriteXsiType("SimpleRoleInfo", "http://tempuri.org/");
                }
                base.WriteElementStringRaw("ID", "http://tempuri.org/", XmlConvert.ToString(o.ID));
                base.WriteElementString("CName", "http://tempuri.org/", o.CName);
                base.WriteElementString("EName", "http://tempuri.org/", o.EName);
                if (o.CompanyID.HasValue)
                {
                    base.WriteNullableStringLiteralRaw("CompanyID", "http://tempuri.org/", XmlConvert.ToString(o.CompanyID.Value));
                }
                else
                {
                    base.WriteNullTagLiteral("CompanyID", "http://tempuri.org/");
                }
                base.WriteEndElement(o);
            }
        }

        private void Write19_PermissionPackage(string n, string ns, PermissionPackage o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
            }
            else
            {
                int num;
                if (!needType && (o.GetType() != typeof(PermissionPackage)))
                {
                    throw base.CreateUnknownTypeException(o);
                }
                base.WriteStartElement(n, ns, o, false, null);
                if (needType)
                {
                    base.WriteXsiType("PermissionPackage", "http://tempuri.org/");
                }
                base.WriteSerializable(o.AllowActions, "AllowActions", "http://tempuri.org/", false, true);
                base.WriteElementStringRaw("BuildTime", "http://tempuri.org/", XmlSerializationWriter.FromDateTime(o.BuildTime));
                base.WriteElementStringRaw("UserId", "http://tempuri.org/", XmlConvert.ToString(o.UserId));
                List<Module> modules = o.Modules;
                if (modules != null)
                {
                    base.WriteStartElement("Modules", "http://tempuri.org/", null, false);
                    for (num = 0; num < modules.Count; num++)
                    {
                        this.Write16_Module("Module", "http://tempuri.org/", modules[num], true, false);
                    }
                    base.WriteEndElement();
                }
                List<UICommand> commands = o.Commands;
                if (commands != null)
                {
                    base.WriteStartElement("Commands", "http://tempuri.org/", null, false);
                    for (num = 0; num < commands.Count; num++)
                    {
                        this.Write17_UICommand("UICommand", "http://tempuri.org/", commands[num], true, false);
                    }
                    base.WriteEndElement();
                }
                List<SimpleRoleInfo> roleList = o.RoleList;
                if (roleList != null)
                {
                    base.WriteStartElement("RoleList", "http://tempuri.org/", null, false);
                    for (num = 0; num < roleList.Count; num++)
                    {
                        this.Write18_SimpleRoleInfo("SimpleRoleInfo", "http://tempuri.org/", roleList[num], true, false);
                    }
                    base.WriteEndElement();
                }
                base.WriteEndElement(o);
            }
        }

        private void Write2_ServiceInfo(string n, string ns, ServiceInfo o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
            }
            else
            {
                if (!needType && (o.GetType() != typeof(ServiceInfo)))
                {
                    throw base.CreateUnknownTypeException(o);
                }
                base.WriteStartElement(n, ns, o, false, null);
                if (needType)
                {
                    base.WriteXsiType("ServiceInfo", "http://tempuri.org/");
                }
                base.WriteEndElement(o);
            }
        }

        private void Write20_LoginUserInfo(string n, string ns, LoginUserInfo o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
            }
            else
            {
                if (!needType && (o.GetType() != typeof(LoginUserInfo)))
                {
                    throw base.CreateUnknownTypeException(o);
                }
                base.WriteStartElement(n, ns, o, false, null);
                if (needType)
                {
                    base.WriteXsiType("LoginUserInfo", "http://tempuri.org/");
                }
                base.WriteElementString("CultureName", "http://tempuri.org/", o.CultureName);
                base.WriteElementString("ApplicationType", "http://tempuri.org/", this.Write9_ApplicationType(o.ApplicationType));
                base.WriteElementStringRaw("AdministratorId", "http://tempuri.org/", XmlConvert.ToString(o.AdministratorId));
                base.WriteElementString("SessionId", "http://tempuri.org/", o.SessionId);
                base.WriteElementStringRaw("ClientId", "http://tempuri.org/", XmlConvert.ToString(o.ClientId));
                base.WriteElementStringRaw("IsEnglish", "http://tempuri.org/", XmlConvert.ToString(o.IsEnglish));
                base.WriteElementStringRaw("DataSyncFinished", "http://tempuri.org/", XmlConvert.ToString(o.DataSyncFinished));
                base.WriteElementStringRaw("EnableCustomDataGrid", "http://tempuri.org/", XmlConvert.ToString(o.EnableCustomDataGrid));
                base.WriteElementString("SkinName", "http://tempuri.org/", o.SkinName);
                base.WriteElementStringRaw("SystemVersionNo", "http://tempuri.org/", XmlConvert.ToString(o.SystemVersionNo));
                base.WriteElementStringRaw("SystemNGenVersionNo", "http://tempuri.org/", XmlConvert.ToString(o.SystemNGenVersionNo));
                base.WriteElementStringRaw("SystemUpdateVersionNo", "http://tempuri.org/", XmlConvert.ToString(o.SystemUpdateVersionNo));
                base.WriteElementString("EmailHost", "http://tempuri.org/", o.EmailHost);
                base.WriteElementStringRaw("IsDesignMode", "http://tempuri.org/", XmlConvert.ToString(o.IsDesignMode));
                base.WriteElementString("SysFontName", "http://tempuri.org/", o.SysFontName);
                base.WriteElementString("MenuFontName", "http://tempuri.org/", o.MenuFontName);
                base.WriteElementString("MainPath", "http://tempuri.org/", o.MainPath);
                this.Write13_LoginInfo("UserInfo", "http://tempuri.org/", o.UserInfo, false, false);
                base.WriteElementString("PortalType", "http://tempuri.org/", this.Write14_PortalType(o.PortalType));
                List<SystemConfigurationInfo> systemConfigInfoList = o.SystemConfigInfoList;
                if (systemConfigInfoList != null)
                {
                    base.WriteStartElement("SystemConfigInfoList", "http://tempuri.org/", null, false);
                    for (int i = 0; i < systemConfigInfoList.Count; i++)
                    {
                        this.Write15_SystemConfigurationInfo("SystemConfigurationInfo", "http://tempuri.org/", systemConfigInfoList[i], true, false);
                    }
                    base.WriteEndElement();
                }
                base.WriteElementStringRaw("Height", "http://tempuri.org/", XmlConvert.ToString(o.Height));
                this.Write19_PermissionPackage("PermissionPackage", "http://tempuri.org/", o.PermissionPackage, false, false);
                base.WriteEndElement(o);
            }
        }

        public void Write21_UnloadSession(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("UnloadSession", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                base.WriteElementString("sessionID", "http://tempuri.org/", (string) p[0]);
            }
            if (length > 1)
            {
                base.WriteElementStringRaw("loginoutTime", "http://tempuri.org/", XmlSerializationWriter.FromDateTime((DateTime) p[1]));
            }
            base.WriteEndElement();
        }

        public void Write22_UnloadSessionResponse(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("UnloadSessionResponse", "http://tempuri.org/", null, false);
            base.WriteEndElement();
        }

        public void Write23_GetUserDisplayName(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("GetUserDisplayName", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                base.WriteElementString("username", "http://tempuri.org/", (string) p[0]);
            }
            base.WriteEndElement();
        }

        public void Write24_GetUserDisplayNameResponse(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("GetUserDisplayNameResponse", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                base.WriteElementString("GetUserDisplayNameResult", "http://tempuri.org/", (string) p[0]);
            }
            base.WriteEndElement();
        }

        public void Write25_ChangePassword(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("ChangePassword", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                base.WriteElementString("username", "http://tempuri.org/", (string) p[0]);
            }
            if (length > 1)
            {
                base.WriteElementString("oldPassword", "http://tempuri.org/", (string) p[1]);
            }
            if (length > 2)
            {
                base.WriteElementString("newPassword", "http://tempuri.org/", (string) p[2]);
            }
            base.WriteEndElement();
        }

        public void Write26_ChangePasswordResponse(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("ChangePasswordResponse", "http://tempuri.org/", null, false);
            base.WriteEndElement();
        }

        public void Write27_ValidateUser(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("ValidateUser", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                base.WriteElementString("username", "http://tempuri.org/", (string) p[0]);
            }
            if (length > 1)
            {
                base.WriteElementString("password", "http://tempuri.org/", (string) p[1]);
            }
            if (length > 2)
            {
                base.WriteElementString("networkAdapter", "http://tempuri.org/", (string) p[2]);
            }
            base.WriteEndElement();
        }

        public void Write28_ValidateUserResponse(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("ValidateUserResponse", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                base.WriteElementString("ValidateUserResult", "http://tempuri.org/", (string) p[0]);
            }
            base.WriteEndElement();
        }

        public void Write29_GetAllFunctionsFromUser(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("GetAllFunctionsFromUser", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                base.WriteElementString("user", "http://tempuri.org/", (string) p[0]);
            }
            base.WriteEndElement();
        }

        public void Write30_Item(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("GetAllFunctionsFromUserResponse", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                string[] strArray = (string[]) p[0];
                if (strArray != null)
                {
                    base.WriteStartElement("GetAllFunctionsFromUserResult", "http://tempuri.org/", null, false);
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        base.WriteNullableStringLiteral("string", "http://tempuri.org/", strArray[i]);
                    }
                    base.WriteEndElement();
                }
            }
            base.WriteEndElement();
        }

        public void Write31_Log(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("Log", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                base.WriteElementStringRaw("userId", "http://tempuri.org/", XmlConvert.ToString((Guid) p[0]));
            }
            if (length > 1)
            {
                base.WriteElementString("code", "http://tempuri.org/", (string) p[1]);
            }
            if (length > 2)
            {
                base.WriteElementString("ip", "http://tempuri.org/", (string) p[2]);
            }
            if (length > 3)
            {
                base.WriteElementString("netWorkNo", "http://tempuri.org/", (string) p[3]);
            }
            if (length > 4)
            {
                base.WriteElementStringRaw("loginTime", "http://tempuri.org/", XmlSerializationWriter.FromDateTime((DateTime) p[4]));
            }
            if (length > 5)
            {
                base.WriteElementString("description", "http://tempuri.org/", (string) p[5]);
            }
            base.WriteEndElement();
        }

        public void Write32_LogResponse(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("LogResponse", "http://tempuri.org/", null, false);
            base.WriteEndElement();
        }

        public void Write33_GetRolesForUser(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("GetRolesForUser", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                base.WriteElementString("username", "http://tempuri.org/", (string) p[0]);
            }
            base.WriteEndElement();
        }

        public void Write34_GetRolesForUserResponse(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("GetRolesForUserResponse", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                string[] strArray = (string[]) p[0];
                if (strArray != null)
                {
                    base.WriteStartElement("GetRolesForUserResult", "http://tempuri.org/", null, false);
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        base.WriteNullableStringLiteral("string", "http://tempuri.org/", strArray[i]);
                    }
                    base.WriteEndElement();
                }
            }
            base.WriteEndElement();
        }

        public void Write35_Save(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("Save", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                base.WriteElementStringRaw("userId", "http://tempuri.org/", XmlConvert.ToString((Guid) p[0]));
            }
            if (length > 1)
            {
                base.WriteElementString("userName", "http://tempuri.org/", (string) p[1]);
            }
            if (length > 2)
            {
                base.WriteElementString("sessionId", "http://tempuri.org/", (string) p[2]);
            }
            if (length > 3)
            {
                base.WriteElementStringRaw("screenCapture", "http://tempuri.org/", XmlSerializationWriter.FromByteArrayBase64((byte[]) p[3]));
            }
            if (length > 4)
            {
                base.WriteElementString("description", "http://tempuri.org/", (string) p[4]);
            }
            if (length > 5)
            {
                base.WriteElementStringRaw("createTime", "http://tempuri.org/", XmlSerializationWriter.FromDateTime((DateTime) p[5]));
            }
            base.WriteEndElement();
        }

        public void Write36_Get(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("Get", "http://tempuri.org/", null, false);
            base.WriteEndElement();
        }

        public void Write37_GetResponse(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("GetResponse", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                this.Write20_LoginUserInfo("GetResult", "http://tempuri.org/", (LoginUserInfo) p[0], false, false);
            }
            base.WriteEndElement();
        }

        public void Write38_Subscribe(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("Subscribe", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                base.WriteElementString("eventOperation", "http://tempuri.org/", (string) p[0]);
            }
            if (length > 1)
            {
                List<Guid> list = (List<Guid>) p[1];
                if (list != null)
                {
                    base.WriteStartElement("userDepartmentIds", "http://tempuri.org/", null, false);
                    for (int i = 0; i < list.Count; i++)
                    {
                        base.WriteElementStringRaw("guid", "http://tempuri.org/", XmlConvert.ToString(list[i]));
                    }
                    base.WriteEndElement();
                }
            }
            if (length > 2)
            {
                base.WriteElementStringRaw("clientId", "http://tempuri.org/", XmlConvert.ToString((Guid) p[2]));
            }
            base.WriteEndElement();
        }

        public void Write39_Unsubscribe(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("Unsubscribe", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                base.WriteElementString("eventOperation", "http://tempuri.org/", (string) p[0]);
            }
            base.WriteEndElement();
        }

        private void Write4_AuthDataInfo(string n, string ns, AuthDataInfo o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
            }
            else
            {
                if (!needType && (o.GetType() != typeof(AuthDataInfo)))
                {
                    throw base.CreateUnknownTypeException(o);
                }
                base.WriteStartElement(n, ns, o, false, null);
                if (needType)
                {
                    base.WriteXsiType("AuthDataInfo", "http://tempuri.org/");
                }
                base.WriteElementString("UserId", "http://tempuri.org/", o.UserId);
                base.WriteElementString("SessionId", "http://tempuri.org/", o.SessionId);
                base.WriteEndElement(o);
            }
        }

        public void Write40_AuthUser(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("AuthUser", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                base.WriteElementString("usercode", "http://tempuri.org/", (string) p[0]);
            }
            if (length > 1)
            {
                base.WriteElementString("password", "http://tempuri.org/", (string) p[1]);
            }
            if (length > 2)
            {
                base.WriteElementString("macAddress", "http://tempuri.org/", (string) p[2]);
            }
            if (length > 3)
            {
                base.WriteElementStringRaw("loginTime", "http://tempuri.org/", XmlSerializationWriter.FromDateTime((DateTime) p[3]));
            }
            base.WriteEndElement();
        }

        public void Write41_AuthUserResponse(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("AuthUserResponse", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                this.Write4_AuthDataInfo("AuthUserResult", "http://tempuri.org/", (AuthDataInfo) p[0], false, false);
            }
            base.WriteEndElement();
        }

        public void Write42_GetPermissionPackage(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("GetPermissionPackage", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                base.WriteElementStringRaw("userid", "http://tempuri.org/", XmlConvert.ToString((Guid) p[0]));
            }
            base.WriteEndElement();
        }

        public void Write43_GetPermissionPackageResponse(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("GetPermissionPackageResponse", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                this.Write8_PermissionPackage("GetPermissionPackageResult", "http://tempuri.org/", (PermissionPackage) p[0], false, false);
            }
            base.WriteEndElement();
        }

        public void Write44_SynchronizeToDatabase(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("SynchronizeToDatabase", "http://tempuri.org/", null, false);
            base.WriteEndElement();
        }

        public void Write45_SynchronizeToDatabaseResponse(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("SynchronizeToDatabaseResponse", "http://tempuri.org/", null, false);
            base.WriteEndElement();
        }

        public void Write46_AddWithContextParameter(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("AddWithContextParameter", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                base.WriteElementStringRaw("operationTime", "http://tempuri.org/", XmlSerializationWriter.FromDateTime((DateTime) p[0]));
            }
            if (length > 1)
            {
                base.WriteElementString("content", "http://tempuri.org/", (string) p[1]);
            }
            base.WriteEndElement();
        }

        public void Write47_Add(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("Add", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                base.WriteElementStringRaw("userId", "http://tempuri.org/", XmlConvert.ToString((Guid) p[0]));
            }
            if (length > 1)
            {
                base.WriteElementString("internetIP", "http://tempuri.org/", (string) p[1]);
            }
            if (length > 2)
            {
                base.WriteElementString("intarnetIP", "http://tempuri.org/", (string) p[2]);
            }
            if (length > 3)
            {
                base.WriteElementString("macAddress", "http://tempuri.org/", (string) p[3]);
            }
            if (length > 4)
            {
                base.WriteElementStringRaw("operationTime", "http://tempuri.org/", XmlSerializationWriter.FromDateTime((DateTime) p[4]));
            }
            if (length > 5)
            {
                base.WriteElementString("assemblyNames", "http://tempuri.org/", (string) p[5]);
            }
            if (length > 6)
            {
                base.WriteElementString("functionName", "http://tempuri.org/", (string) p[6]);
            }
            if (length > 7)
            {
                base.WriteElementString("content", "http://tempuri.org/", (string) p[7]);
            }
            if (length > 8)
            {
                base.WriteElementStringRaw("isEnglish", "http://tempuri.org/", XmlConvert.ToString((bool) p[8]));
            }
            base.WriteEndElement();
        }

        public void Write48_BatchAdd(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("BatchAdd", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                base.WriteSerializable((DataTable) p[0], "dt", "http://tempuri.org/", false, true);
            }
            base.WriteEndElement();
        }

        public void Write49_GetAllServices(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("GetAllServices", "http://tempuri.org/", null, false);
            base.WriteEndElement();
        }

        private void Write5_Module(string n, string ns, Module o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
            }
            else
            {
                if (!needType && (o.GetType() != typeof(Module)))
                {
                    throw base.CreateUnknownTypeException(o);
                }
                base.WriteStartElement(n, ns, o, false, null);
                if (needType)
                {
                    base.WriteXsiType("Module", "http://tempuri.org/");
                }
                base.WriteElementStringRaw("OrderNo", "http://tempuri.org/", XmlConvert.ToString(o.OrderNo));
                base.WriteElementString("Assembly", "http://tempuri.org/", o.Assembly);
                base.WriteElementString("UpdateLocation", "http://tempuri.org/", o.UpdateLocation);
                List<string> allowRoles = o.AllowRoles;
                if (allowRoles != null)
                {
                    base.WriteStartElement("AllowRoles", "http://tempuri.org/", null, false);
                    for (int i = 0; i < allowRoles.Count; i++)
                    {
                        base.WriteNullableStringLiteral("string", "http://tempuri.org/", allowRoles[i]);
                    }
                    base.WriteEndElement();
                }
                base.WriteEndElement(o);
            }
        }

        public void Write50_GetAllServicesResponse(object[] p)
        {
            base.WriteStartDocument();
            base.TopLevelElement();
            int length = p.Length;
            base.WriteStartElement("GetAllServicesResponse", "http://tempuri.org/", null, false);
            if (length > 0)
            {
                ServiceInfo[] infoArray = (ServiceInfo[]) p[0];
                if (infoArray != null)
                {
                    base.WriteStartElement("GetAllServicesResult", "http://tempuri.org/", null, false);
                    for (int i = 0; i < infoArray.Length; i++)
                    {
                        this.Write2_ServiceInfo("ServiceInfo", "http://tempuri.org/", infoArray[i], true, false);
                    }
                    base.WriteEndElement();
                }
            }
            base.WriteEndElement();
        }

        private void Write6_UICommand(string n, string ns, UICommand o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
            }
            else
            {
                if (!needType && (o.GetType() != typeof(UICommand)))
                {
                    throw base.CreateUnknownTypeException(o);
                }
                base.WriteStartElement(n, ns, o, false, null);
                if (needType)
                {
                    base.WriteXsiType("UICommand", "http://tempuri.org/");
                }
                base.WriteElementString("Text", "http://tempuri.org/", o.Text);
                base.WriteElementString("Name", "http://tempuri.org/", o.Name);
                base.WriteElementString("Site", "http://tempuri.org/", o.Site);
                base.WriteElementString("RegisterSite", "http://tempuri.org/", o.RegisterSite);
                base.WriteElementString("Type", "http://tempuri.org/", o.Type);
                base.WriteElementStringRaw("Image", "http://tempuri.org/", XmlSerializationWriter.FromByteArrayBase64(o.Image));
                base.WriteElementString("AssemblyName", "http://tempuri.org/", o.AssemblyName);
                base.WriteElementStringRaw("HasPermission", "http://tempuri.org/", XmlConvert.ToString(o.HasPermission));
                base.WriteEndElement(o);
            }
        }

        private void Write7_SimpleRoleInfo(string n, string ns, SimpleRoleInfo o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
            }
            else
            {
                if (!needType && (o.GetType() != typeof(SimpleRoleInfo)))
                {
                    throw base.CreateUnknownTypeException(o);
                }
                base.WriteStartElement(n, ns, o, false, null);
                if (needType)
                {
                    base.WriteXsiType("SimpleRoleInfo", "http://tempuri.org/");
                }
                base.WriteElementStringRaw("ID", "http://tempuri.org/", XmlConvert.ToString(o.ID));
                base.WriteElementString("CName", "http://tempuri.org/", o.CName);
                base.WriteElementString("EName", "http://tempuri.org/", o.EName);
                if (o.CompanyID.HasValue)
                {
                    base.WriteNullableStringLiteralRaw("CompanyID", "http://tempuri.org/", XmlConvert.ToString(o.CompanyID.Value));
                }
                else
                {
                    base.WriteNullTagLiteral("CompanyID", "http://tempuri.org/");
                }
                base.WriteEndElement(o);
            }
        }

        private void Write8_PermissionPackage(string n, string ns, PermissionPackage o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
            }
            else
            {
                int num;
                if (!needType && (o.GetType() != typeof(PermissionPackage)))
                {
                    throw base.CreateUnknownTypeException(o);
                }
                base.WriteStartElement(n, ns, o, false, null);
                if (needType)
                {
                    base.WriteXsiType("PermissionPackage", "http://tempuri.org/");
                }
                base.WriteSerializable(o.AllowActions, "AllowActions", "http://tempuri.org/", false, true);
                base.WriteElementStringRaw("BuildTime", "http://tempuri.org/", XmlSerializationWriter.FromDateTime(o.BuildTime));
                base.WriteElementStringRaw("UserId", "http://tempuri.org/", XmlConvert.ToString(o.UserId));
                List<Module> modules = o.Modules;
                if (modules != null)
                {
                    base.WriteStartElement("Modules", "http://tempuri.org/", null, false);
                    for (num = 0; num < modules.Count; num++)
                    {
                        this.Write5_Module("Module", "http://tempuri.org/", modules[num], true, false);
                    }
                    base.WriteEndElement();
                }
                List<UICommand> commands = o.Commands;
                if (commands != null)
                {
                    base.WriteStartElement("Commands", "http://tempuri.org/", null, false);
                    for (num = 0; num < commands.Count; num++)
                    {
                        this.Write6_UICommand("UICommand", "http://tempuri.org/", commands[num], true, false);
                    }
                    base.WriteEndElement();
                }
                List<SimpleRoleInfo> roleList = o.RoleList;
                if (roleList != null)
                {
                    base.WriteStartElement("RoleList", "http://tempuri.org/", null, false);
                    for (num = 0; num < roleList.Count; num++)
                    {
                        this.Write7_SimpleRoleInfo("SimpleRoleInfo", "http://tempuri.org/", roleList[num], true, false);
                    }
                    base.WriteEndElement();
                }
                base.WriteEndElement(o);
            }
        }

        private string Write9_ApplicationType(ApplicationType v)
        {
            switch (v)
            {
                case ApplicationType.ICP:
                    return "ICP";

                case ApplicationType.EmailCenter:
                    return "EmailCenter";
            }
            long num = (long) v;
            throw base.CreateInvalidEnumValueException(num.ToString(CultureInfo.InvariantCulture), "ICP.Framework.CommonLibrary.Common.ApplicationType");
        }
    }
}
