using APIWithDatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIWithDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MySaleDBContext _db;
        public static List<Category> _data { get; set; }

        public CategoryController(MySaleDBContext dbContext)
        {
            _db = dbContext;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            _data = _db.Categories.ToList();
            return Ok(_data);
        }

        [HttpGet("id")]
        public IActionResult GetCategoryById(int id)
        {
            var select = _db.Categories.Where(c => c.CategoryId == id).ToList();
            if(select == null)
            {
                return BadRequest();
            }
            return Ok(select);
        }

        [HttpPost]
        public IActionResult AddCategory(string name)
        {
            if(name == null || name.Equals(""))
            {
                return BadRequest();
            }
            Category c = new Category();
            c.CategoryName = name;
            _db.Categories.Add(c);
            _db.SaveChanges();
            return Ok(c);
        }

        [HttpPut]
        public IActionResult UpdateCategory(int id, string name)
        {
            var check = _db.Categories.Find(id);
            if(check == null)
            {
                return NotFound();
            }
            check.CategoryName = name;
            _db.SaveChanges();
            return Ok(check);
        }

        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var check = _db.Categories.Find(id);
            if (check == null)
            {
                return NotFound();
            }
            var listP = _db.Products.Where(p => p.CategoryId == id).ToList();
            foreach(var c in listP)
            {
                if(c.CategoryId == id)
                {
                    _db.Products.Remove(c);
                    _db.SaveChanges();
                }
            }
            _db.Categories.Remove(check);
            _db.SaveChanges();
            return Ok(check);
        }
    }
}
