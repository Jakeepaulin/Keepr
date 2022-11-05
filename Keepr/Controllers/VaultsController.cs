namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{

  private readonly VaultsService _vs;
  private readonly KeepsService _ks;
  private readonly Auth0Provider _auth0provider;

  public VaultsController(VaultsService vs, KeepsService ks, Auth0Provider auth0provider)
  {
    _vs = vs;
    _ks = ks;
    _auth0provider = auth0provider;
  }

  [HttpGet("{vaultId}")]
  public ActionResult<Vault> GetVaultById(int vaultId)
  {
    try
    {
      Vault vault = _vs.GetVaultById(vaultId);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

    [HttpGet("{vaultId}/keeps")]
  public ActionResult<List<Keep>> GetKeepsByVaultId(int vaultId)
  {
    try
    {
      List<Keep> keep = _ks.GetKeepsByVaultId(vaultId);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault newVault)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null){
        throw new Exception("You are a bad user!");
      }
      newVault.CreatorId = userInfo?.Id;
      Vault createdVault = _vs.CreateVault(newVault);
      return Ok(createdVault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [Authorize]
  [HttpPut("{vaultId}")]
  public async Task<ActionResult<Vault>> UpdateVault([FromBody] Vault vaultData, int vaultId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      vaultData.Id = vaultId;
      Vault updatedVault = _vs.UpdateVault(vaultData, userInfo?.Id);
      return Ok(updatedVault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


[Authorize]
[HttpDelete("{vaultId}")]
public async Task<ActionResult<string>> DeleteVault(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      _vs.DeleteVault(vaultId, userInfo?.Id);
      return Ok("Vault has been deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


}