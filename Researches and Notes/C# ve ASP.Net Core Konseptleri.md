# C# ve ASP.NET Core Konseptleri

### Hangi Veriler Nerede Tutulur? (Heap vs Stack)

-   **Stack**: Küçük ve kısa ömürlü veriler tutulur. (değer tipleri →
    int, double, bool, struct)
-   **Heap**: Büyük ve dinamik ömürlü veriler tutulur. (referans tipleri
    → class, object, string)
-   Stack hızlıdır, Heap daha esnektir.

### Trusted Connection Ne İşe Yarar?

-   Veritabanına bağlanırken **Windows kimlik doğrulaması** kullanılır.
-   Kullanıcı adı/şifre vermeden, Windows oturumu üzerinden güvenli
    giriş yapılır.
-   Özellikle **SQL Server** bağlantılarında sıkça tercih edilir.


### Constructor'un Farklı Şekillerde Yazılabilmesi Mümkün mü?

-   Evet, **constructor overloading** yapılabilir.
-   Aynı sınıfta birden fazla constructor tanımlanabilir, farklı
    parametreler alabilir.

``` csharp
public class Kullanici
{
    public string Ad { get; set; }
    public Kullanici() { }
    public Kullanici(string ad) { Ad = ad; }
}
```

-   Ayrıca `new` parametresi ile nesne oluştururken bu constructor
    seçilebilir.

#### Constructor Overloading

Aynı sınıfta birden fazla constructor tanımlanabilir (overloading).

``` csharp
public class Kullanici
{
    public string Ad;

    public Kullanici() { }                // parametresiz
    public Kullanici(string ad) { Ad = ad; } // parametreli
}

var k1 = new Kullanici();         // parametresiz çağrı
var k2 = new Kullanici("Melisa"); // parametreli çağrı
```


###  ValidateAntiForgeryToken

-   ASP.NET Core'da **CSRF saldırılarını** önlemek için kullanılır.
-   Form gönderimlerinde, kullanıcıya özel bir token oluşturur ve
    kontrol eder.
-   Token eşleşmezse istek reddedilir.

CSRF (Cross-Site Request Forgery) Saldırıları
"Siteler Arası İstek Sahteciliği" saldırısıdır.
Amaç, kullanıcının tarayıcısı üzerinden, onun haberi olmadan yetkili bir işlem yaptırmaktır.


Nasıl Çalışır?
* Kullanıcı, güvenilir bir sitede (örneğin internet bankacılığı) oturum açar.
* Bu sırada tarayıcıda oturum çerezi (session cookie) saklanır.
* Kullanıcı saldırganın gönderdiği zararlı linke / forma tıklar.
* Tarayıcı, otomatik olarak geçerli çerezleri de ekleyerek isteği bankaya gönderir.
* Banka, isteğin gerçek kullanıcıdan gelip gelmediğini ayırt edemez → işlem yapılır.


### ViewBag vs TempData

-   ViewBag → Controller'dan View'a **tek yönlü veri aktarımı**
    sağlar. Request bitince kaybolur.
-   TempData → Bir request'ten diğerine veri taşır (Redirect sonrası
    bile). Daha uzun ömürlüdür.


### readonly

-   `readonly` ile işaretlenen değişkenin değeri **yalnızca
    tanımlandığında** veya **constructor içinde** değiştirilebilir.
-   Sonradan başka yerde değiştirilmesi engellenir.
-   Sabit gibi davranır ama **constructor içinde esneklik sağlar**.

``` csharp
public class Ornek
{
    public readonly int Sayi;
    public Ornek(int s) { Sayi = s; }
}
```