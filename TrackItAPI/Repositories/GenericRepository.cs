using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using TrackItAPI.DataContext;
using TrackItAPI.Interfaces;

namespace TrackItAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task Add(T model)
        {
            await Table.AddAsync(model);
        }

        public IEnumerable<T> GetAll()
        {
            return Table;
        }

        public T? GetByID(int id)
        {
            return Table.Find(id);
        }

        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> method)
        {
            return Table.Where(method);
        }

        public void Delete(T model)
        {
            Table.Remove(model);
        }

        public void DeleteById(int id)
        {
            var toRemove = Table.Find(id);

            if (toRemove != null)
                Table.Remove(toRemove);
        }

		public void DeleteByGUID(Guid id)
		{


			var toRemove = Table.Find(id);

			if (toRemove != null)
				Table.Remove(toRemove);
		}

		public void Update(T model)
        {
            Table.Update(model);
        }
    }
}
