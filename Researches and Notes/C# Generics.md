### C# Generics 

##### 1. Generics Nedir?

Generics, C#'ta sınıflar, metotlar, arayüzler ve delegeler için **tip
bağımsız (type-agnostic)** programlama yapmamızı sağlar.\
Yani bir sınıfı veya metodu yalnızca tek bir tip için değil, farklı
tiplerle tekrar kullanılabilir.

Örneğin, `ArrayList` içine her türden veri eklenebilir ama tip güvenliği
yoktur. Generics ile **tip güvenliği** sağlanır ve **boxing/unboxing**
maliyeti ortadan kalkar.



##### 2. Generics'in Sağladığı Özellikler

-   **Kod Tekrarının Azalması** → Aynı sınıf veya metodu farklı tipler
    için tekrar tekrar yazmana gerek kalmaz.\
-   **Tip Güvenliği (Type Safety)** → Derleme (compile) zamanında tip
    denetimi yapılır, hatalar erken yakalanır.\
-   **Performans** → Değer tiplerinde (value types) boxing/unboxing
    işlemlerinden kurtarır.\
-   **Esneklik** → Koleksiyonlar ve metotlar farklı veri tipleriyle
    çalışabilir.



##### 3. C#'ta Generics Kullanımı

###### Generic Sınıf (Generic Class)

-   Tek bir sınıf ile farklı tiplerde nesneler oluşturulabilir.

``` csharp
public class DataStore<T>
{
    public T Data { get; set; }
}
```

Kullanımı:

``` csharp
var storeInt = new DataStore<int> { Data = 123 };
var storeString = new DataStore<string> { Data = "Merhaba" };
```

It is basically a type-safe way of creating reusable classes. Unlike
`ArrayList`, it ensures that only the specified type can be stored.



###### Generic Metot (Generic Method)

-   Generic olmayan bir sınıf içinde bile generic metot yazılabilir.

``` csharp
public class Utils
{
    public void Print<T>(T value)
    {
        Console.WriteLine(value);
    }
}
```

Kullanımı:

``` csharp
Utils u = new Utils();
u.Print<int>(5);
u.Print<string>("hello");
```

It allows methods to be written in a way that can work with any data
type without code duplication.



###### Constraints (Kısıtlamalar)

-   `where` anahtar kelimesi ile tip parametrelerine sınırlamalar
    koyulabilir.

``` csharp
public class Repository<T> where T : new()
{
    public T Create()
    {
        return new T(); // sadece parametresiz ctor'u olan tipler gelebilir
    }
}
```

It restricts the type parameters to ensure they meet certain
requirements (e.g., must be a class, must have a constructor, must
inherit from a base class, etc.).


###### Covariance & Contravariance

-   **Covariance (out)** → Daha türemiş tipleri, temel tipe atama.\
-   **Contravariance (in)** → Daha genel tipleri, türemiş tipe atama.

``` csharp
public interface IProducer<out T>
{
    T Produce();
}

public interface IConsumer<in T>
{
    void Consume(T item);
}
```

It provides flexibility in assigning generic types when working with
inheritance hierarchies.


###### Generic Koleksiyonlar

-   `List<T>`, `Dictionary<TKey, TValue>`, `Queue<T>`, `Stack<T>` gibi
    hazır generic koleksiyonlar vardır.

``` csharp
List<int> numbers = new List<int> { 1, 2, 3 };
Dictionary<int, string> users = new Dictionary<int, string>
{
    {1, "Melisa"},
    {2, "Ensar"}
};
```

It is the recommended way to store collections of strongly-typed data in
modern C# instead of non-generic collections like `ArrayList` or
`Hashtable`.
