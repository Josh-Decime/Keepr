namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
    private readonly Auth0Provider auth;
    private readonly VaultKeepsService vaultKeepsService;
    public VaultKeepsController(Auth0Provider auth, VaultKeepsService vaultKeepsService)
    {
        this.auth = auth;
        this.vaultKeepsService = vaultKeepsService;
    }
    // -------------------------------------------------------------------------------

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKept>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
    {
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            vaultKeepData.CreatorId = userInfo.Id;
            VaultKept vaultKept = vaultKeepsService.CreateVaultKeep(vaultKeepData);
            return Ok(vaultKept);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }




}