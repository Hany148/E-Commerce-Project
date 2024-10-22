using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Entities.OrderEntites
{
    public class Order : BaseEntity<Guid>
    {
        // User Email 
        public string UserEmail { get; set; }

        // user دة لل order العنوان اللي اتشحن علية ال

        public Address ShippingAdress { get; set; }

        // المنتجات (products)
        public ICollection<OrderItem> OrderItems { get; set; } // collection navigational property

        // Payment status ( Pendding , received , failed )
        public OrderPaymentStatus paymentStatus { get; set; }

        // payment intent

        public string PaymentIntentId { get; set; } = string.Empty; 

        // طرق التوصيل (DeliveryMethod)

        public DeliveryMethod DeliveryMethod { get; set; } // ref navigational property
        public int? DeliveryMethodId { get; set; } // ref navigational property

        // SubTotal = items of Quntity * price 

        public decimal SubTotal { get; set; }

        // order date

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

    }
}
