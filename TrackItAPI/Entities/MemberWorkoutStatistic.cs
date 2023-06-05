using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackItAPI.Entities
{
    [Table("MemberWorkoutLogStat")]
    public class MemberWorkoutLogStat
    {
        [Key]
		public int MemberStatisticsID { get; set; }
		public Guid GUID { get; set; }
		public int MemberWorkoutLogID { get; set; }
		public int ItemID { get; set; }
		public int Sets { get; set; }
		public int Reps { get; set; }
		public double Weight { get; set; }
		public string? Notes { get; set; }
		public string? TableName { get; set; }
		public bool isDone { get; set; }
	}
}
