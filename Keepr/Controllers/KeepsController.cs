namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{

  private readonly KeepsService _ks;
  private readonly Auth0Provider _auth0provider;

  public KeepsController(KeepsService ks, Auth0Provider auth0provider)
  {
    _ks = ks;
    _auth0provider = auth0provider;
  }

  [HttpGet]
  public ActionResult<List<Keep>> GetAllKeeps()
  {
    try
    {
      List<Keep> keeps = _ks.GetAllKeeps();
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{keepId}")]
  public ActionResult<Keep> GetKeepById(int keepId)
  {
    try
    {
      Keep keep = _ks.GetKeepById(keepId);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep newKeep)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null){
        throw new Exception("You are a bad user!");
      }
      newKeep.CreatorId = userInfo?.Id;
      Keep createdKeep = _ks.CreateKeep(newKeep);
      return Ok(createdKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [Authorize]
  [HttpPut("{keepId}")]
  public async Task<ActionResult<Keep>> UpdateKeep([FromBody] Keep keepData, int keepId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      keepData.Id = keepId;
      Keep updatedKeep = _ks.UpdateKeep(keepData, userInfo?.Id);
      return Ok(updatedKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


[Authorize]
[HttpDelete("{keepId}")]
public async Task<ActionResult<string>> DeleteKeep(int keepId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      _ks.DeleteKeep(keepId, userInfo?.Id);
      return Ok("Keep has been deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


}