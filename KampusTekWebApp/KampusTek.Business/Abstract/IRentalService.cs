using KampusTek.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampusTek.Business.Abstract
{
    public interface IRentalService
    {
        void Add(Rental rental);
        void Update(Rental rental);
        void EndRental(int rentalId, int endStationId);
        Rental GetById(int id);
        List<Rental> GetAll();
    }
}
