using TrackItAPI.DataContext;
using TrackItAPI.Entities;
using TrackItAPI.Interfaces;
using static TrackItAPI.Interfaces.IRepository;

namespace TrackItAPI.Repositories
{
    public class MuscleGroupRepository : GenericRepository<MuscleGroup>, IMuscleGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public MuscleGroupRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int GetIDByName(string name)
        {
            var data = _context.MuscleGroups.Where(x => x.Name == name);

            if (data != null)
            {
                var muscleGroup = data.FirstOrDefault();

                return muscleGroup.MuscleGroupID;
            }
            else
            {
                return 0;
            }
        }
    }
}
