using TrackItAPI.DataContext;
using TrackItAPI.Entities;
using TrackItAPI.Interfaces;
using static TrackItAPI.Interfaces.IRepository;

namespace TrackItAPI.Repositories
{
    public class WorkoutRepository : GenericRepository<Workout>, IWorkoutRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkoutRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
