using KampusTek.Entities;

namespace KampusTek.Business.Abstract
{
    public interface IBicycleService : IGenericService<Bicycle>
    {
        string GetNextBicycleCode();
    }
}
