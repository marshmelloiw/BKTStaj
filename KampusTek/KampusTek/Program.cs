using System;
using System.Linq;
using KampusTek.Models;

namespace KampusTek
{
    /*
     *  Stations tablosu üzerinde CRUD (Create, Read, Update, Delete) işlemlerini gerçekleştiren bir konsol uygulaması.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. CREATE: Yeni İstasyon Ekleme");
            using (var dbContext = new KampusTekDbv2Context())
            {
                var yeniIstasyon = new Station
                {
                    NameOfStation = "Yemekhane Önü",
                    Location = "Merkez Kampüs",
                    Capacity = 25
                };

                dbContext.Stations.Add(yeniIstasyon);

                dbContext.SaveChanges();

                Console.WriteLine($"'{yeniIstasyon.NameOfStation}' adlı istasyon başarıyla eklendi.\n");
            }

            Console.WriteLine("2. READ: Mevcut İstasyonları Okuma");
            using (var dbContext = new KampusTekDbv2Context())
            {
                var tumIstasyonlar = dbContext.Stations.ToList();

                Console.WriteLine("Veritabanındaki İstasyonlar:");
                foreach (var istasyon in tumIstasyonlar) {
                    Console.WriteLine($"- ID: {istasyon.StationId}, Ad: {istasyon.NameOfStation}, Kapasite: {istasyon.Capacity}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("3. UPDATE: İstasyon Bilgisini Güncelleme");
            using (var dbContext = new KampusTekDbv2Context())
            {
                var guncellenecekIstasyon = dbContext.Stations.FirstOrDefault(s => s.NameOfStation == "Yemekhane Önü");

                if (guncellenecekIstasyon != null)  {
                    Console.WriteLine($"'{guncellenecekIstasyon.NameOfStation}' istasyonunun eski kapasitesi: {guncellenecekIstasyon.Capacity}");

                    guncellenecekIstasyon.Capacity = 30;

                    dbContext.SaveChanges();

                    Console.WriteLine($"İstasyonun yeni kapasitesi: {guncellenecekIstasyon.Capacity} olarak güncellendi.\n");
                }
                else {
                    Console.WriteLine("Güncellenecek istasyon bulunamadı.\n");
                }
            }

            Console.WriteLine("4. DELETE: İstasyon Silme");
            using (var dbContext = new KampusTekDbv2Context())
            {
                var silinecekIstasyon = dbContext.Stations.FirstOrDefault(s => s.NameOfStation == "Yemekhane Önü");

                if (silinecekIstasyon != null)  {
                    dbContext.Stations.Remove(silinecekIstasyon);

                    dbContext.SaveChanges();

                    Console.WriteLine($"'{silinecekIstasyon.NameOfStation}' adlı istasyon başarıyla silindi.\n");
                }
                else {
                    Console.WriteLine("Silinecek istasyon bulunamadı.\n");
                }
            }
            Console.WriteLine("Tüm CRUD işlemleri tamamlandı.");
        }
    }
}