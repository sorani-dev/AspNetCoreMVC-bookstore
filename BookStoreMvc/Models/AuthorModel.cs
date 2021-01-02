using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Please enter your name")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [DataType(DataType.Date), Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
    }
}
