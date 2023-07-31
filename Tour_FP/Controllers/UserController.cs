using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Tour_FP.Models.Domain;
using Tour_FP.Repositories.Abstract;
using static NuGet.Packaging.PackagingConstants;

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
            // Get the current logged-in user
            var user = await _userManager.GetUserAsync(User);

            // Retrieve the CustomerDetail records for the logged-in user
            var userCustomerDetails = _dbContext.Customer
                .Where(cd => cd.UserId == user.Id)
                .ToList();

            // Get the DestinationIds associated with the logged-in user
            var destinationIds = userCustomerDetails.Select(cd => cd.DestinationId).ToList();

            // Retrieve the Admin_Dashboard records for the DestinationIds
            var adminDashboardsForUserDestinations = _dbContext.Admin
                .Where(ad => destinationIds.Contains(ad.DestinationId))
                .ToList();

            // Create the view model by combining CustomerDetail and Admin_Dashboard data
            var viewModel = new CustomerDashboardViewModel
            {
                CustomerInfo = userCustomerDetails,
                DestinationInfo = adminDashboardsForUserDestinations
            };

            return View(viewModel);
        }


        public IActionResult Index()
        {
            var data = this._adminService.List();
            return View(data);
        }
      

    }
}
