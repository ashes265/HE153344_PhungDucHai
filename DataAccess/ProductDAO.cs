using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            var listProducts = new List<Product>();
            try
            {
                using (var context = new ConnectDB())
                {
                    listProducts = context.Products.ToList();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return listProducts;
        }
        public static Product FindProductById(int productId)
        {
            Product product = new Product();
            try
            {
                using (var context = new ConnectDB())
                {
                    product = context.Products.SingleOrDefault(p => p.ProductId == productId);
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return product;
        }
        public static void SaveProduct(Product product)
        {
            try
            {
                using (var context = new ConnectDB())
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public static void UpdateProduct(Product product)
        {
            try
            {
                using (var context = new ConnectDB())
                {
                    context.Entry<Product>(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public static void DeleteProduct(Product product)
        {
            try
            {
                using (var context = new ConnectDB())
                {
                    var productDelete = context.Products.SingleOrDefault(p => p.ProductId == product.ProductId);
                    context.Products.Remove(productDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

    }
}
