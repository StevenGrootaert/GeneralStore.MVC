using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeneralStore.MVC.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; } // I would have named this variable ProductName

        [Required]
        [Display(Name = "# In Stock")]
        public int InventoryCount { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Is it food")]
        public bool IsFood { get; set; }
    }

    // db context?? this was taken from the restaurant rater example
    /*
     * public class ProductDbCOntext : DbContext
     * {
     *      public DbSet<Product> Products { get; set; }
     * }
     */
}