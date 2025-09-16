using KampusTek.Business.Abstract;
using KampusTek.Data;
using KampusTek.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampusTek.Business.Concrete
{
    public class UserTypeService : IUserTypeService
    {
        private readonly KampusTekDbContext _context;

        public UserTypeService(KampusTekDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserType> GetAll()
        {
            return _context.UserTypes.ToList();
        }

        public UserType? GetById(int id)
        {
            return _context.UserTypes.Find(id);
        }

        public void Add(UserType userType)
        {
            _context.UserTypes.Add(userType);
            _context.SaveChanges();
        }

        public void Update(UserType userType)
        {
            _context.UserTypes.Update(userType);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var userType = _context.UserTypes.Find(id);
            if (userType != null)
            {
                _context.UserTypes.Remove(userType);
                _context.SaveChanges();
            }
        }
    }
}
