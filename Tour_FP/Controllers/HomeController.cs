using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Tour_FP.Models.Domain;
using Tour_FP.Repositories.Abstract;

namespace Tour_FP.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _admincontext;
        private readonly IFileService _fileService;

        public HomeController(DatabaseContext admincontext, IFileService fileService)
        {
            _admincontext = admincontext;
            _fileService = fileService;

        }
        public IActionResult Index(string destination = "", string location = "")
        {

            IEnumerable<Admin_Dashboard> data = _admincontext.Admin;
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

        public IActionResult Book_Detail(int? id)
        {
            var data = _admincontext.Admin.Find(id);
            return View(data);
        }
    }
}
