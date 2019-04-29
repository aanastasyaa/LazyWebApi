using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LazyWebApi.BusinessLogic;
using LazyWebApi.Commands;
using LazyWebApi.Customers;
using LazyWebApi.Services;
using LazyWebApi.Services.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LazyWebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<GetProductInfoRequestHandler>();
            services.AddScoped<AppendProductRequestHandler>();
            services.AddScoped<IProductInfoService, ProductInfoService>();

            services.AddScoped<AppendProductConsumer>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<AppendProductConsumer>();
                x.AddBus(provider => MassTransit.Bus.Factory.CreateUsingInMemory(cfg =>
                {
                    cfg.ReceiveEndpoint("append-product-queue", ep =>
                    {
                        ep.ConfigureConsumer<AppendProductConsumer>(provider);
                        EndpointConvention.Map<AppendProductCommand>(ep.InputAddress);
                    });
                }));

                x.AddRequestClient<AppendProductCommand>();
            });

            services.AddSingleton<IHostedService, BusService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.Extensions.Hosting.IHostingEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
			app.UseMvc();
        }
    }
}
