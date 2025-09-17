using KampusTek.Entities;
using System.Collections.Generic;

namespace KampusTek.Business.Abstract
{
    public interface IUserTypeService
    {
        List<UserType> GetAll();
        UserType GetById(int id);
    }
}
