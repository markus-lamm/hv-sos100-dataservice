using Hv.Sos100.DataService.SingleSignOn.Api.Data;
using Hv.Sos100.DataService.SingleSignOn.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hv.Sos100.DataService.SingleSignOn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly AuthenticationDbContext _context;

    public UsersController(AuthenticationDbContext context)
    {
        _context = context;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    // PUT: api/Users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
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

    // POST: api/Users
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }

    // POST: api/Users/validateNewAuthentication
    [HttpPost("validateNewAuthentication")]
    public async Task<ActionResult<string>> ValidateNewAuthentication(User clientUser)
    {
        var apiUser = await _context.Users.FirstOrDefaultAsync(item => item.Username == clientUser.Username && item.Password == clientUser.Password);
        if (apiUser == null) { return NotFound(); }

        apiUser.LastActivity = DateTime.Now;
        apiUser.Token = Guid.NewGuid().ToString();
        await _context.SaveChangesAsync();

        return Ok(apiUser);
    }

    // POST: api/Users/validateExistingAuthentication
    [HttpPost("validateExistingAuthentication")]
    public async Task<ActionResult<User>> ValidateExistingAuthentication()
    {
        var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
        var user = await _context.Users.FirstOrDefaultAsync(item => item.Token == token);
        if (user == null) { return NotFound(); }

        if (user.LastActivity < DateTime.Now.AddHours(-12)) { return Unauthorized(); }

        user.LastActivity = DateTime.Now;
        await _context.SaveChangesAsync();

        return Ok(user);
    }
}
