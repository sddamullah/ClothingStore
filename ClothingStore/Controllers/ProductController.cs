using Microsoft.AspNetCore.Mvc;
using ClothingStore.Data;
using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
namespace ClothingStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;

        public ProductController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
           

            return View();
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var product = _db.Products.FirstOrDefault(x => x.intSeqId == id);

            if (product == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Product not found."
                });
            }

            _db.Products.Remove(product);
            _db.SaveChanges();

            return Json(new
            {
                success = true,
                message = "Product deleted successfully."
            });
        }
        public IActionResult ProductGrid()
        {
           var products = _db.Products.ToList();

            return PartialView("_ProductGrid" ,products );
        }


        public IActionResult Create()
        {
            ViewBag.CategoryList = _db.Categories.Where(x => x.IsActive).ToList();

            return PartialView("_Create");
        }

        [HttpPost]
       
        public IActionResult AddProduct(Product model)
        {

            if (model.ImageFile != null)
            {

                string folderPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/images/products"
                );


                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }


                string fileName =
                    Guid.NewGuid().ToString()
                    + Path.GetExtension(model.ImageFile.FileName);



                string filePath =
                    Path.Combine(folderPath, fileName);



                using (FileStream stream =
                      new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(stream);
                }



                // Save URL in database

                model.varImageUrl =
                    "/images/products/" + fileName;

            }



            model.dtCreatedDate = DateTime.Now;


            _db.Products.Add(model);

            _db.SaveChanges();



            return Json(new
            {
                success = true,
                message = "Product added successfully"
            });

        }


        public IActionResult Edit(int id)
        {

            var product = _db.Products
                             .FirstOrDefault(x => x.intSeqId == id);


            if (product == null)
            {
                return NotFound();
            }


            ViewBag.CategoryList = _db.Categories
                                      .Where(x => x.IsActive == true)
                                      .ToList();


            return PartialView("_Edit", product);

        }

        [HttpPost]
        public IActionResult UpdateProduct(Product model)
        {

            var product = _db.Products
                             .FirstOrDefault(x => x.intSeqId == model.intSeqId);


            if (product == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Product not found"
                });
            }



            product.varName = model.varName;

            product.intCategoryId = model.intCategoryId;

            product.flPrice = model.flPrice;

            product.intQuantity = model.intQuantity;



            if (model.ImageFile != null)
            {

                string folder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/images/products");


                string fileName = Guid.NewGuid()
                + Path.GetExtension(model.ImageFile.FileName);


                string path = Path.Combine(folder, fileName);


                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.ImageFile.CopyTo(stream);
                }


                product.varImageUrl =
                "/images/products/" + fileName;

            }



            product.dtUpdatedDate = DateTime.Now;


            _db.SaveChanges();


            return Json(new
            {
                success = true,
                message = "Product updated successfully"
            });

        }
    }
}
