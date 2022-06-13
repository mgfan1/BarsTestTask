using CoreWCF;
using CoreWCF.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Bars.Entities.Dto;
using Bars.Entities.Interfaces;
using Bars.Service.Components;

namespace Bars.Service
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceModelServices()
                    .AddServiceModelMetadata();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseServiceModel(builder =>
            {
                void ConfigureSoapService<TService, TContract>(string prefix) where TService : class
                {
                    var settings = new ServiceSettings().SetDefaults(prefix);
                    builder.AddService<TService>()
                        .AddServiceEndpoint<TService, TContract>(new NetTcpBinding(),
                            settings.NetTcpAddress.LocalPath);
                }

                ConfigureSoapService<ContractService, IContractsService>(nameof(IContractsService));
            });
        }
    }

}
