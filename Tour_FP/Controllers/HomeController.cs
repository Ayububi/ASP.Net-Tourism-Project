using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Tour_FP.Models.Domain;
using Tour_FP.Repositories.Abstract;
using Tour_FP.Repositories.Implementation;

namespace Tour_FP.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _Customercontext;
        private readonly ICustomerService _customerService;
        private readonly IFileService _fileService;

        public HomeController(DatabaseContext Customercontext, IFileService fileService, ICustomerService customerService)
        {
            _Customercontext = Customercontext;
            _fileService = fileService;
            _customerService= customerService;

        }
        public IActionResult Index(string destination = "", string location = "")
        {

            IEnumerable<Admin_Dashboard> data = _Customercontext.Admin;
            if (!string.IsNullOrEmpty(destination))
            {
                // Filter the data based on the Destination (city) field
                data = data.Where(item => item.CountryName.Contains(destination, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(location))
            {
                // Filter the data based on the Location (within city) field
                data = data.Where(item => item.Location.Contains(location, StringComparison.OrdinalIgnoreCase));
            }
            return View(data);
        }

        public IActionResult Destination_Detail(int? id)
        {
            var data = _Customercontext.Admin.Find(id);
            return View(data);
        }

        
    }
}
