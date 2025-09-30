MVC Request Pipeline Döngüsü

## 1. İstemci Tarafı (Client-Side) - OSI Layer 7 (Application)

### Razor View ve Form Hazırlama
Kullanıcı bir Razor View üzerinden form doldurur:

```cshtml
<form asp-action="Create" method="post">
    @Html.AntiForgeryToken()
    <input asp-for="FirstName" />
    <input asp-for="LastName" />
    <button type="submit">Save</button>
</form>
```

**Razor Tag Helper'ların Görevi:**
- Form action URL'ini oluşturur (`/User/Create`)
- Alan isimlerini model property'leriyle eşleştirir
- CSRF koruması için Antiforgery Token ekler
- Model validation attribute'larını client-side validation'a çevirir

### Tarayıcı HTTP Request Oluşturur
```http
POST /User/Create HTTP/1.1
Host: example.com
Content-Type: application/x-www-form-urlencoded
Cookie: .AspNetCore.Antiforgery=...; .AspNetCore.Identity=...
Content-Length: 156

__RequestVerificationToken=xyz&FirstName=John&LastName=Doe&...
```

---

## 2. OSI Katmanlarında Request İletimi

### OSI Layer 7 (Application Layer)
- **HTTP protokolü** aktif
- Tarayıcı HTTP POST isteğini hazırlar
- Headers, body, cookies eklenir

### OSI Layer 6 (Presentation Layer)
- **TLS/SSL şifreleme** (HTTPS kullanılıyorsa)
- İçerik encoding (gzip, deflate)
- Character set dönüşümleri (UTF-8)

### OSI Layer 5 (Session Layer)
- **TCP oturum yönetimi**
- Keep-alive bağlantıları
- Session persistence

### OSI Layer 4 (Transport Layer)
- **TCP protokolü**
- Port numarası eklenir (genellikle 443 HTTPS için)
- Segmentlere bölünür
- Sıralama ve hata kontrolü (checksum)

### OSI Layer 3 (Network Layer)
- **IP protokolü**
- Kaynak ve hedef IP adresleri eklenir
- Paketlere bölünür
- Routing kararları alınır

### OSI Layer 2 (Data Link Layer)
- **Ethernet/WiFi protokolü**
- MAC adresleri eklenir
- Frame'lere bölünür
- Yerel ağ iletimi

### OSI Layer 1 (Physical Layer)
- **Fiziksel kablo/kablosuz sinyal**
- Elektrik/ışık/radyo sinyalleri
- Bit'ler iletilir

---

## 3. Sunucu Tarafında Paket İşleme (Server)

### OSI Layer 1-4: Ağ Yığını (Network Stack)
İşletim sistemi seviyesinde:
- Fiziksel sinyaller alınır
- Frame'ler işlenir
- IP paketleri birleştirilir
- TCP segmentleri sıralanır ve kontrol edilir
- **TCP port 443** üzerinden dinleyen **Kestrel** web server'a iletilir

### OSI Layer 5-7: Uygulama Seviyesi
Kestrel web server HTTP isteğini parse eder.

---

## 4. Kestrel Web Server ve Middleware Pipeline

### Kestrel (Web Server)
```
TCP Stream → HTTP Parser → HttpContext nesnesi
```

**HttpContext oluşturulur:**
- Request (Headers, Body, Query, Form)
- Response (StatusCode, Headers, Body)
- User (Identity, Claims)
- Session, Items, Features

### Middleware Hattı (Program.cs)
```csharp
app.UseHttpsRedirection();      // 1. HTTPS kontrolü
app.UseStaticFiles();           // 2. Statik dosya kontrolü
app.UseRouting();               // 3. Endpoint matching
app.UseAuthentication();        // 4. Kimlik doğrulama
app.UseAuthorization();         // 5. Yetkilendirme
app.MapControllers();           // 6. Endpoint execution
```

**Her middleware sırayla:**
1. İsteği inceleyebilir
2. İsteği değiştirebilir
3. Yanıt üretebilir (pipeline'ı sonlandırır)
4. Sonraki middleware'e devredebilir
5. Yanıt dönüşünde işlem yapabilir

---

## 5. Routing ve Endpoint Seçimi (MVC ve API)

### Route Matching
```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
   .RequireAuthorization();
```

**URL Analysis:**
```
/User/Create
 ↓
controller = "User"
action = "Create"
HTTP Method = POST
```

```csharp
UserController.Create(User user) [HttpPost]
```
Attribute routing (API) ve conventional routing (MVC) birlikte çalışabilir.

---

## 6. Authentication (Kimlik Doğrulama) - OSI Layer 7

### Cookie Authentication (MVC için)
```csharp
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });
```

**İşlem Adımları:**
1. Cookie'den `.AspNetCore.Identity` okunur
2. Cookie decrypt edilir
3. ClaimsPrincipal nesnesi oluşturulur
4. `HttpContext.User` property'sine atanır

### JWT Authentication (API için)
```csharp
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = "YourIssuer",
        ValidAudience = "YourAudience",
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
```

**API Request Header:**
```http
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

---

## 7. Authorization (Yetkilendirme) - OSI Layer 7

```csharp
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    ...
}
```

**Kontrol Adımları:**
1. Kullanıcı authenticate mi? → Hayırsa login'e yönlendir
2. "Admin" rolü var mı? → Yoksa 403 Forbidden
3. İkisi de OK → Action method'a geç

---

## 8. Model Binding - OSI Layer 7

### Otomatik Veri Bağlama
```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(User user)
```

**Binding Sources (Öncelik sırasıyla):**
1. **Form data:** `FirstName=John&LastName=Doe`
2. **Route values:** `/User/Create/{id}`
3. **Query string:** `?userId=123`
4. **Headers:** Custom headers
5. **Body:** JSON/XML (API için)

**Model Binding Engine:**
```
Form data → Value Provider → Model Binder → User object
FirstName=John → user.FirstName = "John"
LastName=Doe → user.LastName = "Doe"
```

---

## 9. Model Validation - OSI Layer 7

### Data Annotations
```csharp
public class User
{
    [Required(ErrorMessage = "Ad zorunludur")]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [EmailAddress]
    public string Email { get; set; }
}
```

### Validation Pipeline
```csharp
if (ModelState.IsValid)
{
    // İşleme devam
}
else
{
    // Hataları göster
    return View(user);
}
```

**ModelState içeriği:**
```
ModelState.IsValid = false
ModelState["FirstName"].Errors[0] = "Ad zorunludur"
```

---

## 10. CSRF Protection (MVC) - OSI Layer 7

```csharp
[ValidateAntiForgeryToken]
```

**İşlem:**
1. Form'daki hidden token okunur: `__RequestVerificationToken=xyz`
2. Cookie'deki token okunur: `.AspNetCore.Antiforgery=abc`
3. İkisi kriptografik olarak eşleştirilir
4. Eşleşmezse → 400 Bad Request

---

## 11. Business Logic ve Dependency Injection - OSI Layer 7

### DI Container (Program.cs)
```csharp
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContext<KampusTekDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
```

### Controller Constructor Injection
```csharp
public class UserController : Controller
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
}
```

### Service Katmanı
```csharp
public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    
    public void Add(User user)
    {
        // İş kuralları
        if (_repository.EmailExists(user.Email))
            throw new BusinessException("Email zaten kullanımda");
            
        _repository.Add(user);
        _repository.SaveChanges();
    }
}
```

---

## 12. Data Access Layer (Repository Pattern) - OSI Layer 7

### Repository
```csharp
public class UserRepository : IUserRepository
{
    private readonly KampusTekDbContext _context;
    
    public void Add(User user)
    {
        _context.Users.Add(user);
    }
    
    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
```

### Entity Framework Core - SQL Generation
```sql
INSERT INTO Users (FirstName, LastName, Email, CreatedDate)
VALUES ('John', 'Doe', 'john@example.com', '2025-09-30')
```

### Database Transaction
- SQL Server'a TCP bağlantısı (OSI Layer 4-7)
- TDS protokolü (Tabular Data Stream)
- Transaction başlatılır
- INSERT komutu çalıştırılır
- COMMIT yapılır

---

## 13. Response Generation (Yanıt Üretimi) - OSI Layer 7

### Başarılı Senaryo
```csharp
return RedirectToAction(nameof(Index));
```

**HTTP Response:**
```http
HTTP/1.1 302 Found
Location: /User/Index
Set-Cookie: ...
```

### Hata Senaryosu
```csharp
return View(user);
```

**Razor View Engine:**
1. Create.cshtml bulunur
2. Model (user) ile birleştirilir
3. HTML üretilir
4. Validation mesajları eklenir

**HTTP Response:**
```http
HTTP/1.1 200 OK
Content-Type: text/html; charset=utf-8

<!DOCTYPE html>
<html>
...
<span class="text-danger">Ad zorunludur</span>
...
</html>
```

---

## 14. Response'un Client'a Dönüşü (OSI Layer 7 → 1)

### Middleware Pipeline (Reverse)
Response, middleware'lerden **geriye doğru** geçer:
```
Controller → Authorization → Authentication → Routing → 
StaticFiles → HTTPS → Kestrel
```

Her middleware response'u inceleyebilir/değiştirebilir.

### OSI Layer 7-1 (Tersine İşlem)
```
Application Layer:   HTTP response headers + body
Presentation Layer:  TLS şifreleme, gzip compression
Session Layer:       TCP session yönetimi
Transport Layer:     TCP segmentlere bölünür
Network Layer:       IP paketlere bölünür
Data Link Layer:     Ethernet frame'lere bölünür
Physical Layer:      Elektrik/ışık sinyalleri
```

---

## 15. Client-Side (Tarayıcı) - OSI Layer 7

### Response İşleme
1. TCP paketleri birleştirilir
2. TLS decrypt edilir
3. HTTP response parse edilir

### Redirect Senaryosu (302)
```javascript
// Tarayıcı otomatik yeni istek yapar
GET /User/Index HTTP/1.1
```

### View Senaryosu (200)
- HTML parse edilir
- DOM oluşturulur
- CSS uygulanır
- JavaScript çalıştırılır (validation scriptleri)
- Sayfa render edilir

---

## Şematik Özet

```
┌─────────────────────────────────────────────────────────────┐
│ CLIENT (Browser - OSI Layer 7)                              │
│ Razor Form → HTTP POST /User/Create                         │
└────────────────────┬────────────────────────────────────────┘
                     │
              OSI Layer 6-1
         (TLS, TCP, IP, Ethernet)
                     │
                     ▼
┌─────────────────────────────────────────────────────────────┐
│ SERVER - Network Stack (OSI Layer 1-4)                      │
│ Physical → Data Link → Network → Transport                  │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────────┐
│ Kestrel Web Server (OSI Layer 5-7)                          │
│ TCP Stream → HTTP Parser → HttpContext                      │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────────┐
│ Middleware Pipeline                                         │
│ ├─ UseHttpsRedirection                                      │
│ ├─ UseStaticFiles                                           │
│ ├─ UseRouting ─────────────┐                                │
│ ├─ UseAuthentication       │                                │
│ ├─ UseAuthorization        │                                │
│ └─ MapControllers          │                                │
└────────────────────────────┼────────────────────────────────┘
                             │
                             ▼
                    ┌────────────────┐
                    │ Routing Engine │
                    │ /User/Create   │
                    └────────┬───────┘
                             │
                             ▼
            ┌────────────────────────────────┐
            │ Authentication & Authorization │
            │ Cookie/JWT → Claims → Roles    │
            └────────────┬───────────────────┘
                         │
                         ▼
            ┌────────────────────────────────┐
            │ Model Binding & Validation     │
            │ Form → User object             │
            │ CSRF Token Check               │
            └────────────┬───────────────────┘
                         │
                         ▼
            ┌────────────────────────────────┐
            │ UserController.Create()        │
            │ [HttpPost, ValidateAntiForgery]│
            └────────────┬───────────────────┘
                         │
                         ▼
            ┌────────────────────────────────┐
            │ Business Layer (DI)            │
            │ IUserService.Add(user)         │
            └────────────┬───────────────────┘
                         │
                         ▼
            ┌────────────────────────────────┐
            │ Data Access Layer              │
            │ Repository → DbContext         │
            └────────────┬───────────────────┘
                         │
                         ▼
            ┌────────────────────────────────┐
            │ Database (SQL Server)          │
            │ INSERT INTO Users...           │
            └────────────┬───────────────────┘
                         │
                         ▼
            ┌────────────────────────────────┐
            │ Response Generation            │
            │ Redirect (302) or View (200)   │
            └────────────┬───────────────────┘
                         │
                         ▼
            Middleware Pipeline (Reverse) → Kestrel
                         │
                         ▼
              OSI Layer 7-1 (Reverse)
                         │
                         ▼
            ┌────────────────────────────────┐
            │ CLIENT (Browser)               │
            │ New Page Render or Validation  │
            └────────────────────────────────┘
```

## Önemli Notlar

1. **OSI Layer 1-4:** İşletim sistemi ve ağ donanımı tarafından yönetilir
2. **OSI Layer 5-7:** Uygulama (Kestrel, ASP.NET Core) tarafından yönetilir
3. **Middleware sırası kritiktir:** Yanlış sıralama güvenlik açıklarına neden olabilir
4. **Authentication ≠ Authorization:** Önce kim olduğun (AuthN), sonra ne yapabileceğin (AuthZ) kontrol edilir
5. **Model binding otomatiktir:** Ancak kompleks senaryolarda custom binder yazılabilir
6. **DI container request başına scope oluşturur:** Her HTTP request için yeni service instance'ları
7. **Response middleware'lerden tersine geçer:** Request → | → Response ←
