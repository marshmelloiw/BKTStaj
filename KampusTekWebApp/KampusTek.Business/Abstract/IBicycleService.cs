using KampusTek.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampusTek.Business.Abstract
{
    public interface IBicycleService
    {
        void Add(Bicycle bicycle);
        void Update(Bicycle bicycle);
        void Delete(int id);
        Bicycle GetById(int id);
        List<Bicycle> GetAll();
    }
}
