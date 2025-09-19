using KampusTek.Business.Abstract;
using KampusTek.Data;
using KampusTek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KampusTek.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly KampusTekDbContext _context;

        public UserService(KampusTekDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public User GetById(int id)
        {
            return _context.Users
                .Include(u => u.UserType)
                .FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetAll()
        {
            return _context.Users
                .Include(u => u.UserType)
                .AsNoTracking()
                .ToList();
        }
    }
}
