using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Data
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<BookGenre> BookGenres  { get; set; }
    }
}
