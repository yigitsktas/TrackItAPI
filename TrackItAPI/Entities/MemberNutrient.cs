using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackItAPI.Entities
{
    [Table("MemberNutrient")]
    public class MemberNutrient
    {
        [Key]
        public int MemberNutrientID { get; set; }
        public int MemberID { get; set; }
        public int NutrientID { get; set; }
        public string? Notes { get; set; }
        public double ServingSize { get; set; }
        public int ServingType { get; set; }
        public DateTime CreatedDate{ get; set; }
    }
}
