using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Tour_FP.Models.Domain;
using Tour_FP.Repositories.Abstract;
using Tour_FP.Repositories.Implementation;
using Tour_FP.Services;

namespace Tour_FP.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _dbContext;
        private readonly ICustomerService _customerService;
        private readonly IFileService _fileService;
        private readonly WeatherService _weatherService;

        public HomeController(DatabaseContext dbContext, IFileService fileService, ICustomerService customerService, WeatherService weatherService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _customerService= customerService;
            _weatherService = weatherService;

        }
        public IActionResult Index(string destination = "", string location = "")
        {

            IEnumerable<Admin_Dashboard> data = _dbContext.Admin;
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
            ViewBag.ReviewCountForDestination = new Dictionary<int, int>();
            ViewBag.AverageRatingForDestination = new Dictionary<int, double>();

            // Fetch review data for each destination and store in ViewBag
            foreach (var item in data)
            {
                int reviewCount = _dbContext.ReviewTable.Count(r => r.DestinationId == item.DestinationId);
                double averageRating = _dbContext.ReviewTable.Where(r => r.DestinationId == item.DestinationId).Average(r => r.Rating);

                // Store review count and average rating in ViewBag
                ViewBag.ReviewCountForDestination[item.DestinationId] = reviewCount;
                ViewBag.AverageRatingForDestination[item.DestinationId] = averageRating;
            }
            return View(data);
        }

        public async  Task<IActionResult> Destination_Detail(int? id)
        {
            
            var destination = _dbContext.Admin.FirstOrDefault(d => d.DestinationId == id);
            var reviews = _dbContext.ReviewTable.Where(r => r.DestinationId == id).ToList();
            // Get weather information for the destination's city
            string cityName = destination.Location; 
            string weatherInfo = await _weatherService.GetWeatherAsync(cityName);

            var viewModel = new DestinationWithReviewsViewModel
            {
                Destination = destination,
                Reviews = reviews,
                WeatherInfo = weatherInfo // Add the weather information to the view model
            };

            return View(viewModel);
           
        }

        
    }
}
