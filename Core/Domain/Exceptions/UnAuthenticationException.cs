using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class UnAuthenticationException : Exception
    {
        public UnAuthenticationException(string Massage = "Invalid Email or Password") :base(Massage) { }

       
    }
}
