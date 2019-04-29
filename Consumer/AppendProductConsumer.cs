using LazyWebApi.Commands;
using LazyWebApi.Services.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LazyWebApi.Customers
{
    public class AppendProductConsumer : IConsumer<AppendProductCommand>
    {
        private readonly IProductInfoService _productInfoService;

        public AppendProductConsumer(IProductInfoService productInfoService)
        {
            _productInfoService = productInfoService;
        }
        public async Task Consume(ConsumeContext<AppendProductCommand> context)
        {
           await _productInfoService.AppendProduct(context.Message.Product);
        }
    }
}
