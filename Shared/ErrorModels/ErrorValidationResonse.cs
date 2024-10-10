using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public class ErrorValidationResonse
    {
        public int statusCode { get; set; }
        public string Errormessage { get; set; }

        public IEnumerable<ValidationError> Errors { get; set; }
    }

    public class ValidationError
    {
        public string Feild { get; set; }  
        public IEnumerable<string> Errors { get; set; }
    }

}
