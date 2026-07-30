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
    public class ProductImageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductImageController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductImagesGrid()
        {
            var list = await _context.ProductImages
                .Include(x => x.Product)
                .OrderBy(x => x.ProductId)
                .ThenBy(x => x.DisplayOrder)
                .ToListAsync();

            return PartialView("_ProductImagesGrid", list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Default DisplayOrder starts at 1 until the user enters a Product Id,
            // at which point the JS auto-fetches the real next order for that product
            var model = new ProductImage
            {
                DisplayOrder = 1,
                IsMain = false
            };
            
            ViewBag.ProductList = _context.Products.ToList();

            return PartialView("_Create", model);
        }

        // Called via AJAX when the user enters/changes Product Id on the Create form
        [HttpGet]
        public async Task<IActionResult> GetNextDisplayOrder(int productId)
        {
            var maxOrder = await _context.ProductImages
                .Where(p => p.ProductId == productId)
                .Select(p => (int?)p.DisplayOrder)
                .MaxAsync();

            var nextOrder = (maxOrder ?? 0) + 1;

            return Json(new { nextOrder = nextOrder });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductImage model, IFormFile? imageFile)
        {
            // Manual validation (no data annotations / unobtrusive validation used)
            if (model.ProductId <= 0)
            {
                return Json(new { success = false, message = "Product Id is required" });
            }

            if (model.DisplayOrder <= 0)
            {
                return Json(new { success = false, message = "Display Order is required" });
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                model.ImagePath = await SaveImageAsync(imageFile);
            }

            // If this image is marked as Main, un-mark all other images
            // belonging to the same product so only one Main image exists
            if (model.IsMain)
            {
                await UnmarkOtherMainImagesAsync(model.ProductId, 0);
            }

            _context.ProductImages.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.ProductImages.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.ProductList = _context.Products.ToList();
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductImage model, IFormFile? imageFile)
        {
            // Manual validation
            if (model.ProductId <= 0)
            {
                return Json(new { success = false, message = "Product Id is required" });
            }

            if (model.DisplayOrder <= 0)
            {
                return Json(new { success = false, message = "Display Order is required" });
            }

            var existing = await _context.ProductImages.FindAsync(model.ImageId);
            if (existing == null)
            {
                return Json(new { success = false, message = "Image not found" });
            }

            existing.ProductId = model.ProductId;
            existing.DisplayOrder = model.DisplayOrder;
            existing.IsMain = model.IsMain;

            if (imageFile != null && imageFile.Length > 0)
            {
                existing.ImagePath = await SaveImageAsync(imageFile);
            }

            if (existing.IsMain)
            {
                await UnmarkOtherMainImagesAsync(existing.ProductId, existing.ImageId);
            }

            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.ProductImages.FindAsync(id);
            if (existing == null)
            {
                return Json(new { success = false, message = "Image not found" });
            }

            _context.ProductImages.Remove(existing);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // Ensures only one image per product has IsMain = true
        private async Task UnmarkOtherMainImagesAsync(int productId, int excludeImageId)
        {
            var others = await _context.ProductImages
                .Where(p => p.ProductId == productId && p.ImageId != excludeImageId && p.IsMain)
                .ToListAsync();

            foreach (var img in others)
            {
                img.IsMain = false;
            }
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var folderPath = Path.Combine(_env.WebRootPath, "images", "products");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return "/images/products/" + fileName;
        }
    }
}
