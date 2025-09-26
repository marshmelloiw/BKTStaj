using KampusTek.Business.Abstract;
using KampusTek.Data.Abstract;
using System.Linq.Expressions;

namespace KampusTek.Business.Concrete
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        protected readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual void Add(T entity)
        {
            _repository.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _repository.Update(entity);
        }

        public virtual void Delete(int id)
        {
            _repository.Delete(id);
        }

        public virtual void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        public virtual T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public virtual T GetById(Expression<Func<T, bool>> predicate)
        {
            return _repository.GetById(predicate);
        }

        public virtual List<T> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual List<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _repository.GetAll(predicate);
        }

        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _repository.Exists(predicate);
        }

        public virtual int Count()
        {
            return _repository.Count();
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return _repository.Count(predicate);
        }
    }
}
