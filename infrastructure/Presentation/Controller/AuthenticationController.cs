using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Services.Abstractions;
using Shared;
using Shared.Identity_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<UserResult>> LoginAsync (UserLoginDTO userLoginDTO)
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


    }
}
