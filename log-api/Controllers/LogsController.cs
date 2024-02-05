using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using log_api.Data;
using log_api.Models;

namespace log_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LogsController : ControllerBase
{
    private readonly LogDbContext _context;

    public LogsController(LogDbContext context) { _context = context; }

    // GET: api/Logs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Log>>> GetLogs() => await _context.Logs.ToListAsync();

    // GET: api/Logs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Log>> GetLog(int id)
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
    public async Task<ActionResult<Log>> PostLog(Log log)
    {
        _context.Logs.Add(log);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetLog", new { id = log.Id }, log);
    }
}
