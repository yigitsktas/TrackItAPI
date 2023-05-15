using TrackItAPI.DataContext;
using TrackItAPI.Entities;
using TrackItAPI.Interfaces;
using static TrackItAPI.Interfaces.IRepository;

namespace TrackItAPI.Repositories
{
    public class MemberWorkoutLogStatRepository : GenericRepository<MemberWorkoutLogStat>, IMemberWorkoutLogStatRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberWorkoutLogStatRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
