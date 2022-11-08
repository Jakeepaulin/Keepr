namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{

  private readonly VaultKeepsService _vs;
  private readonly Auth0Provider _auth0provider;

  public VaultKeepsController(VaultKeepsService vs, Auth0Provider auth0provider)
  {
    _vs = vs;
    _auth0provider = auth0provider;
  }

[HttpGet("{vaultKeepId}")]
  public ActionResult<VaultKeep> GetVaultKeepById(int vaultKeepId)
  {
    try
    {
      VaultKeep vaultKeep = _vs.GetVaultKeepById(vaultKeepId);
      return Ok(vaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep newVaultKeep)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null){
        throw new Exception("You are a bad user!");
      }
      if (newVaultKeep == null){
        throw new Exception("Bad Request");
      }
      newVaultKeep.CreatorId = userInfo.Id;
      VaultKeep createdVaultKeep = _vs.CreateVaultKeep(newVaultKeep);
      return Ok(createdVaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


[Authorize]
[HttpDelete("{vaultKeepId}")]
public async Task<ActionResult<string>> DeleteVaultKeep(int vaultKeepId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      _vs.DeleteVaultKeep(vaultKeepId, userInfo?.Id);
      return Ok("VaultKeep has been deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


}