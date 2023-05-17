using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using ProductManagementWebClient.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ProductManagementWebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        List<Product> listProducts;
        public HomeController(ILogger<HomeController> logger)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("appication/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7007/api/products";
            listProducts = new List<Product>();
            _logger = logger;
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
            return View(listProducts);
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomProduct customProduct)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("error", "Please fix the errors in the form.");
                return View(customProduct);
            }
            HttpResponseMessage response = await client.PostAsJsonAsync(ProductApiUrl, customProduct);
            if (response.IsSuccessStatusCode)
            {
                // Handle success case
                return RedirectToAction("Index");
            }
            return View(customProduct);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Detail(int id)
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl + "/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Product customProduct = JsonSerializer.Deserialize<Product>(strData, options);
            return View("Detail", customProduct);
        }

        public async Task<ActionResult<CustomProduct>> Edit(int id)
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl + "/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Product customProduct = JsonSerializer.Deserialize<Product>(strData, options);
            return View("Edit", customProduct);
        }

        public IActionResult RedirectToEdit(int id)
        {
            // Assuming the edit action method is named "Edit" in the same controller

            return View("Edit", new { id = id });
        }

        [HttpPut("id")]
        public async Task<ActionResult<CustomProduct>> Edit(int id, CustomProduct customProduct)
        {
            string json = JsonSerializer.Serialize(customProduct);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(ProductApiUrl + "/" + id, content);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Edit", customProduct);
        }
    }
}