using System.ComponentModel.DataAnnotations;

namespace BookStoreMvc.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter the title of your book")]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the author of your book")]
        public string Author { get; set; }

        [StringLength(500, ErrorMessage = "Please enter the description of your book")]
        public string Description { get; set; }

        public string Category { get; set; }

        [Required(ErrorMessage ="Please choose the language of the book")]
        public string Language { get; set; }

        [Required(ErrorMessage = "Please enter the number of pages of your book")]
        [Display(Name ="Total pages of the book")]
        public int? TotalPages { get; set; }
    }
}
