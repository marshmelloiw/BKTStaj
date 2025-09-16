using KampusTek.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampusTek.Business.Abstract
{
    public interface IMaintenanceService
    {
        void Add(Maintenance maintenance);
        void Update(Maintenance maintenance);
        void Delete(int id);
        Maintenance GetById(int id);
        List<Maintenance> GetAll();
    }
}
