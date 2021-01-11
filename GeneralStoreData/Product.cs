using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStoreData  // should have named this GenrealStore.Data
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
        /// <summary>
        /// SO you hit backslash 3 times and you get this.. okay nice
        /// this is stuff maybe you DON'T want in the model but all the data for this object is in the DATA layer stored here. 
        /// </summary>
        [Required]
        [Display(Name = "Percent Markup")]
        public int MarkUp { get; set; }

        [Display(Name = "Date Entered")]
        public int DateInt { get; set; }

        [Display(Name = "WHere does it come from")]
        public string OriginLoc { get; set; }

       
    }
}
