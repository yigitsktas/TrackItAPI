using Microsoft.AspNetCore.Mvc.Rendering;
using TrackItAPI.Entities;

namespace TrackItAPI.Interfaces
{
    public interface IRepository
    {
        public interface IFavRecipeRepository : IGenericRepository<FavRecipe>
        {
           
        }

        public interface IMemberRepository : IGenericRepository<Member>
        {

        }

        public interface IMemberMetricRepository : IGenericRepository<MemberMetric>
        {

        }

        public interface IMemberNutrientRepository : IGenericRepository<MemberNutrient>
        {
            List<MemberNutrient> SqlRaw(DateTime MyDate, int id);
		}

		public interface IMemberSpecificWorkoutRepository : IGenericRepository<MemberSpecificWorkout>
        {

        }

        public interface IMemberWorkoutLogRepository : IGenericRepository<MemberWorkoutLog>
        {

        }

        public interface IMemberWorkoutLogStatRepository : IGenericRepository<MemberWorkoutLogStat>
        {

        }

        public interface IMuscleGroupRepository : IGenericRepository<MuscleGroup>
        {
            int GetIDByName(string name);
        }

        public interface INutrientRepository : IGenericRepository<Nutrient>
        {
            int GetIDByName(string name);
        }

        public interface IRecipeRepository : IGenericRepository<Recipe>
        {

        }

        public interface IWorkoutRepository : IGenericRepository<Workout>
        {

        }

        public interface IWorkoutTypeRepository : IGenericRepository<WorkoutType>
        {
            int GetIDByName(string name);
        }
    }
}
