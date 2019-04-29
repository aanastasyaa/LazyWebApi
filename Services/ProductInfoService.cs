using LazyWebApi.Models;
using LazyWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LazyWebApi.Services
{
    public class ProductInfoService : IProductInfoService
    {
        private const string ConnectionString = "host=89.208.199.118;port=5432;database=PostgreSQL-2564;username=student;password=password";
        public async Task<IActionResult> AppendProduct(Product product)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                "INSERT INTO schema007.products (id, name, category) VALUES (@id, @name, @category)", product);
            }
            return new OkResult();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return await connection.QueryAsync<Product>("SELECT *FROM schema007.products ");
            }
        }

        public async Task<Product> GetById(Guid id)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return await connection.QuerySingleAsync<Product>(
                "SELECT *FROM schema007.products WHERE id= @id", new { id });
            }
        }
    }
}
