using KampusTek.Business.Abstract;
using KampusTek.Data;
using KampusTek.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampusTek.Business.Concrete
{
    public class BicycleService : IBicycleService
    {
        private readonly KampusTekDbContext _context;

        public BicycleService(KampusTekDbContext context)
        {
            _context = context;
        }

        public void Add(Bicycle bicycle)
        {
            _context.Bicycles.Add(bicycle);
            _context.SaveChanges();
        }

        public void Update(Bicycle bicycle)
        {
            _context.Bicycles.Update(bicycle);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var bicycle = _context.Bicycles
                .Include(b => b.Rentals)
                .FirstOrDefault(b => b.Id == id);
                
            if (bicycle != null)
            {
                // Aktif kiralama var mı kontrol et (ReturnTime null olan kiralama)
                var activeRental = bicycle.Rentals.Any(r => r.ReturnTime == null);
                if (activeRental)
                {
                    throw new InvalidOperationException("Bu bisiklet aktif bir kiralama işleminde kullanılıyor. Silme işlemi yapılamaz.");
                }
                
                _context.Bicycles.Remove(bicycle);
                _context.SaveChanges();
            }
        }

        public Bicycle GetById(int id)
        {
            return _context.Bicycles
                .FirstOrDefault(b => b.Id == id);
        }

        public List<Bicycle> GetAll()
        {
            return _context.Bicycles
                .ToList();
        }

        public string GetNextBicycleCode()
        {
            var lastBicycle = _context.Bicycles
                .Where(b => b.BicycleCode.StartsWith("B"))
                .OrderByDescending(b => b.BicycleCode)
                .FirstOrDefault();

            if (lastBicycle == null)
            {
                return "B001";
            }

            // B002 -> 002 -> 2 -> 3 -> B003
            var lastCode = lastBicycle.BicycleCode;
            var numberPart = lastCode.Substring(1); // "002"
            if (int.TryParse(numberPart, out int number))
            {
                var nextNumber = number + 1;
                return $"B{nextNumber:D3}"; // 3 haneli format: B003
            }

            return "B001"; // Hata durumunda varsayılan
        }
    }
}
