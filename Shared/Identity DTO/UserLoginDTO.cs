using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Identity_DTO
{
    public class UserLoginDTO
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        
        public string Password { get; set; }

    }
}
