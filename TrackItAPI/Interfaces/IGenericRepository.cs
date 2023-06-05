using System.Linq.Expressions;

namespace TrackItAPI.Interfaces
{

    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll(); 
        
        T? GetByID(int id);

        IEnumerable<T> GetWhere(Expression<Func<T, bool>> method); 

        Task Add(T model);

        void Update(T Model);

        void DeleteById(int id);

        void DeleteByGUID(Guid id);

        void Delete(T model);
    }
}
