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
    public class StationService : IStationService
    {
        private readonly KampusTekDbContext _context;

        public StationService(KampusTekDbContext context)
        {
            _context = context;
        }

        public void Add(Station station)
        {
            _context.Stations.Add(station);
            _context.SaveChanges();
        }

        public void Update(Station station)
        {
            _context.Stations.Update(station);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var station = _context.Stations.Find(id);
            if (station != null)
            {
                _context.Stations.Remove(station);
                _context.SaveChanges();
            }
        }

        public void Delete(Station station)
        {
            _context.Stations.Remove(station);
            _context.SaveChanges();
        }

        public Station GetById(int id)
        {
            return _context.Stations
                .FirstOrDefault(s => s.Id == id);
        }

        public List<Station> GetAll()
        {
            return _context.Stations
                .AsNoTracking()
                .ToList();
        }
    }
}
