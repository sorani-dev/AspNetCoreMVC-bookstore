using BookStoreMvc.Models;
using System.ComponentModel.DataAnnotations;

namespace BookStoreMvc.Areas.Admin.Models
{
    public class AppUserModel : ApplicationUser
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
