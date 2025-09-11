# Çok Katmanlı (N-Tier) Mimari

Yazılım mühendisliğinde mimari seçimler, bir projenin ölçeklenebilirliği, sürdürülebilirliği ve yönetilebilirliği açısından kritik bir rol oynar. Bu mimarilerden en yaygın kullanılanlarından biri **Çok Katmanlı (N-Tier) Mimari**dir. N harfi, katman sayısının projeden projeye değişebileceğini ifade eder.  

Bu yaklaşımın temelinde **Separation of Concerns (Görevlerin Ayrılığı)** ilkesi bulunur. Yani her katman yalnızca kendi sorumluluk alanıyla ilgilenir, diğer katmanların işlevlerine doğrudan müdahale etmez. Böylece sistem daha modüler, bakımı daha kolay ve uzun vadede daha güvenilir bir hale gelir.  



## Katmanların Genel Yapısı  

### 1. Sunum Katmanı (Presentation Layer)  
Sunum katmanı, kullanıcının doğrudan etkileşim kurduğu arayüzdür. Web sayfaları, mobil uygulama ekranları, masaüstü pencereleri bu katmanda yer alır.  

- **Görevleri:**  
  - Kullanıcıdan veri toplamak ve iş mantığı katmanına aktarmak.  
  - İş mantığından gelen sonuçları kullanıcıya uygun formatta sunmak.  
  - Temel giriş doğrulamaları (örneğin e-posta formatı kontrolü) gerçekleştirmek.  

Sunum katmanı, veritabanına doğrudan erişmez; yalnızca iş mantığı katmanıyla iletişim kurar.  



### 2. İş Mantığı Katmanı (Business Logic Layer)  
Bu katman, uygulamanın kalbidir. Tüm kurallar, süreçler ve hesaplamalar burada yer alır.  

- **Görevleri:**  
  - Kullanıcıdan gelen verileri iş kurallarına göre doğrulamak.  
  - İş akışlarını yürütmek ve koordinasyonu sağlamak.  
  - Veri erişim katmanına hangi işlemlerin yapılacağını bildirmek.  

Örneğin bir e-ticaret uygulamasında indirim hesaplamaları, stok kontrolü veya kullanıcı yetkilendirme kontrolleri bu katmanda yapılır.  



### 3. Veri Erişim Katmanı (Data Access Layer)  
Veri erişim katmanı, veritabanı veya başka bir kalıcı depolama sistemi ile iletişim kurar.  

- **Görevleri:**  
  - CRUD (Create, Read, Update, Delete) işlemlerini gerçekleştirmek.  
  - Veritabanı bağlantısını yönetmek.  
  - Ham veriyi uygulamanın anlayabileceği modellere dönüştürmek.  

Bu katmanda iş kuralları yer almaz. Örneğin “ürünün indirimli olup olmadığını” hesaplamak DAL’ın görevi değildir; DAL sadece “ürünü kaydet” veya “ürünü getir” komutlarını uygular.  



## Yardımcı Katmanlar  

- **Entities / Domain Katmanı:** Uygulamanın temel veri modellerini (örneğin `Product`, `Customer`) içerir. Projenin en bağımsız katmanıdır.  
- **Core Katmanı:** Ortak altyapı kodlarını barındırır. Örneğin logging, caching veya generic repository arayüzleri burada bulunabilir.  



## Katmanlar Arası İletişim: Örnek Senaryo  

Bir e-ticaret sisteminde yeni ürün kaydı işlemini ele alalım:  

1. **Sunum Katmanı:** Kullanıcı form üzerinden ürün bilgilerini girer ve kaydetme talebinde bulunur.  
2. **İş Mantığı Katmanı:** Bilgileri iş kurallarına göre doğrular (örneğin fiyatın pozitif olup olmadığını kontrol eder).  
3. **Veri Erişim Katmanı:** İş mantığından gelen talimatla ürünü veritabanına kaydeder.  
4. **Veritabanı:** Ürün kalıcı olarak saklanır.  
5. **Geri Bildirim:** Sonuç bilgisi tekrar üst katmanlara iletilir ve kullanıcıya bildirilir.  



## Avantajları  

Çok Katmanlı Mimari, özellikle orta ve büyük ölçekli projelerde tercih edilir çünkü:  

- **Modülerlik:** Katmanlar bağımsızdır; biri değiştiğinde diğerleri minimum düzeyde etkilenir.  
- **Bakım Kolaylığı:** Hata ayıklama veya yeni özellik geliştirme süreçleri daha kontrollü ilerler.  
- **Esneklik:** Arayüz veya veritabanı teknolojisi değiştirildiğinde yalnızca ilgili katman güncellenir.  
- **Paralel Geliştirme:** Farklı ekipler aynı anda farklı katmanlarda çalışabilir.  
- **Test Edilebilirlik:** Katmanlar birbirinden izole edilerek test edilebilir.  