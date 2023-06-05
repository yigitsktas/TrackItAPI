using static TrackItAPI.Interfaces.IRepository;
using TrackItAPI.Repositories;
using TrackItAPI.UnitOfWork;

namespace TrackItAPI
{
    public static class RegisterServices
    {
        public static void AddRegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IFavRecipeRepository, FavRecipeRepository>();
            services.AddTransient<IMemberMetricRepository, MemberMetricRepository>();
            services.AddTransient<IMemberNutrientRepository, MemberNutrientRepository>();
            services.AddTransient<IMemberRepository, MemberRepository>();
            services.AddTransient<IMemberSpecificWorkoutRepository, MemberSpecificWorkoutRepository>();
            services.AddTransient<IMemberWorkoutLogRepository, MemberWorkoutLogRepository>();
            services.AddTransient<IMemberWorkoutLogStatRepository, MemberWorkoutLogStatRepository>();
            services.AddTransient<IMuscleGroupRepository, MuscleGroupRepository>();
            services.AddTransient<INutrientRepository, NutrientRepository>();
            services.AddTransient<IRecipeRepository, RecipeRepository>();
            services.AddTransient<IWorkoutRepository, WorkoutRepository>();
            services.AddTransient<IWorkoutTypeRepository, WorkoutTypeRepository>();
            services.AddTransient<IChatLogRepository, ChatLogRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}