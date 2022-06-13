using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Bars.Entities.Dto;
using Bars.Infrasctucture.Entities;
using Bars.Infrasctucture.Enums;
using Bars.Infrasctucture.Interfaces;

namespace Bars.Business.Extensions
{
    public static class WCFExtension
    {
        private static readonly TimeSpan DebugTimeout = TimeSpan.FromMinutes(20);

        #region Methods

        public static TResult WcfInvoke<TContract, TResult>
            (this ChannelFactory<TContract> factory, Func<TContract, TResult> action)
        {
            var client = factory.CreateChannel();
            var clientInstance = (IClientChannel)client;

            try
            {
                var result = action(client);
                clientInstance.Close();
                return result;
            }
            catch (Exception)
            {
                clientInstance.Abort();
                throw;
            }
        }

        public static OperationResult<TResult> WcfInvoke<TContract, TResult>
            (this Func<TContract, TResult> wcfAction) where TContract : IService
        {
            var binding = new NetTcpBinding();
            var settings = new ServiceSettings().SetDefaults(typeof(TContract).Name);
            binding.ApplyDebugTimeouts();
            var factory = new ChannelFactory<TContract>( binding, new EndpointAddress(settings.NetTcpAddress));
            factory.Open();
            try
            {
                var result = factory.WcfInvoke(wcfAction);
                return new OperationResult<TResult>(ResultCode.Success, result);
            }
            catch (Exception ex)
            {
                return new OperationResult<TResult>(ResultCode.Failure, ex.Message, default);
            }
            finally
            {
                factory.Close();
            }
        }

        public static void ApplyDebugTimeouts(this Binding binding, TimeSpan debugTimeout = default)
        {
            if (Debugger.IsAttached)
            {
                debugTimeout = default == debugTimeout ? DebugTimeout : debugTimeout;
                binding.OpenTimeout =
                    binding.CloseTimeout =
                    binding.SendTimeout =
                    binding.ReceiveTimeout = debugTimeout;
            }
        }

        #endregion
    }
}
