using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProductManagementWebClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        List<Product> listProducts;
        public ProductController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("appication/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "http://localhost:7007/api/products";
            listProducts = new List<Product>();
        }
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            listProducts = JsonSerializer.Deserialize<List<Product>>(strData, options);
            ViewData["List"] = listProducts;
            return View(listProducts);
        }

        //public ActionResult Details(int id)
        //{

        //}
        //public ActionResult Create()
        //{

        //}

        //public ActionResult Edit()
        //{

        //}
    }
}
