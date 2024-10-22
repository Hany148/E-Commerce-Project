using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ParameterOfOrder
    {
        public string BasketId { get; set; }

        public AddressDTO ShippingAdress { get; set; }
        public int DeliveryMethodId { get; set; }
    }
}
