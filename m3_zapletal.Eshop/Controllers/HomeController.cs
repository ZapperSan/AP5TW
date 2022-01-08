using m3_zapletal.Eshop.Areas.Admin.Models.Database;
using m3_zapletal.Eshop.Areas.Admin.Models.ViewModels;
using m3_zapletal.Eshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PagedList;

namespace m3_zapletal.Eshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly EshopDbContext eshopDbContext;

        public HomeController(ILogger<HomeController> logger, EshopDbContext eshopDB)
        {
            _logger = logger;
            eshopDbContext = eshopDB;
        }

        public IActionResult Index(string category, int? page)
        {
            /*
            _logger.LogInformation("Byla zobrazena hlavni stranka");

            IndexViewModel indexViewModel = new IndexViewModel();
            indexViewModel.CarouselItems = eshopDbContext.CarouselItem.ToList();
            indexViewModel.Products = eshopDbContext.Products.ToList();

            return View(indexViewModel);
            */
            var products = from s in eshopDbContext.Products
                           select s;
            if (!String.IsNullOrEmpty(category))
            {
                products = products.Where(s => s.productCategory.Equals(category));
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
