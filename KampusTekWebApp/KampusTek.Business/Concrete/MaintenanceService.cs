using KampusTek.Business.Abstract;
using KampusTek.Data;
using KampusTek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KampusTek.Business.Concrete
{
    public class MaintenanceService : GenericService<Maintenance>, IMaintenanceService
    {
        private readonly KampusTekDbContext _context;

        public MaintenanceService(KampusTekDbContext context) : base(new Data.Concrete.GenericRepository<Maintenance>(context))
        {
            _context = context;
        }

        public override Maintenance GetById(int id)
        {
            return _context.Maintenances
                .Include(m => m.Bicycle)
                .AsNoTracking()
                .FirstOrDefault(m => m.Id == id);
        }

        public override List<Maintenance> GetAll()
        {
            return _context.Maintenances
                .Include(m => m.Bicycle)
                .AsNoTracking()
                .ToList();
        }
    }
}