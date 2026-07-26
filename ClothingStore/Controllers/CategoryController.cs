using Microsoft.AspNetCore.Mvc;
using ClothingStore.Data;
using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;

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
        // GET: fetch a single category by id (to populate the edit form)
        [HttpGet]
        public IActionResult GetCategoryById(int id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.intSeqId == id);
            if (category == null)
                return Json(new { success = false, message = "Category not found." });

            return PartialView("_Edit", category);
        }

        // POST: update category
        [HttpPost]
        public IActionResult UpdateCategory(int intSeqId, string varName, string varDescription, bool IsActive)
        {
            var category = _db.Categories.FirstOrDefault(c => c.intSeqId == intSeqId);
            if (category == null)
                return Json(new { success = false, message = "Category not found." });

            if (string.IsNullOrWhiteSpace(varName))
                return Json(new { success = false, message = "Category name is required." });

            category.varName = varName.Trim();
            category.varDescription = varDescription?.Trim();
            category.IsActive = IsActive;
            category.dtUpdatedDate = DateTime.Now;

            _db.SaveChanges();

            return Json(new { success = true, message = "Category updated successfully." });
        }
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
        public IActionResult Delete(int id)
        {
            var category = _db.Categories.FirstOrDefault(x => x.intSeqId == id);

            if (category == null)
            {
                return Json(new
                {
                    success = false,
                    message = "category not found."
                });
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();

            return Json(new
            {
                success = true,
                message = "category deleted successfully."
            });
        }
    }
}