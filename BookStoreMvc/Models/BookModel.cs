using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStoreMvc.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the title of your book")]
        [StringLength(100, MinimumLength = 2)]
        //[MyCustomValidation("java", ErrorMessage ="My Error Message", Text ="java")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the author of your book")]
        public string Author { get; set; }

        [StringLength(500, ErrorMessage = "Please enter the description of your book")]
        public string Description { get; set; }

        public string Category { get; set; }

        //[Required(ErrorMessage ="Please choose the language of the book")]
        [Display(Name = "Language of the book")]
        public int LanguageId { get; set; }
        public string Language { get; set; }

        //[Required(ErrorMessage = "Please choose the languages of the book")]
        //public List<string> MultiLanguage { get; set; }

        [Required(ErrorMessage = "Please enter the number of pages of your book")]
        [Display(Name = "Total pages of the book")]
        public int? TotalPages { get; set; }

        [Display(Name = "Choose the cover photo of the book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }

        public string CoverImageUrl { get; set; }

        [Display(Name = "Choose the gallery images of the book")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryModel> Gallery { get; set; }


        [Display(Name = "Upload the book in PDF format")]
        [Required]
        public IFormFile BookPdf { get; set; }

        public string BookPdfUrl { get; set; }
    }
}
