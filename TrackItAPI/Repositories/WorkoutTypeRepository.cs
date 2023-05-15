using TrackItAPI.DataContext;
using TrackItAPI.Entities;
using TrackItAPI.Interfaces;
using static TrackItAPI.Interfaces.IRepository;

namespace TrackItAPI.Repositories
{
    public class WorkoutTypeRepository : GenericRepository<WorkoutType>, IWorkoutTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkoutTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int GetIDByName(string name)
        {
            var data = _context.WorkoutType.Where(x => x.Name == name);

            if (data != null)
            {
                var workoutType = data.FirstOrDefault();

                return workoutType.WorkoutTypeID;
            }
            else
            {
                return 0;
            }
        }
    }
}
