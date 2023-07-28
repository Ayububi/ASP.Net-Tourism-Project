using Humanizer.Localisation;
using Tour_FP.Models.Domain;
using Tour_FP.Repositories.Abstract;

namespace Tour_FP.Repositories.Implementation
{
    public class CustomerServices : ICustomerService
    {
        private readonly DatabaseContext ctx;
        public CustomerServices(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public bool Add(CustomerDetail model)
        {
            try
            {
                ctx.Customer.Add(model);
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
                ctx.Customer.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

      public  CustomerDetail GetById(int id)
        {
            return ctx.Customer.Find(id);
        }

       public IQueryable<CustomerDetail> List()
        {
            var data = ctx.Customer.AsQueryable();
            return data;
        }

        public bool Update(CustomerDetail model)
        {
            try
            {
                ctx.Customer.Update(model);
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
