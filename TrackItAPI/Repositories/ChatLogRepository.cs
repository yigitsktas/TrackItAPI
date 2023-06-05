using TrackItAPI.DataContext;
using TrackItAPI.Entities;
using TrackItAPI.Interfaces;
using static TrackItAPI.Interfaces.IRepository;

namespace TrackItAPI.Repositories
{
    public class ChatLogRepository : GenericRepository<ChatLog>, IChatLogRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatLogRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
