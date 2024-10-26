using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.BsketDTO
{
    public record CustomerBasketDTO
    {
        public string Id { get; set; }

        public IEnumerable<BasketItemDTO> basketItemDTOs { get; set; }

        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }

        public int? DeliveryMethod { get; set; }

        public decimal? ShippingPrice { get; set; }
    }
}
