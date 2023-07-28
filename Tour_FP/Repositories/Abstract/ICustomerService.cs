using Humanizer.Localisation;
using Tour_FP.Models.Domain;
using Tour_FP.Models.DTO;

namespace Tour_FP.Repositories.Abstract
{
    public interface ICustomerService
    {

        bool Add(CustomerDetail model);
        bool Update(CustomerDetail model);
        CustomerDetail GetById(int id);
        bool Delete(int id);
        IQueryable<CustomerDetail> List();
        
    }
}
