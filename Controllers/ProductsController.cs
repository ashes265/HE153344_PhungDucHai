using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace Lab1.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository repository = new ProductRepository();
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts() => repository.GetProducts();
        [HttpPost]
        public IActionResult PostProduct(Product product)
        {
            repository.SaveProduct(product);
            return NoContent();
        }
        [HttpDelete("id")]
        public IActionResult DeleteProduct(int id)
        {
            var product = repository.GetProductById(id);
            if (product == null) return NotFound();
            repository.DeleteProduct(product);
            return NoContent();
        }
        [HttpPut("id")]
        public IActionResult UpdateProduct(int id, Product p)
        {
            var product = repository.GetProductById(id);
            if(product == null) return NotFound();
            repository.UpdateProduct(p);
            return NoContent();
        }
    }
}
