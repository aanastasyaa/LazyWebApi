using LazyWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LazyWebApi.Services.Interfaces
{
    public interface IProductInfoService
    {
        Task<Product> GetById(Guid id);
        Task<IActionResult> AppendProduct(Product product);
    }
}
