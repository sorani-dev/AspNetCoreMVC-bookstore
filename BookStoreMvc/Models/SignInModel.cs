using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Models
{
    public class SignInModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]

        [Display(Name = "Remember me")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
