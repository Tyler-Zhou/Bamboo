namespace Microsoft.Xml.Serialization.GeneratedAssembly
{
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Server;
    using System;
    using System.Collections;
    using System.Xml.Serialization;

    public class XmlSerializerContract : XmlSerializerImplementation
    {
        private Hashtable readMethods = null;
        private Hashtable typedSerializers = null;
        private Hashtable writeMethods = null;

        public override bool CanSerialize(Type type)
        {
            return (
                (type == typeof(ISessionService))
                || ((type == typeof(IMembershipService))
                || ((type == typeof(ISystemErrorLogService))
                || ((type == typeof(ILoginUserInfoService))
                || ((type == typeof(ISubscriptionService))
                || ((type == typeof(IAuthenticateService))
                || ((type == typeof(IFrameworkInitializeService))
                || ((type == typeof(IColumnTemplateSynchronizeService))
                || ((type == typeof(IOperationLogService))
                || (type == typeof(IFrameworkSystemService)
               ))))))))));
        }

        public override XmlSerializer GetSerializer(Type type)
        {
            return null;
        }

        public override XmlSerializationReader Reader
        {
            get
            {
                return new XmlSerializationReader1();
            }
        }

        public override Hashtable ReadMethods
        {
            get
            {
                if (this.readMethods == null)
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable["ICP.Framework.CommonLibrary.Server.ISessionService:Void UnloadSession(System.String, System.DateTime):Request"] = "Read24_UnloadSession";
                    hashtable["ICP.Framework.CommonLibrary.Server.ISessionService:Void UnloadSession(System.String, System.DateTime):Response"] = "Read25_UnloadSessionResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:System.String GetUserDisplayName(System.String):Request"] = "Read26_GetUserDisplayName";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:System.String GetUserDisplayName(System.String):Response"] = "Read27_GetUserDisplayNameResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:Void ChangePassword(System.String, System.String, System.String):Request"] = "Read28_ChangePassword";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:Void ChangePassword(System.String, System.String, System.String):Response"] = "Read29_ChangePasswordResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:System.String ValidateUser(System.String, System.String, System.String):Request"] = "Read30_ValidateUser";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:System.String ValidateUser(System.String, System.String, System.String):Response"] = "Read31_ValidateUserResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:System.String[] GetAllFunctionsFromUser(System.String):Request"] = "Read32_GetAllFunctionsFromUser";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:System.String[] GetAllFunctionsFromUser(System.String):Response"] = "Read33_Item";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:Void Log(System.Guid, System.String, System.String, System.String, System.DateTime, System.String):Request"] = "Read34_Log";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:Void Log(System.Guid, System.String, System.String, System.String, System.DateTime, System.String):Response"] = "Read35_LogResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:System.String[] GetRolesForUser(System.String):Request"] = "Read36_GetRolesForUser";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:System.String[] GetRolesForUser(System.String):Response"] = "Read37_GetRolesForUserResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.ISystemErrorLogService:Void Save(System.Guid, System.String, System.String, Byte[], System.String, System.DateTime):Request"] = "Read38_Save";
                    hashtable["ICP.Framework.CommonLibrary.Common.ILoginUserInfoService:ICP.Framework.CommonLibrary.Common.LoginUserInfo Get():Request"] = "Read39_Get";
                    hashtable["ICP.Framework.CommonLibrary.Common.ILoginUserInfoService:ICP.Framework.CommonLibrary.Common.LoginUserInfo Get():Response"] = "Read40_GetResponse";
                    hashtable["ICP.Framework.CommonLibrary.Common.ISubscriptionService:Void Subscribe(System.String, System.Collections.Generic.List`1[System.Guid], System.Guid):Request"] = "Read41_Subscribe";
                    hashtable["ICP.Framework.CommonLibrary.Common.ISubscriptionService:Void Unsubscribe(System.String):Request"] = "Read42_Unsubscribe";
                    hashtable["ICP.Framework.CommonLibrary.Server.IAuthenticateService:ICP.Framework.CommonLibrary.Common.AuthDataInfo AuthUser(System.String, System.String, System.String, System.DateTime):Request"] = "Read43_AuthUser";
                    hashtable["ICP.Framework.CommonLibrary.Server.IAuthenticateService:ICP.Framework.CommonLibrary.Common.AuthDataInfo AuthUser(System.String, System.String, System.String, System.DateTime):Response"] = "Read44_AuthUserResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.IAuthenticateService:ICP.Framework.CommonLibrary.Server.PermissionPackage GetPermissionPackage(System.Guid):Request"] = "Read45_GetPermissionPackage";
                    hashtable["ICP.Framework.CommonLibrary.Server.IAuthenticateService:ICP.Framework.CommonLibrary.Server.PermissionPackage GetPermissionPackage(System.Guid):Response"] = "Read46_GetPermissionPackageResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.IColumnTemplateSynchronizeService:Void SynchronizeToDatabase():Request"] = "Read47_SynchronizeToDatabase";
                    hashtable["ICP.Framework.CommonLibrary.Server.IColumnTemplateSynchronizeService:Void SynchronizeToDatabase():Response"] = "Read48_SynchronizeToDatabaseResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.IOperationLogService:Void Add(System.DateTime, System.String):Request"] = "Read49_AddWithContextParameter";
                    hashtable["ICP.Framework.CommonLibrary.Server.IOperationLogService:Void Add(System.Guid, System.String, System.String, System.String, System.DateTime, System.String, System.String, System.String, Boolean):Request"] = "Read50_Add";
                    hashtable["ICP.Framework.CommonLibrary.Server.IOperationLogService:Void BatchAdd(System.Data.DataTable):Request"] = "Read51_BatchAdd";
                    hashtable["ICP.Framework.CommonLibrary.Server.IFrameworkSystemService:ICP.Framework.CommonLibrary.Server.ServiceInfo[] GetAllServices():Request"] = "Read52_GetAllServices";
                    hashtable["ICP.Framework.CommonLibrary.Server.IFrameworkSystemService:ICP.Framework.CommonLibrary.Server.ServiceInfo[] GetAllServices():Response"] = "Read53_GetAllServicesResponse";
                    if (this.readMethods == null)
                    {
                        this.readMethods = hashtable;
                    }
                }
                return this.readMethods;
            }
        }

        public override Hashtable TypedSerializers
        {
            get
            {
                if (this.typedSerializers == null)
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IMembershipService:System.String[] GetRolesForUser(System.String):Request", new ArrayOfObjectSerializer12());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IMembershipService:System.String[] GetAllFunctionsFromUser(System.String):Request", new ArrayOfObjectSerializer8());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IMembershipService:System.String[] GetAllFunctionsFromUser(System.String):Response", new ArrayOfObjectSerializer9());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IFrameworkSystemService:ICP.Framework.CommonLibrary.Server.ServiceInfo[] GetAllServices():Request", new ArrayOfObjectSerializer28());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IMembershipService:Void ChangePassword(System.String, System.String, System.String):Response", new ArrayOfObjectSerializer5());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IMembershipService:Void ChangePassword(System.String, System.String, System.String):Request", new ArrayOfObjectSerializer4());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IMembershipService:Void Log(System.Guid, System.String, System.String, System.String, System.DateTime, System.String):Request", new ArrayOfObjectSerializer10());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.ISessionService:Void UnloadSession(System.String, System.DateTime):Response", new ArrayOfObjectSerializer1());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.ISessionService:Void UnloadSession(System.String, System.DateTime):Request", new ArrayOfObjectSerializer());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IMembershipService:System.String ValidateUser(System.String, System.String, System.String):Response", new ArrayOfObjectSerializer7());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IOperationLogService:Void BatchAdd(System.Data.DataTable):Request", new ArrayOfObjectSerializer27());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IAuthenticateService:ICP.Framework.CommonLibrary.Server.PermissionPackage GetPermissionPackage(System.Guid):Request", new ArrayOfObjectSerializer21());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IAuthenticateService:ICP.Framework.CommonLibrary.Server.PermissionPackage GetPermissionPackage(System.Guid):Response", new ArrayOfObjectSerializer22());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IColumnTemplateSynchronizeService:Void SynchronizeToDatabase():Request", new ArrayOfObjectSerializer23());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IAuthenticateService:ICP.Framework.CommonLibrary.Common.AuthDataInfo AuthUser(System.String, System.String, System.String, System.DateTime):Response", new ArrayOfObjectSerializer20());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IFrameworkSystemService:ICP.Framework.CommonLibrary.Server.ServiceInfo[] GetAllServices():Response", new ArrayOfObjectSerializer29());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IMembershipService:System.String ValidateUser(System.String, System.String, System.String):Request", new ArrayOfObjectSerializer6());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IColumnTemplateSynchronizeService:Void SynchronizeToDatabase():Response", new ArrayOfObjectSerializer24());
                    hashtable.Add("ICP.Framework.CommonLibrary.Common.ISubscriptionService:Void Unsubscribe(System.String):Request", new ArrayOfObjectSerializer18());
                    hashtable.Add("ICP.Framework.CommonLibrary.Common.ISubscriptionService:Void Subscribe(System.String, System.Collections.Generic.List`1[System.Guid], System.Guid):Request", new ArrayOfObjectSerializer17());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IAuthenticateService:ICP.Framework.CommonLibrary.Common.AuthDataInfo AuthUser(System.String, System.String, System.String, System.DateTime):Request", new ArrayOfObjectSerializer19());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IMembershipService:System.String GetUserDisplayName(System.String):Response", new ArrayOfObjectSerializer3());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IMembershipService:Void Log(System.Guid, System.String, System.String, System.String, System.DateTime, System.String):Response", new ArrayOfObjectSerializer11());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IOperationLogService:Void Add(System.DateTime, System.String):Request", new ArrayOfObjectSerializer25());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IMembershipService:System.String GetUserDisplayName(System.String):Request", new ArrayOfObjectSerializer2());
                    hashtable.Add("ICP.Framework.CommonLibrary.Common.ILoginUserInfoService:ICP.Framework.CommonLibrary.Common.LoginUserInfo Get():Request", new ArrayOfObjectSerializer15());
                    hashtable.Add("ICP.Framework.CommonLibrary.Common.ILoginUserInfoService:ICP.Framework.CommonLibrary.Common.LoginUserInfo Get():Response", new ArrayOfObjectSerializer16());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.ISystemErrorLogService:Void Save(System.Guid, System.String, System.String, Byte[], System.String, System.DateTime):Request", new ArrayOfObjectSerializer14());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IOperationLogService:Void Add(System.Guid, System.String, System.String, System.String, System.DateTime, System.String, System.String, System.String, Boolean):Request", new ArrayOfObjectSerializer26());
                    hashtable.Add("ICP.Framework.CommonLibrary.Server.IMembershipService:System.String[] GetRolesForUser(System.String):Response", new ArrayOfObjectSerializer13());
                    if (this.typedSerializers == null)
                    {
                        this.typedSerializers = hashtable;
                    }
                }
                return this.typedSerializers;
            }
        }

        public override Hashtable WriteMethods
        {
            get
            {
                if (this.writeMethods == null)
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable["ICP.Framework.CommonLibrary.Server.ISessionService:Void UnloadSession(System.String, System.DateTime):Request"] = "Write21_UnloadSession";
                    hashtable["ICP.Framework.CommonLibrary.Server.ISessionService:Void UnloadSession(System.String, System.DateTime):Response"] = "Write22_UnloadSessionResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:System.String GetUserDisplayName(System.String):Request"] = "Write23_GetUserDisplayName";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:System.String GetUserDisplayName(System.String):Response"] = "Write24_GetUserDisplayNameResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:Void ChangePassword(System.String, System.String, System.String):Request"] = "Write25_ChangePassword";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:Void ChangePassword(System.String, System.String, System.String):Response"] = "Write26_ChangePasswordResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:System.String ValidateUser(System.String, System.String, System.String):Request"] = "Write27_ValidateUser";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:System.String ValidateUser(System.String, System.String, System.String):Response"] = "Write28_ValidateUserResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:System.String[] GetAllFunctionsFromUser(System.String):Request"] = "Write29_GetAllFunctionsFromUser";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:System.String[] GetAllFunctionsFromUser(System.String):Response"] = "Write30_Item";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:Void Log(System.Guid, System.String, System.String, System.String, System.DateTime, System.String):Request"] = "Write31_Log";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:Void Log(System.Guid, System.String, System.String, System.String, System.DateTime, System.String):Response"] = "Write32_LogResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:System.String[] GetRolesForUser(System.String):Request"] = "Write33_GetRolesForUser";
                    hashtable["ICP.Framework.CommonLibrary.Server.IMembershipService:System.String[] GetRolesForUser(System.String):Response"] = "Write34_GetRolesForUserResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.ISystemErrorLogService:Void Save(System.Guid, System.String, System.String, Byte[], System.String, System.DateTime):Request"] = "Write35_Save";
                    hashtable["ICP.Framework.CommonLibrary.Common.ILoginUserInfoService:ICP.Framework.CommonLibrary.Common.LoginUserInfo Get():Request"] = "Write36_Get";
                    hashtable["ICP.Framework.CommonLibrary.Common.ILoginUserInfoService:ICP.Framework.CommonLibrary.Common.LoginUserInfo Get():Response"] = "Write37_GetResponse";
                    hashtable["ICP.Framework.CommonLibrary.Common.ISubscriptionService:Void Subscribe(System.String, System.Collections.Generic.List`1[System.Guid], System.Guid):Request"] = "Write38_Subscribe";
                    hashtable["ICP.Framework.CommonLibrary.Common.ISubscriptionService:Void Unsubscribe(System.String):Request"] = "Write39_Unsubscribe";
                    hashtable["ICP.Framework.CommonLibrary.Server.IAuthenticateService:ICP.Framework.CommonLibrary.Common.AuthDataInfo AuthUser(System.String, System.String, System.String, System.DateTime):Request"] = "Write40_AuthUser";
                    hashtable["ICP.Framework.CommonLibrary.Server.IAuthenticateService:ICP.Framework.CommonLibrary.Common.AuthDataInfo AuthUser(System.String, System.String, System.String, System.DateTime):Response"] = "Write41_AuthUserResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.IAuthenticateService:ICP.Framework.CommonLibrary.Server.PermissionPackage GetPermissionPackage(System.Guid):Request"] = "Write42_GetPermissionPackage";
                    hashtable["ICP.Framework.CommonLibrary.Server.IAuthenticateService:ICP.Framework.CommonLibrary.Server.PermissionPackage GetPermissionPackage(System.Guid):Response"] = "Write43_GetPermissionPackageResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.IColumnTemplateSynchronizeService:Void SynchronizeToDatabase():Request"] = "Write44_SynchronizeToDatabase";
                    hashtable["ICP.Framework.CommonLibrary.Server.IColumnTemplateSynchronizeService:Void SynchronizeToDatabase():Response"] = "Write45_SynchronizeToDatabaseResponse";
                    hashtable["ICP.Framework.CommonLibrary.Server.IOperationLogService:Void Add(System.DateTime, System.String):Request"] = "Write46_AddWithContextParameter";
                    hashtable["ICP.Framework.CommonLibrary.Server.IOperationLogService:Void Add(System.Guid, System.String, System.String, System.String, System.DateTime, System.String, System.String, System.String, Boolean):Request"] = "Write47_Add";
                    hashtable["ICP.Framework.CommonLibrary.Server.IOperationLogService:Void BatchAdd(System.Data.DataTable):Request"] = "Write48_BatchAdd";
                    hashtable["ICP.Framework.CommonLibrary.Server.IFrameworkSystemService:ICP.Framework.CommonLibrary.Server.ServiceInfo[] GetAllServices():Request"] = "Write49_GetAllServices";
                    hashtable["ICP.Framework.CommonLibrary.Server.IFrameworkSystemService:ICP.Framework.CommonLibrary.Server.ServiceInfo[] GetAllServices():Response"] = "Write50_GetAllServicesResponse";
                    if (this.writeMethods == null)
                    {
                        this.writeMethods = hashtable;
                    }
                }
                return this.writeMethods;
            }
        }

        public override XmlSerializationWriter Writer
        {
            get
            {
                return new XmlSerializationWriter1();
            }
        }
    }
}
