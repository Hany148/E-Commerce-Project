using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Services.Abstractions;
using Shared;
using Shared.DTO;
using Shared.Identity_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    public class AuthenticationController : APIController
    {
        private readonly IServiceManger _serviceManger;
        public AuthenticationController(IServiceManger serviceManger)
        {
            _serviceManger = serviceManger;
        }

        [HttpPost(template: "login")]
        public async Task<ActionResult<UserResult>> LoginAsync(UserLoginDTO userLoginDTO)
        {
            var result = await _serviceManger.authentication.LoginAsnc(userLoginDTO);
            return Ok(result);
        }

        [HttpPost(template: "Register")]
        public async Task<ActionResult<UserResult>> RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            var result = await _serviceManger.authentication.RegisterAsync(userRegisterDTO);
            return Ok(result);
        }

        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            var Result = await _serviceManger.authentication.CheckUserByEmail(email);

            return Ok(Result);
        }

        [Authorize]
        [HttpGet("GetUser")]
        public async Task<ActionResult<UserResult>> GetUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var Result = await _serviceManger.authentication.GetUserByEmail(email!);

            return Ok(Result);
        }

        [Authorize]
        [HttpGet("GetAddress")]
        public async Task<ActionResult<AddressDTO>> GetAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var Result = await _serviceManger.authentication.GetAddressUserByEmail(email!);

            return Ok(Result);
        }



        [Authorize]
        [HttpPut("UpdateAddress")]
        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO addressDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var Result = await _serviceManger.authentication.UpdateAddressUserByEmail(addressDTO , email);

            return Ok(Result);
        }


    }
}
