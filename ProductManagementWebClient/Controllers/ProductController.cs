using System.Net.Http.Headers;
using System.Text.Json;
using AutoMapper;
using BusinessObjects;
using BusinessObjects.Dtos;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace ProductManagementWebClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        private IProductRepository productsRepository = new ProductRepository();

        private string ProductApiUrl = "";

        public ProductController()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7189/api/products";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Product> products = JsonSerializer.Deserialize<List<Product>>(strData,options);
            return View(products);
        }

        public IActionResult Create()
        {
            List<Category> categories = productsRepository.GetCategories();
            ViewData["Category"] = categories;
            return View();
        }

        public IActionResult AddProduct([FromForm] Product product)
        {
            productsRepository.SaveProduct(product);
            return Redirect("/Product/Index");
        }

        public IActionResult EditProduct([FromForm] Product product)
        {
            productsRepository.UpdateProduct(product);
            return Redirect("/Product/Index");
        }

        public IActionResult Edit(int id)
        {
            var product = productsRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            List<Category> categories = productsRepository.GetCategories();
            ViewData["Category"] = categories;
            return View(product);
        }

        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Product> products = JsonSerializer.Deserialize<List<Product>>(strData, options);
            Product product = products.FirstOrDefault(p => p.ProductId == id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);

        }

        public IActionResult Delete(int id)
        {
            var product = productsRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            productsRepository.DeleteProduct(product);
            return Redirect("/Product/Index");
        }
    }
}
