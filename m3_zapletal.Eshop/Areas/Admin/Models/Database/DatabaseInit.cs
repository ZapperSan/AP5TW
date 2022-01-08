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
                ID = 10,
                ImageSource = "https://media3.giphy.com/media/Ju7l5y9osyymQ/200.webp?cid=ecf05e47xj3do935qd0g73gx8yb386pcjrfk3yeieggxtzy6&rid=200.webp&ct=g",
                ImageAlt = "First slide"
            };

            CarouselItem ci2 = new CarouselItem()
            {
                ID = 11,
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
                ID = 100,
                productName = "Trollface",
                productDesc = "The father of memes",
                productPrice = 420,
                productCategory = "Classic",
                productImage = "https://upload.wikimedia.org/wikipedia/en/9/9a/Trollface_non-free.png"
            };

            Product pr2 = new Product()
            {
                ID = 101,
                productName = "No regrets",
                productDesc = "RIP steak",
                productPrice = 15,
                productCategory = "Cats",
                productImage = "https://ahseeit.com//king-include/uploads/2021/06/35_no-regrets-1-806123667.jpg"
            };

            Product pr3 = new Product()
            {
                ID = 102,
                productName = "Dog and Snog",
                productDesc = "The similarity is staggering",
                productPrice = 39,
                productCategory = "Dogs",
                productImage = "https://i.pinimg.com/736x/19/ae/62/19ae621196b14c16cd90bc6300325ab6.jpg"
            };

            Product pr4 = new Product()
            {
                ID = 103,
                productName = "Monorail cat",
                productDesc = "It has one rail only",
                productPrice = 397,
                productCategory = "Cats",
                productImage = "https://bestlifeonline.com/wp-content/uploads/sites/3/2018/06/cat-meme-4.jpg"
            };

            Product pr5 = new Product()
            {
                ID = 104,
                productName = "Pixar Dog",
                productDesc = "Its a lamp!",
                productPrice = 1,
                productCategory = "Dogs",
                productImage = "https://dogsrealty.com/wp-content/uploads/2019/07/PIXAR.jpg"
            };

            Product pr6 = new Product()
            {
                ID = 105,
                productName = "Pikachu",
                productDesc = ":O",
                productPrice = 20,
                productCategory = "Classic",
                productImage = "https://i.kym-cdn.com/photos/images/original/001/431/201/40f.png"
            };

            Product pr7 = new Product()
            {
                ID = 106,
                productName = "Spare cats",
                productDesc = "Pile of fluff",
                productPrice = 792,
                productCategory = "Cats",
                productImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSLoh_YQ0SxcDJeSZlu9i2D5DLE0UxBKTEyORblK1A9Ci5xmbLsNZ6TBsGigbLk0fWbkSg&usqp=CAU"
            };

            Product pr8 = new Product()
            {
                ID = 107,
                productName = "Pockets",
                productDesc = "Life sux sometimes",
                productPrice = 310,
                productCategory = "Cats",
                productImage = "https://thumbor.granitemedia.com/cat-in-the-snow/ssSWhdpSl5b2-D8d4fZyhejnT48=/800x0/filters:quality(80)/granite-web-prod/8c/6a/8c6a426f9082458e9dd30bacfaa2c6ce.jpeg"
            };

            Product pr9 = new Product()
            {
                ID = 108,
                productName = "This is fine",
                productDesc = "No seriously...",
                productPrice = 51,
                productCategory = "Classic",
                productImage = "https://wompampsupport.azureedge.net/fetchimage?siteId=7575&v=2&jpgQuality=100&width=700&url=https%3A%2F%2Fi.kym-cdn.com%2Fentries%2Ficons%2Foriginal%2F000%2F018%2F012%2Fthis_is_fine.jpeg"
            };

            Product pr10 = new Product()
            {
                ID = 109,
                productName = "Where nuts?",
                productDesc = "Huh? Answer me!",
                productPrice = 404, 
                productCategory = "Cats",
                productImage = "http://www.quickmeme.com/img/d9/d9b6145cc5d9b484eb7b39d0077d04ad38cab0bf236e60b3d42bdd84be343949.jpg"
            };

            products.Add(pr1);
            products.Add(pr2);
            products.Add(pr3);
            products.Add(pr4);
            products.Add(pr5);
            products.Add(pr6);
            products.Add(pr7);
            products.Add(pr8);
            products.Add(pr9);
            products.Add(pr10);
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
