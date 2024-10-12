﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public override string ToString()
        {
           return JsonSerializer.Serialize(this);
        }
    }
}