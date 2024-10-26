﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IServiceManger
    {
        public IProductService Product { get; }
        public IBasketService Basket { get; }
        public IAuthenticationServices authentication { get; }
        public IOrderService Order { get; }
    }
}
