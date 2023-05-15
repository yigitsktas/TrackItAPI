using static TrackItAPI.Interfaces.IRepository;

namespace TrackItAPI.UnitOfWork
{
    public interface IUnitOfWork
    {
        IFavRecipeRepository FavRecipes { get; }
        IMemberRepository Members { get; }
        IMemberMetricRepository MemberMetrics { get; }
        IMemberNutrientRepository MemberNutrients { get; }
        IMemberSpecificWorkoutRepository MemberSpecificWorkouts { get; }
        IMemberWorkoutLogRepository MemberWorkoutLogs{ get; }
        IMemberWorkoutLogStatRepository MemberWorkoutLogStat { get; }
        IMuscleGroupRepository MuscleGroups{ get; }
        INutrientRepository Nutrients { get; }
        IRecipeRepository Recipes{ get; }
        IWorkoutRepository Workouts { get; }
        IWorkoutTypeRepository WorkoutTypes { get; }

        void SaveAsync();
        void Dispose();
    }
}
