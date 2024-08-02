using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;
using WebApplication5.Services;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetReports()
        {
            return await _reportService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReport(Guid id)
        {
            var report = await _reportService.GetByIdAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            return report;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutReport(Guid id, Report report)
        {
            if (id != report.Id)
            {
                return BadRequest();
            }

            if (!_reportService.ReportExists(id))
            {
                return NotFound();
            }

            await _reportService.Update(report);

            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Report>> PostReport(Report report)
        {
            await _reportService.Add(report);

            return CreatedAtAction("GetReport", new { id = report.Id }, report);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReport(Guid id)
        {
            await _reportService.Delete(id);

            return NoContent();
        }

        [HttpGet]
        [Route("Report")]
        public async Task<ActionResult<ReportModel>> GetReport(string name)
        {
            return await _reportService.Report(name);
        }

        [HttpGet]
        [Route("Reports")]
        public async Task<ActionResult<IEnumerable<ReportModel>>> GetAllReports()
        {
            return await _reportService.ReportAll();
        }
    }
}
