using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackItAPI.Entities
{
    [Table("MemberSpecificWorkout")]
    public class MemberSpecificWorkout
    {
        [Key]
        public int MemberSpecificWorkoutID { get; set; }
        public int MuscleGroupID { get; set; }
        public int WorkoutTypeID { get; set; }
        public int MemberID { get; set; }
        public string? WorkoutName { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
    }
}
