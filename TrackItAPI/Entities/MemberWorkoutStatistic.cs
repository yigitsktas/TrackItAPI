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
		public int MemberID { get; set; }
		public int MemberWorkoutLogID { get; set; }
		public int ItemID { get; set; }
		public int? Reps { get; set; }
		public double? Weight { get; set; }
		public string? TableName { get; set; }
	}
}
