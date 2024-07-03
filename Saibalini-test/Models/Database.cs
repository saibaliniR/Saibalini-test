using Microsoft.EntityFrameworkCore;
using System;
namespace Saibalini_test.Models
{

    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options)
            : base(options)
        { }
        public DbSet<Product> Products => Set<Product>();

    }
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
