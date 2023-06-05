using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackItAPI.Entities
{
    [Table("Recipe")]
    public class Recipe
    {
        [Key]
        public int RecipeID { get; set; }
        public Guid GUID { get; set; }
        public int MemberID { get; set; }
        public string? Summary { get; set; }
        public string? Directions { get; set; }
        public int? Serve { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public string? Ingredients { get; set; }
        public int Season { get; set; }
        public int ServingSize { get; set; }
        public int ServingType { get; set; }
        public int HealthLevel { get; set; }
    }
}
