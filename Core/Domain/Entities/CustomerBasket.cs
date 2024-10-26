﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; } // pramiry kay

        public IEnumerable<BasketItem> BasketItems { get; set; } = Enumerable.Empty<BasketItem>();

        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }

        public int? DeliveryMethod { get; set; }

        public decimal? ShippingPrice { get; set; }
    }
}
