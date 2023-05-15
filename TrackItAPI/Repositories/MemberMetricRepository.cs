using TrackItAPI.DataContext;
using TrackItAPI.Entities;
using TrackItAPI.Interfaces;
using static TrackItAPI.Interfaces.IRepository;

namespace TrackItAPI.Repositories
{
    public class MemberMetricRepository : GenericRepository<MemberMetric>, IMemberMetricRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberMetricRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
