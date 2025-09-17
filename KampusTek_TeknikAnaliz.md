# KampusTek Bisiklet Kiralama Sistemi

## Proje Mimarisi (N-Tier Architecture)

Proje **3 katmanlı (N-Tier)** mimari kullanıyor:

1.  **KampusTek.Entities** - Veri modelleri (Entity Layer)
2.  **KampusTek.Data** - Veri erişim katmanı (Data Access Layer)
3.  **KampusTek.Business** - İş mantığı katmanı (Business Logic Layer)
4.  **KampusTekWebApp** - Sunum katmanı (Presentation Layer)


## Veri Modelleri (Entities)

``` csharp
public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string CellNumber { get; set; } = null!;
    public int UserTypeId { get; set; }
    public virtual UserType? UserType { get; set; }
    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
```

**Ana Modeller:** 
- **User**: Kullanıcı bilgileri (Ad, soyad, email,
telefon, kullanıcı tipi)
- **Bicycle**: Bisiklet bilgileri (Kod, model, renk, durum, mevcut
istasyon)
- **Station**: İstasyon bilgileri (İsim, konum, kapasite)
- **Rental**: Kiralama işlemleri (Başlangıç/bitiş zamanı, istasyonlar)
- **Maintenance**: Bakım kayıtları
- **UserType**: Kullanıcı tipleri (Student/Staff)


## Veri Erişim Katmanı (Data Layer)

``` csharp
public class KampusTekDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserType> UserTypes { get; set; }
    public DbSet<Bicycle> Bicycles { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }
}
```

**Özellikler:** 
- **Entity Framework Core** ile SQL Server bağlantısı
- **Fluent API** ile ilişki konfigürasyonları
- **Unique constraint** bisiklet kodları için
- **Seed data** ile UserType verileri


## Service Katmanı (Business Logic)

**Service Pattern** kullanılıyor.

### Abstract (Interface)

``` csharp
public interface IUserService
{
    void Add(User user);
    void Update(User user);
    void Delete(int id);
    User GetById(int id);
    List<User> GetAll();
}
```

### Concrete (Implementation)

``` csharp
public class UserService : IUserService
{
    private readonly KampusTekDbContext _context;

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
}
```

**Service'ler:** - **UserService**: Kullanıcı CRUD işlemleri
- **BicycleService**: Bisiklet CRUD işlemleri
- **StationService**: İstasyon CRUD işlemleri
- **RentalService**: Kiralama işlemleri (EndRental özel metodu)
- **MaintenanceService**: Bakım işlemleri
- **UserTypeService**: Kullanıcı tipi işlemleri


## Controller Katmanı (Presentation Layer)

**MVC Pattern** ile çalışıyor.

``` csharp
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IUserTypeService _userTypeService;

    public UserController(IUserService userService, IUserTypeService userTypeService)
    {
        _userService = userService;
        _userTypeService = userTypeService;
    }
}
```

**Controller'lar:** 
- **UserController**: Kullanıcı yönetimi
- **BicycleController**: Bisiklet yönetimi
- **StationController**: İstasyon yönetimi
- **RentalController**: Kiralama yönetimi
- **MaintenanceController**: Bakım yönetimi


## Veri Akışı (Data Flow)

1.  **View** → Kullanıcı arayüzü
2.  **Controller** → HTTP isteklerini karşılar
3.  **Service** → İş mantığını uygular
4.  **DbContext** → Veritabanı işlemleri
5.  **SQL Server** → Veri saklama


## Dependency Injection

``` csharp
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserTypeService, UserTypeService>();
builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddScoped<IBicycleService, BicycleService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();
```

**Scoped** lifetime ile her HTTP isteği için aynı instance kullanılıyor.


## İlişkiler (Relationships)

-   **User ↔ UserType** (Many-to-One)
-   **User ↔ Rental** (One-to-Many)
-   **Bicycle ↔ Station** (Many-to-One)
-   **Bicycle ↔ Rental** (One-to-Many)
-   **Bicycle ↔ Maintenance** (One-to-Many)
-   **Station ↔ Rental** (One-to-Many, Start/End)

