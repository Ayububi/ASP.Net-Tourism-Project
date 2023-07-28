using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using Tour_FP.Models.Domain;
using Tour_FP.Repositories.Abstract;

namespace Tour_FP.Controllers
{
   
    public class BookingController : Controller
    {
        private readonly ICustomerService _customerService;
       

        public BookingController(ICustomerService CustomerService)
        {
            _customerService = CustomerService;
          

        }

        public IActionResult Booking_Form()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Booking_Form(CustomerDetail model)
        {

            if (!ModelState.IsValid)
                return View(model);

            var Data = _customerService.Add(model);
            if (Data)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(CheckOut));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }



        }

        public IActionResult CheckOut()
        {
            List<Admin_Dashboard> productlist = new List<Admin_Dashboard>();

            productlist = new List<Admin_Dashboard>
            {
                new Admin_Dashboard
                {
                    CountryName="london",
                    Location="Gold",
                    Price=360,
               
                },
            };
            var domain = "https://localhost:7044/";
            var option = new SessionCreateOptions 
            {
                SuccessUrl=domain+$"CheckOut/OrderConfirmation",
                CancelUrl=domain+ "CheckOut/Login" ,
                LineItems=new List<SessionLineItemOptions>(),
                Mode="payment",
                CustomerEmail="ayububi1234@gmial.com",
            };
            foreach(var item in productlist)
            {
                var SessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = item.Price*100 ,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.CountryName.ToString(),
                        }
                    },

                    Quantity=item.Price
                };
                option.LineItems.Add(SessionListItem);

            }
            var services = new SessionService();
            Session session = services.Create(option);
            Response.Headers.Add("location", session.Url);

            return new StatusCodeResult(303);
        }








    }
}
