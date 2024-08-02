using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Services
{
    public interface ICompanyService
    {
        Task<ActionResult<IEnumerable<Company>>> GetAllAsync();

        Task<ActionResult<Company>> GetByIdAsync(Guid id);

        Task Update(Company company);

        Task Add(Company company);

        Task Delete(Guid id);

        bool CompanyExists(Guid id);
    }
}
