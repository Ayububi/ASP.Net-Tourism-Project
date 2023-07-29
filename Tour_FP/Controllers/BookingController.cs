using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stripe.Checkout;
using System.Text;
using Tour_FP.Models.Domain;
using Tour_FP.Repositories.Abstract;

namespace Tour_FP.Controllers
{
    [Authorize(Roles = "User")]
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

            model.Status = "Pending";
            HttpContext.Session.Set("PendingBooking", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model)));

            return RedirectToAction(nameof(CheckOut));


        }
        public IActionResult PaymentSuccess(string sessionId)
        {
            // Retrieve the booking data from the session
            byte[] bookingData;
            if (HttpContext.Session.TryGetValue("PendingBooking", out bookingData))
            {
                var booking = JsonConvert.DeserializeObject<CustomerDetail>(Encoding.UTF8.GetString(bookingData));

                // Update the booking status to "paid" in the database using the UpdateStatus method
                _customerService.UpdateStatus(booking, "paid");

                // Save the booking data to the database using the Add method
                _customerService.Add(booking);

                // Remove the booking data from the session after it has been processed
                HttpContext.Session.Remove("PendingBooking");
            }

            return RedirectToAction("Index","Home");
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
                SuccessUrl = domain + "Booking/PaymentSuccess?sessionId={CHECKOUT_SESSION_ID}",
                CancelUrl =domain+ "CheckOut/Login" ,
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
