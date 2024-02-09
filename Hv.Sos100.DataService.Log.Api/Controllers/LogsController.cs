using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hv.Sos100.DataService.Log.Api.Data;

namespace Hv.Sos100.DataService.Log.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LogsController : ControllerBase
{
    private readonly LogDbContext _context;

    public LogsController(LogDbContext context) { _context = context; }

    // GET: api/Logs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Models.Log>>> GetLogs() => await _context.Logs.ToListAsync();

    // GET: api/Logs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Models.Log>> GetLog(int id)
    {
        var log = await _context.Logs.FindAsync(id);

        if (log == null)
        {
            return NotFound();
        }

        return log;
    }

    // POST: api/Logs
    [HttpPost]
    public async Task<ActionResult<Models.Log>> PostLog(Models.Log log)
    {
        _context.Logs.Add(log);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetLog", new { id = log.Id }, log);
    }
}
