using m3_zapletal.Eshop.Areas.Admin.Models.Database;
using m3_zapletal.Eshop.Areas.Admin.Models.Database.Entity;
using m3_zapletal.Eshop.Areas.Admin.Models.Implementation;
using m3_zapletal.Eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace m3_zapletal.Eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + " , " + nameof(Roles.Manager))]
    public class CarouselController : Controller
    {
        readonly EshopDbContext eshopDbContext;
        IWebHostEnvironment env;
        public CarouselController(EshopDbContext eshopDB, IWebHostEnvironment environment)
        {
            eshopDbContext = eshopDB;
            env = environment;
        }
        public IActionResult Select()
        {
            List<CarouselItem> carouselItems = eshopDbContext.CarouselItem.ToList();
            return View(carouselItems);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CarouselItem carouselItem)
        {
            if(carouselItem != null && carouselItem.Image != null)
            {
                FileUpload fileUpload = new FileUpload(env.WebRootPath, "img/CarouselItems", "image");
                carouselItem.ImageSource = await fileUpload.FileUploadAsync(carouselItem.Image);

                ModelState.Clear();
                TryValidateModel(carouselItem);
                if(ModelState.IsValid)
                {
                    eshopDbContext.CarouselItem.Add(carouselItem);
                    await eshopDbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(CarouselController.Select));
                }

               //if (String.IsNullOrWhiteSpace(carouselItem.ImageSource) == false)
            }
                return View(carouselItem);
        }
        public IActionResult Edit(int ID)
        {
            CarouselItem ciFromDatabase = eshopDbContext.CarouselItem.FirstOrDefault(ci => ci.ID == ID);
            if(ciFromDatabase != null)
            {
                return View(ciFromDatabase);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CarouselItem carouselItem)
        {
            CarouselItem ciFromDatabase = eshopDbContext.CarouselItem.FirstOrDefault(ci => ci.ID == carouselItem.ID);
            if(ciFromDatabase != null && carouselItem.Image != null)
            {
                FileUpload fileUpload = new FileUpload(env.WebRootPath, "img/CarouselItems", "image");
                carouselItem.ImageSource = await fileUpload.FileUploadAsync(carouselItem.Image);

                if (String.IsNullOrWhiteSpace(carouselItem.ImageSource) == false)
                {
                    ciFromDatabase.ImageSource = carouselItem.ImageSource;
                }
                else
                {
                    carouselItem.ImageSource = "^w^";
                }

                ModelState.Clear();
                TryValidateModel(carouselItem);
                if (ModelState.IsValid)
                {
                    ciFromDatabase.ImageAlt = carouselItem.ImageAlt;
                    await eshopDbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(CarouselController.Select));
                }
            }
            return View(carouselItem);
        }
        public async Task<IActionResult> Delete(int ID)
        {
            DbSet<CarouselItem> carouselItems = eshopDbContext.CarouselItem;
            CarouselItem carouselItem = carouselItems.Where(CarouselItem => CarouselItem.ID == ID).FirstOrDefault();
            if(carouselItem != null)
            {
                carouselItems.Remove(carouselItem);
                await eshopDbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(CarouselController.Select));
        }
    }
}
