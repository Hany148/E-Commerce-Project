using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ValidationErrorsExpception : Exception
    {

        public ValidationErrorsExpception(IEnumerable<string> errors) : base("Validation Failed") {
          Errors = errors;
        }

        public IEnumerable<string> Errors { get; set; }
    }
}
