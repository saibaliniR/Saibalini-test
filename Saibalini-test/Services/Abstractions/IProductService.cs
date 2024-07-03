using Saibalini_test.Models;

namespace Saibalini_test.Services.Abstractions
{
    public interface IProductService
    {
        Task<List<Product>> GetProductList();
        Task AddProduct(string name, decimal price, string description);
        Task EditProduct(int id, string name, decimal price, string description);
        Task DeleteProduct(int id);
    }
}
