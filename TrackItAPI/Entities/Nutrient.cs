using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackItAPI.Entities
{
    [Table("Nutrient")]
    public class Nutrient
    {
        [Key]
        public int NutrientID { get; set; }
        public string? NutrientName { get; set; }
        public string? Brand { get; set; }
        public int NutrientType { get; set; }
        public int Calorie { get; set; }
        public double TotalFat { get; set; }
        public double TotalCarb { get; set; }
        public double TotalProtein { get; set; }
        public double TotalSugar { get; set; }
    }
}
