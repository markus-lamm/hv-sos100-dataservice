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

    // PUT: api/Logs/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLog(int id, Models.Log log)
    {
        if (id != log.Id)
        {
            return BadRequest();
        }

        _context.Entry(log).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LogExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Logs
    [HttpPost]
    public async Task<ActionResult<Models.Log>> PostLog(Models.Log log)
    {
        if (log.SourceSystem == "mySystem" || log.Message == "this is a message")
        {
            return BadRequest("Log posts containing mySystem or this is a message will be rejected");
        }

        _context.Logs.Add(log);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetLog", new { id = log.Id }, log);
    }

    // DELETE: api/Logs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLog(int id)
    {
        var log = await _context.Logs.FindAsync(id);
        if (log == null)
        {
            return NotFound();
        }

        _context.Logs.Remove(log);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Logs/BadLogs
    [HttpDelete("BadLogs")]
    public async Task<IActionResult> DeleteBadLogs()
    {
        var logsToDelete = _context.Logs.Where(log => log.SourceSystem == "mySystem" || log.Message == "this is a message");

        _context.Logs.RemoveRange(logsToDelete);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LogExists(int id)
    {
        return _context.Logs.Any(e => e.Id == id);
    }
}
