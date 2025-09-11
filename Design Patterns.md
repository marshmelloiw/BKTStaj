# Tasarım Desenleri (Design Patterns)

## Tasarım Deseni (Design Pattern) Nedir?
Tasarım desenleri, yazılım geliştirmede sıkça karşılaşılan ve tekrar eden sorunlara yönelik kanıtlanmış, yeniden kullanılabilir çözümlerdir. Bu tarifler size tam olarak bitmiş bir kod vermez; size belirli bir problemi nasıl çözeceğinize dair en iyi yöntemi, adımları ve prensipleri sunar. Örnek: "Ocağı nereye koymalıyım?", "Buzdolabı nerede durmalı?" gibi spesifik sorunlara en verimli çözümü tasarlamak gibidir.

## Tasarım Desenleri Kaça Ayrılır?
Tasarım desenleri, çözdükleri problemin doğasına göre, "Gang of Four" (Dörtlü Çete) olarak bilinen ünlü yazılımcıların kitabında tanımlandığı şekliyle üç ana kategoriye ayrılır:

### 1. Yaratımsal Desenler (Creational Patterns)
Bu desenler, nesnelerin (object) kontrollü, esnek ve verimli bir şekilde **yaratılmasıyla** ilgilenir. Nesne oluşturma mantığını gizleyerek, kodun daha dinamik hale gelmesini sağlarlar.

**Amaç:** "Bir nesne nasıl ve ne zaman oluşturulmalı?" sorusuna cevap verir.

- **Singleton:** Bir sınıftan yalnızca tek bir nesne (instance) olmasını sağlarken, bu örneğe global bir erişim noktası sunar.
- **Prototype:** Kodunuzu sınıflarına bağımlı hale getirmeden mevcut nesneleri kopyalamanızı sağlar.
- **Factory Method:** Üst sınıfta nesne yaratmak için bir arayüz sağlar, ancak alt sınıfların oluşturulacak nesnelerin türünü değiştirmesine olanak tanır.
- **Builder:** Karmaşık bir nesneyi (örneğin çok sayıda ayarı olan bir nesneyi) adım adım ve daha okunaklı bir şekilde oluşturmayı sağlar.
- **Abstract Factory:** Somut sınıflarını belirtmeden birbiriyle ilişkili nesne aileleri üretebilmeyi sağlar.

### 2. Yapısal Desenler (Structural Patterns)
Bu desenler, sınıfların ve nesnelerin daha büyük ve kullanışlı yapılar oluşturmak için nasıl **bir araya getirileceğiyle** ilgilenir. Farklı parçaları birleştirerek yeni işlevler kazandırmayı hedefler.

**Amaç:** "Nesneler ve sınıflar arasında nasıl ilişkiler kurarak daha büyük yapılar oluşturabiliriz?" sorusuna cevap verir.

- **Adapter:** Birbirleriyle uyumsuz arayüzlere sahip iki sınıfın birlikte çalışmasını sağlar (Örn: Priz dönüştürücüleri gibi).
- **Decorator:** Bir nesnenin mevcut yapısını bozmadan, ona dinamik olarak yeni özellikler veya sorumluluklar eklemeyi sağlar.
- **Facade:** Karmaşık bir alt sisteme (Örneğin bir kütüphane veya bir grup sınıfa) basitleştirilmiş, tek bir arayüz sunar.
- **Bridge:** Büyük bir sınıfı veya birbiriyle yakından ilişkili bir dizi sınıfı iki ayrı hiyerarşiye ayırmanıza olanak tanır: soyutlama ve gerçekleştirim. Bu hiyerarşiler birbirinden bağımsız olarak geliştirilebilir.
- **Composite:** Nesneleri ağaç yapıları şeklinde birleştirmenizi ve ardından bu yapılarla sanki tek bir nesneymiş gibi çalışmanızı sağlar.
- **Flyweight:** Tüm veriyi her nesnede tutmak yerine, birden fazla nesne arasında durumun (state) ortak kısımlarını paylaşarak mevcut RAM miktarına daha fazla nesne sığdırmanıza olanak tanır.
- **Proxy:** Başka bir nesne için bir vekil veya yer tutucu sağlamanıza olanak tanır. Bir vekil, orijinal nesneye erişimi kontrol ederek, istek orijinal nesneye ulaşmadan önce veya ulaştıktan sonra bir şeyler yapmanıza imkan tanır.

### 3. Davranışsal Desenler (Behavioral Patterns)
Bu desenler, nesneler arasındaki **etkileşim, iletişim ve sorumluluk dağılımı** ile ilgilenir. Nesnelerin birlikte verimli bir şekilde nasıl çalışacağını tanımlar.

**Amaç:** "Nesneler birbirleriyle nasıl haberleşmeli ve görevleri nasıl paylaşmalı?" sorusuna cevap verir.

- **Chain of Responsibility:** İstekleri bir işleyici (handler) zinciri boyunca iletmenizi sağlar. Her işleyici, isteği işleme veya zincirdeki bir sonraki işleyiciye iletme kararı verir.
- **Command:** Bir isteği, isteğe ilişkin tüm bilgileri içeren bağımsız bir nesneye dönüştürür. Bu dönüşüm, istekleri metot argümanı olarak geçirmenize, bir isteğin yürütülmesini geciktirmenize veya sıraya koymanıza ve geri alınabilir işlemleri desteklemenize olanak tanır.
- **Iterator:** Bir koleksiyonun temel temsilini (liste, yığın, ağaç vb.) açığa çıkarmadan elemanları arasında gezinmenizi sağlar.
- **Mediator:** Nesneler arasındaki kaotik bağımlılıkları azaltmanızı sağlar. Bu desen, nesneler arasındaki doğrudan iletişimi kısıtlar ve onları yalnızca bir arabulucu nesne aracılığıyla iş birliği yapmaya zorlar.
- **Memento:** Bir nesnenin gerçekleştirim detaylarını açığa çıkarmadan önceki durumunu kaydetmenize ve geri yüklemenize olanak tanır.
- **Observer:** Gözlemledikleri nesnede meydana gelen herhangi bir olay hakkında, ona abone olan diğer tüm nesneleri bilgilendirmek için bir abonelik mekanizması tanımlamanızı sağlar.
- **State:** Bir nesnenin, iç durumu değiştiğinde davranışını değiştirmesine olanak tanır. Nesne sanki sınıfını değiştirmiş gibi görünür.
- **Strategy:** Bir algoritma ailesi tanımlamanıza, her birini ayrı bir sınıfa koymanıza ve nesnelerini birbirinin yerine kullanılabilir hale getirmenize olanak tanır.
- **Template Method:** Bir algoritmanın iskeletini üst sınıfta tanımlar, ancak alt sınıfların algoritmanın yapısını değiştirmeden belirli adımlarını yeniden tanımlamasına (override) izin verir.
- **Visitor:** Algoritmaları, üzerinde çalıştıkları nesnelerden ayırmanıza olanak tanır.