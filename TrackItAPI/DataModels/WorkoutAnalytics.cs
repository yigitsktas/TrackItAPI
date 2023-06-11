namespace TrackItAPI.DataModels
{
	public class WorkoutAnalytics
	{
        public string? Name { get; set; }
        public List<WorkoutLogStat>? Logs { get; set; }
    }

	public class WorkoutLogStat
	{
		public string? Name { get; set; }
		public string? Reps { get; set; }
		public string? Weight { get; set; }
	}
}
