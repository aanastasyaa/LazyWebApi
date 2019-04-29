using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LazyWebApi.BusinessLogic;
using LazyWebApi.Models;

namespace LazyWebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly GetProductInfoRequestHandler _getProductsInfoRequestHandler;
        private readonly AppendProductRequestHandler _appendProductRequestHandler;
        public ProductsController(GetProductInfoRequestHandler getProductsInfoRequestHandler,
            AppendProductRequestHandler appendProductRequestHandler)
        {
            _getProductsInfoRequestHandler = getProductsInfoRequestHandler;
            _appendProductRequestHandler = appendProductRequestHandler;
        }
        [HttpGet("{id}")]
        public Task<Product> GetProductInfo(Guid id)
        {
            return _getProductsInfoRequestHandler.Handle(id);
        }

        [HttpPost("append")]
        public Task<Product> AppendProduct(Product product)
        {
            return _appendProductRequestHandler.Handle(product);
        } 
    }
}