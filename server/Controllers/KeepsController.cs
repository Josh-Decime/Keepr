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

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = userInfo.Id;
            Keep keep = keepsService.CreateKeep(keepData);
            return Ok(keep);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetAllKeeps()
    {
        try
        {
            List<Keep> keeps = keepsService.GetAllKeeps();
            return Ok(keeps);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [HttpGet("{keepId}")]
    public ActionResult<Keep> GetKeepById(int keepId)
    {
        try
        {
            Keep keep = keepsService.GetKeepById(keepId);
            return Ok(keep);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [HttpPut("{keepId}")]
    public async ActionResult<Keep> UpdateKeep([FromBody] Keep updateData, int keepId)
    {
        try
        {
            // Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);

            updateData.Id = keepId;
            Keep update = keepsService.UpdateKeep(updateData);
            return Ok(update);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}