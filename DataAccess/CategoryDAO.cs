using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var listCategories = new List<Category>();
            try
            {
                using (var context = new ConnectDB())
                {
                    listCategories = context.Categories.ToList();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return listCategories;
        }

        public static void SaveCate(Category product)
        {
            try
            {
                using (var context = new ConnectDB())
                {
                    context.Categories.Add(product);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);

            }
        }

        public static Category FindCategoryById(int categoryId)
        {
            Category category = new Category();
            try
            {
                using (var context = new ConnectDB())
                {
                    category = context.Categories.SingleOrDefault(p => p.CategoryId == categoryId);
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return category;
        }
    }
}