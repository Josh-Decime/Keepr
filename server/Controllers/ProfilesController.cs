namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly Auth0Provider auth;
    private readonly ProfilesService profilesService;
    public ProfilesController(Auth0Provider auth, ProfilesService profilesService)
    {
        this.auth = auth;
        this.profilesService = profilesService;
    }
    // ----------------------------------------------------------------

    [HttpGet("{profileId}")]
    public ActionResult<Account> GetUsersProfile(string profileId)
    {
        try
        {
            Account account = profilesService.GetUsersProfile(profileId);
            return Ok(account);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetUsersKeeps(string profileId)
    {
        try
        {
            List<Keep> keeps = profilesService.GetUsersKeeps(profileId);
            return Ok(keeps);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}