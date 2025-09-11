# Repository Pattern
Repository Pattern, yazılım geliştirmede veri erişim katmanının soyutlanması için kullanılan temel tasarım desenlerinden biridir. Domain-Driven Design (DDD) ve Clean Architecture gibi modern yaklaşımların vazgeçilmez bir parçası olan bu desen, veri kaynaklarına erişimin merkezi bir noktadan yönetilmesi ve bu erişimin iş mantığından tamamen ayrılması prensibi üzerine kuruludur.

## Repository Pattern Nedir?
Repository Pattern, veri erişim işlemlerinin kapsüllenmesi ve veri kaynağının teknik detaylarının gizlenmesi için tasarlanmış bir soyutlama katmanıdır. Bu desen sayesinde:
  - **Veri kaynağı soyutlanabilir** (veritabanı, dosya sistemi, web servisi)
  - **İş mantığı veri erişim detaylarından bağımsız hale gelir**
  - **Veri işlemleri merkezi olarak yönetilebilir**

### Basit Analoji
Repository'yi bir kütüphane gibi düşünmek mümkündür. Kütüphaneden kitap istendiğinde, kütüphanecinin kitabı hangi raftan, hangi bölümden aldığı önemli değildir. Sadece "şu kitabı istiyorum" denilir ve kitapçı getirir. Repository de benzer şekilde veri erişim detaylarını gizler.

## Temel Mantık
Repository Pattern'in temelinde **soyutlama** bulunur. Bu desen şu prensipler üzerine inşa edilmiştir:
### 1. Liskov Substitution Principle
Bir repository implementasyonu yerine başka bir implementasyon geçirildiğinde, kullanan kod hiçbir değişiklik olmadan çalışmaya devam edebilir.
### 2. Dependency Inversion Principle  
Üst seviye modüller (iş mantığı) alt seviye modüllere (veri erişim) bağımlı olmamalıdır. Her ikisi de soyutlamalara bağımlı olmalıdır.
### 3. Single Responsibility Principle
Her repository sadece bir entity veya aggregate root için veri erişim sorumluluğu taşır.

## Repository Pattern'in Bileşenleri

### 1. Repository Interface (Arayüz)
Veri erişim operasyonları tanımlanır ancak nasıl implement edileceği hakkında bilgi verilmez.

**Temel operasyonlar:**
  - `GetById()` - Belirli bir kayıt getirilir
  - `GetAll()` - Tüm kayıtlar getirilir  
  - `Add()` - Yeni kayıt eklenir
  - `Update()` - Mevcut kayıt güncellenir
  - `Delete()` - Kayıt silinir
  - Özelleştirilmiş sorgular (örn: `GetActiveUsers()`)

### 2. Concrete Repository (Somut Repository)
Interface'i implement eden ve gerçek veri erişim kodlarını içeren sınıftır. Bu sınıf:
  - Veritabanı bağlantıları yönetilir
  - SQL sorguları veya ORM çağrıları gerçekleştirilir
  - Veri dönüşümleri yapılır
  - Hata yönetimi sağlanır

### 3. Domain Models/Entities
Repository tarafından yönetilen veri yapılarıdır. İş kuralları ve veri yapısı temsil edilir.

## Repository Türleri
### 1. Concrete Repository (Özel Repository)
Her entity için ayrı repository sınıfları oluşturulur.
**Avantajları:**
  - Her entity'nin kendine özgü metodları bulunabilir
  - Daha spesifik ve anlaşılır kod yazılabilir
  - Type-safe operasyonlar gerçekleştirilebilir
**Dezavantajları:**
- Kod tekrarı oluşabilir
- Daha fazla sınıf yönetimi gerekir

### 2. Generic Repository (Genel Repository)
Tüm entity'ler için ortak temel operasyonlar sağlayan genel repository'dir.
**Avantajları:**
  - Kod tekrarı azaltılır
  - Hızlı geliştirme imkanı sağlanır
  - Tutarlı interface sunulur
**Dezavantajları:**
  - Entity-spesifik operasyonlar için ek çözümler gerekir
  - Bazen gereksiz metodlar expose edilebilir

### 3. Hybrid Approach (Karma Yaklaşım)
Generic repository'nin base class olarak kullanılması ve gerektiğinde özelleştirilmiş metodların eklenmesi yaklaşımıdır. Repository Pattern genellikle **Unit of Work** deseniyle birlikte kullanılır:
  - **Transaction yönetimi:** Birden fazla repository'nin aynı transaction içinde çalışması sağlanır
  - **Change tracking:** Hangi entity'lerin değiştiği takip edilir
  - **Batch operations:** Değişiklikler toplu olarak veritabanına gönderilir
  - **Consistency:** Veri tutarlılığı garanti edilir

### Repository + Unit of Work Avantajları:
  - Atomik işlemler gerçekleştirilebilir (ya hep ya hiç prensibi)
  - Performans optimizasyonu sağlanabilir
  - Veri tutarlılığı korunabilir
  - Complex business transaction'ları yönetilebilir

## Avantajları
  - İş mantığı veri erişim detaylarından tamamen izole edilebilir. Veritabanı türü değişse bile iş mantığı etkilenmez.
  - Mock repository'ler oluşturularak unit testler kolayca yazılabilir. Gerçek veritabanına ihtiyaç duyulmaz.
  - Esneklik: Aynı interface'i implement eden farklı veri kaynakları kullanılabilir:
      - SQL Server → PostgreSQL
      - Relational DB → NoSQL  
      - Database → Web API
      - Production → Test data
  - Tüm veri erişim kuralları tek yerde toplanabilir:
      - Caching stratejileri
      - Logging işlemleri  
      - Security kontrolleri
      - Performance monitoring
  - Veri erişim kodları merkezi olduğu için değişiklikler tek yerden gerçekleştirilebilir.
  - Paralel Geliştirme: Farklı geliştiriciler aynı anda farklı repository'ler üzerinde çalışabilir.

## Dezavantajları
  - Aşırı Soyutlama (Over-abstraction): Basit CRUD işlemleri için repository pattern gereksiz karmaşıklık yaratabilir.
  - Lazy loading, N+1 query problemleri yaşanabilir veya gereksiz veri transferi gerçekleşebilir
  - Zamanla repository'ler çok fazla metod içerebilir ve Single Responsibility Principle'ı ihlal edebilir.
  - Yeni geliştiriciler için öğrenme eğrisi gerekir.
  - Çok fazla interface ve implementasyon yönetimi gerekebilir.

Ancak her pattern gibi Repository Pattern de **her durumda gerekli değildir**. Proje boyutu, takım yapısı ve gereksinimler göz önünde bulundurularak karar verilmelidir. Fakat temel prensip, Complexity'yi justify edecek kadar value sağlanacağından emin olunmalıdır. Basit projeler için basit çözümler, karmaşık projeler için gelişmiş pattern'ler tercih edilebilir.
