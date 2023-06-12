namespace TrackItAPI.DataModels
{
	public class MemberWorkoutLogStat_DM
	{
		public int MemberStatisticID { get; set; }
		public string? WorkoutName { get; set; }
		public string? MuscleGroup { get; set; }
		public int? Reps { get; set; }
		public double? Weight { get; set; }
	}
}
