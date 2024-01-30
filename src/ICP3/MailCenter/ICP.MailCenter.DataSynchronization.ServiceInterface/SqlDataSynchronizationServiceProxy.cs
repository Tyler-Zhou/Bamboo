using System;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Synchronization.Data;

namespace ICP.DataSynchronization.ServiceInterface
{
    public class SqlDataSynchronizationServiceProxy : RelationalProviderProxy
    {
        ISqlDataSynchronizationService dbProxy;
        private Guid userId;
        public SqlDataSynchronizationServiceProxy(String scopeName, Guid userId)
            : base(scopeName, userId)
        {
            this.userId = userId;
        }

        protected override void CreateProxy()
        {
            SessionCustomHttpBinding binding = new SessionCustomHttpBinding();
            binding.OpenTimeout = binding.ReceiveTimeout = binding.SendTimeout = TimeSpan.FromMinutes(10);
            binding.CloseTimeout = TimeSpan.FromHours(14);
            string endpointUri = string.Format("{0}/{1}", ClientHelper.GetAppSettingValue(ClientConstants.ServiceBaseAddressKey), "SqlDataSynchronizationService");
            ChannelFactory<ISqlDataSynchronizationService> factory = new ChannelFactory<ISqlDataSynchronizationService>(binding, new EndpointAddress(endpointUri));
            base.proxy = factory.CreateChannel();
            this.dbProxy = base.proxy as ISqlDataSynchronizationService;
        }

        public DbSyncScopeDescription GetScopeDescription()
        {
            return this.dbProxy.GetScopeDescription();
        }
    }
}
