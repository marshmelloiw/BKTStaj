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
    public class RentalService : IRentalService
    {
        private readonly KampusTekDbContext _context;

        public RentalService(KampusTekDbContext context)
        {
            _context = context;
        }

        public void Add(Rental rental)
        {
            _context.Rentals.Add(rental);
            _context.SaveChanges();
        }

        public void Update(Rental rental)
        {
            _context.Rentals.Update(rental);
            _context.SaveChanges();
        }

        public void EndRental(int rentalId, int endStationId)
        {
            var rental = _context.Rentals.Find(rentalId);
            if (rental != null)
            {
                rental.EndStationId = endStationId;
                rental.ReturnTime = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public Rental GetById(int id)
        {
            return _context.Rentals
                .Include(r => r.User)
                .Include(r => r.Bicycle)
                .Include(r => r.StartStation)
                .Include(r => r.EndStation)
                .AsNoTracking()
                .FirstOrDefault(r => r.Id == id);
        }

        public List<Rental> GetAll()
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
