Abstraction = Soyutlama
Nesneye yönelimli programlamadaki en önemli kavramlardan birisi de soyutlamadır. Büyük ve kapsamlı program birçok sistem parçası ile mesajlaşarak çalışmaktadır.Sistemin detaylarına odaklanmak yerine input ve outputlarına bakmak sistemi daha net anlamamıza olanak sağlar. Javada soyutlama abstract class (soyut sınıflar) ve interface (arayüzler) aracılığıyla yapılır.

### Abstract Class
Abstract class, ortak özellikleri olan nesneleri bir çatı altında toplamak için kullanılır:
- Sınıfın önüne “abstract” sözcüğü yazılarak soyutlaştırma işlemi yapılır. Abstract sınıftan kalıtım almak için de “extends” kullanılır.
- Abstract class önüne virtual yazılmaz çünkü default sanal olarak gelirler.
- Genellikle Base Class tanımlamak için kullanılan ve soyutlama yeteneği kazandıran sınıflardır.
- Abstract class, is-a ilişkilerinde kullanılır

The abstract keyword is used for classes and methods:
* Abstract class: is a restricted class that cannot be used to create objects (to access it, it must be inherited from another class).
* Abstract method: can only be used in an abstract class, and it does not have a body. The body is provided by the derived class (inherited from).

### Interface
Nesne yönelimli programlamadaki karşılığı metot ve property listesidir.
- Interface içerisinde yalnıza method tanımları bulunur. İçerisine kod parçacığı yazılmaz.
- Interface başka bir interfaceden inherit olabilir.
- Interfaceler genelde “can-do” ilişkisine göre çalışır.
- “new” ile oluşturulamaz.
- Bir sınıf birden fazla interface implement edebilir.
- Interface içerisine yalnızca boş metot tanımlanabilir.
- Interface içerisinde özellik ve metodlarda erişim belirleyiciler kullanılmaz. Her şey public kabul edilir.

An interface is a completely "abstract class", which can only contain abstract methods and properties (with empty bodies)

Notes on Interfaces:
Like abstract classes, interfaces cannot be used to create objects (in the example above, it is not possible to create an "IAnimal" object in the Program class)
Interface methods do not have a body - the body is provided by the "implement" class
On implementation of an interface, you must override all of its methods
Interfaces can contain properties and methods, but not fields/variables
Interface members are by default abstract and public
An interface cannot contain a constructor (as it cannot be used to create objects)

Abstract Class vs. Interface
1-) Bir sınıf birden fazla interface’i inherit olarak alabilir ama bir sınıfa bir tane abstract class inherit alınabilir.
2-) Interface içerisinde boş metodlar tanımlanabilir ama abstract class’larda hem boş metodlar tanımlanabilir hemde içi dolu metodlar tanımlabilir.
3-) Abstract classları kullanmak hız açısından avantaj sağlar.
4-) Interface de yeni bir metod yazdığımız zaman bu interfaceden implement ettiğimiz tüm classlarda bu metodun içini tek tek doldurmak gerekiyor ancak abstract classlarda durum farklıdır burada bir metod tanımlayıp içini doldurduğumuzda abstract sınıfımızdan türetilmiş bütün sınıflar bu özelliği kazanmış olur.
5-) Interfaceler çoklu kalıtımı sağlamaya yardımcı abstract classlar ise çoklu kalıtımı desteklemez.
6-) Interface içerisindeki tüm nesnelerin “public” olması gerekirken Abstract classlarda tüm öğelerin “public” olması zorunlu değildir.
7-) Interface yapıcı metodlar(constructor) içermez. Abstract class yapıcı metodlar içerebilir.
8-) Interface metodlar static olamaz. Abstract class soyut olmayan metodlar static olarak tanımlanabilir.


Kaynak
https://mbilgil0.medium.com/abstract-class-vs-interface-bf98133bfadf