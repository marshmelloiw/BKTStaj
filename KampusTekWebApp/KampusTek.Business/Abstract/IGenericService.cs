using System.Linq.Expressions;

namespace KampusTek.Business.Abstract
{
    public interface IGenericService<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void Delete(T entity);
        T GetById(int id);
        T GetById(Expression<Func<T, bool>> predicate);
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> predicate);
        bool Exists(Expression<Func<T, bool>> predicate);
        int Count();
        int Count(Expression<Func<T, bool>> predicate);
    }
}
