using Azure.Core;
using Microsoft.EntityFrameworkCore;
using TrackItAPI.Entities;

namespace TrackItAPI.DataContext
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FavRecipe> FavRecipes { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberMetric> MemberMetrics { get; set; }
        public virtual DbSet<MemberNutrient> MemberNutrient { get; set; }
        public virtual DbSet<MemberSpecificWorkout> MemberSpecificWorkouts { get; set; }
        public virtual DbSet<MemberWorkoutLog> MemberWorkoutLogs { get; set; }
        public virtual DbSet<MemberWorkoutLogStat> MemberWorkoutLogStats { get; set; }
        public virtual DbSet<MuscleGroup> MuscleGroups { get; set; }
        public virtual DbSet<Nutrient> Nutrients { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<Workout> Workouts { get; set; }
        public virtual DbSet<WorkoutType> WorkoutType { get; set; }
        public virtual DbSet<ChatLog> ChatLogs { get; set; }
    }
}
