using System.ComponentModel.DataAnnotations;

namespace BookStoreMvc.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [Required, StringLength(100, MinimumLength = 2), DataType(DataType.Text)]
        [Display(Name ="Category name")]
        public string Name { get; set; }
    }
}
