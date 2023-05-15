using TrackItAPI.DataContext;
using TrackItAPI.Entities;
using TrackItAPI.Interfaces;
using static TrackItAPI.Interfaces.IRepository;

namespace TrackItAPI.Repositories
{
    public class MemberSpecificWorkoutRepository : GenericRepository<MemberSpecificWorkout>, IMemberSpecificWorkoutRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberSpecificWorkoutRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
