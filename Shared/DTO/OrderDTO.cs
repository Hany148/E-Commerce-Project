using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class OrderDTO
    {

        public Guid Id { get; set; }

        // User Email 
        public string UserEmail { get; set; }

        // user دة لل order العنوان اللي اتشحن علية ال

        public AddressDTO ShippingAdress { get; set; }

        // المنتجات (products)
        public ICollection<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO> (); 

        // name of Payment status 
        public string paymentStatus { get; set; } 

        // payment intent

        public string PaymentIntentId { get; set; } = string.Empty;

        // name of DeliveryMethod

        public string DeliveryMethod { get; set; } 

        // SubTotal = items of Quntity * price 

        public decimal SubTotal { get; set; }

        // Total =  SubTotal + price of DeliveryMethod

        public decimal Total { get; set; }

        // order date

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;


    }
}
