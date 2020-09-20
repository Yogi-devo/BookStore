using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookStore.Models
{
    public class BookModel
    { 
        [DataType(DataType.Date)]
        [Display(Name ="Please Select Date")]
        public string MyFielf { get; set; }
        public int id { get; set; }
        [StringLength(100, MinimumLength =4)]
        [Required(ErrorMessage ="Please enter the Title of your Book")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Please enter the Author name")]
        public string Author { get; set; }
        [StringLength(500, MinimumLength = 15)]
        public string Description { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        [Required(ErrorMessage ="Please enter the total pages")]
        [Display(Name ="Total Pages of Book")]
        public int? TotalPages { get; set; }

    }
}
