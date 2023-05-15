using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackItAPI.Entities
{
    [Table("MemberMetric")]
    public class MemberMetric
    {
        [Key]
        public int MemberMetricID { get; set; }
        public int MemberID { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double BMI { get; set; }
        public double FatRate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
