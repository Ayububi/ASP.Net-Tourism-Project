using Microsoft.EntityFrameworkCore;
using Tour_FP.Models.Domain;

namespace Tour_FP.Models.Domain
{
    [Keyless]
    public class CustomerDashboardViewModel
    {
        public List<CustomerDetail>? CustomerInfo { get; set; }
        public List<Admin_Dashboard>? DestinationInfo { get; set; }
    }
}
