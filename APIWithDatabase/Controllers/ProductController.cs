using APIWithDatabase.Data;
using APIWithDatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIWithDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MySaleDBContext _db;

        public static List<Product> _data { get; set; }

        public ProductController(MySaleDBContext dbContext)
        {
            _db = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(_db.Products.ToList());
        }

        [HttpGet("id")]
        public IActionResult GetProductById(int ID)
        {
            var findItem = _db.Products.Find(ID);
            return Ok(findItem);
        }

        [HttpPost]
        public IActionResult AddProduct(AddProduct item)
        {
            if(item == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            try
            {
                Product newP = new Product()
                {
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    UnitsInStock = item.UnitsInStock,
                    Image = item.Image,
                    CategoryId = item.CategoryId
                };
                _db.Products.Add(newP);
                _db.SaveChanges();
                return Ok(newP);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var check = _db.Products.Find(id);
            _db.Products.Remove(check);
            _db.SaveChanges();
            return Ok();
        }
    }
}
