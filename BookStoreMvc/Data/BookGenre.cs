﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Data
{
    public class BookGenre
    {
        [Key, Column(Order = 0)]
        public int BookId { get; set; }
        public Book Book { get; set; }
        [Key, Column(Order = 1)]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
