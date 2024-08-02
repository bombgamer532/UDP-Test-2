using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Services
{
    public class UserService : IUserService
    {
        private readonly AccountContext _context;

        public UserService(AccountContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<User>>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<ActionResult<User>> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public User GetByLogin(string login, string password)
        {
            return _context.Users.Where(x => x.Login == login && x.Password == password).FirstOrDefault();
        }

        public async Task Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
