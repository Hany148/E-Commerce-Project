using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using Shared;
using Shared.Identity_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Idntity_Entities;

namespace Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationServices(UserManager<User> userManager  , RoleManager<IdentityRole> roleManager )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserResult> LoginAsnc(UserLoginDTO userDTO)
        {
            // 1. check if this email is exist

            var User = await _userManager.FindByEmailAsync( userDTO.Email );
            if (User == null) throw new UnauthorizedAccessException($"this user : {User?.UserName} is not Find");

            // 2. check if the Password of this email is Correct

           var Result =  await _userManager.CheckPasswordAsync(User, userDTO.Password );

            if (!Result) new UnauthorizedAccessException();

            // 3. create token for this user

            var Token = "Token";

            // 4. return UserResult

            return new UserResult(User.DisplayName , User.Email! , Token);



        }

        public Task<UserResult> RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            throw new NotImplementedException();
        }
    }
}
