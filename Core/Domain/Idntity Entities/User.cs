using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Idntity_Entities
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
    
        public Address Address { get; set; } // reference navigational property
    }
}
