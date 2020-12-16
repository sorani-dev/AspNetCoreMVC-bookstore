using Microsoft.AspNetCore.Identity;
using System;

namespace BookStoreMvc.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string ProfilePicturePath { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
