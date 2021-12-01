using m3_zapletal.Eshop.Areas.Admin.Models.Database;
using m3_zapletal.Eshop.Areas.Admin.Models.Database.Entity;
using m3_zapletal.Eshop.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace m3_zapletal.Eshop.Areas.Admin.Controllers
{
    [Area("Admin")] 
    public class HomeController : Controller
    {
        readonly EshopDbContext eshopDbContext;
        public HomeController(EshopDbContext eshopDB)
        {
            eshopDbContext = eshopDB;
        }
        public IActionResult Index()
        {
            IndexViewModel carouselItems = new IndexViewModel();
            carouselItems.CarouselItems = eshopDbContext.CarouselItem.ToList();

            return View(carouselItems);
        }
    }
}
