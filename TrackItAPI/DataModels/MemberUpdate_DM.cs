namespace TrackItAPI.DataModels
{
	public class MemberUpdate_DM
	{
		public int MemberID { get; set; }
		public int MemberMetricID { get; set; }
		public string? Username { get; set; }
		public string? EMail { get; set; }
		public double Height { get; set; }
		public double Weight { get; set; }
		public double BMI { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}
