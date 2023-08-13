using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using Tour_FP.Models.Domain;
using Tour_FP.Repositories.Abstract;

namespace Tour_FP.Repositories.Implementation
{
    public class FourmServices : IFourmService
    {
        private readonly DatabaseContext ctx;
        public FourmServices(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public bool Add(CommunityPostTable model)
        {
            try
            {
                ctx.PostTable.Add(model);
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
                ctx.PostTable.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public CommunityPostTable GetById(int id)
        {
            return ctx.PostTable.Find(id);
        }

        public IQueryable<CommunityPostTable> List()
        {
            var data = ctx.PostTable.AsQueryable();
            return data;
        }

        public bool Update(CommunityPostTable model)
        {
            try
            {
                ctx.PostTable.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<CommunityPostTable> ListWithComments()
        {
            return ctx.PostTable.Include(p => p.Comments).ToList();
        }

    }
}
