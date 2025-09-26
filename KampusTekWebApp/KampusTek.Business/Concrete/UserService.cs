using KampusTek.Business.Abstract;
using KampusTek.Data;
using KampusTek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KampusTek.Business.Concrete
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly KampusTekDbContext _context;

        public UserService(KampusTekDbContext context) : base(new Data.Concrete.GenericRepository<User>(context))
        {
            _context = context;
        }

        public override void Delete(int id)
        {
            var user = _context.Users
                .Include(u => u.Rentals)
                .FirstOrDefault(u => u.Id == id);
                
            if (user != null)
            {
                // Aktif kiralama var mı kontrol et (ReturnTime null olan kiralama)
                var activeRental = user.Rentals.Any(r => r.ReturnTime == null);
                if (activeRental)
                {
                    throw new InvalidOperationException("Bu kullanıcının aktif bir kiralama işlemi bulunuyor. Silme işlemi yapılamaz.");
                }
                
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public override User GetById(int id)
        {
            return _context.Users
                .Include(u => u.UserType)
                .FirstOrDefault(u => u.Id == id);
        }

        public override List<User> GetAll()
        {
            return _context.Users
                .Include(u => u.UserType)
                .AsNoTracking()
                .ToList();
        }
    }
}
