using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public class ApplicationUser : IdentityUser
    {
     
        public string FullName { get; set; }
    }
}
