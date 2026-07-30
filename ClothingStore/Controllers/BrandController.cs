using ClothingStore.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClothingStore.Data;
using ClothingStore.Models;

namespace ClothingStore.Controllers
{
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BrandController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetBrandGrid()
        {
            var list = await _context.Brands
                .OrderByDescending(b => b.intSeqId)
                .ToListAsync();

            return PartialView("_BrandGrid", list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new Brand
            {
                isActive = true
            };
            return PartialView("_Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Brand model, IFormFile? logoFile)
        {
            if (logoFile == null || logoFile.Length == 0)
            {
                ModelState.AddModelError("varLogoUrl", "Logo is required");
            }

            

            model.varLogoUrl = await SaveLogoAsync(logoFile!);
            model.dtCreatedDate = DateTime.Now;
            model.dtUpdatedDate = null;

            _context.Brands.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Brands.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Brand model, IFormFile? logoFile)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Validation failed" });
            }

            var existing = await _context.Brands.FindAsync(model.intSeqId);
            if (existing == null)
            {
                return Json(new { success = false, message = "Brand not found" });
            }

            existing.varBrandName = model.varBrandName;
            existing.isActive = model.isActive;
            existing.dtUpdatedDate = DateTime.Now;

            if (logoFile != null && logoFile.Length > 0)
            {
                existing.varLogoUrl = await SaveLogoAsync(logoFile);
            }

            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Brands.FindAsync(id);
            if (existing == null)
            {
                return Json(new { success = false, message = "Brand not found" });
            }

            _context.Brands.Remove(existing);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        private async Task<string> SaveLogoAsync(IFormFile logoFile)
        {
            var folderPath = Path.Combine(_env.WebRootPath, "images", "logos");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(logoFile.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await logoFile.CopyToAsync(stream);
            }

            return "/images/logos/" + fileName;
        }
    }
}
