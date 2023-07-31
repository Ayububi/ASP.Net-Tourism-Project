using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stripe.Checkout;
using System.Security.Claims;
using System.Text;
using Tour_FP.Models.Domain;
using Tour_FP.Repositories.Abstract;
using Tour_FP.Repositories.Implementation;

namespace Tour_FP.Controllers
{
    [Authorize(Roles = "User")]
    public class BookingController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAdminService _adminService;

        public BookingController(ICustomerService CustomerService, UserManager<ApplicationUser> userManager,IAdminService adminService)
        {
            _customerService = CustomerService;
            _adminService = adminService;
            _userManager = userManager;


        }

        public IActionResult Booking_Form(int destinationId)
        {
            ViewData["DestinationId"] = destinationId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Booking_FormAsync(CustomerDetail model)
        {
            var user = await _userManager.GetUserAsync(User);

            // Ensure the user is not null before accessing the Id property
            if (user != null)
            {
                model.UserId = user.Id; // Assuming UserId is of type string
            }
            model.Status = "Pending";

            if (!ModelState.IsValid)
            {
                // Output ModelState errors to the console
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }

                return View(model);
            }

            HttpContext.Session.Set("PendingBooking", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model)));
            return RedirectToAction(nameof(CheckOut), new { destinationId = model.DestinationId });


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




        public IActionResult CheckOut(int destinationId)
        {
            var destination = _adminService.GetById(destinationId);

            // Ensure the destination is not null
            if (destination == null)
            {
                return NotFound();
            }

            var domain = "https://localhost:7044/";
            var option = new SessionCreateOptions
            {
                SuccessUrl = domain + "Booking/PaymentSuccess?sessionId={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "CheckOut/Login",
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = destination.Price*100,
                    Currency = "pkr",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = destination.CountryName,
                    }
                },
                Quantity = 1,
            }
        },
                Mode = "payment",
                PaymentMethodTypes = new List<string> { "card" },
                CustomerEmail = "ayububi1234@gmial.com",
            };

            var services = new SessionService();
            Session session = services.Create(option);
            Response.Headers.Add("location", session.Url);

            return new StatusCodeResult(303);
        }









    }
}
