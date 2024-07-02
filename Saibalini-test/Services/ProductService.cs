using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Saibalini_test.Models;
using System;

namespace Saibalini_test.Services
{
    public class ProductService
    {
        //private readonly ILogger<IndexModel> _logger;
        private readonly Database _database;
        public Product? Product { get; set; }
        public ProductService(Database database)
        {
            _database = database;
        }
        public async Task<List<Product>> GetProductList()
        {
            return await _database.Products.ToListAsync();
        }

        public async Task AddProduct(string name, decimal price, string description)
        {
            Product product = new() { Name = name, Price = price, Description = description };
            _database.Products.Add(product);
            await _database.SaveChangesAsync();
        }
        public async Task EditProduct(int id, string name, decimal price, string description)
        { 
            Product product = GetProductList().Result.FirstOrDefault(i => i.Id == id);
            if(product is not null)
            {
                product.Name = name;
                product.Price = price;
                product.Description = description;
                await _database.SaveChangesAsync();
            }
            
        }

        public async Task DeleteProduct(int id)
        {
            Product product = GetProductList().Result.FirstOrDefault(i => i.Id == id);
            if (product is not null)
            {
                _database.Products.Remove(product);
            }
            await _database.SaveChangesAsync();
        }
    }
}
