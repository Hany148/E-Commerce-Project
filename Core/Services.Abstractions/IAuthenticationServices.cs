using Shared;
using Shared.Identity_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAuthenticationServices
    {
        public Task<UserResult> LoginAsnc(UserLoginDTO userDTO);
        public Task<UserResult> RegisterAsync(UserRegisterDTO userRegisterDTO);
    }
}
