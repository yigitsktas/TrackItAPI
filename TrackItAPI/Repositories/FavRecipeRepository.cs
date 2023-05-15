using TrackItAPI.DataContext;
using TrackItAPI.Entities;
using TrackItAPI.Interfaces;
using static TrackItAPI.Interfaces.IRepository;

namespace TrackItAPI.Repositories
{
    public class FavRecipeRepository : GenericRepository<FavRecipe>, IFavRecipeRepository
    {
        private readonly ApplicationDbContext _context;

        public FavRecipeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
