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

        // Login
        public Task<UserResult> LoginAsnc(UserLoginDTO userDTO);

        // Register
        public Task<UserResult> RegisterAsync(UserRegisterDTO userRegisterDTO);

        // get current user by email

        public Task<UserResult> GetUserByEmail(string email);

        // check if email is exist

        public Task<bool> CheckUserByEmail(string email);

        // get Address of user by email

        public Task<AddressDTO> AddressUserByEmail(string email);

        // update address of user 

        public Task<AddressDTO> UpdateAddressUserByEmail(AddressDTO addressDTO , string email);

    }
}
