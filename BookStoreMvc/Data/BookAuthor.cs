using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Data
{
    public class BookAuthor
    {
        [Key, Column(Order = 0)]
        public int AuthorId { get; set; }
        [Key, Column(Order = 1)]
        public int BookId { get; set; }

        public Book Book { get; set; }

        public Author Author { get; set; }
    }
}
