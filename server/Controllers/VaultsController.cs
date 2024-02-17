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

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> createVault([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            Vault vault = vaultsService.CreateVault(vaultData);
            return Ok(vault);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [HttpGet("{vaultId}")]
    public ActionResult<Vault> getVaultById(int vaultId)
    {
        try
        {
            Vault vault = vaultsService.GetVaultById(vaultId);
            return Ok(vault);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }


}