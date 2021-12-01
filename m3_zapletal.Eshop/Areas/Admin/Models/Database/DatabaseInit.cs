using m3_zapletal.Eshop.Areas.Admin.Models.Database.Entity;
using m3_zapletal.Eshop.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace m3_zapletal.Eshop.Areas.Admin.Models.Database
{
    public class DatabaseInit
    {
        public void Initialize(EshopDbContext eshopDbContext)
        {
            eshopDbContext.Database.EnsureCreated();
            if(eshopDbContext.CarouselItem.Count() == 0)
            {
                IList<CarouselItem> carouselItems = GenerateCarouselItems();
                foreach(var ci in carouselItems)
                {
                    eshopDbContext.CarouselItem.Add(ci);
                }
                eshopDbContext.SaveChanges();
            }
            if (eshopDbContext.Products.Count() == 0)
            {
                IList<Product> products = GenerateProducts();
                foreach (var ci in products)
                {
                    eshopDbContext.Products.Add(ci);
                }
                eshopDbContext.SaveChanges();
            }
        }

        public List<CarouselItem> GenerateCarouselItems()
        {
            List<CarouselItem> carouselItems = new List<CarouselItem>();

            CarouselItem ci1 = new CarouselItem()
            {
                ID = 0,
                ImageSource = "https://media3.giphy.com/media/Ju7l5y9osyymQ/200.webp?cid=ecf05e47xj3do935qd0g73gx8yb386pcjrfk3yeieggxtzy6&rid=200.webp&ct=g",
                ImageAlt = "First slide"
            };

            CarouselItem ci2 = new CarouselItem()
            {
                ID = 1,
                ImageSource = "https://media3.giphy.com/media/tANpI4H9zlv1u/200w.webp?cid=ecf05e47wwrzqh4svjtdevj09yssxp2dvoxyfrj17mmqcykk&rid=200w.webp&ct=g",
                ImageAlt = "Second slide"
            };

            carouselItems.Add(ci1);
            carouselItems.Add(ci2);
            return carouselItems;
        }

        public List<Product> GenerateProducts()
        {
            List<Product> products = new List<Product>();
            Product pr1 = new Product()
            {
                ID = 0,
                productName = "Vec",
                productDesc = "Zarizeni, ktere ma urcity ucel",
                productPrice = 10
            };

            Product pr2 = new Product()
            {
                ID = 1,
                productName = "Predmet",
                productDesc = "Nepostradatelny pomocnik pro kazdeho",
                productPrice = 15
            };

            products.Add(pr1);
            products.Add(pr2);
            return products;
        }

        public async Task EnsureRoleCreated(RoleManager<Role> roleManager)
        {
            string[] roles = Enum.GetNames(typeof(Roles));

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new Role(role));
            }
        }

        public async Task EnsureAdminCreated(UserManager<User> userManager)
        {
            User user = new User
            {
                UserName = "admin",
                Email = "admin@admin.cz",
                EmailConfirmed = true,
                FirstName = "jmeno",
                LastName = "prijmeni"
            };
            string password = "abc";

            User adminInDatabase = await userManager.FindByNameAsync(user.UserName);

            if (adminInDatabase == null)
            {

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (result == IdentityResult.Success)
                {
                    string[] roles = Enum.GetNames(typeof(Roles));
                    foreach (var role in roles)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
                else if (result != null && result.Errors != null && result.Errors.Count() > 0)
                {
                    foreach (var error in result.Errors)
                    {
                        Debug.WriteLine($"Error during Role creation for Admin: {error.Code}, {error.Description}");
                    }
                }
            }

        }

        public async Task EnsureManagerCreated(UserManager<User> userManager)
        {
            User user = new User
            {
                UserName = "manager",
                Email = "manager@manager.cz",
                EmailConfirmed = true,
                FirstName = "jmeno",
                LastName = "prijmeni"
            };
            string password = "abc";

            User managerInDatabase = await userManager.FindByNameAsync(user.UserName);

            if (managerInDatabase == null)
            {

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (result == IdentityResult.Success)
                {
                    string[] roles = Enum.GetNames(typeof(Roles));
                    foreach (var role in roles)
                    {
                        if (role != Roles.Admin.ToString())
                            await userManager.AddToRoleAsync(user, role);
                    }
                }
                else if (result != null && result.Errors != null && result.Errors.Count() > 0)
                {
                    foreach (var error in result.Errors)
                    {
                        Debug.WriteLine($"Error during Role creation for Manager: {error.Code}, {error.Description}");
                    }
                }
            }

        }

    }
}
