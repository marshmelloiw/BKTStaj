### C# Collection Classes (Koleksiyon Sınıfları)

##### 1\. Koleksiyon Sınıfları Nedir?



Koleksiyon sınıfları, verileri saklamak (storage) ve veriye ulaşmak (retrieval) için özel olarak hazırlanmış sınıflardır. Yani normal dizilerden (array) daha gelişmiş, esnek yapılardır. Çoğu koleksiyon sınıfı, benzer arayüzleri (interfaces) uygular. Bu sayede kullanımları birbirine benzer olur.



##### 2\. Koleksiyonların Sağladığı Özellikler



* Dinamik Bellek Kullanımı → Normal diziler sabit boyutludur, koleksiyonlar ise gerektiğinde belleği büyütüp küçültebilir.
* Index ile erişim → Elemanlara, tıpkı dizilerdeki gibi indeks üzerinden erişebilirsin.
* Object tabanlı → Koleksiyon sınıfları Object sınıfını temel alır. Yani içine her türlü veri tipi eklenebilir (ama bu yüzden “boxing/unboxing” maliyeti olabilir).



##### 3\. C#’ta Önemli Koleksiyon Sınıfları



###### ArrayList

* Dinamik boyutlu bir listedir.
* Eleman sayısı arttıkça kapasitesi otomatik artar.
* Farklı tipte veriler aynı anda saklanabilir.



```csharp

ArrayList list = new ArrayList();

list.Add(10);

list.Add("Merhaba");

list.Add(3.14);

```



It is basically an alternative to an array. However, unlike array you can add and remove items from a list at a specified position using an index and the array resizes itself automatically. It also allows dynamic memory allocation, adding, searching and sorting items in the list.



###### Hashtable

* Anahtar (key) – Değer (value) ikilileri şeklinde veri tutar. 
* Aynı anahtar tekrar kullanılamaz.



```csharp

Hashtable table = new Hashtable();

table\["id"] = 101;

table\["name"] = "Melisa";

```



A hash table is used when you need to access elements by using key, and you can identify a useful key value. Each item in the hash table has a key/value pair. The key is used to access the items in the collection.



###### SortedList

* Hem dizi hem de hash mantığını birleştirir.
* Veriler anahtarlarına göre sıralı şekilde tutulur.



```csharp
SortedList sorted = new SortedList();

sorted\["b"] = "banana";

sorted\["a"] = "apple";

sorted\["c"] = "cherry";

// Sıralı şekilde: a, b, c

```



A sorted list is a combination of an array and a hash table. It contains a list of items that can be accessed using a key or an index. If you access items using an index, it is an ArrayList, and if you access items using a key , it is a Hashtable. The collection of items is always sorted by the key value.



###### Stack (Yığın)

* LIFO (Last In, First Out) mantığıyla çalışır → Son giren ilk çıkar.



```csharp

Stack stack = new Stack();

stack.Push(1);

stack.Push(2);

Console.WriteLine(stack.Pop()); // 2 çıkar

```



It represents a last-in, first out collection of object.

It is used when you need a last-in, first-out access of items. When you add an item in the list, it is called pushing the item and when you remove it, it is called popping the item.





###### Queue (Kuyruk)

* FIFO (First In, First Out) mantığıyla çalışır → İlk giren ilk çıkar.



```csharp

Queue queue = new Queue();

queue.Enqueue("A");

queue.Enqueue("B");

Console.WriteLine(queue.Dequeue());

```



It is used when you need a first-in, first-out access of items. When you add an item in the list, it is called enqueue and when you remove an item, it is called deque.



###### BitArray

* It represents an array of the binary representation using the values 1 and 0.
* It is used when you need to store the bits but do not know the number of bits in advance. You can access items from the BitArray collection by using an integer index, which starts from zero.
