using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hv.Sos100.DataService.SingleSignOn.Api.Data;
using Hv.Sos100.DataService.SingleSignOn.Api.Models;
using Hv.Sos100.Logger;

namespace Hv.Sos100.DataService.SingleSignOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly AuthenticationDbContext _context;
        private readonly ApiService _apiService = new();
        private readonly LogService _logService = new();

        public AuthenticationsController(AuthenticationDbContext context)
        {
            _context = context;
        }

        // GET: api/Authentications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Authentication>>> GetAuthentication() => await _context.Authentication.ToListAsync();

        // GET: api/Authentications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Authentication>> GetAuthentication(Guid id)
        {
            var authentication = await _context.Authentication.FindAsync(id);

            if (authentication == null)
            {
                return NotFound();
            }

            return authentication;
        }

        // PUT: api/Authentications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthentication(Guid id, Authentication authentication)
        {
            if (id != authentication.AuthenticationId)
            {
                return BadRequest();
            }

            _context.Entry(authentication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthenticationExists(id))
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

        // POST: api/Authentications
        [HttpPost]
        public async Task<ActionResult<Authentication>> PostAuthentication(Authentication authentication)
        {
            _context.Authentication.Add(authentication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthentication", new { id = authentication.AuthenticationId }, authentication);
        }

        // DELETE: api/Authentications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthentication(Guid id)
        {
            var authentication = await _context.Authentication.FindAsync(id);
            if (authentication == null)
            {
                return NotFound();
            }

            _context.Authentication.Remove(authentication);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthenticationExists(Guid id)
        {
            return _context.Authentication.Any(e => e.AuthenticationId == id);
        }

        // POST: api/Authentications/validateNewSession
        [HttpPost("validateNewSession")]
        public async Task<ActionResult<Authentication>> ValidateNewSession(Account inputAccount)
        {
            var apiAccounts = await _apiService.GetAccounts();
            if (apiAccounts == null) { return NotFound("The api could not find any accounts"); }

            var foundAccount = apiAccounts.FirstOrDefault(searchAccount => inputAccount.Email == searchAccount.Email && inputAccount.Password == searchAccount.Password);
            if (foundAccount == null) { return NotFound("The api could not match any accounts to the input"); }

            var existingAuthentication = await _context.Authentication.FirstOrDefaultAsync(authentication => authentication.AccountId == foundAccount.Id.ToString());
            if (existingAuthentication != null)
            {
                existingAuthentication.LastActivity = DateTime.Now;
                if (existingAuthentication.TokenExpiration < DateTime.Now)
                {
                    existingAuthentication.Token = Guid.NewGuid().ToString();
                    existingAuthentication.TokenExpiration = DateTime.Now.AddMonths(1);
                }
                await _context.SaveChangesAsync();
                return Ok(existingAuthentication);
            }
            
            var newAuthentication = new Authentication
            {
                AccountId = foundAccount.Id.ToString(),
                LastActivity = DateTime.Now,
                Token = Guid.NewGuid().ToString(),
                TokenExpiration = DateTime.Now.AddMonths(1),
                AccountType = foundAccount.AccountType
            };

            _context.Authentication.Add(newAuthentication);
            await _context.SaveChangesAsync();

            // Clear sensitive data before returning
            newAuthentication.AuthenticationId = Guid.Empty;
            newAuthentication.LastActivity = null;
            newAuthentication.TokenExpiration = null;

            return Ok(newAuthentication);
        }

        // POST: api/Authentications/validateExistingSession
        [HttpPost("validateExistingSession")]
        public async Task<ActionResult<Authentication>> ValidateExistingSession()
        {
            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            var authentication = await _context.Authentication.FirstOrDefaultAsync(authentication => authentication.Token == token);
            if (authentication == null) { return NotFound(); }

            if (authentication.LastActivity < DateTime.Now.AddHours(-12) 
                || authentication.TokenExpiration < DateTime.Now)
            {
                return Unauthorized();
            }

            authentication.LastActivity = DateTime.Now;
            await _context.SaveChangesAsync();

            // Clear sensitive data before returning
            authentication.AuthenticationId = Guid.Empty;
            authentication.LastActivity = null;
            authentication.TokenExpiration = null;

            return Ok(authentication);
        }

        // GET: api/Authentications/accounts
        [HttpGet("accounts")]
        public async Task<ActionResult<Account>> GetAccount()
        {
            var accounts = await _apiService.GetAccounts();
            if (accounts == null) { return NotFound(); }

            return Ok(accounts);
        }
    }
}
