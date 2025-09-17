using KampusTek.Entities;

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