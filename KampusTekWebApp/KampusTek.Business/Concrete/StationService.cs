using KampusTek.Business.Abstract;
using KampusTek.Data;
using KampusTek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KampusTek.Business.Concrete
{
    public class StationService : GenericService<Station>, IStationService
    {
        private readonly KampusTekDbContext _context;

        public StationService(KampusTekDbContext context) : base(new Data.Concrete.GenericRepository<Station>(context))
        {
            _context = context;
        }

        public override void Delete(int id)
        {
            var station = _context.Stations
                .Include(s => s.Bicycles)
                .Include(s => s.RentalsAsStart)
                .Include(s => s.RentalsAsEnd)
                .FirstOrDefault(s => s.Id == id);
                
            if (station != null)
            {
                // İstasyonda bisiklet var mı kontrol et
                if (station.Bicycles.Any())
                {
                    throw new InvalidOperationException("Bu istasyonda bisikletler bulunuyor. Önce bisikletleri başka istasyona taşıyın.");
                }
                
                // Bu istasyondan başlayan veya biten aktif kiralama var mı kontrol et
                var activeRentals = station.RentalsAsStart.Any(r => r.ReturnTime == null) || 
                                  station.RentalsAsEnd.Any(r => r.ReturnTime == null);
                if (activeRentals)
                {
                    throw new InvalidOperationException("Bu istasyonla ilgili aktif kiralama işlemleri bulunuyor. Silme işlemi yapılamaz.");
                }
                
                _context.Stations.Remove(station);
                _context.SaveChanges();
            }
        }

        public override List<Station> GetAll()
        {
            return _context.Stations
                .AsNoTracking()
                .ToList();
        }
    }
}
