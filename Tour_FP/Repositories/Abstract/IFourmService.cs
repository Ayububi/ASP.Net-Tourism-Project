using Humanizer.Localisation;
using Tour_FP.Models.Domain;
using Tour_FP.Models.DTO;

namespace Tour_FP.Repositories.Abstract
{
    public interface IFourmService
    {

        bool Add(CommunityPostTable model);
        bool Update(CommunityPostTable model);
        CommunityPostTable GetById(int id);
        bool Delete(int id);
        IQueryable<CommunityPostTable> List();
        List<CommunityPostTable> ListWithComments();

    }
}
