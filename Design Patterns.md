# Tasarım Desenleri (Design Patterns)  
Bu iskeletin içindeki daha küçük ve spesifik problemler için kanıtlanmış çözümler sunar. Kod seviyesindeki yaygın sorunlara çözüm getirir.  
**Örnek:** Kat planı belli olan bir binanın içindeki mutfağı tasarlamak gibidir. *“Ocağı nereye koymalıyım?”*, *“Buzdolabı nerede durmalı?”* gibi spesifik sorunlara en verimli çözümü sunar.  

## Tasarım Desenleri Kaça Ayrılır?  
Tasarım desenleri, çözdükleri problemin doğasına göre, "Gang of Four" (Dörtlü Çete) olarak bilinen ünlü yazılımcıların kitabında tanımlandığı şekliyle üç ana kategoriye ayrılır:  

---

### 1. Yaratımsal Desenler (Creational Patterns)  
Bu desenler, nesnelerin kontrollü, esnek ve verimli bir şekilde **oluşturulmasıyla** ilgilenir. Nesne oluşturma mantığını gizleyerek, kodun daha dinamik hale gelmesini sağlar.  

**Amaç:** “Bir nesne nasıl ve ne zaman oluşturulmalı?” sorusuna cevap verir.  

- **Singleton:** Bir sınıftan yalnızca tek bir nesne (instance) olmasını sağlarken, bu örneğe global bir erişim noktası sağlar.  
- **Prototype:** Kodun sınıflara bağımlı hale gelmeden mevcut nesnelerin kopyalanabilmesini sağlar.  
- **Factory Method:** Üst sınıfta nesne yaratmak için bir arayüz sunar, ancak alt sınıfların oluşturulacak nesnelerin türünü değiştirebilmesini sağlar.  
- **Builder:** Karmaşık bir nesnenin adım adım ve daha okunaklı şekilde oluşturulabilmesini sağlar.  
- **Abstract Factory:** Somut sınıflar belirtilmeden birbiriyle ilişkili nesne ailelerinin üretilebilmesini sağlar.  

---

### 2. Yapısal Desenler (Structural Patterns)  
Bu desenler, sınıfların ve nesnelerin daha büyük ve kullanışlı yapılar oluşturmak için nasıl **bir araya getirilebileceğiyle** ilgilenir. Farklı parçaları birleştirerek yeni işlevler kazandırmayı hedefler.  

**Amaç:** “Nesneler ve sınıflar arasında nasıl ilişkiler kurarak daha büyük yapılar oluşturabiliriz?” sorusuna cevap verir.  

- **Adapter:** Birbirleriyle uyumsuz arayüzlere sahip iki sınıfın birlikte çalışabilmesini sağlar (Örn: Priz dönüştürücüleri gibi).  
- **Decorator:** Bir nesnenin mevcut yapısı bozulmadan, ona dinamik olarak yeni özellikler veya sorumluluklar eklenebilmesini sağlar.  
- **Facade:** Karmaşık bir alt sisteme (örneğin bir kütüphane veya bir grup sınıfa) basitleştirilmiş, tek bir arayüz sunar.  
- **Bridge:** Büyük bir sınıfı veya birbiriyle yakından ilişkili bir dizi sınıfı iki ayrı hiyerarşiye ayırarak, soyutlama ve gerçekleştirim kısımlarının birbirinden bağımsız olarak geliştirilebilmesini sağlar.  
- **Composite:** Nesnelerin ağaç yapıları şeklinde birleştirilip tek bir nesneymiş gibi kullanılabilmesini sağlar.  
- **Flyweight:** Birden fazla nesne arasında ortak durumların paylaşılabilmesini sağlayarak bellek kullanımını optimize eder.  
- **Proxy:** Başka bir nesne için vekil veya yer tutucu görevi görür. Orijinal nesneye erişim kontrol edilebilmesini sağlar.  

---

### 3. Davranışsal Desenler (Behavioral Patterns)  
Bu desenler, nesneler arasındaki **etkileşim, iletişim ve sorumluluk dağılımı** ile ilgilenir. Nesnelerin birlikte verimli bir şekilde çalışabilmesini tanımlar.  

**Amaç:** “Nesneler birbirleriyle nasıl haberleşmeli ve görevleri nasıl paylaşmalı?” sorusuna cevap verir.  

- **Chain of Responsibility:** İsteklerin bir işleyici zinciri boyunca iletilebilmesini sağlar.  
- **Command:** Bir isteğin bağımsız bir nesneye dönüştürülerek saklanabilmesini, sıraya alınabilmesini veya geri alınabilmesini sağlar.  
- **Iterator:** Bir koleksiyonun elemanları arasında, temel temsilini açığa çıkarmadan gezinilebilmesini sağlar.  
- **Mediator:** Nesneler arasındaki karmaşık bağımlılıkların azaltılabilmesini sağlar. İletişim yalnızca arabulucu üzerinden gerçekleşir.  
- **Memento:** Nesnenin önceki durumlarının kaydedilip geri yüklenebilmesini sağlar.  
- **Observer:** Bir nesnede meydana gelen değişikliklerin, ona abone olan diğer nesnelere bildirilebilmesini sağlar.  
- **State:** Nesnenin iç durumu değiştiğinde davranışlarının buna göre farklılaşabilmesini sağlar.  
- **Strategy:** Farklı algoritmaların tanımlanıp, birbirinin yerine kullanılabilmesini sağlar.  
- **Template Method:** Bir algoritmanın iskeletini üst sınıfta tanımlayıp, alt sınıflarda adımların yeniden tanımlanabilmesini sağlar.  
- **Visitor:** Algoritmaların, üzerinde çalıştıkları nesnelerden ayrılabilmesini sağlar.  
