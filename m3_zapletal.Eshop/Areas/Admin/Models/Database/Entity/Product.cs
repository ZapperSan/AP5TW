using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace m3_zapletal.Eshop.Areas.Admin.Models.Database.Entity
{
    [Table(nameof(Product))]
    public class Product
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [StringLength(50)]
        [Required]
        public string productName { get; set; }

        [StringLength(255)]
        public string productDesc { get; set; }

        [Required]
        public int productPrice { get; set; }

        [StringLength(50)]
        [Required]
        public string productCategory { get; set; }

        [StringLength(255)]
        [Required]
        public string productImage { get; set; }
    }
}
