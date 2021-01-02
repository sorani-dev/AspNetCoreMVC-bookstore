using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreMvc.Data
{
    public class Category
    {
        [Column(name: "Id")]
        public int CategoryId { get; set; }
        [Column(name: "Name")]
        public string CategoryName { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
