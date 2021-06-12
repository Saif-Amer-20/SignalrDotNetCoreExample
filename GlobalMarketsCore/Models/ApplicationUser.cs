using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GlobalMarketsCore.DataModel;
using Microsoft.AspNetCore.Identity;

namespace GlobalMarketsCore.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Ip { get; set; }
        public string AccountID { get; set; }
        public string Pass { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Date { get; set; }

       

    }
}
