using Microsoft.EntityFrameworkCore;
using TrackItAPI.DataContext;
using TrackItAPI.Entities;
using TrackItAPI.Interfaces;
using static TrackItAPI.Interfaces.IRepository;

namespace TrackItAPI.Repositories
{
    public class MemberNutrientRepository : GenericRepository<MemberNutrient>, IMemberNutrientRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberNutrientRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int GetIDByName(string name)
        {
            var data = _context.Nutrients.Where(x => x.NutrientName == name);

            if (data != null)
            {
                var nutrient = data.FirstOrDefault();

                return nutrient.NutrientID;
            }
            else
            {
                return 0;
            }
        }

		public List<MemberNutrient> SqlRaw(DateTime MyDate, int id)
		{
            string str = "SELECT * FROM MemberNutrient WHERE " + "DATEDIFF(day, CreatedDate,'" + MyDate.ToString("yyyy-MM-dd") + "')= 0" + " AND MemberID = " + id;

			var data = _context.MemberNutrient.FromSqlRaw(str).ToList();

            return data;
		}
	}
}
