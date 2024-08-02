using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly AccountContext _context;

        public CompanyService(AccountContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Company>>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<ActionResult<Company>> GetByIdAsync(Guid id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task Update(Company company)
        {
            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task Add(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }

        public bool CompanyExists(Guid id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
