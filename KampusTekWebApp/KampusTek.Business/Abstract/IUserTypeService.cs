using KampusTek.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampusTek.Business.Abstract
{
    public interface IUserTypeService
    {
        IEnumerable<UserType> GetAll();
        UserType? GetById(int id);
        void Add(UserType userType);
        void Update(UserType userType);
        void Delete(int id);
    }
}
