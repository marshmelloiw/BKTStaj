using KampusTek.Entities;

namespace KampusTek.Business.Abstract
{
    public interface IRentalService : IGenericService<Rental>
    {
        void EndRental(int rentalId, int endStationId);
    }
}
