using Microsoft.AspNetCore.Mvc;
using ClothingStore.Data;
using ClothingStore.Models;

namespace ClothingStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
 

            return View();
        }
        public IActionResult CategoryGrid()
        {
            var categories = _db.Categories.ToList();

            return PartialView("_CategoryGrid", categories);
        }
        public IActionResult Create()
        {
 
            return PartialView("_Create");
        }
        [HttpPost]
        [HttpPost]
        public IActionResult AddCategory(Category model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                       .SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage);

                return Json(new
                {
                    success = false,
                    message = string.Join(", ", errors)
                });
            }

            model.dtCreatedDate = DateTime.Now;

            _db.Categories.Add(model);
            _db.SaveChanges();

            return Json(new
            {
                success = true,
                message = "Category added successfully."
            });
        }
    }
}