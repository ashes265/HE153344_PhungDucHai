using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICategoryRepository
    {
        void SaveCategory(Category category);
        List<Category> GetCategories();
        Category GetCategoryById(int id);
    }
}
