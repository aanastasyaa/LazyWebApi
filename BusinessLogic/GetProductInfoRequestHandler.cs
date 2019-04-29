using LazyWebApi.Models;
using LazyWebApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LazyWebApi.BusinessLogic
{
    public class GetProductInfoRequestHandler
    {
        private readonly IProductInfoService _productInfoService;
        
        public GetProductInfoRequestHandler(IProductInfoService productInfoService)
        {
            _productInfoService = productInfoService;
        }

        public async Task<Product> Handle(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Некорректный идентификатор пользователя", nameof(id));
            }
            try
            {
                return await _productInfoService.GetById(id);
            }
            catch(InvalidOperationException)
            {
                throw new InvalidOperationException("Такой идентификатор отсутствует в БД");
            }
        }

        public async Task<IEnumerable<Product>> HandleAll()
        {
            return await _productInfoService.GetAll();
        }
    }
}
