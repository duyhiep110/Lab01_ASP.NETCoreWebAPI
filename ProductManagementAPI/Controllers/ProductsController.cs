using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository productsRepository = new ProductRepository();

        //Get: api/Products
        [HttpGet]
        public List<Product> GetProducts() => productsRepository.GetProducts();
        

        //Post :ProductsController/Products
        [HttpPost]
        public IActionResult PostProduct(Product product)
        {
            productsRepository.SaveProduct(product);
            return NoContent();
        }

        //Get: ProductsController/Delete/5
        [HttpDelete("id")]
        public IActionResult DeleteProduct(int id)
        {
            var product = productsRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            productsRepository.DeleteProduct(product);
            return NoContent();
        }


        //Get: ProductsController/Delete/5
        [HttpPut("id")]
        public IActionResult UpdateProduct(int id,Product product)
        {
            var p = productsRepository.GetProductById(id);
            if(p == null)
            {
                return NotFound();
            }
            productsRepository.UpdateProduct(product);
            return NoContent();
        }

    }
}
