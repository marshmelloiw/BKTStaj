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
            var bicycle = _context.Bicycles.Find(id);
            if (bicycle != null)
            {
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
    }
}
