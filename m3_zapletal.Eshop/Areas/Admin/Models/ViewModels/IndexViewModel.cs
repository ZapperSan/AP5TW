using m3_zapletal.Eshop.Areas.Admin.Models.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace m3_zapletal.Eshop.Areas.Admin.Models.ViewModels
{
    public class IndexViewModel
    {
        public IList<Product> Products { get; set; }
        public IList<CarouselItem> CarouselItems { get; set; }
    }
}
