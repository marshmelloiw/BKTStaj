using KampusTek.Business.Abstract;
using KampusTek.Data;
using KampusTek.Entities;
using Microsoft.EntityFrameworkCore;

namespace KampusTek.Business.Concrete
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly KampusTekDbContext _context;

        public MaintenanceService(KampusTekDbContext context)
        {
            _context = context;
        }

        public void Add(Maintenance maintenance)
        {
            _context.Maintenances.Add(maintenance);
            _context.SaveChanges();
        }

        public void Update(Maintenance maintenance)
        {
            _context.Maintenances.Update(maintenance);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var maintenance = _context.Maintenances.Find(id);
            if (maintenance != null)
            {
                _context.Maintenances.Remove(maintenance);
                _context.SaveChanges();
            }
        }

        public Maintenance GetById(int id)
        {
            return _context.Maintenances
                .Include(m => m.Bicycle)
                .FirstOrDefault(m => m.Id == id);
        }

        public List<Maintenance> GetAll()
        {
            return _context.Maintenances
                .Include(m => m.Bicycle)
                .ToList();
        }
    }
}