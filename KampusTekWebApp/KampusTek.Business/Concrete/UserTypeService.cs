using KampusTek.Business.Abstract;
using KampusTek.Data;
using KampusTek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KampusTek.Business.Concrete
{
    public class UserTypeService : IUserTypeService
    {
        private readonly KampusTekDbContext _context;

        public UserTypeService(KampusTekDbContext context)
        {
            _context = context;
        }

        public List<UserType> GetAll()
        {
            var userTypes = _context.UserTypes.ToList();
            Console.WriteLine($"UserTypeService.GetAll() - {userTypes.Count} user type bulundu");
            foreach (var ut in userTypes)
            {
                Console.WriteLine($"  - ID: {ut.Id}, Name: {ut.Name}");
            }
            return userTypes;
        }

        public UserType GetById(int id)
        {
            return _context.UserTypes.Find(id);
        }
    }
}
