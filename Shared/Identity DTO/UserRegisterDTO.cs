using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Identity_DTO
{
    public record UserRegisterDTO
    {

        [Required(ErrorMessage = "DisplayName is Required")]
        public string DisplayName { get; init; }

        //================================================================

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; init; }

        //================================================================

        [Required(ErrorMessage = "Password is Required")]
       // [RegularExpression(" Write Any Pattern ")]
        public string Password { get; init; }

        //================================================================

        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; init; }

        //================================================================

        [Required(ErrorMessage = "PhoneNumber is Required")]
        // [Phone]
        public string? PhonNumber { get; init; }

    }
}
