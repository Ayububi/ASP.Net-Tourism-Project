using Humanizer.Localisation;
using Tour_FP.Models.Domain;
using Tour_FP.Models.DTO;

namespace Tour_FP.Repositories.Abstract
{
    public interface IAdminService
    {

        bool Add(Admin_Dashboard model);
        bool Update(Admin_Dashboard model);
        Admin_Dashboard GetById(int id);
        bool Delete(int id);
        IQueryable<Admin_Dashboard> List();
    }
}
