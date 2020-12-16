using BookStoreMvc.Models;
using System.Collections.Generic;

namespace BookStoreMvc.Areas.Admin.Models
{
    public class UserViewModel : ApplicationUser
    {
        public string Edit { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
