using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ClothingStore.Data;
using ClothingStore.Models;

namespace ClothingStore.Controllers
{
    public class GenderController : Controller
    {
        private readonly AppDbContext _context;

        public GenderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetGenderGrid()
        {
            var list = await _context.Genders
                .OrderBy(g => g.DisplayOrder)
                .ToListAsync();

            return PartialView("_GenderGrid", list);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Auto-suggest the next Display Order (still editable in the form)
            var maxOrder = await _context.Genders
                .Select(g => (int?)g.DisplayOrder)
                .MaxAsync();

            var model = new Gender
            {
                DisplayOrder = (maxOrder ?? 0) + 1,
                IsActive = true
            };

            return PartialView("_Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Gender model)
        {
            // Manual validation (no data annotations / unobtrusive validation used)
            if (string.IsNullOrWhiteSpace(model.GenderName))
            {
                return Json(new { success = false, message = "Gender Name is required" });
            }

            if (model.DisplayOrder <= 0)
            {
                return Json(new { success = false, message = "Display Order is required" });
            }

            model.CreatedDate = DateTime.Now;

            _context.Genders.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Genders.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Gender model)
        {
            // Manual validation
            if (string.IsNullOrWhiteSpace(model.GenderName))
            {
                return Json(new { success = false, message = "Gender Name is required" });
            }

            if (model.DisplayOrder <= 0)
            {
                return Json(new { success = false, message = "Display Order is required" });
            }

            var existing = await _context.Genders.FindAsync(model.GenderId);
            if (existing == null)
            {
                return Json(new { success = false, message = "Gender not found" });
            }

            existing.GenderName = model.GenderName;
            existing.DisplayOrder = model.DisplayOrder;
            existing.IsActive = model.IsActive;

            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Genders.FindAsync(id);
            if (existing == null)
            {
                return Json(new { success = false, message = "Gender not found" });
            }

            _context.Genders.Remove(existing);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
