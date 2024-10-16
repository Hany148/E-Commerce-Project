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
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly UserManager<User> _userManager;

        public AuthenticationServices(UserManager<User> userManager)
        {
            _userManager = userManager;
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

            var Token = await CreateTokenAsync(User);

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

            var Token = await CreateTokenAsync(User);

            // 4. return UserResult

            return new UserResult(User.DisplayName, User.Email!, Token);


        }


        private async Task<string> CreateTokenAsync(User user)
        {
            // 1. create private clamis

            var AuthClamis = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Name, user.Email!),


            };

            // 2. add roles to my private clamis

            var getRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in getRoles)
            {
                AuthClamis.Add(new Claim(ClaimTypes.Role, role));
            }

            // 3. create key

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KdFFoKdhuQnsdP3ptptaNpMUkgnJouVX1wzMQQbRbyw="));

            // 4. create signingCredentials

            var signingCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            // 4. create signingCredentials

            var Token = new JwtSecurityToken(

                audience: "MyAudience",
                issuer: "https://localhost:7183",
                expires: DateTime.UtcNow.AddDays(10),
                claims: AuthClamis,
                signingCredentials: signingCredential


            );

            //  5. create Token and convert it to string

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }

    }
}
