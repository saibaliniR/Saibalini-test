using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saibalini_test.Models;
using Saibalini_test.Services;
using System.Diagnostics;
using System.Text.Json;

namespace Saibalini_test.Controllers
{
    //[Authorize]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly Database _database;

        public ProductController(ILogger<ProductController> logger, Database database)
        {
            _logger = logger;
            _database = database;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Product()
        {
            var options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = null;
            try
            {
                ProductService ps = new ProductService(_database);
                var productList = await ps.GetProductList();
                return Json(productList, options);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = ex.Message, StackTrace = ex.StackTrace, ExceptionType = "Internal Server Error" }, options)
                {
                    StatusCode = StatusCodes.Status500InternalServerError 
                };
            }
        }
        [HttpPost]
        public async Task<IActionResult> Product([FromBody] Product formProduct)
        {
            var options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = null;
            try
            {
                ProductService ps = new ProductService(_database);
                await ps.AddProduct(formProduct.Name, formProduct.Price, formProduct.Description);
                return new JsonResult(new { Message = "Product Added Successfully"}, options)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = ex.Message, StackTrace = ex.StackTrace, ExceptionType = "Internal Server Error" }, options)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
