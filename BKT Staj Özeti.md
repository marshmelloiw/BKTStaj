# BKT Staj Süreci

## Day 1 - Kurulum ve Ortam Hazırlığı

Bugün projenin temellerini attım. İlk olarak GitHub'da public bir repository oluşturdum ve gerekli geliştirme ortamlarını kurdum.

**Kurduğum Yazılımlar:**
- .NET 8 SDK (Stable sürüm)
- SQL Server Management Studio
- Visual Studio IDE

Stable sürümler konusunda araştırma yaptım ve .NET 7 ile .NET 8 arasındaki farkları detaylı bir şekilde inceledim. Bu karşılaştırmayı markdown formatında hazırlayıp GitHub repository'me yükledim. Özellikle performans iyileştirmeleri ve yeni özellikler üzerinde durdum.

---

## Day 2 - Veritabanı Temelleri

İkinci gün veritabanı konseptleri üzerine yoğunlaştım. SQL ve NoSQL arasındaki temel farkları araştırdım ve SQL'de normalizasyon kurallarını detaylı olarak inceledim.

**Çalıştığım Konular:**
- SQL vs NoSQL karşılaştırması
- Normalizasyon kuralları (1NF, 2NF, 3NF)
- Clustered ve Non-clustered indexlerin farkları
- Database constraint'ler
- Primary key ve Foreign key kavramları

KampusTek isimli bir relational database tasarımına başladım. DDL sorguları kullanarak temel tablo yapılarını oluşturdum ve aralarındaki ilişkileri kurdum.

---

## Day 3 - Mentor Toplantısı ve Teknik Derinlik

Bugün SQL Server'da tasarladığım database'in diyagramını oluşturdum. Akşam 18:00'da mentor ile Google Meet üzerinden toplantı yaptık.

Toplantıda tablo yapıları üzerinde konuştuk ve canlı olarak kod yazdık. Clustered ve non-clustered indexleri tekrar gözden geçirdim. Ayrıca C# ve LINQ konularında araştırma yaparak temel kavramları öğrendim.

Mentor ile yapılan kod yazma seansı oldukça verimli geçti. Özellikle tablo ilişkileri ve index kullanımı konularında pratik deneyim kazandım.

---

## Day 4 - Mimari Tasarım Desenleri

Bugün yazılım mimarisi konularına odaklandım. N-tier architecture kavramını detaylı olarak inceledim ve repository pattern implementasyonunu öğrendim.

**Çalışılan Konular:**
- N-tier architecture prensipleri
- Repository pattern implementasyonu
- Genel design patterns kavramları

Her konu için ayrı markdown dosyaları oluşturdum ve küçük örnek proje dosyalarını GitHub'a yükledim. Bu yaklaşım sayesinde bilgileri daha organize bir şekilde saklamış oldum.

---

## Day 5 - Veritabanı Optimizasyonu ve Entity Framework

Bugün veritabanı tasarımımda önemli iyileştirmeler yaptım. ID alanları için auto increment özelliğini ekledim ve veri tiplerini optimize ettim.

**Yapılan İyileştirmeler:**
- ID'ler için auto increment implementasyonu
- SQL case sensitivity kontrolü
- Usertype için ayrı tablo oluşturma
- Status alanları ekleme
- Veri tipi optimizasyonları (int vs varchar karşılaştırması)
  
+ Location alanları için enlem-boylam yapısı

Entity Framework kullanarak veritabanımı C# projeme bağladım. Code-first yaklaşımı yerine database-first yaklaşımını kullanarak mevcut veritabanından otomatik model oluşturdum.

**Veri Tipi Karşılaştırması:**

| Veri Tipi | Kapladığı Alan | Değer Aralığı | Kullanım Alanı |
|-----------|----------------|---------------|----------------|
| TINYINT | 1 Byte | 0-255 | Yaş, durum kodları |
| SMALLINT | 2 Byte | -32,768 - 32,767 | Küçük ID'ler |
| INT | 4 Byte | ~-2.1M - 2.1M | Standart ID'ler |
| BIGINT | 8 Byte | Çok geniş aralık | Büyük sistem ID'leri |

CRUD operasyonlarını başarıyla test ettim ve projeyi Git versiyon kontrolü altına aldım. .gitignore dosyası oluşturarak gereksiz dosyaları repository'den çıkardım.

---

## Day 7 - Web Projesi Geliştirme

Bugün MVC web projesi geliştirmeye başladım. Code-first yaklaşımını benimserek yeni bir proje oluşturdum.

**Proje Yapısı:**
- Data katmanı ve Presentation katmanının ayrı olduğu mimari
- Model sınıflarının bulunduğu yapı
- Repository pattern implementasyonu
- Veri erişim katmanının soyutlanması

İlk aşamada HTML arayüzü olmadan backend kısmını tamamladım. Temel CRUD operasyonlarının çalıştığını doğruladım.

---

## Day 8 - Frontend Entegrasyonu

Bugün projeye HTML arayüzünü ekledim. Ancak User kısmında UserType ilişkisinden kaynaklı problemlerle karşılaştım.

UserType foreign key ilişkisi beklendiği gibi çalışmıyordu. Bu sorunu çözmek için Entity Framework konfigürasyonlarını gözden geçirdim ve model ilişkilerini yeniden düzenledim.

Navigation property'leri doğru şekilde konfigüre ettikten sonra problem çözüldü.

---

## Day 9 - Proje Tamamlama

Bugün projenin tamamlanması üzerinde çalıştım. User kısmı artık tamamen çalışır durumda.

**Karşılaştığım Problemler ve Çözümleri:**
- Bicycle code'un unique olmaması sorunu - Database constraint ekleyerek çözdüm
- Rental sayfasının düzgün çalışmaması - Controller ve View arasındaki veri akışını düzelttim

Tüm functionality'leri test ettikten sonra projeyi başarıyla tamamladım. CRUD operasyonları, ilişkisel sorgular ve kullanıcı arayüzü tam olarak çalışır durumda.

---

## Day 10 - Dokümantasyon ve İleri Seviye Konular

Bugünkü çalışmamda Entity Framework’ün kritik konularına odaklandım. Öncelikle iki farklı loading yöntemi olan Lazy Loading ve Eager Loading hakkında detaylı araştırma yaparak kullanım senaryolarını ve avantaj/dezavantajlarını öğrendim. Ardından, tracking mekanizmasını inceledim ve özellikle AsNoTracking() kullanımının performans üzerindeki etkilerini araştırdım. Bu sayede Entity Framework’te veri yükleme stratejilerini ve değişiklik izleme yapısını daha iyi kavramış oldum. En son markdown dosyası olarak GitHub'a yükledim.

---

## Day 11
Proje bazında kütüphane araştırması yaptım. Bazı teknik konuların daha da oturması için kod üzerinde okuma yaptım
Akşam mentör ile meet yaparak ertesi günü çalışması için araştırma konuları belirledik

---

## Day 12
Araştırma konuları üzerinde detaylı incelemeler yapıp öğrenmeye çalıştım. Özellikle teknik kavramları derinlemesine okuyup notlar çıkararak konuları anlamaya odaklandım.
Üzerine çalıştığım konular:
- C#’ta Generics
- C# Collection Sınıfları
- Repository Pattern ve Generic Repository
- Abstract Sınıflar ve Kullanım Amaçları
- Bellek Yönetimi: Heap ve Stack
- Veritabanı Bağlantıları ve Trusted Connection
- Constructor Kavramı ve Overloading
- Web Güvenliği: CSRF ve Anti-Forgery Token
- ASP.NET Core’da Veri Taşıma Mekanizmaları: ViewBag, TempData, ViewData
- C# readonly Anahtar Kelimesi
- Dependency injection, constructor injection, property injection

---

## Day 13
Database’e anlamlı veriler kaydederek tüm tabloları doldurdum. Ardından SQL tarafında JOIN çeşitleri (INNER, LEFT, RIGHT, FULL OUTER) ve GROUP BY fonksiyonlarını (COUNT, SUM, AVG, MIN, MAX) çalıştım. Bu sorguların LINQ karşılıklarını da inceledim ve örneklerle pekiştirdim. Böylece SQL ve LINQ arasındaki benzerlikleri daha iyi kavradım.

---

## Day 14
AdventureWorks 2022 veritabanı üzerinde pratik yaptım. Özellikle JOIN çeşitleri ve GROUP BY fonksiyonlarını AdventureWorks tablolarında uygulayarak öğrendiklerimi pekiştirmeye çalıştım.
Rahatsızlandığım için bu günü verimli değerlendiremedim fakat temel sorgular üzerinde çalışmayı sürdürdüm.

---

## Day 15
Northwind Database'i üzerinde SQL sorgularına çalıştım. Mentörümün verdiği sorulara uygun SQL sorgularını yazdım. Github'a sorguların bulunduğu dosyayı ekledim

---

## Day 16
AuthController üzerinden login endpointi geliştirdim ve girişte kullanıcıya JWT token üretilmesini sağladım. User tablosuna şifre güvenliği için hash ve salt alanlarını ekledim. Program.cs tarafında hem Cookie Authentication (MVC) hem de JWT Authentication (API) yapılandırmasını yaptım. Ayrıca test amaçlı kullanıcı seeding ekledim.

---
