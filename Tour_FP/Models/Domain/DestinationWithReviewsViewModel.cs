using Microsoft.EntityFrameworkCore;
using Stripe;
using Tour_FP.Models.Domain;

namespace Tour_FP.Models.Domain
{
    [Keyless]
    public class DestinationWithReviewsViewModel
    {
        public Admin_Dashboard? Destination { get; set; }
        public List<ReviewTable>? Reviews { get; set; }

        public string WeatherInfo { get; set; }
    }
}
