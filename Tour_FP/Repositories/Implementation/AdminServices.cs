using Humanizer.Localisation;
using Tour_FP.Models.Domain;
using Tour_FP.Repositories.Abstract;

namespace Tour_FP.Repositories.Implementation
{
    public class AdminServices : IAdminService
    {
        private readonly DatabaseContext ctx;
        public AdminServices(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public bool Add(Admin_Dashboard model)
        {
            try
            {
                ctx.Admin.Add(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                ctx.Admin.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Admin_Dashboard GetById(int id)
        {
            return ctx.Admin.Find(id);
        }

        public IQueryable<Admin_Dashboard> List()
        {
            var data = ctx.Admin.AsQueryable();
            return data;
        }

        public bool Update(Admin_Dashboard model)
        {
            try
            {
                ctx.Admin.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
