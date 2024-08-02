using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Services
{
    public interface IUserService
    {
        Task<ActionResult<IEnumerable<User>>> GetAllAsync();

        Task<ActionResult<User>> GetByIdAsync(Guid id);

        User GetByLogin(string login, string password);

        Task Update(User user);

        Task Add(User user);

        Task Delete(Guid id);

        bool UserExists(Guid id);
    }
}
