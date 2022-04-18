using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HealthcareSystem.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the Users class
    public class Users : IdentityUser
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int role { get; set; }

        internal static string FindFirstValue(object role)
        {
            throw new NotImplementedException();
        }
    }
}
