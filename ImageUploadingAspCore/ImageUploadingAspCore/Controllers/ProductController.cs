using ImageUploadingAspCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageUploadingAspCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ImageDBContext dbContext;

        private readonly IWebHostEnvironment env;

        public ProductController(ImageDBContext dBContext, IWebHostEnvironment env)
        {
            this.dbContext = dBContext;
            this.env = env;
        }

        public IActionResult Index()
        {
            List<Product> products = new List<Product>();
            products = dbContext.Products.ToList();
            return View(products);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductViewModel prod)
        {
            string fileName = "";
            if (prod.Image != null)
            {
                var extension = Path.GetExtension(prod.Image.FileName);
                var size = prod.Image.Length;

                if (extension.Equals(".png") || extension.Equals(".jpg") || extension.Equals(".jpeg"))
                {

                    if (size < 1000000)
                    {

                        string folder = Path.Combine(env.WebRootPath, "images");
                        fileName = Guid.NewGuid().ToString() + "_" + prod.Image.FileName;
                        string filePath = Path.Combine(folder, fileName);
                        prod.Image.CopyTo(new FileStream(filePath, FileMode.Create));


                        Product p = new Product()
                        {
                            Name = prod.Name,
                            Price = prod.Price,
                            ImagePath = fileName
                        };

                        dbContext.Products.Add(p);
                        dbContext.SaveChanges();
                        TempData["success"] = "Product Added...";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["size_error"] = "Image size must be less than 1MB";
                    }
                }
                else
                {
                    TempData["ext_error"] = "Only JPG, JPEG & PNG formats are allowed";
                }
            }

            return View();
        }
    }
}
