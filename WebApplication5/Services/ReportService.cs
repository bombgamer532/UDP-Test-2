using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Services
{
    public class ReportService : IReportService
    {
        private readonly AccountContext _context;

        public ReportService(AccountContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Report>>> GetAllAsync()
        {
            return await _context.Reports.ToListAsync();
        }

        public async Task<ActionResult<Report>> GetByIdAsync(Guid id)
        {
            return await _context.Reports.FindAsync(id);
        }

        public async Task<ActionResult<ReportModel>> Report(string name)
        {
            var time = _context.Reports.Where(x => x.WorkerName == name).Sum(x => x.HoursSpent);
            return new ReportModel { Name = name, HoursWorked = time };
        }

        public async Task<ActionResult<IEnumerable<ReportModel>>> ReportAll()
        {
            //var reports = await _context.Reports.DistinctBy(x => x.WorkerName).ToListAsync();
            var reports = await _context.Reports.ToListAsync();
            var list = new List<ReportModel>();
            foreach (var report in reports)
            {
                var time = _context.Reports.Where(x => x.WorkerName == report.WorkerName).Sum(x => x.HoursSpent);
                list.Add(new ReportModel { Name = report.WorkerName, HoursWorked = time });
            }
            return list;
        }

        public async Task Update(Report report)
        {
            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task Add(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
            }
        }

        public bool ReportExists(Guid id)
        {
            return _context.Reports.Any(e => e.Id == id);
        }
    }
}
