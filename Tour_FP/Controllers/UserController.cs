using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tour_FP.Models.Domain;
using Tour_FP.Repositories.Abstract;

namespace Tour_FP.Controllers
{
    [Authorize(Roles ="User")]
    public class UserController : Controller
    {
        private readonly DatabaseContext _dbContext;
        private readonly ICustomerService _customerService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAdminService _adminService;

        public UserController(DatabaseContext dbContext,ICustomerService CustomerService, UserManager<ApplicationUser> userManager,IAdminService adminService)
        {
            _dbContext = dbContext;
            _customerService = CustomerService;
            _adminService = adminService;
            _userManager = userManager;


        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);

            // Retrieve all bookings for the customer
            var customerBookings = _dbContext.Customer
                .Include(c => c.Admin_Dashboard)
                .Where(c => c.UserId == user.Id)
                .ToList();

            // If the user has made bookings, create the ViewModel
            if (customerBookings.Count > 0)
            {
                var viewModel = new List<CustomerDashboardViewModel>();

                foreach (var customerBooking in customerBookings)
                {
                    var bookingViewModel = new CustomerDashboardViewModel
                    {
                        CustomerInfo = customerBooking,
                        DestinationInfo = customerBooking.Admin_Dashboard
                    };

                    viewModel.Add(bookingViewModel);
                }

                return View(viewModel);
            }

            // Handle the case where the user has not made any bookings yet
            return View();
        }


        public IActionResult Index()
        {
            var data = this._adminService.List();
            return View(data);
        }
      

    }
}
