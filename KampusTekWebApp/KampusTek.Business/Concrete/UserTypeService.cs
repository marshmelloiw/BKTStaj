using KampusTek.Business.Abstract;
using KampusTek.Data;
using KampusTek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KampusTek.Business.Concrete
{
    public class UserTypeService : GenericService<UserType>, IUserTypeService
    {
        private readonly KampusTekDbContext _context;

        public UserTypeService(KampusTekDbContext context) : base(new Data.Concrete.GenericRepository<UserType>(context))
        {
            _context = context;
        }

        public override void Delete(int id)
        {
            var userType = _context.UserTypes
                .Include(ut => ut.Users)
                .FirstOrDefault(ut => ut.Id == id);
                
            if (userType != null)
            {
                // Bu UserType'ı kullanan kullanıcı var mı kontrol et
                if (userType.Users.Any())
                {
                    throw new InvalidOperationException("Bu kullanıcı tipini kullanan kullanıcılar bulunuyor. Silme işlemi yapılamaz.");
                }
                
                _context.UserTypes.Remove(userType);
                _context.SaveChanges();
            }
        }

        public override List<UserType> GetAll()
        {
            var userTypes = _context.UserTypes.ToList();
            Console.WriteLine($"UserTypeService.GetAll() - {userTypes.Count} user type bulundu");
            foreach (var ut in userTypes)
            {
                Console.WriteLine($"  - ID: {ut.Id}, Name: {ut.Name}");
            }
            return userTypes;
        }
    }
}
