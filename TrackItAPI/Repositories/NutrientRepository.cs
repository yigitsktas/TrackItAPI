using TrackItAPI.DataContext;
using TrackItAPI.Entities;
using TrackItAPI.Interfaces;
using static TrackItAPI.Interfaces.IRepository;

namespace TrackItAPI.Repositories
{
    public class NutrientRepository : GenericRepository<Nutrient>, INutrientRepository
    {
        private readonly ApplicationDbContext _context;

        public NutrientRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int GetIDByName(string name)
        {
            var data = _context.Nutrients.Where(x => x.NutrientName == name);

            if (data != null)
            {
                var nutrient = data.FirstOrDefault();

                return nutrient.NutrientID;
            }
            else
            {
                return 0;
            }
        }
    }
}
