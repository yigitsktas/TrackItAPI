using TrackItAPI.DataContext;
using TrackItAPI.Entities;
using TrackItAPI.Interfaces;
using static TrackItAPI.Interfaces.IRepository;

namespace TrackItAPI.Repositories
{
    public class MemberWorkoutLogRepository : GenericRepository<MemberWorkoutLog>, IMemberWorkoutLogRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberWorkoutLogRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
