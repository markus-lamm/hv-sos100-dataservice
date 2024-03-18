using Hv.Sos100.SingleSignOn;
using Microsoft.AspNetCore.Mvc;

namespace Hv.Sos100.DataService.Statistics.AdminGui.Data;

public class Authenticate
{
    private readonly AuthenticationService _authenticationService;

    public Authenticate(AuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<bool> IsAuthenticatedAdmin(Controller controller, HttpContext httpContext)
    {
        var isAuthenticated = httpContext.Session.GetString("IsAuthenticated");
        if (isAuthenticated == null)
        {
            var existingSession = await _authenticationService.ResumeSession(controllerBase: controller, httpContext);
            if (existingSession == false)
            {
                return false;
            }
            var userRole = httpContext.Session.GetString("UserRole");
            if (userRole != "Admin")
            {
                return false;
            }
        }
        return true;
    }
}
