using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Saibalini_test.Controllers;
using Saibalini_test.Models;
using Saibalini_test.Services.Abstractions;

namespace TestProduct
{
    public class Tests
    {
        private ProductController _productController;
        private Mock<IProductService> _mockProductService;
        private Mock<Database> _database;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Database>()
            .UseInMemoryDatabase(databaseName: "ProductDatabase")
            .Options;
            _database = new Mock<Database>(options);
            _mockProductService = new Mock<IProductService>();
            _productController = new ProductController(_database.Object, _mockProductService.Object);
        }

        [Test]

        public void GetProductList_ReturnsProductList()
        {
            // Arrange
            int productId = 1;
            var expectedProduct = new List<Product>()
                {
                    new Product {Id = productId, Name = "Test Product", Price=100, Description="test" }
                };
            _mockProductService.Setup(service => service.GetProductList()).Returns(Task.FromResult(expectedProduct));

            // Act
            var result = _productController.Product();

            // Assert
            Assert.IsInstanceOf<JsonResult>(result.Result);
            var jsonResult = result.Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.That(jsonResult.Value, Is.EqualTo(expectedProduct));
        }

        [Test]
        public void AddProductList_ReturnsJsonResult()
        {
            // Arrange
            var product = new Product() { Name = "test-product",Price=100,Description="test-product" };
            var expectedProduct = new Task(() => { });
            _mockProductService.Setup(service => service.AddProduct(product.Name, product.Price, product.Description)).Returns(Task.FromResult(expectedProduct));

            // Act
            var result = _productController.Product(product);

            // Assert
            Assert.IsInstanceOf<JsonResult>(result.Result);
            var jsonResult = result.Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.That(jsonResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }
        [Test]
        public void EditProductList_ReturnsJsonResult()
        {
            // Arrange
            var product = new Product() {Id=1, Name = "test-product", Price = 100, Description = "test-product" };
            var expectedProduct = new Task(() => { });
            _mockProductService.Setup(service => service.EditProduct(product.Id,product.Name, product.Price, product.Description)).Returns(Task.FromResult(expectedProduct));

            // Act
            var result = _productController.SaveProduct(product);

            // Assert
            Assert.IsInstanceOf<JsonResult>(result.Result);
            var jsonResult = result.Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.That(jsonResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }
        [Test]
        public void DeleteProductList_ReturnsJsonResult()
        {
            // Arrange
            var product = new Product() { Id = 1, Name = "test-product", Price = 100, Description = "test-product" };
            var expectedProduct = new Task(() => { });
            _mockProductService.Setup(service => service.DeleteProduct(product.Id)).Returns(Task.FromResult(expectedProduct));

            // Act
            var result = _productController.DeleteProduct(product);

            // Assert
            Assert.IsInstanceOf<JsonResult>(result.Result);
            var jsonResult = result.Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.That(jsonResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

    }
}