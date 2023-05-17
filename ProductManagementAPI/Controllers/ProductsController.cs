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
        public IActionResult PostProduct(CustomProduct product)
        {
            Product forSave = new Product
            {
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                CategoryId = product.CategoryId,
                UnitsInStock = product.UnitsInStock,
            };
            repository.SaveProduct(forSave);
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
        public IActionResult UpdateProduct(int id, CustomProduct product)
        {
            Product result = repository.GetProductById(id);
            if (result == null) return NotFound();
            result.ProductName = product.ProductName;
            result.UnitPrice = product.UnitPrice;
            result.CategoryId = product.CategoryId;
            result.UnitsInStock = product.UnitsInStock;
            repository.UpdateProduct(result);
            return NoContent();
        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = repository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
    }
}
