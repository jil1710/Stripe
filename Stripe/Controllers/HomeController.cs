using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe.Checkout;
using Stripe.Models;

namespace Stripe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private List<ProductEntity> _products;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _products = new List<ProductEntity>()
            {
                new ProductEntity(){Quantity=2,Id = "pro_123",Name="Stubborn Attachments",Photo="https://i.imgur.com/EHyR2nP.png",Price=290},
                new ProductEntity(){Quantity=1,Id = "pro_124",Name="Volly Ball",Photo="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSyj59aYIyr2u1g_dEUYS42Gm8RmOBy-tV7oQ&usqp=CAU",Price=1000},
                new ProductEntity(){Quantity=1,Id = "pro_125",Name="Nike AMG 123",Photo="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRx7L_Jwmo5mDR86Jk7cgWQdl1cwTVyiqPesA&usqp=CAU",Price=6000},
                new ProductEntity(){Quantity=1,Id = "pro_126",Name="MRF Bat",Photo="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT48TtE43Nh-kibsrrDhxK-ZV7gw-uc06--2w&usqp=CAU",Price=5000},
            };
        }

        public IActionResult Index()
        {
            return View(_products);
        }

        
        public IActionResult Checkout(string products)
        {
            var productItems = JsonConvert.DeserializeObject<List<ProductEntity>>(products);

            var domain = "https://localhost:7154/";

            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + "Home/TransactionSuccess",
                CancelUrl = domain + "Home/TransactionFail",
                ExpiresAt = DateTime.Now.AddMinutes(31),
                AllowPromotionCodes = true,
                PaymentMethodTypes = new List<string> { "card" },
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                {
                    AllowedCountries = new List<string> { "IN","US"},
                },
                ShippingOptions = new List<SessionShippingOptionOptions>
                {
                    new SessionShippingOptionOptions
                    {
                        ShippingRateData = new SessionShippingOptionShippingRateDataOptions
                        {
                            Type = "fixed_amount",
                            FixedAmount = new SessionShippingOptionShippingRateDataFixedAmountOptions
                            {
                                Amount = 0,
                                Currency = "inr",
                            },
                            DisplayName = "Free shipping",
                            DeliveryEstimate = new SessionShippingOptionShippingRateDataDeliveryEstimateOptions
                            {
                                Minimum = new SessionShippingOptionShippingRateDataDeliveryEstimateMinimumOptions
                                {
                                    Unit = "business_day",
                                    Value = 5,
                                },
                                Maximum = new SessionShippingOptionShippingRateDataDeliveryEstimateMaximumOptions
                                {
                                    Unit = "business_day",
                                    Value = 7,
                                },
                            },
                        },
                    },
                    new SessionShippingOptionOptions
                    {
                        ShippingRateData = new SessionShippingOptionShippingRateDataOptions
                        {
                            Type = "fixed_amount",
                            FixedAmount = new SessionShippingOptionShippingRateDataFixedAmountOptions
                            {
                                Amount = 1500,
                                Currency = "inr",
                            },
                            DisplayName = "Next day air",
                            DeliveryEstimate = new SessionShippingOptionShippingRateDataDeliveryEstimateOptions
                            {
                                Minimum = new SessionShippingOptionShippingRateDataDeliveryEstimateMinimumOptions
                                {
                                    Unit = "business_day",
                                    Value = 1,
                                },
                                Maximum = new SessionShippingOptionShippingRateDataDeliveryEstimateMaximumOptions
                                {
                                    Unit = "business_day",
                                    Value = 1,
                                },
                            },
                        },
                    },
                }


            };

            foreach (var item in productItems)
            {
                var sessionListItem = new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        UnitAmount = (long)(item.Price * item.Quantity) * 100,
                        Currency = "inr",
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = item.Name,
                            Images = new List<string>() { item.Photo }

                        }
                    },
                    Quantity = item.Quantity
                };

                options.LineItems.Add(sessionListItem);
            }

            var service = new SessionService();
            Session session = service.Create(options);
            TempData["Session"] = session.Id;

            return new JsonResult(session.Url);

        }


        public IActionResult TransactionFail()
        {
            return View();
        }


        public IActionResult TransactionSuccess()
        {
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());

            if (session.PaymentStatus == "paid")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Home/TransactionFail");
            }
        }


    }
}