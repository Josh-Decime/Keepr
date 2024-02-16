namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
    private readonly Auth0Provider auth;
    private readonly KeepsService keepsService;
    public KeepsController(KeepsService keepsService, Auth0Provider auth)
    {
        this.auth = auth;
        this.keepsService = keepsService;
    }
    // ---------------------------------------------------------------------
}