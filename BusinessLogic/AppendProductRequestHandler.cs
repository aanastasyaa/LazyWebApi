using LazyWebApi.Commands;
using LazyWebApi.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LazyWebApi.BusinessLogic
{
    public class AppendProductRequestHandler
    {
        private readonly IBus _bus;

        public AppendProductRequestHandler(IBus bus)
        {
            _bus = bus;
        }

        public Task<Product> Handle(Product product)
        {
            Guid guid = Guid.NewGuid();
            product.Id = guid;

            _bus.Send(new AppendProductCommand()
            {
                Product = product
            });

            return Task.FromResult<Product>(product);
        }
    }

}
