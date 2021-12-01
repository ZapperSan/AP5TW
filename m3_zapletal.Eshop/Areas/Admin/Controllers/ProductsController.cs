using m3_zapletal.Eshop.Areas.Admin.Models.Database;
using m3_zapletal.Eshop.Areas.Admin.Models.Database.Entity;
using m3_zapletal.Eshop.Areas.Admin.Models.Implementation;
using m3_zapletal.Eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace m3_zapletal.Eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + " , " + nameof(Roles.Manager))]
    public class ProductsController : Controller
    {
        readonly EshopDbContext eshopDbContext;
        IWebHostEnvironment env;
        public ProductsController(EshopDbContext eshopDB, IWebHostEnvironment environment)
        {
            eshopDbContext = eshopDB;
            env = environment;
        }
        public IActionResult Index()
        {
            List<Product> products = eshopDbContext.Products.ToList();
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (product != null && product.productName != null && product.productPrice != 0)
            {
                if (String.IsNullOrWhiteSpace(product.productName) == false)
                {
                    eshopDbContext.Products.Add(product);
                    await eshopDbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(ProductsController.Index));
                }
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            Product ciFromDatabase = eshopDbContext.Products.FirstOrDefault(ci => ci.ID == product.ID);
            if (ciFromDatabase != null && product.productPrice != 0)
            {
                if (String.IsNullOrWhiteSpace(product.productName) == false)
                {
                    ciFromDatabase.productName = product.productName;
                }
                ciFromDatabase.productDesc = product.productDesc;
                ciFromDatabase.productPrice = product.productPrice;
                await eshopDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(ProductsController.Index));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
