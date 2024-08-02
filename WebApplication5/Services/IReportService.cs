using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Services
{
    public interface IReportService
    {
        Task<ActionResult<IEnumerable<Report>>> GetAllAsync();

        Task<ActionResult<Report>> GetByIdAsync(Guid id);

        Task<ActionResult<ReportModel>> Report(string name);

        Task<ActionResult<IEnumerable<ReportModel>>> ReportAll();

        Task Update(Report report);

        Task Add(Report report);

        Task Delete(Guid id);

        bool ReportExists(Guid id);
    }
}
