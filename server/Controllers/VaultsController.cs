namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
    private readonly Auth0Provider auth;
    private readonly VaultsService vaultsService;
    public VaultsController(Auth0Provider auth, VaultsService vaultsService)
    {
        this.auth = auth;
        this.vaultsService = vaultsService;
    }
    // --------------------------------------------------------------------------
}