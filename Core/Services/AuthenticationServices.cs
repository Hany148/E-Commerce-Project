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
using System.ComponentModel.DataAnnotations;
using Domain.Exceptions;

namespace Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationServices(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserResult> LoginAsnc(UserLoginDTO userDTO)
        {
            // 1. check if this email is exist

            var User = await _userManager.FindByEmailAsync(userDTO.Email);
            if (User == null) throw new AuthenticationException($"this user : {User?.UserName} is not Find");

            // 2. check if the Password of this email is Correct

            var Result = await _userManager.CheckPasswordAsync(User, userDTO.Password);

            if (!Result) new AuthenticationException();

            // 3. create token for this user

            var Token = "Token";

            // 4. return UserResult

            return new UserResult(User.DisplayName, User.Email!, Token);



        }

        public async Task<UserResult> RegisterAsync(UserRegisterDTO userRegisterDTO)
        {

            // 1. create user 

            var User = new User()
            {
                Email = userRegisterDTO.Email,
                DisplayName = userRegisterDTO.DisplayName,
                PhoneNumber = userRegisterDTO.PhonNumber,
                UserName = userRegisterDTO.UserName
            };


            // 2. create password

            var Result = await _userManager.CreateAsync(User, userRegisterDTO.Password);

            if (!Result.Succeeded)
            {
                var Errors = Result.Errors.Select(error => error.Description).ToList();
                throw new ValidationErrorsExpception(Errors);
            }

            // 3. Create Token 

            var Token = "Token";

            // 3. return UserResult

            return new UserResult(User.DisplayName, User.Email!, Token);


        }
    }
}
