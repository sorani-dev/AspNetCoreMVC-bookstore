using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Models
{
    public class GenreModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the name of the genre")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
