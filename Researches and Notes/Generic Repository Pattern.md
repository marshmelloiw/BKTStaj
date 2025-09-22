# Generic Repository Pattern

Repository Pattern, veri erişim katmanını (Data Access Layer) iş
mantığından (Business Layer) ayırmak için kullanılan bir tasarım
desenidir.

Amaç, **veri erişim kodunu soyutlamak** ve üst katmanların veritabanı
detaylarından bağımsız çalışmasını sağlamaktır.


### Generic Repository Nedir?

Generic Repository, Repository Pattern'in daha **esnek ve yeniden
kullanılabilir** bir versiyonudur.
Her entity için ayrı ayrı repository yazmak yerine, **generic tip
parametresi** ile tüm entity'ler için ortak bir repository oluşturulur.

Böylece kod tekrarını azaltır, yönetilebilirliği artırır.



### Generic Repository Arayüzü

``` csharp
public interface IGenericRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Insert(T entity);
    void Update(T entity);
    void Delete(int id);
}
```


### Generic Repository Implementasyonu

``` csharp
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public void Insert(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        T entity = _dbSet.Find(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
```


### Örnek

``` csharp
public class UserService
{
    private readonly IGenericRepository<User> _userRepository;

    public UserService(IGenericRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAll();
    }

    public void AddUser(User user)
    {
        _userRepository.Insert(user);
    }
}
```
