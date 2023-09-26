
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
            var select = _db.Categories.SingleOrDefault(x => x.CategoryId==id);
            if (select == null)
            {
                return BadRequest();
            }
            return Ok(select);
        }

        [HttpGet("name")]
        public IActionResult GetCategoryByName(string name)
        {
            var select = _db.Categories.Where(x => x.CategoryName.Contains(name));
            if (select == null)
            {
                return BadRequest();
            }
            return Ok(select);
        }

        [HttpPost]
        public IActionResult AddCategory(Category c)
        {
            if (c.CategoryName == null)
            {
                return BadRequest();
            }
            _db.Categories.Add(c);
            _db.SaveChanges();
            return Ok(c);
        }

        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            var check = _db.Categories.Find(category.CategoryId);
            if (check == null)
            {
                return NotFound();
            }
            check.CategoryName = category.CategoryName;
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
            foreach (var c in listP)
            {
                if (c.CategoryId == id)
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
