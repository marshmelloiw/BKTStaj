using KampusTek.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampusTek.Business.Abstract
{
    public interface IStationService
    {
        void Add(Station station);
        void Update(Station station);
        void Delete(int id);
        Station GetById(int id);
        List<Station> GetAll();
    }
}
