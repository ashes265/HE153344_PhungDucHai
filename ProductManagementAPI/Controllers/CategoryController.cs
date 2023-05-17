using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace ProductManagementAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository repository = new CategoryRepository();
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories() => repository.GetCategories();
        [HttpPost]
        public IActionResult PostCategory(Category category)
        {
            repository.SaveCategory(category);
            return NoContent();
        }
        [HttpGet("id")]
        public ActionResult<Category> GetCategorieById(int id) => repository.GetCategoryById(id);

    }
}
