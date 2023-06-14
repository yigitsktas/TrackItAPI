using Microsoft.EntityFrameworkCore;
using TrackItAPI.DataContext;
using TrackItAPI.Entities;
using TrackItAPI.Repositories;
using static TrackItAPI.Interfaces.IRepository;

namespace TrackItAPI.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;

        public IFavRecipeRepository FavRecipes { get; private set; }
        public IMemberRepository Members { get; private set; }
        public IMemberMetricRepository MemberMetrics { get; private set; }
        public IMemberNutrientRepository MemberNutrients { get; private set; }
        public IMemberSpecificWorkoutRepository MemberSpecificWorkouts{ get; private set; }
        public IMemberWorkoutLogRepository MemberWorkoutLogs { get; private set; }
        public IMemberWorkoutLogStatRepository MemberWorkoutLogStat { get; private set; }
        public IMuscleGroupRepository MuscleGroups { get; private set; }
        public INutrientRepository Nutrients { get; private set; }
        public IRecipeRepository Recipes { get; private set; }
        public IWorkoutRepository Workouts { get; private set; }
        public IWorkoutTypeRepository WorkoutTypes { get; private set; }
        public IChatLogRepository ChatLogs { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            FavRecipes = new FavRecipeRepository(_context);
            Members = new MemberRepository(_context);
            MemberMetrics = new MemberMetricRepository(_context);
            MemberNutrients = new MemberNutrientRepository(_context);
            MemberSpecificWorkouts = new MemberSpecificWorkoutRepository(_context);
            MemberWorkoutLogs = new MemberWorkoutLogRepository(_context);
            MemberWorkoutLogStat = new MemberWorkoutLogStatRepository(_context);
            MuscleGroups = new MuscleGroupRepository(_context);
            Nutrients = new NutrientRepository(_context);
            Recipes = new RecipeRepository(_context);
            Workouts = new WorkoutRepository(_context);
            WorkoutTypes = new WorkoutTypeRepository(_context);
			ChatLogs = new ChatLogRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveAsync()
        {
            _context.SaveChanges();
        }
    }
}
