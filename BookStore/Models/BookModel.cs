using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BookStore.Data;
using BookStore.Helpers;
using Microsoft.AspNetCore.Http;

namespace BookStore.Models
{
    public class BookModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Please Select Date")]
        public string MyField { get; set; }
        public int id { get; set; }
        [StringLength(100, MinimumLength = 4)]
        [Required(ErrorMessage = "Please enter the Title of your Book")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter the Author name")]
        public string Author { get; set; }
        [StringLength(500, MinimumLength = 5)]
        [MyCustomValidationAttributes("Description")]
        public string Description { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Please choose the language of your book")]
        public int LanguageId { get; set; }
        public string Language { get; set; }
        [Required(ErrorMessage = "Please enter the total pages")]
        [Display(Name = "Total Pages of Book")]
        public int? TotalPages { get; set; }
        [Display(Name = "Choose the cover photo of your book")]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }

        [Display(Name = "Choose the cover photo of your book")]
        public IFormFileCollection GalleryFiles { get; set; }
        public List<GalleryModel> Gallery{ get; set; }
        [Display(Name = "Upload your book in pdf format")]
        public IFormFile BookPdf { get; set; }
        public string BookPdfUrl { get; set; }
    }
}
