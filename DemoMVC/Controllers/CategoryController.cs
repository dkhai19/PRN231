using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly MySaleDBContext _dbContext;

        public CategoryController(MySaleDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        List<Category> categories { get; set; }
        public IActionResult Index()
        {
            categories = _dbContext.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            var delItem = _dbContext.Categories.Find(id);
            foreach(var item in _dbContext.Products.ToList())
            {
                if(item.CategoryId == id)
                {
                    _dbContext.Remove(item);    
                }
            }
            if(delItem != null)
            {
                _dbContext.Categories.Remove(delItem);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if(!id.HasValue)
            {
                return NotFound();
            }
            var editItem = _dbContext.Categories.Find(id.Value);
            if(editItem == null)
            {
                return NotFound();
            }
            return View(editItem);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Categories.Update(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
