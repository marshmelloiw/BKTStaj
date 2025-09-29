using KampusTek.Business.Abstract;
using KampusTek.Data;
using KampusTek.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KampusTek.Business.Concrete
{
    public class RentalService : GenericService<Rental>, IRentalService
    {
        private readonly KampusTekDbContext _context;

        public RentalService(KampusTekDbContext context) : base(new Data.Concrete.GenericRepository<Rental>(context))
        {
            _context = context;
        }

        public override void Add(Rental rental)
        {
            // Prevent renting a bicycle that is already in an active rental
            var hasActiveForBike = _context.Rentals.Any(r => r.BicycleId == rental.BicycleId && r.ReturnTime == null);
            if (hasActiveForBike)
            {
                throw new InvalidOperationException("Seçilen bisiklet şu anda kullanımda.");
            }

            // Ensure start time is set
            if (rental.StartTime == default)
            {
                rental.StartTime = DateTime.Now;
            }

            // Persist rental
            _context.Rentals.Add(rental);
            _context.SaveChanges();

            // Update bicycle state (now rented, no station)
            var bicycle = _context.Bicycles.FirstOrDefault(b => b.Id == rental.BicycleId);
            if (bicycle != null)
            {
                bicycle.Status = "Rented";
                bicycle.CurrentStationId = null;
                _context.SaveChanges();
            }
        }

        public override void Delete(int id)
        {
            var rental = _context.Rentals
                .Include(r => r.User)
                .Include(r => r.Bicycle)
                .FirstOrDefault(r => r.Id == id);
                
            if (rental != null)
            {
                // Aktif kiralama var mı kontrol et (ReturnTime null olan kiralama)
                if (rental.ReturnTime == null)
                {
                    throw new InvalidOperationException("Bu kiralama işlemi henüz tamamlanmamış. Önce kiralama işlemini tamamlayın.");
                }
                
                _context.Rentals.Remove(rental);
                _context.SaveChanges();
            }
        }

        public void EndRental(int rentalId, int endStationId)
        {
            var rental = _context.Rentals.FirstOrDefault(r => r.Id == rentalId);
            if (rental == null)
                return;

            if (rental.ReturnTime != null)
                return; // already ended

            rental.EndStationId = endStationId;
            rental.ReturnTime = DateTime.Now;

            // Update bicycle state (available at end station)
            var bicycle = _context.Bicycles.FirstOrDefault(b => b.Id == rental.BicycleId);
            if (bicycle != null)
            {
                bicycle.Status = "Available";
                bicycle.CurrentStationId = endStationId;
            }

            _context.SaveChanges();
        }

        public override Rental GetById(int id)
        {
            return _context.Rentals
                .Include(r => r.User)
                .Include(r => r.Bicycle)
                .Include(r => r.StartStation)
                .Include(r => r.EndStation)
                .AsNoTracking()
                .FirstOrDefault(r => r.Id == id);
        }

        public override List<Rental> GetAll()
        {
            return _context.Rentals
                .Include(r => r.User)
                .Include(r => r.Bicycle)
                .Include(r => r.StartStation)
                .Include(r => r.EndStation)
                .AsNoTracking()
                .ToList();
        }
    }
}
