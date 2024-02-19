namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
    private readonly Auth0Provider auth;
    private readonly VaultsService vaultsService;
    private readonly VaultKeepsService vaultKeepsService;
    public VaultsController(Auth0Provider auth, VaultsService vaultsService, VaultKeepsService vaultKeepsService)
    {
        this.auth = auth;
        this.vaultsService = vaultsService;
        this.vaultKeepsService = vaultKeepsService;
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
    public async Task<ActionResult<Vault>> getVaultById(int vaultId)
    {
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = vaultsService.GetVaultById(vaultId, userInfo?.Id);
            return Ok(vault);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [HttpPut("{vaultId}")]
    public async Task<ActionResult<Vault>> updateVault([FromBody] Vault updateData, int vaultId)
    {
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            updateData.Id = vaultId;
            Vault update = vaultsService.UpdateVault(updateData, userInfo.Id);
            return Ok(update);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [HttpDelete("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteVault(int vaultId)
    {
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            string message = vaultsService.DeleteVault(vaultId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<VaultKept>>> GetKeepsInVault(int vaultId)
    {
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            // TODO run getVaultById before passing it to the vaultKeepsService
            Vault vault = vaultsService.GetVaultById(vaultId, userInfo?.Id);
            List<VaultKept> vaultKept = vaultKeepsService.GetKeepsInVault(vault.Id);
            return Ok(vaultKept);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

}