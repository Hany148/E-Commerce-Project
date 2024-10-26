global using Stripe;
global using Product = Domain.Entities.Product;
using AutoMapper;
using Domain.Contracts.IRepository;
using Domain.Contracts___Interface__;
using Domain.Entities;
using Domain.Entities.OrderEntites;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Persistence.Exceptions;
using Services.Abstractions;
using Services.Specification;
using Shared.BsketDTO;
using Stripe.Forwarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PaymentService(IBasketRepository
        basketRepository,
        IUnitOfWork unitOfWork
        , IMapper mapper,
        IConfiguration configuration)

        : IPaymentService
    {

        private readonly IGenericRepository<Product, int> ProductObject = unitOfWork.GetRepository<Product, int>();
        private readonly IGenericRepository<DeliveryMethod, int> DeliveryObject = unitOfWork.GetRepository<DeliveryMethod, int>();
        private readonly IGenericRepository<OrderEntity, Guid> OrderObject = unitOfWork.GetRepository<OrderEntity, Guid>();

        public async Task<CustomerBasketDTO> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {

            #region Using Environment Variable

            // var EnvironmentVariable = Environment.GetEnvironmentVariable("SecretKey");

            #endregion


            // 1. set secret key ==> Stripe اللي علي business account دة كلمه السر بتاع ال secret key ال 
            StripeConfiguration.ApiKey = configuration.GetSection("StripeSettings")["SecretKey"];

            // 2. Get (retrieve) basket from basketRepository to get items then retrieve the product form db to calculate subtotal
            var basket = await basketRepository.GetBasketAsync(BasketId) ?? throw new BasketNotFoundException(BasketId);

            // 3. get product 

            foreach (var item in basket.BasketItems)
            {
                var product = await ProductObject.FindByIdAysnc(item.Id) ?? throw new ProductNotFoundExceptions(item.Id);
                item.Price = product.Price;
            }

            // 5. get price of delivery method (shipping price)


            if (!basket.DeliveryMethodId.HasValue) // true = flase == false
            {
                throw new Exception("No delivery method is selected");
            }

            var deliveryMethod = await DeliveryObject.FindByIdAysnc(basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundExceptions(basket.DeliveryMethodId.Value);

            basket.ShippingPrice = deliveryMethod.Price;

            // create price that will payment 

            var PricePayment = (long)(basket.BasketItems.Sum(b => (b.Price * b.Quntity)) + basket.ShippingPrice) * 100;

            // 6. to create payment (create opitions) in stripe we need ===>> 1. key , 2. price , 3. Payment Method

            var paymentIntentService = new PaymentIntentService();

            //                      first create payment

            if (string.IsNullOrWhiteSpace(basket.PaymentIntentId))
            {
                var paymentIntentCreateOptions = new PaymentIntentCreateOptions()
                {
                    Amount = PricePayment,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string>() { "card" }
                };

                var paymentIntent = await paymentIntentService.CreateAsync(paymentIntentCreateOptions);

                /*
                    جديد PaymentIntent  ويعمل ليا E-Commerce account بياخد كلمه السر بتاع ال stripe هنا ال
                                                secret key كلمه السر هي ال
                */

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else
            {
                var paymentIntentUpdateOptions = new PaymentIntentUpdateOptions()
                {
                    Amount = PricePayment,

                };

                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, paymentIntentUpdateOptions);

                /*
                   PaymentIntent لل update ويعمل E-Commerce account بياخد كلمه السر بتاع ال stripe هنا ال
                                               secret key كلمه السر هي ال
                */

            }

            await basketRepository.UpdateBasketAsync(basket);

            return mapper.Map<CustomerBasketDTO>(basket);

        }

        public async Task UpdateOrderPaymentStatusAsync(string request, string stripeHeader)
        {

            var endpointSecret = configuration.GetSection("StripeSettings")["EndPointSecret"];

            var stripeEvent = EventUtility.ConstructEvent(request, stripeHeader, endpointSecret);

            var payMentIntent = stripeEvent.Data.Object as PaymentIntent;

            /*
                stripeEvent.Type ==> بتاعنا payment عشان نعرف الحالة بتاعت ال stripeEvent.Type بستخدم ال
              , وهكذا Succeeded ولا بقت Failed بتاعتنا ايه اللي حصل فيها هل بقت payment يعني عشان نعرف الحالة بتاعت ال

             */

            if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                await UpdatePaymentStatusFailed(payMentIntent?.Id!);
            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                await UpdatePaymentStatusReceived(payMentIntent?.Id!);
            }
            else
            {
                // Handle the event
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }

        }


        private async Task UpdatePaymentStatusFailed (string paymentIntentId)
        {
            //  get order by using paymentIntentId
            var order = await OrderObject.FindByIdAysnc(new OrderWithSpecificationPaymentIntentId(paymentIntentId))
                ?? throw new Exception();

            order.paymentStatus = OrderPaymentStatus.PaymentFailed;

            OrderObject.Update(order);

           await unitOfWork.ToSaveChanges();

        }

        private async Task UpdatePaymentStatusReceived(string paymentIntentId)
        {
            // 1. get order by using paymentIntentId
            var order = await OrderObject.FindByIdAysnc(new OrderWithSpecificationPaymentIntentId(paymentIntentId))
                ?? throw new Exception();

            order.paymentStatus = OrderPaymentStatus.PaymentRecevied;

            OrderObject.Update(order);

            await unitOfWork.ToSaveChanges();
        }

    }
}
