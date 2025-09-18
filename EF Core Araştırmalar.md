# Entity Framework Core - Loading Stratejileri ve Change Tracking

## Veri Yükleme Stratejileri

Entity Framework'te ilişkili verileri yüklemenin üç yolu vardır: Lazy Loading, Eager Loading ve Explicit Loading. Her birinin farklı kullanım senaryoları ve performans etkileri bulunmaktadır.

### Lazy Loading

Lazy Loading, navigation property'lere ilk kez erişildiğinde ilgili verilerin otomatik olarak yüklenmesi işlemidir.

**Konfigürasyon:**
```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseLazyLoadingProxies();
}
```

**Kullanım örneği:**
```csharp
var user = context.Users.First(); // Sadece User tablosu sorgulanır
Console.WriteLine(user.Name); // Normal property erişimi

// Navigation property'ye ilk erişimde otomatik sorgu çalışır
foreach (var rental in user.Rentals) // Burada ek sorgu tetiklenir
{
    Console.WriteLine(rental.StartDate);
}
```

**Oluşturulan SQL sorguları:**
```sql
-- İlk sorgu
SELECT TOP(1) [u].[Id], [u].[Name], [u].[Email] FROM [Users] AS [u]

-- user.Rentals'a erişildiğinde çalışan ikinci sorgu
SELECT [r].[Id], [r].[UserId], [r].[StartDate] 
FROM [Rentals] AS [r] 
WHERE [r].[UserId] = @userId
```

**N+1 Problem:**
```csharp
var users = context.Users.ToList(); // 1 sorgu

foreach (var user in users) // Her kullanıcı için
{
    Console.WriteLine($"Rentals: {user.Rentals.Count}"); // +1 sorgu
}
// Toplam sorgu sayısı: 1 + kullanıcı sayısı
```

**Lazy Loading'in dezavantajları:**
- N+1 problem riski yüksek
- DbContext'in dispose edilmemesi gerekir
- Asenkron operasyonlarda sorun yaratabilir
- Performans öngörüsü zor

### Eager Loading

Eager Loading, Include() metodu kullanılarak ilişkili verilerin ana sorgu ile birlikte yüklenmesi işlemidir.

**Temel kullanım:**
```csharp
var users = context.Users
    .Include(u => u.Rentals)
    .ToList();

// Bu noktada hem User hem de Rental verileri yüklü
foreach (var user in users)
{
    foreach (var rental in user.Rentals) // Ek sorgu yok
    {
        Console.WriteLine(rental.StartDate);
    }
}
```

**Çoklu seviye ilişkiler:**
```csharp
var users = context.Users
    .Include(u => u.Rentals)
        .ThenInclude(r => r.Bicycle)
    .Include(u => u.Profile)
    .ToList();
```

**String tabanlı Include:**
```csharp
var users = context.Users
    .Include("Rentals.Bicycle")
    .ToList();
```

**Filtreleme ile Include:**
```csharp
var users = context.Users
    .Include(u => u.Rentals.Where(r => r.IsActive))
    .ToList();
```

**Oluşturulan SQL:**
```sql
SELECT [u].[Id], [u].[Name], [r].[Id], [r].[UserId], [r].[StartDate]
FROM [Users] AS [u]
LEFT JOIN [Rentals] AS [r] ON [u].[Id] = [r].[UserId]
```

**Eager Loading'in avantajları:**
- Tek sorguda tüm veriler gelir
- N+1 problem riski yok
- Performans öngörüsü kolay

**Eager Loading'in dezavantajları:**
- Gereksiz veri yüklenebilir
- JOIN'ler kompleks hale gelebilir
- Bellek kullanımı yüksek olabilir
- Cartesian product sorunu

### Explicit Loading

Explicit Loading, verilerin manuel olarak ve istendiğinde yüklenmesi işlemidir.

**Collection loading:**
```csharp
var user = context.Users.First();

// Manuel olarak collection yükleme
context.Entry(user)
    .Collection(u => u.Rentals)
    .Load();

// Filtreleme ile yükleme
context.Entry(user)
    .Collection(u => u.Rentals)
    .Query()
    .Where(r => r.IsActive)
    .Load();
```

**Reference loading:**
```csharp
var rental = context.Rentals.First();

context.Entry(rental)
    .Reference(r => r.User)
    .Load();
```

**Asenkron yükleme:**
```csharp
await context.Entry(user)
    .Collection(u => u.Rentals)
    .LoadAsync();
```

## Change Tracking Mekanizması

Entity Framework varsayılan olarak yüklenen tüm entity'leri izler ve değişiklikleri takip eder.

### Tracking Nasıl Çalışır

```csharp
var user = context.Users.First(); // Entity tracked olarak yüklenir

user.Name = "Yeni İsim"; // Değişiklik tracked

context.SaveChanges(); // Sadece değişen alanlar güncellenir
```

**Change Tracker kontrolü:**
```csharp
var entries = context.ChangeTracker.Entries();
foreach (var entry in entries)
{
    Console.WriteLine($"Entity: {entry.Entity.GetType().Name}");
    Console.WriteLine($"State: {entry.State}");
}
```

### AsNoTracking()

Sadece okuma amaçlı sorgularda tracking'i devre dışı bırakır.

**Kullanım:**
```csharp
var users = context.Users
    .AsNoTracking()
    .ToList();

// Bu entity'ler tracked değil, değişiklikler kaydedilmez
users.First().Name = "Değişiklik";
context.SaveChanges(); // Hiçbir şey olmaz
```

**Global AsNoTracking:**
```csharp
context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
```

### AsNoTrackingWithIdentityResolution()

.NET 5 ile gelen bu özellik, tracking olmadan identity resolution sağlar.

```csharp
var data = context.Users
    .AsNoTrackingWithIdentityResolution()
    .Include(u => u.Rentals)
    .ToList();
```

## Performans Karşılaştırmaları

### Bellek Kullanımı

**Tracking aktif:**
```csharp
var users = context.Users.Take(1000).ToList();
// Her entity Change Tracker'da saklanır
// Bellek kullanımı: Entity boyutu + tracking bilgileri
```

**No tracking:**
```csharp
var users = context.Users.AsNoTracking().Take(1000).ToList();
// Sadece entity'ler bellekte
// Bellek kullanımı: Sadece entity boyutu
```

### Sorgu Performansı

**Eager Loading performansı:**
```csharp
// İyi performans
var result1 = context.Users
    .Include(u => u.Rentals)
    .AsNoTracking()
    .ToList();

// Kötü performans - çok fazla JOIN
var result2 = context.Users
    .Include(u => u.Rentals)
        .ThenInclude(r => r.Bicycle)
            .ThenInclude(b => b.Brand)
    .Include(u => u.Profile)
    .Include(u => u.Address)
    .ToList();
```

## Hangi Stratejiyi Ne Zaman Kullanmalı

### Lazy Loading kullanım senaryoları:
- Prototyping aşamasında
- Hangi verilerin kullanılacağı belirsizse
- Kod basitliği önemliyse
- N+1 problem kontrol altındaysa

### Eager Loading kullanım senaryoları:
- İlişkili veriler mutlaka kullanılacaksa
- Performans kritikse
- DbContext kısa süre aktif kalacaksa
- Az sayıda ilişki varsa

### Explicit Loading kullanım senaryoları:
- Koşullu veri yükleme gerekiyorsa
- Filtreleme ile veri yükleme gerekiyorsa
- Lazy loading devre dışıysa
- Asenkron veri yükleme gerekiyorsa

### AsNoTracking kullanım senaryoları:
- Read-only operasyonlar
- Raporlama sorguları
- API'den JSON döndürme
- Büyük veri setleri
- Dropdown/list dolduruması

## Pratik Örnekler

### Raporlama sorgusu:
```csharp
var report = context.Users
    .AsNoTracking()
    .Include(u => u.Rentals)
    .Select(u => new ReportDto
    {
        UserName = u.Name,
        RentalCount = u.Rentals.Count(),
        TotalAmount = u.Rentals.Sum(r => r.Amount)
    })
    .ToList();
```

### CRUD operasyonları:
```csharp
// Create
var user = new User { Name = "Test" };
context.Users.Add(user);
context.SaveChanges();

// Read
var existingUser = context.Users.Find(user.Id);

// Update
existingUser.Name = "Updated";
context.SaveChanges(); // Change tracking sayesinde otomatik UPDATE

// Delete
context.Users.Remove(existingUser);
context.SaveChanges();
```

### Projection kullanımı:
```csharp
// Sadece gerekli alanları seç
var userInfo = context.Users
    .Select(u => new
    {
        u.Name,
        u.Email,
        RentalCount = u.Rentals.Count()
    })
    .ToList();
// Bu durumda tracking gerekli değil çünkü entity döndürülmüyor
```