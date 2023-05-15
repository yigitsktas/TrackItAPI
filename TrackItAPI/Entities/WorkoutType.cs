using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackItAPI.Entities
{
    [Table("WorkoutType")]
    public class WorkoutType
    {
        [Key]
        public int WorkoutTypeID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
