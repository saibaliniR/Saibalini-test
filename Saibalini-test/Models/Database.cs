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
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();

    }
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Role? Role { get; set; }
    }
    public class Role
    {
        public int RoleId { get; set; }
        public required string Name { get; set; }
        public IEnumerable<User>? Users { get; set; }
    }
}
