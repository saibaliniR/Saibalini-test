using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saibalini_test.Models;
using Saibalini_test.Services;
using Saibalini_test.Services.Abstractions;
using System.Diagnostics;
using System.Text.Json;

namespace Saibalini_test.Controllers
{
    //[Authorize]
    public class ProductController : Controller
    {
        private readonly Database _database;
        private readonly IProductService _productService;

        public ProductController(Database database, IProductService productService)
        {
            _database = database;
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Product()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = null;
            try
            {
                List<Product> productList = await _productService.GetProductList();
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
                await _productService.AddProduct(formProduct.Name, formProduct.Price, formProduct.Description);
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
        [HttpPut]
        public async Task<IActionResult> SaveProduct([FromBody] Product formProduct)
        {
            var options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = null;
            try
            {
                await _productService.EditProduct(formProduct.Id, formProduct.Name, formProduct.Price, formProduct.Description);
                return new JsonResult(new { Message = "Product Saved Successfully" }, options)
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

        [HttpPost]
        public async Task<IActionResult> DeleteProduct([FromBody] Product formProduct)
        {
            var options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = null;
            try
            {
                await _productService.DeleteProduct(Convert.ToInt32(formProduct.Id));
                return new JsonResult(new { Message = "Product deleted Successfully" }, options)
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
