using Microsoft.AspNetCore.Mvc;
using MVCWebCallAPI.Models;
using Newtonsoft.Json;

namespace MVCWebCallAPI.Controllers
{
    public class CategoryController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5289/api");
        private readonly HttpClient _client;
        public CategoryController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<CategoryViewModel> categoryList = new List<CategoryViewModel>();
            List<ProductViewModel> productList = new List<ProductViewModel>();
            HttpResponseMessage response1 = _client.GetAsync(baseAddress + "/Category").Result;
            HttpResponseMessage response2 = _client.GetAsync(baseAddress + "/Product").Result;
            if(response1.IsSuccessStatusCode && response2.IsSuccessStatusCode)
            {
                string data1 = response1.Content.ReadAsStringAsync().Result;
                categoryList = JsonConvert.DeserializeObject<List<CategoryViewModel>>(data1);
                string data2 = response2.Content.ReadAsStringAsync().Result;
                productList  = JsonConvert.DeserializeObject<List<ProductViewModel>>(data2);
                ViewBag.category = categoryList;
                ViewBag.product = productList;
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryViewModel model)
        {
            try
            {
                HttpResponseMessage response = _client.PostAsJsonAsync(baseAddress + "/Category", model).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            ProductViewModel product = new ProductViewModel();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/Product/id?id=" + id).Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<ProductViewModel>(data);
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult UpdateProduct(ProductViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                HttpResponseMessage response = _client.PutAsJsonAsync(baseAddress + "/Product", model).Result;
                if (response.IsSuccessStatusCode) return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(model);
                throw;
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            HttpResponseMessage response = _client.DeleteAsync(baseAddress + "/Product?id="+id).Result;
            if(response.IsSuccessStatusCode)
            {
                TempData["success"] = "Delete success!";
                return RedirectToAction("Index");
            }
            TempData["failed"] = "Delete Failed!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductViewModel model) 
        {
            string data = JsonConvert.SerializeObject(model);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(baseAddress + "/Product",content).Result;
            if(response.IsSuccessStatusCode)
            {
                TempData["success"] = "Product successfully created!";
                return RedirectToAction("Index");
            }
            TempData["failed"] = "Check input fields again!";
            return View(model);
        }
    }
}
