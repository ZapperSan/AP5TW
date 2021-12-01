using m3_zapletal.Eshop.Areas.Admin.Models.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace m3_zapletal.Eshop.Areas.Admin.Models.Database
{
    public static class DatabaseFake
    {
        public static List<CarouselItem> CarouselItems;
        public static List<Product> Products;

        static DatabaseFake()
        {
            DatabaseInit dbInit = new DatabaseInit();
            CarouselItems = dbInit.GenerateCarouselItems();
            Products = dbInit.GenerateProducts();
        }
    }
}
